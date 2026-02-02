using DVLD.Licenses.Detained_Licenses;
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

namespace DVLD.Applications.Release_Detained_Licenses
{
    public partial class frmListDetainedLicenses : Form
    {

        private DataTable _dtDetainedLicenses;

        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void _DesignDataGridView()
        {

            // Background
            dgvDetainedLicenses.BackgroundColor = Color.White;
            dgvDetainedLicenses.BorderStyle = BorderStyle.None;

            // Header
            dgvDetainedLicenses.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 103, 178);
            dgvDetainedLicenses.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDetainedLicenses.ColumnHeadersDefaultCellStyle.Padding = new Padding(4);
            dgvDetainedLicenses.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvDetainedLicenses.ColumnHeadersHeight = 40;
            dgvDetainedLicenses.EnableHeadersVisualStyles = false;
            dgvDetainedLicenses.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single;
            dgvDetainedLicenses.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // Rows
            dgvDetainedLicenses.DefaultCellStyle.BackColor = Color.White;
            dgvDetainedLicenses.DefaultCellStyle.ForeColor = Color.Black;
            dgvDetainedLicenses.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
            dgvDetainedLicenses.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvDetainedLicenses.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            dgvDetainedLicenses.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255);

            dgvDetainedLicenses.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDetainedLicenses.GridColor = Color.LightGray;

            dgvDetainedLicenses.RowTemplate.Height = 35;
            dgvDetainedLicenses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetainedLicenses.AllowUserToAddRows = false;

            dgvDetainedLicenses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetainedLicenses.MultiSelect = false;
        }

        private void _ConfigureDetainedLicensesGrid()
        {
            _dtDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();
            dgvDetainedLicenses.DataSource = _dtDetainedLicenses;

            lblRecordsCount.Text = $"{dgvDetainedLicenses.Rows.Count} Record(s)";

            if (dgvDetainedLicenses.Rows.Count > 0)
            {
                dgvDetainedLicenses.Columns["DetainID"].HeaderText = "Detain ID";
                dgvDetainedLicenses.Columns["DetainID"].FillWeight = 80;

                dgvDetainedLicenses.Columns["LicenseID"].HeaderText = "License ID";
                dgvDetainedLicenses.Columns["LicenseID"].FillWeight = 100;

                dgvDetainedLicenses.Columns["DetainDate"].HeaderText = "Detain Date";
                dgvDetainedLicenses.Columns["DetainDate"].FillWeight = 120;

                dgvDetainedLicenses.Columns["IsReleased"].HeaderText = "Released";
                dgvDetainedLicenses.Columns["IsReleased"].FillWeight = 80;

                dgvDetainedLicenses.Columns["FineFees"].HeaderText = "Fine Fees";
                dgvDetainedLicenses.Columns["FineFees"].FillWeight = 90;

                dgvDetainedLicenses.Columns["ReleaseDate"].HeaderText = "Release Date";
                dgvDetainedLicenses.Columns["ReleaseDate"].FillWeight = 120;

                dgvDetainedLicenses.Columns["NationalNo"].HeaderText = "National No.";
                dgvDetainedLicenses.Columns["NationalNo"].FillWeight = 120;

                dgvDetainedLicenses.Columns["FullName"].HeaderText = "Full Name";
                dgvDetainedLicenses.Columns["FullName"].FillWeight = 180;

                dgvDetainedLicenses.Columns["ReleaseApplicationID"].HeaderText = "Release App. ID";
                dgvDetainedLicenses.Columns["ReleaseApplicationID"].FillWeight = 120;

                // Optional: Autosize columns based on FillWeight
                dgvDetainedLicenses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void _FillFilterColumnsComboBox()
        {

            cbFilterBy.Items.Clear();
            cbFilterBy.Items.Add("None");
            cbFilterBy.Items.Add("Detain ID");
            cbFilterBy.Items.Add("Is Released");
            cbFilterBy.Items.Add("National No.");
            cbFilterBy.Items.Add("Full Name");
            cbFilterBy.Items.Add("Release Application ID");

            // Optional: default selection
            cbFilterBy.SelectedIndex = 0;
        }

        private void _FillReleaseFilterComboBox()
        {
            cbReleaseStatus.Items.Clear();
            cbReleaseStatus.Items.Add("All");
            cbReleaseStatus.Items.Add("Yes");
            cbReleaseStatus.Items.Add("No");
            cbReleaseStatus.SelectedIndex = 0;
        }

        private void _RefreshDetainedLicensesGrid()
        {
            _dtDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();
            dgvDetainedLicenses.DataSource = _dtDetainedLicenses;

            lblRecordsCount.Text = $"{dgvDetainedLicenses.Rows.Count} Record(s)";
        }

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            _DesignDataGridView();
            _ConfigureDetainedLicensesGrid();
            _FillFilterColumnsComboBox();
            _FillReleaseFilterComboBox();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtFilterValue.Visible = (cbFilterBy.Text != "None" && cbFilterBy.Text != "Is Released");
            cbReleaseStatus.Visible = (cbFilterBy.Text == "Is Released");

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
            string FilterColumn = "";

            // Map Selected Filter to real Column name
            switch (cbFilterBy.Text)
            {
                case "Detain ID":
                    FilterColumn = "DetainID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Release Application ID":
                    FilterColumn = "ReleaseApplicationID";
                    break;

                default:
                    FilterColumn = "None"; // Skip "Is Released" and "None"
                    break;
            }

            // Reset filters if nothing selected or filter value is empty
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtDetainedLicenses.DefaultView.RowFilter = "";
                lblFilteredRecordsCount.Text = "0 Record(s)";
                return;
            }

            // If integer column, filter as number
            if (FilterColumn == "DetainID" || FilterColumn == "ReleaseApplicationID")
                _dtDetainedLicenses.DefaultView.RowFilter = $"[{FilterColumn}] = {txtFilterValue.Text.Trim()}";
            else // String columns
                _dtDetainedLicenses.DefaultView.RowFilter = $"[{FilterColumn}] LIKE '{txtFilterValue.Text.Trim()}%'";

            lblFilteredRecordsCount.Text = dgvDetainedLicenses.Rows.Count.ToString() + " Record(s)";
        }

        private void cbReleaseStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

            string filter = "";

            switch (cbReleaseStatus.Text)
            {
                case "Yes":
                    filter = "[IsReleased] = True"; // RowFilter expects boolean literal
                    break;

                case "No":
                    filter = "[IsReleased] = False";
                    break;

                case "All":
                default:
                    filter = ""; // no filter
                    break;
            }

            _dtDetainedLicenses.DefaultView.RowFilter = filter;

            lblFilteredRecordsCount.Text = dgvDetainedLicenses.Rows.Count.ToString() + " Record(s)";

        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
            _RefreshDetainedLicensesGrid();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
            _RefreshDetainedLicensesGrid();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            bool isReleased = Convert.ToBoolean(dgvDetainedLicenses.CurrentRow.Cells[3].Value);
            releaseDetainedLicenseToolStripMenuItem.Enabled = isReleased;
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string NationalNumber = dgvDetainedLicenses.CurrentRow.Cells[6].Value.ToString();

            frmPersonInfo frm = new frmPersonInfo(clsPerson.Find(NationalNumber).PersonID);

            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = Convert.ToInt32(dgvDetainedLicenses.CurrentRow.Cells[1].Value);

            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);

            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string NationalNumber = dgvDetainedLicenses.CurrentRow.Cells[6].Value.ToString();

            frmShowPersonLicenseHistory frm =
                new frmShowPersonLicenseHistory(clsPerson.Find(NationalNumber).PersonID);

            frm.ShowDialog();
        }
        
        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = Convert.ToInt32(dgvDetainedLicenses.CurrentRow.Cells[1].Value);
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense(LicenseID);
            frm.ShowDialog();
            _RefreshDetainedLicensesGrid();
        }
    }
}