using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using System.Threading.Tasks;

// Il modello di elemento per la pagina vuota è documentato all'indirizzo http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x410

namespace Wallet
{
    /// <summary>
    /// Pagina vuota che può essere utilizzata autonomamente oppure esplorata all'interno di un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
        private StorageFolder appFolder;

        private String main_folder = "wallet";
        private String db_file = "fist";

        public String error = null;

        public MainPage()
        {
            this.InitializeComponent();
            InitAppFile();
        }

        async Task<bool> InitAppFile()
        {
            //Create app folder and file into AppData
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
