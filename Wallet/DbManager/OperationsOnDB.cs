using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;

namespace Wallet.DbManager
{
    class OperationsOnDB
    {
        SQLiteConnection db = null;

        public OperationsOnDB(SQLiteConnection conn)
        {
            db = conn;
            setEvent("Scontrino", "Scontrino supermercato");
        }

        public void setEvent(string eventName, string description)
        {
            try
            { 
                var s = db.Insert(new Event()
                {
                    IdEvent = 1,
                    Name = eventName,
                    Description = description
                });
            }
            catch (System.NullReferenceException ex)
            {
                
            }
            //Devo recuperare l'ultima chiave usata
        }

        public void setPrice(float value, DateTime date, int eventKey)
        {
            //Devo recuperare l'ultima chiave usata
        }
    }
}
