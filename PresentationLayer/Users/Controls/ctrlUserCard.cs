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

namespace DVLD.Users.Controls
{
    public partial class ctrlUserCard : UserControl
    {

        private int _userID = -1;
        public int userID
        {
            get { return _userID; }
        }

        private clsUser _user;

        private void _FillUserInfo()
        {

            ctrlPersonCard1.LoadPersonInfo(_user.PersonID);

            lblUserID.Text = _user.UserID.ToString();
            lblUserName.Text = _user.UserName.ToString();
            lblIsActive.Text = (_user.IsActive) ? "Yes" : "No";

        }

        public void _ResetUserInfo()
        {

            ctrlPersonCard1.ResetPersonInfo();
            lblUserID.Text = "[???]";
            lblUserName.Text = "[???]";
            lblIsActive.Text = "[???]";
        }

        public void LoadUserInfo(int UserID)
        {
            _user = clsUser.Find(UserID);

            if (_user == null)
            {
                _ResetUserInfo();
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillUserInfo();
        }

        public ctrlUserCard()
        {
            InitializeComponent();
        }

    }
}
