using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RxDotNetDemoProject
{
    class PrgmNonBlockTimer
    {
        static void Main(string[] args)
        {
            Demo();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void Demo()
        {
            var observable = Observable.Create<long>(observer =>
            {
                try
                {
                    var innerObservable = Observable
                    .Timer(
                        TimeSpan.FromMilliseconds(0),
                        TimeSpan.FromMilliseconds(500))
                        .Skip(1)    //to skip the value 0 and start from 1 instead
                        .Take(3);   //to take the first three value

                    return innerObservable.Subscribe(observer);
                }
                catch (Exception ex)
                {
                    observer.OnError(ex);

                    return Disposable.Empty;
                }

            });

            var subscription = observable.Subscribe(
                Console.WriteLine,
                ex => Console.WriteLine(ex.Message),
                () => Console.WriteLine("Completed")
                );

            Console.WriteLine("Press any key to Dispose the subscription");
            Console.ReadKey();
            subscription.Dispose();
        }
    }
}
