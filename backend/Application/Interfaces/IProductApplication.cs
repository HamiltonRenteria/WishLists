using Application.Commons.Bases;
using Application.DTOs.Request;
using Application.DTOs.Response;
using Infrastructure.Commons.Bases.Request;
using Infrastructure.Commons.Bases.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductApplication
    {
        Task<BaseResponse<BaseEntityResponse<ProductResponseDto>>> ListProducts(BaseFiltersRequest filters);
        Task<BaseResponse<IEnumerable<ProductResponseDto>>> ListSelectProduct();
        Task<BaseResponse<ProductResponseDto>> GetProductById(int productId);
        Task<BaseResponse<int>> CreateProduct(ProductRequestDto requestDto);
        Task<BaseResponse<bool>> UpdateProduct(int productId, ProductRequestDto requestDto);
        Task<BaseResponse<bool>> DeleteProduct(int productId);
    }
}
