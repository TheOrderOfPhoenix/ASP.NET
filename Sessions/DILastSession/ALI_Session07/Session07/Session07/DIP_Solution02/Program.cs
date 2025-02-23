using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIP_Solution02
{
    class Program
    {
        static void Main(string[] args)
        {
            LifeCycle.A Ref_A = new LifeCycle.A();

            LifeCycle.B Ref_B = new LifeCycle.B();

            LifeCycle.C Ref_C = new LifeCycle.C();


            // Disable

            #region [ - Eager Aggregation - ]

            //Console.WriteLine("A:");
            //Ref_A.Ref_Service01.M1();
            //Ref_A.Ref_Service02.M2();
            //Ref_A.Ref_Service03.M3();

            //Console.WriteLine("--------------");

            //Console.WriteLine("B:");
            //Ref_B.Ref_Service01.M1();
            //Ref_B.Ref_Service02.M2();
            //Ref_B.Ref_Service03.M3();

            //Console.WriteLine("--------------");

            //Console.WriteLine("C:");
            //Ref_C.Ref_Service01.M1();
            //Ref_C.Ref_Service02.M2();
            //Ref_C.Ref_Service03.M3();

            #endregion


            // Active

            #region [ - Lazy Aggregation - ]

            #region [ - A - ]

            Console.WriteLine("A:");

            Ref_A.InitializeService01Refrence(new ExternalService.Service01());
            Ref_A.Ref_Service01.M1();

            Ref_A.InitializeService02Refrence(new ExternalService.Service02());
            Ref_A.Ref_Service02.M2();

            Ref_A.InitializeService03Refrence(new ExternalService.Service03());
            Ref_A.Ref_Service03.M3();

            #endregion

            #region [ - B - ]

            Console.WriteLine("B:");

            Ref_B.InitializeService01Refrence(new ExternalService.Service01());
            Ref_B.Ref_Service01.M1();

            Ref_B.InitializeService02Refrence(new ExternalService.Service02());
            Ref_B.Ref_Service02.M2();

            Ref_B.InitializeService03Refrence(new ExternalService.Service03());
            Ref_B.Ref_Service03.M3();

            #endregion

            #region [ - C - ]

            Console.WriteLine("C:");

            Ref_C.InitializeService01Refrence(new ExternalService.Service01());
            Ref_C.Ref_Service01.M1();

            Ref_C.InitializeService02Refrence(new ExternalService.Service02());
            Ref_C.Ref_Service02.M2();

            Ref_C.InitializeService03Refrence(new ExternalService.Service03());
            Ref_C.Ref_Service03.M3();

            #endregion

            #endregion



            System.Console.ReadLine();
        }
    }
}
