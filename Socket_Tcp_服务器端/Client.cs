//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Net;
//using System.Threading.Tasks;
//using System.Net.Sockets;
//using System.Threading;
//namespace Socket_Tcp_服务器端
//{
//    class Client
//    {
//        private Socket clientSocket;
//        List<byte[]> dataList = new List<byte[]>();
//        private Thread t;
//        private byte[] data = new byte[1024];

//        public Client(Socket s)
//        {
//            clientSocket = s;
//            t = new Thread(ReciveMessage);
//            t.Start();
//        }
//        private void ReciveMessage()
//        {
//            while (true)
//            {

//                if (clientSocket.Poll(10, SelectMode.SelectRead))
//                {
//                    Console.WriteLine("client has gone...");
//                    clientSocket.Close();
//                    break;
//                }
//                int length = clientSocket.Receive(data);
//                string message = Encoding.UTF8.GetString(data, 0, length);
//                Program.BroadCastMessage(message);
//                Console.WriteLine("client：" + message);

//            }

//        }
//        //public void ReciveMessage()
//        //{
//        //    clientSocket.Receive(dataList[dataList.Count]);
//        //}
//        public void SendMessage(string message)
//        {
//            byte[] data = Encoding.UTF8.GetBytes(message);
//            clientSocket.Send(data);
//        }

//        public bool Connected
//        {

//            get
//            {
//                return clientSocket.Connected;
//            }

//        }
//    }
//}
