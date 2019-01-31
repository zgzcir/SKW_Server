using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
namespace tcpClient
{


    class Program
    {

        static void Main(string[] args)
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7788));
            byte[] dataRec = new byte[1024];
            var length = clientSocket.Receive(dataRec);//pause here
            string msgRec = Encoding.UTF8.GetString(dataRec, 0, length);
            Console.WriteLine(msgRec);


            //while (true)
            //{
            //    string s = Console.ReadLine();
            //    if (s == "q")
            //    {
            //        clientSocket.Close();
            //        return;
            //    }
            //    clientSocket.Send(Encoding.UTF8.GetBytes(s));
            //}
             
            for(int i=0;i<100;i++)
            {
                clientSocket.Send(Message.GetBytes(i.ToString()));
            }


            //length =clientSocket.Receive(dataRec);
            //Console.WriteLine(Encoding.UTF8.GetString(dataRec, 0, length));
            //Console.ReadLine();

            clientSocket.Close();




        }

    }
}
