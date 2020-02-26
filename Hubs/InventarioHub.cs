using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using InventarioApi.AuxModels;

namespace InventarioApi.Hubs
{
    public class InventarioHub : Hub
    {
        public override Task  OnConnectedAsync(){

            Console.WriteLine("New connection");
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception){
            Console.WriteLine("Se desconecto");

            return base.OnDisconnectedAsync(exception);
        }
         public async Task SendPosition(int idPedido, Coordinates coordeadas )
         {
             await Clients.All.SendAsync("RecibirCoordenadas", idPedido, coordeadas);
         }
    }
}