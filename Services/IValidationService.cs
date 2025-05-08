using Dummy.Models;

namespace Dummy.Services
{
    public interface IValidationService
    {
        bool ValidateProduct(Product product, out string errorMessage);
    }
} 