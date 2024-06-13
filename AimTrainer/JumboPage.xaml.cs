
using CommunityToolkit.Maui.Views;

namespace AimTrainer
{
    public partial class JumboPage : ContentPage
    {
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

        private void onPopupClose()
        {
            // Start countdown animation
        }
    }

}
