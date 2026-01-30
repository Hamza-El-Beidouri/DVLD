using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public static class clsTestsData
    {
        public static bool GetTestByID(int TestID,
            ref int TestAppointmentID,
            ref bool TestResult,
            ref string Notes,
            ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"SELECT * FROM Tests
                             WHERE TestID = @TestID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"]; // BIT → bool
                    Notes = (reader["Notes"] != DBNull.Value) ? reader["Notes"].ToString() : "";
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }

                reader.Close();
            }
            catch (Exception)
            {
                // handle exception if needed
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static int AddTest(
            int TestAppointmentID,
            bool TestResult,
            string Notes,
            int CreatedByUserID)
        {
            int TestID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"
                             INSERT INTO Tests
                             (TestAppointmentID, TestResult, Notes, CreatedByUserID)
                             VALUES
                             (@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID);
                             SELECT SCOPE_IDENTITY();
                             ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult); // bool mapped automatically to BIT

            if (string.IsNullOrEmpty(Notes))
                command.Parameters.AddWithValue("@Notes", DBNull.Value);
            else
                command.Parameters.AddWithValue("@Notes", Notes);

            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int id))
                    TestID = id;
            }
            catch (Exception)
            {
                // handle exception if needed
            }
            finally
            {
                connection.Close();
            }

            return TestID;
        }

        public static bool UpdateTest(
            int TestID,
            int TestAppointmentID,
            bool TestResult,
            string Notes,
            int CreatedByUserID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"
                             UPDATE Tests
                             SET
                                 TestAppointmentID = @TestAppointmentID,
                                 TestResult = @TestResult,
                                 Notes = @Notes,
                                 CreatedByUserID = @CreatedByUserID
                             WHERE TestID = @TestID;
                             ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);

            if (string.IsNullOrEmpty(Notes))
                command.Parameters.AddWithValue("@Notes", DBNull.Value);
            else
                command.Parameters.AddWithValue("@Notes", Notes);

            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                // handle exception if needed
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool DeleteTest(int TestID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"DELETE FROM Tests
                             WHERE TestID = @TestID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                // handle exception if needed
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static DataTable GetAllTests()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"SELECT * FROM Tests;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    dt.Load(reader);

                reader.Close();
            }
            catch (Exception)
            {
                // handle exception if needed
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }
    }
}
