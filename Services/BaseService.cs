using System;

namespace Dummy.Services
{
    public abstract class BaseService
    {
        protected readonly ILoggingService _logger;

        protected BaseService(ILoggingService logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected virtual void LogServiceCall(string methodName)
        {
            _logger.LogInfo($"Service method called: {methodName}");
        }

        protected virtual void LogServiceError(string methodName, Exception ex)
        {
            _logger.LogError($"Error in service method: {methodName}", ex);
        }
    }
} 