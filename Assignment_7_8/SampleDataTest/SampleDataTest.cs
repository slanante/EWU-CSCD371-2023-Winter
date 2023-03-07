using InterfaceLibrary;

namespace SampleDataTest;

[TestClass]
public class SampleDataTest
{
    private List<string> newList = new()
    {
        "1,Isabella,Johnson,ijohnson6@amazon.com,1275 Kinnear Rd,Columbus,OH,43212",
        "2,Bryce,Miller,bmiller7@twitter.com,1600 Amphitheatre Pkwy,Mountain View,CA,94043",
        "3,Aiden,Roberts,aroberts8@yelp.com,123 Main St,Philadelphia,PA,19103",
        "4,Abigail,Lee,alee9@google.com,1600 Pennsylvania Ave NW,Washington,DC,20500"
    };

    [TestMethod]
    public void TestCsvRows()
    {
        // Arrange
        ISampleData sampleData = new SampleData();

        // Act
        var rows = sampleData.CsvRows.ToArray();

        // Assert
        Assert.AreEqual(50, rows.Length); // expect 50 rows (excluding header)
        Assert.IsFalse(rows.Contains("Id,FirstName,LastName,Email,StreetAddress,City,State,Zip")); // expect skipped header row
    }

    [TestMethod]
    public void TestUniqueSortedListOfStates()
    {
        // Arrange
        ISampleData sampleData = new SampleData();

        // Act
        var states = sampleData.GetUniqueSortedListOfStatesGivenCsvRows().ToArray();

        // Assert
        Assert.IsTrue(states.SequenceEqual(states.OrderBy(s => s))); // expect the list to be sorted alphabetically
    }

    [TestMethod]
    public void AggregateSortedList()
    {
        // Arrange
        ISampleData sampleData = new SampleData();
        string expected = "AL,CA,IL,NY";

        // Act
        string actual = sampleData.GetAggregateSortedListOfStatesUsingCsvRows();

        // Assert
        Assert.AreEqual<string>(expected, actual);
    }


}