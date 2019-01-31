using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Servers;
using GameServer.DAO;
using GameServer.Modle;
namespace GameServer.Controller
{
    class UserController : BaseController
    {
        private UserDAO userDAO = new UserDAO();
        private ResultDAO resultDAO = new ResultDAO();
        public UserController()
        {
            requestCode = RequestCode.User;
        }
        public string Login(string data, Client client, Server server)
        {
            Console.WriteLine("Login方法被调用了!");
            string[] strs = data.Split(',');
            User user = userDAO.VerifyUser(client.MySQLconn, strs[0], strs[1]);
            if (user == null)
            {
                return ((int)ReturnCode.Fail).ToString();
            }
            else
            {
                Result res = resultDAO.GetResultByUserId(client.MySQLconn, user.Id);
                client.SetUserData(user, res);
                return string.Format("{0},{1},{2},{3}", ((int)ReturnCode.Success).ToString(),user.Username,res.TotalCount,res.WinCount);
                 

            }
        }
        public string Register(string data,Client client,Server server)
        {
            string[] strs = data.Split(',');
            string username = strs[0];
            string password = strs[1];
            bool res = userDAO.GetByUserName(client.MySQLconn,username);
if(res)
            {
                return ((int)ReturnCode.Fail).ToString();

            }
            else
            {
                userDAO.AddUSer(client.MySQLconn, username, password);
                return ((int)ReturnCode.Success).ToString();
            }
            return null;
        }
    }
}
