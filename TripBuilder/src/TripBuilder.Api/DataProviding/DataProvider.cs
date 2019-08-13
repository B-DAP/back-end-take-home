using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using CsvHelper;
using TripBuilder.Api.DataProviding.Models;

namespace TripBuilder.Api.DataProviding
{
    public sealed class DataProvider : IDataProvider
    {
        public ReadOnlyCollection<Airport> Airports { get; private set; }
        public ReadOnlyCollection<Route> Routes { get; private set; }

        public DataProvider()
        {
            GetRoutes();
            GetAirports();
        }

        private void GetRoutes()
        {
            using (var reader = new StreamReader("Data\\routes.csv"))
            {
                using (var csvReader = new CsvReader(reader))
                {
                    var routes = csvReader.GetRecords<Route>();
                    Routes = new ReadOnlyCollection<Route>(routes.ToList());
                }
            }
        }

        private void GetAirports()
        {
            using (var reader = new StreamReader("Data\\airports.csv"))
            {
                using (var csvReader = new CsvReader(reader))
                {
                    var airports = csvReader.GetRecords<Airport>();
                    Airports = new ReadOnlyCollection<Airport>(airports.ToList());
                }
            }
        }
    }
}