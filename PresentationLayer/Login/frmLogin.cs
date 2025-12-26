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
using System.IO;

namespace DVLD.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            string UserName = txtUserName.Text;
            string Password = txtPassword.Text;

            if (clsUser.IsUserExist(UserName, Password))
            {

                clsUser User = clsUser.Find(UserName, Password);

                if (User.IsActive)
                {

                    if (chkRememberMe.Checked)
                        clsGlobal.RememberUserNameAndPassword(UserName, Password);
                    else
                        clsGlobal.RememberUserNameAndPassword("", "");

                    clsGlobal.CurrentUser = User;
                    Form frmMain = new frmMain(this);
                    frmMain.Show();
                    this.Hide();
                }
                else
                {
                    // Login Failed - Account is deactivated
                    MessageBox.Show(
                                    "Your account is deactivated. Please contact your system administrator.",
                                    "Account Deactivated",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Stop
                                    );
                }

            }
            else
            {
                MessageBox.Show("Invalid username or password. Please check your credentials and try again.",
                                "Login Failed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                                );
            }

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

            string UserName = "", Password = "";

            if (clsGlobal.GetStoredCredentials(ref UserName, ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                chkRememberMe.Checked = true;
            }
            else
                chkRememberMe.Checked = false;

            chkShowPassword.Checked = false;
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = (chkShowPassword.Checked) ? '\0' : '●';
        }
    }
}
