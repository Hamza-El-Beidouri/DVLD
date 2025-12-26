using DVLD.Global_Classes;
using DVLD.Properties;
using DVLD_BusinessLayer;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DVLD
{
    public partial class frmAddUpdatePerson : Form
    {

        // Declare a delegate
        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declaring the delegate as an event enforces two key constraints:
        // 1) Only the declaring class can invoke it (external code cannot raise the event).
        // 2) External code can only subscribe (+=) or unsubscribe (-=), preventing full reassignment
        //    such as setting it to null or assigning a new delegate reference.
        public event DataBackEventHandler DataBack;

        public enum enMode { AddNew = 0, Update = 1 };
        public enum enGender { Male = 0, Female = 1 };

        private enMode _Mode;
        private int _PersonID = -1;
        private clsPerson _Person;        

        private void _FillCountriesInComboBox()
        {

            DataTable countriesTable = clsCountry.GetAllCountries();

            foreach (DataRow row in countriesTable.Rows)
                cbCountries.Items.Add(row["CountryName"]);

        }

        private void _ResetDefaultValues()
        {

            _FillCountriesInComboBox();

            if (_Mode == enMode.AddNew)
            {
                lblAddUpdateMode.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else
                lblAddUpdateMode.Text = "Update Person Info";

            // Restrict the date picker to only allow dates for users 18 years or older
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            rbMale.Tag = enGender.Male;
            rbFemale.Tag = enGender.Female;

            //hide/show the remove linke in case there is no image for the person.
            llRemoveImage.Visible = (pbPersonImage.ImageLocation != null);

            cbCountries.SelectedIndex = cbCountries.FindString("Morocco");

        }

        public frmAddUpdatePerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();

            _Mode = enMode.Update;
            _PersonID = PersonID;
        }

        private void _PopulateControlsFromObject()
        {

            lblPersonID.Text = _Person.PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text =  _Person.ThirdName;

            txtLastName.Text = _Person.LastName;
            txtNationalNo.Text = _Person.NationalNo;
            txtPhone.Text = _Person.Phone;

            txtEmail.Text = _Person.Email;

            dtpDateOfBirth.Value = _Person.DateOfBirth;
            rtbAddress.Text = _Person.Address;

            if (_Person.Gender == (short)enGender.Male)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;

            if (_Person.ImagePath != "")
            {
                llRemoveImage.Visible = true;
                pbPersonImage.Load(_Person.ImagePath);
            }

            cbCountries.SelectedIndex = cbCountries.FindString(clsCountry.Find(_Person.NationalityCountryID).CountryName);

        }

        private void _LoadData()
        {

            _Person = clsPerson.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("This person doesn\'t exist, this form will be closed",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            _PopulateControlsFromObject();

        }

        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void _GetPictureByGender(object sender, EventArgs e)
        {

            if (pbPersonImage.ImageLocation != null)
                return;

            RadioButton gender = (RadioButton)sender;
            enGender Gender = (enGender)gender.Tag;

            if (Gender == enGender.Male)
                pbPersonImage.Image = Resources.Male;
            else
                pbPersonImage.Image = Resources.Female;

        }

        private void IsValidName(object sender, CancelEventArgs e)
        {

            Guna2TextBox NameControl = (Guna2TextBox)sender;
            string Name = NameControl.Text.Trim();

            if (string.IsNullOrEmpty(Name))
            {
                e.Cancel = true;
                NameControl.Focus();
                errorProvider1.SetError(NameControl, "Name is required.Please enter your full name.");
            }
            else if (!clsValidation.IsValidName(Name))
            {
                e.Cancel = true;
                NameControl.Focus();
                errorProvider1.SetError(NameControl, "Name can only contain letters (A-Z, accents), spaces, hyphens, or apostrophes. Numbers and symbols are not allowed.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(NameControl, "");
            }

        }

        private void IsValidNationalNumber(object sender, CancelEventArgs e)
        {

            Guna2TextBox nationalNumberControl = (Guna2TextBox)sender;
            string nationalNumber = nationalNumberControl.Text;


            if (string.IsNullOrEmpty(nationalNumber))
            {
                e.Cancel = true;
                nationalNumberControl.Focus();
                errorProvider1.SetError(nationalNumberControl, "National Number is required.");
            }
            else
            {

                // Make sure the national number is not used by another person
                if (txtNationalNo.Text != _Person.NationalNo && clsPerson.IsPersonExist(txtNationalNo.Text.Trim()))
                {
                    e.Cancel = true;
                    nationalNumberControl.Focus();
                    errorProvider1.SetError(nationalNumberControl, "The National Number must be unique. Please verify the number.");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(nationalNumberControl, "");
                }

            }
            
        }

        private void IsValidPhoneNumber(object sender, CancelEventArgs e)
        {

            Guna2TextBox PhoneNumberControl = (Guna2TextBox)sender;
            string PhoneNumber = PhoneNumberControl.Text;

            if (string.IsNullOrEmpty(PhoneNumber))
            {
                e.Cancel = true;
                PhoneNumberControl.Focus();
                errorProvider1.SetError(PhoneNumberControl, "Phone number is required. Please enter a contact number.");
            }
            else if (!clsValidation.IsValidPhoneNumber(PhoneNumber))
            {
                e.Cancel = true;
                PhoneNumberControl.Focus();
                errorProvider1.SetError(PhoneNumberControl, "Please enter a valid phone number.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(PhoneNumberControl, "");
            }

        }

        private void IsValidEmailAddress(object sender, CancelEventArgs e)
        {

            Guna2TextBox EmailControl = (Guna2TextBox)sender;
            string EmailAddress = EmailControl.Text.Trim();

            if (string.IsNullOrEmpty(EmailAddress))
                return;

            if (!clsValidation.IsValidEmailAddress(EmailAddress))
            {
                e.Cancel = true;
                EmailControl.Focus();
                errorProvider1.SetError(EmailControl, "Please enter a valid email address. It must be in the format: example@domain.com.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(EmailControl, "");
            }

        }

        private void IsValidStreetAddress(object sender, CancelEventArgs e)
        {

            RichTextBox AddressControl = (RichTextBox)sender;
            string StreetAddress = AddressControl.Text;

            if (string.IsNullOrEmpty(StreetAddress))
            {
                e.Cancel = true;
                AddressControl.Focus();
                errorProvider1.SetError(AddressControl, "Street Address is required.");
            }
            else if (!clsValidation.IsValidStreetAddress(StreetAddress))
            {
                e.Cancel = true;
                AddressControl.Focus();
                errorProvider1.SetError(AddressControl, "Address contains invalid characters. Only letters, numbers, spaces, and the following punctuation are allowed: - , . / # & ' @ ( ).");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(AddressControl, "");
            }

        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            openFileDialog1.InitialDirectory = @"C:\Users\PC\Pictures";
            openFileDialog1.Title = "Set Profile Picture";

            // Essential: Set the filter to only show image files
            openFileDialog1.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                string FilePath = openFileDialog1.FileName;              
                pbPersonImage.ImageLocation = FilePath;

                llRemoveImage.Visible = true;
            }

        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            llRemoveImage.Visible = false;
            pbPersonImage.ImageLocation = null;
            pbPersonImage.Image = (rbMale.Checked) ? Resources.Male : Resources.Female;
        }

        private bool _HandlePersonImage()
        {

            //this procedure will handle the person image,
            //it will take care of deleting the old image from the folder
            //in case the image changed. and it will rename the new image with guid and 
            // place it in the images folder.


            //_Person.ImagePath contains the old Image, we check if it changed then we copy the new image
            if (_Person.ImagePath != pbPersonImage.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    //first we delete the old image from the folder in case there is any.

                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {
                        // We could not delete the file.
                        //log it later   
                    }

                }

                if (pbPersonImage.ImageLocation != null)
                {
                    //then we copy the new image to the image folder after we rename it
                    string SourceImageFile = pbPersonImage.ImageLocation.ToString();

                    if (clsUtils.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        _Person.ImagePath = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!_HandlePersonImage())
                return;

            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.NationalNo = txtNationalNo.Text.Trim();            
            _Person.Phone = txtPhone.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.Address = rtbAddress.Text.Trim();

            _Person.Gender = (rbMale.Checked) ? (byte)enGender.Male : (byte)enGender.Female;
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            

            _Person.NationalityCountryID = clsCountry.Find(cbCountries.Text).CountryID;

            if (_Person.Save())
            {              

                lblAddUpdateMode.Text = "Update Person Info";
                lblPersonID.Text = _Person.PersonID.ToString();
                _Mode = enMode.Update;

                MessageBox.Show(
                    "Person record saved successfully.",        // Message text
                    "Success",                             // Title
                    MessageBoxButtons.OK,                  // Buttons
                    MessageBoxIcon.Information            // Icon
                );

                // Trigger the event to send data back to the caller form.
                DataBack?.Invoke(this, _Person.PersonID);

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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}