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

namespace DVLD.TestTypes
{
    public partial class frmEditTestType : Form
    {

        private clsTestType.enTestType _testTypeID = clsTestType.enTestType.VisionTest;
        private clsTestType _testType;

        public frmEditTestType(clsTestType.enTestType testTypeID)
        {
            InitializeComponent();
            _testTypeID = testTypeID;
        }

        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            _testType = clsTestType.Find(_testTypeID);

            if (_testType == null)
            {
                lblID.Text = "[????]";
                MessageBox.Show(
                                "The selected test type could not be found. It may have been deleted or no longer exists.",
                                "Not Found",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                                );
                this.Close();
            }

            lblID.Text = _testTypeID.ToString();
            txtTestTitle.Text = _testType.Title;
            rtbDescription.Text = _testType.Description;
            txtTestFees.Text = _testType.Fees.ToString();
        }

        private void txtTestTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTestTitle.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTestTitle, "Test description is required.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtTestTitle, string.Empty);
            }
        }

        private void rtbDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(rtbDescription.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(rtbDescription, "Test title is required.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(rtbDescription, string.Empty);
            }
        }

        private void txtTestFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTestFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTestFees, "Test fees are required.");
                return;
            }

            if (!decimal.TryParse(txtTestFees.Text, out decimal fees))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTestFees,
                    "Please enter a valid numeric fee amount.");
                return;
            }

            if (fees < 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTestFees, "Fees cannot be negative.");
                return;
            }

            // Valid input
            e.Cancel = false;
            errorProvider1.SetError(txtTestFees, string.Empty);
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

            _testType.Title = txtTestTitle.Text;
            _testType.Fees = decimal.Parse(txtTestFees.Text);

            if (_testType.Save())
            {
                // Success
                MessageBox.Show(
                    "The test type has been updated successfully.",
                    "Update Successful",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                // Failure
                MessageBox.Show(
                    "Failed to update the test type. Please check the data and try again.",
                    "Update Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

    }
}