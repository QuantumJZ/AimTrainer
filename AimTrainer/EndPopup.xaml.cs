using CommunityToolkit.Maui.Views;

namespace AimTrainer
{
    public partial class EndPopup : Popup
    {
        public EndPopup(string score)
        {
            InitializeComponent();
            Score.Text = score;
        }
        private void ClosePopup(object? sender, EventArgs e) => this.Close();
    }
}
