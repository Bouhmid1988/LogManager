namespace LogManager.Core.Models;

public class LogMessageRequest 
{
    public int Logdate { get; set; }
    public string Application { get; set; }
    public string Message { get; set; }
}

