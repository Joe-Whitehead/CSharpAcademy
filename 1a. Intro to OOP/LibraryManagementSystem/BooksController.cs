using Spectre.Console;

namespace LibraryManagementSystem;

internal class BooksController
{

    internal void ViewBooks()
    {
        AnsiConsole.MarkupLine("[yellow]List of Books: [/]");
        foreach (var book in MockDatabase.books)
        {
            AnsiConsole.MarkupLine($"- [cyan]{book}[/]");
        }

        AnsiConsole.MarkupLine("Press Any Key to Continue");
        Console.ReadKey();
    }

    internal  void AddBook()
    {
        var title = AnsiConsole.Ask<string>("Enter the [green]title[/] of the book: ");

        if (MockDatabase.books.Contains(title))
        {
            AnsiConsole.MarkupLine("[red]This book already exists.[/]");
        }
        else
        {
            MockDatabase.books.Add(title);
            AnsiConsole.MarkupLine("[green]Book added successfully[/]");
        }

        AnsiConsole.MarkupLine("Press Any Key to Continue");
        Console.ReadKey();
    }

    internal void DeleteBook()
    {
        if (MockDatabase.books.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No books available to delete.[/]");
            Console.ReadKey();
            return;
        }

        var bookToDelete = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Select a [red]book[/] to delete:")
            .AddChoices(MockDatabase.books));

        if (MockDatabase.books.Remove(bookToDelete))
        {
            AnsiConsole.MarkupLine("[red]Book deleted successfully[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Book not found.[/]");
        }

        AnsiConsole.MarkupLine("Press Any Key to COntinue");
        Console.ReadKey();
    }
}
