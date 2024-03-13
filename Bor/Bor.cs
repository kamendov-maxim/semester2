using System.Buffers.Binary;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Data_Structure;
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
            // Node newTrie;
            if (tempRoot.Children.Keys.Contains(element[i]))
            {
                tempRoot = tempRoot.Children[element[i]];
            }
            else
            {
                answer = true;
                Node newTrie = new Node();

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
            if (tempRoot.Children.Keys.Contains(element[i]))
            {
                tempRoot = tempRoot.Children[element[i]];

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
            if (wordWasHere && charNumber == element.Length - 1)
            {
                root.endOfWord = false;
            }
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

    private int PrefixRecursion(Node root)
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
    private Node root;
    private int globalWordCount;
}
