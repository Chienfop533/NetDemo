using AutoMapper;

namespace NetDemo.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Models.StudentDTO, Data.Student>()
                .ForMember(n => n.StudentName, opt => opt.MapFrom(x => x.Name))
                .ReverseMap();
            //CreateMap<Models.StudentDTO, Data.Student>()
            //    .ForMember(n => n.StudentName, opt => opt.Ignore())
            //    .ReverseMap();
            //CreateMap<Models.StudentDTO, Data.Student>()
            //    .ForMember(n => n.StudentName, opt => opt.AddTransform(n => string.IsNullOrEmpty(n) ? "No address" : n))
            //    .ReverseMap();

            CreateMap<Models.RoleDTO, Data.Role>().ReverseMap();
        }
    }
}
