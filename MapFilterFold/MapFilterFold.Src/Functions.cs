using System.Security.Cryptography;
using System.Xml.XPath;

namespace MyFunctions;

public class MapFilterFold
{
    public static List<T> Map<T>(List<T>? input, Func<T, T>? Function)
    {
        ArgumentNullException.ThrowIfNull(input);
        ArgumentNullException.ThrowIfNull(Function);
        var result = new List<T>();
        foreach (var item in input)
        {
            result.Add(Function(item));
        }
        return result;
    }

    public static List<T> Filter<T>(List<T>? input, Func<T, bool>? Filter)
    {
        ArgumentNullException.ThrowIfNull(input);
        ArgumentNullException.ThrowIfNull(Filter);
        var result = new List<T>();
        foreach (var item in input)
        {
            if (Filter(item))
            {
                result.Add(item);
            }
        }
        return result;
    }

    public static T Fold<T>(List<T>? input, T startValue, Func<T, T, T>? AccumulateValue)
    {
        ArgumentNullException.ThrowIfNull(input);
        ArgumentNullException.ThrowIfNull(AccumulateValue);
        T result = startValue;
        foreach (var item in input)
        {
            result = AccumulateValue(result, item);
        }
        return result;
    }
}
