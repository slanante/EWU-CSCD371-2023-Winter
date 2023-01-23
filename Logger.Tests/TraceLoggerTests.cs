using System.IO;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Logger.Tests
{
    [TestClass]
    public class TraceLoggerTests
    {
        [TestMethod]
        public void Trace_WriteIntoFile()
        {
            // Arrange
            string filePath = "C:\\Users\\Public\\Documents\\example.txt";
            var logger = new TraceLogger(filePath) { ClassName = nameof(TraceLoggerTests) };

            // Act
            logger.Log(LogLevel.Information, "Here be some Info, yarr!");

            // Assert
            string[] content = File.ReadAllLines(filePath);
            Trace.Assert(content[0].Contains("Here be some Info, yarr!"));
            File.Delete(filePath);
        }
        [TestMethod]
        public void Trace_WriteIntoFileMultiple()
        {
            //Should print each time logs are written to the file
            // Arrange
            string filePath = "C:\\Users\\Public\\Documents\\example.txt";
            var logger = new TraceLogger(filePath) { ClassName = nameof(TraceLoggerTests) };

            // Act
            logger.Log(LogLevel.Information, "Here be some Info, yarr!");
            logger.Log(LogLevel.Warning, "Don't touch my treasure!");

            // Assert
            string[] content = File.ReadAllLines(filePath);
            Assert.AreEqual(2, content.Length);
            File.Delete(filePath);
        }
        [TestMethod]
        public void No_FileForTrace_CatchError()
        {
            string filePath = null;
            try
            {
                // Arrange
                var logger = new TraceLogger(filePath) { ClassName = nameof(TraceLoggerTests) };

                // Act
                logger.Log(LogLevel.Information, "Here be some Info, yarr!");


                // assert (this line should not be reached)
                Assert.Fail("An exception should have been thrown.");
            }
            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.Contains("File path is null or empty"));
            }


        }

    }
}