using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Wallet.InitApp
{
    class InitFF
    {
        private StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
        private StorageFolder appFolder;

        private String main_folder = "wallet";
        private String db_file = "dbw.db";

        private dbManager.DBManager manager = null;

        public String error = null;

        public InitFF()
        {
            createFF();
        }

        public async Task<StorageFile> getDB() {
            return await appFolder.GetFileAsync(db_file);
        }

        //Create app folder and file into AppData

        private async Task<bool> createFF()
        {
            try
            {
                storageFolder.CreateFolderAsync(main_folder);
                appFolder = await storageFolder.GetFolderAsync(main_folder);
                appFolder.CreateFileAsync(db_file);
            }
            catch (System.IO.FileNotFoundException fn)
            {
                error = fn.Message;
                return false;
            }
            catch (System.UnauthorizedAccessException ua)
            {
                error = ua.Message;
                return false;
            }
            catch (System.ArgumentException ae)
            {
                error = ae.Message;
                return false;
            }

            return true;
        }
    }
}
