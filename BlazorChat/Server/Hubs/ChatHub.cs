using Microsoft.AspNetCore.SignalR;

namespace BlazorChat.Server.Hubs
{
    public class ChatHub : Hub
    {
        private static Dictionary<string, string> usuarios = new Dictionary<string,string>();

        //metodo para conectarse
        public override async Task OnConnectedAsync()
        {
            string username = Context.GetHttpContext().Request.Query["username"];
            usuarios.Add(Context.ConnectionId, username);
            await AddMessageToChat(string.Empty, $"**{username} a ingresado al chat epicamente**");
            await base.OnConnectedAsync();
        }
        //metodo para desconectarse
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            //evaluamos si el usuario esta conectado
            string username = usuarios.FirstOrDefault(u => u.Key == Context.ConnectionId).Value;
            await AddMessageToChat(string.Empty, $"**{username} Abandonó el chat**");

        }

        //metodo que añade los mensaje
        public async Task AddMessageToChat( string user, string message)
        {
            await Clients.All.SendAsync("Mensaje Recibido",user, message);
        }



    }
}
