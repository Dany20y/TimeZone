using AutoMapper;
using Time_Zone.Domain.Entities.User;

namespace Time_Zone.BussinessLogic.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration ConfigureMappings()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ULoginData, UserMinimal>();
                // Add more mappings if needed
            });
            return config;
        }
    }
}
