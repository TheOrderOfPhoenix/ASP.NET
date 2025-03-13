using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    public class Bookshelf
    {
        public int ShelfNumber { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();

        public void AddBook(Book book)
        {
            Books.Add(book);
        }
    }
}
