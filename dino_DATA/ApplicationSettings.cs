using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Reflection;

namespace dino_ENTITY {

    //NOTE: No longer using registry keys to do startup. Still leaving code bc it looks nice :o
    //      This is from issue of program crashing when started from the registry for some odd reason.

    public class ApplicationSettings {

        public enum RegistryAction {
            LOCAL_MACHINE_ADD_STARTUP,
            LOCAL_MACHINE_REMOVE_STARTUP,
            CURRENT_USER_ADD_STARTUP,
            CURRENT_USER_REMOVE_STARTUP
        }

        public double interval1 { get; set; }
        public double interval2 { get; set; }
        public bool enabled { get; set; }
        public string selectedCharacter { get; set; }
        public int counter { get; set; }
        public string status { get; set; }
        public int secondsTilNext { get; set; }

        public ApplicationSettings() { } //For XML Serializer

        public ApplicationSettings(double i1, double i2) {
            interval1 = i1;
            interval2 = i2;
        }

        public ApplicationSettings(double i1, double i2, bool active) {
            interval1 = i1;
            interval2 = i2;
            enabled = active;
        }

        public static void CreateStartup() {

        }

        public static void RemoveStartup() {

        }

        public static void RegModify(RegistryAction r, Assembly curAssembly) {
            //Assembly curAssembly = Assembly.GetExecutingAssembly();
            Microsoft.Win32.RegistryKey key;
            string keyVal = curAssembly.GetName().Name;
            string keyLocation = "\"" + curAssembly.Location + "\"";
            string resultMessage = string.Empty;
            try {
                switch (r) {
                    case RegistryAction.CURRENT_USER_ADD_STARTUP:
                        key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                        key.SetValue(keyVal, keyLocation);
                        resultMessage = "Successfully created key.";
                        break;
                    case RegistryAction.CURRENT_USER_REMOVE_STARTUP:
                        key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                        key.DeleteValue(keyVal, false);
                        resultMessage = "Successfully removed key.";
                        break;
                    case RegistryAction.LOCAL_MACHINE_ADD_STARTUP:
                        key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                        key.SetValue(keyVal, keyLocation);
                        resultMessage = "Successfully created key.";
                        break;
                    case RegistryAction.LOCAL_MACHINE_REMOVE_STARTUP:
                        key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                        key.DeleteValue(keyVal, false);
                        resultMessage = "Successfully removed key.";
                        break;
                }
                MessageBox.Show(resultMessage, "Windows Provider Host", MessageBoxButton.OK,
                MessageBoxImage.Information, MessageBoxResult.OK,
                MessageBoxOptions.DefaultDesktopOnly);
            } catch (System.Security.SecurityException se) {
                string message = se.Message + " To use this function, run the program as administrator";
                MessageBox.Show(message, "Windows Provider Host", MessageBoxButton.OK,
                MessageBoxImage.Error, MessageBoxResult.OK,
                MessageBoxOptions.DefaultDesktopOnly);
            } catch (Exception e) {
                MessageBox.Show(e.Message, "Windows Provider Host", MessageBoxButton.OK,
                MessageBoxImage.Error, MessageBoxResult.OK,
                MessageBoxOptions.DefaultDesktopOnly);
            }
        }

    }
}
