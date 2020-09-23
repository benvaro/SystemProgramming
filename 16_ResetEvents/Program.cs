using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _16_ResetEvents
{
    class MyThread
    {
        Thread thread = null;
        public static int Count { get; set; }
        //  AutoResetEvent resetEvent = new AutoResetEvent(false);
        ManualResetEvent resetEvent = new ManualResetEvent(false);
        //public MyThread(AutoResetEvent ev)
        //{
        //    thread = new Thread(Run);
        //    resetEvent = ev;
        //    thread.Start();
        //}

        public MyThread(ManualResetEvent ev)
        {
            thread = new Thread(Run);
            resetEvent = ev;
            thread.Start();
        }
        private void Run()
        {
            Console.WriteLine("Thread #{0}", Thread.CurrentThread.ManagedThreadId); // 2 6
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("in thread: {0} --- {1}", Thread.CurrentThread.ManagedThreadId, Count++); // 3 7
            }
            Console.WriteLine("Run in #{0} finished work", Thread.CurrentThread.ManagedThreadId); // 4 8
            resetEvent.Set();
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            // події автоматичного скидання стану. Автоматично стають несигнальними,
            // коли потік викликав Set()
            //    AutoResetEvent resetEvent = new AutoResetEvent(false);

            // Після того, як хтось сигналізує про завершення - стан події треба вручну скинути...
            ManualResetEvent resetEvent = new ManualResetEvent(false);
            Console.WriteLine("Main begins work...");  // 1
            MyThread t1 = new MyThread(resetEvent);

            // Очікуємо сигнал про завершення
            resetEvent.WaitOne();
            // ... за допомогою метода Reset()
            // якщо для Manual, не встановимо Reset - то подія більше не перебуватиме в несигнальному стані -
            // відповідно waitOne працювати не буде
            resetEvent.Reset();
            Console.WriteLine("Main continue work...."); // 5

            MyThread t2 = new MyThread(resetEvent);
            resetEvent.WaitOne();
            
            Console.WriteLine("Main finished work...."); // 9
        }
    }
}
