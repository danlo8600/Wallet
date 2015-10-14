using SQLite.Net.Attributes;
using System;
using System.IO;
using Windows.Storage;
using SQLite.Net;

namespace Wallet.DbManager
{

    class CreateDB
    {
        public SQLiteConnection cdb = null;

        public CreateDB(StorageFolder folder)
        {
            var path = Path.Combine(folder.Path, "wallet.db");
    
            using (cdb = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path))
            {
                cdb.CreateTable<Event>();
                cdb.CreateTable<Price>();
            }
        }

        public SQLiteConnection getConnection()
        {
            return cdb;
        }

        public void closeDB()
        {
            cdb.Close();
        }

    }

    [Table("Event")]
    public class Event
    {
        [Column("IdEvent"), PrimaryKey, AutoIncrement]
        public int IdEvent { get; set; }
        [Column("Name"), Unique, NotNull, MaxLength(225)]
        public string Name { get; set; }
        [Column("Description"), MaxLength(225)]
        public string Description { get; set; }
    }

    [Table("Price")]
    public class Price
    {
        [Column("IdPrice"), PrimaryKey, AutoIncrement]
        public int IdPrice { get; set; }
        [Column("Value"), NotNull, MaxLength(10)]
        public float price { get; set; }
        [Column("Date"), NotNull, MaxLength(10)]
        public DateTime date { get; set; }

        [Indexed, Column("IdE")]
        public int IdE { get; set; }
    }
}
