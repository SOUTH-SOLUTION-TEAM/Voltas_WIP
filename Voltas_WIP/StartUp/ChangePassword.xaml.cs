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

namespace Voltas_WIP.StartUp
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Page
    {
        public ChangePassword()
        {
            InitializeComponent();
        }
        #region Variables and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Login.Login obj_Login = new BUSINESS_LAYER.Login.Login();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        #endregion
        private void ShowCapslock()
        {
            this.dispatcherTimer.Tick += new EventHandler(this.dispatcherTimer_Tick);
            this.dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            this.dispatcherTimer.Start();
        }

        private void Clear()
        {
            this.txtUserID.Text = "";
            this.txtOldPassowrd.Password = "";
            this.txtNewPassword.Password = "";
            this.txtConfirmedPassword.Password = "";
            this.txtUserID.Focus();
        }

        private void Transaction()
        {
            ENTITY_LAYER.Login.Login.UserID = this.txtUserID.Text;
            ENTITY_LAYER.Login.Login.Password = this.txtOldPassowrd.Password;
            ENTITY_LAYER.Login.Login.ConfirmPassword = this.txtConfirmedPassword.Password;
            ENTITY_LAYER.Login.Login.Type = nameof(ChangePassword);
            CommonVariable.Result = this.obj_Login.BL_Login();
            if (CommonVariable.Result == "Updated")
            {
                CommonMethods.MessageBoxShow(CommonVariable.DataUpdated, CommonVariable.CustomStriing.Successfull.ToString());
                this.Clear();
            }
            else if (CommonVariable.Result == "INVALID USER ID")
            {
                CommonMethods.MessageBoxShow(CommonVariable.Result, CommonVariable.CustomStriing.Information.ToString());
                this.txtUserID.Focus();
            }
            else if (CommonVariable.Result == "INVALID PASSWORD")
            {
                CommonMethods.MessageBoxShow(CommonVariable.Result, CommonVariable.CustomStriing.Information.ToString());
                this.txtOldPassowrd.Focus();
            }
            else
            {
                CommonMethods.MessageBoxShow(CommonVariable.Result, CommonVariable.CustomStriing.Information.ToString());
                this.txtUserID.Focus();
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            bool capsLock = Console.CapsLock;
            if (this.txtConfirmedPassword.IsFocused || this.txtOldPassowrd.IsFocused || this.txtNewPassword.IsFocused)
            {
                if (capsLock)
                    this.txtPasswordPopup.IsOpen = true;
                else
                    this.txtPasswordPopup.IsOpen = false;
            }
            else
                this.txtPasswordPopup.IsOpen = false;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Clear();
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CHANGE_PASSWORD", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtUserID.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER USER ID", CommonVariable.CustomStriing.Information.ToString());
                this.txtUserID.Focus();
            }
            else if (this.txtOldPassowrd.Password == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER OLD PASSWORD", CommonVariable.CustomStriing.Information.ToString());
                this.txtOldPassowrd.Focus();
            }
            else if (this.txtNewPassword.Password == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER NEW PASSWORD", CommonVariable.CustomStriing.Information.ToString());
                this.txtNewPassword.Focus();
            }
            else if (this.txtConfirmedPassword.Password == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER CONFIRMED PASSWORD", CommonVariable.CustomStriing.Information.ToString());
                this.txtConfirmedPassword.Focus();
            }
            else if (this.txtNewPassword.Password != this.txtConfirmedPassword.Password)
            {
                CommonMethods.MessageBoxShow("NEW AND CONFIRMED PASWWORD IS NOT MATCHING", CommonVariable.CustomStriing.Information.ToString());
                this.txtNewPassword.Focus();
            }
            else
                this.Transaction();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
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
                this.NavigationService.Navigate((object)new Voltas_WIP.StartUp.Login());
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CHANGE_PASSWORD", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.S) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.S))
                this.btnsave_Click(sender, (RoutedEventArgs)e);
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.C) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.C))
                this.btnClear_Click(sender, (RoutedEventArgs)e);
            if ((!Keyboard.IsKeyDown(Key.LeftAlt) || !Keyboard.IsKeyDown(Key.B)) && (!Keyboard.IsKeyDown(Key.RightAlt) || !Keyboard.IsKeyDown(Key.B)))
                return;
            this.btnExit_Click(sender, (RoutedEventArgs)e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.txtUserID.Focus();
                this.ShowCapslock();
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CHANGE_PASSWORD", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }
    }
}
