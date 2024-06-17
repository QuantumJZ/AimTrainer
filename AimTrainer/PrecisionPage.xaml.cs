using CommunityToolkit.Maui.Views;
using System.Runtime.CompilerServices;

namespace AimTrainer
{
    public partial class PrecisionPage : ContentPage
    {
        public PrecisionPage()
        {
            InitializeComponent();
            DisplayPopup();
        }

        public async void DisplayPopup()
        {
            var popup = new InfoPopup("After the 3 second countdown, track the target with your mouse for 30 seconds.");
            popup.Closed += (s, e) => onPopupClose();
            await this.ShowPopupAsync(popup);
        }

        private async void onPopupClose()
        {
            Loading.IsVisible = true;
            Countdown.Text = "3";
            Countdown.IsVisible = true;
            await Task.Delay(1000);
            Countdown.Text = "2";
            await Task.Delay(1000);
            Countdown.Text = "1";
            await Task.Delay(1000);
            Countdown.IsVisible = false;
            Loading.IsVisible = false;
            startGame();
        }

        private void startGame()
        {

        }
    }

}
