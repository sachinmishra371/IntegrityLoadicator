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
using System.Data;
using Microsoft.VisualBasic;
using System.Data.Common;
using System.ComponentModel;
using iTextSharp.text;
using ZebecLoadMaster.Models;
using ZebecLoadMaster.Models.DAL;
using System.Windows.Media.Media3D;

namespace ZebecLoadMaster
{
    /// <summary>
    /// Interaction logic for BaplieContents.xaml
    /// </summary>
    public partial class BaplieContents : Window
    {
        DataTable dtupdate=new DataTable();
        string header;
        int index;

        public BaplieContents(DataTable dt)
        {
            InitializeComponent();
            dataGridBaplieContents.ItemsSource = dt.DefaultView;
            //dttableNew = dt.Clone();
            dtupdate = dt.Copy();
            //foreach (DataRow drtableOld in dt.Rows)
            //{
            //    //if (/*put some Condition */)
            //    //{
            //        dtTableNew.ImportRow(drtableOld);
            //    //}
            //}
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dtupdate.AcceptChanges();
                DbCommand command = Models.DAL.clsDBUtilityMethods.GetCommand();
                DbCommand command1 = Models.DAL.clsDBUtilityMethods.GetCommand();
                string Err = "", cmd = " ", cmd1 = " ";
                int rowsupdatecount = dtupdate.Rows.Count;
                for (int i = 0; i < rowsupdatecount; i++)
                {

                    cmd += "Update [20Ft_Container_Loading] set [Weight]=" + dtupdate.Rows[i]["Weight"].ToString() + " where [Container_ID]=" + dtupdate.Rows[i]["ContainerID"].ToString() + " ";
                    cmd += "Update [40Ft_Container_Loading] set [Weight]=" + dtupdate.Rows[i]["Weight"].ToString() + " where [Container_ID]=" + dtupdate.Rows[i]["ContainerID"].ToString() + " ";
                    //cmd += "Update [20Ft_Container_Loading] set [Weight]="  +0+ " where [Container_ID]=" + dtupdate.Rows[i]["ContainerID"].ToString() + " ";
                    //cmd += "Update [40Ft_Container_Loading] set [Weight]=" +0+ " where [Container_ID]=" + dtupdate.Rows[i]["ContainerID"].ToString() + " ";
                    //cmd1 += "select Container_No,bay,location,Weight,statustrueorfalse,Container_ID,cmbselected  from [20Ft_Container_Loading] where Container_ID=" + dtupdate.Rows[i]["ContainerID"].ToString() + " union ";
                    //cmd1 += "select Container_No,bay,location,Weight,statustrueorfalse,Container_ID,cmbselected   from [40Ft_Container_Loading] where Container_ID=" + dtupdate.Rows[i]["ContainerID"].ToString() + " union ";//union last m hatana hai forloop chalne k baad

                }
                command.CommandText = cmd;
                command.CommandType = CommandType.Text;
                Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command, Err);
                //command1.CommandText = cmd1;
                //command1.CommandType = CommandType.Text;
                //Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command1, Err);
                System.Windows.MessageBox.Show("Database Updated");
                //DataTable dttwentyforty = new DataTable();
                //DataSet ds = new DataSet();
                //ds = Models.DAL.clsDBUtilityMethods.GetDataSet(command1, Err);
                //dttwentyforty = ds.Tables[0];
            }
            catch (Exception ex) 
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            
        }
        DataGridRow dgRow;
        private void dataGridBaplieContents_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            try
            {
                header = e.Column.Header.ToString();
                if (e.Column.GetType().ToString() == "System.Windows.Controls.DataGridTextColumn")
                {
                    index = e.Row.GetIndex();
                    dgRow = e.Row;
                    TextBlock cbo = (System.Windows.Controls.TextBlock)e.Column.GetCellContent(e.Row);
                    if (index > 1048 && clsGlobVar.FlagDamageCases == false)
                    {
                        dgRow.IsEnabled = false;
                    }

                }
                //if (e.Column.GetType().ToString() == "System.Windows.Controls.DataGridComboBoxColumn")
                //{
                //    index = e.Row.GetIndex();
                //    System.Windows.Controls.ComboBox cbo;
                //    cbo = (System.Windows.Controls.ComboBox)e.Column.GetCellContent(e.Row);
                //    cbo.SelectionChanged += new SelectionChangedEventHandler(FSMType_SelectionChanged);
                //}
            }
            catch
            {

            }
        }

        private void DataGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            // Lookup for the source to be DataGridCell
            if (e.OriginalSource.GetType() == typeof(System.Windows.Controls.DataGridCell))
            {
                // Starts the Edit on the row;
                System.Windows.Controls.DataGrid grd = (System.Windows.Controls.DataGrid)sender;
                grd.BeginEdit(e);
            }
        }
       

        private void dataGridBaplieContents_KeyDown(object sender, KeyEventArgs e)
        {
            System.Windows.Input.Key k = e.Key;
            bool controlKeyIsDown = Keyboard.IsKeyDown(Key.LeftShift);
            if (!controlKeyIsDown &&
               Key.D0 <= k && k <= Key.D9 ||
                 Key.NumPad0 <= k && k <= Key.NumPad9 ||
                 k == Key.Decimal || k == Key.OemPeriod)
            {
                //e.Handled = false;

            }
            else
            {
                e.Handled = true;

            }
        }

        private void dataGridBaplieContents_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                DbCommand command = Models.DAL.clsDBUtilityMethods.GetCommand();
                string Err = "";
                int Containerid, fsmType;
                decimal percentfill;
                decimal sounding;

                Containerid = Convert.ToInt32((dataGridBaplieContents.Items[index] as DataRowView)["ContainerID"]);

                if (header == "Weight(T)")
                {
                    decimal weight = 0;
                   
                    //string Grp = (dataGridBaplieContents.Items[index] as DataRowView)["Group"].ToString();
                    weight = Convert.ToDecimal((dataGridBaplieContents.Items[index] as DataRowView)["Weight"]);
                    string query = "update [20Ft_Container_Loading] set [Weight]=" + weight + "";
                    //query += "[Temperature]=" + temp + " ,[VCF]=" + VCF + ",[Volume_Corr]=" + VolumeCor + ",[WCF]=" + WCF + ",[Weight_in_Air]=" + WtInAir + "" ;
                    query += " where Container_id=" + Containerid + " ";
                    query += "update [40Ft_Container_Loading] set [Weight]=" + weight + "";
                    query += " where Container_id=" + Containerid + " ";
                    //string query = "update tblSimulationMode_Tank_Status set [Volume]=" + Math.Round(volume, 3) + " ,[SG]=" + sg + ",";
                    //query += "[weight]=" + Math.Round(weight, 3) + "";
                    //query += " where Tank_ID=" + TankId + " ";
                    ////query += "update tblSimulationMode_Loading_Condition set [Percent_Full]=" + percentfill + " ";
                    ////query += " where Tank_ID=" + TankId + " ";
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command, Err);
                    #region 
                    //volume = Convert.ToDecimal((dataGridBaplieContents.Items[index] as DataRowView)["Volume"]);
                    //sg = Convert.ToDecimal((dataGridBaplieContents.Items[index] as DataRowView)["SG"]);
                    //percentfill = Convert.ToDecimal((dataGridBaplieContents.Items[index] as DataRowView)["Percent_Full"]);
                    //sounding = Convert.ToDecimal((dgTanks.Items[index] as DataRowView)["Sounding_Level"]);
                    //Ullage = Convert.ToDecimal((dgTanks.Items[index] as DataRowView)["Ullage"]);
                    //Den@15Deg = Convert.ToDecimal((dgTanks.Items[index] as DataRowView)["Den@15Deg"]);
                    //temp = Convert.ToDecimal((dgTanks.Items[index] as DataRowView)["Temperature"]);
                    //VCF = Convert.ToDecimal((dgTanks.Items[index] as DataRowView)["VCF"]);
                    //VolumeCor = Convert.ToDecimal((dgTanks.Items[index] as DataRowView)["Volume_Corr"]);
                    //WCF= Convert.ToDecimal((dgTanks.Items[index] as DataRowView)["WCF"]);
                    //WtInAir = Convert.ToDecimal((dgTanks.Items[index] as DataRowView)["Weight_in_Air"]);
                    //decimal maxsounding = maxVolume[TankId];

                    //if (header == "Volume")
                    //{



                    //    //command.CommandText = "SPChangeVolume";
                    //    //command.CommandType = CommandType.StoredProcedure;

                    //    //DbParameter param1 = Models.DAL.clsDBUtilityMethods.GetParameter();
                    //    //param1.DbType = DbType.Decimal;
                    //    //param1.ParameterName = "@Volume";
                    //    //command.Parameters.Add(param1);

                    //    //DbParameter param2 = Models.DAL.clsDBUtilityMethods.GetParameter();
                    //    //param2.DbType = DbType.Int32;
                    //    //param2.ParameterName = "@TankId";
                    //    //command.Parameters.Add(param2);

                    //    //DbParameter param3 = Models.DAL.clsDBUtilityMethods.GetParameter();
                    //    //param3.DbType = DbType.Double;
                    //    //param3.Direction = ParameterDirection.Output;
                    //    //param3.ParameterName = "@Sounding";
                    //    //command.Parameters.Add(param3);

                    //    //DbParameter param4 = Models.DAL.clsDBUtilityMethods.GetParameter();
                    //    //param4.DbType = DbType.Double;
                    //    //param4.Direction = ParameterDirection.Output;
                    //    //param4.ParameterName = "@Percent";
                    //    //command.Parameters.Add(param4);

                    //    ////DbParameter param5 = Models.DAL.clsDBUtilityMethods.GetParameter();
                    //    ////param5.DbType = DbType.Double;
                    //    ////param5.Direction = ParameterDirection.Output;
                    //    ////param5.ParameterName = "@Ullage";
                    //    ////command.Parameters.Add(param5);


                    //    //param1.Value = volume;
                    //    //param2.Value = TankId;
                    //    //Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command, Err);

                    //    //sounding =  Math.Round(Convert.ToDecimal(command.Parameters["@Sounding"].Value), 3);
                    //    //percentfill = Math.Round(Convert.ToDecimal(command.Parameters["@Percent"].Value), 2);
                    //    ////Ullage = Math.Round(Convert.ToDecimal(command.Parameters["@Ullage"].Value), 3);

                    //    //volume = Convert.ToDecimal(((sender as DataGrid).Items[index] as DataRowView)["Volume"]);
                    //    percentfill = Convert.ToDecimal((volume * 100) / maxsounding);
                    //    weight = volume * sg;
                    //}
                    //if (header == "% Fill")
                    //{
                    //    //command.CommandText = "SPChangePercentFill";
                    //    //command.CommandType = CommandType.StoredProcedure;

                    //    //DbParameter param1 = Models.DAL.clsDBUtilityMethods.GetParameter();
                    //    //param1.DbType = DbType.Decimal;
                    //    //param1.ParameterName = "@Percent";
                    //    //command.Parameters.Add(param1);

                    //    //DbParameter param2 = Models.DAL.clsDBUtilityMethods.GetParameter();
                    //    //param2.DbType = DbType.Int32;
                    //    //param2.ParameterName = "@TankId";
                    //    //command.Parameters.Add(param2);

                    //    ////DbParameter param3 = Models.DAL.clsDBUtilityMethods.GetParameter();
                    //    ////param3.DbType = DbType.Double;
                    //    ////param3.Direction = ParameterDirection.Output;
                    //    ////param3.ParameterName = "@Sounding";
                    //    ////command.Parameters.Add(param3);

                    //    //DbParameter param4 = Models.DAL.clsDBUtilityMethods.GetParameter();
                    //    //param4.DbType = DbType.Double;
                    //    //param4.Direction = ParameterDirection.Output;
                    //    //param4.ParameterName = "@Volume";
                    //    //command.Parameters.Add(param4);

                    //    //DbParameter param5 = Models.DAL.clsDBUtilityMethods.GetParameter();
                    //    //param5.DbType = DbType.Double;
                    //    //param5.Direction = ParameterDirection.Output;
                    //    //param5.ParameterName = "@Ullage";
                    //    //command.Parameters.Add(param5);

                    //    //param1.Value = percentfill;
                    //    //param2.Value = TankId;
                    //    //Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command, Err);

                    //    //sounding = Math.Round(Convert.ToDecimal(command.Parameters["@Sounding"].Value), 3);
                    //    //volume = Math.Round(Convert.ToDecimal(command.Parameters["@Volume"].Value), 3);
                    //    //Ullage = Math.Round(Convert.ToDecimal(command.Parameters["@Ullage"].Value), 3);
                    //    volume = (percentfill * maxsounding) / 100;
                    //    weight = volume * sg;
                    //}
                    //if (header == "S.G.")
                    //{
                    //    volume = Convert.ToDecimal((dgTanks.Items[index] as DataRowView)["Volume"]);

                    //    weight = volume * sg;
                    //}
                    //if (header == "Weight")
                    //{
                    //    //command.CommandText = "SPChangeVolume";
                    //    //command.CommandType = CommandType.StoredProcedure;

                    //    //DbParameter param1 = Models.DAL.clsDBUtilityMethods.GetParameter();
                    //    //param1.DbType = DbType.Decimal;
                    //    //param1.ParameterName = "@Volume";
                    //    //command.Parameters.Add(param1);

                    //    //DbParameter param2 = Models.DAL.clsDBUtilityMethods.GetParameter();
                    //    //param2.DbType = DbType.Int32;
                    //    //param2.ParameterName = "@TankId";
                    //    //command.Parameters.Add(param2);

                    //    //DbParameter param3 = Models.DAL.clsDBUtilityMethods.GetParameter();
                    //    //param3.DbType = DbType.Double;
                    //    //param3.Direction = ParameterDirection.Output;
                    //    //param3.ParameterName = "@Sounding";
                    //    //command.Parameters.Add(param3);

                    //    //DbParameter param4 = Models.DAL.clsDBUtilityMethods.GetParameter();
                    //    //param4.DbType = DbType.Double;
                    //    //param4.Direction = ParameterDirection.Output;
                    //    //param4.ParameterName = "@Percent";
                    //    //command.Parameters.Add(param4);

                    //    ////DbParameter param5 = Models.DAL.clsDBUtilityMethods.GetParameter();
                    //    ////param5.DbType = DbType.Double;
                    //    ////param5.Direction = ParameterDirection.Output;
                    //    ////param5.ParameterName = "@Ullage";
                    //    ////command.Parameters.Add(param5);

                    //    //param1.Value = weight/sg;
                    //    //param2.Value = TankId;
                    //    //Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command, Err);

                    //    //sounding = Math.Round(Convert.ToDecimal(command.Parameters["@Sounding"].Value), 3);
                    //    //percentfill = Math.Round(Convert.ToDecimal(command.Parameters["@Percent"].Value), 2);
                    //    //Ullage = Math.Round(Convert.ToDecimal(command.Parameters["@Ullage"].Value), 3);
                    //    //volume = Convert.ToDecimal(((sender as DataGrid).Items[index] as DataRowView)["Weight"]) / sg;

                    //    //volume = weight / sg;
                    //    //percentfill = Convert.ToDecimal((volume * 100) / maxsounding);
                    //}
                    //if(header == "Sounding")
                    //{

                    //        command.CommandText = "SPChangeSounding";
                    //        command.CommandType = CommandType.StoredProcedure;

                    //        DbParameter param1 = Models.DAL.clsDBUtilityMethods.GetParameter();
                    //        param1.DbType = DbType.Decimal;
                    //        param1.ParameterName = "@Sounding";
                    //        command.Parameters.Add(param1);

                    //        DbParameter param2 = Models.DAL.clsDBUtilityMethods.GetParameter();
                    //        param2.DbType = DbType.Int32;
                    //        param2.ParameterName = "@TankId";
                    //        command.Parameters.Add(param2);

                    //        DbParameter param3 = Models.DAL.clsDBUtilityMethods.GetParameter();
                    //        param3.DbType = DbType.Double;
                    //        param3.Direction = ParameterDirection.Output;
                    //        param3.ParameterName = "@Percent";
                    //        command.Parameters.Add(param3);

                    //        DbParameter param4 = Models.DAL.clsDBUtilityMethods.GetParameter();
                    //        param4.DbType = DbType.Double;
                    //        param4.Direction = ParameterDirection.Output;
                    //        param4.ParameterName = "@Volume";
                    //        command.Parameters.Add(param4);

                    //        //DbParameter param5 = Models.DAL.clsDBUtilityMethods.GetParameter();
                    //        //param5.DbType = DbType.Double;
                    //        //param5.Direction = ParameterDirection.Output;
                    //        //param5.ParameterName = "@Ullage";
                    //        //command.Parameters.Add(param5);

                    //        param1.Value = sounding;
                    //        param2.Value = TankId;
                    //        Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command, Err);

                    //        percentfill = Math.Round(Convert.ToDecimal(command.Parameters["@Percent"].Value), 2);
                    //        volume = Math.Round(Convert.ToDecimal(command.Parameters["@Volume"].Value), 3);
                    //        //Ullage = Math.Round(Convert.ToDecimal(command.Parameters["@Ullage"].Value), 3);
                    //        weight = volume * sg;

                    //}20.11.22

                    //if (header == "Ullage")
                    //{

                    //    command.CommandText = "SPChangeUllage";
                    //    command.CommandType = CommandType.StoredProcedure;

                    //    DbParameter param1 = Models.DAL.clsDBUtilityMethods.GetParameter();
                    //    param1.DbType = DbType.Decimal;
                    //    param1.ParameterName = "@Ullage";
                    //    command.Parameters.Add(param1);

                    //    DbParameter param2 = Models.DAL.clsDBUtilityMethods.GetParameter();
                    //    param2.DbType = DbType.Int32;
                    //    param2.ParameterName = "@TankId";
                    //    command.Parameters.Add(param2);

                    //    DbParameter param3 = Models.DAL.clsDBUtilityMethods.GetParameter();
                    //    param3.DbType = DbType.Double;
                    //    param3.Direction = ParameterDirection.Output;
                    //    param3.ParameterName = "@Percent";
                    //    command.Parameters.Add(param3);

                    //    DbParameter param4 = Models.DAL.clsDBUtilityMethods.GetParameter();
                    //    param4.DbType = DbType.Double;
                    //    param4.Direction = ParameterDirection.Output;
                    //    param4.ParameterName = "@Volume";
                    //    command.Parameters.Add(param4);

                    //    //DbParameter param5 = Models.DAL.clsDBUtilityMethods.GetParameter();
                    //    //param5.DbType = DbType.Double;
                    //    //param5.Direction = ParameterDirection.Output;
                    //    //param5.ParameterName = "@Sounding";
                    //    //command.Parameters.Add(param5);

                    //    param1.Value = Ullage;
                    //    param2.Value = TankId;
                    //    Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command, Err);

                    //    percentfill = Math.Round(Convert.ToDecimal(command.Parameters["@Percent"].Value), 2);
                    //    volume = Math.Round(Convert.ToDecimal(command.Parameters["@Volume"].Value), 3);
                    //    Sounding = Math.Round(Convert.ToDecimal(command.Parameters["@Sounding"].Value), 3);
                    //    weight = volume * sg;

                }




                //if (header == "Temp" && Grp == "CARGO_TANK" && cbDensity.IsChecked == true)
                //{
                //    double DensityValue = Convert.ToDouble(txtDensity.Text);
                //    if (DensityValue <= 0.770)
                //    {

                //        VCF = Convert.ToDecimal(Math.Pow(2.7182, -((346.422 + 0.438 * DensityValue * 1000) / (DensityValue * 1000 * DensityValue * 1000)) * (Convert.ToDouble(temp) - 15) * (1 + 0.8 * ((346.422 + 0.438 * DensityValue*1000) / (DensityValue*1000 * DensityValue*1000)) * (Convert.ToDouble(temp) - 15))));


                //    }
                //    if (DensityValue >= 0.839)
                //    {

                //        VCF = Convert.ToDecimal(Math.Pow(2.7182, -((594.5418 + 0 * DensityValue*1000) / (DensityValue*1000 * DensityValue*1000)) * (Convert.ToDouble(temp) - 15) * (1 + 0.8 * ((594.5418 + 0 * DensityValue*1000) / (DensityValue*1000 * DensityValue*1000)) * (Convert.ToDouble(temp) - 15))));
                //    }
                //    if (DensityValue >= 0.778 && DensityValue < 0.839)
                //    {

                //        VCF = Convert.ToDecimal(Math.Pow(2.7182, -((186.9696 + 0.4861 * DensityValue*1000) / (DensityValue*1000 * DensityValue*1000)) * (Convert.ToDouble(temp) - 15) * (1 + 0.8 * ((186.9696 + 0.4861 * DensityValue*1000) / (DensityValue*1000 * DensityValue*1000)) * (Convert.ToDouble(temp) - 15))));
                //    }
                //    if (DensityValue > 0.770 && DensityValue < 0.778)
                //    {

                //        VCF = Convert.ToDecimal(Math.Pow(2.7182, -((-0.003361 + 2680.32) / (DensityValue*1000 * DensityValue*1000)) * (Convert.ToDouble(temp) - 15) * (1 + 0.8 * ((-0.003361 + 2680.32) / (DensityValue*1000 * DensityValue*1000)) * (Convert.ToDouble(temp) - 15))));
                //    }



                //    WCF = Convert.ToDecimal((DensityValue) - 0.0011);
                //    VolumeCor = VCF * volume;
                //    WtInAir = VolumeCor * WCF;
                //}
                else
                    {
                        //temp = 0;
                    }
                //(dataGridBaplieContents.Items[index] as DataRowView)["Weight(T)"] = Math.Round(volume, 3);
                // weight = volume * sg;
                //(dataGridBaplieContents.Items[index] as DataRowView)["Volume"] = Math.Round(volume, 3);
                //(dataGridBaplieContents.Items[index] as DataRowView)["Weight"] = Math.Round(weight, 3);
                //(dataGridBaplieContents.Items[index] as DataRowView)["Percent_Full"] = Math.Round(percentfill, 3);
                //(dataGridBaplieContents.Items[index] as DataRowView)["Sounding_Level"] = Math.Round(sounding, 3);
                //(dataGridBaplieContents.Items[index] as DataRowView)["Ullage"] = Math.Round(Ullage, 3);
                //(dataGridBaplieContents.Items[index] as DataRowView)["Den@15Deg"] = Math.Round(Den@15Deg, 3);
                //(dataGridBaplieContents.Items[index] as DataRowView)["Temperature"] = Math.Round(temp, 3);
                //(dataGridBaplieContents.Items[index] as DataRowView)["VCF"] = Math.Round(VCF, 3);
                //(dataGridBaplieContents.Items[index] as DataRowView)["Volume_Corr"] = Math.Round(VolumeCor, 3);
                //(dataGridBaplieContents.Items[index] as DataRowView)["WCF"] = Math.Round(WCF, 3);
                //(dataGridBaplieContents.Items[index] as DataRowView)["Weight_in_Air"] = Math.Round(WtInAir, 3);
                //(dataGridBaplieContents.Items[index] as DataRowView)["SG"] = Math.Round(sg, 3);

                //decimal res1 = decimal.Compare(minsounding, volume);
                //decimal res2 = decimal.Compare(volume, maxsounding);
                //int result1 = (int)res1;
                //int result2 = (int)res2;
                //if (result1 > 0 || result2 > 0)
                //{
                //    string error = "Volume should be between " + minsounding + " and " + maxsounding;
                //    System.Windows.MessageBox.Show(error);
                //    // e.Cancel = true;
                //    return;
                //}
                //else
                //{
                //string query = "update tblSimulationMode_Tank_Status set [Volume]=" + volume + " ,[Sounding_Level]=" + sounding + ",[SG]=" + sg + ",";
                //query += "[Temperature]=" + temp + " ,[VCF]=" + VCF + ",[Volume_Corr]=" + VolumeCor + ",[WCF]=" + WCF + ",[Weight_in_Air]=" + WtInAir + "" ;
                //query += " where Tank_ID=" + TankId + " ";
                //string query = "update tblSimulationMode_Tank_Status set [Volume]=" + Math.Round(volume, 3) + " ,[SG]=" + sg + ",";
                //query += "[weight]=" + Math.Round(weight, 3) + "";
                //query += " where Tank_ID=" + TankId + " ";
                ////query += "update tblSimulationMode_Loading_Condition set [Percent_Full]=" + percentfill + " ";
                ////query += " where Tank_ID=" + TankId + " ";
                //command.CommandText = query;
                //command.CommandType = CommandType.Text;
                //Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command, Err);
                #endregion
                
                //}
                index = -1;
                Containerid = 0;
                }

                //if (header == "FSM")
                //{
                //    decimal fsm;
                //    fsm = Convert.ToDecimal((dgTanks.Items[index] as DataRowView)["FSM"]);
                //    fsmType = Convert.ToInt16((dgTanks.Items[index] as DataRowView)["max_1_act_0"]);

                //    if (fsmType == 2)
                //    {
                //        string query1 = "update tblSimulationMode_Loading_Condition set FSM=" + fsm + " where Tank_ID=" + TankId;
                //        command.CommandText = query1;
                //        command.CommandType = CommandType.Text;
                //        Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command, Err);
                //    }
                //}

            //}
            catch (Exception ex)
            {
                //System.Windows.MessageBox.Show(ex.ToString());
            }
        }
    }
}
