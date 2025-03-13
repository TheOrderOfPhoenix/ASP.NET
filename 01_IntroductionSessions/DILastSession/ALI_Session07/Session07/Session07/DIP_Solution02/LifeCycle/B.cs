using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIP_Solution02.LifeCycle
{
    public class B
    {
        #region [ - Ctor - ]

        #region [ - B(ExternalService.Service01 ref_Service01 ExternalService.Service02 ref_Service02 ExternalService.Service03 ref_Service03) - ]

        ////Eager Aggregation

        //public B(ExternalService.Service01 ref_Service01,
        //    ExternalService.Service02 ref_Service02,
        //    ExternalService.Service03 ref_Service03)
        //{
        //    Ref_Service01 = ref_Service01;
        //    Ref_Service02 = ref_Service02;
        //    Ref_Service03 = ref_Service03;
        //}

        #endregion

        #region [ - B() - ]

        public B()
        {

        }

        #endregion

        #endregion

        #region [ - Props - ]
        public ExternalService.Service01 Ref_Service01 { get; set; }
        public ExternalService.Service02 Ref_Service02 { get; set; }
        public ExternalService.Service03 Ref_Service03 { get; set; }
        #endregion

        #region [ - Methods - ]

        //Lazy Aggregation:

        #region [ - InitializeService01Refrence(ExternalService.Service01 ref_Service01) - ]

        public void InitializeService01Refrence(ExternalService.Service01 ref_Service01)
        {
            Ref_Service01 = ref_Service01;
        }
        #endregion

        #region [ - InitializeService02Refrence(ExternalService.Service02 ref_Service02) - ]
        public void InitializeService02Refrence(ExternalService.Service02 ref_Service02)
        {
            Ref_Service02 = ref_Service02;
        }
        #endregion

        #region [ - InitializeService03Refrence(ExternalService.Service03 ref_Service03) - ]
        public void InitializeService03Refrence(ExternalService.Service03 ref_Service03)
        {
            Ref_Service03 = ref_Service03;
        }
        #endregion

        #endregion
    }
}
