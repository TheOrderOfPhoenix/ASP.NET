using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        protected int a;
        public virtual string Introduce()
        {
            return "Hi, I'm " + Name;
        }
    }
}
