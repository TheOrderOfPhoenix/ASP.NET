using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DILastSession.LifeCycle
{
    public class C
    {
        #region [ - Ctor - ]

        #region [ - C(Framework.Interface.IService ref_IService) - ]
        // Eager Aggregation
        public C(Framework.Interface.IService ref_IService)
        {
            Ref_IService = ref_IService;
        }
        #endregion

        #region [ - C() - ]
        public C()
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
