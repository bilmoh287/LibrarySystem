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

        public static bool FindByID(int AuthorID, ref string FullName, ref DateTime DateOfBirth,
             ref string Nationality, ref string ContactInfo, ref string ImagePath)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string Query = "SELECT * FROM Authors WHERE AuthorID = @AuthorID";

            using (SqlCommand command = new SqlCommand(Query, connection))
            {
                command.Parameters.AddWithValue("@AuthorID", AuthorID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        // no nulls allowed here
                        FullName = (string)reader["FullName"];
                        DateOfBirth = (DateTime)reader["DateOfBirth"];
                        Nationality = (string)reader["Nationality"];

                        // only these need null checks
                        ContactInfo = reader["ContactInfo"] != DBNull.Value ? (string)reader["ContactInfo"] : "";
                        ImagePath = reader["ImagePath"] != DBNull.Value ? (string)reader["ImagePath"] : "";

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

        public static bool UpdateAuthor(int AuthorID, string FullName, DateTime DateOfBirth,
                                        string Nationality, string ContactInfo, string ImagePath)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                string Query = @"
                                UPDATE [dbo].[Authors]
                                SET [FullName] = @FullName,
                                    [DateOfBirth] = @DateOfBirth,
                                    [Nationality] = @Nationality,
                                    [ContactInfo] = @ContactInfo,
                                    [ImagePath] = @ImagePath
                                WHERE AuthorID = @AuthorID";

                SqlCommand command = new SqlCommand(Query, connection);

                command.Parameters.AddWithValue("@AuthorID", AuthorID);
                command.Parameters.AddWithValue("@FullName", FullName);
                command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                command.Parameters.AddWithValue("@Nationality", Nationality);

                // Handle ContactInfo
                command.Parameters.AddWithValue("@ContactInfo",
                    string.IsNullOrEmpty(ContactInfo) ? (object)DBNull.Value : ContactInfo);

                // Handle ImagePath
                command.Parameters.AddWithValue("@ImagePath",
                    string.IsNullOrEmpty(ImagePath) ? (object)DBNull.Value : ImagePath);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in UpdateAuthor: " + ex.Message);
                }
            }

            return rowsAffected > 0;
        }

        public static bool DeleteAuthor(int AuthorID)
        {
            bool IsDeleted = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string Query = "DELETE FROM Authors WHERE AuthorID = @AuthorID";

            using (SqlCommand command = new SqlCommand(Query, connection))
            {
                command.Parameters.AddWithValue("@AuthorID", AuthorID);

                try
                {
                    connection.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                    if (rowsaffected > 0)
                    {
                        IsDeleted = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return IsDeleted;
        }

        public static bool AddRelation(int bookID, int authorID)
        {
            bool isAdded = false;

            string query = "INSERT INTO BookAuthor (BookID, AuthorID) VALUES (@BookID, @AuthorID)";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@BookID", bookID);
                command.Parameters.AddWithValue("@AuthorID", authorID);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    isAdded = (rowsAffected > 0);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in AddRelation: " + ex.Message);
                }
            }

            return isAdded;
        }

        public static DataTable GetAllBooksWrittenByAuthor(int AuthorID)
        {
            DataTable dtBooks = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string Query = @"
                            SELECT Books.BookID, Books.Title, Books.ISBN, Books.PublicationDate, Books.Genre, Books.AdditionInfo
                            FROM     Books INNER JOIN
                                              BookAuthors ON Books.BookID = BookAuthors.BookID
                            WHERE BookAuthors.AuthorID = @AuthorID";

            using (SqlCommand command = new SqlCommand(Query, connection))
            {
                command.Parameters.AddWithValue("@AuthorID", AuthorID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if(reader.HasRows)
                    {
                        dtBooks.Load(reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return dtBooks;
        }

    }
}
