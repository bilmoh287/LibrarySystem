using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LibraryDataAccessLayer
{
    public class clsBooksData
    {
        public static DataTable GetAllBooks()
        {
            DataTable dtbooks = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string Query = "SELECT * FROM Books;";

            SqlCommand command = new SqlCommand(Query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.HasRows)
                {
                    dtbooks.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }

            return dtbooks;
        }

        public static bool FindByName(string Title, ref int ID, ref string ISBN,
            ref DateTime PubDate, ref string Genre, ref string AdditionalInfo)
        {
            bool IsFound = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                string Query = "SELECT * FROM Books WHERE Title = @Title;";

                SqlCommand command = new SqlCommand(Query, connection);
                command.Parameters.AddWithValue("@Title", Title);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        ID = Convert.ToInt32(reader["BookID"]);
                        ISBN = (string)reader["ISBN"];
                        PubDate = (DateTime)reader["PublicationDate"];
                        Genre = (string)reader["Genre"];
                        AdditionalInfo = reader["AdditionInfo"] != DBNull.Value ? (string)reader["AdditionInfo"] : "";

                        IsFound = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return IsFound;
        }


        public static bool FindByID(int ID, ref string Title, ref string ISBN,
            ref DateTime PubDate, ref string Genre, ref string AdditionalInfo)
        {
            bool IsFound = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                string Query = "SELECT * FROM Books WHERE BookID = @ID;";

                SqlCommand command = new SqlCommand(Query, connection);
                command.Parameters.AddWithValue("@ID", ID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Title = (string)reader["Title"];
                        ISBN = (string)reader["ISBN"];
                        PubDate = (DateTime)reader["PublicationDate"];
                        Genre = (string)reader["Genre"];
                        AdditionalInfo = reader["AdditionInfo"] != DBNull.Value ? (string)reader["AdditionInfo"] : "";

                        IsFound = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return IsFound;
        }

        public static int AddNewBook(string Title, String ISBN, DateTime PubDate, string Genre, string AdditionalInfo)
        {
            int ID = -1;

            SqlConnection connection = new SqlConnection (clsDataAccessSetting.ConnectionString);

            string Query = @"
                            INSERT INTO [dbo].[Books]
                                       ([Title]
                                       ,[ISBN]
                                       ,[PublicationDate]
                                       ,[Genre]
                                       ,[AdditionInfo])
                                 VALUES
                                       (@Title
                                       ,@ISBN
                                       ,@PublicationDate
                                       ,@Genre
                                       ,@AdditionalInfo);
                                SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@Title", Title);
            command.Parameters.AddWithValue("@ISBN", ISBN);
            command.Parameters.AddWithValue("@PublicationDate", PubDate);
            command.Parameters.AddWithValue("@Genre", Genre);
            if (AdditionalInfo != "")
                command.Parameters.AddWithValue("@AdditionalInfo", AdditionalInfo);
            else
                command.Parameters.AddWithValue("@AdditionalInfo", System.DBNull.Value);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    ID = InsertedID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }

            return ID;

        }

        public static bool UpdateBook(int ID, string Title, string ISBN, DateTime PubDate, string Genre, string AdditionalInfo)
        {
            bool isUpdated = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                string Query = @"
                                UPDATE Books
                                SET Title = @Title,
                                    ISBN = @ISBN,
                                    PublicationDate = @PublicationDate,
                                    Genre = @Genre,
                                    AdditionInfo = @AdditionalInfo
                                WHERE BookID = @ID;";

                SqlCommand command = new SqlCommand(Query, connection);
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@Title", Title);
                command.Parameters.AddWithValue("@ISBN", ISBN);
                command.Parameters.AddWithValue("@PublicationDate", PubDate);
                command.Parameters.AddWithValue("@Genre", Genre);
                if (!string.IsNullOrEmpty(AdditionalInfo))
                    command.Parameters.AddWithValue("@AdditionalInfo", AdditionalInfo);
                else
                    command.Parameters.AddWithValue("@AdditionalInfo", DBNull.Value);

                try
                {
                    connection.Open();
                    int rows = command.ExecuteNonQuery();
                    isUpdated = (rows > 0);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return isUpdated;
        }
    }
}
