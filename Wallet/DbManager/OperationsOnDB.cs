using System;
using SQLite.Net;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace Wallet.DbManager
{
    class OperationsOnDB
    {
        SQLiteConnection db = null;
        Exception succes = null;
        Exception fail = null;

        public OperationsOnDB(SQLiteConnection conn)
        {
            db = conn;
            succes = new Exception("true");
            fail = new Exception("false");
        }

        /************************************************/
        /*** This code manage query on Activity table ***/
        /************************************************/

        //Insert new Activity
        public Exception setActivity(string activityId, string description)
        {
            if (!findActivity(activityId))
            {
                try
                {
                    var s = db.Insert(new Activity()
                    {
                        Id = activityId,
                        Description = description
                    });
                    return succes;
                }
                catch (System.NullReferenceException SYSEX)
                {
                    return SYSEX;
                }
                catch (SQLite.Net.SQLiteException SQLEX)
                {
                    return SQLEX;
                }
            }
            else
            {
                return fail;
            }
        }

        //Find if Activity exist
        public bool findActivity(string activityId)
        {
            try
            {
                var res = db.Query<Activity>("select * from Activity where Id = " + "'" + activityId + "'");

                if(res.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(NullReferenceException NREEX)
            {
                Debug.WriteLine("ERRORE:" + NREEX.Message);
            }
            catch(SQLite.Net.SQLiteException SQLEX)
            {
                Debug.WriteLine(SQLEX.Message);
            }

            return false;
        }

        //Get data from all exsisting activities
        public List<Activity> getActivity()
        {
            var res = db.Query<Activity>("select * from Activity");
            return res;
        }

        //Remove an exsisting Activity
        public bool removeActivity(string activityId)
        {
            if (findActivity(activityId))
            {
                db.Delete(new Activity(){
                    Id = activityId
                });
                return true;
            }
            else return false;
        }

        /********************************************/
        /*** This code manage query on Cost table ***/
        /********************************************/

        //Insert new Cost
        public void setCost(float value, DateTime date, int eventKey)
        {
            //Devo recuperare l'ultima chiave usata
        }

        //Find if Cost exist
        public bool findnCost(int idCost)
        {
            try
            {
                var res = db.Query<Cost>("select * from Cost where IdCost = " + "'" + idCost + "'");

                if (res.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (NullReferenceException NREEX)
            {
                Debug.WriteLine("ERRORE:" + NREEX.Message);
            }
            catch (SQLite.Net.SQLiteException SQLEX)
            {
                Debug.WriteLine(SQLEX.Message);
            }

            return false;
        }

        //Get all data from exsisting Cost
        public void getCost()
        {

        }

        //Remove an exsisting Cost
        public bool removeCost()
        {
            return true;
        }

        //Insert new Account
        public void setAccount()
        {
            //Devo recuperare l'ultima chiave usata
        }

        //Find if Account exist
        public bool findAccount()
        {
            return true;
        }

        //Get all data from exsisting Account
        public void getBAccounts()
        {

        }

        //Remove an exsisting Account
        public bool removeAccount()
        {
            return true;
        }
    }
}
