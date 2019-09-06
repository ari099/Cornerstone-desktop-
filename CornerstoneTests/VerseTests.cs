using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cornerstone__desktop_.Custom;
using NUnit.Framework;

namespace CornerstoneTests {
    [TestFixture]
    public class VerseTests {
        #region Bible Verse and Chapter Methods
        [Test]
        void GetVerseTest() {
            string verse = new Cornerstone("kjv").GetVerse(1, 1, 1);
            Assert.AreEqual(verse, "In the beginning God created the heaven and the earth.");

            string verse2 = new Cornerstone("kjv").GetVerse("Genesis", 1, 1);
            Assert.AreEqual(verse2, "In the beginning God created the heaven and the earth.");

            string verse3 = new Cornerstone("kjv").GetVerse("01001001");
            Assert.AreEqual(verse3, "In the beginning God created the heaven and the earth.");
        }

        [Test]
        void GetNumberOfChaptersTest() {
            int chapters = new Cornerstone("kjv").GetNumberOfChapters(1);
            Assert.AreEqual(chapters, 50);
        }

        [Test]
        void GetNumberOfVersesInChapterTest() {
            int verses = new Cornerstone("kjv").GetNumberOfVerses(1, 1);
            Assert.AreEqual(verses, 31);
        }
        #endregion

        #region Bible Note Methods
        [Test]
        void GetNoteTest() {
            // complete this test....
        }
        #endregion
    }
}
