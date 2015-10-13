using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Windows.Storage;

namespace Wallet.dbManager
{
    class DBManager
    {
        private StorageFile db = null;

        public DBManager(StorageFile db)
        {
            this.db = db;
        }

        public async Task<bool> writeDB(DateTime data, Product pr, float price)
        {
            String std = data.ToString("dd-MM-yyyy");
            String product = pr.getName();
            await FileIO.AppendTextAsync(db, "##" + std + "#" + product + "#" + price + "###" + Environment.NewLine);
            return true;
        }

        public void readDB()
        {
            //TODO
        }
    }
}
