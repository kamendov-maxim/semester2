namespace CalculatorAlgorithm;

/// <summary>
/// Arithmetic operations for calculator
/// </summary>
internal class CalculatorOperations
{
    /// <summary>
    /// First operand
    /// </summary>
    public double CurrentNumber {get; set;}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="operation">Char '+', '-', '/' or '*' which represents the operation</param>
    /// <param name="operand">Second operand</param>
    /// <returns></returns>
    /// <exception cref="DivideByZeroException"></exception>
    public double Operation(char operation, double operand)
    {
        switch (operation)
        {
            case '+':
            {
                CurrentNumber += operand;
                break;
            }
            case '-':
            {
                CurrentNumber -= operand;
                break;
            }
            case '/':
            {
                if (operand == 0)
                {
                    throw new DivideByZeroException();
                }
                CurrentNumber /= operand;
                break;
            }
            case '*':
            {
                CurrentNumber *= operand;
                break;
            }
        }

        return CurrentNumber;
    }
}
