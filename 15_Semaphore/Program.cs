using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _15_Semaphore
{
    class Program
    {
        static void Main(string[] args)
        {
            // Дозволяємо працювати тільки певній кількості потоків (3)
            Semaphore semaphore = new Semaphore(5, 5);

            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(SomeMethod, semaphore);
            }
            Console.ReadLine();
        }

        private static void SomeMethod(object state)
        {
            Semaphore s = state as Semaphore;
            bool flag = true;
            while(flag)
            {
                // Чекаємо 2 секунди, якщо в методі відбувся Реліз, 
                //то в семафорі з'являється місце, і ми пропускаємо ще один потік
                if(s.WaitOne(2000))
                {
                    try
                    {
                        // Даний код "зайнятий" 4 секунди
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Thread #{0} got block", Thread.CurrentThread.ManagedThreadId);
                        Console.ForegroundColor = ConsoleColor.White;
                        Thread.Sleep(4000);
                    }
                    finally
                    {
                        flag = false;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Thread #{0} зняв блок", Thread.CurrentThread.ManagedThreadId);
                        Console.ForegroundColor = ConsoleColor.White;
                        s.Release();
                    }
                }
                else
                    Console.WriteLine("Thread #{0} is busy. Timeout finished", Thread.CurrentThread.ManagedThreadId);
            }
        }
    }
}
