using AsukaApi.Infrastructure.Maps;
using AutoMapper;
using NUnit.Framework;

namespace AsukaApi.Application.IntegrationTests
{
    public class MappingConfigurationTests
    {
        [Test]
        public void MappingProfile_VerifyMappings()
        {
            var mappingProfile = new MappingProfile();

            var config = new MapperConfiguration(mappingProfile);
            var mapper = new Mapper(config) as IMapper;

            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
