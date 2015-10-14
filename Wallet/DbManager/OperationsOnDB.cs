using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;
using SQLiteNetExtensions;

namespace Wallet.DbManager
{
    class OperationsOnDB
    {
        SQLiteConnection db = null;

        public OperationsOnDB(SQLiteConnection conn)
        {
            db = conn;
        }

        public void setEvent(string eventName, string description)
        {
            //Devo recuperare l'ultima chiave usata
        }

        public void setPrice(float value, DateTime date, int eventKey)
        {
            //Devo recuperare l'ultima chiave usata
        }
    }
}
