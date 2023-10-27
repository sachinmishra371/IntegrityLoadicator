using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using ZebecLoadMaster.Models.DAL;

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.Common;

namespace ZebecLoadMaster
{
    /// <summary>
    /// Interaction logic for SHOWBAYWISE.xaml
    /// </summary>
    public partial class SHOWBAYWISE : Window
    {
        public SHOWBAYWISE()    //hold20
        {
            InitializeComponent();
            CollectionRefresh20inhold();
            Collection bay = new Collection();
            bay.CollectionRefresh();
        }

        public void CollectionRefresh20inhold()
        {
            string[] bayname = new string[14] { "Bay1", "bay3", "bay5", "bay7", "bay9", "bay11", "bay13", "bay15", "bay17", "bay19", "bay21", "bay23", "bay25", "bay27" };
            int i1 = 0;
            dgshwbaywise.IsReadOnly = true;
            Collection objCollection = new Collection();
            objCollection.CollectionRefresh();
            dgshwbaywise.ItemsSource = null;
            //sachin
            DataSet dsbay;
            DataTable dtbay = new DataTable();
            DbCommand command1 = Models.DAL.clsDBUtilityMethods.GetCommand();
            string Err1 = "";
            ObservableCollection<Bays> _load20InHoldBaySourceTemp = new ObservableCollection<Bays>();


            for (int i2 = 0; i2 < bayname.Length; i2++)
            {
                Bays myObj = new Bays();
                string cmd = " select sum(Container_Count),sum(lmom)/sum(weight),sum(VMom)/sum(weight),sum(TMom)/sum(weight),sum(weight) from [20Ft_Container_Loading] where bay='" + bayname[i2] + "' and  location='hold' and weight>0";

                command1.CommandText = cmd;
                command1.CommandType = CommandType.Text;
                dsbay = Models.DAL.clsDBUtilityMethods.GetDataSet(command1, Err1);
                dtbay = dsbay.Tables[0];
                if (dtbay.Rows[0][0] == DBNull.Value)
                {
                    myObj.Bay = bayname[i2];
                    myObj.LCG = 0;
                    myObj.Count = 0;
                    myObj.VCG = 0;
                    myObj.TCG = 0;
                    myObj.Weight = 0;
                    _load20InHoldBaySourceTemp.Add(myObj);

                }
                else
                {
                    //var item = _load20ondeckBaySourceTemp.FirstOrDefault(i => i.Bay == b.Bay);
                    //if (item == null)

                    myObj.Bay = bayname[i2];
                    myObj.LCG = Math.Round(Convert.ToDecimal(dtbay.Rows[0][1]),3);
                    myObj.Count = Convert.ToInt16(dtbay.Rows[0][0]);
                    myObj.VCG = Math.Round(Convert.ToDecimal(dtbay.Rows[0][2]),3);
                    myObj.TCG = Math.Round( Convert.ToDecimal(dtbay.Rows[0][3]),3);
                    myObj.Weight = Convert.ToDecimal(dtbay.Rows[0][4]);
                    i1++;



                    //b.LCG = b.Stack.Average(i => i.LCG);
                    ////sum of lmom/sum of weight

                    //b.VCG = b.Stack.Average(i => i.VCG);

                    //b.TCG = b.Stack.Average(i => i.TCG);
                    //b.Weight = b.Stack.Sum(i => i.Weight);

                    _load20InHoldBaySourceTemp.Add(myObj);
                }
                //}
            }


            //objCollection.dgContainers20FootOnDeck = new ObservableCollection<Bays>(Load20InHoldBaySource.Distinct());
            dgshwbaywise.ItemsSource = _load20InHoldBaySourceTemp;
        }






        // foreach (Bays b in objCollection.Load20InHoldBaySource)
        //{
        // var item = _load20InHoldBaySourceTemp.FirstOrDefault(i => i.Bay == b.Bay);
        // if (item == null)
        // {
        //b.LCG = b.Stack.Average(i => i.LCG);

        //            b.VCG = b.Stack.Average(i => i.VCG);

        //            b.TCG = b.Stack.Average(i => i.TCG);
        //            b.Weight = b.Stack.Sum(i => i.Weight);

        //            _load20InHoldBaySourceTemp.Add(b);
                //}
            //}



            //objCollection.dgContainers20FootOnDeck = new ObservableCollection<Bays>(Load20InHoldBaySource.Distinct());
        //    dgshwbaywise.ItemsSource = _load20InHoldBaySourceTemp;
        //}
       
    }
}
