using System;

namespace Dummy.Services
{
    public class EmailNotificationService : BaseService, INotificationService
    {
        private readonly IEmailService _emailService;

        public EmailNotificationService(ILoggingService logger, IEmailService emailService) 
            : base(logger)
        {
            _emailService = emailService;
        }

        public void NotifyProductCreated(int productId, string productName)
        {
            LogServiceCall(nameof(NotifyProductCreated));
            try
            {
                string message = $"New product created - ID: {productId}, Name: {productName}";
                _emailService.SendEmail("admin@example.com", "Product Created", message);
            }
            catch (Exception ex)
            {
                LogServiceError(nameof(NotifyProductCreated), ex);
                throw;
            }
        }

        public void NotifyProductUpdated(int productId, string productName)
        {
            LogServiceCall(nameof(NotifyProductUpdated));
            try
            {
                string message = $"Product updated - ID: {productId}, Name: {productName}";
                _emailService.SendEmail("admin@example.com", "Product Updated", message);
            }
            catch (Exception ex)
            {
                LogServiceError(nameof(NotifyProductUpdated), ex);
                throw;
            }
        }

        public void NotifyProductDeleted(int productId)
        {
            LogServiceCall(nameof(NotifyProductDeleted));
            try
            {
                string message = $"Product deleted - ID: {productId}";
                _emailService.SendEmail("admin@example.com", "Product Deleted", message);
            }
            catch (Exception ex)
            {
                LogServiceError(nameof(NotifyProductDeleted), ex);
                throw;
            }
        }
    }
} 