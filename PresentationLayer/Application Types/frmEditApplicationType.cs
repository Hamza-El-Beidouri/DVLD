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

namespace DVLD.Application_Types
{
    public partial class frmEditApplicationType : Form
    {

        private sbyte _applicationTypeID;
        private clsApplicationType _applicationType;

        public frmEditApplicationType(sbyte applicationTypeID)
        {
            InitializeComponent();
            _applicationTypeID = applicationTypeID;
        }

        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {

            _applicationType = clsApplicationType.Find(_applicationTypeID);

            if (_applicationType == null)
            {
                lblID.Text = "[????]";
                MessageBox.Show(
                                "The selected application type could not be found. It may have been deleted or no longer exists.",
                                "Not Found",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                                );
                this.Close();
            }

            lblID.Text = _applicationTypeID.ToString();
            txtApplicationTitle.Text = _applicationType.Title;
            txtApplicationFees.Text = _applicationType.Fees.ToString();
        }

        private void txtApplicationTitle_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtApplicationTitle.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtApplicationTitle, "Application title is required.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtApplicationTitle, string.Empty);
            }

        }

        private void txtApplicationFees_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtApplicationFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtApplicationFees, "Application fees are required.");
                return;
            }

            if (!decimal.TryParse(txtApplicationFees.Text, out decimal fees))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtApplicationFees,
                    "Please enter a valid numeric fee amount.");
                return;
            }

            if (fees < 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtApplicationFees, "Fees cannot be negative.");
                return;
            }

            // Valid input
            e.Cancel = false;
            errorProvider1.SetError(txtApplicationFees, string.Empty);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                MessageBox.Show(
                                "Please correct the highlighted errors before saving.",
                                "Validation Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                                );
                return;
            }

            _applicationType.Title = txtApplicationTitle.Text;
            _applicationType.Fees = decimal.Parse(txtApplicationFees.Text);

            if (_applicationType.Save())
            {
                // Success
                MessageBox.Show(
                    "The application type has been updated successfully.",
                    "Update Successful",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                // Failure
                MessageBox.Show(
                    "Failed to update the application type. Please check the data and try again.",
                    "Update Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

        }

    }
}
