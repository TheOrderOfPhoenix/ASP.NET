using MyLibrary;
using System;

class Program
{
    static void Main(string[] args)
    {
        // Create Library
        TheLibrary library = new TheLibrary("Central Library");

        // Create Bookshelf and Books
        Bookshelf shelf1 = new Bookshelf { ShelfNumber = 1 };
        shelf1.AddBook(new Book { Title = "C# Programming", Author = "John Doe" });
        shelf1.AddBook(new Book { Title = "Data Structures", Author = "Jane Smith" });

        // Add Bookshelf to Library (Composition)
        library.AddBookshelf(shelf1);

        // Display Library Info
        library.DisplayLibraryInfo();

        // Create Member with Aggregated Address
        Address memberAddress = new Address
        {
            Street = "123 Main St",
            City = "Metropolis",
            State = "NY",
            Pincode = "10001"
        };

        Member member = new Member
        {
            Name = "Alice",
            Age = 25,
            MemberAddress = memberAddress,
            Card = new LibraryCard { CardNumber = 101, IssuedDate = DateTime.Now }
        };

        // Create Librarian
        Librarian librarian = new Librarian
        {
            Name = "Mr. Smith",
            Age = 40
        };

        // Issue Book (Association)
        Book selectedBook = shelf1.Books[0]; // Select the first book on the shelf
        librarian.IssueBook(member, selectedBook);

        // Display Member Info and Address
        Console.WriteLine($"Member Info:\nName: {member.Name}\nAddress: {member.MemberAddress.Street}, {member.MemberAddress.City}");
        Console.ReadLine();
    }
}