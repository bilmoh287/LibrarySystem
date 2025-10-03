using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDataAccessLayer
{
    public class clsUsersData
    {
        public static DataTable GetAllUsersList()
        {
            DataTable dtAuthors = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string Query = "SELECT * FROM Users";

            SqlCommand command = new SqlCommand(Query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dtAuthors.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }

            return dtAuthors;
        }

        public static int AddNewUser(string FullName, DateTime DateOfBirth, string ContactInfo, int Permission)
        {
            int NewID = -1;
            string _LibraryCard = "";

            // Use 'using' statement for better resource management
            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                string Query = @"
                            INSERT INTO [dbo].[Users]
                                        ([FullName]
                                        ,[DateOfBirth]
                                        ,[ContactInfo]
                                        ,[Permission])
                                  VALUES
                                        (@FullName
                                        ,@DateOfBirth
                                        ,@ContactInfo
                                        ,@Permission);
                            SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@FullName", FullName);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);

                    // Handle ContactInfo (which is nullable/optional)
                    if (!string.IsNullOrEmpty(ContactInfo))
                        command.Parameters.AddWithValue("@ContactInfo", ContactInfo);
                    else
                        command.Parameters.AddWithValue("@ContactInfo", System.DBNull.Value);

                    command.Parameters.AddWithValue("@Permission", Permission);
                    try
                    {
                        connection.Open();
                        // ExecuteScalar returns the first column of the first row (SCOPE_IDENTITY)
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                        {
                            NewID = InsertedID;
                            _LibraryCard = "lib" + InsertedID.ToString("D4");

                            //Update LibraryCardNumber;
                            SqlCommand Updatecmd = new SqlCommand(@"UPDATE Users SET LibraryCardNumber = @Card
                                WHERE UserID = @UserID;", connection);

                            Updatecmd.Parameters.AddWithValue("@Card", _LibraryCard);
                            Updatecmd.Parameters.AddWithValue("@UserID", NewID);

                            Updatecmd.ExecuteNonQuery();


                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the error message
                        Console.WriteLine("Error in AddNewUser: " + ex.Message);
                    }
                } // SqlCommand is disposed here
            } // SqlConnection is closed and disposed here

            return NewID;
        }

        public static bool FindByID(int AuthorID, ref string FullName, ref DateTime DateOfBirth,
              ref string ContactInfo, ref string LibraryCard, ref int Permission)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string Query = "SELECT * FROM Users WHERE UserID = @UserID";

            using (SqlCommand command = new SqlCommand(Query, connection))
            {
                command.Parameters.AddWithValue("@UserID", AuthorID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        // no nulls allowed here
                        FullName = (string)reader["FullName"];
                        DateOfBirth = (DateTime)reader["DateOfBirth"];
                        Permission = (int)reader["Permission"];

                        // only these need null checks
                        ContactInfo = reader["ContactInfo"] != DBNull.Value ? (string)reader["ContactInfo"] : "";
                        LibraryCard = reader["LibraryCardNumber"] != DBNull.Value ? (string)reader["LibraryCardNumber"] : "";

                        IsFound = true;
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return IsFound;
        }

        public static bool UpdateUser(int UserID, string FullName, DateTime DateOfBirth,
                                      string ContactInfo, int Permission)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                string Query = @"
                            UPDATE [dbo].[Users]
                            SET [FullName] = @FullName,
                                [DateOfBirth] = @DateOfBirth,
                                [ContactInfo] = @ContactInfo,
                                [Permission] = @Permission
                            WHERE UserID = @UserID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.AddWithValue("@FullName", FullName);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@Permission", Permission);
                    command.Parameters.AddWithValue("@ContactInfo",
                        string.IsNullOrEmpty(ContactInfo) ? (object)DBNull.Value : ContactInfo);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error in UpdateUser: " + ex.Message);
                    }
                }
            }

            return rowsAffected > 0;
        }

        public static bool DeleteUser(int UserID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                string Query = "DELETE FROM Users WHERE UserID = @UserID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", UserID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error in DeleteUser: " + ex.Message);
                    }
                }
            }

            return rowsAffected > 0;
        }


    }
}
