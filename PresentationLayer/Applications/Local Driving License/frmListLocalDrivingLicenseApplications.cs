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

namespace DVLD.Applications.Local_Driving_License
{
    public partial class frmListLocalDrivingLicenseApplications : Form
    {
        public frmListLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        private DataTable _dtLocalDrivingLicenseApplications = new DataTable();

        private void _DesignDataGridView()
        {
            Color primaryColor = Color.FromArgb(233, 93, 53); // Your new color
            Color lightPrimary = Color.FromArgb(255, 224, 214); // soft selection
            Color alternateRow = Color.FromArgb(255, 245, 240); // very light orange

            // Background
            dgvLocalDrivingLicenseApplications.BackgroundColor = Color.White;
            dgvLocalDrivingLicenseApplications.BorderStyle = BorderStyle.None;

            // Header
            dgvLocalDrivingLicenseApplications.ColumnHeadersDefaultCellStyle.SelectionBackColor = primaryColor;
            dgvLocalDrivingLicenseApplications.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            dgvLocalDrivingLicenseApplications.ColumnHeadersDefaultCellStyle.BackColor = primaryColor;
            dgvLocalDrivingLicenseApplications.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLocalDrivingLicenseApplications.ColumnHeadersDefaultCellStyle.Padding = new Padding(4);
            dgvLocalDrivingLicenseApplications.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);

            dgvLocalDrivingLicenseApplications.ColumnHeadersHeight = 40;
            dgvLocalDrivingLicenseApplications.EnableHeadersVisualStyles = false;
            dgvLocalDrivingLicenseApplications.AdvancedColumnHeadersBorderStyle.Bottom =
                DataGridViewAdvancedCellBorderStyle.Single;
            dgvLocalDrivingLicenseApplications.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // Rows
            dgvLocalDrivingLicenseApplications.DefaultCellStyle.BackColor = Color.White;
            dgvLocalDrivingLicenseApplications.DefaultCellStyle.ForeColor = Color.Black;
            dgvLocalDrivingLicenseApplications.DefaultCellStyle.SelectionBackColor = lightPrimary;
            dgvLocalDrivingLicenseApplications.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvLocalDrivingLicenseApplications.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            dgvLocalDrivingLicenseApplications.AlternatingRowsDefaultCellStyle.BackColor = alternateRow;

            dgvLocalDrivingLicenseApplications.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvLocalDrivingLicenseApplications.GridColor = Color.FromArgb(220, 220, 220);

            dgvLocalDrivingLicenseApplications.RowTemplate.Height = 35;
            dgvLocalDrivingLicenseApplications.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLocalDrivingLicenseApplications.AllowUserToAddRows = false;

            dgvLocalDrivingLicenseApplications.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLocalDrivingLicenseApplications.MultiSelect = false;
        }

        private void _ConfigureLicenseApplicationsGrid()
        {

            _dtLocalDrivingLicenseApplications =
                clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();

            dgvLocalDrivingLicenseApplications.DataSource = _dtLocalDrivingLicenseApplications;
            lblTotalRecordsCount.Text =
                dgvLocalDrivingLicenseApplications.Rows.Count.ToString() + " Record(s)";

            if (dgvLocalDrivingLicenseApplications.Rows.Count > 0)
            {
                dgvLocalDrivingLicenseApplications.Columns["LocalDrivingLicenseApplicationID"].HeaderText = "Local App ID";
                dgvLocalDrivingLicenseApplications.Columns["LocalDrivingLicenseApplicationID"].Width = 90;

                dgvLocalDrivingLicenseApplications.Columns["ClassName"].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplications.Columns["ClassName"].Width = 130;

                dgvLocalDrivingLicenseApplications.Columns["NationalNo"].HeaderText = "National No";
                dgvLocalDrivingLicenseApplications.Columns["NationalNo"].Width = 110;

                dgvLocalDrivingLicenseApplications.Columns["FullName"].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplications.Columns["FullName"].Width = 180;

                dgvLocalDrivingLicenseApplications.Columns["ApplicationDate"].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplications.Columns["ApplicationDate"].Width = 130;

                dgvLocalDrivingLicenseApplications.Columns["PassedTests"].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplications.Columns["PassedTests"].Width = 95;

                dgvLocalDrivingLicenseApplications.Columns["Status"].HeaderText = "Status";
                dgvLocalDrivingLicenseApplications.Columns["Status"].Width = 100;
            }

        }

        private void _FillFilterColumnsComboBox()
        {
            cbFilterBy.Items.Add("None");
            cbFilterBy.Items.Add("Local App ID");
            cbFilterBy.Items.Add("National No");
            cbFilterBy.Items.Add("Full Name");
            cbFilterBy.Items.Add("Status");
        }

        private void frmListLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _DesignDataGridView();
            _ConfigureLicenseApplicationsGrid();
            _FillFilterColumnsComboBox();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
            else
                lblFilteredRecordsCount.Text = "0 Record(s)";

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {

            string filterColumn = "";

            switch (cbFilterBy.Text)
            {

                case "Local App ID":
                    filterColumn = "LocalDrivingLicenseApplicationID";
                    break;

                case "National No":
                    filterColumn = "NationalNo";
                    break;

                case "Full Name":
                    filterColumn = "FullName";
                    break;

                case "Status":
                    filterColumn = "Status";
                    break;

                default:
                    filterColumn = "None";
                    break;

            }

            if (filterColumn == "None" || txtFilterValue.Text.Trim() == "")
            {
                _dtLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
                lblFilteredRecordsCount.Text = "0 Record(s)";
                return;
            }

            if (filterColumn == "LocalDrivingLicenseApplicationID")
                _dtLocalDrivingLicenseApplications.DefaultView.RowFilter = $"[{filterColumn}] = {txtFilterValue.Text.Trim()}";
            else
                _dtLocalDrivingLicenseApplications.DefaultView.RowFilter = $"[{filterColumn}] LIKE '{txtFilterValue.Text.Trim()}%'";

            lblFilteredRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString() + " Record(s)";

        }

        private void _RefreshGridData()
        {
            _dtLocalDrivingLicenseApplications =
                clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvLocalDrivingLicenseApplications.DataSource = _dtLocalDrivingLicenseApplications;
            lblTotalRecordsCount.Text =
                dgvLocalDrivingLicenseApplications.Rows.Count.ToString() + " Record(s)";
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {


            int ApplicationID = -1;
            int LocalAppID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);

            if (clsLocalDrivingLicenseApplication.GetApplicationID(LocalAppID, ref ApplicationID))
            {

                if (MessageBox.Show("Are you sure you want to cancel this application?",
                                    "Confirm",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question
                                    ) == DialogResult.Yes)
                {
                    if (clsLocalDrivingLicenseApplication.Cancel(ApplicationID))
                    {
                        MessageBox.Show("Application cancelled successfully.", "Succeeded",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);


                        _RefreshGridData();
                    }
                    else
                        MessageBox.Show("Failed to cancel the application.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);


                }
                else
                    MessageBox.Show("Could not find the associated Application ID for this record.", "Not Found",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        private void btnAddLocalApp_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication();
            frm.ShowDialog();
            _RefreshGridData();
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            int LocalAppID = 
                Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);

            frmAddUpdateLocalDrivingLicenseApplication frm =
                new frmAddUpdateLocalDrivingLicenseApplication(LocalAppID);

            frm.ShowDialog();
            _RefreshGridData();
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int LocalAppID =
                Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);

            DialogResult result = MessageBox.Show(
                                                  $"Are you sure you want to delete the application with ID {LocalAppID}?",
                                                  "Confirm Deletion",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning
                                                  );

            if (result == DialogResult.Yes)
            {
                
                if (clsLocalDrivingLicenseApplication.Delete(LocalAppID))
                {
                    _RefreshGridData();
                    MessageBox.Show(
                                    "The application was deleted successfully.",
                                    "Deleted",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information
                                    );
                }
                else
                    MessageBox.Show(
                                    "Failed to delete the application. It may be linked to other records or no longer exists.",
                                    "Deletion Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                    );

            }
        }

    }
    
}