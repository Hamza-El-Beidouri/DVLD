using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_BusinessLayer
{
    public class clsDriver
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode { get; set; }

        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo;   // Link to Person BLL
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo;
        public DateTime CreatedDate { get; set; }

        public clsDriver()
        {
            DriverID = -1;
            PersonID = -1;
            PersonInfo = new clsPerson();
            CreatedByUserID = -1;
            CreatedByUserInfo = new clsUser();
            CreatedDate = DateTime.Now;

            Mode = enMode.AddNew;
        }

        private clsDriver(int driverID, int personID, int createdByUserID, DateTime createdDate)
        {
            DriverID = driverID;
            PersonID = personID;
            PersonInfo = clsPerson.Find(personID);
            CreatedByUserID = createdByUserID;
            CreatedByUserInfo = clsUser.Find(createdByUserID);
            CreatedDate = createdDate;

            Mode = enMode.Update;
        }

        private bool _AddNewDriver()
        {
            DriverID = clsDriversData.AddNewDriver(
                PersonID,
                CreatedByUserID,
                CreatedDate
            );

            return (DriverID != -1);
        }

        private bool _UpdateDriver()
        {
            return clsDriversData.UpdateDriver(
                DriverID,
                PersonID,
                CreatedByUserID,
                CreatedDate
            );
        }

        public static clsDriver Find(int DriverID)
        {
            int personID = -1;
            int createdByUserID = -1;
            DateTime createdDate = DateTime.Now;

            bool isFound = clsDriversData.GetDriverByID(
                DriverID,
                ref personID,
                ref createdByUserID,
                ref createdDate
            );

            if (isFound)
                return new clsDriver(
                    DriverID,
                    personID,
                    createdByUserID,
                    createdDate
                );

            return null;
        }

        public static clsDriver FindByPersonID(int PersonID)
        {
            int driverID = -1;
            int createdByUserID = -1;
            DateTime createdDate = DateTime.Now;

            bool isFound = clsDriversData.GetDriverByPersonID(
                PersonID,
                ref driverID,
                ref createdByUserID,
                ref createdDate
            );

            if (isFound)
                return new clsDriver(
                    driverID,
                    PersonID,
                    createdByUserID,
                    createdDate
                );

            return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateDriver();

                default:
                    return false;
            }
        }

        public static bool Delete(int DriverID)
        {
            return clsDriversData.DeleteDriver(DriverID);
        }

        public static bool IsExist(int DriverID)
        {
            return clsDriversData.IsDriverExist(DriverID);
        }

        public static bool IsExistByPerson(int PersonID)
        {
            return clsDriversData.IsDriverExistByPersonID(PersonID);
        }

        public static DataTable GetAllDrivers()
        {
            return clsDriversData.GetAllDrivers();
        }
    }
}