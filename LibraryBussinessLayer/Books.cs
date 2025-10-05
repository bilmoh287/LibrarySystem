using System;
using System.Data;
using LibraryDataAccessLayer;

namespace LibraryBussinessLayer
{
    public class clsBooks
    {
        public enum enMode { AddNewMode, UpdateMode };
        public enMode _Mode = enMode.UpdateMode;

        public int ID {  get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Genre { get; set; }
        public string AdditionalInfo { get; set; }

        public clsBooks()
        {
            ID = -1;
            Title = "";
            ISBN = "";
            PublicationDate = DateTime.Now;
            Genre = "";
            AdditionalInfo = "";

            _Mode = enMode.AddNewMode;
        }

        private clsBooks(int ID, string Title, String ISBN, DateTime PubDate, string Genre, string AdditionalInfo)
        {
            this.ID = ID;
            this.Title = Title;
            this.ISBN = ISBN;
            this.PublicationDate = PubDate;
            this.Genre = Genre;
            this.AdditionalInfo = AdditionalInfo;

            _Mode = enMode.UpdateMode;
        }

        public static clsBooks Find(String Name)
        {
            int ID = -1;
            DateTime PubDate = DateTime.Now;
            string Genre = "", AdditionalInfo = "", ISBN = "";

            if(clsBooksData.FindByName(Name, ref ID, ref ISBN, ref PubDate, ref Genre, ref AdditionalInfo))
            {
                return new clsBooks(ID, Name, ISBN, PubDate, Genre, AdditionalInfo);
            }
            else
            {
                return null;
            }
        }

        public static clsBooks Find(int ID)
        {
            DateTime PubDate = DateTime.Now;
            string Genre = "", AdditionalInfo = "", ISBN = "", Name = "";

            if (clsBooksData.FindByID(ID, ref  Name, ref ISBN, ref PubDate, ref Genre, ref AdditionalInfo))
            {
                return new clsBooks(ID, Name, ISBN, PubDate, Genre, AdditionalInfo);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllBooks()
        {
            return clsBooksData.GetAllBooks();
        }


        private bool _AddNewBook()
        {
            this.ID =  clsBooksData.AddNewBook(this.Title, this.ISBN, this.PublicationDate,
                this.Genre, this.AdditionalInfo);

            return this.ID != -1;
        }

        private bool _UpdateBook()
        {
            return clsBooksData.UpdateBook(this.ID, this.Title, this.ISBN, 
                this.PublicationDate, this.Genre, this.AdditionalInfo);
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.AddNewMode:
                    if (_AddNewBook())
                    {
                        _Mode = enMode.UpdateMode;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.UpdateMode:
                   return _UpdateBook();
            }
            return false;
        }

        public static bool DeleteBook(int ID)
        {
            return clsBooksData.DeleteBook(ID);
        }

        public static DataTable SearchBooks(string keyword)
        {
            return clsBooksData.SearchBooksByTitle(keyword);
        }
    }
}
