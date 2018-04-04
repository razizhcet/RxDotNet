using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RxDotNetDemoProject
{
    class MySlowObserver<T> : MyObserver<T>
    {
        public MySlowObserver(string subscriberName="Subscriber 1")
            : base(subscriberName)
        {

        }

        public override void OnNext(T value)
        {
            Thread.Sleep(1000);

            base.OnNext(value);
        }
    }
}
