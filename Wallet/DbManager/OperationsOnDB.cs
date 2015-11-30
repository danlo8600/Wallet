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
                    var s = db.Insert(new setActivity()
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
                var res = db.Query<setActivity>("select * from Activity where Id = " + "'" + activityId + "'");

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
        public List<setActivity> getActivity()
        {
            var res = db.Query<setActivity>("select * from Activity");
            return res;
        }

        //Remove an exsisting Activity
        public bool removeActivity(string activityId)
        {
            if (findActivity(activityId))
            {
                db.Delete(new setActivity(){
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
        public Exception setCost(string activityId, float value, DateTime date,  string description)
        {
            try
            {
                var s = db.Insert(new Cost()
                {
                    ActivityId = activityId,
                    Date = date,
                    Description = description,
                    Price = value
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

        //Find Costs of a specifc date
        public List<Cost> findCosts(DateTime startDate, DateTime endDate)
        {
            var res = db.Query<Cost>("select * from cost where Date >= " + "'" + startDate + "'" + " and " + "Date <= " + "'" + endDate + "'");
            return res;
        }

        //Get all data from exsisting Cost
        public List<Cost> getCosts(string act)
        {
            try
            {
                if (act == "All")
                {
                    var res = db.Query<Cost>("select * from Cost");
                    return res;
                }
                else
                {
                    var res = db.Query<Cost>("select * from Cost where ActivityId = " + "'" + act + "'");
                    return res;
                }
            }
            catch(NullReferenceException NRE)
            {
                return null;
            }
        }

        //Remove an exsisting Cost
        public bool removeCost(int costId)
        {
            db.Delete(new Cost()
            {
                IdCost = costId
            });
            return true;
        }

        /***********************************************/
        /*** This code manage query on Account table ***/
        /***********************************************/

        //Insert new Account
        public Exception setAccount(float amount, DateTime date, string description)
        {
            try
            {
                var s = db.Insert(new Account()
                {
                    Amount = amount,
                    Date = date, 
                    Description = description,
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

        //Find Accounts of a specifc date
        public List<Account> findAccounts(DateTime startDate, DateTime endDate)
        {
            var res = db.Query<Account>("select * from cost where Date >= " + "'" + startDate + "'" + " and " + "Date <= " + "'" + endDate + "'");
            return res;
        }

        //Get all data from exsisting Account
        public List<Account> getAccounts()
        {
            var res = db.Query<Account>("select * from Account");
            return res;
        }

        //Remove an exsisting Account
        public bool removeAccount(int accountId)
        {
            db.Delete(new Account()
            {
                IdAccount = accountId
            });
            return true;
        }
    }
}
