using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoHub
{   
    //class that handles database relationships.
    public class SQLiteRelation
    {
        //connection string
        SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source= database.db; Version = 3; New = True; Compress = True;");

        //Method that creats the table if it doesn't exist.
        public void CreateTable()
        {
            //first we check if table exists with the method below.
            if (!checkIfExist("WatchList_tbl"))
            {
                //if it does not we create it
                sqlite_conn.Open();
                string sql = "CREATE TABLE WatchList_tbl ([ID] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, [Coin] [nchar](15) NOT NULL,[DateOfAddition] [date] NOT NULL, [PriceWhenAdded] [decimal](18, 0) NOT NULL)";
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = sql;
                sqlite_cmd.ExecuteNonQuery();
                sqlite_conn.Close();
                //ALWAYS close the connection
            }
        }

        //method that checks if a table exists
        public bool checkIfExist(string tableName)
        {
            sqlite_conn.Open();
            SQLiteCommand command;
            command = sqlite_conn.CreateCommand();
            command.CommandText = "SELECT name FROM sqlite_master WHERE name='" + tableName + "'";
            var result = command.ExecuteScalar();
            sqlite_conn.Close();
            return result != null && result.ToString() == tableName ? true : false;
        }

        //method that reads from the table and returns a list of the watched coins in the database.
        public List<Tuple<int, string, DateTime, decimal>> ReadWatchListTable()
        {
            //select everything from the table
            sqlite_conn.Open();
            string sql = "SELECT * from WatchList_tbl";

            //create empty coinlist
            List<Tuple<int, string, DateTime, decimal>> watchListCoinsList = new List<Tuple<int, string, DateTime, decimal>>();

            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = sql;

            //use datareader to raed table and fill the list

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                Tuple<int, string, DateTime, decimal> coin = new Tuple<int, string, DateTime, decimal>(Convert.ToInt16(sqlite_datareader.GetValue(0)), sqlite_datareader.GetValue(1).ToString(), Convert.ToDateTime(sqlite_datareader.GetValue(2)), Convert.ToDecimal(sqlite_datareader.GetValue(3)));
                watchListCoinsList.Add(coin);
            }
            sqlite_conn.Close();
            //return the list
            return watchListCoinsList;
        }

        //method that adds to the watchlist table
        public void AddToWatchListTable(string symbol, DateTime date, decimal price)
        {
            //Add to list with parameters cause it is safer
            sqlite_conn.Open();
            string sql = "INSERT INTO WatchList_tbl([Coin],[DateOfAddition],[PriceWhenAdded]) VALUES(@coin, @date, @price)";
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = sql;
            sqlite_cmd.Parameters.AddWithValue("@coin", symbol);
            sqlite_cmd.Parameters.AddWithValue("@date", date);
            sqlite_cmd.Parameters.AddWithValue("@price", price);
            sqlite_cmd.ExecuteNonQuery();
            sqlite_conn.Close();
        }

        //method that removes from the watchlist table
        public void RemoveFromWatchListTable(int id)
        {
            sqlite_conn.Open();
            string sql = "DELETE FROM WatchList_tbl WHERE ID = @id";
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = sql;
            sqlite_cmd.Parameters.AddWithValue("@id", id);
            sqlite_cmd.ExecuteNonQuery();
            sqlite_conn.Close();
        }
    }
}
