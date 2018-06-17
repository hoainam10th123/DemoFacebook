using FacebookLite.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacebookLite
{
    public class ChatHub : Hub
    {
        static List<_User> loginUsers = new List<_User>();

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            string contextid = Context.ConnectionId;
            return base.OnDisconnected(stopCalled);
        }

        public void Connected(string userName)
        {
            string connectionId = Context.ConnectionId;
            loginUsers.Add(new _User { Id = connectionId, UserName = userName });

            Clients.All.onNewUserConnected(userName);            
        }

        public void Disconnected(string userName)
        {
            string connectionId = Context.ConnectionId;
            Clients.All.onUserDisconnected(userName);
        }

        public void Send(string fromUsername, string usernameSelect, string message)
        {
            string fromUserId = Context.ConnectionId;
            var toUser = loginUsers.FirstOrDefault(x => x.UserName == usernameSelect);

            Clients.Client(toUser.Id).sendPrivateMessage(fromUsername, usernameSelect, message);            
        }
    }
}