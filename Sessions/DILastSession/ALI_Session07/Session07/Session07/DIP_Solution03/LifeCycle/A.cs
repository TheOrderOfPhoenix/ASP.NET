using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIP_Solution03.LifeCycle
{
    public class A: Service
    {
        #region [ - Ctor - ]
        public A(): base(new ExternalService.Service01(), new ExternalService.Service02(), new ExternalService.Service03())
        {

        }
        #endregion
    }
}
