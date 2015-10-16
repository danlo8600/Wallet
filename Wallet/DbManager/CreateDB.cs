using SQLite.Net.Attributes;
using System;
using System.IO;
using Windows.Storage;
using SQLite.Net;
using System.Diagnostics;

namespace Wallet.DbManager
{

    class CreateDB
    {
        private SQLiteConnection db = null;

        public CreateDB(StorageFolder folder)
        {
            try
            {
                var path = Path.Combine(folder.Path, "wallet.db");
                db = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
                db.CreateTable<Account>();
                db.CreateTable<Cost>();
                db.CreateTable<Activity>();
            }
            catch(System.NullReferenceException NREEX)
            {
                Debug.WriteLine("ERROR: " + NREEX.Message);
            }
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

    [Table("Account")]
    public class Account
    {
        [PrimaryKey, Column("IdAccount"), AutoIncrement]
        public int IdAccount { get; set; }
        [Column("Amount"), NotNull]
        public float Amount { get; set; }
        [Column("Date"), NotNull]
        public DateTime Date { get; set; }
        [Column("Description"), MaxLength(50)]
        public string Description { get; set; }
    }

    [Table("Cost")]
    public class Cost
    {
        [PrimaryKey, Column("IdCost"), AutoIncrement]
        public int IdCost { get; set; }
        [Column("ActivityId")]
        public string ActivityId { get; set; }
        [Column("Price"), NotNull, MaxLength(10)]
        public float Price { get; set; }
        [Column("Date"), NotNull, MaxLength(10)]
        public DateTime Date { get; set; }
        [Column("Description"), MaxLength(50)]
        public string Description { get; set; }
    }

    [Table("Activity")]
    public class Activity
    {
        [Column("Id"), PrimaryKey, MaxLength(50)]
        public string Id { get; set; }
        [Column("Description"), MaxLength(50)]
        public string Description { get; set; }
    }

}
