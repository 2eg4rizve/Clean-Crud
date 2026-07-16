# CleanCrud API

Clean Architecture ASP.NET Core controller-based CRUD API for a `Product` model, backed by Microsoft SQL Server through Entity Framework Core.

## Layers

- `CleanCrud.Domain`: product entity.
- `CleanCrud.Application`: use-case service, DTOs, AutoMapper profile, generic repository and Unit of Work contracts.
- `CleanCrud.Infrastructure`: EF Core DbContext, generic repository, Unit of Work implementation, and SQL Server migrations.
- `CleanCrud.Api`: controllers, Swagger, dependency injection, and configuration.

## Run

```powershell
dotnet ef database update --project .\CleanCrud.Infrastructure --startup-project .\CleanCrud.Api
dotnet run --project .\CleanCrud.Api
```

## Endpoints

| Method | Route | Purpose |
|---|---|---|
| GET | `/api/products` | List products |
| GET | `/api/products/{id}` | Get one product |
| POST | `/api/products` | Create a product |
| PUT | `/api/products/{id}` | Update a product |
| DELETE | `/api/products/{id}` | Delete a product |

Create/update request body:

```json
{
  "name": "Laptop",
  "description": "14-inch developer laptop",
  "price": 999.99,
  "stockQuantity": 10
}
```

Swagger is available at `/swagger` while the application runs in the Development environment.

The default connection string uses SQL Server LocalDB. Change `ConnectionStrings:DefaultConnection` in `CleanCrud.Api/appsettings.json` to use your SQL Server instance before running the database update command.
