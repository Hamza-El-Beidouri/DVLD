using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.People.Controls;
using DVLD_BusinessLayer;
using Guna.UI2.WinForms;

namespace DVLD.Users
{
    public partial class frmAddUpdateUser : Form
    {

        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _mode;

        private int _userID = -1;
        private clsUser _user;

        public frmAddUpdateUser()
        {
            InitializeComponent();

            _mode = enMode.AddNew;
        }

        public frmAddUpdateUser(int userID)
        {
            InitializeComponent();

            _userID = userID;
            _mode = enMode.Update;
        }

        private void _ResetDefaultValues()
        {
            //this will initialize the reset the defaule values

            if (_mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New User";
                this.Text = "Add New User";
                _user = new clsUser();

                tpLoginInfo.Enabled = false;

                ctrlPersonCardWithFilter1.FilterFocus();
            }
            else
            {
                lblTitle.Text = "Update User";
                this.Text = "Update User";

                tpLoginInfo.Enabled = true;
                ctrlPersonCardWithFilter1.FilterEnabled = false;

            }

            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            chkIsActive.Checked = true;

        }

        private void _LoadData()
        {

            _user = clsUser.Find(_userID);

            if (_user == null)
            {
                MessageBox.Show("No User with ID = " + _user, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            //the following code will not be executed if the person was not found
            lblUserID.Text = _user.UserID.ToString();
            txtUserName.Text = _user.UserName;
            txtPassword.Text = _user.Password;
            txtConfirmPassword.Text = _user.Password;
            chkIsActive.Checked = _user.IsActive;
            ctrlPersonCardWithFilter1.LoadPersonInfo(_user.PersonID);
        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            
            _ResetDefaultValues();

            if (_mode == enMode.Update)
                _LoadData();

        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            if (_mode == enMode.Update)
            {
                tcUserDetails.SelectedTab = tpLoginInfo;
                return;
            }

            if (ctrlPersonCardWithFilter1.personID != -1)
            {

                if (clsUser.IsUserExistForPersonID(ctrlPersonCardWithFilter1.personID))
                {
                    MessageBox.Show(
                                    "The selected person already has a user account.\n\nPlease choose another person.",
                                    "User Already Exists",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning
                                    );
                }
                else
                {
                    tpLoginInfo.Enabled = true;
                    tcUserDetails.SelectedTab = tpLoginInfo;
                }                   

            }
            else
                MessageBox.Show(
                    "The selected person does not exist.\n\nPlease search and select a valid person.",
                    "Person Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            Guna2TextBox UserNameControl = (Guna2TextBox)sender;
            string userName = UserNameControl.Text.Trim();

            // Update mode and unchanged username → valid
            if (_mode == enMode.Update && _user.UserName == userName)
                return;

            if (string.IsNullOrEmpty(userName))
            {
                e.Cancel = true;
                errorProvider1.SetError(UserNameControl, "Username is required.");
                return;
            }

            if (clsUser.IsUserExist(userName))
            {
                e.Cancel = true;
                errorProvider1.SetError(UserNameControl, "This username is already taken.");
                return;
            }

            errorProvider1.SetError(UserNameControl, null);
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {

            Guna2TextBox PasswordControl = (Guna2TextBox)sender;
            string Password = PasswordControl.Text.Trim();

            if (string.IsNullOrEmpty(Password))
            {
                e.Cancel = true;
                errorProvider1.SetError(PasswordControl, "Please enter a password.\r\n");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(PasswordControl, "");
            }

        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {

            Guna2TextBox ConfirmPasswordControl = (Guna2TextBox)sender;
            string ConfirmPassword = ConfirmPasswordControl.Text.Trim();

            if (ConfirmPassword != txtPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(ConfirmPasswordControl, "Please confirm your password.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(ConfirmPasswordControl, "");
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                MessageBox.Show(
                                 "Some fields contain invalid or missing data.\n\n" +
                                 "Please correct the highlighted fields and try again.",
                                 "Validation Error",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning
                                );
                return;
            }

            _user.PersonID = ctrlPersonCardWithFilter1.personID;
            _user.UserName = txtUserName.Text;
            _user.Password = txtPassword.Text;
            _user.IsActive = (chkIsActive.Checked);

            if (_user.Save())
            {

                lblTitle.Text = "Update Person Info";
                lblUserID.Text = _user.UserID.ToString();
                this.Text = lblTitle.Text;
                _mode = enMode.Update;
                ctrlPersonCardWithFilter1.Enabled = false;

                MessageBox.Show(
                                    "User record saved successfully.",
                                    "Success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information
                                );

            }
            else
            {
                MessageBox.Show(
                                    "Error: Data was not saved successfully.",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                );
            }

        }
    }

}