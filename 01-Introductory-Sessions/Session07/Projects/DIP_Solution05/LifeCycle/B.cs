using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIP_Solution05.LifeCycle
{
    public class B
    {
        #region [ - Ctor - ]

        #region [ - B(Framework.Interface.IService ref_IService) - ]
        // Eager Aggregation
        public B(Framework.Interface.IService ref_IService)
        {
            Ref_IService = ref_IService;
        }
        #endregion

        #region [ - A() - ]
        public B()
        {

        }
        #endregion

        #endregion

        #region [ - Prop - ]
        public Framework.Interface.IService Ref_IService { get; set; }
        #endregion

        #region [ - Method - ]

        #region [ - InitializeIServiceRefrence(Framework.Interface.IService ref_IService) - ]
        // Lazy Aggregation
        public void InitializeIServiceRefrence(Framework.Interface.IService ref_IService)
        {
            Ref_IService = ref_IService;
        }
        #endregion

        #endregion
    }
}
