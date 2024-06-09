(A) Setting up the environment/configuration
    The application has 6 layers (Domain, Application, Infrastructure, Presentation, Setup and Test) with all but Domain having nugget package and/or dependencies.
    - Restore dependency on Layers, one at a time
    - Add the connection string to any file ICongiguration reads from. PostgreSQL with Dapper was used for Db management and so there's need for host machine to have                 PostgreSQL installed. Connection string should follow format 
          "ConnectionStrings": { "BookStoreConnection": "Host=<host>;Port=<port>;Database=<database>;Username=<username>;Password=<password>" }
      This was added to user secrete during development.
    - Using Package manager console on Infrastructure, Update-Database for db creation and seeding.

(B) Dependencies
    - Domain layer is independent of any layer nor Nugget package
    - Application layer is dependent on Domain and no Nugget package
    - Infrastructure layer is dependent on Application layer and Nugget packages (Dapper, Microsoft.AspNetCore.Identity.EntityFrameworkCore, Microsoft.EntityFrameworkCore.Tools and Npgsql)
    - Setup layer is dependent on Infrastructure layer and Nugget packages (Microsoft.AspNetCore.Identity.EntityFrameworkCore, Microsoft.Identity.Web and Npgsql.EntityFrameworkCore.PostgreSQL)
    - Presentation layer is dependent on Infrastructure layer and Nugget packages (Microsoft.EntityFrameworkCore.Design and Swashbuckle.AspNetCore) and reference Setup Layer
    - Test layer is dependent on Infrastructure layer and Nugget packages (coverlet.collector, Microsoft.NET.Test.Sdk, xunit and xunit.runner.visualstudio)

(C) Building the App
    - Application should be built from Presentation layer after setting as Startup project.
    - Build can be done with IIS Express which fires up swagger at which point API interaction can be done. Other tools like Postman can also be used.

(D) Endpoints
    -Authors
        GET https://localhost:44360/Authors
        POST https://localhost:44360/Authors/AddAuthor
        POST https://localhost:44360/Authors/EditAuthor
        GET https://localhost:44360/Authors/GetAuthor/{id}
        POST https://localhost:44360/Authors/DeleteAuthor

    -Book
        GET https://localhost:44360/Book/Books
        POST https://localhost:44360/Book/AddBook
        POST https://localhost:44360/Book/EditBook
        GET https://localhost:44360/Book/GetBook/{id}
        GET https://localhost:44360/Book/GetBooksByAuthor/{id}
        GET https://localhost:44360/Book/GetBooksByTitle/{title}
        GET https://localhost:44360/Book/GetBooksByGenre/{id}
        GET https://localhost:44360/Book/GetBooksByYear/{year}
        POST https://localhost:44360/Book/DeleteBook
        
    -Cart
        GET https://localhost:44360/Cart
        POST https://localhost:44360/Cart/AddToCart
        POST https://localhost:44360/Cart/DeleteFromCart
        POST https://localhost:44360/Cart/Checkout
        POST https://localhost:44360/Cart/GetCartHistory
        
    -Genre
        GET https://localhost:44360/Genre/GetAll
        POST https://localhost:44360/Genre/AddGenre
        POST https://localhost:44360/Genre/EditGenre
        GET https://localhost:44360/Genre/GetGenre/{id}
        
