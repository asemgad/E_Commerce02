using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Specifications
{
    internal class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity , Tkey>(IQueryable<TEntity> inputQuery , ISpecification<TEntity , Tkey> spec) where TEntity : BaseEntity<Tkey>
        {
            var query = inputQuery;

            if (spec.icludeRxpressions.Any())
            {
                query = spec.icludeRxpressions.Aggregate(query, (current, next) => current.Include(next));
            }
            return query;

        }
    }
}
