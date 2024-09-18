# BookStore Application

## Project Overview

This is a project based on ASP.NET Core 8, containing three API: `AuthController`, `BooksController`, and `ShoppingCartController`. The project is configured with Swagger for API documentation and uses Docker for containerized deployment. Additionally, the project uses JWT for authentication, with relevant configurations in `appsettings.json`.

### APIs

1. **AuthController**: Handles user registration and login.

   - `POST /api/auth/register`: User registration.
   - `POST /api/auth/login`: User login.

2. **BooksController**: Manages book information.

   - `GET /api/books`: Retrieve all books.
   - `GET /api/books/{id}`: Retrieve book details by ID.
   - `POST /api/books`: Add a new book.

3. **ShoppingCartController**: Handles shopping cart operations (requires user authentication).
   - `POST /api/shoppingcart/{bookId}`: Add a book to the shopping cart.
   - `GET /api/shoppingcart`: Retrieve the current user's shopping cart information.
   - `GET /api/shoppingcart/checkout`: Checkout the shopping cart and return the total price.

### Configuration

In `appsettings.json`, you need to configure some JWT settings:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "JwtSettings": {
    "ValidIssuer": "domain.com",
    "ValidAudience": "domain.com",
    "IssuerSigningKey": "Keyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy"
  }
}
```

ValidIssuer: The issuer of the JWT.
ValidAudience: The audience of the JWT.
IssuerSigningKey: The key used to sign the JWT.

### Running the Project

Using .NET CLI to Run the Project

```
dotnet build
dotnet run
```

The project will by default run at http://localhost:5000. You can access the API documentation via Swagger UI at http://localhost:5000/swagger.

### Using Docker to Run the Project

Build the Docker image:

```
docker build -t bookstoreapplication .
```

Run the Docker container:

```
docker run -p 8080:80 -p 8081:443 bookstoreapplication
```

The project will by default run at http://localhost:8080.

### Running the Test Project

Navigate to the test project directory (assuming the test project is located in the tests directory):

```
cd BookStoreApplication\BookStoreApplication.Test
dotnet test
```

By following these steps, you can run the project and its tests. If you encounter any issues, please refer to the official documentation or contact the project maintainers.

The design decisions for the BookStoreApplication project are aimed at creating a secure, scalable, and maintainable web API. By leveraging modern frameworks and best practices, the project ensures a robust solution for managing books and user authentication.
