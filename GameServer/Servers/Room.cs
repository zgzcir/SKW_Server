using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Common;

namespace GameServer.Servers
{
    enum RoomState
    {
        WaitingJoin,
        WaitingBattle,
        Battling,
        End
    }
    class Room
    {
        private RoomState state = RoomState.WaitingJoin;
        private List<Client> clientRoom = new List<Client>();
        private Server server;

        public Room(Server server)
        {
            this.server = server;
        }
        public bool IsWaitingJoin()
        {
            return state == RoomState.WaitingJoin;
        }

        public void AddClient(Client client)
        {
            clientRoom.Add(client);
            client.Room = this;
            if(clientRoom.Count>=2)
            {
                state = RoomState.WaitingBattle;
               
            }
        }
        public void RemoveClient(Client client)
        {
            client.Room = null;
            clientRoom.Remove(client);
            if (clientRoom.Count >= 2)
            {
                state = RoomState.WaitingBattle;

            }
            else
            {
                state = RoomState.WaitingJoin;
            }
        }

        public string GetHouseOwnerData()
        {
            return  clientRoom[0].GetUserData();
        }

        public void Close(Client client)//包含非房主的离开  此处是强制离开
        {
            if (client == clientRoom[0])
            {
                server.RemoveRoom(this);
            }
            else
                clientRoom.Remove(client);
        }
        public void Close()
        {
            foreach(Client client in clientRoom)
            {
                client.Room = null;
            }
            server.RemoveRoom(this);
        }
        public int  GetId()
        {
            if(clientRoom.Count>0)
            {
                return clientRoom[0].GetUserId();
            }
            return -1;
        }
        public string GetRoomData()
        {
            StringBuilder sb = new StringBuilder();
            foreach(Client client in clientRoom)
            {
                sb.Append(client.GetUserData()+"|");
            }
            if(sb.Length>0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }
        public void BroadcastMessage(Client excludeClient,ActionCode actionCode,string data)
        {
            foreach(Client client in clientRoom)
            {
                if(client!=excludeClient)
                {
                    server.SendResponse(client, actionCode, data);
                }
            }
        }
        public bool IsHouseOwner(Client client)
        {
            return client == clientRoom[0];
        }
        public void StartTimer()
        {
            new Thread(RunTimer).Start();
        }
        private void RunTimer()
        {
            for(int i=3; i>0; i--)
            {
         
                BroadcastMessage(null, ActionCode.ShowTimer, i.ToString());
                Thread.Sleep(1000);
            }
            BroadcastMessage(null, ActionCode.StartPlay,"r");
        }
    }
}
