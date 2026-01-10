using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_BusinessLayer
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {

        public int LocalDrivingLicenseApplicationID {  get; set; }
        public int LicenseClassID { get; set; }

        public clsLicenseClass LicenseClassInfo;

        private bool _AddNewLocalDrivingLicenseApplication()
        {
            LocalDrivingLicenseApplicationID = 
                clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication
                (ApplicationID, LicenseClassID);
            return (LocalDrivingLicenseApplicationID != -1);
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication
                (LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID);
        }

        public clsLocalDrivingLicenseApplication() : base()
        {
            LocalDrivingLicenseApplicationID = -1;
            LicenseClassID = -1;
            LicenseClassInfo = new clsLicenseClass();
        }

        private clsLocalDrivingLicenseApplication(int localDrivingLicenseApplicationID,
                                          int applicationID, int applicantPersonID,
                                          DateTime applicationDate, sbyte applicationTypeID,
                                          enApplicationStatus applicationStatus,
                                          DateTime lastStatusDate, decimal paidFees,
                                          int createdByUserID, int licenseClassID)
                                          : base(applicationID, applicantPersonID,
                                                 applicationDate, applicationTypeID,
                                                 applicationStatus, lastStatusDate,
                                                 paidFees, createdByUserID)
        {
            LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            LicenseClassID = licenseClassID;
            LicenseClassInfo = clsLicenseClass.Find(licenseClassID);
        }

        public static clsLocalDrivingLicenseApplication Find(int LocalDrivingLicenseApplicationID)
        {

            int ApplicationID = -1;
            int LicenseClassID = -1;

            bool isFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByID
                (LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID);

            if (isFound)
            {
                
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                return new clsLocalDrivingLicenseApplication(
                                                              LocalDrivingLicenseApplicationID,
                                                              Application.ApplicationID,
                                                              Application.ApplicantPersonID,
                                                              Application.ApplicationDate,
                                                              Application.ApplicationTypeID,
                                                              Application.ApplicationStatus,
                                                              Application.LastStatusDate,
                                                              Application.PaidFees,
                                                              Application.CreatedByUserID,
                                                              LicenseClassID
                                                             );

            }
            else
                return null;

        }

        public static clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {

            int LocalDrivingLicenseApplicationID = -1;
            int LicenseClassID = -1;

            bool isFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByApplicationID
                (ApplicationID, ref LocalDrivingLicenseApplicationID, ref LicenseClassID);

            if (isFound)
            {

                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                return new clsLocalDrivingLicenseApplication(
                                                              LocalDrivingLicenseApplicationID,
                                                              Application.ApplicationID,
                                                              Application.ApplicantPersonID,
                                                              Application.ApplicationDate,
                                                              Application.ApplicationTypeID,
                                                              Application.ApplicationStatus,
                                                              Application.LastStatusDate,
                                                              Application.PaidFees,
                                                              Application.CreatedByUserID,
                                                              LicenseClassID
                                                             );

            }
            else
                return null;

        }

        public static bool IsLocalDrivingLicenseApplicationExists(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.IsLocalDrivingLicenseApplicationExists(LocalDrivingLicenseApplicationID);
        }

        public static new bool Delete(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1;

            // Retrieve the ApplicationID associated with the given LocalDrivingLicenseApplicationID
            clsLocalDrivingLicenseApplicationData.GetApplicationID(LocalDrivingLicenseApplicationID, ref ApplicationID);

            // Delete the child record from LocalDrivingLicenseApplications table
            bool childDeleted = clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID);

            if (childDeleted)
                // If the child record was deleted successfully, delete the corresponding Application record
                return clsApplication.Delete(ApplicationID);

            // Return false if the child record could not be deleted
            return false;
        }

        public override bool Save()
        {

            enMode mode = Mode;

            if (base.Save())
            {               

                Mode = mode;

                switch (Mode)
                {

                    case enMode.AddNew:

                        if (_AddNewLocalDrivingLicenseApplication())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else
                            return false;
                    case enMode.Update:
                        return _UpdateLocalDrivingLicenseApplication();
                    default:
                        return false;

                }

            }
            else
                return false;

        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }

        public static bool GetApplicationID(int LocalDrivingLicenseApplicationID, ref int ApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.GetApplicationID(LocalDrivingLicenseApplicationID, ref ApplicationID);
        }

    }
}
