namespace LZW.Dependencies;

internal class Trie
{
    private class Node
    {
        public Dictionary<byte, Node> Children { get; set; } = new();
        public int Number { get; set; }
        public Node(int number)
        {
            Number = number;
        }
        public Node() { }
    }
    
    public Trie()
    {
        for (int i = 0; i < 256; ++i)
        {
            root.Children[(byte)i] = new Node(Size++);
        }
    }

    public int Add(ref int startIndex, byte[] bytes)
    {
        ArgumentNullException.ThrowIfNull(bytes);
        var currentNode = root;
        int shift = -1;
        for (int i = startIndex; i < bytes.Length; ++i)
        {
            if (!currentNode.Children.ContainsKey(bytes[i]))
            {
                Node newNode = new(Size++);
                currentNode.Children[bytes[i]] = newNode;
                break;
            }
            currentNode = currentNode.Children[bytes[i]];
            ++shift;
        }
        startIndex += shift;
        return currentNode.Number;
    }


    public int Size { get; private set; } = 0;
    private readonly Node root = new();
}
