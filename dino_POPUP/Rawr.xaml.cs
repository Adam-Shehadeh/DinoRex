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
using dino_ENTITY;
using dino_SERVICE;

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

        protected override void OnDeactivated(EventArgs e) {
            //Makes window always ontop
            base.OnDeactivated(e);
            this.Topmost = true;
            this.Activate();
        }

        public void InvokeMove(string charName) {
            this.Top = System.Windows.Forms.Screen.AllScreens.Min(x => x.Bounds.Top);
            this.Left = System.Windows.Forms.Screen.AllScreens.Min(x => x.Bounds.Left);
            this.Width = CalculateScreenWidth();
            this.Show();
            this.Topmost = true;
            MoveDino(DinoService.MasterCharacterList.Where(i=>i.Name.Equals(charName)).First());
        }

        private void MoveDino(ICharacter chara) {
            Image target = (Image)this.FindName(chara.FormImageName);
            double speed = (chara.Speed / 100) * NumOfScreens();
            //double speed = rnd.Next(500, 550) / 100;
            //Image target = DINO_2;

            //Reset Location
            Canvas.SetTop(target, rnd.Next(0, (int)(this.Height - target.ActualHeight)));
            Canvas.SetLeft(target, 0);

            target.Visibility = Visibility.Visible;

            //Do animation
            TranslateTransform trans = new TranslateTransform();
            target.RenderTransform = trans;
            DoubleAnimation anim1 = new DoubleAnimation(0-target.ActualWidth, this.Width, TimeSpan.FromSeconds(speed));
            anim1.Completed += (s, e) => {
                this.Close();
            };
            trans.BeginAnimation(TranslateTransform.XProperty, anim1);
            

        }
        private void btnExit_Click(object sender, RoutedEventArgs e) {
            Environment.Exit(1);
        }
        private int CalculateScreenWidth() {
            int local = 0;
            foreach(var screen in System.Windows.Forms.Screen.AllScreens) {
                local += screen.Bounds.Width;
            }
            return local;
        }
        private int NumOfScreens() {
            int local = 0;
            foreach (var screen in System.Windows.Forms.Screen.AllScreens) {
                local++;
            }
            return local;
        }
    }
}
