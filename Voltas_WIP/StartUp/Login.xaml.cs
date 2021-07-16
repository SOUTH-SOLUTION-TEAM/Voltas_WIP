using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Voltas_WIP.CommonClasses;

namespace Voltas_WIP.StartUp
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
            
        }
        #region Variable and Objects
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

        private void ValidateLogin()
        {
            ENTITY_LAYER.Login.Login.UserID = this.txtUserID.Text;
            ENTITY_LAYER.Login.Login.Password = this.txtPassword.Password;
            ENTITY_LAYER.Login.Login.Type = nameof(Login);
            CommonVariable.Result = this.obj_Login.BL_Login();
            if (CommonVariable.Result.StartsWith("VALID CREDENTIAL"))
            {
                CommonVariable.UserID = this.txtUserID.Text;
                CommonVariable.UserName = CommonVariable.Result.Split('+')[1].ToString();
                CommonVariable.Rights = CommonVariable.Result.Split('+')[2].ToString();
                CommonMethods commonMethods = new CommonMethods();
                this.NavigationService.Navigate((object)new MainWindow());
            }
            else if (CommonVariable.Result == "INVALID USER ID")
            {
                CommonMethods.MessageBoxShow(CommonVariable.Result, CommonVariable.CustomStriing.Information.ToString());
                this.txtUserID.Text = "";
                this.txtUserID.Focus();
            }
            else if (CommonVariable.Result == "INVALID PASSWORD")
            {
                CommonMethods.MessageBoxShow(CommonVariable.Result, CommonVariable.CustomStriing.Information.ToString());
                this.txtPassword.Password = "";
                this.txtPassword.Focus();
            }
            else if (CommonVariable.Result == "FIRST TIME LOGIN")
            {
                if (this.txtUserID.Text.ToUpper() == "SARBLR" && this.txtPassword.Password.ToUpper() == "SARBLR")
                {
                    CommonVariable.UserName = "SARBLR";
                    CommonVariable.Rights = "USER MASTER,GROUP MASTER";
                    CommonVariable.UserID = "SARBLR";
                    this.NavigationService.Navigate((object)new MainWindow());
                }
                else
                    CommonMethods.MessageBoxShow("FIRST TIME LOGIN. PLEASE ENTER VALID USER ID OR PASSWORD", CommonVariable.CustomStriing.Information.ToString());
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
            if (this.txtPassword.IsFocused)
            {
                if (capsLock)
                    this.txtPasswordPopup.IsOpen = true;
                else
                    this.txtPasswordPopup.IsOpen = false;
            }
            else
                this.txtPasswordPopup.IsOpen = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                while (this.NavigationService.CanGoBack)
                    this.NavigationService.RemoveBackEntry();
                Version version = Assembly.GetExecutingAssembly().GetName().Version;
                this.txtVersion.Text = string.Format(this.txtVersion.Text, (object)version.Major, (object)version.Minor, (object)version.Build, (object)version.Revision);
                this.ShowCapslock();
                this.txtUserID.Focus();
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LOGIN", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.txtUserID.Text == "")
                {
                    CommonMethods.MessageBoxShow("PLEASE ENTER THE USER ID", CommonVariable.CustomStriing.Information.ToString());
                    this.txtUserID.Focus();
                }
                else if (this.txtPassword.Password == "")
                {
                    CommonMethods.MessageBoxShow("PLEASE ENTER THE PASSWORD", CommonVariable.CustomStriing.Information.ToString());
                    this.txtPassword.Focus();
                }
                else
                    this.ValidateLogin();
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LOGIN", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SendKeys.SendWait("^w");
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LOGIN", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void LinkForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.txtUserID.Text == "")
                {
                    CommonMethods.MessageBoxShow("PLEASE ENTER THE USER ID", CommonVariable.CustomStriing.Information.ToString());
                    this.txtUserID.Focus();
                }
                else
                {
                    ENTITY_LAYER.Login.Login.UserID = this.txtUserID.Text;
                    ENTITY_LAYER.Login.Login.Type = "GetRights";
                    CommonVariable.Result = this.obj_Login.BL_Login();
                    if (CommonVariable.Result.Contains("FORGOT PASSWORD"))
                        this.NavigationService.Navigate((object)new ForgotPassword());
                    else
                        CommonMethods.MessageBoxShow("ACCESS DENIED", CommonVariable.CustomStriing.Information.ToString());
                }
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LOGIN", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void LinkChangePassword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.txtUserID.Text == "")
                {
                    CommonMethods.MessageBoxShow("PLEASE ENTER THE USER ID", CommonVariable.CustomStriing.Information.ToString());
                    this.txtUserID.Focus();
                }
                else
                {
                    ENTITY_LAYER.Login.Login.UserID = this.txtUserID.Text;
                    ENTITY_LAYER.Login.Login.Type = "GetRights";
                    CommonVariable.Result = this.obj_Login.BL_Login();
                    if (CommonVariable.Result.Contains("CHANGE PASSWORD"))
                        this.NavigationService.Navigate((object)new ChangePassword());
                    else
                        CommonMethods.MessageBoxShow("ACCESS DENIED", CommonVariable.CustomStriing.Information.ToString());
                }
            }
            catch (Exception ex)
            {
                this.obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LOGIN", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.L) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.L))
                this.btnLogin_Click(sender, (RoutedEventArgs)e);
            if ((!Keyboard.IsKeyDown(Key.LeftAlt) || !Keyboard.IsKeyDown(Key.E)) && (!Keyboard.IsKeyDown(Key.RightAlt) || !Keyboard.IsKeyDown(Key.E)))
                return;
            this.btnExit_Click(sender, (RoutedEventArgs)e);
        }

        private void TxtPassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != Key.Return)
                return;
            this.btnLogin_Click(sender, (RoutedEventArgs)e);
        }

    }
   
}
