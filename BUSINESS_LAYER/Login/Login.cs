// Decompiled with JetBrains decompiler
// Type: BUSINESS_LAYER.Login.Login
// Assembly: BUSINESS_LAYER, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 28D8CCE9-2878-4754-A05B-67DED2BF195F
// Assembly location: C:\Users\sar.puttaraju.ah\Desktop\volt..xbap_fcb01545d3e3f744_0001.0000_abdc6ecc1eecd744\BUSINESS_LAYER.dll

using DATA_LAYER.DatabaseConnectivity;
using System;

namespace BUSINESS_LAYER.Login
{
    public class Login
    {
        private DatabaseConnections obj_DB = new DatabaseConnections();

        public string BL_Login()
        {
            try
            {
                return this.obj_DB.ExecuteProcedureParam("Proc_Login", (object)ENTITY_LAYER.Login.Login.UserID, (object)ENTITY_LAYER.Login.Login.Password, (object)ENTITY_LAYER.Login.Login.ConfirmPassword, (object)ENTITY_LAYER.Login.Login.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
