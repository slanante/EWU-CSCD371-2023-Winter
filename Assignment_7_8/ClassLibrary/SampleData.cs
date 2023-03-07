using System.IO;
using InterfaceLibrary;

public class SampleData : ISampleData
{
    public IEnumerable<string> CsvRows => File.ReadLines("People.csv").Skip(1);
    public IEnumerable<string> GetUniqueSortedListOfStatesGivenCsvRows()
    {
        return CsvRows
            .Select(row => row.Split(',')[6])
            .Distinct()
            .OrderBy(state => state);
    }

    public string GetAggregateSortedListOfStatesUsingCsvRows()
    {
        var states = GetUniqueSortedListOfStatesGivenCsvRows().ToArray();

        return string.Join(",", states);
    }
}