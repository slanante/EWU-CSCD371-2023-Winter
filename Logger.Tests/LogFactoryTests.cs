using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Logger.Tests
{
    [TestClass]
    public class LogFactoryTests
    {
        [TestMethod]
        public void LogFactory_ObjecyInitialize()
        {
            var logger = new LogFactory();
            var logger_object = logger.CreateLogger("Sample Class");

            // Assert
           Assert.IsInstanceOfType(logger_object, typeof(ConcreteLogger));
        }
    }
}
