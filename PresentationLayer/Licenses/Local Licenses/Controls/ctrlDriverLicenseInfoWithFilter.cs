using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.Local_Licenses.Controls
{
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {

        public event Action<int> LicenseSelected;

        protected virtual void OnLicenseSelected(int LicenseID)
        {
            Action<int> handler = LicenseSelected;
            if (handler != null)
                handler(LicenseID);
        }

        private bool _FilterEnabled = true;

        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set 
            {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }

        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtLicenseID.Text))
            {
                e.Cancel = true; // Prevent leaving the textbox
                errorProvider1.SetError(txtLicenseID, "License ID is required");
            }
            else
                errorProvider1.SetError(txtLicenseID, "");

        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow digits and control keys (Backspace, Delete)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true; // Block the character
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            int LicenseID = Convert.ToInt32(txtLicenseID.Text);
            ctrlDriverLicenseInfo1.LoadDriverLicenseInfo(LicenseID);

            if (LicenseSelected != null && ctrlDriverLicenseInfo1.isLicenseFound)
                OnLicenseSelected(LicenseID);

        }
    }
}