using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public virtual string Introduce()
        {
            return "Hi, I am " + Name;
        }
    }
}
