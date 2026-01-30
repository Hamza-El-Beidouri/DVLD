using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_BusinessLayer
{
    public class clsTestAppointment
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode { get; set; }

        public int TestAppointmentID { get; set; }
        public int TestTypeID { get; set; }
        public clsTestType TestTypeInfo;
        public int LocalDrivingLicenseApplicationID { get; set; }
        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplicationInfo;
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }
        public clsApplication RetakeTestApplicationInfo;

        public clsTestAppointment()
        {
            TestAppointmentID = -1;
            TestTypeID = -1;
            TestTypeInfo = new clsTestType();
            LocalDrivingLicenseApplicationID = -1;
            LocalDrivingLicenseApplicationInfo = new clsLocalDrivingLicenseApplication();
            AppointmentDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = -1;
            IsLocked = false;
            RetakeTestApplicationID = -1;
            RetakeTestApplicationInfo = new clsApplication();

            Mode = enMode.AddNew;
        }

        private clsTestAppointment(
            int testAppointmentID,
            int testTypeID,
            int localDrivingLicenseApplicationID,
            DateTime appointmentDate,
            decimal paidFees,
            int createdByUserID,
            bool isLocked,
            int retakeTestApplicationID)
        {
            TestAppointmentID = testAppointmentID;
            TestTypeID = testTypeID;
            TestTypeInfo = clsTestType.Find((clsTestType.enTestType)testTypeID);
            LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.Find(localDrivingLicenseApplicationID);
            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
            IsLocked = isLocked;
            RetakeTestApplicationID = retakeTestApplicationID;
            RetakeTestApplicationInfo = clsApplication.FindBaseApplication(retakeTestApplicationID);

            Mode = enMode.Update;
        }

        private bool _AddNewTestAppointment()
        {
            TestAppointmentID =
                clsTestAppointmentsData.AddTestAppointment(
                    TestTypeID,
                    LocalDrivingLicenseApplicationID,
                    AppointmentDate,
                    PaidFees,
                    CreatedByUserID,
                    IsLocked,
                    RetakeTestApplicationID
                );

            return (TestAppointmentID != -1);
        }

        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentsData.UpdateTestAppointment(
                TestAppointmentID,
                TestTypeID,
                LocalDrivingLicenseApplicationID,
                AppointmentDate,
                PaidFees,
                CreatedByUserID,
                IsLocked,
                RetakeTestApplicationID
            );
        }

        public static clsTestAppointment Find(int TestAppointmentID)
        {
            int TestTypeID = -1;
            int LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            decimal PaidFees = 0;
            int CreatedByUserID = -1;
            bool IsLocked = false;
            int RetakeTestApplicationID = -1;

            bool isFound =
                clsTestAppointmentsData.GetTestAppointmentByID(
                    TestAppointmentID,
                    ref TestTypeID,
                    ref LocalDrivingLicenseApplicationID,
                    ref AppointmentDate,
                    ref PaidFees,
                    ref CreatedByUserID,
                    ref IsLocked,
                    ref RetakeTestApplicationID
                );

            if (isFound)
                return new clsTestAppointment(
                TestAppointmentID,
                TestTypeID,
                LocalDrivingLicenseApplicationID,
                AppointmentDate,
                PaidFees,
                CreatedByUserID,
                IsLocked,
                RetakeTestApplicationID
            );

            return null;


        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateTestAppointment();

                default:
                    return false;
            }
        }

        public static bool Delete(int TestAppointmentID)
        {
            return clsTestAppointmentsData.DeleteTestAppointment(TestAppointmentID);
        }

        public static DataTable GetTestAppointments(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType)
        {
            return clsTestAppointmentsData.GetTestAppointments(LocalDrivingLicenseApplicationID, (byte)TestType);
        }

    }
}