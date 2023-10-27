using System;
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
using ZebecLoadMaster.Models.DAL;
using System.Data.Common;
using System.Data;
using System.Text.RegularExpressions;
namespace ZebecLoadMaster
{
    /// <summary>
    /// Interaction logic for LightShipdetails.xaml
    /// </summary>
    public partial class UnitConCalcWindow : Window
    {
        double TempFer, Bare, Den60;
        public UnitConCalcWindow()
        {
            InitializeComponent();

            try
            {
  
            }
            catch (Exception)
            {
                
                
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
           this.Close();
        }

      


       

        private void txtCB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtBarrels_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtBarrels.Text != "")
            {


                 Bare = Convert.ToDouble(txtBarrels.Text);
               txtVolCum.Text = (Math.Round(Bare / 6.2898, 3)).ToString();
                
            }
            else
            {
                txtVolCum.Text = "";
            }

        }

     

        private void txttempFer_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txttempFer.Text != "")
            {

                 TempFer = Convert.ToDouble(txttempFer.Text);
                txttempCel.Text = (Math.Round((TempFer - 32) / 1.8, 3)).ToString();
                
            }
            else
            {
                txttempCel.Text = "";
            }
        }

        private void txtDen60_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtDen60.Text != "")
            {

               Den60 = Convert.ToDouble(txtDen60.Text);
                txtDen15.Text = (Math.Round(730 - 4 * (Den60 - 66))).ToString(); ;
            }
            else
            {
                txtDen15.Text = "";
            }
        }

    


       
 
    }
}
