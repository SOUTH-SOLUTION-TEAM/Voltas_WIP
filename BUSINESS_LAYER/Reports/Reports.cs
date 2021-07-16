// Decompiled with JetBrains decompiler
// Type: BUSINESS_LAYER.Reports.Reports
// Assembly: BUSINESS_LAYER, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 28D8CCE9-2878-4754-A05B-67DED2BF195F
// Assembly location: C:\Users\sar.puttaraju.ah\Desktop\volt..xbap_fcb01545d3e3f744_0001.0000_abdc6ecc1eecd744\BUSINESS_LAYER.dll

using DATA_LAYER.DatabaseConnectivity;
using System;
using System.Data;

namespace BUSINESS_LAYER.Reports
{
    public class Reports
    {
        private DatabaseConnections obj_DB = new DatabaseConnections();

        public DataSet BL_Reports()
        {
            try
            {
                return this.obj_DB.ExecuteDataSetParam("Proc_Reports", (object)ENTITY_LAYER.Reports.Reports.MachineGroupName, (object)ENTITY_LAYER.Reports.Reports.Machinename, (object)ENTITY_LAYER.Reports.Reports.ShiftName, (object)ENTITY_LAYER.Reports.Reports.FromDate, (object)ENTITY_LAYER.Reports.Reports.Todate, (object)ENTITY_LAYER.Reports.Reports.Type, (object)ENTITY_LAYER.Reports.Reports.ModelNo, (object)ENTITY_LAYER.Reports.Reports.Station, (object)ENTITY_LAYER.Reports.Reports.Time);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
