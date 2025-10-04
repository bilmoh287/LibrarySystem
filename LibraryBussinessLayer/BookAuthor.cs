using System;
using System.Collections.Generic;
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
    }
}
