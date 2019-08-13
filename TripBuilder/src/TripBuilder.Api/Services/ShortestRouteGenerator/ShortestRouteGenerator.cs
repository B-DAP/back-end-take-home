using System.Collections.Generic;
using System.Linq;
using TripBuilder.Api.DataProviding;
using TripBuilder.Api.Services.ShortestRouteGenerator.Models;

namespace TripBuilder.Api.Services.ShortestRouteGenerator
{
    public sealed class ShortestRouteGenerator : IShortestRouteGenerator
    {
        private readonly IDataProvider _dataProvider;

        public ShortestRouteGenerator(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public string GenerateShortestRoute(string origin, string destination)
        {
            var graph = GenerateGraph();
            
            var path = FindShortestRoute(graph, origin, destination);

            return path == null ? null : string.Join(" -> ", path.Select(x => x));
        }

        private Graph GenerateGraph()
        {
            var graph = new Graph
            {
                Nodes = new List<Node>()
            };

            foreach (var airport in _dataProvider.Airports)
            {
                graph.Nodes.Add(new Node
                {
                    Airport = airport.AirportCode,
                    Children = new List<Node>()
                });
            }

            foreach (var route in _dataProvider.Routes)
            {
                var originNode = graph.Nodes.Single(x => x.Airport == route.Origin);
                var destinationNode = graph.Nodes.Single(x => x.Airport == route.Destination);
                originNode.Children.Add(destinationNode);
            }

            return graph;
        }

        private static List<string> FindShortestRoute(Graph graph, string origin, string destination)
        {
            var root = graph.Nodes.Single(x => x.Airport == origin);
            var path = new List<string>();
            var queue = new Queue<Node>();

            root.Visited = true;
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var r = queue.Dequeue();

                r.Visited = true;
            
                if (r.Airport == destination)
                {
                    path = CreatePath(graph, r, origin);
                    break;
                }

                foreach (var n in r.Children.Where(n => !n.Visited))
                {
                    n.VisitedFrom = r.Airport;
                    n.Visited = true;
                    queue.Enqueue(n);
                }
            }

            return path.Last() != destination ? null : path;
        }

        private static List<string> CreatePath(Graph graph, Node node, string origin)
        {
            var path = new List<string> {node.Airport};
            
            while (node.Airport != origin)
            {
                path.Add(node.VisitedFrom);
                node = graph.Nodes.Single(x => x.Airport == node.VisitedFrom);
            }

            path.Reverse();
            return path;
        }
    }
}