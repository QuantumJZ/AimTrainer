using CommunityToolkit.Maui.Views;

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

        private void onPopupClose()
        {
            // Start countdown animation
        }
    }

}
