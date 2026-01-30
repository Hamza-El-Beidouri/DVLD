using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsTestTypeData
    {

        public static bool GetTestType(int TestTypeID, ref string TestTypeTitle, ref string TestTypeDescription, ref decimal TestTypeFees)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT * FROM TestTypes WHERE TestTypeID = @TestTypeID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    TestTypeTitle = (string)reader["TestTypeTitle"];
                    TestTypeDescription = (string)reader["TestTypeDescription"];
                    TestTypeFees = (decimal)reader["TestTypeFees"];
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return isFound;

        }

        public static bool UpdateTestType(int TestTypeID, string TestTypeTitle, string TestTypeDescription, decimal TestTypeFees)
        {

            sbyte rowsAffected = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"UPDATE TestTypes
                             SET
                                TestTypeTitle = @TestTypeTitle,
                                TestTypeDescription = @TestTypeDescription,
                                TestTypeFees = @TestTypeFees
                             WHERE TestTypeID = @TestTypeID;
                            ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                rowsAffected = (sbyte)command.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);

        }

        public static DataTable GetAllTestTypes()
        {

            DataTable dtTestTypes = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT * FROM TestTypes;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    dtTestTypes.Load(reader);

            }
            catch (Exception e)
            {

            }
            finally
            {
                connection.Close();
            }

            return dtTestTypes;

        }

        public static decimal GetTestTypeFees(byte TestTypeID)
        {
            decimal Fees = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT TestTypeFees FROM TestTypes WHERE TestTypeID = @TestTypeID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                // Check if the result is not null before casting
                if (result != null && decimal.TryParse(result.ToString(), out decimal extractedFees))
                    Fees = extractedFees;
            }
            catch (Exception ex)
            {
                // Log error if needed
            }
            finally
            {
                connection.Close();
            }

            return Fees;
        }

    }
}