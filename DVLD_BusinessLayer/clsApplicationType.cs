using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_BusinessLayer
{
    public class clsApplicationType
    {

        public int ID { get; set; }
        public string Title { get; set; }
        public decimal Fees { get; set; }

        private bool _UpdateApplicationType()
        {
            return clsApplicationTypeData.UpdateApplicationType(ID, Title, Fees);
        }

        public clsApplicationType()
        {
            this.ID = -1;
            this.Title = string.Empty;
            this.Fees = 0;
        }

        private clsApplicationType(int applicationTypeID, string applicationTypeTitle, decimal applicationTypeFees)
        {
            ID = applicationTypeID;
            Title = applicationTypeTitle;
            Fees = applicationTypeFees;
        }

        public static clsApplicationType Find(int applicationTypeID)
        {

            decimal applicationTypeFees = 0;
            string applicationTypeTitle = "";

            if (clsApplicationTypeData.GetApplicationType(applicationTypeID, ref applicationTypeTitle, ref applicationTypeFees))
                return new clsApplicationType(applicationTypeID, applicationTypeTitle, applicationTypeFees);
            else
                return null;

        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationTypes();
        }

        public bool Save()
        {
            return _UpdateApplicationType();
        }

        public static decimal GetFees(clsApplication.enApplicationType ApplicationType)
        {
            return clsApplicationTypeData.GetFees((byte)ApplicationType);
        }

    }
}
