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
using System.Data.Common;
using System.IO;
using System.Collections;
using ZebecLoadMaster.Models.DAL;
using System.Runtime.Serialization.Formatters.Binary;
using ZebecLoadMaster.Models;

namespace ZebecLoadMaster
{
    /// <summary>
    /// Interaction logic for LoadingCondition.xaml
    /// </summary>
    public partial class LoadingCondition : Window
    {
        string CalculationMethod, DamageCase;
        public static string folder = "";
        public int savedcount = 0;
        public bool btnokclick;
        public int bay20datainsertedcount = 0;
        int index;
       
        public LoadingCondition(string folderName)
        {
            InitializeComponent();
            LoadingConditionList(folderName);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void LoadingConditionList(string folderName)
        {
            try
            {
                folder = folderName;
                lblError.Visibility = Visibility.Hidden;
                string st = System.IO.Directory.GetCurrentDirectory();
                string path = st + folderName;
               
                var dir = System.IO.Directory.GetDirectories(path).OrderBy(d => new System.IO.DirectoryInfo(d).FullName);
                if (folder == "\\SMData")
                {
                    listBoxSavedCondition.Items.SortDescriptions.Add(
                          new System.ComponentModel.SortDescription("",
                          System.ComponentModel.ListSortDirection.Descending));
                    index = st.Length + 8;
                    btnDelete.Visibility = Visibility.Visible;
                    lblConditionType.Content = "Saved Loading Condition";
                }
                else
                {
                    
                    index = st.Length + 14;
                    btnDelete.Visibility = Visibility.Hidden;
                    lblConditionType.Content = "Standard Loading Condition";
                }
                string names;
                foreach (string s in dir)
                {
                    names = s.Remove(0, index);
                    listBoxSavedCondition.Items.Add(names);
                }
                //listBoxSavedCondition.Items.SortDescriptions.Add(
                //        new System.ComponentModel.SortDescription("",
                //        System.ComponentModel.ListSortDirection.Ascending));
                if (listBoxSavedCondition.Items.Count == 0)
                {
                    lblError.Visibility = Visibility.Visible;
                }
            }
            catch
            {

            }

        }

        public void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                

                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

                if (listBoxSavedCondition.SelectedItem != null)
                {

                    //SFcorrection();
                    string filename = listBoxSavedCondition.SelectedItem.ToString();
                    //commented for testing 27.11.22 for standard data
                    //if (folder == "\\SMData")
                    //{
                    //    string[] file1 = filename.Split('_');
                    //    CalculationMethod = file1[5].ToString();
                    //    //if (CalculationMethod == "Damage")
                    //    //{
                    //    //    DamageCase = file1[5].ToString();
                    //    //    MainWindow.CheckNCheckOutCount = 1;
                    //    //    //if (chk1 == null) { checkBox = false; } else { checkBox = (bool)chk1.IsChecked; }
                    //    //}
                    //}
                    //end 
                    //Models.clsGlobVar.stdLoad = "Saved";
                    //MainWindow.saveDate = listBoxSavedCondition.SelectedItem.ToString();
                    //MainScreen obj = new MainScreen();
                    // Switcher.Switch(new MainScreen());
                    //MainWindow.refresh();

                    foreach (Window window in Application.Current.Windows)
                        {
                        if (window.GetType() == typeof(MainWindow))
                        {
                            if (folder == "\\SMData")
                            {
                                //(window as MainWindow).lblCalculationMethod.Content = CalculationMethod;

                                //if (CalculationMethod == "Intact")
                                //{
                                //    (window as MainWindow).btnToggle.IsChecked = false;
                                //}
                                //else
                                //{
                                //    (window as MainWindow).btnToggle.IsChecked = true;
                                //    if (DamageCase == "CASE 1(P)")
                                //    {
                                //        //(window as MainWindow).radioButtonCaseA.IsChecked = true;
                                //        (window as MainWindow).cmbDamageCases.SelectedIndex = 1;
                                //    }

                                //    if (DamageCase == "CASE 1(S)")
                                //    {
                                //        //(window as MainWindow).radioButtonCaseA.IsChecked = true;
                                //        (window as MainWindow).cmbDamageCases.SelectedIndex = 9;
                                //    }

                                //    else if (DamageCase == "CASE 11")
                                //    {
                                //        (window as MainWindow).cmbDamageCases.SelectedIndex = 19;
                                //    }

                                //}                           

                            }
                            else
                            {
                                (window as MainWindow).lblCalculationMethod.Content = "Intact";
                                (window as MainWindow).btnToggle.IsChecked = false;
                            }
                         
                                DbCommand command = Models.DAL.clsDBUtilityMethods.GetCommand();
                                string Err = "";
                                string cmd = "";
                                cmd = "update [20Ft_Showbaywise]  set weight=0,cmbselected=0 ";
                                cmd += "update [20Ft_Container_Loading]  set weight=0,cmbselected=0 ";
                                cmd += "update [40Ft_Container_Loading] set weight=0,cmbselected=0 ";
                                cmd += "update [40Ft_Showbaywise] set weight=0,cmbselected=0 ";
                                command.CommandText = cmd;
                                command.CommandType = CommandType.Text;
                                Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command, Err);

                               //(window as MainWindow).refresh();
                               //refresh(); commented bcoz adding code below to avoid agian & again run refreshscreen 
                            //addedcode in place of refresh function to avoid again & again run refreshscreen due to mainwindow calling
                            try
                            {
                                string path = System.IO.Directory.GetCurrentDirectory() + folder + "\\" + listBoxSavedCondition.SelectedItem.ToString();


                                FileStream fs = new FileStream(path + "\\Tanks.cnd", FileMode.Open, FileAccess.Read, FileShare.None);
                                BinaryFormatter ob = new BinaryFormatter();
                                 Err = "";
                                 cmd = "";

                                List<Tanks> listTank = new List<Tanks>();
                                listTank = (List<Tanks>)ob.Deserialize(fs);
                                fs.Close();
                                //dtSMBallast= liBallast.toDa
                                DataTable dtTanks = CollectionHelper.ConvertTo<Tanks>(listTank);
                                //dtTanks = dtSMTanks.Clone();
                                 command = Models.DAL.clsDBUtilityMethods.GetCommand();
                                try
                                {

                                    foreach (DataRow row in dtTanks.Rows)
                                    {
                                        if (Convert.ToInt16(row["Tank_ID"].ToString()) > 0)
                                            cmd += " UPDATE tblSimulationMode_Tank_Status  SET Volume=" + row["Volume"].ToString() + ",SG=" + row["SG"].ToString() + ",IsDamaged='" + row["IsDamaged"].ToString() + "' WHERE Tank_ID=" + row["Tank_ID"].ToString() + " Update tblFSM_max_act set max_1_act_0=" + row["max_1_act_0"].ToString() + " WHERE Tank_ID=" + row["Tank_ID"].ToString() + " Update tblSimulationMode_Loading_Condition set FSM=" + row["FSM"].ToString() + " WHERE Tank_ID=" + row["Tank_ID"].ToString();
                                    }
                                    //con.Close();
                                    command.CommandText = cmd;
                                    command.CommandType = CommandType.Text;
                                    Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command, Err);
                                }
                                catch { }
                                //for (int i = 0; i < dtTanks.Rows.Count; i++)
                                //{
                                //    dtTanks.ImportRow(dtSMTanks.Rows[i]);

                                //}
                                //dtTanks.AcceptChanges();
                                try
                                {
                                    fs = new FileStream(path + "\\FixedLoads.cnd", FileMode.Open, FileAccess.Read, FileShare.None);
                                    //  ob = new BinaryFormatter();
                                    cmd = "";
                                    List<FixedItems> liDeck1 = new List<FixedItems>();
                                    liDeck1 = (List<FixedItems>)ob.Deserialize(fs);
                                    fs.Close();
                                    DataTable dtFixedLoads = CollectionHelper.ConvertTo<FixedItems>(liDeck1);
                                    DbCommand command4 = Models.DAL.clsDBUtilityMethods.GetCommand();
                                    command4 = Models.DAL.clsDBUtilityMethods.GetCommand();
                                    cmd += " delete from [tblFixedLoad_Simulation] where Tank_ID>46";
                                    //cmd += " delete FROM [Saushyant_Stability].[dbo].[tblMaster_Tank] where [Group]='FIXED_WEIGHT'";
                                    //cmd += "  delete FROM [Saushyant_Stability].[dbo].[tblSimulationMode_Loading_Condition] where Tank_ID in (select Tank_ID FROM [Saushyant_Stability].[dbo].[tblMaster_Tank] where [Group]='FIXED_WEIGHT')";
                                    //cmd += "delete FROM [Saushyant_Stability].[dbo].tblSimulationMode_Tank_Status where Tank_ID in (select Tank_ID FROM [Saushyant_Stability].[dbo].[tblMaster_Tank] where [Group]='FIXED_WEIGHT')";
                                    cmd += " delete FROM [Saushyant_Stability].[dbo].[tblMaster_Tank] where Tank_ID>46";
                                    cmd += " delete FROM [Saushyant_Stability].[dbo].[tblSimulationMode_Loading_Condition] where Tank_ID>46";
                                    cmd += "delete FROM [Saushyant_Stability].[dbo].tblSimulationMode_Tank_Status where Tank_ID>46";
                                    command4.CommandText = cmd;
                                    command4.CommandType = CommandType.Text;
                                    Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command4, Err);
                                    foreach (DataRow row in dtFixedLoads.Rows)
                                    {

                                        string cmd1 = "INSERT INTO [tblMaster_Tank] ([Tank_ID],[Group],[Tank_Name])  VALUES (" + row["Tank_ID"].ToString() + ",'FIXED_WEIGHT','FIXED WEIGHT')";

                                        cmd1 += "INSERT INTO [tblSimulationMode_Loading_Condition] ([Tank_ID],[Percent_Full],[Volume],[SG],[Weight],[LCG],[Lmom],[TCG]," +
                                                 " [Tmom],[VCG],[Vmom],[FSM],[User],[IsManualEntry],[Sounding_Level],[Timestamp],[Permeability],[IsDamaged])" +
                                                 "VALUES (" + row["Tank_ID"].ToString() + ",0,0,1,0,0,0,0,0,0,0,0,'dbo',1,0,GETDATE(),1,0)";

                                        cmd1 += "INSERT INTO [tblSimulationMode_Tank_Status] ([Tank_ID],[Volume],[SG],[IsDamaged],[Timestamp],[Sounding_Level],[User],[Weight])" +
                                                              "VALUES (" + row["Tank_ID"].ToString() + ",0,1,0,GETDATE(),0,'dbo',0)";
                                        //cmd += "INSERT INTO [tblFixedLoad_Simulation] ([Tank_ID],[Volume],[SG],[IsDamaged],[Timestamp],[Sounding_Level],[User],[Weight])" +
                                        //                      "VALUES (" + row["Tank_ID"].ToString() + ",0,1,0,GETDATE(),0,'dbo',0)";
                                        cmd1 += @"INSERT INTO tblFixedLoad_Simulation ([tank_Id],[Load_Name],[Weight],[LCG],[TCG],[VCG],[Length],[Breadth],[Depth])
                                                              VALUES (" + row["Tank_ID"].ToString() + ",'FIXED WEIGHT',0,0,0,0,0,0,0)";
                                        cmd1 += " UPDATE tblSimulationMode_Loading_Condition SET [Weight]=" + row["Weight"].ToString() + ", LCG =" + row["LCG"].ToString() + ",VCG=" + row["VCG"].ToString() + ",TCG=" + row["TCG"].ToString() + " WHERE Tank_ID=" + row["Tank_ID"].ToString();
                                        cmd1 += " UPDATE tblSimulationMode_Tank_Status SET Weight=" + row["Weight"].ToString() + "  WHERE Tank_ID=" + row["Tank_ID"].ToString();
                                        cmd1 += " UPDATE tblMaster_Tank SET Tank_Name='" + row["Tank_Name"].ToString() + "'  WHERE Tank_ID=" + row["Tank_ID"].ToString();

                                        //FIXED WEIGHT TEST UPDATE 



                                        cmd1 += " update tblFixedLoad_Simulation set [Weight]=" + row["Weight"].ToString() + " where Tank_ID=" + row["Tank_ID"].ToString();
                                        cmd1 += " update tblFixedLoad_Simulation set VCG=" + row["VCG"].ToString() + " where Tank_ID=" + row["Tank_ID"].ToString();
                                        cmd1 += " update tblFixedLoad_Simulation set LCG=" + row["LCG"].ToString() + " where Tank_ID=" + row["Tank_ID"].ToString();
                                        cmd1 += " update tblFixedLoad_Simulation set TCG=" + row["TCG"].ToString() + " where Tank_ID=" + row["Tank_ID"].ToString();
                                        //END

                                        command4.CommandText = cmd1;
                                        command4.CommandType = CommandType.Text;
                                        Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command4, Err);
                                        Models.clsGlobVar.dtSimulationfixedload = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationModefixedload");//simultaionfixed commented bcoz refresh commneted 6march
                                        (window as MainWindow).dgVariableItems.ItemsSource = Models.clsGlobVar.dtSimulationfixedload.DefaultView; //commented bcoz refresh commneted 6march


                                    }
                                    //command4.CommandText = cmd1;
                                    //command4.CommandType = CommandType.Text;
                                    //Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command4, Err);

                                    //Models.clsGlobVar.dtSimulationVariableItems = dtFixedLoads;
                                    //TEST 22FEB4.34PM

                                    //MainWindow M = new MainWindow();
                                    Models.clsGlobVar.dtSimulationfixedload = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationModefixedload");//simultaionfixed 
                                    (window as MainWindow).dgVariableItems.ItemsSource = Models.clsGlobVar.dtSimulationfixedload.DefaultView;
                                    //END 
                                    //M.dgVariableItems.ItemsSource = Models.clsGlobVar.dtSimulationVariableItems.DefaultView;
                                }
                                catch (Exception ex)
                                {
                                    //   System.Windows.MessageBox.Show(EX.ToString());
                                }
                                try
                                {
                                    //for container 20
                                    fs = new FileStream(path + "\\bays.cnd", FileMode.Open, FileAccess.Read, FileShare.None);

                                    List<Bays> bays1 = new List<Bays>();
                                    bays1 = (List<Bays>)ob.Deserialize(fs);
                                    fs.Close();
                                    //dtSMBallast= liBallast.toDa
                                    DataTable dtbays = CollectionHelper.ConvertTo<Bays>(bays1);
                                    //dtTanks = dtSMTanks.Clone();
                                    DbCommand command1 = Models.DAL.clsDBUtilityMethods.GetCommand();
                                    command1 = Models.DAL.clsDBUtilityMethods.GetCommand();
                                    string cmd1 = "";
                                    //row["cmbselected"].ToString()=dtbays.Rows;
                                    foreach (DataRow row in dtbays.Rows)
                                    {

                                        cmd1 += " UPDATE [20Ft_Container_Loading]  SET bay='" + row["Bay"].ToString() + "',[cmbselected]=" + row["cmbselected"].ToString() + ",Container_No=" + row["Container_No"].ToString() + ",weight=" + row["weight"].ToString() + ",lcg=" + row["lcg"].ToString() + ",tcg=" + row["tcg"].ToString() + ",vcg=" + row["vcg"].ToString() + ",[Container_Count]='" + row["Container_Count"].ToString() + "' WHERE [Container_No]=" + row["Container_No"].ToString();

                                    }
                                    //con.Close();
                                    command1.CommandText = cmd1;
                                    command1.CommandType = CommandType.Text;
                                    Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command1, Err);
                                    //end
                                    //System.Windows.MessageBox.Show(""+Convert.ToInt32(dtbays.Rows[0][12]));
                                    if (1 == Convert.ToInt32(dtbays.Rows[0][12]))
                                    {
                                        Collection objCollection1 = new Collection();
                                        objCollection1.CollectionRefresh();
                                        //MainWindow m = new MainWindow();
                                        (window as MainWindow).dgContainers20FootInHold.ItemsSource = null;
                                        (window as MainWindow).dgContainers20FootOnDeck.ItemsSource = null;
                                        (window as MainWindow).dgContainers20FootInHold.ItemsSource = objCollection1.Load20InHoldBaySource;
                                        (window as MainWindow).dgContainers20FootOnDeck.ItemsSource = objCollection1.Load20OnDeckBaySource;
                                    }
                                }
                                catch { }
                                try
                                {
                                    //for container 40
                                    fs = new FileStream(path + "\\bays40.cnd", FileMode.Open, FileAccess.Read, FileShare.None);

                                    List<Bays> bays2 = new List<Bays>();
                                    bays2 = (List<Bays>)ob.Deserialize(fs);
                                    fs.Close();
                                    //dtSMBallast= liBallast.toDa
                                    DataTable dtbays2 = CollectionHelper.ConvertTo<Bays>(bays2);
                                    DbCommand command2 = Models.DAL.clsDBUtilityMethods.GetCommand();
                                    //dtTanks = dtSMTanks.Clone();
                                    command2 = Models.DAL.clsDBUtilityMethods.GetCommand();
                                    string cmd2 = "";
                                    foreach (DataRow row in dtbays2.Rows)
                                    {

                                        cmd2 += " UPDATE [40Ft_Container_Loading]  SET bay='" + row["Bay"].ToString() + "',[cmbselected]=" + row["cmbselected"].ToString() + ",Container_No = " + row["Container_No"].ToString() + ",weight=" + row["weight"].ToString() + ",lcg=" + row["lcg"].ToString() + ",tcg=" + row["tcg"].ToString() + ",vcg=" + row["vcg"].ToString() + ",[Container_Count]='" + row["Container_Count"].ToString() + "' WHERE [Container_No]=" + row["Container_No"].ToString();
                                    }
                                    //con.Close();
                                    command2.CommandText = cmd2;
                                    command2.CommandType = CommandType.Text;
                                    Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command2, Err);
                                    //System.Windows.MessageBox.Show("" + Convert.ToInt32(dtbays2.Rows[0][12]));
                                    if (1 == Convert.ToInt32(dtbays2.Rows[0][13]))
                                    {
                                        Collection objCollection1 = new Collection();
                                        //objCollection.CollectionRefresh();
                                        //MainWindow m = new MainWindow();
                                        (window as MainWindow).dgContainers40FootInHold.ItemsSource = objCollection1.Load40InHoldBaySource;

                                    }
                                    //end
                                }
                                catch (Exception)
                                {
                                    //System.Windows.MessageBox.Show(ex.ToString()); 
                                }

                                try
                                {
                                    //for bay 20
                                    fs = new FileStream(path + "\\showbays20.cnd", FileMode.Open, FileAccess.Read, FileShare.None);

                                    List<Bays> bays3 = new List<Bays>();
                                    bays3 = (List<Bays>)ob.Deserialize(fs);
                                    fs.Close();
                                    //dtSMBallast= liBallast.toDa
                                    DataTable dtbays3 = CollectionHelper.ConvertTo<Bays>(bays3);
                                    //dtTanks = dtSMTanks.Clone();
                                    DbCommand command3 = Models.DAL.clsDBUtilityMethods.GetCommand();
                                    command3 = Models.DAL.clsDBUtilityMethods.GetCommand();
                                    string cmd2 = "";
                                    foreach (DataRow row in dtbays3.Rows)
                                    {

                                        cmd2 += " UPDATE [20Ft_Showbaywise]  SET bay='" + row["Bay"].ToString() + "',[cmbselected]=" + row["cmbselected"].ToString() + ",Container_No = " + row["Container_No"].ToString() + ",weight=" + row["weight"].ToString() + ",lcg=" + row["lcg"].ToString() + ",tcg=" + row["tcg"].ToString() + ",vcg=" + row["vcg"].ToString() + ",[Container_Count]='" + row["count"].ToString() + "' WHERE [Container_No]=" + row["Container_No"].ToString();
                                    }

                                    command3.CommandText = cmd2;
                                    command3.CommandType = CommandType.Text;
                                    Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command3, Err);
                                    //con.Close();
                                    //end
                                }
                                catch (Exception)
                                {
                                    //System.Windows.MessageBox.Show(ex.ToString());
                                }
                                try
                                {
                                    //for bay 40
                                    fs = new FileStream(path + "\\showbays40.cnd", FileMode.Open, FileAccess.Read, FileShare.None);

                                    List<Bays> bays3 = new List<Bays>();
                                    bays3 = (List<Bays>)ob.Deserialize(fs);
                                    fs.Close();
                                    //dtSMBallast= liBallast.toDa
                                    DataTable dtbays3 = CollectionHelper.ConvertTo<Bays>(bays3);
                                    //dtTanks = dtSMTanks.Clone();
                                    DbCommand command3 = Models.DAL.clsDBUtilityMethods.GetCommand();
                                    command3 = Models.DAL.clsDBUtilityMethods.GetCommand();
                                    string cmd2 = "";
                                    foreach (DataRow row in dtbays3.Rows)
                                    {

                                        cmd2 += " UPDATE [40Ft_Showbaywise]  SET bay='" + row["Bay"].ToString() + "',[cmbselected]=" + row["cmbselected"].ToString() + ",Container_No = " + row["Container_No"].ToString() + ",weight=" + row["weight"].ToString() + ",lcg=" + row["lcg"].ToString() + ",tcg=" + row["tcg"].ToString() + ",vcg=" + row["vcg"].ToString() + ",[Container_Count]='" + row["count"].ToString() + "' WHERE [Container_No]=" + row["Container_No"].ToString();
                                    }
                                    //con.Close();
                                    command3.CommandText = cmd2;
                                    command3.CommandType = CommandType.Text;
                                    Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command3, Err);
                                    //end
                                }
                                catch { }
                                Models.clsGlobVar.dtSimulationAllTanks = dtTanks;


                            }
                            catch (Exception ex)
                            {
                                System.Windows.MessageBox.Show(ex.Message);
                            }
                            //end the added code
                            try
                                {
                                    (window as MainWindow).LBLBTNOKCLICK.Content = "TRUE";
                                }
                                catch(Exception ex) 
                                {
                                  //  System.Windows.MessageBox.Show(ex.ToString());
                                }
                                (window as MainWindow).dgTanks.ItemsSource = Models.clsGlobVar.dtSimulationAllTanks.DefaultView;
                                // (window as MainWindow).dgFixedLoad.ItemsSource = Models.clsGlobVar.dtSimulationVariableItems.DefaultView;           commnetd by sachin for contaier             
                                //commented for making saved 20showbay and testing quoki jb combo box  selected karege tv ayega datagrid m
                                // (window as MainWindow).dgContainers20FootInHold.ItemsSource = null;
                                Models.clsGlobVar.dtFinalLoadingCondition1show20 = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationModeshowbay20");

                                Models.clsGlobVar.dtFinalLoadingCondition2show40 = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationModeshowbay40");
                                Models.clsGlobVar.dtFinalLoadingCondition1 = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationMode20Ft_Container_Loading");
                                Models.clsGlobVar.dtFinalLoadingCondition2 = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationMode40Ft_Container_Loading");
                                //Models.clsGlobVar.dtFinalLoadingCondition1 = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationModeshowbay20");
                                //Models.clsGlobVar.dtFinalLoadingCondition2 = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationModeshowbay40");
                                Collection objCollection = new Collection();
                                //DataRow row in dtBay.Rows
                                if (Convert.ToInt32(1) == Convert.ToInt32(Models.clsGlobVar.dtFinalLoadingCondition1.Rows[0]["cmbselected"].ToString())) //20 container
                                {
                                    objCollection.CollectionRefresh();
                                    (window as MainWindow).dgContainers20FootInHold.ItemsSource = objCollection.Load20InHoldBaySource;
                                    //(window as MainWindow).dgContainers40FootOnDeck
                                    (window as MainWindow).dgContainers20FootOnDeck.ItemsSource = objCollection.Load20OnDeckBaySource;
                                //DataView DV = Models.clsGlobVar.dtFinalLoadingCondition1.AsDataView();
                                //(window as MainWindow).dgContainers40FootInHold.ItemsSource = objCollection.Load40InHoldBaySource;26.11
                                //(window as MainWindow).dgContainers40FootOnDeck.ItemsSource = objCollection.Load40OnDeckBaySource;26.11
                                //(window as MainWindow).dgContainers40FootInHold.Columns[1].Visibility = Visibility.Visible;
                                (window as MainWindow).dgContainers20FootInHold.Columns[2].Visibility = Visibility.Visible;
                                (window as MainWindow).dgContainers20FootOnDeck.Columns[2].Visibility = Visibility.Visible;
                                (window as MainWindow).dgContainers20FootOnDeck.Columns[1].Visibility = Visibility.Hidden;
                                (window as MainWindow).cmbloadingdata.SelectedIndex = 1;
                                    //(window as MainWindow).cmbloadingdata40.SelectedIndex = 1;
                                    (window as MainWindow).cmbloadingdatadeck20.SelectedIndex = 1;
                                    //(window as MainWindow).cmbloadingdata40deck.SelectedIndex = 1;
                                    (window as MainWindow).LBLBTNOKCLICK.Content = "FALSE";
                                (window as MainWindow).btnshow.Visibility = Visibility.Visible;
                                (window as MainWindow).btnshow1.Visibility = Visibility.Visible;
                                Models.clsGlobVar.dtSimulationfixedload = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationModefixedload");//simultaionfixed 
                                (window as MainWindow).dgVariableItems.ItemsSource = Models.clsGlobVar.dtSimulationfixedload.DefaultView;

                            }
                                 if (Convert.ToInt32(1) == Convert.ToInt32(Models.clsGlobVar.dtFinalLoadingCondition2.Rows[0]["cmbselected"].ToString()))//40 container
                                {
                                    objCollection.CollectionRefresh();
                                    (window as MainWindow).dgContainers20FootInHold.ItemsSource = objCollection.Load20InHoldBaySource;
                                    //(window as MainWindow).dgContainers40FootOnDeck
                                    (window as MainWindow).dgContainers20FootOnDeck.ItemsSource = objCollection.Load20OnDeckBaySource;
                                    //DataView DV = Models.clsGlobVar.dtFinalLoadingCondition1.AsDataView();
                                    (window as MainWindow).dgContainers40FootInHold.ItemsSource = objCollection.Load40InHoldBaySource;
                                    (window as MainWindow).dgContainers40FootOnDeck.ItemsSource = objCollection.Load40OnDeckBaySource;

                                    //(window as MainWindow).cmbloadingdata.SelectedIndex = 1;
                                    (window as MainWindow).cmbloadingdata40.SelectedIndex = 1;
                                    //(window as MainWindow).cmbloadingdatadeck20.SelectedIndex = 1;
                                    (window as MainWindow).cmbloadingdata40deck.SelectedIndex = 1;
                                    (window as MainWindow).LBLBTNOKCLICK.Content = "FALSE";
                                }
                            if (Convert.ToInt32(1) == Convert.ToInt32(Models.clsGlobVar.dtFinalLoadingCondition1show20.Rows[0]["cmbselected"].ToString()))
                            {
                                
                                if (0 == bay20datainsertedcount)
                                {
                                    objCollection.CollectionRefreshBAY();//showbay20
                                    (window as MainWindow).dgContainers20FootInHold.ItemsSource = null;
                                    (window as MainWindow).dgContainers20FootInHold.ItemsSource = objCollection.Load20InHoldBaySource;
                                    (window as MainWindow).dgContainers20FootInHold.Columns[2].Visibility = Visibility.Hidden;
                                    //(window as MainWindow).dgContainers40FootOnDeck
                                    (window as MainWindow).dgContainers20FootOnDeck.ItemsSource = null;
                                    (window as MainWindow).dgContainers20FootOnDeck.ItemsSource = objCollection.Load20OnDeckBaySource;
                                    (window as MainWindow).dgContainers20FootOnDeck.Columns[2].Visibility = Visibility.Hidden;
                                    //DataView DV = Models.clsGlobVar.dtFinalLoadingCondition1.AsDataView();
                                    // (window as MainWindow).dgContainers40FootInHold.ItemsSource = objCollection.Load40InHoldBaySource;26.11.22 
                                    //(window as MainWindow).dgContainers40FootInHold.Columns[1].Visibility = Visibility.Hidden;26.11.22
                                    //(window as MainWindow).dgContainers40FootOnDeck.ItemsSource = objCollection.Load40OnDeckBaySource;26.11.22
                                    //(window as MainWindow).dgContainers40FootOnDeck.Columns[1].Visibility = Visibility.Hidden;26.11.22
                                    //(window as MainWindow).dgContainers40FootOnDeck.Columns[2].Visibility = Visibility.Hidden;26.11.22
                                    //check 6pm25
                                    (window as MainWindow).cmbloadingdata.SelectedIndex = 2;
                                    //(window as MainWindow).cmbloadingdata40.SelectedIndex = 2;
                                    (window as MainWindow).cmbloadingdatadeck20.SelectedIndex = 2;
                                    //(window as MainWindow).cmbloadingdata40deck.SelectedIndex = 2;
                                    (window as MainWindow).LBLBTNOKCLICK.Content = "FALSE";
                                    (window as MainWindow).btnshow.Visibility = Visibility.Hidden;
                                    (window as MainWindow).btnshow1.Visibility = Visibility.Hidden;
                                    //MainWindow M = new MainWindow();
                                    Models.clsGlobVar.dtSimulationfixedload = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationModefixedload");//simultaionfixed 
                                    (window as MainWindow).dgVariableItems.ItemsSource = Models.clsGlobVar.dtSimulationfixedload.DefaultView;
                                    bay20datainsertedcount++;
                                }
                            }
                                if (Convert.ToInt32(1) == Convert.ToInt32(Models.clsGlobVar.dtFinalLoadingCondition2show40.Rows[0]["cmbselected"].ToString()))
                                {

                                    objCollection.CollectionRefreshBAY();//showbay 40
                                                                         //(window as MainWindow).dgContainers20FootInHold.ItemsSource = objCollection.Load20InHoldBaySource;26.11.22
                                                                         //(window as MainWindow).dgContainers20FootInHold.Columns[2].Visibility = Visibility.Hidden;26.11.22

                                    //(window as MainWindow).dgContainers20FootOnDeck.ItemsSource = objCollection.Load20OnDeckBaySource;26.11.22
                                    //(window as MainWindow).dgContainers20FootOnDeck.Columns[2].Visibility = Visibility.Hidden;26.11.22
                                    //DataView DV = Models.clsGlobVar.dtFinalLoadingCondition1.AsDataView();
                                    (window as MainWindow).dgContainers40FootInHold.ItemsSource = objCollection.Load40InHoldBaySource;
                                    (window as MainWindow).dgContainers40FootInHold.Columns[1].Visibility = Visibility.Hidden;
                                    (window as MainWindow).dgContainers40FootOnDeck.ItemsSource = objCollection.Load40OnDeckBaySource;
                                    (window as MainWindow).dgContainers40FootOnDeck.Columns[1].Visibility = Visibility.Hidden;
                                    (window as MainWindow).dgContainers40FootOnDeck.Columns[2].Visibility = Visibility.Hidden;
                                    //check 6pm25
                                    //(window as MainWindow).cmbloadingdata.SelectedIndex = 2;
                                    (window as MainWindow).cmbloadingdata40.SelectedIndex = 2;
                                    //(window as MainWindow).cmbloadingdatadeck20.SelectedIndex = 2;
                                    (window as MainWindow).cmbloadingdata40deck.SelectedIndex = 2;


                                }
                                //string x=Models.clsGlobVar.dtFinalLoadingCondition2show40.Rows[0]["cmbselected"].ToString();

                                (window as MainWindow).txtLoadingConditionName.Text = listBoxSavedCondition.SelectedItem.ToString();
                                (window as MainWindow).LBLBTNOKCLICK.Content = "FALSE";

                        }
                        try
                        {
                            if (savedcount == 0)
                            {
                                //(window as MainWindow).LBLBTNOKCLICK.Content = "FALSE";
                                savedcount++;
                            }
                        }
                        catch { }
                        }
                    this.Close();
                    
                    MessageBox.Show(listBoxSavedCondition.SelectedItem.ToString() + " Loading Condition Loaded");
                    bay20datainsertedcount = 0;
                    clsGlobVar.loadingconname = listBoxSavedCondition.SelectedItem.ToString();
                    //MainWindow M = new MainWindow(); commented bcoz refresh commneted 6march
                    //Models.clsGlobVar.dtSimulationfixedload = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationModefixedload");//simultaionfixed commented bcoz refresh commneted 6march
                    //M.dgVariableItems.ItemsSource = Models.clsGlobVar.dtSimulationfixedload.DefaultView; commented bcoz refresh commneted 6march
                    //Models.clsGlobVar.flagLoadingCondition = true;
                    //M.FUNCTIONOTHERWINDOWTESTING();
                    //MessageBox.Show("Loading Condition Completed");
                }
                else
                {
                    MessageBox.Show("Please Select a Loading Condition");
                }
                Mouse.OverrideCursor = null;
            }
            catch
            {
                this.Close();
                Mouse.OverrideCursor = null;
            }

            
        }

        //private void
        //()
        //{
        //    try
        //    {
        //        string path = System.IO.Directory.GetCurrentDirectory() + folder + "\\" + listBoxSavedCondition.SelectedItem.ToString();

              
        //        FileStream fs = new FileStream(path + "\\Tanks.cnd", FileMode.Open, FileAccess.Read, FileShare.None);
        //        BinaryFormatter ob = new BinaryFormatter();


        //        List<Tanks> listTank = new List<Tanks>();
        //        listTank = (List<Tanks>)ob.Deserialize(fs);
        //        fs.Close();
        //        //dtSMBallast= liBallast.toDa
        //        DataTable dtTanks = CollectionHelper.ConvertTo<Tanks>(listTank);
        //        //dtTanks = dtSMTanks.Clone();
        //        DbCommand command = Models.DAL.clsDBUtilityMethods.GetCommand();
        //        string Err = "";
        //        string cmd = "";

        //        foreach (DataRow row in dtTanks.Rows)
        //        {
        //            cmd += " UPDATE tblSimulationMode_Tank_Status  SET Volume=" + row["Volume"].ToString() + ",SG=" + row["SG"].ToString() + ",IsDamaged=" + Convert.ToInt16(row["IsDamaged"]) + " WHERE Tank_ID=" + row["Tank_ID"].ToString() + " Update tblFSM_max_act set max_1_act_0=" + row["max_1_act_0"].ToString() + " WHERE Tank_ID=" + row["Tank_ID"].ToString() + " Update tblSimulationMode_Loading_Condition set FSM=" + row["FSM"].ToString() + " WHERE Tank_ID=" + row["Tank_ID"].ToString() ;
        //        }
        //        //con.Close();
        //        command.CommandText = cmd;
        //        command.CommandType = CommandType.Text;
        //        Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command, Err);
        //        //for (int i = 0; i < dtTanks.Rows.Count; i++)
        //        //{
        //        //    dtTanks.ImportRow(dtSMTanks.Rows[i]);

        //        //}
        //        //dtTanks.AcceptChanges();

        //        fs = new FileStream(path + "\\FixedLoads.cnd", FileMode.Open, FileAccess.Read, FileShare.None);
        //        //  ob = new BinaryFormatter();
        //        cmd = "";
        //        List<FixedItems> liDeck1 = new List<FixedItems>();
        //        liDeck1 = (List<FixedItems>)ob.Deserialize(fs);
        //        fs.Close();
        //        DataTable dtFixedLoads = CollectionHelper.ConvertTo<FixedItems>(liDeck1);
        //        command = Models.DAL.clsDBUtilityMethods.GetCommand();
        //        foreach (DataRow row in dtFixedLoads.Rows)
        //        {
        //            cmd += " UPDATE tblSimulationMode_Loading_Condition SET LCG=" + row["LCG"].ToString() + ",VCG=" + row["VCG"].ToString() + ",TCG=" + row["TCG"].ToString() + " ,FSM=" + row["FSM"].ToString() + " WHERE Tank_ID=" + row["Tank_ID"].ToString();
        //            cmd += " UPDATE tblSimulationMode_Tank_Status SET Weight=" + row["Weight"].ToString() + "  WHERE Tank_ID=" + row["Tank_ID"].ToString(); ;
        //        }

        //        command.CommandText = cmd;
        //        command.CommandType = CommandType.Text;
        //        Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command, Err);
        //        Models.clsGlobVar.dtSimulationAllTanks = dtTanks;
        //        Models.clsGlobVar.dtSimulationVariableItems = dtFixedLoads;
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }

        //}

        //private void
        //
        //{
        //    try
        //    {
        //        string path = System.IO.Directory.GetCurrentDirectory() + folder + "\\" + listBoxSavedCondition.SelectedItem.ToString();


        //        FileStream fs = new FileStream(path + "\\Tanks.cnd", FileMode.Open, FileAccess.Read, FileShare.None);
        //        BinaryFormatter ob = new BinaryFormatter();


        //        List<Tanks> listTank = new List<Tanks>();
        //        listTank = (List<Tanks>)ob.Deserialize(fs);
        //        fs.Close();
        //        //dtSMBallast= liBallast.toDa
        //        DataTable dtTanks = CollectionHelper.ConvertTo<Tanks>(listTank);
        //        //dtTanks = dtSMTanks.Clone();
        //        DbCommand command = Models.DAL.clsDBUtilityMethods.GetCommand();
        //        string Err = "";
        //        string cmd = "";

        //        foreach (DataRow row in dtTanks.Rows)
        //        {
        //            cmd += " UPDATE tblSimulationMode_Tank_Status  SET Volume=" + row["Volume"].ToString() + ",SG=" + row["SG"].ToString() + ",IsDamaged=" + Convert.ToInt16(row["IsDamaged"]) + " WHERE Tank_ID=" + row["Tank_ID"].ToString() + " Update tblFSM_max_act set max_1_act_0=" + row["max_1_act_0"].ToString() + " WHERE Tank_ID=" + row["Tank_ID"].ToString() + " Update tblSimulationMode_Loading_Condition set FSM=" + row["FSM"].ToString() + " WHERE Tank_ID=" + row["Tank_ID"].ToString();
        //        }
        //        //con.Close();
        //        command.CommandText = cmd;
        //        command.CommandType = CommandType.Text;
        //        Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command, Err);
        //        //for (int i = 0; i < dtTanks.Rows.Count; i++)
        //        //{
        //        //    dtTanks.ImportRow(dtSMTanks.Rows[i]);

        //        //}
        //        //dtTanks.AcceptChanges();

        //        fs = new FileStream(path + "\\FixedLoads.cnd", FileMode.Open, FileAccess.Read, FileShare.None);
        //        //  ob = new BinaryFormatter();
        //        cmd = "";
        //        List<FixedItems> liDeck1 = new List<FixedItems>();
        //        liDeck1 = (List<FixedItems>)ob.Deserialize(fs);
        //        fs.Close();
        //        DataTable dtFixedLoads = CollectionHelper.ConvertTo<FixedItems>(liDeck1);
        //        command = Models.DAL.clsDBUtilityMethods.GetCommand();
        //        foreach (DataRow row in dtFixedLoads.Rows)
        //        {
        //            cmd += "INSERT INTO [tblMaster_Tank] ([Tank_ID],[Group],[Tank_Name])  VALUES (" + row["Tank_ID"].ToString() + ",'FIXED_WEIGHT','FixedWeight')";

        //            cmd += "INSERT INTO [tblSimulationMode_Loading_Condition] ([Tank_ID],[Percent_Full],[Volume],[SG],[Weight],[LCG],[Lmom],[TCG]," +
        //                     " [Tmom],[VCG],[Vmom],[FSM],[User],[IsManualEntry],[Sounding_Level],[Timestamp],[Permeability],[IsDamaged])" +
        //                     "VALUES (" + row["Tank_ID"].ToString() + ",0,0,1,0,0,0,0,0,0,0,0,'dbo',1,0,GETDATE(),1,0)";

        //            cmd += "INSERT INTO [tblSimulationMode_Tank_Status] ([Tank_ID],[Volume],[SG],[IsDamaged],[Timestamp],[Sounding_Level],[User],[Weight])" +
        //                                  "VALUES (" + row["Tank_ID"].ToString() + ",0,1,0,GETDATE(),0,'dbo',0)";

        //            cmd += " UPDATE tblSimulationMode_Loading_Condition SET LCG=" + row["LCG"].ToString() + ",VCG=" + row["VCG"].ToString() + ",TCG=" + row["TCG"].ToString() + " ,FSM=" + row["FSM"].ToString() + " WHERE Tank_ID=" + row["Tank_ID"].ToString();
        //            cmd += " UPDATE tblSimulationMode_Tank_Status SET Weight=" + row["Weight"].ToString() + "  WHERE Tank_ID=" + row["Tank_ID"].ToString();
        //            cmd += " UPDATE tblMaster_Tank SET Tank_Name='" + row["Tank_Name"].ToString() + "'  WHERE Tank_ID=" + row["Tank_ID"].ToString();
        //        }

        //        command.CommandText = cmd;
        //        command.CommandType = CommandType.Text;
        //        Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command, Err);
        //        Models.clsGlobVar.dtSimulationAllTanks = dtTanks;
        //        Models.clsGlobVar.dtSimulationVariableItems = dtFixedLoads;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }

        //}

        private void refresh()
        {
            try
            {
                string path = System.IO.Directory.GetCurrentDirectory() + folder + "\\" + listBoxSavedCondition.SelectedItem.ToString();


                FileStream fs = new FileStream(path + "\\Tanks.cnd", FileMode.Open, FileAccess.Read, FileShare.None);
                BinaryFormatter ob = new BinaryFormatter();
                string Err = "";
                string cmd = "";
                 
                List<Tanks> listTank = new List<Tanks>();
                listTank = (List<Tanks>)ob.Deserialize(fs);
                fs.Close();
                //dtSMBallast= liBallast.toDa
                DataTable dtTanks = CollectionHelper.ConvertTo<Tanks>(listTank);
                //dtTanks = dtSMTanks.Clone();
                DbCommand command = Models.DAL.clsDBUtilityMethods.GetCommand();
                try { 

                foreach (DataRow row in dtTanks.Rows)
                {
                    if (Convert.ToInt16(row["Tank_ID"].ToString()) > 0)
                        cmd += " UPDATE tblSimulationMode_Tank_Status  SET Volume=" + row["Volume"].ToString() + ",SG=" + row["SG"].ToString() + ",IsDamaged='" + row["IsDamaged"].ToString() + "' WHERE Tank_ID=" + row["Tank_ID"].ToString() + " Update tblFSM_max_act set max_1_act_0=" + row["max_1_act_0"].ToString() + " WHERE Tank_ID=" + row["Tank_ID"].ToString() + " Update tblSimulationMode_Loading_Condition set FSM=" + row["FSM"].ToString() + " WHERE Tank_ID=" + row["Tank_ID"].ToString();
                }
                //con.Close();
                command.CommandText = cmd;
                command.CommandType = CommandType.Text;
                Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command, Err);
            }
            catch { }
                //for (int i = 0; i < dtTanks.Rows.Count; i++)
                //{
                //    dtTanks.ImportRow(dtSMTanks.Rows[i]);

                //}
                //dtTanks.AcceptChanges();
                try
                {
                    fs = new FileStream(path + "\\FixedLoads.cnd", FileMode.Open, FileAccess.Read, FileShare.None);
                    //  ob = new BinaryFormatter();
                    cmd = "";
                    List<FixedItems> liDeck1 = new List<FixedItems>();
                    liDeck1 = (List<FixedItems>)ob.Deserialize(fs);
                    fs.Close();
                    DataTable dtFixedLoads = CollectionHelper.ConvertTo<FixedItems>(liDeck1);
                    DbCommand command4 = Models.DAL.clsDBUtilityMethods.GetCommand();
                    command4 = Models.DAL.clsDBUtilityMethods.GetCommand();
                    cmd += " delete from [tblFixedLoad_Simulation] where Tank_ID>46";
                    //cmd += " delete FROM [Saushyant_Stability].[dbo].[tblMaster_Tank] where [Group]='FIXED_WEIGHT'";
                    //cmd += "  delete FROM [Saushyant_Stability].[dbo].[tblSimulationMode_Loading_Condition] where Tank_ID in (select Tank_ID FROM [Saushyant_Stability].[dbo].[tblMaster_Tank] where [Group]='FIXED_WEIGHT')";
                    //cmd += "delete FROM [Saushyant_Stability].[dbo].tblSimulationMode_Tank_Status where Tank_ID in (select Tank_ID FROM [Saushyant_Stability].[dbo].[tblMaster_Tank] where [Group]='FIXED_WEIGHT')";
                    cmd += " delete FROM [Saushyant_Stability].[dbo].[tblMaster_Tank] where Tank_ID>46";
                    cmd += " delete FROM [Saushyant_Stability].[dbo].[tblSimulationMode_Loading_Condition] where Tank_ID>46";
                    cmd += "delete FROM [Saushyant_Stability].[dbo].tblSimulationMode_Tank_Status where Tank_ID>46";
                    command4.CommandText = cmd;
                    command4.CommandType = CommandType.Text;
                    Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command4, Err);
                    foreach (DataRow row in dtFixedLoads.Rows)
                    {

                       string cmd1 = "INSERT INTO [tblMaster_Tank] ([Tank_ID],[Group],[Tank_Name])  VALUES (" + row["Tank_ID"].ToString() + ",'FIXED_WEIGHT','FIXED WEIGHT')";

                        cmd1 += "INSERT INTO [tblSimulationMode_Loading_Condition] ([Tank_ID],[Percent_Full],[Volume],[SG],[Weight],[LCG],[Lmom],[TCG]," +
                                 " [Tmom],[VCG],[Vmom],[FSM],[User],[IsManualEntry],[Sounding_Level],[Timestamp],[Permeability],[IsDamaged])" +
                                 "VALUES (" + row["Tank_ID"].ToString() + ",0,0,1,0,0,0,0,0,0,0,0,'dbo',1,0,GETDATE(),1,0)";

                        cmd1 += "INSERT INTO [tblSimulationMode_Tank_Status] ([Tank_ID],[Volume],[SG],[IsDamaged],[Timestamp],[Sounding_Level],[User],[Weight])" +
                                              "VALUES (" + row["Tank_ID"].ToString() + ",0,1,0,GETDATE(),0,'dbo',0)";
                        //cmd += "INSERT INTO [tblFixedLoad_Simulation] ([Tank_ID],[Volume],[SG],[IsDamaged],[Timestamp],[Sounding_Level],[User],[Weight])" +
                        //                      "VALUES (" + row["Tank_ID"].ToString() + ",0,1,0,GETDATE(),0,'dbo',0)";
                        cmd1 += @"INSERT INTO tblFixedLoad_Simulation ([tank_Id],[Load_Name],[Weight],[LCG],[TCG],[VCG],[Length],[Breadth],[Depth])
                                                              VALUES (" + row["Tank_ID"].ToString() + ",'FIXED WEIGHT',0,0,0,0,0,0,0)";
                        cmd1 += " UPDATE tblSimulationMode_Loading_Condition SET [Weight]=" + row["Weight"].ToString() +", LCG =" + row["LCG"].ToString() + ",VCG=" + row["VCG"].ToString() + ",TCG=" + row["TCG"].ToString() + " WHERE Tank_ID=" + row["Tank_ID"].ToString();
                        cmd1 += " UPDATE tblSimulationMode_Tank_Status SET Weight=" + row["Weight"].ToString() + "  WHERE Tank_ID=" + row["Tank_ID"].ToString();
                        cmd1 += " UPDATE tblMaster_Tank SET Tank_Name='" + row["Tank_Name"].ToString() + "'  WHERE Tank_ID=" + row["Tank_ID"].ToString();

                        //FIXED WEIGHT TEST UPDATE 



                        cmd1 += " update tblFixedLoad_Simulation set [Weight]=" + row["Weight"].ToString() + " where Tank_ID=" + row["Tank_ID"].ToString();
                        cmd1 += " update tblFixedLoad_Simulation set VCG=" + row["VCG"].ToString() + " where Tank_ID=" + row["Tank_ID"].ToString();
                        cmd1 += " update tblFixedLoad_Simulation set LCG=" + row["LCG"].ToString() + " where Tank_ID=" + row["Tank_ID"].ToString();
                        cmd1 += " update tblFixedLoad_Simulation set TCG=" + row["TCG"].ToString() + " where Tank_ID=" + row["Tank_ID"].ToString();
                        //END

                        command4.CommandText = cmd1;
                        command4.CommandType = CommandType.Text;
                        Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command4, Err);


                    }
                    //command4.CommandText = cmd1;
                    //command4.CommandType = CommandType.Text;
                    //Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command4, Err);

                    //Models.clsGlobVar.dtSimulationVariableItems = dtFixedLoads;
                    //TEST 22FEB4.34PM

                    MainWindow M = new MainWindow();
                    Models.clsGlobVar.dtSimulationfixedload = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationModefixedload");//simultaionfixed 
                    M.dgVariableItems.ItemsSource = Models.clsGlobVar.dtSimulationfixedload.DefaultView;
                    //END 
                    //M.dgVariableItems.ItemsSource = Models.clsGlobVar.dtSimulationVariableItems.DefaultView;
                }
                catch(Exception ex) {
                 //   System.Windows.MessageBox.Show(EX.ToString());
                }
                try
                {
                    //for container 20
                    fs = new FileStream(path + "\\bays.cnd", FileMode.Open, FileAccess.Read, FileShare.None);

                    List<Bays> bays1 = new List<Bays>();
                    bays1 = (List<Bays>)ob.Deserialize(fs);
                    fs.Close();
                    //dtSMBallast= liBallast.toDa
                    DataTable dtbays = CollectionHelper.ConvertTo<Bays>(bays1);
                    //dtTanks = dtSMTanks.Clone();
                    DbCommand command1 = Models.DAL.clsDBUtilityMethods.GetCommand();
                    command1 = Models.DAL.clsDBUtilityMethods.GetCommand();
                    string cmd1 = "";
                    //row["cmbselected"].ToString()=dtbays.Rows;
                    foreach (DataRow row in dtbays.Rows)
                    {

                        cmd1 += " UPDATE [20Ft_Container_Loading]  SET bay='" + row["Bay"].ToString() + "',[cmbselected]="+ row["cmbselected"].ToString() + ",Container_No=" + row["Container_No"].ToString() + ",weight=" + row["weight"].ToString() + ",lcg=" + row["lcg"].ToString() + ",tcg=" + row["tcg"].ToString() + ",vcg=" + row["vcg"].ToString() + ",[Container_Count]='" + row["Container_Count"].ToString() + "' WHERE [Container_No]=" + row["Container_No"].ToString();

                    }
                    //con.Close();
                    command1.CommandText = cmd1;
                    command1.CommandType = CommandType.Text;
                    Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command1, Err);
                    //end
                    //System.Windows.MessageBox.Show(""+Convert.ToInt32(dtbays.Rows[0][12]));
                    if (1 == Convert.ToInt32(dtbays.Rows[0][12]))
                    {
                        Collection objCollection = new Collection();
                        objCollection.CollectionRefresh();
                        MainWindow m = new MainWindow();
                        m.dgContainers20FootInHold.ItemsSource = null;
                        m.dgContainers20FootOnDeck.ItemsSource = null;
                        m.dgContainers20FootInHold.ItemsSource = objCollection.Load20InHoldBaySource;
                        m.dgContainers20FootOnDeck.ItemsSource = objCollection.Load20OnDeckBaySource;
                    }
                }
                catch { }
                try
                {
                    //for container 40
                    fs = new FileStream(path + "\\bays40.cnd", FileMode.Open, FileAccess.Read, FileShare.None);

                    List<Bays> bays2 = new List<Bays>();
                    bays2 = (List<Bays>)ob.Deserialize(fs);
                    fs.Close();
                    //dtSMBallast= liBallast.toDa
                    DataTable dtbays2 = CollectionHelper.ConvertTo<Bays>(bays2);
                    DbCommand command2 = Models.DAL.clsDBUtilityMethods.GetCommand();
                    //dtTanks = dtSMTanks.Clone();
                    command2 = Models.DAL.clsDBUtilityMethods.GetCommand();
                    string cmd2 = "";
                    foreach (DataRow row in dtbays2.Rows)
                    {

                        cmd2 += " UPDATE [40Ft_Container_Loading]  SET bay='" + row["Bay"].ToString() + "',[cmbselected]=" + row["cmbselected"].ToString() + ",Container_No = " + row["Container_No"].ToString() + ",weight=" + row["weight"].ToString() + ",lcg=" + row["lcg"].ToString() + ",tcg=" + row["tcg"].ToString() + ",vcg=" + row["vcg"].ToString() + ",[Container_Count]='" + row["Container_Count"].ToString() + "' WHERE [Container_No]=" + row["Container_No"].ToString();
                    }
                    //con.Close();
                    command2.CommandText = cmd2;
                    command2.CommandType = CommandType.Text;
                    Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command2, Err);
                    //System.Windows.MessageBox.Show("" + Convert.ToInt32(dtbays2.Rows[0][12]));
                    if (1 == Convert.ToInt32(dtbays2.Rows[0][13]))
                    {
                        Collection objCollection = new Collection();
                        //objCollection.CollectionRefresh();
                        MainWindow m = new MainWindow();
                        m.dgContainers40FootInHold.ItemsSource = objCollection.Load40InHoldBaySource;
                        
                    }
                    //end
                }
                catch(Exception ) {
                    //System.Windows.MessageBox.Show(ex.ToString()); 
                    }

                try
                {
                    //for bay 20
                    fs = new FileStream(path + "\\showbays20.cnd", FileMode.Open, FileAccess.Read, FileShare.None);

                    List<Bays> bays3 = new List<Bays>();
                    bays3 = (List<Bays>)ob.Deserialize(fs);
                    fs.Close();
                    //dtSMBallast= liBallast.toDa
                    DataTable dtbays3 = CollectionHelper.ConvertTo<Bays>(bays3);
                    //dtTanks = dtSMTanks.Clone();
                    DbCommand command3 = Models.DAL.clsDBUtilityMethods.GetCommand();
                    command3 = Models.DAL.clsDBUtilityMethods.GetCommand();
                    string cmd2 = "";
                    foreach (DataRow row in dtbays3.Rows)
                    {

                        cmd2 += " UPDATE [20Ft_Showbaywise]  SET bay='" + row["Bay"].ToString() + "',[cmbselected]=" + row["cmbselected"].ToString() + ",Container_No = " + row["Container_No"].ToString() + ",weight=" + row["weight"].ToString() + ",lcg=" + row["lcg"].ToString() + ",tcg=" + row["tcg"].ToString() + ",vcg=" + row["vcg"].ToString() + ",[Container_Count]='" + row["count"].ToString() + "' WHERE [Container_No]=" + row["Container_No"].ToString();
                    }
                    
                    command3.CommandText = cmd2;
                    command3.CommandType = CommandType.Text;
                    Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command3, Err);
                    //con.Close();
                    //end
                }
                catch (Exception) 
                {
                    //System.Windows.MessageBox.Show(ex.ToString());
                }
                try
                {
                    //for bay 40
                    fs = new FileStream(path + "\\showbays40.cnd", FileMode.Open, FileAccess.Read, FileShare.None);

                    List<Bays> bays3 = new List<Bays>();
                    bays3 = (List<Bays>)ob.Deserialize(fs);
                    fs.Close();
                    //dtSMBallast= liBallast.toDa
                    DataTable dtbays3 = CollectionHelper.ConvertTo<Bays>(bays3);
                    //dtTanks = dtSMTanks.Clone();
                    DbCommand command3 = Models.DAL.clsDBUtilityMethods.GetCommand();
                    command3 = Models.DAL.clsDBUtilityMethods.GetCommand();
                    string cmd2 = "";
                    foreach (DataRow row in dtbays3.Rows)
                    {

                        cmd2 += " UPDATE [40Ft_Showbaywise]  SET bay='" + row["Bay"].ToString() + "',[cmbselected]=" + row["cmbselected"].ToString() + ",Container_No = " + row["Container_No"].ToString() + ",weight=" + row["weight"].ToString() + ",lcg=" + row["lcg"].ToString() + ",tcg=" + row["tcg"].ToString() + ",vcg=" + row["vcg"].ToString() + ",[Container_Count]='" + row["count"].ToString() + "' WHERE [Container_No]=" + row["Container_No"].ToString();
                    }
                    //con.Close();
                    command3.CommandText = cmd2;
                    command3.CommandType = CommandType.Text;
                    Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command3, Err);
                    //end
                }
                catch { }
                Models.clsGlobVar.dtSimulationAllTanks = dtTanks;

                
            }
            catch (Exception ex)
            {
               System.Windows.MessageBox.Show(ex.Message);
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (listBoxSavedCondition.SelectedItem != null)
                {
                    MessageBoxResult result = MessageBox.Show("Do you want to Delete Selected Loading Condition ?", "Delete Loading Condition", MessageBoxButton.YesNoCancel);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            Directory.Delete(System.IO.Directory.GetCurrentDirectory() + folder + "\\" + listBoxSavedCondition.SelectedItem.ToString(), true);
                            listBoxSavedCondition.Items.Clear();
                            LoadingConditionList(folder);
                            break;
                        case MessageBoxResult.No:

                            break;
                        case MessageBoxResult.Cancel:

                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Please Select a Loading Condition");
                }

            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void SFcorrection()
        {
            try
            {
                string cmd = "", Err = "";
                DbCommand command = Models.DAL.clsDBUtilityMethods.GetCommand();
                cmd = " DELETE FROM  [tblMaster_Tank] WHERE [Tank_ID] >54";
                cmd += " DELETE FROM  [tblSimulationMode_Loading_Condition]  WHERE [Tank_ID] >54 ";
                cmd += " DELETE FROM  [tblSimulationMode_Tank_Status] WHERE [Tank_ID] >54 ";


                cmd += " DELETE FROM  [tblLoading_Condition] WHERE [Tank_ID] >54 ";
                cmd += " DELETE FROM  [tblTank_Status] WHERE [Tank_ID] >54 ";
                command.CommandText = cmd;
                command.CommandType = CommandType.Text;
                Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command, Err);

                //cmd += " UPDATE tbl_SFCorreFactStdLoadCon SET ACTIVE=0 ";
                if (lblConditionType.Content == "Standard Loading Condition")
                {
                    cmd = "";
                    Err = "";

                    string std = listBoxSavedCondition.SelectedItem.ToString();
                    if (std == "LIGHTSHIP COND")
                    {
                        cmd = " UPDATE tbl_SFCorreFactStdLoadCon SET ACTIVE=1 WHERE Conditions='LIGHTSHIP COND' ";
                    }
                    else if (std == "HOMO. LOAD SCANT. DEP. COND. (S.G.=0.8601)")
                    {
                        cmd = " UPDATE tbl_SFCorreFactStdLoadCon SET ACTIVE=1 WHERE Conditions='HOMO. LOAD SCANT. DEP. COND. (S.G.=0.8601)' ";
                    }
                 
                
                    command.CommandText = cmd;
                    command.CommandType = CommandType.Text;
                    Models.DAL.clsDBUtilityMethods.ExecuteNonQuery(command, Err);
                }

            }
            catch (Exception)
            {


            }
        }

       
    }
}
