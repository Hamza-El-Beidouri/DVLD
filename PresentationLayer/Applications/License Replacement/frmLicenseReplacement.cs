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

namespace DVLD.Applications.License_Replacement
{
    public partial class frmLicenseReplacement : Form
    {

        private int _OldLicenseID = -1;
        private clsLicense _OldLicense = null;
        private clsLicense _NewLicense = null;

        private clsLicense.enIssueReason _IssueReason = 
            clsLicense.enIssueReason.ReplacementForDamaged;

        private clsApplication.enApplicationType _ApplicationType = 
            clsApplication.enApplicationType.ReplaceDamagedDrivingLicense;
        

        public frmLicenseReplacement()
        {
            InitializeComponent();
        }

        private void _ResetDefaultValues()
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblApplicationFees.Text = clsApplicationType
                .GetFees(clsApplication.enApplicationType.ReplaceDamagedDrivingLicense).ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
            rbDamagedLicense.Checked = true;
        }

        private void frmLicenseReplacement_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
        }

        private void ctrlDriverLicenseInfoWithFilter1_LicenseSelected(int obj)
        {

            _OldLicenseID = obj;
            _OldLicense = clsLicense.Find(_OldLicenseID);

            // Check if this license is inactive
            if (!_OldLicense.IsActive)
            {
                MessageBox.Show(
                                "This license is inactive and cannot be replaced or renewed.",
                                "License Inactive",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                                );
                return;
            }

            // Check if this driver has already replaced his license
            if (_OldLicense.HasDriverRenewedLicense())
            {
                MessageBox.Show(
                                "This license has already been replaced.\n\n" +
                                "You cannot replace the same license more than once.",
                                "Already Replaced",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                                );
                return;
            }

            lblOldLicenseID.Text = _OldLicenseID.ToString();
            llShowLicensesHistory.Enabled = true;
            btnIssueReplacement.Enabled = true;

        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            _IssueReason = clsLicense.enIssueReason.ReplacementForLost;
            _ApplicationType = clsApplication.enApplicationType.ReplaceLostDrivingLicense;
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            _IssueReason = clsLicense.enIssueReason.ReplacementForDamaged;
            _ApplicationType = clsApplication.enApplicationType.ReplaceDamagedDrivingLicense;
        }

        private clsApplication _CreateReplacementApplication()
        {

            clsApplication application = new clsApplication();

            application.ApplicantPersonID = _OldLicense.DriverInfo.PersonID;
            application.ApplicationDate = DateTime.Now;

            if (_IssueReason == clsLicense.enIssueReason.ReplacementForLost)
                application.ApplicationTypeID = (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;
            else
                application.ApplicationTypeID = (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense;

            application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            application.LastStatusDate = application.ApplicationDate;
            application.PaidFees = clsApplicationType.GetFees(_ApplicationType);
            application.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            return application;

        }

        private clsLicense _CreateReplacementLicense(int ApplicationID)
        {

            clsLicense License = new clsLicense();

            License.ApplicationID = ApplicationID;
            License.DriverID = _OldLicense.DriverID;
            License.LicenseClassID = _OldLicense.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate =
                License.IssueDate.AddYears(_OldLicense.LicenseClassInfo.DefaultValidityLength);
            License.Notes = string.Empty;
            License.PaidFees = Convert.ToDecimal(lblApplicationFees.Text) + _OldLicense.LicenseClassInfo.ClassFees;
            License.IsActive = true;
            License.IssueReason = _IssueReason;
            License.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            return License;

        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show(
                                "Are you sure you want to replace this license?",
                                "Confirm License Replacement",
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

                clsApplication application = _CreateReplacementApplication();

                if (!application.Save())
                {
                    MessageBox.Show(
                                    "Failed to save the replacement application.\n\nPlease try again or contact support if the problem persists.",
                                    "Save Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                    );
                    return;
                }

                _NewLicense = _CreateReplacementLicense(application.ApplicationID);

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

                lblLicenseReplacementApplicationID.Text = application.ApplicationID.ToString();
                lblReplacedLicenseID.Text = _NewLicense.LicenseID.ToString();
                llShowLicensesInfo.Enabled = true;
                ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
                btnIssueReplacement.Enabled = false;

                MessageBox.Show(
                                $"License replaced successfully with ID = {_NewLicense.LicenseID}",
                                "Renewal Successful",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                                );

            }

        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = 
                new frmShowPersonLicenseHistory(_OldLicense.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_NewLicense.LicenseID);
            frm.ShowDialog();
        }
    }
}