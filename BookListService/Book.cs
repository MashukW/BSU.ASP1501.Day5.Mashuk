using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookService
{
    public class Book : IEquatable<Book>, IComparable<Book>
    {
        #region Сlass Сonstructors

        public Book(string author, string title, int numberOfPages) :
            this(author, title, numberOfPages, "None", 0)
        { }

        public Book(string author, string title, int numberOfPages, string publisher) :
            this(author, title, numberOfPages, publisher, 0)
        { }

        public Book(string author, string title, int numberOfPages, string publisher, int yearIssued)
        {
            if (author == null)
                throw new ArgumentException("Author is not specified");
            if (title == null)
                throw new ArgumentException("Title is not specified");
            if (numberOfPages == 0)
                throw new ArgumentException("Number of pages cannot be equal to zero");

            Author = author;
            Title = title;
            Publisher = publisher;
            YearIssued = yearIssued;
            NumberOfPages = numberOfPages;
        }

        #endregion

        #region Property

        public string Author { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public int YearIssued { get; set; }
        public int NumberOfPages { get; set; }

        #endregion

        public bool Equals(Book other)
        {
            if (other == null)
                return false;
            if (this.Author != other.Author)
                return false;
            if (this.Title != other.Title)
                return false;
            if (this.Publisher != other.Publisher)
                return false;
            if (this.YearIssued != other.YearIssued)
                return false;
            if (this.NumberOfPages != other.NumberOfPages)
                return false;

            return true;
        }

        public int CompareTo(Book other)
        {
            return this.Author.CompareTo(other.Author);
        }

        public override string ToString()
        {
            StringBuilder resultStr = new StringBuilder();
            resultStr.Append("Author: " + Author + " ");
            resultStr.Append("Title: " + Title + " ");
            resultStr.Append("Publisher: " + Publisher + " ");
            resultStr.Append("YearIssued: " + YearIssued + " ");
            resultStr.Append("NumberOfPages: " + NumberOfPages);

            return resultStr.ToString();
        }
    }
}
