using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    public class EmailSender : ISender
    {
        public void Send(string to)
        {
            Console.WriteLine("Sent Mail by Email");
        }
        public void Hi()
        {

        }
    }
}
