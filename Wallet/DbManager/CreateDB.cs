using SQLite.Net.Attributes;
using System;
using System.IO;
using Windows.Storage;
using SQLite.Net;

namespace Wallet.DbManager
{

    class CreateDB
    {
        public SQLiteConnection db = null;

        public CreateDB(StorageFolder folder)
        {
            var path = Path.Combine(folder.Path, "wallet.db");

            db = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            db.CreateTable<Bill>();
            db.CreateTable<Cost>();
            db.CreateTable<Activity>();
        }

        public SQLiteConnection getConnection()
        {
            return db;
        }

        public void closeDB()
        {
            db.Close();
        }

    }

    [Table("Bill")]
    public class Bill
    {
        [PrimaryKey, Column("IdA"), AutoIncrement]
        public int IdBill { get; set; }
        [Column("Amount"), NotNull]
        public float Amount { get; set; }
        [Column("Date"), NotNull]
        public DateTime Date { get; set; }
        [Column("Description"), MaxLength(10)]
        public string Description { get; set; }
    }

    [Table("Cost")]
    public class Cost
    {
        [Column("IdA")]
        public int IdA { get; set; }
        [Column("IdB")]
        public int IdB { get; set; }
        [Column("Value"), NotNull, MaxLength(10)]
        public float Price { get; set; }
        [Column("Date"), NotNull, MaxLength(10)]
        public DateTime Date { get; set; }
    }

    [Table("Activity")]
    public class Activity
    {
        [Column("IdActivity"), PrimaryKey, AutoIncrement]
        public int IdActivity { get; set; }
        [Column("Name"), Unique, NotNull, MaxLength(225)]
        public string Name { get; set; }
        [Column("Description"), MaxLength(225)]
        public string Description { get; set; }
    }

}
