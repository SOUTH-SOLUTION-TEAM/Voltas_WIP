// Decompiled with JetBrains decompiler
// Type: ENTITY_LAYER.Login.Login
// Assembly: ENTITY_LAYER, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1C83220-F04C-49C9-8153-96F1099C147B
// Assembly location: C:\Users\sar.puttaraju.ah\Desktop\volt..xbap_fcb01545d3e3f744_0001.0000_abdc6ecc1eecd744\ENTITY_LAYER.dll

namespace ENTITY_LAYER.Login
{
    public class Login
    {
        private static string _UserID;
        private static string _UserName;
        private static string _Password;
        private static string _Type;
        private static string _ConfirmPassword;
        private static string _Rights;

        public static string Rights
        {
            get => ENTITY_LAYER.Login.Login._Rights;
            set => ENTITY_LAYER.Login.Login._Rights = value;
        }

        public static string ConfirmPassword
        {
            get => ENTITY_LAYER.Login.Login._ConfirmPassword;
            set => ENTITY_LAYER.Login.Login._ConfirmPassword = value;
        }

        public static string Type
        {
            get => ENTITY_LAYER.Login.Login._Type;
            set => ENTITY_LAYER.Login.Login._Type = value;
        }

        public static string Password
        {
            get => ENTITY_LAYER.Login.Login._Password;
            set => ENTITY_LAYER.Login.Login._Password = value;
        }

        public static string UserName
        {
            get => ENTITY_LAYER.Login.Login._UserName;
            set => ENTITY_LAYER.Login.Login._UserName = value;
        }

        public static string UserID
        {
            get => ENTITY_LAYER.Login.Login._UserID;
            set => ENTITY_LAYER.Login.Login._UserID = value;
        }
    }
}
