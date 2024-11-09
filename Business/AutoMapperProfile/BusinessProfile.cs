using AutoMapper;
using Entities.Concrete;

namespace Business.AutoMapperProfile
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            //CreateMap<GuidanceQuestion, GuidanceQuestionDto>().ReverseMap();
            CreateMap<CategoryTable, CategoryTableDto>().ReverseMap();
            CreateMap<ProductPropertyTable, ProductPropertyTableDto>().ReverseMap();
            CreateMap<ProductTable, ProductTableDto>().ReverseMap();
            CreateMap<PropertyTable, PropertyTableDto>().ReverseMap();
            CreateMap<UserTable, UserTableDto>().ReverseMap();


        }
    }
}
