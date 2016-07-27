using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotPotato.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //to run locally, set the connection string to http://localhost:17553 in app settings. You may need to change the port number on the url.
            //to hit azure, use http://hotpotato220160726083439.azurewebsites.net

            var connectionString = ConfigurationManager.ConnectionStrings["signalRConnectionString"].ConnectionString;
            
            var hubConnection = new HubConnection(connectionString);
            var hotPotato = hubConnection.CreateHubProxy("PotatoHub");

            hotPotato.On<string, bool>("UpdatePotatoState", (potatoHolder, iHavePotato) =>
            {
                if (iHavePotato)
                {
                    Console.WriteLine($"I Have The Potato! ");
                }
                else
                {
                    Console.WriteLine($"{potatoHolder} Has The Potato! ");
                }
            });

            hotPotato.On("gameReset", () =>
            {
                Console.Clear();
                Console.WriteLine("Game has been reset!");
            });

            hotPotato.On<string>("updateClientWithError", (error) =>
            {
                Console.WriteLine(error);
            });

            hotPotato.On<string>("playerJoined", (message) =>
            {
                Console.WriteLine(message);
            });

            hotPotato.On<string>("joined", (message) =>
            {
                Console.WriteLine(message);
            });

            hubConnection.Start().Wait();

            hotPotato.Invoke("addClient").Wait();

            var key = ' ';

            while (key != 'e')
            {
                if (key == 'p')
                {
                    hotPotato.Invoke("passPotato").Wait();
                }
                else if (key == 'r')
                {
                    hotPotato.Invoke("resetGame").Wait();
                }
                else if (key == 'j')
                {
                    hotPotato.Invoke("addClient").Wait();
                }

                key = Console.ReadKey().KeyChar;
            }
        }
    }
}