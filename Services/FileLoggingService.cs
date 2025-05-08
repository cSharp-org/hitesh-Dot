using System;
using System.IO;

namespace Dummy.Services
{
    public class FileLoggingService : ILoggingService
    {
        private readonly string _logPath;

        public FileLoggingService(string logPath = "app.log")
        {
            _logPath = logPath;
        }

        public void LogInfo(string message)
        {
            WriteLog($"INFO: {message}");
        }

        public void LogError(string message, Exception ex = null)
        {
            string errorMessage = $"ERROR: {message}";
            if (ex != null)
            {
                errorMessage += $"\nException: {ex.Message}\nStackTrace: {ex.StackTrace}";
            }
            WriteLog(errorMessage);
        }

        private void WriteLog(string message)
        {
            try
            {
                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}\n";
                File.AppendAllText(_logPath, logEntry);
            }
            catch { /* Suppress logging errors */ }
        }
    }
} 