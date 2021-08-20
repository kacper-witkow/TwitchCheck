using System;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace TwitchCheck
{
    internal class TwitchChatBot
    {
        readonly ConnectionCredentials credentials = new ConnectionCredentials(TwitchInfo.BotUserName, TwitchInfo.BotToken);
        TwitchClient client;
        string channelId;


        internal void Connect()
        {
            channelId = TwitchInfo.ChannelName;

            var clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 20,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };
            WebSocketClient customClient = new WebSocketClient(clientOptions);

            client = new TwitchClient(customClient);

            client.Initialize(credentials,TwitchInfo.ChannelName);

            client.OnLog += Client_OnLog;
            client.OnConnectionError += Client_OnConnectionError;
            client.OnMessageReceived += Client_OnMessageReceived;

            client.Connect();

        }

        internal void Connect(string channel_name)
        {
            channelId = channel_name;

            var clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 20,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };
            WebSocketClient customClient = new WebSocketClient(clientOptions);

            client = new TwitchClient(customClient);

            client.Initialize(credentials, channel_name);

            client.OnLog += Client_OnLog;
            

            client.OnConnectionError += Client_OnConnectionError;
            client.OnMessageReceived += Client_OnMessageReceived;
            client.OnConnected += Client_OnConnected;
            client.OnChatCommandReceived += Client_OnChatCommandReceived;
            client.Connect();
            


        }

        private void Client_OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            switch(e.Command.CommandText.ToLower())
            {
                case "dice":
                    client.SendMessage(channelId,e.Command.ChatMessage.DisplayName+" you rolled "+ Roll());
                    break;
                case "hi":
                    client.SendMessage(TwitchInfo.ChannelName, $"Hello there {e.Command.ChatMessage.DisplayName}");
                    break;

            }
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine("Connected to "+ channelId);
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            Console.WriteLine(e.ChatMessage.DisplayName+":"+e.ChatMessage.Message);
        }

        private void Client_OnConnectionError(object sender, OnConnectionErrorArgs e)
        {
            Console.WriteLine($"Error!!{e.Error}");
        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
             Console.WriteLine(e.Data);

        }

        internal void Disconnect()
        {
            Console.WriteLine("Disconnected");
        }

        public int Roll()
        {
            int score;
            Random random = new Random();
            score = random.Next(1,6);
            return score;
        }
    }
}