using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Dummy.Models;
using Dummy.Services;

namespace Dummy.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IProductService _productService;
        private readonly ILoggingService _logger;
        private readonly INotificationService _notificationService;

        public ProductsController()
        {
            // In a real application, you would use dependency injection here
            _logger = new FileLoggingService();
            IValidationService validator = new ProductValidationService();
            IEmailService emailService = new SmtpEmailService(); // You would need to implement this
            _notificationService = new EmailNotificationService(_logger, emailService);
            _productService = new ProductService(_logger, validator, _notificationService);
        }

        // GET: api/Products
        public IHttpActionResult Get()
        {
            try
            {
                var products = _productService.GetAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error retrieving products", ex);
                return InternalServerError(ex);
            }
        }

        // GET: api/Products/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var product = _productService.GetProductById(id);
                if (product == null)
                {
                    _logger.LogInfo($"Product not found with ID: {id}");
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving product with ID: {id}", ex);
                return InternalServerError(ex);
            }
        }

        // POST: api/Products
        public IHttpActionResult Post([FromBody] Product product)
        {
            try
            {
                if (product == null)
                {
                    _logger.LogError("Attempted to create null product");
                    return BadRequest("Product cannot be null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid model state for product creation");
                    return BadRequest(ModelState);
                }

                _productService.AddProduct(product);
                return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError("Validation error during product creation", ex);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating product", ex);
                return InternalServerError(ex);
            }
        }

        // PUT: api/Products/5
        public IHttpActionResult Put(int id, [FromBody] Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest("Product cannot be null");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (id != product.Id)
                    return BadRequest("ID mismatch");

                _productService.UpdateProduct(product);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError("Validation error during product update", ex);
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException)
            {
                _logger.LogError($"Product not found for update with ID: {id}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating product with ID: {id}", ex);
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Products/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var product = _productService.GetProductById(id);
                if (product == null)
                {
                    _logger.LogInfo($"Attempted to delete non-existent product with ID: {id}");
                    return NotFound();
                }

                _productService.DeleteProduct(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting product with ID: {id}", ex);
                return InternalServerError(ex);
            }
        }
    }
} 