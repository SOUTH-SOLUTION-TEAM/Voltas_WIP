// Decompiled with JetBrains decompiler
// Type: BUSINESS_LAYER.Transaction.Transaction
// Assembly: BUSINESS_LAYER, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 28D8CCE9-2878-4754-A05B-67DED2BF195F
// Assembly location: C:\Users\sar.puttaraju.ah\Desktop\volt..xbap_fcb01545d3e3f744_0001.0000_abdc6ecc1eecd744\BUSINESS_LAYER.dll

using DATA_LAYER.DatabaseConnectivity;
using System;
using System.Data;

namespace BUSINESS_LAYER.Transaction
{
    public class Transaction
    {
        private DatabaseConnections obj_DB = new DatabaseConnections();

        public string BL_ProductPlanTransaction()
        {
            try
            {
                return this.obj_DB.ExecuteProcedureParam("Proc_ProductPlan", (object)ENTITY_LAYER.Transaction.Transaction.RefNo, (object)ENTITY_LAYER.Transaction.Transaction.ProductCode, (object)ENTITY_LAYER.Transaction.Transaction.Mixgroup, (object)ENTITY_LAYER.Transaction.Transaction.plannedqty, (object)ENTITY_LAYER.Transaction.Transaction.subqty, (object)ENTITY_LAYER.Transaction.Transaction.lot, (object)ENTITY_LAYER.Login.Login.UserID, (object)ENTITY_LAYER.Transaction.Transaction.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BL_ProductPlanDetails()
        {
            try
            {
                return this.obj_DB.ExecuteDataSetParam("Proc_ProductPlan", (object)ENTITY_LAYER.Transaction.Transaction.RefNo, (object)ENTITY_LAYER.Transaction.Transaction.ProductCode, (object)ENTITY_LAYER.Transaction.Transaction.Mixgroup, (object)ENTITY_LAYER.Transaction.Transaction.plannedqty, (object)ENTITY_LAYER.Transaction.Transaction.subqty, (object)ENTITY_LAYER.Transaction.Transaction.lot, (object)ENTITY_LAYER.Login.Login.UserID, (object)ENTITY_LAYER.Transaction.Transaction.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BL_Transaction()
        {
            try
            {
                return this.obj_DB.ExecuteProcedureParam("Proc_Transaction", (object)ENTITY_LAYER.Transaction.Transaction.BatchCode, (object)ENTITY_LAYER.Transaction.Transaction.Barcode, (object)ENTITY_LAYER.Transaction.Transaction.Type, (object)ENTITY_LAYER.Transaction.Transaction.TransactionType, (object)ENTITY_LAYER.Transaction.Transaction.subqty, (object)ENTITY_LAYER.Login.Login.UserID, (object)ENTITY_LAYER.Transaction.Transaction.Barcode1, (object)ENTITY_LAYER.Transaction.Transaction.Status, (object)ENTITY_LAYER.Transaction.Transaction.RepairCode, (object)ENTITY_LAYER.Transaction.Transaction.ProductCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BL_TransactionDetails()
        {
            try
            {
                return this.obj_DB.ExecuteDataSetParam("Proc_Transaction", (object)ENTITY_LAYER.Transaction.Transaction.BatchCode, (object)ENTITY_LAYER.Transaction.Transaction.Barcode, (object)ENTITY_LAYER.Transaction.Transaction.Type, (object)ENTITY_LAYER.Transaction.Transaction.TransactionType, (object)ENTITY_LAYER.Transaction.Transaction.subqty, (object)ENTITY_LAYER.Login.Login.UserID, (object)ENTITY_LAYER.Transaction.Transaction.Barcode1, (object)ENTITY_LAYER.Transaction.Transaction.Status, (object)ENTITY_LAYER.Transaction.Transaction.RepairCode, (object)ENTITY_LAYER.Transaction.Transaction.ProductCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
