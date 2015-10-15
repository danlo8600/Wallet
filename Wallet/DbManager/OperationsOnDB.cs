using System;
using SQLite.Net;

namespace Wallet.DbManager
{
    class OperationsOnDB
    {
        SQLiteConnection db = null;

        public OperationsOnDB(SQLiteConnection conn)
        {
            db = conn;
        }

        //Insert new Event
        public Exception setEvent(string eventName, string description)
        {
            //Devo controllare se l'evento è già presente
            try
            {
                var s = db.Insert(new Activity()
                {
                    Name = eventName,
                    Description = description
                });
                return new Exception("true");
            }
            catch (System.NullReferenceException SYSEX)
            {
                return SYSEX;
            }catch(SQLite.Net.SQLiteException SQLEX)
            {
                return SQLEX;
            }
        }

        //Find if Event exist
        public bool returnEvent()
        {
            return true;
        }

        //Get data from exsisting Event
        public void getEvent()
        {

        }

        //Remove an exsisting Event
        public bool removeEvent()
        {
            return true;
        }

        //Insert new Price
        public void setPrice(float value, DateTime date, int eventKey)
        {
            //Devo recuperare l'ultima chiave usata
        }

        //Find if price exist
        public bool returnPrice()
        {
            return true;
        }

        //Get data from exsisting Price
        public void getPrice()
        {

        }

        //Remove an exsisting Price
        public bool removePrice()
        {
            return true;
        }
    }
}
