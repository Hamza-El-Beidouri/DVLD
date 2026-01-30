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

namespace DVLD.Licenses.Controls
{
    public partial class ctrlDriverLicenses : UserControl
    {

        private int _PersonID = -1;
        private clsDriver _Driver;
        private DataTable _dtLocalLicenses;
        private DataTable _dtInternationalLicenses;

        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }

        private void _DesignLocalLicensesDataGridView()
        {
            // Background
            dgvLocalLicenses.BackgroundColor = Color.White;
            dgvLocalLicenses.BorderStyle = BorderStyle.None;

            // Header
            dgvLocalLicenses.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 103, 178);
            dgvLocalLicenses.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLocalLicenses.ColumnHeadersDefaultCellStyle.Padding = new Padding(4);
            dgvLocalLicenses.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvLocalLicenses.ColumnHeadersHeight = 40;
            dgvLocalLicenses.EnableHeadersVisualStyles = false;
            dgvLocalLicenses.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single;
            dgvLocalLicenses.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // Rows
            dgvLocalLicenses.DefaultCellStyle.BackColor = Color.White;
            dgvLocalLicenses.DefaultCellStyle.ForeColor = Color.Black;
            dgvLocalLicenses.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
            dgvLocalLicenses.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvLocalLicenses.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            dgvLocalLicenses.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255);

            dgvLocalLicenses.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvLocalLicenses.GridColor = Color.LightGray;

            dgvLocalLicenses.RowTemplate.Height = 35;
            dgvLocalLicenses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLocalLicenses.AllowUserToAddRows = false;

            dgvLocalLicenses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLocalLicenses.MultiSelect = false;
        }

        private void _DesignInternationalLicensesDataGridView()
        {
            // Background
            dgvInternationalLicenses.BackgroundColor = Color.White;
            dgvInternationalLicenses.BorderStyle = BorderStyle.None;

            // Header
            dgvInternationalLicenses.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 103, 178);
            dgvInternationalLicenses.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvInternationalLicenses.ColumnHeadersDefaultCellStyle.Padding = new Padding(4);
            dgvInternationalLicenses.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvInternationalLicenses.ColumnHeadersHeight = 40;
            dgvInternationalLicenses.EnableHeadersVisualStyles = false;
            dgvInternationalLicenses.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single;
            dgvInternationalLicenses.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // Rows
            dgvInternationalLicenses.DefaultCellStyle.BackColor = Color.White;
            dgvInternationalLicenses.DefaultCellStyle.ForeColor = Color.Black;
            dgvInternationalLicenses.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
            dgvInternationalLicenses.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvInternationalLicenses.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            dgvInternationalLicenses.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255);

            dgvInternationalLicenses.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvInternationalLicenses.GridColor = Color.LightGray;

            dgvInternationalLicenses.RowTemplate.Height = 35;
            dgvInternationalLicenses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInternationalLicenses.AllowUserToAddRows = false;

            dgvInternationalLicenses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInternationalLicenses.MultiSelect = false;
        }

        private void _ConfigureLocalLicensesGrid()
        {

            _dtLocalLicenses = clsLicense.GetLicensesByDriverID(_Driver.DriverID);
            dgvLocalLicenses.DataSource = _dtLocalLicenses;
            lblLocalLicensesCount.Text = $"{dgvLocalLicenses.Rows.Count} Record(s)";
            
            if (dgvLocalLicenses.Rows.Count > 0)
            {
                dgvLocalLicenses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgvLocalLicenses.Columns["LicenseID"].HeaderText = "License ID";
                dgvLocalLicenses.Columns["ApplicationID"].HeaderText = "Application ID";
                dgvLocalLicenses.Columns["ClassName"].HeaderText = "License Class";
                dgvLocalLicenses.Columns["IssueDate"].HeaderText = "Issue Date";
                dgvLocalLicenses.Columns["ExpirationDate"].HeaderText = "Expiration Date";
                dgvLocalLicenses.Columns["IsActive"].HeaderText = "Active";

                dgvLocalLicenses.Columns["LicenseID"].FillWeight = 12;
                dgvLocalLicenses.Columns["ApplicationID"].FillWeight = 12;
                dgvLocalLicenses.Columns["ClassName"].FillWeight = 30;
                dgvLocalLicenses.Columns["IssueDate"].FillWeight = 15;
                dgvLocalLicenses.Columns["ExpirationDate"].FillWeight = 15;
                dgvLocalLicenses.Columns["IsActive"].FillWeight = 8;

            }

        }

        private void _ConfigureInternationalLicensesGrid()
        {
            _dtInternationalLicenses = clsInternationalLicense.GetAllInternationalLicensesByDriverID(_Driver.DriverID);
            dgvInternationalLicenses.DataSource = _dtInternationalLicenses;
            lblInternationalLicensesCount.Text = $"{dgvInternationalLicenses.Rows.Count} Record(s)";

            if (dgvInternationalLicenses.Rows.Count > 0)
            {
                dgvInternationalLicenses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgvInternationalLicenses.Columns["InternationalLicenseID"].HeaderText = "International License ID";
                dgvInternationalLicenses.Columns["ApplicationID"].HeaderText = "Application ID";
                dgvInternationalLicenses.Columns["LocalLicenseID"].HeaderText = "Local License ID";
                dgvInternationalLicenses.Columns["IssueDate"].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns["ExpirationDate"].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns["IsActive"].HeaderText = "Active";

                dgvInternationalLicenses.Columns["InternationalLicenseID"].FillWeight = 20;
                dgvInternationalLicenses.Columns["ApplicationID"].FillWeight = 15;
                dgvInternationalLicenses.Columns["LocalLicenseID"].FillWeight = 20;
                dgvInternationalLicenses.Columns["IssueDate"].FillWeight = 15;
                dgvInternationalLicenses.Columns["ExpirationDate"].FillWeight = 15;
                dgvInternationalLicenses.Columns["IsActive"].FillWeight = 10;
            }
        }

        private void _DesignDataGridViews()
        {
            _DesignLocalLicensesDataGridView();
            _DesignInternationalLicensesDataGridView();
        }

        private void _ConfigureDataGridViews()
        {
            _ConfigureLocalLicensesGrid();
            _ConfigureInternationalLicensesGrid();
        }

        private void _LoadData()
        {
            _DesignDataGridViews();
            _ConfigureDataGridViews();
        }

        public void LoadDriverLicenses(int PersonID)
        {
            _PersonID = PersonID;
            _Driver = clsDriver.FindByPersonID(_PersonID);
            _LoadData();
        }

    }
}