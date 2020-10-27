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
using System.Windows.Shapes;

namespace Animal_Racing_2
{
    public delegate void Notify();
    /// <summary>
    /// Interaction logic for WinConfirmAnimal.xaml
    /// </summary>
    public partial class WinConfirmAnimal : Window
    {
        public string DisplayAddAnimal { get; set; }
        private string animal;
        private List<IAnimal> listAnimals;
        public event Notify Confirmed;

        public WinConfirmAnimal(List<IAnimal> lstAnimals, string SelectedAnimal)
        {
            InitializeComponent();
            this.animal = SelectedAnimal;
            this.listAnimals = lstAnimals;
            DataContext = this;

            PopulateWindow();
        }

        private void PopulateWindow()
        {
            DisplayAddAnimal = $"voeg {animal} toe aan de lijst van kandidaten?";
            switch (animal.ToLower())
            {
                case "chicken":
                    CreateCenterImage("chicken.jpg");
                    break;
                case "cow":
                    CreateCenterImage("cow.jpg");
                    break;
                case "dog":
                    CreateCenterImage("dog.jpg");
                    break;
                case "duck":
                    CreateCenterImage("duck.png");
                    break;
                case "goat":
                    CreateCenterImage("goat.jpg");
                    break;
                case "horse":
                    CreateCenterImage("horse.jpg");
                    break;
                case "pig":
                    CreateCenterImage("pig.jpg");
                    break;
                case "rooster":
                    CreateCenterImage("rooster.jpg");
                    break;
                default:
                    break;
            }

            //create images from list, confirmed earlier
            int indexList = 0;
            foreach (IAnimal listedAnimal in listAnimals)
            {
                switch (listedAnimal.ToString())
                {
                    case "Animal_Racing_2.Chicken":
                        CreateImagePosition("chicken.jpg", indexList);
                        break;
                    case "Animal_Racing_2.Cow":
                        CreateImagePosition("cow.jpg", indexList);
                        break;
                    case "Animal_Racing_2.Dog":
                        CreateImagePosition("dog.jpg", indexList);
                        break;
                    case "Animal_Racing_2.Duck":
                        CreateImagePosition("duck.png", indexList);
                        break;
                    case "Animal_Racing_2.Goat":
                        CreateImagePosition("goat.jpg", indexList);
                        break;
                    case "Animal_Racing_2.Horse":
                        CreateImagePosition("horse.jpg", indexList);
                        break;
                    case "Animal_Racing_2.Pig":
                        CreateImagePosition("pig.jpg", indexList);
                        break;
                    case "Animal_Racing_2.Rooster":
                        CreateImagePosition("rooster.jpg", indexList);
                        break;
                    default:
                        break;
                }
                indexList++;
            }
        }
        private void CreateImagePosition(string fileName, int index)
        {
            //column positie van image in grid.row
            int pos = 0;
            switch (index)
            {
                case 0: pos = 1; break;
                case 1: pos = 3; break;
                case 2: pos = 5; break;
                case 3: pos = 7; break;
            }

            //image animal
            Image img = new Image(); //in images/animals
            img.Name = $"imgAnimal{index}";
            img.Source = new BitmapImage(new Uri($"/Animal_Racing_2;component/images/animals/{fileName}", UriKind.Relative));
            img.Stretch = Stretch.Fill;
            Grid.SetRow(img, 1);
            Grid.SetColumn(img, pos);
            grdConfirmedList.Children.Add(img);


            Image imgDel = new Image(); //in images/animals
            imgDel.Name = $"imgRemove{index}";
            imgDel.Tag = index;
            imgDel.Source = new BitmapImage(new Uri($"/Animal_Racing_2;component/images/err.png", UriKind.Relative));
            imgDel.Height = 20;
            imgDel.Width = 20;
            imgDel.ToolTip = "Remove";
            imgDel.MouseDown += ImgDel_Remove;
            Grid.SetRow(imgDel, 2);
            Grid.SetColumn(imgDel, pos);
            grdConfirmedList.Children.Add(imgDel);
        }

        private void ImgDel_Remove(object sender, MouseButtonEventArgs e)
        {
            //waardes aanpassen in dit scherm en teruggeven aan hoofdscherm

            //1. alle plaatjes verwijderen van scherm
            IEnumerable<Image> someImgs = grdConfirmedList.Children.OfType<Image>();
            List<Image> tmpList = someImgs.ToList();
            foreach (Image item in tmpList)
            {
                grdConfirmedList.Children.Remove(item);
            }

            //2. plaatje verwijderen uit List<Animals>
            int indexTag = Convert.ToInt32(((Image)sender).Tag);
            listAnimals.RemoveAt(indexTag);

            //3. scherm opbouwen
            PopulateWindow();

        }

        private void CreateCenterImage(string imgFile)
        {
            //image animal
            Image img = new Image(); //in images/animals
            img.Source = new BitmapImage(new Uri($"/Animal_Racing_2;component/images/animals/{imgFile}", UriKind.Relative));
            img.Stretch = Stretch.Fill;
            Grid.SetRow(img, 3);
            Grid.SetColumn(img, 1);
            grdCenter.Children.Add(img);
        }

        private void ImgOk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //voer event uit
            Confirmed();
            this.Close();
        }

        private void ImgDel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //geen confirm
            this.Close();
        }
    }
}
