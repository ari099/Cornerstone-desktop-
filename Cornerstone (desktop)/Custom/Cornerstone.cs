using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cornerstone__desktop_.Custom {
    /// <summary>
    /// Cornerstone static object class for handling the actual
    /// cornerstone.sqlite3 database
    /// </summary>
    public static class Cornerstone {
        /// <summary>
        /// Add verse note to database
        /// </summary>
        /// <param name="verse_code"></param>
        /// <param name="note"></param>
        public static void AddNote(string verse_code, string note) {
            string query = string.Format("INSERT INTO Note (code, note) VALUES ({0}, {1});", verse_code, note);
            QueryDatabase(query);
        }

        /// <summary>
        /// Remove verse note from database
        /// </summary>
        /// <param name="verse_code"></param>
        public static void RemoveNote(string verse_code) {
            string query = string.Format("DELETE FROM Note WHERE code = {0}", verse_code);
            QueryDatabase(query);
        }

        /// <summary>
        /// Query the Cornerstone database
        /// (cornerstone.sqlite3)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private static int QueryDatabase(string query) {
            SQLiteConnection conn = new SQLiteConnection("DataSource=.\\cornerstone.sqlite3;Version=3");
            int rows = 0;
            try {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(query);
                rows = cmd.ExecuteNonQuery();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            } finally {
                conn.Close();
            }

            return rows;
        }
    }
}
