using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _10_Interlocked
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread[] threads = new Thread[5];
            Counter c = new Counter();
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() =>
                {
                    for (int j = 0; j < 1000000; j++)
                    {
                        //Counter.Field++;  
                        Interlocked.Increment(ref c.Field);
                        // ++:
                        // 1) Читай і ,, блок 2
                        // 2) передавання керування іншому потоку №2
                        // 2) збільш на 1 // блок 1
                        // 3) запиши ,, блок 3  2
                    }
                });
                threads[i].Start();
            }

            for (int i = 0; i < 5; i++)
            {
                threads[i].Join();
            }

            Console.WriteLine("Counter: " + c.Field); //? 
        }
    }

    class Counter
    {
        public int Field;//{ get; set; }
    }

}
