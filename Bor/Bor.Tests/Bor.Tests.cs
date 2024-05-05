namespace Bor.Tests;
using Data_Structures;

public class Tests
{
    Bor trie;
    readonly string[] testStrings = ["abcdef", "abcdrfg", "abuq", "abcdeyunkhnrlnh",
    "slgbfb;gajga", "sldgaslgnlkg", "agdfjhahg"];

    [SetUp]
    public void Setup()
    {
        trie = new();
    }

    [Test]
    public void Test1()
    {
        foreach (string testString in testStrings)
            trie.Add(testString);

        foreach (var testString in testStrings)
        {
            Assert.That(trie.Contains(testString));
        }

        trie.Remove("abcdef");

        foreach (var testString in testStrings)
        {
            if (testString == "abcdef")
            {
                Assert.That(!trie.Contains(testString));
            }
            else
            {
                Assert.That(trie.Contains(testString));
            }
        }

        trie.Remove("abcdeyunkhnrlnh");
        trie.Remove("slgbfb;gajga");

        foreach (var testString in testStrings)
        {
            if (testString == "abcdef" || testString == "abcdeyunkhnrlnh" || testString == "slgbfb;gajga")
            {
                Assert.That(!trie.Contains(testString));
            }
            else
            {
                Assert.That(trie.Contains(testString));
            }
        }
    }
}