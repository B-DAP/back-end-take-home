using CsvHelper.Configuration.Attributes;

namespace TripBuilder.Api.DataProviding.Models
{
    public sealed class Airport
    {
        [Name("IATA 3")]
        public string AirportCode { get; set; }
        
        [Name("City")]
        public string CityName { get; set; }
    }
}