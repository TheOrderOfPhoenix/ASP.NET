using DILastSession.Framework.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DILastSession.ExternalService
{
    public class Service01 : IService
    {
        public Service01()
        {

        }
        public void M1()
        {
            Console.WriteLine("M1 is running in Server 1...");
        }
        public void M2()
        {
            Console.WriteLine("M2 is running in Server 1...");
        }
        public void M3()
        {
            Console.WriteLine("M3 is running in Server 1...");
        }

    }
}
