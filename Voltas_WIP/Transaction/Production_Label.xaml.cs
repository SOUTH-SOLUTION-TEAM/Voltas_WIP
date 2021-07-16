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
    /// Interaction logic for Production_Label.xaml
    /// </summary>
    public partial class Production_Label : Page
    {
        public Production_Label()
        {
            InitializeComponent();
        }
        private BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        private BUSINESS_LAYER.Transaction.Transaction obj_Tran = new BUSINESS_LAYER.Transaction.Transaction();
        private NetworkClient obj_Client = new NetworkClient();
        private DataTable dt = new DataTable();
        private string ProductCode = "";
        private string subQty = "";
        private string PrinterIP = "";
        private string Port = "";
        private string PrnData = "";
        private delegate void dlgcannerStatusChange(bool Flag, string Client);

        private delegate void dlgacnnerDataReacive(string Barcode, string Client);

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
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCT_LABEL", CommonVariable.UserID);
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
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCT_LABEL", CommonVariable.UserID);
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
                ENTITY_LAYER.Transaction.Transaction.TransactionType = "Slave_Barcode";
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
                    this.txtScanCount.Text = table.Rows[0]["ScannedQty"].ToString();
                }
            }
            if (Type == "GetIPDetails")
            {
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.TransactionType = "Slave_Barcode";
                this.dt = this.obj_Tran.BL_TransactionDetails().Tables[0];
                if (this.dt.Rows.Count > 0 && this.dt.Rows[0]["HardwareType"].ToString() == "SCANNER")
                {
                    this.obj_Client = (NetworkClient)null;
                    this.obj_Client = new NetworkClient();
                    this.obj_Client.OnDataArrived += new NetworkClient.DataArrivalHandler(this.DataReacive);
                    this.obj_Client.OnScannerStatusChanged += new NetworkClient.ScannerStatusChangeHandler(this.Scanner_OnStatusChanged);
                    this.obj_Client.connection(this.dt.Rows[0]["IPAddress"].ToString(), Convert.ToInt32(Convert.ToInt32(this.dt.Rows[0]["PortNo"])));
                }
            }
            if (!(Type == "Save"))
                ;
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
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCT_LABEL", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Scanner_OnStatusChanged(bool Flag, string Client) => this.Scanner_StatusChange(Flag, Client);

        private void Scanner_StatusChange(bool Flag, string Client)
        {
            try
            {
                if (this.Dispatcher.CheckAccess())
                    this.Dispatcher.BeginInvoke((Delegate)new Production_Label.dlgcannerStatusChange(this.Scanner_StatusChange), (object)Flag, (object)Client);
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
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCT_LABEL", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Sacnner_DataReacive(string Barcode, string ClientIP) => this.DataReacive(Barcode, ClientIP);

        private void DataReacive(string Barcode, string Client)
        {
            try
            {
                if (this.Dispatcher.CheckAccess())
                    this.Dispatcher.Invoke((Delegate)new Production_Label.dlgacnnerDataReacive(this.DataReacive), (object)Barcode, (object)Client);
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
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCT_LABEL", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

    }
}
