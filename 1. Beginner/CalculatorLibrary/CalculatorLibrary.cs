namespace CalculatorLibrary;
using System.Diagnostics;
using System.Transactions;
using Newtonsoft.Json;

public class CalcLib
{
    private readonly JsonWriter _writer;
    private readonly JsonReader _reader;
    private List<Calculation> _calculations;
    public CalcLib()
    {
        if(File.Exists("Calculations.json"))
        {
            _calculations = JsonConvert.DeserializeObject<List<Calculation>>(File.ReadAllText("Calculations.json"));
        }
        else _calculations = [];
    }

    public double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN;
        
        switch (op.Trim().ToLower())
        {
            case "a":
                result = num1 + num2;
                AddToList(num1, num2, result, "+");
                break;

            case "s":
                result = num1 - num2;
                AddToList(num1, num2, result, "-");
                break;

            case "m":
                result = num1 * num2;
                AddToList(num1, num2, result, "*");
                break;

            case "d":
                if (num2 != 0)
                {
                    result = num1 / num2;
                    AddToList(num1, num2, result, "/");
                }
                break;

            default:
                break;
        }        
        return result;
    }

    private void AddToList(double num1, double num2, double result, string operation)
    {
        _calculations.Add(new Calculation(num1, operation, num2, result));
    }

    public void Finish()
    {
        File.WriteAllText("Calculations.json", JsonConvert.SerializeObject(_calculations));

        using (StreamWriter file = File.CreateText("Calculations.json"))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, _calculations);
        }
    }

    public IEnumerable<Calculation> PreviousCalculations
    {
        get { return _calculations; }
    }

    public record Calculation(double Operand1, string Operation, double Operand2, double Result);
}


