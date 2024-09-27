using Application.Commons.Bases;
using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Interfaces;
using Application.Validators;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Commons.Bases.Request;
using Infrastructure.Commons.Bases.Response;
using Infrastructure.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Static;

namespace Application.Services
{
    public class ProductApplication : IProductApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ProductValidator _validatorRules;

        public ProductApplication(IUnitOfWork unitOfWork, IMapper mapper, ProductValidator validatorRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validatorRules = validatorRules;
        }

        public async Task<BaseResponse<int>> CreateProduct(ProductRequestDto requestDto)
        {
            var response = new BaseResponse<int>();
            var validationResult = await _validatorRules.ValidateAsync(requestDto);

            if (!validationResult.IsValid) {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_VALIDATE;
                response.Errors = validationResult.Errors;
                return response;
            }

            var product = _mapper.Map<Product>(requestDto);
            product.State = Convert.ToBoolean(StateTypes.Active);

            response.Data = await _unitOfWork.Product.CreateAsync(product);

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

        public async Task<BaseResponse<bool>> DeleteProduct(int productId)
        {
            var response = new BaseResponse<bool>();
            var courseUpdate = await GetProductById(productId);

            if (courseUpdate.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                return response;
            }

            response.Data = await _unitOfWork.Product.DeleteAsync(productId);

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

        public async Task<BaseResponse<ProductResponseDto>> GetProductById(int productId)
        {
            var response = new BaseResponse<ProductResponseDto>();
            var course = await _unitOfWork.Product.GetByIdAsync(productId);

            if (course is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<ProductResponseDto>(course);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            return response;
        }

        public async Task<BaseResponse<BaseEntityResponse<ProductResponseDto>>> ListProducts(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<ProductResponseDto>>();
            var courses = await _unitOfWork.Product.ListProducts(filters);

            if (courses is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<BaseEntityResponse<ProductResponseDto>>(courses);
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

        public async Task<BaseResponse<IEnumerable<ProductResponseDto>>> ListSelectProduct()
        {
            var response = new BaseResponse<IEnumerable<ProductResponseDto>>();
            var product = await _unitOfWork.Product.GetAllAsync();

            if (product is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<IEnumerable<ProductResponseDto>>(product);
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

        public async Task<BaseResponse<bool>> UpdateProduct(int productId, ProductRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var productUpdate = await GetProductById(productId);

            if (productUpdate.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                return response;
            }

            var product = _mapper.Map<Product>(requestDto);
            product.Id = productId;
            product.State = Convert.ToBoolean(StateTypes.Active);

            response.Data = await _unitOfWork.Product.UpdateAsync(product);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_UPDATE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }

            return response;
        }
    }
}
