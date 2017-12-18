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
using System.Text.RegularExpressions;
using System.Windows.Threading;
using dino_DATA;
using dino_POPUP;

namespace dino
{
    /// <summary>
    /// Interaction logic for Configuration.xaml
    /// </summary>
    public partial class Configuration : UserControl
    {
        ApplicationSettings applicationSettings = new ApplicationSettings();
        DataReader dr = new DataReader();
        
        int secondsTilNext = 0;
        int counter;

        public Configuration()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e) {
            base.OnInitialized(e);
            applicationSettings = dr.ReadSettingsFromXML();
        }

        private void ResetTimer() {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(Update);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void Update(object sender, EventArgs e) {
            Rawr r = new Rawr();
            updateLabelStatus();
            counter++;
            if (counter >= secondsTilNext) {
                r.InvokeMove();
                counter = 0;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) {
            Environment.Exit(1);
        }

        private void btnSetInterval_Click(object sender, RoutedEventArgs e) {
            Random rnd = new Random(Convert.ToInt32(DateTime.Now.Millisecond));
            secondsTilNext = rnd.Next(Convert.ToInt32(txtInterval1.Text), Convert.ToInt32(txtInterval2.Text));
            ResetTimer();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void updateLabelStatus() {
            lblStatus.Content = "V: 1.0.0" + System.Environment.NewLine + 
                "Stat: Running" + System.Environment.NewLine + 
                "Time: " + DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
