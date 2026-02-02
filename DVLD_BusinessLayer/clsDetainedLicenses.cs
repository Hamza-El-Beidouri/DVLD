using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_BusinessLayer
{
    public class clsDetainedLicense
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode { get; set; }

        public int DetainID { get; set; }

        public int LicenseID { get; set; }
        public clsLicense LicenseInfo;   // Composition

        public DateTime DetainDate { get; set; }

        public decimal FineFees { get; set; }

        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo; // Composition

        public bool IsReleased { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int ReleasedByUserID { get; set; }
        public clsUser ReleasedByUserInfo; // Composition

        public int ReleaseApplicationID { get; set; }
        public clsApplication ReleaseApplicationInfo; // Composition

        // =========================
        // DEFAULT CONSTRUCTOR
        // =========================
        public clsDetainedLicense()
        {
            DetainID = -1;

            LicenseID = -1;
            LicenseInfo = new clsLicense();

            DetainDate = DateTime.Now;
            FineFees = 0;

            CreatedByUserID = -1;
            CreatedByUserInfo = new clsUser();

            IsReleased = false;

            ReleaseDate = DateTime.MinValue;

            ReleasedByUserID = -1;
            ReleasedByUserInfo = new clsUser();

            ReleaseApplicationID = -1;
            ReleaseApplicationInfo = new clsApplication();

            Mode = enMode.AddNew;
        }

        // =========================
        // PRIVATE CONSTRUCTOR (Find)
        // =========================
        private clsDetainedLicense(int detainID, int licenseID, DateTime detainDate,
            decimal fineFees, int createdByUserID, bool isReleased,
            DateTime releaseDate, int releasedByUserID, int releaseApplicationID)
        {
            DetainID = detainID;

            LicenseID = licenseID;
            LicenseInfo = clsLicense.Find(licenseID);

            DetainDate = detainDate;
            FineFees = fineFees;

            CreatedByUserID = createdByUserID;
            CreatedByUserInfo = clsUser.Find(createdByUserID);

            IsReleased = isReleased;

            ReleaseDate = releaseDate;

            ReleasedByUserID = releasedByUserID;
            ReleasedByUserInfo = releasedByUserID != -1 ? clsUser.Find(releasedByUserID) : null;

            ReleaseApplicationID = releaseApplicationID;
            ReleaseApplicationInfo = releaseApplicationID != -1 ?
                                     clsApplication.FindBaseApplication(releaseApplicationID) : null;

            Mode = enMode.Update;
        }

        // =========================
        // ADD NEW
        // =========================
        private bool _AddNewDetain()
        {
            DetainID = clsDetainedLicensesData.AddNewDetainedLicense(
                LicenseID,
                DetainDate,
                FineFees,
                CreatedByUserID
            );

            return (DetainID != -1);
        }

        // =========================
        // RELEASE UPDATE
        // =========================
        private bool _Release()
        {
            return clsDetainedLicensesData.ReleaseDetainedLicense(
                DetainID,
                ReleaseDate,
                ReleasedByUserID,
                ReleaseApplicationID
            );
        }

        // =========================
        // SAVE
        // =========================
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetain())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _Release();

                default:
                    return false;
            }
        }

        // =========================
        // FIND BY ID
        // =========================
        public static clsDetainedLicense Find(int DetainID)
        {
            int licenseID = -1;
            DateTime detainDate = DateTime.Now;
            decimal fineFees = 0;
            int createdByUserID = -1;
            bool isReleased = false;
            DateTime releaseDate = DateTime.MinValue;
            int releasedByUserID = -1;
            int releaseApplicationID = -1;

            bool isFound = clsDetainedLicensesData.GetDetainedLicenseByID(
                DetainID,
                ref licenseID,
                ref detainDate,
                ref fineFees,
                ref createdByUserID,
                ref isReleased,
                ref releaseDate,
                ref releasedByUserID,
                ref releaseApplicationID
            );

            if (!isFound)
                return null;

            return new clsDetainedLicense(
                DetainID,
                licenseID,
                detainDate,
                fineFees,
                createdByUserID,
                isReleased,
                releaseDate,
                releasedByUserID,
                releaseApplicationID
            );
        }

        public static clsDetainedLicense FindByLicenseID(int LicenseID)
        {
            int detainID = -1;
            DateTime detainDate = DateTime.Now;
            decimal fineFees = 0;
            int createdByUserID = -1;
            bool isReleased = false;
            DateTime releaseDate = DateTime.MinValue;
            int releasedByUserID = -1;
            int releaseApplicationID = -1;

            bool isFound = clsDetainedLicensesData.GetDetainedLicenseByLicenseID(
                LicenseID,
                ref detainID,
                ref detainDate,
                ref fineFees,
                ref createdByUserID,
                ref isReleased,
                ref releaseDate,
                ref releasedByUserID,
                ref releaseApplicationID
            );

            if (!isFound)
                return null;

            return new clsDetainedLicense(
                detainID,
                LicenseID,
                detainDate,
                fineFees,
                createdByUserID,
                isReleased,
                releaseDate,
                releasedByUserID,
                releaseApplicationID
            );
        }


        // =========================
        // DELETE (ADMIN)
        // =========================
        public static bool Delete(int DetainID)
        {
            return clsDetainedLicensesData.DeleteDetainedLicense(DetainID);
        }

        // =========================
        // CHECK IF LICENSE DETAINED
        // =========================
        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainedLicensesData.IsLicenseDetained(LicenseID);
        }

        public bool IsLicenseDetained()
        {
            return clsDetainedLicensesData.IsLicenseDetained(this.LicenseID);
        }

        // =========================
        // GET ALL
        // =========================
        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicensesData.GetAllDetainedLicenses();
        }

    }
}