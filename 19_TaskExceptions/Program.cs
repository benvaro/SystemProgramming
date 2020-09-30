using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19_TaskExceptions
{
    class Program
    {
        static void Main(string[] args)
        {
            Task t1 = new Task(() =>
            {
                throw new NullReferenceException("Task 1");
            });
            Task t2 = new Task(() =>
            {
                Exception ex = new Exception();
                ex.Source = "Task 2";
                throw ex;
            });

            Task t3 = new Task(() => Console.WriteLine("Hello task 3"));

            t1.Start();
            t2.Start();
            t3.Start();

            try
            {
                Task.WaitAll(t1, t2, t3);
            }
            catch(AggregateException ex)
            {
                foreach (Exception item in ex.InnerExceptions)
                {
                    Console.WriteLine(item.Message + " " + item.Source + " " + item.GetType().Name);
                }
            }

            Console.WriteLine("Work in main...");
            Console.ReadLine();
        }
    }
}
