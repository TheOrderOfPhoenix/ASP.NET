using DIP_Solution04.ExternalService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIP_Solution04.Framework.Base
{
    public class BaseService : Interface.IService
    {
        #region [ - Ctor - ]

        #region [ - BaseService(ExternalService.Service01 ref_Service01, ExternalService.Service02 ref_Service02, ExternalService.Service03 ref_Service03) - ]
        //Eager Aggregation
        public BaseService(Service01 ref_Service01,
            Service02 ref_Service02,
            Service03 ref_Service03)
        {
            Ref_Service01 = ref_Service01;
            Ref_Service02 = ref_Service02;
            Ref_Service03 = ref_Service03;
        }
        #endregion

        #region [ - BaseService() - ]
        public BaseService()
        {

        } 
        #endregion

        #endregion

        #region [ - Props - ]
        public Service01 Ref_Service01 { get; set; }
        public Service02 Ref_Service02 { get; set; }
        public Service03 Ref_Service03 { get; set; }
        #endregion

        #region [ - Methods - ]

        // Implement Lazy Aggregation

        #region [ - GenerateService01(Service01 ref_Service01) - ]
        public void GenerateService01(Service01 ref_Service01)
        {
            Ref_Service01 = ref_Service01;
        }
        #endregion

        #region [ - GenerateService02(Service02 ref_Service02) - ]
        public void GenerateService02(Service02 ref_Service02)
        {
            Ref_Service02 = ref_Service02;
        }
        #endregion

        #region [ - GenerateService03(Service03 ref_Service03) - ]
        public void GenerateService03(Service03 ref_Service03)
        {
            Ref_Service03 = ref_Service03;
        }
        #endregion 

        #endregion
    }
}
