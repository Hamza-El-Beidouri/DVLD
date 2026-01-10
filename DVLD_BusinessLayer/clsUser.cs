using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_BusinessLayer
{
    public class clsUser
    {

        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public clsPerson PersonInfo;

        private bool _AddNewUser()
        {
            UserID = clsUserData.AddNewUser(PersonInfo.PersonID, UserName, Password, IsActive);
            return (UserID != -1);
        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(UserID, UserName, Password, IsActive);
        }

        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = false;
            this.PersonInfo = new clsPerson();
            Mode = enMode.AddNew;
        }

        private clsUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
            Mode = enMode.Update;
        }

        public static clsUser Find(int UserID)
        {

            int PersonID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;

            if (clsUserData.GetUserByUserID(UserID, ref PersonID, ref UserName, ref Password, ref IsActive))
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;

        }

        public static clsUser FindByPersonID(int PersonID)
        {

            int UserID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;

            if (clsUserData.GetUserByPersonID(PersonID, ref UserID, ref UserName, ref Password, ref IsActive))
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;

        }

        public static clsUser Find(string UserName, string Password)
        {

            int UserD = -1, PersonID = -1;
            bool IsActive = false;

            if (clsUserData.GetUserByUserNameAndPassword(UserName, Password, 
                            ref UserD, ref PersonID, ref IsActive))
                return new clsUser(UserD, PersonID, UserName, Password, IsActive);
            else
                return null;

        }

        public bool Save()
        {

            switch (Mode)
            {

                case enMode.AddNew:

                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateUser();

            }

            return false;

        }

        public bool ChangePassword(string NewPassword)
        {
            return clsUserData.ChangePassword(this.UserID, NewPassword);
        }

        public static bool Delete(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public static bool IsUSerExist(int UserID)
        {
            return clsUserData.IsUserExist(UserID);
        }

        public static bool IsUserExistForPersonID(int PersonID)
        {
            return clsUserData.IsUserExistForPersonID(PersonID);
        }

        public static bool IsUserExist(string UserName)
        {
            return clsUserData.IsUserExist(UserName);
        }

        public static bool IsUserExist(string UserName, string Password)
        {
            return clsUserData.IsUserExist(UserName, Password);
        }

    }
}