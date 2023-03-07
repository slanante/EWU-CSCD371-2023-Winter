namespace InterfaceLibrary;
public interface ISampleData
{
    IEnumerable<string> CsvRows { get; }
    IEnumerable<string> GetUniqueSortedListOfStatesGivenCsvRows();
    string GetAggregateSortedListOfStatesUsingCsvRows();
    IEnumerable<IPerson> People { get; }
}