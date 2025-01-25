using Spectre.Console;

string[] menuChoices = ["View Books", "Add Book", "Delete Book"];

var choice = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
    .Title("What do you want to do next?")
    .AddChoices(menuChoices));
