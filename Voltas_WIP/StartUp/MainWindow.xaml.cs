using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Data;
using Voltas_WIP.CommonClasses;
using System.Windows.Forms;

namespace Voltas_WIP.StartUp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Page
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Varialble and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        #endregion

        private void ShowDateTime()
        {
            this.dispatcherTimer.Tick += new EventHandler(this.dispatcherTimer_Tick);
            this.dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
            this.dispatcherTimer.Start();
        }

        private void ImgSmily3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                while (this.NavigationService.CanGoBack)
                {
                    try
                    {
                        this.NavigationService.RemoveBackEntry();
                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                }
                this.NavigationService.Navigate((object)new Login());
                this.dispatcherTimer.Stop();
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.dispatcherTimer.Stop();
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                this.txtDatetime.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");
                while (this.NavigationService.CanGoBack)
                {
                    try
                    {
                        this.NavigationService.RemoveBackEntry();
                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                }
                if (!(CommonVariable.PageOpenClose == "Close"))
                    return;
                this.frmPage.Navigate((Uri)null);
                this.Background = (Brush)Brushes.White;
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                while (this.NavigationService.CanGoBack)
                {
                    try
                    {
                        this.NavigationService.RemoveBackEntry();
                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                }
                this.txtuserID.Text = "Application is using by " + CommonVariable.UserName;
                this.GetUserRights();
                this.ShowDateTime();
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void GetUserRights()
        {
            string str1 = CommonVariable.Rights.ToString();
            char[] chArray = new char[1] { ',' };
            foreach (string str2 in str1.Split(chArray))
            {
                switch (str2)
                {
                    case "DASH BOARD":
                        this.mnuDashBoard.IsEnabled = true;
                        this.mnuDashBoard.Foreground = (Brush)Brushes.White;
                        break;
                    case "EST TEST":
                        this.mnuESTtest.IsEnabled = true;
                        this.mnuESTtest.Foreground = (Brush)Brushes.White;
                        break;
                    case "GROUP DEFINITION":
                        this.mnuGroupMaster.IsEnabled = true;
                        this.mnuGroupMaster.Foreground = (Brush)Brushes.White;
                        break;
                    case "IP CONFIG DEFINITION":
                        this.mnuIPConfigMaster.IsEnabled = true;
                        this.mnuIPConfigMaster.Foreground = (Brush)Brushes.White;
                        break;
                    case "MASTER BARCODE PRINTING":
                        this.mnuMasterBarcode.IsEnabled = true;
                        this.mnuMasterBarcode.Foreground = (Brush)Brushes.White;
                        break;
                    case "MOTOR CROSS CHECK":
                        this.mnuCrossCheck.IsEnabled = true;
                        this.mnuCrossCheck.Foreground = (Brush)Brushes.White;
                        break;
                    case "PACKING":
                        this.mnuPACKING.IsEnabled = true;
                        this.mnuPACKING.Foreground = (Brush)Brushes.White;
                        break;
                    case "PRODUCT DEFINITION":
                        this.mnuProductMaster.IsEnabled = true;
                        this.mnuProductMaster.Foreground = (Brush)Brushes.White;
                        break;
                    case "PRODUCT LABEL PRINT":
                        this.mnuProductLabel.IsEnabled = true;
                        this.mnuProductLabel.Foreground = (Brush)Brushes.White;
                        break;
                    case "PRODUCTION PLAN":
                        this.mnuProductionPlan.IsEnabled = true;
                        this.mnuProductionPlan.Foreground = (Brush)Brushes.White;
                        break;
                    case "REPAIR":
                        this.mnuRepair.IsEnabled = true;
                        this.mnuRepair.Foreground = (Brush)Brushes.White;
                        break;
                    case "REPAIR DEFINITION":
                        this.mnuRepairyMaster.IsEnabled = true;
                        this.mnuRepairyMaster.Foreground = (Brush)Brushes.White;
                        break;
                    case "SERIAL AND MRP LABEL PRINT":
                        this.mnuSerialMRP.IsEnabled = true;
                        this.mnuSerialMRP.Foreground = (Brush)Brushes.White;
                        break;
                    case "SHIFT DEFINITION":
                        this.mnuShiftMaster.IsEnabled = true;
                        this.mnuShiftMaster.Foreground = (Brush)Brushes.White;
                        break;
                    case "SKU DEFINITION":
                        this.mnuModelMaster.IsEnabled = true;
                        this.mnuModelMaster.Foreground = (Brush)Brushes.White;
                        break;
                    case "SLAVE BARCODE PRINTING":
                        this.mnuSlaveBarcode.IsEnabled = true;
                        this.mnuSlaveBarcode.Foreground = (Brush)Brushes.White;
                        break;
                    case "STATION DEFINITION":
                        this.mnuStationMaster.IsEnabled = true;
                        this.mnuStationMaster.Foreground = (Brush)Brushes.White;
                        break;
                    case "TEST DEFINITION":
                        this.mnuTestMaster.IsEnabled = true;
                        this.mnuTestMaster.Foreground = (Brush)Brushes.White;
                        break;
                    case "USER DEFINITION":
                        this.mnuUserMaster.IsEnabled = true;
                        this.mnuUserMaster.Foreground = (Brush)Brushes.White;
                        break;
                    case "VISUAL TEST":
                        this.mnuVisualTest.IsEnabled = true;
                        this.mnuVisualTest.Foreground = (Brush)Brushes.White;
                        break;
                }
            }
        }

        private void BrdMaster_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.ContextMaster.PlacementTarget = sender as UIElement;
                this.ContextMaster.IsOpen = true;
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void BrdMaster_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                e.Handled = true;
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void BrdTransaction_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.ContextTransaction.PlacementTarget = sender as UIElement;
                this.ContextTransaction.IsOpen = true;
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void BrdTransaction_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                e.Handled = true;
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void BrdReport_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.ContextReports.PlacementTarget = sender as UIElement;
                this.ContextReports.IsOpen = true;
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void BrdReport_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                e.Handled = true;
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Grid_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                SendKeys.SendWait("^w");
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void MnuUserMaster_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonVariable.PageOpenClose = "Open";
                this.Background = (Brush)Brushes.White;
                this.frmPage.Navigate(new Uri("/Masters/UserMaster.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void MnuGroupMaster_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonVariable.PageOpenClose = "Open";
                this.Background = (Brush)Brushes.White;
                this.frmPage.Navigate(new Uri("/Masters/GroupMaster.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void MnuModelMaster_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonVariable.PageOpenClose = "Open";
                this.Background = (Brush)Brushes.White;
                this.frmPage.Navigate(new Uri("/Masters/ModelMaster.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void MnuProductMaster_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonVariable.PageOpenClose = "Open";
                this.Background = (Brush)Brushes.White;
                this.frmPage.Navigate(new Uri("/Masters/ProductDefinition.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void MnuTestMaster_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonVariable.PageOpenClose = "Open";
                this.Background = (Brush)Brushes.White;
                this.frmPage.Navigate(new Uri("/Masters/TestMaster.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void MnuRepairyMaster_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonVariable.PageOpenClose = "Open";
                this.Background = (Brush)Brushes.White;
                this.frmPage.Navigate(new Uri("/Masters/RepairMaster.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void MnuShiftMaster_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonVariable.PageOpenClose = "Open";
                this.Background = (Brush)Brushes.White;
                this.frmPage.Navigate(new Uri("/Masters/ShiftMaster.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void MnuStationMaster_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonVariable.PageOpenClose = "Open";
                this.Background = (Brush)Brushes.White;
                this.frmPage.Navigate(new Uri("/Masters/StationMaster.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void MnuIPConfigMaster_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonVariable.PageOpenClose = "Open";
                this.Background = (Brush)Brushes.White;
                this.frmPage.Navigate(new Uri("/Masters/IPConfigurationMaster.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void MnuProductionPlan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonVariable.PageOpenClose = "Open";
                this.Background = (Brush)Brushes.White;
                this.frmPage.Navigate(new Uri("/Transaction/Production_Plan.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void MnuMasterBarcode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonVariable.PageOpenClose = "Open";
                this.Background = (Brush)Brushes.Black;
                this.frmPage.Navigate(new Uri("/Transaction/Master_Barcode_Printing.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void MnuSlaveBarcode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonVariable.PageOpenClose = "Open";
                this.Background = (Brush)Brushes.Black;
                this.frmPage.Navigate(new Uri("/Transaction/Slave_Barcode_Printing.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void MnuCrossCheck_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonVariable.PageOpenClose = "Open";
                this.Background = (Brush)Brushes.Black;
                this.frmPage.Navigate(new Uri("/Transaction/Motor_CrossCheeck.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void MnuESTtest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonVariable.PageOpenClose = "Open";
                this.Background = (Brush)Brushes.Black;
                this.frmPage.Navigate(new Uri("/Transaction/EST_Test.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void MnuVisualTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonVariable.PageOpenClose = "Open";
                this.Background = (Brush)Brushes.Black;
                this.frmPage.Navigate(new Uri("/Transaction/Visual_Test.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void MnuRepair_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonVariable.PageOpenClose = "Open";
                this.Background = (Brush)Brushes.Black;
                this.frmPage.Navigate(new Uri("/Transaction/Repair.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void MnuProductLabel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonVariable.PageOpenClose = "Open";
                this.Background = (Brush)Brushes.Black;
                this.frmPage.Navigate(new Uri("/Transaction/Production_Label.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void MnuSerialMRP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonVariable.PageOpenClose = "Open";
                this.Background = (Brush)Brushes.Black;
                this.frmPage.Navigate(new Uri("/Transaction/Serial_and_MRP_Label.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void MnuPACKING_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonVariable.PageOpenClose = "Open";
                this.Background = (Brush)Brushes.Black;
                this.frmPage.Navigate(new Uri("/Transaction/Packing.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void MnuDashBoard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonVariable.PageOpenClose = "Open";
                this.Background = (Brush)Brushes.Black;
                this.frmPage.Navigate(new Uri("/Transaction/DashBoard.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

    }
}
