using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Products;
using E_Commerce.Domain.Entities.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ApiBaseController
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]

        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAllProducts (CancellationToken ct)
        {
            var data = await productService.GetProductsAsync(ct);
            return ToActionResult(data);

        }
        [HttpGet("types")]

        public async Task<ActionResult<IReadOnlyList<TypeDto>>> GetAllTypes(CancellationToken ct)
        {

            return ToActionResult(await productService.GetAllTypeAsync(ct));
        }


        [HttpGet("brands")]

        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetAllBrands(CancellationToken ct)
        {
            return ToActionResult(await productService.GetAllBrandAsync(ct));
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ProductDto>> GetProduct(int id, CancellationToken ct)
        {
            var result = await productService.GetProductByIdAsync(id, ct);
            return ToActionResult(result);
        }


    }
}
