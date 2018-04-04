using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxDotNetDemoProject
{
    class MyDisposable : IDisposable
    {
        public void Dispose()
        {
            var callerName = new StackTrace().GetFrame(1)?.GetMethod()?.Name;
            Console.WriteLine($"Dispose called by: {callerName}.");
        }
    }
}
