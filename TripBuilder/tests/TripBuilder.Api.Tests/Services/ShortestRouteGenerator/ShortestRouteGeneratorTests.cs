using TripBuilder.Api.Tests.Mocks;
using Xunit;

namespace TripBuilder.Api.Tests.Services.ShortestRouteGenerator
{
    public sealed class ShortestRouteGeneratorTests
    {
        private readonly Api.Services.ShortestRouteGenerator.ShortestRouteGenerator _generator =
            new Api.Services.ShortestRouteGenerator.ShortestRouteGenerator(new MockDataProvider());
        
        [Fact]
        public void ShouldReturnDirectRoute()
        {
            var result = _generator.GenerateShortestRoute("YYZ", "JFK");

            Assert.Equal("YYZ -> JFK", result);
        }

        [Fact]
        public void ShouldReturn2LegRoute()
        {
            var result = _generator.GenerateShortestRoute("YYZ", "YVR");

            Assert.Equal("YYZ -> JFK -> LAX -> YVR", result);
        }
    }
}