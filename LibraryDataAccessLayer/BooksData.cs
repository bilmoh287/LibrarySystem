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
                while (reader.Read())
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
    }
}
