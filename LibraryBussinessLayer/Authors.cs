using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LibraryDataAccessLayer;

namespace LibraryBussinessLayer
{
    public class clsAuthors
    {
        public enum enMode { AddNewMode, UpdateMode}
        private enMode _Mode;
        public int AuthorsID { set; get; }
        public string FullName { set; get; }
        public DateTime DateOfBirth { set; get; }
        public string Nationality { set; get; }
        public string ContactInfo { set; get; }
        public string ImagePath { set; get; }

        public clsAuthors()
        {
            AuthorsID = -1;
            FullName = string.Empty;
            DateOfBirth = DateTime.MinValue;
            Nationality = string.Empty;
            ContactInfo = string.Empty;
            ImagePath = string.Empty;

            _Mode = enMode.AddNewMode;
        }

        private clsAuthors(int ID, string FullName, DateTime DateOfBirth, string Nationality, string ContactInfo, string ImagePath)
        {
            this.AuthorsID = ID;
            this.FullName = FullName;
            this.DateOfBirth = DateOfBirth;
            this.Nationality = Nationality;
            this.ContactInfo = ContactInfo;
            this.ImagePath = ImagePath;

            _Mode= enMode.UpdateMode;
        }

        public static DataTable GetAllAuthorsList()
        {
            return clsAuthorsData.GetAllAuthorsList();
        }


        public static clsAuthors Find(int AuthorID)
        {
            string FullName = "", Nationality = "", ContactInfo = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;

            if(clsAuthorsData.FindByID(AuthorID, ref FullName, ref DateOfBirth, ref Nationality, ref ContactInfo, ref ImagePath))
            {
                return new clsAuthors(AuthorID, FullName, DateOfBirth, Nationality, ContactInfo, ImagePath);
            }
            else
            {
                return null;
            }
        }
        private bool _AddNewAuthor()
        {
            this.AuthorsID = clsAuthorsData.AddNewAuthor(this.FullName, this.DateOfBirth,
                this.Nationality, this.ContactInfo, this.ImagePath);

            return this.AuthorsID != -1;
        }

        //private bool _UpdateAuthor()
        //{
        //    if(clsAuthors.Find(this.AuthorsID) != null)
        //    {

        //    }
        //}

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNewMode:
                    if(_AddNewAuthor())
                    {
                        _Mode = enMode.UpdateMode;
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
