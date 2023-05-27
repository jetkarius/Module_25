using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Module_25
{
    public class UserMenu
    {
        public static void ShowUserMenu()
        {
            var user = new User();

            Console.WriteLine("Меню пользователей");
            Console.WriteLine("Для выбора по идентификатору введите 1");
            Console.WriteLine("Для выбора всех пользователей нажмите 2");
            Console.WriteLine("Для добавления пользователя введите 3");
            Console.WriteLine("Для удаления пользователя введите 4");
            Console.WriteLine("Для обновления имени пользователя введите 5");
            Console.WriteLine(Command.stop + ": прекращение работы");

            string command;



            do
            {
                Console.WriteLine(" Ввидите команду");
                command = Console.ReadLine();

                switch (command)
                {
                    case "1":
                        SelectUser();
                        break;
                    case "2":
                        SelectAll();
                        break;
                    case "3":
                        AddUser();
                        break;
                    case "4":
                        RemoveUser();
                        break;
                    case "5":
                        UodateUserName();
                        break;
                }
                Console.WriteLine();
            }
            while (command != nameof(Command.stop));
            Console.WriteLine("Работа прекращена");
        }

        public static void SelectUser()
        {
            var db = new AppContext();
            Console.WriteLine("Введите идентификатор пользователя");
            var ID = Int32.Parse(Console.ReadLine());
            var user = db.Users.Where(o => o.Id == ID).FirstOrDefault();
            Console.WriteLine(user.Name);
        }

        public static void SelectAll()
        {
            var db = new AppContext();
            var allUsers = db.Users.ToList();
            foreach (var user in allUsers)
            {
                Console.WriteLine(user.Name);
            }
        }

        public static void AddUser()
        {
            var db = new AppContext();
            Console.WriteLine("Введите имя ползователя");
            string name = Console.ReadLine();
            Console.WriteLine("Введите Email");
            string email = Console.ReadLine();
            User user = new User { Name = name, Email = email };
            db.Users.Add(user);
            db.SaveChanges();
        }

        public static void RemoveUser()
        {
            var db = new AppContext();
            Console.WriteLine("Введите идентификатор пользователя для удаления");
            var ID = Int32.Parse(Console.ReadLine());
            User user = db.Users.Where(o => o.Id == ID).FirstOrDefault();
            db.Users.Remove(user);
            db.SaveChanges();
        }

        public static void UodateUserName()
        {
            var db = new AppContext();

            Console.WriteLine("Введите идентификатор пользователя");
            var ID = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите новое имя пользователя");
            string newName = Console.ReadLine();
            db.Users.Where(o => o.Id == ID).First().Name = newName;
            db.SaveChanges();
        }
        public enum Command
        {
            stop
        }
    }
}