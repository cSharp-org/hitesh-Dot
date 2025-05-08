using Dummy.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dummy.Services
{
    public class ProductService : BaseService, IProductService
    {
        private List<Product> _products;
        private readonly IValidationService _validator;
        private readonly INotificationService _notificationService;

        public ProductService(
            ILoggingService logger,
            IValidationService validator,
            INotificationService notificationService) 
            : base(logger)
        {
            _validator = validator;
            _notificationService = notificationService;
            
            // In-memory product list for demo purposes
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 9.99m },
                new Product { Id = 2, Name = "Product 2", Price = 19.99m },
                new Product { Id = 3, Name = "Product 3", Price = 29.99m }
            };
        }

        public IEnumerable<Product> GetAllProducts()
        {
            LogServiceCall(nameof(GetAllProducts));
            return _products;
        }

        public Product GetProductById(int id)
        {
            LogServiceCall(nameof(GetProductById));
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public void AddProduct(Product product)
        {
            LogServiceCall(nameof(AddProduct));
            
            if (!_validator.ValidateProduct(product, out string errorMessage))
            {
                LogServiceError(nameof(AddProduct), new ArgumentException(errorMessage));
                throw new ArgumentException(errorMessage);
            }

            if (_products.Count > 0)
                product.Id = _products.Max(p => p.Id) + 1;
            else
                product.Id = 1;

            _products.Add(product);
            _notificationService.NotifyProductCreated(product.Id, product.Name);
        }

        public void UpdateProduct(Product product)
        {
            LogServiceCall(nameof(UpdateProduct));

            if (!_validator.ValidateProduct(product, out string errorMessage))
            {
                LogServiceError(nameof(UpdateProduct), new ArgumentException(errorMessage));
                throw new ArgumentException(errorMessage);
            }

            var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct == null)
            {
                var ex = new InvalidOperationException($"Product with ID {product.Id} not found.");
                LogServiceError(nameof(UpdateProduct), ex);
                throw ex;
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            _notificationService.NotifyProductUpdated(product.Id, product.Name);
        }

        public void DeleteProduct(int id)
        {
            LogServiceCall(nameof(DeleteProduct));
            
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _products.Remove(product);
                _notificationService.NotifyProductDeleted(id);
            }
        }
    }
}