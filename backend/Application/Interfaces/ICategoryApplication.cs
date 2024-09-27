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
    public interface ICategoryApplication
    {
        Task<BaseResponse<BaseEntityResponse<CategoryResponseDto>>> ListCategories(BaseFiltersRequest filters);
        Task<BaseResponse<IEnumerable<CategorySelectResponseDto>>> ListSelectCategories();
        Task<BaseResponse<CategoryResponseDto>> GetCategoryById(int categoryId);
        Task<BaseResponse<int>> CreateCategory(CategoryRequestDto requestDto);
        Task<BaseResponse<bool>> UpdateCategory(int categoryId, CategoryRequestDto requestDto);
        Task<BaseResponse<bool>> DeleteCategory(int categoryId);
    }
}
