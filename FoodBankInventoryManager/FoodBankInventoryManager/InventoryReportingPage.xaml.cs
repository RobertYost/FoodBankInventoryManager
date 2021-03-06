﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Configuration;

namespace FoodBankInventoryManager
{
    /// <summary>
    /// Interaction logic for StatisticsPage.xaml
    /// </summary>
    public partial class InventoryReportingPage : Page
    {
        private L2S_FoodBankDBDataContext dbContext;
        private User myCurrentUser;
        List<InventoryInfo> currentInventory;
        List<MinWatchInfo> watchList;
        private DateTime lastKeyPress;
        private List<string> textStream;

        private const string APPLICATION_NAME = "INVENTORY_TRACKER";
        public InventoryReportingPage(User aUser)
        {
            InitializeComponent();
            txtHidden.Focus();
            myCurrentUser = aUser;
            lastKeyPress = new DateTime(0);
            textStream = new List<string>();
            currentInventory = new List<InventoryInfo>();
            watchList = new List<MinWatchInfo>();
            dbContext = new L2S_FoodBankDBDataContext(ConfigurationManager.ConnectionStrings["FoodBankInventoryManager.Properties.Settings.FoodBankDBConnectionString"].ConnectionString);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDataGrids();
            Application.Current.MainWindow.KeyDown += new KeyEventHandler(Page_KeyDown);
        }
        /// <summary>
        /// Navigates back to the homepage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            HomePage h = new HomePage(myCurrentUser);
            NavigationService.Navigate(h);
        }
        /// <summary>
        /// Retrieves current inventory
        /// </summary>
        /// <returns></returns>
        private List<InventoryInfo> getCurrentInventory()
        {
            return (from items in dbContext.GetTable<InventoryEntry>()
                    select new InventoryInfo
                    {
                        FoodName = items.FoodName,
                        DateEntered = items.DateEntered,
                        BinId = string.Join(", ", (from bins in dbContext.GetTable<InventoryEntry>() //grabs all bins food is in and puts them in a comma seperated string
                                                   where bins.FoodName == items.FoodName
                                                   select bins.BinId).Distinct().ToList()),
                        ShelfId = string.Join(", ", (from shelves in dbContext.GetTable<InventoryEntry>() //grabs all shelves that food is on and puts them in a comma seperated string
                                                     where shelves.FoodName == items.FoodName
                                                     select shelves.ShelfId).ToList().Distinct()),
                        Quantity = (from foods in dbContext.GetTable<Food>()
                                    where foods.FoodName == items.FoodName
                                    select foods.Quantity).First()
                    }).GroupBy(i => i.FoodName).Select(g => g.First()).ToList(); //Groups inventory entries by food name
        }
        /// <summary>
        /// Retrieves current minimum watch list
        /// </summary>
        /// <returns></returns>
        private List<MinWatchInfo> getCurrentMinWatchList()
        {
            List<Food> belowThreshold = (from foods in dbContext.GetTable<Food>() //Grabs items below their minimum threshold
                                         where foods.Quantity < foods.MinimumQty
                                         select foods).ToList();
            return (from items in dbContext.GetTable<InventoryEntry>() //Grabs inventory entries that contain foods below their minimum threshold
                    where belowThreshold.Contains((from foods in dbContext.GetTable<Food>()
                                                   where foods.FoodName == items.FoodName
                                                   select foods).First())
                    select new MinWatchInfo
                    {
                        FoodName = items.FoodName,
                        CurrentQuantity = (from foods in dbContext.GetTable<Food>()
                                           where foods.FoodName == items.FoodName
                                           select foods.Quantity).First(),
                        MinThreshold = (from foods in dbContext.GetTable<Food>()
                                        where foods.FoodName == items.FoodName
                                        select foods.MinimumQty).First()
                    }).ToList();
        }
        private void RowContMenuDel_Click(object sender, RoutedEventArgs e)
        {
            //If a food is in more than one bin, a special window will pop up allowing user to delete each individual entry
            if ((from entries in dbContext.GetTable<InventoryEntry>()
                 where entries.FoodName == ((InventoryInfo)gridItems.SelectedValue).FoodName
                 select entries)
                 .ToList().Count > 1)
            {
                DeletionManagementWindow d = new DeletionManagementWindow(myCurrentUser, (InventoryInfo)gridItems.SelectedValue, false);
                d.ShowInTaskbar = false;
                d.Owner = Application.Current.MainWindow;
                d.ShowDialog();
                UpdateDataGrids();
            }
            else
            {
                if (myCurrentUser.AccessLevel == 0)
                {
                    try
                    {
                        if (sender != null)
                        {
                            InventoryInfo selectedItem = ((InventoryInfo)gridItems.SelectedValue);

                            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this row?", "Food Bank Manager", MessageBoxButton.YesNo);

                            if (result == MessageBoxResult.Yes)
                            {
                                if (!string.IsNullOrEmpty(selectedItem.FoodName))
                                {
                                    InventoryEntry entryToDelete = (from items in dbContext.GetTable<InventoryEntry>()
                                                                    where items.FoodName == selectedItem.FoodName
                                                                    select items).First<InventoryEntry>();

                                    Food associatedFoodItem = (from items in dbContext.GetTable<Food>()
                                                               where items.FoodName == entryToDelete.FoodName
                                                               select items).First();

                                    associatedFoodItem.Quantity -= entryToDelete.ItemQty;

                                    AuditEntry auditRecord = new AuditEntry();
                                    auditRecord.Action = "DELETION";
                                    auditRecord.ApplicationName = APPLICATION_NAME;
                                    auditRecord.BinId = entryToDelete.BinId;
                                    auditRecord.ItemQty = -entryToDelete.ItemQty;
                                    auditRecord.Date_Action_Occured = DateTime.Now;
                                    auditRecord.FoodName = entryToDelete.FoodName;
                                    auditRecord.ShelfId = entryToDelete.ShelfId;
                                    auditRecord.UserName = myCurrentUser.LastName + ", " + myCurrentUser.FirstName;
                                    switch (myCurrentUser.AccessLevel)
                                    {
                                        case 0:
                                            auditRecord.AccessLevel = "Administrator";
                                            break;
                                        case 1:
                                            auditRecord.AccessLevel = "Standard User";
                                            break;
                                        default:
                                            break;
                                    }
                                    dbContext.InventoryEntries.DeleteOnSubmit(entryToDelete);
                                    dbContext.AuditEntries.InsertOnSubmit(auditRecord);
                                    dbContext.SubmitChanges();
                                }
                                currentInventory.Remove((InventoryInfo)selectedItem);
                                UpdateDataGrids();
                                
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        MessageBox.Show("Item unable to be deleted at this time", "Food Bank Manager Error System");
                        return;

                    }
                }
                else
                {
                    return;
                }
            }
        }

        /// <summary>
        /// When the database is modified, the two datagrids on this page are updated
        /// </summary>
        private void UpdateDataGrids()
        {
            currentInventory = getCurrentInventory();
            gridItems.ItemsSource = currentInventory;
            gridItems.Items.Refresh();
            gridMinWatch.ItemsSource = getCurrentMinWatchList();
            gridMinWatch.Items.Refresh();
        }
        /// <summary>
        /// When user double clicks, display inventory entry info in an easier to see messagebox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;

                InventoryInfo selectedRow = (InventoryInfo)grid.SelectedItem;
                MessageBox.Show(
                    $"Item Name: {selectedRow.FoodName}\nBins item is in: {selectedRow.BinId}\nShelves item is on: {selectedRow.ShelfId}\nQuantity: {selectedRow.Quantity}\nDate Entered: {selectedRow.DateEntered}", "Entry Info");
            }
            else
            {
                MessageBox.Show("Please click on an inventory entry item");
                throw new NullReferenceException();
            }
        }

        /// <summary>
        /// When clicked, exports current inventory to an excel spreadsheet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            ExcelExporter<ReportEntry, ReportEntries> exporter =
                new ExcelExporter<ReportEntry, ReportEntries>();
            if (getCurrentInventory().Count > 0)
            {
                exporter.dataToPrint = (from entries in dbContext.GetTable<InventoryEntry>()
                                        select new ReportEntry
                                        {
                                            Shelf = entries.ShelfId,
                                            Bin = entries.BinId,
                                            Recorded_Food = entries.FoodName,
                                            Actual_Food = "",
                                            Recorded_Quantity = entries.ItemQty,
                                            Actual_Quantity = ""
                                        }).ToList();
                exporter.GenerateReport();
            }
            else
            {
                MessageBox.Show("Nothing is currently in inventory", "Inventory Manager Error System");
                return;
            }
            
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            TimeSpan elasped = DateTime.Now - lastKeyPress;
            if (elasped.TotalMilliseconds > 100)
            {
                textStream.Clear();
            }
            if ((e.Key >= Key.D0) && (e.Key <= Key.D9))
            {
                textStream.Add(e.Key.ToString()[1].ToString());
            }
            else if (e.Key == Key.LeftShift)
            {
                return;
            }
            else if (e.Key == Key.Space)
            {
                textStream.Add(" ");
            }
            else
            {
                textStream.Add(e.Key.ToString());
            }
            lastKeyPress = DateTime.Now;
            if (e.Key == Key.Tab && textStream.Count > 1)
            {
                string barcodeData = string.Join("", textStream).TrimEnd();
                barcodeData = barcodeData.Substring(0, barcodeData.IndexOf("Tab", StringComparison.Ordinal));
                
                string nums = "0123456789";
                
                if (barcodeData[0] == 'B' && nums.Contains(barcodeData[1]))
                {
                    List<InventoryInfo> scannedEntry = (from entries in dbContext.GetTable<InventoryEntry>()
                    where entries.FoodName == (from items in dbContext.GetTable<InventoryEntry>()
                                               where items.BinId == barcodeData
                                               select items.FoodName).First()
                    select new InventoryInfo
                    {
                        FoodName = entries.FoodName,
                        DateEntered = entries.DateEntered,
                        BinId = entries.BinId,
                        ShelfId = entries.ShelfId,
                        Quantity = entries.ItemQty
                    }).ToList();
                    if (scannedEntry.Count > 1)
                    {
                        DeletionManagementWindow d = new DeletionManagementWindow(myCurrentUser, scannedEntry.First(), true);
                        d.ShowInTaskbar = false;
                        d.Owner = Application.Current.MainWindow;
                        d.ShowDialog();
                        UpdateDataGrids();
                    }
                    else
                    {
                        if (myCurrentUser.AccessLevel == 0)
                        {
                            try
                            {
                                if (sender != null)
                                {
                                    InventoryInfo selectedItem = scannedEntry.First();

                                    MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this row?", "Food Bank Manager", MessageBoxButton.YesNo);

                                    if (result == MessageBoxResult.Yes)
                                    {
                                        if (!string.IsNullOrEmpty(selectedItem.FoodName))
                                        {
                                            InventoryEntry entryToDelete = (from items in dbContext.GetTable<InventoryEntry>()
                                                                            where items.FoodName == selectedItem.FoodName
                                                                            select items).First<InventoryEntry>();

                                            Food associatedFoodItem = (from items in dbContext.GetTable<Food>()
                                                                       where items.FoodName == entryToDelete.FoodName
                                                                       select items).First();

                                            associatedFoodItem.Quantity -= entryToDelete.ItemQty;

                                            AuditEntry auditRecord = new AuditEntry();
                                            auditRecord.Action = "DELETION";
                                            auditRecord.ApplicationName = APPLICATION_NAME;
                                            auditRecord.BinId = entryToDelete.BinId;
                                            auditRecord.ItemQty = -entryToDelete.ItemQty;
                                            auditRecord.Date_Action_Occured = DateTime.Now;
                                            auditRecord.FoodName = entryToDelete.FoodName;
                                            auditRecord.ShelfId = entryToDelete.ShelfId;
                                            auditRecord.UserName = myCurrentUser.LastName + ", " + myCurrentUser.FirstName;
                                            switch (myCurrentUser.AccessLevel)
                                            {
                                                case 0:
                                                    auditRecord.AccessLevel = "Administrator";
                                                    break;
                                                case 1:
                                                    auditRecord.AccessLevel = "Standard User";
                                                    break;
                                                default:
                                                    break;
                                            }
                                            dbContext.InventoryEntries.DeleteOnSubmit(entryToDelete);
                                            dbContext.AuditEntries.InsertOnSubmit(auditRecord);
                                            dbContext.SubmitChanges();
                                        }
                                        currentInventory.Remove((InventoryInfo)selectedItem);
                                        UpdateDataGrids();

                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                                MessageBox.Show("Item unable to be deleted at this time", "Food Bank Manager Error System");
                                return;

                            }
                        }
                        else
                        {
                            MessageBox.Show("Sorry, you do not have the right permission level to remove this item.");
                            return;
                        }
                    }
                }
            }
        }
    }

    public class ReportEntries : List<ReportEntry> { }

    public class ReportEntry
    {
        public string Shelf
        {
            get; set;
        }
        public string Bin
        {
            get; set;
        }
        public string Recorded_Food
        {
            get; set;
        }
        public string Actual_Food
        {
            get; set;
        }
        public int Recorded_Quantity
        {
            get; set;
        }
        public string Actual_Quantity
        {
            get; set;
        }
    }
    public class InventoryInfo
    {
        public string FoodName
        {
            get; set;
        }
        public DateTime DateEntered
        {
            get; set;
        }
        public string BinId
        {
            get; set;
        }
        public string ShelfId
        {
            get; set;
        }
        public int Quantity
        {
            get; set;
        }
    }
    public class MinWatchInfo
    {
        public string FoodName
        {
            get; set;
        }
        public int CurrentQuantity
        {
            get; set;
        }
        public int MinThreshold
        {
            get; set;
        }
    }
}
