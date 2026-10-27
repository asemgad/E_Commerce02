using E_Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
    public interface ISpecification <TEntity , Tkey> where TEntity : BaseEntity<Tkey>
    {
        ICollection<Expression<Func<TEntity , object>>> icludeRxpressions{ get; }

    }
}
