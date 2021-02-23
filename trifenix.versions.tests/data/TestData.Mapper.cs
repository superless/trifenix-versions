using AutoMapper;
using trifenix.versions.model;

namespace trifenix.versions.tests.mock
{
    public static partial class TestData
    {
        public static class MapperData {

            public static MapperConfiguration Configuration => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VersionStructure, VersionStructure>();
            });

            public static IMapper Mapper => Configuration.CreateMapper();


        
        }
    }
}
