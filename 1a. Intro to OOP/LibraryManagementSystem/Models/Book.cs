using Spectre.Console;
namespace LibraryManagementSystem.Models;

internal class Book : LibraryItem
{
    internal string Author { get; set; }
    internal string Category { get; set; }
    internal int Pages { get; set; }

    internal Book(int id, string name, string author, string category, string location, int pages)
        : base(id, name, location)
    {
        Author = author; 
        Category = category;
        Pages = pages;
    }

    public override void DisplayDetails()
    {
        var panel = new Panel(new Markup($"""
            [bold]Book:[/] [cyan]{Name}[/] by [cyan]{Author}[/]"
            [bold]Pages:[/] {Pages}
            [bold]Category:[/] [green]{Category}[/]
            [bold]Location:[/] [blue]{Location}[/]
            """))
        {
            Border = BoxBorder.Rounded
        };

        AnsiConsole.Write(panel);
    }
}

