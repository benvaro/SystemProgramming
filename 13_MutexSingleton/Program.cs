using System;
using System.Threading;
using System.Windows.Forms;

namespace _13_MutexSingleton
{
    class Program
    {
        static Mutex mutex;
        static void Main(string[] args)
        {
            try
            {
                mutex = Mutex.OpenExisting("my_mutex");
            }
            catch (WaitHandleCannotBeOpenedException e)
            {
                Console.WriteLine(e.Message);
            }
            if (mutex != null)
            {
                MessageBox.Show("Program has already started!!!");
                return;
            }
            else
            {
                mutex = new Mutex(true, "my_mutex");
                Console.WriteLine("Hello world. Program works....");
            }
            Console.ReadLine();
        }
    }
}
