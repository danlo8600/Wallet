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
// Il modello di elemento per la pagina vuota è documentato all'indirizzo http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x410

namespace Wallet
{
    /// <summary>
    /// Pagina vuota che può essere utilizzata autonomamente oppure esplorata all'interno di un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private StorageFolder dbFolder = null;
        private DbManager.CreateDB db = null;
        private DbManager.OperationsOnDB opp = null;

        public MainPage()
        {
            this.InitializeComponent();
            dbFolder = new InitApp.InitFolder().getFolder();
            db = new DbManager.CreateDB(dbFolder);
            opp = new DbManager.OperationsOnDB(db.getConnection());
            //opp.setActivity("Biglietto", "Descrizione di prova2");
            //opp.removeActivity("Biglietto");
            
        }
    }
}
