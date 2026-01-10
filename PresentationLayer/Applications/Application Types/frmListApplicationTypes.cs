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

namespace DVLD.Application_Types
{
    public partial class frmListApplicationTypes : Form
    {

        private DataTable _dtApplicationTypes;

        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        private void _DesignDataGridView()
        {
            // Background
            dgvApplicationTypes.BackgroundColor = Color.White;
            dgvApplicationTypes.BorderStyle = BorderStyle.None;

            // Header
            dgvApplicationTypes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(225, 169, 84);
            dgvApplicationTypes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvApplicationTypes.ColumnHeadersDefaultCellStyle.Padding = new Padding(4);
            dgvApplicationTypes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvApplicationTypes.ColumnHeadersHeight = 40;
            dgvApplicationTypes.EnableHeadersVisualStyles = false;
            dgvApplicationTypes.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single;
            dgvApplicationTypes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // 🔥 IMPORTANT: prevent blue selection on header
            dgvApplicationTypes.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                dgvApplicationTypes.ColumnHeadersDefaultCellStyle.BackColor;
            dgvApplicationTypes.ColumnHeadersDefaultCellStyle.SelectionForeColor =
                dgvApplicationTypes.ColumnHeadersDefaultCellStyle.ForeColor;

            // Rows
            dgvApplicationTypes.DefaultCellStyle.BackColor = Color.White;
            dgvApplicationTypes.DefaultCellStyle.ForeColor = Color.Black;
            dgvApplicationTypes.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 214, 155); // light gold
            dgvApplicationTypes.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvApplicationTypes.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            dgvApplicationTypes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 243, 230); // very soft gold tint

            dgvApplicationTypes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvApplicationTypes.GridColor = Color.FromArgb(230, 215, 180); // subtle gold-gray

            dgvApplicationTypes.RowTemplate.Height = 35;
            dgvApplicationTypes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvApplicationTypes.AllowUserToAddRows = false;

            dgvApplicationTypes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvApplicationTypes.MultiSelect = false;

        }

        private void _ConfigureApplicationsTypeGrid()
        {

            _dtApplicationTypes = clsApplicationType.GetAllApplicationTypes();
            dgvApplicationTypes.DataSource = _dtApplicationTypes;

            if (dgvApplicationTypes.Rows.Count > 0 )
            {

                dgvApplicationTypes.Columns["ApplicationTypeID"].HeaderText = "ID";
                dgvApplicationTypes.Columns["ApplicationTypeID"].Width = 50;
                dgvApplicationTypes.Columns["ApplicationTypeTitle"].HeaderText = "Title";
                dgvApplicationTypes.Columns["ApplicationTypeTitle"].Width = 280;

                dgvApplicationTypes.Columns["ApplicationFees"].HeaderText = "Fees";
                dgvApplicationTypes.Columns["ApplicationFees"].Width = 100;

            }

        }

        private void _RefreshApplicationTypesList()
        {
            _dtApplicationTypes = clsApplicationType.GetAllApplicationTypes();
            dgvApplicationTypes.DataSource = _dtApplicationTypes;
        }

        private void frmListApplicationTypes_Load(object sender, EventArgs e)
        {
            _DesignDataGridView();
            _ConfigureApplicationsTypeGrid();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sbyte id = Convert.ToSByte(dgvApplicationTypes.CurrentRow.Cells[0].Value);
            frmEditApplicationType frm = new frmEditApplicationType(id);
            frm.ShowDialog();
            _RefreshApplicationTypesList();
        }
    }
}
