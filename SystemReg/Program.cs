using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemReg
{
    class Program
    {
        static void Main(string[] args)
        {
            // Registry
            // RegistryKey

            RegistryKey key = Registry.CurrentUser;

           // PrintKey(key);

            RegistryKey newKey = key.CreateSubKey("NewKey");
            newKey.Close();

            newKey = key.OpenSubKey("NewKey", true);
            RegistryKey subKey = newKey.CreateSubKey("Config");

            subKey.SetValue("login", "admin");
            subKey.SetValue("Password", 5678);

            newKey.SetValue("some value", "hello, registry");
            newKey.Close();

            // Доступаюсь до ключа в реєстрі - в даному випадку список автозавантажень в системі
            RegistryKey run = key.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

            var items = run.GetValueNames();

            foreach (var item in items)
            {
                if (item.Equals("Mikogo"))
                {
                    run.DeleteValue(item);
                    continue;
                }
                Console.WriteLine($"{item, -28}{run.GetValueKind(item), -15}{run.GetValue(item), -35}");
            }
        }

        private static void PrintKey(RegistryKey key)
        {
            string[] names = key.GetSubKeyNames();
            Console.WriteLine("Subkey of {0}", key.Name);
            foreach (var item in names)
            {
                Console.WriteLine($"{item}");
            }
        }
    }
}
