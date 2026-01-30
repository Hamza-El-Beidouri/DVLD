using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_BusinessLayer
{
    public class clsLicense
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode { get; set; }

        public enum enIssueReason : byte { FirstTime = 1, Renew = 2, ReplacementForDamaged = 3,
                                           ReplacementForLost = 4 }

        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public clsApplication ApplicationInfo;
        public int DriverID { get; set; }
        public clsDriver DriverInfo;           // Link to Driver BLL
        public int LicenseClassID { get; set; }
        public clsLicenseClass LicenseClassInfo;
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsActive { get; set; }
        public enIssueReason IssueReason { get; set; }

        public string IssueReasonText
        {
            get
            {
                switch (IssueReason)
                {
                    case enIssueReason.FirstTime:
                        return "First Time";

                    case enIssueReason.Renew:
                        return "Renew";

                    case enIssueReason.ReplacementForDamaged:
                        return "Replacement for damaged";

                    case enIssueReason.ReplacementForLost:
                        return "Replacement for lost";

                    default:
                        return "Unkown";
                }
            }
        }

        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo;

        public clsLicense()
        {
            LicenseID = -1;
            ApplicationID = -1;
            ApplicationInfo = new clsApplication();
            DriverID = -1;
            DriverInfo = new clsDriver();
            LicenseClassID = -1;
            LicenseClassInfo = new clsLicenseClass();
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            Notes = "";
            PaidFees = 0;
            IsActive = true;
            IssueReason = enIssueReason.FirstTime;
            CreatedByUserID = -1;
            CreatedByUserInfo = new clsUser();

            Mode = enMode.AddNew;
        }

        private clsLicense(int licenseID, int applicationID, int driverID, int licenseClassID,
            DateTime issueDate, DateTime expirationDate, string notes, decimal paidFees,
            bool isActive, enIssueReason issueReason, int createdByUserID)
        {
            LicenseID = licenseID;
            ApplicationID = applicationID;
            ApplicationInfo = clsApplication.FindBaseApplication(applicationID);
            DriverID = driverID;
            DriverInfo = clsDriver.Find(driverID);
            LicenseClassID = licenseClassID;
            LicenseClassInfo = clsLicenseClass.Find(licenseClassID);
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            Notes = notes;
            PaidFees = paidFees;
            IsActive = isActive;
            IssueReason = issueReason;
            CreatedByUserID = createdByUserID;
            CreatedByUserInfo = clsUser.Find(createdByUserID);

            Mode = enMode.Update;
        }

        private bool _AddNewLicense()
        {
            LicenseID = clsLicensesData.AddNewLicense(
                ApplicationID,
                DriverID,
                LicenseClassID,
                IssueDate,
                ExpirationDate,
                Notes,
                PaidFees,
                IsActive,
                (byte)IssueReason,
                CreatedByUserID
            );

            return (LicenseID != -1);
        }

        private bool _UpdateLicense()
        {
            return clsLicensesData.UpdateLicense(
                LicenseID,
                ApplicationID,
                DriverID,
                LicenseClassID,
                IssueDate,
                ExpirationDate,
                Notes,
                PaidFees,
                IsActive,
                (byte)IssueReason,
                CreatedByUserID
            );
        }

        public static clsLicense Find(int LicenseID)
        {
            int applicationID = -1;
            int driverID = -1;
            int licenseClassID = -1;
            DateTime issueDate = DateTime.Now;
            DateTime expirationDate = DateTime.Now;
            string notes = "";
            decimal paidFees = 0;
            bool isActive = true;
            byte issueReasonByte = 0;
            int createdByUserID = -1;

            bool isFound = clsLicensesData.GetLicenseByID(
                LicenseID,
                ref applicationID,
                ref driverID,
                ref licenseClassID,
                ref issueDate,
                ref expirationDate,
                ref notes,
                ref paidFees,
                ref isActive,
                ref issueReasonByte,
                ref createdByUserID
            );

            if (!isFound)
                return null;

            // Convert DAL byte → Business enum
            enIssueReason issueReason = (enIssueReason)issueReasonByte;

            return new clsLicense(
                                  LicenseID,
                                  applicationID,
                                  driverID,
                                  licenseClassID,
                                  issueDate,
                                  expirationDate,
                                  notes,
                                  paidFees,
                                  isActive,
                                  issueReason,
                                  createdByUserID
                                  );

        }

        public static clsLicense FindByApplicationID(int ApplicationID)
        {
            int licenseID = -1;
            int driverID = -1;
            int licenseClassID = -1;
            DateTime issueDate = DateTime.Now;
            DateTime expirationDate = DateTime.Now;
            string notes = "";
            decimal paidFees = 0;
            bool isActive = true;
            byte issueReasonByte = 0;
            int createdByUserID = -1;

            bool isFound = clsLicensesData.GetLicenseByApplicationID(
                ApplicationID,
                ref licenseID,
                ref driverID,
                ref licenseClassID,
                ref issueDate,
                ref expirationDate,
                ref notes,
                ref paidFees,
                ref isActive,
                ref issueReasonByte,
                ref createdByUserID
            );

            if (!isFound)
                return null;

            // Convert DAL byte → Business enum
            enIssueReason issueReason = (enIssueReason)issueReasonByte;

            return new clsLicense(
                                  licenseID,
                                  ApplicationID,
                                  driverID,
                                  licenseClassID,
                                  issueDate,
                                  expirationDate,
                                  notes,
                                  paidFees,
                                  isActive,
                                  issueReason,
                                  createdByUserID
                                  );
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateLicense();

                default:
                    return false;
            }
        }

        public static bool Delete(int LicenseID)
        {
            return clsLicensesData.DeleteLicense(LicenseID);
        }

        public static bool IsLicenseExist(int LicenseID)
        {
            return clsLicensesData.IsLicenseExist(LicenseID);
        }

        public static bool IsLicenseExist(int PersonID, int LicenseClassID)
        {
            return clsLicensesData.IsLicenseExist(PersonID, LicenseClassID);
        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsLicensesData.IsLicenseDetained(LicenseID);
        }

        public bool IsLicenseDetained()
        {
            return clsLicensesData.IsLicenseDetained(this.LicenseID);
        }

        public static DataTable GetLicensesByDriverID(int DriverID)
        {
            return clsLicensesData.GetLicensesByDriverID(DriverID);
        }

    }
}