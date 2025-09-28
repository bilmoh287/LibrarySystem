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

        public static bool FindByName(string Name, ref int ID, ref string ISBN
            , ref DateTime PubDate, ref string Genre, ref string AdditionalInfo)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string Query = "SELECCT * FROM Books WHERE Name = @Name;";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("Name", Name);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    ID = Convert.ToInt32(reader["ID"]);
                    Name = (string)reader["Name"];
                    ISBN = (string)reader["ISBN"];
                    PubDate = (DateTime)reader["PubDate"];
                    AdditionalInfo = (string)reader["AdditionalInfo"];

                    IsFound = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }

            return IsFound;
        }

        public static bool FindByID(int ID, ref string Name, ref string ISBN
            , ref DateTime PubDate, ref string Genre, ref string AdditionalInfo)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string Query = "SELECCT * FROM Books WHERE BookID = @ID;";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("ID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ID = Convert.ToInt32(reader["ID"]);
                    Name = (string)reader["Name"];
                    ISBN = (string)reader["ISBN"];
                    PubDate = (DateTime)reader["PubDate"];
                    AdditionalInfo = (string)reader["AdditionalInfo"];

                    IsFound = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }

            return IsFound;
        }
        public static int AddNewBook(string Name, String ISBN, DateTime PubDate, string Genre, string AdditionalInfo)
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
                                       @(Name
                                       ,@ISBN
                                       ,@PubDate
                                       ,@Genre
                                       ,@AdditionalInfo);
                                SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@Title", Name);
            command.Parameters.AddWithValue("@ISBN", ISBN);
            command.Parameters.AddWithValue("@PublicationDate", PubDate);
            command.Parameters.AddWithValue("@Genre", Genre);
            if (AdditionalInfo != "")
                command.Parameters.AddWithValue("@AdditionalInfo", Genre);
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
    }
}
