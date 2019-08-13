using System.Collections.ObjectModel;
using TripBuilder.Api.DataProviding.Models;

namespace TripBuilder.Api.DataProviding
{
    public interface IDataProvider
    {
        ReadOnlyCollection<Airport> Airports { get; }
        ReadOnlyCollection<Route> Routes { get; }
    }
}