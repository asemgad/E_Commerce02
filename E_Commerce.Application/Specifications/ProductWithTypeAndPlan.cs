using E_Commerce.Application.Common;
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

        // get all 
        public ProductWithTypeAndPlan( ProductQueryParams queryParams ) : 
            base( p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId.Value) && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId.Value)
            && (string.IsNullOrEmpty(queryParams.SearchName) || p.Name.ToLower().Contains(queryParams.SearchName.ToLower())))
        {
            // brandid is not null -> p => p.ProductBrandId == BrandId
            // TypeId is not null -> p => p.ProductTypeId == TypeId
            // brandid is not null and TypeId is not null -> p => p.ProductBrandId == BrandId && p.ProductTypeId == TypeId

            AddInclude(p=> p.ProductType);
            AddInclude(p=> p.ProductBrand);

            
                switch (queryParams.Sort)
                {
                    case ProductSortingOptions.NameAscending:
                        AddOrderBy(p => p.Name);
                        break;
                    case ProductSortingOptions.NameDescending:
                        AddOrderByDescending(p => p.Name);
                        break;
                    case ProductSortingOptions.PriceAscending :
                        AddOrderBy(p => p.Price);
                        break;
                    case ProductSortingOptions.PriceDescending:
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Id);
                        break;
                }

            ApplyPaging(queryParams.PageSize, queryParams.PageIndex);
        }

        
        
        // get By Id  
        public ProductWithTypeAndPlan(int id ) : base(x => x.Id == id)
        {
            AddInclude(p=> p.ProductType);
            AddInclude(p=> p.ProductBrand);
        }
    }
}
