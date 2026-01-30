using System;
using System.Data;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccess
{
    public static class clsApplicationsData
    {

        public static bool GetApplicationByID(int ApplicationID, ref int ApplicantPersonID, 
                                                ref DateTime ApplicationDate, ref int ApplicationTypeID, 
                                                ref byte ApplicationStatus, ref DateTime LastStatusDate, 
                                                ref decimal PaidFees, ref int CreatedByUserID)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT * FROM Applications WHERE ApplicationID = @ApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ApplicantPersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = (byte)reader["ApplicationStatus"];
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = (decimal)reader["PaidFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
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

        public static int AddNewApplicationRecord(int ApplicantPersonID, DateTime ApplicationDate, 
                                                  int ApplicationTypeID, byte ApplicationStatus, 
                                                  DateTime LastStatusDate, decimal PaidFees, 
                                                  int CreatedByUserID)
        {

            int ApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"
                             INSERT INTO Applications
                                (
                                    ApplicantPersonID,
                                    ApplicationDate,
                                    ApplicationTypeID,
                                    ApplicationStatus,
                                    LastStatusDate,
                                    PaidFees,
                                    CreatedByUserID
                                )
                             VALUES
                                (
                                    @ApplicantPersonID,
                                    @ApplicationDate,
                                    @ApplicationTypeID,
                                    @ApplicationStatus,
                                    @LastStatusDate,
                                    @PaidFees,
                                    @CreatedByUserID
                                )
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                    ApplicationID = InsertedID;

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return ApplicationID;

        }

        public static bool UpdateApplicationRecord(int ApplicationID, int ApplicantPersonID, 
                                                   DateTime ApplicationDate, int ApplicationTypeID,
                                                   byte ApplicationStatus, DateTime LastStatusDate,
                                                   decimal PaidFees, int CreatedByUserID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"
                             UPDATE Applications
                             SET
                                ApplicantPersonID   = @ApplicantPersonID,
                                ApplicationDate     = @ApplicationDate,
                                ApplicationTypeID   = @ApplicationTypeID,
                                ApplicationStatus   = @ApplicationStatus,
                                LastStatusDate      = @LastStatusDate,
                                PaidFees            = @PaidFees,
                                CreatedByUserID     = @CreatedByUserID
                             WHERE
                                ApplicationID = @ApplicationID;
                             ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);

        }

        public static bool DeleteApplicationRecord(int ApplicationID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "DELETE FROM Applications WHERE ApplicationID = @ApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);

        }

        public static DataTable GetAllApplications()
        {

            DataTable dtApplications = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT * FROM Applications;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    dtApplications.Load(reader);

                reader.Close();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return dtApplications;

        }

        public static bool IsApplicationExist(int ApplicationID)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT Found = 1 FROM Applications WHERE ApplicationID = @ApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                
                object result = command.ExecuteScalar();

                 if (result != null)
                    isFound = true;

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

        public static int GetActiveApplicationID(int PersonID, byte ApplicationTypeID)
        {

            int ActiveApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"SELECT ActiveApplicationID = ApplicationID FROM Applications 
                             WHERE 
                             ApplicantPersonID = @ApplicantPersonID AND ApplicationTypeID = @ApplicationTypeID AND ApplicationStatus = 1;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int activeApplicationID))
                    ActiveApplicationID = activeApplicationID;


            }
            catch (Exception e)
            {

            }
            finally
            {
                connection.Close();
            }

            return ActiveApplicationID;

        }

        public static bool DoesPersonHaveActiveApplication(int PersonID, byte ApplicationTypeID)
        {
            return (GetActiveApplicationID(PersonID, ApplicationTypeID) != -1);
        }

        public static int GetActiveApplicationIdByLicenseClass(int PersonID, int ApplicationTypeID , int LicenseClassID)
        {

            int ActiveApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"
                            SELECT 
                                Applications.ApplicationID AS ActiveApplicationID
                            FROM Applications
                            INNER JOIN LocalDrivingLicenseApplications
                                ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            WHERE ApplicantPersonID   = @ApplicantPersonID
                              AND ApplicationTypeID   = @ApplicationTypeID
                              AND LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                              AND ApplicationStatus   = 1;
                            ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int activeApplicationID))
                    ActiveApplicationID = activeApplicationID;

            }
            catch (Exception e)
            {

            }
            finally
            {
                connection.Close();
            }

            return ActiveApplicationID;

        }

        public static bool UpdateStatus(int ApplicationID, byte NewStatus)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"
                             UPDATE Applications
                             SET
                                 ApplicationStatus = @ApplicationStatus,
                                 LastStatusDate    = @LastStatusDate
                             WHERE ApplicationID  = @ApplicationID;
                             ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationStatus", NewStatus);
            command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
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

        public static DataTable GetAllInternationalLicensesApplications()
        {

            DataTable dtInternationalLicenseApplications = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"
                            SELECT InternationalLicenseID, Applications.ApplicationID,
		                    IssuedUsingLocalLicenseID As LocalLicenseID,
		                    IssueDate, ExpirationDate, IsActive
                            FROM InternationalLicenses
                            INNER JOIN Applications
                            ON InternationalLicenses.ApplicationID = Applications.ApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    dtInternationalLicenseApplications.Load(reader);

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return dtInternationalLicenseApplications;

        }

    }
}