namespace LogManager.Core.Models;

public class LogMessage
{
    public int LogMessageId { get; set; }
    public DateTime LogDate { get; set; }
    public int ApplicationId { get; set; } 
    public string MessageLog { get; set; } 
    public string LogLevel { get; set; }
    public Application Application { get; set; } 
}

public enum LogLevel
{
    Info,
    Trace,
    Debug,
    Warn,
    Error
}
