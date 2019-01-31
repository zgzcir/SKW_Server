using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.IO;
using System.Reflection;
using GameServer.Servers;
namespace GameServer.Controller
{
    class ControllerManager
    {
        private Dictionary<RequestCode, BaseController> controllerDict = new Dictionary<RequestCode, BaseController>();
        private Server server;
        public ControllerManager(Server server)
        {
            this.server = server;
            InitController();
        }
        void InitController()
        {
            DefaultController defaultController = new DefaultController();
            controllerDict.Add(defaultController.RequestCode, defaultController);
            controllerDict.Add(RequestCode.User, new UserController());
            controllerDict.Add(RequestCode.Room, new RoomController());
            controllerDict.Add(RequestCode.Game, new GameController());
        }

        public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data,Client client)
        {
            BaseController controller;
            bool isGet = controllerDict.TryGetValue(requestCode, out controller);
            if (!isGet)
            {
                Console.WriteLine("Cant Get " + requestCode + " Handler");
                return;
            }
            string methodName = Enum.GetName(typeof(ActionCode), actionCode);
            MethodInfo mi = controller.GetType().GetMethod(methodName);
            if (mi == null)
            {
                Console.WriteLine("[warning]no method:<" + controller.GetType() + "." + methodName + ">");
            }
            object[] parameters = new object[] { data ,client,server};
            object o = mi.Invoke(controller, parameters);
             if(o==null||string.IsNullOrEmpty(o as string))
            {
                return;
            }
            server.SendResponse(client, actionCode, o as string);
        }
    }
}
