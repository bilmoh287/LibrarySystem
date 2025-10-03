using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDataAccessLayer
{
    public class clsBorrowingLibData
    {
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
    }
}
