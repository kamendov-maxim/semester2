using System.Text;
namespace Routers;

internal class IO
{
    public static RoutersGraph Read(string path)
    {
        var text = File.ReadAllText(path);
        var graph = new RoutersGraph();
        char[] separators = [' ', ':', ',', '(', ')'];
        foreach (var line in text.Split('\n'))
        {
            int currentVertex = 0;
            int currentSecondVertex = 0;
            int currentBandwith = 0;
            int counter = 0;
            foreach (var item in line.Split(separators, StringSplitOptions.RemoveEmptyEntries))
            {
                if (counter == 0)
                {
                    currentVertex = int.Parse(item);
                }
                else
                {
                    if (counter % 2 != 0)
                    {
                        currentSecondVertex = int.Parse(item);
                    }
                    else
                    {
                        currentBandwith = int.Parse(item);
                        graph.AddEdge(currentVertex, currentSecondVertex, currentBandwith);
                    }
                }
                ++counter;
            }
        }
        return graph;
    }

    public static void Write(RoutersGraph graph, string path)
    {
        ArgumentNullException.ThrowIfNull(graph);
        var sb = new StringBuilder();
        int currentFirstVertex = -1;
        foreach (var edge in graph.GetEdges())
        {
            if (edge.FirstVertex != currentFirstVertex)
            {
                if (currentFirstVertex != -1)
                {
                    sb.Append("\n");
                }
                currentFirstVertex = edge.FirstVertex;
                sb.Append($"{currentFirstVertex}: {edge.SecondVertex} ({edge.Bandwith})");
            }
            else
            {
                sb.Append($", {edge.SecondVertex} ({edge.Bandwith})");
            }
        }
        File.WriteAllText(path, sb.ToString());
    }
}
