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
    /// Interaction logic for Production_Plan.xaml
    /// </summary>
    public partial class Production_Plan : Page
    {
        public Production_Plan()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Transaction.Transaction obj_Tran = new BUSINESS_LAYER.Transaction.Transaction();
        int RefNo = 0;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        #endregion


        private bool ControlValidation()
        {
            if (this.cmbProductCode.SelectedIndex == -1)
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER PRODUCT CODE", CommonVariable.CustomStriing.Information.ToString());
                this.cmbProductCode.Focus();
                return false;
            }
            if (this.txtPrdName.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER PRODUCT NAME", CommonVariable.CustomStriing.Information.ToString());
                this.txtPrdName.Focus();
                return false;
            }
            if (this.txtBrand.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER BRAND NAME", CommonVariable.CustomStriing.Information.ToString());
                this.txtBrand.Focus();
                return false;
            }
            if (this.txtGroup.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER GROUP", CommonVariable.CustomStriing.Information.ToString());
                this.txtGroup.Focus();
                return false;
            }
            if (this.txtPlannedQty.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER PLANNED QTY", CommonVariable.CustomStriing.Information.ToString());
                this.txtPlannedQty.Focus();
                return false;
            }
            if (this.txtSubqty.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER SUB QTY", CommonVariable.CustomStriing.Information.ToString());
                this.txtSubqty.Focus();
                return false;
            }
            if (!(this.txtLot.Text == ""))
                return true;
            CommonMethods.MessageBoxShow("PLEASE ENTER LOT", CommonVariable.CustomStriing.Information.ToString());
            this.txtLot.Focus();
            return false;
        }

        private void Transaction(string Type)
        {
            if (Type == "Save" || Type == "Update" || Type == "Delete")
            {
                ENTITY_LAYER.Transaction.Transaction.ProductCode = this.cmbProductCode.Text;
                ENTITY_LAYER.Transaction.Transaction.Mixgroup = this.txtGroup.Text;
                ENTITY_LAYER.Transaction.Transaction.plannedqty = this.txtPlannedQty.Text;
                ENTITY_LAYER.Transaction.Transaction.subqty = this.txtSubqty.Text;
                ENTITY_LAYER.Transaction.Transaction.lot = this.txtLot.Text;
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.RefNo = this.RefNo;
                CommonVariable.Result = this.obj_Tran.BL_ProductPlanTransaction();
                if (CommonVariable.Result == "Saved")
                {
                    CommonMethods.MessageBoxShow(CommonVariable.DataSaved, CommonVariable.CustomStriing.Successfull.ToString());
                    this.Clear();
                }
                else if (CommonVariable.Result == "Updated")
                {
                    CommonMethods.MessageBoxShow(CommonVariable.DataUpdated, CommonVariable.CustomStriing.Successfull.ToString());
                    this.Clear();
                }
                else if (CommonVariable.Result == "Duplicate")
                    CommonMethods.MessageBoxShow(CommonVariable.Duplicate, CommonVariable.CustomStriing.Information.ToString());
                else if (CommonVariable.Result != "Deleted")
                    CommonMethods.MessageBoxShow(CommonVariable.Result, CommonVariable.CustomStriing.Information.ToString());
            }
            if (Type == "LoadDetails")
            {
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                this.dvgMasterDeatils.ItemsSource = this.obj_Tran.BL_ProductPlanDetails().Tables[0].DefaultView;
            }
            if (!(Type == "LoadProductcode"))
                return;
            ENTITY_LAYER.Transaction.Transaction.Type = Type;
            CommonMethods.FillComboBox(this.cmbProductCode, this.obj_Tran.BL_ProductPlanDetails().Tables[0], "ProductCode", "ProductDetails");
        }

        private void Clear()
        {
            this.RefNo = 0;
            this.cmbProductCode.Text = "";
            this.txtPrdName.Text = "";
            this.txtModelName.Text = "";
            this.txtBrand.Text = "";
            this.txtGroup.Text = "";
            this.txtPlannedQty.Text = "";
            this.txtSubqty.Text = "";
            this.txtLot.Text = "";
            this.Transaction("LoadDetails");
            this.cmbProductCode.Focus();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Transaction("LoadDetails");
                this.Transaction("LoadProductcode");
                this.cmbProductCode.Focus();
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_PLAN", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.dvgMasterDeatils.SelectedItems.Count == 0)
                {
                    if (!this.ControlValidation())
                        return;
                    this.Transaction("Save");
                }
                else
                    CommonMethods.MessageBoxShow("YOU CAN NOT SAVE THE RECORDS PLEASE GO FOR DELETION OR UPDATION", CommonVariable.CustomStriing.Information.ToString());
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_PLAN", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.dvgMasterDeatils.SelectedItems.Count > 0)
                {
                    if (this.dvgMasterDeatils.SelectedItems.Count == 1)
                    {
                        if (!this.ControlValidation())
                            return;
                        this.Transaction("Update");
                    }
                    else
                        CommonMethods.MessageBoxShow("MULTIPLE SELECTION WILL NOT SUPPORT FOR UPDATE", CommonVariable.CustomStriing.Information.ToString());
                }
                else
                    CommonMethods.MessageBoxShow(CommonVariable.RowSelection, CommonVariable.CustomStriing.Information.ToString());
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_PLAN", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.dvgMasterDeatils.SelectedItems.Count > 0)
                {
                    CommonMethods.MessageBoxShow(CommonVariable.DeleteConfirm, CommonVariable.CustomStriing.Question.ToString());
                    if (!(CommonVariable.Result == "YES"))
                        return;
                    for (int index = 0; index < this.dvgMasterDeatils.SelectedItems.Count; ++index)
                    {
                        this.RefNo = Convert.ToInt32(((DataRowView)this.dvgMasterDeatils.SelectedItems[index])["Refno"]);
                        this.Transaction("Delete");
                    }
                    if (CommonVariable.Result == "Deleted")
                    {
                        CommonMethods.MessageBoxShow(CommonVariable.DataDeleted, CommonVariable.CustomStriing.Successfull.ToString());
                        this.Clear();
                    }
                    else
                        CommonMethods.MessageBoxShow(CommonVariable.Result, CommonVariable.CustomStriing.Information.ToString());
                }
                else
                    CommonMethods.MessageBoxShow(CommonVariable.RowSelection, CommonVariable.CustomStriing.Information.ToString());
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_PLAN", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Clear();
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_PLAN", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonVariable.PageOpenClose = "Close";
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_PLAN", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void dvgMasterDeatils_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.dvgMasterDeatils.SelectedItems.Count == 1)
                {
                    DataRowView selectedItem = (DataRowView)this.dvgMasterDeatils.SelectedItems[0];
                    this.RefNo = Convert.ToInt32(selectedItem["Refno"]);
                    this.cmbProductCode.Text = selectedItem["ProductCode"].ToString();
                    this.txtGroup.Text = selectedItem["Mixgroup"].ToString();
                    this.txtPlannedQty.Text = selectedItem["PlannedQty"].ToString();
                    this.txtSubqty.Text = selectedItem["SubQty"].ToString();
                    this.txtLot.Text = selectedItem["Lot"].ToString();
                    this.cmbProductCode.Focus();
                }
                else
                {
                    this.RefNo = 0;
                    this.RefNo = 0;
                    this.cmbProductCode.Text = "";
                    this.txtModelName.Text = "";
                    this.txtPrdName.Text = "";
                    this.txtBrand.Text = "";
                    this.txtGroup.Text = "";
                    this.txtPlannedQty.Text = "";
                    this.txtSubqty.Text = "";
                    this.txtLot.Text = "";
                    this.cmbProductCode.Focus();
                }
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_PLAN", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.S) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.S))
                this.btnSave_Click(sender, (RoutedEventArgs)e);
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.U) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.U))
                this.btnUpdate_Click(sender, (RoutedEventArgs)e);
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.C) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.C))
                this.btnClear_Click(sender, (RoutedEventArgs)e);
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.B) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.B) || Keyboard.IsKeyDown(Key.Escape) && Keyboard.IsKeyDown(Key.Escape))
                this.btnExit_Click(sender, (RoutedEventArgs)e);
            if ((!Keyboard.IsKeyDown(Key.LeftAlt) || !Keyboard.IsKeyDown(Key.D)) && (!Keyboard.IsKeyDown(Key.RightAlt) || !Keyboard.IsKeyDown(Key.D)))
                return;
            this.btnDelete_Click(sender, (RoutedEventArgs)e);
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Clear();
                this.dispatcherTimer.Stop();
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_PLAN", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void CmbProductCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.cmbProductCode.SelectedIndex > -1)
                {
                    DataRowView selectedItem = (DataRowView)this.cmbProductCode.SelectedItem;
                    this.txtPrdName.Text = selectedItem["ProductDetails"].ToString().Split('~')[0];
                    this.txtModelName.Text = selectedItem["ProductDetails"].ToString().Split('~')[1];
                    this.txtBrand.Text = selectedItem["ProductDetails"].ToString().Split('~')[2];
                }
                this.Transaction("LoadProductName");
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_PLAN", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

    }
}
