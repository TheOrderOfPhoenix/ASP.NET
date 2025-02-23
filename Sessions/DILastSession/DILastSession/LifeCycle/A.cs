using DILastSession.ExternalService;
using DILastSession.Framework.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DILastSession.LifeCycle
{
    public class A
    {
        public A()
        {

        }

        public void Print(IService newService)
        {
            IService service = newService;
            service.M1();
        }
        
    }
}
