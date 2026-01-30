using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_BusinessLayer
{
    public class clsTest
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode { get; set; }

        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public clsTestAppointment TestAppointmentInfo;
        public bool TestResult { get; set; }   // CHANGED
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo;

        public clsTest()
        {
            TestID = -1;
            TestAppointmentID = -1;
            TestAppointmentInfo = new clsTestAppointment();
            TestResult = false;   // CHANGED
            Notes = "";
            CreatedByUserID = -1;
            CreatedByUserInfo = new clsUser();
            Mode = enMode.AddNew;
        }

        private clsTest(
            int testID,
            int testAppointmentID,
            bool testResult,   // CHANGED
            string notes,
            int createdByUserID)
        {
            TestID = testID;
            TestAppointmentID = testAppointmentID;
            TestAppointmentInfo = clsTestAppointment.Find(testAppointmentID);
            TestResult = testResult;
            Notes = notes;
            CreatedByUserID = createdByUserID;
            CreatedByUserInfo = clsUser.Find(createdByUserID);
            Mode = enMode.Update;
        }

        private bool _AddNewTest()
        {
            TestID = clsTestsData.AddTest(
                TestAppointmentID,
                TestResult,    // CHANGED
                Notes,
                CreatedByUserID
            );

            return (TestID != -1);
        }

        private bool _UpdateTest()
        {
            return clsTestsData.UpdateTest(
                TestID,
                TestAppointmentID,
                TestResult,    // CHANGED
                Notes,
                CreatedByUserID
            );
        }

        public static clsTest Find(int TestID)
        {
            int testAppointmentID = -1;
            bool testResult = false;   // CHANGED
            string notes = "";
            int createdByUserID = -1;

            bool isFound = clsTestsData.GetTestByID(
                TestID,
                ref testAppointmentID,
                ref testResult,   // CHANGED
                ref notes,
                ref createdByUserID
            );

            if (isFound)
                return new clsTest(
                    TestID,
                    testAppointmentID,
                    testResult,   // CHANGED
                    notes,
                    createdByUserID
                );

            return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateTest();

                default:
                    return false;
            }
        }

        public static bool Delete(int TestID)
        {
            return clsTestsData.DeleteTest(TestID);
        }

        public static DataTable GetAllTests()
        {
            return clsTestsData.GetAllTests();
        }
    }
}