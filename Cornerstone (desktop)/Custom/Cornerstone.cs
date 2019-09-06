using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
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
        private string bibleXmlPath = "../../Bible/xml/t_kjv.xml";
        private XmlDocument bible = new XmlDocument();

        /// <summary>
        /// KJV version of the Bible is automatically loaded in
        /// this constructor
        /// </summary>
        public Cornerstone() {
            bible.Load(bibleXmlPath);
        }

        /// <summary>
        /// Version of the Bible is specified on instance creation
        /// </summary>
        /// <param name="version"></param>
        public Cornerstone(string version) {
            bibleXmlPath = "../../Bible/xml/t_" + version + ".xml";
            bible.Load(bibleXmlPath);
        }

        /// <summary>
        /// List of the books of the Bible
        /// </summary>
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

        #region Database Functions
        /// <summary>
        /// Query the Cornerstone database
        /// (cornerstone.sqlite3)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private int QueryDatabase(string query) {
            SQLiteConnection conn = new SQLiteConnection("DataSource=.\\cornerstone.sqlite3;Version=3");
            int rows = 0;
            try {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(query);
                rows = cmd.ExecuteNonQuery();
            } catch (Exception ex) {
                Debug.Print(ex.Message);
            } finally {
                conn.Close();
            }

            return rows;
        }

        /// <summary>
        /// Query the Cornerstone database
        /// </summary>
        /// <param name="query"></param>
        /// <param name="standin"></param>
        /// <returns></returns>
        private string[] QueryDatabase(string query, int standin = 0) {
            SQLiteConnection conn = new SQLiteConnection("DataSource=.\\cornerstone.sqlite3;Version=3");
            List<string> results = new List<string>();
            try {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query)) {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader()) {
                        while (rdr.Read()) {
                            results.Add(rdr.GetString(1));
                        }
                    }
                }
                //rows = cmd.ExecuteNonQuery();
            } catch (Exception ex) {
                Debug.Print(ex.Message);
            } finally {
                conn.Close();
            }

            return results.ToArray();
        }
        #endregion

        #region Note Methods
        /// <summary>
        /// Add verse note to database
        /// </summary>
        /// <param name="verse_code"></param>
        /// <param name="note"></param>
        public void AddNote(string verse_code, string note) {
            string query = string.Format("INSERT INTO Note (code, note) VALUES ('{0}', '{1}');", verse_code, note);
            QueryDatabase(query);
        }

        /// <summary>
        /// Remove verse note from database
        /// </summary>
        /// <param name="verse_code"></param>
        public void RemoveNote(string verse_code) {
            string query = string.Format("DELETE FROM Note WHERE code = '{0}'", verse_code);
            QueryDatabase(query);
        }

        /// <summary>
        /// Get verse note from database
        /// </summary>
        /// <param name="verse_code"></param>
        /// <returns></returns>
        public string GetNote(string verse_code) {
            string query = string.Format("SELECT * FROM Note WHERE code = '{0}'", verse_code);
            string[] notes = QueryDatabase(query, 0);
            if (notes.Length > 0) return notes[0];
            else return string.Empty;
        }
        #endregion

        #region Bible Methods
        /// <summary>
        /// Get Bible book name from the book number
        /// </summary>
        /// <param name="book_no"></param>
        /// <returns></returns>
        public string GetBookName(int book_no) {
            return books.ElementAt(book_no - 1);
        }

        /// <summary>
        /// Get the index of the list of Bible books by the book name
        /// </summary>
        /// <param name="book_name"></param>
        /// <returns></returns>
        public int GetBookNumber(string book_name) {
            int index = books.IndexOf(book_name);
            return index + 1;
        }

        /// <summary>
        /// Get number of chapters in a particular book of the Bible
        /// </summary>
        /// <param name="book_no"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public int GetNumberOfChapters(int book_no) {
            List<int> chapter_numbers = new List<int>();
            XmlElement root = bible.DocumentElement;
            List<XmlNode> book = root.ChildNodes.Cast<XmlNode>()
                .Where(n => Convert.ToInt32(n.ChildNodes.Item(1).InnerText) == book_no)
                .Select(x => x)
                .ToList();
            book.ForEach(x => {
                var code = x.FirstChild.InnerText;
                if(!chapter_numbers.Contains(Convert.ToInt32(code.Substring(2, 3))))
                    chapter_numbers.Add(Convert.ToInt32(code.Substring(2, 3)));
            });

            return chapter_numbers.Count;
        }

        /// <summary>
        /// Get number of chapters in a particular book of the Bible
        /// </summary>
        /// <param name="book_name"></param>
        /// <returns></returns>
        public int GetNumberOfChapters(string book_name) {
            List<int> chapter_numbers = new List<int>();
            XmlElement root = bible.DocumentElement;
            int book_no = GetBookNumber(book_name);
            List<XmlNode> book = root.ChildNodes.Cast<XmlNode>()
                .Where(n => Convert.ToInt32(n.ChildNodes.Item(1).InnerText) == book_no)
                .Select(x => x)
                .ToList();
            book.ForEach(x => {
                var code = x.FirstChild.InnerText;
                if (!chapter_numbers.Contains(Convert.ToInt32(code.Substring(2, 3))))
                    chapter_numbers.Add(Convert.ToInt32(code.Substring(2, 3)));
            });

            return chapter_numbers.Count;
        }

        /// <summary>
        /// Get number of verses in a particular chapter of a
        /// particular book of the Bible
        /// </summary>
        /// <param name="book_no"></param>
        /// <param name="chapter_no"></param>
        /// <returns></returns>
        public int GetNumberOfVerses(int book_no, int chapter_no) {
            List<int> verse_numbers = new List<int>();
            XmlElement root = bible.DocumentElement;
            List<XmlNode> book = root.ChildNodes.Cast<XmlNode>()
                .Where(n => Convert.ToInt32(n.ChildNodes.Item(1).InnerText) == book_no && Convert.ToInt32(n.ChildNodes.Item(2).InnerText) == chapter_no)
                .Select(x => x)
                .ToList();
            book.ForEach(x => {
                var code = x.FirstChild.InnerText;
                if (!verse_numbers.Contains(Convert.ToInt32(code.Substring(5, 3))))
                    verse_numbers.Add(Convert.ToInt32(code.Substring(5, 3)));
            });

            return verse_numbers.Count;
        }

        /// <summary>
        /// Get number of verses in a particular chapter of a
        /// particular book of the Bible
        /// </summary>
        /// <param name="book_name"></param>
        /// <param name="chapter_no"></param>
        /// <returns></returns>
        public int GetNumberOfVerses(string book_name, int chapter_no) {
            List<int> verse_numbers = new List<int>();
            XmlElement root = bible.DocumentElement;
            int book_no = GetBookNumber(book_name);
            List<XmlNode> book = root.ChildNodes.Cast<XmlNode>()
                .Where(n => Convert.ToInt32(n.ChildNodes.Item(1).InnerText) == book_no && Convert.ToInt32(n.ChildNodes.Item(2).InnerText) == chapter_no)
                .Select(x => x)
                .ToList();
            book.ForEach(x => {
                var code = x.FirstChild.InnerText;
                if (!verse_numbers.Contains(Convert.ToInt32(code.Substring(5, 3))))
                    verse_numbers.Add(Convert.ToInt32(code.Substring(5, 3)));
            });

            return verse_numbers.Count;
        }

        /// <summary>
        /// Obtain the Bible verse with the verse code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetVerse(string code) {
            XmlElement root = bible.DocumentElement;
            string verse_text = null;
            List<XmlNode> book = root.ChildNodes.Cast<XmlNode>()
                .Where(verse => verse.FirstChild.InnerText == code)
                .Select(x => x)
                .ToList();
            book.ForEach(x => {
                verse_text = x.ChildNodes.Item(4).InnerText;
            });

            return verse_text;
        }

        /// <summary>
        /// Obtain the Bible verse with the book, chapter,
        /// and verse specified
        /// </summary>
        /// <param name="book_no"></param>
        /// <param name="chapter_no"></param>
        /// <param name="verse_no"></param>
        /// <returns></returns>
        public string GetVerse(int book_no, int chapter_no, int verse_no) {
            string code = book_no.ToString("00") +
                chapter_no.ToString("000") +
                verse_no.ToString("000");

            XmlElement root = bible.DocumentElement;
            string verse_text = null;
            var book = root.ChildNodes.Cast<XmlNode>()
                .Where(verse => verse.FirstChild.InnerText == code)
                .Select(x => x)
                .ToList();
            book.ForEach(x => {
                verse_text = x.ChildNodes.Item(4).InnerText;
            });

            return verse_text;
        }

        /// <summary>
        /// Obtain the Bible verse with the book, chapter,
        /// and verse specified
        /// </summary>
        /// <param name="book_name"></param>
        /// <param name="chapter_no"></param>
        /// <param name="verse_no"></param>
        /// <returns></returns>
        public string GetVerse(string book_name, int chapter_no, int verse_no) {
            int book_no = GetBookNumber(book_name);
            string code = book_no.ToString("00") +
                chapter_no.ToString("000") +
                verse_no.ToString("000");

            XmlElement root = bible.DocumentElement;
            string verse_text = null;
            var book = root.ChildNodes.Cast<XmlNode>()
                .Where(verse => verse.FirstChild.InnerText == code)
                .Select(x => x)
                .ToList();
            book.ForEach(x => {
                verse_text = x.ChildNodes.Item(4).InnerText;
            });

            return verse_text;
        }

        /// <summary>
        /// Obtain all the verses associated with a particular chapter
        /// of a particular book of the Bible
        /// </summary>
        /// <param name="book_no"></param>
        /// <param name="chapter_no"></param>
        /// <returns></returns>
        public string[] GetFullChapter(int book_no, int chapter_no) {
            List<string> verses = new List<string>();
            XmlElement root = bible.DocumentElement;
            List<XmlNode> book = root.ChildNodes.Cast<XmlNode>()
                .Where(n => Convert.ToInt32(n.ChildNodes.Item(1).InnerText) == book_no && Convert.ToInt32(n.ChildNodes.Item(2).InnerText) == chapter_no)
                .Select(x => x)
                .ToList();
            book.ForEach(x => {
                string verse = x.ChildNodes.Item(4).InnerText;
                verses.Add(verse);
            });

            return verses.ToArray();
        }

        /// <summary>
        /// Obtain all the verses associated with a particular chapter
        /// of a particular book of the Bible
        /// </summary>
        /// <param name="book_name"></param>
        /// <param name="chapter_no"></param>
        /// <returns></returns>
        public string[] GetFullChapter(string book_name, int chapter_no) {
            List<string> verses = new List<string>();
            XmlElement root = bible.DocumentElement;
            int book_no = GetBookNumber(book_name);
            List<XmlNode> book = root.ChildNodes.Cast<XmlNode>()
                .Where(n => Convert.ToInt32(n.ChildNodes.Item(1).InnerText) == book_no && Convert.ToInt32(n.ChildNodes.Item(2).InnerText) == chapter_no)
                .Select(x => x)
                .ToList();
            book.ForEach(x => {
                string verse = x.ChildNodes.Item(4).InnerText;
                verses.Add(verse);
            });

            return verses.ToArray();
        }

        #region Specific Verse Methods
        /// <summary>
        /// Get verse code
        /// </summary>
        /// <param name="book_name"></param>
        /// <param name="chapter_no"></param>
        /// <param name="verse_no"></param>
        /// <returns></returns>
        public string GetVerseCode(string book_name, int chapter_no, int verse_no) {
            int book_no = GetBookNumber(book_name);
            string code = book_no.ToString("00") +
                chapter_no.ToString("000") +
                verse_no.ToString("000");
            return code;
        }

        /// <summary>
        /// Get verse code
        /// </summary>
        /// <param name="book_no"></param>
        /// <param name="chapter_no"></param>
        /// <param name="verse_no"></param>
        /// <returns></returns>
        public string GetVerseCode(int book_no, int chapter_no, int verse_no) {
            string code = book_no.ToString("00") +
                chapter_no.ToString("000") +
                verse_no.ToString("000");
            return code;
        }

        /// <summary>
        /// Highlight a verse on the FlowDocument
        /// </summary>
        /// <param name="verse_code"></param>
        public void HighlightVerse(string verse_code) {
            // complete this method....
        }

        /// <summary>
        /// Remove verse highlight on the FlowDocument
        /// </summary>
        /// <param name="verse_code"></param>
        public void RemoveHighlightVerse(string verse_code) {
            // complete this method....
        }

        #endregion

        #endregion
    }
}
