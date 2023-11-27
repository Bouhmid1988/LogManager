using LogManager.Core.Models;

namespace LogManager.Core.Services;
   
/// <summary>
/// Define the Log Message Service Interface
/// </summary>
public interface ILogMessageServices
{
    /// <summary>
    /// Creates a new Log Message.
    /// </summary>
    /// <param name="newLogMessage">The new Log Message to create.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>The created Log Message.</returns>
    Task<bool> CreateLogMessage(List<LogMessageRequest> newLogMessage, CancellationToken cancellationToken);
}

