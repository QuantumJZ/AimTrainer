namespace AimTrainer
{
    public partial class MainPage : ContentPage
    {
        JumboPage? jumbo;
        PrecisionPage? precision;
        MotionPage? motion;

        /// <summary>
        /// MainPage constructor
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Open a new JumboPage window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenJumbo(object sender, EventArgs e)
        {
            jumbo = new JumboPage();
            Navigation.PushAsync(jumbo);
        }

        /// <summary>
        /// Open a new PrecisionPage window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenPrecision(object sender, EventArgs e)
        {
            precision = new PrecisionPage();
            Navigation.PushAsync(precision);
        }

        /// <summary>
        /// Open a new MotionPage window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenMotion(object sender, EventArgs e)
        {
            motion = new MotionPage();
            Navigation.PushAsync(motion);
        }

        /// <summary>
        /// Add highlight when mouse enters button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void highlight(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            button.BackgroundColor = Color.FromArgb("#bfa100");
        }

        /// <summary>
        /// Remove highlight when mouse leaves button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lowlight(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            button.BackgroundColor = Colors.Gray;
        }

        /// <summary>
        /// Handle active games when going back to main window by prematurely stopping them.
        /// </summary>
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
