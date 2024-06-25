using CommunityToolkit.Maui.Views;

namespace AimTrainer
{
    public partial class InfoPopup : Popup
    {

        /// <summary>
        /// InfoPopup constructor
        /// </summary>
        /// <param name="description">Any string</param>
        public InfoPopup(string description)
        {
            InitializeComponent();
            Description.Text = description;
        }

        /// <summary>
        /// Close the popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClosePopup(object? sender, EventArgs e) => this.Close();
    }
}
