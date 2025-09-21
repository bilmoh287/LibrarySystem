using System;
using System.Data;
using LibraryDataAccessLayer;

namespace LibraryBussinessLayer
{
    public class clsBooks
    {
        public enum enMode { AddNewMode, UpdateMode };
        public enMode Mode = enMode.UpdateMode;


        public static DataTable GetAllBooks()
        {
            return clsBooksData.GetAllBooks();
        }
    }
}
