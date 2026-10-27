using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specifications
{
    internal class ProductWithTypeAndPlan : BaseSpecification<Product, int>
    {
        public ProductWithTypeAndPlan()
        {
            AddInclude(p=> p.ProductType);
            AddInclude(p=> p.ProductBrand);
        }
    }
}
