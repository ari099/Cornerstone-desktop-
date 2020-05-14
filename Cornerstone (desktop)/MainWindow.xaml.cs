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
            // Getting the verse code....
            string verse_code = bible.GetBookNumber((Books.SelectedItem as ComboBoxItem).Content.ToString()).ToString("00") +
                Convert.ToInt32((Chapters.SelectedItem as ComboBoxItem).Content.ToString()).ToString("000") +
                Convert.ToInt32((Verses.SelectedItem as ComboBoxItem).Content.ToString()).ToString("000");

            // Save note to database....
            //string note = VerseNote.Text;
            //bible.AddNote(verse_code, note);

            //Cornerstone cs = new Cornerstone();
            //Debug.Print(cs.GetNumberOfChapters(1).ToString());
            //Debug.Print(cs.GetNumberOfVerses(1, 1).ToString());
            //Debug.Print(cs.GetVerse("01001001"));
            //Debug.Print(cs.GetVerse(1, 1, 1));
            //foreach (var verse in cs.GetFullChapter(1, 1))
            //    Debug.Print(verse.ToString());
            //Debug.Print(string.Empty);
            //foreach (var verse in cs.GetFullChapter("Genesis", 1))
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
            ScriptureText.Blocks.Clear();
            Chapters.Items.Clear();
            string selectedBook = (Books.SelectedItem as ComboBoxItem).Content.ToString();
            int limit = bible.GetNumberOfChapters(selectedBook);
            for (int i = 1; i <= limit; i++) {
                ComboBoxItem chapter_n = new ComboBoxItem();
                chapter_n.Content = i;
                //chapter_n.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF261704"));
                chapter_n.Background = new SolidColorBrush(Colors.LightYellow);
                chapter_n.BorderBrush = null;
                Chapters.Items.Add(chapter_n);
            }

            // Select the first chapter of the book selected....
            Chapters.SelectedIndex = 0;

            // Lookup chapter 1 of whatever book is selected....
            Lookup(sender, e);
        }

        private void Chapters_Selected(object sender, RoutedEventArgs e) {
            Verses.Items.Clear();
            string selectedBook = (Books.SelectedItem as ComboBoxItem).Content.ToString();
            if(Chapters.SelectedItem != null) {
                string selectedChapter = (Chapters.SelectedItem as ComboBoxItem).Content.ToString();
                int limit = bible.GetNumberOfVerses(selectedBook, Convert.ToInt32(selectedChapter));
                for (int i = 1; i <= limit; i++) {
                    ComboBoxItem verse_n = new ComboBoxItem();
                    verse_n.Content = i;
                    //verse_n.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF261704"));
                    verse_n.Background = new SolidColorBrush(Colors.LightYellow);
                    verse_n.BorderBrush = null;
                    Verses.Items.Add(verse_n);
                }
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
            if (Books != null && Books.SelectedItem != null)
                selectedBook = (Books.SelectedItem as ComboBoxItem).Content.ToString();
            if (Chapters != null && Chapters.SelectedItem != null)
                selectedChapter = (Chapters.SelectedItem as ComboBoxItem).Content.ToString();
            if (Verses != null && Verses.SelectedItem != null)
                selectedVerse = (Verses.SelectedItem as ComboBoxItem).Content.ToString();

            if (!string.IsNullOrEmpty(selectedBook) && !string.IsNullOrEmpty(selectedChapter)) {
                ScriptureText.Blocks.Clear();
                int verseCount = 1; // Keep count of verses....
                foreach (string verse in bible.GetFullChapter(selectedBook, Convert.ToInt32(selectedChapter))) {
                    Paragraph verseText = new Paragraph(new Run(verse));
                    verseText.Name = "V" + bible.GetVerseCode(selectedBook, Convert.ToInt32(selectedChapter), verseCount);
                    verseText.Cursor = Cursors.Hand;
                    verseText.FontSize = 20;
                    verseText.Foreground = new SolidColorBrush(Colors.LightYellow);
                    verseText.FontFamily = new FontFamily("Nilland");
                    verseText.Margin = new Thickness(5.0);
                    verseText.Padding = new Thickness(5.0);
                    verseText.MouseEnter += new MouseEventHandler(VerseMouseOver);
                    verseText.MouseLeave += new MouseEventHandler(VerseMouseOut);
                    verseText.MouseDown += new MouseButtonEventHandler(VerseClick);
                    ScriptureText.Blocks.Add(verseText);
                    verseCount++;
                }
            }

            if (!string.IsNullOrEmpty(selectedBook) && !string.IsNullOrEmpty(selectedChapter) && !string.IsNullOrEmpty(selectedVerse)) {
                ScriptureText.Blocks.Clear();
                //VerseNote.IsEnabled = true;
                //SaveVerseNote.IsEnabled = true;
                string v = bible.GetVerse(selectedBook, Convert.ToInt32(selectedChapter), Convert.ToInt32(selectedVerse));
                Paragraph verseText = new Paragraph(new Run(v));
                verseText.Name = "V" + bible.GetVerseCode(selectedBook, Convert.ToInt32(selectedChapter), Convert.ToInt32(selectedVerse));
                verseText.Cursor = Cursors.Hand;
                verseText.FontSize = 20;
                verseText.Foreground = new SolidColorBrush(Colors.LightYellow);
                verseText.FontFamily = new FontFamily("Nilland");
                verseText.Margin = new Thickness(5.0);
                verseText.Padding = new Thickness(5.0);
                verseText.MouseEnter += new MouseEventHandler(VerseMouseOver);
                verseText.MouseLeave += new MouseEventHandler(VerseMouseOut);
                verseText.MouseDown += new MouseButtonEventHandler(VerseClick);
                ScriptureText.Blocks.Add(verseText);

                // Get the note saved in the database....
                //string code = bible.GetVerseCode(selectedBook, Convert.ToInt32(selectedChapter), Convert.ToInt32(selectedVerse));
                //VerseNote.Text = bible.GetNote(code);
            }
        }

        private void VerseClick(object sender, MouseButtonEventArgs e) {
            // Open verse analysis window...
        }

        private void VerseMouseOver(object sender, MouseEventArgs e) {
            Paragraph verseText = sender as Paragraph;
            verseText.Background = new SolidColorBrush(Colors.Black);
            //verseText.Foreground = ScriptureText.Background;
        }

        private void VerseMouseOut(object sender, MouseEventArgs e) {
            Paragraph verseText = sender as Paragraph;
            verseText.Background = null;
            //verseText.Foreground = new SolidColorBrush(Colors.LightYellow);
        }

        private void VersionList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            // Change the bible version....
            bible = new Cornerstone((VersionList.SelectedItem as ComboBoxItem).Content.ToString());
            Lookup(this, e);
        }

        private void LeftChapterSwitch_Click(object sender, RoutedEventArgs e) {
            if (Chapters.SelectedIndex >= 0) Chapters.SelectedIndex -= 1;
        }

        private void RightChapterSwitch_Click(object sender, RoutedEventArgs e) {
            if (Chapters.SelectedIndex <= 65) Chapters.SelectedIndex += 1;
        }
    }
}
