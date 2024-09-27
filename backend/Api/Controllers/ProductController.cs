using Application.DTOs.Request;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductApplication _productApplication;

        public ProductController(IProductApplication productApplication)
        {
            _productApplication = productApplication;
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequestDto request)
        {
            var response = await _productApplication.CreateProduct(request);
            return Ok(response);
        }

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var response = await _productApplication.ListSelectProduct();
            return Ok(response);
        }

        [HttpGet("GetProductById/{productId:int}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var response = await _productApplication.GetProductById(productId);
            return Ok(response);
        }

        [HttpPut("Update/{CourseId:int}")]
        public async Task<IActionResult> UpdateCourse(int CourseId, [FromBody] ProductRequestDto courseRequestDto)
        {
            var response = await _productApplication.UpdateProduct(CourseId, courseRequestDto);
            return Ok(response);
        }

        [HttpPut("Delete/{CourseId:int}")]
        public async Task<IActionResult> DeleteProduct(int CourseId)
        {
            var response = await _productApplication.DeleteProduct(CourseId);
            return Ok(response);
        }
    }
}
