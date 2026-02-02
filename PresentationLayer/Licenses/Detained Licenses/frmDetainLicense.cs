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

namespace DVLD.Licenses.Detained_Licenses
{
    public partial class frmDetainLicense : Form
    {

        private int _LicenseID = -1;
        private clsLicense _License = null;

        public frmDetainLicense()
        {
            InitializeComponent();
        }

        private void _ResetDefaultValues()
        {
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
        }

        private void ctrlDriverLicenseInfoWithFilter1_LicenseSelected(int obj)
        {

            _LicenseID = obj;
            _License = clsLicense.Find(_LicenseID);

            // Check if license is inactive
            if (!_License.IsActive)
            {
                MessageBox.Show(
                                "This license is inactive and cannot be detained.",
                                "License Inactive",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                                );
                return;
            }

            // Check if license is already been detained
            if (clsDetainedLicense.IsLicenseDetained(_LicenseID))
            {
                MessageBox.Show(
                                "This license is already detained and cannot be detained again.",
                                "License Already Detained",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                                );
                return;
            }

            lblLicenseID.Text = _LicenseID.ToString();
            llShowLicensesHistory.Enabled = true;
            btnDetain.Enabled = true;

        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFineFees.Text))
            {
                e.Cancel = true; // Prevent losing focus
                errorProvider1.SetError(txtFineFees, "Fine fees field is required.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFineFees, "");
            }
        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (Backspace, Delete, etc.)
            if (char.IsControl(e.KeyChar))
                return;

            // Allow only digits
            if (!char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private clsDetainedLicense _CreateDetainedLicense()
        {

            clsDetainedLicense detainedLicense = new clsDetainedLicense();

            detainedLicense.LicenseID = _LicenseID;
            detainedLicense.DetainDate = DateTime.Now;
            detainedLicense.FineFees = Convert.ToDecimal(txtFineFees.Text);
            detainedLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            return detainedLicense;

        }

        private void btnDetain_Click(object sender, EventArgs e)
        {

            // step 1: deactivate license
            if (!_License.DeactivateLicense())
            {
                MessageBox.Show(
                                    "Failed to deactivate the license.\n\nPlease try again or contact support.",
                                    "Deactivation Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                    );
                return;
            }

            // step2: create a detain license record
            clsDetainedLicense detainedLicense = _CreateDetainedLicense();

            if (!detainedLicense.Save())
            {
                MessageBox.Show(
                    "Failed to save the detained license.\n\nPlease try again or contact support if the problem persists.",
                    "Save Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            lblDetainID.Text = detainedLicense.DetainID.ToString();
            btnDetain.Enabled = false;
            llShowLicensesInfo.Enabled = true;

            MessageBox.Show(
                            $"License detained successfully with Detain ID = {detainedLicense.DetainID}.",
                            "Detain Successful",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                            );


        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(_License.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_LicenseID);
            frm.ShowDialog();
        }
    }
}