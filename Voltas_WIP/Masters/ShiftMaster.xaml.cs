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
using System.Windows.Shapes;
using Voltas_WIP.CommonClasses;

namespace Voltas_WIP.Masters
{
    /// <summary>
    /// Interaction logic for ShiftMaster.xaml
    /// </summary>
    public partial class ShiftMaster : Page
    {
        public ShiftMaster()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Masters.Masters obj_Mast = new BUSINESS_LAYER.Masters.Masters();
        int RefNo = 0;
        string shifttime = "", BreakTiming = "";
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        #endregion
        private bool ControlValidation()
        {
            if (this.cmbshiftname.SelectedIndex == -1)
            {
                CommonMethods.MessageBoxShow("PLEASE SELECT SHIFT NAME", CommonVariable.CustomStriing.Information.ToString());
                this.cmbshiftname.Focus();
                return false;
            }
            if (this.txtstime1.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER SHIFT TIME 1", CommonVariable.CustomStriing.Information.ToString());
                this.txtstime1.Focus();
                return false;
            }
            if (this.txtstime2.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER SHIFT TIME 2", CommonVariable.CustomStriing.Information.ToString());
                this.txtstime2.Focus();
                return false;
            }
            if (this.txtstime3.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER SHIFT TIME 3", CommonVariable.CustomStriing.Information.ToString());
                this.txtstime3.Focus();
                return false;
            }
            if (this.txtstime4.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER SHIFT TIME 4", CommonVariable.CustomStriing.Information.ToString());
                this.txtstime4.Focus();
                return false;
            }
            if (this.cmbBreak.SelectedIndex == -1)
            {
                CommonMethods.MessageBoxShow("PLEASE SELECT BREAK", CommonVariable.CustomStriing.Information.ToString());
                this.cmbBreak.Focus();
                return false;
            }
            if (this.txtB1.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER BREAK TIME 1", CommonVariable.CustomStriing.Information.ToString());
                this.txtB1.Focus();
                return false;
            }
            if (this.txtB2.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER BREAK TIME 2", CommonVariable.CustomStriing.Information.ToString());
                this.txtB2.Focus();
                return false;
            }
            if (this.txtB3.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER BREAK TIME 3", CommonVariable.CustomStriing.Information.ToString());
                this.txtB3.Focus();
                return false;
            }
            if (this.txtB4.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER BREAK TIME 4", CommonVariable.CustomStriing.Information.ToString());
                this.txtB4.Focus();
                return false;
            }
            if (this.txtwrkhrs.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER TOTAL WORKING HRS", CommonVariable.CustomStriing.Information.ToString());
                this.txtB4.Focus();
                return false;
            }
            if (this.cmbstatus.SelectedIndex != -1)
                return true;
            CommonMethods.MessageBoxShow("PLEASE SELECT STATUS", CommonVariable.CustomStriing.Information.ToString());
            this.cmbstatus.Focus();
            return false;
        }

        private void merging()
        {
            this.shifttime = this.txtstime1.Text.PadLeft(2, '0') + ":" + this.txtstime2.Text.PadLeft(2, '0') + " " + this.txtstime3.Text.PadLeft(2, '0') + ":" + this.txtstime4.Text.PadLeft(2, '0');
            this.BreakTiming = this.txtB1.Text.PadLeft(2, '0') + ":" + this.txtB2.Text.PadLeft(2, '0') + " " + this.txtB3.Text.PadLeft(2, '0') + ":" + this.txtB4.Text.PadLeft(2, '0');
        }

        private void Transaction(string Type)
        {
            if (Type == "Save" || Type == "Update" || Type == "Delete")
            {
                this.merging();
                ENTITY_LAYER.Masters.Masters.ShiftName = this.cmbshiftname.Text;
                ENTITY_LAYER.Masters.Masters.Shifttiming = this.shifttime.ToString();
                ENTITY_LAYER.Masters.Masters.ShiftBreak = this.cmbBreak.Text;
                ENTITY_LAYER.Masters.Masters.BreakTime = this.BreakTiming.ToString();
                ENTITY_LAYER.Masters.Masters.TotalWorkingHrs = this.txtwrkhrs.Text;
                ENTITY_LAYER.Masters.Masters.Status = this.cmbstatus.Text;
                ENTITY_LAYER.Masters.Masters.Type = Type;
                ENTITY_LAYER.Masters.Masters.RefNo = this.RefNo;
                CommonVariable.Result = this.obj_Mast.BL_ShiftMasterTransaction();
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
            if (!(Type == "LoadDetails"))
                return;
            ENTITY_LAYER.Masters.Masters.Type = Type;
            this.dvgMasterDeatils.ItemsSource = this.obj_Mast.BL_ShiftMasterDetails().Tables[0].DefaultView;
            this.dvgMasterDeatils.Columns[6].Width = new DataGridLength(1.0, DataGridLengthUnitType.Star);
        }

        private void Clear()
        {
            this.cmbshiftname.Text = "";
            this.txtstime1.Text = "";
            this.txtstime2.Text = "";
            this.txtstime3.Text = "";
            this.txtstime4.Text = "";
            this.txtB1.Text = "";
            this.txtB2.Text = "";
            this.txtB3.Text = "";
            this.txtB4.Text = "";
            this.cmbBreak.Text = "";
            this.txtwrkhrs.Text = "";
            this.Transaction("LoadDetails");
            this.RefNo = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Transaction("LoadDetails");
                this.cmbshiftname.Focus();
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "Shift_MASTER", CommonVariable.UserID);
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
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "Shift_MASTER", CommonVariable.UserID);
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
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "Shift_MASTER", CommonVariable.UserID);
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
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "Shift_MASTER", CommonVariable.UserID);
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
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "Shift_MASTER", CommonVariable.UserID);
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
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "Shift_MASTER", CommonVariable.UserID);
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
                    this.cmbshiftname.Text = selectedItem["ShiftName"].ToString();
                    this.cmbBreak.Text = selectedItem["ShiftBreak"].ToString();
                    this.cmbstatus.Text = selectedItem["Status"].ToString();
                    this.txtwrkhrs.Text = selectedItem["WorkingHrs"].ToString();
                    if (selectedItem["ShiftTime"].ToString() != "")
                    {
                        this.txtstime1.Text = selectedItem["ShiftTime"].ToString().Split(' ')[0].Split(':')[0].ToString();
                        this.txtstime2.Text = selectedItem["ShiftTime"].ToString().Split(' ')[0].Split(':')[1].ToString();
                        this.txtstime3.Text = selectedItem["ShiftTime"].ToString().Split(' ')[1].Split(':')[0].ToString();
                        this.txtstime4.Text = selectedItem["ShiftTime"].ToString().Split(' ')[1].Split(':')[1].ToString();
                    }
                    if (selectedItem["BreakTime"].ToString() != "")
                    {
                        this.txtB1.Text = selectedItem["BreakTime"].ToString().Split(' ')[0].Split(':')[0].ToString();
                        this.txtB2.Text = selectedItem["BreakTime"].ToString().Split(' ')[0].Split(':')[1].ToString();
                        this.txtB3.Text = selectedItem["BreakTime"].ToString().Split(' ')[1].Split(':')[0].ToString();
                        this.txtB4.Text = selectedItem["BreakTime"].ToString().Split(' ')[1].Split(':')[1].ToString();
                    }
                    this.cmbshiftname.Focus();
                }
                else
                {
                    this.RefNo = 0;
                    this.cmbshiftname.Text = "";
                    this.txtstime1.Text = "";
                    this.txtstime2.Text = "";
                    this.txtstime3.Text = "";
                    this.txtstime4.Text = "";
                    this.txtB1.Text = "";
                    this.txtB2.Text = "";
                    this.txtB3.Text = "";
                    this.txtB4.Text = "";
                    this.cmbstatus.Text = "";
                    this.cmbBreak.Text = "";
                    this.txtwrkhrs.Text = "";
                    this.cmbshiftname.Focus();
                }
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "Shift_MASTER", CommonVariable.UserID);
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

        private void Txtstime1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                CommonMethods.NumericValue(e);
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SHIFT_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
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
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "IP_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }
    }
}

