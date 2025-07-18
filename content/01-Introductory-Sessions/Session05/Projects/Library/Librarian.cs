using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    public class Librarian : Person
    {
        public void IssueBook(Member member, Book book)
        {
            Console.WriteLine($"{Name} (Librarian) issued '{book.Title}' to {member.Name}.");
        }
    
        public override string Introduce()
        {
            return "Hi, I am " + Name + ", a librarian";
        }

    }
}
