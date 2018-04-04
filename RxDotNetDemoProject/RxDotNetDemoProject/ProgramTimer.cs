using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxDotNetDemoProject
{
    class ProgramTimer
    {
        static void Main(string[] args)
        {
            //TimerDemo();
            GetEnumPeriodically();
        }

        static void TimerDemo()
        {
            var period = TimeSpan.FromMilliseconds(500);
            var observable = Observable.Timer(period, period)
                .Skip(1)
                .Take(3);
            
            var subscription = observable.Subscribe(
                Console.WriteLine,
                ex => Console.WriteLine(ex.Message),
                () => Console.WriteLine("Observation Completed")
                );

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            subscription.Dispose();
        }

        static void GetEnumPeriodically()
        {
            var period = TimeSpan.FromMilliseconds(500);
            var enumerable = new List<string> { "One", "Two", "Three" };

            var observable = Observable.Interval(period)
                .Zip(enumerable.ToObservable(),
                (n, s) => s);

            var subscription = observable.Subscribe(
                Console.WriteLine,
                ex => Console.WriteLine(ex.Message),
                () => Console.WriteLine("Observation Completed")
                );

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            subscription.Dispose();
        }
    }
}
