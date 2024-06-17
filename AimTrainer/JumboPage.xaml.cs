
using CommunityToolkit.Maui.Views;
using System.Timers;

namespace AimTrainer
{
    public partial class JumboPage : ContentPage
    {
        bool gameActive = false;
        public JumboPage()
        {
            InitializeComponent();
            DisplayPopup();
        }

        public async void DisplayPopup()
        {
            var popup = new InfoPopup("After the 3 second countdown, click as many tiles as possible within 30 seconds.");
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
            gameActive = true;
        }
    }

}
