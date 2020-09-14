using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _01_Intro
{
    class Program
    {
        static void Main(string[] args)
        {
            // Process - виконувана програма
            // PID, Priority, Type, Machine
            // Потоки - Thread
            //
            // 
            // BCL - System.Diagnostic
            // Assemblies
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "Hello.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
         //   startInfo.Arguments = "https://stackoverflow.com";

            Process proc = new Process();
            proc.StartInfo = startInfo;
            proc.Start();

            Console.WriteLine($"ID: {proc.Id}");
            Console.WriteLine($"Priority: {proc.BasePriority}");
            Console.WriteLine($"Name: {proc.ProcessName}");
            Console.WriteLine($"Main module: {proc.MainModule}");
            Console.WriteLine($"Machine: {proc.MachineName}");
            Console.WriteLine($"Time: {proc.StartTime}");
            proc.Exited += Proc_Exited;
            //proc.WaitForExit();

            Thread.Sleep(2000);
            proc.Kill();

            //    Console.WriteLine($"Exit code: {proc.ExitCode}");

            // Process.Start("chrome.exe", "https://google.com");
            //  Console.ReadLine();
            Console.Write("{0, -7}{1, 5}{2,-20}{3,25}, {4,-25}", "ID", "Priority", "StartTime", "Name", "Module");
            Console.WriteLine();
            foreach (var process in Process.GetProcesses())
            {
                try
                {
                    ShowInfo(process);
                }
                catch { }
            }

            Console.ReadLine();
            proc = Process.GetProcessById(int.Parse(Console.ReadLine()));
            Console.ForegroundColor = ConsoleColor.Green;
            ShowInfo(proc);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void ShowInfo(Process proc)
        {
           
            Console.Write($"{proc.Id, -7}{proc.BasePriority,5}{proc.StartTime,-20}{proc.ProcessName,25}{proc.MainWindowTitle,-25}");
            Console.WriteLine();
        }

        private static void Proc_Exited(object sender, EventArgs e)
        {
           
        }
    }
}
