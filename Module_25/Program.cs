using Module_25;
using System.Windows.Input;
using AppContext = Module_25.AppContext;

namespace ConsoleAppDATABASE
{
    class Program
    {
        static void Main(string[] args)

        {
            using (var db = new AppContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var user1 = new User { Name = "Arthur", Email = "Arthur@mail.ru" };
                var user2 = new User { Name = "klim", Email = "Klim@mail.ru" };
                var user3 = new User { Name = "Masha", Email = "Masha@mail.ru" };

                var book1 = new Book { Name = "Моя жена ведьма", Year = 1999, Author = "А.Белянин", Gener = "Фентези" };
                var book2 = new Book { Name = "Ааргх", Year = 2007, Author = "А.Белянин", Gener = "Фентези" };
                var book3 = new Book { Name = "Игры чудовищ", Year = 2006, Author = "Т.Рымжанов", Gener = "Фентези" };
                var book4 = new Book { Name = "Ловец снов", Year = 2001, Author = "С.Кинг", Gener = "Фантастика" };
                var book5 = new Book { Name = "Видоизменённый углерод", Year = 2002, Author = "Р.Морган", Gener = "Фантастика" };
                var book6 = new Book { Name = "Марсианин", Year = 2014, Author = "Э.Вейер", Gener = "Фантастика" };

                user1.Books.AddRange(new[] { book3, book4 });
                user2.Books.AddRange(new[] { book5, book1 });
                user3.Books.AddRange(new[] { book2, book6 });

                book4.Users.AddRange(new[] { user1, user2 });

                db.Users.AddRange(user1, user2, user3);
                db.Books.AddRange(book1, book2, book3, book4, book5, book6);

                db.SaveChanges();
            }
            Console.WriteLine("Добро пожаловать!");
            Console.WriteLine("Для работы с пользователями введите 1!");
            Console.WriteLine("Для работы с книгами введите 2!");
            var commands = Console.ReadLine();

            switch (commands)
            {
                case "1":
                    UserMenu.ShowUserMenu();
                    break;
                case "2":
                    BookMenu.ShowBookMenu();
                    break;

            }

            using (var db = new AppContext())
            {
                //Получать список книг определенного жанра и вышедших между определенными годами
                var books = db.Books.Where(b => b.Gener == "Фентези").Where(y => y.Year == 1990 - 2015).ToList();
                //Получать количество книг определенного автора в библиотеке
                var countbook = db.Books.Where(a => a.Author == "А.Белянин").Count();
                //Получать количество книг определенного жанра в библиотеке
                var bookgenre = db.Books.Where(g => g.Gener == "Фантастика").Count();
                //Получать булевый флаг о том, есть ли книга определенного автора и с определенным названием в библиотеке
                var trueBook = db.Books.All(t => t.Author == "С.Кинг" == (true));
                //Получать булевый флаг о том, есть ли определенная книга на руках у пользователя
                var trueUser = db.Users.All(t => t.Name == "Ловец снов" == (true));
                //Получать количество книг на руках у пользователя
                var joinedUserBook = db.Users.Join(db.Books, b => b.Id, u => u.Id, (b, u) => new { BookUser = u.Id });
                //Получение последней вышедшей книги
                var yearBook = db.Books.Max(b => b.Year == 2014);
                //Получение списка всех книг, отсортированного в алфавитном порядке по названию
                var listBook = db.Books.OrderBy(b => b.Name).ToList();
                //Получение списка всех книг, отсортированного в порядке убывания года их выхода
                var listBookYear = db.Books.OrderByDescending(b => b.Year).ToList();

            }
        }
    }
}