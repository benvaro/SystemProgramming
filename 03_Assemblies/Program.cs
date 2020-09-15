using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _03_Assemblies
{
    class Program
    {
        static void Main(string[] args)
        {
            // Основний контейнер, в якому запускається збірка
            AppDomain app = AppDomain.CurrentDomain;

            Console.WriteLine(app.BaseDirectory);
            Console.WriteLine(app.FriendlyName);
            Console.WriteLine();
            
            foreach (Assembly item in app.GetAssemblies())
            {
                Console.WriteLine("Name: " + item.FullName);
                Console.WriteLine("Name: " + item.Location);
                    Console.WriteLine("Modules: ");
                var result = from module in item.Modules
                             select module;

                //foreach (var module in item.GetModules())
                //{
                //    Console.WriteLine(module.Name);
                //    Console.WriteLine(module.Assembly);
                //}
                //Console.WriteLine();

                // Переглядаємо модулі (бібліотеки), з яких складається збірка

                foreach (var module in result)
                {
                    Console.WriteLine(module.Name);
                    Console.WriteLine(module.Assembly);
                }
                Console.WriteLine();

          //      Console.WriteLine(MathLibrary.MathLibrary.Add(5, 8));
            }

            TestReflection();
        }
        public static void TestReflection()
        {
            // Створили домен
            AppDomain test = AppDomain.CreateDomain("Test");
            // Загрузили бібліотеку в домен
            var asm = test.Load("MathLibrary");
            // Отримуємо тип з нашої бібліотеки
            var type = asm.GetType("MathLibrary.MathLibrary");
            // Створюємо об'єкт нашого класу
           // dynamic obj = Activator.CreateInstance(type);
            var obj = Activator.CreateInstance(type);
            // викликаємо метод
          //  Console.WriteLine(obj.Add(4,7));
            var res = type.GetMethod("Add").Invoke(obj, new object[] { 4, 7 });
            Console.WriteLine(res);
        }
    }
}
