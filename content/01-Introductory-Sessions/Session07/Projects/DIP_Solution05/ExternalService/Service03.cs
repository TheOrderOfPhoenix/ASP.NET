using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIP_Solution05.ExternalService
{
    public class Service03 : Framework.Base.BaseService
    {
        #region [ - Ctor - ]
        public Service03()
        {

        }
        #endregion

        #region [ - Method - ]

        #region [ - M3() - ]
        public override void M3()
        {
            Console.WriteLine("M3 is running ...");
        } 
        #endregion

        #endregion
    }
}
