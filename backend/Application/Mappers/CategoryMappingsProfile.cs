using Application.DTOs.Request;
using Application.DTOs.Response;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Commons.Bases.Response;

namespace Application.Mappers
{
    public class CategoryMappingsProfile : Profile
    {
        public CategoryMappingsProfile()
        {
            _ = CreateMap<Category, CategoryResponseDto>()
                .ForMember(x => x.CategoryId, x => x.MapFrom(y => y.Id))
                .ReverseMap();

            _ = CreateMap<CategoryResponseDto, Category>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.CategoryId))
                .ReverseMap();

            _ = CreateMap<BaseEntityResponse<Category>, BaseEntityResponse<CategoryResponseDto>>()
                    .ReverseMap();

            _ = CreateMap<CategoryRequestDto, Category>()
                    .ReverseMap();

            _ = CreateMap<Category, CategoryRequestDto>()
                    .ReverseMap();

            _ = CreateMap<Category, CategorySelectResponseDto>()
                    .ForMember(x => x.CategoryId, x => x.MapFrom(y => y.Id))
                    .ReverseMap();
        }
    }
}
