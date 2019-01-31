using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Modle;
using MySql.Data.MySqlClient;
namespace GameServer.DAO
{
    class ResultDAO
    {
        public Result GetResultByUserId(MySqlConnection conn, int userId)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("Select * from result where userId =@userId", conn);
                cmd.Parameters.AddWithValue("userId", userId);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                { 
                    int id = reader.GetInt32("id");
                    int totalCount = reader.GetInt32("totalcount");
                    int winCount = reader.GetInt32("wincount");
                    Result res = new Result(id, userId, totalCount, winCount);
                    return res;
                }
                else
                {
                    Result res = new Result(-1, userId, 0, 0);
                    return res;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("当GetResultByUserId 时出现异常：" + e);

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

    }
}
