using SignalR.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SignalR.Mobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = ViewModel;
            ViewModel.NavigationCallback = NavigateToChat;
        }

        private async void NavigateToChat(string username)
        {
            await Navigation.PushModalAsync(new ChatPage(username));
        }

        public LoginViewModel ViewModel { get; } = new LoginViewModel();
    }
}
