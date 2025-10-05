using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LibraryDataAccessLayer;


namespace LibraryBussinessLayer
{
    public class clsBorrowingLibrary
    {
        enum enMode { AddNewLib, UpdateLib};
        enMode _Mode;
        public int BorrowingID { set; get; }
        public int BookID { set; get; }
        public  int UserID { set; get; }
        public DateTime BorrowingDate { set; get; }
        public DateTime DueDate { set; get; }
        public DateTime ActualReturnDate { set; get; }

        public clsBorrowingLibrary(int BookID, int UserID)
        {
            this.BorrowingID = -1;
            this.BookID = BookID;
            this.UserID = UserID; // ✅ Current logged-in user;
            this.BorrowingDate = DateTime.Now;
            this.DueDate = DateTime.Now.AddDays(7); // Due date days
            this.ActualReturnDate = DateTime.MinValue;

            _Mode = enMode.AddNewLib;
        }
        private clsBorrowingLibrary(int BorrowingID, int BookID, int UserID, DateTime BorrowingDate,
            DateTime DueDate, DateTime ActualReturnDate)
        {
            this.BorrowingID = BorrowingID;
            this.BookID = BookID;
            this.UserID = UserID;
            this.BorrowingDate = BorrowingDate;
            this.DueDate = DueDate;
            this.ActualReturnDate = ActualReturnDate;

            _Mode = enMode.UpdateLib;
        }

        public static DataTable GetAllBooksList()
        {
            return clsBorrowingLibData.GetAllBooks();
        }

        public static string GetBorrowedBookAdditionalInfo(int BorrowingID)
        {
            return clsBorrowingLibData.GetBorrowedBookAdditionalInfo(BorrowingID);
        }

        public static DataTable GetAllBorrowedBooksList(int UserID)
        {
            return clsBorrowingLibData.GetAllBorrowedBooks(UserID);
        }

        public bool _BorrowNewBook()
        {
            if(clsBorrowingLibData.IsBookAlreadyBorrowed(this.BookID, this.UserID))
            {
                // Book already borrowed
                return false;
            }
            return clsBorrowingLibData.AddNewLibrary(this.BookID, this.UserID, this.BorrowingDate, 
                this.DueDate, this.ActualReturnDate) !=  this.BorrowingID;
        }

        public static bool ReturnBook(int BookID)
        {
            return clsBorrowingLibData.ReturnBook(BookID);
        }
        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.AddNewLib:
                    if (_BorrowNewBook())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

            }
            return false;
        }

    }
}
