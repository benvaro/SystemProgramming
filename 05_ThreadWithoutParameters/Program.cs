using System;
using System.Threading;

namespace _05_ThreadWithoutParameters
{
    class Program
    {
        static void Show()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("Thread: #{0}  - {1}", Thread.CurrentThread.ManagedThreadId, i);
                //Thread.Sleep(100);
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Thread: #{0}", Thread.CurrentThread.ManagedThreadId);

            Thread t1 = new Thread(new ThreadStart(Show));
            Thread t2 = new Thread(new ThreadStart(Show));
            t1.Start();
            t2.Start();

            Console.WriteLine("Main works....");
            Console.WriteLine("Main ended....");
        }
    }
}
