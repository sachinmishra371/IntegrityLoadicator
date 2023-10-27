using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace ZebecLoadMaster.Models.BLL
{
    class clsBLL
    {
        public static DataTable GetEnttyDBRecs(string sEntityName)
        {
            string sCmd="";
            DataTable dtable = new DataTable();
            switch (sEntityName)
            {   ////////////////Real Mode///////////////


                case "vsGetRealModeAllTankFillDetails":
                    sCmd = "Select T.Tank_ID,T.[Group],T.Tank_Name,L.Weight, L.Volume, L.Percent_Full,L.SG,L.FSM,S.IsDamaged,F.max_1_act_0, ";
                    sCmd += "S.Sounding_Level,S.Temperature,S.VCF,S.Volume_Corr,S.WCF,S.Weight_in_Air, ";
                    sCmd += "CASE WHEN M.Tank_ID  IS NULL  THEN 0 ELSE 1 END AS RealData ";
                    sCmd += " From tblLoading_Condition L ";
                    sCmd += "Join tblMaster_Tank T ";
                    sCmd += "ON L.Tank_ID = T.Tank_ID ";
                    sCmd += "Join tblTank_Status S ";
                    sCmd += "On L.Tank_ID = S.Tank_ID ";
                    sCmd += "Join tblFSM_max_act F ";
                    sCmd += "On L.Tank_ID = F.Tank_ID ";
                    sCmd += "Left join tblMaster_SensorMapping M "; 
                    sCmd += "On L.Tank_ID = M.Tank_ID  ";
                    sCmd += "order by T.roworder,T.tank_id ";
                    break;
                case "vstank":
                    sCmd = "Select M.Tank_ID,M.[Group], M.Tank_Name,M.Max_Volume,f.[max_1_act_0],L.[Sounding_Level], L.Volume, L.SG, L.Weight,L.Percent_Full, L.LCG, L.TCG, L.VCG, L.FSM ,S.IsDamaged ";
                    sCmd +="From tblSimulationMode_Loading_Condition L ";
                    sCmd += "JOIN dbo.tblMaster_Tank M ";
                    sCmd +="On L.Tank_ID = M.Tank_ID ";
                    sCmd += " Join tblSimulationMode_Tank_Status S ";
                    sCmd += "On L.Tank_ID = S.Tank_ID ";
                    sCmd += " Join tblFSM_max_act f ";
                    sCmd += " on f.Tank_ID = M.Tank_ID ";
                    sCmd += " where m.Tank_ID<46 ORDER BY Tank_ID";
                    break;
                case "vsGetBallastTankFillDetails":
                    sCmd = "Select   T.Tank_ID,T.Tank_Name, L.Volume, L.Percent_Full,L.SG ";
                     sCmd+=" From tblLoading_Condition L ";
                     sCmd+="Join tblMaster_Tank T ";
                     sCmd+="ON L.Tank_ID = T.Tank_ID ";
	                 sCmd+="Join tblTank_Status S ";
                     sCmd+= "On L.Tank_ID = S.Tank_ID ";
	                 sCmd+="Where T.[Group] = 'BALLAST_TANK' ORDER BY Tank_ID";
                    break;
                case "vsGetCARGOTANKTankFillDetails":
                    sCmd = "Select   T.Tank_ID,T.Tank_Name, L.Volume, L.Percent_Full,L.SG ";
                    sCmd += " From tblLoading_Condition L ";
                    sCmd += "Join tblMaster_Tank T ";
                    sCmd += "ON L.Tank_ID = T.Tank_ID ";
                    sCmd += "Join tblTank_Status S ";
                    sCmd += "On L.Tank_ID = S.Tank_ID ";
                    sCmd += "Where T.[Group] = 'CARGO_TANK' ORDER BY Tank_ID";
                    break;

                case "vsGetFuelOilTankFillDetails":
                    sCmd = "Select T.Tank_ID, T.Tank_Name, L.Volume, L.Percent_Full,S.IsSensorFaulty,S.IsDamaged,L.SG ";
                     sCmd+=" From tblLoading_Condition L ";
                     sCmd+="Join tblMaster_Tank T ";
                     sCmd+="ON L.Tank_ID = T.Tank_ID ";
	                 sCmd+="Join tblTank_Status S ";
                     sCmd+= "On L.Tank_ID = S.Tank_ID ";
                     sCmd += "Where T.[Group] = 'FUELOIL_TANK' ORDER BY Tank_ID";
                    break;
                case "vsGetFreshWaterTankFillDetails":
                    sCmd = "Select T.Tank_ID, T.Tank_Name, L.Volume, L.Percent_Full,S.IsSensorFaulty,S.IsDamaged,L.SG ";
                     sCmd+=" From tblLoading_Condition L ";
                     sCmd+="Join tblMaster_Tank T ";
                     sCmd+="ON L.Tank_ID = T.Tank_ID ";
	                 sCmd+="Join tblTank_Status S ";
                     sCmd+= "On L.Tank_ID = S.Tank_ID ";
                     sCmd += "Where T.[Group] = 'FRESHWATER_TANK' ORDER BY Tank_ID";
                    break;
                case "vsGetMiscTankFillDetails":
                    sCmd = "Select T.Tank_ID, T.Tank_Name, L.Volume, L.Percent_Full,S.IsSensorFaulty,S.IsDamaged,L.SG ";
                     sCmd+=" From tblLoading_Condition L ";
                     sCmd+="Join tblMaster_Tank T ";
                     sCmd+="ON L.Tank_ID = T.Tank_ID ";
	                 sCmd+="Join tblTank_Status S ";
                     sCmd+= "On L.Tank_ID = S.Tank_ID ";
                     sCmd += "Where T.[Group] = 'MISC_TANK' ORDER BY Tank_ID";
                    break;
                case "vsGetCompartmentFillDetails":
                    sCmd = "Select T.Tank_ID, T.Tank_Name, L.Volume, L.Percent_Full,S.IsSensorFaulty,S.IsDamaged,L.SG ";
                     sCmd+=" From tblLoading_Condition L ";
                     sCmd+="Join tblMaster_Tank T ";
                     sCmd+="ON L.Tank_ID = T.Tank_ID ";
	                 sCmd+="Join tblTank_Status S ";
                     sCmd+= "On L.Tank_ID = S.Tank_ID ";
                     sCmd += "Where T.[Group] = 'Compartment' ORDER BY Tank_ID";
                    break;
                case "vsGetVariableDetails":
                     sCmd = "Select T.Tank_ID, T.Tank_Name, S.[Weight],L.LCG,L.TCG,L.VCG ";
	                 sCmd+="From tblLoading_Condition L,tblMaster_Tank T,tblTank_Status S ";
                     sCmd+= "Where L.Tank_ID = T.Tank_ID and L.Tank_ID = S.Tank_ID ";
                     sCmd += "and T.[Group] = 'FIXED_WEIGHT'";
                    break;
                case "vsGetlubeOilDetails":
                    sCmd = "Select T.Tank_ID, T.Tank_Name, S.[Weight],L.LCG,L.TCG,L.VCG ";
                    sCmd += "From tblLoading_Condition L,tblMaster_Tank T,tblTank_Status S ";
                    sCmd += "Where L.Tank_ID = T.Tank_ID and L.Tank_ID = S.Tank_ID ";
                    sCmd += "and T.[Group] = 'LUB_OIL_TANK'";
                    break;
                case "vsGetBallastTankLoadingStatusDetails":
                    sCmd = "Select M.Tank_ID, M.Tank_Name, L.Volume, L.SG, L.Weight,L.Percent_Full, L.LCG, L.TCG, L.VCG, L.FSM ,S.IsDamaged,S.IsSensorFaulty ";  
	                sCmd+="From dbo.tblLoading_Condition L ";
	                sCmd+="JOIN dbo.tblMaster_Tank M ";
	                sCmd+="On L.Tank_ID = M.Tank_ID ";
	                sCmd+="Join tblTank_Status S ";
	                sCmd+="On L.Tank_ID = S.Tank_ID ";
	                sCmd+="Where M.[Group] = 'BALLAST_TANK' ORDER BY Tank_ID";
                    break;
                case "vsGetFuelOilTankLoadingStatusDetails":
                    sCmd = "Select M.Tank_ID, M.Tank_Name,, L.Volume, L.SG, L.Weight,L.Percent_Full, L.LCG, L.TCG, L.VCG, L.FSM ,S.IsDamaged,S.IsSensorFaulty ";  
	                sCmd+="From dbo.tblLoading_Condition L ";
	                sCmd+="JOIN dbo.tblMaster_Tank M ";
	                sCmd+="On L.Tank_ID = M.Tank_ID ";
	                sCmd+="Join tblTank_Status S ";
	                sCmd+="On L.Tank_ID = S.Tank_ID ";
                    sCmd += "Where M.[Group] = 'FUELOIL_TANK' ORDER BY Tank_ID";
                    break;
                case "vsGetlubeOilTankLoadingStatusDetails":
                    sCmd = "Select M.Tank_ID, M.Tank_Name,, L.Volume, L.SG, L.Weight,L.Percent_Full, L.LCG, L.TCG, L.VCG, L.FSM ,S.IsDamaged,S.IsSensorFaulty ";
                    sCmd += "From dbo.tblLoading_Condition L ";
                    sCmd += "JOIN dbo.tblMaster_Tank M ";
                    sCmd += "On L.Tank_ID = M.Tank_ID ";
                    sCmd += "Join tblTank_Status S ";
                    sCmd += "On L.Tank_ID = S.Tank_ID ";
                    sCmd += "Where M.[Group] = 'LUB_OIL_TANK' ORDER BY Tank_ID";
                    break;
                case "vsGetFreshWaterTankLoadingStatusDetails":
                    sCmd = "Select M.Tank_ID, M.Tank_Name, L.Volume, L.SG, L.Weight,L.Percent_Full, L.LCG, L.TCG, L.VCG, L.FSM ,S.IsDamaged,S.IsSensorFaulty ";  
	                sCmd+="From dbo.tblLoading_Condition L ";
	                sCmd+="JOIN dbo.tblMaster_Tank M ";
	                sCmd+="On L.Tank_ID = M.Tank_ID ";
	                sCmd+="Join tblTank_Status S ";
	                sCmd+="On L.Tank_ID = S.Tank_ID ";
                    sCmd += "Where M.[Group] = 'FRESHWATER_TANK' ORDER BY Tank_ID";
                    break;
                case "vsGetMiscTankLoadingStatusDetails":
                    sCmd = "Select M.Tank_ID, M.Tank_Name, L.Volume, L.SG, L.Weight,L.Percent_Full, L.LCG, L.TCG, L.VCG, L.FSM ,S.IsDamaged,S.IsSensorFaulty ";  
	                sCmd+="From dbo.tblLoading_Condition L ";
	                sCmd+="JOIN dbo.tblMaster_Tank M ";
	                sCmd+="On L.Tank_ID = M.Tank_ID ";
	                sCmd+="Join tblTank_Status S ";
	                sCmd+="On L.Tank_ID = S.Tank_ID ";
                    sCmd += "Where M.[Group] = 'MISC_TANK' ORDER BY Tank_ID";
                    break;
                case "vsGetCompartmentLoadingStatusDetails":
                    sCmd = "Select M.Tank_ID, M.Tank_Name, L.Volume, L.SG, L.Weight,L.Percent_Full, L.LCG, L.TCG, L.VCG, L.FSM ,S.IsDamaged,S.IsSensorFaulty ";  
	                sCmd+="From dbo.tblLoading_Condition L ";
	                sCmd+="JOIN dbo.tblMaster_Tank M ";
	                sCmd+="On L.Tank_ID = M.Tank_ID ";
	                sCmd+="Join tblTank_Status S ";
	                sCmd+="On L.Tank_ID = S.Tank_ID ";
                    sCmd += "Where M.[Group] = 'Compartment' ORDER BY Tank_ID";
                    break;
                case "vsGetRealModeEquillibriumValues":
                    sCmd = " SELECT [Stability_Values_ID],[Lightship_Weight],[Displacement],[Draft_STBT_AP]";
                    sCmd+=",[Draft_STBT_MID],[Draft_STBT_FP],[Draft_PORT_AP],[Draft_PORT_MID]";
                    sCmd += ",[Draft_PORT_FP],[TRIM],[Heel],[GMT],[SF],[BM] "; 
                    sCmd+="FROM [tblEquilibrium_Values]";
                    break;


                case "vsGetRealModeTrimHeelvalues":
                    sCmd = "SELECT [Trim],[Heel] FROM tbl_RealTrimHeel";
                    break;
                case "dublicate":
                    sCmd = "	 select * from [20Ft_Container_Loading] where container_no in(1,192)";
                    break;
                case "vsGetRealModeStabilitySummary":
                    sCmd = " Select (Case M_Status When 1 Then 'OK' "; 
					sCmd+="When 0 Then 'NOT OK' ";
					sCmd+="Else 'NA' END) Stability_Status, ";
					sCmd+="Stability_Type ";
                    sCmd+="From ";					
                    sCmd+="(Select Min(Cast(C.Status As Int)) M_Status, S.Summary_Type AS Stability_Type ";
                    sCmd += "From tblRealMode_Stability_Actual_Criteria_Calc C ";
                    sCmd+="Join tblMaster_Stability_Criteria_Summary S ";
                    sCmd+="ON C.Stability_Summary_ID = S.Stability_Summary_ID ";
                    sCmd += "where  S.Summary_Type IN ('Damage', 'Intact') ";
                    sCmd+="Group By S.Summary_Type) A ";
                    sCmd += "Where A.M_Status Is Not Null ";
                    break;
                case "vsGetRealModeStabilityType":
                    sCmd = " Select Stability_Type ";
	                sCmd+="From ";					
	                sCmd+="(Select Min(Cast(C.Status As Int)) M_Status, S.Summary_Type AS Stability_Type ";
                    sCmd += "From tblRealMode_Stability_Actual_Criteria_Calc C ";
	                sCmd+="Join tblMaster_Stability_Criteria_Summary S ";
	                sCmd+="ON C.Stability_Summary_ID = S.Stability_Summary_ID ";
	                sCmd+="Where S.Summary_Type IN ('Damage', 'Intact') ";
                    sCmd += "Group By S.Summary_Type) A ";    
	                sCmd+="Where A.M_Status Is Not Null";
                    break;
                case "vsGetRealModeLoadingSummaryCurrent":
                    sCmd = "Select Tank_Name, Frames, Cargo, Percent_Full,  Volume, SG, [Weight], LCG, TCG, VCG, FSM ";
		            sCmd+="FROM ";
		            sCmd+="( ";
                    sCmd += "SELECT T.Tank_Name, "; 
		            sCmd+="	(Cast(T.Frame_Init As Varchar(10)) + ' - ' + Cast(T.Frame_End As Varchar(10))) Frames,";  
		            sCmd+="	T.Cargo, L.Percent_Full,  L.Volume, L.SG, L.[Weight], L.LCG, L.TCG, L.VCG, L.FSM, 1 'Orderby' ";     
		            sCmd+="	FROM [tblLoading_Condition] L ";
		            sCmd+="	JOIN [tblMaster_Tank] T ";
		            sCmd+="	ON L.Tank_ID = T.Tank_ID ";
                    sCmd += "	Where T.[Group] In ('BALLAST_TANK','CARGO_TANK', 'FRESHWATER_TANK','FUELOIL_TANK', 'MISC_TANK','DIESEL_OIL_TANK','LUB_OIL_TANK') ";
		            sCmd+="UNION ";	
		            sCmd+="SELECT T.Tank_Name, "; 
		            sCmd+="	(Cast(T.Frame_Init As Varchar(10)) + ' - ' + Cast(T.Frame_End As Varchar(10))) Frames, ";  
		            sCmd+="	T.Cargo, L.Percent_Full,  L.Volume, L.SG, L.[Weight], L.LCG, L.TCG, L.VCG, L.FSM, 2 'Orderby' ";     
		            sCmd+="	FROM [tblLoading_Condition] L ";
		            sCmd+="	JOIN [tblMaster_Tank] T ";
		            sCmd+="	ON L.Tank_ID = T.Tank_ID ";
		            sCmd+="	Where T.[Group] In ('Variable Data') "; 
		            sCmd+="UNION ";	
		            sCmd+="SELECT T.Tank_Name, ";  
		            sCmd+="	(Cast(T.Frame_Init As Varchar(10)) + ' - ' + Cast(T.Frame_End As Varchar(10))) Frames, ";  
		            sCmd+="	T.Cargo, L.Percent_Full,  L.Volume, L.SG, L.[Weight], L.LCG, L.TCG, L.VCG, L.FSM, 3 'Orderby' ";     
		            sCmd+="	FROM [tblLoading_Condition] L ";
		            sCmd+="	JOIN [tblMaster_Tank] T ";
		            sCmd+="	ON L.Tank_ID = T.Tank_ID ";
		            sCmd+="	Where T.[Group] In ('Compartment') And L.Volume > 0 ";
		            sCmd+="UNION ";
		            sCmd+="SELECT 'Deadweight' As 'Tank_Name', '' As 'Frames', '' As 'Cargo', Null As 'Percent_Full', Null As 'Volume', Null As 'SG', ";
		            sCmd+="		Sum(L.[Weight]),";
		            sCmd+="		CASE WHEN SUM(L.[Weight]) > 0 THEN (SUM(Lmom)/Sum(L.[Weight])) ELSE 0 END AS 'LCG',"; 
		            sCmd+="		CASE WHEN SUM(L.[Weight]) > 0 THEN (SUM(Tmom)/Sum(L.[Weight])) ELSE 0 END AS 'TCG', ";
		            sCmd+="		CASE WHEN SUM(L.[Weight]) > 0 THEN (SUM(Vmom)/Sum(L.[Weight])) ELSE 0 END AS 'VCG', ";
		            sCmd+="		SUM(FSM) As 'FSM', 4 'Orderby' ";     
		            sCmd+="	FROM [tblLoading_Condition] L ";
		            sCmd+="	JOIN [tblMaster_Tank] T";
		            sCmd+="	ON L.Tank_ID = T.Tank_ID";
		            sCmd+="	Where T.[Group] <> ('LightShip') ";
		            sCmd+="UNION ";
		            sCmd+="SELECT T.Tank_Name, "; 
		            sCmd+="	(Cast(T.Frame_Init As Varchar(10)) + ' - ' + Cast(T.Frame_End As Varchar(10))) Frames, "; 
		            sCmd+="	T.Cargo, L.Percent_Full,  L.Volume, L.SG, L.[Weight], L.LCG, L.TCG, L.VCG, L.FSM, 5 'Orderby' ";     
		            sCmd+="	FROM [tblLoading_Condition] L ";
		            sCmd+="	JOIN [tblMaster_Tank] T ";
		            sCmd+="	ON L.Tank_ID = T.Tank_ID ";
		            sCmd+="	Where T.[Group] In ('LightShip') "; 	
		            sCmd+="UNION ";
		            sCmd+="SELECT 'Total Displacement' As 'Tank_Name', '' As 'Frames', '' As 'Cargo', Null As 'Percent_Full', Null As 'Volume', Null As 'SG', ";
		            sCmd+="		Sum(L.[Weight]), ";
		            sCmd+="		(SUM(Lmom)/Sum(L.[Weight])) As 'LCG', ";
		            sCmd+="		(SUM(Tmom)/Sum(L.[Weight])) As 'TCG', ";
		            sCmd+="		(SUM(Vmom)/Sum(L.[Weight])) As 'VCG', ";
		            sCmd+="		SUM(FSM) As 'FSM', 6 'Orderby' ";    
		            sCmd+="	FROM [tblLoading_Condition] L ";
		            sCmd+="	JOIN [tblMaster_Tank] T ";
		            sCmd+="	ON L.Tank_ID = T.Tank_ID ";
		            sCmd+=") A ";
		            sCmd+="Order by Orderby";
                    break;
                case "vsGetRealModeGzDataCurrent":
                    sCmd = "SELECT a.heelAng,a.heelGZ,b.heelArm AS WH,c.heelArm AS HL,d.heelArm AS HS,e.heelArm AS PC from GZDataRealMode_New a,tblWindHeelSimulation b,tblHeavyLiftingSimulation c,tblHighSpeedSimulation d,tblPassengerCrowdingSimulation e where a.heelAng=b.heelAng and a.heelAng=c.heelAng and a.heelAng=d.heelAng and a.heelAng=e.heelAng And a.[User]=b.c_User and a.[User]=c.c_User and a.[User]=d.c_User and a.[User]=e.c_User AND a.[User] = 'dbo'";
                    break;
                case "vsGetRealModeLongitudinalDataCurrent":
                    sCmd = "SELECT [Length],BuoyanceUDL,NetUDL,SF,BM FROM tblSFAndBM order by [Length]";
                    break;
                case "vsGetRealModeIntactStabilityCriteriaCurrent":
                    sCmd = "Select S.Criterion, C.CriticalValue Critical_Value , C.Actual_Value,C.[Status] ";
		            sCmd+="From tblMaster_Stability_Criteria_Summary S ";
                    sCmd += "JOIN [tblRealMode_Stability_Actual_Criteria_Calc] C ";
		            sCmd+="ON S.Stability_Summary_ID = C.Stability_Summary_ID "; 
		            sCmd+="Where Summary_Type = 'Intact'";
                    break;
                case "vsGetRealModeDamageStabilityCriteriaCurrent":
                    sCmd = "Select S.Criterion, C.CriticalValue Critical_Value , C.Actual_Value,C.[Status] ";
		            sCmd+="From tblMaster_Stability_Criteria_Summary S ";
                    sCmd += "JOIN [tblRealMode_Stability_Actual_Criteria_Calc] C ";
		            sCmd+="ON S.Stability_Summary_ID = C.Stability_Summary_ID "; 
		            sCmd+="Where Summary_Type = 'Damage'";
                    break;

          

                case "vsGetRealModeHydrostaticDataCurrent":
                    sCmd = "Select [Lightship_Weight],[Displacement],[Draft_AP],[Draft_MID],[Draft_FP],[Draft_AFT_MARK],";
                    sCmd+="[Draft_FWD_MARK],[TRIM],[Heel],[GMT],[KG(Solid)],[FSC],[KG(Fluid)],[WPA],[LCG],[LCF]";
                    sCmd += ",[TPC],[MCT],[Last_Updated],[Draft_LCF],[Sea_water_density],[USER],[TCG],[Rolling_Period],[Deadweight],[Prop_Immersion],[LCB],[TCB],[MeanDraft],[TrimAngle] From tblRealMode_Equilibrium_Values ";
                    break;
                case "vsGetRealModeDraftsCurrent":
                    sCmd = "Select Draft_LCF, Draft_STBT_AP, Draft_STBT_FP, Draft_PORT_AP, Draft_PORT_FP, Draft_STBT_MID, Draft_PORT_MID From tblRealMode_Equilibrium_Values";
                    break;
                case "vsGetRealModeManualLoadingConditionEntriesCurrent":
                    sCmd = "Select Tank_Name, [Weight], Sounding_Level ";
		            sCmd+="From ";
			        sCmd+="(SELECT T.Tank_Name, L.[Weight], L.Sounding_Level, ";     
			        sCmd+="(Case When T.DataComingFromSensor = 1 And L.IsManualEntry = 1 Then 'Manual Input' End) IsManualInput "; 
			        sCmd+="FROM [tblLoading_Condition] L ";
			        sCmd+="JOIN [tblMaster_Tank] T ";
			        sCmd+="ON L.Tank_ID = T.Tank_ID ";
			        sCmd+="Where IsManualEntry = 1 ";
			        sCmd+=") A";
                    break;

                case "vsGetRealModeSFBMMax":
                    sCmd = "Select Distance_SF,Max_SF,Distance_BM,Max_BM From tbl_SF_BM_Max_real ";
                    break;


                ////////////////////Simulation Mode/////////////////////////
                case "vsGetSimulationModeAllTankFillDetails":
                    sCmd = "Select T.Tank_ID,T.[Group],T.Tank_Name,L.Weight, L.Volume,L.Percent_Full,L.SG,L.FSM,S.IsDamaged,F.max_1_act_0, ";
                    sCmd += "S.Sounding_Level,S.Temperature,S.VCF,S.Volume_Corr,S.WCF,S.Weight_in_Air ";
                    sCmd += " From tblSimulationMode_Loading_Condition L ";
                    sCmd += "Join tblMaster_Tank T ";
                    sCmd += "ON L.Tank_ID = T.Tank_ID ";
                    sCmd += "Join tblSimulationMode_Tank_Status S ";
                    sCmd += "On L.Tank_ID = S.Tank_ID ";
                    sCmd += "Join tblFSM_max_act F ";
                    sCmd += "On L.Tank_ID = F.Tank_ID ";
                    sCmd += "order by T.roworder,T.tank_id ";
                    break;

                case "vsGetSimulationModeAllTankFillDetailsForDamage":
                    sCmd = "Select T.Tank_ID,T.[Group],T.Tank_Name,L.Weight, S.Volume, L.Percent_Full,L.SG,L.FSM,S.IsDamaged,F.max_1_act_0, ";
                    sCmd += "S.Sounding_Level,S.Temperature,S.VCF,S.Volume_Corr,S.WCF,S.Weight_in_Air ";
                    sCmd += " From tblSimulationMode_Loading_Condition L ";
                    sCmd += "Join tblMaster_Tank T ";
                    sCmd += "ON L.Tank_ID = T.Tank_ID ";
                    sCmd += "Join tblSimulationMode_Tank_Status S ";
                    sCmd += "On L.Tank_ID = S.Tank_ID ";
                    sCmd += "Join tblFSM_max_act F ";
                    sCmd += "On L.Tank_ID = F.Tank_ID ";
                    sCmd += "order by T.roworder,T.tank_id ";
                    break;

                case "vsGetSimulationModeCargoTankFillDetails":
                    sCmd = "Select T.Tank_ID, T.Tank_Name, L.Volume, L.Percent_Full,S.IsSensorFaulty,S.IsDamaged,L.SG ";
                    sCmd += " From tblSimulationMode_Loading_Condition L ";
                    sCmd += "Join tblMaster_Tank T ";
                    sCmd += "ON L.Tank_ID = T.Tank_ID ";
                    sCmd += "Join tblSimulationMode_Tank_Status S ";
                    sCmd += "On L.Tank_ID = S.Tank_ID ";
                    sCmd += "Where T.[Group] = 'CARGO_TANK' ORDER BY Tank_ID";
                    break;

                case "vsGetSimulationModeBallastTankFillDetails":
                     sCmd = "Select T.Tank_ID, T.Tank_Name, L.Volume, L.Percent_Full,S.IsSensorFaulty,S.IsDamaged,L.SG ";
                     sCmd += " From tblSimulationMode_Loading_Condition L ";
                     sCmd+="Join tblMaster_Tank T ";
                     sCmd+="ON L.Tank_ID = T.Tank_ID ";
                     sCmd += "Join tblSimulationMode_Tank_Status S ";
                     sCmd+= "On L.Tank_ID = S.Tank_ID ";
	                 sCmd+="Where T.[Group] = 'BALLAST_TANK' ORDER BY Tank_ID";
                    break;

                case "vsGetSimulationModeDieselOilTankFillDetails":
                     sCmd = "Select T.Tank_ID, T.Tank_Name, L.Volume, L.Percent_Full,S.IsSensorFaulty,S.IsDamaged,L.SG ";
                     sCmd += " From tblSimulationMode_Loading_Condition L ";
                     sCmd+="Join tblMaster_Tank T ";
                     sCmd+="ON L.Tank_ID = T.Tank_ID ";
                     sCmd += "Join tblSimulationMode_Tank_Status S ";
                     sCmd+= "On L.Tank_ID = S.Tank_ID ";
                     sCmd += "Where T.[Group] = 'DIESEL_OIL_TANK' ORDER BY Tank_ID";
                    break;

                case "vsGetSimulationModeFreshWaterTankFillDetails":
                    sCmd = "Select T.Tank_ID, T.Tank_Name, L.Volume, L.Percent_Full,S.IsSensorFaulty,S.IsDamaged,L.SG ";
                    sCmd += " From tblSimulationMode_Loading_Condition L ";
                    sCmd += "Join tblMaster_Tank T ";
                    sCmd += "ON L.Tank_ID = T.Tank_ID ";
                    sCmd += "Join tblSimulationMode_Tank_Status S ";
                    sCmd += "On L.Tank_ID = S.Tank_ID ";
                    sCmd += "Where T.[Group] = 'FRESHWATER_TANK' ORDER BY Tank_ID";
                    break;

                case "vsGetSimulationModeLubOilTankFillDetails":
                    sCmd = "Select T.Tank_ID, T.Tank_Name, L.Volume, L.Percent_Full,S.IsSensorFaulty,S.IsDamaged,L.SG ";
                    sCmd += " From tblSimulationMode_Loading_Condition L ";
                    sCmd += "Join tblMaster_Tank T ";
                    sCmd += "ON L.Tank_ID = T.Tank_ID ";
                    sCmd += "Join tblSimulationMode_Tank_Status S ";
                    sCmd += "On L.Tank_ID = S.Tank_ID ";
                    sCmd += "Where T.[Group] = 'LUB_OIL' ORDER BY Tank_ID";
                    break;


                case "vsGetSimulationModeMiscTankFillDetails":
                    sCmd = "Select T.Tank_ID, T.Tank_Name, L.Volume, L.Percent_Full,S.IsSensorFaulty,S.IsDamaged,L.SG ";
                    sCmd += " From tblSimulationMode_Loading_Condition L ";
                    sCmd += "Join tblMaster_Tank T ";
                    sCmd += "ON L.Tank_ID = T.Tank_ID ";
                    sCmd += "Join tblSimulationMode_Tank_Status S ";
                    sCmd += "On L.Tank_ID = S.Tank_ID ";
                    sCmd += "Where T.[Group] = 'MISC_TANK' ORDER BY Tank_ID";
                    break;

                case "vsGetSimulationModeCompartmentFillDetails":
                     sCmd = "Select T.Tank_ID, T.Tank_Name, L.Volume, L.Percent_Full,S.IsSensorFaulty,S.IsDamaged,L.SG ";
                     sCmd += " From tblSimulationMode_Loading_Condition L ";
                     sCmd+="Join tblMaster_Tank T ";
                     sCmd+="ON L.Tank_ID = T.Tank_ID ";
                     sCmd += "Join tblSimulationMode_Tank_Status S ";
                     sCmd+= "On L.Tank_ID = S.Tank_ID ";
                     sCmd += "Where T.[Group] = 'COMPARTMENT' ORDER BY Tank_ID";
                    break;

                //case "vsGetSimulationModeVariableDetails":
                //    sCmd = @"SELECT tank_id,[Load_Id],[Load_Name],[Weight],[LCG],[TCG],[VCG],[Length],[Breadth],[Depth]
                //            FROM [tblFixedLoad_Simulation]";
                //    break;
                case "vsGetSimulationModeVariableDetails":
                    sCmd = @"select  m.[Tank_Name] as Load_Name,l.[Tank_ID],l.[Weight],l.[LCG],l.[TCG],l.[VCG] from [tblSimulationMode_Loading_Condition] l,[tblMaster_Tank] m where m.[Tank_ID]=l.[Tank_ID]  and l.[Tank_ID]>46";
                    break;
                case "vsGetSimulationModeshowbay20":
                    sCmd = @"select* FROM[Saushyant_Stability].[dbo].[20Ft_Showbaywise]";
                    break;
                case "vsGetSimulationModeshowbay40":
                    sCmd = @"select* FROM[Saushyant_Stability].[dbo].[40Ft_Showbaywise]";
                    break;


                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                case "vsGetSimulationCargoTankLoadingStatusDetails":
                    sCmd = "Select M.Tank_ID, M.Tank_Name, L.Volume, L.SG, L.Weight,L.Percent_Full, L.LCG, L.TCG, L.VCG, L.FSM ,S.IsDamaged ";
                    sCmd += "From dbo.tblSimulationMode_Loading_Condition L ";
                    sCmd += "JOIN dbo.tblMaster_Tank M ";
                    sCmd += "On L.Tank_ID = M.Tank_ID ";
                    sCmd += "Join tblSimulationMode_Tank_Status S ";
                    sCmd += "On L.Tank_ID = S.Tank_ID ";
                    sCmd += "Where M.[Group] = 'Cargo_Tank' ORDER BY Tank_ID";
                    break;

                case "vsGetSimulationBallastTankLoadingStatusDetails":
                    sCmd = "Select M.Tank_ID, M.Tank_Name, L.Volume, L.SG, L.Weight,L.Percent_Full, L.LCG, L.TCG, L.VCG, L.FSM ,S.IsDamaged ";
                      sCmd += "From dbo.tblSimulationMode_Loading_Condition L ";
	                sCmd+="JOIN dbo.tblMaster_Tank M ";
	                sCmd+="On L.Tank_ID = M.Tank_ID ";
                    sCmd += "Join tblSimulationMode_Tank_Status S ";
	                sCmd+="On L.Tank_ID = S.Tank_ID ";
	                sCmd+="Where M.[Group] = 'BALLAST_TANK' ORDER BY Tank_ID";
                    break;

                case "vsGetSimulationDieselOilTankLoadingStatusDetails":
                    sCmd = "Select M.Tank_ID, M.Tank_Name, L.Volume, L.SG, L.Weight,L.Percent_Full, L.LCG, L.TCG, L.VCG, L.FSM ,S.IsDamaged ";
                    sCmd += "From dbo.tblSimulationMode_Loading_Condition L ";
                    sCmd += "JOIN dbo.tblMaster_Tank M ";
                    sCmd += "On L.Tank_ID = M.Tank_ID ";
                    sCmd += "Join tblSimulationMode_Tank_Status S ";
                    sCmd += "On L.Tank_ID = S.Tank_ID ";
                    sCmd += "Where M.[Group] = 'DIESEL_OIL_TANK' ORDER BY Tank_ID";
                    break;

                case "vsGetSimulationFreshWaterTankLoadingStatusDetails":
                    sCmd = "Select M.Tank_ID, M.Tank_Name, L.Volume, L.SG, L.Weight,L.Percent_Full, L.LCG, L.TCG, L.VCG, L.FSM ,S.IsDamaged ";
                    sCmd += "From dbo.tblSimulationMode_Loading_Condition L ";
                    sCmd += "JOIN dbo.tblMaster_Tank M ";
                    sCmd += "On L.Tank_ID = M.Tank_ID ";
                    sCmd += "Join tblSimulationMode_Tank_Status S ";
                    sCmd += "On L.Tank_ID = S.Tank_ID ";
                    sCmd += "Where M.[Group] = 'FRESHWATER_TANK' ORDER BY Tank_ID";
                    break;

                case "vsGetSimulationLubOilTankLoadingStatusDetails":
                    sCmd = "Select M.Tank_ID, M.Tank_Name, L.Volume, L.SG, L.Weight,L.Percent_Full, L.LCG, L.TCG, L.VCG, L.FSM ,S.IsDamaged ";
                    sCmd += "From dbo.tblSimulationMode_Loading_Condition L ";
                    sCmd += "JOIN dbo.tblMaster_Tank M ";
                    sCmd += "On L.Tank_ID = M.Tank_ID ";
                    sCmd += "Join tblSimulationMode_Tank_Status S ";
                    sCmd += "On L.Tank_ID = S.Tank_ID ";
                    sCmd += "Where M.[Group] = 'LUB_OIL' ORDER BY Tank_ID";
                    break;



                case "vsGetSimulationCompartmentLoadingStatusDetails":
                    sCmd = "Select M.Tank_ID, M.Tank_Name, L.Volume, L.SG, L.Weight,L.Percent_Full, L.LCG, L.TCG, L.VCG, L.FSM ,S.IsDamaged ";
                      sCmd += "From dbo.tblSimulationMode_Loading_Condition L ";
	                sCmd+="JOIN dbo.tblMaster_Tank M ";
	                sCmd+="On L.Tank_ID = M.Tank_ID ";
                    sCmd += "Join tblSimulationMode_Tank_Status S ";
	                sCmd+="On L.Tank_ID = S.Tank_ID ";
                    sCmd += "Where M.[Group] = 'COMPARTMENT' ORDER BY Tank_ID";
                    break;

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                case "vsGetSimulationModeEquillibriumValues":
                     sCmd = " SELECT [Stability_Values_ID],[Lightship_Weight],[Displacement],[Draft_STBT_AP]";
                    sCmd+=",[Draft_STBT_MID],[Draft_STBT_FP],[Draft_PORT_AP],[Draft_PORT_MID]";
                    sCmd += ",[Draft_PORT_FP],[TRIM],[Heel],[GMT],[SF],[BM] ";
                    sCmd += "FROM [tblSimulationMode_Equilibrium_Values]";
                    break;
                case "vsGetSimulationModeStabilitySummary":
                    sCmd = " Select (Case M_Status When 1 Then 'OK' "; 
					sCmd+="When 0 Then 'NOT OK' ";
					sCmd+="Else 'NA' END) Stability_Status, ";
					sCmd+="Stability_Type ";
                    sCmd+="From ";					
                    sCmd+="(Select Min(Cast(C.Status As Int)) M_Status, S.Summary_Type AS Stability_Type ";
                    sCmd += "From tblSimulationMode_Stability_Actual_Criteria_Calc C ";
                    sCmd+="Join tblMaster_Stability_Criteria_Summary S ";
                    sCmd+="ON C.Stability_Summary_ID = S.Stability_Summary_ID ";
                    sCmd += "where C.[USER] = 'dbo' and S.Summary_Type IN ('Damage', 'Intact') ";
                    sCmd+="Group By S.Summary_Type) A ";
                    sCmd += "Where A.M_Status Is Not Null ";
                    break;
                case "vsGetSimulationModeStabilityType":
                     sCmd = " Select Stability_Type ";
	                sCmd+="From ";					
	                sCmd+="(Select Min(Cast(C.Status As Int)) M_Status, S.Summary_Type AS Stability_Type ";
                    sCmd += "From tblSimulationMode_Stability_Actual_Criteria_Calc C ";
	                sCmd+="Join tblMaster_Stability_Criteria_Summary S ";
	                sCmd+="ON C.Stability_Summary_ID = S.Stability_Summary_ID ";
	                sCmd+="Where S.Summary_Type IN ('Damage', 'Intact') ";
                    sCmd += "Group By S.Summary_Type) A ";    
	                sCmd+="Where A.M_Status Is Not Null";
                    break;
                case "vsGetSimulationModeLoadingSummaryCurrent":
                    sCmd = "Select Tank_Name, Frames, Tank_ID,IsDamaged, Percent_Full,  Volume, SG, [Weight], LCG, TCG, VCG, FSM,Sounding_Level ";
                    sCmd += "FROM ";
                    sCmd += "( ";
                    sCmd += "SELECT T.Tank_Name, ";
                    sCmd += "	(Cast(T.Frame_Init As Varchar(10)) + ' - ' + Cast(T.Frame_End As Varchar(10))) Frames, ";
                    sCmd += "	T.Tank_ID,S.IsDamaged, L.Percent_Full,  L.Volume, L.SG, L.[Weight], L.LCG, L.TCG, L.VCG, L.FSM ,";
                    sCmd += "	S.Sounding_Level";
                    sCmd += "	FROM [tblSimulationMode_Loading_Condition] L ";
                    sCmd += "	JOIN [tblMaster_Tank] T ";
                    sCmd += "	ON L.Tank_ID = T.Tank_ID ";
                    sCmd += "	JOIN [tblSimulationMode_Tank_Status] S ";
                    sCmd += "	ON L.Tank_ID = S.Tank_ID ";
                    sCmd += "	Where T.[Group] In ('FIXED_WEIGHT') ";
                    sCmd += "UNION ";
                    sCmd += "SELECT 'DEADWEIGHT' As 'Tank_Name', '' As 'Frames', '99' As 'Tank_ID',Null As 'IsDamaged', Null As 'Percent_Full', Null As 'Volume', Null As 'SG', ";
                    sCmd += "		Sum(L.[Weight]),";
                    sCmd += "		CASE WHEN SUM(L.[Weight]) > 0 THEN (SUM(Lmom)/Sum(L.[Weight])) ELSE 0 END AS 'LCG',";
                    sCmd += "		CASE WHEN SUM(L.[Weight]) > 0 THEN (SUM(Tmom)/Sum(L.[Weight])) ELSE 0 END AS 'TCG', ";
                    sCmd += "		CASE WHEN SUM(L.[Weight]) > 0 THEN (SUM(Vmom)/Sum(L.[Weight])) ELSE 0 END AS 'VCG', ";
                    sCmd += "		SUM(FSM) As 'FSM', ";
                    sCmd += "		00 As 'Sounding_Level' ";
                    sCmd += "	FROM [tblSimulationMode_Loading_Condition] L ";
                    sCmd += "	JOIN [tblMaster_Tank] T";
                    sCmd += "	ON L.Tank_ID = T.Tank_ID";
                    sCmd += "	Where T.[Group] <> ('LightShip') ";
                    sCmd += "UNION ";
                    sCmd += "SELECT T.Tank_Name, ";
                    sCmd += "	(Cast(T.Frame_Init As Varchar(10)) + ' - ' + Cast(T.Frame_End As Varchar(10))) Frames, ";
                    sCmd += "	T.Tank_ID, Null As 'IsDamaged', L.Percent_Full,  L.Volume, L.SG, L.[Weight], L.LCG, L.TCG, L.VCG, L.FSM , ";
                    sCmd += "    S.Sounding_Level ";
                    sCmd += "	FROM [tblSimulationMode_Loading_Condition] L ";
                    sCmd += "	JOIN [tblMaster_Tank] T ";
                    sCmd += "	ON L.Tank_ID = T.Tank_ID ";
                    sCmd += "	JOIN [tblSimulationMode_Tank_Status] S ";
                    sCmd += "	ON L.Tank_ID = S.Tank_ID ";
                    sCmd += "	Where T.[Group] In ('LightShip') ";
                    sCmd += "UNION ";
                    sCmd += "SELECT 'TOTAL DISPLACEMENT ' As 'Tank_Name', '' As 'Frames', '100' As 'Tank_ID', Null As 'IsDamaged', Null As 'Percent_Full', Null As 'Volume', Null As 'SG', ";
                    sCmd += "		Sum(L.[Weight]), ";
                    sCmd += "		(SUM(Lmom)/Sum(L.[Weight])) As 'LCG', ";
                    sCmd += "		(SUM(Tmom)/Sum(L.[Weight])) As 'TCG', ";
                    sCmd += "		(SUM(Vmom)/Sum(L.[Weight])) As 'VCG', ";
                    sCmd += "		SUM(FSM) As 'FSM' ,";
                    sCmd += " 00 As 'Sounding_Level' ";
                    sCmd += "	FROM [tblSimulationMode_Loading_Condition] L ";
                    sCmd += "	JOIN [tblMaster_Tank] T ";
                    sCmd += "	ON L.Tank_ID = T.Tank_ID ";
                    sCmd += "UNION ";
                    sCmd += "SELECT '' As 'Tank_Name', '' As 'Frames', '101' As 'Tank_ID', Null As 'IsDamaged', Null As 'Percent_Full', Null As 'Volume', Null As 'SG', ";
                    sCmd += "		Sum(L.[Weight]), ";
                    sCmd += "		(SUM(Lmom)/Sum(L.[Weight])) As 'LCG', ";
                    sCmd += "		(SUM(Tmom)/Sum(L.[Weight])) As 'TCG', ";
                    sCmd += "		(SUM(Vmom)/Sum(L.[Weight])) As 'VCG', ";
                    sCmd += "		SUM(FSM) As 'FSM' ,";
                    sCmd += " 00 As 'Sounding_Level' ";
                    sCmd += "	FROM [tblSimulationMode_Loading_Condition] L ";
                    sCmd += "	JOIN [tblMaster_Tank] T ";
                    sCmd += "	ON L.Tank_ID = T.Tank_ID ";
                    sCmd += "UNION ";
                    sCmd += "SELECT T.Tank_Name, ";
                    sCmd += "	(Cast(T.Frame_Init As Varchar(10)) + ' - ' + Cast(T.Frame_End As Varchar(10))) Frames,";
                    sCmd += "	T.Tank_ID,S.IsDamaged, L.Percent_Full,  L.Volume, L.SG, L.[Weight], L.LCG, L.TCG, L.VCG, L.FSM, ";
                    sCmd += "   S.Sounding_Level	";
                    sCmd += "	FROM [tblSimulationMode_Loading_Condition] L ";
                    sCmd += "	JOIN [tblMaster_Tank] T ";
                    sCmd += "	ON L.Tank_ID = T.Tank_ID ";
                    sCmd += "	JOIN [tblSimulationMode_Tank_Status] S ";
                    sCmd += "	ON L.Tank_ID = S.Tank_ID ";
                    sCmd += "	Where T.[Group] In ('CARGO','CARGO_TANK', 'BALLAST_TANK', 'DIESEL_OIL_TANK', 'FRESHWATER_TANK','LUB_OIL_TANK','FUEL_OIL_TANK','MISC_TANK','OTHER_TANK','COMPARTMENT') ";       
                    sCmd += "UNION ";
                    sCmd += "SELECT T.Tank_Name, ";
                    sCmd += "	(Cast(T.Frame_Init As Varchar(10)) + ' - ' + Cast(T.Frame_End As Varchar(10))) Frames,";
                    sCmd += "	T.Tank_ID,S.IsDamaged, L.Percent_Full,  L.Volume, L.SG, L.[Weight], L.LCG, L.TCG, L.VCG, L.FSM, ";
                    sCmd += "   S.Sounding_Level	";
                    sCmd += "	FROM [tblSimulationMode_Loading_Condition] L ";
                    sCmd += "	JOIN [tblMaster_Tank] T ";
                    sCmd += "	ON L.Tank_ID = T.Tank_ID ";
                    sCmd += "	JOIN [tblSimulationMode_Tank_Status] S ";
                    sCmd += "	ON L.Tank_ID = S.Tank_ID ";
                    sCmd += "	Where T.[Group] In ('COMPARTMENT') AND S.IsDamaged =1 ";       

                    sCmd += ") A ";
                    sCmd += "Order by Tank_ID";
                    break;
                case "vsGetSimulationModeGzDataCurrent":
                    sCmd = "SELECT a.heelAng,a.heelGZ,b.heelArm AS WH,c.heelArm AS HL,d.heelArm AS HS,e.heelArm AS PC from GZDataSimulationMode_New a,tblWindHeelSimulation b,tblHeavyLiftingSimulation c,tblHighSpeedSimulation d,tblPassengerCrowdingSimulation e where a.heelAng=b.heelAng and a.heelAng=c.heelAng and a.heelAng=d.heelAng and a.heelAng=e.heelAng And a.[User]=b.c_User and a.[User]=c.c_User and a.[User]=d.c_User and a.[User]=e.c_User AND a.[User] = 'dbo'";
                    break;
                case "vsGetSimulationModeLongitudinalDataCurrent":
                    sCmd = "SELECT [Length],BuoyanceUDL,NetUDL,SF,BM FROM tbl_SimulationMode_SFAndBM Where [User] = 'dbo' order by [Length]";
                    break;
                case "vsGetSimulationModeIntactStabilityCriteriaCurrent":
                    sCmd = "Select S.Criterion, C.CriticalValue Critical_Value , C.Actual_Value,C.[Status] ";
                    sCmd += "From tblMaster_Stability_Criteria_Summary S ";
                    sCmd += "JOIN tblSimulationMode_Stability_Actual_Criteria_Calc C ";
                    sCmd += "ON S.Stability_Summary_ID = C.Stability_Summary_ID ";
                    sCmd += "Where Summary_Type IN ('Intact') and C.[User]='dbo'";
                    break;
                case "vsGetSimulationModeDamageStabilityCriteriaCurrent":
                     sCmd = "Select S.Criterion, C.CriticalValue Critical_Value , C.Actual_Value,C.[Status] ";
		            sCmd+="From tblMaster_Stability_Criteria_Summary S ";
                    sCmd += "JOIN tblSimulationMode_Stability_Actual_Criteria_Calc C ";
		            sCmd+="ON S.Stability_Summary_ID = C.Stability_Summary_ID ";
                    sCmd += "Where Summary_Type = 'Damage' and C.[User]='dbo'";
                    break;

                case "vsGetSimulationMode20Ft_Container_Loading":
                    sCmd = "select * from [20Ft_Container_Loading]";
                    break;
                case "vsGetSimulationModefixedload":
                    sCmd = "select  m.[Tank_Name] as Load_Name,l.[Tank_ID],l.[Weight],l.[LCG],l.[TCG],l.[VCG] from [tblSimulationMode_Loading_Condition] l,[tblMaster_Tank] m where m.[Tank_ID]=l.[Tank_ID]  and l.[Tank_ID]>46"; //si,ulatrionfixed
                    break;

                case "vsGetSimulationMode40Ft_Container_Loading":
                    sCmd = "select * from [40Ft_Container_Loading]";
                    break;

                //case "vsGetSimulationModeHydrostaticDataCurrent":
                //    sCmd = "Select [Lightship_Weight],[Displacement],[Draft_AP],[Draft_MID],[Draft_FP],[Draft_AFT_MARK],";
                //    sCmd+="[Draft_FWD_MARK],[TRIM],[Heel],[GMT],[KG(Solid)],[FSC],[KG(Fluid)],[WPA],[LCG],[LCF]";
                //    sCmd += ",[TPC],[MCT],[Last_Updated],[Draft_LCF],[Sea_water_density],[USER],[TCG],[Rolling_Period],[Deadweight],[Prop_Immersion],[LCB],[TCB],[MeanDraft],[TrimAngle] From tblSimulationMode_Equilibrium_Values Where [USER] = 'dbo'";
                //    break;
                case "vsGetSimulationModeHydrostaticDataCurrent":
                    sCmd = "Select [Lightship_Weight],[Displacement],[Draft_AP],[Draft_MID],[Draft_FP],[Draft_AFT_MARK],";
                    sCmd += "[Draft_FWD_MARK],[TRIM],[Heel],[GMT],[KG(Solid)],[FSC],[KG(Fluid)],[WPA],[LCG],[LCF]";
                    sCmd += ",[TPC],[MCT],[Last_Updated],[Draft_LCF],[Sea_water_density],[USER],[TCG],[Rolling_Period],[Deadweight],[Prop_Immersion],[LCB],[TCB],[MeanDraft] From tblSimulationMode_Equilibrium_Values Where [USER] = 'dbo'";
                    break;
                case "vsGetSimulationModeDraftsCurrent":
                    sCmd = "Select Draft_LCF, Draft_STBT_AP, Draft_STBT_FP, Draft_PORT_AP, Draft_PORT_FP, Draft_STBT_MID, Draft_PORT_MID From tblSimulationMode_Equilibrium_Values Where [USER] = 'dbo'";
                    break;
                case "vsGetSimulationModeSFBMMax":
                    sCmd = "Select Distance_SF,Max_SF,Distance_BM,Max_BM From tbl_SF_BM_Max ";
                    break;

                case "vsgetlightshipdata":
                    sCmd = "SELECT Lightship_wt,Lightship_LCG,Lightship_VCG,Lightship_TCG  FROM tblMaster_Config_Addi";
                    break;


                ////////////////////Corrective Mode///////////////////////// 

                case "vsGetCorrectiveEquillibriumValues":
                    sCmd = "";
                    break;
                default:
                    sCmd = "";
                    break;

            }
            dtable = DAL.clsDAL.GetAllRecsDT(sCmd);
            return dtable;
        }
    }
}
