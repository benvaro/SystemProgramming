using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Dynamic_var
{
    class Program
    {
        static void Main(string[] args)
        {
            // тип знаємо на етапі компіляції
            var result = 34;
            Console.WriteLine(result.GetType().Name);
            //  result = "Hello"; // error

            // тип дізнаємось на етапі виконання
            dynamic res = 12;
            res = "Hello";
            Console.WriteLine(res + " " + res.GetType().Name);
            res = new DynamicTest { MyProperty = 34, Test = new DynamicTest { MyProperty = 100, Test = "Hello" } };
            res.SetValue(34f);
            Console.WriteLine(res);


        }
    }
}
