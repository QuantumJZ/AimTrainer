namespace AimTrainer
{
    public partial class MainPage : ContentPage
    {
        JumboPage jumbo;
        PrecisionPage precision;
        MotionPage motion;
        public MainPage()
        {
            InitializeComponent();
        }

        private void OpenJumbo(object sender, EventArgs e)
        {
            jumbo = new JumboPage();
            Navigation.PushAsync(jumbo);
        }
        private void OpenPrecision(object sender, EventArgs e)
        {
            precision = new PrecisionPage();
            Navigation.PushAsync(precision);
        }
        private void OpenMotion(object sender, EventArgs e)
        {
            motion = new MotionPage();
            Navigation.PushAsync(motion);
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (jumbo != null)
            {
                jumbo.GameActive = false;
            }
            if(precision != null)
            {
                precision.GameActive = false;
            }
            if(motion != null)
            {
                motion.GameActive = false;
            }
        }
    }

}
