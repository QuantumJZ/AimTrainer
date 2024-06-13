namespace AimTrainer
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OpenJumbo(object sender, EventArgs e) => Navigation.PushAsync(new JumboPage());
        private void OpenPrecision(object sender, EventArgs e) => Navigation.PushAsync(new PrecisionPage());
        private void OpenMotion(object sender, EventArgs e) => Navigation.PushAsync(new MotionPage());


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
    }

}
