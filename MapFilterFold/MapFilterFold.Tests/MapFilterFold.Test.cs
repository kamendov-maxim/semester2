using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace MapFilterFold.Tests;

using MyFunctions;

public class Tests
{
    private static readonly List<int>? intInput = [23, 543, 654, 1, 0, 5];
    private static readonly List<string> stringInput = ["abcdefg", "qwerty", "jbdfg;abf"];

    [Test]
    public void MapTest()
    {
        List<int> intAnswers = [1, 1, 0, 1, 0, 1];
        var intResult = MapFilterFold.Map<int>(intInput, x => x % 2);
        Assert.That(intResult, Is.EqualTo(intAnswers));

        List<string> stringsAnswers = ["aceg", "qet", "jdgaf"];
        var stringResult = MapFilterFold.Map(stringInput, RemoveEven);
        Assert.That(stringResult, Is.EqualTo(stringsAnswers));
    }

    [Test]
    public void FilterTest()
    {
        List<int> intAnswers = [654, 0];
        var intResult = MapFilterFold.Filter<int>(intInput, x => x % 2 == 0);
        Assert.That(intResult, Is.EqualTo(intAnswers));

        List<string> stringsAnswers = ["qwerty"];
        var stringResult = MapFilterFold.Filter(stringInput, x => x.Length == 6);
        Assert.That(stringResult, Is.EqualTo(stringsAnswers));
    }

    [Test]
    public void FoldTest()
    {
        int intAnswer = 1226;
        var intResult = MapFilterFold.Fold<int>(intInput, 0, (x, y) => x + y);
        Assert.That(intResult, Is.EqualTo(intAnswer));

        string stringAnswer = "aqj";
        var stringResult = MapFilterFold.Fold(stringInput, "", BuildString);
        Assert.That(stringResult, Is.EqualTo(stringAnswer));
    }

    static string BuildString(string startValue, string currentValue) => startValue + currentValue[0];

    static string RemoveEven(string input)
    {
        var array = new char[input.Length / 2 + (input.Length % 2 == 0 ? 0 : 1)];
        int c = 0;
        for (int i = 0; i < input.Length; ++i)
        {
            if (i % 2 == 0)
            {
                array[c] = input[i];
                c += 1;
            }
        }
        return new string(array);
    }
}
