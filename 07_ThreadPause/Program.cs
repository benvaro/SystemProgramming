using System;
using System.Threading;

namespace _07_ThreadPause
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread = new Thread(ShowInfo);
            thread.Start();

            Console.WriteLine("Enter to pause");
            Console.ReadLine();
            thread.Suspend();

            Console.WriteLine("Enter to resume");
            Console.ReadLine();
            thread.Resume();

            Console.WriteLine("Enter to Abort");
            Console.ReadLine();
            thread.Abort();

            Console.ReadLine();
        }

        private static void ShowInfo()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("work in Thread #" + i);
                Thread.Sleep(200);
            }
        }
    }
}
