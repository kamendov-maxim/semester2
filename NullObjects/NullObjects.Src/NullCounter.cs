namespace Zachet;

/// <summary>
/// Class with method to count null objects in IEnumerable 
/// </summary>
public static class NullCounter
{
    /// <summary>
    /// Count null objects int collection
    /// </summary>
    /// <typeparam name="T">Any type for which you have null checker</typeparam>
    /// <param name="list">Collection implementing IEnumerable (to iterate through it)</param>
    /// <param name="nullChecker">Object which can check if element of T type is null</param>
    /// <returns>Count of null objects</returns>
    public static int CountNullObjects<T>(IEnumerable<T> list, INullChecker<T> nullChecker)
    {
        int count = 0;
        foreach (var item in list)
        {
            if (nullChecker.IsNull(item))
            {
                ++count;
            }
        }
        return count;
    }
}
