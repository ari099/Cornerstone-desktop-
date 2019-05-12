using Cornerstone__desktop_.Custom;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Cornerstone bible = new Cornerstone();
        public MainWindow() {
            //Chapters = new ComboBox();
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
            //Cornerstone cs = new Cornerstone();
            //Debug.Print(cs.GetNumberOfChapters(1).ToString());
            //Debug.Print(cs.GetNumberOfVerses(1, 1).ToString());
            //Debug.Print(cs.GetVerse("01001001"));
            //Debug.Print(cs.GetVerse(1,1,1));
            //foreach(var verse in cs.GetFullChapter(1, 1))
            //    Debug.Print(verse.ToString());
        }

        private void StyleTopBar_MouseDown(object sender, MouseButtonEventArgs e) {
            // Drag window by top styled bar....
            DragMove();
        }

        private void AppMaximize_Click(object sender, RoutedEventArgs e) {
            // Change window state....
            if (Application.Current.MainWindow.WindowState == WindowState.Normal)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        private void Books_Selected(object sender, RoutedEventArgs e) {
            string selectedBook = (Books.SelectedItem as ComboBoxItem).Content.ToString();
            int limit = bible.GetNumberOfChapters(selectedBook);
            if (Chapters != null) Chapters.Items.Clear();
            for (int i = 1; i <= limit; i++) Chapters.Items.Add(i);
        }

        private void Chapters_Selected(object sender, RoutedEventArgs e) {
            string selectedBook = (Books.SelectedItem as ComboBoxItem).Content.ToString();
            string selectedChapter = Chapters.SelectedItem.ToString();
            int limit = bible.GetNumberOfVerses(selectedBook, Convert.ToInt32(selectedChapter));
            if (Verses != null) Verses.Items.Clear();
            for (int i = 1; i <= limit; i++) Verses.Items.Add(i);
        }
    }
}
