using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace HotPotato
{
    public class PotatoHub : Hub
    {
        PotatoManager _potatoManager;

        public PotatoHub(PotatoManager potatoManager)
        {
            _potatoManager = potatoManager;
        }

        public void PassPotato()
        {
            string message = string.Empty;

            if (_potatoManager.PassPotato(Context.ConnectionId, out message))
            {
                UpdateClients();
            }
            else
            {
                UpdateClientWithError(Context.ConnectionId, message);
            }
        }

        public void AddClient()
        {
            _potatoManager.AddClient(Context.ConnectionId);
            UpdateClient(Context.ConnectionId);
        }

        public void UpdateClient(string client)
        {
            Clients.Client(client).UpdatePotatoState(_potatoManager.ClientWithPotato, _potatoManager.HasPotato(client));
        }

        public void UpdateClientWithError(string client, string error)
        {
            Clients.Client(client).UpdateClientWithError(error);
        }

        public void UpdateClients()
        {
            Clients.Client(_potatoManager.ClientWithPotato).UpdatePotatoState(_potatoManager.ClientWithPotato, true);
            Clients.Clients(_potatoManager.GetClientsWithoutPotato()).UpdatePotatoState(_potatoManager.ClientWithPotato, false);
        }

        public void ResetGame()
        {
            var allClients = _potatoManager.GetClients();

            var otherClients = allClients.Where(client => client != Context.ConnectionId).ToList();

            _potatoManager.ResetClients();

            Clients.Clients(otherClients).GameReset();

            AddClient();
        }
    }
}