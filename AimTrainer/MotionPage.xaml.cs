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
        (int, int, int, int)[] moveLoc = { (0, 0, 1800, 700), (3, 0, 1800, 0), (6, 0, 1800, 100),
        (7, 0, 1800, -300), (0, 0, 100, 700), (0, 3, 1000, 700), (0, 6, -100, 700), (0, 11, -800, 700), (0, 16, -1000, 700)};
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
            GameActive = true;
            startGame();
        }

        private async void startGame()
        {
            score = 0;
            scoreTracker.Text = score.ToString();
            startTimer();
            locations.Clear();
            locations.Add((0, 0));
            locations.Add((0, 17));
            await target.TranslateTo(0, 0, 0);
            GameGrid.SetRow(target, rand.Next(1, rowLength) - 1);
            GameGrid.SetColumn(target, rand.Next(colLength));
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
            this.CancelAnimations();
            DisplayEndPopup();
        }

        private async void targetClicked(object sender, EventArgs e)
        {
            moving = !moving;
            score++;
            scoreTracker.Text = score.ToString();
            if (moving)
            {
                (int, int, int, int) curr = moveLoc[rand.Next(moveLoc.Length)];
                GameGrid.SetRow(target, curr.Item1);
                GameGrid.SetColumn(target, curr.Item2);
                await target.TranslateTo(curr.Item3, curr.Item4, 3000);
            }
            else
            {
                this.CancelAnimations();
                await target.TranslateTo(0, 0, 0);
                GameGrid.SetRow(target, rand.Next(1, rowLength)-1);
                GameGrid.SetColumn(target, rand.Next(colLength));
            }
        }
    }

}
