using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_BusinessLayer;
using DVLD.Global_Classes;

namespace DVLD.Users
{
    public partial class frmListUsers : Form
    {

        private static DataTable _dtAllUsers;

        private void _DesignDataGridView()
        {

            // Background
            dgvUsers.BackgroundColor = Color.White;
            dgvUsers.BorderStyle = BorderStyle.None;

            // Header
            dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 103, 178);
            dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUsers.ColumnHeadersDefaultCellStyle.Padding = new Padding(4);
            dgvUsers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvUsers.ColumnHeadersHeight = 40;
            dgvUsers.EnableHeadersVisualStyles = false;
            dgvUsers.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single;
            dgvUsers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // Rows
            dgvUsers.DefaultCellStyle.BackColor = Color.White;
            dgvUsers.DefaultCellStyle.ForeColor = Color.Black;
            dgvUsers.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
            dgvUsers.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvUsers.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255);

            dgvUsers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvUsers.GridColor = Color.LightGray;

            dgvUsers.RowTemplate.Height = 35;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.AllowUserToAddRows = false;

            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.MultiSelect = false;
        }

        private void _ConfigureUsersGrid()
        {

            _dtAllUsers = clsUser.GetAllUsers();
            dgvUsers.DataSource = _dtAllUsers;
            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString() + " Record(s)";
            lblFilteredRecordsCount.Text = "0 Record(s)";

            if (dgvUsers.Rows.Count > 0 )
            {

                dgvUsers.Columns["UserID"].HeaderText = "User ID";
                dgvUsers.Columns["UserID"].Width = 60;

                dgvUsers.Columns["PersonID"].HeaderText = "Person ID";
                dgvUsers.Columns["PersonID"].Width = 80;

                dgvUsers.Columns["FullName"].HeaderText = "Full Name";
                dgvUsers.Columns["FullName"].Width = 300;

                dgvUsers.Columns["IsActive"].HeaderText = "Is Active";
                dgvUsers.Columns["IsActive"].Width = 30;

                dgvUsers.Columns["UserName"].Width = 100;

            }

        }

        private void _FillFilterColumnsComboBox()
        {
            cbFilterBy.Items.Clear();
            cbFilterBy.Items.Add("None");

            foreach(DataGridViewColumn col in dgvUsers.Columns)
            {
                cbFilterBy.Items.Add(col.HeaderText);
            }

            cbFilterBy.SelectedIndex = 0;

        }

        private void _FillActiveFilterComboBox()
        {
            cbFilterByStatus.Items.Add("All");
            cbFilterByStatus.Items.Add("Yes");
            cbFilterByStatus.Items.Add("No");
            cbFilterByStatus.SelectedIndex = 0;
        }

        private void _RefreshUsersList()
        {

            _dtAllUsers = clsUser.GetAllUsers();

            dgvUsers.DataSource = _dtAllUsers;
            lblRecordsCount.Text = _dtAllUsers.Rows.Count.ToString() + " Record(s)";

        }

        public frmListUsers()
        {
            InitializeComponent();
        }

        private void frmListUsers_Load(object sender, EventArgs e)
        {
            _DesignDataGridView();
            _ConfigureUsersGrid();
            _FillFilterColumnsComboBox();
            _FillActiveFilterComboBox();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 1. Check if we need to apply the filter logic
            if (cbFilterBy.Text != "User ID" && cbFilterBy.Text != "Person ID")
                return;

            // 2. Allow control keys like Backspace and Delete (Mandatory for editing)
            if (char.IsControl(e.KeyChar))
                return;

            // 3. Check if the pressed character is NOT a digit
            //    The e.KeyChar contains the character pressed.
            if (!char.IsDigit(e.KeyChar))
                // Block the character from being entered
                e.Handled = true;
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            cbFilterByStatus.Visible = (cbFilterBy.Text == "Is Active");
            txtFilterValue.Visible = (cbFilterBy.Text != "None" && cbFilterBy.Text != "Is Active");

            if (txtFilterValue.Visible)
            {

                txtFilterValue.Text = "";
                txtFilterValue.Focus();

                switch (cbFilterBy.Text)
                {

                    case "User ID":
                        txtFilterValue.PlaceholderText = "Enter User ID...";
                        break;

                    case "Person ID":
                        txtFilterValue.PlaceholderText = "Enter Person ID...";
                        break;

                    case "UserName":
                        txtFilterValue.PlaceholderText = "Enter Username...";
                        break;

                    case "Full Name":
                        txtFilterValue.PlaceholderText = "Enter Full Name...";
                        break;

                }

            }
            else
                lblFilteredRecordsCount.Text = "0 Record(s)";

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {

            string filterColumn = "";

            switch (cbFilterBy.Text)
            {

                case "User ID":
                    filterColumn = "UserID";
                    break;

                case "Person ID":
                    filterColumn = "PersonID";
                    break;

                case "UserName":
                    filterColumn = "UserName";
                    break;

                case "Full Name":
                    filterColumn = "FullName";
                    break;

                case "IsActive":
                    return;

                default:
                    filterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value contains nothing.
            if (txtFilterValue.Text.Trim() == "" || filterColumn == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lblFilteredRecordsCount.Text = "0 Record(s)";
                return;
            }

            if (filterColumn == "UserID" || filterColumn == "PersonID")
                _dtAllUsers.DefaultView.RowFilter = $"[{filterColumn}] = {txtFilterValue.Text.Trim()}";
            else
                _dtAllUsers.DefaultView.RowFilter = $"[{filterColumn}] LIKE '{txtFilterValue.Text.Trim()}%'";

            lblFilteredRecordsCount.Text = dgvUsers.Rows.Count.ToString() + " Record(s)";

        }

        private void cbActiveFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            bool activeStatus = false;

            switch (cbFilterByStatus.Text)
            {

                case "Yes":
                    activeStatus = true;
                    break;

                case "No":
                    activeStatus = false;
                    break;

                default:
                    _dtAllUsers.DefaultView.RowFilter = string.Empty;
                    lblFilteredRecordsCount.Text = "0 Record(s)";
                    return;

            }

            _dtAllUsers.DefaultView.RowFilter = $"[IsActive] = {activeStatus}";

            lblFilteredRecordsCount.Text = dgvUsers.Rows.Count.ToString() + " Record(s)";

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            Form frmAddUpdateUser = new frmAddUpdateUser();
            frmAddUpdateUser.ShowDialog();
            _RefreshUsersList();
        }

        private void tsmEditUser_Click(object sender, EventArgs e)
        {
            Form frmAddUpdateUser = new frmAddUpdateUser((int)dgvUsers.CurrentRow.Cells[0].Value);
            frmAddUpdateUser.ShowDialog();
            _RefreshUsersList();
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {

            int userID = (int)dgvUsers.CurrentRow.Cells[0].Value;

            string Message = $"Are you sure you want to delete the selected user with ID[{userID}] ?\n\nThis action cannot be undone.";
            string Caption = "Warning";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Warning;

            if (MessageBox.Show(Message, Caption, buttons, icon) == DialogResult.Yes)
            {
                if (clsUser.Delete(userID))
                {
                    MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshUsersList();
                }
                else
                {
                    MessageBox.Show("User was not deleted because it has data linked to it.", "Error: Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void tsmChangePassword_Click(object sender, EventArgs e)
        {            
            Form frmChangePassword = new frmChangePassword((int)dgvUsers.CurrentRow.Cells[0].Value);
            frmChangePassword.ShowDialog();
        }

        private void tsmAddNewUser_Click(object sender, EventArgs e)
        {
            Form frmAddUpdateUser = new frmAddUpdateUser();
            frmAddUpdateUser.ShowDialog();
            _RefreshUsersList();
        }

        private void tsmShowDetails_Click(object sender, EventArgs e)
        {
            frmUserInfo frmUserInfo = new frmUserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            frmUserInfo.ShowDialog();
        }

        private void tsmSendEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!",
                "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tsmPhoneCall_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!",
                "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}