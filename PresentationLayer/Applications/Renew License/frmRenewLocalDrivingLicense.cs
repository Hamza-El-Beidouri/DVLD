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

namespace DVLD.Applications.Renew_License
{
    public partial class frmRenewLocalDrivingLicense : Form
    {

        private int _LicenseID = -1;
        private clsLicense _OldLicense = null;
        private clsLicense _NewLicense = null;

        public frmRenewLocalDrivingLicense()
        {
            InitializeComponent();
        }


        private void _ResetDefaultValues()
        {
            lblApplicationFees.Text = clsApplicationType.GetFees(clsApplication.enApplicationType.RenewDrivingLicense).ToString();
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private void frmRenewLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
        }

        private void _EnableLicenseRenewal()
        {
            lblOldLicenseID.Text = _OldLicense.LicenseID.ToString();
            lblExpirationDate.Text = 
                Convert.ToDateTime(lblIssueDate.Text)
                .AddYears(_OldLicense.LicenseClassInfo.DefaultValidityLength).ToShortDateString();
            lblLicenseFees.Text = _OldLicense.LicenseClassInfo.ClassFees.ToString();
            lblTotalFees.Text =
                (Convert.ToDecimal(lblApplicationFees.Text) + _OldLicense.LicenseClassInfo.ClassFees).ToString();
            btnRenew.Enabled = true;
        }

        private void ctrlDriverLicenseInfoWithFilter1_LicenseSelected(int obj)
        {

            _LicenseID = obj;
            _OldLicense = clsLicense.Find(_LicenseID);
            llShowLicensesHistory.Enabled = true;

            // Check if this license wasn't expired yet
            if (!_OldLicense.IsLicenseExpired())
            {
                MessageBox.Show(
                                $"This license is still active and cannot be renewed yet.\n\n" +
                                $"Expiration Date: {_OldLicense.ExpirationDate:dd/MM/yyyy}",
                                "License Active",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                                );
                return;
            }

            // Check if this driver has already renewed his license
            if (_OldLicense.HasDriverRenewedLicense())
            {
                MessageBox.Show(
                                "This license has already been renewed.\n\n" +
                                "You cannot renew the same license more than once.",
                                "Already Renewed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                                );
                return;
            }

            _EnableLicenseRenewal();

        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = 
                new frmShowPersonLicenseHistory(clsDriver.Find(_OldLicense.DriverID).PersonID);
            frm.ShowDialog();
        }

        private void llShowLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_NewLicense.LicenseID);
            frm.ShowDialog();
        }

        private clsApplication _CreateRenewalApplication()
        {

            clsApplication application = new clsApplication();

            application.ApplicantPersonID = _OldLicense.ApplicationInfo.ApplicantPersonID;
            application.ApplicationDate = Convert.ToDateTime(lblApplicationDate.Text);
            application.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicense;
            application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            application.LastStatusDate = application.ApplicationDate;
            application.PaidFees = Convert.ToDecimal(lblApplicationFees.Text);
            application.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            return application;

        }

        private clsLicense _CreateNewLicense(int ApplicationID)
        {

            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = ApplicationID;
            NewLicense.DriverID = _OldLicense.DriverID;
            NewLicense.LicenseClassID = _OldLicense.LicenseClassID;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = 
                NewLicense.IssueDate.AddYears(_OldLicense.LicenseClassInfo.DefaultValidityLength);
            NewLicense.Notes = rtbNotes.Text.Trim();
            NewLicense.PaidFees = Convert.ToDecimal(lblApplicationFees.Text) + _OldLicense.LicenseClassInfo.ClassFees;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = clsLicense.enIssueReason.Renew;
            NewLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            return NewLicense;

        }

        private void btnRenew_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show(
                                "Are you sure you want to renew this license?",
                                "Confirm License Renewal",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {


                if (!_OldLicense.DeactivateLicense())
                {
                    MessageBox.Show(
                                    "Failed to deactivate the old license.\n\nPlease try again or contact support.",
                                    "Deactivation Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                    );
                    return;
                }

                clsApplication application = _CreateRenewalApplication();

                if (!application.Save())
                {
                    MessageBox.Show(
                                    "Failed to save the renewal application.\n\nPlease try again or contact support if the problem persists.",
                                    "Save Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                    );
                    return;
                }

                _NewLicense = _CreateNewLicense(application.ApplicationID);

                if (!_NewLicense.Save())
                {
                    MessageBox.Show(
                                    "Failed to save the new license.\n\nPlease try again or contact support if the problem persists.",
                                    "Save Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                    );

                    return;
                }

                lblRenewLicenseApplicationID.Text = application.ApplicationID.ToString();
                lblRenewedLicenseID.Text = _NewLicense.LicenseID.ToString();
                llShowLicensesInfo.Enabled = true;

                MessageBox.Show(
                                $"License renewed successfully with ID = {_NewLicense.LicenseID}",
                                "Renewal Successful",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                                );

            }

        }
    }
}