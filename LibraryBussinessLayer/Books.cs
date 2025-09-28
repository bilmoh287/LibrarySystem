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
        public string Name { get; set; }
        public string ISBN { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Genre { get; set; }
        public string AdditionalInfo { get; set; }

        public clsBooks()
        {
            ID = -1;
            Name = "";
            ISBN = "";
            PublicationDate = DateTime.Now;
            Genre = "";
            AdditionalInfo = "";

            _Mode = enMode.AddNewMode;
        }

        private clsBooks(int ID, string Name, String ISBN, DateTime PubDate, string Genre, string AdditionalInfo)
        {
            this.ID = 0;
            this.Name = Name;
            this.ISBN = ISBN;
            this.PublicationDate = PublicationDate;
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
            this.ID =  clsBooksData.AddNewBook(this.Name, this.ISBN, this.PublicationDate,
                this.Genre, this.AdditionalInfo);

            return this.ID != -1;
        }

        private bool _UpdateBook()
        {
            return true;
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
    }
}
