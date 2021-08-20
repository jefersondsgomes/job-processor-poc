using Serilog;

namespace JobProcessorPoc
{
    public static class Logger
    {
        private static readonly ILogger _logger;

        static Logger()
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
        }

        public static void Log(string message)
        {
            _logger.Information(message);
        }
    }
}