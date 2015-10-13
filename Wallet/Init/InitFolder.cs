using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Wallet.InitApp
{
    class InitFolder
    {
        private StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
        private StorageFolder dataFolder = null;

        private String main_folder = "data_wallet";

        public String error = null;

        public InitFolder()
        {
            setFolder();
        }

        //Create folder into AppData for database

        private async Task<bool> setFolder()
        {
            try
            {
                storageFolder.CreateFolderAsync(main_folder);
                dataFolder = await storageFolder.GetFolderAsync(main_folder);
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

        public StorageFolder getFolder()
        {
            return dataFolder;
        }
    }
}
