using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _20_TaskCancelationToken
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;
            Task t = new Task(() =>
            {
                for (int i = 0; i < int.MaxValue; i++)
                {
                    if(token.IsCancellationRequested)
                    {
                        Console.WriteLine("Task cancel by user");
                        throw new OperationCanceledException();
                    }
                    Console.Write($"{ i,-5}");
                }
            }, token);

            Console.WriteLine("Press any key to start task:");
            Console.ReadKey();
            t.Start();
            Console.WriteLine("Press enter to stop");
            Console.ReadLine();
            // сигналізуємо про відміну таски
            tokenSource.Cancel();
            Console.WriteLine("Work in main");
            Console.ReadLine();
        }
    }
}
