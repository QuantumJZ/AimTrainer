using CommunityToolkit.Maui.Views;

namespace AimTrainer
{
    public partial class EndPopup : Popup
    {

        /// <summary>
        /// EndPopup constructor
        /// </summary>
        /// <param name="score">Any string</param>
        public EndPopup(string score)
        {
            InitializeComponent();
            Score.Text = score;
        }

        /// <summary>
        /// Close the popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClosePopup(object? sender, EventArgs e) => this.Close();
    }
}
