using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotPotato.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var hubConnection = new HubConnection("http://localhost:17553");

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

                key = Console.ReadKey().KeyChar;
            }
        }
    }
}