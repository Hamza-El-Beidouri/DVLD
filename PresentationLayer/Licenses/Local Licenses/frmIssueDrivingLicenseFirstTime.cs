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

namespace DVLD.Licenses.Local_Licenses
{
    public partial class frmIssueDrivingLicenseFirstTime : Form
    {

        private int _LocalDrivingLicenseApplicationID = -1;

        public frmIssueDrivingLicenseFirstTime(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        private void frmIssueDrivingLicenseFirstTime_Load(object sender, EventArgs e)
        {
            ctrlLocalDrivingLicenseApplicationInfo1.LoadLocalDrivingLicenseAppInfo(_LocalDrivingLicenseApplicationID);
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {

            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = 
                clsLocalDrivingLicenseApplication.Find(_LocalDrivingLicenseApplicationID);

            int LicenseID = localDrivingLicenseApplication.IssueDrivingLicenseForTheFirstTime(rtbNotes.Text.Trim(), clsGlobal.CurrentUser.UserID);

            if (LicenseID != -1)
                MessageBox.Show(
                                $"Driving license issued successfully.\nLicense ID: {LicenseID}",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                                );
            else
                MessageBox.Show(
                                "Failed to issue driving license.\nPlease try again or contact the administrator.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                                );

        }
    }
}
