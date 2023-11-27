using LogManager.Core.Models;
using LogManager.Core.Repositories;
using LogManager.Core.Services;
using System.Text.RegularExpressions;
using Application = LogManager.Core.Models.Application;

namespace LogManager.Services.Services;

public class LogMessageService : ILogMessageServices
{

    private readonly IApplicationRepository _applicationRepository;
    private readonly ILogMessageRepository _messageRepository;

    public LogMessageService(IApplicationRepository applicationRepository, ILogMessageRepository messageRepository)
    {
        _applicationRepository = applicationRepository ?? throw new ArgumentNullException(nameof(applicationRepository));
        _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
    }


    /// <inheritdoc cref="ILogMessageServices.CreateLogMessage"/>
    public async Task<bool> CreateLogMessage(List<LogMessageRequest>? newLogMessage, CancellationToken cancellationToken)
    {
        if (newLogMessage == null || !newLogMessage.Any())
            return false;

        var groupedByApplicationName = newLogMessage.GroupBy(x => x.Application).ToList();

        foreach (var newApplication in groupedByApplicationName.Select(app => new Application { Name = app.Key }))
        {
            await _applicationRepository.AddAsync(newApplication, cancellationToken);
        }

        await _applicationRepository.CommitChangeAsync(cancellationToken);

        var applicationDictionary = await _applicationRepository.GetIdsByApplicationNames(groupedByApplicationName.Select(x => x.Key).ToList());

        var tasks = groupedByApplicationName.Select(group =>
        {
            return Task.Run(async () =>
            {

                if (applicationDictionary.TryGetValue(group.Key, out int applicationId))
                {
                    foreach (var item in group.ToList())
                    {
                        var (logLevel, message) = ParseLogMessage(item.Message);
                        var newLog = new LogMessage
                        {
                            ApplicationId = applicationId,
                            LogDate = DateTimeOffset.FromUnixTimeSeconds(item.Logdate).UtcDateTime,
                            MessageLog = message,
                            LogLevel = logLevel.ToString()
                        };

                        await _messageRepository.AddAsync(newLog, cancellationToken);
                    }
                }

            }, cancellationToken);
        });

        await Task.WhenAll(tasks);
        await _messageRepository.CommitChangeAsync(cancellationToken);
        return true;
    }

    private (LogLevel LogLevel, string Message) ParseLogMessage(string logMessage)
    {
        LogLevel logLevel = LogLevel.Info;
        var message = logMessage.Trim();

        Regex logLevelRegex = new Regex(@"^\s*\[(\w+)\]\s*(.*)$", RegexOptions.IgnoreCase);
        Match match = logLevelRegex.Match(logMessage);

        if (match.Success)
        {
            var potentialLogLevel = match.Groups[1].Value.Trim();
            if (!Enum.TryParse(potentialLogLevel, true, out logLevel))
                logLevel = LogLevel.Info;

            message = match.Groups[2].Value.Trim();
        }
        return (logLevel, message);
    }
}

