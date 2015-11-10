using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace Wallet.InitApp
{
    class InitFolder
    {
        private StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
        private StorageFolder dataFolder = null;

        private String main_folder = "data_wallet";

        public InitFolder()
        {
            setFolder();
        }

        //Create folder into AppData for database

        private async Task<Exception> setFolder()
        {
            try
            {
                /*** WARNING Don't add await in front of this line of code, the DB stop to work ***/
                storageFolder.CreateFolderAsync(main_folder);
                dataFolder = await storageFolder.GetFolderAsync(main_folder);
                return new Exception("true");
            }
            catch (System.IO.FileNotFoundException FNFEX)
            {
                return FNFEX;
            }
            catch (System.UnauthorizedAccessException UAEX)
            {
                return UAEX;
            }
            catch (System.ArgumentException AEEX)
            {
                return AEEX;
            }

        }
        //Return the database folder
        public StorageFolder getFolder()
        {
            return dataFolder;
        }
    }
}
