using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDataAccessLayer
{
    public class clsBookAuthorData
    {
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
    }
}
