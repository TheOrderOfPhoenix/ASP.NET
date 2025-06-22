using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIP_Solution01
{
    class Program
    {
        static void Main(string[] args)
        {
            LifeCycle.A Ref_A = new LifeCycle.A(new ExternalService.Service01(),
                                                new ExternalService.Service02(),
                                                new ExternalService.Service03());

            LifeCycle.B Ref_B = new LifeCycle.B();

            LifeCycle.C Ref_C = new LifeCycle.C();


            // Active

            #region [ - Eager Composition - ]

            Console.WriteLine("A:");
            Ref_A.Ref_Service01.M1();
            Ref_A.Ref_Service02.M2();
            Ref_A.Ref_Service03.M3();

            Console.WriteLine("--------------");

            Console.WriteLine("B:");
            Ref_B.Ref_Service01.M1();
            Ref_B.Ref_Service02.M2();
            Ref_B.Ref_Service03.M3();

            Console.WriteLine("--------------");

            Console.WriteLine("C:");
            Ref_C.Ref_Service01.M1();
            Ref_C.Ref_Service02.M2();
            Ref_C.Ref_Service03.M3();

            #endregion


            // Disable

            #region [ - Lazy Composition - ]

            //#region [ - A - ]

            //Console.WriteLine("A:");

            //Ref_A.InitializeService01Refrence();
            //Ref_A.Ref_Service01.M1();

            //Ref_A.InitializeService02Refrence();
            //Ref_A.Ref_Service02.M2();

            //Ref_A.InitializeService03Refrence();
            //Ref_A.Ref_Service03.M3();

            //#endregion

            //#region [ - B - ]

            //Console.WriteLine("B:");

            //Ref_B.InitializeService01Refrence();
            //Ref_B.Ref_Service01.M1();

            //Ref_B.InitializeService02Refrence();
            //Ref_B.Ref_Service02.M2();

            //Ref_B.InitializeService03Refrence();
            //Ref_B.Ref_Service03.M3();

            //#endregion

            //#region [ - C - ]

            //Console.WriteLine("C:");

            //Ref_C.InitializeService01Refrence();
            //Ref_C.Ref_Service01.M1();

            //Ref_C.InitializeService02Refrence();
            //Ref_C.Ref_Service02.M2();

            //Ref_C.InitializeService03Refrence();
            //Ref_C.Ref_Service03.M3();

            //#endregion

            #endregion



            System.Console.ReadLine();
        }
    }
}
