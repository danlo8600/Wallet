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
        private InitApp.InitFF app = null;
        private StorageFile db = null;
        private dbManager.DBManager manager = null;

        public MainPage()
        {
            this.InitializeComponent();
            start();
            //Remove this code
            dbManager.Product pd = new dbManager.Product();
            pd.setName("monnezza");
            manager.writeDB(DateTime.Today, pd, 10.50F);
            //Remove this code
        }

        private async Task<bool> start()
        {
            app = new InitApp.InitFF();
            db = await app.getDB();
            manager = new dbManager.DBManager(db);
            return true;
        }
    }
}
