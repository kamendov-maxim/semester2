using Microsoft.VisualBasic;

namespace Stack;

/// <summary>
/// Интерфейс для создания стека, хранящего значения типа double
/// </summary>
public interface IStack
{
    /// <summary>
    /// Метод для добавления элемента типа double в стек
    /// </summary>
    /// <param name="element">Элемент, который необходимо добавить</param>
    void Add(double element);

    /// <summary>
    /// Метод для извлечения верхнего элемента из стека
    /// </summary>
    /// <returns>double - значение верхнего элемента (-1, если его не было) и bool - true, если элемент был в стеке, false, если не было</returns>
    Tuple<double, bool> Pop();

    /// <summary>
    /// Количество элементов в стеке
    /// </summary>
    int Size();
}
