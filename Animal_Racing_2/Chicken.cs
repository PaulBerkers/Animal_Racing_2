using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Animal_Racing_2
{
    class Chicken : IAnimal
    {
        MediaPlayer mp = new MediaPlayer();

        public string Specie { get; set; }
        public string Name { get; set; }
        public int NumberOfLegs { get; set; }
        private Image img = new Image();
        public Image ImgAnimal
        {
            get { return img; }
            set { img = value; }
        }
        public Chicken()
        {
            img.Source = new BitmapImage(new Uri(@"/images/animals/chicken.jpg", UriKind.RelativeOrAbsolute));
        }
        public void MakeSomeNoise()
        {
            mp.Open(new Uri("sounds/chicken.mp3", UriKind.Relative));

            mp.Play();
        }

        public Task<bool> MakeSomeNoiseAsync()
        {
            var tcs = new TaskCompletionSource<bool>();
            mp.MediaEnded += (sender, e) =>
            {
                mp.Close();
                tcs.TrySetResult(true);
            };
            mp.Open(new Uri(@"sounds/chicken.mp3", UriKind.Relative));
            mp.Play();

            return tcs.Task;
        }

        public string Says()
        {
            return "Moeoe";
        }
    }
}
