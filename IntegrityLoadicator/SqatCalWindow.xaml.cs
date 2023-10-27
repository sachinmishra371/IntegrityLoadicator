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
    public partial class SqatCalWindow : Window
    {
        public SqatCalWindow()
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
            try
            {
                double Br   =Convert.ToDouble(txtBrShip.Text);
                double CanalW=Convert.ToDouble(txtCanWidth.Text);
                double CB   =Convert.ToDouble(txtCB.Text);
                double DfAP =Convert.ToDouble(txtDraftAP.Text);
                double DfFP =Convert.ToDouble(txtDraftFP.Text);
                double Vspeed =Convert.ToDouble(txtVsSpeed.Text );
                double WtDep = Convert.ToDouble(txtxWtDepth.Text);
                double MaxDraft, CalcValue;

                if (DfFP > DfAP)
                {
                    MaxDraft = DfFP;
                }
                else
                {
                    MaxDraft = DfAP;
                }


                if (rbConfined.IsChecked == true)
                {
                   

                    CalcValue = Math.Round((CB * Math.Pow(Vspeed, 2.08) / 30) * Math.Pow(((Br * MaxDraft) /((WtDep * CanalW - Br * MaxDraft))),0.667),3);
               
                   
                    lblcalc.Content="Confined water = " +CalcValue+" m";
                }

                else
                {

                    CalcValue = Math.Round((CB * Math.Pow(Vspeed, 2.08) / 30) * Math.Pow((Br * MaxDraft / (WtDep * Br * (7.7+ 20*(1-CB)*(1-CB)) - (Br*MaxDraft))), 0.667),3);
                    
                    lblcalc.Content="Open Water = " +CalcValue+" m";
                }
          
            }
            catch (Exception ex)
            {
                MessageBox.Show("Input all Values");
                
             
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
           this.Close();
        }

        private void btnclear_Click(object sender, RoutedEventArgs e)
        {
            txtBrShip.Text = "";
            txtCanWidth.Text = "";
            txtCB.Text = "";
            txtDraftAP.Text = "";
            txtDraftFP.Text = "";
            txtVsSpeed.Text = "";
            txtxWtDepth.Text = "";
            lblcalc.Content = "";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            rbConfined.IsChecked = true;

        }


       

        private void txtCB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

       
 
    }
}
