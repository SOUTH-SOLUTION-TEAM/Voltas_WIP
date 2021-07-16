// Decompiled with JetBrains decompiler
// Type: Voltas_WIP.CommonClasses.CommonVariable
// Assembly: Voltas_WIP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1DB10D9-18A7-4F84-8868-D2A49100A407
// Assembly location: C:\Users\sar.puttaraju.ah\Desktop\volt..xbap_fcb01545d3e3f744_0001.0000_abdc6ecc1eecd744\Voltas_WIP.exe

namespace Voltas_WIP.CommonClasses
{
    public class CommonVariable
    {
        public static string DataSaved = "DATA SAVED SUCCESSFULLY";
        public static string DataDeleted = "DATA DELETED SUCCESSFULLY";
        public static string DataUpdated = "DATA UPDATED SUCCESSFULLY";
        public static string Duplicate = "DATA ALREADY EXIST";
        public static string DeleteConfirm = "DO YOU WANT TO DELETE SELECTED DATA?";
        public static string RowSelection = " PLEASE SELECT ROW FROM LIST VIEW FOR YOUR TRANSACTION!!!";
        public static string UserID = "";
        public static string UserName = "";
        public static string UserType = "";
        public static string Rights = "";
        public static int RefNo = 0;
        public static string Active = nameof(Active);
        public static string InActive = nameof(InActive);
        public static string Result = "";
        public static string PageOpenClose = "Close";

        public enum CustomStriing
        {
            YESNO,
            OKCancel,
            OK,
            Error,
            Successfull,
            Information,
            Question,
            Exclamatory,
        }
    }
}
