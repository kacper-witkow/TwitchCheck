using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TwitchLib.Api;
using TwitchLib.Api.Services;
using TwitchLib.Api.Services.Events.LiveStreamMonitor;
using TwitchLib.Client;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace TwitchCheck
{
    class Program
    {
        static void Main(string[] args)
        {

            TwitchChatBot bot = new TwitchChatBot();
            Console.Write("Gdzie idzemy (nie wpisuj nic żeby przyjść do domyślnego):");

            string channel_name = Console.ReadLine();
            if (channel_name == null)
                bot.Connect();
            else
                bot.Connect(channel_name);

            Console.ReadLine();

            bot.Disconnect();

            
        }
        
    }
}