using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Common;
using MySql.Data.MySqlClient;
using GameServer.Tool;
using GameServer.Modle;
namespace GameServer.Servers
{
    class Client
    {
        private Socket clientSocket;
        private Server server;

        private User user;
        private Result result;

        private Message msg = new Message();
        private MySqlConnection mySqlConn;


        private Room room;

        public Room Room
        {
            set
            {
                room = value;
            }
            get
            {
                return room;
            }
        }
        public MySqlConnection MySQLconn
        {
            get
            {
                return mySqlConn;
            }
        }

        public User User
        {

            set
            {
                user = value;
            }
        }
        public Result Result
        {
            set
            {
                result = value;
            }
        }

        public void SetUserData(User user,Result result)
        {
            this.user = user;
            this.result = result;

        }
        public string GetUserData()
        {
            return user.Id+":"+user.Username + ":" + result.TotalCount + ":" + result.WinCount;
        }
        public int GetUserId()
        {
            return user.Id;
        }
        public Client() { }
        public Client(Socket clientSocket, Server server)
        {
            this.clientSocket = clientSocket;
            this.server = server;
            mySqlConn = ConnectHelper.Connect();


        }
        public void Start()
        {
            if (clientSocket == null || clientSocket.Connected == false)
            {
                return;
            }
            clientSocket.BeginReceive(msg.data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReciveCallBack, null);
        }

        public void ReciveCallBack(IAsyncResult ar)
        {

            try
            {
                if(clientSocket==null||clientSocket.Connected==false)
                {
                    if (clientSocket != null)
                    {
                        Console.WriteLine("一个clientSocket已在内存中清理");
                    }
                    else if(clientSocket.Connected==false)
                    {
                        Console.WriteLine("一个clientSocket的连接被断开");

                    }
                    return;
                }
                int count = clientSocket.EndReceive(ar);
                if (count == 0)
                {
                    Close();
                }
                msg.ReadMessage(count, OnProcessMessage);//CALLBACK!  
                Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Close();
            }

        }
        private void OnProcessMessage(RequestCode requestCode, ActionCode actionCode, string data)
        {
            server.HandleRequest(requestCode, actionCode, data, this);
        }


        //public void Send(RequestCode requestCode,string data)
        //{

        //}
        public void Send(ActionCode actionCode, string data)
        {
            Console.WriteLine("向客户端发送了一条消息:"+data+"    ///"+actionCode+"///");
            byte[] bytes = Message .PackData(actionCode, data);
            clientSocket.Send(bytes);
        }

        private void Close()
        {
            ConnectHelper.CloseConnection(mySqlConn);
            if (clientSocket != null)
            {
                Console.WriteLine("一个远程连接已关闭");

                clientSocket.Close();
                if (room != null)
                 {
                    room.Close(this);
                }
                server.RemoveClient(this);
             
            }

        }
        public bool  IsHouseOwner()
        {
            return room.IsHouseOwner(this);
        }

       
    }

}
