using Application.Commons.Bases;
using Application.DTOs.Request;
using Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IWishListApplication
    {
        Task<BaseResponse<int>> CreateWishList(WishListRequestDto requestDto);
        Task<BaseResponse<IEnumerable<ProductResponseDto>>> GetWishListByUserId(int userId);
        Task<BaseResponse<bool>> DeleteProductWishList(int wishlistId, int productId);
    }
}
