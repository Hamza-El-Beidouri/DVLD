using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_BusinessLayer
{
    public class clsTestType
    {

        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 }

        public enTestType ID {  get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Fees { get; set; }

        private bool _UpdateTestType()
        {
            return clsTestTypeData.UpdateTestType((int)ID, Title, Description, Fees);
        }

        public clsTestType()
        {
            this.ID = enTestType.VisionTest;
            this.Title = string.Empty;
            this.Description = string.Empty;
            this.Fees = 0;
        }

        private clsTestType(enTestType iD, string testTypeTitle, string testTypeDescription, decimal testTypeFess)
        {
            ID = iD;
            Title = testTypeTitle;
            Description = testTypeDescription;
            Fees = testTypeFess;
        }

        public static clsTestType Find(enTestType ID)
        {

            string testTypeTitle = "", testTypeDescription = "";
            decimal testTypeFess = 0;

            if (clsTestTypeData.GetTestType((int)ID, ref testTypeTitle, ref testTypeDescription, ref testTypeFess))
                return new clsTestType(ID, testTypeTitle, testTypeDescription, testTypeFess);
            else
                return null;
            
        }

        public bool Save()
        {
            return _UpdateTestType();
        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypeData.GetAllTestTypes();
        }

    }
}
