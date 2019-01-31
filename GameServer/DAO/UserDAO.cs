using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using GameServer.Modle;
namespace GameServer.DAO
{
    class UserDAO
    {
        public bool GetByUserName(MySqlConnection conn,string username)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("Select * from user where username=@username", conn);
                cmd.Parameters.AddWithValue("username", username);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在GetByUserName时发生了错误:" + e);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return false;
            
        }
        public User VerifyUser(MySqlConnection conn, string username, string password)
        {
            MySqlDataReader reader=null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("Select * from user where username =@username and password=@password", conn);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
                 reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    User user = new User(id, username, password);
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("当verifiuser时出现异常：" + e);

            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return null;
        }

        public void AddUSer(MySqlConnection conn, string username, string password)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into user set username=@username,password=@password", conn);

                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
                 cmd.ExecuteNonQuery();
               
            }
            catch (Exception e)
            {
                Console.WriteLine("当AddUser时出现异常：" + e);

            }
          
        }

    }
}
