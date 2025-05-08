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

        public ProductsController()
        {
            // In a real application, you would use dependency injection here
            _productService = new ProductService();
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
                    return NotFound();

                return Ok(product);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Products
        public IHttpActionResult Post([FromBody] Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest("Product cannot be null");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _productService.AddProduct(product);
                return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
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

                try
                {
                    _productService.UpdateProduct(product);
                }
                catch (InvalidOperationException ex)
                {
                    return NotFound();
                }

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
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
                    return NotFound();

                _productService.DeleteProduct(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}