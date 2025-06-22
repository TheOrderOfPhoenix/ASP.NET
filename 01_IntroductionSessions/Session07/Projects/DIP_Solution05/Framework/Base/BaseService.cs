using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIP_Solution05.Framework.Base
{
    public class BaseService : Interface.IService
    {
        #region [ - Ctor - ]
        public BaseService()
        {

        }
        #endregion

        #region [ - Methods - ]

        #region [ - M1() - ]
        public virtual void M1()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region [ - M2() - ]
        public virtual void M2()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region [ - M3() - ]
        public virtual void M3()
        {
            throw new NotImplementedException();
        }
        #endregion

        #endregion
    }
}
