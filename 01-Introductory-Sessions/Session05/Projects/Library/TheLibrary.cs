using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    public class TheLibrary
    {
        public string LibraryName { get; set; }
        public List<Bookshelf> Bookshelves { get; set; }

        public TheLibrary(string libraryName)
        {
            LibraryName = libraryName;
            Bookshelves = new List<Bookshelf>();
        }

        public void AddBookshelf(Bookshelf shelf)
        {
            Bookshelves.Add(shelf);
        }

        public void DisplayLibraryInfo()
        {
            Console.WriteLine($"Welcome to {LibraryName} Library!");
            Console.WriteLine($"We have {Bookshelves.Count} bookshelves.");
        }
    }
}
