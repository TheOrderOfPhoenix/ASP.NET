using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIP_Solution01.LifeCycle
{
    public class A
    {
        #region [ - Ctor - ]
        public A(ExternalService.Service01 service01,
            ExternalService.Service02 service02, 
            ExternalService.Service03 service03)
        {
            //Eager Composition
            //Ref_Service01 = new ExternalService.Service01();
            //Ref_Service02 = new ExternalService.Service02();
            //Ref_Service03 = new ExternalService.Service03();

            //Eager Aggregation
            Ref_Service01 = service01;
            Ref_Service02 = service02;
            Ref_Service03 = service03;

        }
        #endregion

        #region [ - Props - ]
        public ExternalService.Service01 Ref_Service01 { get; set; }
        public ExternalService.Service02 Ref_Service02 { get; set; }
        public ExternalService.Service03 Ref_Service03 { get; set; }
        #endregion

        #region [ - Methods - ]

        ////Lazy Composition:

        //#region [ - InitializeService01Refrence() - ]

        //public void InitializeService01Refrence()
        //{
        //    Ref_Service01 = new ExternalService.Service01();
        //}
        //#endregion

        //#region [ - InitializeService02Refrence() - ]
        //public void InitializeService02Refrence()
        //{
        //    Ref_Service02 = new ExternalService.Service02();
        //}
        //#endregion

        //#region [ - InitializeService03Refrence() - ]
        //public void InitializeService03Refrence()
        //{
        //    Ref_Service03 = new ExternalService.Service03();
        //}
        //#endregion

        #endregion
    }
}
