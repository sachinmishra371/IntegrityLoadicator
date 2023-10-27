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
namespace ZebecLoadMaster
{
    /// <summary>
    /// Interaction logic for LightShipdetails.xaml
    /// </summary>
    public partial class LightShipdetails : Window
    {
        public LightShipdetails()
        {
            InitializeComponent();

            try
            {
            string sCmd = "SELECT [Lightship_wt],[Lightship_LCG],[Lightship_VCG],[Lightship_TCG]FROM [tblMaster_Config_Addi]";
            DbCommand command = Models.DAL.clsDBUtilityMethods.GetCommand();
            command.CommandText = sCmd;
            command.CommandType = CommandType.Text;
            string Err = "";
            DataSet ds = new DataSet();
            ds = Models.DAL.clsDBUtilityMethods.GetDataSet(command, Err);
            txtWeight.Text = ds.Tables[0].Rows[0]["Lightship_wt"].ToString();
            txtLCG.Text= ds.Tables[0].Rows[0]["Lightship_LCG"].ToString();
            txtVCG.Text = ds.Tables[0].Rows[0]["Lightship_VCG"].ToString();
            txtTCG.Text = ds.Tables[0].Rows[0]["Lightship_TCG"].ToString();
            }
            catch (Exception)
            {
                
                
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            DbCommand command = Models.DAL.clsDBUtilityMethods.GetCommand();
            string Err = "", sCmd="";

            sCmd = "UPDATE [tblMaster_Config_Addi] SET [Lightship_wt]=" + txtWeight.Text + ",[Lightship_LCG]=" + txtLCG.Text + ",[Lightship_VCG]=" + txtVCG.Text + ",[Lightship_TCG]=" + txtTCG.Text + " ";

            sCmd += "UPDATE [tblSimulationMode_Loading_Condition] SET [Weight]=" + txtWeight.Text + ",[LCG]=" + txtLCG.Text + ",[VCG]=" + txtVCG.Text + ",[TCG]=" + txtTCG.Text + "," +
                   "[Lmom]=" + txtLCG.Text + "*" + txtWeight.Text + ",[Tmom]=" + txtTCG.Text + "*" + txtWeight.Text + ",[Vmom]=" + txtVCG.Text + "*" + txtWeight.Text + " " +
                   " WHERE [Tank_ID]= (SELECT [Tank_ID] FROM [tblMaster_Tank] WHERE [Tank_Name]='LIGHTSHIP WEIGHT' AND [Group]='LIGHTSHIP')";

            sCmd += "UPDATE [tblSimulationMode_Tank_Status] SET [Weight]=" + txtWeight.Text + " WHERE [Tank_ID]= (SELECT [Tank_ID] FROM [tblMaster_Tank] WHERE [Tank_Name]='LIGHTSHIP WEIGHT' AND [Group]='LIGHTSHIP')";

            sCmd += "UPDATE [tblSimulationMode_Equilibrium_Values] SET [Lightship_Weight]=" + txtWeight.Text + "";

            sCmd += "UPDATE [tblLoading_Condition] SET [Weight]=" + txtWeight.Text + ",[LCG]=" + txtLCG.Text + ",[VCG]=" + txtVCG.Text + ",[TCG]=" + txtTCG.Text + "," +
                    "[Lmom]=" + txtLCG.Text + "*" + txtWeight.Text + ",[Tmom]=" + txtTCG.Text + "*" + txtWeight.Text + ",[Vmom]=" + txtVCG.Text + "*" + txtWeight.Text + " " +
                    " WHERE [Tank_ID]= (SELECT [Tank_ID] FROM [tblMaster_Tank] WHERE [Tank_Name]='LIGHTSHIP WEIGHT' AND [Group]='LIGHTSHIP')";

            sCmd += "UPDATE [tblTank_Status] SET [Weight]=" + txtWeight.Text + " WHERE [Tank_ID]= (SELECT [Tank_ID] FROM [tblMaster_Tank] WHERE [Tank_Name]='LIGHTSHIP WEIGHT' AND [Group]='LIGHTSHIP')";

            sCmd += "UPDATE [tblRealMode_Equilibrium_Values] SET [Lightship_Weight]=" + txtWeight.Text + "";

            command.CommandText = sCmd;
            command.CommandType = CommandType.Text;
            Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command, Err);

            System.Windows.MessageBox.Show("Updated Successfully");
            }
            catch (Exception)
            {
                
             
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
           this.Close();
        }

        private void btnclear_Click(object sender, RoutedEventArgs e)
        {
            txtLCG.Text = "";
            txtTCG.Text = "";
            txtWeight.Text = "";
            txtVCG.Text = "";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
