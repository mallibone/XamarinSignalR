using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using SignalR.Backend;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Mobile.Services
{
    internal class ChatService : IChatService
    {
        private HubConnection _connection;
        private readonly HttpClient _httpClient;
        private const string backendUrl = "https://gnabbersignalr.azurewebsites.net/api/";
        //private const string backendUrl = "http://localhost:7071/api/";
        //public event EventHandler<Measurement> NewMeasurement;
        private Subject<Message> _newMessage = new Subject<Message>();

        public ChatService()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(backendUrl)
                .Build();
            _httpClient = new HttpClient();
        }

        public IObservable<Message> NewMeussageReceived => _newMessage;

        public async Task Connect()
        {
            if (_connection.State == HubConnectionState.Connected) return;

            _connection.On<string>("NewMessage", (messageString) =>
            {
                var message = JsonConvert.DeserializeObject<Message>(messageString);
                _newMessage.OnNext(message);
                Debug.WriteLine(messageString);
            });

            try
            {
                await _connection.StartAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async Task Disconnect()
        {
            await _connection.DisposeAsync();

            _connection = new HubConnectionBuilder()
                .WithUrl(backendUrl)
                .Build();
        }

        public async Task Send(Message message)
        {
            var serializedPayload = JsonConvert.SerializeObject(message);
        
            var response = await _httpClient.PostAsync("https://gnabbersignalr.azurewebsites.net/api/SendMessage", new StringContent(serializedPayload));
            Debug.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }
}
