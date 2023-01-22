using System;
using System.IO;

namespace Logger
{
    public class FileLogger : BaseLogger
    {
        private string _FilePath { get; }
        public FileLogger(string filePath)
        {
            _FilePath = filePath;
        }

        public override void Log(LogLevel logLevel, string message)
        {
            if (!string.IsNullOrEmpty(_FilePath))
            {
                File.AppendAllText(_FilePath, $"{DateTime.Now:MM/dd/yyyy HH:mm:ss tt} " +
                    $"{nameof(ClassName)} {logLevel}: {message} {Environment.NewLine}");
            }
            else
            {
                throw new ArgumentException("File path is null or empty");
            }

        }
    }
}