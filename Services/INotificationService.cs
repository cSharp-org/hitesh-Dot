namespace Dummy.Services
{
    public interface INotificationService
    {
        void NotifyProductCreated(int productId, string productName);
        void NotifyProductUpdated(int productId, string productName);
        void NotifyProductDeleted(int productId);
    }
} 