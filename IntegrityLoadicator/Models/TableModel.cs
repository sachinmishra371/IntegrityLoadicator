using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace ZebecLoadMaster.Models
{
    class TableModel
    {
        public static void RealModeData()
        {
            Models.clsGlobVar.dtRealAllTanks = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetRealModeAllTankFillDetails");
            Models.clsGlobVar.dtRealBallastTanks = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetBallastTankLoadingStatusDetails");
            Models.clsGlobVar.dtRealFuelOilTanks = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetFuelOilTankLoadingStatusDetails");
            Models.clsGlobVar.dtRealCompartments = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetCompartmentLoadingStatusDetails");
            Models.clsGlobVar.dtRealVariableItems = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetVariableDetails");
            Models.clsGlobVar.dtRealEquillibriumValues = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetRealModeEquillibriumValues");
            Models.clsGlobVar.dtRealTrimHeelValues = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetRealModeTrimHeelvalues");
            Models.clsGlobVar.dtRealLoadingSummary = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetRealModeLoadingSummaryCurrent");
            Models.clsGlobVar.dtRealDrafts = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetRealModeDraftsCurrent");
            Models.clsGlobVar.dtRealHydrostatics = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetRealModeHydrostaticDataCurrent");
            Models.clsGlobVar.dtRealStabilityCriteriaIntact = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetRealModeIntactStabilityCriteriaCurrent");
            Models.clsGlobVar.dtRealStabilitySummary = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetRealModeStabilitySummary");
            Models.clsGlobVar.dtRealLongitudinal = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetRealModeLoadingSummaryCurrent");
            Models.clsGlobVar.dtRealSFBMMax = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetRealModeSFBMMax");
            Models.clsGlobVar.dtlighshipvalues = Models.BLL.clsBLL.GetEnttyDBRecs("vsgetlightshipdata");
            #region SP_Calling
            {
                Models.clsGlobVar.FloodingPoint_Damage = Models.DAL.clsDAL.SP_Execution("[spGet_flooding_points_damage]");
                Models.clsGlobVar.FloodingPoint_Intact = Models.DAL.clsDAL.SP_Execution("[spGet_flooding_points_intact]");
            }
            #endregion

            string sCmd = "Select Sum(Weight) From [tblLoading_Condition] Where [USER] = 'dbo' and Tank_ID BETWEEN 1 AND 14"; //Cargo Tanks
            sCmd += " Select Sum(Weight) From [tblLoading_Condition] Where [USER] = 'dbo' and Tank_ID BETWEEN 15 AND 28"; //BALLAST_TANK
            sCmd += " Select Sum(Weight) From [tblLoading_Condition] Where [USER] = 'dbo' and Tank_ID BETWEEN 29 AND 33"; //Fuel_OIL_TANK
            sCmd += " Select Sum(Weight) From [tblLoading_Condition] Where [USER] = 'dbo' and Tank_ID BETWEEN 34 AND 35"; //FRESHWATER_TANK  
            sCmd += " Select Sum(Weight) From [tblLoading_Condition] Where [USER] = 'dbo' and Tank_ID BETWEEN 36 AND 39"; //Diseal_oil_tanks
            sCmd += " Select Sum(Weight) From [tblLoading_Condition] Where [USER] = 'dbo' and Tank_ID between 40 and 43"; //LUB_OIL
            sCmd += " Select Sum(Weight) From [tblLoading_Condition] Where [USER] = 'dbo' and Tank_ID between 44 and 49"; //MISC_TANK
            sCmd += " Select Sum(Weight) From [tblLoading_Condition] Where [USER] = 'dbo' and Tank_ID in (Select Tank_ID from tblMaster_Tank where [group]= 'FIXED_WEIGHT')"; //Fixed Load
            sCmd += " Select Sum(Weight) From [tblLoading_Condition] Where [USER] = 'dbo' and Tank_ID=54 "; //LIGHTSHIP
            DbCommand command = Models.DAL.clsDBUtilityMethods.GetCommand();
            command.CommandText = sCmd;
            command.CommandType = CommandType.Text;
            string Err = "";
            Models.clsGlobVar.dsRealDeadWeightDetails = Models.DAL.clsDBUtilityMethods.GetDataSet(command, Err);
        }
        public static DataTable JoinDataTables(DataTable t1, DataTable t2, params Func<DataRow, DataRow, bool>[] joinOn)
        {
            DataTable result = new DataTable();
            foreach (DataColumn col in t1.Columns)
            {
                if (result.Columns[col.ColumnName] == null)
                    result.Columns.Add(col.ColumnName, col.DataType);
            }
            foreach (DataColumn col in t2.Columns)
            {
                if (result.Columns[col.ColumnName] == null)
                    result.Columns.Add(col.ColumnName, col.DataType);
            }
            foreach (DataRow row1 in t1.Rows)
            {
                var joinRows = t2.AsEnumerable().Where(row2 =>
                {
                    foreach (var parameter in joinOn)
                    {
                        if (!parameter(row1, row2)) return false;
                    }
                    return true;
                });
                foreach (DataRow fromRow in joinRows)
                {
                    DataRow insertRow = result.NewRow();
                    foreach (DataColumn col1 in t1.Columns)
                    {
                        insertRow[col1.ColumnName] = row1[col1.ColumnName];
                    }
                    foreach (DataColumn col2 in t2.Columns)
                    {
                        insertRow[col2.ColumnName] = fromRow[col2.ColumnName];
                    }
                    result.Rows.Add(insertRow);
                }
            }
            return result;
        }
        public static void SimulationModeData()
        {
            //// sachin added for container 
            ///

            DataSet ds = new DataSet();
            string cmdText = "";
            cmdText = "SELECT Tank_ID,[Group],Tank_Name FROM tblMaster_Tank";
            SqlConnection saConn = new SqlConnection(Models.DAL.clsSqlData.GetConnectionString());
            SqlCommand cmd = new SqlCommand(cmdText, saConn);
            clsGlobVar.DataAdapterMasterTank = new SqlDataAdapter(cmd);
            clsGlobVar.DataAdapterMasterTank.Fill(ds);
            Models.clsGlobVar.dtSimulationMasterTank = ds.Tables[0];

            ds = new DataSet();
            cmdText = "";
            cmdText = "SELECT Tank_ID,Weight, Volume, Percent_Full,SG,FSM,Container_Count,LCG,VCG,TCG FROM tblSimulationMode_Loading_Condition";
            saConn = new SqlConnection(Models.DAL.clsSqlData.GetConnectionString());
            cmd = new SqlCommand(cmdText, saConn);
            clsGlobVar.DataAdapterLoadingCondition = new SqlDataAdapter(cmd);
            clsGlobVar.DataAdapterLoadingCondition.Fill(ds);
            Models.clsGlobVar.dtSimulationLoadingCondition = ds.Tables[0];

            ds = new DataSet();
            cmdText = "";
            cmdText = "SELECT * FROM tblSimulationMode_Tank_Status";
            saConn = new SqlConnection(Models.DAL.clsSqlData.GetConnectionString());
            cmd = new SqlCommand(cmdText, saConn);
            clsGlobVar.DataAdapterTankStatus = new SqlDataAdapter(cmd);
            clsGlobVar.DataAdapterTankStatus.Fill(ds);
            Models.clsGlobVar.dtSimulationTankStatus = ds.Tables[0];

            ds = new DataSet();
            cmdText = "";
            cmdText = "SELECT [Stack_ID],[Group],[Bay],[Stack],[Container_Count],[Weight],[LCG],[VCG],[TCG],[Max_Weight] FROM tblMaster_Stack";
            saConn = new SqlConnection(Models.DAL.clsSqlData.GetConnectionString());
            cmd = new SqlCommand(cmdText, saConn);
            clsGlobVar.DataAdapterMasterStack = new SqlDataAdapter(cmd);
            clsGlobVar.DataAdapterMasterStack.Fill(ds);
            Models.clsGlobVar.dtSimulationMasterStack = ds.Tables[0];

            ds = new DataSet();
            cmdText = "";
            cmdText = "SELECT [Container_ID],[Group],[Bay],[Stack],[Tier],[Weight],[LCG],[VCG],[TCG],[Container_Count],[Flag] FROM tblMaster_Tier";
            saConn = new SqlConnection(Models.DAL.clsSqlData.GetConnectionString());
            cmd = new SqlCommand(cmdText, saConn);
            clsGlobVar.DataAdapterMasterTier = new SqlDataAdapter(cmd);
            clsGlobVar.DataAdapterMasterTier.Fill(ds);
            Models.clsGlobVar.dtSimulationMasterTier = ds.Tables[0];
            //-----------sachin added---------
            ds = new DataSet();
            cmdText = "";
            cmdText = "  SELECT [Container_No],[Weight],[LCG],[LMom],[TCG],[TMom],[VCG],[VMom],[Bay],[Location],[Container_Count] FROM [Saushyant_Stability].[dbo].[20Ft_Container_Loading]";
            saConn = new SqlConnection(Models.DAL.clsSqlData.GetConnectionString());
            cmd = new SqlCommand(cmdText, saConn);
            clsGlobVar.DataAdapterMasterTier = new SqlDataAdapter(cmd);
            clsGlobVar.DataAdapterMasterTier.Fill(ds);
            Models.clsGlobVar.dtSimulationMasterTier1 = ds.Tables[0];
            //-----------------end -----------

            ds = new DataSet();
            cmdText = "";
            cmdText = "SELECT * FROM tblFSM_max_act";
            saConn = new SqlConnection(Models.DAL.clsSqlData.GetConnectionString());
            cmd = new SqlCommand(cmdText, saConn);
            clsGlobVar.DataAdapterFSM = new SqlDataAdapter(cmd);
            clsGlobVar.DataAdapterFSM.Fill(ds);
            Models.clsGlobVar.dtSimulationFSM = ds.Tables[0];

            Models.clsGlobVar.dtFinalLoadingCondition = JoinDataTables(Models.clsGlobVar.dtSimulationMasterTank, Models.clsGlobVar.dtSimulationLoadingCondition,
                                                                   (row1, row2) => row1.Field<int>("Tank_ID") == row2.Field<int>("Tank_ID")); //COMMENTED BY SACHIN 11.11.22
            //Models.clsGlobVar.dtFinalLoadingCondition1 = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationMode20Ft_Container_Loading"); //CODE BY SACHIN 11.11.22
            //Models.clsGlobVar.dtFinalLoadingCondition2 = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationMode40Ft_Container_Loading"); //CODE BY SACHIN 11.11.22
            Models.clsGlobVar.dtSimulationAllTanks = Models.BLL.clsBLL.GetEnttyDBRecs("vstank");
            //Models.clsGlobVar.dtSimulationAllTanks = JoinDataTables(Models.clsGlobVar.dtFinalLoadingCondition, Models.clsGlobVar.dtSimulationFSM,
            //                                                          (row1, row2) => row1.Field<int>("Tank_ID") == row2.Field<int>("Tank_ID"));
            Models.clsGlobVar.dtFinalTankStatus = JoinDataTables(Models.clsGlobVar.dtSimulationMasterTank, Models.clsGlobVar.dtSimulationTankStatus,
                                                                        (row1, row2) => row1.Field<int>("Tank_ID") == row2.Field<int>("Tank_ID"));
            //




           // Models.clsGlobVar.dtSimulationAllTanks = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationModeAllTankFillDetails");
            Models.clsGlobVar.dtSimulationAllTanksForDamage = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationModeAllTankFillDetailsForDamage");
            Models.clsGlobVar.dtSimulationBallastTanks = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationBallastTankLoadingStatusDetails");
            Models.clsGlobVar.dtSimulationFuelOilTanks = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationFuelOilTankLoadingStatusDetails");
            Models.clsGlobVar.dtSimulationMiscTanks = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationMiscTankLoadingStatusDetails");
            Models.clsGlobVar.dtSimulationCompartments = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationCompartmentLoadingStatusDetails");
            Models.clsGlobVar.dtSimulationVariableItems = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationModeVariableDetails");
            Models.clsGlobVar.dtSimulationfixedload = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationModefixedload");//simultaionfixed 
            
            Models.clsGlobVar.dtSimulationEquillibriumValues = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationModeEquillibriumValues");
            Models.clsGlobVar.dtSimulationLoadingSummary = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationModeLoadingSummaryCurrent");
            Models.clsGlobVar.dtSimulationDrafts = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationModeDraftsCurrent");
            Models.clsGlobVar.dtSimulationHydrostatics = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationModeHydrostaticDataCurrent");
            Models.clsGlobVar.dtSimulationStabilityCriteriaIntact = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationModeIntactStabilityCriteriaCurrent");
            Models.clsGlobVar.dtSimulationStabilityCriteriaDamage = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationModeDamageStabilityCriteriaCurrent");
            Models.clsGlobVar.dtSimulationStabilitySummary = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationModeStabilitySummary");
            Models.clsGlobVar.dtSimulationLongitudinal = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationModeLongitudinalDataCurrent");
            Models.clsGlobVar.dtSimulationSFBMMax = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationModeSFBMMax");
            Models.clsGlobVar.dtlighshipvalues = Models.BLL.clsBLL.GetEnttyDBRecs("vsgetlightshipdata");
            Models.clsGlobVar.dtshowbay20 = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationModeshowbay20");
            Models.clsGlobVar.dtshowbay40 = Models.BLL.clsBLL.GetEnttyDBRecs("vsGetSimulationModeshowbay40");

            #region SP_Calling
            {
                Models.clsGlobVar.FloodingPoint_Damage = Models.DAL.clsDAL.SP_Execution("[spGet_flooding_points_damage]");
                Models.clsGlobVar.FloodingPoint_Intact = Models.DAL.clsDAL.SP_Execution("[spGet_flooding_points_intact]");
            }
            #endregion
            //DEADWEIGHT DETAILS
            string sCmd = "Select Sum(Weight) From [tblSimulationMode_Loading_Condition] Where [USER] = 'dbo' and Tank_ID BETWEEN 34 AND 45"; //Cargo Tanks
            sCmd += " Select Sum(Weight) From [tblSimulationMode_Loading_Condition] Where [USER] = 'dbo' and Tank_ID BETWEEN 1 AND 11"; //BALLAST_TANK
            sCmd += " Select Sum(Weight) From [tblSimulationMode_Loading_Condition] Where [USER] = 'dbo' and Tank_ID BETWEEN 17 AND 22"; //Fuel_OIL_TANK
            sCmd += " Select Sum(Weight) From [tblSimulationMode_Loading_Condition] Where [USER] = 'dbo' and Tank_ID BETWEEN 12 AND 16"; //FRESHWATER_TANK  
            sCmd += " Select Sum(Weight) From [tblSimulationMode_Loading_Condition] Where [USER] = 'dbo' and Tank_ID BETWEEN 23 AND 24"; //Diseal_oil_tanks
            //sCmd += " Select Sum(Weight) From [tblSimulationMode_Loading_Condition] Where [USER] = 'dbo' and Tank_ID between 40 and 43"; //LUB_OIL
            sCmd += " Select Sum(Weight) From [tblSimulationMode_Loading_Condition] Where [USER] = 'dbo' and Tank_ID between 25 and 33"; //MISC_TANK
            sCmd += " Select Sum(Weight) From [tblSimulationMode_Loading_Condition] Where [USER] = 'dbo' and Tank_ID in (Select Tank_ID from tblMaster_Tank where [group]= 'FIXED_WEIGHT')"; //Fixed Load
            sCmd += " Select Sum(Weight) From [tblSimulationMode_Loading_Condition] Where [USER] = 'dbo' and Tank_ID=46 "; //LIGHTSHIP
            DbCommand command = Models.DAL.clsDBUtilityMethods.GetCommand();
            command.CommandText = sCmd;
            command.CommandType = CommandType.Text;
            string Err = "";
            Models.clsGlobVar.dsSimulationDeadWeightDetails = Models.DAL.clsDBUtilityMethods.GetDataSet(command, Err);
             string sCmdD = "Select Sum(Weight) From [tblSimulationMode_Loading_Condition] Where [USER] = 'dbo' and Tank_ID BETWEEN 25 AND 33";
            DbCommand command1 = Models.DAL.clsDBUtilityMethods.GetCommand();
            command1.CommandText = sCmdD;
            command1.CommandType = CommandType.Text;
            string Err1 = "";
            Models.clsGlobVar.dsSimulationDeadWeightDetails1 = Models.DAL.clsDBUtilityMethods.GetDataSet(command1, Err1);


            //string sCmdVol = "Select Sum(Volume) From [tblSimulationMode_Loading_Condition] Where [USER] = 'dbo' and Tank_ID BETWEEN 1 AND 14"; //Cargo Tanks
            //sCmdVol += " Select Sum(Volume) From [tblSimulationMode_Loading_Condition] Where [USER] = 'dbo' and Tank_ID BETWEEN 15 AND 28"; //BALLAST_TANK
            //sCmdVol += " Select Sum(Volume) From [tblSimulationMode_Loading_Condition] Where [USER] = 'dbo' and Tank_ID BETWEEN 29 AND 33"; //Fuel_OIL_TANK
            //sCmdVol += " Select Sum(Volume) From [tblSimulationMode_Loading_Condition] Where [USER] = 'dbo' and Tank_ID BETWEEN 34 AND 35"; //FRESHWATER_TANK  
            //sCmdVol += " Select Sum(Volume) From [tblSimulationMode_Loading_Condition] Where [USER] = 'dbo' and Tank_ID BETWEEN 36 AND 39"; //Diseal_oil_tanks
            //sCmdVol += " Select Sum(Volume) From [tblSimulationMode_Loading_Condition] Where [USER] = 'dbo' and Tank_ID between 40 and 43"; //LUB_OIL
            //sCmdVol += " Select Sum(Volume) From [tblSimulationMode_Loading_Condition] Where [USER] = 'dbo' and Tank_ID between 44 and 49"; //MISC_TANK
            //sCmdVol += " Select Sum(Volume) From [tblSimulationMode_Loading_Condition] Where [USER] = 'dbo' and Tank_ID in (Select Tank_ID from tblMaster_Tank where [group]= 'FIXED_WEIGHT')";
            //DbCommand commandVol = Models.DAL.clsDBUtilityMethods.GetCommand();
            //commandVol.CommandText = sCmdVol;
            //commandVol.CommandType = CommandType.Text;
            string Error = "";
            //Models.clsGlobVar.dsSimulationDWVolumes = Models.DAL.clsDBUtilityMethods.GetDataSet(commandVol, Error);


            string sCmdVolCorrection = "Select Sum(Volume_Corr) From [tblSimulationMode_Tank_Status] Where [USER] = 'dbo' and Tank_ID BETWEEN 1 AND 14"; //Cargo Tanks
            sCmdVolCorrection += " Select Sum(Volume_Corr) From [tblSimulationMode_Tank_Status] Where [USER] = 'dbo' and Tank_ID BETWEEN 15 AND 28"; //BALLAST_TANK
            sCmdVolCorrection += " Select Sum(Volume_Corr) From [tblSimulationMode_Tank_Status] Where [USER] = 'dbo' and Tank_ID BETWEEN 29 AND 33"; //Fuel_OIL_TANK
            sCmdVolCorrection += " Select Sum(Volume_Corr) From [tblSimulationMode_Tank_Status] Where [USER] = 'dbo' and Tank_ID BETWEEN 34 AND 35"; //FRESHWATER_TANK  
            sCmdVolCorrection += " Select Sum(Volume_Corr) From [tblSimulationMode_Tank_Status] Where [USER] = 'dbo' and Tank_ID BETWEEN 36 AND 39"; //Diseal_oil_tanks
            sCmdVolCorrection += " Select Sum(Volume_Corr) From [tblSimulationMode_Tank_Status] Where [USER] = 'dbo' and Tank_ID between 40 and 43"; //LUB_OIL
            sCmdVolCorrection += " Select Sum(Volume_Corr) From [tblSimulationMode_Tank_Status] Where [USER] = 'dbo' and Tank_ID between 44 and 49"; //MISC_TANK
            sCmdVolCorrection += " Select Sum(Volume_Corr) From [tblSimulationMode_Tank_Status] Where [USER] = 'dbo' and Tank_ID in (Select Tank_ID from tblMaster_Tank where [group]= 'FIXED_WEIGHT')";
            DbCommand commandVolCorrection = Models.DAL.clsDBUtilityMethods.GetCommand();
            commandVolCorrection.CommandText = sCmdVolCorrection;
            commandVolCorrection.CommandType = CommandType.Text;
            Error = "";
            Models.clsGlobVar.dsSimulationDWVolumesCorretion = Models.DAL.clsDBUtilityMethods.GetDataSet(commandVolCorrection, Error);


        }

        public static void CoordinateData()
        {
            try
            {
                string sCmd = "Select * from [DeckPlan] "; 
                 sCmd += "Select * from [Profile_View]";
                DbCommand command = ZebecLoadMaster.Models.DAL.clsDBUtilityMethods.GetCommand();
                command.CommandText = sCmd;
                command.CommandType = CommandType.Text;
                string Err = "";
                Models.clsGlobVar.dsCoordinates = ZebecLoadMaster.Models.DAL.clsDBUtilityMethods.GetDataSet(command, Err);
                DataTable dtCoordinatesDeckPlan = new DataTable();
                dtCoordinatesDeckPlan = Models.clsGlobVar.dsCoordinates.Tables[0];
                for (int i = 1; i <= 11; i++)
                {
                    string sc = Convert.ToString("X" + i);
                    string sr = Convert.ToString("Y" + i);

                    Models.clsGlobVar.Tank1x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[0][sc]);
                    Models.clsGlobVar.Tank2x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[1][sc]);
                    Models.clsGlobVar.Tank3x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[2][sc]);
                    Models.clsGlobVar.Tank4x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[3][sc]);
                    Models.clsGlobVar.Tank5x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[4][sc]);
                    Models.clsGlobVar.Tank6x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[5][sc]);
                    Models.clsGlobVar.Tank7x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[6][sc]);
                    Models.clsGlobVar.Tank8x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[7][sc]);
                    Models.clsGlobVar.Tank9x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[8][sc]);
                    Models.clsGlobVar.Tank10x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[9][sc]);
                    Models.clsGlobVar.Tank11x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[10][sc]);
                    Models.clsGlobVar.Tank12x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[11][sc]);
                    Models.clsGlobVar.Tank13x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[11][sc]);
                    Models.clsGlobVar.Tank14x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[12][sc]);
                    Models.clsGlobVar.Tank15x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[13][sc]);
                    Models.clsGlobVar.Tank16x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[14][sc]);
                    Models.clsGlobVar.Tank17x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[15][sc]);
                    Models.clsGlobVar.Tank18x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[16][sc]);
                    Models.clsGlobVar.Tank19x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[17][sc]);
                    Models.clsGlobVar.Tank20x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[18][sc]);
                    Models.clsGlobVar.Tank21x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[19][sc]);
                    Models.clsGlobVar.Tank22x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[20][sc]);
                    Models.clsGlobVar.Tank23x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[21][sc]);
                    Models.clsGlobVar.Tank24x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[22][sc]);
                    Models.clsGlobVar.Tank25x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[23][sc]);
                    //Models.clsGlobVar.Tank26x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[25][sc]);
                    Models.clsGlobVar.Tank27x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[24][sc]);
                    Models.clsGlobVar.Tank28x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[25][sc]);
                    Models.clsGlobVar.Tank29x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[26][sc]);
                    Models.clsGlobVar.Tank30x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[27][sc]);
                    //Models.clsGlobVar.Tank31x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[30][sc]);
                    //Models.clsGlobVar.Tank32x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[25][sc]);
                    Models.clsGlobVar.Tank33x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[28][sc]);
                    //Models.clsGlobVar.Tank34x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[27][sc]);
                    //Models.clsGlobVar.Tank35x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[28][sc]);
                    //Models.clsGlobVar.Tank36x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[29][sc]);
                    //Models.clsGlobVar.Tank37x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[30][sc]);
                    //Models.clsGlobVar.Tank38x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[37][sc]);
                    //Models.clsGlobVar.Tank39x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[38][sc]);
                    //Models.clsGlobVar.Tank40x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[39][sc]);
                    //Models.clsGlobVar.Tank41x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[40][sc]);
                    //Models.clsGlobVar.Tank42x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[41][sc]);
                    //Models.clsGlobVar.Tank43x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[42][sc]);
                    //Models.clsGlobVar.Tank44x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[43][sc]);
                    //Models.clsGlobVar.Tank45x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[44][sc]);
                    //Models.clsGlobVar.Tank46x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[45][sc]);
                    //Models.clsGlobVar.Tank47x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[46][sc]);
                    //Models.clsGlobVar.Tank48x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[47][sc]);
                    //Models.clsGlobVar.Tank49x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[48][sc]);
                    //Models.clsGlobVar.Tank50x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[49][sc]);
                    //Models.clsGlobVar.Tank51x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[50][sc]);
                    //Models.clsGlobVar.Tank52x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[51][sc]);
                    //Models.clsGlobVar.Tank53x[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[52][sc]);








                    Models.clsGlobVar.Tank1y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[0][sr]);
                    Models.clsGlobVar.Tank2y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[1][sr]);
                    Models.clsGlobVar.Tank3y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[2][sr]);
                    Models.clsGlobVar.Tank4y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[3][sr]);
                    Models.clsGlobVar.Tank5y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[4][sr]);
                    Models.clsGlobVar.Tank6y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[5][sr]);
                    Models.clsGlobVar.Tank7y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[6][sr]);
                    Models.clsGlobVar.Tank8y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[7][sr]);
                    Models.clsGlobVar.Tank9y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[8][sr]);
                    Models.clsGlobVar.Tank10y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[9][sr]);
                    Models.clsGlobVar.Tank11y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[10][sr]);
                    Models.clsGlobVar.Tank12y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[11][sr]);
                    Models.clsGlobVar.Tank13y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[11][sr]);
                    Models.clsGlobVar.Tank14y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[12][sr]);
                    Models.clsGlobVar.Tank15y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[13][sr]);
                    Models.clsGlobVar.Tank16y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[14][sr]);
                    Models.clsGlobVar.Tank17y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[15][sr]);
                    Models.clsGlobVar.Tank18y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[16][sr]);
                    Models.clsGlobVar.Tank19y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[17][sr]);
                    Models.clsGlobVar.Tank20y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[18][sr]);
                    Models.clsGlobVar.Tank21y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[19][sr]);
                    Models.clsGlobVar.Tank22y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[20][sr]);
                    Models.clsGlobVar.Tank23y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[21][sr]);
                    Models.clsGlobVar.Tank24y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[22][sr]);
                    Models.clsGlobVar.Tank25y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[23][sr]);
                   // Models.clsGlobVar.Tank26y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[25][sr]);
                    Models.clsGlobVar.Tank27y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[24][sr]);
                    Models.clsGlobVar.Tank28y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[25][sr]);
                    Models.clsGlobVar.Tank29y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[26][sr]);
                    Models.clsGlobVar.Tank30y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[27][sr]);
                    //Models.clsGlobVar.Tank31y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[30][sr]);
                    //Models.clsGlobVar.Tank32y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[25][sr]);
                    Models.clsGlobVar.Tank33y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[28][sr]);
                    //Models.clsGlobVar.Tank34y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[27][sr]);
                    //Models.clsGlobVar.Tank35y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[28][sr]);
                    //Models.clsGlobVar.Tank36y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[29][sr]);
                    //Models.clsGlobVar.Tank37y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[30][sr]);
                    //Models.clsGlobVar.Tank38y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[37][sr]);
                    //Models.clsGlobVar.Tank39y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[38][sr]);
                    //Models.clsGlobVar.Tank40y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[39][sr]);
                    //Models.clsGlobVar.Tank41y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[40][sr]);
                    //Models.clsGlobVar.Tank41y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[40][sc]);
                    //Models.clsGlobVar.Tank42y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[41][sc]);
                    //Models.clsGlobVar.Tank43y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[42][sc]);
                    //Models.clsGlobVar.Tank44y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[43][sc]);
                    //Models.clsGlobVar.Tank45y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[44][sc]);
                    //Models.clsGlobVar.Tank46y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[45][sc]);
                    //Models.clsGlobVar.Tank47y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[46][sc]);
                    //Models.clsGlobVar.Tank48y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[47][sc]);
                    //Models.clsGlobVar.Tank49y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[48][sc]);
                    //Models.clsGlobVar.Tank50y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[49][sc]);
                    //Models.clsGlobVar.Tank51y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[50][sc]);
                    //Models.clsGlobVar.Tank52y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[51][sc]);
                    //Models.clsGlobVar.Tank53y[i] = Convert.ToDouble(dtCoordinatesDeckPlan.Rows[52][sc]);

                }


                DataTable dtCoordinatesProfile = new DataTable();
                dtCoordinatesProfile = Models.clsGlobVar.dsCoordinates.Tables[1];
                for (int i = 1; i <= 4; i++)
                {
                    string sc = Convert.ToString("X" + i);
                    string sr = Convert.ToString("Y" + i);

                    Models.clsGlobVar.ProfileCoordinate.Tank1x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[0][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank2x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[1][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank3x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[2][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank4x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[3][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank5x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[4][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank6x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[5][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank7x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[6][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank8x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[7][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank9x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[8][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank10x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[9][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank11x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[10][sc]);
                   ///// Models.clsGlobVar.ProfileCoordinate.Tank12x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[11][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank13x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[11][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank14x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[12][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank15x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[13][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank16x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[14][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank17x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[15][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank18x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[16][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank19x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[17][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank20x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[18][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank21x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[19][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank22x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[20][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank23x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[21][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank24x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[22][sc]);
                   ///// Models.clsGlobVar.ProfileCoordinate.Tank25x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[24][sc]);
                  ///  // Models.clsGlobVar.ProfileCoordinate.Tank26x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[25][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank27x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[23][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank28x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[24][sc]);
                    //// Models.clsGlobVar.ProfileCoordinate.Tank29x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[28][sc]);
                    //// Models.clsGlobVar.ProfileCoordinate.Tank30x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[27][sc]);
                    //// Models.clsGlobVar.ProfileCoordinate.Tank31x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[30][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank32x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[25][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank33x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[26][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank34x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[27][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank35x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[28][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank36x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[29][sc]);
                    Models.clsGlobVar.ProfileCoordinate.Tank37x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[30][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank38x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[31][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank39x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[32][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank40x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[33][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank41x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[34][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank42x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[41][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank43x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[42][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank44x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[43][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank45x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[44][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank46x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[45][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank47x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[46][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank48x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[47][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank49x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[48][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank50x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[49][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank51x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[50][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank52x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[51][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank53x[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[52][sc]);


                    Models.clsGlobVar.ProfileCoordinate.Tank1y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[0][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank2y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[1][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank3y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[2][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank4y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[3][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank5y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[4][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank6y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[5][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank7y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[6][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank8y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[7][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank9y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[8][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank10y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[9][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank11y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[10][sr]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank12y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[11][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank13y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[11][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank14y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[12][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank15y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[13][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank16y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[14][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank17y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[15][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank18y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[16][sr]);
                     Models.clsGlobVar.ProfileCoordinate.Tank19y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[17][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank20y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[18][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank21y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[19][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank22y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[20][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank23y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[21][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank24y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[22][sr]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank25y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[24][sr]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank26y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[25][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank27y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[23][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank28y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[24][sr]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank29y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[28][sr]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank30y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[27][sr]);
                    // Models.clsGlobVar.ProfileCoordinate.Tank31y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[30][sr]);
                     Models.clsGlobVar.ProfileCoordinate.Tank32y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[25][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank33y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[26][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank34y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[27][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank35y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[28][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank36y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[29][sr]);
                    Models.clsGlobVar.ProfileCoordinate.Tank37y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[30][sr]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank38y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[37][sr]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank39y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[38][sr]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank40y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[39][sr]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank41y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[40][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank42y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[41][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank43y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[42][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank44y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[43][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank45y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[44][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank46y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[45][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank47y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[46][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank48y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[47][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank49y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[48][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank50y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[49][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank51y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[50][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank52y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[51][sc]);
                    //Models.clsGlobVar.ProfileCoordinate.Tank53y[i] = Convert.ToDouble(dtCoordinatesProfile.Rows[52][sc]);

                }
            }
            catch (Exception ex)
            {

                System.Windows.MessageBox.Show(ex.Message);
            }


        }
    }
}





