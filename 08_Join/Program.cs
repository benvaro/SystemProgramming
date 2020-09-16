using System;
using System.Threading;

namespace _08_Join
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Work in main"); // 1
            Random rand = new Random();
            Thread[] threads = new Thread[5];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(ShowInfo);
            }
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Start(rand.Next(500, 3000));
                threads[i].Join();
            }
            //  Thread.Sleep(1000);
            Console.WriteLine("Main ended"); //2
        }

        private static void ShowInfo(object state)
        {
            Thread.Sleep((int)state);
            Console.WriteLine("Work in thread {0}", Thread.CurrentThread.ManagedThreadId); // 3
        }
    }
}
