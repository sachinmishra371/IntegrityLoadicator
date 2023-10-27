using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Reflection;
using System.Collections.ObjectModel;
using ZebecLoadMaster.Models.BLL;

namespace ZebecLoadMaster.Models.DAL
{
    [Serializable]
    public class Tanks
    {
        public int Tank_ID { get; set; }
        public string Group { get; set; }
        public string Tank_Name { get; set; }
        public decimal Weight { get; set; }
        public decimal Volume { get; set; }
        public decimal Percent_Full { get; set; }
        public decimal SG { get; set; }
        public decimal FSM { get; set; }
        public Boolean IsDamaged { get; set; }
        public int max_1_act_0 { get; set; }

    }
    [Serializable]
    public class FixedItems
    {
        public int Tank_ID { get; set; }
        public string Tank_Name { get; set; }
        public decimal Weight { get; set; }
        public decimal LCG { get; set; }
        public decimal TCG { get; set; }
        public decimal VCG { get; set; }

    }
    [Serializable]
    public class Bays : INotifyPropertyChanged
    {
        private int _Container_No;
        private int _Container_Count;
        private int _tankid;
        private int _count;
        private int _containerid;
        private int _cmbselected;
        private string _group;
        private string _bay;

        private decimal _weight;
        private decimal _lcg;
        private decimal _vcg;
        private decimal _tcg;
        private ObservableCollection<ZebecLoadMaster.Models.DAL.Stack> _stack;
        public string Tank_Name { get; set; }
        public decimal Volume { get; set; }
        public decimal Percent_Full { get; set; }
        public decimal SG { get; set; }
        public decimal FSM { get; set; }
        public int max_1_act_0 { get; set; }
        public int Tank_Id
        {
            get { return _tankid; }
            set
            {
                if (_tankid != value)
                {
                    _tankid = value;
                    OnPropertyChanged("Tank_Id");
                }
            }
        }
        public int Container_No
        {
            get { return _Container_No; }
            set
            {
                if (_Container_No != value)
                {
                    _Container_No = value;
                    OnPropertyChanged("Container_No");
                }
            }
        }

        public int Container_Count
        {
            get { return _Container_Count; }
            set
            {
                if (_Container_Count != value)
                {
                    _Container_Count = value;
                    OnPropertyChanged("Container_Count");
                }
            }
        }

        
        public int Count
        {
            get { return _count; }
            set
            {
                if (_count != value)
                {
                    _count = value;
                    OnPropertyChanged("Count");
                }
            }
        }
        public string Group
        {
            get { return _group; }
            set
            {
                if (_group != value)
                {
                    _group = value;
                    OnPropertyChanged("Group");
                }
            }
        }
        public string Bay
        {
            get { return _bay; }
            set
            {
                if (_bay != value)
                {
                    _bay = value;
                    OnPropertyChanged("Bay");
                }
            }
        }
        public int Container_ID
        {
            get { return _containerid; }
            set
            {
                if (_containerid != value)
                {
                    _containerid = value;
                    OnPropertyChanged("Container_ID");
                }
            }
        }
        public int CMBSELECTED
        {
            get { return _cmbselected; }
            set
            {
                if (_cmbselected != value)
                {
                    _cmbselected = value;
                    OnPropertyChanged("CMBSELECTED");
                }
            }
        }
        // public int Total_Containers { get; set; }
        public decimal Weight
        {
            get { return _weight; }
            set
            {
                if (_weight != value)
                {
                    _weight = value;
                    OnPropertyChanged("Weight");
                }
            }
        }
        public decimal LCG
        {
            get { return _lcg; }
            set
            {
                if (_lcg != value)
                {
                    _lcg = value;
                    OnPropertyChanged("LCG");
                }
            }
        }
        public decimal VCG
        {
            get { return _vcg; }
            set
            {
                if (_vcg != value)
                {
                    _vcg = value;
                    OnPropertyChanged("VCG");
                }
            }
        }
        public decimal TCG
        {
            get { return _tcg; }
            set
            {
                if (_tcg != value)
                {
                    _tcg = value;
                    OnPropertyChanged("TCG");
                }
            }
        }
        public ObservableCollection<ZebecLoadMaster.Models.DAL.Stack> Stack
        {
            get { return _stack; }
            set
            {
                if (_stack != value)
                {
                    _stack = value;
                    OnPropertyChanged("Stack");
                }
            }
        }
        //public List<Stack> Stack { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propName));
            }
        }
        private static ObservableCollection<Bays> _load20InHoldBaySource = new ObservableCollection<Bays>();
        public static ObservableCollection<Bays> Load20InHoldBaySource
        {
            get { return _load20InHoldBaySource; }
            set
            {
                if (_load20InHoldBaySource != value)
                {
                    _load20InHoldBaySource = value;
                }
            }
        }
        //public Bays()
        //{
        //    DataTable dtBay20 = new DataTable();
        //    DataTable dtStack20 = new DataTable();
        //    dtBay20 = clsBLL.GetEnttyDBRecs("vsGetBay20InHoldDetails");
        //    ObservableCollection<ZebecLoadMaster.Models.DAL.Stack> dtstack = new ObservableCollection<ZebecLoadMaster.Models.DAL.Stack>();
        //    ObservableCollection<Bays> bays = new ObservableCollection<Bays>();
        //    int rowcount = dtBay20.Rows.Count;
        //    decimal totalweight = 0;
        //    foreach (DataRow row in dtBay20.Rows)
        //    {
        //        dtstack = LoadStack(row["Group"].ToString(), row["Tank_Name"].ToString());
        //        totalweight = dtstack.Sum(item => item.Weight);
        //        Bays cd = new Bays();
        //        cd.Tank_Id = Convert.ToInt32(row["Tank_ID"]);
        //        //cd.Group = row["Group"].ToString();
        //        cd.Bay = row["Tank_Name"].ToString();
        //        //cd.Weight = Convert.ToDecimal(row["Weight"]);
        //        cd.LCG = Convert.ToDecimal(row["LCG"]);
        //        cd.VCG = Convert.ToDecimal(row["VCG"]);
        //        cd.TCG = Convert.ToDecimal(row["TCG"]);
        //        cd.Weight = totalweight;
        //        cd.Stack = dtstack;
        //        // cd.Weight = Convert.ToDecimal(dtstack.Rows[dtstack.Rows.Count - 1]["Total_Weight"]);
        //        Load20InHoldBaySource.Add(cd);

        //    }

        //}
        //    public static ObservableCollection<ZebecLoadMaster.Models.DAL.Stack> LoadStack(string Group, string Bay)
        //    {
        //        DataTable dtStack = new DataTable();
        //        ObservableCollection<ZebecLoadMaster.Models.DAL.Tier> dttier = new ObservableCollection<ZebecLoadMaster.Models.DAL.Tier>();
        //        decimal totalWeight = 0;
        //        int totalCount = 0;
        //        string sCmd = "Select [Stack_ID],[Group],[Bay],[Stack],[Container_Count],[Weight],[LCG],[VCG],[TCG],[Max_Weight],[Max_Count] from [tblMaster_Stack] where [Group]='" + Group + "' and Bay='" + Bay + "'  ";
        //        dtStack = clsDAL.GetAllRecsDT(sCmd);
        //        ObservableCollection<ZebecLoadMaster.Models.DAL.Stack> stack = new ObservableCollection<ZebecLoadMaster.Models.DAL.Stack>();
        //        int rowcount = dtStack.Rows.Count;
        //        for (int i = 0; i < rowcount; i++)
        //        {
        //            dttier = LoadTier(Group, Bay, dtStack.Rows[i]["Stack"].ToString());
        //            totalWeight = dttier.Sum(item => item.Weight);
        //            totalCount = dttier.Sum(item => item.Container_Count);
        //            stack.Add(new ZebecLoadMaster.Models.DAL.Stack()
        //            {
        //                Stack_Name = Convert.ToString(dtStack.Rows[i]["Stack"]),
        //                Group = Convert.ToString(dtStack.Rows[i]["Group"]),
        //                Bay = Convert.ToString(dtStack.Rows[i]["Bay"]),
        //                Container_Count = Convert.ToInt32(dtStack.Rows[i]["Container_Count"]),
        //                Weight = Convert.ToDecimal(dtStack.Rows[i]["Weight"]),
        //                //Weight = totalWeight,
        //                LCG = Convert.ToDecimal(dtStack.Rows[i]["LCG"]),
        //                VCG = Convert.ToDecimal(dtStack.Rows[i]["VCG"]),
        //                TCG = Convert.ToDecimal(dtStack.Rows[i]["TCG"]),
        //                Max_Weight = Convert.ToDecimal(dtStack.Rows[i]["Max_Weight"]),
        //                Max_Count = Convert.ToInt32(dtStack.Rows[i]["Max_Count"]),
        //                //Total_Weight = totalWeight,
        //                //Total_Count=totalCount,
        //                dtTier = dttier,
        //            });
        //        }
        //        return stack;
        //    }

        //    public static ObservableCollection<ZebecLoadMaster.Models.DAL.Tier> LoadTier(string Group, string Bay, string Stack)
        //    {
        //        DataTable dtTier = new DataTable();

        //        string sCmd = "Select [Container_ID],[Group],[Bay],[Stack],[Tier],[Weight],[LCG],[VCG],[TCG] from [tblMaster_Tier] where [Group]='" + Group + "' and Bay='" + Bay + "' and Stack='" + Stack + "'";
        //        dtTier = clsDAL.GetAllRecsDT(sCmd);
        //        dtTier.
        //
        //
        //
        //
        //
        //
        //        s.Add("Count");


        //        ObservableCollection<ZebecLoadMaster.Models.DAL.Tier> tier = new ObservableCollection<ZebecLoadMaster.Models.DAL.Tier>();
        //        int rowcount = dtTier.Rows.Count;
        //        for (int i = 0; i < rowcount; i++)
        //        {
        //            dtTier.Rows[i]["Count"] = 0;
        //            tier.Add(new ZebecLoadMaster.Models.DAL.Tier()
        //            {
        //                Container_ID = Convert.ToInt32(dtTier.Rows[i]["Container_ID"]),
        //                Group = Convert.ToString(dtTier.Rows[i]["Group"]),
        //                Bay = Convert.ToString(dtTier.Rows[i]["Bay"]),
        //                Stack_Name = Convert.ToString(dtTier.Rows[i]["Stack"]),
        //                Tier_Name = Convert.ToString(dtTier.Rows[i]["Tier"]),
        //                Container_Count = Convert.ToInt32(dtTier.Rows[i]["Count"]),
        //                Weight = Convert.ToDecimal(dtTier.Rows[i]["Weight"]),
        //                LCG = Convert.ToDecimal(dtTier.Rows[i]["LCG"]),
        //                VCG = Convert.ToDecimal(dtTier.Rows[i]["VCG"]),
        //                TCG = Convert.ToDecimal(dtTier.Rows[i]["TCG"]),

        //            });

        //        }

        //        return tier;
        //    }


    }
    [Serializable]
    public class Stack : INotifyPropertyChanged
    {
        private int _tankid;
        private int _stackid;
        private int _Container_No;
        private int _containercount;
        private string _bay;
        private string _group;
        private string _stackname;
        private string _tiername;
        private decimal _weight;
        private decimal _lcg;
        private decimal _vcg;
        private decimal _tcg;
        private decimal _maxweight;
        private int _maxcount;
        private string _rowColor;
        private decimal _totalweight;
        private int _totalcount;
        private ObservableCollection<ZebecLoadMaster.Models.DAL.Tier> _tier;
        public string RowColor
        {
            get { return _rowColor; }
            set
            {
                if (_rowColor != value)
                {
                    _rowColor = value;
                    OnPropertyChanged("RowColor");
                }
            }
        }
        public int Tank_Id
        {
            get { return _tankid; }
            set
            {
                if (_tankid != value)
                {
                    _tankid = value;
                    OnPropertyChanged("Tank_Id");
                }
            }
        }
        public int Container_No
        {
            get { return _Container_No; }
            set
            {
                if (_Container_No != value)
                {
                    _Container_No = value;
                    OnPropertyChanged("Container_No");
                }
            }
        }
        public int Stack_Id
        {
            get { return _stackid; }
            set
            {
                if (_stackid != value)
                {
                    _stackid = value;
                    OnPropertyChanged("Stack_Id");
                }
            }
        }
        public string Group
        {
            get { return _group; }
            set
            {
                if (_group != value)
                {
                    _group = value;
                    OnPropertyChanged("Group");
                }
            }
        }
        public string Bay
        {
            get { return _bay; }
            set
            {
                if (_bay != value)
                {
                    _bay = value;
                    OnPropertyChanged("Bay");
                }
            }
        }
        public string Stack_Name
        {
            get { return _stackname; }
            set
            {
                if (_stackname != value)
                {
                    _stackname = value;
                    OnPropertyChanged("Stack_Name");
                }
            }
        }
        public string Tier_Name
        {
            get { return _tiername; }
            set
            {
                if (_tiername != value)
                {
                    _tiername = value;
                    OnPropertyChanged("Tier_Name");
                }
            }
        }
        public int Container_Count
        {
            get { return _containercount; }
            set
            {
                if (_containercount != value)
                {
                    _containercount = value;
                    OnPropertyChanged("Container_Count");
                }
            }
        }
        public decimal Weight
        {
            get { return _weight; }
            set
            {
                if (_weight != value)
                {
                    _weight = value;
                    OnPropertyChanged("Weight");
                }
            }
        }
        public decimal LCG
        {
            get { return _lcg; }
            set
            {
                if (_lcg != value)
                {
                    _lcg = value;
                    OnPropertyChanged("LCG");
                }
            }
        }
        public decimal VCG
        {
            get { return _vcg; }
            set
            {
                if (_vcg != value)
                {
                    _vcg = value;
                    OnPropertyChanged("VCG");
                }
            }
        }
        public decimal TCG
        {
            get { return _tcg; }
            set
            {
                if (_tcg != value)
                {
                    _tcg = value;
                    OnPropertyChanged("TCG");
                }
            }
        }
        public decimal Max_Weight
        {
            get { return _maxweight; }
            set
            {
                if (_maxweight != value)
                {
                    _maxweight = value;
                    OnPropertyChanged("Max_Weight");
                }
            }
        }
        public int Max_Count
        {
            get { return _maxcount; }
            set
            {
                if (_maxcount != value)
                {
                    _maxcount = value;
                    OnPropertyChanged("Max_Count");
                }
            }
        }
        public decimal Total_Weight
        {
            get { return _totalweight; }
            set
            {
                if (_totalweight != value)
                {
                    _totalweight = value;
                    OnPropertyChanged("Total_Weight");
                }
            }
        }
        public int Total_Count
        {
            get { return _totalcount; }
            set
            {
                if (_totalcount != value)
                {
                    _totalcount = value;
                    OnPropertyChanged("Total_Count");
                }
            }
        }
        //public DataTable dtTier { get; set; }
        public ObservableCollection<ZebecLoadMaster.Models.DAL.Tier> dtTier
        {
            get { return _tier; }
            set
            {
                if (_tier != value)
                {
                    _tier = value;
                    OnPropertyChanged("dtTier");
                    OnPropertyChanged("Weight");
                    OnPropertyChanged("VCG");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propName));
            }
        }


    }
    [Serializable]
    public class Tier : INotifyPropertyChanged
    {
        private int _tankid;
        private int _containerid;
        private int _containercount;
        private string _bay;
        private string _group;
        private string _stackname;
        private string _tiername;
        private decimal _weight;
        private decimal _lcg;
        private decimal _vcg;
        private decimal _tcg;
        private decimal _totalweight;
        private int _totalcount;
        public int Tank_Id
        {
            get { return _tankid; }
            set
            {
                if (_tankid != value)
                {
                    _tankid = value;
                    OnPropertyChanged("Tank_Id");
                }
            }
        }
        public int Container_ID
        {
            get { return _containerid; }
            set
            {
                if (_containerid != value)
                {
                    _containerid = value;
                    OnPropertyChanged("Container_ID");
                }
            }
        }
        public string Group
        {
            get { return _group; }
            set
            {
                if (_group != value)
                {
                    _group = value;
                    OnPropertyChanged("Group");
                }
            }
        }
        public string Bay
        {
            get { return _bay; }
            set
            {
                if (_bay != value)
                {
                    _bay = value;
                    OnPropertyChanged("Bay");
                }
            }
        }
        public string Stack_Name
        {
            get { return _stackname; }
            set
            {
                if (_stackname != value)
                {
                    _stackname = value;
                    OnPropertyChanged("Stack_Name");
                }
            }
        }
        public string Tier_Name
        {
            get { return _tiername; }
            set
            {
                if (_tiername != value)
                {
                    _tiername = value;
                    OnPropertyChanged("Tier_Name");
                }
            }
        }
        public int Container_Count
        {
            get { return _containercount; }
            set
            {
                if (_containercount != value)
                {
                    _containercount = value;
                    OnPropertyChanged("Container_Count");
                }
            }
        }
        public decimal Weight
        {
            get { return _weight; }
            set
            {
                if (_weight != value)
                {
                    _weight = value;
                    OnPropertyChanged("Weight");

                }
            }
        }
        public decimal LCG
        {
            get { return _lcg; }
            set
            {
                if (_lcg != value)
                {
                    _lcg = value;
                    OnPropertyChanged("LCG");
                }
            }
        }
        public decimal VCG
        {
            get { return _vcg; }
            set
            {
                if (_vcg != value)
                {
                    _vcg = value;
                    OnPropertyChanged("VCG");
                }
            }
        }
        public decimal TCG
        {
            get { return _tcg; }
            set
            {
                if (_tcg != value)
                {
                    _tcg = value;
                    OnPropertyChanged("TCG");
                }
            }
        }
        public decimal Total_Weight
        {
            get { return _totalweight; }
            set
            {
                if (_totalweight != value)
                {
                    _totalweight = value;
                    OnPropertyChanged("Total_Weight");
                }
            }
        }
        public int Total_Count
        {
            get { return _totalcount; }
            set
            {
                if (_totalcount != value)
                {
                    _totalcount = value;
                    OnPropertyChanged("Total_Count");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propName));
            }
        }




    }
    public class CollectionHelper
    {
        private CollectionHelper()
        {
        }

        public static DataTable ConvertTo<T>(IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (T item in list)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }

                table.Rows.Add(row);
            }

            return table;
        }

        public static IList<T> ConvertTo<T>(IList<DataRow> rows)
        {
            IList<T> list = null;

            if (rows != null)
            {
                list = new List<T>();

                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    list.Add(item);
                }
            }

            return list;
        }

        public static IList<T> ConvertTo<T>(DataTable table)
        {
            if (table == null)
            {
                return null;
            }

            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }

            return ConvertTo<T>(rows);
        }

        public static T CreateItem<T>(DataRow row)
        {
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                    try
                    {
                        object value = row[column.ColumnName];
                        prop.SetValue(obj, value, null);
                    }
                    catch
                    {
                        // You can log something here
                        throw;
                    }
                }
            }

            return obj;
        }

        public static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            return table;
        }
    }
}




//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Data;
//using System.ComponentModel;
//using System.Reflection;

//namespace ZebecLoadMaster.Models.DAL
//{
//    [Serializable]
//    public class Tanks
//    {

//        public int Tank_ID { get; set; }
//        public string Group { get; set; }
//        public string Tank_Name { get; set; }
//        public string Weight { get; set; }
//        public string Volume { get; set; }
//        public string Percent_Full { get; set; }
//        public string SG { get; set; }
//        public string FSM { get; set; }
//        public string IsDamaged { get; set; }
//        public string max_1_act_0 { get; set; }
//        public string SoundingLevel { get; set; }
//        public string Temperature { get; set; }
//        public string VCF { get; set; }
//        public string Volume_Corr { get; set; }
//        public string WCF { get; set; }
//        public string Weight_in_Air { get; set; }

//    }
//    [Serializable]
//    public class FixedItems
//    {
//        public int Tank_ID { get; set; }
//        public string Tank_Name { get; set; }
//        public decimal Weight { get; set; }
//        public decimal LCG { get; set; }
//        public decimal TCG { get; set; }
//        public decimal VCG { get; set; }
//        public decimal FSM { get; set; }
//    }
//    public class CollectionHelper
//    {
//        private CollectionHelper()
//        {
//        }

//        public static DataTable ConvertTo<T>(IList<T> list)
//        {
//            DataTable table = CreateTable<T>();
//            Type entityType = typeof(T);
//            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

//            foreach (T item in list)
//            {
//                DataRow row = table.NewRow();

//                foreach (PropertyDescriptor prop in properties)
//                {
//                    row[prop.Name] = prop.GetValue(item);
//                }

//                table.Rows.Add(row);
//            }

//            return table;
//        }

//        public static IList<T> ConvertTo<T>(IList<DataRow> rows)
//        {
//            IList<T> list = null;

//            if (rows != null)
//            {
//                list = new List<T>();

//                foreach (DataRow row in rows)
//                {
//                    T item = CreateItem<T>(row);
//                    list.Add(item);
//                }
//            }

//            return list;
//        }

//        public static IList<T> ConvertTo<T>(DataTable table)
//        {
//            if (table == null)
//            {
//                return null;
//            }

//            List<DataRow> rows = new List<DataRow>();

//            foreach (DataRow row in table.Rows)
//            {
//                rows.Add(row);
//            }

//            return ConvertTo<T>(rows);
//        }

//        public static T CreateItem<T>(DataRow row)
//        {
//            T obj = default(T);
//            if (row != null)
//            {
//                obj = Activator.CreateInstance<T>();

//                foreach (DataColumn column in row.Table.Columns)
//                {
//                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
//                    try
//                    {
//                        object value = row[column.ColumnName];
//                        prop.SetValue(obj, value, null);
//                    }
//                    catch
//                    {
//                        // You can log something here
//                        throw;
//                    }
//                }
//            }

//            return obj;
//        }

//        public static DataTable CreateTable<T>()
//        {
//            Type entityType = typeof(T);
//            DataTable table = new DataTable(entityType.Name);
//            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

//            foreach (PropertyDescriptor prop in properties)
//            {
//                table.Columns.Add(prop.Name, prop.PropertyType);
//            }

//            return table;
//        }
//    }
//}
