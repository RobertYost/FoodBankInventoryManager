﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;

namespace FoodBankInventoryManager
{
    /// <summary>
    /// Interaction logic for PasswordManagementWindow.xaml
    /// </summary>
    public partial class PasswordManagementWindow : Window
    {
        private User myCurrentUser;
        private L2S_FoodBankDBDataContext dbContext;
        public PasswordManagementWindow(User currentUser)
        {
            InitializeComponent();
            myCurrentUser = currentUser;
            dbContext = new L2S_FoodBankDBDataContext(ConfigurationManager.ConnectionStrings["FoodBankInventoryManager.Properties.Settings.FoodBankDBConnectionString"].ConnectionString);
        }

        private void txtUserName_Loaded(object sender, RoutedEventArgs e)
        {
            txtUserName.Text = myCurrentUser.FirstName + " " + myCurrentUser.LastName;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (BCrypt.CheckPassword(pwBoxCurrent.Password, myCurrentUser.Password))
            {
                string myHash = "";
                if (Validate(pwBoxNew.Password))
                {
                    string mySalt = BCrypt.GenerateSalt();
                    myHash = BCrypt.HashPassword(pwBoxNew.Password, mySalt);
                }
                else
                {
                    MessageBox.Show("Please enter a password");
                }
                if (Validate(pwBoxConfirm.Password) && BCrypt.CheckPassword(pwBoxConfirm.Password, myHash))
                {
                    //For some reason you have to query for the user instead of being able to use
                    //the one passed to you (i.e. myCurrentUser)
                    User user = (from users in dbContext.GetTable<User>()
                                 where users.Email == myCurrentUser.Email
                                 select users).First<User>();
                    user.Password = myHash;
                    dbContext.SubmitChanges();
                    MessageBox.Show("Password successfully changed!");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Incorrect password provided");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool Validate(string content)
        {
            return !(String.IsNullOrWhiteSpace(content) || String.IsNullOrEmpty(content));
        }
    }
}
