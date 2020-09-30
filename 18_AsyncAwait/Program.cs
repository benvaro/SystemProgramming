using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _18_AsyncAwait
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // 1 test
            //  DoWorkAsync();
            // Виконуємо роботу в мейн
            //  Console.WriteLine("Work in main...."); // 2

            // 2 test
            /*Task<string>*/string res = await ReturnStringAsync();
            Console.WriteLine("Result: " + res/*.Result*/); // ?

            Console.WriteLine("Hello"); //?

            Console.ReadLine();
        }

        private static void DoWork()
        {
            Console.WriteLine("Work in doWork begin..."); // 3
            Thread.Sleep(5000);
            Console.WriteLine("Work in do work finished...."); //4
        }

        private async static Task DoWorkAsync()
        {
            // спочатку заходимо в даний метод і виконуємо код синхронно
            Console.WriteLine("Code in async method..."); //1
            // допоки не зустрінемо await...
            // стартує задача в окремому паралельному потоці 
            // а керування передається викликаючому методу (в мейн)

            // коли задача завершиться, то нам повернеться результат (бо await - означає чекати завершення)
            await Task.Run(DoWork);
            // і ми продовжимо роботу в методі (в основному потоці)
            Console.WriteLine("DoworkAsync finished..."); // 5
        }

        private async static Task<string> ReturnStringAsync()
        {
            return await Task.Run(() =>
            {
                Thread.Sleep(2000);
                return "Return string finished....";
            });
        }

    }
}
