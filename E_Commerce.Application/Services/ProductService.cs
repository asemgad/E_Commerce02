using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Products;
using E_Commerce.Application.Specifications;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    internal class ProductService : IProductService
    {

        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;
        public ProductService(IUnitOfWork unitOfWork , IMapper mapper) 
        { 
            unit = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandAsync(CancellationToken ct = default)
        {
            var spec = new ProductWithTypeAndPlan();
            var brands = await unit.GetGenericRepository<ProductBrand, int>().GetAllAsync(spec);
            var date = mapper.Map<IReadOnlyList<BrandDto>>(brands);
            return Result<IReadOnlyList<BrandDto>>.Ok(date);

        }

        public async Task<Result<IReadOnlyList<TypeDto>>> GetAllTypeAsync(CancellationToken ct = default)
        {
            var types = await unit.GetGenericRepository<ProductType , int >().GetAllAsync(ct);

            var data = mapper.Map<IReadOnlyList <TypeDto>>(types);

            return Result<IReadOnlyList<TypeDto>>.Ok(data);
        }

        public async Task<Result<ProductDto>> GetProductByIdAsync(int id, CancellationToken ct = default)
        {
            var product =await unit.GetGenericRepository<Product , int>().GetByIdAsync(id, ct);
            var data = mapper.Map<ProductDto>(product);
            if (data == null)
                return Result<ProductDto>.Fail(Error.NotFound("Product.Notfound", $"Prduct with ID {id} is not found"));
            return Result<ProductDto>.Ok(data);

        }

        public async Task<Result<IReadOnlyList<ProductDto>>> GetProductsAsync(CancellationToken ct = default)
        {
            var products = await unit.GetGenericRepository<Product, int>().GetAllAsync(ct);
            var data = mapper.Map<IReadOnlyList<ProductDto>>(products);
            return Result<IReadOnlyList< ProductDto>>.Ok(data);
        }
    }
}
