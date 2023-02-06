using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logger.Tests;

[TestClass]
public class StorageTests
{
    [TestMethod]
    public void Adding_Entities_Employee()
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

    [TestMethod]
    public void Adding_Entities_Student()
    {
        // Arrange

        Storage storage = new();
        Student entity = new Student
        {
            Id = Guid.NewGuid(),
            FullName = new FullName("Foo", "Bar"),
            StudentYear = StudentLevels.Junior
        };

        // Act

        storage.Add(entity);

        // Assert

        Assert.IsTrue(storage.Contains(entity));
    }

    [TestMethod]
    public void Adding_Entities_Book()
    {
        // Arrange

        FullName author = new FullName("John", "Snow");

        Storage storage = new();
        Book entity = new Book
        {
            Id = Guid.NewGuid(),
            BookName = "Something About Game of Thrones",
            Author = author,
            ISBN = "9781004367239"
        };

        // Act

        storage.Add(entity);

        // Assert

        Assert.IsTrue(storage.Contains(entity));
    }

    [TestMethod]
    public void Removing_Entities_General()
    {
        // Arrange

        Storage storage = new();
        Student entity = new Student
        {
            Id = Guid.NewGuid(),
            FullName = new FullName("Rand", "Person"),
            StudentYear = StudentLevels.Senior
        };

        // Act

        storage.Add(entity);
        storage.Remove(entity);

        // Assert

        Assert.IsFalse(storage.Contains(entity));
    }
}