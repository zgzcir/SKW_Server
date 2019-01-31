using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
namespace Socket_Tcp_服务器端
{
    class Program
    {
        static int clientCount = 0;
        static byte[] dataBuffer = new byte[1024];
        //static List<Client> clientList = new List<Client>();
        static void Main(string[] args)
        {
            StartServerAsync();

            Console.ReadKey();
        }
        static Message msg = new Message();
        static void StartServerAsync()
        {
            Socket tcpServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpServer.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7788));
            //tcpServer.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7788));
            tcpServer.Listen(0);
            Console.WriteLine("server listening...");
            //Socket clientSocket = tcpServer.Accept();//pause here
            tcpServer.BeginAccept(AcceptCallBack, tcpServer);
            //clientSocket.BeginReceive(msg.Data, msg.startIndex, msg.RemainSize, SocketFlags.None, ReciveCallBack, clientSocket);

            //*******************************************
            //clientSocket.Send(System.Text.Encoding.UTF8.GetBytes("welcome"));
            //*******************************************
            //byte[] dataBuffer = new byte[1024];
            //int length = clientSocket.Receive(dataBuffer);
            //string msgRecive = System.Text.Encoding.UTF8.GetString(dataBuffer, 0, length);//pause here
            //Console.WriteLine(msgRecive);
            //*******************************************
            //clientSocket.Send(Encoding.UTF8.GetBytes(Console.ReadLine()));
            //Client client = new Client(clientSocket);
            //clientList.Add(client);
        }
  

        static void AcceptCallBack(IAsyncResult ar)
        {
            clientCount++;
            Console.WriteLine("nowClients: " + clientCount);
            Socket tcpServer = ar.AsyncState as Socket;
            
            Socket clientSocket = tcpServer.EndAccept(ar);
            clientSocket.Send(System.Text.Encoding.UTF8.GetBytes("welcome"));
            clientSocket.BeginReceive(msg.Data, msg.startIndex, msg.RemainSize, SocketFlags.None, ReciveCallBack, clientSocket);
            tcpServer.BeginAccept(AcceptCallBack, tcpServer);
        }
        static void ReciveCallBack(IAsyncResult ar)
        {
            Socket clientSocket = null;
            try
            {
                clientSocket = ar.AsyncState as Socket;
                int length = clientSocket.EndReceive(ar);
               
                if (length == 0)
                {
                    clientSocket.Close();
                    Console.WriteLine("someone has gone...");
                    return;
                }

                msg.AddCount(length);


                msg.ReadMessage();
                //string msgString = Encoding.UTF8.GetString(dataBuffer, 0, length);
                //Console.WriteLine("a Client said:" + msgString);

                clientSocket.BeginReceive(msg.Data, msg.startIndex, msg.RemainSize, SocketFlags.None, ReciveCallBack, clientSocket);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (clientSocket != null)
                {
                    clientSocket.Close();
                    clientCount--;
                }
            }


        }
        //public static void BroadCastMessage(string message)
        //{
        //    var notConnectedLIst = new List<Client>();
        //    foreach (var client in clientList)
        //    {
        //        if (client.Connected)
        //        {
        //            client.SendMessage(message);
        //        }
        //        else
        //        {
        //            notConnectedLIst.Add(client);
        //        }
        //    }
        //    foreach (var temp in notConnectedLIst)
        //    {
        //        clientList.Remove(temp);
        //    }
        //}

    }

}
