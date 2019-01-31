using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace Mysql
{
    class Program
    {
        static void Main(string[] args)
        {
            string conStr = "Database=test001;Data Source=127.0.0.1;port=3306;User Id=root;Password=a123456789...;";
            MySqlConnection conn = new MySqlConnection(conStr);
            conn.Open();
            #region serach
            //MySqlCommand cmd = new MySqlCommand("select * from user",conn);
            //MySqlDataReader reader= cmd.ExecuteReader();
            //while (reader.Read())
            //{
            //    string username= reader.GetString("username");
            //    string password = reader.GetString(2);
            //    Console.WriteLine(username + ":" + password);
            //}
            //reader.Close();
            #endregion
            #region insert
            //string username = "cwer";
            //string password="sss';delete from user;";
            ////MySqlCommand cmd = new MySqlCommand("insert into user set username='"+ username+"',password='" + password + "'",conn);
            //MySqlCommand cmd = new MySqlCommand("insert into user set username=@un,password=@pwd", conn);
            //cmd.Parameters.AddWithValue("un", username);
            //cmd.Parameters.AddWithValue("pwd", password);
            //cmd.ExecuteNonQuery();
            #endregion
            #region delete
            //int deleteId=0;
            //MySqlCommand cmd = new MySqlCommand("delete from user where id =@id", conn);
            //cmd.Parameters.AddWithValue("id",deleteId);
            //cmd.ExecuteNonQuery(); 
            #endregion
            #region update
            MySqlCommand cmd = new MySqlCommand("update user set password =@pwd where id=1");
            cmd.Parameters.AddWithValue("pwd", "a123");
            cmd.ExecuteNonQuery();
            #endregion
            conn.Close();
            Console.ReadKey();
        } 
    }
}
