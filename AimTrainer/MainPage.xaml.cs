namespace AimTrainer
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void highlight(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            button.BackgroundColor = Color.FromArgb("#bfa100");
        }

        private void lowlight(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            button.BackgroundColor = Colors.Gray;
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;
        }
    }

}
