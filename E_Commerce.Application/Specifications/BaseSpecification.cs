using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specifications
{
    internal class BaseSpecification<TEntity, Tkey> : ISpecification<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public ICollection<Expression<Func<TEntity, object>>> icludeRxpressions { get; } = [];

        public Expression<Func<TEntity, bool>> Criteria { get; private set;  }

        public Expression<Func<TEntity, object>>? OrderBy { get; private set;}

        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set;}

        public int Take { get; private set; }

        public int Skip { get; private set; }
        public bool IsPaginated { get; private set; }

        protected void ApplyPaging(int PageSize, int PageIndex)
        {
            Skip = (PageIndex - 1) * PageSize;
            Take = PageSize;
            IsPaginated = true;
        }

        protected BaseSpecification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        protected void AddInclude(Expression<Func<TEntity, object>> include)
        {
            icludeRxpressions.Add(include);
        }
    }
}
