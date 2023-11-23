using Khadamati.DAL;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Khadamati.SignalrHub
{
    public class NotificationHub : Hub
    {
        private readonly NotificationRepo _repo;
        public NotificationHub(NotificationRepo repo )
        {
            _repo = repo;
        }
        
        public async void Sendnotification(string text , string id )
        {
            Notification notification = new Notification();
            notification.Text = text;   
            notification.date = DateTime.Now;
            notification.UserId = id;
            _repo.AddNotification(notification);
        }
        public async void SendMessage(Chat_Message chat)
        {

            #region Get Users
            User? sender = _context.Users.Where(u => u.Id == chat.Sender_ID).FirstOrDefault();
            User? receiver = _context.Users.Where(u => u.Id == chat.Receiver_ID).FirstOrDefault();

            if (sender == null || receiver == null) return;
            #endregion

            #region Get Recever Connections ID
            IReadOnlyList<string> listOfConnectionsID = _context.User_Connections.Where(uc => uc.User_ID == receiver.Id).Select(uc => uc.Connection_ID).ToImmutableList();
            #endregion

            #region Save Into Database
            try
            {
                _context.Chat_Messages.Add(chat);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            #endregion

            #region Call Back Function
            //await Clients.Clients(listOfConnectionsID).SendAsync("retrieveMessage", chat.Message,chat.Date,chat.Time,chat.Receiver_ID,chat.Sender_ID,chat.Id);
            await Clients.Clients(listOfConnectionsID).SendAsync("retrieveMessage", chat.Message);

            #endregion
        }

        #region On Connected
        public override Task OnConnectedAsync()
        {
            if (Context.GetHttpContext().Request.Query.TryGetValue("userId", out var userId))
            {
                int userid = int.Parse(userId);

                _context.User_Connections.Add(new User_Connection { User_ID = userid, Connection_ID = Context.ConnectionId });
                _context.SaveChanges();
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (Context.GetHttpContext().Request.Query.TryGetValue("userId", out var userId))
            {
                int userid = int.Parse(userId);

                _context.User_Connections.Remove(new User_Connection { User_ID = userid, Connection_ID = Context.ConnectionId });
                _context.SaveChanges();
            }

            return base.OnDisconnectedAsync(exception);
        }
        


    }






}
}
