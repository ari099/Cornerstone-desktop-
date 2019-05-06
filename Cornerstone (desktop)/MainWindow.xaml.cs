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

namespace Cornerstone__desktop_ {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void AppClose_Click(object sender, RoutedEventArgs e) {
            // Close application....
            Application.Current.Shutdown();
        }

        private void TopBar_MouseDown(object sender, MouseButtonEventArgs e) {
            // Drag window by top bar....
            DragMove();
        }

        private void AppMinimize_Click(object sender, RoutedEventArgs e) {
            // Minimize applcation window....
            WindowState = WindowState.Minimized;
        }

        private void SaveVerseNote_Click(object sender, RoutedEventArgs e) {
            // Save note to database....
        }
    }
}
