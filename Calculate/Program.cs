namespace Calculate
{
    class Program
    {
        public Func<string>? ReadLine { get; init; } = Console.ReadLine;
        public Action<string>? WriteLine { get; init; } = Console.WriteLine;

        public Program()
        {

        }
    }
}
