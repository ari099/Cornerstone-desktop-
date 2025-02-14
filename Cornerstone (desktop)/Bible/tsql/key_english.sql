
/***********************************************************************************
    Author: Daniel Bouchard
    Date:   Nov. 18th, 2017

    Notes:
	   This is an adaptation of the mysql script key_abbreviations_english.sql for TSQL
***********************************************************************************/

-- Create Table

DROP TABLE IF EXISTS 
     key_english;

CREATE TABLE key_english (
    b INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    n TEXT NOT NULL,
    t VARCHAR(2) NOT NULL,
    g INT NOT NULL
);

-- Add comments

EXEC sp_addextendedproperty 
     @name = N'Description',
     @value = 'Book #',
     @level0type = N'Schema',
     @level0name = 'dbo',
     @level1type = N'Table',
     @level1name = 'key_english',
     @level2type = N'Column',
     @level2name = 'b';

EXEC sp_addextendedproperty 
     @name = N'Description',
     @value = 'Name',
     @level0type = N'Schema',
     @level0name = 'dbo',
     @level1type = N'Table',
     @level1name = 'key_english',
     @level2type = N'Column',
     @level2name = 'n';

EXEC sp_addextendedproperty 
     @name = N'Description',
     @value = 'Which Testament this book is in',
     @level0type = N'Schema',
     @level0name = 'dbo',
     @level1type = N'Table',
     @level1name = 'key_english',
     @level2type = N'Column',
     @level2name = 't';

EXEC sp_addextendedproperty 
     @name = N'Description',
     @value = 'A genre ID to identify the type of book this is',
     @level0type = N'Schema',
     @level0name = 'dbo',
     @level1type = N'Table',
     @level1name = 'key_english',
     @level2type = N'Column',
     @level2name = 'g';

-- Insert Data

SET IDENTITY_INSERT dbo.key_english ON;

INSERT INTO             key_english (
       b,
       n,
       t,
       g
) 
VALUES (
       1,'Genesis','OT',1
 ), (
       2,'Exodus','OT',1
 ), (
       3,'Leviticus','OT',1
 ), (
       4,'Numbers','OT',1
 ), (
       5,'Deuteronomy','OT',1
 ), (
       6,'Joshua','OT',2
 ), (
       7,'Judges','OT',2
 ), (
       8,'Ruth','OT',2
 ), (
       9,'1 Samuel','OT',2
 ), (
       10,'2 Samuel','OT',2
 ), (
       11,'1 Kings','OT',2
 ), (
       12,'2 Kings','OT',2
 ), (
       13,'1 Chronicles','OT',2
 ), (
       14,'2 Chronicles','OT',2
 ), (
       15,'Ezra','OT',2
 ), (
       16,'Nehemiah','OT',2
 ), (
       17,'Esther','OT',2
 ), (
       18,'Job','OT',3
 ), (
       19,'Psalms','OT',3
 ), (
       20,'Proverbs','OT',3
 ), (
       21,'Ecclesiastes','OT',3
 ), (
       22,'Song of Solomon','OT',3
 ), (
       23,'Isaiah','OT',4
 ), (
       24,'Jeremiah','OT',4
 ), (
       25,'Lamentations','OT',4
 ), (
       26,'Ezekiel','OT',4
 ), (
       27,'Daniel','OT',4
 ), (
       28,'Hosea','OT',4
 ), (
       29,'Joel','OT',4
 ), (
       30,'Amos','OT',4
 ), (
       31,'Obadiah','OT',4
 ), (
       32,'Jonah','OT',4
 ), (
       33,'Micah','OT',4
 ), (
       34,'Nahum','OT',4
 ), (
       35,'Habakkuk','OT',4
 ), (
       36,'Zephaniah','OT',4
 ), (
       37,'Haggai','OT',4
 ), (
       38,'Zechariah','OT',4
 ), (
       39,'Malachi','OT',4
 ), (
       40,'Matthew','NT',5
 ), (
       41,'Mark','NT',5
 ), (
       42,'Luke','NT',5
 ), (
       43,'John','NT',5
 ), (
       44,'Acts','NT',6
 ), (
       45,'Romans','NT',7
 ), (
       46,'1 Corinthians','NT',7
 ), (
       47,'2 Corinthians','NT',7
 ), (
       48,'Galatians','NT',7
 ), (
       49,'Ephesians','NT',7
 ), (
       50,'Philippians','NT',7
 ), (
       51,'Colossians','NT',7
 ), (
       52,'1 Thessalonians','NT',7
 ), (
       53,'2 Thessalonians','NT',7
 ), (
       54,'1 Timothy','NT',7
 ), (
       55,'2 Timothy','NT',7
 ), (
       56,'Titus','NT',7
 ), (
       57,'Philemon','NT',7
 ), (
       58,'Hebrews','NT',7
 ), (
       59,'James','NT',7
 ), (
       60,'1 Peter','NT',7
 ), (
       61,'2 Peter','NT',7
 ), (
       62,'1 John','NT',7
 ), (
       63,'2 John','NT',7
 ), (
       64,'3 John','NT',7
 ), (
       65,'Jude','NT',7
 ), (
       66,'Revelation','NT',8
 );

SET IDENTITY_INSERT dbo.key_english OFF;
