using DVLD.Properties;
using DVLD.Tests;
using DVLD.Users;
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
using static DVLD_BusinessLayer.clsTestType;

namespace DVLD.TestAppointments
{
    public partial class frmListTestAppointments : Form
    {

        private clsTestType.enTestType _TestType;
        private int _LocalDrivingLicenseApplicationID = -1;
        private DataTable _dtTestAppointments;

        public frmListTestAppointments(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestType = TestType;
        }

        private void _ConfigureTestTypeUI(clsTestType.enTestType testType)
        {

            switch (testType)
            {

                case clsTestType.enTestType.VisionTest:
                    this.Text = "Vision Test Appointments";
                    lblTitle.Text = this.Text;
                    pbTestTypeImage.Image = Resources.VisionTest;
                    break;

                case clsTestType.enTestType.WrittenTest:
                    this.Text = "Theory Test Appointments";
                    lblTitle.Text = this.Text;
                    pbTestTypeImage.Image = Resources.TheoryTest;
                    break;

                case clsTestType.enTestType.StreetTest:
                    this.Text = "Driving Test Appointments";
                    lblTitle.Text = this.Text;
                    pbTestTypeImage.Image = Resources.DrivingTest;
                    break;

            }

        }

        private void _DesignDataGridView()
        {
            Color primaryColor = Color.FromArgb(233, 93, 53); // Your new color
            Color lightPrimary = Color.FromArgb(255, 224, 214); // soft selection
            Color alternateRow = Color.FromArgb(255, 245, 240); // very light orange

            // Background
            dgvTestAppointments.BackgroundColor = Color.White;
            dgvTestAppointments.BorderStyle = BorderStyle.None;

            // Header
            dgvTestAppointments.ColumnHeadersDefaultCellStyle.SelectionBackColor = primaryColor;
            dgvTestAppointments.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            dgvTestAppointments.ColumnHeadersDefaultCellStyle.BackColor = primaryColor;
            dgvTestAppointments.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTestAppointments.ColumnHeadersDefaultCellStyle.Padding = new Padding(4);
            dgvTestAppointments.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);

            dgvTestAppointments.ColumnHeadersHeight = 40;
            dgvTestAppointments.EnableHeadersVisualStyles = false;
            dgvTestAppointments.AdvancedColumnHeadersBorderStyle.Bottom =
                DataGridViewAdvancedCellBorderStyle.Single;
            dgvTestAppointments.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // Rows
            dgvTestAppointments.DefaultCellStyle.BackColor = Color.White;
            dgvTestAppointments.DefaultCellStyle.ForeColor = Color.Black;
            dgvTestAppointments.DefaultCellStyle.SelectionBackColor = lightPrimary;
            dgvTestAppointments.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvTestAppointments.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            dgvTestAppointments.AlternatingRowsDefaultCellStyle.BackColor = alternateRow;

            dgvTestAppointments.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvTestAppointments.GridColor = Color.FromArgb(220, 220, 220);

            dgvTestAppointments.RowTemplate.Height = 35;
            dgvTestAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTestAppointments.AllowUserToAddRows = false;

            dgvTestAppointments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTestAppointments.MultiSelect = false;
        }

        private void _ConfigureTestAppointmentsGrid()
        {

            _dtTestAppointments = 
                clsTestAppointment.GetTestAppointments(_LocalDrivingLicenseApplicationID, _TestType);

            dgvTestAppointments.DataSource = _dtTestAppointments;
            lblTotalRecordsCount.Text =
                dgvTestAppointments.Rows.Count.ToString() + " Record(s)";

            if (dgvTestAppointments.Rows.Count > 0)
            {

                dgvTestAppointments.Columns["TestAppointmentID"].HeaderText = "Appointment ID";
                dgvTestAppointments.Columns["AppointmentDate"].HeaderText = "Appointment Date";
                dgvTestAppointments.Columns["AppointmentDate"].DefaultCellStyle.Format = "dd/MM/yyyy hh:mm tt";
                dgvTestAppointments.Columns["PaidFees"].HeaderText = "Paid Fees";
                dgvTestAppointments.Columns["IsLocked"].HeaderText = "Is Locked";
                dgvTestAppointments.Columns["IsLocked"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // Weight-based distribution
                dgvTestAppointments.Columns["TestAppointmentID"].FillWeight = 20;
                dgvTestAppointments.Columns["AppointmentDate"].FillWeight = 40;
                dgvTestAppointments.Columns["PaidFees"].FillWeight = 20;
                dgvTestAppointments.Columns["IsLocked"].FillWeight = 20;
            }

        }

        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _ConfigureTestTypeUI(_TestType);
            ctrlLocalDrivingLicenseApplicationInfo1.LoadLocalDrivingLicenseAppInfo(_LocalDrivingLicenseApplicationID);
            _DesignDataGridView();
            _ConfigureTestAppointmentsGrid();
        }

        private void _RefreshTestAppointmentsGrid()
        {
            _dtTestAppointments =
                clsTestAppointment.GetTestAppointments(_LocalDrivingLicenseApplicationID, _TestType);

            dgvTestAppointments.DataSource = _dtTestAppointments;
            lblTotalRecordsCount.Text =
                dgvTestAppointments.Rows.Count.ToString() + " Record(s)";

            if (dgvTestAppointments.Columns.Count > 0)
            {
                dgvTestAppointments.Columns["TestAppointmentID"].HeaderText = "Appointment ID";
                dgvTestAppointments.Columns["AppointmentDate"].HeaderText = "Appointment Date";
                dgvTestAppointments.Columns["AppointmentDate"].DefaultCellStyle.Format = "dd/MM/yyyy hh:mm tt";
                dgvTestAppointments.Columns["PaidFees"].HeaderText = "Paid Fees";
                dgvTestAppointments.Columns["IsLocked"].HeaderText = "Is Locked";
                dgvTestAppointments.Columns["IsLocked"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvTestAppointments.Columns["TestAppointmentID"].FillWeight = 20;
                dgvTestAppointments.Columns["AppointmentDate"].FillWeight = 40;
                dgvTestAppointments.Columns["PaidFees"].FillWeight = 20;
                dgvTestAppointments.Columns["IsLocked"].FillWeight = 20;
            }

        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {

            // Check if this local driving license application has already an active appointment
            bool isActive = clsLocalDrivingLicenseApplication.HasActiveTestAppointment(_LocalDrivingLicenseApplicationID, _TestType);

            if (isActive)
            {
                
                MessageBox.Show(
                    "This person already has an active appointment for this test type. You cannot add a new test appointment.",
                    "Cannot Add Appointment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            // Check if this local driving license application has already passed this test
            bool isTestPassed = clsLocalDrivingLicenseApplication.IsTestPassed(_LocalDrivingLicenseApplicationID, _TestType);

            if (isTestPassed)
            {
                MessageBox.Show(
                    "This person has already passed this test. You cannot add a new appointment for a test that is already completed.",
                    "Test Already Passed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            // at this point, we can safely display schedule test form
            frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestType);
            frm.ShowDialog();
            _RefreshTestAppointmentsGrid();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = Convert.ToInt32(dgvTestAppointments.CurrentRow.Cells[0].Value);
            frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestType, TestAppointmentID);
            frm.ShowDialog();
            _RefreshTestAppointmentsGrid();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = Convert.ToInt32(dgvTestAppointments.CurrentRow.Cells[0].Value);
            frmTakeTest frm = new frmTakeTest(TestAppointmentID);
            frm.ShowDialog();
            ctrlLocalDrivingLicenseApplicationInfo1.LoadLocalDrivingLicenseAppInfo(_LocalDrivingLicenseApplicationID);
            _RefreshTestAppointmentsGrid();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            bool isLocked = Convert.ToBoolean(dgvTestAppointments.CurrentRow.Cells[3].Value);
            takeTestToolStripMenuItem.Enabled = !isLocked;
        }
    }
}
