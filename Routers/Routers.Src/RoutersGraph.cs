namespace Routers;

internal class RoutersGraph
{
    private readonly List<Edge> Edges;
    public int Size => vertices.Count;

    private readonly HashSet<int> vertices;

    public class Edge(int firstVertex, int secondVertex, int bandwith)
    {
        public int Bandwith { get; } = bandwith;
        public int FirstVertex { get; } = firstVertex;
        public int SecondVertex { get; } = secondVertex;
    }

    public void AddEdge(int firstVertex, int secondVertex, int bandwith)
    {
        Edges?.Add(new Edge(firstVertex, secondVertex, bandwith));
        vertices.Add(firstVertex);
        vertices.Add(secondVertex);
    }

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
