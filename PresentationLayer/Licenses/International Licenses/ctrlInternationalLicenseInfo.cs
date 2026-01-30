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

namespace DVLD.Licenses.International_Licenses
{
    public partial class ctrlInternationalLicenseInfo : UserControl
    {

        private clsInternationalLicense _InternationalLicense;

        public ctrlInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        private void _ResetDefaultValues()
        {
            lblFullName.Text = "[????]";
            lblInternationalLicenseID.Text = "[????]";
            lblLocalLicenseID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblGender.Text = "[????]";
            lblIssueDate.Text = "[????]";
            lblApplicationID.Text = "[????]";
            lblIsActive.Text = "[????]";
            lblBirthDate.Text = "[????]";
            lblDriverID.Text = "[????]";
            lblExpirationDate.Text = "[????]";
        }

        private void _FillDriverLicenseInfo()
        {
            lblFullName.Text = _InternationalLicense.DriverInfo.PersonInfo.FullName;
            lblInternationalLicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();
            lblLocalLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
            lblNationalNo.Text = _InternationalLicense.DriverInfo.PersonInfo.NationalNo;
            lblGender.Text = _InternationalLicense.DriverInfo.PersonInfo.Gender == 0 ? "Male" : "Female";
            lblIssueDate.Text = _InternationalLicense.IssueDate.ToShortDateString();
            lblApplicationID.Text = _InternationalLicense.ApplicationID.ToString();
            lblIsActive.Text = _InternationalLicense.IsActive ? "Yes" : "No";
            lblBirthDate.Text = _InternationalLicense.DriverInfo.PersonInfo.DateOfBirth.ToString();
            lblDriverID.Text = _InternationalLicense.DriverID.ToString();
            lblExpirationDate.Text = _InternationalLicense.ExpirationDate.ToShortDateString();
        }

        public void LoadInternationalDriverLicenseInfo(int internationalLicenseID)
        {

            _InternationalLicense = clsInternationalLicense.Find(internationalLicenseID);

            if (_InternationalLicense == null)
            {
                _ResetDefaultValues();
                MessageBox.Show(
                                $"No international license record was found for ID: {internationalLicenseID}",
                                "International License Not Found",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                                );
                return;
            }

            _FillDriverLicenseInfo();

        }

    }
}