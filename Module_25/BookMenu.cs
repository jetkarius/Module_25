using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_25
{
    public class BookMenu
    {
        public static void ShowBookMenu()
        {
            var db = new AppContext();

            Console.WriteLine("Меню книг");
            Console.WriteLine("Для выбора по идентификатору введите 1");
            Console.WriteLine("Для выбора всех нажмите 2");
            Console.WriteLine("Для добавления введите 3");
            Console.WriteLine("Для удаления введите 4");
            Console.WriteLine("Для изменения года выхода книги введите 5");
            Console.WriteLine(Command.stop + ": прекращение работы");
            string command;

            do
            {
                Console.WriteLine(" Ввидите команду");
                command = Console.ReadLine();

                switch (command)
                {
                    case "1":
                        SelectBook();
                        break;
                    case "2":
                        SelectAll();
                        break;
                    case "3":
                        AddBook();
                        break;
                    case "4":
                        RemoveBook();
                        break;
                    case "5":
                        UodateBookName();
                        break;
                }
                Console.WriteLine();
            }
            while (command != nameof(Command.stop));
            Console.WriteLine("Работа прекращена");
        }

        public static void SelectBook()
        {
            var db = new AppContext();
            Console.WriteLine("Введите идентификатор книги");
            var ID = Int32.Parse(Console.ReadLine());
            var book = db.Books.Where(o => o.Id == ID).FirstOrDefault();
            Console.WriteLine(book.Name);
        }

        public static void SelectAll()
        {
            var db = new AppContext();
            var allBooks = db.Books.ToList();
            foreach (var book in allBooks)
            {
                Console.WriteLine(book.Name);
            }
        }

        public static void AddBook()
        {
            var db = new AppContext();
            Console.WriteLine("Введите название книги");
            string name = Console.ReadLine();
            Console.WriteLine("Введите год публикации");
            int year = Int32.Parse(Console.ReadLine());
            Book book = new Book { Name = name, Year = year };
            db.Books.Add(book);
            db.SaveChanges();
        }

        public static void RemoveBook()
        {
            var db = new AppContext();
            Console.WriteLine("Введите идентификатор книги для удаления");
            var ID = Int32.Parse(Console.ReadLine());
            Book book = db.Books.Where(o => o.Id == ID).FirstOrDefault();
            db.Books.Remove(book);
            db.SaveChanges();
        }

        public static void UodateBookName()
        {
            var db = new AppContext();
            Console.WriteLine("Введите идентификатор пользователя");
            var ID = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите новое значение");
            int newYear = Int32.Parse(Console.ReadLine());
            db.Books.Where(o => o.Id == ID).First().Year = newYear;
            db.SaveChanges();
        }

        public enum Command
        {
            stop
        }
    }
}
