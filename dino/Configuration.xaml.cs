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
using System.Reflection;
using IWshRuntimeLibrary;
using dino_ENTITY;
using dino_POPUP;
using dino_SERVICE;


namespace dino
{
    public partial class Configuration : UserControl {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        ApplicationSettings applicationSettings = new ApplicationSettings();
        DataReader dr = new DataReader();
        Random rnd = new Random(Convert.ToInt32(DateTime.Now.Millisecond));

        public Configuration() {
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

            foreach (var s in DinoService.GetAvailableCharacters()) {
                ddlCharacters.Items.Add(s);
            }
            if (applicationSettings.selectedCharacter != null) {
                ddlCharacters.SelectedItem = applicationSettings.selectedCharacter;
            } else {
                ddlCharacters.SelectedIndex = 0;
            }
        }

        private void ResetTimer() {
            applicationSettings.secondsTilNext = rnd.Next(Convert.ToInt32(applicationSettings.interval1), Convert.ToInt32(applicationSettings.interval2) + 1);
            applicationSettings.counter = 0;
            applicationSettings.status = "Running";
        }

        private void Update(object sender, EventArgs e) {
            if (applicationSettings.enabled) {
                applicationSettings.counter++;
            }
            if (applicationSettings.counter >= applicationSettings.secondsTilNext) {
                new Rawr().InvokeMove(ddlCharacters.SelectedValue.ToString());
                updateLabelStatus();
                ResetTimer();
            }
            updateLabelStatus();
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
            } else {
                System.Windows.MessageBox.Show("Value 1 cannot be greater than Value 2.", "Windows Provider Host", MessageBoxButton.OK,
                MessageBoxImage.Error, MessageBoxResult.OK,
                MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void updateLabelStatus() {
            lblStatus.Content = "V: 1.0.0" + System.Environment.NewLine +
                "Stat: " + applicationSettings.status + System.Environment.NewLine +
                "Time: " + DateTime.Now.ToString("HH:mm:ss") + System.Environment.NewLine +
                "Next: " + applicationSettings.secondsTilNext + System.Environment.NewLine +
                "Count: " + applicationSettings.counter;
        }

        private void ddlCharacters_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            applicationSettings.selectedCharacter = ddlCharacters.SelectedItem.ToString();
            dr.SaveSettingsToXML(applicationSettings);
        }



        //private void btnSetAutoStart_Click(object sender, RoutedEventArgs e) {
        //    ApplicationSettings.RegModify(ApplicationSettings.RegistryAction.LOCAL_MACHINE_ADD_STARTUP, Assembly.GetExecutingAssembly());
        //}

        //private void btnRemoveAutoStart_Click(object sender, RoutedEventArgs e) {
        //    ApplicationSettings.RegModify(ApplicationSettings.RegistryAction.LOCAL_MACHINE_REMOVE_STARTUP, Assembly.GetExecutingAssembly());
        //}

        private void btnSetAutoStartCurrentUser_Click(object sender, RoutedEventArgs e) {
            //ApplicationSettings.RegModify(ApplicationSettings.RegistryAction.CURRENT_USER_ADD_STARTUP, Assembly.GetExecutingAssembly());

            WshShell shell = new WshShell();

            string startupDir = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string shortcutName = "\\Windows Provider Host.lnk";
            string appPath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

            if (!System.IO.File.Exists(startupDir + shortcutName)) {
                IWshShortcut link = (IWshShortcut)shell.CreateShortcut(@startupDir + shortcutName);
                link.TargetPath = @appPath;
                link.Save();
                dino.MessageBox mb = new MessageBox("Successfully configured application to begin on startup!");
                mb.Show();
            } else {
                
                dino.MessageBox mb = new MessageBox("The application is already configured to autostart.");
                mb.Show();
            }
        }

        

        private void btnRemoveAutoStartCurrentUser_Click(object sender, RoutedEventArgs e) {
            //ApplicationSettings.RegModify(ApplicationSettings.RegistryAction.CURRENT_USER_REMOVE_STARTUP, Assembly.GetExecutingAssembly());

            string startupDir = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string shortcutName = "\\Windows Provider Host.lnk";
            string appPath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

            if (!System.IO.File.Exists(startupDir + shortcutName)) {
                System.IO.File.Delete(startupDir + shortcutName);
                dino.MessageBox mb = new MessageBox("Successfully removed startup configuration.");
                mb.Show();
            } else {
                dino.MessageBox mb = new MessageBox("The application is not configured to autostart.");
                mb.Show();
            }
        }

        private void btnDisable_Click(object sender, RoutedEventArgs e) {
            updateLabelStatus();
            applicationSettings.status = "Paused";
            applicationSettings.enabled = false;
            dr.SaveSettingsToXML(applicationSettings);
        }

        private void btnEnable_Click(object sender, RoutedEventArgs e) {
            updateLabelStatus();
            applicationSettings.status = "Running";
            applicationSettings.enabled = true;
            dr.SaveSettingsToXML(applicationSettings);
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e) {
            About.ShowForm();
        }
    }
}
