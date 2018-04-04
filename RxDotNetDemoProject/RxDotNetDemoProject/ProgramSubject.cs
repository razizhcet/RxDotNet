using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RxDotNetDemoProject
{
    class ProgramSubject
    {
        static void Main(string[] args)
        {
            Demo();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void Demo()
        {
            var subject = new Subject<string>();

            var o1 = new[] { "Hello", "World!" }.ToObservable();

            var s1 = subject.Subscribe(v => ProgramSubject.PrintToConsole("Sub 1", v));
            var s2 = subject.Subscribe(v => ProgramSubject.PrintToDebugWindow("Sub 2", v));
            var s3 = subject.Subscribe(v => ProgramSubject.PrintToConsoleSlowly("Sub 3", v));

            var s4 = o1.Subscribe(subject);

            Console.WriteLine("\nPress any key to dispose all subscriptions.");
            s1.Dispose();
            s2.Dispose();
            s3.Dispose();
            s4.Dispose();
        }

        static void PrintToConsole<T>(string subscriberName, T value)
        {
            Console.WriteLine($"{subscriberName}: {value.ToString()}");
        }

        static void PrintToConsoleSlowly<T>(string subscriberName, T value)
        {
            Thread.Sleep(500);
            Console.WriteLine($"{subscriberName}: {value.ToString()} slowly...");
        }

        static void PrintToDebugWindow<T>(string subscriberName, T value)
        {
            Debug.Print(string.Format($"{subscriberName}: {value.ToString()}"));
        }

        //var subscription = subject.Subscribe(Console.WriteLine);

        //subject.OnNext(1);
        //subject.OnNext(2);

        //subscription.Dispose();

        //subject.OnNext(3);


    }
}
