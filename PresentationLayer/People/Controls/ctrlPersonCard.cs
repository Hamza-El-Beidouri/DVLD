using DVLD.Properties;
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
using System.IO;

namespace DVLD.People.Controls
{
    public partial class ctrlPersonCard : UserControl
    {
        public enum enGender { Male = 0, Female = 1 }        

        private int _PersonID = -1;
        public int PersonID
        {
            get { return _PersonID; }
        }

        private clsPerson _Person;
        public clsPerson PersonInfo
        {
            get { return _Person; }
        }

        public void ResetPersonInfo()
        {
            _PersonID = -1;
            lblPersonID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblFullName.Text = "[????]";
            pbGenderIcon.Image = Resources.Male;
            lblGender.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblBirthDate.Text = "[????]";
            lblCountry.Text = "[????]";
            lblAddress.Text = "[????]";
            pbPersonImage.Image = Resources.Male;
            btnEdit.Enabled = false;
        }

        private void _LoadPersonImage()
        {

            if (_Person.Gender == (byte)enGender.Male)
                pbPersonImage.Image = Resources.Male;
            else
                pbPersonImage.Image = Resources.Female;

            string ImagePath = _Person.ImagePath;

            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void _FillPersonInfo()
        {
            btnEdit.Enabled = true;
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblNationalNo.Text = _Person.NationalNo;
            lblGender.Text = (_Person.Gender == (byte)enGender.Male) ? "Male" : "Female";
            lblFullName.Text = _Person.FullName;
            lblEmail.Text = _Person.Email;
            lblPhone.Text = _Person.Phone;
            lblBirthDate.Text = _Person.DateOfBirth.ToShortDateString();
            lblCountry.Text = clsCountry.Find(_Person.NationalityCountryID).CountryName;
            lblAddress.Text = _Person.Address;
            _LoadPersonImage();
        }

        public void LoadPersonInfo(int PersonID)
        {

            _Person = clsPerson.Find(PersonID);

            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with PersonID = " + PersonID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();

        }

        public void LoadPersonInfo(string NationalNo)
        {

            _Person = clsPerson.Find(NationalNo);

            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with National No. " + NationalNo,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();

        }

        public ctrlPersonCard()
        {
            InitializeComponent();
            ResetPersonInfo();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frmAddEditPerson = new frmAddUpdatePerson(PersonID);
            frmAddEditPerson.ShowDialog();

            // refresh
            LoadPersonInfo(PersonID);
        }
    }
}
