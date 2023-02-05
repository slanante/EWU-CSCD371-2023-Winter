using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logger.Tests;

[TestClass]
public class StorageTests
{
    [TestMethod]
    public void Adding_Entities()
    {
        Storage storage = new Storage();
        Employee entity = new Employee
        {
            Id = Guid.NewGuid(),
            FullName = new FullName("Colonel","Sanders"),
            Employer = "Kentucky Fried Chicken"
        };

        // Act
        storage.Add(entity);

        // Assert
        Assert.IsTrue(storage.Contains(entity));
    }
}