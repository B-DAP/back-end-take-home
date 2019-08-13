using System.Collections.Generic;

namespace TripBuilder.Api.Services.ShortestRouteGenerator.Models
{
    public class Node
    {
        public string Airport { get; set; }
        public bool Visited { get; set; }
        public string VisitedFrom { get; set; }
        public List<Node> Children { get; set; }
    }
}