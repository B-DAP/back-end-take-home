using Microsoft.AspNetCore.Mvc;
using TripBuilder.Api.Endpoints.TripBuilder;
using TripBuilder.Api.Tests.Mocks;
using Xunit;

namespace TripBuilder.Api.Tests.Endpoints.TripBuilder
{
    public sealed class TripBuilderControllerTests
    {
        private readonly TripBuilderController _controller =
            new TripBuilderController(new MockShortestRouteGenerator(), new MockDataProvider());

        

        [Fact]
        public void ShouldReturnNoRouteOkRequest()
        {
            var result = _controller.GetShortestRoute("YYZ", "ORD");
            var response = Assert.IsType <OkObjectResult>(result.Result);
            Assert.Equal("No Route", response.Value);
        }

        [Fact]
        public void ShouldReturnInvalidOriginBadRequest()
        {
            var result = _controller.GetShortestRoute("XXX", "ORD");
            var response = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Invalid Origin", response.Value);
        }

        [Fact]
        public void ShouldReturnInvalidDestinationBadRequest()
        {
            var result = _controller.GetShortestRoute("ORD", "XXX");
            var response = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Invalid Destination", response.Value);
        }
    }
}