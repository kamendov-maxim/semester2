using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Routers;

/// <summary>
/// instrument for calculationg the best configuration of network
/// </summary>
public class Configurator
{
    private static RoutersGraph? initialGraph;
    private static HashSet<int>? connectedVertices;

    /// <summary>
    /// Calculates best configuration based on configuration from input file and writes it to outputfile
    /// </summary>
    /// <param name="pathToInputFile">Path to input file</param>
    /// <param name="pathToOutputFile">Path to outpput file</param>
    /// <exception cref="ArgumentException">Thrown if path strings are null or empty</exception>
    /// <exception cref="NetworkIsNotConnectedException">Thrown if network from input file is not connected</exception>
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
