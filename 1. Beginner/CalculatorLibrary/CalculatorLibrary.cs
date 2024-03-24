namespace CalculatorLibrary;
using System.Text.Json;

public class CalcLib
{
    private const string _jsonFileName = "Calculations.json";
    private List<Calculation> _calculations = new();

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

    public async Task OpenFile()
    {
        if (File.Exists(_jsonFileName))
        {
            await using FileStream openStream = File.OpenRead(_jsonFileName);
            _calculations = await JsonSerializer.DeserializeAsync<List<Calculation>>(openStream);
        }        
    }

    public async Task SaveFile()
    {
        await using FileStream createStream  =File.Create(_jsonFileName);
        await JsonSerializer.SerializeAsync(createStream, _calculations);
    }

    public IEnumerable<Calculation> PreviousCalculations => _calculations;

    public record Calculation(double Operand1, string Operation, double Operand2, double Result);
}