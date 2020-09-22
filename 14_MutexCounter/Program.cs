using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _14_MutexCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread[] threads = new Thread[5];
            Counter c = new Counter();
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(c.UpdateField);
                threads[i].Start();
            }

            for (int i = 0; i < 5; i++)
            {
                threads[i].Join();
            }

            Console.WriteLine("Counter Field1: " + c.Field); //? 
            Console.WriteLine("Counter Field2: " + c.Field2); //? 
        }

        class Counter
        {
            private int field;
            private int field2;
            private Mutex mutex = new Mutex();

            public int Field
            {
                get => field;
                set => field = value;
            }
            public int Field2
            {
                get => field2;
                set => field2 = value;
            }

            public void UpdateField()
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();

                for (int i = 0; i < 1000000; i++)
                {
                    mutex.WaitOne();
                    try
                    {
                        Field++;
                        if (Field % 2 == 0)
                            Field2++;
                    }
                    finally
                    {
                        mutex.ReleaseMutex();
                    }
                }
                watch.Stop();
                Console.WriteLine("Ms dec: {0}", watch.Elapsed.TotalSeconds);
            }

        }

    }

}
