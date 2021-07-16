// Decompiled with JetBrains decompiler
// Type: BUSINESS_LAYER.Masters.Masters
// Assembly: BUSINESS_LAYER, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 28D8CCE9-2878-4754-A05B-67DED2BF195F
// Assembly location: C:\Users\sar.puttaraju.ah\Desktop\volt..xbap_fcb01545d3e3f744_0001.0000_abdc6ecc1eecd744\BUSINESS_LAYER.dll

using DATA_LAYER.DatabaseConnectivity;
using System;
using System.Data;

namespace BUSINESS_LAYER.Masters
{
    public class Masters
    {
        private DatabaseConnections obj_DB = new DatabaseConnections();

        public string BL_GroupMasterTransaction()
        {
            try
            {
                return this.obj_DB.ExecuteProcedureParam("Proc_GroupMaster", (object)ENTITY_LAYER.Masters.Masters.GroupID, (object)ENTITY_LAYER.Masters.Masters.Rights, (object)ENTITY_LAYER.Masters.Masters.Type, (object)ENTITY_LAYER.Login.Login.UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BL_GroupMasterDetails()
        {
            try
            {
                return this.obj_DB.ExecuteDataSetParam("Proc_GroupMaster", (object)ENTITY_LAYER.Masters.Masters.GroupID, (object)ENTITY_LAYER.Masters.Masters.Rights, (object)ENTITY_LAYER.Masters.Masters.Type, (object)ENTITY_LAYER.Login.Login.UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BL_UserMasterTransaction()
        {
            try
            {
                return this.obj_DB.ExecuteProcedureParam("Proc_UserMaster", (object)ENTITY_LAYER.Masters.Masters.RefNo, (object)ENTITY_LAYER.Masters.Masters.UserID, (object)ENTITY_LAYER.Masters.Masters.UserName, (object)ENTITY_LAYER.Masters.Masters.Password, (object)ENTITY_LAYER.Masters.Masters.GroupID, (object)ENTITY_LAYER.Login.Login.UserID, (object)ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BL_UserMasterDetails()
        {
            try
            {
                return this.obj_DB.ExecuteDataSetParam("Proc_UserMaster", (object)ENTITY_LAYER.Masters.Masters.RefNo, (object)ENTITY_LAYER.Masters.Masters.UserID, (object)ENTITY_LAYER.Masters.Masters.UserName, (object)ENTITY_LAYER.Masters.Masters.Password, (object)ENTITY_LAYER.Masters.Masters.GroupID, (object)ENTITY_LAYER.Login.Login.UserID, (object)ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BL_IPConfigMasterTransaction()
        {
            try
            {
                return this.obj_DB.ExecuteProcedureParam("Proc_IPconfigMaster", (object)ENTITY_LAYER.Masters.Masters.RefNo, (object)ENTITY_LAYER.Masters.Masters.HardwareType, (object)ENTITY_LAYER.Masters.Masters.IP, (object)ENTITY_LAYER.Masters.Masters.Port, (object)ENTITY_LAYER.Masters.Masters.StationNo, (object)ENTITY_LAYER.Login.Login.UserID, (object)ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BL_IPConfigMasterDetails()
        {
            try
            {
                return this.obj_DB.ExecuteDataSetParam("Proc_IPconfigMaster", (object)ENTITY_LAYER.Masters.Masters.RefNo, (object)ENTITY_LAYER.Masters.Masters.HardwareType, (object)ENTITY_LAYER.Masters.Masters.IP, (object)ENTITY_LAYER.Masters.Masters.Port, (object)ENTITY_LAYER.Masters.Masters.StationNo, (object)ENTITY_LAYER.Login.Login.UserID, (object)ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BL_ModelMasterTransaction()
        {
            try
            {
                return this.obj_DB.ExecuteProcedureParam("Proc_ModelMaster", (object)ENTITY_LAYER.Masters.Masters.RefNo, (object)ENTITY_LAYER.Masters.Masters.ModelName, (object)ENTITY_LAYER.Masters.Masters.BrandName, (object)ENTITY_LAYER.Masters.Masters.Components, (object)ENTITY_LAYER.Login.Login.UserID, (object)ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BL_ModelMasterDetails()
        {
            try
            {
                return this.obj_DB.ExecuteDataSetParam("Proc_ModelMaster", (object)ENTITY_LAYER.Masters.Masters.RefNo, (object)ENTITY_LAYER.Masters.Masters.ModelName, (object)ENTITY_LAYER.Masters.Masters.BrandName, (object)ENTITY_LAYER.Masters.Masters.Components, (object)ENTITY_LAYER.Login.Login.UserID, (object)ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BL_ProductDefinitionTransaction()
        {
            try
            {
                return this.obj_DB.ExecuteProcedureParam("Proc_ProductDefinitionMaster", (object)ENTITY_LAYER.Masters.Masters.RefNo, (object)ENTITY_LAYER.Masters.Masters.ProductCode, (object)ENTITY_LAYER.Masters.Masters.ProductName, (object)ENTITY_LAYER.Masters.Masters.ModelName, (object)ENTITY_LAYER.Masters.Masters.BrandName, (object)ENTITY_LAYER.Masters.Masters.SerialNo, (object)ENTITY_LAYER.Masters.Masters.Demoded, (object)ENTITY_LAYER.Masters.Masters.Status, (object)ENTITY_LAYER.Login.Login.UserID, (object)ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BL_ProductDefinitionDetails()
        {
            try
            {
                return this.obj_DB.ExecuteDataSetParam("Proc_ProductDefinitionMaster", (object)ENTITY_LAYER.Masters.Masters.RefNo, (object)ENTITY_LAYER.Masters.Masters.ProductCode, (object)ENTITY_LAYER.Masters.Masters.ProductName, (object)ENTITY_LAYER.Masters.Masters.ModelName, (object)ENTITY_LAYER.Masters.Masters.BrandName, (object)ENTITY_LAYER.Masters.Masters.SerialNo, (object)ENTITY_LAYER.Masters.Masters.Demoded, (object)ENTITY_LAYER.Masters.Masters.Status, (object)ENTITY_LAYER.Login.Login.UserID, (object)ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BL_RepairMasterTransaction()
        {
            try
            {
                return this.obj_DB.ExecuteProcedureParam("Proc_RepairMaster", (object)ENTITY_LAYER.Masters.Masters.RefNo, (object)ENTITY_LAYER.Masters.Masters.RepairName, (object)ENTITY_LAYER.Masters.Masters.RepairCode, (object)ENTITY_LAYER.Login.Login.UserID, (object)ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BL_RepairMasterDetails()
        {
            try
            {
                return this.obj_DB.ExecuteDataSetParam("Proc_RepairMaster", (object)ENTITY_LAYER.Masters.Masters.RefNo, (object)ENTITY_LAYER.Masters.Masters.ModelName, (object)ENTITY_LAYER.Masters.Masters.RepairCode, (object)ENTITY_LAYER.Login.Login.UserID, (object)ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BL_ShiftMasterTransaction()
        {
            try
            {
                return this.obj_DB.ExecuteProcedureParam("Proc_ShiftMaster", (object)ENTITY_LAYER.Masters.Masters.RefNo, (object)ENTITY_LAYER.Masters.Masters.ShiftName, (object)ENTITY_LAYER.Masters.Masters.Shifttiming, (object)ENTITY_LAYER.Masters.Masters.ShiftBreak, (object)ENTITY_LAYER.Masters.Masters.BreakTime, (object)ENTITY_LAYER.Masters.Masters.TotalWorkingHrs, (object)ENTITY_LAYER.Masters.Masters.Status, (object)ENTITY_LAYER.Login.Login.UserID, (object)ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BL_ShiftMasterDetails()
        {
            try
            {
                return this.obj_DB.ExecuteDataSetParam("Proc_ShiftMaster", (object)ENTITY_LAYER.Masters.Masters.RefNo, (object)ENTITY_LAYER.Masters.Masters.ShiftName, (object)ENTITY_LAYER.Masters.Masters.Shifttiming, (object)ENTITY_LAYER.Masters.Masters.ShiftBreak, (object)ENTITY_LAYER.Masters.Masters.BreakTime, (object)ENTITY_LAYER.Masters.Masters.TotalWorkingHrs, (object)ENTITY_LAYER.Masters.Masters.Status, (object)ENTITY_LAYER.Login.Login.UserID, (object)ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BL_StationMasterTransaction()
        {
            try
            {
                return this.obj_DB.ExecuteProcedureParam("Proc_StationMaster", (object)ENTITY_LAYER.Masters.Masters.RefNo, (object)ENTITY_LAYER.Masters.Masters.StationNo, (object)ENTITY_LAYER.Masters.Masters.StationName, (object)ENTITY_LAYER.Masters.Masters.Process, (object)ENTITY_LAYER.Login.Login.UserID, (object)ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BL_StationMasterDetails()
        {
            try
            {
                return this.obj_DB.ExecuteDataSetParam("Proc_StationMaster", (object)ENTITY_LAYER.Masters.Masters.RefNo, (object)ENTITY_LAYER.Masters.Masters.StationNo, (object)ENTITY_LAYER.Masters.Masters.StationName, (object)ENTITY_LAYER.Masters.Masters.Process, (object)ENTITY_LAYER.Login.Login.UserID, (object)ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BL_TestMasterTransaction()
        {
            try
            {
                return this.obj_DB.ExecuteProcedureParam("Proc_TestMaster", (object)ENTITY_LAYER.Masters.Masters.RefNo, (object)ENTITY_LAYER.Masters.Masters.StationNo, (object)ENTITY_LAYER.Masters.Masters.TestName, (object)ENTITY_LAYER.Masters.Masters.TestRequired, (object)ENTITY_LAYER.Login.Login.UserID, (object)ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BL_TestMasterDetails()
        {
            try
            {
                return this.obj_DB.ExecuteDataSetParam("Proc_TestMaster", (object)ENTITY_LAYER.Masters.Masters.RefNo, (object)ENTITY_LAYER.Masters.Masters.StationNo, (object)ENTITY_LAYER.Masters.Masters.TestName, (object)ENTITY_LAYER.Masters.Masters.TestRequired, (object)ENTITY_LAYER.Login.Login.UserID, (object)ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
