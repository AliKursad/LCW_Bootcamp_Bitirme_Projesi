using LCW.Core.Repositories;
using LCW.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LCW.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IColorRepository _colorRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IBrandRepository brandRepository, IColorRepository colorRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _colorRepository = colorRepository;
        }

        [HttpGet("Categories")]
        public IActionResult GetCategory()
        {

            return Ok(_categoryRepository.GetAll());
        }

        [HttpGet("Brands")]
        public IActionResult GetBrand()
        {

            return Ok(_brandRepository.GetAll());
        }

        [HttpGet("Colors")]
        public IActionResult GetColor()
        {

            return Ok(_colorRepository.GetAll());
        }

        [HttpGet("Products")]
        public IActionResult GetProduct()
        {

            return Ok(_productRepository.GetAll());
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            await _productRepository.AddAsync(product);

            return Ok("Product is added");
        }

    }
}
