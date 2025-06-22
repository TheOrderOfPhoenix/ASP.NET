using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    public class User : Person
    {

        public void SendMessageToSomeone(string to, ISender sender)
        {
            sender.Send(to);
        }

        public override string Introduce()
        {
            
            return "Hi, I'm " + Name + ". I am a user";
        }

    }
}
