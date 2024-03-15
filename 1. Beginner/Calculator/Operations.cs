namespace Calculator;

internal class Operations
{
	public static double DoOperation(double num1, double num2, string op) 
	{
		return op.Trim().ToLower() switch
		{
			"a" => num1 + num2,
			"s" => num1 - num2,
			"m" => num1 * num2,
			"d" => num1 / num2,
			_ => 0
		};
	}
}
