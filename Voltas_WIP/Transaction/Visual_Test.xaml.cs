using Sato_Network_Client_DLL;
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
    /// Interaction logic for Visual_Test.xaml
    /// </summary>
    public partial class Visual_Test : Page
    {
        public Visual_Test()
        {
            InitializeComponent();
        }

        private BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        private BUSINESS_LAYER.Transaction.Transaction obj_Tran = new BUSINESS_LAYER.Transaction.Transaction();
        private NetworkClient obj_Client = new NetworkClient();
        private NetworkClient obj_Client1 = new NetworkClient();
        private DataTable dt = new DataTable();
        private string ProductCode = "";
        private string Status = "";
        private string SubQty = "";

        private delegate void dlgcannerStatusChange(bool Flag, string Client);

        private delegate void dlgacnnerDataReacive(string Barcode, string Client);

        private delegate void dlgPLCStatusChange(bool Flag, string Client);

        private delegate void dlgPLCDataReacive(string Barcode, string Client);


        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonVariable.PageOpenClose = "Close";
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "VISUAL_TEST", CommonVariable.UserID);
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
                ENTITY_LAYER.Transaction.Transaction.TransactionType = "VISUAL_TEST";
                ENTITY_LAYER.Transaction.Transaction.ProductCode = this.cmbBatchNo.SelectedValue.ToString();
                DataTable table = this.obj_Tran.BL_TransactionDetails().Tables[0];
                if (table.Rows.Count > 0)
                {
                    this.ProductCode = table.Rows[0]["ProductCode"].ToString();
                    this.txtProductName.Text = table.Rows[0]["ProductName"].ToString();
                    this.txtModelNo.Text = table.Rows[0]["ModelName"].ToString();
                    this.txtBrand.Text = table.Rows[0]["BrandName"].ToString();
                    this.txtGroup.Text = table.Rows[0]["Mixgroup"].ToString();
                    this.txtLot.Text = table.Rows[0]["Lot"].ToString();
                    this.txtOKCount.Text = table.Rows[0]["ScannedQty"].ToString();
                }
            }
            if (Type == "GetIPDetails")
            {
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.TransactionType = "VISUAL_TEST";
                this.dt = this.obj_Tran.BL_TransactionDetails().Tables[0];
                for (int index = 0; index < this.dt.Rows.Count; ++index)
                {
                    if (index == 0 && this.dt.Rows[index]["HardwareType"].ToString() == "SCANNER")
                    {
                        this.obj_Client = (NetworkClient)null;
                        this.obj_Client = new NetworkClient();
                        this.obj_Client.OnDataArrived += new NetworkClient.DataArrivalHandler(this.DataReacive);
                        this.obj_Client.OnScannerStatusChanged += new NetworkClient.ScannerStatusChangeHandler(this.Scanner_OnStatusChanged);
                        this.obj_Client.connection(this.dt.Rows[0]["IPAddress"].ToString(), Convert.ToInt32(Convert.ToInt32(this.dt.Rows[0]["PortNo"])));
                    }
                    if (index == 1 && this.dt.Rows[index]["HardwareType"].ToString() == "PLC")
                    {
                        this.obj_Client1 = (NetworkClient)null;
                        this.obj_Client1 = new NetworkClient();
                        this.obj_Client1.OnDataArrived += new NetworkClient.DataArrivalHandler(this.PLCDataReacive);
                        this.obj_Client1.OnScannerStatusChanged += new NetworkClient.ScannerStatusChangeHandler(this.PLC_OnStatusChanged);
                        this.obj_Client1.connection(this.dt.Rows[0]["IPAddress"].ToString(), Convert.ToInt32(Convert.ToInt32(this.dt.Rows[0]["PortNo"])));
                    }
                }
            }
            if (!(Type == "Save"))
                return;
            ENTITY_LAYER.Transaction.Transaction.Type = Type;
            ENTITY_LAYER.Transaction.Transaction.TransactionType = "VISUAL_TEST";
            ENTITY_LAYER.Transaction.Transaction.BatchCode = this.cmbBatchNo.SelectedValue.ToString();
            ENTITY_LAYER.Transaction.Transaction.Barcode = this.txtLastPrintedbarcode.Text;
            ENTITY_LAYER.Transaction.Transaction.subqty = this.SubQty;
            ENTITY_LAYER.Transaction.Transaction.Status = this.Status;
            CommonVariable.Result = this.obj_Tran.BL_Transaction();
            if (CommonVariable.Result != "OK")
                CommonMethods.MessageBoxShow(CommonVariable.Result, CommonVariable.CustomStriing.Error.ToString());
            else if (CommonVariable.Result == "OK")
                this.Transaction("GetProductionPlan");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Transaction("LoadProdutCode");
                //this.Transaction("GetIPDetails");
                this.cmbBatchNo.Focus();
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "VISUAL_TEST", CommonVariable.UserID);
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
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "VISUAL_TEST", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Scanner_OnStatusChanged(bool Flag, string Client) => this.Scanner_StatusChange(Flag, Client);

        private void Scanner_StatusChange(bool Flag, string Client)
        {
            try
            {
                if (this.Dispatcher.CheckAccess())
                    this.Dispatcher.BeginInvoke((Delegate)new Visual_Test.dlgcannerStatusChange(this.Scanner_StatusChange), (object)Flag, (object)Client);
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
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "VISUAL_TEST", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Sacnner_DataReacive(string Barcode, string ClientIP) => this.DataReacive(Barcode, ClientIP);

        private void DataReacive(string Barcode, string Client)
        {
            try
            {
                if (this.Dispatcher.CheckAccess())
                    this.Dispatcher.Invoke((Delegate)new Visual_Test.dlgacnnerDataReacive(this.DataReacive), (object)Barcode, (object)Client);
                else
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        if (!(Barcode != ""))
                            return;
                        this.txtLastPrintedbarcode.Text = Barcode;
                    }));
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "VISUAL_TEST", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void PLC_OnStatusChanged(bool Flag, string Client) => this.PLC_StatusChange(Flag, Client);

        private void PLC_StatusChange(bool Flag, string Client)
        {
            try
            {
                if (this.Dispatcher.CheckAccess())
                    this.Dispatcher.BeginInvoke((Delegate)new Visual_Test.dlgPLCStatusChange(this.PLC_StatusChange), (object)Flag, (object)Client);
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
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "VISUAL_TEST", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void PLC_DataReacive(string Barcode, string ClientIP) => this.PLCDataReacive(Barcode, ClientIP);

        private void PLCDataReacive(string Barcode, string Client)
        {
            try
            {
                if (this.Dispatcher.CheckAccess())
                    this.Dispatcher.Invoke((Delegate)new Visual_Test.dlgPLCDataReacive(this.PLCDataReacive), (object)Barcode, (object)Client);
                else
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        if (!(Barcode != ""))
                            ;
                    }));
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "VISUAL_TEST", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void BtnNG_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Status = "NG";
                this.Transaction("Save");
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "VISUAL_TEST", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Status = "OK";
                this.Transaction("Save");
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "VISUAL_TEST", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

    }
}
