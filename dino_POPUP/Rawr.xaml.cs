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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace dino_POPUP {
    /// <summary>
    /// Interaction logic for Rawr.xaml
    /// </summary>
    public partial class Rawr : Window {

        private Random rnd = new Random(Convert.ToInt32(DateTime.Now.Millisecond));


        public Rawr() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {

        }

        public void InvokeMove() {
            this.Show();
            MoveDino(DINO_1);
        }

        private void MoveDino(Image target) {
            //Get a speed 
            double speed = rnd.Next(30, 120) / 100;
            //Reset Location
            Canvas.SetTop(target, rnd.Next(0, (int)this.Height));
            Canvas.SetLeft(target, 0 - target.Width);

            Vector offset = VisualTreeHelper.GetOffset(target);
            var top = offset.Y;
            var left = offset.X;
            TranslateTransform trans = new TranslateTransform();
            target.RenderTransform = trans;
            DoubleAnimation anim1 = new DoubleAnimation(0, this.Width, TimeSpan.FromSeconds(1));
            trans.BeginAnimation(TranslateTransform.XProperty, anim1);

        }
        private void btnExit_Click(object sender, RoutedEventArgs e) {
            Environment.Exit(1);
        }
    }
}
