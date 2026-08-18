using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace AutoComboBox
{
	// Token: 0x020000B7 RID: 183
	public class ExportClass
	{
		// Token: 0x060006DA RID: 1754 RVA: 0x00036E5F File Offset: 0x00035E5F
		public static void ExportToExcel(DataTable t, string tempFilename, string startDirectory, bool askUserToFilterColumns)
		{
			ExportClass.ExportToExcel(new DataView(t), tempFilename, startDirectory, askUserToFilterColumns);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00036E74 File Offset: 0x00035E74
		private static string GetExcelConnectionString(string filename)
		{
			string result;
			if (IntPtr.Size == 4)
			{
				result = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;", filename);
			}
			else
			{
				result = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0;", filename);
			}
			return result;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00036EB0 File Offset: 0x00035EB0
		public static void ExportToExcel(DataView dv, string tempFilename, string startDirectory, bool askUserToFilterColumns)
		{
			ExportClass.AllowUserToChooseColumns(ref dv, askUserToFilterColumns, false);
			DataTable table = dv.Table;
			Type type = Type.GetType("System.Int32");
			Type type2 = Type.GetType("System.Boolean");
			Type type3 = Type.GetType("System.DateTime");
			File.Copy(Path.Combine(startDirectory, "BlankExcel.xls"), tempFilename, true);
			OleDbConnection oleDbConnection = new OleDbConnection(ExportClass.GetExcelConnectionString(tempFilename));
			OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("", oleDbConnection);
			string text = "[sheet1$]";
			int num = 0;
			string text2 = "";
			string text3 = "";
			string[] array = new string[table.Columns.Count];
			oleDbDataAdapter.SelectCommand.CommandText = "CREATE TABLE " + text + " (";
			oleDbDataAdapter.SelectCommand.Parameters.Clear();
			for (int i = 0; i < table.Columns.Count; i++)
			{
				DataColumn dataColumn = table.Columns[i];
				if (dataColumn.ColumnMapping != MappingType.Hidden)
				{
					string text4 = dataColumn.ColumnName.Replace("/", "");
					text4 = text4.Replace(".", "");
					text4 = text4.Replace(",", "");
					if (text4.Length < 1)
					{
						text4 = "col" + i.ToString();
					}
					int num2 = 65;
					if (text4.Length >= num2)
					{
						text4 = text4.Substring(0, num2) + i.ToString();
					}
					string text5 = "@col" + i.ToString();
					if (num++ > 0)
					{
						OleDbCommand selectCommand = oleDbDataAdapter.SelectCommand;
						selectCommand.CommandText += ",";
						text2 += ",";
						text3 += ",";
					}
					text4 = "[" + text4 + "]";
					OleDbCommand selectCommand2 = oleDbDataAdapter.SelectCommand;
					selectCommand2.CommandText += text4;
					text2 += text5;
					text3 += text4;
					array[i] = text5;
					Type dataType = table.Columns[i].DataType;
					if (dataType == type)
					{
						OleDbCommand selectCommand3 = oleDbDataAdapter.SelectCommand;
						selectCommand3.CommandText += " NUMBER";
					}
					else if (dataType == type2)
					{
						OleDbCommand selectCommand4 = oleDbDataAdapter.SelectCommand;
						selectCommand4.CommandText += " BIT";
					}
					else if (dataType == type3)
					{
						OleDbCommand selectCommand5 = oleDbDataAdapter.SelectCommand;
						selectCommand5.CommandText += " DATETIME";
					}
					else
					{
						OleDbCommand selectCommand6 = oleDbDataAdapter.SelectCommand;
						selectCommand6.CommandText += " TEXT";
					}
				}
			}
			OleDbCommand selectCommand7 = oleDbDataAdapter.SelectCommand;
			selectCommand7.CommandText += ")";
			try
			{
				oleDbDataAdapter.Fill(new DataTable());
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				MessageBox.Show(message);
			}
			int num3 = 0;
			oleDbConnection.Open();
			ArrayList arrayList = new ArrayList();
			foreach (object obj in dv)
			{
				DataRowView dataRowView = (DataRowView)obj;
				num3++;
				DataRow row = dataRowView.Row;
				oleDbDataAdapter.SelectCommand.CommandText = string.Concat(new string[]
				{
					"INSERT INTO ",
					text,
					" (",
					text3,
					") VALUES (",
					text2,
					")"
				});
				oleDbDataAdapter.SelectCommand.Parameters.Clear();
				for (int i = 0; i < table.Columns.Count; i++)
				{
					DataColumn dataColumn = table.Columns[i];
					if (dataColumn.ColumnMapping != MappingType.Hidden)
					{
						object obj2 = row[i];
						if (obj2 is DateTime)
						{
							DateTime dateTime = (DateTime)obj2;
							obj2 = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
						}
						oleDbDataAdapter.SelectCommand.Parameters.Add(array[i], obj2);
					}
				}
				try
				{
					oleDbDataAdapter.SelectCommand.ExecuteNonQuery();
				}
				catch (Exception ex)
				{
					string message2 = ex.Message;
					if (!arrayList.Contains(message2))
					{
						arrayList.Add(message2);
					}
				}
			}
			if (arrayList.Count > 0)
			{
				string text6 = "";
				foreach (object obj3 in arrayList)
				{
					string str = (string)obj3;
					if (text6.Length > 0)
					{
						text6 += Environment.NewLine;
					}
					text6 += str;
				}
				MessageBox.Show("Some rows were not added due to errors: " + Environment.NewLine + text6, "There were errors.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			oleDbConnection.Close();
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x000374EC File Offset: 0x000364EC
		public static void ExportToAccess(string tableName, DataTable t, string tempFilename, string startDirectory, bool askUserToFilterColumns)
		{
			bool askUserToFilterColumns2 = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
			ExportClass.ExportToAccess(tableName, new DataView(t), tempFilename, startDirectory, askUserToFilterColumns2);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00037520 File Offset: 0x00036520
		public static string GetAccessConnectionString(string filename)
		{
			string result;
			if (IntPtr.Size == 4)
			{
				result = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Persist Security Info=False", filename);
			}
			else
			{
				result = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Persist Security Info=False", filename);
			}
			return result;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0003755C File Offset: 0x0003655C
		public static void ExportToAccess(string tableName, DataView dv, string tempFilename, string startDirectory, bool askUserToFilterColumns)
		{
			string text = ExportClass.AllowUserToChooseColumns(ref dv, askUserToFilterColumns, true);
			if (text != null && text.Length > 0)
			{
				tableName = text;
			}
			else
			{
				tableName = tableName.Trim();
			}
			if (tableName.Length <= 0)
			{
				tableName = "table1";
			}
			DataTable table = dv.Table;
			Type type = Type.GetType("System.DateTime");
			Type type2 = Type.GetType("System.Double");
			Type type3 = Type.GetType("System.Int32");
			Type type4 = Type.GetType("System.Boolean");
			File.Copy(Path.Combine(startDirectory, "BlankAccess.mdb"), tempFilename, true);
			OleDbConnection oleDbConnection = new OleDbConnection(ExportClass.GetAccessConnectionString(tempFilename));
			OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("", oleDbConnection);
			int num = 0;
			string text2 = "";
			string text3 = "";
			string[] array = new string[table.Columns.Count];
			oleDbDataAdapter.SelectCommand.CommandText = "CREATE TABLE " + tableName + " (";
			for (int i = 0; i < table.Columns.Count; i++)
			{
				DataColumn dataColumn = table.Columns[i];
				if (dataColumn.ColumnMapping != MappingType.Hidden)
				{
					string text4 = dataColumn.ColumnName.Replace("/", "");
					text4 = text4.Replace("_", "");
					text4 = text4.Replace(".", "");
					if (text4.Length < 1)
					{
						text4 = "Unknown" + i.ToString();
					}
					text4 = "[" + text4 + "]";
					string text5 = "@col" + i.ToString();
					if (num++ > 0)
					{
						OleDbCommand selectCommand = oleDbDataAdapter.SelectCommand;
						selectCommand.CommandText += ",";
						text2 += ",";
						text3 += ",";
					}
					OleDbCommand selectCommand2 = oleDbDataAdapter.SelectCommand;
					selectCommand2.CommandText += text4;
					text2 += text5;
					text3 += text4;
					array[i] = text5;
					if (table.Columns[i].DataType == type3)
					{
						OleDbCommand selectCommand3 = oleDbDataAdapter.SelectCommand;
						selectCommand3.CommandText += " NUMBER";
					}
					else if (table.Columns[i].DataType == type)
					{
						OleDbCommand selectCommand4 = oleDbDataAdapter.SelectCommand;
						selectCommand4.CommandText += " DATETIME";
					}
					else if (table.Columns[i].DataType == type2)
					{
						OleDbCommand selectCommand5 = oleDbDataAdapter.SelectCommand;
						selectCommand5.CommandText += " DOUBLE";
					}
					else if (table.Columns[i].DataType == type4)
					{
						OleDbCommand selectCommand6 = oleDbDataAdapter.SelectCommand;
						selectCommand6.CommandText += " BIT";
					}
					else
					{
						OleDbCommand selectCommand7 = oleDbDataAdapter.SelectCommand;
						selectCommand7.CommandText += " TEXT";
					}
				}
			}
			OleDbCommand selectCommand8 = oleDbDataAdapter.SelectCommand;
			selectCommand8.CommandText += ")";
			try
			{
				oleDbDataAdapter.Fill(new DataTable());
			}
			catch (Exception ex)
			{
				string message = ex.Message;
			}
			int num2 = 0;
			oleDbConnection.Open();
			foreach (object obj in dv)
			{
				DataRowView dataRowView = (DataRowView)obj;
				num2++;
				DataRow row = dataRowView.Row;
				oleDbDataAdapter.SelectCommand.CommandText = string.Concat(new string[]
				{
					"INSERT INTO ",
					tableName,
					" (",
					text3,
					") VALUES (",
					text2,
					")"
				});
				oleDbDataAdapter.SelectCommand.Parameters.Clear();
				for (int i = 0; i < table.Columns.Count; i++)
				{
					DataColumn dataColumn = table.Columns[i];
					if (dataColumn.ColumnMapping != MappingType.Hidden)
					{
						oleDbDataAdapter.SelectCommand.Parameters.Add(array[i], row[i]);
					}
				}
				try
				{
					oleDbDataAdapter.SelectCommand.ExecuteNonQuery();
				}
				catch (Exception ex)
				{
				}
			}
			oleDbConnection.Close();
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00037AA4 File Offset: 0x00036AA4
		public static void ExportToDelimeteredText(DataTable t, string tempFilename, string startDirectory, bool askUserToFilterColumns, string colDelimiter, string rowDelimiter)
		{
			ExportClass.ExportToDelimeteredText(new DataView(t), tempFilename, startDirectory, askUserToFilterColumns, colDelimiter, rowDelimiter);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00037ABA File Offset: 0x00036ABA
		public static void ExportToDelimeteredText(DataView dv, string tempFilename, string startDirectory, bool askUserToFilterColumns)
		{
			ExportClass.ExportToDelimeteredText(dv, tempFilename, startDirectory, askUserToFilterColumns, ",", Environment.NewLine);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00037AD4 File Offset: 0x00036AD4
		public static void ExportToDelimeteredText(DataView dv, string tempFilename, string startDirectory, bool askUserToFilterColumns, string colDelimiter, string rowDelimiter)
		{
			ExportClass.AllowUserToChooseColumns(ref dv, askUserToFilterColumns, false);
			DataTable table = dv.Table;
			Type type = Type.GetType("System.Int32");
			Type type2 = Type.GetType("System.DateTime");
			Type type3 = Type.GetType("System.Double");
			StreamWriter streamWriter = new StreamWriter(tempFilename);
			bool flag = true;
			for (int i = 0; i < table.Columns.Count; i++)
			{
				DataColumn dataColumn = table.Columns[i];
				if (dataColumn.ColumnMapping != MappingType.Hidden)
				{
					string text = dataColumn.ColumnName;
					text = text.Replace(colDelimiter, " ");
					text = text.Replace(rowDelimiter, "");
					if (!flag)
					{
						streamWriter.Write(colDelimiter);
					}
					else
					{
						flag = false;
					}
					streamWriter.Write(text);
				}
			}
			streamWriter.Write(rowDelimiter);
			foreach (object obj in dv)
			{
				DataRowView dataRowView = (DataRowView)obj;
				DataRow row = dataRowView.Row;
				flag = true;
				for (int i = 0; i < table.Columns.Count; i++)
				{
					DataColumn dataColumn = table.Columns[i];
					if (dataColumn.ColumnMapping != MappingType.Hidden)
					{
						if (!flag)
						{
							streamWriter.Write(colDelimiter);
						}
						else
						{
							flag = false;
						}
						string text2;
						if (table.Columns[i].DataType == type)
						{
							text2 = row[i].ToString();
						}
						else if (table.Columns[i].DataType == type2)
						{
							if (row[i] == DBNull.Value)
							{
								text2 = "";
							}
							else
							{
								DateTime dateTime = (DateTime)row[i];
								text2 = dateTime.ToShortDateString();
								if (dateTime.Hour != 0 || dateTime.Minute != 0)
								{
									text2 = text2 + " " + dateTime.ToLongTimeString();
								}
							}
						}
						else if (table.Columns[i].DataType == type3)
						{
							text2 = row[i].ToString();
						}
						else
						{
							text2 = row[i].ToString();
						}
						text2 = text2.Replace(colDelimiter, " ").Replace(rowDelimiter, "");
						streamWriter.Write(text2);
					}
				}
				streamWriter.Write(rowDelimiter);
			}
			streamWriter.Close();
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00037DD0 File Offset: 0x00036DD0
		public static void ExportToFormattedText(DataTable t, string tempFilename, bool askUserToFilterColumns)
		{
			ExportClass.ExportToFormattedText(t, tempFilename, askUserToFilterColumns, true);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00037DE0 File Offset: 0x00036DE0
		public static void ExportToFormattedText(DataTable t, string tempFilename, bool askUserToFilterColumns, bool showNotepad)
		{
			DataView dv = new DataView(t);
			ExportClass.ExportToFormattedText(dv, tempFilename, askUserToFilterColumns, showNotepad);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00037DFF File Offset: 0x00036DFF
		public static void ExportToFormattedText(DataView dv, string tempFilename, bool askUserToFilterColumns)
		{
			ExportClass.ExportToFormattedText(dv, tempFilename, askUserToFilterColumns, true);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00037E0C File Offset: 0x00036E0C
		public static void ExportToFormattedText(DataView dv, string tempFilename, bool askUserToFilterColumns, bool showNotepad)
		{
			DataTable table = dv.Table;
			ExportClass.AllowUserToChooseColumns(ref dv, askUserToFilterColumns, false);
			StreamWriter streamWriter = new StreamWriter(tempFilename);
			int[] array = new int[table.Columns.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = table.Columns[i].ColumnName.Length + 1;
			}
			foreach (object obj in dv)
			{
				DataRowView dataRowView = (DataRowView)obj;
				DataRow row = dataRowView.Row;
				for (int i = 0; i < table.Columns.Count; i++)
				{
					DataColumn dataColumn = table.Columns[i];
					if (dataColumn.ColumnMapping != MappingType.Hidden)
					{
						int num = row[i].ToString().Trim().Length + 1;
						if (num > array[i])
						{
							array[i] = num;
						}
					}
				}
			}
			for (int i = 0; i < table.Columns.Count; i++)
			{
				DataColumn dataColumn = table.Columns[i];
				if (dataColumn.ColumnMapping != MappingType.Hidden)
				{
					string text = dataColumn.ColumnName;
					int num2 = array[i] - text.Length;
					if (num2 > 0)
					{
						text += new string(' ', num2);
					}
					streamWriter.Write(text);
				}
			}
			streamWriter.WriteLine();
			for (int i = 0; i < table.Columns.Count; i++)
			{
				DataColumn dataColumn = table.Columns[i];
				if (dataColumn.ColumnMapping != MappingType.Hidden)
				{
					string text = "";
					int num2 = array[i] - text.Length - 1;
					if (num2 > 0)
					{
						text += new string('=', num2);
					}
					text += "+";
					streamWriter.Write(text);
				}
			}
			streamWriter.WriteLine();
			foreach (object obj2 in dv)
			{
				DataRowView dataRowView = (DataRowView)obj2;
				DataRow row = dataRowView.Row;
				for (int i = 0; i < table.Columns.Count; i++)
				{
					DataColumn dataColumn = table.Columns[i];
					if (dataColumn.ColumnMapping != MappingType.Hidden)
					{
						string text2 = row[i].ToString();
						int num2 = array[i] - text2.Length;
						if (num2 > 0)
						{
							text2 += new string(' ', num2);
						}
						streamWriter.Write(text2);
					}
				}
				streamWriter.WriteLine();
			}
			streamWriter.Close();
			if (showNotepad && File.Exists(tempFilename))
			{
				Process.Start(tempFilename, "");
			}
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0003817C File Offset: 0x0003717C
		private static string AllowUserToChooseColumns(ref DataView dv, bool askUserToFilterColumns, bool showTableName)
		{
			string result;
			if (askUserToFilterColumns)
			{
				FilterColumns filterColumns = new FilterColumns(dv, showTableName);
				filterColumns.ShowDialog();
				foreach (object obj in filterColumns.listView1.Items)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					if (!listViewItem.Checked)
					{
						DataColumn dataColumn = (DataColumn)listViewItem.Tag;
						dataColumn.ColumnMapping = MappingType.Hidden;
					}
				}
				result = filterColumns.txt_tableName.Text.Trim();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00038244 File Offset: 0x00037244
		public static string GetTempFilename(string fnExtension)
		{
			string tempFileName = Path.GetTempFileName();
			string extension = Path.GetExtension(tempFileName);
			return tempFileName.Replace(extension, fnExtension);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00038270 File Offset: 0x00037270
		public static string GetStartDirectory()
		{
			object registryValue = ExportClass.GetRegistryValue(Registry.LocalMachine, ExportClass.registryBreakdown, "InstallPath");
			string text;
			if (registryValue == null)
			{
				text = "";
			}
			else
			{
				text = registryValue.ToString().Trim();
			}
			if (text.Length < 1 || !Directory.Exists(text))
			{
				text = Directory.GetCurrentDirectory();
			}
			return text;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x000382D8 File Offset: 0x000372D8
		public static object GetRegistryValue(RegistryKey StartKey, string[] RegKeyBreakdown, string valueName)
		{
			RegistryKey registryKey = ExportClass.GetRegistryKey(StartKey, RegKeyBreakdown, false, false);
			return ExportClass.GetRegistryValue(registryKey, valueName);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x000382FC File Offset: 0x000372FC
		public static RegistryKey GetRegistryKey(RegistryKey StartKey, string[] RegKeyBreakdown, bool CreateKeyIfNotPresent, bool openWritable)
		{
			RegistryKey registryKey;
			for (;;)
			{
				registryKey = StartKey;
				int i = 0;
				while (i < RegKeyBreakdown.Length)
				{
					string text = RegKeyBreakdown[i];
					RegistryKey registryKey2 = registryKey.OpenSubKey(text, openWritable);
					if (registryKey2 != null)
					{
						registryKey = registryKey2;
						i++;
					}
					else
					{
						if (CreateKeyIfNotPresent)
						{
							registryKey2 = registryKey.CreateSubKey(text);
							registryKey = null;
							break;
						}
						goto IL_44;
					}
				}
				if (registryKey != null)
				{
					goto Block_3;
				}
			}
			IL_44:
			return null;
			Block_3:
			return registryKey;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00038380 File Offset: 0x00037380
		public static object GetRegistryValue(RegistryKey regKey, string valueName)
		{
			if (regKey != null)
			{
				try
				{
					return regKey.GetValue(valueName);
				}
				catch (Exception result)
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x04000564 RID: 1380
		public static string[] registryBreakdown = new string[]
		{
			"Software",
			"TechnoPro",
			"ClockWork"
		};
	}
}
