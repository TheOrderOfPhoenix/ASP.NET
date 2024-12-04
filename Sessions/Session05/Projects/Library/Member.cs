using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    public class Member : Person
    {
        public LibraryCard Card { get; set; } // Association with LibraryCard
        public Address MemberAddress { get; set; } // Aggregation with Address
    }
}
