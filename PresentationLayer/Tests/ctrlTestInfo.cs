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
    public partial class ctrlTestInfo : UserControl
    {

        private clsTestAppointment _TestAppointment;

        public ctrlTestInfo()
        {
            InitializeComponent();
        }

        private void _LoadTestInfo()
        {
            lblLocalDrivingLicenseAppID.Text = _TestAppointment.LocalDrivingLicenseApplicationID.ToString();
            lblLicenseClass.Text = _TestAppointment.LocalDrivingLicenseApplicationInfo.LicenseClassInfo.ClassName.ToString();
            lblApplicantName.Text = _TestAppointment.LocalDrivingLicenseApplicationInfo.ApplicantInfo.FullName;
            lblTrial.Text = clsLocalDrivingLicenseApplication.GetFailedTrialsCount(_TestAppointment.LocalDrivingLicenseApplicationID, _TestAppointment.TestTypeInfo.ID).ToString();
            lblDate.Text = _TestAppointment.AppointmentDate.ToShortDateString();
            lblFees.Text = _TestAppointment.PaidFees.ToString();
        }

        public void LoadTestAppointmentInfo(int TestAppointmentID)
        {

            _TestAppointment = clsTestAppointment.Find(TestAppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show(
                                "Test appointment not found.\nIt may have been deleted or does not exist.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                                );
                return;
            }

            _LoadTestInfo();

        }

        public void DisplayTestID(int TestID)
        {
            lblTestID.Text = TestID.ToString();
        }

    }
}
