namespace Calculate
{
    public class Calculator
    {
        public static Func<int, int, int> Add { get; set; } = (v1, v2) => v1 + v2;
        public static Func<int, int, int> Subtract { get; set; } = (v1, v2) => v1 - v2;
        public static Func<int, int, int> Multiply { get; set; } = (v1, v2) => v1 * v2;
        public static Func<int, int, int> Divide { get; set; } = (v1, v2) => v1 / v2;

        public static IReadOnlyDictionary<char, Func<int, int, int>> MathematicalOperations { get; } =
            new Dictionary<char, Func<int, int, int>>()
            {
                {'+', Add},
                {'-', Subtract},
                {'*', Multiply},
                {'/', Divide}
            };

        public static bool TryCalculate(string input, out int result)
        {
            result = 0;

            string[] parts = input.Split(' ');

            if (parts.Length != 3)
            {
                return false;
            }

            int v1, v2;
            char symbol;



            if (!int.TryParse(parts[0], out v1) ||
                !int.TryParse(parts[2], out v2) ||
                !char.TryParse(parts[1], out symbol))
            {
                return false;
            }

            if (!Calculator.MathematicalOperations.TryGetValue(symbol, out var operation))
            {
                return false;
            }

            result = operation(v1, v2);
            return true;
        }

    }
}
