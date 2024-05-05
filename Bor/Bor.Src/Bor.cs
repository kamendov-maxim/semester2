using System.Buffers.Binary;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Data_Structures;
public class Bor : IDataStructure
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
            endOfWord = false;
        }

        public Dictionary<char, Node> Children;
        public bool endOfWord;
    }

    public bool Add(string element)
    {
        Node tempRoot = root;
        int total = element.Length - 1;
        bool answer = false;
        for (int i = 0; i < element.Length; i++)
        {
            if (tempRoot.Children.TryGetValue(element[i], out Node? value))
            {
                tempRoot = value;
                if (!tempRoot.endOfWord && i == element.Length - 1)
                {
                    tempRoot.endOfWord = true;
                    return true;
                }
            }
            else
            {
                answer = true;
                var newTrie = new Node();

                if (total == i)
                {
                    newTrie.endOfWord = true;
                }

                tempRoot.Children.Add(element[i], newTrie);
                tempRoot = newTrie;
            }
        }
        return answer;
    }

    public bool Contains(string element)
    {
        Node tempRoot = root;
        int total = element.Length - 1;
        for (int i = 0; i < element.Length; i++)
        {
            if (tempRoot.Children.TryGetValue(element[i], out Node? value))
            {
                tempRoot = value;

                if (total == i)
                {
                    if (tempRoot.endOfWord == true)
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public bool Remove(string element)
    {
        Node? nodeFromWhereToDelete = null;
        char startOfWordToDelete = default;
        var previousNode = root;
        int i = 0;
        for (i = 0; i < element.Length; ++i)
        {
            if (previousNode.Children.TryGetValue(element[i], out Node? currentNode))
            {
                if (i + 1 == element.Length && currentNode.endOfWord == true && currentNode.Children.Count > 0)
                {
                    currentNode.endOfWord = false;
                    return true;
                }
                if (nodeFromWhereToDelete is null || currentNode.Children.Count > 1)
                {
                    nodeFromWhereToDelete = currentNode;
                    startOfWordToDelete = element[i + 1];
                }

                previousNode = currentNode;
            }
            else
            {
                return false;
            }
        }

        if (nodeFromWhereToDelete is null)
        {
            throw new InvalidOperationException("Trie does not work correclty");
        }

        nodeFromWhereToDelete.Children.Remove(startOfWordToDelete);

        return true;
    }

    public int HowManyStartsWithPrefix(string prefix)
    {
        Node tempNode = root;
        int total = prefix.Length - 1;
        for (int i = 0; i < prefix.Length; i++)
        {
            if (tempNode.Children.ContainsKey(prefix[i]))
            {
                tempNode = tempNode.Children[prefix[i]];
            }
            else
            {
                return 0;
            }
        }
        return PrefixRecursion(tempNode);
    }

    private static int PrefixRecursion(Node root)
    {
        int answer = 0;
        var nodes = root.Children.Select(x => x.Value);
        foreach (var node in nodes)
        {
            if (node.endOfWord)
            {
                ++answer;
            }
            answer += PrefixRecursion(node);
        }
        return answer;
    }

    public int Size;
    private readonly Node root;
}
