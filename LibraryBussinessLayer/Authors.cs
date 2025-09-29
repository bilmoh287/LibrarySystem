using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public clsAuthors()
        {
            AuthorsID = 0;
            FullName = string.Empty;
            DateOfBirth = DateTime.MinValue;
            Nationality = string.Empty;
            ContactInfo = string.Empty;
        }

        private clsAuthors(int ID, string FullName, DateTime DateOfBirth, string Nationality, string ContactInfo)
        {
            this.AuthorsID = ID;
            this.FullName = FullName;
            this.DateOfBirth = DateOfBirth;
            this.Nationality = Nationality;
            this.ContactInfo = ContactInfo;
        }

        public static DataTable GetAllAuthorsList()
        {
            return clsAuthorsData.GetAllAuthorsList();
        }
    }
}
