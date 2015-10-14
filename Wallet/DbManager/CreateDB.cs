using SQLite.Net.Attributes;
using System;
using System.IO;
using Windows.Storage;
using SQLite.Net;
using SQLiteNetExtensions;
using SQLiteNetExtensions.Attributes;

namespace Wallet.DbManager
{
    class CreateDB
    {
        SQLiteConnection cdb = null;

        public void initDB(StorageFolder folder)
        {
            var path = Path.Combine(folder.Path, "db.sqlite");
    
            using (cdb = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path))
            {
                cdb.CreateTable<Event>();
                cdb.CreateTable<Belongs>();
                cdb.CreateTable<Price>();
            }
        }
    }

    [Table("Event")]
    class Event
    {
        [Column("IdEvent"), PrimaryKey, AutoIncrement]
        public int IdEvent { get; set; }
        [Column("Name"), Unique, NotNull, MaxLength(225)]
        public string Name { get; set; }
        [Column("Description"), MaxLength(225)]
        public string Description { get; set; }
    }

    [Table("Belongs")]
    class Belongs
    {
        [ForeignKey(typeof(Event)), Column("IdE")]
        public int IdE { get; set; }
        [ForeignKey(typeof(Price)), Column("IdP")]
        public int IdP { get; set; }
    }

    [Table("Price")]
    class Price
    {
        [Column("IdPrice"), PrimaryKey, AutoIncrement]
        public int IdPrice { get; set; }
        [Column("price"), NotNull, MaxLength(10)]
        public float price { get; set; }
        [Column("date"), NotNull, MaxLength(10)]
        public DateTime date { get; set; }
    }
}
