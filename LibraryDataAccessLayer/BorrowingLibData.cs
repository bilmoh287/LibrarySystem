using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDataAccessLayer
{
    public class clsBorrowingLibData
    {
        public static DataTable GetAllBooks()
        {
            DataTable dtbooks = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string Query = "SELECT BookID, Title, ISBN, PublicationDate, Genre FROM Books;";

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

        public static string GetBooksAddionalInfo(int BorrowingID)
        {
            string additionalInfo = "";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                string Query = "SELECT AdditionInfo FROM Books WHERE BookID = @BookID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@BorrowingID", BorrowingID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar(); // returns first column of first row
                        if (result != null && result != DBNull.Value)
                        {
                            additionalInfo = result.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error in GetBorrowedBookAdditionalInfo: " + ex.Message);
                    }
                }
            }

            return additionalInfo;
        }


        public static int AddNewLibrary(int BookID, int UserID, DateTime BorrowingDate, DateTime DueDate, DateTime ActualReturnDate)
        {
            int NewID = -1;

            // Use 'using' statement for better resource management
            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                string Query = @"
                            INSERT INTO [dbo].[Borrowing]
                                        ([CopyID]
                                        ,[UserID]
                                        ,[BorrowingDate]
                                        ,[DueDate]
                                        ,[ActualReturnDate])
                                  VALUES
                                        (@CopyID
                                        ,@UserID
                                        ,@BorrowingDate
                                        ,@DueDate
                                        ,@ActualReturnDate);
                            SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@CopyID", BookID);
                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.AddWithValue("@BorrowingDate", BorrowingDate);
                    command.Parameters.AddWithValue("@DueDate", DueDate);

                    //insert null untill Returning Book
                    command.Parameters.AddWithValue("@ActualReturnDate", DBNull.Value);


                    try
                    {
                        connection.Open();
                        // ExecuteScalar returns the first column of the first row (SCOPE_IDENTITY)
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                        {
                            NewID = InsertedID;
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

        public static DataTable GetAllBorrowedBooks(int UserID)
        {
            DataTable drBorrowedBooks = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string Query = @"SELECT Borrowing.BorrowingID, Users.Username, Books.Title AS BookTitle, Books.ISBN, 
                                   Borrowing.BorrowingDate, Borrowing.DueDate
                            FROM Borrowing
                            INNER JOIN Users ON Users.UserID = Borrowing.UserID
                            INNER JOIN Books ON Borrowing.CopyID = Books.BookID
                            WHERE Borrowing.ActualReturnDate IS NULL
                              AND Borrowing.UserID = @UserID;";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    drBorrowedBooks.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }

            return drBorrowedBooks;
        }

        public static bool ReturnBook(int borrowingID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                string Query = @"
                                UPDATE [dbo].[Borrowing]
                                SET ActualReturnDate = @ReturnDate
                                WHERE BorrowingID = @BorrowingID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@ReturnDate", DateTime.Now);
                    command.Parameters.AddWithValue("@BorrowingID", borrowingID);

                    try
                    {
                        connection.Open();
                        return command.ExecuteNonQuery() > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error in ReturnBook: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        public static bool IsBookAlreadyBorrowed(int BookID, int UserID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                string Query = @"
                                SELECT COUNT(*) 
                                FROM Borrowing
                                WHERE CopyID = @BookID 
                                  AND UserID = @UserID
                                  AND ActualReturnDate IS NULL";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@BookID", BookID);
                    command.Parameters.AddWithValue("@UserID", UserID);

                    try
                    {
                        connection.Open();
                        int count = (int)command.ExecuteScalar();
                        return count > 0; // true if book is already borrowed
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error in IsBookAlreadyBorrowed: " + ex.Message);
                        return false;
                    }
                }
            }
        }

    }
}
