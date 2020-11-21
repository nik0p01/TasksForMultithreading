using System;
using System.Collections.Generic;
using System.Text;

namespace HalfAsyncCaller
{
     public class MyEventHandler
    {
        public event EventHandler myEventHandler;
        public bool MyEventHandlerMethod(int time)
        {
            EventHandler h = new EventHandler(this.myEventHandler);
            var ac = new AsyncCaller(h);
            bool completedOK = ac.Invoke(5000, null, EventArgs.Empty);
            return completedOK;
        }
    }
}
