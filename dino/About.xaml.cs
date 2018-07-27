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
using System.Diagnostics;

namespace dino {
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window {
        public About() {
            InitializeComponent();
        }

        public static void ShowForm() {
            About a = new About();
            a.ShowDialog();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void btnGithub_Click(object sender, RoutedEventArgs e) {
            Process.Start(new ProcessStartInfo("https://github.com/Adam-Shehadeh/DinoRex"));
        }
    }
}
