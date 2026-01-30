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

namespace DVLD.People.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {

        // Define a custom event handler delegate with parameters
        public event Action<int> OnPersonSelected;
        // Create a protected method to raise the event with a parameter
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(PersonID); // Raise the event with the parameter
            }
        }

        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }
            set
            {
                _ShowAddPerson = value;
                btnAddNewPerson.Visible = _ShowAddPerson;
            }
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
                gbPersonFilter.Enabled = _FilterEnabled;
            }
        }

        public int personID
        {
            get { return ctrlPersonCard1.PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return ctrlPersonCard1.PersonInfo; }
        }

        private void _FindPerson()
        {

            switch (cbFilterBy.Text)
            {

                case "National No.":
                    ctrlPersonCard1.LoadPersonInfo(txtFilterValue.Text);
                    break;

                case "Person ID":
                    ctrlPersonCard1.LoadPersonInfo(int.Parse(txtFilterValue.Text));
                    break;

            }

            if (OnPersonSelected != null && FilterEnabled)
                PersonSelected(ctrlPersonCard1.PersonID);

        }

        public void LoadPersonInfo(string nationalNo)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text = nationalNo;
            _FindPerson();
        }

        public void LoadPersonInfo(int personID)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = personID.ToString();
            _FindPerson();
        }

        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        private void _FillFilterColumnsComboBox()
        {
            cbFilterBy.Items.Add("National No.");
            cbFilterBy.Items.Add("Person ID");
            cbFilterBy.SelectedIndex = 0;
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            _FillFilterColumnsComboBox();
            txtFilterValue.Focus();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = string.Empty;
            txtFilterValue.Focus();
        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtFilterValue.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "This field is required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFilterValue, null);
            }

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 1. Check if we need to apply the filter logic
            if (cbFilterBy.Text != "Person ID")
                return;

            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
                btnSearch.PerformClick();

            // 2. Allow control keys like Backspace and Delete (Mandatory for editing)
            if (char.IsControl(e.KeyChar))
                return;

            // 3. Check if the pressed character is NOT a digit
            //    The e.KeyChar contains the character pressed.
            if (!char.IsDigit(e.KeyChar))
                // Block the character from being entered
                e.Handled = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                MessageBox.Show(
                    "Please fix the errors indicated by the red icons and try again.",
                    "Invalid Input",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            _FindPerson();
        }

        private void _LoadAddedPersonInfo(object sender, int personID)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = personID.ToString();
            ctrlPersonCard1.LoadPersonInfo(personID);
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frmAddUpdatePerson = new frmAddUpdatePerson();
            frmAddUpdatePerson.DataBack += _LoadAddedPersonInfo;
            frmAddUpdatePerson.ShowDialog();
        }

        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

    }
}
