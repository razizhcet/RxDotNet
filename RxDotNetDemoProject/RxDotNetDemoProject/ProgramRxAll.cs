using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RxDotNetDemoProject
{
    class ProgramRxAll
    {
        static void Main(string[] args)
        {
            //DefaultError();
            DefaultForError();
            //DefaultForNext();
            //MustOnNext();
            //CallDisposeAutomatically();
            //CallDisposeNotAutomatically();
            //CallDisposeAutoForError()

        }
        static void DefaultError()
        {
            try
            {
                var observable = Observable.Create<int>(observer =>
                {
                    observer.OnNext(1);
                    observer.OnError(new Exception("Oops1 That's all we know"));

                    return Disposable.Create(() =>
                    {
                        Console.WriteLine("Dispose called");
                    });

                });
                var subscription = observable.Subscribe(ValueHandler, CompletionHandler);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
                Console.WriteLine($"But we do have the remote stack trace: {ex.StackTrace.ToString()}");
                Console.WriteLine($"But still, this is kind of shoddy, don't you think?");
                Console.WriteLine();
            }
        }

        static void DefaultForError()
        {
            try
            {
                var observable = Observable.Create<int>(observer =>
                {
                    observer.OnNext(1);
                    observer.OnError(new Exception("Oops1 That's all we know"));

                    return Disposable.Create(() =>
                    {
                        Console.WriteLine("Dispose called");
                    });

                });
                var subscription = observable.Subscribe(ValueHandler, ExceptionHandler, CompletionHandler);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
                Console.WriteLine($"But we do have the remote stack trace: {ex.StackTrace.ToString()}");
                Console.WriteLine($"But still, this is kind of shoddy, don't you think?");
                Console.WriteLine();
            }
        }

        static void DefaultForNext()
        {
            try
            {
                var observable = Observable.Create<int>(observer =>
                {
                    observer.OnNext(1);
                    observer.OnCompleted();

                    return Disposable.Create(() =>
                    {
                        Debug.Print("Dispose() called");
                        Console.WriteLine("Dispose() called");
                    });
                });
                // dn't provide:
                //a. an OnNext: default one ignore the value
                //b. never know about a value, never know sequence is completed
                var subscription = observable.Subscribe();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
                Console.WriteLine($"But we do have the remote stack trace: {ex.StackTrace.ToString()}");
                Console.WriteLine($"But still, this is kind of shoddy, don't you think?");
                Console.WriteLine();
            }
        }

        static void MustOnNext()
        {
            try
            {
                var observable = Observable.Create<int>(observer =>
                {
                    observer.OnNext(1);
                    observer.OnCompleted();

                    return Disposable.Create(() =>
                    {
                        Debug.Print("Dispose() called");
                        Console.WriteLine("Dispose() called");
                    });
                });
                // dn't provide:
                //a. an OnNext: default one ignore the value
                //b. never know about a value, never know sequence is completed
                var subscription = observable.Subscribe(onNext: null, onError: ExceptionHandler, onCompleted: CompletionHandler);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
                Console.WriteLine($"But we do have the remote stack trace: {ex.StackTrace.ToString()}");
                Console.WriteLine($"But still, this is kind of shoddy, don't you think?");
                Console.WriteLine();
            }
        }

        static void CallDisposeAutomatically()
        {
                var observable = Observable.Create<int>(observer =>
                {
                    observer.OnNext(1);
                    observer.OnCompleted();

                    return Disposable.Create(() =>
                    {
                        var stackFrame = new StackTrace();
                        var callingMethodBase = stackFrame.GetFrame(1)?.GetMethod();
                        var callingTypeName = callingMethodBase?.DeclaringType.FullName;
                        var callingMethodName = callingMethodBase?.Name;

                        var msg = string.Format($"Dispose called by {callingTypeName}.{callingMethodName}.");

                        Debug.Print(msg);
                        Console.WriteLine(msg);
                    });
                });
                //a. an OnNext: default one ignore the value
                //b. never know about a value, never know sequence is completed
                var subscription = observable.Subscribe(ValueHandler, ExceptionHandler, CompletionHandler);
        }

        static void CallDisposeNotAutomatically()
        {
            var observable = Observable.Create<int>(observer =>
            {
                observer.OnNext(1);

                return Disposable.Create(() =>
                {
                    var stackFrame = new StackTrace();
                    var callingMethodBase = stackFrame.GetFrame(1)?.GetMethod();
                    var callingTypeName = callingMethodBase?.DeclaringType.FullName;
                    var callingMethodName = callingMethodBase?.Name;

                    var msg = string.Format($"Dispose called by {callingTypeName}.{callingMethodName}.");

                    Debug.Print(msg);
                    Console.WriteLine(msg);
                });
            });
            //a. an OnNext: default one ignore the value
            //b. never know about a value, never know sequence is completed
            var subscription = observable.Subscribe(ValueHandler, ExceptionHandler, CompletionHandler);
        }

        static void CallDisposeAutoForError()
        {
            var observable = Observable.Create<int>(observer =>
            {
                observer.OnNext(1);
                observer.OnError(new Exception("Oops1 That's all we know"));

                return Disposable.Create(() =>
                {
                    var stackFrame = new StackTrace();
                    var callingMethodBase = stackFrame.GetFrame(1)?.GetMethod();
                    var callingTypeName = callingMethodBase?.DeclaringType.FullName;
                    var callingMethodName = callingMethodBase?.Name;

                    var msg = string.Format($"Dispose called by {callingTypeName}.{callingMethodName}.");

                    Debug.Print(msg);
                    Console.WriteLine(msg);
                });
            });
            //a. an OnNext: default one ignore the value
            //b. never know about a value, never know sequence is completed
            var subscription = observable.Subscribe(ValueHandler, ExceptionHandler, CompletionHandler);
        }

        static void ValueHandler(int value)
        {
            Console.WriteLine($"Next value: {value}");
        }

        static void ExceptionHandler(Exception ex)
        {
            Console.WriteLine($"An error occured: {ex.Message}");
        }

        static void CompletionHandler()
        {
            Console.WriteLine($"We are done.");
        }
    }
}
