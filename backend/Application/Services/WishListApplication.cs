using Application.Commons.Bases;
using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Static;

namespace Application.Services
{
    public class WishListApplication : IWishListApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WishListApplication(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<int>> CreateWishList(WishListRequestDto requestDto)
        {
            var response = new BaseResponse<int>();

            var route = _mapper.Map<Wishlist>(requestDto);
            route.State = Convert.ToBoolean(StateTypes.Active);
            response.Data = await _unitOfWork.WishList.CreateAsync(route);

            if (response.Data > 0)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_SAVE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteProductWishList(int userId, int productId)
        {
            var response = new BaseResponse<bool>();
            var wishlistItem = _unitOfWork.WishList
                .GetAllAsync()
                .Result
                .FirstOrDefault(w => w.UserId == userId && w.ProductId == productId);


            if (wishlistItem == null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.Data = await _unitOfWork.WishList.DeleteAsync(wishlistItem.Id);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_DELETE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }

            return response;
        }

        public async Task<BaseResponse<WishListResponseDto>> GetWishListById(int wishListId)
        {
            var response = new BaseResponse<WishListResponseDto>();
            var course = await _unitOfWork.WishList.GetByIdAsync(wishListId);

            if (course is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<WishListResponseDto>(course);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            return response;
        }

        public async Task<BaseResponse<IEnumerable<ProductResponseDto>>> GetWishListByUserId(int userId)
        {
            var response = new BaseResponse<IEnumerable<ProductResponseDto>>();
            var wishLists = _unitOfWork.WishList.GetAllAsync().Result.Where(x => x.UserId == userId);
            var products = await _unitOfWork.Product.GetAllAsync();

            if (wishLists is not null)
            {
                var wishListProducts = (from w in wishLists
                                        join p in products on w.ProductId equals p.Id
                                        select new Product
                                        {
                                            Id = p.Id,
                                            Name = p.Name,
                                            Category = p.Category,
                                            Description = p.Description,
                                            Price = p.Price,
                                            State = p.State,
                                        }).ToList();

                response.IsSuccess = true;
                response.Data = _mapper.Map<IEnumerable<ProductResponseDto>>(wishListProducts);
                response.Message = ReplyMessage.MESSAGE_QUERY;

                return response;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            return response;
        }

    }
}
