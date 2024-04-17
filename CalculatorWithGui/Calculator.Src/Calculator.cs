namespace CalculatorAlgorithm;

using System.Drawing.Design;
using CalculatorAlgorithm;

/// <summary>
/// Calculator supporitng operations +, -, /, * 
/// Input is being done character by character with operations "C", "±", "%", "/", "7", "8", "9",
/// "*", "4", "5", "6", "-", "1", "2", "3", "+", "0", ",", "=" standing for clear, negate current value, percent, divide, sum, substract, multiply, numbers in range of 0 to 9, 
/// ',' for writing float values and get answer for current expression
/// Calculator evaluates expressions in process of writing so for example if you write '2', '+', '2', '+', '3', '=' into it, it will return a sequence of strings "2", "2+", 
/// "2+2", "4+", "4+3", "7"
/// </summary>
public class Calculator
{
  private int state = 0;
  private double currentNumber = 0;
  private char currentOperation = ' ';
  private bool afterPoint = false;
  private double divisor = 1;

  private readonly CalculatorOperations calculator = new();

  /// <summary>
  /// Next operation of calculator
  /// </summary>
  /// <param name="input">Next character in expression</param>
  /// <returns>Current result of calculating the expression</returns>
  public string NextStep(char input)
  {

    if (input >= '0' && input <= '9')
    {
      if (afterPoint)
      {
        divisor *= 10;
      }
      else
      {
        currentNumber *= 10;
      }
      currentNumber += (input - '0') / divisor;
      if (state == 1)
      {
        state = 2;
      }
    }
    else if (input == 'C')
    {
      calculator.CurrentNumber = 0;
      currentNumber = 0;
      divisor = 1;
      state = 0;
      afterPoint = false;
    }
    else if (input == '%')
    {
      if (state == 0 || state == 2)
      {
        currentNumber /= 100;
      }
    }
    else if (input == ',')
    {
      afterPoint = true;
    }
    else if (input == '=')
    {
      if (state >= 1)
      {
        if (state == 2)
        {
          try
          {
            calculator.Operation(currentOperation, currentNumber);
          }
          catch (DivideByZeroException)
          {
            return "Error";
          }
        }

        currentNumber = calculator.CurrentNumber;
        calculator.CurrentNumber = 0;
        state = 0;
      }
    }
    else if (input == '±')
    {
      currentNumber *= -1;
    }
    else
    {
      if (state == 0)
      {
        calculator.CurrentNumber = currentNumber;
        currentNumber = 0;
        state = 1;
      }
      if (state == 2)
      {
        try
        {
          calculator.Operation(currentOperation, currentNumber);
        }
        catch (DivideByZeroException)
        {
          return "Error";
        }
        currentNumber = 0;
        state = 1;
      }

      currentOperation = input;
    }

    string answer = "";
    switch (state)
    {
      case 0:
        {
          answer += $"{currentNumber}";
          break;
        }
      case 1:
        {
          answer += $"{calculator.CurrentNumber}{currentOperation}";
          break;
        }
      case 2:
        {
          answer += $"{calculator.CurrentNumber}{currentOperation}{currentNumber}";
          break;
        }
    }
    if (afterPoint && divisor == 1)
    {
      answer += ",";
    }

    return answer;
  }
}
