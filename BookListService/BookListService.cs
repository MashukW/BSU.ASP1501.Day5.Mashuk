using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace BookService
{
    public class BookListService
    {
        private List<Book> listBooks = new List<Book>();
        private readonly string path = null;

        public IList<Book> ListBooks
        {
            get
            {
                return listBooks;
            }
        }

        #region Сlass Сonstructors

        public BookListService() :
            this(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BookLibrary.txt"))
        { }

        public BookListService(IList<Book> books)
            : this(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BookLibrary.txt"), books)
        {

        }

        public BookListService(string path)
        {
            if (!File.Exists(path))
                File.Create(path);
            else
                ReadFromFileToListBooks(path, listBooks);

            this.path = path;
        }

        public BookListService(string path, IList<Book> books)
        {
            if (books == null)
                throw new ArgumentException("List Books is null");

            for (int i = 0; i < books.Count; i++)
                listBooks.Add(books[i]);

            this.path = path;
            WriteListBooksInFile(books);
        }

        #endregion

        #region Public Methods (AddBook, RemoveBook, FindByTag, SortsBooksByTag)

        public void AddBook(Book book)
        {
            if (book == null)
                return;
            if (listBooks.Contains(book))
                throw new Exception("This book already exists in the list");

            listBooks.Add(book);
            WriteBookInFile(listBooks[listBooks.Count - 1]);
        }
        public void AddBook(IList<Book> books)
        {
            if (books == null)
                throw new ArgumentException("Books collection is null");

            for (int i = 0; i < books.Count; i++)
                AddBook(books[i]);
        }

        public void RemoveBook(Book book)
        {
            if (book == null)
                return;
            if (!listBooks.Contains(book))
                throw new Exception("Book to be deleted was not found");

            listBooks.Remove(book);
            File.Delete(path);
            WriteListBooksInFile(listBooks);
        }

        public IEnumerable<Book> FindByTag(Predicate<Book> tag)
        {
            return listBooks.FindAll(tag);
        }

        public void SortsBooksByTag()
        {
            if (listBooks.Count == 0)
                return;

            listBooks.Sort();
            File.Delete(path);
            WriteListBooksInFile(listBooks);
        }
        public void SortsBooksByTag(IComparer<Book> comparer)
        {
            if (listBooks.Count == 0)
                return;

            IComparer<Book> listComparer = comparer as IComparer<Book>;

            if (listComparer == null)
                SortsBooksByTag();
            else
            {
                listBooks.Sort(comparer);
                File.Delete(path);
                WriteListBooksInFile(listBooks);
            }
        }

        #endregion

        #region Private (helper) methods

        private void WriteBookInFile(Book bookForWrite)
        {
            if (bookForWrite == null)
                throw new ArgumentNullException("Book for write is null");

            using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                writer.Write(bookForWrite.Author);
                writer.Write(bookForWrite.Title);
                writer.Write(bookForWrite.NumberOfPages);
                writer.Write(bookForWrite.Publisher);
                writer.Write(bookForWrite.YearIssued);
            }
        }

        private void WriteListBooksInFile(IList<Book> listBookForWrite)
        {
            if (listBookForWrite == null)
                throw new ArgumentException("Collection books is null");

            using (FileStream fs = new FileStream(path, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                for (int i = 0; i < listBookForWrite.Count; i++)
                {
                    writer.Write(listBookForWrite[i].Author);
                    writer.Write(listBookForWrite[i].Title);
                    writer.Write(listBookForWrite[i].NumberOfPages);
                    writer.Write(listBookForWrite[i].Publisher);
                    writer.Write(listBookForWrite[i].YearIssued);
                }
            }
        }

        private void ReadFromFileToListBooks(string path, List<Book> listBookForRead)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                while (reader.PeekChar() > -1)
                {
                    string author = reader.ReadString();
                    string title = reader.ReadString();
                    int numberOfPages = reader.ReadInt32();
                    string publisher = reader.ReadString();
                    int yearIssued = reader.ReadInt32();

                    listBookForRead.Add(new Book(author, title, numberOfPages, publisher, yearIssued));
                }
            }
        }

        #endregion
    }
}
