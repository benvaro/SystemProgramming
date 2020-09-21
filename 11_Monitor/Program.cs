using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _11_Monitor
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread[] threads = new Thread[5];
            //Counter c = new Counter();
            //for (int i = 0; i < threads.Length; i++)
            //{
            //    threads[i] = new Thread(c.UpdateField);
            //    threads[i].Start();
            //}

            //for (int i = 0; i < 5; i++)
            //{
            //    threads[i].Join();
            //}

            //Console.WriteLine("Counter Field1: " + c.Field); //? 
            //Console.WriteLine("Counter Field2: " + c.Field2); //? 


            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(CounterStatic.UpdateField);
                threads[i].Start();
            }

            for (int i = 0; i < 5; i++)
            {
                threads[i].Join();
            }

            //  Thread.CurrentThread.Join();// deadlock

            Console.WriteLine("counter field1: " + CounterStatic.Field); //? 
            Console.WriteLine("counter field2: " + CounterStatic.Field2); //? 
        }

        class Counter
        {
            private int field;
            private int field2;

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
                Monitor.Enter(this);
                try
                {
                    for (int i = 0; i < 1000000; i++)
                    {
                        #region Interlocked sync
                        //Field++;
                        //Interlocked.Increment(ref field);
                        //if (Field % 2 == 0)
                        //    //Field2++;
                        //    Interlocked.Increment(ref field2); 
                        #endregion

                        Field++;
                        if (Field % 2 == 0)
                            Field2++;
                    }
                }
                finally
                {
                    Monitor.Exit(this);
                }
            }
        }
        class CounterStatic
        {
            private static int field;
            private static int field2;
            private static object obj = new object();

            public static int Field
            {
                get => field;
                set => field = value;
            }
            public static int Field2
            {
                get => field2;
                set => field2 = value;
            }

            public static void UpdateField()
            {
                //  Monitor.Enter(typeof(CounterStatic));
                //   Monitor.Enter(obj);
                lock (obj)
                {
                    //  try
                    //  {
                    for (int i = 0; i < 1000000; i++)
                    {
                        #region Interlocked sync
                        //Field++;
                        //Interlocked.Increment(ref field);
                        //if (Field % 2 == 0)
                        //    //Field2++;
                        //    Interlocked.Increment(ref field2); 
                        #endregion

                        Field++;
                        if (Field % 2 == 0)
                            Field2++;
                    }
                    //  }
                }
                //finally
                //{
                //  //  Monitor.Exit(typeof(CounterStatic));
                //    Monitor.Exit(obj);
                //}
            }
        }
    }
}
