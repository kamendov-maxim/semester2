using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Routers;

public class Configurator
{
    private static RoutersGraph? initialGraph;
    private static HashSet<int>? connectedVertices;
    public static void Configure(string pathToInputFile, string pathToOutputFile)
    {
        if (pathToInputFile == string.Empty || pathToInputFile == null || pathToOutputFile == string.Empty || pathToOutputFile == null)
        {
            throw new ArgumentException("String should contain path to file");
        }

        initialGraph = IO.Read(pathToInputFile);
        connectedVertices = [1];
        
        var result = Algorithm();

        IO.Write(result, pathToOutputFile);
    }

    private static RoutersGraph Algorithm()
    {
        var result = new RoutersGraph();

        while (connectedVertices?.Count < initialGraph?.Size)
        {
            var bestEdge = FindBestEdge();
            if (bestEdge.Bandwith == -1)
            {
                throw new NetworkIsNotConnectedException("Network is not connected");
            }
            if (connectedVertices.Contains(bestEdge.FirstVertex))
            {
                connectedVertices.Add(bestEdge.SecondVertex);
            }
            else
            {
                connectedVertices.Add(bestEdge.FirstVertex);
            }
            result.AddEdge(bestEdge.FirstVertex, bestEdge.SecondVertex, bestEdge.Bandwith);
        }

        return result;
    }

    private static RoutersGraph.Edge FindBestEdge()
    {
        if (initialGraph == null || connectedVertices == null)
        {
            throw new FieldAccessException("Graph is corrupted");
        }

        RoutersGraph.Edge bestEdge = new(-1, -1, -1);

        foreach (var edge in initialGraph.GetEdges())
        {
            if ((connectedVertices.Contains(edge.FirstVertex) && !connectedVertices.Contains(edge.SecondVertex)) ||
            (!connectedVertices.Contains(edge.FirstVertex) && connectedVertices.Contains(edge.SecondVertex)))
            {
                if (edge.Bandwith > bestEdge.Bandwith)
                {
                    bestEdge = edge;
                }
            }
        }

        return bestEdge;
    }
}
