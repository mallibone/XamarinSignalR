using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SignalR.Mobile.ViewModels
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
            LoginCommand = new Command(() => NavigateToChat());
        }

        public string Username { get; set; }
        public ICommand LoginCommand { get; set; }
        public Action<string> NavigationCallback { get; set; } = (s) => { };

        public void NavigateToChat()
        {
            NavigationCallback(Username);
        }
    }
}
