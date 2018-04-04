using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxDotNetDemoProject
{
    class PrgmObservableCreate
    {
        static void Main(string[] args)
        {
            Demo();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void Demo()
        {
            IObservable<int> observable = Observable.Create<int>(observer =>
            {
                try
                {
                    observer.OnNext(1);
                    observer.OnNext(2);
                    observer.OnNext(3);
                    //throw new Exception("Oops!");
                    observer.OnCompleted();
                }
                catch(Exception ex)
                {
                    observer.OnError(ex);
                }

                return Disposable.Empty;
            });

            var subscription = observable.Subscribe(
                Console.WriteLine,
                ex => Console.WriteLine(ex.Message),
                () => Console.WriteLine("Completed")
                );

            Console.WriteLine("Disposing the subscription");
            subscription.Dispose();
        }
    }
}
