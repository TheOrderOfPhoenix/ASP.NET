using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIP_Solution05.ExternalService
{
    public class Service01: Framework.Base.BaseService
    {
        #region [ - Ctor - ]
        public Service01()
        {

        }
        #endregion

        #region [ - Method - ]

        #region [ - M1() - ]
        public override void M1()
        {
            Console.WriteLine("M1 is running ...");
        }
        #endregion

        #endregion
    }
}
