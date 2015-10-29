using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookService
{
    public class ComparerByNumberOfPages : IComparer<Book>
    {
        public int Compare(Book x, Book y)
        {
            if (x.NumberOfPages > y.NumberOfPages)
                return 1;
            else if (x.NumberOfPages < y.NumberOfPages)
                return -1;
            else
                return 0;
        }
    }
}
