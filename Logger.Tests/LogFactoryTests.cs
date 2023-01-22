using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Logger.Tests
{
    [TestClass]
    public class LogFactoryTests
    {
        [TestMethod]
        public void LogFactory_ObjectInitialize()
        {
            var logger = new LogFactory();
            string exampleFilePath = "C:\\Users\\Public\\Documents\\example.txt";
            logger.ConfigureFileLogger(exampleFilePath);
            BaseLogger logger_object = logger.CreateLogger(nameof(LogFactoryTests));

            Assert.IsTrue(logger_object is FileLogger);
        }

        [TestMethod]
        public void LogFactory_NoFilePath_CreateLoggerNull()
        {
            var logger = new LogFactory();
            BaseLogger logger_object = logger.CreateLogger(nameof(LogFactoryTests));
            Assert.IsNull(logger_object);
        }
    }
}
