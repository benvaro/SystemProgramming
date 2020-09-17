using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _09_TheadPool
{
    class Program
    {
        static void Main(string[] args)
        {
            // start-stop
            // 1) перехід в режим ядра
            // 2) збережи регістри процесора в режимі ядра поточного процеса (до 1 МБ)
            // 3) установка блокування
            // 4) визначити, який процес буде наступним для запуску
            // 5) Звільнити блокування, якщо запускається потік іншого процесу - то виконується перемикання віртуального адресного простору
            // 6) загрузити регістри із значеннями із об'єктів ядра потока, який має виконуватись наступним
            // 7) Вихід з режиму ядра
            // перемикання раз в 20мс

            // Пул потоків - ThreadPool
            // CLR керує виділенням потоків в ThreadPool
            // TimerCallback
            // 1 thread / 500ms
            // 25 / 1000

            //ShowInfoThreadPool();

            Random rnd = new Random();

            for (int i = 0; i < 10; i++)
            {
                int a = rnd.Next(-30, 30);
                // "Ставимо" в чергу метод (функціонал), нехай виділенням додаткових потоків займається CLR 
                ThreadPool.QueueUserWorkItem(PrintInfo, a);
                a = rnd.Next(-30, 30);
            }
            Console.WriteLine("Main works...");
            Console.WriteLine("Main ended");
            // Всі потоки в ThreadPool - фонові
            // не можна керувати паузою та перериванням потоків з threadPool
            Console.ReadLine();
        }

        private static void ShowInfoThreadPool()
        {
            ThreadPool.GetMinThreads(out int workerThreads, out int ioThreads);
            Console.WriteLine("Min count of threads: {0} - {1}", workerThreads, ioThreads );
           // ThreadPool.SetMaxThreads(25, 500);
            ThreadPool.GetMaxThreads(out workerThreads, out ioThreads);
            Console.WriteLine("Max count of threads: {0} - {1}", workerThreads, ioThreads );
        }

        static void PrintInfo(object state)
        {
            Console.WriteLine("Thread #{0}, \tstate{1}", Thread.CurrentThread.ManagedThreadId, (int)state);
        }
    }
}
