namespace TripBuilder.Api.Services.ShortestRouteGenerator
{
    public interface IShortestRouteGenerator
    {
        string GenerateShortestRoute(string origin, string destination);
    }
}