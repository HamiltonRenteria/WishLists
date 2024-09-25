using Application.Commons.Bases;
using Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserApplication
    {
        Task<BaseResponse<int>> RegisterUser(UserRequestDto requestDto);
        Task<BaseResponse<string>> GenerateToken(TokenRequestDto requestDto);
    }
}
