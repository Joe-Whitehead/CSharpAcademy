namespace LibraryManagementSystem;

internal static class MockDatabase
{
    internal static List<string> books =
    [
     "The Great Gatsby", "To Kill a Mockingbird", "1984", "Pride and Prejudice", "The Catcher in the Rye", "The Hobbit", "War and Peace", "The Odyssey",
     "The Lord of the Rings", "Jane Eyre", "Animal Farm", "Brave New World", "The Chronicles of Narnia", "The Diary of a Young Girl", "The Alchemist",
     "Wuthering Heights", "Fahrenheit 451", "Catch-22", "The Hitchhiker's Guide to the Galaxy"
    ];

    internal static List<Book> Books =
        [
            new Book("The Great Gatsby", 180),
            new Book("To Kill a Mockingbird", 281),
            new Book("1984", 328),
            new Book("Pride and Prejudice", 432),
            new Book("The Catcher in the Rye", 277),
            new Book("The Hobbit", 310),
            new Book("Moby-Dick", 585),
            new Book("War and Peace", 1225),
            new Book("The Odyssey", 400),
            new Book("The Lord of the Rings", 1178),
            new Book("Jane Eyre", 500),
            new Book("Animal Farm", 112),
            new Book("Brave New World", 268),
            new Book("The Chronicles of Narnia", 768),
            new Book("The Diary of a Young Girl", 283),
            new Book("The Alchemist", 208),
            new Book("Wuthering Heights", 400),
            new Book("Fahrenheit 451", 158),
            new Book("Catch-22", 453),
            new Book("The Hitchhiker's Guide to the Galaxy", 224)
        ];
}
