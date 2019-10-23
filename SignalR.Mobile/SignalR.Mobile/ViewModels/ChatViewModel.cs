using SignalR.Backend;
using SignalR.Mobile.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace SignalR.Mobile.ViewModels
{
    public class ChatViewModel : IDisposable
    {
        private IChatService _chatService;
        private IDisposable _messageSubscription;

        public ChatViewModel(IChatService chatService = null)
        {
            _chatService = chatService ?? new ChatService();
            SendMessageCommand = new Command(SendMessage);
            Messages = new ObservableCollection<Message>();
        }

        public string Username { get; set; }
        public ObservableCollection<Message> Messages { get; set; }

        public string Message { get; set; }
        public ICommand SendMessageCommand { get; set; }

        public async void Init()
        {
            await _chatService.Connect();
            _messageSubscription = _chatService.NewMeussageReceived.Subscribe(NewMessageReceived);
        }

        private void NewMessageReceived(Message newMessage)
        {
            Messages.Add(newMessage);
        }

        private async void SendMessage()
        {
            var message = new Message { Author = Username, Text = Message, Timestamp = DateTimeOffset.Now };
            await _chatService.Send(message);
        }

        public void Dispose()
        {
            _messageSubscription.Dispose();
        }
    }
}
