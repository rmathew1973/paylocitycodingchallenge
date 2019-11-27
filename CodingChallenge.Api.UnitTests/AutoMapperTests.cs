using AutoMapper;
using CodingChallenge.Api.Bootstrap;
using NUnit.Framework;

namespace CodingChallenge.Api.UnitTests
{
    public class AutoMapperTests
    {
        [Test]
        public void MapperConfigurationsShouldBeValid()
        {
            var myProfile = new AutoMapperProfile();

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

            configuration.AssertConfigurationIsValid();
        }
    }
}
