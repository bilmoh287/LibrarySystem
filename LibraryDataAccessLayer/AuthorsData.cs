using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDataAccessLayer
{
    public class clsAuthorsData
    {
        public static DataTable GetAllAuthorsList()
        {
            DataTable dtAuthors = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string Query = "SELECT * FROM Authors";

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

        public static int AddNewAuthor(string FullName, DateTime DateOfBirth, string Nationality, string ContactInfo, string ImagePath)
        {
            int ID = -1;

            // Use 'using' statement for better resource management
            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                string Query = @"
                            INSERT INTO [dbo].[Authors]
                                        ([FullName]
                                        ,[DateOfBirth]
                                        ,[Nationality]
                                        ,[ContactInfo]
                                        ,[ImagePath])
                                  VALUES
                                        (@FullName
                                        ,@DateOfBirth
                                        ,@Nationality
                                        ,@ContactInfo
                                        ,@AuthorImagePath);
                            SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@FullName", FullName);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@Nationality", Nationality);

                    // Handle ContactInfo (which is nullable/optional)
                    if (!string.IsNullOrEmpty(ContactInfo))
                        command.Parameters.AddWithValue("@ContactInfo", ContactInfo);
                    else
                        command.Parameters.AddWithValue("@ContactInfo", System.DBNull.Value);

                    // Handle AuthorImagePath (which is nullable/optional)
                    if (!string.IsNullOrEmpty(ImagePath))
                        command.Parameters.AddWithValue("@AuthorImagePath", ImagePath);
                    else
                        command.Parameters.AddWithValue("@AuthorImagePath", System.DBNull.Value);

                    try
                    {
                        connection.Open();
                        // ExecuteScalar returns the first column of the first row (SCOPE_IDENTITY)
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                        {
                            ID = InsertedID;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the error message
                        Console.WriteLine("Error in AddNewAuthor: " + ex.Message);
                    }
                } // SqlCommand is disposed here
            } // SqlConnection is closed and disposed here

            return ID;
        }

    }
}
