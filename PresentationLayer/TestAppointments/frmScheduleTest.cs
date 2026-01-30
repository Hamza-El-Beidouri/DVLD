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

namespace DVLD.TestAppointments
{
    public partial class frmScheduleTest : Form
    {

        private clsTestType.enTestType _TestType;
        private int _LocalDrivingLicenseApplication;
        private int _TestAppointmentID;

        public frmScheduleTest(int localDrivingLicenseApplication, clsTestType.enTestType testType, int testAppointmentID = -1)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplication = localDrivingLicenseApplication;
            _TestType = testType;
            _TestAppointmentID = testAppointmentID;
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            ctrlScheduleTest1.LoadTestTypeInfo(_LocalDrivingLicenseApplication, _TestType, _TestAppointmentID);
        }
    }
}
