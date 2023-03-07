public interface ISampleData
{
    IEnumerable<string> CsvRows { get; }
    IEnumerable<string> GetUniqueSortedListOfStatesGivenCsvRows();
}