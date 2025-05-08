# ASP.NET Web API Sample Project

This is a basic ASP.NET Web API project using .NET Framework 4.8. It includes:

- A self-hosted Web API service
- A product controller with CRUD operations
- A product service to manage product data

## Project Structure

- `Program.cs` - Main entry point that sets up and runs the self-hosted Web API
- `WebApiConfig.cs` - Web API configuration and routing
- `ProductService.cs` - Service with business logic for product management
- `ProductsController.cs` - Web API controller exposing product CRUD operations
- `App.config` - Application configuration file
- `packages.config` - NuGet package references

## Required NuGet Packages

- Microsoft.AspNet.WebApi.Client (5.2.9)
- Microsoft.AspNet.WebApi.Core (5.2.9)
- Microsoft.AspNet.WebApi.SelfHost (5.2.9)
- Newtonsoft.Json (13.0.3)

## How to Run

1. Open the project in Visual Studio
2. Restore NuGet packages
3. Build the solution
4. Run the project

The API will be available at `http://localhost:8080/api/products`

## API Endpoints

- GET `/api/products` - Get all products
- GET `/api/products/{id}` - Get a product by ID
- POST `/api/products` - Add a new product
- PUT `/api/products/{id}` - Update an existing product
- DELETE `/api/products/{id}` - Delete a product

## Example Usage

### Get all products
```
GET http://localhost:8080/api/products
```

### Get a product by ID
```
GET http://localhost:8080/api/products/1
```

### Add a new product
```
POST http://localhost:8080/api/products
Content-Type: application/json

{
  "Name": "New Product",
  "Price": 39.99
}
```

### Update a product
```
PUT http://localhost:8080/api/products/1
Content-Type: application/json

{
  "Id": 1,
  "Name": "Updated Product",
  "Price": 49.99
}
```

### Delete a product
```
DELETE http://localhost:8080/api/products/1
```