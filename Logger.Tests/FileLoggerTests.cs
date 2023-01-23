using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logger.Tests
{
    [TestClass]
    public class FileLoggerTests
    {
        [TestMethod]
        public void FileLogger_SampleFileWriteAndRead()
        {
            // Arrange
            string filePath = "C:\\Users\\Public\\Documents\\example.txt";
            var logger = new FileLogger(filePath) {ClassName = nameof(FileLoggerTests)};

            // Act
            logger.Log(LogLevel.Information, "Here be some Info, yarr!");

            // Assert
            string[] content = File.ReadAllLines(filePath);
            Assert.IsTrue(content[0].Contains("Here be some Info, yarr!"));
            File.Delete(filePath);
        }

        [TestMethod]
        public void FileLogger_SampleFileMultiLogs()
        {
            // Arrange
            string filePath = "C:\\Users\\Public\\Documents\\test.txt";
            var logger = new FileLogger(filePath) {ClassName = nameof(FileLoggerTests)};

            // Act
            logger.Log(LogLevel.Error, "Ya Dun Goofed!");
            logger.Log(LogLevel.Warning, "Ya Dun Goofed Part 2!");
            logger.Log(LogLevel.Information, "Top Secret Info!");
            logger.Log(LogLevel.Debug, "Hehe, bugs!");

            // Assert
            string[] content = File.ReadAllLines(filePath);
            Assert.AreEqual(4, content.Length);
            File.Delete(filePath);
        }
    }
}
