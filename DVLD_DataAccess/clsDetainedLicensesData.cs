using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public static class clsDetainedLicensesData
    {

        // =========================
        // GET BY ID
        // =========================
        public static bool GetDetainedLicenseByID(int DetainID,
            ref int LicenseID, ref DateTime DetainDate, ref decimal FineFees,
            ref int CreatedByUserID, ref bool IsReleased,
            ref DateTime ReleaseDate, ref int ReleasedByUserID,
            ref int ReleaseApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT * FROM DetainedLicenses WHERE DetainID = @DetainID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    LicenseID = (int)reader["LicenseID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = (decimal)reader["FineFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];

                    ReleaseDate = (reader["ReleaseDate"] != DBNull.Value)
                        ? (DateTime)reader["ReleaseDate"] : DateTime.MinValue;

                    ReleasedByUserID = (reader["ReleasedByUserID"] != DBNull.Value)
                        ? (int)reader["ReleasedByUserID"] : -1;

                    ReleaseApplicationID = (reader["ReleaseApplicationID"] != DBNull.Value)
                        ? (int)reader["ReleaseApplicationID"] : -1;
                }

                reader.Close();
            }
            catch (Exception)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetDetainedLicenseByLicenseID(int LicenseID,
            ref int DetainID, ref DateTime DetainDate, ref decimal FineFees,
            ref int CreatedByUserID, ref bool IsReleased,
            ref DateTime ReleaseDate, ref int ReleasedByUserID,
            ref int ReleaseApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"SELECT TOP 1 * FROM DetainedLicenses
                     WHERE LicenseID = @LicenseID
                     AND IsReleased = 0
                     ORDER BY DetainDate DESC;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    DetainID = (int)reader["DetainID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = (decimal)reader["FineFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];

                    ReleaseDate = (reader["ReleaseDate"] != DBNull.Value)
                        ? (DateTime)reader["ReleaseDate"] : DateTime.MaxValue;

                    ReleasedByUserID = (reader["ReleasedByUserID"] != DBNull.Value)
                        ? (int)reader["ReleasedByUserID"] : -1;

                    ReleaseApplicationID = (reader["ReleaseApplicationID"] != DBNull.Value)
                        ? (int)reader["ReleaseApplicationID"] : -1;
                }

                reader.Close();
            }
            catch (Exception)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }


        // =========================
        // ADD NEW DETAIN RECORD
        // =========================
        public static int AddNewDetainedLicense(int LicenseID, DateTime DetainDate,
            decimal FineFees, int CreatedByUserID)
        {
            int DetainID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"
                INSERT INTO DetainedLicenses
                (LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased)
                VALUES
                (@LicenseID, @DetainDate, @FineFees, @CreatedByUserID, 0);

                SELECT SCOPE_IDENTITY();
            ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    DetainID = insertedID;
            }
            catch (Exception)
            {
                // log if needed
            }
            finally
            {
                connection.Close();
            }

            return DetainID;
        }

        // =========================
        // RELEASE DETAINED LICENSE
        // =========================
        public static bool ReleaseDetainedLicense(int DetainID,
            DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"
                UPDATE DetainedLicenses
                SET
                    IsReleased = 1,
                    ReleaseDate = @ReleaseDate,
                    ReleasedByUserID = @ReleasedByUserID,
                    ReleaseApplicationID = @ReleaseApplicationID
                WHERE DetainID = @DetainID
                AND IsReleased = 0;
            ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                // log if needed
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        // =========================
        // DELETE (ADMIN ONLY)
        // =========================
        public static bool DeleteDetainedLicense(int DetainID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "DELETE FROM DetainedLicenses WHERE DetainID = @DetainID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        // =========================
        // CHECK IF LICENSE IS DETAINED
        // =========================
        public static bool IsLicenseDetained(int LicenseID)
        {
            bool isDetained = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"SELECT 1 
                             FROM DetainedLicenses
                             WHERE LicenseID = @LicenseID
                             AND IsReleased = 0;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                isDetained = (result != null);
            }
            catch (Exception)
            {
                isDetained = false;
            }
            finally
            {
                connection.Close();
            }

            return isDetained;
        }

        // =========================
        // GET ALL DETAINED LICENSES
        // =========================
        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"SELECT * FROM DetainedLicenses_View;";

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
                dt = null;
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

    }
}