using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Windows.Forms;
using TechnoPro.Common.TextFormat.Adapters;

namespace ImportExportClassLibrary
{
	// Token: 0x02000024 RID: 36
	public class Core
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x000065CC File Offset: 0x000055CC
		public static byte[] ConvertRtfToPdf(string rtfCode)
		{
			if (rtfCode == null)
			{
				rtfCode = "";
			}
			return rtfCode.ConvertRtfToPdf();
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x000065E0 File Offset: 0x000055E0
		public static string StartDirectory
		{
			get
			{
				string text = Path.GetDirectoryName(Application.ExecutablePath);
				string path = Path.Combine(text, "ClockWork Database Scheduler.exe");
				if (!File.Exists(path))
				{
					string[] directories = Directory.GetDirectories(text);
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = 0; i < directories.Length - 1; i++)
					{
						stringBuilder.Append(directories[i]);
						stringBuilder.Append(Path.PathSeparator);
					}
					text = Path.Combine(text, "ClockWork DBS 3");
					path = Path.Combine(text, "ClockWork Database Scheduler.exe");
					if (!File.Exists(path))
					{
						text = "c:\\program files\\technopro computer solutions";
						path = Path.Combine(text, "ClockWork Database Scheduler.exe");
						if (!File.Exists(path))
						{
							text = "C:\\Program Files (x86)\\TechnoPro Computer Solutions";
							path = Path.Combine(text, "ClockWork Database Scheduler.exe");
						}
					}
				}
				return text;
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00006694 File Offset: 0x00005694
		public static string GetAccessConnectionString(string filename)
		{
			if (IntPtr.Size == 4)
			{
				return string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};User ID=Admin;Password=", filename);
			}
			return string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};User ID=Admin;Password=", filename);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000066B5 File Offset: 0x000056B5
		public static string GetExcelConnectionString(string filename)
		{
			if (IntPtr.Size == 4)
			{
				return string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;", filename);
			}
			return string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0;", filename);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000066D8 File Offset: 0x000056D8
		public static DataTable ImportFromExcel(string fileName, string sql, Dictionary<string, object> parameters)
		{
			string excelConnectionString = Core.GetExcelConnectionString(fileName);
			DataTable result;
			using (OleDbConnection oleDbConnection = new OleDbConnection(excelConnectionString))
			{
				using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("", oleDbConnection))
				{
					oleDbDataAdapter.SelectCommand.CommandText = sql;
					oleDbDataAdapter.SelectCommand.Parameters.Clear();
					foreach (KeyValuePair<string, object> keyValuePair in parameters)
					{
						oleDbDataAdapter.SelectCommand.Parameters.Add(keyValuePair.Key, keyValuePair.Value);
					}
					DataTable dataTable = new DataTable();
					oleDbDataAdapter.Fill(dataTable);
					result = dataTable;
				}
			}
			return result;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000067BC File Offset: 0x000057BC
		public static void ExportToExcel(DataView dv, string startDirectory, bool askUserToChooseColumns)
		{
			DataTable table = dv.Table;
			string tempFilename = TemplatesClass.GetTempFilename(".xls");
			TemplatesClass.ExportToExcel(dv, tempFilename, startDirectory, askUserToChooseColumns);
			if (!File.Exists(tempFilename))
			{
				return;
			}
			try
			{
				TemplatesClass.OpenExcel(tempFilename);
			}
			catch
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.FileName = Path.Combine(Environment.CurrentDirectory, Path.GetFileName(tempFilename));
				DialogResult dialogResult = saveFileDialog.ShowDialog();
				if (dialogResult == DialogResult.OK)
				{
					try
					{
						File.Copy(tempFilename, saveFileDialog.FileName);
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.ToString());
					}
				}
			}
		}
	}
}
