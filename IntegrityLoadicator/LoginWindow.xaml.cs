﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ZebecLoadMaster
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public string pswrd, defaultpwd = string.Empty;
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btncancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogin(object sender, RoutedEventArgs e)
        {
            pswrd = txtPaswword.Text;
            if (pswrd == "zebec123")
            {
                this.Close();
                
                    LightShipdetails objLshpData = new LightShipdetails();
                    objLshpData.ShowDialog();
            
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Incorrect Password");
            }
        }

        private void btnResetPwd(object sender, RoutedEventArgs e)
        {
            grdResetPWD.Visibility = Visibility.Visible;
            grdLogin.Visibility = Visibility.Hidden;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            defaultpwd = txtdefaultPaswword.Text;
            if (txtdefaultPaswword.Text == "pass@123")
            {
                //defaultpwd = "pass@123";
                string message = "Your password is Zebec123";
                string title = "Confirmation";
                System.Windows.Forms.MessageBox.Show(message, title);
                txtdefaultPaswword.Clear();
                grdResetPWD.Visibility = Visibility.Hidden;
                grdLogin.Visibility = Visibility.Visible;
                
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Incorrect Password");
            }

        }
    }
}