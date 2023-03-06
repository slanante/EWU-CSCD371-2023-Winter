using System.IO;

public class SampleData : ISampleData
{
    public IEnumerable<string> CsvRows => File.ReadLines("People.csv");
}