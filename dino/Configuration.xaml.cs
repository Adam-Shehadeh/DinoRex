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
using dino_ENTITY;
using dino_POPUP;
using dino_SERVICE;

namespace dino
{
    public partial class Configuration : UserControl
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        ApplicationSettings applicationSettings = new ApplicationSettings();
        DataReader dr = new DataReader();
        Random rnd = new Random(Convert.ToInt32(DateTime.Now.Millisecond));
        Rawr r = new Rawr();
        int secondsTilNext = 0;
        int counter;
        string status = "Off";

        public Configuration()
        {
            dispatcherTimer.Tick += new EventHandler(Update);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            InitializeComponent();
        }


        protected override void OnInitialized(EventArgs e) {
            base.OnInitialized(e);
            applicationSettings = dr.ReadSettingsFromXML();
            txtInterval1.Text = applicationSettings.interval1.ToString();
            txtInterval2.Text = applicationSettings.interval2.ToString();
            foreach(var s in DinoService.GetAvailableCharacters()) {
                ddlCharacters.Items.Add(s);
            }
            if (applicationSettings.selectedCharacter != null) {
                ddlCharacters.SelectedItem = "Bongo";
            } else {
                ddlCharacters.SelectedIndex = 0;
            }
            ResetTimer();
        }

        private void ResetTimer() {
            secondsTilNext = rnd.Next(Convert.ToInt32(applicationSettings.interval1), Convert.ToInt32(applicationSettings.interval2) + 1);
            counter = 0;
            status = "Running";
        }

        private void Update(object sender, EventArgs e) {
            counter++;
            updateLabelStatus();
            if (counter >= secondsTilNext) {
                r.InvokeMove(ddlCharacters.SelectedValue.ToString());
                ResetTimer();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) {
            Environment.Exit(1);
        }

        private void btnSetInterval_Click(object sender, RoutedEventArgs e) {
            double i1 = Convert.ToDouble(txtInterval1.Text);
            double i2 = Convert.ToDouble(txtInterval2.Text);

            if (i1 < i2) {
                applicationSettings.interval1 = i1;
                applicationSettings.interval2 = i2;
                dr.SaveSettingsToXML(applicationSettings);
                ResetTimer();
            }
            else {
                MessageBox.Show("Value 1 cannot be greater than Value 2.");
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void updateLabelStatus() {
            lblStatus.Content = "V: 1.0.0" + System.Environment.NewLine +
                "Stat: " + status + System.Environment.NewLine +
                "Time: " + DateTime.Now.ToString("HH:mm:ss") + System.Environment.NewLine +
                "Next: " + secondsTilNext + System.Environment.NewLine +
                "Count: " + counter;
        }

        private void ddlCharacters_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            applicationSettings.selectedCharacter = ddlCharacters.SelectedItem.ToString();
            dr.SaveSettingsToXML(applicationSettings);
        }
    }
}
