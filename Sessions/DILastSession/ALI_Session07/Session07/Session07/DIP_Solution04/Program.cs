using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIP_Solution04
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

            Ref_A.GenerateService01(new ExternalService.Service01());
            Ref_A.Ref_Service01.M1();

            Ref_A.GenerateService02(new ExternalService.Service02());
            Ref_A.Ref_Service02.M2();

            Ref_A.GenerateService03(new ExternalService.Service03());
            Ref_A.Ref_Service03.M3();

            #endregion

            Console.WriteLine("--------------");

            #region [ - B - ]

            Console.WriteLine("B:");

            Ref_B.GenerateService01(new ExternalService.Service01());
            Ref_B.Ref_Service01.M1();

            Ref_B.GenerateService02(new ExternalService.Service02());
            Ref_B.Ref_Service02.M2();

            Ref_B.GenerateService03(new ExternalService.Service03());
            Ref_B.Ref_Service03.M3();

            #endregion

            Console.WriteLine("--------------");

            #region [ - C - ]

            Console.WriteLine("C:");

            Ref_C.GenerateService01(new ExternalService.Service01());
            Ref_C.Ref_Service01.M1();

            Ref_C.GenerateService02(new ExternalService.Service02());
            Ref_C.Ref_Service02.M2();

            Ref_C.GenerateService03(new ExternalService.Service03());
            Ref_C.Ref_Service03.M3();

            #endregion

            #endregion



            System.Console.ReadLine();
        }
    }
}
