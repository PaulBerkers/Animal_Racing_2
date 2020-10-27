using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Animal_Racing_2
{
    /// <summary>
    /// Interaction logic for WinBet.xaml
    /// </summary>
    public partial class WinBet : Window
    {
        MediaPlayer mp = new MediaPlayer();
        List<IAnimal> listAnimals = new List<IAnimal>();

        DispatcherTimer timer = new DispatcherTimer();
        Image movingImage;

        public WinBet()
        {
            InitializeComponent();

            listAnimals.Add(new Dog());
            listAnimals.Add(new Duck());
            listAnimals.Add(new Goat());
            listAnimals.Add(new Horse());

            CreateImageTiles();

            mp.Open(new Uri(@"sounds/cheer.wav", UriKind.Relative));
            mp.Play();
        }
        public WinBet(List<IAnimal> lstAnimals)
        {
            InitializeComponent();

            this.listAnimals = lstAnimals;

            CreateImageTiles();

            mp.Open(new Uri(@"sounds/cheer.wav", UriKind.Relative));
            mp.Play();
        }

        private void CreateImageTiles()
        {
            for (int i = 0; i < listAnimals.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        imgAnimal1.Source = listAnimals[i].ImgAnimal.Source;
                        break;
                    case 1:
                        imgAnimal2.Source = listAnimals[i].ImgAnimal.Source;
                        break;
                    case 2:
                        imgAnimal3.Source = listAnimals[i].ImgAnimal.Source;
                        break;
                    case 3:
                        imgAnimal4.Source = listAnimals[i].ImgAnimal.Source;
                        break;
                }

                //Images below, choose to win
                Image imgChoose = new Image();
                imgChoose.Name = $"imgAnimal{i}";
                imgChoose.Source = listAnimals[i].ImgAnimal.Source;
                imgChoose.Stretch = Stretch.Fill;
                Grid.SetRow(imgChoose, 1);
                Grid.SetColumn(imgChoose, i + 1);
                gdGrassDown.Children.Add(imgChoose);
            }
        }
        private void cmdMoneyUp_Click(object sender, RoutedEventArgs e)
        {
            mp.Open(new Uri(@"sounds/cash.mp3", UriKind.Relative));
            mp.Play();

            int iReadNumBet = Convert.ToInt32(txtMoney.Text);
            int iReadNumWallet = Convert.ToInt32(lblScore.Content);
            if (iReadNumWallet >= 5)
            {
                iReadNumBet += 5;
                iReadNumWallet -= 5;
            }
            txtMoney.Text = iReadNumBet.ToString();
            lblScore.Content = iReadNumWallet.ToString();
        }
        

        private void cmdMoneyDown_Click(object sender, RoutedEventArgs e)
        {
            int iReadNumBet = Convert.ToInt32(txtMoney.Text);
            int iReadNumWallet = Convert.ToInt32(lblScore.Content);
            if (iReadNumBet >= 5)
            {
                iReadNumBet -= 5;
                iReadNumWallet += 5;
            }
            txtMoney.Text = iReadNumBet.ToString();
            lblScore.Content = iReadNumWallet.ToString();
        }

        private void BtnAnimal_Checked(object sender, RoutedEventArgs e)
        {
            btnStartRace.IsEnabled = true;
        }
        private async void btnStartRace_Click(object sender, RoutedEventArgs e)
        {
            DisableControls();

            //sounds: after mario all animals
            await MakeSomeNoiseHereAsync("sounds/mario-kart_start-race.mp3");

            //rennen!
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void DisableControls()
        {
            imgAnimal1.Visibility = Visibility.Visible;
            imgAnimal2.Visibility = Visibility.Visible;
            imgAnimal3.Visibility = Visibility.Visible;
            imgAnimal4.Visibility = Visibility.Visible;

            rbWin1.IsEnabled = false;
            rbWin2.IsEnabled = false;
            rbWin3.IsEnabled = false;
            rbWin4.IsEnabled = false;

            txtMoney.IsEnabled = false;
            cmdMoneyUp.IsEnabled = false;
            cmdMoneDown.IsEnabled = false;
        }
        private void EnableControls()
        {
            rbWin1.IsEnabled = true;
            rbWin2.IsEnabled = true;
            rbWin3.IsEnabled = true;
            rbWin4.IsEnabled = true;

            txtMoney.IsEnabled = true;
            cmdMoneyUp.IsEnabled = true;
            cmdMoneDown.IsEnabled = true;
        }
        private async Task<bool> MakeSomeNoiseHereAsync(string sMySound)
        {
            var tcs = new TaskCompletionSource<bool>();

            MediaPlayer mediaplayer = new MediaPlayer();
            mediaplayer.MediaEnded += (sender, e) =>
            {
                mediaplayer.Close();
                tcs.TrySetResult(true);
            };

            mediaplayer.Open(new Uri(sMySound, UriKind.Relative));
            mediaplayer.Play();

            return await tcs.Task;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            //random animal + stepsize berekenen op basis van milliseconden
            var ms = DateTime.Now.Millisecond; //943  //4
            string sDigits = DateTime.Now.Millisecond.ToString();

            int numStepSize = sDigits.Length > 1 ? int.Parse(sDigits.Substring(0, 1)) : 0; //for size step animal 
            int numAnimal = Convert.ToInt32(sDigits.Substring(ms.ToString().Length - 1, 1)); //for animal 1-4 
            int numSleep = (int.Parse(numAnimal.ToString() + numStepSize.ToString())); //for sleep

            //random animal berekenen (0, 1, 2 of 3)
            if (numAnimal >= 8)
            {
                return; //anders heeft 0 en 1 meer kans
            }

            //nu: 0-7 overgebleven
            numAnimal = (numAnimal > 3) ? numAnimal - 3 : numAnimal + 1;

            //wie gaat hoeveel vooruit
            switch (numAnimal)
            {
                case 1:
                    imgAnimal1.Margin = new Thickness(imgAnimal1.Margin.Left + (numStepSize * 6), imgAnimal1.Margin.Top, 0, 0);
                    break;
                case 2:
                    imgAnimal2.Margin = new Thickness(imgAnimal2.Margin.Left + (numStepSize * 6), imgAnimal2.Margin.Top, 0, 0);
                    break;
                case 3:
                    imgAnimal3.Margin = new Thickness(imgAnimal3.Margin.Left + (numStepSize * 6), imgAnimal3.Margin.Top, 0, 0);
                    break;
                case 4:
                    imgAnimal4.Margin = new Thickness(imgAnimal4.Margin.Left + (numStepSize * 6), imgAnimal4.Margin.Top, 0, 0);
                    break;
            }

            //end of the race!
            if (imgAnimal1.Margin.Left >= (racetrack.ActualWidth - 150) ||
                imgAnimal2.Margin.Left >= (racetrack.ActualWidth - 150) ||
                imgAnimal3.Margin.Left >= (racetrack.ActualWidth - 150) ||
                imgAnimal4.Margin.Left >= (racetrack.ActualWidth - 150))
            {
                timer.Stop();
                //StopMakingThatNoise();
                UpdatePlayersBalance(numAnimal);
                TakeStartPositions();
            }

            //variabel wait 0-99 millisec, for better random
            Thread.Sleep(Convert.ToInt32(numSleep));
        }
        private void UpdatePlayersBalance(int winner)
        {
            if ((rbWin1.IsChecked == true && winner == 1) ||
                (rbWin2.IsChecked == true && winner == 2) ||
                (rbWin3.IsChecked == true && winner == 3) ||
                (rbWin4.IsChecked == true && winner == 4))
            {
                MessageBox.Show("We have a winner - Congratulations #" + winner + "! " + "\n" + "And you have won this bet!");
                lblScore.Content = (int.Parse(lblScore.Content.ToString()) + (int.Parse(txtMoney.Text) * 3));
            }
            else
            {
                MessageBox.Show("We have a winner - Congratulations #" + winner + "! " + "\n" + "But you have lost your money!");
            }
            txtMoney.Text = "0";
        }

        private void TakeStartPositions()
        {
            imgAnimal1.Margin = new Thickness(50, 0, 0, 0);
            imgAnimal2.Margin = new Thickness(50, 60, 0, 0);
            imgAnimal3.Margin = new Thickness(50, 120, 0, 0);
            imgAnimal4.Margin = new Thickness(50, 170, 0, 0);

            EnableControls();
        }
    }
}
