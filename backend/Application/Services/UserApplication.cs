using Application.Commons.Bases;
using Application.DTOs.Request;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Utilities.Static;
using BC = BCrypt.Net.BCrypt;

namespace Application.Services
{
    public class UserApplication : IUserApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserApplication(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<BaseResponse<string>> GenerateToken(TokenRequestDto requestDto)
        {
            var response = new BaseResponse<string>();
            var account = await _unitOfWork.User.AccountByUserName(requestDto.UserName!);

            if (account is not null)
            {
                if (BC.Verify(requestDto.Password, account.Password))
                {
                    response.IsSuccess = true;
                    response.Data = GenerateToken(account);
                    response.Message = ReplyMessage.MESSAGE_TOKEN;
                    return response;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Data = null;
                    response.Message = ReplyMessage.MESSAGE_TOKEN_ERROR;
                    return response;
                }
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_TOKEN_ERROR;
            }

            return response;
        }

        private string? GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, user.Name!),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, Guid.NewGuid().ToString(), ClaimValueTypes.Integer64)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:Expires"]!)),
                notBefore: DateTime.UtcNow,
                signingCredentials: credentials
             );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<BaseResponse<int>> RegisterUser(UserRequestDto requestDto)
        {
            var response = new BaseResponse<int>();

            try
            {
                var account = _mapper.Map<User>(requestDto);
                account.Password = BC.HashPassword(account.Password);
                account.State = Convert.ToBoolean(StateTypes.Active);

                response.Data = await _unitOfWork.User.CreateAsync(account);

                if (response.Data > 0)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_SAVE;
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
