using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookService;

namespace ConsoleUI
{
    class UserInterface
    {
        static void DisplayBook(IList<Book> listBook)
        {
            foreach (Book book in listBook)
                Console.WriteLine(book.ToString());

            Console.WriteLine(new string('*', 80));
        }

        static void Main(string[] args)
        {
            List<Book> library = new List<Book>
            {
                new Book("Михаил Булгаков", "Мастер и Маргарита",  462, "Эксмо", 2006),
                new Book("Фёдор Достоевский", "Преступление и наказание",  630, "АСТ, Неоклассик", 2006),
                new Book("Михаил Лермонтов", "Герой нашего времени",  189, "АСТ, Неоклассик", 2008),
                new Book("Лев Толстой", "Анна Каренина",  706, "Издательский Дом Ридерз Дайджест", 2006),
            };

            BookListService service = new BookListService(library);
            DisplayBook(service.ListBooks);
           
            Book bookToAdd = new Book("Александр Пушкин", "Евгений Онегин", 203, "Питер", 2012);
            service.AddBook(bookToAdd);
            DisplayBook(service.ListBooks);
            
            service.RemoveBook(library[2]);
            DisplayBook(service.ListBooks);

            ComparerByNumberOfPages compareNumberOfPages = new ComparerByNumberOfPages();
            ComparerByPublisher comparePublisher = new ComparerByPublisher();
            ComparerByTitle comaperTitle = new ComparerByTitle();
            ComparerByYearIssued comaperYearIssued = new ComparerByYearIssued();

            service.SortsBooksByTag(comaperTitle);
            DisplayBook(service.ListBooks);

            IEnumerable<Book> resultBook = service.FindByTag(book => book.YearIssued == 2006);
            foreach (var item in resultBook)
                Console.WriteLine(item);

            Console.ReadKey();
        }
    }
}
