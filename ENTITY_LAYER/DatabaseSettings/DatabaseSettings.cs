// Decompiled with JetBrains decompiler
// Type: ENTITY_LAYER.DatabaseSettings.DatabaseSettings
// Assembly: ENTITY_LAYER, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1C83220-F04C-49C9-8153-96F1099C147B
// Assembly location: C:\Users\sar.puttaraju.ah\Desktop\volt..xbap_fcb01545d3e3f744_0001.0000_abdc6ecc1eecd744\ENTITY_LAYER.dll

namespace ENTITY_LAYER.DatabaseSettings
{
    public class DatabaseSettings
    {
        private static string _SqldbServer;
        private static string _SqlDBName;
        private static string _SqlDBUserID;
        private static string _SqlDBPassword;

        public static string SqlDBPassword
        {
            get => ENTITY_LAYER.DatabaseSettings.DatabaseSettings._SqlDBPassword;
            set => ENTITY_LAYER.DatabaseSettings.DatabaseSettings._SqlDBPassword = value;
        }

        public static string SqlDBUserID
        {
            get => ENTITY_LAYER.DatabaseSettings.DatabaseSettings._SqlDBUserID;
            set => ENTITY_LAYER.DatabaseSettings.DatabaseSettings._SqlDBUserID = value;
        }

        public static string SqlDBName
        {
            get => ENTITY_LAYER.DatabaseSettings.DatabaseSettings._SqlDBName;
            set => ENTITY_LAYER.DatabaseSettings.DatabaseSettings._SqlDBName = value;
        }

        public static string SqldbServer
        {
            get => ENTITY_LAYER.DatabaseSettings.DatabaseSettings._SqldbServer;
            set => ENTITY_LAYER.DatabaseSettings.DatabaseSettings._SqldbServer = value;
        }
    }
}
