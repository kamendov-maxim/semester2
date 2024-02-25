using System.Buffers.Binary;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Data_Structure;
class Bor : IDataStructure
{
    public Bor()
    {
        this.Size = 0;
        this.root = new Node();
    }

    private class Node
    {
        public Node()
        {
            this.Children = new Dictionary<char, Node>();
        }

        public Dictionary<char, Node> Children;
        public int WordCount;
    }

    public bool Add(string element)
    {
        (bool exists, Node node, int charNumber) = FindNode(this.root, element, 0);
        if (exists)
        {
            return false;
        }

        for (int i = charNumber; i < element.Length; ++i)
        {
            Node newNode = new Node();
            node.Children.Add(element[i], newNode);
            node = newNode;
            ++this.Size;
        }
        ++globalWordCount;
        node.WordCount = globalWordCount;
        return true;
    }

    private static Tuple<bool, Node, int> FindNode(Node root, string element, int currentChar)
    {
        if (currentChar == element.Length)
        {
            return Tuple.Create(true, root, currentChar);
        }

        if (root.Children.ContainsKey(element[currentChar]))
        {
            return FindNode(root.Children[element[currentChar]], element, ++currentChar);
        }
        else
        {
            return Tuple.Create(false, root, currentChar);
        }
    }

    public bool Contains(string element)
    {
        (bool exists, Node node, int charNumber) = FindNode(root, element, 0);
        return exists;
    }

    public bool Remove(string element)
    {
        (bool wordWasHere, bool f) = RemoveRecursion(this.root, element, 0);
        return wordWasHere;
    }

    private Tuple<bool, bool> RemoveRecursion(Node root, string element, int charNumber)
    {
        bool wordWasHere = false;
        bool finish = false;
        if (charNumber == element.Length)
        {
            return Tuple.Create(true, root.Children.Count > 0);
        }
        if (root.Children.ContainsKey(element[charNumber]))
        {
            (wordWasHere, finish) = RemoveRecursion(root.Children[element[charNumber]], element, charNumber + 1);
        }
        else
        {
            return Tuple.Create(false, true);
        }

        if (wordWasHere && !finish)
        {
            root.Children.Remove(element[charNumber]);
        }
        if (!finish)
        {
            finish = root.Children.Count > 0;
        }
        return Tuple.Create(wordWasHere, finish);
    }

    public int HowManyStartsWithPrefix(string prefix)
    {
        (bool exists, Node node, int charNumber) = FindNode(root, prefix, 0);
        return exists ? node.Children.Count : 0;
    }

    public int Size;
    private Node root;
    private int globalWordCount;
}
