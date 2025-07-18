using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIP_Solution05.ExternalService
{
    public class Service02: Framework.Base.BaseService
    {
        #region [ - Ctor - ]
        public Service02()
        {

        }
        #endregion

        #region [ - Method - ]

        #region [ - M2() - ]
        public override void M2()
        {
            Console.WriteLine("M2 is running ...");
        }
        #endregion

        #endregion
    }
}
