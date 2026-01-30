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

namespace DVLD.Licenses.Local_Licenses
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {

        private bool _isLicenseFound = false;

        public bool isLicenseFound
        {
            get { return _isLicenseFound;  }
        }

        private clsLicense _License;

        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }

        private void _ResetDefaultValues()
        {
            lblLicenseClass.Text = "[????]";
            lblApplicantName.Text = "[????]";
            lblLicenseID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblGender.Text = "[????]";
            lblIssueDate.Text = "[????]";
            lblIssueReason.Text = "[????]";
            lblNotes.Text = "[????]";
            lblIsActive.Text = "[????]";
            lblBirthDate.Text = "[????]";
            lblDriverID.Text = "[????]";
            lblExpirationDate.Text = "[????]";
            lblIsDetained.Text = "[????]";
            pbApplicantImage.Image = Resources.Male;
        }

        private void _HandleApplicantImage()
        {

            if (_License.DriverInfo.PersonInfo.ImagePath == "")
            {
                pbApplicantImage.Image = 
                    (_License.DriverInfo.PersonInfo.Gender == 0) ? 
                    Resources.Male : Resources.Female;
                return;
            }

            pbApplicantImage.ImageLocation = _License.ApplicationInfo.ApplicantInfo.ImagePath;

        }

        private void _FillDriverLicenseInfo()
        {
            lblLicenseClass.Text = _License.LicenseClassInfo.ClassName;
            lblApplicantName.Text = _License.ApplicationInfo.ApplicantInfo.FullName;
            lblLicenseID.Text = _License.LicenseID.ToString();
            lblNationalNo.Text = _License.ApplicationInfo.ApplicantInfo.NationalNo;
            lblGender.Text = (_License.ApplicationInfo.ApplicantInfo.Gender == 0) ? "Male" : "Female";
            lblIssueDate.Text = _License.IssueDate.ToShortDateString();
            lblIssueReason.Text =_License.IssueReasonText;
            lblNotes.Text = (_License.Notes == "") ? "No Notes" : _License.Notes;
            lblIsActive.Text = (_License.IsActive) ? "Yes" : "No";
            lblBirthDate.Text = _License.ApplicationInfo.ApplicantInfo.DateOfBirth.ToShortDateString();
            lblDriverID.Text = _License.DriverID.ToString();
            lblExpirationDate.Text = _License.ExpirationDate.ToShortDateString();
            lblIsDetained.Text = (_License.IsLicenseDetained()) ? "Yes" : "No";
            _HandleApplicantImage();
        }

        public void LoadDriverLicenseInfo(int LicenseID)
        {
            
            _License = clsLicense.Find(LicenseID);

            if (_License == null)
            {
                _isLicenseFound = false;
                _ResetDefaultValues();
                MessageBox.Show(
                                $"No license record was found for license ID: {LicenseID}",
                                "License Not Found",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                                );
                return;
            }

            _isLicenseFound = true;
            _FillDriverLicenseInfo();

        }

    }
}