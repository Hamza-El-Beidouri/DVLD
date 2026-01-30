using DVLD.Global_Classes;
using DVLD.Licenses.International_Licenses;
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

namespace DVLD.Applications.International_Licenses
{
    public partial class frmInternationalLicenseApplication : Form
    {

        private int _LocalLicenseID = -1;
        private clsLicense _LocalLicense;
        private clsInternationalLicense _InternationalLicense;

        public frmInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void frmInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblFees.Text = clsApplicationType.GetFees(clsApplication.enApplicationType.NewInternationalLicense).ToString();
            lblExpirationDate.Text = Convert.ToDateTime(lblIssueDate.Text)
                            .AddYears(1)
                            .ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
        }

        private void ctrlDriverLicenseInfoWithFilter1_LicenseSelected(int obj)
        {

            _LocalLicenseID = obj;
            lblLocalLicenseID.Text = obj.ToString();
            llShowLicensesHistory.Enabled = true;

            _LocalLicense = clsLicense.Find(_LocalLicenseID);

            bool isValidLocalLicense =
                                        _LocalLicense.IsActive &&
                                        _LocalLicense.ExpirationDate.Date >= DateTime.Today;

            btnIssue.Enabled = isValidLocalLicense;

        }

        private void btnIssue_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show(
                "Are you sure you want to issue a new international license?",
                "Confirm Issue",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes) // Only proceed if user clicks Yes
            {
                // Check if an active international license already exists
                if (clsInternationalLicense.HasActiveInternationalLicenseForLocalLicenseID(_LocalLicenseID))
                {
                    MessageBox.Show(
                        "An active international license has already been issued for this local license.",
                        "Cannot Issue License",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Create the application
                clsApplication application = new clsApplication();
                application.ApplicantPersonID = _LocalLicense.ApplicationInfo.ApplicantPersonID;
                application.ApplicationDate = DateTime.Now;
                application.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
                application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                application.LastStatusDate = application.ApplicationDate;
                application.PaidFees = clsApplicationType.GetFees(clsApplication.enApplicationType.NewInternationalLicense);
                application.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                if (!application.Save())
                {
                    MessageBox.Show(
                        "Failed to save the international license application. Please try again or contact support.",
                        "Save Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                // Intialize the international license
                _InternationalLicense = new clsInternationalLicense();
                _InternationalLicense.ApplicationID = application.ApplicationID;
                _InternationalLicense.DriverID = _LocalLicense.DriverID;
                _InternationalLicense.IssuedUsingLocalLicenseID = _LocalLicenseID;
                _InternationalLicense.IssueDate = DateTime.Now;
                _InternationalLicense.ExpirationDate = _InternationalLicense.IssueDate.AddYears(1);
                _InternationalLicense.IsActive = true;
                _InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                if (_InternationalLicense.Save())
                {
                    lblInternationalLicenseApplicationID.Text = application.ApplicationID.ToString();
                    lblInternationalLicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();

                    MessageBox.Show(
                        $"International license saved successfully with ID = {_InternationalLicense.InternationalLicenseID}.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    btnIssue.Enabled = false;
                    ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
                    llShowLicensesInfo.Enabled = true;

                }
                else
                    MessageBox.Show(
                        "Failed to save the international license. Please try again or contact support.",
                        "Save Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
            }


        }

        private void llShowLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInternationalDriverLicenseInfo frm =
                new frmInternationalDriverLicenseInfo(_InternationalLicense.InternationalLicenseID);
            frm.ShowDialog();
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm =
                new frmShowPersonLicenseHistory(_LocalLicense.DriverInfo.PersonID);
            frm.ShowDialog();
        }
    }
}
