using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using Wallet.DbManager;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        string simbol = System.Globalization.RegionInfo.CurrentRegion.CurrencySymbol;

        public MainPage()
        {
            this.InitializeComponent();
            /*** Init and Connect DB ***/
            dbFolder = new InitApp.InitFolder().getFolder();
            db = new DbManager.CreateDB(dbFolder);
            opp = new DbManager.OperationsOnDB(db.getConnection());

            /*** Start UI population ***/
            populateActivityList(false);
            populateCostsList("All");
            populateMyBill();
        }

        /*** Hamburger Menu Show / Hide Content ***/

        private void hMenu_Click(object sender, RoutedEventArgs e)
        {
            DesktopShell.IsPaneOpen = !DesktopShell.IsPaneOpen;
        }

        /*** Hamburger Menu Button ***/

        private void addCost_Click(object sender, RoutedEventArgs e)
        {
            List<setActivity> activity = null;

            if (flyAddCost.Visibility == Visibility.Collapsed)
            {
                ObservableCollection<string> act = new ObservableCollection<string>();

                try
                {
                    activity = opp.getActivity();

                    foreach (setActivity a in activity)
                    {
                        act.Add(a.Id);
                    }
                    ComboCost.ItemsSource = act;

                }
                catch (NullReferenceException NRE)
                {

                }
                
                flyAddAccount.Visibility = Visibility.Collapsed;
                flyAddActivity.Visibility = Visibility.Collapsed;
                flyAddCost.Visibility = Visibility.Visible;
            }
            else
            {
                flyAddCost.Visibility = Visibility.Collapsed;
            }
        }

        private void addActivity_Click(object sender, RoutedEventArgs e)
        {
            if (flyAddActivity.Visibility == Visibility.Collapsed)
            {
                flyAddAccount.Visibility = Visibility.Collapsed;
                flyAddCost.Visibility = Visibility.Collapsed;
                flyAddActivity.Visibility = Visibility.Visible;
            }
            else
            {
                flyAddActivity.Visibility = Visibility.Collapsed;
            }
        }

        private void addAccount_Click(object sender, RoutedEventArgs e)
        {
            if (flyAddAccount.Visibility == Visibility.Collapsed)
            {
                flyAddActivity.Visibility = Visibility.Collapsed;
                flyAddCost.Visibility = Visibility.Collapsed;
                flyAddAccount.Visibility = Visibility.Visible;
            }
            else
            {
                flyAddAccount.Visibility = Visibility.Collapsed;
            }
        }

        /*** Add Actions ***/

        private void addCostButton_click(object sender, RoutedEventArgs e)
        {
            float val = 0.0f;
            string date = null;
            DateTime preDate;
            string act = null;
            try
            {
                act = ComboCost.SelectedItem.ToString();

                preDate = costDate.Date.DateTime;
                val = float.Parse(CostValue.Text);

                opp.setCost(act, val, preDate, "Coming Soon");
                populateCostsList(act);
                ActivityList.SelectedItem = act;
                addCost_Click(null, null);
                populateMyBill();
            }
            catch (NullReferenceException NRE)
            {

            }
            
        }

        private void addActivityButton_click(object sender, RoutedEventArgs e)
        {
            string newact = setActivity.Text;

            opp.setActivity(newact , "Coming Soon");
            populateActivityList(true);
            populateCostsList(newact);
            addActivity_Click(null, null);
        }

        private void addAmountButton_click(object sender, RoutedEventArgs e)
        {
            opp.setAccount(float.Parse(amount.Text), amountDate.Date.DateTime, "Coming Soon");
            populateMyBill();
            addAccount_Click(null, null);
        }

        /*** Populate UI ***/

        private void populateActivityList(bool repop)
        {
            List<setActivity> act = null;
            TextBlock txt = null;
            StackPanel item = null;

            try
            {
                act = opp.getActivity();

                ActivityList.Items.Clear();
                ActivityList.Items.Add("All");

                foreach (var a in act)
                {
                    //item = new StackPanel();
                    //item.Orientation = Orientation.Horizontal;

                    //txt = new TextBlock();
                    //txt.Text = a.Id;
                    //Button bt = new Button();
                    //bt.Name = a.Id;

                    //item.Children.Add(txt);
                    //item.Children.Add(bt);
                    
                    ActivityList.Items.Add(item);
                }

                if (repop)
                {
                    ActivityList.SelectedItem = txt;
                }
                else
                {
                    ActivityList.SelectedItem = ActivityList.Items[0];
                }
                
            }
            catch(NullReferenceException NRE)
            {

            }
        }

        private void populateCostsList(string act)
        {
            float total = 0;
            List<Cost> costs = null;

            CostList.Items.Clear();

            try
            {
                costs = opp.getCosts(act);

                foreach (var a in costs)
                {
                    TextBlock it = new TextBlock();
                    total += a.Price;
                    it.Name = a.IdCost.ToString();
                    it.Text = a.ActivityId + " " + a.Price + " " + simbol + " " + a.Date.ToString(System.Globalization.DateTimeFormatInfo.CurrentInfo);
                    CostList.Items.Add(it);
                }
            }
            catch(NullReferenceException NRE)
            {
                Debug.WriteLine(NRE.Message);
            }
            Partial.Text = act + ": " + total.ToString() + simbol;
        }


        private void populateMyBill()
        {
            float bill = 0;
            float cost = 0;
            List<Account> accounts = null;
            List<Cost> costs = null;

            try
            {
                accounts = opp.getAccounts();
                costs = opp.getCosts("All");

                foreach (var a in accounts)
                {
                    bill += a.Amount;
                }

                foreach (var a in costs)
                {
                    cost += a.Price;
                }
            }
            catch(NullReferenceException NRE)
            {

            }

            MyBill.Text = "Bill: " + (bill - cost).ToString() + simbol;
        }

        /*** Select Item from ListView Operations ***/

        private void activityCostClick(object sender, ItemClickEventArgs e)
        {
            string act = "All";

            TextBlock blk = e.ClickedItem as TextBlock;

            if (blk != null){
                act = blk.Name;
            }

            populateCostsList(act);
        }

        private void selectedDate_Click(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {

        }
    }
}
