using LogManager.Core.Models;
using LogManager.Core.Repositories;

namespace LogManager.Data.Repositories;

public class ApplicationRepository : Repository<Application>, IApplicationRepository
{


    /// <summary>
    /// Initializes a new instance of the Application Repository class.
    /// </summary>
    /// <param name="logDbContext">The database context.</param>
    /// <exception cref="ArgumentNullException">Thrown if the provided context is null.</exception>
    public ApplicationRepository(LogDbContext logDbContext) : base(logDbContext)
    {
    }

    public override async Task<Application> AddAsync(Application entity, CancellationToken cancellationToken)
    {
        var newApplication =   Find(x => x.Name.Equals(entity.Name)).FirstOrDefault() 
                  ?? await base.AddAsync(entity, cancellationToken);

        return newApplication;
    }
    public Task<Dictionary<string,int>> GetIdsByApplicationNames(List<string> applicationNames)
    {
        return  Task.FromResult(Find(x => applicationNames.Contains(x.Name)).ToDictionary(x => x.Name, x => x.Id));
    }
}