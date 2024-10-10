namespace Zachet;

/// <summary>
/// Null checker for ints which considers zeroes as nulls
/// </summary>
public class IntNullChecker : INullChecker<int>
{
    /// <summary>
    /// check if element is null
    /// </summary>
    /// <param name="value">element to check</param>
    /// <returns></returns>
    public bool IsNull(int value)
    {
        return value == 0;
    }
}
