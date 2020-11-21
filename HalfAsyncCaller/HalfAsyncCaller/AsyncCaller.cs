using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;

namespace HalfAsyncCaller
{
    class AsyncCaller
    {
        private EventHandler _h;
        private bool _invokeComplete = false;

        public AsyncCaller(EventHandler h)
        {
            _h = h;
        }

        internal bool Invoke(int time, object nullObject, EventArgs emptyEventArgs)
        {
            return Invoke(time);
        }
        internal bool Invoke(int time)
        {
            Thread threadInvoke = new Thread(new ThreadStart(EventHandlerThreadInvoke));
            threadInvoke.Start();
            Thread.Sleep(time);
            threadInvoke.Abort(); //This method is obsolete. On .NET 5.0 and later versions, calling this method produces a compile-time warning. This method throws a PlatformNotSupportedException at run time on .NET 5.0 and later and .NET Core.
            return _invokeComplete;
        }

        private void EventHandlerThreadInvoke()
        {
            _h.Invoke(null, EventArgs.Empty);
            _invokeComplete = true;
        }

     
    }
}
