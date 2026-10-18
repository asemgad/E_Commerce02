using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Infrastructure.Date;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

/* What it means: This is a Primary Constructor (a modern C# feature introduced in C# 12 / .NET 8). Instead of writing a traditional constructor method with brackets { }, you can pass parameters directly into the class header.

The Dependencies being injected:

StoreDbContext dbContext: This is your Entity Framework Core database context. Your class uses this to talk to your database (e.g., checking if tables are empty, running migrations, or saving new records).

ILogger<CatalogDateSeeder> logger: This is a built-in .NET logging service. It allows your class to print messages, warnings, or errors to your console or log files, which is very helpful for debugging if the seeding fails.

How it works behind the scenes: ASP.NET Core's Dependency Injection container automatically provides (injects) these two objects whenever a CatalogDateSeeder is */

namespace E_Commerce.Infrastructure.DataSeeding
{
    internal class CatalogDateSeeder(StoreDbContext dbContext , ILogger<CatalogDateSeeder> logger) : IDateSeeder
    {
        public async Task SeedDateAsync(CancellationToken ct = default)
        {
            try
            {
                var pendingMigrations = dbContext.Database.GetPendingMigrations();
                if (pendingMigrations.Any())
                {
                   await dbContext.Database.MigrateAsync(ct);
                }

                // seeding
                // Path
                // "C:\Users\HP\Downloads\Route\.Net\C#\E_Commerce02\E_Commerce.API\bin\Debug\net8.0\DateSeed\products.json"

                var seedRoot = Path.Combine(AppContext.BaseDirectory, "DataSeed");

                await SeedIfEmptyAsync<ProductBrand , int>(seedRoot , "brands.json" , ct);
                await SeedIfEmptyAsync< ProductType, int>(seedRoot , "types.json" , ct);
                await SeedIfEmptyAsync<Product , int>(seedRoot , "products.json" , ct);

                int result = await dbContext.SaveChangesAsync(ct);
                if (result > 0)
                {
                    logger.LogInformation($"{result} rows added");
                }
                else
                {
                    logger.LogInformation("database already seeded");
                }

            }
            catch 
            {
                // Handle exception
            }
        }

        private async Task SeedIfEmptyAsync<T , Tkey> (string rootPath , string fileName , CancellationToken ct) where T : BaseEntity<Tkey>
        {
            if(!await dbContext.Set<T>().AnyAsync(ct))
            {
                logger.LogInformation("Table already has date");
                return;
            }
            ;
                
            var filePath = Path.Combine(rootPath , fileName);
            if (!File.Exists(filePath))
            {
                logger.LogWarning($"file {fileName} is not existed");
                return;
            };

            using var fileStream = File.OpenRead(filePath);

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };

            var items = await JsonSerializer.DeserializeAsync<List<T>>(fileStream, options);
            if (items?.Any() ?? false)
            {
                dbContext.Set<T>().AddRange(items);
            }
            



        }
    }
}
