using Application.DTOs.Request;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishListApplication _wishListApplication;

        public WishListController(IWishListApplication wishListApplication)
        {
            _wishListApplication = wishListApplication;
        }

        [HttpPost("CreateWishList")]
        public async Task<IActionResult> CreateCourse([FromBody] WishListRequestDto wishListRequestDto)
        {
            var response = await _wishListApplication.CreateWishList(wishListRequestDto);
            return Ok(response);
        }

        [HttpGet("GetWishListByUser/{userId:int}")]
        public async Task<IActionResult> GetWishListByUser(int userId)
        {
            var response = await _wishListApplication.GetWishListByUserId(userId);
            return Ok(response);
        }

        [HttpPut("Delete/{userId:int}/{productId:int}")]
        public async Task<IActionResult> DeleteProduct(int userId, int productId)
        {
            var response = await _wishListApplication.DeleteProductWishList(userId,productId);
            return Ok(response);
        }
    }
}
