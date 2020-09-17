using System;
using System.Threading;

namespace _04_ThreadTimer
{
    class Program
    {
        static void ShowInfo(object state)
        {
            Console.WriteLine("Thread #{0}", Thread.CurrentThread.ManagedThreadId); // 3
            Console.WriteLine("Thread isBackground? {0}", Thread.CurrentThread.IsBackground); // 3
                                                                                              // Thread.Sleep(2000); // 
        }
        static void Main(string[] args)
        {
            // Багатопоточність - властивість ОС або додатку, що полягає в тому щоб один процес може складатись із багатьох потоків

            Console.WriteLine("Main thread #{0}", Thread.CurrentThread.ManagedThreadId); // 1

            TimerCallback timerCallback = new TimerCallback(ShowInfo);
            // #1
            Timer timer = new Timer(timerCallback);
            timer.Change(1000, 1000);

            //  Timer timer = new Timer(/*new TimerCallback(ShowInfo)*/PrintTime, null, 1000, 1000);

            //  Thread.Sleep(2000);  // 2
            // ShowInfo();
            Console.WriteLine("Main ended"); // 4
            Console.ReadLine();
        }

        private static void PrintTime(object state)
        {
            Console.Clear();
            Console.WriteLine(DateTime.Now.ToLongTimeString());
        }
    }
}
