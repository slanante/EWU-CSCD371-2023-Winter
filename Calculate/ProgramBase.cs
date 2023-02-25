namespace Calculate
{
    public abstract class ProgramBase
    {
        public Action<string> WriteLine { get; init; } = Console.WriteLine;
        public Func<string?> ReadLine { get; init; } = Console.ReadLine;
    }
}