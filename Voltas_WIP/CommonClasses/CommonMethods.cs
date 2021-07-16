// Decompiled with JetBrains decompiler
// Type: Voltas_WIP.CommonClasses.CommonMethods
// Assembly: Voltas_WIP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1DB10D9-18A7-4F84-8868-D2A49100A407
// Assembly location: C:\Users\sar.puttaraju.ah\Desktop\volt..xbap_fcb01545d3e3f744_0001.0000_abdc6ecc1eecd744\Voltas_WIP.exe

using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;

namespace Voltas_WIP.CommonClasses
{
    public class CommonMethods
    {
        public static string DataTableToString(DataTable dt1)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int index1 = 0; index1 < dt1.Rows.Count; ++index1)
                {
                    for (int index2 = 0; index2 < dt1.Columns.Count; ++index2)
                        stringBuilder.AppendFormat("{0}:{1}$", (object)dt1.Columns[index2].ColumnName, dt1.Rows[index1][index2]);
                }
                return stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DigitsOnly(TextCompositionEventArgs e)
        {
            try
            {
                if (!(e.Text != ""))
                    return;
                char c = Convert.ToChar(e.Text);
                if (e.Text == ".")
                    e.Handled = false;
                else if (char.IsNumber(c))
                    e.Handled = false;
                else
                    e.Handled = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void MessageBoxShow(string Description, string ErrorType)
        {
            try
            {
                if (ErrorType == "Successfull")
                {
                    int num = (int)MessageBox.Show(Description, ErrorType, MessageBoxButtons.OK, MessageBoxIcon.None);
                    CommonVariable.Result = "OK";
                }
                if (ErrorType == "Error")
                {
                    int num = (int)MessageBox.Show(Description, ErrorType, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    CommonVariable.Result = "OK";
                }
                if (ErrorType == "Information")
                {
                    int num = (int)MessageBox.Show(Description, ErrorType, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    CommonVariable.Result = "OK";
                }
                if (!(ErrorType == "Question"))
                    return;
                CommonVariable.Result = MessageBox.Show(Description, ErrorType, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes ? "NO" : "YES";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void FillComboBox(System.Windows.Controls.ComboBox ComboBox, DataTable dt, string DisPlayMember)
        {
            try
            {
                ComboBox.ItemsSource = (IEnumerable)null;
                ComboBox.DisplayMemberPath = "";
                ComboBox.ItemsSource = (IEnumerable)dt.DefaultView;
                ComboBox.DisplayMemberPath = DisPlayMember;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UNFill_ComboBox(System.Windows.Controls.ComboBox ComboBox)
        {
            try
            {
                ComboBox.ItemsSource = (IEnumerable)null;
                ComboBox.DisplayMemberPath = "";
                ComboBox.SelectedValuePath = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void NumericValue(TextCompositionEventArgs e)
        {
            try
            {
                if (!(e.Text != ""))
                    return;
                if (char.IsNumber(Convert.ToChar(e.Text)))
                    e.Handled = false;
                else
                    e.Handled = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void FillComboBox(
          System.Windows.Controls.ComboBox ComboBox,
          DataTable dt,
          string DisPlayMember,
          string ValueMember)
        {
            try
            {
                ComboBox.ItemsSource = (IEnumerable)null;
                ComboBox.DisplayMemberPath = "";
                ComboBox.ItemsSource = (IEnumerable)dt.DefaultView;
                ComboBox.DisplayMemberPath = DisPlayMember;
                ComboBox.SelectedValuePath = ValueMember;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void CreatLogDetails(
          string ErrorDescrription,
          string methodName,
          string ModuleName,
          string CreatedBy)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Log\\" + ModuleName + "-" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt", true);
                streamWriter.WriteLine(ErrorDescrription + " , " + methodName + " , " + ModuleName + " , " + CreatedBy + " , " + DateTime.Now.ToString());
                streamWriter.Dispose();
                streamWriter.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void CreatDataBaseLogDetails(
          string DBServerName,
          string DBSarverID,
          string DBServerPassword,
          string DataBaseName)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "DataBaseSetting.txt");
                streamWriter.WriteLine("DBServerName,DBSarverID,DBServerPassword,DataBaseName");
                streamWriter.WriteLine(DBServerName + "," + DBSarverID + "," + DBServerPassword + "," + DataBaseName);
                streamWriter.Dispose();
                streamWriter.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ReadFile(string Path)
        {
            try
            {
                string str = "";
                if (File.Exists(Path))
                {
                    StreamReader streamReader = new StreamReader(Path);
                    str = streamReader.ReadToEnd();
                    streamReader.Dispose();
                    streamReader.Close();
                }
                return str;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Encrypt_data(string str)
        {
            try
            {
                char[] charArray = str.ToCharArray();
                Array.Reverse((Array)charArray);
                str = new string(charArray);
                return Convert.ToBase64String(Encoding.Unicode.GetBytes(str));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Decrypt_data(string str)
        {
            try
            {
                char[] charArray = Encoding.Unicode.GetString(Convert.FromBase64String(str)).ToCharArray();
                Array.Reverse((Array)charArray);
                return new string(charArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable ReadExcelData(string fileName, string SheetName)
        {
            try
            {
                string empty = string.Empty;
                DataTable dataTable = new DataTable();
                using (OleDbConnection selectConnection = new OleDbConnection(!fileName.EndsWith(".xls") ? "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1';" : "provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"))
                    new OleDbDataAdapter("select * from " + SheetName, selectConnection).Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
