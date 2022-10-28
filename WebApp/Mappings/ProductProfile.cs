using AutoMapper;
using DbFirst.Entities;
using WebApp.Models;

namespace WebApp.Mappings
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductModel,Product>()
                .ForMember(dest=> dest.Name,
                opt=> opt.MapFrom(src=>src.Name))
                .ForMember(dest=> dest.Category,opt=>opt.Ignore())
               // .ForSourceMember(dest=>dest.CategoryId,opt=>opt.DoNotValidate())
                .ReverseMap();

        }

    }
}
