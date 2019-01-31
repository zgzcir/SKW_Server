using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace GameServer.Tool
{
    class ConnectHelper
    {
        public const string CONNECTIONSTRING = "datasource=127.0.0.1;port=3306;database=game_sikila;user=root;pwd=a123456789...;";
        public static MySqlConnection Connect()
        {
            MySqlConnection conn = new MySqlConnection(CONNECTIONSTRING);
            try
            {
                conn.Open();
                return conn;

            }
            catch (Exception e)
            {
                Console.WriteLine("Connect to Database wrong:" + e + ".");
                return null;
            }
        }
        public static void CloseConnection(MySqlConnection conn)
        {
            

            if(conn!=null)
            {
                conn.Close();
            }
            else
            {
                Console.WriteLine("conn cant be null");
            }
        }

    }

}
