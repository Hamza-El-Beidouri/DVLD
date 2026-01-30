using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public static class clsLocalDrivingLicenseApplicationData
    {

        public static bool GetLocalDrivingLicenseApplicationInfoByID(int LocalDrivingLicenseApplicationID,
            ref int ApplicationID, ref int LicenseClassID)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"SELECT * FROM LocalDrivingLicenseApplications
                             WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
                }

                reader.Close();

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

        public static bool GetLocalDrivingLicenseApplicationInfoByApplicationID(
                            int ApplicationID,
                            ref int LocalDrivingLicenseApplicationID,
                            ref int LicenseClassID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"SELECT * FROM LocalDrivingLicenseApplications
                     WHERE ApplicationID = @ApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    LocalDrivingLicenseApplicationID =
                        (int)reader["LocalDrivingLicenseApplicationID"];

                    LicenseClassID =
                        (int)reader["LicenseClassID"];
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                // log exception if needed
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetApplicationID(int LocalDrivingLicenseApplicationID, ref int ApplicationID)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"SELECT ApplicationID FROM LocalDrivingLicenseApplications
                             WHERE
                             LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int applicationID))
                {
                    isFound = true;
                    ApplicationID = applicationID;
                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                connection.Close();
            }

            return isFound;

        }

        public static int AddNewLocalDrivingLicenseApplication(int ApplicationID, 
            int LicenseClassID)
        {

            int LocalDrivingLicenseApplicationID = -1;

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"
                            INSERT INTO LocalDrivingLicenseApplications
                                (ApplicationID, LicenseClassID)
                            VALUES
                                (@ApplicationID, @LicenseClassID);

                            SELECT SCOPE_IDENTITY();
                            ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                    LocalDrivingLicenseApplicationID = InsertedID;
            }
            catch (Exception ex)
            {
                // log exception if needed
            }
            finally
            {
                connection.Close();
            }

            return LocalDrivingLicenseApplicationID;
        }

        public static bool UpdateLocalDrivingLicenseApplication(
            int LocalDrivingLicenseApplicationID,
            int ApplicationID,
            int LicenseClassID)
        {

            int rowsAffected = 0;

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"
                            UPDATE LocalDrivingLicenseApplications
                            SET
                                ApplicationID  = @ApplicationID,
                                LicenseClassID = @LicenseClassID
                            WHERE
                                LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;
                            ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID",
                                            LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // log exception if needed
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool IsLocalDrivingLicenseApplicationExists(int LocalDrivingLicenseApplicationID)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"SELECT Found = 1 FROM LocalDrivingLicenseApplications 
                             WHERE
                             LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    isFound = true;

            }
            catch (Exception e)
            {

            }
            finally
            {
                connection.Close();
            }

            return isFound;

        }

        public static bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            int rowsAffected = 0;

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"
                            DELETE FROM LocalDrivingLicenseApplications
                            WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;
                            ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(
                "@LocalDrivingLicenseApplicationID",
                LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // log exception if needed
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {

            DataTable dtLocalDrivingLicenseApplications = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"SELECT *
                              FROM LocalDrivingLicenseApplications_View
                              ORDER BY ApplicationDate DESC;";


            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dtLocalDrivingLicenseApplications.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dtLocalDrivingLicenseApplications;

        }

        public static int GetPassedTestsCount(int localDrivingLicenseApplicationID)
        {

            int PassedTestsCount = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"
                             SELECT COUNT(*) AS PassedTests
                             FROM Tests
                             INNER JOIN TestAppointments
                                 ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                             WHERE Tests.TestResult = 1
                               AND TestAppointments.LocalDrivingLicenseApplicationID =
                                   @LocalDrivingLicenseApplicationID;
                             ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(
                "@LocalDrivingLicenseApplicationID", localDrivingLicenseApplicationID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int passedTestsCount))
                    PassedTestsCount = passedTestsCount;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return PassedTestsCount;
        }

        public static bool IsTestPassed(int LocalDrivingLicenseApplicationID, byte TestType)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"SELECT Found = 1 FROM Tests
                             INNER JOIN 
                             TestAppointments
                             ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                             WHERE
                             LocalDrivingLicenseApplicationID = @ApplicationID
                             AND TestTypeID = @TestTypeID
                             AND TestResult = 1;
                             ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestType);

            try
            {

                connection.Open();

                object result = command.ExecuteScalar();
                isFound = (result != null);

            }
            catch (Exception e)
            {

            }
            finally
            {
                connection.Close();
            }

            return isFound;

        }

        public static bool HasActiveTestAppointment(int localDrivingLicenseApplicationID, byte testTypeID)
        {
            bool exists = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"SELECT Found = 1 FROM TestAppointments
                             WHERE 
                             LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                             AND TestTypeID = @TestTypeID
                             AND IsLocked = 0;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", localDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", testTypeID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                exists = (result != null);
            }
            catch (Exception)
            {
                // handle exception if needed
                exists = false;
            }
            finally
            {
                connection.Close();
            }

            return exists;
        }

        public static int GetFailedTrialsCount(int localDrivingLicenseApplicationID, byte testTypeID)
        {
            int trialCount = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"SELECT COUNT(*) FROM Tests
                             INNER JOIN TestAppointments
                                 ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                             WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                               AND TestTypeID = @TestTypeID
                               AND TestResult = 0;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", localDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", testTypeID);

            try
            {
                connection.Open();
                trialCount = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception)
            {
                // handle exception if needed
                trialCount = 0;
            }
            finally
            {
                connection.Close();
            }

            return trialCount;
        }

        public static bool HasFailedTestType(int localDrivingLicenseApplicationID, byte testTypeID)
        {
            bool exists = false;

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"SELECT Found = 1
                     FROM Tests
                     INNER JOIN TestAppointments
                         ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                     WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                       AND TestTypeID = @TestTypeID
                       AND TestResult = 0;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", localDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", testTypeID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                exists = (result != null);
            }
            catch (Exception)
            {
                // handle exception if needed
                exists = false;
            }
            finally
            {
                connection.Close();
            }

            return exists;
        }


    }
}
