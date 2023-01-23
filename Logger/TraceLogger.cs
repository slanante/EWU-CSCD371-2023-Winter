using System;
using System.IO;
using System.Diagnostics;

namespace Logger
{
    public class TraceLogger : BaseLogger
    {
        private string _FilePath { get; }
        public TraceLogger(string filePath)
        {
            _FilePath = filePath;
        }

        public override void Log(LogLevel logLevel, string message)
        {
            if (!string.IsNullOrEmpty(_FilePath))
            {
                Trace.WriteLine("Added Log: " + $"{logLevel}");
                File.AppendAllText(_FilePath, $"{DateTime.Now:MM/dd/yyyy HH:mm:ss tt} " +
                    $"{nameof(ClassName)} {logLevel}: {message} {Environment.NewLine}");
                Trace.WriteLine("Log Succesfully Added");
            }
            else
            {
                throw new ArgumentException("File path is null or empty");
                
            }

        }
    }
}