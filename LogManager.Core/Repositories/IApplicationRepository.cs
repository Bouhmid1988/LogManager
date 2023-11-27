using LogManager.Core.Models;

namespace LogManager.Core.Repositories;

/// <summary>
/// Define the Command Request Application  Repository Interface
/// </summary>
public interface IApplicationRepository : IRepository<Application>
{
    Task<Dictionary<string, int>> GetIdsByApplicationNames(List<string> applicationNames);
}

