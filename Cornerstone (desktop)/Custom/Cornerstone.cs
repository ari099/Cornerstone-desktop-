using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Cornerstone__desktop_.Custom {
    /// <summary>
    /// Cornerstone object class for handling the actual
    /// cornerstone.sqlite3 database
    /// </summary>
    public class Cornerstone {
        private string bibleXmlPath = "";
        private XmlDocument bible = new XmlDocument();

        public Cornerstone() {
            bible.Load(bibleXmlPath);
        }

        public List<string> books = new List<string> {
            "Genesis", "Exodus", "Leviticus",
            "Numbers", "Deuteronomy", "Joshua",
            "Judges", "Ruth", "1 Samuel", "2 Samuel",
            "1 Kings", "2 Kings", "1 Chronicles", "2 Chronicles",
            "Ezra", "Nehemiah", "Esther", "Job", "Psalms", "Proverbs",
            "Ecclesiastes", "Song of Solomon", "Isaiah", "Jeremiah",
            "Lamentations", "Ezekiel", "Daniel", "Hosea", "Joel",
            "Amos", "Obadiah", "Jonah", "Micah", "Nahum",
            "Habakkuk", "Zephaniah", "Haggai", "Zechariah",
            "Malachi", "Matthew", "Mark", "Luke", "John",
            "Acts of the Apostles", "Romans", "1 Corinthians",
            "2 Corinthians", "Galatians", "Ephesians", "Phillippians",
            "Colossians", "1 Thessalonians", "2 Thessalonians",
            "1 Timothy", "2 Timothy", "Titus", "Philemon",
            "Hebrews", "James", "1 Peter", "2 Peter", "1 John",
            "2 John", "3 John", "Jude", "Revelation"
        };

        /// <summary>
        /// Query the Cornerstone database
        /// (cornerstone.sqlite3)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private int QueryDatabase(string query)
        {
            SQLiteConnection conn = new SQLiteConnection("DataSource=.\\cornerstone.sqlite3;Version=3");
            int rows = 0;
            try
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(query);
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        #region Note Methods
        /// <summary>
        /// Add verse note to database
        /// </summary>
        /// <param name="verse_code"></param>
        /// <param name="note"></param>
        public void AddNote(string verse_code, string note) {
            string query = string.Format("INSERT INTO Note (code, note) VALUES ({0}, {1});", verse_code, note);
            QueryDatabase(query);
        }

        /// <summary>
        /// Remove verse note from database
        /// </summary>
        /// <param name="verse_code"></param>
        public void RemoveNote(string verse_code) {
            string query = string.Format("DELETE FROM Note WHERE code = {0}", verse_code);
            QueryDatabase(query);
        }
        #endregion

        #region Bible Methods (dev)
        /// <summary>
        /// Get Bible book name from the book number
        /// </summary>
        /// <param name="book_no"></param>
        /// <returns></returns>
        public string GetBookName(int book_no) {
            return books.ElementAt(book_no - 1);
        }

        /// <summary>
        /// Get number of chapters in a particular book
        /// </summary>
        /// <param name="book_no"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public int GetNumberOfChapters(int book_no, string version) {
            string book = Convert.ToString(book_no);
            XmlElement root = bible.DocumentElement;
            // complete this method....
            return 0;
        }
        #endregion
    }
}
