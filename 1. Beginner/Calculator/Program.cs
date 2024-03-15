using Calculator;
bool endApp = false;

Console.WriteLine("""
	Console Calculator in C#
	------------------------
	""");

while (!endApp)
{
	string? numInput1;
	string? numInput2;
	string? op;
	double result = 0;

	//First number input
	Console.Write("Enter the first number: ");
	numInput1 = Console.ReadLine();

	double cleanNum1 = 0;
	while (!double.TryParse(numInput1, out cleanNum1))
	{
		Console.Write("Value must be a number: ");
		numInput1 = Console.ReadLine();
	}

	//Second number input
	Console.Write("Enter the second number: ");
	numInput2 = Console.ReadLine();

	double cleanNum2 = 0;
	while (!Double.TryParse(numInput2, out cleanNum2))
	{
		Console.Write("Value must be a number: ");
		numInput2 = Console.ReadLine();
	}

	//Operator selection
	Console.WriteLine("""
		Choose Operation to perform:
		A - Add
		S - Subtract
		M - Multiply
		D - Divide
		""");
	Console.Write("Menu Selection: ");
	op = Console.ReadLine();

	try
	{
		result = Operations.DoOperation(cleanNum1, cleanNum2, op);
		if (double.IsNaN(result))
			Console.WriteLine("This operation will result in a mathematical error.");
		else
			Console.WriteLine($"Result: {result:0.##}");
	}
	catch (Exception ex)
	{
		Console.WriteLine($"Exception occured whilst calculating: {ex.Message}");
	}

	Console.WriteLine("""
		------------------------
		Press any key to continue or 'Q' to quit
		""");
	if (Console.ReadKey(true).Key == ConsoleKey.Q) endApp = true;
}
return;