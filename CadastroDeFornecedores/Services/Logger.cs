using System;
using System.IO;

public static class Logger
{
    private static readonly string _logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs.txt");

    public static void Log(string message, string tipo = "INFO")
    {
        try
        {
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{tipo}] {message}";
            File.AppendAllText(_logPath, logEntry + Environment.NewLine);
        }
        catch (Exception ex)
        {
            // Fallback: Se falhar, exibe no console (útil durante desenvolvimento)
            Console.WriteLine($"Falha ao escrever no log: {ex.Message}");
        }
    }
}