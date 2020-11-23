using System;
using System.Threading;

namespace HalfAsyncCaller
{
    class AsyncCaller:IDisposable
    {
        private EventHandler _h;
        private bool _invokeComplete = false;
        private IAsyncResult _asyncResult;
        public AsyncCaller(EventHandler h)
        {
            _h = h;
        }

        public void Dispose()
        {
            _h.EndInvoke(_asyncResult);
            _asyncResult.AsyncWaitHandle.Close();
        }

        internal bool Invoke(int time, object nullObject, EventArgs emptyEventArgs)
        {
            return Invoke(time);
        }

        /// <summary>
        /// Этот метод ждет заданный тамаут, затем передает упрвление вызывающему потоку
        /// </summary>
        /// <param name="time">Таймаут, мс></param>
        /// <returns></returns>
        internal bool Invoke(int time)
        {
            _asyncResult = _h.BeginInvoke(null, null, null, null);
            _asyncResult.AsyncWaitHandle.WaitOne(time);
            return _asyncResult.IsCompleted;
        }

        /// <summary>
        /// Этот метод закроет поток с делегатом после тамаута
        /// </summary>
        /// <param name="time">Таймаут, мс</param>
        /// <returns></returns>
        internal bool InvokeForTime(int time)
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
