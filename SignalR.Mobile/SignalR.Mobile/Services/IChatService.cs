using System;
using System.Threading.Tasks;
using SignalR.Backend;

namespace SignalR.Mobile.Services
{
    public interface IChatService
    {
        IObservable<Message> NewMeussageReceived { get; }

        Task Connect();
        Task Disconnect();
        Task Send(Message message);
    }
}