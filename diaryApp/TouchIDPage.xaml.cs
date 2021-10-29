using System;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Fingerprint.Abstractions;
using Xamarin.Forms;

namespace diaryApp
{
    public partial class TouchIDPage : ContentPage
    {
        public TouchIDPage()
        {
            Title = "Login with TouchID";
            InitializeComponent();
        }

        private CancellationTokenSource _cancel;

        private async void OnAuthenticate(object sender, EventArgs e)
        {
            //_cancel = swAutoCancel.IsToggled ? new CancellationTokenSource(TimeSpan.FromSeconds(10)) : new CancellationTokenSource();
            lblStatus.Text = "";

            if (await Plugin.Fingerprint.CrossFingerprint.Current.IsAvailableAsync())
            {
                var result = await Plugin.Fingerprint.CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("TouchID", "ログイン"), _cancel.Token);
                //new AuthenticationRequestConfiguration(("指紋認証", "指を置いてね！"), _cancel.Token);
                await SetResultAsync(result);
            }
            else
            {
                await DisplayAlert("注意", "TouchIDは使用できません", "OK");
            }
        }

        private async Task SetResultAsync(FingerprintAuthenticationResult result)
        {
            if (result.Authenticated)
            {
                await Navigation.PushModalAsync(new AppShell());
            }
            else
            {
                lblStatus.Text = $"{result.Status}: {result.ErrorMessage}";
            }
        }
    }
}
