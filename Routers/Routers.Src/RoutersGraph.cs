namespace Routers;

/// <summary>
/// Implementation of graph datatype created specifically for network connections representation
/// </summary>
internal class RoutersGraph
{
    private readonly List<Edge> Edges;
    public int Size => vertices.Count;

    private readonly HashSet<int> vertices;

    /// <summary>
    /// Edge of graph
    /// </summary>
    /// <param name="firstVertex">Number of first vertex</param>
    /// <param name="secondVertex">Number of second vertex<</param>
    /// <param name="bandwith">Bandwith of a network</param>
    public class Edge(int firstVertex, int secondVertex, int bandwith)
    {
        public int Bandwith { get; } = bandwith;
        public int FirstVertex { get; } = firstVertex;
        public int SecondVertex { get; } = secondVertex;
    }

    /// <summary>
    /// Add edge to graph
    /// </summary>
    /// <param name="firstVertex">Number of first vertex</param>
    /// <param name="secondVertex">Number of second vertex<</param>
    /// <param name="bandwith">Bandwith of a network</param>
    public void AddEdge(int firstVertex, int secondVertex, int bandwith)
    {
        Edges?.Add(new Edge(firstVertex, secondVertex, bandwith));
        vertices.Add(firstVertex);
        vertices.Add(secondVertex);
    }

    /// <summary>
    /// Get all edges current graph contains
    /// </summary>
    /// <returns>Copy of all edges in an enumerable collection</returns>
    public IEnumerable<Edge> GetEdges()
    {
        if (Edges != null)
        {
            return Edges.AsReadOnly();
        }
        else
        {
            return [];
        }
    }

    public RoutersGraph()
    {
        Edges = [];
        vertices = [];
    }
}
