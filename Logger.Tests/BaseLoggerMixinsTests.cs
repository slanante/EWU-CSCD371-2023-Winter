using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Logger.Tests
{
    [TestClass]
    public class BaseLoggerMixinsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Error_WithNullLogger_ThrowsException()
        {
            // Arrange

            // Act
            BaseLoggerMixins.Error(null, "");

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Debug_WithNullLogger_ThrowsException()
        {
            BaseLoggerMixins.Debug(null, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Warning_WithNullLogger_ThrowsException()
        {
            BaseLoggerMixins.Warning(null, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Information_WithNullLogger_ThrowsException()
        {
            BaseLoggerMixins.Information(null, "");
        }

        [TestMethod]
        public void Error_WithData_LogsMessage()
        {
            // Arrange
            var logger = new TestLogger();

            // Act
            logger.Error("Message {0}", 42);

            // Assert
            Assert.AreEqual(1, logger.LoggedMessages.Count);
            Assert.AreEqual(LogLevel.Error, logger.LoggedMessages[0].LogLevel);
            Assert.AreEqual("Message 42", logger.LoggedMessages[0].Message);
        }

        [TestMethod]
        public void Error_WithData_NoLogger()
        {
            try
            {
                // Arrange
                var logger = new TestLogger();
                logger.Error(null, 42);
                // Act

                // assert (this line should not be reached)
                Assert.Fail("An exception should have been thrown.");
            }
            catch (ArgumentNullException ex)
            {
                //assert
                Assert.IsTrue(ex.Message.Contains("Value cannot be null. (Parameter 'format')"));
            }
        }

        [TestMethod]
        public void Warning_WithData_NoLogger()
        {
            try
            {
                // Arrange
                var logger = new TestLogger();
                logger.Warning(null, 42);
                // Act

                // assert (this line should not be reached)
                Assert.Fail("An exception should have been thrown.");
            }
            catch (ArgumentNullException ex)
            {
                //assert
                Assert.IsTrue(ex.Message.Contains("Value cannot be null. (Parameter 'format')"));
            }
        }

        [TestMethod]
        public void Debug_WithData_NoLogger()
        {
            try
            {
                // Arrange
                var logger = new TestLogger();
                logger.Debug(null, 42);
                // Act

                // assert (this line should not be reached)
                Assert.Fail("An exception should have been thrown.");
            }
            catch (ArgumentNullException ex)
            {
                //assert
                Assert.IsTrue(ex.Message.Contains("Value cannot be null. (Parameter 'format')"));
            }
        }

        [TestMethod]
        public void Information_WithData_NoLogger()
        {
            try
            {
                // Arrange
                var logger = new TestLogger();
                logger.Information(null, 42);
                // Act

                // assert (this line should not be reached)
                Assert.Fail("An exception should have been thrown.");
            }
            catch (ArgumentNullException ex)
            {
                //assert
                Assert.IsTrue(ex.Message.Contains("Value cannot be null. (Parameter 'format')"));
            }
        }

    }

    

    public class TestLogger : BaseLogger
    {
        public List<(LogLevel LogLevel, string Message)> LoggedMessages { get; } = new List<(LogLevel, string)>();

        public override void Log(LogLevel logLevel, string message)
        {
            LoggedMessages.Add((logLevel, message));
        }
    }
}
