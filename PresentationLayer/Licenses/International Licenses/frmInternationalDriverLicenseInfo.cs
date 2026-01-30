using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.International_Licenses
{
    public partial class frmInternationalDriverLicenseInfo : Form
    {

        private int _InternationalLicenseID = -1;

        public frmInternationalDriverLicenseInfo(int internationalLicenseID)
        {
            InitializeComponent();
            _InternationalLicenseID = internationalLicenseID;
        }

        private void frmInternationalDriverLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlInternationalLicenseInfo1.LoadInternationalDriverLicenseInfo(_InternationalLicenseID);
        }
    }
}
