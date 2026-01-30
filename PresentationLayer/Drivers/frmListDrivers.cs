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

namespace DVLD.Drivers
{
    public partial class frmListDrivers : Form
    {

        private DataTable _dtDrivers;

        public frmListDrivers()
        {
            InitializeComponent();
        }

        private void _DesignDataGridView()
        {

            // Background
            dgvDrivers.BackgroundColor = Color.White;
            dgvDrivers.BorderStyle = BorderStyle.None;

            // Header
            dgvDrivers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 103, 178);
            dgvDrivers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDrivers.ColumnHeadersDefaultCellStyle.Padding = new Padding(4);
            dgvDrivers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvDrivers.ColumnHeadersHeight = 40;
            dgvDrivers.EnableHeadersVisualStyles = false;
            dgvDrivers.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single;
            dgvDrivers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // Rows
            dgvDrivers.DefaultCellStyle.BackColor = Color.White;
            dgvDrivers.DefaultCellStyle.ForeColor = Color.Black;
            dgvDrivers.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
            dgvDrivers.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvDrivers.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            dgvDrivers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255);

            dgvDrivers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDrivers.GridColor = Color.LightGray;

            dgvDrivers.RowTemplate.Height = 35;
            dgvDrivers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDrivers.AllowUserToAddRows = false;

            dgvDrivers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDrivers.MultiSelect = false;
        }

        private void _ConfigureDriversGrid()
        {

            _dtDrivers = clsDriver.GetAllDrivers();
            dgvDrivers.DataSource = _dtDrivers;
            lblRecordsCount.Text = $"{dgvDrivers.Rows.Count} Record(s)";
            lblFilteredRecordsCount.Text = "0 Record(s)";

            if (dgvDrivers.SelectedRows.Count > 0 )
            {
                // Set header text
                dgvDrivers.Columns["DriverID"].HeaderText = "Driver ID";
                dgvDrivers.Columns["PersonID"].HeaderText = "Person ID";
                dgvDrivers.Columns["NationalNo"].HeaderText = "National No";
                dgvDrivers.Columns["FullName"].HeaderText = "Full Name";
                dgvDrivers.Columns["Date"].HeaderText = "Date";
                dgvDrivers.Columns["ActiveLicenses"].HeaderText = "Active Licenses";

                // Assign relative weights
                dgvDrivers.Columns["DriverID"].FillWeight = 10;
                dgvDrivers.Columns["PersonID"].FillWeight = 10;
                dgvDrivers.Columns["NationalNo"].FillWeight = 20;
                dgvDrivers.Columns["FullName"].FillWeight = 30;
                dgvDrivers.Columns["Date"].FillWeight = 15;
                dgvDrivers.Columns["ActiveLicenses"].FillWeight = 15;
            }

        }

        private void _FillFilterColumnsComboBox()
        {

            cbFilterBy.Items.Clear();
            cbFilterBy.Items.Add("None");

            foreach (DataGridViewColumn col in dgvDrivers.Columns)
            {
                if (col.HeaderText == "Date")
                    break;
                else
                    cbFilterBy.Items.Add(col.HeaderText);
            }

            cbFilterBy.SelectedIndex = 0;

        }

        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            _DesignDataGridView();
            _ConfigureDriversGrid();
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

            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No":
                    FilterColumn = "NationalNo";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtDrivers.DefaultView.RowFilter = "";
                lblFilteredRecordsCount.Text = "0 Record(s)";
                return;
            }

            if (FilterColumn == "DriverID" || FilterColumn == "PersonID")
                _dtDrivers.DefaultView.RowFilter = $"[{FilterColumn}] = {txtFilterValue.Text.Trim()}";
            else
                _dtDrivers.DefaultView.RowFilter = $"[{FilterColumn}] LIKE '{txtFilterValue.Text.Trim()}%'";

            lblFilteredRecordsCount.Text = $"{dgvDrivers.Rows.Count} Record(s)";

        }
    }
}
