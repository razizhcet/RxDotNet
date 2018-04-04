using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading;

namespace RxDotNetDemoProject
{
    class Program
    {
        static void Main(string[] args)
        {
          
            //step1: create an observable using one of the operators
            //on the System.Reactive.Linq.Observable class
            var observable = Observable.Range(5, 8);


            IObserver<int> observer = Observer.Create<int>(
                //step3: receive a value from the observable via the onNext handler
                Console.WriteLine,                                                  //onNext handler
                (error) => { Console.WriteLine($"Error: {error.Message}"); },       //onError handler
                () => { Console.WriteLine("Observation completed."); }              //onCompleted handler
                );

            //step2: subscribe to the observable, passing it the observer
            //This kickstart the observation
            var subscription = observable.Subscribe(observer);

            Console.ReadKey();

            //step4: dispose the subscription when obserfving done
            subscription.Dispose();
        }
    }
}
