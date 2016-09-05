using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PushNotifyTest
{
    public partial class PushNotifyTestPage : ContentPage
    {
        private NativeCallsInterfaces.IPushNotification _pushNotification;
        public PushNotifyTestPage()
        {
            InitializeComponent();

            _pushNotification = DependencyService.Get<NativeCallsInterfaces.IPushNotification>();
        }

        public async void OnClickVerify(object sender, EventArgs e)
        {
            var isServiceAvailible = _pushNotification.IsPlayServicesAvailable();
            if (isServiceAvailible)
            {
                await DisplayAlert(nameof(PushNotifyTest), "Disponivel", "Ok");
            }
            else
            {
                await DisplayAlert(nameof(PushNotifyTest), "Não Disponivel", "Ok");
            }
        }

        public void OnClickToken(object sender, EventArgs e)
        {
            var token = string.Empty;
            token = _pushNotification.GetToken();
            lblToken.Text = token;
        }
    }
}

