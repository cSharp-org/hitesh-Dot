using Dummy.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dummy.Services
{

    public class ProductService : IProductService
    {
        private List<Product> _products;

        public ProductService()
        {
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
            return _products;
        }

        public Product GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public void AddProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            // Generate a new ID (in a real app, this would be handled by the database)
            if (_products.Count > 0)
                product.Id = _products.Max(p => p.Id) + 1;
            else
                product.Id = 1;

            _products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct == null)
                throw new InvalidOperationException($"Product with ID {product.Id} not found.");

            // Update properties
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
        }

        public void DeleteProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
                _products.Remove(product);
        }
    }
}