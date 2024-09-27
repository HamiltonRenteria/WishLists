using Application.DTOs.Request;
using Application.DTOs.Response;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Static;

namespace Application.Mappers
{
    public class WishListMappingsProfile : Profile
    {
        public WishListMappingsProfile()
        {
            CreateMap<WishListRequestDto, Wishlist>()
                .ReverseMap();

            CreateMap<WishListResponseDto, Wishlist>()
                .ForMember(x => x.State, x => x.MapFrom(y => y.State == Convert.ToBoolean(StateTypes.Active) ? "Activo" : "Inactivo"))
                .ReverseMap();
        }
    }
}
