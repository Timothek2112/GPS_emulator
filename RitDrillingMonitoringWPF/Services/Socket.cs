using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RitDrillingMonitoringWPF.Services
{
    internal class Socket
    {
        ClientWebSocket socket;
        string connectionString;

        public Socket(string connectionString)
        {
            socket = new ClientWebSocket();
            this.connectionString = connectionString;
        }

        public async Task Open()
        {
            await socket.ConnectAsync(new Uri(connectionString), CancellationToken.None);
        }

        public async Task Send(string message)
        {
            if (socket.State != WebSocketState.Open)
                return;
            await socket.SendAsync(Encoding.UTF8.GetBytes(message), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task Close()
        {
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "OK", CancellationToken.None);
        }
    }
}
