namespace SampleDataTest;

[TestClass]
public class SampleDataTest
{
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

}