using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logger.Tests;

[TestClass]
public class FullNameTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void FullName_NullFirst_NullException()
    {
        // Arrange

        string? name = null;

        // Act
        
        Employee entity = new()
        {
            Id = Guid.NewGuid(),
            FullName = new FullName(name!, "Stevens"),
            Employer = "Random Name"
        }; 
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void FullName_NullLast_NullException()
    {
        // Arrange

        string? name = null;

        // Act

        Employee entity = new()
        {
            Id = Guid.NewGuid(),
            FullName = new FullName("Jeff", name!),
            Employer = "Different Name"
        };
    }

    [TestMethod]
    public void FullName_Creation_ReturnsName()
    {
        // Arrange

        string name = "John Green Smith";

        // Act

        Student student = new()
        {
            Id = Guid.NewGuid(),
            FullName = new FullName("John", "Smith", "Green"),
            StudentYear = StudentLevels.Freshman
        };

        // Assert

        Assert.AreEqual(name, student.FullName.Name);
    }
}
