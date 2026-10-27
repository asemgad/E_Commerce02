using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure.Date;
using E_Commerce.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Repositories
{
    internal class GenericRepository<TEntity, Tkey>(StoreDbContext dbContext) : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public void Add(TEntity entity)
        {
           dbContext.Set<TEntity>().Add(entity);
        }

        //public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
        //{
        //   return await dbContext.Set<TEntity>().ToListAsync(ct);
        //}

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecification<TEntity, Tkey> spec, CancellationToken ct = default)
        {
            var query = SpecificationEvaluator.CreateQuery<TEntity, Tkey>(dbContext.Set<TEntity>(), spec);
            return await query.ToListAsync(ct);

        }

        public async Task<TEntity?> GetByIdAsync(Tkey id, CancellationToken ct = default)
        {
          return await  dbContext.Set<TEntity>().FindAsync(id, ct);
        }

        public void Remove(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
        }
    }
}
