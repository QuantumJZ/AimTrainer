using CommunityToolkit.Maui.Views;

namespace AimTrainer
{
    public partial class InfoPopup : Popup
    {
        public InfoPopup(string description)
        {
            InitializeComponent();
            Description.Text = description;
        }
        private void ClosePopup(object? sender, EventArgs e) => this.Close();
    }
}
