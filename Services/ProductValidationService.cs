using Dummy.Models;

namespace Dummy.Services
{
    public class ProductValidationService : IValidationService
    {
        public bool ValidateProduct(Product product, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (product == null)
            {
                errorMessage = "Product cannot be null";
                return false;
            }

            if (string.IsNullOrWhiteSpace(product.Name))
            {
                errorMessage = "Product name is required";
                return false;
            }

            if (product.Price < 0)
            {
                errorMessage = "Product price cannot be negative";
                return false;
            }

            return true;
        }
    }
} 