using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_BusinessLayer
{
    public class clsApplication
    {

        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode;

        public enum enApplicationType : byte
        {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        };

        public enum enApplicationStatus : byte { New = 1, Cancelled = 2, Completed = 3 };

        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public clsPerson PersonInfo;
        public DateTime ApplicationDate { get; set; }
        public sbyte ApplicationTypeID { get; set; }
        public clsApplicationType ApplicationTypeInfo;
        public enApplicationStatus ApplicationStatus {  get; set; }
        public string StatusText
        {
            get
            {
                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unkown";
                }
            }
        }
        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo;

        private bool _AddNewApplicationRecord()
        {
            //call DataAccess Layer 

            this.ApplicationID = clsApplicationsData.AddNewApplicationRecord(
                this.ApplicantPersonID, this.ApplicationDate,
                this.ApplicationTypeID, (byte)this.ApplicationStatus,
                this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

            return (this.ApplicationID != -1);
        }

        private bool _UpdateApplicationRecord()
        {
            return clsApplicationsData.UpdateApplicationRecord(ApplicationID, ApplicantPersonID, 
                ApplicationDate, ApplicationTypeID, (byte)ApplicationStatus, LastStatusDate, 
                PaidFees, CreatedByUserID);
        }

        public clsApplication()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.PersonInfo = new clsPerson();
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationTypeInfo = new clsApplicationType();
            this.ApplicationStatus = enApplicationStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.CreatedByUserInfo = new clsUser();
            this.Mode = enMode.AddNew;
        }

        protected clsApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate,
                                sbyte ApplicationTypeID, enApplicationStatus ApplicationStatus,
                                DateTime LastStatusDate,
                                decimal PaidFees, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.PersonInfo = clsPerson.Find(this.ApplicantPersonID);
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeInfo = clsApplicationType.Find(ApplicationTypeID);
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindByPersonID(ApplicantPersonID);
            this.Mode = enMode.Update;
        }

        public static clsApplication FindBaseApplication(int ApplicationID)
        {

            int ApplicantPersonID = -1, CreatedByUserID = -1;
            sbyte ApplicationTypeID = -1;
            byte ApplicationStatus = 1;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            decimal PaidFees = 0;

            if (clsApplicationsData.GetApplicationByID(ApplicationID, ref ApplicantPersonID, ref ApplicationDate, ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreatedByUserID))
                return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, (enApplicationStatus)ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
            else
                return null;

        }

        public virtual bool Save()
        {

            switch (Mode)
            {

                case enMode.AddNew:

                    if (_AddNewApplicationRecord())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                            return _UpdateApplicationRecord();

            }

            return false;

        }

        public static bool Cancel(int ApplicationID)
        {
            return clsApplicationsData.UpdateStatus(ApplicationID, 
               (byte)enApplicationStatus.Cancelled);
        }

        public static bool SetComplete(int ApplicationID)
        {
            return clsApplicationsData.UpdateStatus(ApplicationID,
                (byte)enApplicationStatus.Completed);
        }

        public static bool Delete(int ApplicationID)
        {
            return clsApplicationsData.DeleteApplicationRecord(ApplicationID);
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            return clsApplicationsData.IsApplicationExist(ApplicationID);
        }

        public static DataTable GetAllApplications()
        {
            return clsApplicationsData.GetAllApplications();
        }

        public static int GetActiveApplicationID(int PersonID, enApplicationType ApplicationType)
        {
            return clsApplicationsData.GetActiveApplicationID(PersonID, (byte)ApplicationType);
        }

        public int GetActiveApplicationID(enApplicationType ApplicationType)
        {
            return GetActiveApplicationID(this.ApplicantPersonID, ApplicationType);
        }

        public static bool DoesPersonHaveActiveApplication(int PersonID, enApplicationType ApplicationType)
        {
            return clsApplicationsData.DoesPersonHaveActiveApplication(PersonID, (byte)ApplicationType);
        }

        public bool DoesPersonHaveActiveApplication(enApplicationType ApplicationType)
        {
            return clsApplicationsData.DoesPersonHaveActiveApplication(this.ApplicantPersonID, (byte)ApplicationType);
        }

        public static int GetActiveApplicationIdByLicenseClass(int PersonID, enApplicationType ApplicationType, int LicenseClassID)
        {
            return clsApplicationsData.GetActiveApplicationIdByLicenseClass(PersonID, (int)ApplicationType, LicenseClassID);
        }

    }
}