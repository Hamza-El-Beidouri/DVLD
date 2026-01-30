using DVLD.Global_Classes;
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

namespace DVLD.Tests
{
    public partial class frmTakeTest : Form
    {

        private int _TestAppointmentID = -1;

        public frmTakeTest(int TestAppointmentID)
        {
            InitializeComponent();
            _TestAppointmentID = TestAppointmentID;
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlTestInfo1.LoadTestAppointmentInfo(_TestAppointmentID);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            clsTest Test = new clsTest();
            clsTestAppointment testAppointment = clsTestAppointment.Find(_TestAppointmentID);

            Test.TestAppointmentID = testAppointment.TestAppointmentID;
            Test.TestResult = rbPass.Checked;
            Test.Notes = rtbNotes.Text.Trim();
            Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            testAppointment.IsLocked = true;

            if (testAppointment.Save())
            {
                if (Test.Save())
                {
                    ctrlTestInfo1.DisplayTestID(Test.TestID);

                    MessageBox.Show(
                        "Test information has been saved successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    this.Close();
                }
                else
                    MessageBox.Show(
                        "Test appointment was locked but test information could not be saved.\nPlease try again.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
            }
            else
                MessageBox.Show(
                    "Failed to lock the test appointment.\nPlease try again.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
        }

    }
}
