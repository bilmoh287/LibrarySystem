using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryDataAccessLayer;

namespace LibraryBussinessLayer
{
    public class clsUsers
    {
        public enum enMode { AddNewMode, UpdateMode }
        private enMode _Mode;
        public int UserID { set; get; }
        public string FullName { set; get; }
        public DateTime DateOfBirth { set; get; }
        public string ContactInfo { set; get; }
        public string LibraryCard { set; get; }
        public int Permission { set; get; }

        public clsUsers()
        {
            UserID = -1;
            FullName = string.Empty;
            DateOfBirth = DateTime.MinValue;
            ContactInfo = string.Empty;
            LibraryCard = string.Empty;
            Permission = 0;

            _Mode = enMode.AddNewMode;
        }

        private clsUsers(int ID, string FullName, DateTime DateOfBirth, string ContactInfo, string LibraryCard, int Permission)
        {
            this.UserID = ID;
            this.FullName = FullName;
            this.DateOfBirth = DateOfBirth;
            this.ContactInfo = ContactInfo;
            this.LibraryCard = LibraryCard;
            this.Permission = Permission;

            _Mode = enMode.UpdateMode;
        }

        public static DataTable GetAllUsersList()
        {
            return clsUsersData.GetAllUsersList();
        }


        public static clsUsers Find(int AuthorID)
        {
            string FullName = "", ContactInfo = "", LibraryCard = "";
            DateTime DateOfBirth = DateTime.Now;
            int Permission = 0;

            if (clsUsersData.FindByID(AuthorID, ref FullName, ref DateOfBirth, ref ContactInfo, ref LibraryCard, ref Permission))
            {
                return new clsUsers(AuthorID, FullName, DateOfBirth, ContactInfo, LibraryCard, Permission);
            }
            else
            {
                return null;
            }
        }
        private bool _AddNewAuthor()
        {
            this.UserID = clsUsersData.AddNewUser(this.FullName, this.DateOfBirth,
                this.ContactInfo, this.Permission);

            return this.UserID != -1;
        }

        private bool _UpdateAuthor()
        {
            if (clsUsers.Find(this.UserID) != null)
            {
                return clsUsersData.UpdateUser(this.UserID, this.FullName, this.DateOfBirth,
                    this.ContactInfo, this.Permission);
            }
            else
            {
                return false;
            }
        }

        public static bool DeleteAuthor(int UserID)
        {
            return clsUsersData.DeleteUser(UserID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNewMode:
                    if (_AddNewAuthor())
                    {
                        _Mode = enMode.UpdateMode;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.UpdateMode:
                    return _UpdateAuthor();
            }
            return false;
        }

    }
}
