using CommunityToolkit.Maui.Views;

namespace AimTrainer
{
    public partial class PrecisionPage : ContentPage
    {
        int rowLength = 9;
        int colLength = 18;
        Random rand = new Random();
        HashSet<(int, int)> locations = new HashSet<(int, int)>();
        int score = 0;
        public bool GameActive = false;
        bool tracking = false;

        /// <summary>
        /// PrecisionPage constructor
        /// </summary>
        public PrecisionPage()
        {
            InitializeComponent();
            DisplayPopup();
        }

        /// <summary>
        /// Display the information popup
        /// </summary>
        public async void DisplayPopup()
        {
            var popup = new InfoPopup("After the 3 second countdown, track the target with your mouse for 30 seconds.");
            popup.Closed += (s, e) => onPopupClose();
            await this.ShowPopupAsync(popup);
        }

        /// <summary>
        /// Display the results popup
        /// </summary>
        public async void DisplayEndPopup()
        {
            var popup = new EndPopup("Score: " + score);
            popup.Closed += (s, e) => onPopupClose();
            await this.ShowPopupAsync(popup);
        }

        /// <summary>
        /// Start the game whenever a popup closes after a 3 second countdown
        /// </summary>
        private async void onPopupClose()
        {
            GameActive = true;
            GameGrid.IsEnabled = true;
            GameGrid.IsVisible = false;
            StatsGrid.IsEnabled = true;
            StatsGrid.IsVisible = false;
            Loading.IsVisible = true;
            countdownGrid.IsVisible = true;
            Countdown.Text = "3";
            Countdown.IsVisible = true;
            await Task.Delay(1000);
            Countdown.Text = "2";
            await Task.Delay(1000);
            Countdown.Text = "1";
            await Task.Delay(1000);
            Countdown.IsVisible = false;
            Loading.IsVisible = false;
            countdownGrid.IsVisible = false;
            GameGrid.IsVisible = true;
            StatsGrid.IsVisible = true;
            startGame();
        }

        /// <summary>
        /// Start the game by starting the timer and randomizing the tiles
        /// </summary>
        private async void startGame()
        {
            score = 0;
            scoreTracker.Text = score.ToString();
            startTimer();
            Task.Run(checkTracking);
            locations.Clear();
            locations.Add((0, 0));
            locations.Add((0, 17));
            int x = 0, y = 0, x2 = 0, y2 = 0, flip = 0;
            while (GameActive)
            {
                if (flip == 0)
                {
                    x2 = rand.Next(0, 2)*18;
                    y2 = rand.Next(1, 8);
                    while (locations.Contains((x2, y2)))
                    {
                        y2 = rand.Next(1, 8);
                    }
                    await target.TranslateTo(x2 * 100, y2 * 100, (uint)(distance(x, y, x2, y2) * 200));
                    x = x2;
                    y = y2;
                }
                else
                {
                    x2 = rand.Next(1, 19);
                    y2 = rand.Next(0, 2) * 7;
                    while (locations.Contains((x2, y2)))
                    {
                        x2 = rand.Next(1, 19);
                    }
                    await target.TranslateTo(x2 * 100, y2 * 100, (uint)(distance(x, y, x2, y2) * 200));
                    x = x2;
                    y = y2;
                }
                flip = 1-flip;
            }
        }

        /// <summary>
        /// Calculate the distance between two 2D points in space
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns>The rounded integer distance between p1 and p2</returns>
        private int distance(int x, int y, int x2, int y2)
        {
            return (int)Math.Sqrt(Math.Pow(x - x2, 2) + Math.Pow(y - y2, 2));
        }

        /// <summary>
        /// Start the timer by by decrementing the time variable very 1000ms
        /// </summary>
        private async void startTimer()
        {
            int time = 30;
            while (time > 0 && GameActive)
            {
                timer.Text = time.ToString();
                scoreTracker.Text = score.ToString();
                await Task.Delay(1000);
                time--;
            }
            if (GameActive)
            {
                scoreTracker.Text = score.ToString();
                endGame();
            }
        }

        /// <summary>
        /// End the game by setting game to inactive, disabling the game grid, canceling animations, and showing the results popup
        /// </summary>
        private void endGame()
        {
            GameGrid.IsEnabled = false;
            StatsGrid.IsEnabled = false;
            GameActive = false;
            this.CancelAnimations();
            DisplayEndPopup();
        }

        /// <summary>
        /// Track when the mouse is covering the target
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointerEntered(object sender, PointerEventArgs e)
        {
            tracking = true;
        }

        /// <summary>
        /// Track when the mouse leaves the target
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointerExited(object sender, PointerEventArgs e)
        {
            tracking = false;
        }

        /// <summary>
        /// If the game is active and target is being tracked, increment the score every 10ms
        /// </summary>
        private async void checkTracking()
        {
            while (GameActive)
            {
                if (tracking)
                {
                    score++;
                    await Task.Delay(10);
                }
            }
        }
    }

}
