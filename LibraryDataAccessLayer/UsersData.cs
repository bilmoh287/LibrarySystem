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

        public static int AddNewUser(string FullName, DateTime DateOfBirth, string ContactInfo, int Permission, string Username, string Password)
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
                                        ,[Permission]
                                        ,[Username]
                                        ,[Password])
                                  VALUES
                                        (@FullName
                                        ,@DateOfBirth
                                        ,@ContactInfo
                                        ,@Permission
                                        ,@Username
                                        ,@Password);
                            SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@FullName", FullName.Trim());
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);

                    // Handle ContactInfo (which is nullable/optional)
                    if (!string.IsNullOrEmpty(ContactInfo))
                        command.Parameters.AddWithValue("@ContactInfo", ContactInfo.Trim());
                    else
                        command.Parameters.AddWithValue("@ContactInfo", System.DBNull.Value);

                    command.Parameters.AddWithValue("@Permission", Permission);
                    command.Parameters.AddWithValue("@Username", Username.Trim());
                    command.Parameters.AddWithValue("@Password", Password.Trim());

                    try
                    {
                        connection.Open();
                        // ExecuteScalar returns the first column of the first row (SCOPE_IDENTITY)
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                        {
                            NewID = InsertedID;
                            _LibraryCard = "LIB" + InsertedID.ToString("D4");

                            //Update LibraryCardNumber;
                            using (SqlCommand Updatecmd = new SqlCommand(@"UPDATE Users SET LibraryCardNumber = @Card
                                WHERE UserID = @UserID;", connection))
                            {
                                Updatecmd.Parameters.AddWithValue("@Card", _LibraryCard);
                                Updatecmd.Parameters.AddWithValue("@UserID", NewID);
                                Updatecmd.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the error message
                        Console.WriteLine("Error in AddNewUser: " + ex.Message);
                    }
                }
            }

            return NewID;
        }

        public static bool FindByID(int AuthorID, ref string FullName, ref DateTime DateOfBirth,
              ref string ContactInfo, ref string LibraryCard, ref int Permission, ref string Username, ref string Password)
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
                        Username = (string)reader["Username"];
                        Password = (string)reader["Password"];

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
                                      string ContactInfo, int Permission, string Username, string Password)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                string Query = @"
                            UPDATE [dbo].[Users]
                            SET [FullName] = @FullName,
                                [DateOfBirth] = @DateOfBirth,
                                [ContactInfo] = @ContactInfo,
                                [Permission] = @Permission,                                
                                [Username] = @Username,
                                [Password] = @Password
                            WHERE UserID = @UserID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.AddWithValue("@FullName", FullName.Trim());
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@Permission", Permission);
                    command.Parameters.AddWithValue("@Username", Username.Trim());
                    command.Parameters.AddWithValue("@Password", Password.Trim());
                    command.Parameters.AddWithValue("@ContactInfo",
                        string.IsNullOrEmpty(ContactInfo) ? (object)DBNull.Value : ContactInfo.Trim());

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

        public static bool FindByUsernameAndPassword(string Username, string Password,
            ref int UserID, ref string FullName, ref DateTime DateOfBirth, ref string ContactInfo,
            ref string LibraryCard, ref int Permission, ref string UsernameOut, ref string PasswordOut)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                string Query = @"SELECT * FROM Users WHERE Username = @Username AND Password = @Password";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@Username", Username);
                    command.Parameters.AddWithValue("@Password", Password);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            UserID = (int)reader["UserID"];
                            FullName =  reader["FullName"].ToString().Trim();
                            DateOfBirth = (DateTime)reader["DateOfBirth"];
                            ContactInfo = reader["ContactInfo"] != DBNull.Value ? (string)reader["ContactInfo"] : "";
                            LibraryCard = reader["LibraryCardNumber"] != DBNull.Value ? (string)reader["LibraryCardNumber"] : "";
                            Permission = (int)reader["Permission"];
                            UsernameOut = reader["Username"].ToString().Trim();
                            PasswordOut = reader["Password"].ToString().Trim();
                            IsFound = true;
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error in FindByUsernameAndPassword: " + ex.Message);
                    }
                }
            }

            return IsFound;
        }

    }
}
