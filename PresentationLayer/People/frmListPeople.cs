using DVLD.Global_Classes;
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
using static Guna.UI2.Native.WinApi;

namespace DVLD
{
    public partial class frmListPeople : Form
    {

        private static DataTable _dtAllPeople = clsPerson.GetAllPeople();

        //only select the columns that you want to show in the grid
        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                         "FirstName", "SecondName", "ThirdName", "LastName",
                                                         "GenderCaption", "DateOfBirth", "CountryName",
                                                         "Phone", "Email");



        private void _DesignDataGridView()
        {

            // Background
            dgvPeople.BackgroundColor = Color.White;
            dgvPeople.BorderStyle = BorderStyle.None;

            // Header
            dgvPeople.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 103, 178);
            dgvPeople.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPeople.ColumnHeadersDefaultCellStyle.Padding = new Padding(4);
            dgvPeople.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvPeople.ColumnHeadersHeight = 40;
            dgvPeople.EnableHeadersVisualStyles = false;
            dgvPeople.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single;
            dgvPeople.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // Rows
            dgvPeople.DefaultCellStyle.BackColor = Color.White;
            dgvPeople.DefaultCellStyle.ForeColor = Color.Black;
            dgvPeople.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
            dgvPeople.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvPeople.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            dgvPeople.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255);

            dgvPeople.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvPeople.GridColor = Color.LightGray;

            dgvPeople.RowTemplate.Height = 35;
            dgvPeople.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPeople.AllowUserToAddRows = false;

            dgvPeople.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPeople.MultiSelect = false;
        }

        private void _ConfigurePeopleGrid()
        {

            dgvPeople.DataSource = _dtPeople;
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString() + " Record(s)";

            if (dgvPeople.Rows.Count > 0)
            {

                dgvPeople.Columns["PersonID"].HeaderText = "Person ID";
                dgvPeople.Columns["PersonID"].Width = 110;

                dgvPeople.Columns["NationalNo"].HeaderText = "National No.";
                dgvPeople.Columns["NationalNo"].Width = 120;


                dgvPeople.Columns["FirstName"].HeaderText = "First Name";
                dgvPeople.Columns["FirstName"].Width = 120;

                dgvPeople.Columns["SecondName"].HeaderText = "Second Name";
                dgvPeople.Columns["SecondName"].Width = 140;


                dgvPeople.Columns["ThirdName"].HeaderText = "Third Name";
                dgvPeople.Columns["ThirdName"].Width = 120;

                dgvPeople.Columns["LastName"].HeaderText = "Last Name";
                dgvPeople.Columns["LastName"].Width = 120;

                dgvPeople.Columns["GenderCaption"].HeaderText = "Gender";
                dgvPeople.Columns["GenderCaption"].Width = 120;

                dgvPeople.Columns["DateOfBirth"].HeaderText = "Date Of Birth";
                dgvPeople.Columns["DateOfBirth"].Width = 140;

                dgvPeople.Columns["CountryName"].HeaderText = "Nationality";
                dgvPeople.Columns["CountryName"].Width = 120;

                dgvPeople.Columns["Phone"].Width = 120;
                dgvPeople.Columns["Email"].Width = 170;
            }

        }

        private void _RefreshPeopleList()
        {
            _dtAllPeople = clsPerson.GetAllPeople();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "GenderCaption", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");

            dgvPeople.DataSource = _dtPeople;
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString() + "Record(s)";
        }

        private void _FillFilterColumnsComboBox()
        {
            cbFilterBy.Items.Clear();
            cbFilterBy.Items.Add("None");

            // Loop through all columns of the DataGridView
            foreach (DataGridViewColumn col in dgvPeople.Columns)
            {
                if (col.DataPropertyName == "DateOfBirth")
                    continue;
                else
                    cbFilterBy.Items.Add(col.HeaderText);
            }

            // Optional: default selection
            cbFilterBy.SelectedIndex = 0;
        }

        public frmListPeople()
        {
            InitializeComponent();
        }        

        private void frmPeople_Load(object sender, EventArgs e)
        {
            _DesignDataGridView();
            _ConfigurePeopleGrid();
            _FillFilterColumnsComboBox();
        }                      

        private void cbFiltrerBy_SelectedIndexChanged(object sender, EventArgs e)
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

        private void txtFilterByValue_TextChanged(object sender, EventArgs e)
        {

            string FilterColumn = "";

            //Map Selected Filter to real Column name
            switch (cbFilterBy.Text)
            {

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "CountryName";
                    break;

                case "Gender":
                    FilterColumn = "GenderCaption";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value contains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblFilteredRecordsCount.Text = "0 Record(s)";
                return;
            }

            //in this case we deal with integer not string.
            if (FilterColumn == "PersonID")
                _dtPeople.DefaultView.RowFilter = $"[{FilterColumn}] = {txtFilterValue.Text.Trim()}";
            else
                _dtPeople.DefaultView.RowFilter = $"[{FilterColumn}] LIKE '{txtFilterValue.Text.Trim()}%'";

            lblFilteredRecordsCount.Text = dgvPeople.Rows.Count.ToString() + " Record(s)";

        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson _frmAddEditPerson = new frmAddUpdatePerson();
            _frmAddEditPerson.ShowDialog();
            _RefreshPeopleList();
        }

        private void tmsDelete_Click(object sender, EventArgs e)
        {

            int PersonID = (int)dgvPeople.CurrentRow.Cells[0].Value;

            string Message = $"Are you sure you want to delete the selected person with ID[{PersonID}] ? This action cannot be undone.";
            string Caption = "Warning";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Warning;
            
            if (MessageBox.Show(Message, Caption, buttons, icon) == DialogResult.Yes)
            {
                if (clsPerson.Delete(PersonID))
                {
                    MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshPeopleList();
                }
                else
                {
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error: Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void tmsEditPerson_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPeople.CurrentRow.Cells[0].Value;
            frmAddUpdatePerson frmAddEditPerson = new frmAddUpdatePerson(PersonID);
            frmAddEditPerson.ShowDialog();
            _RefreshPeopleList();
        }

        private void tmsAddNewPerson_Click(object sender, EventArgs e)
        {
            Form frm1 = new frmAddUpdatePerson();
            frm1.ShowDialog();
            _RefreshPeopleList();
        }

        private void tmsShowDetails_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPeople.CurrentRow.Cells[0].Value;
            frmPersonInfo frmPersonDetails = new frmPersonInfo(PersonID);
            frmPersonDetails.ShowDialog();
            _RefreshPeopleList();
        }

        private void tmsSendEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", 
                "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tmsPhoneCall_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", 
                "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 1. Check if we need to apply the filter logic
            if (cbFilterBy.Text != "Person ID")
                return;

            // 2. Allow control keys like Backspace and Delete (Mandatory for editing)
            if (char.IsControl(e.KeyChar))
                return;

            // 3. Check if the pressed character is NOT a digit
            //    The e.KeyChar contains the character pressed.
            if (!char.IsDigit(e.KeyChar))
            {
                // Block the character from being entered
                e.Handled = true;
            }
        }
    }
}