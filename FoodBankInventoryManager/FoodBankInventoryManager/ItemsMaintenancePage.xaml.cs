﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Configuration;

namespace FoodBankInventoryManager
{
    /// <summary>
    /// Interaction logic for ItemMaintenance.xaml
    /// </summary>
    public partial class ItemsMaintenancePage : Page
    {
        #region Instance variables
        private User myCurrentUser;
        private L2S_FoodBankDBDataContext dbContext;

        private List<FoodInfo> allFoods;
        private List<BinInfo> allBins;
        private List<ShelfInfo> allShelves;
        #endregion

        public ItemsMaintenancePage(User aUser)
        {
            myCurrentUser = aUser;
            dbContext = new L2S_FoodBankDBDataContext(ConfigurationManager.ConnectionStrings["FoodBankInventoryManager.Properties.Settings.FoodBankDBConnectionString"].ConnectionString);

            InitializeComponent();
        }

        #region Methods to retrieve all foods, bins, and shelves from database
        private List<FoodInfo> GetFoods()
        {
            return (from foods in dbContext.GetTable<Food>()
                    select new FoodInfo
                    {
                        FoodName = foods.FoodName,
                        MinimumQty = foods.MinimumQty
                    }).ToList();
        }

        private List<BinInfo> GetBins()
        {
            return (from bins in dbContext.GetTable<Bin>()
                    select new BinInfo
                    {
                        BinId = bins.BinId
                    }).ToList();
        }

        private List<ShelfInfo> GetShelves()
        {
            return (from shelves in dbContext.GetTable<Shelf>()
                    select new ShelfInfo
                    {
                        ShelfId = shelves.ShelfId
                    }).ToList();
        }
        #endregion
        #region Context Menu Event Handlers
        #region Context Menu Delete Event Handlers
        /// <summary>
        /// Deletes a shelf from the database, as long as it doesn't have any bins on it with food in it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RowContMenuDelShelf_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                ShelfInfo selectedItem = (ShelfInfo)dgridShelf.SelectedValue;
                List<InventoryEntry> matchingShelves = (from shelves in dbContext.GetTable<InventoryEntry>() //checks to see if there are any
                                                        where shelves.ShelfId == selectedItem.ShelfId           //shelves that have bins with food in them on it
                                                        select shelves).ToList();
                if (matchingShelves.Count != 0)
                {
                    MessageBox.Show("There are inventory entries containing " + selectedItem.ShelfId + ". To prevent " +
                        "unintentional data loss please delete those entries before deleting this item.", "Unable to Delete", MessageBoxButton.OK);
                }
                else
                {
                    allShelves.Remove(selectedItem); //Removes shelf from database and datagrid
                    dgridShelf.ItemsSource = allShelves;
                    dgridShelf.Items.Refresh();
                    Shelf shelfToBeRemoved = (from shelves in dbContext.GetTable<Shelf>()
                                              where shelves.ShelfId == selectedItem.ShelfId
                                              select shelves).First();
                    dbContext.Shelfs.DeleteOnSubmit(shelfToBeRemoved);
                    dbContext.SubmitChanges();
                }
            }
        }

        private void RowContMenuDelBin_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                BinInfo selectedItem = (BinInfo)dgridBin.SelectedValue;
                List<InventoryEntry> matchingBins = (from bins in dbContext.GetTable<InventoryEntry>() //checks to see if there are any
                                                     where bins.BinId == selectedItem.BinId           //inventory entries with the selected food in them
                                                     select bins).ToList();
                if (matchingBins.Count != 0)
                {
                    MessageBox.Show("There are inventory entries containing " + selectedItem.BinId + ". To prevent " +
                        "unintentional data loss please delete those entries before deleting this item.", "Unable to Delete", MessageBoxButton.OK);
                }
                else
                {
                    allBins.Remove(selectedItem); //removes food item from database and datagrid
                    dgridBin.ItemsSource = allBins;
                    dgridBin.Items.Refresh();
                    Bin binToBeRemoved = (from bins in dbContext.GetTable<Bin>()
                                          where bins.BinId == selectedItem.BinId
                                          select bins).First();
                    dbContext.Bins.DeleteOnSubmit(binToBeRemoved);
                    dbContext.SubmitChanges();
                }
            }
        }
        /// <summary>
        /// Deletes desired food
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RowContMenuDel_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                FoodInfo selectedItem = (FoodInfo)dgridFood.SelectedValue;
                List<InventoryEntry> matchingFoods = (from foods in dbContext.GetTable<InventoryEntry>() //Checks to see if there are any inventory entries
                                                      where foods.FoodName == selectedItem.FoodName                //containing the food the user clicked
                                                      select foods).ToList();
                if (matchingFoods.Count != 0)
                {
                    MessageBox.Show("There are inventory entries containing " + selectedItem.FoodName + ". To prevent " +
                        "unintentional data loss please delete those entries before deleting this item.", "Unable to Delete", MessageBoxButton.OK);
                }
                else
                {
                    allFoods.Remove(selectedItem); //Removes item from database and datagrid
                    dgridFood.ItemsSource = allFoods;
                    dgridFood.Items.Refresh();
                    Food foodToBeRemoved = (from foods in dbContext.GetTable<Food>()
                                            where foods.FoodName == selectedItem.FoodName
                                            select foods).First();
                    dbContext.Foods.DeleteOnSubmit(foodToBeRemoved);
                    dbContext.SubmitChanges();
                }
            }
        }
        #endregion
        /// <summary>
        /// Opens food modification window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RowContMenuMod_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                FoodInfo selectedItem = (FoodInfo)dgridFood.SelectedValue;
                ModifyFoodWindow m = new ModifyFoodWindow(selectedItem.FoodName, selectedItem.MinimumQty)
                {
                    Owner = Application.Current.MainWindow
                };
                m.ShowDialog();
                allFoods = GetFoods();
                dgridFood.ItemsSource = allFoods;
                dgridFood.Items.Refresh();
            }
        }
        #endregion
        #region Misc. Event Handlers
        /// <summary>
        /// Grabs all of the foods, bins, and shelves in the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            allFoods = GetFoods();
            dgridFood.ItemsSource = allFoods;

            allBins = GetBins();
            dgridBin.ItemsSource = allBins;

            allShelves = GetShelves();
            dgridShelf.ItemsSource = allShelves;
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            HomePage h = new HomePage(myCurrentUser);
            NavigationService.Navigate(h);
        }
        #endregion
    }
    #region Quasi-anonymous classes to be used when loading data into datagrid
    public class FoodInfo
    {
        public string FoodName
        {
            get; set;
        }
        public int MinimumQty
        {
            get; set;
        }
    }
    public class BinInfo
    {
        public string BinId
        {
            get; set;
        }
    }
    public class ShelfInfo
    {
        public string ShelfId
        {
            get; set;
        }
    }
    #endregion
}
