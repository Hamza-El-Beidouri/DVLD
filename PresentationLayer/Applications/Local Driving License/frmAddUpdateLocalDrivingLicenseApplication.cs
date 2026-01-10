using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Global_Classes;
using DVLD_BusinessLayer;

namespace DVLD.Applications.Local_Driving_License
{
    public partial class frmAddUpdateLocalDrivingLicenseApplication : Form
    {

        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode;

        private int _LocalDrivingLicenseApplicationID = -1;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        public frmAddUpdateLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            Mode = enMode.AddNew;
        }

        public frmAddUpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            Mode = enMode.Update;
        }

        private void _FillLicenseClassesComboBox()
        {

            DataTable dtLicenseClasses = clsLicenseClass.GetAllLicenseClasses();

            foreach (DataRow row in dtLicenseClasses.Rows)
            {
                cbLicenseClasses.Items.Add(row["ClassName"]);
            }

        }

        private void _ResetDefaultValues()
        {
            
            if (Mode == enMode.AddNew)
            {
                tpApplicationInfo.Enabled = false;
                lblLocalDrivingLicenseApplication.Text = "New Local Driving License Application";
                this.Text = lblLocalDrivingLicenseApplication.Text;
                lblLicenseApplicationID.Text = "[????]";
                lblApplicationDate.Text = DateTime.Now.ToShortDateString();

                _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
            }
            else
            {
                tpApplicationInfo.Enabled = true;
                lblLocalDrivingLicenseApplication.Text = "Update Local Driving License Application";
                this.Text = lblLocalDrivingLicenseApplication.Text;
                ctrlPersonCardWithFilter1.FilterEnabled = false;
                _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.Find(_LocalDrivingLicenseApplicationID);
            }

            _FillLicenseClassesComboBox();
            cbLicenseClasses.SelectedIndex = 
                cbLicenseClasses.FindString
                (clsLicenseClass.Find((sbyte)clsLicenseClass.enLicenseClass.OrdinaryDrivingLicense).ClassName);

            lblFees.Text = clsApplicationType.GetFees((byte)clsApplication.enApplicationType.NewDrivingLicense).ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

        }

        private void _LoadData()
        {

            _LocalDrivingLicenseApplication = 
                clsLocalDrivingLicenseApplication.Find(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show(
                                "The local driving license application could not be found.",
                                "Not Found",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                                );
                this.Close();
            }

            ctrlPersonCardWithFilter1.LoadPersonInfo(_LocalDrivingLicenseApplication.ApplicantPersonID);

            lblLicenseApplicationID.Text = 
                _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();

            lblApplicationDate.Text =
                _LocalDrivingLicenseApplication.ApplicationDate.ToShortDateString();

            cbLicenseClasses.SelectedIndex =
                cbLicenseClasses.FindString(clsLicenseClass.Find(_LocalDrivingLicenseApplication.LicenseClassID).ClassName);

        }

        private void frmAddUpdateLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            
            _ResetDefaultValues();
            
            if (Mode == enMode.Update)
                _LoadData();

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            
            if (Mode == enMode.Update)
            {
                tcLocalDrivingLicenseApplicationInfo.SelectedTab = tpApplicationInfo;
                return;
            }

            if (ctrlPersonCardWithFilter1.personID != -1)
            {
                tpApplicationInfo.Enabled = true;
                tcLocalDrivingLicenseApplicationInfo.SelectedTab = tpApplicationInfo;
            }
            else
                MessageBox.Show(
                    "The selected person does not exist.\n\nPlease search and select a valid person.",
                    "Person Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

        }

        private void _PrepareApplicationRecord()
        {

            _LocalDrivingLicenseApplication.ApplicantPersonID = ctrlPersonCardWithFilter1.personID;
            _LocalDrivingLicenseApplication.ApplicationDate = DateTime.Now;
            _LocalDrivingLicenseApplication.ApplicationTypeID =
                (sbyte)clsApplication.enApplicationType.NewDrivingLicense;

            _LocalDrivingLicenseApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;

            _LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;

            _LocalDrivingLicenseApplication.PaidFees =
                clsApplicationType.GetFees((byte)clsApplication.enApplicationType.NewDrivingLicense);

            _LocalDrivingLicenseApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            _LocalDrivingLicenseApplication.LicenseClassID =
                clsLicenseClass.Find(cbLicenseClasses.Text).LicenseClassID;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            _PrepareApplicationRecord();

            int ActiveApplicationID = 
                clsApplication.GetActiveApplicationIdByLicenseClass(
                                                                    ctrlPersonCardWithFilter1.personID,
                                                                    clsApplication.enApplicationType.NewDrivingLicense,
                                                                    _LocalDrivingLicenseApplication.LicenseClassID
                                                                    );

            if (ActiveApplicationID == -1)
            {

                if (_LocalDrivingLicenseApplication.Save())
                {
                    this.Text = "Update Local Driving License Application";
                    lblLocalDrivingLicenseApplication.Text = this.Text;
                    lblLicenseApplicationID.Text =
                        _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();

                    MessageBox.Show(
                                    "The local driving license application was saved successfully.",
                                    "Success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information
                                    );
                }
                else
                {
                    MessageBox.Show(
                                    "Failed to save the local driving license application. Please try again.",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                    );
                }

            }
            else
                MessageBox.Show(
                                $"There is already an active application for this license class with an application ID of {ActiveApplicationID}. \n\nPlease pick another one.",
                                "Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                                );



        }
    }
}
