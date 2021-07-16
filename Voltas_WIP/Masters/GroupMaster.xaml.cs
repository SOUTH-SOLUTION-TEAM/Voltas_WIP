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
using Voltas_WIP.StartUp;

namespace Voltas_WIP.Masters
{
    /// <summary>
    /// Interaction logic for GroupMaster.xaml
    /// </summary>
    public partial class GroupMaster : Page
    {
        public GroupMaster()
        {
            InitializeComponent();
        }

        #region Variable and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Masters.Masters obj_Mast = new BUSINESS_LAYER.Masters.Masters();

        #endregion


        private void Transaction(string Type)
        {
            if (Type == "Save" || Type == "Delete")
            {
                string str = "";
                ENTITY_LAYER.Masters.Masters.GroupID = this.cmbgroupid.Text;
                ENTITY_LAYER.Masters.Masters.Type = Type;
                for (int index = 0; index < this.dvgModules.Items.Count; ++index)
                {
                    DataRowView dataRowView = (DataRowView)this.dvgModules.Items[index];
                    if (dataRowView["Flag"].ToString() == "True")
                        str = str + dataRowView["ModuleName"].ToString() + ",";
                }
                if (str == "")
                {
                    CommonMethods.MessageBoxShow("PLEASE SELECT MODULES FROM LIST VIEW", CommonVariable.CustomStriing.Information.ToString());
                    return;
                }
                ENTITY_LAYER.Masters.Masters.Rights = str;
                CommonVariable.Result = this.obj_Mast.BL_GroupMasterTransaction();
                if (CommonVariable.Result == "Saved")
                {
                    CommonMethods.MessageBoxShow(CommonVariable.DataSaved, CommonVariable.CustomStriing.Successfull.ToString());
                    this.Clear();
                }
                else if (CommonVariable.Result == "Deleted")
                {
                    CommonMethods.MessageBoxShow(CommonVariable.DataDeleted, CommonVariable.CustomStriing.Successfull.ToString());
                    this.Clear();
                }
                else
                    CommonMethods.MessageBoxShow(CommonVariable.Result, CommonVariable.CustomStriing.Information.ToString());
            }
            if (Type == "LoadFormDetails")
            {
                ENTITY_LAYER.Masters.Masters.Type = Type;
                DataTable table = this.obj_Mast.BL_GroupMasterDetails().Tables[0];
                table.Columns["Flag"].ReadOnly = false;
                this.dvgModules.ItemsSource = table.DefaultView;
                this.dvgModules.Columns[1].Width = new DataGridLength(1.0, DataGridLengthUnitType.Star);
            }
            if (!(Type == "getRightsDetails"))
                return;
            this.GridItemUnChecked();
            DataTable dataTable = new DataTable();
            ENTITY_LAYER.Masters.Masters.Type = Type;
            ENTITY_LAYER.Masters.Masters.GroupID = Convert.ToString(this.cmbgroupid.SelectedValue);
            DataTable table1 = this.obj_Mast.BL_GroupMasterDetails().Tables[0];
            if (this.cmbgroupid.SelectedIndex == -1)
                CommonMethods.FillComboBox(this.cmbgroupid, table1, "GroupID", "GroupID");
            else if (table1.Rows.Count > 0)
            {
                string[] strArray = table1.Rows[0]["Rights"].ToString().Split(',');
                for (int index1 = 0; index1 < this.dvgModules.Items.Count; ++index1)
                {
                    for (int index2 = 0; index2 < strArray.Length; ++index2)
                    {
                        DataRowView dataRowView = (DataRowView)this.dvgModules.Items[index1];
                        if (dataRowView["ModuleName"].ToString() == strArray[index2])
                            dataRowView["Flag"] = (object)"True";
                    }
                }
            }
        }

        private void Clear()
        {
            this.cmbgroupid.SelectedIndex = -1;
            this.cmbgroupid.Text = "";
            this.Transaction("getRightsDetails");
            this.GridItemUnChecked();
            this.cmbgroupid.Focus();
        }

        private void GridItemUnChecked()
        {
            this.PCDetails.IsChecked = new bool?(false);
            for (int index = 0; index < this.dvgModules.Items.Count; ++index)
                ((DataRowView)this.dvgModules.Items[index])["Flag"] = (object)"false";
        }

        private void GridItemChecked()
        {
            this.PCDetails.IsChecked = new bool?(true);
            for (int index = 0; index < this.dvgModules.Items.Count; ++index)
                ((DataRowView)this.dvgModules.Items[index])["Flag"] = (object)"True";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Transaction("LoadFormDetails");
                this.Transaction("getRightsDetails");
                this.cmbgroupid.Focus();
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "GROUP_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.cmbgroupid.Text != "")
                {
                    this.Transaction("Save");
                }
                else
                {
                    CommonMethods.MessageBoxShow("PLEASE ENTER GROUP ID", CommonVariable.CustomStriing.Information.ToString());
                    this.cmbgroupid.Focus();
                }
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "GROUP_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.cmbgroupid.SelectedIndex > -1)
                {
                    CommonMethods.MessageBoxShow(CommonVariable.DeleteConfirm, CommonVariable.CustomStriing.Question.ToString());
                    if (!(CommonVariable.Result == "YES"))
                        return;
                    this.Transaction("Delete");
                }
                else
                {
                    CommonMethods.MessageBoxShow("PLEASE SELECT GROUP ID", CommonVariable.CustomStriing.Information.ToString());
                    this.cmbgroupid.Focus();
                }
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "GROUP_MASTER", CommonVariable.UserID);
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
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "GROUP_MASTER", CommonVariable.UserID);
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
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "GROUP_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void PCDetails_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                this.GridItemChecked();
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "GROUP_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void PCDetails_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                this.GridItemUnChecked();
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "GROUP_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void cmbgroupid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.cmbgroupid.SelectedIndex <= -1)
                    return;
                this.Transaction("getRightsDetails");
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "GROUP_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.S) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.S))
                this.btnSave_Click(sender, (RoutedEventArgs)e);
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.C) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.C))
                this.btnClear_Click(sender, (RoutedEventArgs)e);
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.B) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.B) || Keyboard.IsKeyDown(Key.Escape) && Keyboard.IsKeyDown(Key.Escape))
                this.btnExit_Click(sender, (RoutedEventArgs)e);
            if ((!Keyboard.IsKeyDown(Key.LeftAlt) || !Keyboard.IsKeyDown(Key.D)) && (!Keyboard.IsKeyDown(Key.RightAlt) || !Keyboard.IsKeyDown(Key.D)))
                return;
            this.btnDelete_Click(sender, (RoutedEventArgs)e);
        }

    }
}
