using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using Voltas_WIP.CommonClasses;
using Voltas_WIP.StartUp;

namespace Voltas_WIP
{ 
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();

        private void StartUP(object sender, StartupEventArgs e)
        {
            try
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Log"))
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Log");
                string str = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                if (str != "")
                {
                    string[] strArray = str.Split(',');
                    if ((uint)strArray.Length > 0U)
                    {
                        ENTITY_LAYER.DatabaseSettings.DatabaseSettings.SqldbServer = strArray[0].ToString();
                        ENTITY_LAYER.DatabaseSettings.DatabaseSettings.SqlDBUserID = strArray[1].ToString();
                        ENTITY_LAYER.DatabaseSettings.DatabaseSettings.SqlDBPassword = strArray[2].ToString();
                        ENTITY_LAYER.DatabaseSettings.DatabaseSettings.SqlDBName = strArray[3].ToString();
                        Application.Current.MainWindow.Content = new Login();
                    }
                    else
                        CommonMethods.MessageBoxShow("INCORRECT DATABASE SETTING!!", CommonVariable.CustomStriing.Information.ToString());
                }
                else
                    CommonMethods.MessageBoxShow("INCORRECT DATABASE SETTING!!", CommonVariable.CustomStriing.Information.ToString());
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }


    }
}
