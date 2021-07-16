// Decompiled with JetBrains decompiler
// Type: BUSINESS_LAYER.LogCreation.LogCreation
// Assembly: BUSINESS_LAYER, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 28D8CCE9-2878-4754-A05B-67DED2BF195F
// Assembly location: C:\Users\sar.puttaraju.ah\Desktop\volt..xbap_fcb01545d3e3f744_0001.0000_abdc6ecc1eecd744\BUSINESS_LAYER.dll

using DATA_LAYER.DatabaseConnectivity;
using System;

namespace BUSINESS_LAYER.LogCreation
{
    public class LogCreation
    {
        private DatabaseConnections objDB = new DatabaseConnections();

        public void CreateLog(
          string ErrorDescription,
          string MethodName,
          string ModulName,
          string UserId)
        {
            try
            {
                this.objDB.ExecuteProcedureParam("Proc_LogDetails", (object)ErrorDescription, (object)MethodName, (object)ModulName, (object)UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
