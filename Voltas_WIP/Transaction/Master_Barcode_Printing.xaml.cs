using Sato_Network_Client_DLL;
using SATOPrinterAPI;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Voltas_WIP.CommonClasses;

namespace Voltas_WIP.Transaction
{
    /// <summary>
    /// Interaction logic for Master_Barcode_Printing.xaml
    /// </summary>
    public partial class Master_Barcode_Printing : Page
    {
        public Master_Barcode_Printing()
        {
            InitializeComponent();
        }

        private BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        private BUSINESS_LAYER.Transaction.Transaction obj_Tran = new BUSINESS_LAYER.Transaction.Transaction();
        private NetworkClient obj_Client = new NetworkClient();
        private NetworkClient obj_Client1 = new NetworkClient();
        private DataTable dt = new DataTable();
        private string PrinterIP = "";
        private string Port = "";
        private string PrnData = "";
        private string ProductCode = "";
        private string subQty = "";
        private delegate void dlgcannerStatusChange(bool Flag, string Client);

        private delegate void dlgacnnerDataReacive(string Barcode, string Client);

        private delegate void dlgPLCStatusChange(bool Flag, string Client);

        private delegate void dlgPLCDataReacive(string Barcode, string Client);

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonVariable.PageOpenClose = "Close";
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MASTER_BARCODE", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Transaction(string Type)
        {
            if (Type == "LoadProdutCode")
            {
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                CommonMethods.FillComboBox(this.cmbBatchNo, this.obj_Tran.BL_TransactionDetails().Tables[0], "BatchNo", "BatchNo");
            }
            if (Type == "GetProductionPlan")
            {
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.TransactionType = "Master_Barcode";
                ENTITY_LAYER.Transaction.Transaction.BatchCode = this.cmbBatchNo.SelectedValue.ToString();
                DataTable table = this.obj_Tran.BL_TransactionDetails().Tables[0];
                if (table.Rows.Count > 0)
                {
                    this.ProductCode = table.Rows[0]["ProductCode"].ToString();
                    this.txtProductName.Text = table.Rows[0]["ProductName"].ToString();
                    this.txtModelNo.Text = table.Rows[0]["ModelName"].ToString();
                    this.txtBrand.Text = table.Rows[0]["BrandName"].ToString();
                    this.txtGroup.Text = table.Rows[0]["Mixgroup"].ToString();
                    this.txtLot.Text = table.Rows[0]["Lot"].ToString();
                    this.txtPrintCount.Text = table.Rows[0]["PrintQty"].ToString();
                    this.txtScanCount.Text = table.Rows[0]["ScannedQty"].ToString();
                    this.subQty = this.txtScanCount.Text.Split('/')[1].ToString();
                }
            }
            if (Type == "GetIPDetails")
            {
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.TransactionType = "Master_Barcode";
                this.dt = this.obj_Tran.BL_TransactionDetails().Tables[0];
                for (int index = 0; index < this.dt.Rows.Count; ++index)
                {
                    if (this.dt.Rows[index]["HardwareType"].ToString() == "SCANNER")
                    {
                        this.obj_Client = (NetworkClient)null;
                        this.obj_Client = new NetworkClient();
                        this.obj_Client.OnDataArrived += new NetworkClient.DataArrivalHandler(this.DataReacive);
                        this.obj_Client.OnScannerStatusChanged += new NetworkClient.ScannerStatusChangeHandler(this.Scanner_OnStatusChanged);
                        this.obj_Client.connection(this.dt.Rows[0]["IPAddress"].ToString(), Convert.ToInt32(Convert.ToInt32(this.dt.Rows[0]["PortNo"])));
                    }
                    if (this.dt.Rows[index]["HardwareType"].ToString() == "PLC")
                    {
                        this.obj_Client1 = (NetworkClient)null;
                        this.obj_Client1 = new NetworkClient();
                        this.obj_Client1.OnDataArrived += new NetworkClient.DataArrivalHandler(this.PLCDataReacive);
                        this.obj_Client1.OnScannerStatusChanged += new NetworkClient.ScannerStatusChangeHandler(this.PLC_OnStatusChanged);
                        this.obj_Client1.connection(this.dt.Rows[0]["IPAddress"].ToString(), Convert.ToInt32(Convert.ToInt32(this.dt.Rows[0]["PortNo"])));
                    }
                    if (this.dt.Rows[index]["HardwareType"].ToString() == "Printer")
                    {
                        this.PrinterIP = this.dt.Rows[1]["IPAddress"].ToString();
                        this.Port = this.dt.Rows[1]["PortNo"].ToString();
                        this.PrnData = this.dt.Rows[1]["PrnData"].ToString();
                    }
                }
            }
            CommonVariable.CustomStriing customStriing;
            if (Type == "Scan")
            {
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.TransactionType = "Master_Barcode";
                ENTITY_LAYER.Transaction.Transaction.BatchCode = this.cmbBatchNo.SelectedValue.ToString();
                ENTITY_LAYER.Transaction.Transaction.Barcode = this.txtLastPrintedbarcode.Text;
                ENTITY_LAYER.Transaction.Transaction.subqty = this.subQty;
                ENTITY_LAYER.Transaction.Transaction.ProductCode = this.ProductCode;
                CommonVariable.Result = this.obj_Tran.BL_Transaction();
                if (CommonVariable.Result == "Updated")
                {
                    this.Transaction("GetProductionPlan");
                }
                else
                {
                    string result = CommonVariable.Result;
                    customStriing = CommonVariable.CustomStriing.Error;
                    string ErrorType = customStriing.ToString();
                    CommonMethods.MessageBoxShow(result, ErrorType);
                }
            }
            if (!(Type == "Print"))
                return;
            bool? isChecked = this.rbBulk.IsChecked;
            bool flag = true;
            if (isChecked.GetValueOrDefault() == flag & isChecked.HasValue)
            {
                for (int index = 0; index < Convert.ToInt32(this.txtQTy.Text); ++index)
                {
                    ENTITY_LAYER.Transaction.Transaction.Type = Type;
                    ENTITY_LAYER.Transaction.Transaction.TransactionType = "Master_Barcode";
                    ENTITY_LAYER.Transaction.Transaction.BatchCode = this.cmbBatchNo.SelectedValue.ToString();
                    ENTITY_LAYER.Transaction.Transaction.Barcode = this.txtLastPrintedbarcode.Text;
                    ENTITY_LAYER.Transaction.Transaction.subqty = this.subQty;
                    ENTITY_LAYER.Transaction.Transaction.ProductCode = this.ProductCode;
                    CommonVariable.Result = this.obj_Tran.BL_Transaction();
                    if (CommonVariable.Result.StartsWith("Saved"))
                    {
                        Driver driver = new Driver();
                        this.PrnData.Replace("{Barcode}", CommonVariable.Result.Split('~')[1].ToString()).Replace("{Barcode1}", CommonVariable.Result.Split('|')[1].ToString()).Replace("{Len}", CommonVariable.Result.Split('~')[1].Length.ToString());
                        driver.SendRawData(this.PrinterIP, this.PrnData);
                        this.Transaction("GetProductionPlan");
                    }
                    else if (CommonVariable.Result == "Updated")
                    {
                        this.Transaction("GetProductionPlan");
                    }
                    else
                    {
                        string result = CommonVariable.Result;
                        customStriing = CommonVariable.CustomStriing.Error;
                        string ErrorType = customStriing.ToString();
                        CommonMethods.MessageBoxShow(result, ErrorType);
                    }
                }
            }
            else
            {
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.TransactionType = "Master_Barcode";
                ENTITY_LAYER.Transaction.Transaction.BatchCode = this.cmbBatchNo.SelectedValue.ToString();
                ENTITY_LAYER.Transaction.Transaction.Barcode = this.txtLastPrintedbarcode.Text;
                ENTITY_LAYER.Transaction.Transaction.subqty = this.subQty;
                ENTITY_LAYER.Transaction.Transaction.ProductCode = this.ProductCode;
                CommonVariable.Result = this.obj_Tran.BL_Transaction();
                if (CommonVariable.Result.StartsWith("Saved"))
                {
                    Driver driver = new Driver();
                    this.PrnData.Replace("{Barcode}", CommonVariable.Result.Split('~')[1].ToString()).Replace("{Barcode1}", CommonVariable.Result.Split('|')[1].ToString()).Replace("{Len}", CommonVariable.Result.Split('~')[1].Length.ToString());
                    driver.SendRawData(this.PrinterIP, this.PrnData);
                    this.Transaction("GetProductionPlan");
                }
                else if (CommonVariable.Result == "Updated")
                {
                    this.Transaction("GetProductionPlan");
                }
                else
                {
                    string result = CommonVariable.Result;
                    customStriing = CommonVariable.CustomStriing.Error;
                    string ErrorType = customStriing.ToString();
                    CommonMethods.MessageBoxShow(result, ErrorType);
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Transaction("LoadProdutCode");
              //  this.Transaction("GetIPDetails");
                this.cmbBatchNo.Focus();
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MASTER_BARCODE", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void CmbProductCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.cmbBatchNo.SelectedIndex <= -1)
                    return;
                this.Transaction("GetProductionPlan");
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MASTER_BARCODE", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Transaction("Print");
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MASTER_BARCODE", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Scanner_OnStatusChanged(bool Flag, string Client) => this.Scanner_StatusChange(Flag, Client);

        private void Scanner_StatusChange(bool Flag, string Client)
        {
            try
            {
                if (this.Dispatcher.CheckAccess())
                    this.Dispatcher.BeginInvoke((Delegate)new Master_Barcode_Printing.dlgcannerStatusChange(this.Scanner_StatusChange), (object)Flag, (object)Client);
                else if (Flag)
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        this.txtScanner.Text = "CONNECTED";
                        this.txtScanner.Foreground = (Brush)Brushes.Green;
                    }));
                else
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        this.txtScanner.Text = "NOT CONNECTED";
                        this.txtScanner.Foreground = (Brush)Brushes.Red;
                    }));
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MASTER_BARCODE", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Sacnner_DataReacive(string Barcode, string ClientIP) => this.DataReacive(Barcode, ClientIP);

        private void DataReacive(string Barcode, string Client)
        {
            try
            {
                if (this.Dispatcher.CheckAccess())
                    this.Dispatcher.Invoke((Delegate)new Master_Barcode_Printing.dlgacnnerDataReacive(this.DataReacive), (object)Barcode, (object)Client);
                else
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        if (!(Barcode != ""))
                            return;
                        this.txtLastPrintedbarcode.Text = Barcode;
                        this.Transaction("Save");
                    }));
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MASTER_BARCODE", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void PLC_OnStatusChanged(bool Flag, string Client) => this.PLC_StatusChange(Flag, Client);

        private void PLC_StatusChange(bool Flag, string Client)
        {
            try
            {
                if (this.Dispatcher.CheckAccess())
                    this.Dispatcher.BeginInvoke((Delegate)new Master_Barcode_Printing.dlgPLCStatusChange(this.PLC_StatusChange), (object)Flag, (object)Client);
                else if (Flag)
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        this.txtPLC.Text = "CONNECTED";
                        this.txtPLC.Foreground = (Brush)Brushes.Green;
                    }));
                else
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        this.txtPLC.Text = "NOT CONNECTED";
                        this.txtPLC.Foreground = (Brush)Brushes.Red;
                    }));
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "EST_TEST", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void PLC_DataReacive(string Barcode, string ClientIP) => this.PLCDataReacive(Barcode, ClientIP);

        private void PLCDataReacive(string Barcode, string Client)
        {
            try
            {
                if (this.Dispatcher.CheckAccess())
                    this.Dispatcher.Invoke((Delegate)new Master_Barcode_Printing.dlgPLCDataReacive(this.PLCDataReacive), (object)Barcode, (object)Client);
                else
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        if (!(Barcode != ""))
                            return;
                        this.rbInduvidual.IsChecked = new bool?(true);
                        this.Transaction("Print");
                    }));
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "EST_TEST", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void RbInduvidual_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                this.lblQty.Visibility = Visibility.Hidden;
                this.txtQTy.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MASTER_BARCODE", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void RbBulk_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                this.lblQty.Visibility = Visibility.Visible;
                this.txtQTy.Visibility = Visibility.Visible;
                this.txtQTy.Focus();
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MASTER_BARCODE", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }


    }
}
