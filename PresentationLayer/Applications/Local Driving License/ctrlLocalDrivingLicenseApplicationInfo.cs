using DVLD.People;
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

namespace DVLD.Applications.Local_Driving_License
{
    public partial class ctrlLocalDrivingLicenseApplicationInfo : UserControl
    {

        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApp;

        public ctrlLocalDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        private void _ResetDefaultValues()
        {
            lblLocalDrivingLicenseAppID.Text = "[????]";
            lblLicenseClass.Text = "[????]";
            lblPassedTests.Text = "[????]";
            lblApplicationID.Text = "[????]";
            lblStatus.Text = "[????]";
            lblFees.Text = "[????]";
            lblType.Text = "[????]";
            lblApplicantName.Text = "[????]";
            lblApplicationDate.Text = "[????]";
            lblLastStatusDate.Text = "[????]";
            lblCreatedByUser.Text = "[????]";
        }

        private void _FillLicenseInfo()
        {
            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApp.LocalDrivingLicenseApplicationID.ToString();
            lblLicenseClass.Text = _LocalDrivingLicenseApp.LicenseClassInfo.ClassName;
            lblPassedTests.Text = $"{_LocalDrivingLicenseApp.GetPassedTestsCount()}/3";
            lblApplicationID.Text = _LocalDrivingLicenseApp.ApplicationID.ToString();
            lblStatus.Text = _LocalDrivingLicenseApp.StatusText;
            lblFees.Text = _LocalDrivingLicenseApp.ApplicationTypeInfo.Fees.ToString();
            lblType.Text = _LocalDrivingLicenseApp.ApplicationTypeInfo.Title;
            lblApplicantName.Text = _LocalDrivingLicenseApp.ApplicantInfo.FullName;
            lblApplicationDate.Text = _LocalDrivingLicenseApp.ApplicationDate.ToString();
            lblLastStatusDate.Text = _LocalDrivingLicenseApp.LastStatusDate.ToString();
            lblCreatedByUser.Text = _LocalDrivingLicenseApp.CreatedByUserInfo.UserName;
        }

        public void LoadLocalDrivingLicenseAppInfo(int LocalDrivingLicenseAppID)
        {
            
            _LocalDrivingLicenseApp = clsLocalDrivingLicenseApplication.Find(LocalDrivingLicenseAppID);

            if (_LocalDrivingLicenseApp == null)
            {
                _ResetDefaultValues();
                MessageBox.Show("No license application with ID = " + LocalDrivingLicenseAppID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillLicenseInfo();

        }

        private void llShowApplicantInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonInfo frm = new frmPersonInfo(_LocalDrivingLicenseApp.ApplicantPersonID);
            frm.ShowDialog();
        }
    }
}