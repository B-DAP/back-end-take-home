using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TripBuilder.Api.DataProviding;
using TripBuilder.Api.Services.ShortestRouteGenerator;

namespace TripBuilder.Api.Endpoints.TripBuilder
{
    [Route("routes")]
    public sealed class TripBuilderController : Controller
    {
        private readonly IShortestRouteGenerator _shortestRouteGenerator;
        private readonly IDataProvider _dataProvider;
        public TripBuilderController(IShortestRouteGenerator shortestRouteGenerator, IDataProvider dataProvider)
        {
            _shortestRouteGenerator = shortestRouteGenerator;
            _dataProvider = dataProvider;
        }

        /// <summary>
        /// Get shortest route between origin and destination
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        [HttpGet("{origin}/{destination}")]
        public ActionResult<string> GetShortestRoute(string origin, string destination)
        {
            try
            {
                if (!_dataProvider.Airports.Any(x => x.AirportCode.Equals(origin, StringComparison.OrdinalIgnoreCase)))
                {
                    return BadRequest("Invalid Origin");
                }

                if (!_dataProvider.Airports.Any(x =>
                    x.AirportCode.Equals(destination, StringComparison.OrdinalIgnoreCase)))
                {
                    return BadRequest("Invalid Destination");
                }

                var result = _shortestRouteGenerator.GenerateShortestRoute(origin.ToUpper(), destination.ToUpper());

                return Ok(String.IsNullOrEmpty(result) ? "No Route" : result);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error has occured: {ex.Message}");
            }
        }
    }
}