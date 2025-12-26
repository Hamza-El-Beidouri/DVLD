using DVLD.People.Controls;
using DVLD.Users.Controls;
using DVLD_BusinessLayer;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmChangePassword : Form
    {

        private int _userID;
        private clsUser _user;

        public frmChangePassword(int userID)
        {
            InitializeComponent();
            _userID = userID;
        }

        private void _ResetDefaultValues()
        {
            txtCurrentPassword.Text = string.Empty;
            txtNewPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            txtCurrentPassword.Focus();
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {

            _user = clsUser.Find(_userID);

            if (_user == null )
            {
                MessageBox.Show("No User with UserID = " + _userID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            ctrlUserCard1.LoadUserInfo(_userID);

        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {

            Guna2TextBox currentPasswordControl = (Guna2TextBox)sender;
            string currentPassword = currentPasswordControl.Text.Trim();

            if (string.IsNullOrEmpty(currentPassword))
            {
                e.Cancel = true;
                errorProvider1.SetError(currentPasswordControl, "Current Password is required.");
            }

            if (currentPassword != _user.Password)
            {
                e.Cancel = true;
                errorProvider1.SetError(currentPasswordControl, "The password you entered does not match your current password.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(currentPasswordControl, "");
            }

        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {

            Guna2TextBox newPasswordControl = (Guna2TextBox)sender;
            string newPassword = newPasswordControl.Text.Trim();

            if (string.IsNullOrEmpty(newPassword))
            {
                e.Cancel = true;
                errorProvider1.SetError(newPasswordControl, "New Password is required.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(newPasswordControl, "");
            }


        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {

            Guna2TextBox ConfirmPasswordControl = (Guna2TextBox)sender;
            string ConfirmPassword = ConfirmPasswordControl.Text.Trim();
            
            if ((ConfirmPassword != txtNewPassword.Text))
            {
                e.Cancel = true;
                ConfirmPasswordControl.Focus();
                errorProvider1.SetError(ConfirmPasswordControl, "Passwords do not match. Please try again.");
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
                //Here we dont continue because the form is not valid
                MessageBox.Show("Please correct the highlighted fields. Hover over the red icons for more information.",
                    "Validation Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            if (_user.ChangePassword(txtNewPassword.Text))
                MessageBox.Show(
                                 "Your password has been successfully updated.",
                                 "Success",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Information
                                );
            else
                MessageBox.Show(
                                 "Failed to update the password. Please check your input and try again.",
                                 "Update Failed",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error
                                );


        }

    }
}
