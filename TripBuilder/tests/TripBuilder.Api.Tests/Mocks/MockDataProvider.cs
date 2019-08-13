using System.Collections.Generic;
using System.Collections.ObjectModel;
using TripBuilder.Api.DataProviding;
using TripBuilder.Api.DataProviding.Models;

namespace TripBuilder.Api.Tests.Mocks
{
    public sealed class MockDataProvider : IDataProvider
    {
        public ReadOnlyCollection<Airport> Airports { get; private set; }
        public ReadOnlyCollection<Route> Routes { get; private set; }

        public MockDataProvider()
        {
            GetRoutes();
            GetAirports();
        }

        private void GetRoutes()
        {
            Routes = new ReadOnlyCollection<Route>(new List<Route>
            {
                new Route {Origin = "YYZ", Destination = "JFK"},
                new Route {Origin = "JFK", Destination = "YYZ"},
                new Route {Origin = "LAX", Destination = "YVR"},
                new Route {Origin = "YVR", Destination = "LAX"},
                new Route {Origin = "LAX", Destination = "JFK"},
                new Route {Origin = "JFK", Destination = "LAX"}
            });
        }

        private void GetAirports()
        {
            Airports = new ReadOnlyCollection<Airport>(new List<Airport>
            {
                new Airport {CityName = "New York", AirportCode = "JFK"},
                new Airport {CityName = "Toronto", AirportCode = "YYZ"},
                new Airport {CityName = "Los Angeles", AirportCode = "LAX"},
                new Airport {CityName = "Vancouver", AirportCode = "YVR"},
                new Airport {CityName = "Chicago", AirportCode = "ORD"}
            });
        }
    }
}