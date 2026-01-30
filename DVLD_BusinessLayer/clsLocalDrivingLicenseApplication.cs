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
                                          DateTime applicationDate, int applicationTypeID,
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

        public static int GetPassedTestsCount(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.GetPassedTestsCount
                (LocalDrivingLicenseApplicationID);
        }

        public int GetPassedTestsCount()
        {
            return clsLocalDrivingLicenseApplicationData.GetPassedTestsCount
                (this.LocalDrivingLicenseApplicationID);
        }

        public bool IsTestPassed(clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplicationData.IsTestPassed(this.LocalDrivingLicenseApplicationID, (byte)TestType);
        }

        public static bool IsTestPassed(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplicationData.IsTestPassed(LocalDrivingLicenseApplicationID, (byte)TestType);
        }

        public static bool HasActiveTestAppointment(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplicationData.HasActiveTestAppointment(LocalDrivingLicenseApplicationID, (byte)TestType);
        }

        public static int GetFailedTrialsCount(int localDrivingLicenseApplicationID, clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplicationData.GetFailedTrialsCount(localDrivingLicenseApplicationID, (byte)TestType);
        }

        public bool HasFailedTestType(clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplicationData.HasFailedTestType(this.LocalDrivingLicenseApplicationID, (byte)TestType);
        }

        public int IssueDrivingLicenseForTheFirstTime(string Notes, int CreatedByUserID)
        {

            int LicenseID = -1;

            // We check first if an existing driver record exists for this person
            clsDriver driver = clsDriver.FindByPersonID(this.ApplicantPersonID);

            if (driver == null)
            {
                driver = new clsDriver();

                driver.PersonID = this.ApplicantPersonID;
                driver.CreatedByUserID = CreatedByUserID;
                driver.CreatedDate = DateTime.Now;

                if (!driver.Save())
                    return -1;

            }


            clsLicense License = new clsLicense();

            License.ApplicationID = this.ApplicationID;
            License.DriverID = driver.DriverID;
            License.LicenseClassID = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = License.IssueDate.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License.Notes = Notes;
            License.PaidFees = this.LicenseClassInfo.ClassFees;
            License.IsActive = true;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID = CreatedByUserID;

            if (License.Save())
                {
                    this.SetComplete();
                    LicenseID = License.LicenseID;
                }

            return LicenseID;

        }

    }
}
