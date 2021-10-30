using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using diaryApp.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace diaryApp
{
    [DesignTimeVisible(false)]
    public partial class TouchIDPage : ContentPage
    {
        public TouchIDPage()
        {
            InitializeComponent();
            var animation = new Animation(v => btnThrone.Scale = v, 0, 1);
            animation.Commit(this, "PulseAnimation", 16, 2000, Easing.SinOut, (v, c) => btnThrone.Scale = 0, () => true);
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var result = await DependencyService.Get<IBiometricService>().LoginWithBiometrics();
            if (result)
            {
                await Navigation.PushModalAsync(new AppShell());
            }
            else
                await DisplayAlert("", "Try agin later.", "OK");
        }
    }
}