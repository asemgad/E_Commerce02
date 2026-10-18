using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Products;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    internal class ProductService : IProductService
    {

        private readonly IUnitOfWork unit;
        public ProductService(IUnitOfWork unitOfWork) 
        { 
            unit = unitOfWork;
        }
        public async Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandAsync(CancellationToken ct = default)
        {
            var brands = await unit.GetGenericRepository<ProductBrand, int>().GetAllAsync(ct);
        }

        public Task<Result<IReadOnlyList<TypeDto>>> GetAllTypeAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<Result<ProductDto>> GetProductByIdAsync(int id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
