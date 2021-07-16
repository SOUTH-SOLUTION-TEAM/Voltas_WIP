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
    /// Interaction logic for Repair.xaml
    /// </summary>
    public partial class Repair : Page
    {
        public Repair()
        {
            InitializeComponent();
        }

        private BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        private BUSINESS_LAYER.Transaction.Transaction obj_Tran = new BUSINESS_LAYER.Transaction.Transaction();
        private NetworkClient obj_Client = new NetworkClient();
        private DataTable dt = new DataTable();
        private DataTable dtCode = new DataTable();
        private string ProductCode = "";
        private string subQty = "";
        private string RepairCode = "";
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
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "REPAIR", CommonVariable.UserID);
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
                ENTITY_LAYER.Transaction.Transaction.TransactionType = nameof(Repair);
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
                }
            }
            if (Type == "RepairCode")
            {
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.TransactionType = nameof(Repair);
                ENTITY_LAYER.Transaction.Transaction.ModelName = this.txtModelNo.Text;
                this.dtCode = this.obj_Tran.BL_TransactionDetails().Tables[0];
                this.lsrRepairCode.ItemsSource = this.dtCode.DefaultView;
            }
            if (Type == "GetIPDetails")
            {
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.TransactionType = nameof(Repair);
                this.dt = this.obj_Tran.BL_TransactionDetails().Tables[0];
                this.obj_Client = (NetworkClient)null;
                this.obj_Client = new NetworkClient();
                this.obj_Client.OnDataArrived += new NetworkClient.DataArrivalHandler(this.DataReacive);
                this.obj_Client.OnScannerStatusChanged += new NetworkClient.ScannerStatusChangeHandler(this.Scanner_OnStatusChanged);
                this.obj_Client.connection(this.dt.Rows[0]["IPAddress"].ToString(), Convert.ToInt32(Convert.ToInt32(this.dt.Rows[0]["PortNo"])));
            }
            if (!(Type == "Save"))
                return;
            ENTITY_LAYER.Transaction.Transaction.Type = Type;
            ENTITY_LAYER.Transaction.Transaction.TransactionType = nameof(Repair);
            ENTITY_LAYER.Transaction.Transaction.BatchCode = this.cmbBatchNo.SelectedValue.ToString();
            ENTITY_LAYER.Transaction.Transaction.Barcode = this.txtLastProductbarcode.Text;
            ENTITY_LAYER.Transaction.Transaction.subqty = this.subQty;
            this.RepairCode = "";
            for (int index = 0; index < this.dtCode.Rows.Count; ++index)
            {
                if (this.dtCode.Rows[index]["Flag"].ToString() == "True")
                {
                    if (this.RepairCode == "")
                        this.RepairCode = this.RepairCode;
                    else
                        this.RepairCode = this.RepairCode + "|" + this.dtCode.Rows[index]["RepairCode"].ToString().Split('-')[0];
                }
            }
            ENTITY_LAYER.Transaction.Transaction.RepairCode = this.RepairCode;
            CommonVariable.Result = this.obj_Tran.BL_Transaction();
            if (CommonVariable.Result != "OK")
                CommonMethods.MessageBoxShow(CommonVariable.Result, CommonVariable.CustomStriing.Error.ToString());
            else
                this.Transaction("GetProductionPlan");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Transaction("RepairCode");
                this.Transaction("LoadProdutCode");
               // this.Transaction("GetIPDetails");
                this.cmbBatchNo.Focus();
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "REPAIR", CommonVariable.UserID);
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
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "REPAIR", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Transaction("Save");
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "REPAIR", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void ChkModel_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                bool? isChecked = this.chkModel.IsChecked;
                bool flag = false;
                if (!(isChecked.GetValueOrDefault() == flag & isChecked.HasValue))
                    return;
                for (int index = 0; index < this.dtCode.Rows.Count; ++index)
                    this.dtCode.Rows[index]["Flag"] = (object)false;
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "REPAIR", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void ChkModel_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                bool? isChecked = this.chkModel.IsChecked;
                bool flag = true;
                if (!(isChecked.GetValueOrDefault() == flag & isChecked.HasValue))
                    return;
                for (int index = 0; index < this.dtCode.Rows.Count; ++index)
                    this.dtCode.Rows[index]["Flag"] = (object)true;
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "REPAIR", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Scanner_OnStatusChanged(bool Flag, string Client) => this.Scanner_StatusChange(Flag, Client);

        private void Scanner_StatusChange(bool Flag, string Client)
        {
            try
            {
                if (this.Dispatcher.CheckAccess())
                    this.Dispatcher.BeginInvoke((Delegate)new Repair.dlgcannerStatusChange(this.Scanner_StatusChange), (object)Flag, (object)Client);
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
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "REPAIR", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Sacnner_DataReacive(string Barcode, string ClientIP) => this.DataReacive(Barcode, ClientIP);

        private void DataReacive(string Barcode, string Client)
        {
            try
            {
                if (this.Dispatcher.CheckAccess())
                    this.Dispatcher.Invoke((Delegate)new Repair.dlgacnnerDataReacive(this.DataReacive), (object)Barcode, (object)Client);
                else
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        if (!(Barcode != ""))
                            return;
                        this.txtLastProductbarcode.Text = Barcode;
                    }));
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "REPAIR", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

    }
}
