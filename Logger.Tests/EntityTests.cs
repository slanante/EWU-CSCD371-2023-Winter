using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logger.Tests;

[TestClass]
public class EntityTests
{
    [TestMethod]
    public void Students_With_Same_Properties_Are_Equal()
    {
        Student student1 = new Student
        {
            Id = Guid.NewGuid(),
            FullName = new FullName("Spongebob", "SquarePants"),
            StudentYear = "Freshman"
        };

        Student student2 = new Student
        {
            Id = student1.Id,
            FullName = student1.FullName,
            StudentYear = student1.StudentYear
        };

        Assert.AreEqual(student1, student2);
    }

    [TestMethod]
    public void Books_With_Same_Properties_Are_Equal()
    {
        FullName author = new FullName("Eric", "Carle");

        Book book1 = new Book
        {
            Id = Guid.NewGuid(),
            BookName = "The Very Hungry Caterpillar",
            Author = author,
            ISBN = "9780399213014"
        };

        Book book2 = new Book
        {
            Id = book1.Id,
            BookName = book1.BookName,
            Author = book1.Author,
            ISBN = book1.ISBN
        };

        Assert.AreEqual(book1, book2);
        Assert.AreEqual(book1.Name, book2.Name);
    }

    [TestMethod]
    public void Employees_With_Same_Properties_Are_Equal()
    {
        Employee employee1 = new Employee
        {
            Id = Guid.NewGuid(),
            FullName = new FullName("Colonel", "Sanders"),
            Employer = "Kentucky Fried Chicken"
        };

        Employee employee2 = new Employee
        {
            Id = employee1.Id,
            FullName = new FullName("Colonel", "Sanders"),
            Employer = "Kentucky Fried Chicken"
        };

        Assert.AreEqual(employee1, employee2);
    }

}