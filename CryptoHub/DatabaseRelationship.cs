using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CryptoHub
{
    public class DatabaseRelationship
    {
        static string connectionStr = @"Server=localhost\SQLEXPRESS;Database=CryptoHubDB;Trusted_Connection=True;";
        SqlConnection connection = new SqlConnection(connectionStr);
        string sql;
        SqlCommand command;
        SqlDataReader reader;
        private DataTable dataTable = new DataTable();

        public List<Tuple<int, string, DateTime, decimal>> ReadWatchListTable()
        {
            connection.Open();
            sql = "SELECT * from WatchList_tbl";
            command = new SqlCommand(sql, connection);

            List<Tuple<int, string, DateTime, decimal>> watchListCoinsList = new List<Tuple<int, string, DateTime, decimal>>();

        //SqlDataAdapter da = new SqlDataAdapter(command);
        // this will query your database and return the result to your datatable
        //da.Fill(dataTable);
        reader = command.ExecuteReader();
            while (reader.Read())
            {
                Tuple<int, string, DateTime, decimal> coin = new Tuple<int, string, DateTime, decimal>(Convert.ToInt16(reader.GetValue(0)),reader.GetValue(1).ToString(), Convert.ToDateTime(reader.GetValue(2)), Convert.ToDecimal(reader.GetValue(3)));
                watchListCoinsList.Add(coin);
            }

            reader.Close();
            connection.Close();
            return watchListCoinsList;
        }

        public void AddToWatchListTable(string symbol, DateTime date, decimal price)
        {
            connection.Open();
            sql = "INSERT INTO WatchList_tbl VALUES(@coin, @date, @price)";
            command = new SqlCommand(sql, connection);
            
            command.Parameters.AddWithValue("@coin", symbol);
            command.Parameters.AddWithValue("@date", date);
            command.Parameters.AddWithValue("@price", price);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public void RemoveFromWatchListTable(int id)
        {
            connection.Open();
            sql = "DELETE FROM WatchList_tbl WHERE ID = @id";
            command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
