using System;
using System.Collections.Generic;
using System.Data;
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


        public static DataTable GetAllAuthorWhoWroteTheBook(int BookID)
        {
            DataTable dtAuthors = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string Query = @"
                            SELECT Authors.FullName, Authors.DateOfBirth, Authors.Nationality, Authors.ContactInfo, Authors.ImagePath
                            FROM     Authors INNER JOIN
                                              BookAuthors ON Authors.AuthorID = BookAuthors.AuthorID
                            WHERE BookAuthors.BookID = @BookID";

            using (SqlCommand command = new SqlCommand(Query, connection))
            {
                command.Parameters.AddWithValue("@BookID", BookID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        dtAuthors.Load(reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return dtAuthors;
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

                    if (reader.HasRows)
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
