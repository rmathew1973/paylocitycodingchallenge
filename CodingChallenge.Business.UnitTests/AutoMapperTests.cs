using AutoMapper;
using CodingChallenge.Business.Bootstrap;
using NUnit.Framework;

namespace CodingChallenge.Business.UnitTests
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
