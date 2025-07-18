using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIP_Solution04.Framework.Interface
{
    interface IService
    {
        #region [ - Props - ]
        ExternalService.Service01 Ref_Service01{ get; set; }
        ExternalService.Service02 Ref_Service02{ get; set; }
        ExternalService.Service03 Ref_Service03{ get; set; }
        #endregion

        #region [ - Methods - ]

        #region [ - GenerateService01(ExternalService.Service01 ref_Service01) - ]
        void GenerateService01(ExternalService.Service01 ref_Service01);
        #endregion

        #region [ - GenerateService02(ExternalService.Service02 ref_Service02) - ]
        void GenerateService02(ExternalService.Service02 ref_Service02);
        #endregion

        #region [ - GenerateService03(ExternalService.Service03 ref_Service03) - ]
        void GenerateService03(ExternalService.Service03 ref_Service03);
        #endregion

        #endregion
    }
}
