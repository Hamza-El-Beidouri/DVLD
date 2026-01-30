using DVLD.Global_Classes;
using DVLD.Properties;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.TestAppointments.Controls
{
    public partial class ctrlScheduleTest : UserControl
    {

        enum enMode { AddNew = 0, Update = 1 }
        enMode Mode;

        private clsTestType.enTestType _TestType;
        private decimal _TestTypeFees = 0;
        private decimal _RetakeTestFees = 0;
        private int _TestAppointmentID;
        private int _LocalDrivingLicenseApplicationID;
        private clsTestAppointment _TestAppointment;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        private void _ResetDefaultValues()
        {
            lblLocalDrivingLicenseAppID.Text = "[????]";
            lblLicenseClass.Text = "[????]";
            lblApplicantName.Text = "[????]";
            lblTrial.Text = "[????]";
            dtpTestAppointment.Value = DateTime.Today;
            lblFees.Text = "[????]";
            lblRetakeTestApplicationID.Text = "N/A";
            lblRetakeTestApplicationFees.Text = "[????]";
            lblTotalFees.Text = "[????]";
            gbRetakeTestInfo.Enabled = false;
        }

        private void _ConfigureTestTypeUI(clsTestType.enTestType testType)
        {

            switch (testType)
            {

                case clsTestType.enTestType.VisionTest:
                    this.Text = "Schedule Vision Test";
                    lblTitle.Text = this.Text;
                    pbTestTypeImage.Image = Resources.VisionTest;
                    break;

                case clsTestType.enTestType.WrittenTest:
                    this.Text = "Schedule Theory Test";
                    lblTitle.Text = this.Text;
                    pbTestTypeImage.Image = Resources.TheoryTest;
                    break;

                case clsTestType.enTestType.StreetTest:
                    this.Text = "Schedule Driving Test";
                    lblTitle.Text = this.Text;
                    pbTestTypeImage.Image = Resources.DrivingTest;
                    break;

            }

        }

        private void _InitializeAddModeUI()
        {

            _TestTypeFees = clsTestType.GetTestTypeFees(_TestType);

            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblLicenseClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblApplicantName.Text = _LocalDrivingLicenseApplication.ApplicantInfo.FullName;
            lblTrial.Text = clsLocalDrivingLicenseApplication.GetFailedTrialsCount(_LocalDrivingLicenseApplicationID, _TestType).ToString();
            dtpTestAppointment.MinDate = DateTime.Now;
            lblFees.Text = _TestTypeFees.ToString();
            
            if (_LocalDrivingLicenseApplication.HasFailedTestType(_TestType))
            {
                _RetakeTestFees = clsApplicationType.GetFees(clsApplication.enApplicationType.RetakeTest);
                // To Re-enable
                gbRetakeTestInfo.Enabled = true;
                gbRetakeTestInfo.CustomBorderColor = Color.FromArgb(233, 93, 53); // Your Orange
                gbRetakeTestInfo.ForeColor = Color.White;
                gbRetakeTestInfo.BorderColor = Color.FromArgb(213, 218, 223);
                lblRetakeTestApplicationFees.Text = _RetakeTestFees.ToString();
                lblTotalFees.Text = (_TestTypeFees + _RetakeTestFees).ToString();
            }
            else
            {
                // To Disable and make it LOOK disabled
                gbRetakeTestInfo.Enabled = false;
                gbRetakeTestInfo.CustomBorderColor = Color.Silver; // Dims the header orange
                gbRetakeTestInfo.ForeColor = Color.Gray;         // Dims the header text
                gbRetakeTestInfo.BorderColor = Color.LightGray;   // Dims the outer border
                lblRetakeTestApplicationFees.Text = "0";
                lblTotalFees.Text = _TestTypeFees.ToString();
            }

            lblRetakeTestApplicationID.Text = "N/A";

        }

        private void _ConfigureAddMode()
        {

            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.Find(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                _ResetDefaultValues();
                MessageBox.Show("No license application with ID = " + _LocalDrivingLicenseApplicationID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _InitializeAddModeUI();

        }

        private void _LoadAppointmentDetails()
        {
            _TestTypeFees = clsTestType.GetTestTypeFees(_TestType);

            lblLocalDrivingLicenseAppID.Text = _TestAppointment.LocalDrivingLicenseApplicationInfo.LocalDrivingLicenseApplicationID.ToString();
            lblLicenseClass.Text = _TestAppointment.LocalDrivingLicenseApplicationInfo.LicenseClassInfo.ClassName;
            lblApplicantName.Text = _TestAppointment.LocalDrivingLicenseApplicationInfo.ApplicantInfo.FullName;
            lblTrial.Text = clsLocalDrivingLicenseApplication.GetFailedTrialsCount(_TestAppointment.LocalDrivingLicenseApplicationID, _TestType).ToString();
            dtpTestAppointment.Value = _TestAppointment.AppointmentDate;
            dtpTestAppointment.MinDate = DateTime.Today;
            lblFees.Text = _TestTypeFees.ToString();

            if (_TestAppointment.RetakeTestApplicationID != -1)
            {

                _RetakeTestFees = clsApplicationType.GetFees(clsApplication.enApplicationType.RetakeTest);

                // To Re-enable
                gbRetakeTestInfo.Enabled = true;
                gbRetakeTestInfo.CustomBorderColor = Color.FromArgb(233, 93, 53); // Your Orange
                gbRetakeTestInfo.ForeColor = Color.White;
                gbRetakeTestInfo.BorderColor = Color.FromArgb(213, 218, 223);
                lblRetakeTestApplicationID.Text = _TestAppointment.RetakeTestApplicationInfo.ApplicationID.ToString();
                lblRetakeTestApplicationFees.Text = _RetakeTestFees.ToString();
                lblTotalFees.Text = (_TestTypeFees + _RetakeTestFees).ToString();
            }
            else
            {
                // To Disable and make it LOOK disabled
                gbRetakeTestInfo.Enabled = false;
                gbRetakeTestInfo.CustomBorderColor = Color.Silver; // Dims the header orange
                gbRetakeTestInfo.ForeColor = Color.Gray;         // Dims the header text
                gbRetakeTestInfo.BorderColor = Color.LightGray;   // Dims the outer border
                lblRetakeTestApplicationID.Text = "N/A";
                lblRetakeTestApplicationFees.Text = "0";
                lblTotalFees.Text = _TestTypeFees.ToString();
            }

            if (_TestAppointment.IsLocked)
            {
                dtpTestAppointment.Enabled = false;
                btnSave.Enabled = false;
                lblLockedTitle.Visible = true;
            }

        }

        private void _ConfigureUpdateMode()
        {

            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);

            if (_TestAppointment == null)
            {
                _ResetDefaultValues();
                MessageBox.Show("No test appointment with ID = " + _TestAppointmentID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LoadAppointmentDetails();

        }

        private void _ConfigureUIMode()
        {

            _ConfigureTestTypeUI(_TestType);

            switch (Mode)
            {

                case enMode.AddNew:
                    _TestAppointment = new clsTestAppointment();
                    _ConfigureAddMode();
                    break;

                case enMode.Update:
                    _ConfigureUpdateMode();
                    break;

            }

        }

        public void LoadTestTypeInfo(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType , int TestAppointmentID)
        {

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestType = TestType;
            _TestAppointmentID = TestAppointmentID;

            Mode = (_TestAppointmentID == -1) ? enMode.AddNew : enMode.Update;
            _ConfigureUIMode();
        }

        private void _HandleRetakeTestApplicationField()
        {

            if (Mode == enMode.AddNew)
            {

                if (gbRetakeTestInfo.Enabled)
                {

                    clsApplication RetakeTestApplication = new clsApplication();

                    RetakeTestApplication.ApplicantPersonID = _LocalDrivingLicenseApplication.ApplicantPersonID;
                    RetakeTestApplication.ApplicationDate = dtpTestAppointment.Value;
                    RetakeTestApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                    RetakeTestApplication.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                    RetakeTestApplication.LastStatusDate = dtpTestAppointment.Value;
                    RetakeTestApplication.PaidFees = clsApplicationType.GetFees(clsApplication.enApplicationType.RetakeTest);
                    RetakeTestApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                    bool isSaved = RetakeTestApplication.Save();

                    if (isSaved)
                        _TestAppointment.RetakeTestApplicationID = RetakeTestApplication.ApplicationID;
                    else
                        MessageBox.Show(
                                        "Failed to save the retake application record.",
                                        "Save Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error
                                        );

                }

            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            _TestAppointment.TestTypeID = (byte)_TestType;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;
            _TestAppointment.AppointmentDate = dtpTestAppointment.Value;
            _TestAppointment.PaidFees = _TestTypeFees + _RetakeTestFees;
            _TestAppointment.IsLocked = false;
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _HandleRetakeTestApplicationField();

            if (_TestAppointment.Save())
            {
                MessageBox.Show(
                                "Test appointment saved successfully.",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                                );
                lblRetakeTestApplicationID.Text = (_TestAppointment.RetakeTestApplicationID == -1) ? "N/A" : _TestAppointment.RetakeTestApplicationID.ToString();
            }
            else
                MessageBox.Show(
                                "Failed to save the test appointment.",
                                "Save Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                                );

        }
    }
}