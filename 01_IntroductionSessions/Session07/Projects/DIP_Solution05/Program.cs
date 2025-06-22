using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIP_Solution05
{
    class Program
    {
        static void Main(string[] args)
        {
            #region [ - Eager Aggregation - ]

            Console.WriteLine("| Eager Aggregation :");

            #region [ - A - ]
            Console.WriteLine("A:");

            LifeCycle.A Ref_A_1 = new LifeCycle.A(new ExternalService.Service01()); // LSP
            Ref_A_1.Ref_IService.M1();

            LifeCycle.A Ref_A_2 = new LifeCycle.A(new ExternalService.Service02());
            Ref_A_2.Ref_IService.M2();

            LifeCycle.A Ref_A_3 = new LifeCycle.A(new ExternalService.Service03());
            Ref_A_3.Ref_IService.M3();
            #endregion

            Console.WriteLine("--------------");

            #region [ - B - ]
            Console.WriteLine("B:");

            LifeCycle.B Ref_B_1 = new LifeCycle.B(new ExternalService.Service01());
            Ref_B_1.Ref_IService.M1();

            LifeCycle.B Ref_B_2 = new LifeCycle.B(new ExternalService.Service02());
            Ref_B_2.Ref_IService.M2();

            LifeCycle.B Ref_B_3 = new LifeCycle.B(new ExternalService.Service03());
            Ref_B_3.Ref_IService.M3();
            #endregion

            Console.WriteLine("--------------");

            #region [ - C - ]
            Console.WriteLine("C:");

            LifeCycle.C Ref_C_1 = new LifeCycle.C(new ExternalService.Service01());
            Ref_C_1.Ref_IService.M1();

            LifeCycle.C Ref_C_2 = new LifeCycle.C(new ExternalService.Service02());
            Ref_C_2.Ref_IService.M2();

            LifeCycle.C Ref_C_3 = new LifeCycle.C(new ExternalService.Service03());
            Ref_C_3.Ref_IService.M3();
            #endregion

            #endregion

            Console.WriteLine("----------------------------");

            #region [ - Lazy Aggregation - ]

            Console.WriteLine("| Lazy Aggregation :");

            #region [ - A - ]
            Console.WriteLine("A:");

            LifeCycle.A Ref_A_4 = new LifeCycle.A();
            Ref_A_4.InitializeIServiceRefrence(new ExternalService.Service01());
            Ref_A_4.Ref_IService.M1();

            LifeCycle.A Ref_A_5 = new LifeCycle.A();
            Ref_A_5.InitializeIServiceRefrence(new ExternalService.Service02());
            Ref_A_5.Ref_IService.M2();

            LifeCycle.A Ref_A_6 = new LifeCycle.A();
            Ref_A_6.InitializeIServiceRefrence(new ExternalService.Service03());
            Ref_A_6.Ref_IService.M3();
            #endregion

            Console.WriteLine("--------------");

            #region [ - B - ]
            Console.WriteLine("B:");

            LifeCycle.B Ref_B_4 = new LifeCycle.B();
            Ref_B_4.InitializeIServiceRefrence(new ExternalService.Service01());
            Ref_B_4.Ref_IService.M1();

            LifeCycle.B Ref_B_5 = new LifeCycle.B();
            Ref_B_5.InitializeIServiceRefrence(new ExternalService.Service02());
            Ref_B_5.Ref_IService.M2();

            LifeCycle.B Ref_B_6 = new LifeCycle.B();
            Ref_B_6.InitializeIServiceRefrence(new ExternalService.Service03());
            Ref_B_6.Ref_IService.M3();
            #endregion

            Console.WriteLine("--------------");

            #region [ - C - ]
            Console.WriteLine("C:");

            LifeCycle.C Ref_C_4 = new LifeCycle.C();
            Ref_C_4.InitializeIServiceRefrence(new ExternalService.Service01());
            Ref_C_4.Ref_IService.M1();

            LifeCycle.C Ref_C_5 = new LifeCycle.C();
            Ref_C_5.InitializeIServiceRefrence(new ExternalService.Service02());
            Ref_C_5.Ref_IService.M2();

            LifeCycle.C Ref_C_6 = new LifeCycle.C();
            Ref_C_6.InitializeIServiceRefrence(new ExternalService.Service03());
            Ref_C_6.Ref_IService.M3();
            #endregion

            #endregion
            
            Console.ReadLine();
        }
    }
}
