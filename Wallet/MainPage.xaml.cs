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
using Wallet.DbManager;
using System.Globalization;
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

            DateTime date = DateTime.Now;
            opp.setCost("Scontrino", 20, date , "Prova");
            populateCostList();
            //opp.setActivity("Biglietto", "Descrizione di prova2");
            //opp.removeActivity("Biglietto");

        }

        private void hMenu_Click(object sender, RoutedEventArgs e)
        {
            FirstShell.IsPaneOpen = !FirstShell.IsPaneOpen;
        }

        private void addCost_Click(object sender, RoutedEventArgs e)
        {
            //opp.setCost();
        }

        private void addActivity_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addAccount_Click(object sender, RoutedEventArgs e)
        {

        }

        private void populateCostList()
        {
           float total = 0;
           var simbol = System.Globalization.RegionInfo.CurrentRegion.CurrencySymbol;

            List<Cost> act = opp.getCost();
            foreach(var a in act)
            {
                total += a.Price;
                String ls = a.ActivityId + " " + a.Price + " " + simbol + " " + a.Date.ToString(System.Globalization.DateTimeFormatInfo.CurrentInfo);
                CostList.Items.Add(ls);
            }
            AccountText.Text = total.ToString();

        }

    }
}
