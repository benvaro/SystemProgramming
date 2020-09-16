using System;
using System.Diagnostics;
using System.Threading;

namespace _06_threadParametrized
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread t1 = new Thread(new ParameterizedThreadStart(ShowInfo));
            Thread t2 = new Thread(ShowInfo);
            t1.Start("Thread 1");
            t1.Priority = ThreadPriority.Normal;
            t2.Priority = ThreadPriority.Highest;
            // t1.IsBackground = true;
            t2.Start("\t\tThread 2");
            // t2.IsBackground = true;
            Console.ReadLine();
        }

        private static void ShowInfo(object obj)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Console.WriteLine("Thread id: #{0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(Thread.CurrentThread.IsBackground ? "Background thread" : "Primary thread");
            for (int i = 0; i < 5000; i++)
            {
                Console.WriteLine("{0} ---- {1}", obj.ToString(), i);
            }
            watch.Stop();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Time for thread #{0}: {1} ms", Thread.CurrentThread.GetHashCode(), watch.ElapsedMilliseconds);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
