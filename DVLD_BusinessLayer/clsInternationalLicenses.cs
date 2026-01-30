using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_BusinessLayer
{
    public class clsInternationalLicense
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode { get; set; }

        public int InternationalLicenseID { get; set; }
        public int ApplicationID { get; set; }

        public clsApplication ApplicationInfo;

        public int DriverID { get; set; }

        public clsDriver DriverInfo;

        public int IssuedUsingLocalLicenseID { get; set; }

        public clsLicense LocalLicenseInfo;

        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public bool IsActive { get; set; }

        public int CreatedByUserID { get; set; }

        public clsUser CreatedByUserInfo;


        public clsInternationalLicense()
        {
            InternationalLicenseID = -1;
            ApplicationID = -1;
            DriverID = -1;
            IssuedUsingLocalLicenseID = -1;

            ApplicationInfo = new clsApplication();
            DriverInfo = new clsDriver();
            LocalLicenseInfo = new clsLicense();
            CreatedByUserInfo = new clsUser();

            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;

            IsActive = true;
            CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        private clsInternationalLicense(int internationalLicenseID,
                                        int applicationID,
                                        int driverID,
                                        int issuedUsingLocalLicenseID,
                                        DateTime issueDate,
                                        DateTime expirationDate,
                                        bool isActive,
                                        int createdByUserID)
        {
            InternationalLicenseID = internationalLicenseID;
            ApplicationID = applicationID;
            DriverID = driverID;
            IssuedUsingLocalLicenseID = issuedUsingLocalLicenseID;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            IsActive = isActive;
            CreatedByUserID = createdByUserID;

            // Composition loading goes here
            ApplicationInfo = clsApplication.FindBaseApplication(applicationID);
            DriverInfo = clsDriver.Find(driverID);
            LocalLicenseInfo = clsLicense.Find(issuedUsingLocalLicenseID);
            CreatedByUserInfo = clsUser.Find(createdByUserID);

            Mode = enMode.Update;
        }

        // -----------------------------------------
        // Private DAL Wrappers
        // -----------------------------------------

        private bool _AddNewInternationalLicense()
        {
            InternationalLicenseID =
                clsInternationalLicensesData.AddNewInternationalLicense(
                    ApplicationID,
                    DriverID,
                    IssuedUsingLocalLicenseID,
                    IssueDate,
                    ExpirationDate,
                    IsActive,
                    CreatedByUserID
                );

            return (InternationalLicenseID != -1);
        }

        private bool _UpdateInternationalLicense()
        {
            return clsInternationalLicensesData.UpdateInternationalLicense(
                InternationalLicenseID,
                ApplicationID,
                DriverID,
                IssuedUsingLocalLicenseID,
                IssueDate,
                ExpirationDate,
                IsActive,
                CreatedByUserID
            );
        }

        // -----------------------------------------
        // Find Methods
        // -----------------------------------------

        public static clsInternationalLicense Find(int InternationalLicenseID)
        {
            int applicationID = -1;
            int driverID = -1;
            int issuedUsingLocalLicenseID = -1;
            DateTime issueDate = DateTime.Now;
            DateTime expirationDate = DateTime.Now;
            bool isActive = true;
            int createdByUserID = -1;

            bool isFound =
                clsInternationalLicensesData.GetInternationalLicenseByID(
                    InternationalLicenseID,
                    ref applicationID,
                    ref driverID,
                    ref issuedUsingLocalLicenseID,
                    ref issueDate,
                    ref expirationDate,
                    ref isActive,
                    ref createdByUserID
                );

            if (!isFound)
                return null;

            return new clsInternationalLicense(
                InternationalLicenseID,
                applicationID,
                driverID,
                issuedUsingLocalLicenseID,
                issueDate,
                expirationDate,
                isActive,
                createdByUserID
            );
        }

        public static clsInternationalLicense FindByLocalLicenseID(int LocalLicenseID)
        {
            int internationalLicenseID = -1;
            int applicationID = -1;
            int driverID = -1;
            int issuedUsingLocalLicenseID = -1;
            DateTime issueDate = DateTime.Now;
            DateTime expirationDate = DateTime.Now;
            bool isActive = true;
            int createdByUserID = -1;

            bool isFound =
                clsInternationalLicensesData.GetInternationalLicenseByLocalLicenseID(
                    LocalLicenseID,
                    ref internationalLicenseID,
                    ref applicationID,
                    ref driverID,
                    ref issueDate,
                    ref expirationDate,
                    ref isActive,
                    ref createdByUserID
                );

            if (!isFound)
                return null;

            return new clsInternationalLicense(
                internationalLicenseID,
                applicationID,
                driverID,
                issuedUsingLocalLicenseID,
                issueDate,
                expirationDate,
                isActive,
                createdByUserID
            );
        }


        // -----------------------------------------
        // Save Pattern
        // -----------------------------------------

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInternationalLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateInternationalLicense();

                default:
                    return false;
            }
        }

        // -----------------------------------------
        // Static Helpers
        // -----------------------------------------

        public static bool Delete(int InternationalLicenseID)
        {
            return clsInternationalLicensesData.DeleteInternationalLicense(
                InternationalLicenseID);
        }

        public static bool IsInternationalLicenseExist(int InternationalLicenseID)
        {
            return clsInternationalLicensesData
                   .IsInternationalLicenseExist(InternationalLicenseID);
        }

        public static bool HasActiveInternationalLicenseForLocalLicenseID(int LocalLicenseID)
        {
            return clsInternationalLicensesData.HasActiveInternationalLicenseForLocalLicenseID(LocalLicenseID);
        }

        public static DataTable GetAllInternationalLicensesByDriverID(int DriverID)
        {
            return clsInternationalLicensesData.GetAllInternationalLicensesByDriverID(DriverID);
        }

    }
}