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

namespace DVLD.TestTypes
{
    public partial class frmListTestTypes : Form
    {
        public frmListTestTypes()
        {
            InitializeComponent();
        }

        private DataTable _dtAllTestTypes;

        private void _DesignDataGridView()
        {
            // ================= MAIN SETTINGS =================
            dgvTestTypes.BackgroundColor = Color.White;
            dgvTestTypes.BorderStyle = BorderStyle.None;

            // 🔥 FIX: Disable scrollbars to prevent them from appearing unexpectedly
            dgvTestTypes.ScrollBars = ScrollBars.None;

            // Prevent users from messing with the layout manually
            dgvTestTypes.AllowUserToAddRows = false;
            dgvTestTypes.AllowUserToDeleteRows = false;
            dgvTestTypes.AllowUserToResizeRows = false;
            dgvTestTypes.AllowUserToResizeColumns = false;

            // ================= HEADER DESIGN =================
            dgvTestTypes.ColumnHeadersHeight = 40;
            dgvTestTypes.EnableHeadersVisualStyles = false;

            dgvTestTypes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(148, 157, 222);
            dgvTestTypes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTestTypes.ColumnHeadersDefaultCellStyle.Padding = new Padding(4);
            dgvTestTypes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgvTestTypes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvTestTypes.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single;

            dgvTestTypes.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(148, 157, 222);
            dgvTestTypes.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            // ================= ROW DESIGN =================
            dgvTestTypes.DefaultCellStyle.BackColor = Color.White;
            dgvTestTypes.DefaultCellStyle.ForeColor = Color.FromArgb(45, 45, 60);
            dgvTestTypes.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            // Center text vertically for a better look
            dgvTestTypes.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvTestTypes.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Selection Colors
            dgvTestTypes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTestTypes.MultiSelect = false;
            dgvTestTypes.DefaultCellStyle.SelectionBackColor = Color.FromArgb(205, 210, 245);
            dgvTestTypes.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 30, 50);

            dgvTestTypes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 242, 255);

            // ================= GRID LINES =================
            dgvTestTypes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvTestTypes.GridColor = Color.FromArgb(215, 220, 245);

            // ================= SIZING MODES =================
            dgvTestTypes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvTestTypes.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

        }

        private void _ConfigureDataGridView()
        {
            _dtAllTestTypes = clsTestType.GetAllTestTypes();
            dgvTestTypes.DataSource = _dtAllTestTypes;

            // 1. CONFIGURE COLUMNS
            if (dgvTestTypes.Rows.Count > 0)
            {
                dgvTestTypes.Columns["TestTypeID"].HeaderText = "ID";
                dgvTestTypes.Columns["TestTypeID"].Width = 60;

                dgvTestTypes.Columns["TestTypeTitle"].HeaderText = "Title";
                dgvTestTypes.Columns["TestTypeTitle"].Width = 180;

                dgvTestTypes.Columns["TestTypeDescription"].HeaderText = "Description";
                dgvTestTypes.Columns["TestTypeDescription"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgvTestTypes.Columns["TestTypeFees"].HeaderText = "Fees";
                dgvTestTypes.Columns["TestTypeFees"].Width = 120;
            }

            // 2. CALCULATE ROW HEIGHT
            // Subtract 2 pixels as a buffer to ensure borders don't trigger overflow
            int totalAvailableHeight = dgvTestTypes.ClientSize.Height - dgvTestTypes.ColumnHeadersHeight - 2;
            int rowCount = dgvTestTypes.Rows.Count;

            if (rowCount > 0)
            {
                int defaultRowHeight = totalAvailableHeight / rowCount;
                int remainder = totalAvailableHeight % rowCount;

                for (int i = 0; i < rowCount; i++)
                {
                    // If it's the last row, add the remainder pixels to close the gap perfectly
                    if (i == rowCount - 1)
                    {
                        dgvTestTypes.Rows[i].Height = defaultRowHeight + remainder;
                    }
                    else
                    {
                        dgvTestTypes.Rows[i].Height = defaultRowHeight;
                    }
                }
            }

            // 🔥 FIX: DISABLE SORTING FOR ALL COLUMNS
            // This prevents the rows from reordering and the header from looking "pressed"
            foreach (DataGridViewColumn column in dgvTestTypes.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }

        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            _DesignDataGridView();
            _ConfigureDataGridView();
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditTestType frm = new frmEditTestType((clsTestType.enTestType)dgvTestTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListTestTypes_Load(null, null);
        }
    }
}
