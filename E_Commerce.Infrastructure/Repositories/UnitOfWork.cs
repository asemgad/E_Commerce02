using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Repositories
{
    internal class UnitOfWork(StoreDbContext dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> repos = [];
        public  IGenericRepository<TEnity, Tkey> GetGenericRepository<TEnity, Tkey>() where TEnity : BaseEntity<Tkey>
        { 
        
           var typeName = typeof(TEnity).Name;
            if (repos.TryGetValue(typeName , out object? value))
            {
                return (IGenericRepository<TEnity, Tkey>)value;
            }
            else
            {
                var repo = new GenericRepository<TEnity, Tkey>(dbContext);
                repos.Add(typeName, repo);
                return repo;
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct)
        {
           return await dbContext.SaveChangesAsync(ct);
        }

        
    }
}
