﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment;

public record struct PingResult(int ExitCode, string? StdOutput);

public class PingProcess
{
    private ProcessStartInfo StartInfo { get; } = new("ping");

    public PingResult Run(string hostNameOrAddress)
    {
        StartInfo.Arguments = hostNameOrAddress;
        StringBuilder? stringBuilder = null;
        void updateStdOutput(string? line) =>
            (stringBuilder ??= new StringBuilder()).AppendLine(line);
        Process process = RunProcessInternal(StartInfo, updateStdOutput, default, default);
        return new PingResult(process.ExitCode, stringBuilder?.ToString());
    }

    public Task<PingResult> RunTaskAsync(string hostNameOrAddress)
    {
        return Task.Run(() => Run(hostNameOrAddress));
    }

    async public Task<PingResult> RunAsync(
        IEnumerable<string> hostNameOrAddresses, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        StringBuilder? stringBuilder = null;
        ParallelQuery<Task<int>> all = hostNameOrAddresses.AsParallel().Select(async item =>
        {
            cancellationToken.ThrowIfCancellationRequested();

            PingResult result = await RunTaskAsync(item).ConfigureAwait(false);
            lock (stringBuilder ??= new StringBuilder())
            {
                stringBuilder.AppendLine(result.StdOutput);
            }
            return result.ExitCode;
        });

        Task<int>[] allTasks = all.ToArray();
        int[] exitCodes = await Task.WhenAll(allTasks);
        int total = exitCodes.Sum();
        return new PingResult(total, stringBuilder?.ToString());
    }

    async public Task<PingResult> RunAsync(params string[] hostNameOrAddresses)
    {
        StringBuilder? stringBuilder = null;
        object lockObject = new object();
        ParallelQuery<Task<int>> all = hostNameOrAddresses.AsParallel().Select(async item =>
        {
            PingResult result = await RunTaskAsync(item).ConfigureAwait(false);
            lock (lockObject)
            {
                if (stringBuilder == null)
                {
                    stringBuilder = new StringBuilder();
                }
                stringBuilder.Append(result.StdOutput);
            }
            return result.ExitCode;
        });

        Task<int>[] allTasks = all.ToArray();
        int[] exitCodes = await Task.WhenAll(allTasks);
        int total = exitCodes.Sum();
        return new PingResult(total, stringBuilder?.ToString());
    }

    public Task<int> RunLongRunningAsync(ProcessStartInfo startInfo, Action<string?>? progressOutput = default, Action<string?>? progressError = default, CancellationToken token = default)
    {
        return Task.Factory.StartNew(() =>
        {
            Process process = RunProcessInternal(startInfo, progressOutput, progressError, token);
            return process.ExitCode;
        }, token, TaskCreationOptions.LongRunning, TaskScheduler.Current);
    }

    private Process RunProcessInternal(
        ProcessStartInfo startInfo,
        Action<string?>? progressOutput,
        Action<string?>? progressError,
        CancellationToken token)
    {
        var process = new Process
        {
            StartInfo = UpdateProcessStartInfo(startInfo)
        };
        return RunProcessInternal(process, progressOutput, progressError, token);
    }

    private Process RunProcessInternal(
        Process process,
        Action<string?>? progressOutput,
        Action<string?>? progressError,
        CancellationToken token)
    {
        process.EnableRaisingEvents = true;
        process.OutputDataReceived += OutputHandler;
        process.ErrorDataReceived += ErrorHandler;

        try
        {
            if (!process.Start())
            {
                return process;
            }

            token.Register(obj =>
            {
                if (obj is Process p && !p.HasExited)
                {
                    try
                    {
                        p.Kill();
                    }
                    catch (Win32Exception ex)
                    {
                        throw new InvalidOperationException($"Error cancelling process{Environment.NewLine}{ex}");
                    }
                }
            }, process);


            if (process.StartInfo.RedirectStandardOutput)
            {
                process.BeginOutputReadLine();
            }
            if (process.StartInfo.RedirectStandardError)
            {
                process.BeginErrorReadLine();
            }

            if (process.HasExited)
            {
                return process;
            }
            process.WaitForExit();
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Error running '{process.StartInfo.FileName} {process.StartInfo.Arguments}'{Environment.NewLine}{e}");
        }
        finally
        {
            if (process.StartInfo.RedirectStandardError)
            {
                process.CancelErrorRead();
            }
            if (process.StartInfo.RedirectStandardOutput)
            {
                process.CancelOutputRead();
            }
            process.OutputDataReceived -= OutputHandler;
            process.ErrorDataReceived -= ErrorHandler;

            if (!process.HasExited)
            {
                process.Kill();
            }

        }
        return process;

        void OutputHandler(object s, DataReceivedEventArgs e)
        {
            progressOutput?.Invoke(e.Data);
        }

        void ErrorHandler(object s, DataReceivedEventArgs e)
        {
            progressError?.Invoke(e.Data);
        }
    }

    public async Task<PingResult> RunAsync(string hostNameOrAddress, IProgress<string?>? progress = null)
    {
        StartInfo.Arguments = hostNameOrAddress;
        StringBuilder? stringBuilder = null;

        void updateStdOutput(string? line)
        {
            if (line != null)
            {
                progress?.Report(line);
                (stringBuilder ??= new StringBuilder()).AppendLine(line);
            }
        }

        int exitCode = await Task.Run(() =>
        {
            using Process process = RunProcessInternal(StartInfo, updateStdOutput, default, default);
            return process.ExitCode;
        }).ConfigureAwait(false);

        return new PingResult(exitCode, stringBuilder?.ToString());
    }

    private static ProcessStartInfo UpdateProcessStartInfo(ProcessStartInfo startInfo)
    {
        startInfo.CreateNoWindow = true;
        startInfo.RedirectStandardError = true;
        startInfo.RedirectStandardOutput = true;
        startInfo.UseShellExecute = false;
        startInfo.WindowStyle = ProcessWindowStyle.Hidden;

        return startInfo;
    }
}