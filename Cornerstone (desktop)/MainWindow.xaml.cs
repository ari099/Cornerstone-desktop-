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
            Cornerstone cs = new Cornerstone();
            Debug.Print(cs.GetNumberOfChapters(1).ToString());
            Debug.Print(cs.GetNumberOfVerses(1, 1).ToString());
            Debug.Print(cs.GetVerse("01001001"));
            Debug.Print(cs.GetVerse(1, 1, 1));
            foreach (var verse in cs.GetFullChapter(1, 1))
                Debug.Print(verse.ToString());
            Debug.Print(string.Empty);
            foreach (var verse in cs.GetFullChapter("Genesis", 1))
                Debug.Print(verse.ToString());
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
            for (int i = 1; i <= limit; i++) {
                ComboBoxItem n = new ComboBoxItem();
                n.Content = i;
                Chapters.Items.Add(n);
            }

            // Lookup chapter 1 of whatever book is selected....
            Lookup(sender, e);
        }

        private void Chapters_Selected(object sender, RoutedEventArgs e) {
            string selectedBook = (Books.SelectedItem as ComboBoxItem).Content.ToString();
            string selectedChapter = (Chapters.SelectedItem as ComboBoxItem).Content.ToString();
            int limit = bible.GetNumberOfVerses(selectedBook, Convert.ToInt32(selectedChapter));
            if (Verses != null) Verses.Items.Clear();
            for (int i = 1; i <= limit; i++) {
                ComboBoxItem n = new ComboBoxItem();
                n.Content = i;
                Verses.Items.Add(n);
            }

            // Lookup chapter selected of the book selected
            Lookup(sender, e);
        }

        /// <summary>
        /// Verse lookup event handler...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lookup(object sender, RoutedEventArgs e) {
            // complete this method....
            string selectedBook = string.Empty, selectedChapter = string.Empty, selectedVerse = string.Empty;
            if (Books.SelectedItem != null)
                selectedBook = (Books.SelectedItem as ComboBoxItem).Content.ToString();
            if (Chapters.SelectedItem != null)
                selectedChapter = (Chapters.SelectedItem as ComboBoxItem).Content.ToString();
            if (Verses.SelectedItem != null)
                selectedVerse = (Verses.SelectedItem as ComboBoxItem).Content.ToString();

            if (!string.IsNullOrEmpty(selectedBook) && !string.IsNullOrEmpty(selectedChapter)) {
                ScriptureText.Blocks.Clear();
                foreach (string verse in bible.GetFullChapter(selectedBook, Convert.ToInt32(selectedChapter))) {
                    Paragraph verseText = new Paragraph(new Run(verse));
                    verseText.FontSize = 20;
                    verseText.Foreground = new SolidColorBrush(Colors.Green);
                    verseText.FontFamily = new FontFamily("Segoe UI Semibold");
                    ScriptureText.Blocks.Add(verseText);
                }
            }

            if (!string.IsNullOrEmpty(selectedBook) && !string.IsNullOrEmpty(selectedChapter) && !string.IsNullOrEmpty(selectedVerse)) {
                ScriptureText.Blocks.Clear();
                string v = bible.GetVerse(selectedBook, Convert.ToInt32(selectedChapter), Convert.ToInt32(selectedVerse));
                Paragraph verseText = new Paragraph(new Run(v));
                verseText.FontSize = 20;
                verseText.Foreground = new SolidColorBrush(Colors.Green);
                verseText.FontFamily = new FontFamily("Segoe UI Semibold");
                ScriptureText.Blocks.Add(verseText);
            }
        }
    }
}
