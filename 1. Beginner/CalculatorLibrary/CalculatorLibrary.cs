namespace CalculatorLibrary;
using System.Diagnostics;
using System.Transactions;
using Newtonsoft.Json;

public class CalcLib
{
    private readonly JsonWriter _writer;
    private List<Calculation> _calculations;
    public CalcLib()
    {
        StreamWriter logFile = File.CreateText("calculator.log");
        logFile.AutoFlush = true;
        _writer = new JsonTextWriter(logFile);
        _writer.Formatting = Formatting.Indented;
        _writer.WriteStartObject();
        _writer.WritePropertyName("Operation");
        _writer.WriteStartArray();
        _calculations = [];
    }

    public double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN;
        _writer.WriteStartObject();
        _writer.WritePropertyName("Operand1");
        _writer.WriteValue(num1);
        _writer.WritePropertyName("Operand2");
        _writer.WriteValue(num2);
        _writer.WritePropertyName("Operation");

        switch (op.Trim().ToLower())
        {
            case "a":
                result = num1 + num2;
                _writer.WriteValue("Add");
                _calculations.Add(new Calculation(num1, "+", num2, result));
                break;

            case "s":
                result = num1 - num2;
                _writer.WriteValue("Subtract");
                _calculations.Add(new Calculation(num1, "-", num2, result));
                break;

            case "m":
                result = num1 * num2;
                _writer.WriteValue("Multiply");
                _calculations.Add(new Calculation(num1, "*", num2, result));
                break;

            case "d":
                if (num2 != 0)
                {
                    result = num1 / num2;
                    _writer.WriteValue("Divide"); _calculations.Add(new Calculation(num1, "/", num2, result));
                }
                break;

            default:
                break;
        }
        _writer.WritePropertyName("Result");
        _writer.WriteValue(result);
        _writer.WriteEndObject();
        return result;
    }

    public void Finish()
    {
        _writer.WriteEndArray();
        _writer.WriteEndObject();
        _writer.Close();
    }

    public IEnumerable<Calculation> PreviousCalculations
    {
        get { return _calculations; }
    }

    public record Calculation(double Operand1, string Operation, double Operand2, double Result);
}


