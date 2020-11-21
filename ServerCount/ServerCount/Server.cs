using System.Threading;

namespace ServerCount
{
    public static class Server
    {
        private static int count;
        private static readonly ReaderWriterLockSlim mLock = new ReaderWriterLockSlim();
        public static int GetCount()
        {
            mLock.EnterReadLock();
            int temp = count;
            mLock.ExitReadLock();
            return temp;
        }

        public static void AddToCount(int value)
        {
            mLock.EnterWriteLock();
            count = value;
            mLock.ExitWriteLock();
        }
    }
}
