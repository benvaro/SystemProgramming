using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _12_SyncTest
{
    class Program
    {
        // Спільні ресурси в програмі
        class SharedResource
        {
            public static int count;
            // Mutex - об'єкт синхронізації рівня ядра, використовують
            // для синхронізації між процесами або в межах різних доменів додатків
            public static Mutex mutex = new Mutex();
        }
        // клас, який використовує наші спільні ресурси
        class IncResource
        {
            // Інкапсулюємо потік
            Thread thread = null;
            int flag;
            public IncResource(int max)
            {
                flag = max;
                // При створенні об'єкта класу запускаємо метод в іншому потоці
                thread = new Thread(Run);
                thread.Name = "Inc thread";
                thread.Start();
            }

            public void Run()
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                Console.WriteLine($"{Thread.CurrentThread.Name} got mutex");
                // блокуємо спільний ресурс. поки не звільнимо - інший потік не може захопити
                //lock (typeof(SharedResource)) // Monitor.Enter(typeof...)
                //{
                SharedResource.mutex.WaitOne();
                    while (flag > 0)
                    {
                        Console.WriteLine($"Work in {Thread.CurrentThread.Name} ----- result {SharedResource.count}");
                        SharedResource.count++;
                        flag--;
                        Thread.Sleep(50);
                    }
                //} // Monitor.Exit(...)
                SharedResource.mutex.ReleaseMutex();
                Console.WriteLine("{0} releaed Mutex", Thread.CurrentThread.Name);
                watch.Stop();
                Console.WriteLine("Ms inc: {0}", watch.Elapsed.TotalSeconds);
            }
        }
        class DecResource
        {
            Thread thread = null;
            int flag;
            public DecResource(int max)
            {
                flag = max;
                thread = new Thread(Run);
                thread.Name = "Dec thread";
                thread.Start();
            }

            public void Run()
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                Console.WriteLine($"{Thread.CurrentThread.Name} got mutex");
                // lock (typeof(SharedResource))
                // {
                SharedResource.mutex.WaitOne();
                while (flag > 0)
                    {
                        Console.WriteLine($"Work in {Thread.CurrentThread.Name} ----- result {SharedResource.count}");
                        SharedResource.count--;
                        flag--;
                        Thread.Sleep(50);
                    }
                // }
                SharedResource.mutex.ReleaseMutex();
                Console.WriteLine("{0} releaed Mutex", Thread.CurrentThread.Name);
                watch.Stop();
                Console.WriteLine("Ms dec: {0}", watch.Elapsed.TotalSeconds);
            }
        }
        static void Main(string[] args)
        {
            IncResource inc = new IncResource(6); // 0 1 2 3 4 5 
            DecResource dec = new DecResource(6); // 6 5 4 3 2 1

            Console.ReadLine();
        }
    }
}
