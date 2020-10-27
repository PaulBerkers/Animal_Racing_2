using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Animal_Racing_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string SelectedAnimal { get; set; }
        public string SoundToPlay { get; set; }

        MediaPlayer mp = new MediaPlayer();

        List<IAnimal> listAnimals = new List<IAnimal>();
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            Image i = (Image)sender;
            i.Height = i.ActualHeight + 20;
            i.Width = i.ActualWidth + 20;

            SelectedAnimal = i.Tag.ToString();

            switch (SelectedAnimal.ToLower())
            {
                case "imgdog":
                    SoundToPlay = "dogs.mp3";
                    SelectedAnimal = "Dog";
                    break;
                case "imgcow":
                    SoundToPlay = "cow.mp3";
                    SelectedAnimal = "Cow";
                    break;
                case "imgchicken":
                    SoundToPlay = "chicken.mp3";
                    SelectedAnimal = "Chicken";
                    break;
                case "imggoat":
                    SoundToPlay = "goat.mp3";
                    SelectedAnimal = "Goat";
                    break;
                case "imghorse":
                    SoundToPlay = "horse.mp3";
                    SelectedAnimal = "Horse";
                    break;
                case "imgrooster":
                    SoundToPlay = "rooster.mp3";
                    SelectedAnimal = "Rooster";
                    break;
                case "imgduck1":
                    SoundToPlay = "duck.mp3";
                    SelectedAnimal = "Duck";
                    break;
                case "imgpig":
                    SoundToPlay = "pigs.mp3";
                    SelectedAnimal = "Pig";
                    break;
            }

            mp.Open(new Uri($"sounds/{SoundToPlay}", UriKind.Relative));
            mp.Play();

        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            Image i = (Image)sender;
            i.Height = i.ActualHeight - 20;
            i.Width = i.ActualWidth - 20;
            mp.Stop();
        }

        private void Animal_Clicked(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            WinConfirmAnimal winConf = new WinConfirmAnimal(listAnimals, SelectedAnimal);
            winConf.Confirmed += winConf_Confirmed;
            winConf.ShowDialog();

            this.Show();
        }

        private void winConf_Confirmed()
        {
            //de lijst opbouwen
            //reflection: maak class van string selectedAnimal en voeg deze class toe aan de lijst

            Type animalType = Type.GetType($"Animal_Racing_2.{SelectedAnimal}, Animal_Racing_2");

            IAnimal instancObject = (IAnimal)(Activator.CreateInstance(animalType));

            if(listAnimals.Count < 4)
            {
                listAnimals.Add(instancObject);
            }

            btnAnimalsChosen.IsEnabled = listAnimals.Count < 4 ? false : true;
        }

        private void btnAnimalsChosen_Click(object sender, RoutedEventArgs e)
        {
            WinBet winBet = new WinBet(listAnimals);
            winBet.Show();
            this.Close();
        }
    }
}
