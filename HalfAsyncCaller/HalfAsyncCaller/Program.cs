using System;
using System.Threading;

namespace HalfAsyncCaller
{
    class Program
    {
        static int _myEventHandlerSleepTime;
        static void Main(string[] args)
        {
            var meh = new MyEventHandler();
            _myEventHandlerSleepTime = 3000;
            meh.myEventHandler += ((s,e) => 
            {
                Console.WriteLine($"myEventHandler will sleep {_myEventHandlerSleepTime} ms");
                Thread.Sleep(_myEventHandlerSleepTime);
            });
            var res =  meh.MyEventHandlerMethod(5000);
            Console.WriteLine($"myEventHandler completed {res}");
            
            _myEventHandlerSleepTime=6000;
            res = meh.MyEventHandlerMethod(5000);
            Console.WriteLine($"myEventHandler completed {res}");
            Console.ReadKey();
        }
    }
}
