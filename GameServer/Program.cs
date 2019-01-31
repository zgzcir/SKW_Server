using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using GameServer.Servers;
namespace GameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server gameServer = new Server("127.0.0.1", 6688);//172.18.177.108
            gameServer.Start();
            Console.ReadKey();        }
    }
}
