namespace DILastSession
{

internal class Program
{
        private static void Main(string[] args)
        {

            Console.WriteLine("| Eager Aggregation :");

            Console.WriteLine("A:");
            LifeCycle.A a = new LifeCycle.A();

            a.Print(new ExternalService.Service01());
            a.Print(new ExternalService.Service02());
            



            //LifeCycle.A Ref_A_1 = new LifeCycle.A(); 
            //Ref_A_1.Ref_IService.M1();

            //LifeCycle.A Ref_A_2 = new LifeCycle.A();
            //Ref_A_2.Ref_IService.M2();

            //LifeCycle.A Ref_A_3 = new LifeCycle.A();
            //Ref_A_3.Ref_IService.M3();

            Console.WriteLine("--------------");

        }
    }
}