namespace Logger
{
    public class LogFactory
    {
        private string? _FilePath;

        public BaseLogger? CreateLogger(string className)
        {
            if (_FilePath is null) {return null;}
            return new FileLogger(_FilePath) {ClassName = className};

        }

        public void ConfigureFileLogger(string filePath)
        {
            _FilePath = filePath;
        }
    }
}
