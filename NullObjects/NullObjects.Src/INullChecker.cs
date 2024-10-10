namespace Zachet;

/// <summary>
/// Interface for null checker used in NullCounter 
/// </summary>
/// <typeparam name="T">Type of which will be elements you check for null</typeparam>
public interface INullChecker<T>
{
    /// <summary>
    /// Check if element is null
    /// </summary>
    /// <param name="element">element to check for null</param>
    /// <returns>True if element is null, false if not null</returns>
    bool IsNull(T element);
}
