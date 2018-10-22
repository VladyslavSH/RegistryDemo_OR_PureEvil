using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace registry
{
    class Program
    {
        static void Main(string[] args)
        {
            //ShowKeys();
            //CreateKeys();
            //CreateSubKeys();
            //ReadFromKeys();
            //DeleteKeys();
            ChangeKeys();
            Console.ReadKey();
        }

        private static void ChangeKeys()
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey helloKey = currentUserKey.OpenSubKey("HelloKey", true);
            helloKey.SetValue("Password","");
            helloKey.Close();
        }

        private static void DeleteKeys()
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey helloKey = currentUserKey.OpenSubKey("HelloKey", true);
            helloKey.DeleteSubKey("SubHelloKey");
            helloKey.DeleteValue("Login");
            helloKey.Close();

        }

        private static void ReadFromKeys()
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey helloKey = currentUserKey.OpenSubKey("HelloKey");
            string login = helloKey.GetValue("Login").ToString();
            string password = helloKey.GetValue("Password").ToString();
            helloKey.Close();
            Console.WriteLine(login);
            Console.WriteLine(password);
        }

        private static void CreateSubKeys()
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey helloKey = currentUserKey.OpenSubKey("HelloKey",true);
            RegistryKey subHelloKey = helloKey.CreateSubKey("SubHelloKey");
            subHelloKey.SetValue("isAdmin", "1");
            subHelloKey.Close();
            helloKey.Close();
        }

        private static void CreateKeys()
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey helloKey = currentUserKey.CreateSubKey("HelloKey");
            helloKey.SetValue("Login", "admin");
            helloKey.SetValue("Password", "qwerty");
            helloKey.Close();
        }

        private static void ShowKeys()
        {
            int SelectedItem = 0;
            RegistryKey[] regs = new RegistryKey[] {Registry.ClassesRoot,
            Registry.CurrentConfig,
            Registry.LocalMachine,
            Registry.Users,
            Registry.CurrentUser};
            do
            {
                int i = 1;
                Console.WriteLine("Выберете раздел системного реестра");
                foreach (RegistryKey reg in regs)
                {
                    Console.WriteLine($"{i++}.{reg.Name}");
                }
                Console.WriteLine("0.Exit");
                Console.Write(">");
                SelectedItem = Convert.ToInt32(Console.ReadLine());
                if (SelectedItem > 0 && SelectedItem <= regs.Length)
                {
                    PrintRegKeys(regs[SelectedItem - 1]);
                }
            } while (SelectedItem != 0);
        }

        private static void PrintRegKeys(RegistryKey registryKey)
        {
            string[] names = registryKey.GetSubKeyNames();
            Console.WriteLine($"Подразделы {registryKey.Name}");
            Console.WriteLine("_________________");
            foreach (var item in names)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("_________________");

        }
    }
}
