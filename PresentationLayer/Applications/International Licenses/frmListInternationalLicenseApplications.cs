using DVLD.Licenses.International_Licenses;
using DVLD.Licenses.Local_Licenses;
using DVLD.People;
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

namespace DVLD.Applications.International_Licenses
{
    public partial class frmListInternationalLicenseApplications : Form
    {

        private DataTable _dtInternationalLicenseApplications = new DataTable();

        public frmListInternationalLicenseApplications()
        {
            InitializeComponent();
        }

        private void _DesignDataGridView()
        {
            Color primaryColor = Color.FromArgb(233, 93, 53); // Your new color
            Color lightPrimary = Color.FromArgb(255, 224, 214); // soft selection
            Color alternateRow = Color.FromArgb(255, 245, 240); // very light orange

            // Background
            dgvInternationalLicenseApplications.BackgroundColor = Color.White;
            dgvInternationalLicenseApplications.BorderStyle = BorderStyle.None;

            // Header
            dgvInternationalLicenseApplications.ColumnHeadersDefaultCellStyle.SelectionBackColor = primaryColor;
            dgvInternationalLicenseApplications.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            dgvInternationalLicenseApplications.ColumnHeadersDefaultCellStyle.BackColor = primaryColor;
            dgvInternationalLicenseApplications.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvInternationalLicenseApplications.ColumnHeadersDefaultCellStyle.Padding = new Padding(4);
            dgvInternationalLicenseApplications.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);

            dgvInternationalLicenseApplications.ColumnHeadersHeight = 40;
            dgvInternationalLicenseApplications.EnableHeadersVisualStyles = false;
            dgvInternationalLicenseApplications.AdvancedColumnHeadersBorderStyle.Bottom =
                DataGridViewAdvancedCellBorderStyle.Single;
            dgvInternationalLicenseApplications.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // Rows
            dgvInternationalLicenseApplications.DefaultCellStyle.BackColor = Color.White;
            dgvInternationalLicenseApplications.DefaultCellStyle.ForeColor = Color.Black;
            dgvInternationalLicenseApplications.DefaultCellStyle.SelectionBackColor = lightPrimary;
            dgvInternationalLicenseApplications.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvInternationalLicenseApplications.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            dgvInternationalLicenseApplications.AlternatingRowsDefaultCellStyle.BackColor = alternateRow;

            dgvInternationalLicenseApplications.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvInternationalLicenseApplications.GridColor = Color.FromArgb(220, 220, 220);

            dgvInternationalLicenseApplications.RowTemplate.Height = 35;
            dgvInternationalLicenseApplications.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInternationalLicenseApplications.AllowUserToAddRows = false;

            dgvInternationalLicenseApplications.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInternationalLicenseApplications.MultiSelect = false;
        }

        private void _ConfigureLicenseApplicationsGrid()
        {
            
            _dtInternationalLicenseApplications = clsApplication.GetAllInternationalLicenseApplications();
            dgvInternationalLicenseApplications.DataSource = _dtInternationalLicenseApplications;

            lblTotalRecordsCount.Text = $"{dgvInternationalLicenseApplications.Rows.Count} Record(s)";
            

            if (dgvInternationalLicenseApplications.Rows.Count > 0)
            {
                dgvInternationalLicenseApplications.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;

                dgvInternationalLicenseApplications.Columns["InternationalLicenseID"].HeaderText =
                    "International ID";
                dgvInternationalLicenseApplications.Columns["InternationalLicenseID"].FillWeight = 15;

                dgvInternationalLicenseApplications.Columns["ApplicationID"].HeaderText =
                    "Application ID";
                dgvInternationalLicenseApplications.Columns["ApplicationID"].FillWeight = 15;

                dgvInternationalLicenseApplications.Columns["LocalLicenseID"].HeaderText =
                    "Local License ID";
                dgvInternationalLicenseApplications.Columns["LocalLicenseID"].FillWeight = 18;

                dgvInternationalLicenseApplications.Columns["IssueDate"].HeaderText =
                    "Issue Date";
                dgvInternationalLicenseApplications.Columns["IssueDate"].FillWeight = 18;

                dgvInternationalLicenseApplications.Columns["ExpirationDate"].HeaderText =
                    "Expiration Date";
                dgvInternationalLicenseApplications.Columns["ExpirationDate"].FillWeight = 18;

                dgvInternationalLicenseApplications.Columns["IsActive"].HeaderText =
                    "Active";
                dgvInternationalLicenseApplications.Columns["IsActive"].FillWeight = 8;
            }

        }

        private void _FillFilterColumnsComboBox()
        {

            cbFilterBy.Items.Add("None");

            foreach (DataGridViewColumn col in dgvInternationalLicenseApplications.Columns)
            {
                
                // Skip date columns
                if (col.Name == "IssueDate" || col.Name == "ExpirationDate")
                    continue;
                else
                    cbFilterBy.Items.Add(col.HeaderText);
            }

            cbFilterBy.SelectedIndex = 0;
        }

        private void _FillActiveStatusComboBox()
        {
            cbActiveStatus.Items.Add("All");
            cbActiveStatus.Items.Add("Yes");
            cbActiveStatus.Items.Add("No");
            cbActiveStatus.SelectedIndex = 0;
        }

        private void frmListInternationalLicenseApplications_Load(object sender, EventArgs e)
        {
            _DesignDataGridView();
            _ConfigureLicenseApplicationsGrid();
            _FillFilterColumnsComboBox();
            _FillActiveStatusComboBox();
            lblFilteredRecordsCount.Text = "0 Record(s)";
        }

        private void _RefreshGridData()
        {
            _dtInternationalLicenseApplications = clsApplication.GetAllInternationalLicenseApplications();
            dgvInternationalLicenseApplications.DataSource = _dtInternationalLicenseApplications;
            lblTotalRecordsCount.Text = $"{dgvInternationalLicenseApplications.Rows.Count} Record(s)";
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtFilterValue.Visible = (cbFilterBy.Text != "None" && cbFilterBy.Text != "Active");
            cbActiveStatus.Visible = cbFilterBy.Text == "Active";


            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
            else
            {
                _dtInternationalLicenseApplications.DefaultView.RowFilter = "";
                lblFilteredRecordsCount.Text = "0 Record(s)";
            }

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {

            string filterColumn = "";

            switch (cbFilterBy.Text)
            {

                case "International ID":
                    filterColumn = "InternationalLicenseID";
                    break;

                case "Application ID":
                    filterColumn = "ApplicationID";
                    break;

                case "Local License ID":
                    filterColumn = "LocalLicenseID";
                    break;

                default:
                    filterColumn = "None";
                    break;

            }

            if (filterColumn == "None" || txtFilterValue.Text.Trim() == "")
            {
                _dtInternationalLicenseApplications.DefaultView.RowFilter = "";
                lblFilteredRecordsCount.Text = "0 Record(s)";
                return;
            }

            _dtInternationalLicenseApplications.DefaultView.RowFilter = $"[{filterColumn}] = {txtFilterValue.Text.Trim()}";
            lblFilteredRecordsCount.Text = $"{dgvInternationalLicenseApplications.Rows.Count} Record(s)";

        }

        private void cbActiveStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

            string filterColumn = "IsActive";

            switch (cbActiveStatus.Text)
            {
                case "Yes":
                    _dtInternationalLicenseApplications.DefaultView.RowFilter = $"[{filterColumn}] = true";
                    break;

                case "No":
                    _dtInternationalLicenseApplications.DefaultView.RowFilter = $"[{filterColumn}] = false";
                    break;

                default: // All
                    _dtInternationalLicenseApplications.DefaultView.RowFilter = string.Empty;
                    break;
            }

            lblFilteredRecordsCount.Text =
                $"{_dtInternationalLicenseApplications.DefaultView.Count} Record(s)";
        }

        private void btnAddInternationalLicenseApplication_Click(object sender, EventArgs e)
        {
            frmInternationalLicenseApplication frm = new frmInternationalLicenseApplication();
            frm.ShowDialog();
            _RefreshGridData();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ApplicationID = 
                Convert.ToInt32(dgvInternationalLicenseApplications.CurrentRow.Cells[1].Value);

            int PersonID = clsApplication.FindBaseApplication(ApplicationID).ApplicantPersonID;

            frmPersonInfo frm = new frmPersonInfo(PersonID);
            frm.ShowDialog();
        }

        private void showPersonDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            int InternationalLicenseID =
                Convert.ToInt32(dgvInternationalLicenseApplications.CurrentRow.Cells[0].Value);

            frmInternationalDriverLicenseInfo frm = new frmInternationalDriverLicenseInfo(InternationalLicenseID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int ApplicationID =
                Convert.ToInt32(dgvInternationalLicenseApplications.CurrentRow.Cells[1].Value);

            int PersonID = clsApplication.FindBaseApplication(ApplicationID).ApplicantPersonID;

            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }
    }
}