using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.ObjectModel;
using System.Reflection;
using ZebecLoadMaster.Models.DAL;
using ZebecLoadMaster.Models.BLL;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Data.SqlClient;

namespace ZebecLoadMaster
{
    
    public class Collection : INotifyCollectionChanged, INotifyPropertyChanged
    {
        ///<summary>
        ///List of Bays
        ///</summary>
        ///<returns></returns>
        //private static ObservableCollection<Bays> _load20InHoldBaySource = new ObservableCollection<Bays>();
        //public static ObservableCollection<Bays> Load20InHoldBaySource
        //{
        //    get { return _load20InHoldBaySource; }
        //    set
        //    {
        //        if (_load20InHoldBaySource != value)
        //        {
        //            _load20InHoldBaySource = value;
        //        }
        //    }
        //}
        private ObservableCollection<Bays> _load20InHoldBaySource = new ObservableCollection<Bays>();
        public ObservableCollection<Bays> Load20InHoldBaySource
        {
            get { return _load20InHoldBaySource; }
            set
            {
                if (_load20InHoldBaySource != value)
                {
                    _load20InHoldBaySource = value;

                    //OnPropertyChanged("Load20InHoldBaySource");
                    OnCollectionChanged(NotifyCollectionChangedAction.Reset);

                }
            }
        }
        private ObservableCollection<Bays> _load20OnDeckBaySource = new ObservableCollection<Bays>();
        public ObservableCollection<Bays> Load20OnDeckBaySource
        {
            get { return _load20OnDeckBaySource; }
            set
            {
                if (_load20OnDeckBaySource != value)
                {
                    _load20OnDeckBaySource = value;

                    //OnPropertyChanged("Load20InHoldBaySource");
                    OnCollectionChanged(NotifyCollectionChangedAction.Reset);

                }
            }
        }
        private ObservableCollection<Bays> _load40InHoldBaySource = new ObservableCollection<Bays>();
        public ObservableCollection<Bays> Load40InHoldBaySource
        {
            get { return _load40InHoldBaySource; }
            set
            {
                if (_load40InHoldBaySource != value)
                {
                    _load40InHoldBaySource = value;

                    //OnPropertyChanged("Load20InHoldBaySource");
                    OnCollectionChanged(NotifyCollectionChangedAction.Reset);

                }
            }
        }
        private ObservableCollection<Bays> _load40OnDeckBaySource = new ObservableCollection<Bays>();
        public ObservableCollection<Bays> Load40OnDeckBaySource
        {
            get { return _load40OnDeckBaySource; }
            set
            {
                if (_load40OnDeckBaySource != value)
                {
                    _load40OnDeckBaySource = value;

                    //OnPropertyChanged("Load20InHoldBaySource");
                    OnCollectionChanged(NotifyCollectionChangedAction.Reset);

                }
            }
        }
        private ObservableCollection<Bays> _loadBaleSource = new ObservableCollection<Bays>();
        public ObservableCollection<Bays> LoadBaleSource
        {
            get { return _loadBaleSource; }
            set
            {
                if (_loadBaleSource != value)
                {
                    _loadBaleSource = value;

                    //OnPropertyChanged("Load20InHoldBaySource");
                    OnCollectionChanged(NotifyCollectionChangedAction.Reset);

                }
            }
        }
        public bool statustrueorfalse;
        public void CollectionRefresh()
        {
            try
            {
                
                 DataTable dtBay = new DataTable();
                ObservableCollection<ZebecLoadMaster.Models.DAL.Stack> _stack = new ObservableCollection<ZebecLoadMaster.Models.DAL.Stack>();
                ObservableCollection<Bays> bays = new ObservableCollection<Bays>();
                // dtBay = clsBLL.GetEnttyDBRecs("vsGetBay20InHoldDetails");
               
                Models.clsGlobVar.dtFinalLoadingCondition1 = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationMode20Ft_Container_Loading");
                Models.clsGlobVar.dtFinalLoadingCondition2 = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationMode40Ft_Container_Loading");
                DataView DV = Models.clsGlobVar.dtFinalLoadingCondition1.AsDataView();
                DataView DV1 = Models.clsGlobVar.dtFinalLoadingCondition2.AsDataView();
                //DV.RowFilter = "Tank_ID >= '46' and Tank_ID <= '61'";
                DV.RowFilter = "LOCATION = 'HOLD'";
                dtBay = DV.ToTable();
                Load20InHoldBaySource.Clear();
                foreach (DataRow row in dtBay.Rows)
                {
                    //statustrueorfalse = false;
                    _stack = LoadStack( row["location"].ToString(), row["Bay"].ToString(), row["statustrueorfalse"].ToString());// COMMENTED FOR TESTING SACHIN11.11.22
                    Bays cd = new Bays();
                    //cd.Tank_Id = Convert.ToInt32(row["Tank_ID"]);
                    cd.Group = row["location"].ToString();
                    //cd.Bay = row["Tank_Name"].ToString();
                    cd.Container_No = Convert.ToInt32(row["Container_No"]);
                    cd.Bay = row["BAY"].ToString();//ADDED BY SACHIN DUE TO NEW COLUMN
                    cd.Weight = Convert.ToDecimal(row["Weight"]);
                    cd.LCG = Convert.ToDecimal(row["LCG"]);
                    cd.VCG = Convert.ToDecimal(row["VCG"]);
                    cd.TCG = Convert.ToDecimal(row["TCG"]);
                    cd.Stack = _stack;
                    cd.Container_ID = Convert.ToInt32(row["Container_ID"]);
                    
                    cd.Count = Convert.ToInt32(row["Container_Count"]);
                    //Load20InHoldBaySource.Add(cd);
                    if (Load20InHoldBaySource.Count < 288 && Load20InHoldBaySource != null)
                    {
                        Load20InHoldBaySource.Add(cd);
                    }
                }
                OnCollectionChanged(NotifyCollectionChangedAction.Reset);
                //dtBay = clsBLL.GetEnttyDBRecs("vsGetBay20OnDeckDetails");
                //DV.RowFilter = "Tank_ID >= '28' and Tank_ID <= '45'";
                DV.RowFilter = "LOCATION = 'HATCH'";
                dtBay = DV.ToTable();
                Load20OnDeckBaySource.Clear();
                foreach (DataRow row in dtBay.Rows)
                {
                    //statustrueorfalse = false;
                    //_stack = LoadStack(Convert.ToInt32(row["Tank_ID"]), row["Group"].ToString(), row["Tank_Name"].ToString());
                    Bays cd = new Bays();
                    // cd.Tank_Id = Convert.ToInt32(row["Tank_ID"]);
                    cd.Group = row["location"].ToString();
                    // cd.Bay = row["Tank_Name"].ToString();
                    cd.Container_No = Convert.ToInt32(row["Container_No"]);
                    cd.Bay = row["BAY"].ToString();
                    cd.Weight = Convert.ToDecimal(row["Weight"]);
                    cd.LCG = Convert.ToDecimal(row["LCG"]);
                    cd.VCG = Convert.ToDecimal(row["VCG"]);
                    cd.TCG = Convert.ToDecimal(row["TCG"]);
                    cd.Container_ID = Convert.ToInt32(row["Container_ID"]);
                    cd.Stack = _stack;
                    cd.Count = Convert.ToInt32(row["Container_Count"]);
                    //Load20OnDeckBaySource.Add(cd);
                    if (Load20OnDeckBaySource.Count < 412 && Load20OnDeckBaySource != null)
                    {
                        Load20OnDeckBaySource.Add(cd);
                    }
                }
                OnCollectionChanged(NotifyCollectionChangedAction.Reset);
                ////dtBay = clsBLL.GetEnttyDBRecs("vsGetBay40InHoldDetails");
                //DV.RowFilter = "Tank_ID >= '28' and Tank_ID <= '75'";
                //dtBay = DV.ToTable();
                DV1.RowFilter = "LOCATION = 'HOLD'";
                dtBay = DV1.ToTable();
                Load40InHoldBaySource.Clear();
                foreach (DataRow row in dtBay.Rows)
                {
                    //statustrueorfalse = null;
                    //statustrueorfalse = true;
                    // _stack = LoadStack(Convert.ToInt32(row["Tank_ID"]), row["Group"].ToString(), row["Tank_Name"].ToString());
                    _stack = LoadStack(row["location"].ToString(), row["Bay"].ToString(), row["statustrueorfalse"].ToString());// COMMENTED FOR TESTING SACHIN11.11.22
                    Bays cd = new Bays();
                    //cd.Tank_Id = Convert.ToInt32(row["Tank_ID"]);
                    cd.Group = row["location"].ToString();
                    // cd.Bay = row["Tank_Name"].ToString();
                    cd.Container_No = Convert.ToInt32(row["Container_No"]);
                    cd.Bay = row["BAY"].ToString();
                    cd.Weight = Convert.ToDecimal(row["Weight"]);
                    cd.LCG = Convert.ToDecimal(row["LCG"]);
                    cd.VCG = Convert.ToDecimal(row["VCG"]);
                    cd.TCG = Convert.ToDecimal(row["TCG"]);
                    cd.Container_ID = Convert.ToInt32(row["Container_ID"]);
                    cd.Stack = _stack;
                    cd.Count = Convert.ToInt32(row["Container_Count"]);
                    //Load40InHoldBaySource.Add(cd);
                    if (Load40InHoldBaySource.Count < 139 && Load40InHoldBaySource != null)
                    {
                        Load40InHoldBaySource.Add(cd);
                    }
                }
                OnCollectionChanged(NotifyCollectionChangedAction.Reset);
                ////dtBay = clsBLL.GetEnttyDBRecs("vsGetBay40OnDeckDetails");
                //DV.RowFilter = "Tank_ID >= '28' and Tank_ID <= '68'";
                //dtBay = DV.ToTable();
                DV1.RowFilter = "LOCATION = 'HATCH'";
                dtBay = DV1.ToTable();
                Load40OnDeckBaySource.Clear();
                foreach (DataRow row in dtBay.Rows)
                {
                    //statustrueorfalse = false;
                    //_stack = LoadStack(Convert.ToInt32(row["Tank_ID"]), row["Group"].ToString(), row["Tank_Name"].ToString());
                    Bays cd = new Bays();
                    //cd.Tank_Id = Convert.ToInt32(row["Tank_ID"]);
                    cd.Group = row["location"].ToString();
                    //cd.Bay = row["Tank_Name"].ToString();
                    cd.Container_No = Convert.ToInt32(row["Container_No"]);
                    cd.Bay = row["BAY"].ToString();
                    cd.Weight = Convert.ToDecimal(row["Weight"]);
                    cd.LCG = Convert.ToDecimal(row["LCG"]);
                    cd.VCG = Convert.ToDecimal(row["VCG"]);
                    cd.TCG = Convert.ToDecimal(row["TCG"]);
                    cd.Container_ID = Convert.ToInt32(row["Container_ID"]);
                    cd.Stack = _stack;
                    cd.Count = Convert.ToInt32(row["Container_Count"]);
                    //Load40OnDeckBaySource.Add(cd);
                    if (Load40OnDeckBaySource.Count < 194 && Load40OnDeckBaySource != null)
                    {
                        Load40OnDeckBaySource.Add(cd);
                    }
                    
                }
                OnCollectionChanged(NotifyCollectionChangedAction.Reset);
                ////dtBay = clsBLL.GetEnttyDBRecs("vsGetSimulationModeBaleAndConstantDetails");
                //DV.RowFilter = "Tank_ID >= '28' and Tank_ID <= '84'";
                //dtBay = DV.ToTable();
                //foreach (DataRow row in dtBay.Rows)
                //{
                //    // _stack = LoadStack(Convert.ToInt32(row["Tank_ID"]), row["Group"].ToString(), row["Tank_Name"].ToString());
                //    Bays cd = new Bays();
                //    cd.Tank_Id = Convert.ToInt32(row["Tank_ID"]);
                //    cd.Group = row["Group"].ToString();
                //    cd.Bay = row["Tank_Name"].ToString();
                //    cd.Weight = Convert.ToDecimal(row["Weight"]);
                //    cd.LCG = Convert.ToDecimal(row["LCG"]);
                //    cd.VCG = Convert.ToDecimal(row["VCG"]);
                //    cd.TCG = Convert.ToDecimal(row["TCG"]);
                //    //cd.Stack = _stack;
                //    //cd.Count = Convert.ToInt32(row["Container_Count"]);
                //    LoadBaleSource.Add(cd);
                //}
                //OnCollectionChanged(NotifyCollectionChangedAction.Reset);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }

        public void CollectionRefreshBAY()
        {
            try
            {

                DataTable dtBay = new DataTable();
                ObservableCollection<ZebecLoadMaster.Models.DAL.Stack> _stack = new ObservableCollection<ZebecLoadMaster.Models.DAL.Stack>();
                ObservableCollection<Bays> bays = new ObservableCollection<Bays>();
                // dtBay = clsBLL.GetEnttyDBRecs("vsGetBay20InHoldDetails");
              
                Models.clsGlobVar.dtFinalLoadingCondition1 = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationModeshowbay20");
              
                Models.clsGlobVar.dtFinalLoadingCondition2 = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationModeshowbay40");
                DataView DV = Models.clsGlobVar.dtFinalLoadingCondition1.AsDataView();
                DataView DV1 = Models.clsGlobVar.dtFinalLoadingCondition2.AsDataView();
                //DV.RowFilter = "Tank_ID >= '46' and Tank_ID <= '61'";
                DV.RowFilter = "LOCATION = 'HOLD'";
                dtBay = DV.ToTable();
                Load20InHoldBaySource.Clear();
                foreach (DataRow row in dtBay.Rows)
                {
                    //statustrueorfalse = false;
                    //_stack = LoadStack(row["location"].ToString(), row["Bay"].ToString(), row["statustrueorfalse"].ToString());// COMMENTED FOR TESTING SACHIN11.11.22
                    Bays cd = new Bays();
                    //cd.Tank_Id = Convert.ToInt32(row["Tank_ID"]);
                    cd.Group = row["location"].ToString();
                    //cd.Bay = row["Tank_Name"].ToString();
                    cd.Container_No = Convert.ToInt32(row["Container_No"]);
                    cd.Bay = row["BAY"].ToString();//ADDED BY SACHIN DUE TO NEW COLUMN
                    cd.Weight = Convert.ToDecimal(row["Weight"]);
                    cd.LCG = Convert.ToDecimal(row["LCG"]);
                    cd.VCG = Convert.ToDecimal(row["VCG"]);
                    cd.TCG = Convert.ToDecimal(row["TCG"]);
                    //cd.Stack = _stack;
                    //cd.Container_ID = Convert.ToInt32(row["Container_ID"]);

                    cd.Count = Convert.ToInt32(row["Container_Count"]);
                    //Load20InHoldBaySource.Add(cd);
                    //do
                    //{
                        if (Load20InHoldBaySource.Count < 14 && Load20InHoldBaySource != null)
                        {
                            Load20InHoldBaySource.Add(cd);
                        }
                    //} 
                    //while (Load20InHoldBaySource.Count < 14 );


                }
                OnCollectionChanged(NotifyCollectionChangedAction.Reset);
                //dtBay = clsBLL.GetEnttyDBRecs("vsGetBay20OnDeckDetails");
                //DV.RowFilter = "Tank_ID >= '28' and Tank_ID <= '45'";
                DV.RowFilter = "LOCATION = 'HATCH'";
                dtBay = DV.ToTable();
                Load20OnDeckBaySource.Clear();
                foreach (DataRow row in dtBay.Rows)
                {
                    //statustrueorfalse = false;
                    //_stack = LoadStack(Convert.ToInt32(row["Tank_ID"]), row["Group"].ToString(), row["Tank_Name"].ToString());
                    Bays cd = new Bays();
                    // cd.Tank_Id = Convert.ToInt32(row["Tank_ID"]);
                    cd.Group = row["location"].ToString();
                    // cd.Bay = row["Tank_Name"].ToString();
                    cd.Container_No = Convert.ToInt32(row["Container_No"]);
                    cd.Bay = row["BAY"].ToString();
                    cd.Weight = Convert.ToDecimal(row["Weight"]);
                    cd.LCG = Convert.ToDecimal(row["LCG"]);
                    cd.VCG = Convert.ToDecimal(row["VCG"]);
                    cd.TCG = Convert.ToDecimal(row["TCG"]);
                    //cd.Container_ID = Convert.ToInt32(row["Container_ID"]);
                    //cd.Stack = _stack;
                    cd.Count = Convert.ToInt32(row["Container_Count"]);
                    //Load20OnDeckBaySource.Add(cd);
                    if (Load20OnDeckBaySource.Count < 15 && Load20OnDeckBaySource != null)
                    {
                        Load20OnDeckBaySource.Add(cd);
                    }
                }
                OnCollectionChanged(NotifyCollectionChangedAction.Reset);
                ////dtBay = clsBLL.GetEnttyDBRecs("vsGetBay40InHoldDetails");
                //DV.RowFilter = "Tank_ID >= '28' and Tank_ID <= '75'";
                //dtBay = DV.ToTable();
                DV1.RowFilter = "LOCATION = 'HOLD'";
                dtBay = DV1.ToTable();
                Load40InHoldBaySource.Clear();
                foreach (DataRow row in dtBay.Rows)
                {
                    //statustrueorfalse = null;
                    //statustrueorfalse = true;
                    // _stack = LoadStack(Convert.ToInt32(row["Tank_ID"]), row["Group"].ToString(), row["Tank_Name"].ToString());
                    //_stack = LoadStack(row["location"].ToString(), row["Bay"].ToString(), row["statustrueorfalse"].ToString());// COMMENTED FOR TESTING SACHIN11.11.22
                    Bays cd = new Bays();
                    //cd.Tank_Id = Convert.ToInt32(row["Tank_ID"]);
                    cd.Group = row["location"].ToString();
                    // cd.Bay = row["Tank_Name"].ToString();
                    cd.Container_No = Convert.ToInt32(row["Container_No"]);
                    cd.Bay = row["BAY"].ToString();
                    cd.Weight = Convert.ToDecimal(row["Weight"]);
                    cd.LCG = Convert.ToDecimal(row["LCG"]);
                    cd.VCG = Convert.ToDecimal(row["VCG"]);
                    cd.TCG = Convert.ToDecimal(row["TCG"]);
                    //cd.Container_ID = Convert.ToInt32(row["Container_ID"]);
                    //cd.Stack = _stack;
                    cd.Count = Convert.ToInt32(row["Container_Count"]);
                    //Load40InHoldBaySource.Add(cd);
                    if (Load40InHoldBaySource.Count < 7 && Load40InHoldBaySource != null)
                    {
                        Load40InHoldBaySource.Add(cd);
                    }
                }
                OnCollectionChanged(NotifyCollectionChangedAction.Reset);
                ////dtBay = clsBLL.GetEnttyDBRecs("vsGetBay40OnDeckDetails");
                //DV.RowFilter = "Tank_ID >= '28' and Tank_ID <= '68'";
                //dtBay = DV.ToTable();
                DV1.RowFilter = "LOCATION = 'HATCH'";
                dtBay = DV1.ToTable();
                Load40OnDeckBaySource.Clear();
                foreach (DataRow row in dtBay.Rows)
                {
                    //statustrueorfalse = false;
                    //_stack = LoadStack(Convert.ToInt32(row["Tank_ID"]), row["Group"].ToString(), row["Tank_Name"].ToString());
                    Bays cd = new Bays();
                    //cd.Tank_Id = Convert.ToInt32(row["Tank_ID"]);
                    cd.Group = row["location"].ToString();
                    //cd.Bay = row["Tank_Name"].ToString();
                    cd.Container_No = Convert.ToInt32(row["Container_No"]);
                    cd.Bay = row["BAY"].ToString();
                    cd.Weight = Convert.ToDecimal(row["Weight"]);
                    cd.LCG = Convert.ToDecimal(row["LCG"]);
                    cd.VCG = Convert.ToDecimal(row["VCG"]);
                    cd.TCG = Convert.ToDecimal(row["TCG"]);
                    //cd.Container_ID = Convert.ToInt32(row["Container_ID"]);
                    //cd.Stack = _stack;
                    cd.Count = Convert.ToInt32(row["Container_Count"]);
                    //Load40OnDeckBaySource.Add(cd);
                    if (Load40OnDeckBaySource.Count < 7 && Load40OnDeckBaySource != null)
                    {
                        Load40OnDeckBaySource.Add(cd);
                    }
                }
                OnCollectionChanged(NotifyCollectionChangedAction.Reset);
                ////dtBay = clsBLL.GetEnttyDBRecs("vsGetSimulationModeBaleAndConstantDetails");
                //DV.RowFilter = "Tank_ID >= '28' and Tank_ID <= '84'";
                //dtBay = DV.ToTable();
                //foreach (DataRow row in dtBay.Rows)
                //{
                //    // _stack = LoadStack(Convert.ToInt32(row["Tank_ID"]), row["Group"].ToString(), row["Tank_Name"].ToString());
                //    Bays cd = new Bays();
                //    cd.Tank_Id = Convert.ToInt32(row["Tank_ID"]);
                //    cd.Group = row["Group"].ToString();
                //    cd.Bay = row["Tank_Name"].ToString();
                //    cd.Weight = Convert.ToDecimal(row["Weight"]);
                //    cd.LCG = Convert.ToDecimal(row["LCG"]);
                //    cd.VCG = Convert.ToDecimal(row["VCG"]);
                //    cd.TCG = Convert.ToDecimal(row["TCG"]);
                //    //cd.Stack = _stack;
                //    //cd.Count = Convert.ToInt32(row["Container_Count"]);
                //    LoadBaleSource.Add(cd);
                //}
                //OnCollectionChanged(NotifyCollectionChangedAction.Reset);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }

        public static ObservableCollection<ZebecLoadMaster.Models.DAL.Stack> LoadStack( string location, string Bay,string statustrueorfalse)
        {
            DataTable dtStack = new DataTable();
            ObservableCollection<ZebecLoadMaster.Models.DAL.Tier> dttier = new ObservableCollection<ZebecLoadMaster.Models.DAL.Tier>();
            //decimal totalWeight = 0;
            //int totalCount = 0;
            //DataView DV = Models.clsGlobVar.dtSimulationMasterTier.AsDataView();
            DataSet ds = new DataSet();
            ds = new DataSet();
            string cmdText = "";
            Collection c = new Collection();
            if ((statustrueorfalse) == "tweetyfeet")
            {
                cmdText = "  SELECT [Container_No],[Weight],[LCG],[LMom],[TCG],[TMom],[VCG],[VMom],[Bay],[Location],[Container_Count] FROM [Saushyant_Stability].[dbo].[20Ft_Container_Loading]";
            }
            if ((statustrueorfalse) == "fortyfeet")
            {
                cmdText = "  SELECT [Container_No],[Weight],[LCG],[LMom],[TCG],[TMom],[VCG],[VMom],[Bay],[Location],[Container_Count] FROM [Saushyant_Stability].[dbo].[40Ft_Container_Loading]";
            }
                //cmdText = "  SELECT [Container_No],[Weight],[LCG],[LMom],[TCG],[TMom],[VCG],[VMom],[Bay],[Location],[Container_Count] FROM [Saushyant_Stability].[dbo].[20Ft_Container_Loading]";
            SqlConnection saConn = new SqlConnection(Models.DAL.clsSqlData.GetConnectionString());
            saConn = new SqlConnection(Models.DAL.clsSqlData.GetConnectionString());
            SqlCommand cmd = new SqlCommand(cmdText, saConn);
            Models.clsGlobVar.DataAdapterMasterTier = new SqlDataAdapter(cmd);
            Models.clsGlobVar.DataAdapterMasterTier.Fill(ds);
            Models.clsGlobVar.dtSimulationMasterTier1 = ds.Tables[0];   
            DataView DV = Models.clsGlobVar.dtSimulationMasterTier1.AsDataView();
           // string[] arr=new string[200];



            DV.RowFilter = "location = '" + location + "' and Bay = '" + Bay + "'";
            dtStack = DV.ToTable();
            //string sCmd = "Select * from [tblMaster_Tier] where [Group]='" + Group + "' and Bay='" + Bay + "'  ";
            //dtStack = clsDAL.GetAllRecsDT(sCmd);
            ObservableCollection<ZebecLoadMaster.Models.DAL.Stack> stack = new ObservableCollection<ZebecLoadMaster.Models.DAL.Stack>();
            int rowcount = dtStack.Rows.Count;
            for (int i = 0; i < rowcount; i++)
            {
                //dttier = LoadTier(Tankid,Group, Bay, dtStack.Rows[i]["Stack"].ToString());
                //totalWeight = dttier.Sum(item => item.Weight);
                //totalCount =  dttier.Sum(item => item.Container_Count);
                string rowcolor = "";
                //string stackname = Convert.ToString(dtStack.Rows[i]["Stack"]);
                //if (Convert.ToInt32(stackname) % 2 == 0)
                //{
                //    rowcolor = "#FF87CEFA";
                //}
                //else
                //{
                //    rowcolor = "#FF9BD8BB";
                //}
                stack.Add(new ZebecLoadMaster.Models.DAL.Stack()
                {
                    //Tank_Id = Tankid,
                    //Stack_Name = Convert.ToString(dtStack.Rows[i]["Stack"]),
                    //Group = Convert.ToString(dtStack.Rows[i]["Group"]),
                    Group = Convert.ToString(dtStack.Rows[i]["location"]),
                    Container_No = Convert.ToInt32(dtStack.Rows[i]["Container_No"]),
                    Bay = Convert.ToString(dtStack.Rows[i]["Bay"]),
                    Container_Count = Convert.ToInt32(dtStack.Rows[i]["Container_Count"]),
                    Weight = Convert.ToDecimal(dtStack.Rows[i]["Weight"]),
                    //Tier_Name = Convert.ToString(dtStack.Rows[i]["Tier"]),
                    LCG = Convert.ToDecimal(dtStack.Rows[i]["LCG"]),
                    VCG = Convert.ToDecimal(dtStack.Rows[i]["VCG"]),
                    TCG = Convert.ToDecimal(dtStack.Rows[i]["TCG"]),
                    RowColor = rowcolor,
                    //Max_Weight = Convert.ToDecimal(dtStack.Rows[i]["Max_Weight"]),
                    //Max_Count = Convert.ToInt32(dtStack.Rows[i]["Max_Count"]),
                    //Total_Weight = totalWeight,
                    //Total_Count=totalCount,
                    //dtTier=dttier,
                });
            }
            return stack;
        }
        //public static ObservableCollection<ZebecLoadMaster.Models.DAL.Stack> LoadStack(int Tankid, string Group, string Bay)
        //{
        //    DataTable dtStack = new DataTable();
        //    ObservableCollection<ZebecLoadMaster.Models.DAL.Tier> dttier = new ObservableCollection<ZebecLoadMaster.Models.DAL.Tier>();
        //    //decimal totalWeight = 0;
        //    //int totalCount = 0;
        //    DataView DV = Models.clsGlobVar.dtSimulationMasterTier.AsDataView();
        //    DV.RowFilter = "Group = '" + Group + "' and Bay = '" + Bay + "'";
        //    dtStack = DV.ToTable();
        //    //string sCmd = "Select * from [tblMaster_Tier] where [Group]='" + Group + "' and Bay='" + Bay + "'  ";
        //    //dtStack = clsDAL.GetAllRecsDT(sCmd);
        //    ObservableCollection<ZebecLoadMaster.Models.DAL.Stack> stack = new ObservableCollection<ZebecLoadMaster.Models.DAL.Stack>();
        //    int rowcount = dtStack.Rows.Count;
        //    for (int i = 0; i < rowcount; i++)
        //    {
        //        //dttier = LoadTier(Tankid,Group, Bay, dtStack.Rows[i]["Stack"].ToString());
        //        //totalWeight = dttier.Sum(item => item.Weight);
        //        //totalCount =  dttier.Sum(item => item.Container_Count);
        //        string rowcolor = "";
        //        string stackname = Convert.ToString(dtStack.Rows[i]["Stack"]);
        //        if (Convert.ToInt32(stackname) % 2 == 0)
        //        {
        //            rowcolor = "#FF87CEFA";
        //        }
        //        else
        //        {
        //            rowcolor = "#FF9BD8BB";
        //        }
        //        stack.Add(new ZebecLoadMaster.Models.DAL.Stack()
        //        {
        //            Tank_Id = Tankid,
        //            Stack_Name = Convert.ToString(dtStack.Rows[i]["Stack"]),
        //            Group = Convert.ToString(dtStack.Rows[i]["Group"]),
        //            Bay = Convert.ToString(dtStack.Rows[i]["Bay"]),
        //            Container_Count = Convert.ToInt32(dtStack.Rows[i]["Container_Count"]),
        //            Weight = Convert.ToDecimal(dtStack.Rows[i]["Weight"]),
        //            Tier_Name = Convert.ToString(dtStack.Rows[i]["Tier"]),
        //            LCG = Convert.ToDecimal(dtStack.Rows[i]["LCG"]),
        //            VCG = Convert.ToDecimal(dtStack.Rows[i]["VCG"]),
        //            TCG = Convert.ToDecimal(dtStack.Rows[i]["TCG"]),
        //            RowColor = rowcolor,
        //            //Max_Weight = Convert.ToDecimal(dtStack.Rows[i]["Max_Weight"]),
        //            //Max_Count = Convert.ToInt32(dtStack.Rows[i]["Max_Count"]),
        //            //Total_Weight = totalWeight,
        //            //Total_Count=totalCount,
        //            //dtTier=dttier,
        //        });
        //    }
        //    return stack;
        //}
        public static ObservableCollection<ZebecLoadMaster.Models.DAL.Tier> LoadTier(int Tankid, string Group, string Bay, string Stack)
        {
            DataTable dtTier = new DataTable();

            string sCmd = "Select [Container_ID],[Group],[Bay],[Stack],[Tier],[Weight],[LCG],[VCG],[TCG],[Container_Count] from [tblMaster_Tier] where [Group]='" + Group + "' and Bay='" + Bay + "' and Stack='" + Stack + "'";
            dtTier = clsDAL.GetAllRecsDT(sCmd);



            ObservableCollection<ZebecLoadMaster.Models.DAL.Tier> tier = new ObservableCollection<ZebecLoadMaster.Models.DAL.Tier>();
            int rowcount = dtTier.Rows.Count;
            for (int i = 0; i < rowcount; i++)
            {

                tier.Add(new ZebecLoadMaster.Models.DAL.Tier()
                {
                    Tank_Id = Tankid,
                    Container_ID = Convert.ToInt32(dtTier.Rows[i]["Container_ID"]),
                    Group = Convert.ToString(dtTier.Rows[i]["Group"]),
                    Bay = Convert.ToString(dtTier.Rows[i]["Bay"]),
                    Stack_Name = Convert.ToString(dtTier.Rows[i]["Stack"]),
                    Tier_Name = Convert.ToString(dtTier.Rows[i]["Tier"]),
                    Container_Count = Convert.ToInt32(dtTier.Rows[i]["Container_Count"]),
                    Weight = Convert.ToDecimal(dtTier.Rows[i]["Weight"]),
                    LCG = Convert.ToDecimal(dtTier.Rows[i]["LCG"]),
                    VCG = Convert.ToDecimal(dtTier.Rows[i]["VCG"]),
                    TCG = Convert.ToDecimal(dtTier.Rows[i]["TCG"]),

                });

            }

            return tier;
        }

        //public List<Bays> Load20OnDeckBays()
        //{
        //    DataTable dtBay20 = new DataTable();
        //    DataTable dtStack20 = new DataTable();
        //    dtBay20 = clsBLL.GetEnttyDBRecs("vsGetBay20OnDeckDetails");

        //    List<Bays> bays = new List<Bays>();
        //    int rowcount = dtBay20.Rows.Count;
        //    for (int i = 0; i < rowcount; i++)
        //    {
        //        List<ZebecLoadMaster.Models.DAL.Stack> stack = new List<ZebecLoadMaster.Models.DAL.Stack>();

        //        bays.Add(new Bays()
        //        {
        //            Tank_Id = Convert.ToInt32(dtBay20.Rows[i]["Tank_ID"]),
        //            Group = dtBay20.Rows[i]["Group"].ToString(),
        //            Bay = dtBay20.Rows[i]["Tank_Name"].ToString(),
        //            Weight = Convert.ToDecimal(dtBay20.Rows[i]["Weight"]),
        //            LCG = Convert.ToDecimal(dtBay20.Rows[i]["LCG"]),
        //            VCG = Convert.ToDecimal(dtBay20.Rows[i]["VCG"]),
        //            TCG = Convert.ToDecimal(dtBay20.Rows[i]["TCG"]),
        //            Stack = ToDataTable(LoadStack(dtBay20.Rows[i]["Group"].ToString(), dtBay20.Rows[i]["Tank_Name"].ToString())),
        //        });
        //    }
        //    return bays;
        //}
        //public List<Bays> Load40InHoldBays()
        //{
        //    DataTable dtBay20 = new DataTable();
        //    DataTable dtStack20 = new DataTable();
        //    dtBay20 = clsBLL.GetEnttyDBRecs("vsGetBay40InHoldDetails");

        //    List<Bays> bays = new List<Bays>();
        //    int rowcount = dtBay20.Rows.Count;
        //    for (int i = 0; i < rowcount; i++)
        //    {
        //        List<ZebecLoadMaster.Models.DAL.Stack> stack = new List<ZebecLoadMaster.Models.DAL.Stack>();

        //        bays.Add(new Bays()
        //        {
        //            Tank_Id = Convert.ToInt32(dtBay20.Rows[i]["Tank_ID"]),
        //            Group = dtBay20.Rows[i]["Group"].ToString(),
        //            Bay = dtBay20.Rows[i]["Tank_Name"].ToString(),
        //            Weight = Convert.ToDecimal(dtBay20.Rows[i]["Weight"]),
        //            LCG = Convert.ToDecimal(dtBay20.Rows[i]["LCG"]),
        //            VCG = Convert.ToDecimal(dtBay20.Rows[i]["VCG"]),
        //            TCG = Convert.ToDecimal(dtBay20.Rows[i]["TCG"]),
        //            Stack = ToDataTable(LoadStack(dtBay20.Rows[i]["Group"].ToString(), dtBay20.Rows[i]["Tank_Name"].ToString())),
        //        });
        //    }
        //    return bays;
        //}
        //public List<Bays> Load40OnDeckBays()
        //{
        //    DataTable dtBay20 = new DataTable();
        //    DataTable dtStack20 = new DataTable();
        //    dtBay20 = clsBLL.GetEnttyDBRecs("vsGetBay40OnDeckDetails");

        //    List<Bays> bays = new List<Bays>();
        //    int rowcount = dtBay20.Rows.Count;
        //    for (int i = 0; i < rowcount; i++)
        //    {
        //        List<ZebecLoadMaster.Models.DAL.Stack> stack = new List<ZebecLoadMaster.Models.DAL.Stack>();

        //        bays.Add(new Bays()
        //        {
        //            Tank_Id = Convert.ToInt32(dtBay20.Rows[i]["Tank_ID"]),
        //            Group = dtBay20.Rows[i]["Group"].ToString(),
        //            Bay = dtBay20.Rows[i]["Tank_Name"].ToString(),
        //            Weight = Convert.ToDecimal(dtBay20.Rows[i]["Weight"]),
        //            LCG = Convert.ToDecimal(dtBay20.Rows[i]["LCG"]),
        //            VCG = Convert.ToDecimal(dtBay20.Rows[i]["VCG"]),
        //            TCG = Convert.ToDecimal(dtBay20.Rows[i]["TCG"]),
        //            Stack = ToDataTable(LoadStack(dtBay20.Rows[i]["Group"].ToString(), dtBay20.Rows[i]["Tank_Name"].ToString())),
        //        });
        //    }
        //    return bays;
        //}

        //public static DataTable ToDataTable<T>(ItemsChangeObservableCollection<T> items)
        //{
        //    DataTable dataTable = new DataTable(typeof(T).Name);

        //    //Get all the properties
        //    PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        //    foreach (PropertyInfo prop in Props)
        //    {
        //        //Setting column names as Property names
        //        dataTable.Columns.Add(prop.Name);
        //    }
        //    foreach (T item in items)
        //    {
        //        var values = new object[Props.Length];
        //        for (int i = 0; i < Props.Length; i++)
        //        {
        //            //inserting property values to datatable rows
        //            values[i] = Props[i].GetValue(item, null);
        //        }
        //        dataTable.Rows.Add(values);
        //    }
        //    //put a breakpoint here and check datatable
        //    return dataTable;
        //}

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public void OnCollectionChanged(NotifyCollectionChangedAction action)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(action));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
