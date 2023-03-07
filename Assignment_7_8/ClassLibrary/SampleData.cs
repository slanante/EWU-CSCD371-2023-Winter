using System.IO;

public class SampleData : ISampleData
{
    public IEnumerable<string> CsvRows => File.ReadLines("People.csv").Skip(1);
    public IEnumerable<string> GetUniqueSortedListOfStatesGivenCsvRows()
    {
        return CsvRows
            .Select(row => row.Split(',')[6].Trim()) // select state from each row
            .Distinct() // filter out duplicates
            .OrderBy(state => state); // sort the resulting list
    }
}