using AutoMapper;
using Hogwarts.CrossCutting.Mapping;
using System;

namespace Hogwarts.Service.Test
{
    public class BaseTest
    {
        public IMapper Mapper { get; set; }
        public BaseTest()
        {
            Mapper = new AutoMapperFixture().GetMapper();
        }

        public class AutoMapperFixture : IDisposable
        {
            public IMapper GetMapper()
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new DtoToEntity());                    
                });

                return config.CreateMapper();
            }
            public void Dispose()
            {
            }
        }
    }
}
