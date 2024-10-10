namespace finalTest1;

/// <summary>
/// Class containing generic bubble sort implementation
/// </summary>
public class Sorting
{
    /// <summary>
    /// Generic bubble sort implementation
    /// </summary>
    /// <typeparam name="T">Type implementing IComparable interface</typeparam>
    /// <param name="list">list to sort</param>
    /// <param name="comparer">IComparer which will be used to sort elements in the list</param>
    public static void BubbleSort<T>(IList<T> list, IComparer<T> comparer)
    {
        ArgumentNullException.ThrowIfNull(list, "List is null");
        ArgumentNullException.ThrowIfNull(comparer, "Comparer is null");
        for (int i = 0; i < list.Count - 1; ++i)
        {
            bool swapped = false;
            for (int j = 0; j < list.Count - 1; ++j)
            {
                if (comparer.Compare(list[j], list[j + 1]) > 0)
                {
                    (list[j], list[j + 1]) = (list[j + 1], list[j]);
                    swapped = true;
                }
            }
            if (!swapped)
            {
                break;
            }
        }
    }
}
