using CommunityToolkit.Maui.Views;

namespace AimTrainer
{
    public partial class MotionPage : ContentPage
    {
        int rowLength = 9;
        int colLength = 18;
        Random rand = new Random();
        HashSet<(int, int)> locations = new HashSet<(int, int)>();
        int score = 0;
        public bool GameActive = false;
        bool moving = false;
        public MotionPage()
        {
            InitializeComponent();
            DisplayPopup();
        }

        public async void DisplayPopup()
        {
            var popup = new InfoPopup("After the 3 second countdown, click as many targets as possible in 30 seconds.");
            popup.Closed += (s, e) => onPopupClose();
            await this.ShowPopupAsync(popup);
        }

        public async void DisplayEndPopup()
        {
            var popup = new EndPopup("Score: " + score);
            popup.Closed += (s, e) => onPopupClose();
            await this.ShowPopupAsync(popup);
        }

        private async void onPopupClose()
        {
            GameGrid.IsEnabled = true;
            GameGrid.IsVisible = false;
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
            GameActive = true;
            startGame();
        }

        private void startGame()
        {
            score = 0;
            scoreTracker.Text = score.ToString();
            startTimer();
            locations.Clear();
            locations.Add((0, 0));
            locations.Add((0, 17));
        }

        private int distance(int x, int y, int x2, int y2)
        {
            return (int)Math.Sqrt(Math.Pow(x - x2, 2) + Math.Pow(y - y2, 2));
        }

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

        private void endGame()
        {
            GameGrid.IsEnabled = false;
            StatsGrid.IsEnabled = false;
            GameActive = false;
            DisplayEndPopup();
        }

        private async void targetClicked(object sender, EventArgs e)
        {
            if (moving)
            {
                score++;
                int x = 0, y = 0, x2 = 0, y2 = 0, flip = 0;
                while (moving && GameActive)
                {
                    x = rand.Next(0, 19);
                    y = rand.Next(0, 8);
                    while (locations.Contains((x, y)))
                    {
                        x = rand.Next(1, 19);
                        y = rand.Next(1, 8);
                    }
                    if (flip == 0)
                    {
                        x2 = rand.Next(0, 2) * 18;
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
                    flip = 1 - flip;
                }
            }
            else
            {
                score++;
            }
            moving = !moving;
            scoreTracker.Text = score.ToString();
        }
    }

}
