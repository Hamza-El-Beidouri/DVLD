using DVLD.Global_Classes;
using DVLD.Licenses.Local_Licenses;
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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Applications.Release_Detained_Licenses
{
    public partial class frmReleaseDetainedLicense : Form
    {
        private int _LicenseID = -1;
        private clsDetainedLicense _DetainedLicense = null;

        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
        }

        public frmReleaseDetainedLicense(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;
        }

        private void frmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {
            if (_LicenseID != -1)
            {
                ctrlDriverLicenseInfoWithFilter1.LoadDriverLicenseInfo();
                ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            }
        }

        private void _LoadDetainedLicenseInfo()
        {
            llShowLicensesHistory.Enabled = true;
            btnRelease.Enabled = true;

            _DetainedLicense = clsDetainedLicense.FindByLicenseID(_LicenseID);

            lblDetainID.Text = _DetainedLicense.DetainID.ToString();
            lblDetainDate.Text = _DetainedLicense.DetainDate.ToShortDateString();
            lblApplicationFees.Text = clsApplicationType
                .GetFees(clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ToString();
            lblCreatedBy.Text = _DetainedLicense.CreatedByUserInfo.UserName;
            lblFineFees.Text = _DetainedLicense.FineFees.ToString();
            lblTotalFees.Text = (Convert.ToDecimal(lblApplicationFees.Text) + _DetainedLicense.FineFees)
                .ToString();
        }

        private void ctrlDriverLicenseInfoWithFilter1_LicenseSelected(int obj)
        {

            _LicenseID = obj;
            lblLicenseID.Text = _LicenseID.ToString();

            // Check if this license isn't detained
            if (!clsDetainedLicense.IsLicenseDetained(_LicenseID))
            {
                MessageBox.Show(
                                "This license is not currently detained and cannot be released.",
                                "License Not Detained",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                                );
                return;

            }
            
            _LoadDetainedLicenseInfo();

        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = 
                new frmShowPersonLicenseHistory(_DetainedLicense.LicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_DetainedLicense.LicenseID);
            frm.ShowDialog();
        }

        private clsApplication _CreateReleaseDetainedLicenseApplication()
        {

            clsApplication application = new clsApplication();

            application.ApplicantPersonID = _DetainedLicense.LicenseInfo.DriverInfo.PersonID;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
            application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            application.LastStatusDate = application.ApplicationDate;
            application.PaidFees = clsApplicationType.GetFees(clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense);
            application.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            return application;

        }

        private void _SetReleaseDetails(int ApplicationID)
        {
            _DetainedLicense.IsReleased = true;
            _DetainedLicense.ReleaseDate = DateTime.Now;
            _DetainedLicense.ReleasedByUserID = clsGlobal.CurrentUser.UserID;
            _DetainedLicense.ReleaseApplicationID = ApplicationID;
        }

        private void _FinalizeDetainedLicenseRelease(int ApplicationID)
        {
            lblApplicationID.Text = ApplicationID.ToString();
            llShowLicensesInfo.Enabled = true;
            btnRelease.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;

            MessageBox.Show(
                            $"Detained license released successfully.\n\nDetain ID: {_DetainedLicense.DetainID}",
                            "Release Successful",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                            );
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show(
                        "Are you sure you want to release this detained license?",
                        "Confirm License Release",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
            {
                
                clsApplication application = _CreateReleaseDetainedLicenseApplication();

                if (!application.Save())
                {
                    MessageBox.Show(
                                    "Failed to create the release application.\n\nPlease try again or contact support if the problem persists.",
                                    "Save Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                    );
                    return;
                }

                _SetReleaseDetails(application.ApplicationID);

                if (!_DetainedLicense.Save())
                {
                    MessageBox.Show(
                                    "Failed to save the release of the detained license.\n\nPlease try again or contact support if the problem persists.",
                                    "Save Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                    );
                    return;
                }

                _FinalizeDetainedLicenseRelease(application.ApplicationID);


            }

        }

    }
}