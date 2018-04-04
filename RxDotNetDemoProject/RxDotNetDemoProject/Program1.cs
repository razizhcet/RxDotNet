using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace RxDotNetDemoProject
{
    class Program1
    {
        static void Main(string[] args)
        {
            //ObserverContractViolation2();
            //ObserverContractViolation1();
            Demo();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void Demo()
        {
            var observable = new MyRangeObservable(5, 8);

            var observer = new MyObserver<int>();

            //var observer = Observer.Create<int>(
            //    Console.WriteLine,
            //    ex => Console.WriteLine(ex.Message),
            //    () => Console.WriteLine("Completed")
            //    );

            var subscription1 = observable.Subscribe(new MySlowObserver<int>("Subscriber 1"));
            subscription1.Dispose();

            //var subscription2 = observable.Subscribe(new MyObserver<int>("Subscriber 2"));
            //subscription2.Dispose();

            //Console.WriteLine("Press any key to dispose all subscriptions.");
            Console.WriteLine("Subscription has been disposed but we'r still rceiving value");
            //Console.ReadKey();

        }

        static void ObserverContractViolation1()
        {
            var observable = new MyRangeObservable(5, 8);

            //var observer = new MyObserver<int>();

            var subscription = observable.Subscribe(
                Console.WriteLine,
                ex => Console.WriteLine(ex.Message),
                () => Console.WriteLine("Completed")
                );

            Console.WriteLine("Press any key to dispose the subscription.");
            Console.ReadKey();

            subscription.Dispose();
        }
    }
}
