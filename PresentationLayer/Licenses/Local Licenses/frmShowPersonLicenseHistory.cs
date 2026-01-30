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

namespace DVLD.Licenses.Local_Licenses
{
    public partial class frmShowPersonLicenseHistory : Form
    {

        private int _PersonID = -1;

        public frmShowPersonLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        private void frmShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
            ctrlPersonCardWithFilter1.FilterEnabled = false;
            ctrlDriverLicenses1.LoadDriverLicenses(_PersonID);
        }
    }
}
