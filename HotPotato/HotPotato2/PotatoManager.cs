using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotPotato
{
    public class PotatoManager
    {
        private static List<string> _clients = new List<string>();
        private static string _clientWithPotato = string.Empty;
        private static Random _random = new Random();

        public string ClientWithPotato
        {
            get { return _clientWithPotato; }
        }

        public bool HasPotato(string client)
        {
            return _clientWithPotato == client;
        }

        public bool PassPotato(string client, out string message)
        {
            if (!_clients.Contains(client))
            {
                message = "Can't send potato, you're not in the game! Reload to join.";
                return false;
            }

            if (_clientWithPotato == client)
            {
                var otherClients = GetClientsWithoutPotato();

                if (otherClients.Count > 0)
                {
                    _clientWithPotato = otherClients[_random.Next(otherClients.Count)];
                    message = $"Potato sent to {_clientWithPotato}";
                    return true;
                }
                {
                    message =  "Can't send potato, Nobody else is playing!";
                    return false;
                }
            }
            else
            {
                message = $"Can't send potato, {_clientWithPotato} has it!";
                return false;
            }
        }

        public List<string> GetClients()
        {
            var result = new List<string>();
            result.AddRange(_clients);

            return result;
        }

        public void AddClient(string client)
        {
            _clients.Add(client);

            if (_clientWithPotato == string.Empty)
            {
                _clientWithPotato = client;
            }
        }

        public List<string> GetClientsWithoutPotato()
        {
            var result = new List<string>();
            result.AddRange(_clients.Where(m => _clientWithPotato != m));

            return result;
        }

        public void ResetClients()
        {
            _clients.Clear();
            _clientWithPotato = string.Empty;
        }
    }
}