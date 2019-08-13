using TripBuilder.Api.Services.ShortestRouteGenerator;

namespace TripBuilder.Api.Tests.Mocks
{
    public sealed class MockShortestRouteGenerator : IShortestRouteGenerator
    {
        public string GenerateShortestRoute(string origin, string destination)
        {
            switch (origin)
            {
                case "YYZ" when destination == "JFK":
                    return "Toronto (YYZ) -> New York (JFK)";
                case "YYZ" when destination == "YVR":
                    return "Toronto (YYZ) -> New York (JFK) -> Los Angeles (LAX) -> Vancouver (YVR)";
                default:
                    return null;
            }
        }
    }
}