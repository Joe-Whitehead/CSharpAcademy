using CalculatorLibrary;

//Program Variables
bool endApp = false;
int counter = 0;
CalcLib calculator = new();

while (!endApp)
{
    double operand1;
    double operand2;
    string? op;
    double result = 0;
	
    Console.WriteLine($"""
	Console Calculator in C#
	------------------------
	Calculations this session: {counter}
	Calculations Total: {calculator.PreviousCalculations.Count()}
	""");

	if (calculator.PreviousCalculations.Any())
	{
		Console.WriteLine("""
		Previous Calculations:
		----------------------
		""");
		int count = 0;
		var prevList = calculator.PreviousCalculations.GetEnumerator();
		while (prevList.MoveNext())
		{
			count++;
			Console.WriteLine($"{count}: {prevList.Current.Operand1} {prevList.Current.Operation} {prevList.Current.Operand2} = {prevList.Current.Result:0.##}");
		}
		Console.WriteLine("----------------------");
	}

    //First number input
    Console.Write("Enter the first number: ");
	operand1 = ValidateNumber();

    //Second number input
    Console.Write("Enter the second number: ");
    operand2 = ValidateNumber();

    //Operator selection
    Console.WriteLine("""
		Choose Operation to perform:
		A - Add
		S - Subtract
		M - Multiply
		D - Divide
		""");
    Console.Write("Menu Selection: ");
	op = ValidateMenu();

    try
	{
		result = calculator.DoOperation(operand1, operand2, op);
		if (double.IsNaN(result))
			Console.WriteLine("This operation will result in a mathematical error.");
		else
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Green; ;
			Console.WriteLine($"Result: {result:0.##}");
			Console.ResetColor();
		}
	}
	catch (Exception ex)
	{
		Console.WriteLine($"Exception occured whilst calculating: {ex.Message}");
	}
	counter++;

	Console.WriteLine("""
		------------------------
		Press any key to continue or 'Q' to quit
		""");
	if (Console.ReadKey(true).Key == ConsoleKey.Q) endApp = true;
	Console.Clear();
}
calculator.Finish();
return;

double ValidateNumber()
{
	string? input = Console.ReadLine();
	double validNumber;
    while (!double.TryParse(input, out validNumber))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Value must be a number: ");
        Console.ResetColor();
        input = Console.ReadLine();
    }
    return validNumber;
}

string ValidateMenu()
{
    string[] menuOptions = { "a", "s", "m", "d" };
    string? input = Console.ReadLine();

	while (string.IsNullOrEmpty(input) || !menuOptions.Contains(input.Trim().ToLower()))
	{
		Console.ForegroundColor = ConsoleColor.Red;
		Console.Write("Please select a valid menu option: ");
		Console.ResetColor();
		input = Console.ReadLine();
	}

	return input;
}