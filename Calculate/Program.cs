namespace Calculate
{
    public class Program
    {
        public Func<string?>? ReadLine { get; init; } = Console.ReadLine;
        public Action<string>? WriteLine { get; init; } = Console.WriteLine;

        public Program()
        {

        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.WriteLine!.Invoke("Perform a math calculation" +
            "\n(ensure there is a space between operand and operators)" +
            "\nTo quit, press Enter: \n");
            Calculator calculator = new Calculator();

            while (true)
            {
                string? input = program.ReadLine?.Invoke();

                if (string.IsNullOrEmpty(input))
                {
                    break;
                }

                if (Calculator.TryCalculate(input, out int result))
                {
                    program.WriteLine?.Invoke($"Result: {result}");
                }
                else
                {
                    program.WriteLine?.Invoke("Invalid input.");
                }
                
            }
            program.WriteLine!.Invoke("Finished.");
        }
    }
}
