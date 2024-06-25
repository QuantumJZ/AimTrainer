
using CommunityToolkit.Maui.Views;

namespace AimTrainer
{
    public partial class JumboPage : ContentPage
    {
        HashSet<(int, int)> locations = new HashSet<(int, int)>();
        Random rand = new Random();
        int score = 0;
        int rowLength = 6;
        int colLength = 12;
        public bool GameActive = false;

        /// <summary>
        /// JumboPage constructor
        /// </summary>
        public JumboPage()
        {
            InitializeComponent();
            DisplayPopup();
        }

        /// <summary>
        /// Display the information popup
        /// </summary>
        public async void DisplayPopup()
        {
            var popup = new InfoPopup("After the 3 second countdown, click as many tiles as possible within 30 seconds.");
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
            startGame();
        }

        /// <summary>
        /// Start the game by starting the timer and randomizing the tiles
        /// </summary>
        private void startGame()
        {
            score = 0;
            scoreTracker.Text = score.ToString();
            startTimer();
            locations.Clear();
            locations.Add((0, 0));
            locations.Add((0, 11));
            for (int i = 0; i < 3; i++)
            {
                int x = rand.Next(0, rowLength);
                int y = rand.Next(0, colLength);
                while (locations.Contains((x, y)))
                {
                    x = rand.Next(0, rowLength);
                    y = rand.Next(0, colLength);
                }
                locations.Add((x, y));
                Button button = (Button)FindByName("Button" + i);
                Grid.SetRow(button, x);
                Grid.SetColumn(button, y);
            }
        }

        /// <summary>
        /// Randomize the tile location for each click and update the score
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            score++;
            scoreTracker.Text = score.ToString();
            int x = Grid.GetRow(button);
            int y = Grid.GetColumn(button);
            locations.Remove((x, y));
            x = rand.Next(0, rowLength);
            y = rand.Next(0, colLength);
            while (locations.Contains((x, y)))
            {
                x = rand.Next(0, rowLength);
                y = rand.Next(0, colLength);
            }
            locations.Add((x, y));
            Grid.SetRow(button, x);
            Grid.SetColumn(button, y);
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
                await Task.Delay(1000);
                time--;
            }
            if (GameActive)
            {
                endGame();
            }
        }

        /// <summary>
        /// End the game by setting game to inactive, disabling the game grid and showing the results popup
        /// </summary>
        private void endGame()
        {
            GameActive = false;
            GameGrid.IsEnabled = false;
            DisplayEndPopup();
        }
    }

}
