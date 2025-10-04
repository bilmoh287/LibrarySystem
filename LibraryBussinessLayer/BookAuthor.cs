using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryDataAccessLayer;

namespace LibraryBussinessLayer
{
    public class clsBookAuthor
    {
        public static bool AddRelation(int bookID, int authorID)
        {
            return clsBookAuthorData.AddRelation(bookID, authorID);
        }

        public static DataTable GetAllAuthorWhoWroteTheBook(int BookID)
        {
            return clsBookAuthorData.GetAllAuthorWhoWroteTheBook(BookID);
        }

        public static DataTable GetAllBooksWrittenByAuthor(int BookID)
        {
            return clsBookAuthorData.GetAllBooksWrittenByAuthor(BookID);
        }
    }
}
