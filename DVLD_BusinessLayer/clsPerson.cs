using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_BusinessLayer
{
    public class clsPerson
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.SecondName + " " + this.ThirdName + " " + this.LastName;
            }
        }
        public DateTime DateOfBirth { get; set; }
        public byte Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }

        public clsCountry CountryInfo;
        public string ImagePath { get; set; }

        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(
                                                        this.NationalNo,
                                                        this.FirstName, this.SecondName,
                                                        this.ThirdName, this.LastName,
                                                        this.DateOfBirth, this.Gender,
                                                        this.Address, this.Phone,
                                                        this.Email, this.NationalityCountryID,
                                                        this.ImagePath
                                                       );

            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(
                                                this.PersonID,
                                                this.FirstName, this.SecondName,
                                                this.ThirdName, this.LastName,
                                                this.DateOfBirth, this.Gender,
                                                this.Address, this.Phone,
                                                this.Email, this.NationalityCountryID,
                                                this.ImagePath
                                              );
        }

        public clsPerson()
        {

            this.PersonID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Gender = 0;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID = -1;
            this.CountryInfo = new clsCountry();
            this.ImagePath = "";

            this.Mode = enMode.AddNew;

        }

        private clsPerson(int PersonID, string NationalNo, string FirstName, string SecondName,
                          string ThirdName, string LastName, DateTime DateOfBirth, byte Gender,
                          string Address, string Phone, string Email,
                          int NationalityCountryID, string ImagePath
                         )
        {


            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.CountryInfo = clsCountry.Find(this.NationalityCountryID);
            this.ImagePath = ImagePath;

            this.Mode = enMode.Update;

        }

        public static clsPerson Find(int PersonID)
        {

            string NationalNo = "", FirstName = "", SecondName = "", ThirdName = "", LastName = "";
            DateTime DateOfBirth = DateTime.Now;
            byte Gender = 0;
            string Address = "", Phone = "", Email = "", ImagePath = "";
            int NationalityCountryID = -1;

            bool isFound = clsPersonData.GetPersonByID(PersonID,
                            ref NationalNo, ref FirstName, ref SecondName, ref ThirdName,
                            ref LastName, ref DateOfBirth, ref Gender, ref Address,
                            ref Phone, ref Email, ref NationalityCountryID, ref ImagePath);

            if (isFound)
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName,
                                     LastName, DateOfBirth, Gender, Address, Phone, Email,
                                     NationalityCountryID, ImagePath);
            }
            else
                return null;


        }

        public static clsPerson Find(string NationalNo)
        {

            int PersonID = -1;
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "";
            DateTime DateOfBirth = DateTime.Now;
            byte Gender = 0;
            string Address = "", Phone = "", Email = "", ImagePath = "";
            int NationalityCountryID = -1;

            bool isFound = clsPersonData.GetPersonByNationalNo(                                                                
                                                                NationalNo, ref PersonID,
                                                                ref FirstName, ref SecondName,
                                                                ref ThirdName, ref LastName,
                                                                ref DateOfBirth, ref Gender,
                                                                ref Address, ref Phone,
                                                                ref Email, ref NationalityCountryID,
                                                                ref ImagePath
                                                               );

            if (isFound)
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName,
                                     LastName, DateOfBirth, Gender, Address, Phone, Email,
                                     NationalityCountryID, ImagePath);
            else
                return null;

        }

        public bool Save()
        {

            switch (Mode)
            {

                case enMode.AddNew:

                    if (_AddNewPerson())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdatePerson();

            }

            return false;

        }

        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }

        public static bool Delete(int PersonID)
        {
            return clsPersonData.DeletePerson(PersonID);
        }

        public static bool IsPersonExist(int PersonID)
        {
            return clsPersonData.IsPersonExist(PersonID);
        }        
        
        public static bool IsPersonExist(string NationalNo)
        {
            return clsPersonData.IsPersonExist(NationalNo);
        }

    }

}