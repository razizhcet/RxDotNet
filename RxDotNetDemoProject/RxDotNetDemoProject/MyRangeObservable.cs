using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RxDotNetDemoProject
{
    class MyRangeObservable : IObservable<int>
    {
        private int _start;
        private int _count;
        private int _current;

        public MyRangeObservable(int start, int count)
        {
            _start = start;
            _count = count;
            _current = start;
        }

        public IDisposable Subscribe(IObserver<int> observer)
        {
            try
            {
                //observer.OnError(new Exception("Oops!"));
                Task.Run(() =>
                {
                    for (_current = _start; _current < _start + _count; _current++)
                    {
                        Thread.Sleep(1000);
                        observer.OnNext(_current);
                    }

                    observer.OnCompleted();
                });

                return new MyDisposable();
            }
            catch (Exception ex)
            {
                observer.OnError(ex);
                return new MyDisposable();
            }
        }
    }
}
