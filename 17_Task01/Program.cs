using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _17_Task01
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Task - 4.0
            // TPL - task paralel library

            //   Task t1 = new Task(Factorial, 1);
            //   //    t1.Start();
            //   Task t2 = new Task((obj) =>
            //   {
            //       Console.WriteLine("Task: #" + (int)obj);
            //       int f = 1;
            //       for (int i = 1; i <= 6; i++)
            //       {
            //           f *= i;
            //       }
            
            //       Thread.Sleep(3000);
            //       Console.WriteLine("Factorial: {0}", f);
            //   }, 2);

            //   t2.Start();

            //   Task t3 = new Task((obj) =>
            //   {
            //       int f = 1;
            //       Console.WriteLine("Task: #" + (obj as int[])[0]);
            //       for (int i = 1; i <= (obj as int[])[1]; i++)
            //       {
            //           f *= i;
            //       }

            //       Thread.Sleep(1000 * (obj as int[])[1]); // unboxing
            //       Console.WriteLine("Factorial: {0}", f);
            //   }, new[] { 3, 4 }); // boxing
            //                       // object  <-  int
            //                       //       Task.WaitAny(t2);
            //   t3.Start();

            //   Task t4 = Task.Factory.StartNew(Factorial, 4);

            //   Task.WaitAll(t2, t3, t4);
            //   Console.WriteLine("Main works....");

            //   // Console.Clear();

            //   Task<int> t5 = new Task<int>(ResFactorial, 5);
            //   t5.Start();
            ////   Task.WaitAll(t5);
            //  // while (!t5.IsCompleted) ;
            //   Console.WriteLine("After task t5"); //?
            //   Console.WriteLine("Result from t5 task: " + t5.Result);

            Console.WriteLine("Main begins...."); // 1
            test();
            Console.WriteLine("Work in main....");  // 3
            Console.ReadLine();
        }

        private async static Task test()
        {
            Console.WriteLine("Begin test...");
            await ShowFactorial(5);
            Console.WriteLine("Work in method");
        }

        static public void Factorial(object state)
        {
            Console.WriteLine("Task: #" + (int)state);
            int f = 1;
            for (int i = 1; i < 6; i++)
            {
                f *= i;
            }

            Thread.Sleep(5000);
            Console.WriteLine("Factorial: {0}", f);
        }

        static async public Task<int> FactorialAsync(object state)
        {
           
            int f = 1;
            for (int i = 1; i < 6; i++)
            {
                f *= i;
            }

            Thread.Sleep(5000);
            return f;
        }

        static public int ResFactorial(object state)
        {
            int f = 1;
            for (int i = 1; i < (int)state; i++)
            {
                f *= i;
            }
            return f;
        }

        static async public Task ShowFactorial(int number)
        {
            Console.WriteLine("Begin ShowFactorial");  // 2
            int res = await FactorialAsync(number);

            Console.WriteLine("Work in method ShowFactorial"); // 4
            Console.WriteLine("Result: " + res);
        }

    }
}
