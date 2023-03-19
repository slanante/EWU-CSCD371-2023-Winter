﻿using IntelliTect.TestTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Assignment.Tests;



[TestClass]
public class PingProcessTests
{
    PingProcess Sut { get; set; } = new();

    [TestInitialize]
    public void TestInitialize()
    {
        Sut = new();
    }

    [TestMethod]
    public void Start_PingProcess_Success()
    {
        Process process = Process.Start("ping", "localhost");
        process.WaitForExit();
        Assert.AreEqual<int>(0, process.ExitCode);
    }

    [TestMethod]
    public void Run_GoogleDotCom_Success()
    {
        int exitCode = Sut.Run("google.com").ExitCode;
        Assert.AreEqual<int>(0, exitCode);
    }


    [TestMethod]
    public void Run_InvalidAddressOutput_Success()
    {
        (int exitCode, string? stdOutput) = Sut.Run("badaddress");
        Assert.IsFalse(string.IsNullOrWhiteSpace(stdOutput));
        stdOutput = WildcardPattern.NormalizeLineEndings(stdOutput!.Trim());
        Assert.AreEqual<string?>(
            "Ping request could not find host badaddress. Please check the name and try again.".Trim(),
            stdOutput,
            $"Output is unexpected: {stdOutput}");
        Assert.AreEqual<int>(1, exitCode);
    }

    [TestMethod]
    public void Run_CaptureStdOutput_Success()
    {
        PingResult result = Sut.Run("localhost");
        AssertValidPingOutput(result);
    }

    [TestMethod]
    public void RunTaskAsync_Success()
    {
        var task = Sut.RunTaskAsync("localhost");
        var result = task.Result;
        task.Status.ToString();

        Assert.AreEqual(0, result.ExitCode);
        Assert.IsNotNull(result.StdOutput);
        // Do NOT use async/await in this test.
        // Test Sut.RunTaskAsync("localhost");
    }

    [TestMethod]
    public void RunAsync_UsingTaskReturn_Success()
    {
        // Do NOT use async/await in this test.
        // Test Sut.RunAsync("localhost");
        Task<PingResult> result = Sut.RunAsync("localhost");
        AssertValidPingOutput(result.Result);
    }

    [TestMethod]
    async public Task RunAsync_UsingTpl_Success()
    {
        // DO use async/await in this test.
        PingResult result = await Sut.RunAsync("localhost");

        // Test Sut.RunAsync("localhost");
        AssertValidPingOutput(result);
    }

    [TestMethod]
    public void RunAsync_UsingTplWithCancellation_CatchAggregateExceptionWrapping()
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;

        cts.Cancel();
        Task<PingResult> result = Sut.RunAsync(new[] { "localhost" }, token);

        try
        {
            result.Wait();
            Assert.Fail("Expected an exception to be thrown");
        }
        // Here we catch the AggregateException
        catch (AggregateException ex)
        {
            Assert.AreEqual(1, ex.InnerExceptions.Count, "Expected a single inner exception");
            Assert.IsInstanceOfType(ex.InnerExceptions[0], typeof(TaskCanceledException), "Expected a TaskCanceledException");
        }

    }

    [TestMethod]
    [ExpectedException(typeof(TaskCanceledException))]
    public void RunAsync_UsingTplWithCancellation_CatchAggregateExceptionWrappingTaskCanceledException()
    {
        CancellationTokenSource cts = new();
        CancellationToken cancellationToken = cts.Token;
        cts.Cancel();
        Task<PingResult> result = Sut.RunAsync(new[] { "localhost" }, cancellationToken);
        cts.Dispose();
        try
        {
            result.Wait();
        }
        catch (AggregateException exception)
        {
            throw exception.Flatten().InnerException!;
        }
        // Use exception.Flatten()
    }

    [TestMethod]
    async public Task RunAsync_MultipleHostAddresses_True()
    {
        // Pseudo Code - don't trust it!!!
        string[] hostNames = new string[] { "localhost", "localhost", "localhost", "localhost" };
        int expectedLineCount = PingOutputLikeExpression.Split(Environment.NewLine).Length * hostNames.Length - hostNames.Length;
        PingResult result = await Sut.RunAsync(hostNames);
        int? lineCount = result.StdOutput?.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Length;
        Assert.AreEqual(expectedLineCount, lineCount);
    }

    [TestMethod]
    async public Task RunLongRunningAsync_UsingTpl_Success()
    {
        ProcessStartInfo startInfo = new("ping");
        startInfo.Arguments = "localhost";

        int exitCode = await Sut.RunLongRunningAsync(startInfo);
        Assert.AreEqual(0, exitCode, "Expected a successful ping operation");
    }

    [TestMethod]
    public void StringBuilderAppendLine_InParallel_IsNotThreadSafe()
    {
        IEnumerable<int> numbers = Enumerable.Range(0, short.MaxValue);
        System.Text.StringBuilder stringBuilder = new();
        numbers.AsParallel().ForAll(item => stringBuilder.AppendLine(""));
        int lineCount = stringBuilder.ToString().Split(Environment.NewLine).Length;
        Assert.AreNotEqual(lineCount, numbers.Count() + 1);
    }

    /*
    Important Note for Code Reviewers:
    The RunAsync_WithProgress_Success() TestMethod
    sometimes fails, unsure how to fix so that
    it always succeeds
    */

    [TestMethod]
    public async Task RunAsync_WithProgress_Success()
    {

        List<string?> capturedOutput = new List<string?>();
        Progress<string?> progressHandler = new Progress<string?>(line => capturedOutput.Add(line));

        PingResult result = await Sut.RunAsync("localhost", progressHandler);

        string? joinedCapturedOutput = string.Join(Environment.NewLine, capturedOutput) + Environment.NewLine;
        Assert.AreEqual(result.StdOutput, joinedCapturedOutput);
    }

    readonly string PingOutputLikeExpression = @"
Pinging * with 32 bytes of data:
Reply from ::1: time<*
Reply from ::1: time<*
Reply from ::1: time<*
Reply from ::1: time<*

Ping statistics for ::1:
    Packets: Sent = *, Received = *, Lost = 0 (0% loss),
Approximate round trip times in milli-seconds:
    Minimum = *, Maximum = *, Average = *".Trim();
    private void AssertValidPingOutput(int exitCode, string? stdOutput)
    {
        Assert.IsFalse(string.IsNullOrWhiteSpace(stdOutput));
        stdOutput = WildcardPattern.NormalizeLineEndings(stdOutput!.Trim());
        Assert.IsTrue(stdOutput?.IsLike(PingOutputLikeExpression) ?? false,
            $"Output is unexpected: {stdOutput}");
        Assert.AreEqual<int>(0, exitCode);
    }
    private void AssertValidPingOutput(PingResult result) =>
        AssertValidPingOutput(result.ExitCode, result.StdOutput);
}