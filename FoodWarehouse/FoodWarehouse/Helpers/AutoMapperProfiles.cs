using AutoMapper;
using FoodWarehouse.DTOs;
using FoodWarehouse.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodWarehouse.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CategoriesCreateDTO, Categories>();
            CreateMap<Categories, CategoriesDTO>().ReverseMap();

            CreateMap<StatusCreateDTO, status>();
            CreateMap<status, StatusDTO>().ReverseMap();

            CreateMap<ProductCreateDTO, Products>().ReverseMap();
            CreateMap<Products, ProductsDTO>();

            CreateMap<CupBoardCreateDTO, CupBoard>();
            CreateMap<CupBoard, CupBoardDTO>().ReverseMap();
        }
    }
}
