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
using System.Windows.Threading;

namespace dino_POPUP {
    /// <summary>
    /// Interaction logic for Rawr.xaml
    /// </summary>
    public partial class Rawr : Window {
        public Rawr() {
            InitializeComponent();
        }

        public void InvokeMove() {
            this.Show();
            var y = 0;
            var speed = 0;
            Random rnd = new Random(Convert.ToInt32(DateTime.Now.Millisecond));
            y = rnd.Next(0, Convert.ToInt32(this.Height));
            speed = rnd.Next(10, 60);
            Move(btnExit, 0, y, this.Width, speed); 
        }

        private void Move(Control control, double startX, double Y, double endX, double speed) {
            Canvas.SetLeft(control, startX - control.Width);
            Canvas.SetTop(control, Y);

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += (sender, e) => Update(sender, e, control, endX, speed);
            dispatcherTimer.Interval = new TimeSpan(0,0,0,0,1);
            dispatcherTimer.Start();
        }

        private void Update(object sender, EventArgs e, Control control, double endX, double speed) {
            if (Canvas.GetLeft(control) < endX) {
                Canvas.SetLeft(control, Canvas.GetLeft(control) + speed);
                this.Topmost = true;
            }
            else {
                this.Close();
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e) {
            Environment.Exit(1);
        }
    }
}
