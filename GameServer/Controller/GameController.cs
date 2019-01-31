using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Servers;

namespace GameServer.Controller
{
    class GameController: BaseController
    {

        public GameController()
        {
            requestCode = RequestCode.Game;
        }
        public string StartGame(string data, Client client, Server server)
        {
            if(client.IsHouseOwner())
            {
                Room room = client.Room;
                room.BroadcastMessage(client, ActionCode.StartGame, ((int)ReturnCode.Success).ToString());
                
               room.StartTimer();
               return  ((int)ReturnCode.Success).ToString();
            }
            else
            {
                return ((int)ReturnCode.Fail).ToString();
            }


        }
    }
}
