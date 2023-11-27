using LogManager.Core.Models;
using LogManager.Core.Repositories;

namespace LogManager.Data.Repositories;

public class LogMessageRepository : Repository<LogMessage>, ILogMessageRepository
{
 

    /// <summary>
    /// Initializes a new instance of the LogMessageRepository class.
    /// </summary>
    /// <param name="logDbContext">The database contextFactory.</param>
    /// <exception cref="ArgumentNullException">Thrown if the provided contextFactory is null.</exception>
    public LogMessageRepository(LogDbContext logDbContext) : base(logDbContext)
    {
    }
}