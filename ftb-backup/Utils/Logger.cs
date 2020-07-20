using System;
using System.IO;
using System.Text;

namespace ftb_backup.Utils
{
    public class Logger: ILogger
    {
        private StringBuilder _builder;
        private string _filePath;
        private string _logDirectory;

        public Logger()
        {
            _logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "logs");
            _filePath = Path.Combine(_logDirectory, $"log_{getTimestamp()}.txt");
            _builder = new StringBuilder();
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
            _builder.AppendLine(message);
        }

        public void Log(Exception ex)
        {
            while (ex != null)
            {
                Log(ex.GetType().FullName);
                Log("Message : " + ex.Message);
                Log("StackTrace : " + ex.StackTrace);

                ex = ex.InnerException;
            }
            Log("=======================================");
            Log($"A log file was written to {_filePath}");
            Log("=======================================");

            if (!Directory.Exists(_logDirectory))
            {
                Directory.CreateDirectory(_logDirectory);
            }
            File.WriteAllText(_filePath, _builder.ToString());
        }

        private string getTimestamp()
        {
            return DateTime.Now.ToString("MM-dd-yyyy-hh_mm");
        }
    }
}
