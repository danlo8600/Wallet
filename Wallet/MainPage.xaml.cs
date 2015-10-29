using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using Wallet.DbManager;
using System.Collections.ObjectModel;
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
            populateActivityList();
            populateCostsList("All");
            populateMyBill();
        }

        /*** Hamburger Menu Show / Hide Content ***/

        private void hMenu_Click(object sender, RoutedEventArgs e)
        {
            FirstShell.IsPaneOpen = !FirstShell.IsPaneOpen;
            if (!FirstShell.IsPaneOpen)
            {
                Calendar.Visibility = Visibility.Collapsed;
            }
            else
            {
                Calendar.Visibility = Visibility.Visible;
            }
        }

        /*** Hamburger Menu Button ***/

        private void addCost_Click(object sender, RoutedEventArgs e)
        {

            if (flyAddCost.Visibility == Visibility.Collapsed)
            {
                ObservableCollection<string> act = new ObservableCollection<string>();

                foreach (var a in opp.getActivity())
                {
                    act.Add(a.Id);
                }
                ComboCost.ItemsSource = act;

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
            string act = ComboCost.SelectedItem.ToString();

            preDate = costDate.Date.DateTime;
            val = float.Parse(CostValue.Text);

            opp.setCost(act, val, preDate, "Coming Soon");
            populateCostsList(act);
            ActivityList.SelectedItem = act;
            addCost_Click(null, null);
            populateMyBill();
        }

        private void addActivityButton_click(object sender, RoutedEventArgs e)
        {
            string newact = Activity.Text;

            opp.setActivity(newact , "Coming Soon");
            populateActivityList();
            ActivityList.SelectedItem = newact;
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

        private void populateActivityList()
        {
            List<Activity> act = null;

            try
            {
                act = opp.getActivity();

                ActivityList.Items.Clear();
                ActivityList.Items.Add("All");

                foreach (var a in act)
                {
                    ActivityList.Items.Add(a.Id);
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

            costs = opp.getCosts(act);

            foreach (var a in costs)
            {
                total += a.Price;
                String ls = a.ActivityId + " " + a.Price + " " + simbol + " " + a.Date.ToString(System.Globalization.DateTimeFormatInfo.CurrentInfo);
                CostList.Items.Add(ls);
            }
            Partial.Text = act + ": " + total.ToString() + simbol;
        }


        private void populateMyBill()
        {
            float bill = 0;
            float cost = 0;

            var accounts = opp.getAccounts();
            var costs = opp.getCosts("All");

            foreach(var a in accounts)
            {
                bill += a.Amount;
            }

            foreach(var a in costs)
            {
                cost += a.Price;
            }

            MyBill.Text = "Bill: " + (bill - cost).ToString() + simbol;
        }

        /*** Select Item from ListView Operations ***/

        private void activityCostClick(object sender, ItemClickEventArgs e)
        {
            string act = null;

            if (e.ClickedItem is Windows.UI.Xaml.Controls.TextBlock)
            {
                act = "All"; 
            }
            else
            {
                act = e.ClickedItem.ToString();
            }
            populateCostsList(act);
        }

        private void selectedDate_Click(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {

        }
    }
}
