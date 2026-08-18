using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using AutoComboBox;
using ClockWorkAPI;
using ClockWorkDataAccessClassLibrary;
using ClockWorkLogger;
using Databases;
using DynamicScreens;
using EmailClassLibrary;
using EncryptionClassLibrary;
using ImportExportClassLibraryMailMerge;
using Microsoft.CSharp;
using Microsoft.Win32;
using RemoteLoader;
using ReportFunctions.ClockWorkDataSync;
using ReportFunctions.ClockWorkDataSync.Courses;
using ReportFunctions.ClockWorkDataSync.ServiceProviders.ServiceProviderCourses;
using ReportFunctions.ClockWorkDataSync.ServiceProviders.ServiceProviderData;
using SettingsPermissions;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core;
using TechnoPro.Common.Core.DataSync;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.DAO.Impl.DataSync;
using TechnoPro.Common.DataFileIO.cs;
using TechnoPro.Common.DataFileIO.cs.Excel;
using TechnoPro.Common.ICore;
using TechnoPro.Common.ICore.DataSync;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.TPMailMan;
using TechnoPro.Common.UI.ClientManager.OldUserSettings;
using TechnoPro.Common.UI.ClientManager.WinForms.Core.DynamicForms;
using TechnoPro.Common.UI.ClientManager.WinForms.Core.Impl.DynamicForms;
using TechnoPro.Common.Win32;
using UnivOleDb;

namespace ReportFunctions
{
	// Token: 0x02000012 RID: 18
	public class ReportFunction
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00005F64 File Offset: 0x00004F64
		public static string GetStartDirectory()
		{
			return Application.ExecutablePath;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005F88 File Offset: 0x00004F88
		public static void CallIncrementProgressBar(IncrementProgressBar incrementProgressBar, int amount)
		{
			if (incrementProgressBar != null)
			{
				incrementProgressBar(amount);
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00005FA8 File Offset: 0x00004FA8
		public static void CallIncrementProgressBar(IncrementProgressBar incrementProgressBar)
		{
			if (incrementProgressBar != null)
			{
				incrementProgressBar(1);
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00005FC8 File Offset: 0x00004FC8
		public static void CallSetupProgressBar(SetupProgressBar setupProgressBar, int min, int max)
		{
			if (setupProgressBar != null)
			{
				setupProgressBar(min, max);
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00005FE8 File Offset: 0x00004FE8
		public static void CallSetupProgressBar2(SetupProgressBar2 setupProgressBar, int min, int max, string title)
		{
			if (setupProgressBar != null)
			{
				setupProgressBar(min, max, title);
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00006008 File Offset: 0x00005008
		public static void FakeIncrementProgressBar(int amount)
		{
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000600B File Offset: 0x0000500B
		public static void FakeSetupProgressBar(int min, int max)
		{
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000600E File Offset: 0x0000500E
		public static void FakeSetupProgressBar2(int min, int max, string title)
		{
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00006014 File Offset: 0x00005014
		public static DataTable LoadCustomTable(UnivDataAdapter da, TechnoProReports technoProReports)
		{
			DataTable dataTable = new DataTable();
			if (technoProReports != null)
			{
				dataTable = technoProReports.LoadCustomTableFromDataSet();
			}
			DataTable dataTable2 = new DataTable();
			da.SelectCommand.CommandText = "SELECT searchcustomid,searchcustomcode,searchcustomdescription,retrievelistsql,multiselect FROM searchcustom";
			da.Fill(dataTable2);
			foreach (object obj in dataTable2.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string strB = dataRow[1].ToString().Trim().ToLower();
				bool flag = false;
				foreach (object obj2 in dataTable.Rows)
				{
					DataRow dataRow2 = (DataRow)obj2;
					string text = ((string)dataRow[1]).Trim().ToLower();
					if (text.CompareTo(strB) == 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					dataTable.ImportRow(dataRow);
				}
			}
			return dataTable;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00006178 File Offset: 0x00005178
		public static string GetWebModulesBaseUrl(UnivDataAdapter da)
		{
			SettingWithValue settingWithValue = Settings.LoadEveryoneSetting(da, 99627);
			string result;
			if (settingWithValue != null && settingWithValue.ValStr != null)
			{
				string text = settingWithValue.ValStr.Trim();
				if (text.Length > 0 && text[text.Length - 1] != '/')
				{
					result = text + "/";
				}
				else
				{
					result = text;
				}
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000061F0 File Offset: 0x000051F0
		public static DataTable LoadStaffNames(UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			DataTable dataTable = new DataTable();
			da.SelectCommand.CommandText = "SELECT personid, firstname, lastname FROM people WHERE isactive=1 AND personid IN (SELECT personid FROM peoplegroups WHERE groupid=2)";
			da.Fill(dataTable);
			DataTable dataTable2 = new DataTable();
			Type type = Type.GetType("System.Int32");
			dataTable2.Columns.Add("personid", type);
			dataTable2.Columns.Add("lastfirstname");
			dataTable2.Columns.Add("firstname");
			dataTable2.Columns.Add("lastname");
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				object[] array = new object[4];
				array[0] = dataRow[0];
				byte[] inputInBytes = (byte[])dataRow[1];
				byte[] inputInBytes2 = (byte[])dataRow[2];
				string text = tripleDES.Decrypt(inputInBytes);
				string text2 = tripleDES.Decrypt(inputInBytes2);
				array[1] = text + " " + text2;
				array[2] = text;
				array[3] = text2;
				dataTable2.Rows.Add(array);
			}
			dataTable.Dispose();
			return dataTable2;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00006350 File Offset: 0x00005350
		public static RegistryKey GetRegistryKey(RegistryKey StartKey, string[] RegKeyBreakdown, bool CreateKeyIfNotPresent, bool openWritable)
		{
			RegistryKey registryKey = StartKey;
			foreach (string text in RegKeyBreakdown)
			{
				RegistryKey registryKey2 = registryKey.OpenSubKey(text, openWritable);
				if (registryKey2 != null)
				{
					registryKey = registryKey2;
				}
				else
				{
					if (!CreateKeyIfNotPresent)
					{
						return null;
					}
					registryKey2 = registryKey.CreateSubKey(text);
				}
			}
			return registryKey;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000063BC File Offset: 0x000053BC
		public static object GetRegistryValue(RegistryKey regKey, string valueName, bool isEncrypted)
		{
			if (regKey != null)
			{
				try
				{
					object value = regKey.GetValue(valueName);
					if (value != null && isEncrypted)
					{
						string text = (string)value;
						if (text.Length > 0)
						{
							return DPAPIencryption.UnProtectData(text, DPAPIencryption.GetEntropy());
						}
					}
					return value;
				}
				catch (Exception result)
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000643C File Offset: 0x0000543C
		public static object GetRegistryValue(RegistryKey StartKey, string[] RegKeyBreakdown, string valueName, bool isEncrypted)
		{
			RegistryKey registryKey = ReportFunction.GetRegistryKey(StartKey, RegKeyBreakdown, false, false);
			return ReportFunction.GetRegistryValue(registryKey, valueName, isEncrypted);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00006460 File Offset: 0x00005460
		public static DataView DynamicDataToHumanOutput(DataView dvFromViews, string[] columnsToRetain, TripleDESEncryptionClass tripleDES, UnivDataAdapter da = null)
		{
			DataTable table = dvFromViews.Table;
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("personid", typeof(int));
			dataTable.Columns.Add("firstname");
			dataTable.Columns.Add("middlename");
			dataTable.Columns.Add("lastname");
			dataTable.Columns.Add("student_no");
			if (columnsToRetain != null)
			{
				foreach (string text in columnsToRetain)
				{
					if (table.Columns.Contains(text))
					{
						DataColumn dataColumn = table.Columns[text];
						dataTable.Columns.Add(text, dataColumn.DataType);
					}
				}
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			List<string> list = new List<string>();
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string text2 = dataRow["controlcaption"].ToString();
				if (dictionary.ContainsKey(text2))
				{
					string text3 = dictionary[text2];
				}
				else
				{
					int num = text2.IndexOf("~~");
					string text3 = (num > 0) ? text2.Substring(0, num) : text2;
					text3 = ReportFunction.OnlyKeepLettersAndDigits(text3);
					text3 = ReportFunction.EnsureUniqueColumn(dataTable, text3);
					dictionary.Add(text2, text3);
					int num2 = (dataRow["controlcode"] == DBNull.Value) ? 0 : ((int)dataRow["controlcode"]);
					if (num2 == 700 || num2 == 2)
					{
						dataTable.Columns.Add(text3, typeof(bool));
						list.Add(text2);
					}
					else
					{
						dataTable.Columns.Add(text3);
					}
				}
			}
			DataView dataView = new DataView();
			table.TableName = "table1";
			dataView.Table = table;
			dataView.Sort = "personid";
			int j = 0;
			Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
			while (j < dataView.Count)
			{
				DataRow row = dataView[j].Row;
				int num3 = (int)row["personid"];
				int k;
				for (k = j + 1; k < dataView.Count; k++)
				{
					DataRow dataRow = dataView[k].Row;
					int num4 = (int)dataRow["personid"];
					if (num4 != num3)
					{
						break;
					}
				}
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["personid"] = num3;
				if (table.Columns.Contains("firstname"))
				{
					dataRow2["firstname"] = row["firstname"];
				}
				if (table.Columns.Contains("middlename"))
				{
					dataRow2["middlename"] = row["middlename"];
				}
				if (table.Columns.Contains("lastname"))
				{
					dataRow2["lastname"] = row["lastname"];
				}
				if (table.Columns.Contains("student_no"))
				{
					dataRow2["student_no"] = row["student_no"];
				}
				if (columnsToRetain != null)
				{
					foreach (string text in columnsToRetain)
					{
						dataRow2[text] = row[text];
					}
				}
				for (int l = j; l < k; l++)
				{
					DataRow row2 = dataView[l].Row;
					string text2 = row2["controlcaption"].ToString();
					string text3 = dictionary[text2];
					byte[] array = (row2["valbytes"] == DBNull.Value) ? new byte[0] : ((byte[])row2["valbytes"]);
					byte[] array2 = (row2["valimage"] == DBNull.Value) ? new byte[0] : ((byte[])row2["valimage"]);
					bool flag = row2["valbytesisencrypted"] != DBNull.Value && Convert.ToBoolean(row2["valbytesisencrypted"]);
					object value;
					if (array2.Length > 0)
					{
						value = tripleDES.Decrypt(array2);
					}
					else if (flag && array.Length > 0)
					{
						value = tripleDES.Decrypt(array);
					}
					else
					{
						string text4 = row2["valtext"].ToString();
						if (text4.Length < 1 && array != null && array.Length > 0)
						{
							value = Encoding.UTF8.GetString(array);
						}
						else
						{
							value = text4;
						}
					}
					bool flag2;
					if (row2["valint"] != DBNull.Value)
					{
						int num5 = (int)row2["valint"];
						flag2 = (num5 != 0);
					}
					else
					{
						flag2 = false;
					}
					if (list.Contains(text2))
					{
						dataRow2[text3] = flag2;
					}
					else
					{
						dataRow2[text3] = value;
					}
				}
				dataTable.Rows.Add(dataRow2);
				j = k;
			}
			if (dictionary2.Count > 0)
			{
				try
				{
					DataTable dataTable2 = ReportFunction.ExpandListViewData(da, dataTable, dictionary2);
					return dataTable2.DefaultView;
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Debug("ReportFunction.DynamicDataToHumanOutput:Error={0}.", ex.ToString());
				}
			}
			return dataTable.DefaultView;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00006AE8 File Offset: 0x00005AE8
		public static DataTable DynamicDataToHumanOutput2(DataTable t, string[] columnsToRetain, TripleDESEncryptionClass tripleDES)
		{
			return ReportFunction.DynamicDataToHumanOutput2(t, columnsToRetain, tripleDES, "lucourseid", null);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00006B08 File Offset: 0x00005B08
		public static DataTable DynamicDataToHumanOutput2(DataTable t, string[] columnsToRetain, TripleDESEncryptionClass tripleDES, UnivDataAdapter da = null)
		{
			return ReportFunction.DynamicDataToHumanOutput2(t, columnsToRetain, tripleDES, "lucourseid", da);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00006B28 File Offset: 0x00005B28
		public static DataTable DynamicDataToHumanOutput2(DataTable t, string[] columnsToRetain, TripleDESEncryptionClass tripleDES, string secondaryIntColumnName)
		{
			return ReportFunction.DynamicDataToHumanOutput2(t, columnsToRetain, tripleDES, secondaryIntColumnName, null);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00006B44 File Offset: 0x00005B44
		public static DataTable DynamicDataToHumanOutput2(DataTable t, string[] columnsToRetain, TripleDESEncryptionClass tripleDES, string secondaryIntColumnName, UnivDataAdapter da = null)
		{
			bool flag = !string.IsNullOrEmpty(secondaryIntColumnName);
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("personid", typeof(int));
			if (flag)
			{
				dataTable.Columns.Add(secondaryIntColumnName, typeof(int));
			}
			dataTable.Columns.Add("firstname");
			dataTable.Columns.Add("middlename");
			dataTable.Columns.Add("lastname");
			dataTable.Columns.Add("student_no");
			if (secondaryIntColumnName.Equals("lucourseid"))
			{
				dataTable.Columns.Add("course");
			}
			if (columnsToRetain != null)
			{
				foreach (string text in columnsToRetain)
				{
					if (t.Columns.Contains(text))
					{
						DataColumn dataColumn = t.Columns[text];
						dataTable.Columns.Add(text, dataColumn.DataType);
					}
				}
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			List<string> list = new List<string>();
			foreach (object obj in t.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string text2 = dataRow["controlcaption"].ToString();
				if (dictionary.ContainsKey(text2))
				{
					string text3 = dictionary[text2];
				}
				else
				{
					int num = text2.IndexOf("~~");
					string text3 = (num > 0) ? text2.Substring(0, num) : text2;
					text3 = ReportFunction.OnlyKeepLettersAndDigits(text3);
					text3 = ReportFunction.EnsureUniqueColumn(dataTable, text3);
					dictionary.Add(text2, text3);
					int num2 = (dataRow["controlcode"] == DBNull.Value) ? 0 : ((int)dataRow["controlcode"]);
					if (num2 == 700 || num2 == 2)
					{
						dataTable.Columns.Add(text3, typeof(bool));
						list.Add(text2);
					}
					else
					{
						dataTable.Columns.Add(text3);
					}
				}
			}
			DataView dataView = new DataView();
			t.TableName = "table1";
			dataView.Table = t;
			if (flag)
			{
				dataView.Sort = string.Format("personid,{0}", secondaryIntColumnName);
			}
			else
			{
				dataView.Sort = "personid";
			}
			RichTextBox richTextBox = null;
			int j = 0;
			Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
			while (j < dataView.Count)
			{
				DataRow row = dataView[j].Row;
				int num3 = (row["personid"] == DBNull.Value) ? 0 : ((int)row["personid"]);
				int num4;
				if (flag)
				{
					num4 = ((row[secondaryIntColumnName] == DBNull.Value) ? 0 : ((int)row[secondaryIntColumnName]));
				}
				else
				{
					num4 = 0;
				}
				int k;
				for (k = j + 1; k < dataView.Count; k++)
				{
					DataRow dataRow = dataView[k].Row;
					int num5 = (dataRow["personid"] == DBNull.Value) ? 0 : ((int)dataRow["personid"]);
					int num6;
					if (flag)
					{
						num6 = ((dataRow[secondaryIntColumnName] == DBNull.Value) ? 0 : ((int)dataRow[secondaryIntColumnName]));
					}
					else
					{
						num6 = 0;
					}
					if (num5 != num3 || num6 != num4)
					{
						break;
					}
				}
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["personid"] = num3;
				if (flag)
				{
					dataRow2[secondaryIntColumnName] = num4;
				}
				if (t.Columns.Contains("firstname"))
				{
					dataRow2["firstname"] = row["firstname"];
				}
				if (t.Columns.Contains("middlename"))
				{
					dataRow2["middlename"] = row["middlename"];
				}
				if (t.Columns.Contains("lastname"))
				{
					dataRow2["lastname"] = row["lastname"];
				}
				if (t.Columns.Contains("student_no"))
				{
					dataRow2["student_no"] = row["student_no"];
				}
				if (t.Columns.Contains("subject") && t.Columns.Contains("course") && t.Columns.Contains("section"))
				{
					dataRow2["course"] = string.Format("{0}{1} {2} {3}", new object[]
					{
						t.Columns.Contains("term") ? (row["term"].ToString() + " ") : "",
						row["subject"].ToString(),
						row["course"].ToString(),
						row["section"].ToString()
					});
				}
				if (columnsToRetain != null)
				{
					foreach (string text in columnsToRetain)
					{
						dataRow2[text] = row[text];
					}
				}
				for (int l = j; l < k; l++)
				{
					DataRow row2 = dataView[l].Row;
					string text2 = row2["controlcaption"].ToString();
					string text3 = dictionary[text2];
					int num2 = (row2["controlcode"] == DBNull.Value) ? 0 : ((int)row2["controlcode"]);
					if (num2 == 10 && !dictionary2.ContainsKey(text3))
					{
						dictionary2.Add(text3, (int)row2["setting1"]);
					}
					byte[] array = (row2["valbytes"] == DBNull.Value) ? new byte[0] : ((byte[])row2["valbytes"]);
					byte[] array2 = (row2["valimage"] == DBNull.Value) ? new byte[0] : ((byte[])row2["valimage"]);
					bool flag2 = row2["valbytesisencrypted"] != DBNull.Value && Convert.ToBoolean(row2["valbytesisencrypted"]);
					object obj2;
					if (array2.Length > 0)
					{
						obj2 = tripleDES.Decrypt(array2);
					}
					else if (flag2 && array.Length > 0)
					{
						obj2 = tripleDES.Decrypt(array);
					}
					else
					{
						string text4 = row2["valtext"].ToString();
						if (text4.Length < 1 && array != null && array.Length > 0)
						{
							obj2 = Encoding.UTF8.GetString(array);
						}
						else
						{
							obj2 = text4;
						}
					}
					bool flag3;
					if (list.Contains(text2) || obj2 == null || (obj2 is string && string.IsNullOrEmpty((string)obj2) && row2["valint"] != DBNull.Value))
					{
						int num7 = (int)row2["valint"];
						flag3 = (num7 != 0);
					}
					else
					{
						flag3 = false;
					}
					if (list.Contains(text2))
					{
						dataRow2[text3] = flag3;
					}
					else
					{
						if (num2 == 600)
						{
							if (richTextBox == null)
							{
								richTextBox = new RichTextBox();
							}
							try
							{
								richTextBox.Rtf = obj2.ToString();
								obj2 = richTextBox.Text;
							}
							catch
							{
							}
						}
						dataRow2[text3] = obj2;
					}
				}
				dataTable.Rows.Add(dataRow2);
				j = k;
			}
			if (dictionary2.Count > 0)
			{
				try
				{
					return ReportFunction.ExpandListViewData(da, dataTable, dictionary2);
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Debug("ReportFunction.DynamicDataToHumanOutput2:Error={0}.", ex.ToString());
				}
			}
			return dataTable;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000074A4 File Offset: 0x000064A4
		private static DataTable ExpandListViewData(UnivDataAdapter da, DataTable t2, Dictionary<string, int> _listViewColNames)
		{
			string commandText = "SELECT ll.lookuptext\r\nFROM        lookuplists ll LEFT JOIN lookupgroups lg ON lg.lookupgroupid=ll.lookupgroupid\r\nWHERE       ll.lookupgroupid=@lookupgroupid AND ll.visible=1\r\nORDER BY    ll.ordernum,ll.lookuptext";
			List<ReportFunction.ListViewControlColumnGroup> list = new List<ReportFunction.ListViewControlColumnGroup>();
			foreach (KeyValuePair<string, int> keyValuePair in _listViewColNames)
			{
				ReportFunction.ListViewControlColumnGroup listViewControlColumnGroup = new ReportFunction.ListViewControlColumnGroup
				{
					IsActive = false,
					ColName = keyValuePair.Key,
					InternalColNames = new List<string>(),
					StartColIndex = -1
				};
				da.SelectCommand.CommandText = commandText;
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@lookupgroupid", keyValuePair.Value);
				DataTable dataTable = new DataTable();
				string text;
				da.Fill(dataTable, out text);
				if (!string.IsNullOrEmpty(text))
				{
					MessageBox.Show(text);
				}
				if (dataTable.Rows.Count > 0)
				{
					DataRow dataRow = dataTable.NewRow();
					dataRow["lookuptext"] = "Date";
					dataTable.Rows.Add(dataRow);
					foreach (object obj in dataTable.Rows)
					{
						DataRow dataRow2 = (DataRow)obj;
						string text2 = dataRow2["lookuptext"].ToString().Trim();
						int num = text2.IndexOf("~~");
						if (num > 0)
						{
							text2 = text2.Substring(0, num);
						}
						text2 = text2.Replace(" ", "_");
						listViewControlColumnGroup.InternalColNames.Add(text2);
						listViewControlColumnGroup.IsActive = true;
					}
				}
				list.Add(listViewControlColumnGroup);
			}
			foreach (ReportFunction.ListViewControlColumnGroup listViewControlColumnGroup2 in list)
			{
				ReportFunction.ListViewControlColumnGroup listViewControlColumnGroup2;
				if (listViewControlColumnGroup2.IsActive)
				{
					List<string> internalColNames = listViewControlColumnGroup2.InternalColNames;
					for (int i = 0; i < internalColNames.Count; i++)
					{
						string text3 = internalColNames[i];
						if (t2.Columns.Contains(text3))
						{
							text3 += "2";
						}
						if (!t2.Columns.Contains(text3))
						{
							if (listViewControlColumnGroup2.StartColIndex < 0)
							{
								listViewControlColumnGroup2.StartColIndex = t2.Columns.Count;
							}
							t2.Columns.Add(text3);
						}
					}
				}
			}
			DataTable dataTable2 = t2.Clone();
			dataTable2.TableName = "t3";
			foreach (object obj2 in t2.Rows)
			{
				DataRow dataRow2 = (DataRow)obj2;
				int num2 = 0;
				Dictionary<string, List<string[]>> dictionary = new Dictionary<string, List<string[]>>();
				foreach (ReportFunction.ListViewControlColumnGroup listViewControlColumnGroup2 in list)
				{
					ReportFunction.ListViewControlColumnGroup listViewControlColumnGroup2;
					string colName = listViewControlColumnGroup2.ColName;
					List<string[]> list2 = new List<string[]>();
					dictionary.Add(colName, list2);
					string text4 = dataRow2[colName].ToString().Trim();
					if (text4.Length > 0)
					{
						string[] array = text4.Split(new char[]
						{
							'\t'
						}, StringSplitOptions.RemoveEmptyEntries);
						foreach (string text5 in array)
						{
							string text6 = text5.Trim();
							if (text6.Length > 0)
							{
								string text7 = text6;
								char[] separator = new char[1];
								string[] item = text7.Split(separator, StringSplitOptions.RemoveEmptyEntries);
								list2.Add(item);
							}
						}
					}
					if (list2.Count > num2)
					{
						num2 = list2.Count;
					}
				}
				if (num2 > 0)
				{
					DataRow[] array3 = new DataRow[num2];
					for (int k = 0; k < num2; k++)
					{
						array3[k] = dataTable2.NewRow();
						for (int l = 0; l < dataTable2.Columns.Count; l++)
						{
							array3[k][l] = dataRow2[l];
						}
					}
					for (int k = 0; k < list.Count; k++)
					{
						ReportFunction.ListViewControlColumnGroup listViewControlColumnGroup2 = list[k];
						if (listViewControlColumnGroup2.IsActive)
						{
							string text3 = listViewControlColumnGroup2.ColName;
							List<string[]> list3 = dictionary[text3];
							for (int m = 0; m < list3.Count; m++)
							{
								string[] array4 = list3[m];
								DataRow dataRow3 = array3[m];
								int startColIndex = listViewControlColumnGroup2.StartColIndex;
								for (int n = 0; n < array4.Length; n++)
								{
									string value = array4[n];
									int num3 = startColIndex + n;
									if (num3 < t2.Columns.Count)
									{
										dataRow3[startColIndex + n] = value;
									}
								}
							}
						}
					}
					for (int k = 0; k < array3.Length; k++)
					{
						dataTable2.Rows.Add(array3[k]);
					}
				}
				else
				{
					dataTable2.ImportRow(dataRow2);
				}
			}
			for (int k = 0; k < list.Count; k++)
			{
				string text3 = list[k].ColName;
				if (dataTable2.Columns.Contains(text3))
				{
					dataTable2.Columns.Remove(text3);
				}
			}
			return dataTable2;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00007B8C File Offset: 0x00006B8C
		public static string EnsureUniqueColumn(DataTable t, string colName)
		{
			string text = colName;
			for (int i = 1; i < 10000; i++)
			{
				if (!t.Columns.Contains(text))
				{
					return text;
				}
				text = string.Format("{0}{1}", colName, i.ToString());
			}
			return text + "__x";
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00007BE8 File Offset: 0x00006BE8
		public static DataTable RemovePersonIds(DataTable tableWithTooManyPersonIds, DataTable tableWithPersonIdsIWant)
		{
			DataTable dataTable = tableWithTooManyPersonIds.Clone();
			List<string> list = new List<string>();
			foreach (object obj in tableWithTooManyPersonIds.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (tableWithPersonIdsIWant.Columns.Contains(dataColumn.ColumnName))
				{
					list.Add(dataColumn.ColumnName);
				}
			}
			foreach (object obj2 in tableWithPersonIdsIWant.Rows)
			{
				DataRow dataRow = (DataRow)obj2;
				DataRow[] array = tableWithTooManyPersonIds.Select(string.Format("personid={0}", ((dataRow["personid"] == DBNull.Value) ? 0 : ((int)dataRow["personid"])).ToString()));
				if (array.Length > 0)
				{
					foreach (DataRow row in array)
					{
						dataTable.ImportRow(row);
					}
				}
				else
				{
					DataRow dataRow2 = dataTable.NewRow();
					foreach (string columnName in list)
					{
						dataRow2[columnName] = dataRow[columnName];
					}
					dataTable.Rows.Add(dataRow2);
				}
			}
			return dataTable;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00007DFC File Offset: 0x00006DFC
		public static string OnlyKeepLettersAndDigits(string s)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in s)
			{
				if (c == ' ')
				{
					stringBuilder.Append("_");
				}
				else if (char.IsLetterOrDigit(c))
				{
					stringBuilder.Append(c);
				}
			}
			return (stringBuilder.Length > 0) ? stringBuilder.ToString() : "c";
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00007E88 File Offset: 0x00006E88
		public static TechnoProReports SetupTechnoProReports(string version)
		{
			string startupPath = Application.StartupPath;
			return new TechnoProReports();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00007EA8 File Offset: 0x00006EA8
		public static Report RunReport(int reportID, NameValueCollection parameters, out ArrayList errors, IncrementProgressBar IncrementMainProgressBar, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar2 SetupMainProgressBar, SetupProgressBar SetupSubProgressBar, DB[] dbs, bool getUserInputForVariableValues)
		{
			return ReportFunction.RunReport(reportID, parameters, out errors, IncrementMainProgressBar, IncrementSubProgressBar, SetupMainProgressBar, SetupSubProgressBar, dbs, getUserInputForVariableValues, false);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00007ED0 File Offset: 0x00006ED0
		public static Report RunReport(int reportID, NameValueCollection parameters, out ArrayList errors, IncrementProgressBar IncrementMainProgressBar, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar2 SetupMainProgressBar, SetupProgressBar SetupSubProgressBar, DB[] dbs, bool getUserInputForVariableValues, bool suppressGuiMessages)
		{
			return ReportFunction.RunReport(reportID, parameters, out errors, IncrementMainProgressBar, IncrementSubProgressBar, SetupMainProgressBar, SetupSubProgressBar, dbs, getUserInputForVariableValues, "");
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00007EFC File Offset: 0x00006EFC
		public static Report RunReport(int reportID, NameValueCollection parameters, out ArrayList errors, IncrementProgressBar IncrementMainProgressBar, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar2 SetupMainProgressBar, SetupProgressBar SetupSubProgressBar, DB[] dbs, bool getUserInputForVariableValues, string reportVersion)
		{
			return ReportFunction.RunReport(reportID, parameters, out errors, IncrementMainProgressBar, IncrementSubProgressBar, SetupMainProgressBar, SetupSubProgressBar, dbs, getUserInputForVariableValues, reportVersion, false);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00007F24 File Offset: 0x00006F24
		public static void ShowDataView(DataView dv)
		{
			DataTableView dataTableView = new DataTableView(dv, "");
			dataTableView.ShowDialog();
			dataTableView.Dispose();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00007F4C File Offset: 0x00006F4C
		public static Report RunReport(string dbName, DataRow reportDR, UnivDataAdapter da, DataSet comboBoxData, DataSet lookupTablesForControls, ArrayList variables, DataTable sessions, object[] yearStartEnd, DataTable dynamicScreenNonDataControlsTable, DataTable searchCustomTable, TripleDESEncryptionClass tripleDES, IncrementProgressBar IncrementMainProgressBar, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar2 SetupMainProgressBar, SetupProgressBar SetupSubProgressBar, DataTable staffNamesTable, int whoAmIPersonID, TechnoProReports technoProReports, out ArrayList errors)
		{
			return ReportFunction.RunReport(dbName, reportDR, da, comboBoxData, lookupTablesForControls, variables, sessions, yearStartEnd, dynamicScreenNonDataControlsTable, searchCustomTable, tripleDES, IncrementMainProgressBar, IncrementSubProgressBar, SetupMainProgressBar, SetupSubProgressBar, staffNamesTable, whoAmIPersonID, technoProReports, out errors, null);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00007F88 File Offset: 0x00006F88
		public static Report RunReport(string dbName, DataRow reportDR, UnivDataAdapter da, DataSet comboBoxData, DataSet lookupTablesForControls, ArrayList variables, DataTable sessions, object[] yearStartEnd, DataTable dynamicScreenNonDataControlsTable, DataTable searchCustomTable, TripleDESEncryptionClass tripleDES, IncrementProgressBar IncrementMainProgressBar, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar2 SetupMainProgressBar, SetupProgressBar SetupSubProgressBar, DataTable staffNamesTable, int whoAmIPersonID, TechnoProReports technoProReports, out ArrayList errors, EventHandler reportStartedHandler)
		{
			ArrayList arrayList;
			Report result = ReportFunction.RunReport(dbName, reportDR, da, comboBoxData, lookupTablesForControls, variables, sessions, yearStartEnd, dynamicScreenNonDataControlsTable, searchCustomTable, tripleDES, IncrementMainProgressBar, IncrementSubProgressBar, SetupMainProgressBar, SetupSubProgressBar, staffNamesTable, whoAmIPersonID, technoProReports, out arrayList, true);
			errors = arrayList;
			return result;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00007FC8 File Offset: 0x00006FC8
		public static Report RunReport(string dbName, DataRow reportDR, UnivDataAdapter da, DataSet comboBoxData, DataSet lookupTablesForControls, ArrayList variables, DataTable sessions, object[] yearStartEnd, DataTable dynamicScreenNonDataControlsTable, DataTable searchCustomTable, TripleDESEncryptionClass tripleDES, IncrementProgressBar IncrementMainProgressBar, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar2 SetupMainProgressBar, SetupProgressBar SetupSubProgressBar, DataTable staffNamesTable, int whoAmIPersonID, TechnoProReports technoProReports, out ArrayList errors, bool getUserInputForVariableValues, EventHandler reportStartedHandler, ReportParameterCollection reportParameterCollection)
		{
			return ReportFunction.RunReport(dbName, reportDR, da, comboBoxData, lookupTablesForControls, variables, sessions, yearStartEnd, dynamicScreenNonDataControlsTable, searchCustomTable, tripleDES, IncrementMainProgressBar, IncrementSubProgressBar, SetupMainProgressBar, SetupSubProgressBar, staffNamesTable, whoAmIPersonID, technoProReports, out errors, getUserInputForVariableValues, reportStartedHandler, reportParameterCollection, false);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00008008 File Offset: 0x00007008
		public static Report RunReport(string dbName, DataRow reportDR, UnivDataAdapter da, DataSet comboBoxData, DataSet lookupTablesForControls, ArrayList variables, DataTable sessions, object[] yearStartEnd, DataTable dynamicScreenNonDataControlsTable, DataTable searchCustomTable, TripleDESEncryptionClass tripleDES, IncrementProgressBar IncrementMainProgressBar, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar2 SetupMainProgressBar, SetupProgressBar SetupSubProgressBar, DataTable staffNamesTable, int whoAmIPersonID, TechnoProReports technoProReports, out ArrayList errors, bool getUserInputForVariableValues, EventHandler reportStartedHandler, ReportParameterCollection reportParameterCollection, bool suppressGuiMessages)
		{
			return ReportFunction.RunReport(dbName, reportDR, da, comboBoxData, lookupTablesForControls, variables, sessions, yearStartEnd, dynamicScreenNonDataControlsTable, searchCustomTable, tripleDES, IncrementMainProgressBar, IncrementSubProgressBar, SetupMainProgressBar, SetupSubProgressBar, staffNamesTable, whoAmIPersonID, technoProReports, out errors, getUserInputForVariableValues, reportStartedHandler, reportParameterCollection, suppressGuiMessages, null);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000804C File Offset: 0x0000704C
		public static Report RunReport(bool clearWindowsRequestedFiles, string dbName, DataRow reportDR, UnivDataAdapter da, DataSet comboBoxData, DataSet lookupTablesForControls, ArrayList variables, DataTable sessions, object[] yearStartEnd, DataTable dynamicScreenNonDataControlsTable, DataTable searchCustomTable, TripleDESEncryptionClass tripleDES, IncrementProgressBar IncrementMainProgressBar, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar2 SetupMainProgressBar, SetupProgressBar SetupSubProgressBar, DataTable staffNamesTable, int whoAmIPersonID, TechnoProReports technoProReports, out ArrayList errors, bool getUserInputForVariableValues, EventHandler reportStartedHandler, ReportParameterCollection reportParameterCollection, bool suppressGuiMessages, DataTable overrideFunctionsTable)
		{
			return ReportFunction.RunReport(null, clearWindowsRequestedFiles, dbName, reportDR, da, comboBoxData, lookupTablesForControls, variables, sessions, yearStartEnd, dynamicScreenNonDataControlsTable, searchCustomTable, tripleDES, IncrementMainProgressBar, IncrementSubProgressBar, SetupMainProgressBar, SetupSubProgressBar, staffNamesTable, whoAmIPersonID, technoProReports, out errors, getUserInputForVariableValues, reportStartedHandler, reportParameterCollection, suppressGuiMessages, overrideFunctionsTable);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00008094 File Offset: 0x00007094
		public static Report RunReport(Report startingReport, bool clearWindowsRequestedFiles, string dbName, DataRow reportDR, UnivDataAdapter da, DataSet comboBoxData, DataSet lookupTablesForControls, ArrayList variables, DataTable sessions, object[] yearStartEnd, DataTable dynamicScreenNonDataControlsTable, DataTable searchCustomTable, TripleDESEncryptionClass tripleDES, IncrementProgressBar IncrementMainProgressBar, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar2 SetupMainProgressBar, SetupProgressBar SetupSubProgressBar, DataTable staffNamesTable, int whoAmIPersonID, TechnoProReports technoProReports, out ArrayList errors, bool getUserInputForVariableValues, EventHandler reportStartedHandler, ReportParameterCollection reportParameterCollection, bool suppressGuiMessages, DataTable overrideFunctionsTable)
		{
			return ReportFunction.RunReport(startingReport, clearWindowsRequestedFiles, dbName, reportDR, da, comboBoxData, lookupTablesForControls, variables, sessions, yearStartEnd, dynamicScreenNonDataControlsTable, searchCustomTable, tripleDES, IncrementMainProgressBar, IncrementSubProgressBar, SetupMainProgressBar, SetupSubProgressBar, staffNamesTable, whoAmIPersonID, technoProReports, out errors, getUserInputForVariableValues, reportStartedHandler, reportParameterCollection, suppressGuiMessages, overrideFunctionsTable, null);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000080DC File Offset: 0x000070DC
		public static Report RunReport(Report startingReport, bool clearWindowsRequestedFiles, string dbName, DataRow reportDR, UnivDataAdapter da, DataSet comboBoxData, DataSet lookupTablesForControls, ArrayList variables, DataTable sessions, object[] yearStartEnd, DataTable dynamicScreenNonDataControlsTable, DataTable searchCustomTable, TripleDESEncryptionClass tripleDES, IncrementProgressBar IncrementMainProgressBar, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar2 SetupMainProgressBar, SetupProgressBar SetupSubProgressBar, DataTable staffNamesTable, int whoAmIPersonID, TechnoProReports technoProReports, out ArrayList errors, bool getUserInputForVariableValues, EventHandler reportStartedHandler, ReportParameterCollection reportParameterCollection, bool suppressGuiMessages, DataTable overrideFunctionsTable, List<int> onlyRunTheseSearchFunctionIds)
		{
			if (IncrementMainProgressBar == null)
			{
				IncrementMainProgressBar = new IncrementProgressBar(ReportFunction.FakeIncrementProgressBar);
			}
			if (SetupMainProgressBar == null)
			{
				SetupMainProgressBar = new SetupProgressBar2(ReportFunction.FakeSetupProgressBar2);
			}
			if (IncrementSubProgressBar == null)
			{
				IncrementSubProgressBar = new IncrementProgressBar(ReportFunction.FakeIncrementProgressBar);
			}
			if (SetupSubProgressBar == null)
			{
				SetupSubProgressBar = new SetupProgressBar(ReportFunction.FakeSetupProgressBar);
			}
			errors = new ArrayList();
			if (clearWindowsRequestedFiles)
			{
				ReportFunction.WindowsRequestedFiles = new List<string>();
			}
			bool flag = (Control.ModifierKeys & Keys.Alt) == Keys.Alt;
			bool flag2 = (Control.ModifierKeys & Keys.Control) == Keys.Control;
			flag = (flag && flag2);
			if (technoProReports == null)
			{
				technoProReports = ReportFunction.SetupTechnoProReports("2.0");
			}
			int searchInfoID = (int)reportDR[0];
			int num;
			try
			{
				if (reportDR.Table.Columns.Count < 14)
				{
					num = 1;
				}
				else
				{
					num = ((reportDR != null && reportDR[13] != DBNull.Value) ? ((int)reportDR[13]) : 1);
				}
			}
			catch (Exception ex)
			{
				if (!suppressGuiMessages)
				{
					ReportFunction.MessageBoxShow(ex.ToString());
				}
				num = 0;
				if (!suppressGuiMessages)
				{
					errors.Add(ex.ToString());
				}
			}
			Report report = (startingReport == null) ? new Report(reportDR) : startingReport;
			DataTable dataTable;
			if (overrideFunctionsTable != null)
			{
				dataTable = overrideFunctionsTable;
			}
			else if (num == 0)
			{
				dataTable = ReportFunction.GetFunctionsStatic(searchInfoID, technoProReports);
			}
			else
			{
				dataTable = ReportFunction.GetFunctionsCustom(searchInfoID, da);
			}
			foreach (object obj in dataTable.Rows)
			{
				DataRow dr = (DataRow)obj;
				ReportStep reportStep = new ReportStep(dr);
				if (onlyRunTheseSearchFunctionIds == null || onlyRunTheseSearchFunctionIds.Contains(reportStep.Id))
				{
					report.Add(reportStep);
				}
			}
			Report result;
			if (dataTable.Rows.Count > 0)
			{
				DataTable variablesStatic = ReportFunction.GetVariablesStatic(searchInfoID, technoProReports, report.OverrideDynamicControlsScreenNum, da, dataTable);
				if (!da.SelectCommand.Parameters.Contains("@false"))
				{
					da.SelectCommand.Parameters.Add("@false", false);
				}
				if (!da.SelectCommand.Parameters.Contains("@true"))
				{
					da.SelectCommand.Parameters.Add("@true", true);
				}
				if (!da.SelectCommand.Parameters.Contains("@whoamipersonid"))
				{
					da.SelectCommand.Parameters.Add("@whoamipersonid", whoAmIPersonID);
				}
				string pattern = "(?<=#<)custom\\S+(?=>#)";
				Regex regex = new Regex(pattern);
				ArrayList arrayList = new ArrayList();
				foreach (object obj2 in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj2;
					string input = dataRow[3].ToString().Trim();
					MatchCollection matchCollection = regex.Matches(input);
					foreach (object obj3 in matchCollection)
					{
						Match match = (Match)obj3;
						if (match.Success)
						{
							Variable variable = new Variable(match.Value.ToLower().Trim(), "", dataRow);
							bool flag3 = false;
							foreach (object obj4 in arrayList)
							{
								Variable variable2 = (Variable)obj4;
								if (variable2.VariableName.CompareTo(variable.VariableName) == 0)
								{
									flag3 = true;
									break;
								}
							}
							if (!flag3)
							{
								arrayList.Add(variable);
							}
						}
					}
					int num2 = (int)dataRow[2];
					if (num2 == 40)
					{
						string text = dataRow[3].ToString();
						ReportFunction.SetVariablesExplicitly(text.Split(new char[]
						{
							'`'
						}), 0, ref variables, da, tripleDES, da.SelectCommand.Parameters);
						report.RememberVariables(variables);
					}
				}
				if (variablesStatic.Rows.Count > 0 || arrayList.Count > 0)
				{
					if (getUserInputForVariableValues && !ReportFunction.GetVariablesFromUser(searchInfoID, variablesStatic, da, comboBoxData, lookupTablesForControls, ref variables, sessions, yearStartEnd, reportDR[1].ToString().Trim(), dynamicScreenNonDataControlsTable, searchCustomTable, arrayList, report.OverrideDynamicControlsScreenNum, tripleDES, technoProReports, num, reportDR))
					{
						return null;
					}
				}
				foreach (object obj5 in variables)
				{
					Variable variable3 = (Variable)obj5;
					int num3 = variable3.VariableName.IndexOf("encrypt");
					if (num3 > 0)
					{
						if (variable3.VariableName.IndexOf("encryptdatasync") > 0)
						{
							string encryptionType = ReportFunction.UseUpdatedCreateTripleDES ? "tripledes_192bit" : "";
							TripleDESEncryptionClass tripleDESEncryptionClass = ReportFunction.CreateTripleDES(da, encryptionType, "#<407>#", tripleDES);
							variable3.VariableValue = tripleDESEncryptionClass.Encrypt((variable3.VariableValue is string) ? ((string)variable3.VariableValue) : variable3.VariableValue.ToString());
						}
						else
						{
							variable3.VariableValue = tripleDES.Encrypt((variable3.VariableValue is string) ? ((string)variable3.VariableValue) : variable3.VariableValue.ToString());
						}
					}
				}
				ReportFunction.SetVariables(da, ref variables);
				ReportFunction.SetVariablesMissing(ref da, reportParameterCollection);
				report.SetRememberedVariables2(variables);
				report.SetVariables(da);
				ReportFunction.CallSetupProgressBar2(SetupMainProgressBar, 0, dataTable.Rows.Count, "Initiating report execution...");
				report.Start();
				foreach (object obj6 in report)
				{
					ReportStep reportStep2 = (ReportStep)obj6;
					if (SetupMainProgressBar != null)
					{
						ReportFunction.CallSetupProgressBar2(SetupMainProgressBar, -1, -1, "EXECUTING '" + reportStep2.FunctionName + "'...");
					}
					ReportFunction.CallIncrementProgressBar(IncrementMainProgressBar);
					if (SetupSubProgressBar != null)
					{
						SetupSubProgressBar(0, 10);
					}
					DialogResult dialogResult;
					if (flag)
					{
						dialogResult = ((!suppressGuiMessages) ? MessageBox.Show("Would you like to run this function (" + reportStep2.FunctionName + ")? (click Cancel to stop executing the rest of the functions)", "ALT Special Key - Step through functions", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) : DialogResult.Cancel);
					}
					else
					{
						dialogResult = DialogResult.Yes;
					}
					if (dialogResult == DialogResult.Yes)
					{
						ReportFunction.RunFunction(dbName, reportStep2, ref report, da, arrayList, tripleDES, IncrementSubProgressBar, SetupSubProgressBar, ref comboBoxData, ref staffNamesTable, lookupTablesForControls, variables, sessions, yearStartEnd, dynamicScreenNonDataControlsTable, searchCustomTable, whoAmIPersonID, technoProReports, num, ref errors, getUserInputForVariableValues, suppressGuiMessages);
					}
					else if (dialogResult == DialogResult.Cancel)
					{
						break;
					}
				}
				report.End();
				if (SetupMainProgressBar != null)
				{
					SetupMainProgressBar(-1, -1, "Completed execution.");
				}
				string text2 = "";
				foreach (object obj7 in variablesStatic.Rows)
				{
					DataRow dataRow = (DataRow)obj7;
					int controlCode = (int)dataRow[2];
					if (ReportFunction.IsControlCodeDataHolding(dynamicScreenNonDataControlsTable, controlCode))
					{
						string varName = VariablesInput.GetVarName(dataRow);
						Variable variable4 = ReportFunction.GetVariable(variables, varName);
						if (variable4 != null)
						{
							if (text2.Length > 0)
							{
								text2 += ", ";
							}
							text2 = text2 + varName + "=";
							if (variable4.VariableValue is DateTime)
							{
								text2 += ((DateTime)variable4.VariableValue).ToString("MM/dd/yy");
							}
							else
							{
								text2 += variable4.VariableValue.ToString();
							}
						}
					}
				}
				if (clearWindowsRequestedFiles && ReportFunction.WindowsRequestedFiles != null && ReportFunction.WindowsRequestedFiles.Count > 0)
				{
					foreach (string text3 in ReportFunction.WindowsRequestedFiles)
					{
						if (!string.IsNullOrEmpty(text3) && File.Exists(text3))
						{
							try
							{
								File.Delete(text3);
							}
							catch
							{
							}
						}
					}
					ReportFunction.WindowsRequestedFiles.Clear();
				}
				result = report;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00008BF4 File Offset: 0x00007BF4
		public static bool IsControlCodeDataHolding(DataTable dynamicScreenNonDataControlsTable, int controlCode)
		{
			return DynamicScreen.IsControlCodeDataHolding(dynamicScreenNonDataControlsTable, controlCode);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00008C10 File Offset: 0x00007C10
		private static void MessageBoxShow(string msg)
		{
			try
			{
				CWLogger.Logger.Trace(msg);
			}
			catch
			{
				CWLogger.Logger.Warn(msg);
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00008C50 File Offset: 0x00007C50
		public static Report ReportBatchEmail(Form form, DataView dv, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, DataSet comboBoxData, DataTable staffNamesTable, Settings settings, out int goodEmailCount, out int badEmailCount, out int foundEmailCount, out int ignoredEmailCount)
		{
			string startupPath = Application.StartupPath;
			if (form != null)
			{
				form.Cursor = Cursors.WaitCursor;
			}
			Report report = null;
			goodEmailCount = 0;
			badEmailCount = 0;
			foundEmailCount = 0;
			ignoredEmailCount = 0;
			Report result;
			if (dv == null)
			{
				if (form != null)
				{
					form.Cursor = Cursors.Default;
				}
				result = null;
			}
			else
			{
				BatchEmailDialog batchEmailDialog = new BatchEmailDialog(da, settings, dv.Table);
				DialogResult dialogResult = batchEmailDialog.ShowDialog(form);
				if (dialogResult == DialogResult.OK)
				{
					int emailControlId = batchEmailDialog.EmailControlId;
					int emailSecondaryControlId = batchEmailDialog.EmailSecondaryControlId;
					int okToEmailControlId = batchEmailDialog.OkToEmailControlId;
					bool ignoreNotOkToEmail_emails = batchEmailDialog.IgnoreNotOkToEmail_emails;
					int emailColInd = batchEmailDialog.EmailColInd;
					bool lookupEmails = batchEmailDialog.LookupEmails;
					bool flag = lookupEmails ? (emailControlId > 0) : (emailColInd > 0);
					if (flag)
					{
						string text = "";
						string text2 = "";
						string text3 = "";
						DataView dataView;
						if (lookupEmails)
						{
							da.SelectCommand.CommandText = "SELECT dsc.screennum,dsc.controlid,dsc.ordernum,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue FROM dynamicscreencontrols dsc LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid WHERE dsc.controlid=@emailcontrolid OR dsc.controlid=@oktoemailcontrolid OR dsc.controlid=@emailsecondarycontrolid";
							da.SelectCommand.Parameters.Clear();
							da.SelectCommand.Parameters.Add("@emailcontrolid", emailControlId);
							da.SelectCommand.Parameters.Add("@oktoemailcontrolid", okToEmailControlId);
							da.SelectCommand.Parameters.Add("@emailsecondarycontrolid", emailSecondaryControlId);
							DataTable dataTable = new DataTable();
							da.Fill(dataTable);
							for (int i = 0; i < dataTable.Rows.Count; i++)
							{
								if ((int)dataTable.Rows[i][1] == emailControlId)
								{
									text = (string)dataTable.Rows[i][4];
								}
								else if ((int)dataTable.Rows[i][1] == okToEmailControlId)
								{
									text2 = (string)dataTable.Rows[i][4];
								}
								else if ((int)dataTable.Rows[i][1] == emailSecondaryControlId)
								{
									text3 = (string)dataTable.Rows[i][4];
								}
							}
							Report report2 = new Report(dv);
							Exception ex;
							ReportFunction.CrossReferenceWithPerStudentData(da.Clone(), tripleDES, comboBoxData, staffNamesTable, dataTable, ref report2, out ex);
							dataView = report2.GetCurrentDataView();
							if (ex != null)
							{
								ReportFunction.MessageBoxShow(ex.ToString());
								if (form != null)
								{
									form.Cursor = Cursors.Default;
								}
								return null;
							}
						}
						else
						{
							text = dv.Table.Columns[emailColInd].ColumnName;
							dataView = dv;
						}
						DataTable table = dataView.Table;
						ReportFunction.AddDataColumn(ref table, "Used_this_email", typeof(bool));
						int columnIndex = dataView.Table.Columns.Count - 1;
						ArrayList arrayList = new ArrayList();
						ArrayList arrayList2 = new ArrayList();
						ArrayList arrayList3 = new ArrayList();
						foreach (object obj in dataView)
						{
							DataRowView dataRowView = (DataRowView)obj;
							DataRow dataRow = dataRowView.Row;
							string text4 = (text.Length > 0) ? dataRow[text].ToString() : "";
							bool flag2 = ReportFunction.IsValidEmail(text4);
							if (!flag2 && text3.Length > 0)
							{
								text4 = dataRow[text3].ToString();
								if (text.Length > 0)
								{
									dataRow[text] = text4;
								}
								flag2 = ReportFunction.IsValidEmail(text4);
							}
							if (flag2)
							{
								if (ignoreNotOkToEmail_emails && text2.Length > 0)
								{
									flag2 = (dataRow[text2] != DBNull.Value && Convert.ToBoolean(dataRow[text2]));
								}
								if (flag2)
								{
									dataRow[columnIndex] = true;
									if (!arrayList3.Contains(dataRow))
									{
										arrayList3.Add(dataRow);
									}
								}
								else if (!arrayList2.Contains(dataRow))
								{
									arrayList2.Add(dataRow);
								}
							}
							else if (!arrayList.Contains(dataRow))
							{
								arrayList.Add(dataRow);
							}
						}
						string text5 = "";
						List<string> list = new List<string>();
						for (int i = 0; i < arrayList3.Count; i++)
						{
							if (i > 0)
							{
								text5 += ",";
							}
							DataRow dataRow = (DataRow)arrayList3[i];
							string text4 = dataRow[text].ToString().Trim();
							if (!list.Contains(text4))
							{
								list.Add(text4);
								text5 += text4;
							}
						}
						text5 = text5.Replace("\"", "'");
						string text6 = "bcc=\"" + text5 + "\"";
						string tempFileName = FileSystem.GetTempFileName(".txt");
						StreamWriter streamWriter = new StreamWriter(tempFileName);
						streamWriter.Write(text6);
						streamWriter.Close();
						text6 = "checkfile=\"" + tempFileName + "\"";
						try
						{
							string fileName = Path.Combine(startupPath, "tpemailer.exe");
							Process.Start(fileName, text6);
							int num = arrayList3.Count + arrayList2.Count + arrayList.Count;
							goodEmailCount = goodEmailCount;
							badEmailCount = arrayList.Count;
							foundEmailCount = num;
							ignoredEmailCount = arrayList2.Count;
							if (dv != dataView)
							{
								report = new Report(dataView);
							}
						}
						catch (Exception ex2)
						{
							ReportFunction.MessageBoxShow(ex2.ToString());
							InputBox inputBox = new InputBox("Something went wrong; listing of emails for manual copying and pasting", "Something went wrong, so here is the list of emails generated that you can copy out manually:", text6.Substring(5, text6.Length - 6), true);
							inputBox.Height *= 2;
							inputBox.ShowDialog(form);
						}
					}
					else
					{
						ReportFunction.MessageBoxShow("Nothing was done because you didn't select the email field (I don't know which field the emails are stored in!)");
					}
				}
				if (form != null)
				{
					form.Cursor = Cursors.Default;
				}
				result = report;
			}
			return result;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00009340 File Offset: 0x00008340
		private static bool IsValidEmail(string email)
		{
			return email.Trim().Length > 0;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000936C File Offset: 0x0000836C
		private static void SetVariablesMissing(ref UnivDataAdapter da, ReportParameterCollection reportParameterCollection)
		{
			if (reportParameterCollection != null)
			{
				foreach (object obj in reportParameterCollection)
				{
					ReportParameter reportParameter = (ReportParameter)obj;
					if (!da.SelectCommand.Parameters.Contains(reportParameter.ParamName))
					{
						da.SelectCommand.Parameters.Add(reportParameter.ParamName, reportParameter.ParamValue);
					}
				}
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00009410 File Offset: 0x00008410
		private static ArrayList SetVariablesExplicitly(string[] ps, int startPos, ref ArrayList variables, UnivDataAdapter da)
		{
			return ReportFunction.SetVariablesExplicitly(ps, startPos, ref variables, da, null, (da == null) ? null : da.SelectCommand.Parameters);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00009440 File Offset: 0x00008440
		private static ArrayList SetVariablesExplicitly(string[] ps, int startPos, ref ArrayList variables, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, UnivParameterCollection univParameters)
		{
			ArrayList arrayList = new ArrayList();
			int i = startPos;
			while (i < ps.Length)
			{
				string[] array = ps[i].Trim().Split(new char[]
				{
					'='
				});
				string text = array[0];
				string text2 = array[1];
				string[] array2 = text.Split(new char[]
				{
					'.'
				});
				string text3 = array2[0];
				string text4 = (array2.Length > 1) ? array2[1].Trim().ToLower() : "string";
				string text5;
				if (text4.IndexOf('$') > 0)
				{
					text5 = "";
					string data;
					if (text2.IndexOf('@') == 0)
					{
						object obj = univParameters.Value(text2.Substring(1));
						if (obj != null && obj is string)
						{
							data = tripleDES.Decrypt((string)obj);
						}
						else
						{
							data = text2;
						}
					}
					else
					{
						data = text2;
					}
					byte[] inputInBytes = ClockWorkCore.base64Decode(data);
					text2 = tripleDES.Decrypt(inputInBytes);
				}
				else if (text4.IndexOf('%') > 0)
				{
					int num = text4.IndexOf('%');
					text5 = text4.Substring(num + 1);
					text4 = text4.Substring(0, num + 1);
				}
				else
				{
					text5 = "";
				}
				object obj2 = null;
				string text6 = text4;
				if (text6 == null)
				{
					goto IL_395;
				}
				if (!(text6 == "encrypt%"))
				{
					if (!(text6 == "date"))
					{
						if (!(text6 == "bool"))
						{
							if (!(text6 == "int"))
							{
								if (!(text6 == "double"))
								{
									goto IL_395;
								}
								try
								{
									obj2 = double.Parse(text2);
								}
								catch (Exception ex)
								{
									arrayList.Add(ex.ToString());
								}
							}
							else
							{
								try
								{
									obj2 = int.Parse(text2);
								}
								catch (Exception ex)
								{
									arrayList.Add(ex.ToString());
								}
							}
						}
						else
						{
							text2 = text2.Trim().ToLower();
							obj2 = (text2 == "1" || text2 == "yes" || text2 == "t" || text2 == "true");
						}
					}
					else
					{
						try
						{
							obj2 = DateTime.Parse(text2);
						}
						catch (Exception ex)
						{
							arrayList.Add(ex.ToString());
						}
					}
				}
				else
				{
					int num = text5.IndexOf('.');
					EncryptionType encryptionType;
					if (num > 0)
					{
						encryptionType = BaseEncryptionClass.ParseEncryptionType(text5.Substring(0, num));
						try
						{
							int num2 = int.Parse(text5.Substring(num + 1));
						}
						catch
						{
						}
					}
					else
					{
						encryptionType = BaseEncryptionClass.ParseEncryptionType("");
						try
						{
							int num2 = int.Parse(text5);
						}
						catch
						{
						}
					}
					try
					{
						num = int.Parse(text5);
					}
					catch
					{
						num = -1;
					}
					da.SelectCommand.CommandText = "SELECT settingstringvalue FROM settingsgroups WHERE groupid=-1 AND settingcode=" + num.ToString();
					DataTable dataTable = new DataTable();
					da.Fill(dataTable);
					if (dataTable.Rows.Count > 0)
					{
						string data2 = dataTable.Rows[0][0].ToString();
						string password = tripleDES.Decrypt(ClockWorkCore.base64Decode(data2));
						TripleDESEncryptionClass tripleDESEncryptionClass = new TripleDESEncryptionClass(encryptionType, password);
						obj2 = tripleDESEncryptionClass.Encrypt(text2);
					}
					else
					{
						obj2 = null;
					}
				}
				IL_39B:
				Variable variable = new Variable(text3.ToLower().Trim(), obj2);
				bool flag = false;
				foreach (object obj3 in variables)
				{
					Variable variable2 = (Variable)obj3;
					if (variable2.VariableName.CompareTo(variable.VariableName) == 0)
					{
						flag = true;
						variable2.VariableValue = obj2;
						break;
					}
				}
				if (!flag)
				{
					variables.Add(variable);
				}
				if (univParameters != null)
				{
					string parameterName = "@" + text3;
					if (univParameters.Contains(parameterName))
					{
						univParameters.SetValue("@" + text3, obj2);
					}
					else
					{
						univParameters.Add(parameterName, obj2);
					}
				}
				i++;
				continue;
				IL_395:
				obj2 = text2;
				goto IL_39B;
			}
			return arrayList;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00009950 File Offset: 0x00008950
		public static Variable GetVariable(ArrayList variables, string varName)
		{
			string strB = varName.Trim().ToLower();
			foreach (object obj in variables)
			{
				Variable variable = (Variable)obj;
				string text = variable.VariableName.Trim().ToLower();
				if (text.CompareTo(strB) == 0)
				{
					return variable;
				}
			}
			return null;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000099F0 File Offset: 0x000089F0
		public static object GetVariableValue(ArrayList variables, string nameNoAtSign)
		{
			string strB = nameNoAtSign.ToLower().Trim();
			foreach (object obj in variables)
			{
				Variable variable = (Variable)obj;
				string text = variable.VariableName.ToLower().Trim();
				if (text.CompareTo(strB) == 0)
				{
					return variable.VariableValue;
				}
			}
			return null;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00009A94 File Offset: 0x00008A94
		private static void SetVariables(UnivDataAdapter da, ref ArrayList variables)
		{
			bool flag = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
			foreach (object obj in variables)
			{
				Variable variable = (Variable)obj;
				try
				{
					string text = "@" + variable.VariableName;
					if (!da.SelectCommand.Parameters.Contains(text))
					{
						da.SelectCommand.Parameters.Add(text, variable.VariableValue);
					}
					else
					{
						da.SelectCommand.Parameters.Clear(text);
						da.SelectCommand.Parameters.Add(text, variable.VariableValue);
					}
					if (flag)
					{
						ReportFunction.MessageBoxShow(string.Concat(new object[]
						{
							text,
							" = '",
							variable.VariableValue,
							"'"
						}));
					}
				}
				catch
				{
				}
			}
			try
			{
				if (!da.SelectCommand.Parameters.Contains("@true"))
				{
					da.SelectCommand.Parameters.Add("@true", true);
				}
				if (!da.SelectCommand.Parameters.Contains("@false"))
				{
					da.SelectCommand.Parameters.Add("@false", false);
				}
			}
			catch
			{
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00009C5C File Offset: 0x00008C5C
		private static void IncrementProgressBar(object pp)
		{
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00009C60 File Offset: 0x00008C60
		public static DataTable GetFunctionsStatic(int searchInfoID, TechnoProReports technoProReports)
		{
			DataTable dataTable = (technoProReports == null) ? new DataTable() : technoProReports.LoadFunctionsFromDataSet(searchInfoID);
			if (dataTable != null)
			{
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					string text = dataRow["functionparameters"].ToString().ToLower();
					if (text.IndexOf("#<custom") >= 0)
					{
						string text2;
						if (text.IndexOf("#<customapp") >= 0)
						{
							text2 = "app.apptypeid";
						}
						else if (text.IndexOf("#<customdynamiccontrol") >= 0)
						{
							text2 = "dsc.controlid";
						}
						else if (text.IndexOf("#<customgroup") >= 0)
						{
							text2 = "groupid";
						}
						else if (text.IndexOf("#<customscreen") >= 0 || text.IndexOf("#<customperstudentdis") >= 0)
						{
							text2 = "a1.controlid";
						}
						else if (text.IndexOf("customworkshop") >= 0)
						{
							text2 = "w.workshopid";
						}
						else
						{
							text2 = "app.apptypeid";
						}
						if (text2.Length > 0)
						{
							text2 += "=#<0>#";
							dataRow["customsqlinjection"] = text2;
						}
					}
				}
			}
			return dataTable;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00009E0C File Offset: 0x00008E0C
		public static DataTable GetFunctionsCustom(int searchInfoID, UnivDataAdapter da)
		{
			DataTable dataTable = new DataTable();
			da.SelectCommand.CommandText = "SELECT searchfunctionid,searchinfoid,functioncode,functionparameters,ordernum,custom,customsqlinjection,customsqlinjectionoperator,'?' AS functiondescription FROM searchfunctions WHERE searchinfoid=@searchinfoid ORDER BY ordernum";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@searchinfoid", searchInfoID);
			da.Fill(dataTable);
			return dataTable;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00009E6C File Offset: 0x00008E6C
		private static string Decompress(string compressedString)
		{
			return CompressionTP.Decompress(compressedString, true);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00009E88 File Offset: 0x00008E88
		public static DataTable GetVariablesStatic(int searchInfoID, TechnoProReports technoProReports, int overrideDynamicControlsScreenNum, UnivDataAdapter clockWorkDa, DataTable functionsTable)
		{
			DataTable dataTable = new DataTable();
			foreach (object obj in functionsTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow[2];
				if (num == 122)
				{
					string compressedString = dataRow[3].ToString();
					string s = ReportFunction.Decompress(compressedString);
					DataSet dataSet = new DataSet();
					StringReader stringReader = new StringReader(s);
					dataSet.ReadXml(stringReader, XmlReadMode.ReadSchema);
					stringReader.Close();
					if (dataSet.Tables.Count > 0)
					{
						dataTable = dataSet.Tables[0];
						return dataTable;
					}
				}
			}
			if (overrideDynamicControlsScreenNum != 0)
			{
				if (overrideDynamicControlsScreenNum < 0)
				{
					clockWorkDa.SelectCommand.CommandText = "SELECT dsc.controlid,dsc.screennum,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue,'' AS defaultstring,dc.controlgroup FROM dynamicscreencontrols dsc LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid WHERE dsc.screennum=@screennum AND dsc.isactive=@true ORDER BY dsc.ordernum";
					clockWorkDa.SelectCommand.Parameters.Clear();
					clockWorkDa.SelectCommand.Parameters.Add("@screennum", -overrideDynamicControlsScreenNum);
					clockWorkDa.SelectCommand.Parameters.Add("@true", true);
					string text;
					clockWorkDa.Fill(dataTable, out text);
					if (text != null && text.Length > 0)
					{
						ReportFunction.MessageBoxShow(text);
					}
					return dataTable;
				}
			}
			if (technoProReports != null)
			{
				dataTable = technoProReports.LoadDynamicControlsFromDataSet(overrideDynamicControlsScreenNum);
			}
			else
			{
				dataTable = new DataTable();
			}
			return dataTable;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000A05C File Offset: 0x0000905C
		private static DataTable GetVariablesCustom(int searchInfoID, UnivDataAdapter da, int overrideDynamicControlsScreenNum)
		{
			if (overrideDynamicControlsScreenNum == 0)
			{
			}
			DataTable dataTable = new DataTable();
			da.SelectCommand.CommandText = "SELECT dsc.controlid,dsc.screennum,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue,dc.defaultstring FROM searchdynamicscreencontrols dsc LEFT JOIN searchdynamiccontrols dc ON dc.controlid=dsc.controlid WHERE dsc.screennum=@screennum AND dsc.isactive=@true ORDER BY dsc.ordernum";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@screennum", overrideDynamicControlsScreenNum);
			da.SelectCommand.Parameters.Add("@true", true);
			da.Fill(dataTable);
			return dataTable;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000A0E8 File Offset: 0x000090E8
		private static void SetVariables_ifelseif(ref UnivDataAdapter da)
		{
			for (int i = 0; i < da.SelectCommand.Parameters.Count; i++)
			{
				object obj = da.SelectCommand.Parameters.Value(i);
				string text = (obj == null) ? "" : obj.ToString().ToLower().Trim();
				if (text.IndexOf("#<if") == 0)
				{
					text = text.Replace(" eq ", "=");
					text = text.Replace("#<", "");
					text = text.Replace(">#", "");
					text = text.Replace("else if", "elseif");
					text = "else" + text;
					int num = text.LastIndexOf("else");
					string text2 = "";
					if (num > 0)
					{
						num += 4;
						if (num < text.Length && text[num] == ' ')
						{
							text2 = text.Substring(num).Trim();
							num -= 4;
							text = text.Substring(0, num).Trim();
						}
					}
					num = -1;
					ArrayList arrayList = new ArrayList();
					for (;;)
					{
						num = text.IndexOf("elseif ", num + 1);
						if (num < 0)
						{
							break;
						}
						int num2 = text.IndexOf("elseif ", num + 1);
						string text3;
						if (num2 > 0)
						{
							text3 = text.Substring(num + 7, num2 - (num + 7)).Trim();
						}
						else
						{
							text3 = text.Substring(num + 7).Trim();
						}
						if (text3.Length > 0)
						{
							int num3 = text3.IndexOf(" then ");
							if (num3 > 0)
							{
								string text4 = text3.Substring(0, num3);
								string text5 = text3.Substring(num3 + 6);
								int num4 = text4.IndexOf("=");
								int num5 = text5.IndexOf("=");
								if (num4 > 0 && num5 > 0)
								{
									string text6 = text4.Substring(0, num4).Trim();
									string text7 = text5.Substring(0, num5).Trim();
									string text8 = text4.Substring(num4 + 1).Trim();
									string text9 = text5.Substring(num5 + 1).Trim();
									arrayList.Add(new string[]
									{
										text6,
										text8,
										text7,
										text9
									});
								}
							}
						}
					}
					bool flag = false;
					foreach (object obj2 in arrayList)
					{
						string[] array = (string[])obj2;
						string text6 = array[0];
						string text8 = array[1];
						string text7 = array[2];
						string text9 = array[3];
						if (da.SelectCommand.Parameters.Contains(text6))
						{
							object obj3 = da.SelectCommand.Parameters.Value(text6);
							string text10;
							if (obj3 is DateTime)
							{
								text10 = ((DateTime)obj3).ToString("yyyy-MM-dd hh:mm tt").ToLower();
							}
							else
							{
								text10 = obj3.ToString().ToLower().Trim();
							}
							if (text10.CompareTo(text8) == 0)
							{
								object obj4 = ReportFunction.SetIfElseVariable(ref da, text7, text9);
								if (obj4 != null)
								{
									flag = true;
								}
							}
						}
					}
					if (!flag && text2.Length > 0)
					{
						int num4 = text2.IndexOf("=");
						if (num4 > 0)
						{
							string text7 = text2.Substring(0, num4).Trim();
							string text9 = text2.Substring(num4 + 1).Trim();
							ReportFunction.SetIfElseVariable(ref da, text7, text9);
						}
					}
				}
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000A528 File Offset: 0x00009528
		private static object SetIfElseVariable(ref UnivDataAdapter da, string if2, string then2)
		{
			if (then2.Length > 0 && then2[0] == '@')
			{
				if (da.SelectCommand.Parameters.Contains(then2))
				{
					then2 = da.SelectCommand.Parameters.Value(then2).ToString();
				}
			}
			int num = if2.IndexOf('.');
			string parameterName = if2;
			object obj = then2;
			if (num > 0)
			{
				parameterName = if2.Substring(0, num);
				string text = if2.Substring(num + 1);
				if (text.CompareTo("date") == 0)
				{
					try
					{
						obj = DateTime.Parse(then2);
					}
					catch
					{
						obj = then2;
					}
				}
				else if (text.CompareTo("int") == 0)
				{
					try
					{
						obj = int.Parse(then2);
					}
					catch
					{
						obj = then2;
					}
				}
				else if (text.CompareTo("bool") == 0)
				{
					try
					{
						obj = bool.Parse(then2);
					}
					catch
					{
						obj = then2;
					}
				}
				else if (text.CompareTo("double") == 0)
				{
					try
					{
						obj = double.Parse(then2);
					}
					catch
					{
						obj = then2;
					}
				}
			}
			object result;
			if (da.SelectCommand.Parameters.Contains(parameterName))
			{
				da.SelectCommand.Parameters.SetValue(parameterName, obj);
				result = obj;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000A6FC File Offset: 0x000096FC
		private static bool GetVariablesFromUser(int searchInfoID, DataTable variablesTable, UnivDataAdapter da, DataSet comboBoxData, DataSet lookupTablesForControls, ref ArrayList variables, DataTable sessions, object[] yearStartEnd, string searchTitle, DataTable dynamicScreenNonDataControlsTable, DataTable searchCustomTable, ArrayList customVariables, int overrideDynamicControlsScreenNum, TripleDESEncryptionClass tripleDES, TechnoProReports technoProReports, int dbLocationCode)
		{
			return ReportFunction.GetVariablesFromUser(searchInfoID, variablesTable, da, comboBoxData, lookupTablesForControls, ref variables, sessions, yearStartEnd, searchTitle, dynamicScreenNonDataControlsTable, searchCustomTable, customVariables, overrideDynamicControlsScreenNum, tripleDES, technoProReports, dbLocationCode, null);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000A730 File Offset: 0x00009730
		public static bool GetVariablesFromUser(int searchInfoID, DataTable variablesTable, UnivDataAdapter da, DataSet comboBoxData, DataSet lookupTablesForControls, ref ArrayList variables, DataTable sessions, object[] yearStartEnd, string searchTitle, DataTable dynamicScreenNonDataControlsTable, DataTable searchCustomTable, ArrayList customVariables, int overrideDynamicControlsScreenNum, TripleDESEncryptionClass tripleDES, TechnoProReports technoProReports, int dbLocationCode, DataRow reportDr)
		{
			DialogResult dialogResult = new VariablesInput(searchInfoID, variablesTable, da, comboBoxData, lookupTablesForControls, ref variables, sessions, yearStartEnd, searchTitle, dynamicScreenNonDataControlsTable, searchCustomTable, ref customVariables, overrideDynamicControlsScreenNum, tripleDES, technoProReports, dbLocationCode, searchInfoID.ToString())
			{
				ReportDr = reportDr
			}.ShowDialog();
			return dialogResult == DialogResult.OK;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000A790 File Offset: 0x00009790
		private static DataTable GetDataTable(ArrayList tables, string tableName)
		{
			string strB = tableName.Trim().ToLower();
			foreach (object obj in tables)
			{
				DataTable dataTable = (DataTable)obj;
				string text = dataTable.TableName.ToLower().Trim();
				if (text.CompareTo(strB) == 0)
				{
					return dataTable;
				}
			}
			return null;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000A830 File Offset: 0x00009830
		public static string CReplace(string strExpression, string strSearch, string strReplace, int intMode)
		{
			string text;
			if (intMode == 1)
			{
				text = "";
				strSearch = strSearch.ToUpper();
				string text2 = strExpression.ToUpper();
				for (int i = text2.IndexOf(strSearch); i >= 0; i = text2.IndexOf(strSearch))
				{
					text = text + strExpression.Substring(0, i) + strReplace;
					strExpression = strExpression.Substring(i + strSearch.Length);
					text2 = text2.Substring(i + strSearch.Length);
				}
				text += strExpression;
			}
			else
			{
				text = strExpression.Replace(strSearch, strReplace);
			}
			return text;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000A8CC File Offset: 0x000098CC
		private static void MessageBoxShow(ref ArrayList errors, string message, bool suppressGuiMessages)
		{
			errors.Add(message);
			if (!suppressGuiMessages)
			{
				MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000A8F8 File Offset: 0x000098F8
		public static void RunFunction(string dbName, ReportStep reportStep, ref Report report, UnivDataAdapter da, ArrayList customVariables, TripleDESEncryptionClass tripleDES, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar, ref DataSet comboBoxData, ref DataTable staffNamesTable, DataSet lookupTablesForControls, ArrayList variables, DataTable sessions, object[] yearStartEnd, DataTable dynamicScreenNonDataControlsTable, DataTable searchCustomTable, int whoAmIPersonID, TechnoProReports technoProReports, int dbLocationCode, ref ArrayList errors, bool getUserInputForVariableValues)
		{
			ReportFunction.RunFunction(dbName, reportStep, ref report, da, customVariables, tripleDES, IncrementSubProgressBar, SetupSubProgressBar, ref comboBoxData, ref staffNamesTable, lookupTablesForControls, variables, sessions, yearStartEnd, dynamicScreenNonDataControlsTable, searchCustomTable, whoAmIPersonID, technoProReports, dbLocationCode, ref errors, getUserInputForVariableValues, false);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000A934 File Offset: 0x00009934
		public static void RunFunction(string dbName, ReportStep reportStep, ref Report report, UnivDataAdapter da, ArrayList customVariables, TripleDESEncryptionClass tripleDES, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar, ref DataSet comboBoxData, ref DataTable staffNamesTable, DataSet lookupTablesForControls, ArrayList variables, DataTable sessions, object[] yearStartEnd, DataTable dynamicScreenNonDataControlsTable, DataTable searchCustomTable, int whoAmIPersonID, TechnoProReports technoProReports, int dbLocationCode, ref ArrayList errors, bool getUserInputForVariableValues, bool suppressGuiMessages)
		{
			bool flag = (Control.ModifierKeys & Keys.Control) == Keys.Control;
			DataView currentDataView = report.GetCurrentDataView();
			DataTable dataTable = (currentDataView != null) ? currentDataView.Table : null;
			string text = reportStep.Parameters;
			if (report.FunctionParametersAreEncrypted)
			{
				byte[] array = Convert.FromBase64String(text);
				text = tripleDES.Decrypt(array);
			}
			FunctionCode functionCode = reportStep.FunctionCode;
			switch (functionCode)
			{
			case FunctionCode.Sql_Query:
			case FunctionCode.Sql_Query_Dynamic_Data:
			case FunctionCode.Sql_Query_Dynamic_Data_Keep_Rows_Without_Data_Info:
			case FunctionCode.Sql_Query_Dynamic_Data_2_Per_Student:
			case FunctionCode.Sql_Query_Dynamic_Data_2_Per_Appointment:
			{
				int num = -1;
				int num2 = 0;
				ArrayList arrayList = new ArrayList();
				for (;;)
				{
					int num3 = text.IndexOf("#<--");
					if (num3 < 0 || num3 == num)
					{
						break;
					}
					num = num3;
					int num4 = text.IndexOf(">#", num3 + 5);
					if (num4 >= 0)
					{
						string text2 = text.Substring(num3, num4 - num3 + 2);
						string text3 = text2.Substring(4, text2.Length - 6);
						string[] array2 = text3.Split(new char[]
						{
							':'
						});
						if (array2.Length == 2)
						{
							string text4 = array2[0].Trim();
							string text5 = array2[1].Trim().ToLower();
							if (text4.Length > 0)
							{
								int num5;
								try
								{
									num5 = int.Parse(text4);
								}
								catch
								{
									num5 = -1;
								}
								if (num5 >= 0)
								{
									DataTable dataTable2 = ReportFunction.GetDataTable(arrayList, "r" + num5);
									bool flag2 = 0 == 0;
									UnivDataAdapter univDataAdapter = da.Connection.CreateDataAdapter();
									univDataAdapter.SelectCommand.CommandText = "SELECT functionparameters," + num2.ToString() + " AS temptableoffset FROM searchfunctions WHERE searchinfoid=@searchinfoid and (functioncode=0 OR functioncode=1 OR functioncode=33 OR functioncode=44 OR functioncode=45) ORDER BY ordernum";
									univDataAdapter.SelectCommand.Parameters.Clear();
									univDataAdapter.SelectCommand.Parameters.Add("@searchinfoid", num5);
									dataTable2 = new DataTable();
									univDataAdapter.Fill(dataTable2);
									if (dataTable2.Rows.Count < 1 && technoProReports != null)
									{
										DataTable dataTable3 = technoProReports.LoadFunctionsFromDataSet(num5);
										DataRow[] array3 = dataTable3.Select("functioncode=0 OR functioncode=1 OR functioncode=33 OR functioncode=44 OR functioncode=45");
										DataTable dataTable4 = dataTable3.Clone();
										foreach (DataRow row in array3)
										{
											dataTable4.ImportRow(row);
										}
										DataView dataView = new DataView();
										dataView.Table = dataTable4;
										dataView.Sort = "ordernum";
										dataTable2 = dataTable4.Clone();
										foreach (object obj in dataView)
										{
											DataRowView dataRowView = (DataRowView)obj;
											dataTable2.ImportRow(dataRowView.Row);
										}
									}
									if (dataTable2.Rows.Count < 1)
									{
										errors.Add("Missing code reference! (report#" + num5.ToString() + ")");
									}
									else
									{
										dataTable2.TableName = "r" + num5.ToString();
										arrayList.Add(dataTable2);
									}
									num2++;
									string text6 = "";
									if (text5.ToLower().Trim().CompareTo("all") == 0 && dataTable2.Rows.Count > 0)
									{
										text6 = dataTable2.Rows[0][0].ToString().Trim();
									}
									else
									{
										foreach (object obj2 in dataTable2.Rows)
										{
											DataRow dataRow = (DataRow)obj2;
											string text7 = dataRow[0].ToString().Trim();
											string text8 = text7.ToLower();
											num3 = text8.IndexOf("--" + text5);
											if (num3 >= 0)
											{
												num4 = text8.IndexOf(";", num3 + 1);
												if (num4 < 0)
												{
													num4 = text8.IndexOf("--", num3 + 1);
												}
												else
												{
													num4++;
												}
												if (num4 < 0)
												{
													num4 = text8.Length - 1;
												}
												int num6 = num4 - num3;
												if (num6 > 0)
												{
													text6 = text7.Substring(num3, num6) + System.Environment.NewLine;
													int num7 = (int)dataRow[1];
													if (num7 > 0)
													{
														for (int j = 0; j < 10; j++)
														{
															int num8 = j + num7;
															text6 = text6.Replace("#t" + j.ToString(), "#t" + num8.ToString());
														}
													}
												}
												break;
											}
										}
									}
									text = text.Replace(text2, text6);
								}
							}
						}
					}
				}
				int k = text.IndexOf('{');
				string strB = (dbName == null) ? string.Empty : dbName.ToLower().Trim();
				while (k >= 0)
				{
					int num9 = text.IndexOf('}', k);
					if (num9 > k + 1)
					{
						string text9 = text.Substring(k + 1, num9 - k - 1).Trim();
						string[] array5 = text9.Split(new char[]
						{
							'~'
						});
						string text10 = null;
						bool flag3 = false;
						string oldValue = "{" + text9 + "}";
						foreach (string text11 in array5)
						{
							int num10 = text11.IndexOf('=');
							if (num10 >= 0 && num10 < text11.Length - 1)
							{
								if (num10 == 0)
								{
									text10 = text11.Substring(num10 + 1);
								}
								else
								{
									string text12 = text11.Substring(0, num10).Trim().ToLower();
									string newValue = text11.Substring(num10 + 1);
									if (text12.CompareTo(strB) == 0)
									{
										text = text.Replace(oldValue, newValue);
										flag3 = true;
										break;
									}
								}
							}
						}
						if (!flag3 && text10 != null)
						{
							text = text.Replace(oldValue, text10);
						}
					}
					k = text.IndexOf('{', k + 1);
				}
				if (customVariables.Count > 0)
				{
					foreach (object obj3 in customVariables)
					{
						Variable variable = (Variable)obj3;
						string text2 = "#<" + variable.VariableName + ">#";
						text = ReportFunction.CReplace(text, text2, "(" + (string)variable.VariableValue + ")", 1);
					}
				}
				if (functionCode == FunctionCode.Sql_Query_Dynamic_Data || functionCode == FunctionCode.Sql_Query_Dynamic_Data_Keep_Rows_Without_Data_Info || functionCode == FunctionCode.Sql_Query_Dynamic_Data_2_Per_Student || functionCode == FunctionCode.Sql_Query_Dynamic_Data_2_Per_Appointment)
				{
					ReportFunction.CallSetupProgressBar(SetupSubProgressBar, 0, 3);
				}
				else
				{
					ReportFunction.CallSetupProgressBar(SetupSubProgressBar, 0, 2);
				}
				ReportFunction.CallIncrementProgressBar(IncrementSubProgressBar);
				da.SelectCommand.CommandText = text;
				DateTime dateTime = DateTime.MinValue;
				DateTime dateTime2 = DateTime.MinValue;
				if (da.SelectCommand.Parameters.Contains("@enddate"))
				{
					object obj4 = da.SelectCommand.Parameters.Value("@enddate");
					if (obj4 is DateTime)
					{
						dateTime = (DateTime)obj4;
						da.SelectCommand.Parameters.SetValue("@enddate", new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59, 59));
					}
				}
				if (da.SelectCommand.Parameters.Contains("@schoolyear_enddate"))
				{
					object obj4 = da.SelectCommand.Parameters.Value("@schoolyear_enddate");
					if (obj4 is DateTime)
					{
						dateTime2 = (DateTime)obj4;
						da.SelectCommand.Parameters.SetValue("@schoolyear_enddate", new DateTime(dateTime2.Year, dateTime2.Month, dateTime2.Day, 23, 59, 59, 59));
					}
				}
				ReportFunction.SetVariables_ifelseif(ref da);
				DataTable dataTable5 = new DataTable();
				if (flag)
				{
					string text13 = UnivOleDbFactory.ToStringParametersExpanded(da.SelectCommand);
					StringEdit stringEdit = new StringEdit("", "", text13);
					DialogResult dialogResult = stringEdit.ShowDialog();
					if (dialogResult == DialogResult.OK)
					{
						da.SelectCommand.CommandText = stringEdit.UserText;
					}
				}
				string text14 = null;
				Regex regex = new Regex("@!([_a-zA-Z]+)");
				MatchCollection matchCollection = regex.Matches(da.SelectCommand.CommandText);
				if (matchCollection != null && matchCollection.Count > 0)
				{
					foreach (object obj5 in matchCollection)
					{
						Match match = (Match)obj5;
						if (!string.IsNullOrEmpty(match.Value) && match.Value.Length > 2)
						{
							string text15 = string.Format("@{0}", match.Value.Substring(2));
							string parameterName = text15 + "e";
							if (da.SelectCommand.Parameters.Contains(text15))
							{
								object obj6 = da.SelectCommand.Parameters.Value(text15);
								byte[] array;
								if (obj6 == null)
								{
									array = new byte[0];
								}
								else
								{
									array = tripleDES.Encrypt(obj6.ToString());
								}
								if (da.SelectCommand.Parameters.Contains(parameterName))
								{
									da.SelectCommand.Parameters.SetValue(parameterName, array);
								}
								else
								{
									da.SelectCommand.Parameters.Add(parameterName, array);
								}
							}
						}
					}
				}
				string text16 = da.SelectCommand.CommandText.ToLower();
				List<string> list = new List<string>();
				for (int j = 0; j < da.SelectCommand.Parameters.Count; j++)
				{
					string text17 = da.SelectCommand.Parameters.ParameterName(j);
					if (!text16.Contains(text17.ToLower()))
					{
						list.Add(text17);
					}
				}
				foreach (string text17 in list)
				{
					string text17;
					da.SelectCommand.Parameters.Clear(text17);
				}
				if (da.Connection.IsOpen)
				{
					try
					{
						UnivDataReader reader = da.ExecuteSelectCommandReaderInTransaction(da.Connection.Transaction);
						dataTable5 = UnivOleDbFactory.ReaderToDataTable(reader);
					}
					catch (Exception ex)
					{
						text14 = ex.ToString();
						da.Connection.Close();
					}
				}
				else
				{
					da.Fill(dataTable5, out text14);
				}
				if (da.SelectCommand.Parameters.Contains("@enddate"))
				{
					da.SelectCommand.Parameters.SetValue("@enddate", dateTime);
				}
				if (da.SelectCommand.Parameters.Contains("@schoolyear_enddate"))
				{
					da.SelectCommand.Parameters.SetValue("@schoolyear_enddate", dateTime2);
				}
				if (!string.IsNullOrEmpty(text14))
				{
					errors.Add(text14);
					if (!suppressGuiMessages)
					{
						ReportFunction.MessageBoxShow(ref errors, text14, suppressGuiMessages);
					}
				}
				int count = dataTable5.Rows.Count;
				ReportFunction.CallIncrementProgressBar(IncrementSubProgressBar);
				UnivDataAdapter da2 = da.Connection.CreateDataAdapter();
				DataView dataView2;
				if (functionCode == FunctionCode.Sql_Query_Dynamic_Data || functionCode == FunctionCode.Sql_Query_Dynamic_Data_Keep_Rows_Without_Data_Info)
				{
					dataTable5 = Reports.FormatStudentData(dataTable5, tripleDES, da2, ref comboBoxData, staffNamesTable, functionCode == FunctionCode.Sql_Query_Dynamic_Data_Keep_Rows_Without_Data_Info);
					dataView2 = dataTable5.DefaultView;
					ReportFunction.CallIncrementProgressBar(IncrementSubProgressBar);
				}
				else if (functionCode == FunctionCode.Sql_Query_Dynamic_Data_2_Per_Appointment)
				{
					dataView2 = Reports.FormatAndMapToColumnsStudentDataPerAppointment(dataTable5.DefaultView, tripleDES, da2, ref comboBoxData, staffNamesTable);
					ReportFunction.CallIncrementProgressBar(IncrementSubProgressBar);
				}
				else if (functionCode == FunctionCode.Sql_Query_Dynamic_Data_2_Per_Student)
				{
					dataView2 = Reports.FormatAndMapToColumnsStudentDataPerStudent(new DataView(dataTable5), tripleDES, da2, ref comboBoxData, staffNamesTable);
					ReportFunction.CallIncrementProgressBar(IncrementSubProgressBar);
				}
				else
				{
					dataView2 = new DataView(dataTable5);
				}
				report.AddResult(dataView2);
				break;
			}
			case FunctionCode.Breakdown_Numbers_Dynamic_Data:
				ReportFunction.BreakdownData(ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Decrypt_Data:
			{
				string text18;
				string encryptionKey;
				string encryptionType;
				if (text.IndexOf("`") >= 0)
				{
					string[] array7 = text.Split(new char[]
					{
						'`'
					});
					if (array7.Length == 2)
					{
						text18 = array7[1];
						encryptionKey = array7[0];
						encryptionType = "";
					}
					else if (array7.Length == 3)
					{
						text18 = array7[2];
						encryptionType = array7[0];
						encryptionKey = array7[1];
					}
					else
					{
						encryptionType = "";
						encryptionKey = "";
						text18 = text;
					}
				}
				else
				{
					encryptionType = "";
					encryptionKey = "";
					text18 = text;
				}
				ReportFunction.DecryptData(ref report, text18, da, encryptionType, encryptionKey, tripleDES, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			}
			case FunctionCode.Sort:
				ReportFunction.Sort(ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Run_Another_Report:
			{
				int num11 = text.IndexOf('.');
				int num12;
				List<int> list2;
				if (num11 > 0)
				{
					num12 = int.Parse(text.Substring(0, num11));
					list2 = ClockWorkAPI.Utility.IntListFromString(text.Substring(num11 + 1));
					if (list2.Count < 1)
					{
						list2 = null;
					}
				}
				else
				{
					num12 = int.Parse(text);
					list2 = null;
				}
				DataTable dataTable6 = (technoProReports == null) ? new DataTable() : technoProReports.LoadSearchFromDataSet(num12);
				if (dataTable6.Rows.Count < 1)
				{
					da.SelectCommand.CommandText = "SELECT si.searchinfoid,si.title,si.description,si.searchgroupid,si.datecreated,si.datelastmodified,si.whocreated,si.wholastmodified,sgi.grouptitle,sgi.groupdescription,sgi.iconindex,si.searchchartinfoid,si.overrideDynamicControlsScreenNum,1 AS dblocationcode FROM searchinfo si LEFT JOIN searchgroupinfo sgi ON sgi.searchgroupinfoid=si.searchgroupid WHERE si.searchinfoid=@searchinfoid";
					da.SelectCommand.Parameters.Clear();
					da.SelectCommand.Parameters.Add("@searchinfoid", num12);
					string text14 = null;
					da.Fill(dataTable6, out text14);
					if (text14 != null && text14.Length > 0)
					{
						errors.Add(text14);
					}
				}
				if (dataTable6 != null && dataTable6.Rows.Count > 0)
				{
					EventHandler reportStartedHandler = null;
					Report report2 = new Report(dataTable6.Rows[0]);
					report2.AddResult(report.GetCurrentDataView());
					ArrayList arrayList2;
					Report report3 = ReportFunction.RunReport(report2, true, dbName, dataTable6.Rows[0], da, comboBoxData, lookupTablesForControls, variables, sessions, yearStartEnd, dynamicScreenNonDataControlsTable, searchCustomTable, tripleDES, null, null, null, null, staffNamesTable, whoAmIPersonID, technoProReports, out arrayList2, getUserInputForVariableValues, reportStartedHandler, null, suppressGuiMessages, null, list2);
					if (report3 != null)
					{
						ReportResults reportResults = report3.ReportResults;
						report.MergeInReportResults(reportResults);
					}
					else
					{
						report.AddResult(new DataTable().DefaultView);
					}
				}
				break;
			}
			case FunctionCode.Remove_Items_With_Specific_Value:
			{
				string[] array8 = text.Split(new char[]
				{
					','
				});
				ReportFunction.RemoveItems(ref report, array8[0], array8[1], IncrementSubProgressBar, SetupSubProgressBar);
				break;
			}
			case FunctionCode.Reorder_Columns:
				ReportFunction.ReorderColumns(ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Map_Cells_to_Columns:
			{
				string[] array9 = text.Split(new char[]
				{
					'`'
				});
				if (array9.Length > 0)
				{
					string text19 = array9[0];
					string text20 = "";
					int screenNum;
					if (array9.Length > 1)
					{
						for (int j = 1; j < array9.Length; j++)
						{
							text20 = array9[1].Trim();
							if (text20.Length > 0)
							{
								break;
							}
						}
						if (text20.Length > 0)
						{
							try
							{
								screenNum = int.Parse(text20);
							}
							catch
							{
								screenNum = -1;
							}
						}
						else
						{
							screenNum = -1;
						}
					}
					else
					{
						screenNum = -1;
					}
					array9 = text19.Split(new char[]
					{
						','
					});
					if (array9.Length > 1)
					{
						string columnNameColName = array9[0];
						string columnValueColName = array9[1];
						string text21 = "";
						for (int j = 2; j < array9.Length; j++)
						{
							if (text21.Length > 0)
							{
								text21 += ",";
							}
							text21 += array9[j];
						}
						ReportFunction.MapCellsToColumns(da, screenNum, ref report, columnNameColName, columnValueColName, text21, null, IncrementSubProgressBar, SetupSubProgressBar);
					}
				}
				break;
			}
			case FunctionCode.Merge_Rows:
			{
				string[] array10 = text.Split(new char[]
				{
					'`'
				});
				string uniqueColumnNames = array10[0];
				string colNameValueAndList;
				if (array10.Length > 1)
				{
					colNameValueAndList = array10[1];
				}
				else
				{
					colNameValueAndList = "";
				}
				ReportFunction.MergeRows(ref report, uniqueColumnNames, colNameValueAndList, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			}
			case FunctionCode.Remove_Columns:
			{
				string[] colsToRemove = text.Split(new char[]
				{
					','
				});
				ReportFunction.RemoveColumns(ref report, colsToRemove, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			}
			case FunctionCode.Rename_Columns:
			{
				string[] colOldNameEqualsNewName = text.Split(new char[]
				{
					','
				});
				ReportFunction.RenameColumns(ref report, colOldNameEqualsNewName, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			}
			case FunctionCode.Combine_Columns:
			{
				string[] array9 = text.Split(new char[]
				{
					'`'
				});
				ReportFunction.CombineColumns(ref report, array9, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			}
			case FunctionCode.Map_Column_Names_to_Specific_Values:
				ReportFunction.MapColumnNamesToSpecificValues(ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Move_Data_to_Other_Columns_for_Specific_Rows:
			{
				string[] array9 = text.Split(new char[]
				{
					'`'
				});
				ReportFunction.MoveDataToOtherColumnsForSpecificRows(ref report, array9[0], array9[1], IncrementSubProgressBar, SetupSubProgressBar);
				break;
			}
			case FunctionCode.Concatenate_Column_Cell_Data_Text:
				ReportFunction.ConcatenateColumnCellDataText(ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Search_and_Replace_Case_Sensitive:
			{
				string[] array9 = text.Split(new char[]
				{
					'`'
				});
				if (array9.Length == 3)
				{
					ReportFunction.SearchAndReplaceCaseSensitive(ref report, array9[0], array9[1], array9[2], IncrementSubProgressBar, SetupSubProgressBar);
				}
				break;
			}
			case FunctionCode.Remove_Extra_Spaces_From_Comma_Separated_List:
				ReportFunction.RemoveExtraSpacesFromCommaSeparatedList(ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Mark_Rows_as_Special_That_Have_Differing_Values_for_Unique_Row_Groups:
			{
				string[] array9 = text.Split(new char[]
				{
					'`'
				});
				if (array9.Length == 3)
				{
					ReportFunction.MarkRowsAsSpecialThatHaveDiffereningValuesForUniqueRowGroups(ref report, array9[0], array9[1], array9[2], IncrementSubProgressBar, SetupSubProgressBar);
				}
				break;
			}
			case FunctionCode.Remove_Duplicate_Rows:
			{
				int num13 = text.IndexOf('`');
				if (num13 > 0 && num13 < text.Length - 1)
				{
					bool leaveFirstDuplicateRow = text.Substring(num13 + 1).Trim().CompareTo("1") == 0;
					ReportFunction.RemoveDuplicateRows(ref report, text.Substring(0, num13), leaveFirstDuplicateRow, IncrementSubProgressBar, SetupSubProgressBar);
				}
				else
				{
					ReportFunction.RemoveDuplicateRows(ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				}
				break;
			}
			case FunctionCode.Extract_and_Return_Rows_With_Temp_or_Invalid_Student_Numbers:
			{
				string[] array9 = text.Split(new char[]
				{
					','
				});
				if (array9.Length >= 1)
				{
					int exactNumCharactersInValidStudentNum;
					if (array9.Length >= 2)
					{
						string text22 = array9[1].Trim();
						if (text22.Length > 0)
						{
							try
							{
								exactNumCharactersInValidStudentNum = Convert.ToInt32(text22);
							}
							catch
							{
								exactNumCharactersInValidStudentNum = -1;
							}
						}
						else
						{
							exactNumCharactersInValidStudentNum = -1;
						}
					}
					else
					{
						exactNumCharactersInValidStudentNum = -1;
					}
					ReportFunction.ExtractAndReturnRowsWithTemporaryStudentNumbers(ref report, array9[0], exactNumCharactersInValidStudentNum, IncrementSubProgressBar, SetupSubProgressBar);
				}
				break;
			}
			case FunctionCode.Remove_Rows_With_Temp_or_Invalid_Student_Numbers:
			{
				string[] array9 = text.Split(new char[]
				{
					','
				});
				if (array9.Length >= 1)
				{
					int num15;
					int maxNumCharsInValidStudentNum;
					if (array9.Length >= 2)
					{
						string text22 = array9[1].Trim();
						if (text22.Length > 0)
						{
							int num14 = text22.IndexOf('-');
							if (num14 > 0)
							{
								string s = text22.Substring(0, num14);
								string s2 = text22.Substring(num14 + 1);
								try
								{
									num15 = int.Parse(s);
									maxNumCharsInValidStudentNum = int.Parse(s2);
								}
								catch
								{
									num15 = -1;
									maxNumCharsInValidStudentNum = -1;
								}
							}
							else
							{
								try
								{
									num15 = Convert.ToInt32(text22);
									maxNumCharsInValidStudentNum = num15;
								}
								catch
								{
									num15 = -1;
									maxNumCharsInValidStudentNum = -1;
								}
							}
						}
						else
						{
							num15 = -1;
							maxNumCharsInValidStudentNum = -1;
						}
					}
					else
					{
						num15 = -1;
						maxNumCharsInValidStudentNum = -1;
					}
					ReportFunction.RemoveRowsWithTemporaryStudentNumbers(ref report, array9[0], num15, maxNumCharsInValidStudentNum, IncrementSubProgressBar, SetupSubProgressBar);
				}
				break;
			}
			case FunctionCode.Breakdown_Numbers:
			{
				string[] array11 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				if (array11.Length <= 1)
				{
					ReportFunction.BreakdownNumbers(ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				}
				else
				{
					ReportFunction.BreakdownNumbers(ref report, da, array11[0], array11[1], IncrementSubProgressBar, SetupSubProgressBar);
				}
				break;
			}
			case FunctionCode.Keep_Only_Duplicate_Rows:
				ReportFunction.KeepOnlyDuplicateRows(ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Force_Specific_Columns_in_a_Specific_Order:
				ReportFunction.ForceSpecificColumnsAndOrdering(ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Split_Col_Data_into_Multiple_Columns:
				ReportFunction.SplitColDataIntoMultipleColumns(ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Execute_and_Merge_in_Another_Report:
				try
				{
					string[] array12 = text.Split(new char[]
					{
						'`'
					});
					string[] array13 = array12[0].Split(new char[]
					{
						'~'
					});
					int num12 = int.Parse(array13[0]);
					if (array13.Length > 1)
					{
						string[] array14 = array13[1].Split(new char[]
						{
							';'
						});
						foreach (string text23 in array14)
						{
							int num16 = text23.IndexOf('=');
							if (num16 > 0)
							{
								string text17 = text23.Substring(0, num16).Trim().ToLower();
								object obj7;
								if (++num16 < text23.Length)
								{
									string text24 = text23.Substring(num16).Trim();
									if (text24.Length > 0 && char.IsDigit(text24[0]))
									{
										try
										{
											obj7 = int.Parse(text24);
										}
										catch
										{
											obj7 = text24;
										}
									}
									else
									{
										obj7 = text24;
									}
								}
								else
								{
									string text24 = "";
									obj7 = text24;
								}
								if (obj7 is string)
								{
									string text13 = (string)obj7;
									if (text13.Length == 2)
									{
										if (text13[0] == '\'' && text13[text13.Length - 1] == '\'')
										{
											obj7 = "";
										}
									}
									else if (text13.Length > 2)
									{
										if (text13[0] == '\'' && text13[text13.Length - 1] == '\'' && text13[1] != '\'')
										{
											obj7 = text13.Substring(1, text13.Length - 2);
										}
									}
								}
								bool flag3 = false;
								foreach (object obj8 in variables)
								{
									Variable variable2 = (Variable)obj8;
									if (text17.CompareTo(variable2.VariableName) == 0)
									{
										variable2.VariableValue = obj7;
										flag3 = true;
										break;
									}
								}
								if (!flag3)
								{
									Variable value = new Variable(text17, obj7);
									variables.Add(value);
								}
							}
						}
					}
					Report report4 = ReportFunction.ExecuteAnotherReport(da, technoProReports, num12, ref errors, dbName, comboBoxData, lookupTablesForControls, variables, sessions, yearStartEnd, dynamicScreenNonDataControlsTable, searchCustomTable, tripleDES, staffNamesTable, whoAmIPersonID, SetupSubProgressBar, IncrementSubProgressBar);
					if (report4 != null)
					{
						report.MergeInReportResults(report4.ReportResults);
					}
				}
				catch (Exception ex2)
				{
					ReportFunction.MessageBoxShow(ref errors, ex2.ToString(), suppressGuiMessages);
				}
				break;
			case FunctionCode.Stamp_Current_Table:
				try
				{
					string[] array12 = text.Split(new char[]
					{
						'`'
					});
					string text25 = array12[0];
					string dtype = array12[1].Trim().ToLower();
					string newVal = array12[2];
					DataView currentDataView2 = report.GetCurrentDataView();
					ReportFunction.StampTable(ref currentDataView2, text25, dtype, newVal, SetupSubProgressBar, IncrementSubProgressBar);
				}
				catch (Exception ex2)
				{
					ReportFunction.MessageBoxShow(ref errors, ex2.ToString(), suppressGuiMessages);
				}
				break;
			case FunctionCode.Run_Another_Report_and_Concatenate_the_Results_to_the_Current_Table:
				try
				{
					string[] array12 = text.Split(new char[]
					{
						'`'
					});
					int num12 = int.Parse(array12[0]);
					Report report5 = ReportFunction.ExecuteAnotherReport(da, technoProReports, num12, ref errors, dbName, comboBoxData, lookupTablesForControls, variables, sessions, yearStartEnd, dynamicScreenNonDataControlsTable, searchCustomTable, tripleDES, staffNamesTable, whoAmIPersonID, SetupSubProgressBar, IncrementSubProgressBar);
					DataView dataView2 = (report5 == null) ? null : report5.GetCurrentDataView();
					if (dataView2 != null)
					{
						if (array12.Length > 1)
						{
							string[] array15 = array12[1].Split(new char[]
							{
								','
							});
							foreach (string text26 in array15)
							{
								string[] array16 = text26.Split(new char[]
								{
									'='
								});
								if (array16.Length == 2)
								{
									string name = array16[0].Trim();
									string text25 = array16[1].Trim();
									if (dataView2.Table.Columns.Contains(name))
									{
										dataView2.Table.Columns[name].ColumnName = text25;
									}
								}
							}
							if (array12.Length > 2)
							{
								string[] array17 = array12[2].Trim().Split(new char[]
								{
									','
								});
								if (array17.Length >= 3)
								{
									ReportFunction.StampTable(ref dataView2, array17[0], array17[1], array17[2], SetupSubProgressBar, IncrementSubProgressBar);
								}
							}
						}
						if (currentDataView != null && currentDataView.Table != null && currentDataView.Table.Rows.Count > 0)
						{
							int[] array18 = new int[dataView2.Table.Columns.Count];
							for (int l = 0; l < dataView2.Table.Columns.Count; l++)
							{
								int num3 = currentDataView.Table.Columns.IndexOf(dataView2.Table.Columns[l].ColumnName);
								if (num3 >= 0)
								{
									array18[l] = num3;
								}
								else
								{
									DataColumn dataColumn = currentDataView.Table.Columns.Add(dataView2.Table.Columns[l].ColumnName);
									array18[l] = dataColumn.Ordinal;
								}
							}
							for (int m = 0; m < dataView2.Count; m++)
							{
								object[] array19 = new object[currentDataView.Table.Columns.Count];
								for (int n = 0; n < array18.Length; n++)
								{
									array19[array18[n]] = dataView2[m].Row[n];
								}
								currentDataView.Table.Rows.Add(array19);
							}
						}
					}
				}
				catch (Exception ex2)
				{
					ReportFunction.MessageBoxShow(ref errors, ex2.ToString(), suppressGuiMessages);
				}
				break;
			case FunctionCode.Add_New_Columns:
			{
				DataView dataView2 = report.GetCurrentDataView();
				ReportFunction.AddNewColumns(ref dataView2, text);
				break;
			}
			case FunctionCode.Change_Column_DataTypes:
			{
				DataView dataView2 = report.GetCurrentDataView();
				ReportFunction.ChangeColumnDataTypes(ref dataView2, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			}
			case FunctionCode.Add_New_Columns_Dynamic:
				try
				{
					da.SelectCommand.CommandText = text;
					DataTable dataTable7 = new DataTable();
					da.Fill(dataTable7);
					if (dataTable7.Rows.Count > 0)
					{
						string text27 = "";
						for (int j = 0; j < dataTable7.Rows.Count; j++)
						{
							if (text27.Length > 0)
							{
								text27 += "`";
							}
							text27 += dataTable7.Rows[j][0].ToString();
						}
						DataView dataView2 = report.GetCurrentDataView();
						ReportFunction.AddNewColumns(ref dataView2, text27);
					}
				}
				catch (Exception ex2)
				{
					ReportFunction.MessageBoxShow(ref errors, ex2.ToString(), suppressGuiMessages);
				}
				break;
			case FunctionCode.Run_Another_Report_and_Concatenate_UNIQUE_Results_to_the_Current_Table:
				try
				{
					string[] array12 = text.Split(new char[]
					{
						'`'
					});
					int num12 = int.Parse(array12[0]);
					string matchingColsStr = array12[1];
					string colsToImportStr = array12[2];
					Report report6 = ReportFunction.ExecuteAnotherReport(da, technoProReports, num12, ref errors, dbName, comboBoxData, lookupTablesForControls, variables, sessions, yearStartEnd, dynamicScreenNonDataControlsTable, searchCustomTable, tripleDES, staffNamesTable, whoAmIPersonID, SetupSubProgressBar, IncrementSubProgressBar);
					DataView dataView2 = report6.GetCurrentDataView();
					if (dataView2 != null)
					{
						if (array12.Length > 3)
						{
							string[] array15 = array12[3].Split(new char[]
							{
								','
							});
							foreach (string text26 in array15)
							{
								string[] array16 = text26.Split(new char[]
								{
									'='
								});
								if (array16.Length == 2)
								{
									string name = array16[0].Trim();
									string text25 = array16[1].Trim();
									if (dataView2.Table.Columns.Contains(name))
									{
										dataView2.Table.Columns[name].ColumnName = text25;
									}
								}
							}
							if (array12.Length > 4)
							{
								string[] array17 = array12[4].Trim().Split(new char[]
								{
									','
								});
								if (array17.Length >= 3)
								{
									ReportFunction.StampTable(ref dataView2, array17[0], array17[1], array17[2], SetupSubProgressBar, IncrementSubProgressBar);
								}
							}
						}
						ReportFunction.RunAnotherReportAndConcatenateRowsThatArentAlreadyThere(ref report, dataView2, matchingColsStr, colsToImportStr, SetupSubProgressBar, IncrementSubProgressBar);
					}
				}
				catch (Exception ex2)
				{
					ReportFunction.MessageBoxShow(ref errors, ex2.ToString(), suppressGuiMessages);
				}
				break;
			case FunctionCode.Create_New_Boolean_Columns_from_Unique_Values_in_a_Column:
				ReportFunction.CreateNewBooleanColumnsFromUniqueValuesInAColumn(ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Multiple_Rows_One_for_each_Value_in_a_Delimiter_Separated_Column_Cell:
			{
				string[] array20 = text.Split(new char[]
				{
					'`'
				});
				ReportFunction.MultiplyRows(ref report, array20[0], array20[1], SetupSubProgressBar, IncrementSubProgressBar);
				break;
			}
			case FunctionCode.Merge_Rows_Exclude_Duplicate_Items_in_Comma_Separated_Lists:
				try
				{
					string[] array21 = text.Split(new char[]
					{
						'`'
					});
					string uniqueColumnNames2 = array21[0];
					string colNameValueAndList2;
					if (array21.Length > 1)
					{
						colNameValueAndList2 = array21[1];
					}
					else
					{
						colNameValueAndList2 = "";
					}
					ReportFunction.MergeRowsExcludeDuplicatesInCommaSeparatedList(ref report, uniqueColumnNames2, colNameValueAndList2, IncrementSubProgressBar, SetupSubProgressBar);
				}
				catch (Exception ex2)
				{
					ReportFunction.MessageBoxShow(ref errors, ex2.ToString(), suppressGuiMessages);
				}
				break;
			case FunctionCode.Add_Time_Duration_Column:
				ReportFunction.AddTimeDurationColumn(ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Add_Column_with_Count_of_Delimitered_Items_in_Another_Column:
			{
				string[] array22 = text.Split(new char[]
				{
					','
				});
				string delimiter = ",";
				if (array22.Length > 2)
				{
					delimiter = array22[2];
				}
				ReportFunction.AddColumnWithCountOfCommaSeparatedItemsInAnotherColumn(ref report, array22[0], array22[1], delimiter, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			}
			case FunctionCode.Set_Variables:
			{
				string[] ps = text.Split(new char[]
				{
					'`'
				});
				ReportFunction.SetVariablesExplicitly(ps, 0, ref variables, da, tripleDES, da.SelectCommand.Parameters);
				break;
			}
			case FunctionCode.Run_Another_Report_Without_Collecting_Parameters_From_the_User:
				try
				{
					string[] array23 = text.Split(new char[]
					{
						'`'
					});
					int num12 = int.Parse(array23[0]);
					DataTable dataTable8 = new DataTable();
					ReportFunction.SetVariablesExplicitly(array23, 1, ref variables, da, tripleDES, da.SelectCommand.Parameters);
					if (technoProReports != null)
					{
						dataTable8 = technoProReports.LoadSearchFromDataSet(num12);
					}
					if (dataTable8.Rows.Count < 1)
					{
						da.SelectCommand.CommandText = "SELECT si.searchinfoid,si.title,si.description,si.searchgroupid,si.datecreated,si.datelastmodified,si.whocreated,si.wholastmodified,sgi.grouptitle,sgi.groupdescription,sgi.iconindex,si.searchchartinfoid,si.overrideDynamicControlsScreenNum,1 AS dblocationcode FROM searchinfo si LEFT JOIN searchgroupinfo sgi ON sgi.searchgroupinfoid=si.searchgroupid WHERE si.searchinfoid=@searchinfoid";
						da.SelectCommand.Parameters.Clear();
						da.SelectCommand.Parameters.Add("@searchinfoid", num12);
						string text14 = null;
						da.Fill(dataTable8, out text14);
						if (text14 != null && text14.Length > 0)
						{
							errors.Add(text14);
						}
					}
					if (dataTable8.Rows.Count > 0)
					{
						ArrayList arrayList2;
						Report report7 = ReportFunction.RunReport(false, dbName, dataTable8.Rows[0], da, comboBoxData, lookupTablesForControls, variables, sessions, yearStartEnd, dynamicScreenNonDataControlsTable, searchCustomTable, tripleDES, null, null, null, null, staffNamesTable, whoAmIPersonID, technoProReports, out arrayList2, false, null, null, false, null);
						report.MergeInReportResults(report7.ReportResults);
					}
				}
				catch (Exception ex2)
				{
					ReportFunction.MessageBoxShow(ref errors, ex2.ToString(), suppressGuiMessages);
				}
				break;
			case FunctionCode.Set_All_Blank_Cells_to_NULL:
				ReportFunction.SetBlankCellsToNull(ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Merge_Accommodations_for_Students_With_2_Rows_of_Accommodations:
				try
				{
					string[] array12 = text.Split(new char[]
					{
						'`'
					});
					string uniqueCols = array12[0];
					string colsToIgnore;
					if (array12.Length > 1)
					{
						colsToIgnore = array12[1];
					}
					else
					{
						colsToIgnore = "";
					}
					ReportFunction.Merge2DifferentSetsOfStudentAccommodationsForTheSameStudent(ref report, uniqueCols, colsToIgnore, IncrementSubProgressBar, SetupSubProgressBar);
				}
				catch (Exception ex2)
				{
					ReportFunction.MessageBoxShow(ref errors, ex2.ToString(), suppressGuiMessages);
				}
				break;
			case FunctionCode.Encrypt_Data:
			{
				string text18;
				string encryptionKey;
				string encryptionType;
				if (text.IndexOf("`") > 0)
				{
					string[] array7 = text.Split(new char[]
					{
						'`'
					});
					if (array7.Length == 2)
					{
						text18 = array7[1];
						encryptionKey = array7[0];
						encryptionType = "";
					}
					else if (array7.Length == 3)
					{
						text18 = array7[2];
						encryptionType = array7[0];
						encryptionKey = array7[1];
					}
					else
					{
						encryptionType = "";
						encryptionKey = "";
						text18 = text;
					}
				}
				else
				{
					encryptionType = "";
					encryptionKey = "";
					text18 = text;
				}
				ReportFunction.EncryptData(ref report, text18, encryptionType, encryptionKey, tripleDES, IncrementSubProgressBar, SetupSubProgressBar, da);
				break;
			}
			case FunctionCode.Import_User_Data:
			case FunctionCode.Import_User_Data_TEST:
			{
				ReportFunction.Log("Starting import user data...");
				DataView dataView2 = report.GetCurrentDataView();
				ReportFunction.ImportStudents(dataView2, text, IncrementSubProgressBar, SetupSubProgressBar, tripleDES, da, functionCode == FunctionCode.Import_User_Data, suppressGuiMessages);
				break;
			}
			case FunctionCode.Sql_Query_from_External_Table:
			{
				int num17 = text.IndexOf("`");
				if (num17 > 0)
				{
					string text28 = text.Substring(0, num17).Trim().ToLower();
					int num4 = text.IndexOf("`", num17 + 1);
					if (num4 >= 0)
					{
						string connectionString = text.Substring(num17 + 1, num4 - num17 - 1);
						string text29 = text.Substring(num4 + 1);
						DataTable dataTable3 = new DataTable();
						string text30 = da.SelectCommand.CommandText.ToLower();
						List<string> list3 = new List<string>();
						for (int j = 0; j < da.SelectCommand.Parameters.Count; j++)
						{
							string text17 = da.SelectCommand.Parameters.ParameterName(j);
							if (!text29.Contains(text17.ToLower()))
							{
								string text31 = ":" + text17.Substring(1);
								if (!text29.Contains(text31.ToLower()))
								{
									text31 = "&" + text17.Substring(1);
									if (!text29.Contains(text31.ToLower()))
									{
										list3.Add(text17);
									}
								}
							}
						}
						foreach (string text17 in list3)
						{
							string text17;
							da.SelectCommand.Parameters.Clear(text17);
						}
						if (text28.CompareTo("sqlserver") == 0)
						{
							SqlConnection selectConnection = new SqlConnection(connectionString);
							SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("", selectConnection);
							sqlDataAdapter.SelectCommand.CommandText = text29;
							sqlDataAdapter.SelectCommand.Parameters.Clear();
							for (int j = 0; j < da.SelectCommand.Parameters.Count; j++)
							{
								sqlDataAdapter.SelectCommand.Parameters.AddWithValue(da.SelectCommand.Parameters.ParameterName(j), da.SelectCommand.Parameters.Value(j));
							}
							try
							{
								sqlDataAdapter.Fill(dataTable3);
							}
							catch (Exception ex3)
							{
								if (!suppressGuiMessages)
								{
									ReportFunction.MessageBoxShow(ref errors, ex3.ToString(), suppressGuiMessages);
								}
								dataTable3 = new DataTable();
								errors.Add(ex3.ToString());
							}
						}
						else if (text28.CompareTo("oledb") == 0)
						{
							OleDbConnection selectConnection2 = new OleDbConnection(connectionString);
							OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("", selectConnection2);
							oleDbDataAdapter.SelectCommand.CommandText = text29;
							oleDbDataAdapter.SelectCommand.Parameters.Clear();
							for (int j = 0; j < da.SelectCommand.Parameters.Count; j++)
							{
								oleDbDataAdapter.SelectCommand.Parameters.AddWithValue(da.SelectCommand.Parameters.ParameterName(j), da.SelectCommand.Parameters.Value(j));
							}
							try
							{
								oleDbDataAdapter.Fill(dataTable3);
							}
							catch (Exception ex4)
							{
								if (!suppressGuiMessages)
								{
									ReportFunction.MessageBoxShow(ex4.ToString());
								}
								dataTable3 = new DataTable();
								errors.Add(ex4.ToString());
							}
						}
						else if (text28.IndexOf("factory") == 0)
						{
							DatabaseLayer instance = DatabaseLayer.GetInstance();
							string providerName = text.Substring(0, num17).Substring(8);
							instance.ProviderName = providerName;
							instance.ConnectionString = connectionString;
							try
							{
								DbParameter[] array24 = new DbParameter[da.SelectCommand.Parameters.Count];
								for (int j = 0; j < da.SelectCommand.Parameters.Count; j++)
								{
									string pName = ":" + da.SelectCommand.Parameters.ParameterName(j).Substring(1);
									array24[j] = instance.GetParameter(pName, da.SelectCommand.Parameters.ParameterDbType(j), da.SelectCommand.Parameters.Value(j));
								}
								dataTable3 = instance.ExecuteQuery(text29, array24);
							}
							catch (DbException ex5)
							{
								if (!suppressGuiMessages)
								{
									ReportFunction.MessageBoxShow(ex5.ToString());
								}
								dataTable3 = new DataTable();
								errors.Add(ex5.ToString());
							}
						}
						else if (text28.CompareTo("odbc") == 0)
						{
							OdbcConnection selectConnection3 = new OdbcConnection(connectionString);
							OdbcDataAdapter odbcDataAdapter = new OdbcDataAdapter("", selectConnection3);
							odbcDataAdapter.SelectCommand.CommandText = text29;
							odbcDataAdapter.SelectCommand.Parameters.Clear();
							for (int j = 0; j < da.SelectCommand.Parameters.Count; j++)
							{
								odbcDataAdapter.SelectCommand.Parameters.AddWithValue(da.SelectCommand.Parameters.ParameterName(j), da.SelectCommand.Parameters.Value(j));
							}
							try
							{
								odbcDataAdapter.Fill(dataTable3);
							}
							catch (Exception ex4)
							{
								if (!suppressGuiMessages)
								{
									ReportFunction.MessageBoxShow(ex4.ToString());
								}
								dataTable3 = new DataTable();
								errors.Add(ex4.ToString());
							}
						}
						else if (text28.CompareTo("odbc2") == 0)
						{
							OdbcConnection selectConnection3 = new OdbcConnection(connectionString);
							OdbcDataAdapter odbcDataAdapter = new OdbcDataAdapter("", selectConnection3);
							odbcDataAdapter.SelectCommand.CommandText = text29;
							odbcDataAdapter.SelectCommand.Parameters.Clear();
							for (int j = 0; j < da.SelectCommand.Parameters.Count; j++)
							{
								if (da.SelectCommand.Parameters.ParameterName(j).CompareTo("@studentno") != 0)
								{
									odbcDataAdapter.SelectCommand.Parameters.AddWithValue(da.SelectCommand.Parameters.ParameterName(j).Replace("@", "&"), da.SelectCommand.Parameters.Value(j));
								}
								else
								{
									odbcDataAdapter.SelectCommand.CommandText = odbcDataAdapter.SelectCommand.CommandText.Replace("@studentno", "'" + da.SelectCommand.Parameters.Value(j).ToString() + "'");
								}
							}
							try
							{
								odbcDataAdapter.Fill(dataTable3);
							}
							catch (Exception ex4)
							{
								if (!suppressGuiMessages)
								{
									ReportFunction.MessageBoxShow(ex4.ToString());
								}
								dataTable3 = new DataTable();
								errors.Add(ex4.ToString());
							}
						}
						report.AddResult(dataTable3.DefaultView);
					}
				}
				break;
			}
			case FunctionCode.Insert_Rows_From_Current_Table_Into_a_Database_Table:
				ReportFunction.InsertRowsIntoADatabaseTable(ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Backup_ClockWork_Database:
				ReportFunction.BackupDatabase(ref report, ref errors, da, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Export_Data:
				ReportFunction.ExportDatabase(ref report, ref errors, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Merge_Rows_by_Removing_Duplicate_Rows:
				ReportFunction.MergeRowsByDroppingDuplicateRows(ref report, ref errors, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Explode_Rows_for_Per_Screen_List_Data:
			{
				string[] array25 = text.Split(System.Environment.NewLine.ToCharArray());
				bool returnLatestDateRowOnly = false;
				if (array25.Length > 0)
				{
					if (array25.Length > 1)
					{
						for (int num18 = 1; num18 < array25.Length; num18++)
						{
							if (array25[num18].Trim().CompareTo("1") == 0)
							{
								returnLatestDateRowOnly = true;
								break;
							}
						}
					}
					ReportFunction.ExplodeListData(ref report, da, currentDataView.Table.Columns.IndexOf(array25[0]), returnLatestDateRowOnly, IncrementSubProgressBar, SetupSubProgressBar);
				}
				break;
			}
			case FunctionCode.Drop_Day_From_Dates_Only_Keep_Month_and_Year:
				ReportFunction.GeneralizeDateToMonth(ref report, text.Split(new char[]
				{
					','
				}), IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Extract_Unique_Students_With_Row_Having_the_Min_Max_Value_In_a_Specific_Column:
			{
				string[] array26 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				bool returnMinimum = array26[0].Trim().ToLower().CompareTo("min") == 0;
				ReportFunction.ExtractUniqueStudentsWithRowHavingTheMinimumValueInASpecificColumn(ref report, returnMinimum, array26[1], IncrementSubProgressBar, SetupSubProgressBar);
				break;
			}
			case FunctionCode.Decrypt_and_Fix_Appointment_Memos:
			{
				string[] array27 = text.Split(new char[]
				{
					'`'
				});
				ReportFunction.DecryptAndFixAppointmentMemos(ref report, tripleDES, array27[0], array27[1], IncrementSubProgressBar, SetupSubProgressBar);
				break;
			}
			case FunctionCode.Cross_Reference_With_Per_Student_Data:
				try
				{
					if (text.Trim().Length > 0)
					{
						string cids;
						string personidColName;
						if (text[0] == 'p')
						{
							int num19 = text.IndexOf(',');
							cids = text.Substring(num19 + 1);
							personidColName = text.Substring(9, num19 - 9);
						}
						else
						{
							cids = text;
							personidColName = "personid";
						}
						Exception ex6;
						ReportFunction.CrossReferenceWithPerStudentData(da, tripleDES, comboBoxData, staffNamesTable, cids, personidColName, ref report, out ex6);
					}
					else
					{
						object variableValue = ReportFunction.GetVariableValue(variables, "perstudentscreenname");
						object variableValue2 = ReportFunction.GetVariableValue(customVariables, "custom_screen10");
						da.SelectCommand.CommandText = string.Concat(new string[]
						{
							"DECLARE @screennum AS int; SET @screennum = (SELECT screennum FROM screens WHERE description='",
							variableValue.ToString(),
							"' AND typecode=0) SELECT controlid INTO #t0 FROM dynamicscreencontrols WHERE screennum=@screennum  AND isactive=1; SELECT \tdsc.screennum,dsc.controlid,dsc.ordernum,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue  FROM\tdynamicscreencontrols dsc LEFT JOIN dynamiccontrols DC ON\tdc.controlid=dsc.controlid WHERE \tdsc.screennum=@screennum  AND dsc.isactive=1 AND NOT dc.controlcode IN (SELECT controlcode FROM dynamicscreennondatacontrols) AND (",
							variableValue2.ToString(),
							");"
						});
						DataTable dataTable9 = new DataTable();
						da.Fill(dataTable9);
						Exception ex6;
						ReportFunction.CrossReferenceWithPerStudentData(da, tripleDES, comboBoxData, staffNamesTable, dataTable9, ref report, out ex6);
					}
				}
				catch (Exception ex7)
				{
					ReportFunction.MessageBoxShow(ref errors, ex7.ToString(), suppressGuiMessages);
				}
				break;
			case FunctionCode.Execute_Function_Against_Memory_Table:
			{
				int num20 = text.IndexOf(",");
				string s3 = text.Substring(0, num20);
				string parameters = text.Substring(num20 + 1);
				ReportFunction.ExecuteFunctionAgainstMemoryTable(da, ref report, int.Parse(s3), "", parameters, technoProReports, tripleDES, comboBoxData, staffNamesTable, sessions, dynamicScreenNonDataControlsTable, lookupTablesForControls, yearStartEnd, whoAmIPersonID);
				break;
			}
			case FunctionCode.Pull_in_Data_Using_Sql:
				ReportFunction.PullInData(da, tripleDES, ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Sort_Attendees_Into_Staff_Facilitator_and_Client_Groups_With_Counts:
				ReportFunction.SortAttendeesIntoStaffFacilatorAndClientGroupsWithCounts(ref report, da, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Import_Students_Courses:
			{
				Variable variable3 = report.Variables.FindVariable(new string[]
				{
					"studentno",
					"student_no",
					"studentnumberencryptdatasync"
				});
				string snum;
				if (variable3 == null || variable3.VariableValue == null)
				{
					snum = null;
				}
				else if (variable3.VariableValue is string)
				{
					snum = (string)variable3.VariableValue;
				}
				else if (variable3.VariableValue is byte[])
				{
					TripleDESEncryptionClass dataSyncTripleDES = ReportFunction.GetDataSyncTripleDES(da, tripleDES);
					if (dataSyncTripleDES != null)
					{
						snum = dataSyncTripleDES.Decrypt((byte[])variable3.VariableValue);
					}
					else
					{
						snum = tripleDES.Decrypt((byte[])variable3.VariableValue);
					}
				}
				else
				{
					snum = variable3.VariableValue.ToString();
				}
				DataView dataView2 = report.GetCurrentDataView();
				ReportFunction.ImportStudentCourses(dataView2, text, IncrementSubProgressBar, SetupSubProgressBar, tripleDES, da, true, snum);
				break;
			}
			case FunctionCode.Split_Strings:
			{
				int num21 = text.IndexOf('`');
				string colName = text.Substring(0, num21);
				string sections = text.Substring(num21 + 1);
				StringInt[] sections2 = StringInt.ParseStringIntArray(sections);
				ReportFunction.SplitStrings(ref report, colName, sections2, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			}
			case FunctionCode.Find_Personids:
				ReportFunction.FindPersonids(ref report, text, da, tripleDES, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Extract_Unique_Rows:
				ReportFunction.ExtractUniqueRows(ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Divide_and_Conquer:
				ReportFunction.BreakdownMultiple(ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Remove_Duplicate_Items_From_Comma_Separated_List:
				ReportFunction.RemoveDuplicateItemsFromListInOneCell(ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Add_Boolean_Count_Across_Columns:
				ReportFunction.AddBooleanCountAcrossColumns(ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Load_All_Active_Students_With_Specific_Data:
			{
				string[] array28 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				string text32 = "";
				foreach (string str in array28)
				{
					if (text32.Length > 0)
					{
						text32 += ",";
					}
					text32 += str;
				}
				report.AddVariable("cids", text32);
				ReportFunction.LoadAllActiveStudentsWithSpecificData(da, tripleDES, ref report, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			}
			case FunctionCode.Breakdown_Checkbox_Counts:
				ReportFunction.BreakdownCheckboxCounts(ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Cross_Reference_With_Accommodations:
				ReportFunction.CrossReferenceWithAccommodations(da, tripleDES, ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Import_from_formatted_text_file:
			{
				DataView dv = ReportFunction.LoadTextFormattedTable(text, IncrementSubProgressBar, SetupSubProgressBar);
				report.AddResult(dv);
				break;
			}
			case FunctionCode.Delete_file:
				File.Delete(text);
				break;
			case FunctionCode.Only_keep_first_row_for_each_group:
				ReportFunction.OnlyKeepFirstRows(ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Execute_command_line:
			{
				string[] array29 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, false);
				ReportFunction.ExecuteCommandLine(ref report, array29[0], (array29.Length > 1) ? array29[1] : "", IncrementSubProgressBar, SetupSubProgressBar);
				break;
			}
			case FunctionCode.Name_a_table:
			{
				int num22 = text.IndexOf(',');
				List<string> list4 = new List<string>();
				string newName;
				if (num22 > 0)
				{
					string[] array30 = text.Split(new char[]
					{
						','
					});
					newName = array30[0];
					for (int num23 = 1; num23 < array30.Length; num23++)
					{
						list4.Add(array30[num23].ToLower());
					}
				}
				else
				{
					newName = text;
				}
				ReportFunction.NameCurrentTable(ref report, newName, list4);
				break;
			}
			case FunctionCode.Add_students_to_master_student_table_in_memory:
			{
				string[] array31 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				ReportFunction.AddStudentsToMasterStudentTableInMemory(ref report, array31[0], array31[1], IncrementSubProgressBar, SetupSubProgressBar);
				break;
			}
			case FunctionCode.Make_a_table_the_current_table:
				ReportFunction.MakeATableTheCurrentTable(ref report, text);
				break;
			case FunctionCode.Write_Table_to_OleDb_Database:
			{
				string[] array31 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				ReportFunction.WriteTableToOleDbDatabase(ref report, array31[0], array31[1], IncrementSubProgressBar, SetupSubProgressBar);
				break;
			}
			case FunctionCode.Write_Data_CUSTOM_DATA:
				ReportFunction.WriteData_CUSTOM_DATA(da, tripleDES, ref report, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Write_Data_CUSTOM_COURSES:
				ReportFunction.WriteData_CUSTOM_COURSES(da, tripleDES, ref report, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Data_Sync_Update_All_Students:
			{
				DataView dataView2 = ReportFunction.ImportUpdateStudents2(report.GetCurrentDataView(), da, tripleDES, suppressGuiMessages);
				report.AddResult(dataView2);
				break;
			}
			case FunctionCode.Consume_Web_Service:
			{
				string[] array31 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				string[] array32;
				if (array31.Length > 3)
				{
					array32 = new string[array31.Length - 3];
					for (int j = 0; j < array32.Length; j++)
					{
						array32[j] = array31[j + 3];
					}
				}
				else
				{
					array32 = new string[0];
				}
				ReportFunction.ConsumeWebService(da, tripleDES, ref report, array31[0], array31[1], array31[2], null, array32, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			}
			case FunctionCode.Import_CSV_File:
			{
				string[] array33 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				string filename = array33[0];
				string text33 = (array33.Length > 1) ? array33[1].Trim() : "";
				bool headers = text33.CompareTo("1") == 0;
				ReportFunction.ImportCsvFile(ref report, filename, headers);
				break;
			}
			case FunctionCode.Split2:
			{
				string[] array34 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				string colName2 = array34[0];
				string splitString = array34[1];
				string[] array35;
				if (array34.Length > 2)
				{
					array35 = new string[array34.Length - 2];
					for (int j = 2; j < array34.Length; j++)
					{
						array35[j - 2] = array34[j];
					}
				}
				else
				{
					array35 = new string[0];
				}
				ReportFunction.Split2(ref report, colName2, splitString, array35, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			}
			case FunctionCode.Date_Add:
			{
				string[] array36 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				ReportFunction.DateAdd(ref report, array36[0], (array36[1].Length > 0) ? array36[1][0] : 'm', array36[2], IncrementSubProgressBar, SetupSubProgressBar);
				break;
			}
			case FunctionCode.If_then_else:
			{
				string[] array37 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				int num24 = array37[0].IndexOf('=');
				int num25 = array37[1].IndexOf('=');
				int num26 = array37[2].IndexOf('=');
				ReportFunction.IfThenElse(ref report, array37[0].Substring(0, num24), array37[0].Substring(num24 + 1).ToLower(), array37[1].Substring(0, num25), array37[1].Substring(num25 + 1), array37[2].Substring(0, num26), array37[2].Substring(num26 + 1), IncrementSubProgressBar, SetupSubProgressBar);
				break;
			}
			case FunctionCode.Copy_Columns:
			{
				string[] colFromNameCommaColToNames = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				ReportFunction.CopyColumns(ref report, IncrementSubProgressBar, SetupSubProgressBar, colFromNameCommaColToNames);
				break;
			}
			case FunctionCode.CustomFunctions_Fanshawe:
			{
				string student_no = "";
				string addressTypeCode_local = "h";
				string addressTypeCode_permanent = "p";
				string programStatusesToIgnore = "";
				XmlNode x = null;
				foreach (object obj9 in variables)
				{
					Variable variable = (Variable)obj9;
					if (variable.VariableName.CompareTo("studentnumberencryptdatasync") == 0)
					{
						student_no = tripleDES.Decrypt((byte[])variable.VariableValue);
					}
					else if (variable.VariableName.CompareTo("studentno") == 0)
					{
						student_no = (string)variable.VariableValue;
					}
					else if (variable.VariableName.CompareTo("addresstypecodelocal") == 0)
					{
						addressTypeCode_local = (string)variable.VariableValue;
					}
					else if (variable.VariableName.CompareTo("addresstypecodeperm") == 0)
					{
						addressTypeCode_permanent = (string)variable.VariableValue;
					}
					else if (variable.VariableName.CompareTo("programStatusesToIgnore") == 0)
					{
						programStatusesToIgnore = (string)variable.VariableValue;
					}
					else if (variable.VariableName.CompareTo("xmlfilename") == 0)
					{
						XmlDocument xmlDocument = new XmlDocument();
						xmlDocument.Load((string)variable.VariableValue);
						x = xmlDocument.FirstChild;
					}
				}
				DateTime currentSemesterStart = ReportFunction.GetCurrentSemesterStart();
				CustomReport.FanshaweGetStudentData(x, ref report, student_no, currentSemesterStart, addressTypeCode_local, addressTypeCode_permanent, programStatusesToIgnore);
				break;
			}
			case FunctionCode.Remove_Rows_By_Comparison_Operator:
				ReportFunction.RemoveRowsByComparison(ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Right:
			{
				string[] array38 = text.Split(new char[]
				{
					'`'
				});
				ReportFunction.RightLeft(ref report, true, array38[0], array38[1], int.Parse(array38[2]), IncrementSubProgressBar, SetupSubProgressBar);
				break;
			}
			case FunctionCode.Left:
			{
				string[] array39 = text.Split(new char[]
				{
					'`'
				});
				ReportFunction.RightLeft(ref report, false, array39[0], array39[1], int.Parse(array39[2]), IncrementSubProgressBar, SetupSubProgressBar);
				break;
			}
			case FunctionCode.Search_and_Replace_Case_INsensitive:
			{
				string[] searchAndReplaceDefinitions = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				ReportFunction.SearchAndReplaceCaseInsensitive(ref report, IncrementSubProgressBar, SetupSubProgressBar, searchAndReplaceDefinitions);
				break;
			}
			case FunctionCode.Course_Calculate_Start_End_Dates:
				ReportFunction.FigureOutCourseStartEndDates(ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Only_Keep_Rows_Where_a_Column_has_a_matching_value:
			{
				string[] array40 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				string[] array41 = new string[array40.Length - 1];
				for (int l = 1; l < array40.Length; l++)
				{
					array41[l - 1] = array40[l].ToLower().Trim();
				}
				ReportFunction.OnlyKeepRowsWhereASpecificColumnMatchesOneOfASetOfValues(ref report, array40[0], array41, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			}
			case FunctionCode.Date_fix:
			{
				string[] array42 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				ReportFunction.DateFix(ref report, array42[0], array42[1], IncrementSubProgressBar, SetupSubProgressBar);
				break;
			}
			case FunctionCode.Rows_to_columns_DynamicScreenFormat_for_per_appointment_data:
			{
				DataTable dataTable10 = Reports.FixPerAppData(report.GetCurrentDataView(), da, tripleDES);
				DataView dataView2 = dataTable10.DefaultView;
				report.AddResult(dataView2);
				break;
			}
			case FunctionCode.Run_Custom_Function:
				ReportFunction.RunCustomFunction(ref report, da, tripleDES, text);
				break;
			case FunctionCode.CustomFunctions_Fanshawe_Changed:
			{
				XmlNode x2 = null;
				foreach (object obj10 in variables)
				{
					Variable variable = (Variable)obj10;
					if (variable.VariableName.CompareTo("xmlfilename") == 0)
					{
						XmlDocument xmlDocument = new XmlDocument();
						xmlDocument.Load((string)variable.VariableValue);
						x2 = xmlDocument.FirstChild;
					}
				}
				CustomReport.FanshaweGetChangedStudentData(x2, ref report);
				break;
			}
			case FunctionCode.Remove_Non_ClockWork_Students:
				ReportFunction.RemoveNonClockWorkStudents(text.Trim(), ref report, da, tripleDES, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Cross_reference_per_app_data2:
				report.AddResult(ReportFunction.CrossReferencePerAppointmentData(da, tripleDES, report.GetCurrentDataView().Table, text, ref comboBoxData, staffNamesTable).DefaultView);
				break;
			case FunctionCode.Remove_Rows:
			{
				string[] array43 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				bool min = array43.Length > 2 && "1yesYestrueTrue".IndexOf(array43[2]) >= 0;
				ReportFunction.RemoveRows(array43[0], array43[1], min, ref report, da, tripleDES, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			}
			case FunctionCode.Convert_Timetable_to_ClockWork_Timetable:
				if (text.Equals(""))
				{
					ReportFunction.ConvertTimetableToClockWorkTimetable(report);
				}
				else
				{
					string[] array44 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
					ReportFunction.ConvertTimetableToClockWorkTimetable(array44[0], array44[1], array44[2], array44[3], array44[4], ref report, IncrementSubProgressBar, SetupSubProgressBar);
				}
				break;
			case FunctionCode.Freeze_Table:
			{
				DataView currentDataView3 = report.GetCurrentDataView();
				if (currentDataView3 != null)
				{
					string[] array45 = text.Split(new char[]
					{
						','
					});
					foreach (string name2 in array45)
					{
						DataTable table = currentDataView3.Table.Copy();
						DataView dataView3 = new DataView(table);
						dataView3.Sort = currentDataView3.Sort;
						report.AddResultNotPrimary(dataView3, name2);
					}
				}
				break;
			}
			case FunctionCode.Merge_Primary_and_Secondary_Columns:
				ReportFunction.MergePrimaryAndSecondaryColumns(report, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Execute_Script:
			case FunctionCode.Execute_Script_2:
			{
				int num27 = text.IndexOf("using migration;");
				if (num27 == 0)
				{
					ReportFunction.ExecuteScriptMigration(report, text.Substring(16), IncrementSubProgressBar, SetupSubProgressBar, da, tripleDES, suppressGuiMessages);
				}
				else
				{
					Exception ex8 = ReportFunction.ExecuteScript2(report, text, IncrementSubProgressBar, SetupSubProgressBar, da, tripleDES, suppressGuiMessages);
					if (ex8 != null)
					{
						errors.Add(ex8.ToString());
					}
				}
				break;
			}
			case FunctionCode.Combine_Boolean_Columns:
			{
				string[] array46 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				ReportFunction.MergeBooleanColumns(report, array46[0], array46[1], array46[2].ToLower(), IncrementSubProgressBar, SetupSubProgressBar);
				break;
			}
			case FunctionCode.Import_CSV_File_Directly_to_ClockWork_Table:
			case FunctionCode.Import_Tab_Delimitered_Directly_to_ClockWork_Table:
			{
				string[] array47 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				string filename2 = array47[0];
				string text34 = array47[1];
				string text35 = array47[2];
				string tableName = array47[3];
				int[] array48;
				if (text35.CompareTo("all") == 0)
				{
					array48 = null;
				}
				else if (text35.Equals("."))
				{
					array48 = new int[0];
				}
				else
				{
					string[] array49 = text35.Split(new char[]
					{
						','
					});
					array48 = new int[array49.Length];
					for (int j = 0; j < array48.Length; j++)
					{
						array48[j] = int.Parse(array49[j]);
					}
				}
				if (functionCode == FunctionCode.Import_CSV_File_Directly_to_ClockWork_Table)
				{
					ReportFunction.ImportCSVDirectlyIntoClockWorkTable(report, da, tripleDES, filename2, text34.Trim().CompareTo("1") == 0, array48, tableName, IncrementSubProgressBar, SetupSubProgressBar);
				}
				else
				{
					string text36 = (array47.Length > 4) ? array47[4].Trim() : "";
					char delimiter2 = (text36.Length > 0) ? text36[0] : '\t';
					ReportFunction.Import_Tab_Delimitered_Directly_to_ClockWork_Table(report, da, tripleDES, filename2, text34.Trim().CompareTo("1") == 0, array48, tableName, IncrementSubProgressBar, SetupSubProgressBar, delimiter2);
				}
				break;
			}
			case FunctionCode.Hide_Columns:
				ReportFunction.HideColumns(report, text);
				break;
			case FunctionCode.Filter_Rows:
				ReportFunction.SetRowFilter(report, text);
				break;
			case FunctionCode.Decode_Dynamic_Data:
			{
				DataView currentDataView4 = report.GetCurrentDataView();
				DataView dv2 = ReportFunction.DecodeDynamicData(da, tripleDES, (currentDataView4 == null) ? null : currentDataView4.Table, text.Split(new char[]
				{
					','
				}));
				report.AddResult(dv2);
				break;
			}
			case FunctionCode.Export_to_xml:
				ReportFunction.ExportToXml(ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Decrypt_Dynamic_Data:
				ReportFunction.DecryptDynamicData(ref report, tripleDES);
				break;
			case FunctionCode.Export_to_csv:
			{
				string text37 = text;
				TemplatesClass.ExportToDelimeteredText(report.GetCurrentDataView(), text37, ReportFunction.GetStartDirectory(), false, ",", System.Environment.NewLine);
				break;
			}
			case FunctionCode.Cross_Reference_With_Accommodations2:
				ReportFunction.CrossReferenceWithAccommodations2(da, tripleDES, ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Decrypt_and_fix_dynamic_data:
				ReportFunction.DecryptDynamicData(ref report, tripleDES);
				ReportFunction.MergeRows(ref report, text);
				break;
			case FunctionCode.Batch_Email_with_Mail_Merge_3:
				ReportFunction.BatchEmailWithMailMerge3(da, tripleDES, ref report, text, IncrementSubProgressBar, SetupSubProgressBar);
				break;
			case FunctionCode.Data_Sync_Courses_2:
			{
				DataView currentDataView5 = report.GetCurrentDataView();
				if (currentDataView5 != null && currentDataView5.Table != null && currentDataView5.Table.Rows.Count > 0)
				{
					string text38 = currentDataView5.Table.Rows[0]["student_no"].ToString().Trim().ToUpper();
					if (text38.Length > 0)
					{
						IDataSyncCourseManager dataSyncCourseManager = new DataSyncCourseManager(new OperationContext
						{
							WhoAmI = whoAmIPersonID
						});
						DataSyncCourseDAO dataSyncCourseDAO = new DataSyncCourseDAO();
						List<DataSyncExternalCourseRowPart> rowPartsFromDataTable = dataSyncCourseDAO.GetRowPartsFromDataTable(currentDataView5.Table);
						List<TechnoPro.Common.Public.Entities.DataSync.DataSyncExternalCourse> allExternalCourses = dataSyncCourseManager.ParseExternalCourseRowParts(rowPartsFromDataTable);
						List<DataSyncExternalCourseSyncResult> list5 = dataSyncCourseManager.DataSyncCourses(text38, allExternalCourses);
						DataTable dataTable11 = new DataTable();
						dataTable11.TableName = "Results";
						dataTable11.Columns.Add("msg");
						foreach (DataSyncExternalCourseSyncResult result in list5)
						{
							dataTable11.Rows.Add(new object[]
							{
								ReportFunction.GetResultString(result)
							});
						}
						report.AddResult(dataTable11.DefaultView);
					}
				}
				break;
			}
			case FunctionCode.Data_Sync_Service_Provider_Data:
			{
				DataView currentDataView6;
				if (report != null && (currentDataView6 = report.GetCurrentDataView()) != null && currentDataView6.Table != null)
				{
					ServiceProviderDataSync serviceProviderDataSync = new ServiceProviderDataSync();
					List<ServiceProviderDataSyncDataItemAction> actions = serviceProviderDataSync.DataSyncServiceProviderData(currentDataView6.Table);
					DataTable dataTable12 = ReportFunctions.ClockWorkDataSync.Utility.TableFromActions(actions);
					report.AddResult(dataTable12.DefaultView);
				}
				break;
			}
			case FunctionCode.Data_Sync_Service_Provider_Courses:
			{
				DataView currentDataView7;
				if (report != null && (currentDataView7 = report.GetCurrentDataView()) != null && currentDataView7.Table != null)
				{
					ServiceProviderDataSyncCourses serviceProviderDataSyncCourses = new ServiceProviderDataSyncCourses();
					List<DataSyncCourseAction> actions2 = serviceProviderDataSyncCourses.DataSyncCoursesServiceProvider(currentDataView7.Table);
					DataTable dataTable13 = ReportFunctions.ClockWorkDataSync.Utility.TableFromActions(actions2);
					report.AddResult(dataTable13.DefaultView);
				}
				break;
			}
			case FunctionCode.Execute_Basic_Oracle_Query:
			{
				string[] array50 = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
				DatabaseLayer databaseLayer = new DatabaseLayer();
				databaseLayer.ProviderName = ProviderNames.OracleClient;
				databaseLayer.ConnectionString = array50[0];
				string text39 = array50[1];
				if (dataTable != null && dataTable.Rows.Count > 0)
				{
					foreach (object obj11 in dataTable.Columns)
					{
						DataColumn dataColumn2 = (DataColumn)obj11;
						string text40 = "@" + dataColumn2.ColumnName;
						if (text39.IndexOf(text40, StringComparison.OrdinalIgnoreCase) >= 0)
						{
							text39 = text39.Replace(text40, dataTable.Rows[0][dataColumn2].ToString());
						}
					}
				}
				DataTable dataTable14 = databaseLayer.ExecuteQuery(text39);
				report.AddResult(dataTable14.DefaultView);
				break;
			}
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000F454 File Offset: 0x0000E454
		private static string GetResultString(DataSyncExternalCourseSyncResult result)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (result.CourseRegistrationAction != eDataSyncCourseRegistrationAction.eNoChange)
			{
				stringBuilder.AppendFormat("COURSE REG CHANGE: {0}: ", Enum.GetName(typeof(eDataSyncCourseRegistrationAction), result.CourseRegistrationAction));
			}
			else if (result.LookupCourseAction != eDataSyncCourseLookupCourseAction.eNoChange)
			{
				stringBuilder.AppendFormat("LOOKUP COURSE CHANGE: {0}: ", Enum.GetName(typeof(eDataSyncCourseLookupCourseAction), result.LookupCourseAction));
			}
			else if (result.InstructorAction != eDataSyncCourseInstructorAction.eNoChange)
			{
				stringBuilder.AppendFormat("INSTRUCTOR CHANGE: {0}: ", Enum.GetName(typeof(eDataSyncCourseInstructorAction), result.InstructorAction));
			}
			else if (result.MiscAction != eDataSyncCourseMiscAction.eNoAction)
			{
				stringBuilder.AppendFormat("MISC CHANGE: {0}: ", Enum.GetName(typeof(eDataSyncCourseMiscAction), result.MiscAction));
			}
			else if (result.ErrorAction != eDataSyncCourseError.eNoError)
			{
				stringBuilder.AppendFormat("ERROR CHANGE: {0}: ", Enum.GetName(typeof(eDataSyncCourseError), result.ErrorAction));
			}
			else
			{
				stringBuilder.Append("UNKNOWN ACTION");
			}
			stringBuilder.AppendFormat(" [lucid={0},iid={1},ext={2}]", result.Lucid.ToString(), result.InstructorId.ToString(), (result.ExternalCourse == null) ? "NULL" : string.Format("{0} {1} {2} {3} {4}", new object[]
			{
				result.ExternalCourse.Term,
				result.ExternalCourse.Subject,
				result.ExternalCourse.Course,
				result.ExternalCourse.Section,
				result.ExternalCourse.TimeOfDay
			}));
			return stringBuilder.ToString();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000F630 File Offset: 0x0000E630
		public static void MergeRows(ref Report report, string uniqueColumns)
		{
			string[] array = uniqueColumns.Split(new char[]
			{
				','
			});
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			table.DefaultView.Sort = uniqueColumns;
			DataTable dataTable = table.Clone();
			List<object[]> list = new List<object[]>();
			int k;
			for (int i = 0; i < table.DefaultView.Count; i = k)
			{
				DataRow row = table.DefaultView[i].Row;
				string[] array2 = new string[array.Length];
				for (int j = 0; j < array.Length; j++)
				{
					array2[j] = row[array[j]].ToString();
				}
				k = i;
				StringDictionary stringDictionary = new StringDictionary();
				while (k < table.DefaultView.Count)
				{
					DataRow row2 = table.DefaultView[k].Row;
					bool flag = true;
					for (int j = 0; j < array.Length; j++)
					{
						string text = row2[array[j]].ToString();
						if (!text.Equals(array2[j]))
						{
							flag = false;
							break;
						}
					}
					if (!flag)
					{
						break;
					}
					string text2 = row2["controlcaption"].ToString();
					if (!string.IsNullOrEmpty(text2))
					{
						text2 = ReportFunction.GetUniqueColName2(dataTable, text2);
						stringDictionary.Add(text2, row2["valtext"].ToString());
					}
					k++;
				}
				string[] array3 = new string[stringDictionary.Keys.Count];
				stringDictionary.Keys.CopyTo(array3, 0);
				foreach (string text3 in array3)
				{
					if (!dataTable.Columns.Contains(text3))
					{
						dataTable.Columns.Add(text3);
					}
				}
				object[] array5 = new object[dataTable.Columns.Count];
				for (int j = 0; j < table.Columns.Count; j++)
				{
					array5[j] = row[j];
				}
				foreach (string text3 in array3)
				{
					int num = dataTable.Columns.IndexOf(text3);
					array5[num] = stringDictionary[text3];
				}
				list.Add(array5);
			}
			int count = dataTable.Columns.Count;
			foreach (object[] array5 in list)
			{
				DataRow dataRow = dataTable.NewRow();
				object[] array5;
				for (int j = 0; j < array5.Length; j++)
				{
					dataRow[j] = array5[j];
				}
				dataTable.Rows.Add(dataRow);
			}
			string[] array6 = new string[]
			{
				"setting1",
				"setting2",
				"setting3",
				"setting4",
				"controlcaption",
				"valtext",
				"valint",
				"valdate",
				"valimage",
				"defaultvalue",
				"controlcode",
				"dataid"
			};
			foreach (string name in array6)
			{
				if (dataTable.Columns.Contains(name))
				{
					dataTable.Columns.Remove(name);
				}
			}
			dataTable.DefaultView.Sort = uniqueColumns;
			report.AddResult(dataTable.DefaultView);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000FA3C File Offset: 0x0000EA3C
		public static void ExportToXml(ref Report report, string filename, IncrementProgressBar ipb, SetupProgressBar sbp)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable dataTable = currentDataView.Table.Clone();
			foreach (object obj in currentDataView)
			{
				DataRowView dataRowView = (DataRowView)obj;
				dataTable.ImportRow(dataRowView.Row);
			}
			dataTable.TableName = "item";
			DataSet dataSet = new DataSet("DataSet");
			dataSet.Tables.Add(dataTable);
			if (File.Exists(filename))
			{
				File.Delete(filename);
			}
			XmlTextWriter writer = new XmlTextWriter(filename, new ASCIIEncoding());
			dataSet.WriteXml(writer, XmlWriteMode.WriteSchema);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000FB14 File Offset: 0x0000EB14
		public static void SetRowFilter(Report report, string filter)
		{
			DataView currentDataView = report.GetCurrentDataView();
			if (currentDataView != null)
			{
				currentDataView.RowFilter = filter;
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000FB3C File Offset: 0x0000EB3C
		public static void UnhideAllColumns(DataTable t)
		{
			foreach (object obj in t.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (dataColumn.ColumnMapping == MappingType.Hidden)
				{
					dataColumn.ColumnMapping = MappingType.Element;
				}
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000FBB4 File Offset: 0x0000EBB4
		private static TripleDESEncryptionClass GetDataSyncTripleDES(UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			da.SelectCommand.CommandText = "SELECT settingstringvalue FROM settingsgroups WHERE settingcode=407";
			da.SelectCommand.Parameters.Clear();
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			TripleDESEncryptionClass result;
			if (dataTable.Rows.Count > 0)
			{
				byte[] inputInBytes = ClockWorkCore.base64Decode(dataTable.Rows[0][0].ToString());
				string password = tripleDES.Decrypt(inputInBytes);
				byte[][] bytes = TripleDESEncryptionClass.GetBytes(true, password);
				da.SelectCommand.CommandText = "SELECT misccode FROM misc WHERE misccode=1";
				da.SelectCommand.Parameters.Clear();
				dataTable = new DataTable();
				da.Fill(dataTable);
				if (dataTable.Rows.Count > 0)
				{
					result = new TripleDESEncryptionClass(EncryptionType.TripleDES_128bit, bytes[0], bytes[1]);
				}
				else
				{
					result = new TripleDESEncryptionClass(EncryptionType.TripleDES_192bit, bytes[0], bytes[1]);
				}
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000FCAC File Offset: 0x0000ECAC
		public static void HideColumns(Report report, string colNames)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			string[] array = colNames.Split(new char[]
			{
				','
			});
			foreach (string name in array)
			{
				if (table.Columns.Contains(name))
				{
					table.Columns[name].ColumnMapping = MappingType.Hidden;
				}
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000FD30 File Offset: 0x0000ED30
		private static bool ClearTable(string tableName, UnivDataAdapter da)
		{
			da.SelectCommand.CommandText = "TRUNCATE TABLE " + tableName;
			da.SelectCommand.Parameters.Clear();
			try
			{
				da.Fill(new DataTable());
			}
			catch
			{
			}
			da.SelectCommand.CommandText = "SELECT COUNT(*) FROM " + tableName;
			da.SelectCommand.Parameters.Clear();
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			if (dataTable.Rows.Count < 1 || (int)dataTable.Rows[0][0] > 0)
			{
				da.SelectCommand.CommandText = "DELETE FROM " + tableName;
				da.SelectCommand.Parameters.Clear();
				try
				{
					da.Fill(new DataTable());
				}
				catch
				{
				}
			}
			da.SelectCommand.CommandText = "SELECT COUNT(*) FROM " + tableName;
			da.SelectCommand.Parameters.Clear();
			dataTable = new DataTable();
			da.Fill(dataTable);
			return dataTable.Rows.Count >= 1 && (int)dataTable.Rows[0][0] <= 0;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000FEB4 File Offset: 0x0000EEB4
		public static int Import_Tab_Delimitered_Directly_to_ClockWork_Table2(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, string filename, bool headersInFirstRow, string ClockWorkTableName, params int[] encryptedColumnIndices)
		{
			int result;
			if (!ClockWorkTableName.StartsWith("custom_", StringComparison.OrdinalIgnoreCase))
			{
				result = 0;
			}
			else
			{
				da.SelectCommand.CommandText = "SELECT * FROM " + ClockWorkTableName + " WHERE 1=0";
				da.SelectCommand.Parameters.Clear();
				DataTable dataTable = new DataTable();
				da.Fill(dataTable);
				List<string> list = new List<string>();
				List<int> list2;
				bool flag;
				if (encryptedColumnIndices == null || encryptedColumnIndices.Length < 1)
				{
					list2 = new List<int>();
					flag = true;
				}
				else
				{
					list2 = new List<int>(encryptedColumnIndices);
					flag = false;
				}
				int num = 0;
				foreach (object obj in dataTable.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					list.Add(dataColumn.ColumnName);
					if (flag)
					{
						if (dataColumn.DataType == typeof(byte[]))
						{
							list2.Add(num);
						}
					}
					num++;
				}
				if (!ReportFunction.ClearTable(ClockWorkTableName, da))
				{
					result = 0;
				}
				else
				{
					List<string> list3 = new List<string>();
					int? num2 = null;
					string commandText = "";
					int num3 = 0;
					using (StreamReader streamReader = new StreamReader(filename))
					{
						if (headersInFirstRow)
						{
							string text = streamReader.ReadLine();
							if (!string.IsNullOrEmpty(text))
							{
								string[] array = text.Split(new char[]
								{
									'\t'
								});
								int num4 = array.Length;
								if (num4 > 0)
								{
									num2 = new int?(num4);
									for (int num5 = 0; num5 < num2; num5++)
									{
										list3.Add(list[num5]);
									}
								}
							}
						}
						string text2;
						while ((text2 = streamReader.ReadLine()) != null)
						{
							string[] array2 = text2.Split(new char[]
							{
								'\t'
							});
							int num6 = array2.Length;
							if (num2 == null)
							{
								num2 = new int?(num6);
								for (int num5 = 0; num5 < num2; num5++)
								{
									list3.Add(list[num5]);
								}
								string text3 = "";
								foreach (string str in list3)
								{
									if (text3.Length > 0)
									{
										text3 += ", ";
									}
									text3 = text3 + "@" + str;
								}
								commandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", ClockWorkTableName, string.Join(",", list3.ToArray()), text3);
							}
							if (num6 > 0)
							{
								da.SelectCommand.CommandText = commandText;
								da.SelectCommand.Parameters.Clear();
								for (int i = 0; i < list3.Count; i++)
								{
									string str2 = list3[i];
									if (list2.Contains(i))
									{
										if (i >= array2.Length)
										{
											da.SelectCommand.Parameters.Add("@" + str2, tripleDES.Encrypt(""));
										}
										else
										{
											da.SelectCommand.Parameters.Add("@" + str2, tripleDES.Encrypt(array2[i] ?? ""));
										}
									}
									else if (i >= array2.Length)
									{
										da.SelectCommand.Parameters.Add("@" + str2, "");
									}
									else
									{
										da.SelectCommand.Parameters.Add("@" + str2, array2[i] ?? "");
									}
								}
								da.Fill(new DataTable());
							}
						}
					}
					result = num3;
				}
			}
			return result;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000103A4 File Offset: 0x0000F3A4
		public static void Import_Tab_Delimitered_Directly_to_ClockWork_Table(Report report, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, string filename, bool headersInFirstRow, int[] encryptedIndices, string tableName, IncrementProgressBar ipb, SetupProgressBar sbp)
		{
			char delimiter = '\t';
			ReportFunction.Import_Tab_Delimitered_Directly_to_ClockWork_Table(report, da, tripleDES, filename, headersInFirstRow, encryptedIndices, tableName, ipb, sbp, delimiter);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000103CC File Offset: 0x0000F3CC
		private static string GetNextColumnHeader(DataTable table)
		{
			int num = 1;
			string text;
			do
			{
				text = "Column" + num++;
			}
			while (table.Columns.Contains(text));
			return text;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00010410 File Offset: 0x0000F410
		public static DataTable ParseTabDelimiteredToClockWorkTable(TextReader stream, bool headers, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, string tableName, int[] colIndicesToDecrypt, char delimiter)
		{
			DataTable dataTable = new DataTable();
			string text = stream.ReadLine();
			string[] array = text.Split(new char[]
			{
				delimiter
			});
			if (headers)
			{
				foreach (string text2 in array)
				{
					if (text2 != null && text2.Length > 0 && !dataTable.Columns.Contains(text2))
					{
						dataTable.Columns.Add(text2, typeof(string));
					}
					else
					{
						dataTable.Columns.Add(ReportFunction.GetNextColumnHeader(dataTable), typeof(string));
					}
				}
				text = stream.ReadLine();
				if (text != null)
				{
					array = text.Split(new char[]
					{
						delimiter
					});
				}
			}
			else
			{
				while (array.Length > dataTable.Columns.Count)
				{
					dataTable.Columns.Add(ReportFunction.GetNextColumnHeader(dataTable), typeof(string));
				}
			}
			da.SelectCommand.CommandText = "SELECT * FROM " + tableName + " WHERE 1=0";
			DataTable dataTable2 = new DataTable();
			da.Fill(dataTable2);
			da.SelectCommand.CommandText = "TRUNCATE " + tableName;
			da.Fill(new DataTable());
			da.SelectCommand.CommandText = "SELECT COUNT(*) FROM " + tableName;
			DataTable dataTable3 = new DataTable();
			da.Fill(dataTable3);
			if (dataTable3.Rows.Count > 0 && (int)dataTable3.Rows[0][0] > 0)
			{
				da.SelectCommand.CommandText = "DELETE FROM " + tableName;
				da.Fill(new DataTable());
			}
			da.SelectCommand.CommandText = "SELECT COUNT(*) FROM " + tableName;
			dataTable3 = new DataTable();
			da.Fill(dataTable3);
			DataTable result;
			if (dataTable3.Rows.Count > 0 && (int)dataTable3.Rows[0][0] > 0)
			{
				result = dataTable;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("INSERT INTO ");
				stringBuilder.Append(tableName);
				stringBuilder.Append(" (");
				for (int j = 0; j < dataTable2.Columns.Count; j++)
				{
					if (j > 0)
					{
						stringBuilder.Append(",[");
					}
					else
					{
						stringBuilder.Append("[");
					}
					stringBuilder.Append(dataTable2.Columns[j].ColumnName);
					stringBuilder.Append("]");
				}
				stringBuilder.Append(") VALUES (");
				int count = dataTable2.Columns.Count;
				for (int j = 0; j < count; j++)
				{
					if (j > 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append("@p");
					stringBuilder.Append(j.ToString());
				}
				stringBuilder.Append(")");
				string sql = stringBuilder.ToString();
				UnivTransaction univTransaction = null;
				try
				{
					da.Connection.Open();
					univTransaction = da.Connection.BeginTransaction();
					object[] oo = null;
					while (text != null)
					{
						array = text.Split(new char[]
						{
							delimiter
						});
						using (UnivCommand univCommand = da.CreateCommand(sql))
						{
							univCommand.Transaction = univTransaction;
							for (int j = 0; j < count; j++)
							{
								string parameterName = "@p" + j.ToString();
								if (j < array.Length)
								{
									if (colIndicesToDecrypt == null || Array.IndexOf<int>(colIndicesToDecrypt, j) >= 0)
									{
										string text3 = array[j].Trim();
										if (text3.Length > 0)
										{
											byte[] parameterValue;
											oo = tripleDES.EncryptBatch(out parameterValue, array[j], oo);
											univCommand.Parameters.Add(parameterName, parameterValue);
										}
										else
										{
											univCommand.Parameters.Add(parameterName, new byte[0]);
										}
									}
									else
									{
										univCommand.Parameters.Add(parameterName, array[j]);
									}
								}
								else if (colIndicesToDecrypt == null || Array.IndexOf<int>(colIndicesToDecrypt, j) >= 0)
								{
									univCommand.Parameters.Add(parameterName, new byte[0]);
								}
								else
								{
									univCommand.Parameters.Add(parameterName, "");
								}
							}
							univCommand.ExecuteNonQuery2();
						}
						text = stream.ReadLine();
					}
					univTransaction.Commit();
				}
				catch (Exception ex)
				{
					if (univTransaction != null)
					{
						univTransaction.Rollback();
					}
				}
				finally
				{
					da.Connection.Close();
				}
				result = dataTable;
			}
			return result;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000109BC File Offset: 0x0000F9BC
		public static void Import_Tab_Delimitered_Directly_to_ClockWork_Table(Report report, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, string filename, bool headersInFirstRow, int[] encryptedIndices, string tableName, IncrementProgressBar ipb, SetupProgressBar sbp, char delimiter)
		{
			string text = tableName.ToLower().Trim();
			if (text.ToLower().IndexOf("custom_") != 0)
			{
				throw new Exception("Unsupported table name: " + tableName);
			}
			TripleDESEncryptionClass tripleDES2 = ReportFunction.CreateTripleDES(da, "", "#<407>#", tripleDES);
			TextReader stream = new StreamReader(filename, Encoding.Default);
			DataTable dataTable = ReportFunction.ParseTabDelimiteredToClockWorkTable(stream, headersInFirstRow, da, tripleDES2, tableName, encryptedIndices, delimiter);
			report.AddResult(dataTable.DefaultView);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00010A44 File Offset: 0x0000FA44
		public static DataTable ParseToClockWorkTable(TextReader stream, bool headers, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, string tableName, int[] colIndicesToDecrypt)
		{
			DataTable dataTable = new DataTable();
			CsvStream csvStream = new CsvStream(stream);
			string[] nextRow = csvStream.GetNextRow();
			DataTable result;
			if (nextRow == null)
			{
				result = null;
			}
			else
			{
				if (headers)
				{
					foreach (string text in nextRow)
					{
						if (text != null && text.Length > 0 && !dataTable.Columns.Contains(text))
						{
							dataTable.Columns.Add(text, typeof(string));
						}
						else
						{
							dataTable.Columns.Add(ReportFunction.GetNextColumnHeader(dataTable), typeof(string));
						}
					}
					nextRow = csvStream.GetNextRow();
				}
				else
				{
					while (nextRow.Length > dataTable.Columns.Count)
					{
						dataTable.Columns.Add(ReportFunction.GetNextColumnHeader(dataTable), typeof(string));
					}
				}
				da.SelectCommand.CommandText = "SELECT * FROM " + tableName + " WHERE 1=0";
				DataTable dataTable2 = new DataTable();
				da.Fill(dataTable2);
				da.SelectCommand.CommandText = "TRUNCATE TABLE " + tableName;
				da.Fill(new DataTable());
				da.SelectCommand.CommandText = "SELECT COUNT(*) FROM " + tableName;
				DataTable dataTable3 = new DataTable();
				da.Fill(dataTable3);
				if (dataTable3.Rows.Count > 0 && (int)dataTable3.Rows[0][0] > 0)
				{
					da.SelectCommand.CommandText = "DELETE FROM " + tableName;
					da.Fill(new DataTable());
				}
				da.SelectCommand.CommandText = "SELECT COUNT(*) FROM " + tableName;
				dataTable3 = new DataTable();
				da.Fill(dataTable3);
				if (dataTable3.Rows.Count > 0 && (int)dataTable3.Rows[0][0] > 0)
				{
					result = dataTable;
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("INSERT INTO ");
					stringBuilder.Append(tableName);
					stringBuilder.Append(" (");
					for (int j = 0; j < dataTable2.Columns.Count; j++)
					{
						if (j > 0)
						{
							stringBuilder.Append(",[");
						}
						else
						{
							stringBuilder.Append("[");
						}
						stringBuilder.Append(dataTable2.Columns[j].ColumnName);
						stringBuilder.Append("]");
					}
					stringBuilder.Append(") VALUES (");
					int count = dataTable2.Columns.Count;
					for (int j = 0; j < count; j++)
					{
						if (j > 0)
						{
							stringBuilder.Append(",");
						}
						stringBuilder.Append("@p");
						stringBuilder.Append(j.ToString());
					}
					stringBuilder.Append(")");
					string commandText = stringBuilder.ToString();
					try
					{
						da.Connection.Open();
						da.SelectCommand.CommandText = commandText;
						object[] oo = null;
						int num = 0;
						while (nextRow != null && num++ < 350000000)
						{
							UnivCommand selectCommand = da.SelectCommand;
							selectCommand.Parameters.Clear();
							for (int j = 0; j < count; j++)
							{
								string parameterName = "@p" + j.ToString();
								if (j < nextRow.Length)
								{
									if (colIndicesToDecrypt == null || Array.IndexOf<int>(colIndicesToDecrypt, j) >= 0)
									{
										string text2 = nextRow[j].Trim();
										if (text2.Length > 0)
										{
											byte[] parameterValue;
											oo = tripleDES.EncryptBatch(out parameterValue, nextRow[j], oo);
											selectCommand.Parameters.Add(parameterName, parameterValue);
										}
										else
										{
											selectCommand.Parameters.Add(parameterName, new byte[0]);
										}
									}
									else
									{
										selectCommand.Parameters.Add(parameterName, nextRow[j]);
									}
								}
								else if (colIndicesToDecrypt != null && Array.IndexOf<int>(colIndicesToDecrypt, j) >= 0)
								{
									selectCommand.Parameters.Add(parameterName, new byte[0]);
								}
								else
								{
									selectCommand.Parameters.Add(parameterName, "");
								}
							}
							selectCommand.ExecuteNonQuery2();
							nextRow = csvStream.GetNextRow();
						}
					}
					catch (Exception ex)
					{
					}
					finally
					{
						try
						{
							da.Connection.Close();
						}
						catch
						{
						}
					}
					result = dataTable;
				}
			}
			return result;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00010FA0 File Offset: 0x0000FFA0
		public static void ImportCSVDirectlyIntoClockWorkTable(Report report, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, string filename, bool headersInFirstRow, int[] encryptedIndices, string tableName, IncrementProgressBar ipb, SetupProgressBar sbp)
		{
			string text = tableName.ToLower().Trim();
			if (text.ToLower().IndexOf("custom_") != 0)
			{
				throw new Exception("Unsupported table name: " + tableName);
			}
			TripleDESEncryptionClass tripleDES2 = ReportFunction.CreateTripleDES(da, "", "#<407>#", tripleDES);
			TextReader stream = new StreamReader(filename, Encoding.Default);
			DataTable dataTable = ReportFunction.ParseToClockWorkTable(stream, headersInFirstRow, da, tripleDES2, tableName, encryptedIndices);
			report.AddResult(dataTable.DefaultView);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00011024 File Offset: 0x00010024
		public static void MergeBooleanColumns(Report report, string colNames, string newColName, string booleanOperator, IncrementProgressBar ipb, SetupProgressBar sbp)
		{
			DataView currentDataView = report.GetCurrentDataView();
			currentDataView.Table.Columns.Add(newColName, typeof(bool));
			string[] array = colNames.Split(new char[]
			{
				','
			});
			ReportFunction.GenericRowLoopAction rowAction = new ReportFunction.GenericRowLoopAction(ReportFunction.MergeBooleanColumns);
			ReportFunction.GenericRowLooper(report, currentDataView, ipb, sbp, rowAction, new object[]
			{
				array,
				newColName,
				booleanOperator
			});
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0001109C File Offset: 0x0001009C
		private static void MergeBooleanColumns(DataRow dr, params object[] oo)
		{
			string[] array = (string[])oo[0];
			string columnName = (string)oo[1];
			string text = (string)oo[2];
			DataTable table = dr.Table;
			foreach (string text2 in array)
			{
				if (!table.Columns.Contains(text2))
				{
					table.Columns.Add(text2, typeof(bool));
				}
			}
			bool flag = false;
			if (text.CompareTo("and") == 0)
			{
				foreach (string text2 in array)
				{
					if (dr[text2] == DBNull.Value || ((!(dr[text2] is bool) || !Convert.ToBoolean(dr[text2])) && "yestrue1".IndexOf(dr[text2].ToString().ToLower().Trim()) < 0))
					{
						flag = false;
						break;
					}
					flag = true;
				}
			}
			else if (text.CompareTo("or") == 0)
			{
				foreach (string text2 in array)
				{
					if (dr[text2] != DBNull.Value && ((dr[text2] is bool && Convert.ToBoolean(dr[text2])) || "yestrue1".IndexOf(dr[text2].ToString().ToLower().Trim()) >= 0))
					{
						flag = true;
						break;
					}
				}
			}
			else if (text.CompareTo("!or") == 0)
			{
				foreach (string text2 in array)
				{
					if (dr[text2] != DBNull.Value && ((dr[text2] is bool && Convert.ToBoolean(dr[text2])) || "yestrue1".IndexOf(dr[text2].ToString().ToLower().Trim()) >= 0))
					{
						flag = true;
						break;
					}
				}
				flag = !flag;
			}
			dr[columnName] = flag;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0001133C File Offset: 0x0001033C
		public static void ExtractUniqueRows(ref Report report, string colNames, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			string[] array = colNames.Split(new char[]
			{
				','
			});
			DataView currentDataView = report.GetCurrentDataView();
			if (currentDataView != null)
			{
				DataTable dataTable = currentDataView.Table.Clone();
				foreach (object obj in currentDataView)
				{
					DataRowView dataRowView = (DataRowView)obj;
					DataRow row = dataRowView.Row;
					StringBuilder stringBuilder = new StringBuilder(array[0] + "='" + row[array[0]].ToString().Replace("'", "`") + "'");
					for (int i = 1; i < array.Length; i++)
					{
						stringBuilder.Append(" AND ");
						stringBuilder.Append(array[i]);
						stringBuilder.Append("='");
						stringBuilder.Append(row[array[i]].ToString().Replace("'", "`"));
						stringBuilder.Append("'");
					}
					DataRow[] array2 = dataTable.Select(stringBuilder.ToString());
					if (array2 == null || array2.Length < 1)
					{
						dataTable.ImportRow(row);
					}
				}
				DataView dataView = new DataView(dataTable);
				string text = array[0];
				for (int i = 1; i < array.Length; i++)
				{
					text = text + "," + array[i];
				}
				dataView.Sort = text;
				report.AddResult(currentDataView);
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0001151C File Offset: 0x0001051C
		public static void EndExecuteScriptCaching()
		{
			if (ReportFunction.ExecuteScript_Cache != null)
			{
				ReportFunction.ExecuteScript_Cache.EndExecuteScriptCaching();
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00011544 File Offset: 0x00010544
		public static void StartExecuteScriptCaching()
		{
			if (ReportFunction.ExecuteScript_Cache == null)
			{
				ReportFunction.ExecuteScript_Cache = new ExecuteScriptCache();
			}
			ReportFunction.ExecuteScript_Cache.StartExecuteScriptCaching();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00011578 File Offset: 0x00010578
		private static string GetTempFilename(string fnExtension)
		{
			string tempFileName = Path.GetTempFileName();
			if (ReportFunction.WindowsRequestedFiles == null)
			{
				ReportFunction.WindowsRequestedFiles = new List<string>();
			}
			ReportFunction.WindowsRequestedFiles.Add(tempFileName);
			string text = Path.GetTempPath();
			text = Path.Combine(text, "TechnoPro");
			text = Path.Combine(text, "ClockWork");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			string path = Path.GetFileNameWithoutExtension(tempFileName) + fnExtension;
			return Path.Combine(text, path);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000115FC File Offset: 0x000105FC
		public static void ExecuteScript(Report report, string codeString, IncrementProgressBar ipb, SetupProgressBar sbp, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, bool suppressGuiMessages)
		{
			ReportFunction.ExecuteScript(report, codeString, ipb, sbp, da, tripleDES, suppressGuiMessages, true);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00011610 File Offset: 0x00010610
		public static Exception ExecuteScript2(Report report, string codeString, IncrementProgressBar ipb, SetupProgressBar sbp, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, bool suppressGuiMessages)
		{
			Exception ex = CompilerReports.ExecuteCodeString(codeString, da, tripleDES, ref report);
			if (ex != null && !suppressGuiMessages)
			{
				if (!suppressGuiMessages)
				{
					ReportFunction.MessageBoxShow(ex.ToString());
				}
			}
			return ex;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00011654 File Offset: 0x00010654
		public static Exception ExecuteScriptMigration(Report report, string codeString, IncrementProgressBar ipb, SetupProgressBar sbp, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, bool suppressGuiMessages)
		{
			Exception ex = CompilerImporter.ExecuteCodeString(codeString, da, tripleDES, ref report);
			if (ex != null && !suppressGuiMessages)
			{
				ReportFunction.MessageBoxShow(ex.ToString());
			}
			return ex;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00011690 File Offset: 0x00010690
		public static Exception TryCompile(string codeString)
		{
			return CompilerReports.CompileCodeString(codeString);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000116AC File Offset: 0x000106AC
		public static void ExecuteScript(Report report, string codeString, IncrementProgressBar ipb, SetupProgressBar sbp, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, bool suppressGuiMessages, bool actuallyExecute)
		{
			AppDomain executeScript_cachedSandboxDomain = null;
			IRemoteInterface remoteInterface = null;
			string text = "";
			try
			{
				List<string> list = new List<string>();
				list.Add("System");
				list.Add("System.Data");
				list.Add("System.Collections");
				list.Add("System.Collections.Generic");
				list.Add("System.Reflection");
				list.Add("System.Windows.Forms");
				list.Add("System.Xml");
				list.Add("RemoteLoader");
				list.Add("UnivOleDb");
				list.Add("EncryptionClassLibrary");
				list.Add("ReportFunctions");
				list.Add("AutoComboBox");
				int count = list.Count;
				string[] array = new string[]
				{
					"AutoComboBox",
					"ReportFunctions",
					"ImportExportClassLibrary",
					"UnivOleDb",
					"EncryptionClassLibrary"
				};
				StringReader stringReader = new StringReader(codeString);
				string value = "";
				string text2;
				while ((text2 = stringReader.ReadLine()) != null)
				{
					if (text2.IndexOf("using ") != 0)
					{
						value = text2 + System.Environment.NewLine + stringReader.ReadToEnd();
						break;
					}
					string text3 = text2.Substring(6).Trim();
					if (text3.Length > 1)
					{
						text3 = text3.Substring(0, text3.Length - 1);
					}
					if (text3.Length > 0 && Array.IndexOf<string>(array, text3) >= 0 && !list.Contains(text3))
					{
						list.Add(text3);
					}
				}
				string newLine = System.Environment.NewLine;
				string text4 = "blahblah";
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string value2 in list)
				{
					stringBuilder.Append("using ");
					stringBuilder.Append(value2);
					stringBuilder.Append(";");
					stringBuilder.Append(newLine);
				}
				stringBuilder.Append("namespace ");
				stringBuilder.Append(text4);
				stringBuilder.Append(" {");
				stringBuilder.Append(newLine);
				stringBuilder.Append("public class ClockWorkRowScript : MarshalByRefObject,IRemoteInterface ");
				stringBuilder.Append("{");
				stringBuilder.Append(newLine);
				stringBuilder.Append("public object Invoke(string lcMethod,object[] Parameters) {");
				stringBuilder.Append("return this.GetType().InvokeMember(lcMethod,");
				stringBuilder.Append("BindingFlags.InvokeMethod,null,this,Parameters);");
				stringBuilder.Append("}");
				stringBuilder.Append(newLine);
				stringBuilder.Append("  public static DataTable TableAction( System.Data.DataTable t, string dvSortString, DataTable[] otherTables, string cs, TripleDESEncryptionClass tripleDES )");
				stringBuilder.Append("{");
				stringBuilder.Append(newLine);
				stringBuilder.Append("DataView dv = new DataView( t ); dv.Sort = dvSortString;");
				stringBuilder.Append("UnivDataAdapter da; if ( ! String.IsNullOrEmpty( cs ) ) { UnivConnection conn = UnivOleDbFactory.CreateConnection(cs); da = conn.CreateDataAdapter(); } else { da = null; } ");
				stringBuilder.Append(newLine);
				stringBuilder.Append(value);
				stringBuilder.Append("  }");
				stringBuilder.Append(newLine);
				stringBuilder.Append("}");
				stringBuilder.Append(newLine);
				stringBuilder.Append("}");
				string text5 = stringBuilder.ToString();
				if (ReportFunction.ExecuteScript_Cache == null)
				{
					ReportFunction.ExecuteScript_Cache = new ExecuteScriptCache();
				}
				ExecuteScriptCacheItem executeScriptCacheItem;
				if (ReportFunction.ExecuteScript_Cache.ExecuteScript_CachingEnabled)
				{
					executeScriptCacheItem = ReportFunction.ExecuteScript_Cache.FindItem(text5);
				}
				else
				{
					executeScriptCacheItem = null;
				}
				if (executeScriptCacheItem != null)
				{
					remoteInterface = executeScriptCacheItem.ExecuteScript_cachedRemoteInterface;
				}
				else
				{
					text = ReportFunction.GetTempFilename(".dll");
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);
					string sourceCode = text5.ToString().Replace("namespace " + text4, "namespace " + fileNameWithoutExtension);
					remoteInterface = ReportFunction.Compile(text, out executeScript_cachedSandboxDomain, list, fileNameWithoutExtension, count, sourceCode);
					if (ReportFunction.ExecuteScript_Cache.ExecuteScript_CachingEnabled)
					{
						ExecuteScriptCacheItem executeScriptCacheItem2 = new ExecuteScriptCacheItem();
						executeScriptCacheItem2.ExecuteScript_cachedCode = text5;
						executeScriptCacheItem2.ExecuteScript_cachedRemoteInterface = remoteInterface;
						executeScriptCacheItem2.ExecuteScript_cachedTempFile = text;
						executeScriptCacheItem2.ExecuteScript_cachedSandboxDomain = executeScript_cachedSandboxDomain;
						ReportFunction.ExecuteScript_Cache.Add(executeScriptCacheItem2);
					}
				}
				DataView currentDataView = report.GetCurrentDataView();
				DataTable[] tablesExceptCurrent = report.GetTablesExceptCurrent();
				if (currentDataView != null)
				{
					DataTable dataTable = currentDataView.Table;
					object obj = null;
					try
					{
						if (actuallyExecute)
						{
							obj = remoteInterface.Invoke("TableAction", new object[]
							{
								dataTable,
								(currentDataView == null) ? "" : currentDataView.Sort,
								tablesExceptCurrent,
								da.Connection.ConnectionString,
								tripleDES
							});
						}
						else
						{
							obj = null;
						}
					}
					catch
					{
						obj = null;
					}
					if (obj == null)
					{
						ReportFunction.ExecuteScript_Cache.Remove(text5);
						text = ReportFunction.GetTempFilename(".dll");
						string fileNameWithoutExtension2 = Path.GetFileNameWithoutExtension(text);
						string sourceCode2 = text5.ToString().Replace("namespace " + text4, "namespace " + fileNameWithoutExtension2);
						remoteInterface = ReportFunction.Compile(text, out executeScript_cachedSandboxDomain, list, fileNameWithoutExtension2, count, sourceCode2);
						if (ReportFunction.ExecuteScript_Cache.ExecuteScript_CachingEnabled)
						{
							ExecuteScriptCacheItem executeScriptCacheItem2 = new ExecuteScriptCacheItem();
							executeScriptCacheItem2.ExecuteScript_cachedCode = text5;
							executeScriptCacheItem2.ExecuteScript_cachedRemoteInterface = remoteInterface;
							executeScriptCacheItem2.ExecuteScript_cachedTempFile = text;
							executeScriptCacheItem2.ExecuteScript_cachedSandboxDomain = executeScript_cachedSandboxDomain;
							ReportFunction.ExecuteScript_Cache.Add(executeScriptCacheItem2);
						}
						if (actuallyExecute)
						{
							obj = remoteInterface.Invoke("TableAction", new object[]
							{
								dataTable,
								(currentDataView == null) ? "" : currentDataView.Sort,
								tablesExceptCurrent,
								da.Connection.ConnectionString,
								tripleDES
							});
						}
						else
						{
							obj = null;
						}
					}
					if (obj != null && obj is DataTable)
					{
						dataTable = (DataTable)obj;
						report.AddResult(dataTable.DefaultView);
					}
				}
			}
			catch (Exception ex)
			{
				if (!suppressGuiMessages)
				{
					ReportFunction.MessageBoxShow(ex.ToString());
				}
				ReportFunction.AddError(report, "ExecuteScript", ex.Message);
				CWLogger.Logger.ErrorException("ExecuteScript", ex);
			}
			finally
			{
				if (!ReportFunction.ExecuteScript_Cache.ExecuteScript_CachingEnabled)
				{
					ReportFunction.UnloadSandboxDomain(ref executeScript_cachedSandboxDomain, ref remoteInterface, text);
				}
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00011DAC File Offset: 0x00010DAC
		private static IRemoteInterface Compile(string tempFile, out AppDomain sandboxDomain, List<string> usings, string namespaceName, int defaultUsingsCount, string sourceCode)
		{
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
			permissionSet.AddPermission(new FileIOPermission(FileIOPermissionAccess.AllAccess, tempFile));
			permissionSet.Demand();
			PolicyLevel policyLevel = PolicyLevel.CreateAppDomainLevel();
			policyLevel.RootCodeGroup.PolicyStatement = new PolicyStatement(permissionSet);
			Dictionary<string, string> providerOptions = new Dictionary<string, string>
			{
				{
					"CompilerVersion",
					"v3.5"
				}
			};
			ICodeCompiler codeCompiler = new CSharpCodeProvider(providerOptions).CreateCompiler();
			CompilerParameters compilerParameters = new CompilerParameters();
			string[] array = new string[]
			{
				"System.Collections",
				"System.Reflection"
			};
			for (int i = 0; i < usings.Count; i++)
			{
				string text = usings[i];
				bool flag = true;
				foreach (string value in array)
				{
					if (text.IndexOf(value) == 0)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					try
					{
						if (i >= defaultUsingsCount)
						{
							int length = text.IndexOf('.');
							compilerParameters.ReferencedAssemblies.Add(text.Substring(0, length) + ".dll");
						}
						else
						{
							compilerParameters.ReferencedAssemblies.Add(text + ".dll");
						}
					}
					catch
					{
					}
				}
			}
			compilerParameters.GenerateInMemory = false;
			compilerParameters.GenerateExecutable = false;
			compilerParameters.OutputAssembly = tempFile;
			CompilerResults compilerResults = codeCompiler.CompileAssemblyFromSource(compilerParameters, sourceCode);
			if (compilerResults.Errors.HasErrors)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object obj in compilerResults.Errors)
				{
					CompilerError compilerError = (CompilerError)obj;
					int num = compilerError.Line - 17;
					stringBuilder.Append(compilerError.ErrorText);
					stringBuilder.Append(": line: ");
					stringBuilder.Append(num.ToString());
					stringBuilder.Append(System.Environment.NewLine);
				}
				throw new Exception(stringBuilder.ToString());
			}
			sandboxDomain = AppDomain.CreateDomain("SandboxDomain", null, new AppDomainSetup
			{
				ApplicationBase = AppDomain.CurrentDomain.BaseDirectory
			});
			RemoteLoaderFactory remoteLoaderFactory = (RemoteLoaderFactory)sandboxDomain.CreateInstance("RemoteLoader", "RemoteLoader.RemoteLoaderFactory").Unwrap();
			object obj2 = remoteLoaderFactory.Create(tempFile, namespaceName + ".ClockWorkRowScript", null);
			return (IRemoteInterface)obj2;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00012098 File Offset: 0x00011098
		public static void UnloadSandboxDomain(ref AppDomain sandboxDomain, ref IRemoteInterface loRemote, string tempFile)
		{
			try
			{
				if (sandboxDomain != null)
				{
					AppDomain.Unload(sandboxDomain);
					sandboxDomain = null;
				}
				loRemote = null;
				if (tempFile.Length > 0 && File.Exists(tempFile))
				{
					File.Delete(tempFile);
				}
			}
			catch
			{
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000120FC File Offset: 0x000110FC
		private static void AddError(Report report, string comingFrom, string message)
		{
			DataView dataView = report.GetDataView("ErrorReport");
			if (dataView == null)
			{
				dataView = new DataView(new DataTable
				{
					Columns = 
					{
						"ComingFrom",
						"ErrMsg"
					}
				});
				report.AddResultNotPrimary(dataView, "ErrorReport");
			}
			dataView.Table.Rows.Add(new object[]
			{
				comingFrom,
				message
			});
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00012180 File Offset: 0x00011180
		public static void MergePrimaryAndSecondaryColumns(Report report, string primaryColName, IncrementProgressBar ipb, SetupProgressBar sbp)
		{
			DataView currentDataView = report.GetCurrentDataView();
			int num = currentDataView.Table.Columns.IndexOf(primaryColName);
			ReportFunction.GenericRowLoopAction rowAction = new ReportFunction.GenericRowLoopAction(ReportFunction.MergePrimaryAndSecondaryColumns);
			ReportFunction.GenericRowLooper(report, currentDataView, ipb, sbp, rowAction, new object[]
			{
				num
			});
			currentDataView.Table.Columns.Remove(primaryColName);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000121E4 File Offset: 0x000111E4
		private static void MergePrimaryAndSecondaryColumns(DataRow dr, params object[] oo)
		{
			int columnIndex = (int)oo[0];
			DataTable table = dr.Table;
			string columnName = dr[columnIndex].ToString();
			int num = table.Columns.IndexOf(columnName);
			if (num >= 0)
			{
				if (table.Columns[num].DataType == typeof(bool))
				{
					dr[num] = true;
				}
				else
				{
					dr[num] = "True";
				}
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0001226C File Offset: 0x0001126C
		private static void GenericRowLooper(Report report, DataView dvCurrent, IncrementProgressBar ipb, SetupProgressBar sbp, ReportFunction.GenericRowLoopAction rowAction, params object[] oo)
		{
			DataView dataView = (dvCurrent == null) ? report.GetCurrentDataView() : dvCurrent;
			if (dataView != null && dataView.Count > 0)
			{
				sbp(0, dataView.Count);
				int num = 0;
				foreach (object obj in dataView)
				{
					DataRowView dataRowView = (DataRowView)obj;
					DataRow row = dataRowView.Row;
					int num2 = ReportFunction.ShowIncrementAmount(ref num);
					if (num2 > 0 && ipb != null)
					{
						ipb(num2);
					}
					rowAction(row, oo);
				}
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00012344 File Offset: 0x00011344
		public static void RemoveRows(string uniqueColnames, string valueColName, bool min, ref Report report, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, IncrementProgressBar ipb, SetupProgressBar sbp)
		{
			DataTable table = report.GetCurrentDataView().Table;
			DataTable dataTable = table.Clone();
			DataView dataView = new DataView(table);
			dataView.Sort = uniqueColnames + "," + valueColName;
			string[] array = uniqueColnames.Split(new char[]
			{
				','
			});
			int j;
			for (int i = 0; i < dataView.Count; i = j)
			{
				DataRow row = dataView[i].Row;
				DataRow dataRow = row;
				for (j = i + 1; j < dataView.Count; j++)
				{
					DataRow row2 = dataView[j].Row;
					bool flag = true;
					foreach (string columnName in array)
					{
						if (row2[columnName].ToString().Trim().CompareTo(row[columnName].ToString().Trim()) != 0)
						{
							flag = false;
							break;
						}
					}
					if (!flag)
					{
						break;
					}
					int num = ReportFunction.CompareRows(row2, dataRow, valueColName);
					if (min && num < 0)
					{
						dataRow = row2;
					}
					else if (!min && num > 0)
					{
						dataRow = row2;
					}
				}
				dataTable.ImportRow(dataRow);
			}
			report.AddResult(dataTable.DefaultView);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000124CC File Offset: 0x000114CC
		private static int CompareRows(DataRow dr1, DataRow dr2, string valueColname)
		{
			int columnIndex = dr1.Table.Columns.IndexOf(valueColname);
			int result;
			if (dr1[columnIndex] == DBNull.Value)
			{
				if (dr2[columnIndex] == DBNull.Value)
				{
					result = 0;
				}
				else
				{
					result = -1;
				}
			}
			else if (dr2[columnIndex] == DBNull.Value)
			{
				result = 1;
			}
			else
			{
				Type dataType = dr1.Table.Columns[valueColname].DataType;
				if (dataType == typeof(DateTime))
				{
					DateTime dateTime = (DateTime)dr1[columnIndex];
					DateTime value = (DateTime)dr2[columnIndex];
					result = dateTime.CompareTo(value);
				}
				else if (dataType == typeof(int))
				{
					int num = (int)dr1[columnIndex];
					int value2 = (int)dr2[columnIndex];
					result = num.CompareTo(value2);
				}
				else
				{
					string text = dr1[columnIndex].ToString();
					string strB = dr2[columnIndex].ToString();
					result = text.CompareTo(strB);
				}
			}
			return result;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0001260C File Offset: 0x0001160C
		public static void RemoveNonClockWorkStudents(string snumColName, ref Report report, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, IncrementProgressBar ipb, SetupProgressBar sbp)
		{
			if (snumColName.Length < 1)
			{
				snumColName = "student_no";
			}
			DataTable table = report.GetCurrentDataView().Table;
			if (sbp != null)
			{
				sbp(0, table.Rows.Count);
			}
			da.SelectCommand.CommandText = "SELECT student_no,personid FROM people WHERE isactive=1";
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			dataTable = tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
			{
				"student_no"
			});
			ArrayList arrayList = new ArrayList();
			int num = 0;
			if (table.Columns.IndexOf("personid") < 0)
			{
				table.Columns.Add("personid", typeof(int));
			}
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num2 = ReportFunction.ShowIncrementAmount(ref num);
				if (num2 > 0 && ipb != null)
				{
					ipb(num2);
				}
				string str = ((string)dataRow[snumColName]).Trim().ToLower();
				DataRow[] array = dataTable.Select("student_no='" + str + "'");
				if (array != null && array.Length > 0)
				{
					DataRow dataRow2 = array[0];
					int num3 = (int)dataRow2["personid"];
					dataRow["personid"] = num3;
				}
				else
				{
					arrayList.Add(dataRow);
				}
			}
			foreach (object obj2 in arrayList)
			{
				DataRow dataRow = (DataRow)obj2;
				table.Rows.Remove(dataRow);
			}
			arrayList.Clear();
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00012854 File Offset: 0x00011854
		public static void RunCustomFunction(ref Report report, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, string parameters)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable dataTable = (currentDataView != null) ? currentDataView.Table : null;
			string[] array = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(parameters, true);
			int num = int.Parse(array[0]);
			int num2 = num;
			if (num2 == 1)
			{
				int num3 = int.Parse(array[1]);
				string columnName = array[2];
				dataTable.Columns.Add("Expired", typeof(bool));
				dataTable.Columns.Add("Changes_Pending", typeof(bool));
				da.SelectCommand.CommandText = "SELECT DISTINCT personid FROM (SELECT personid FROM maininfops WHERE controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@sn) UNION SELECT personid FROM otherinfops WHERE controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@sn) UNION SELECT personid FROM datetimeinfops WHERE controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@sn) ) x";
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@sn", num3);
				DataTable dataTable2 = new DataTable();
				da.Fill(dataTable2);
				int[] array2 = new int[dataTable2.Rows.Count];
				for (int i = 0; i < dataTable2.Rows.Count; i++)
				{
					array2[i] = (int)dataTable2.Rows[i][0];
				}
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					int value = (int)dataRow["personid"];
					dataRow["Expired"] = (dataRow[columnName] == DBNull.Value || (DateTime)dataRow[columnName] < DateTime.Now);
					dataRow["Changes_Pending"] = (Array.IndexOf<int>(array2, value) >= 0);
				}
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00012A54 File Offset: 0x00011A54
		public static void ConvertTimetableToClockWorkTimetable(Report report)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			if (table.Rows.Count > 0 && table.Columns.Contains("dayofweek") && table.Columns.Contains("starttime") && table.Columns.Contains("endtime"))
			{
				bool flag = table.Columns.IndexOf("timetableroom") >= 0;
				DataView dataView = new DataView();
				if (string.IsNullOrEmpty(table.TableName))
				{
					table.TableName = "courserows";
				}
				dataView.Table = table;
				List<string> list = new List<string>();
				string[] array = new string[]
				{
					"duration",
					"term",
					"startdate",
					"enddate",
					"subject",
					"course",
					"section",
					"timeofday"
				};
				foreach (string text in array)
				{
					if (table.Columns.Contains(text))
					{
						list.Add(text);
					}
				}
				dataView.Sort = string.Join(",", list.ToArray());
				table.Columns.Add("groupcode", typeof(int));
				int k;
				for (int j = 0; j < dataView.Count; j = k)
				{
					DataRow row = dataView[j].Row;
					row["groupcode"] = j;
					for (k = j + 1; k < dataView.Count; k++)
					{
						DataRow row2 = dataView[k].Row;
						if (!ReportFunction.AreExternalCourseRowsTheSameCourse(row, row2, list))
						{
							break;
						}
						row2["groupcode"] = j;
					}
				}
				string[] array3 = new string[]
				{
					"sun",
					"mon",
					"tue",
					"wed",
					"thu",
					"fri",
					"sat"
				};
				Type typeFromHandle = typeof(int);
				for (int l = 0; l < 7; l++)
				{
					string columnName = array3[l] + "startminutes";
					string columnName2 = array3[l] + "endminutes";
					table.Columns.Add(columnName, typeFromHandle);
					table.Columns.Add(columnName2, typeFromHandle);
					if (flag)
					{
						table.Columns.Add(array3[l] + "room");
					}
				}
				DataTable dataTable = table.Clone();
				for (int j = 0; j < dataView.Count; j = k)
				{
					DataRow row = dataView[j].Row;
					int num = (int)row["groupcode"];
					ReportFunction.SetTimetableInfo(row, array3);
					for (k = j + 1; k < dataView.Count; k++)
					{
						DataRow row2 = dataView[k].Row;
						int num2 = (int)row2["groupcode"];
						ReportFunction.SetTimetableInfo(row2, array3);
						for (int m = 0; m < table.Columns.Count; m++)
						{
							if (row[m] == DBNull.Value || row[m].ToString().Length < 1)
							{
								row[m] = row2[m];
							}
						}
						if (num2 != num)
						{
							break;
						}
					}
					dataTable.ImportRow(row);
				}
				report.AddResult(dataTable.DefaultView);
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00012E84 File Offset: 0x00011E84
		private static void SetTimetableInfo(DataRow dr, string[] daysOfWeek)
		{
			string text = dr["dayofweek"].ToString().Trim().ToLower();
			string text2 = dr["starttime"].ToString().Trim();
			string text3 = dr["endtime"].ToString().Trim();
			if (text.Length > 0 && text2.Length > 0 && text3.Length > 0)
			{
				string arg = DateTime.Now.ToString("yyyy-MM-dd");
				string s = string.Format("{0} {1}", arg, text2);
				string s2 = string.Format("{0} {1}", arg, text3);
				DateTime dateTime;
				DateTime dateTime2;
				if (DateTime.TryParse(s, out dateTime) && DateTime.TryParse(s2, out dateTime2))
				{
					string text4 = text;
					string text5;
					switch (text4)
					{
					case "monday":
					case "mon":
					case "mo":
					case "m":
					case "lundi":
					case "lun":
					case "lu":
					case "l":
						text5 = daysOfWeek[1];
						goto IL_48C;
					case "tuesday":
					case "tue":
					case "tu":
					case "mardi":
					case "mar":
					case "ma":
						text5 = daysOfWeek[2];
						goto IL_48C;
					case "wednesday":
					case "wed":
					case "we":
					case "w":
					case "mercredi":
					case "mer":
					case "me":
						text5 = daysOfWeek[3];
						goto IL_48C;
					case "thursday":
					case "thu":
					case "thur":
					case "th":
					case "jeudi":
					case "jeu":
					case "je":
					case "j":
						text5 = daysOfWeek[4];
						goto IL_48C;
					case "friday":
					case "fri":
					case "fr":
					case "f":
					case "vendredi":
					case "vend":
					case "ven":
					case "ve":
					case "v":
						text5 = daysOfWeek[5];
						goto IL_48C;
					case "saturday":
					case "sat":
					case "sa":
					case "samedi":
					case "sam":
						text5 = daysOfWeek[6];
						goto IL_48C;
					case "sunday":
					case "sun":
					case "su":
					case "dimanche":
					case "dim":
					case "di":
					case "d":
						text5 = daysOfWeek[0];
						goto IL_48C;
					}
					text5 = "";
					IL_48C:
					if (!string.IsNullOrEmpty(text5))
					{
						string columnName = string.Format("{0}{1}", text5, "startminutes");
						string columnName2 = string.Format("{0}{1}", text5, "endminutes");
						string text6 = string.Format("{0}{1}", text5, "room");
						dr[columnName] = dateTime.Hour * 60 + dateTime.Minute;
						dr[columnName2] = dateTime2.Hour * 60 + dateTime2.Minute;
						if (dr.Table.Columns.Contains(text6))
						{
							dr[text6] = dr["timetableroom"].ToString();
						}
					}
				}
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000133E0 File Offset: 0x000123E0
		private static bool AreExternalCourseRowsTheSameCourse(DataRow dr1, DataRow dr2, List<string> availableColumns)
		{
			foreach (string columnName in availableColumns)
			{
				string text = dr1[columnName].ToString().Trim();
				string value = dr2[columnName].ToString().Trim();
				if (!text.Equals(value, StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00013470 File Offset: 0x00012470
		public static void ConvertTimetableToClockWorkTimetable(string convertFromType, string studentNumberColname, string subjectColName, string courseColName, string parameters, ref Report report, IncrementProgressBar ipb, SetupProgressBar sbp)
		{
			string[] array = new string[]
			{
				"sun",
				"mon",
				"tue",
				"wed",
				"thu",
				"fri",
				"sat"
			};
			Type typeFromHandle = typeof(int);
			DataTable table = report.GetCurrentDataView().Table;
			bool flag = table.Columns.IndexOf("timetableroom") >= 0;
			for (int i = 0; i < 7; i++)
			{
				string columnName = array[i] + "startminutes";
				string columnName2 = array[i] + "endminutes";
				table.Columns.Add(columnName, typeFromHandle);
				table.Columns.Add(columnName2, typeFromHandle);
				if (flag)
				{
					table.Columns.Add(array[i] + "room");
				}
			}
			DataTable dataTable = table.Clone();
			string[] array2 = parameters.Split(new char[]
			{
				','
			});
			DataView dataView = new DataView(table);
			dataView.Sort = string.Concat(new string[]
			{
				studentNumberColname,
				",",
				subjectColName,
				",",
				courseColName
			});
			if (convertFromType.CompareTo("NA") != 0)
			{
				string text = array2[0];
				string text2 = array2[1];
				string text3 = array2[2];
				bool flag2 = table.Columns.Contains("timeofday");
				int k;
				for (int j = 0; j < dataView.Count; j = k)
				{
					DataRow row = dataView[j].Row;
					string strB = row[studentNumberColname].ToString();
					string value = row[subjectColName].ToString();
					string value2 = row[courseColName].ToString();
					string text4 = flag2 ? row["timeofday"].ToString().Trim() : "";
					for (k = j + 1; k < dataView.Count; k++)
					{
						DataRow row2 = dataView[k].Row;
						string text5 = row2[studentNumberColname].ToString();
						string text6 = row2[subjectColName].ToString();
						string text7 = row2[courseColName].ToString();
						string value3 = flag2 ? row2["timeofday"].ToString().Trim() : "";
						if (text5.CompareTo(strB) != 0 || !text6.Equals(value, StringComparison.OrdinalIgnoreCase) || !text7.Equals(value2, StringComparison.OrdinalIgnoreCase) || !text4.Equals(value3, StringComparison.OrdinalIgnoreCase))
						{
							break;
						}
					}
					int l = j;
					while (l < k)
					{
						DataRow row3 = dataView[l].Row;
						string text8 = row3[text].ToString().ToLower();
						string text9 = row3[text2].ToString();
						string text10 = row3[text3].ToString();
						string text11 = text8;
						if (text11 == null)
						{
							goto IL_6D4;
						}
						if (<PrivateImplementationDetails>{EB7E743C-23B4-4E9D-8F63-33DF2E415822}.$$method0x60000e9-1 == null)
						{
							<PrivateImplementationDetails>{EB7E743C-23B4-4E9D-8F63-33DF2E415822}.$$method0x60000e9-1 = new Dictionary<string, int>(50)
							{
								{
									"monday",
									0
								},
								{
									"mon",
									1
								},
								{
									"mo",
									2
								},
								{
									"m",
									3
								},
								{
									"lundi",
									4
								},
								{
									"lun",
									5
								},
								{
									"lu",
									6
								},
								{
									"l",
									7
								},
								{
									"tuesday",
									8
								},
								{
									"tue",
									9
								},
								{
									"tu",
									10
								},
								{
									"mardi",
									11
								},
								{
									"mar",
									12
								},
								{
									"ma",
									13
								},
								{
									"wednesday",
									14
								},
								{
									"wed",
									15
								},
								{
									"we",
									16
								},
								{
									"w",
									17
								},
								{
									"mercredi",
									18
								},
								{
									"mer",
									19
								},
								{
									"me",
									20
								},
								{
									"thursday",
									21
								},
								{
									"thu",
									22
								},
								{
									"thur",
									23
								},
								{
									"th",
									24
								},
								{
									"jeudi",
									25
								},
								{
									"jeu",
									26
								},
								{
									"je",
									27
								},
								{
									"j",
									28
								},
								{
									"friday",
									29
								},
								{
									"fri",
									30
								},
								{
									"fr",
									31
								},
								{
									"f",
									32
								},
								{
									"vendredi",
									33
								},
								{
									"vend",
									34
								},
								{
									"ven",
									35
								},
								{
									"ve",
									36
								},
								{
									"v",
									37
								},
								{
									"saturday",
									38
								},
								{
									"sat",
									39
								},
								{
									"sa",
									40
								},
								{
									"samedi",
									41
								},
								{
									"sam",
									42
								},
								{
									"sunday",
									43
								},
								{
									"sun",
									44
								},
								{
									"su",
									45
								},
								{
									"dimanche",
									46
								},
								{
									"dim",
									47
								},
								{
									"di",
									48
								},
								{
									"d",
									49
								}
							};
						}
						int num;
						if (!<PrivateImplementationDetails>{EB7E743C-23B4-4E9D-8F63-33DF2E415822}.$$method0x60000e9-1.TryGetValue(text11, out num))
						{
							goto IL_6D4;
						}
						string text12;
						switch (num)
						{
						case 0:
						case 1:
						case 2:
						case 3:
						case 4:
						case 5:
						case 6:
						case 7:
							text12 = array[1];
							break;
						case 8:
						case 9:
						case 10:
						case 11:
						case 12:
						case 13:
							text12 = array[2];
							break;
						case 14:
						case 15:
						case 16:
						case 17:
						case 18:
						case 19:
						case 20:
							text12 = array[3];
							break;
						case 21:
						case 22:
						case 23:
						case 24:
						case 25:
						case 26:
						case 27:
						case 28:
							text12 = array[4];
							break;
						case 29:
						case 30:
						case 31:
						case 32:
						case 33:
						case 34:
						case 35:
						case 36:
						case 37:
							text12 = array[5];
							break;
						case 38:
						case 39:
						case 40:
						case 41:
						case 42:
							text12 = array[6];
							break;
						case 43:
						case 44:
						case 45:
						case 46:
						case 47:
						case 48:
						case 49:
							text12 = array[0];
							break;
						default:
							goto IL_6D4;
						}
						IL_6DD:
						if (text12.Length > 0)
						{
							DateTime dateTime;
							if (!DateTime.TryParse("2000-01-01 " + text9, out dateTime))
							{
								if (DateTime.TryParse(text9, out dateTime))
								{
									dateTime = new DateTime(2000, 1, 1, dateTime.Hour, dateTime.Minute, 0);
								}
							}
							DateTime dateTime2;
							if (!DateTime.TryParse("2000-01-01 " + text10, out dateTime2))
							{
								if (DateTime.TryParse(text10, out dateTime2))
								{
									dateTime2 = new DateTime(2000, 1, 1, dateTime2.Hour, dateTime2.Minute, 0);
								}
							}
							int num2 = dateTime.Hour * 60 + dateTime.Minute;
							int num3 = dateTime2.Hour * 60 + dateTime2.Minute;
							row[text12 + "startminutes"] = num2;
							row[text12 + "endminutes"] = num3;
							if (flag)
							{
								string text13 = text12 + "room";
								if (row.Table.Columns.Contains(text13))
								{
									row[text13] = row3["timetableroom"].ToString().Trim();
								}
							}
						}
						l++;
						continue;
						IL_6D4:
						text12 = "";
						goto IL_6DD;
					}
					dataTable.ImportRow(row);
				}
				dataTable.Columns.Remove(text);
				dataTable.Columns.Remove(text2);
				dataTable.Columns.Remove(text3);
				if (flag)
				{
					dataTable.Columns.Remove("timetableroom");
				}
			}
			report.AddResult(dataTable.DefaultView);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00013D60 File Offset: 0x00012D60
		public static DataTable CrossReferenceDisabilityInfo(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, DataView dv)
		{
			int num;
			List<int> list;
			int num2;
			ReportFunction.FindDisabilityPrimaryAndSecondaryCids(da, tripleDES, out num, out list, out num2);
			string text = num.ToString();
			foreach (int num3 in list)
			{
				text = text + "," + num3.ToString();
			}
			DataTable dataTable = new DataTable();
			da.SelectCommand.CommandText = "SELECT dsc.controlid,dc.controlcaption FROM dynamicscreencontrols dsc LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid WHERE dsc.screennum=@screennum AND dsc.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,',')) ORDER BY dsc.ordernum";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@cids", text);
			da.SelectCommand.Parameters.Add("@screennum", num2);
			da.Fill(dataTable);
			string text2 = da.Connection.GetTempTablePrefix() + "pids";
			string sql = "SELECT x.personid,ps.controlid,ps.valint,ps.valbytes,ps.valdate,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.setting4,dc.defaultvalue FROM " + text2 + " x LEFT JOIN perstudentdata ps ON ps.personid=x.personid LEFT JOIN dynamiccontrols dc ON dc.controlid=ps.controlid WHERE ps.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','))";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@cids", text);
			DataTable dataTable2 = ReportFunction.ExecuteSqlForPids(da, tripleDES, dv, text2, sql, da.SelectCommand.Parameters);
			DataTable table = dv.Table;
			string text3 = "PrimaryDisability";
			DataRow dataRow = null;
			List<DynamicDataColumn> list2 = new List<DynamicDataColumn>();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow2 = (DataRow)obj;
				int num4 = (int)dataRow2["controlid"];
				if (num4 == num)
				{
					dataRow = dataRow2;
				}
				else
				{
					string controlCaptionForDisplay = DynamicControl.GetControlCaptionForDisplay(dataRow2["controlcaption"].ToString());
					list2.Add(new DynamicDataColumn(num4, controlCaptionForDisplay));
					dataRow2["controlcaption"] = controlCaptionForDisplay;
				}
			}
			if (dataRow != null)
			{
				text3 = dataRow["controlcaption"].ToString();
				text3 = DynamicControl.GetControlCaptionForDisplay(text3);
				dataTable.Rows.Remove(dataRow);
			}
			if (!table.Columns.Contains(text3))
			{
				table.Columns.Add(text3);
			}
			string text4 = "HasMultipleDisabilities";
			if (!table.Columns.Contains(text4))
			{
				table.Columns.Add(text4, typeof(bool));
			}
			int columnIndex = table.Columns.IndexOf("HasMultipleDisabilities");
			foreach (DynamicDataColumn dynamicDataColumn in list2)
			{
				string colName = dynamicDataColumn.ColName;
				if (!table.Columns.Contains(colName))
				{
					table.Columns.Add(colName, typeof(bool));
				}
			}
			foreach (object obj2 in table.Rows)
			{
				DataRow dataRow2 = (DataRow)obj2;
				DataRow[] array = dataTable2.Select("personid=" + ((int)dataRow2["personid"]).ToString());
				int num5 = 0;
				foreach (DataRow dataRow3 in array)
				{
					int num4 = (int)dataRow3["controlid"];
					int i2 = (dataRow3["valint"] == DBNull.Value) ? 0 : ((int)dataRow3["valint"]);
					if (num4 == num)
					{
						DataRow[] array3 = dataTable.Select("controlid=" + i2.ToString());
						if (array3.Length > 0)
						{
							string text5 = array3[0]["controlcaption"].ToString();
							dataRow2[text3] = text5;
							if (text5.Trim().Length > 0)
							{
								num5++;
							}
						}
						else
						{
							dataRow2[text3] = "";
						}
					}
					else
					{
						bool flag = DynamicScreen.IntToBool(i2);
						if (flag)
						{
							foreach (DynamicDataColumn dynamicDataColumn in list2)
							{
								if (dynamicDataColumn.ControlId == num4)
								{
									dataRow2[dynamicDataColumn.ColName] = true;
									num5++;
									break;
								}
							}
						}
					}
				}
				if (num5 > 1)
				{
					dataRow2[columnIndex] = true;
				}
			}
			dataTable2.Dispose();
			return table;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00014320 File Offset: 0x00013320
		public static DataTable ExecuteSqlForPids(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, DataView dv, string tempTableName, string sql, UnivParameterCollection sqlParameters)
		{
			UnivTransaction univTransaction = null;
			DataColumn dataColumn = dv.Table.Columns["personid"];
			if (dataColumn != null && dataColumn.DataType == typeof(string))
			{
				if (dv.Table.Columns.Contains("personidold"))
				{
					dv.Table.Columns.Remove(dataColumn);
				}
				else
				{
					dataColumn.ColumnName = "personidold";
				}
				dv.Table.Columns.Add("personid", typeof(int));
				int num = dv.Table.Columns.IndexOf("personid");
				int columnIndex = dv.Table.Columns.IndexOf("personidold");
				dv.Table.Columns[num].ColumnMapping = MappingType.Hidden;
				foreach (object obj in dv)
				{
					DataRowView dataRowView = (DataRowView)obj;
					DataRow row = dataRowView.Row;
					int num2;
					if (row[columnIndex] == DBNull.Value)
					{
						num2 = 0;
					}
					else
					{
						string text = ((string)row[columnIndex]).Trim();
						if (text.Length > 0)
						{
							try
							{
								num2 = int.Parse(text);
							}
							catch
							{
								num2 = 0;
							}
						}
						else
						{
							num2 = 0;
						}
					}
					row[num] = num2;
				}
			}
			DataTable result;
			try
			{
				da.Connection.Open();
				univTransaction = da.Connection.BeginTransaction();
				DataTable dataTable = new DataTable();
				using (UnivCommand univCommand = UnivOleDbFactory.CreateCommand("", da.Connection, univTransaction))
				{
					univCommand.CommandText = "CREATE TABLE " + tempTableName + " (personid int)";
					univCommand.ExecuteNonQuery2();
					foreach (object obj2 in dv)
					{
						DataRowView dataRowView = (DataRowView)obj2;
						DataRow row = dataRowView.Row;
						int num2 = (int)row["personid"];
						univCommand.CommandText = string.Concat(new string[]
						{
							"INSERT INTO ",
							tempTableName,
							" (personid) VALUES (",
							num2.ToString(),
							")"
						});
						univCommand.ExecuteNonQuery2();
					}
					univTransaction.Commit();
					univCommand.CommandText = sql;
					univCommand.Parameters.Clear();
					for (int i = 0; i < sqlParameters.Count; i++)
					{
						string parameterName = sqlParameters.ParameterName(i);
						univCommand.Parameters.Add(parameterName, sqlParameters.Value(i));
					}
					UnivDataReader univDataReader = univCommand.ExecuteReader2();
					DataReaderAdapter dataReaderAdapter = new DataReaderAdapter();
					dataReaderAdapter.FillFromReader(dataTable, univDataReader.GetNativeDataReader());
					try
					{
						univCommand.CommandText = "DROP TABLE " + tempTableName;
						univCommand.ExecuteNonQuery2();
					}
					catch
					{
					}
				}
				result = dataTable;
			}
			catch (Exception ex)
			{
				try
				{
					univTransaction.Rollback();
				}
				catch
				{
				}
				result = null;
			}
			finally
			{
				da.Connection.Close();
			}
			return result;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00014790 File Offset: 0x00013790
		private static void FindDisabilityPrimaryAndSecondaryCids(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, out int primaryCid, out List<int> secondaryCids, out int screenNum)
		{
			screenNum = ReportFunction.FindDisabilityScreenNum(da, tripleDES);
			primaryCid = ReportFunction.FindDisabilityPrimaryDisabilityRadioGroupCid(screenNum, da, tripleDES);
			secondaryCids = ReportFunction.FindDisabilitySecondaryDisabilityCheckboxesCids(screenNum, primaryCid, da, tripleDES);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000147B8 File Offset: 0x000137B8
		private static int FindDisabilityScreenNum(UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			da.SelectCommand.CommandText = "SELECT screennum,description FROM screens WHERE isactive=1";
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string text = dataRow["description"].ToString().Trim().ToLower();
				if (text.IndexOf("disability") >= 0)
				{
					return (int)dataRow[0];
				}
			}
			return 0;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00014888 File Offset: 0x00013888
		private static int FindDisabilityPrimaryDisabilityRadioGroupCid(int screenNum, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			if (screenNum == 0)
			{
				screenNum = 8;
			}
			da.SelectCommand.CommandText = "SELECT controlid,controlcaption FROM dynamiccontrols WHERE controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=" + screenNum.ToString() + ") AND controlcode=14 AND setting4>0 AND enabled=1";
			DataTable dataTable = new DataTable();
			string text;
			da.Fill(dataTable, out text);
			int result;
			if (dataTable.Rows.Count > 0)
			{
				result = (int)dataTable.Rows[0][0];
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0001490C File Offset: 0x0001390C
		private static List<int> FindDisabilitySecondaryDisabilityCheckboxesCids(int screenNum, int primaryDisCid, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			if (screenNum == 0)
			{
				screenNum = 8;
			}
			da.SelectCommand.CommandText = "SELECT dsc.controlid,dc.controlcode,dc.controlcaption,dc.setting4 FROM dynamicscreencontrols dsc LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid WHERE dsc.isactive=1 AND dsc.screennum=@screennum AND dc.enabled=1 ORDER BY dsc.ordernum";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@screennum", screenNum);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			bool flag = false;
			List<int> list = new List<int>();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow[0];
				if (!flag)
				{
					if (num == primaryDisCid)
					{
						flag = true;
					}
				}
				else
				{
					int num2 = (int)dataRow[1];
					if (num2 == 31)
					{
						break;
					}
					if (num2 == 2)
					{
						list.Add(num);
					}
				}
			}
			return list;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00014A44 File Offset: 0x00013A44
		public static void DateFix(ref Report report, string colNames, string format, IncrementProgressBar ipb, SetupProgressBar sbp)
		{
			string[] array = colNames.Split(new char[]
			{
				','
			});
			DataTable table = report.GetCurrentDataView().Table;
			if (sbp != null)
			{
				sbp(0, table.Rows.Count);
			}
			char c = '\0';
			foreach (char c2 in format)
			{
				if (c2 != 'm' && c2 != 'd' && c2 != 'y')
				{
					c = c2;
					break;
				}
			}
			string[] array2 = format.ToLower().Split(new char[]
			{
				c
			});
			int num = Array.IndexOf<string>(array2, "d");
			int num2 = Array.IndexOf<string>(array2, "m");
			int num3 = Array.IndexOf<string>(array2, "y");
			int num4 = 0;
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num5 = ReportFunction.ShowIncrementAmount(ref num4);
				if (num5 > 0 && ipb != null)
				{
					ipb(num5);
				}
				foreach (string text in array)
				{
					string text2 = dataRow[text].ToString().Trim();
					if (text2.Length > 0)
					{
						string[] array4 = text2.Split(new char[]
						{
							c
						});
						string text3 = array4[num3];
						if (text3.Length == 2)
						{
							text3 = "20" + text3;
						}
						DateTime dateTime = new DateTime(int.Parse(text3), int.Parse(array4[num2]), int.Parse(array4[num]));
						if (table.Columns[text].DataType == typeof(DateTime))
						{
							dataRow[text] = dateTime;
						}
						else
						{
							dataRow[text] = dateTime.ToString("yyyy-MM-dd");
						}
					}
				}
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00014CC8 File Offset: 0x00013CC8
		public static void OnlyKeepRowsWhereASpecificColumnMatchesOneOfASetOfValues(ref Report report, string colName, string[] possibleMatchingValues, IncrementProgressBar ipb, SetupProgressBar sbp)
		{
			DataTable table = report.GetCurrentDataView().Table;
			if (sbp != null)
			{
				sbp(0, table.Rows.Count);
			}
			ArrayList arrayList = new ArrayList();
			int num = 0;
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num2 = ReportFunction.ShowIncrementAmount(ref num);
				if (num2 > 0 && ipb != null)
				{
					ipb(num2);
				}
				string value = dataRow[colName].ToString().Trim().ToLower();
				if (Array.IndexOf<string>(possibleMatchingValues, value) < 0)
				{
					arrayList.Add(dataRow);
				}
			}
			foreach (object obj2 in arrayList)
			{
				DataRow dataRow = (DataRow)obj2;
				table.Rows.Remove(dataRow);
			}
			arrayList.Clear();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00014E28 File Offset: 0x00013E28
		public static void SearchAndReplaceCaseInsensitive(ref Report report, IncrementProgressBar ipb, SetupProgressBar spb, params string[] searchAndReplaceDefinitions)
		{
			DataTable table = report.GetCurrentDataView().Table;
			if (spb != null)
			{
				spb(0, table.Rows.Count * searchAndReplaceDefinitions.Length);
			}
			foreach (string text in searchAndReplaceDefinitions)
			{
				string[] array = text.Split(new char[]
				{
					'`'
				});
				string text2 = array[0];
				bool flag;
				if (text2.Length > 2 && text2[text2.Length - 2] == '!' && text2[text2.Length - 1] == '=')
				{
					text2 = text2.Substring(0, text2.Length - 2);
					flag = true;
				}
				else
				{
					flag = false;
				}
				string text3 = array[1].ToLower();
				string text4 = array[2];
				int num = 0;
				foreach (object obj in table.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					int num2 = ReportFunction.ShowIncrementAmount(ref num);
					if (num2 > 0 && ipb != null)
					{
						ipb(num2);
					}
					string text5 = dataRow[text2].ToString();
					string text6 = text5.ToLower();
					bool flag2;
					if (text6.Length < 1)
					{
						flag2 = (text3.Length < 1);
					}
					else
					{
						flag2 = (text6.IndexOf(text3) >= 0);
					}
					if (flag2 && !flag)
					{
						dataRow[text2] = ((text5.Length > 0) ? ReportFunction.ReplaceEx(text5, text3, text4) : text4);
					}
					else if (!flag2 && flag)
					{
						dataRow[text2] = text4;
					}
				}
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00015038 File Offset: 0x00014038
		private static string ReplaceEx(string original, string pattern, string replacement)
		{
			int length;
			int num = length = 0;
			string text = original.ToUpper();
			string value = pattern.ToUpper();
			int val = original.Length / pattern.Length * (replacement.Length - pattern.Length);
			char[] array = new char[original.Length + Math.Max(0, val)];
			int num2;
			while ((num2 = text.IndexOf(value, num)) != -1)
			{
				for (int i = num; i < num2; i++)
				{
					array[length++] = original[i];
				}
				for (int i = 0; i < replacement.Length; i++)
				{
					array[length++] = replacement[i];
				}
				num = num2 + pattern.Length;
			}
			string result;
			if (num == 0)
			{
				result = original;
			}
			else
			{
				for (int i = num; i < original.Length; i++)
				{
					array[length++] = original[i];
				}
				result = new string(array, 0, length);
			}
			return result;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00015150 File Offset: 0x00014150
		public static void FigureOutCourseStartEndDates(ref Report report, string defn, IncrementProgressBar ipb, SetupProgressBar spb)
		{
			DataTable table = report.GetCurrentDataView().Table;
			ReportFunction.CourseStartEndDateRuleCollection courseStartEndDateRuleCollection = new ReportFunction.CourseStartEndDateRuleCollection(defn);
			if (spb != null)
			{
				spb(0, table.Rows.Count * courseStartEndDateRuleCollection.Count);
			}
			table.Columns.Add("CourseStartDate", typeof(DateTime));
			table.Columns.Add("CourseEndDate", typeof(DateTime));
			int num = 0;
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num2 = ReportFunction.ShowIncrementAmount(ref num);
				if (num2 > 0 && ipb != null)
				{
					ipb(num2);
				}
				DateTime dateTime;
				DateTime dateTime2;
				courseStartEndDateRuleCollection.CalculateStartEndDates(dataRow, out dateTime, out dateTime2);
				dataRow["CourseStartDate"] = dateTime;
				dataRow["CourseEndDate"] = dateTime2;
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00015280 File Offset: 0x00014280
		public static void RightLeft(ref Report report, bool right, string colName, string colNameDest, int numChars, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			if (!table.Columns.Contains(colNameDest))
			{
				table.Columns.Add(colNameDest);
			}
			if (SetupSubProgressBar != null)
			{
				SetupSubProgressBar(0, table.Rows.Count);
			}
			int num = 0;
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num2 = ReportFunction.ShowIncrementAmount(ref num);
				if (num2 > 0 && IncrementSubProgressBar != null)
				{
					IncrementSubProgressBar(num2);
				}
				string s = dataRow[colName].ToString();
				if (right)
				{
					dataRow[colNameDest] = ReportFunction.Right(s, numChars);
				}
				else
				{
					dataRow[colNameDest] = ReportFunction.Left(s, numChars);
				}
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000153A4 File Offset: 0x000143A4
		private static void ExtractNameValueWithOperator(string s, string[] possibleOperators, out string foundOperator, out string name, out string val, out Type forceType)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			foreach (string text4 in possibleOperators)
			{
				int num = s.IndexOf(text4);
				if (num > 0 && num < s.Length - 1)
				{
					text2 = text4;
					text = s.Substring(0, num);
					text3 = s.Substring(num + 1);
					break;
				}
			}
			if (text == null)
			{
				text = s;
				val = "";
				foundOperator = "";
				name = s;
			}
			else
			{
				val = text3;
				foundOperator = text2;
				name = text;
			}
			int num2 = text.IndexOf('[');
			if (num2 > 0 && text.Length > 2)
			{
				string text5 = text.Substring(num2 + 1, text.Length - num2 - 2).ToLower();
				text = text.Substring(0, num2);
				string text6 = text5;
				if (text6 != null)
				{
					if (text6 == "datetime")
					{
						forceType = typeof(DateTime);
						goto IL_157;
					}
					if (text6 == "string")
					{
						forceType = typeof(string);
						goto IL_157;
					}
					if (text6 == "int")
					{
						forceType = typeof(int);
						goto IL_157;
					}
				}
				forceType = null;
				IL_157:;
			}
			else
			{
				forceType = null;
			}
			name = text;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00015514 File Offset: 0x00014514
		public static DateTime GetCurrentSemesterStart()
		{
			DateTime now = DateTime.Now;
			DateTime now2 = DateTime.Now;
			DateTime now3 = DateTime.Now;
			int year = now.Year;
			DateTime t = new DateTime(year, 8, 12);
			DateTime t2 = new DateTime(year, 12, 12);
			DateTime result;
			if (!(t2 < now2) && !(t > now3))
			{
				result = new DateTime(year, 9, 1);
			}
			else
			{
				t = new DateTime(year, 4, 12);
				t2 = new DateTime(year, 8, 12);
				if (!(t2 < now2) && !(t > now3))
				{
					result = new DateTime(year, 5, 1);
				}
				else
				{
					result = new DateTime(year, 1, 1);
				}
			}
			return result;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000155CC File Offset: 0x000145CC
		public static void RemoveRowsByComparison(ref Report report, string colNameOperatorVal, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			if (SetupSubProgressBar != null)
			{
				SetupSubProgressBar(0, table.Rows.Count);
			}
			string text;
			string text2;
			string text3;
			Type type;
			ReportFunction.ExtractNameValueWithOperator(colNameOperatorVal, new string[]
			{
				"<",
				">",
				"=",
				"!=",
				">=",
				"<="
			}, out text, out text2, out text3, out type);
			DateTime dateTime = DateTime.MinValue;
			int num = 0;
			Type dataType = table.Columns[text2].DataType;
			Type type2 = (type == null) ? dataType : type;
			if (type2 == typeof(DateTime))
			{
				if (text3.ToLower().CompareTo("currentsemesterstart") == 0)
				{
					dateTime = ReportFunction.GetCurrentSemesterStart();
				}
				else
				{
					try
					{
						dateTime = DateTime.Parse(text3);
					}
					catch
					{
						dateTime = DateTime.MinValue;
						text3 = "";
					}
				}
			}
			else if (type2 == typeof(int))
			{
				try
				{
					num = int.Parse(text3);
				}
				catch
				{
					num = 0;
					text3 = "";
				}
			}
			else if (type2 == typeof(double))
			{
				try
				{
					double num2 = double.Parse(text3);
				}
				catch
				{
					text3 = "";
				}
			}
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < table.Rows.Count; i++)
			{
				DataRow dataRow = table.Rows[i];
				bool flag = false;
				if (dataRow[text2] == DBNull.Value)
				{
					if (text3.Length < 1)
					{
						flag = true;
					}
				}
				else
				{
					object obj;
					if (dataType == typeof(DateTime))
					{
						obj = (DateTime)dataRow[text2];
					}
					else if (dataType == typeof(int))
					{
						obj = (int)dataRow[text2];
					}
					else
					{
						obj = dataRow[text2].ToString();
					}
					if (obj.GetType() != type2)
					{
						string text4 = obj.ToString().Trim();
						if (type2 == typeof(DateTime))
						{
							obj = ReportFunction.ParseDateTime(text4);
						}
						else if (type2 == typeof(int))
						{
							obj = ReportFunction.ParseInt(text4);
						}
						else
						{
							obj = text4;
						}
					}
					if (obj is DateTime)
					{
						string text5 = text;
						if (text5 != null)
						{
							if (!(text5 == "<"))
							{
								if (!(text5 == ">"))
								{
									if (!(text5 == "<="))
									{
										if (!(text5 == ">="))
										{
											if (!(text5 == "="))
											{
												if (text5 == "!=")
												{
													flag = ((DateTime)obj != dateTime);
												}
											}
											else
											{
												flag = ((DateTime)obj == dateTime);
											}
										}
										else
										{
											flag = ((DateTime)obj >= dateTime);
										}
									}
									else
									{
										flag = ((DateTime)obj <= dateTime);
									}
								}
								else
								{
									flag = ((DateTime)obj > dateTime);
								}
							}
							else
							{
								flag = ((DateTime)obj < dateTime);
							}
						}
					}
					else if (obj is int)
					{
						string text5 = text;
						if (text5 != null)
						{
							if (!(text5 == "<"))
							{
								if (!(text5 == ">"))
								{
									if (!(text5 == "<="))
									{
										if (!(text5 == ">="))
										{
											if (!(text5 == "="))
											{
												if (text5 == "!=")
												{
													flag = ((int)obj != num);
												}
											}
											else
											{
												flag = ((int)obj == num);
											}
										}
										else
										{
											flag = ((int)obj >= num);
										}
									}
									else
									{
										flag = ((int)obj <= num);
									}
								}
								else
								{
									flag = ((int)obj > num);
								}
							}
							else
							{
								flag = ((int)obj < num);
							}
						}
					}
					else
					{
						string text5 = text;
						if (text5 != null)
						{
							if (!(text5 == "<"))
							{
								if (!(text5 == ">"))
								{
									if (!(text5 == "<="))
									{
										if (!(text5 == ">="))
										{
											if (!(text5 == "="))
											{
												if (text5 == "!=")
												{
													flag = (obj.ToString().CompareTo(text3) != 0);
												}
											}
											else
											{
												flag = (obj.ToString().CompareTo(text3) == 0);
											}
										}
										else
										{
											flag = (obj.ToString().CompareTo(text3) >= 0);
										}
									}
									else
									{
										flag = (obj.ToString().CompareTo(text3) <= 0);
									}
								}
								else
								{
									flag = (obj.ToString().CompareTo(text3) > 0);
								}
							}
							else
							{
								flag = (obj.ToString().CompareTo(text3) < 0);
							}
						}
					}
				}
				if (flag)
				{
					arrayList.Add(dataRow);
				}
			}
			foreach (object obj2 in arrayList)
			{
				DataRow row = (DataRow)obj2;
				table.Rows.Remove(row);
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00015C14 File Offset: 0x00014C14
		public static DateTime ParseDateTime(string s)
		{
			if (s.Trim().Length > 0)
			{
				try
				{
					return DateTime.Parse(s);
				}
				catch
				{
					return DateTime.MinValue;
				}
			}
			return DateTime.MinValue;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00015C68 File Offset: 0x00014C68
		public static int ParseInt(string s)
		{
			string text = "";
			foreach (char c in s)
			{
				if (char.IsDigit(c))
				{
					text += c;
				}
			}
			if (text.Length > 0)
			{
				try
				{
					return int.Parse(text);
				}
				catch
				{
					return 0;
				}
			}
			return 0;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00015CFC File Offset: 0x00014CFC
		public static void CopyColumns(ref Report report, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar, params string[] colFromNameCommaColToNames)
		{
			DataTable table = report.GetCurrentDataView().Table;
			if (SetupSubProgressBar != null)
			{
				SetupSubProgressBar(0, table.Rows.Count * colFromNameCommaColToNames.Length);
			}
			foreach (string text in colFromNameCommaColToNames)
			{
				int num = text.IndexOf(',');
				if (num > 0)
				{
					string columnName = text.Substring(0, num);
					string text2 = text.Substring(num + 1);
					if (!table.Columns.Contains(text2))
					{
						table.Columns.Add(text2);
					}
					foreach (object obj in table.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						dataRow[text2] = dataRow[columnName].ToString();
					}
				}
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00015E30 File Offset: 0x00014E30
		public static void IfThenElse(ref Report report, string colNameToMatch, string valToMatch, string colNameToSet_ONTRUE, string colValueToSet_ONTRUE, string colNameToSet_ONFALSE, string colValueToSet_ONFALSE, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			if (SetupSubProgressBar != null)
			{
				SetupSubProgressBar(0, table.Rows.Count);
			}
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string text = dataRow[colNameToMatch].ToString().Trim().ToLower();
				if (text.CompareTo(valToMatch) == 0)
				{
					dataRow[colNameToSet_ONTRUE] = colValueToSet_ONTRUE;
				}
				else
				{
					dataRow[colValueToSet_ONFALSE] = colValueToSet_ONFALSE;
				}
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00015F14 File Offset: 0x00014F14
		public static void DateAdd(ref Report report, string colName, char datePart, string amountToAddString, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			if (SetupSubProgressBar != null)
			{
				SetupSubProgressBar(0, table.Rows.Count);
			}
			bool flag;
			int num;
			string columnName;
			if (amountToAddString[0] == '[')
			{
				flag = true;
				num = 0;
				columnName = amountToAddString.Substring(1, amountToAddString.Length - 2);
			}
			else
			{
				flag = false;
				num = int.Parse(amountToAddString);
				columnName = "";
			}
			bool flag2 = table.Columns[colName].DataType == typeof(DateTime);
			int num2 = 0;
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num3 = ReportFunction.ShowIncrementAmount(ref num2);
				if (num3 > 0 && IncrementSubProgressBar != null)
				{
					IncrementSubProgressBar(num3);
				}
				DateTime dateTime;
				if (dataRow[colName] == DBNull.Value)
				{
					dateTime = DateTime.MinValue;
				}
				else if (flag2)
				{
					dateTime = (DateTime)dataRow[colName];
				}
				else
				{
					string text = dataRow[colName].ToString().Trim();
					if (text.Length > 0)
					{
						try
						{
							dateTime = DateTime.Parse(text);
						}
						catch
						{
							dateTime = DateTime.MinValue;
						}
					}
					else
					{
						dateTime = DateTime.MinValue;
					}
				}
				if (dateTime != DateTime.MinValue)
				{
					int num4;
					if (flag)
					{
						string text = dataRow[columnName].ToString().Trim();
						if (text.Length > 0)
						{
							try
							{
								num4 = int.Parse(text);
							}
							catch
							{
								num4 = 0;
							}
						}
						else
						{
							num4 = 0;
						}
					}
					else
					{
						num4 = num;
					}
					if (datePart <= 'd')
					{
						if (datePart != 'M')
						{
							if (datePart == 'd')
							{
								dateTime = dateTime.AddDays((double)num4);
							}
						}
						else
						{
							dateTime = dateTime.AddMonths(num4);
						}
					}
					else if (datePart != 'm')
					{
						if (datePart == 'y')
						{
							dateTime = dateTime.AddYears(num4);
						}
					}
					else
					{
						dateTime = dateTime.AddMinutes((double)num4);
					}
					if (flag2)
					{
						dataRow[colName] = dateTime;
					}
					else
					{
						dataRow[colName] = dateTime.ToString("yyyy-MM-dd");
					}
				}
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00016208 File Offset: 0x00015208
		public static void Split2(ref Report report, string colName, string splitString, string[] newColNames, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			int count = table.Columns.Count;
			foreach (string columnName in newColNames)
			{
				table.Columns.Add(columnName);
			}
			if (SetupSubProgressBar != null)
			{
				SetupSubProgressBar(0, table.Rows.Count);
			}
			int num = 0;
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num2 = ReportFunction.ShowIncrementAmount(ref num);
				if (num2 > 0 && IncrementSubProgressBar != null)
				{
					IncrementSubProgressBar(num2);
				}
				string text = dataRow[colName].ToString().Trim();
				string[] array = text.Split(splitString.ToCharArray());
				for (int j = 0; j < array.Length; j++)
				{
					int num3 = count + j;
					if (num3 >= table.Columns.Count)
					{
						table.Columns.Add("temp" + j.ToString());
					}
					dataRow[num3] = array[j];
				}
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00016394 File Offset: 0x00015394
		public static int ShowIncrementAmount(ref int counter)
		{
			counter++;
			int result;
			if (counter % 1500 == 0)
			{
				result = 1500;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000163C8 File Offset: 0x000153C8
		public static DataTable Parse(TextReader stream, bool headers)
		{
			DataTable dataTable = new DataTable();
			CsvStream csvStream = new CsvStream(stream);
			string[] nextRow = csvStream.GetNextRow();
			DataTable result;
			if (nextRow == null)
			{
				result = null;
			}
			else
			{
				if (headers)
				{
					foreach (string text in nextRow)
					{
						if (text != null && text.Length > 0 && !dataTable.Columns.Contains(text))
						{
							dataTable.Columns.Add(text, typeof(string));
						}
						else
						{
							dataTable.Columns.Add(ReportFunction.GetNextColumnHeader(dataTable), typeof(string));
						}
					}
					nextRow = csvStream.GetNextRow();
				}
				while (nextRow != null)
				{
					while (nextRow.Length > dataTable.Columns.Count)
					{
						dataTable.Columns.Add(ReportFunction.GetNextColumnHeader(dataTable), typeof(string));
					}
					dataTable.Rows.Add(nextRow);
					nextRow = csvStream.GetNextRow();
				}
				result = dataTable;
			}
			return result;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000164F0 File Offset: 0x000154F0
		public static void ImportCsvFile(ref Report report, string filename, bool headers)
		{
			TextReader stream = new StreamReader(filename, Encoding.Default);
			DataTable dataTable = ReportFunction.Parse(stream, headers);
			if (dataTable == null)
			{
				dataTable = new DataTable();
			}
			dataTable.Columns.Add("NoResults");
			report.AddResult(dataTable.DefaultView);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00016544 File Offset: 0x00015544
		public static object ConsumeWebService0(DataView dv, string url, string serviceName, string methodName, string extraInfo, object[] args)
		{
			object result;
			try
			{
				object[] array = new object[args.Length];
				for (int i = 0; i < args.Length; i++)
				{
					if (args[i] is string)
					{
						string text = (string)args[i];
						if (text.Length > 0 && text[0] == '@')
						{
							string text2 = args[i].ToString().Substring(1);
							if (!dv.Table.Columns.Contains(text2))
							{
								text2 = args[1].ToString();
							}
							array[i] = dv.Table.Rows[0][text2];
						}
						else if (text.IndexOf("Guid(") == 0)
						{
							string g = text.Substring(5, text.Length - 6);
							array[i] = new Guid(g);
						}
						else
						{
							array[i] = args[i];
						}
					}
					else
					{
						array[i] = args[i];
					}
				}
				object obj = WsProxy.CallWebService(url, serviceName, methodName, extraInfo, array);
				result = obj;
			}
			catch (Exception ex)
			{
				ReportFunction.MessageBoxShow(ex.ToString());
				result = null;
			}
			return result;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000166A0 File Offset: 0x000156A0
		public static void ConsumeWebService(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, ref Report report, string url, string serviceName, string methodName, string extraInfo, string[] args, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			try
			{
				DataView currentDataView = report.GetCurrentDataView();
				object obj = ReportFunction.ConsumeWebService0(currentDataView, url, serviceName, methodName, extraInfo, args);
				if (obj == null)
				{
					ReportFunction.MessageBoxShow("NULL");
				}
				else if (obj is string)
				{
					DataTable dataTable = new DataTable();
					dataTable.Columns.Add("xml");
					dataTable.Rows.Add(new object[]
					{
						(string)obj
					});
					report.AddResult(dataTable.DefaultView);
				}
				else if (obj is XmlNode)
				{
					XmlNode xmlNode = (XmlNode)obj;
					MemoryStream memoryStream = new MemoryStream();
					StreamWriter streamWriter = new StreamWriter(memoryStream);
					streamWriter.Write(xmlNode.OuterXml);
					streamWriter.Flush();
					memoryStream.Position = 0L;
					DataSet dataSet = new DataSet();
					dataSet.ReadXml(memoryStream);
					memoryStream.Close();
					report.AddResult(dataSet);
				}
				else
				{
					ReportFunction.MessageBoxShow(obj.GetType().ToString());
				}
			}
			catch (Exception ex)
			{
				ReportFunction.MessageBoxShow(ex.ToString());
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000167FC File Offset: 0x000157FC
		public static void MakeATableTheCurrentTable(ref Report report, string tableName)
		{
			report.MakeATableTheCurrentTable(tableName);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00016808 File Offset: 0x00015808
		private static DataTable GetOleDbTables(OleDbConnection conn)
		{
			try
			{
				conn.Open();
				return conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[]
				{
					null,
					null,
					null,
					"TABLE"
				});
			}
			catch (OleDbException ex)
			{
			}
			finally
			{
				conn.Close();
			}
			return null;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00016874 File Offset: 0x00015874
		private static string GetColNameAndDbTypeString(string colname, Type type, Type byteArrayType)
		{
			string result;
			if (type == typeof(int))
			{
				result = colname + " INT";
			}
			else if (type == typeof(string))
			{
				result = colname + " TEXT";
			}
			else if (type == byteArrayType)
			{
				result = colname + " VARBINARY(8000)";
			}
			else if (type == typeof(bool))
			{
				result = colname + " BIT";
			}
			else
			{
				result = colname + " TEXT";
			}
			return result;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00016918 File Offset: 0x00015918
		public static void WriteTableToOleDbDatabase(ref Report report, string connectionString, string tableName, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			OleDbConnection oleDbConnection = new OleDbConnection(connectionString);
			OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("", oleDbConnection);
			DataView currentDataView = report.GetCurrentDataView();
			if (currentDataView.Count >= 1)
			{
				if (SetupSubProgressBar != null)
				{
					SetupSubProgressBar(0, currentDataView.Count);
				}
				byte[] array = new byte[0];
				Type type = array.GetType();
				DataTable oleDbTables = ReportFunction.GetOleDbTables(oleDbConnection);
				if (oleDbTables != null)
				{
					string strB = tableName.ToLower().Trim();
					bool flag = false;
					foreach (object obj in oleDbTables.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						string text = dataRow["table"].ToString().Trim().ToLower();
						if (text.CompareTo(strB) == 0)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						oleDbDataAdapter.SelectCommand.CommandText = "CREATE TABLE " + tableName + " (";
						for (int i = 0; i < currentDataView.Table.Columns.Count; i++)
						{
							string columnName = currentDataView.Table.Columns[i].ColumnName;
							Type dataType = currentDataView.Table.Columns[i].DataType;
							if (i > 0)
							{
								OleDbCommand selectCommand = oleDbDataAdapter.SelectCommand;
								selectCommand.CommandText += ",";
							}
							OleDbCommand selectCommand2 = oleDbDataAdapter.SelectCommand;
							selectCommand2.CommandText += ReportFunction.GetColNameAndDbTypeString(columnName, dataType, type);
						}
						OleDbCommand selectCommand3 = oleDbDataAdapter.SelectCommand;
						selectCommand3.CommandText += ")";
						oleDbDataAdapter.Fill(new DataTable());
					}
				}
				oleDbDataAdapter.SelectCommand.CommandText = "TRUNCATE TABLE " + tableName;
				oleDbDataAdapter.Fill(new DataTable());
				oleDbDataAdapter.SelectCommand.CommandText = "SELECT * FROM " + tableName;
				DataTable dataTable = new DataTable();
				oleDbDataAdapter.Fill(dataTable);
				if (dataTable.Rows.Count > 0)
				{
					oleDbDataAdapter.SelectCommand.CommandText = "DELETE FROM " + tableName;
					oleDbDataAdapter.Fill(new DataTable());
					oleDbDataAdapter.SelectCommand.CommandText = "SELECT * FROM " + tableName;
					dataTable = new DataTable();
					oleDbDataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0)
					{
						throw new Exception("Can't empty table.");
					}
				}
				dataTable = new DataTable();
				oleDbDataAdapter.SelectCommand.CommandText = "SELECT * FROM " + tableName + " WHERE 1=0";
				oleDbDataAdapter.Fill(dataTable);
				string text2 = "";
				string text3 = "";
				for (int i = 0; i < currentDataView.Table.Columns.Count; i++)
				{
					string columnName = currentDataView.Table.Columns[i].ColumnName;
					if (text2.Length > 0)
					{
						text2 += ",";
						text3 += ",";
					}
					text2 += columnName;
					text3 = text3 + "@" + columnName;
					if (!dataTable.Columns.Contains(columnName))
					{
						DataRow row = currentDataView[0].Row;
						Type type2 = row[columnName].GetType();
						oleDbDataAdapter.SelectCommand.CommandText = "ALTER TABLE " + tableName + " ADD COLUMN " + ReportFunction.GetColNameAndDbTypeString(columnName, type2, type);
						oleDbDataAdapter.Fill(new DataTable());
					}
				}
				oleDbConnection.Open();
				OleDbTransaction oleDbTransaction = oleDbConnection.BeginTransaction();
				OleDbCommand oleDbCommand = oleDbConnection.CreateCommand();
				oleDbCommand.Connection = oleDbConnection;
				oleDbCommand.Transaction = oleDbTransaction;
				try
				{
					for (int i = 0; i < currentDataView.Count; i++)
					{
						DataRow row2 = currentDataView[i].Row;
						if (IncrementSubProgressBar != null && i % 25 == 0)
						{
							IncrementSubProgressBar(25);
						}
						oleDbCommand.CommandText = string.Concat(new string[]
						{
							"INSERT INTO ",
							tableName,
							" (",
							text2,
							") (",
							text3,
							")"
						});
						oleDbCommand.Parameters.Clear();
						for (int j = 0; j < currentDataView.Table.Columns.Count; j++)
						{
							string columnName2 = currentDataView.Table.Columns[j].ColumnName;
							string parameterName = "@" + columnName2;
							oleDbCommand.Parameters.AddWithValue(parameterName, row2[columnName2]);
							oleDbCommand.ExecuteNonQuery();
						}
					}
					oleDbTransaction.Commit();
				}
				catch (Exception ex)
				{
					try
					{
						oleDbTransaction.Rollback();
					}
					catch (OleDbException ex2)
					{
						if (oleDbTransaction.Connection != null)
						{
							throw new Exception("An exception of type " + ex2.GetType() + " was encountered while attempting to roll back the transaction.");
						}
					}
					throw new Exception("An exception of type " + ex.GetType() + " was encountered while inserting the data.");
				}
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00016F2C File Offset: 0x00015F2C
		public static void AddStudentsToMasterStudentTableInMemory(ref Report report, string studentNumColName, string memoryTableName, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataView dataView = report.GetDataView(memoryTableName);
			DataTable dataTable;
			if (dataView == null)
			{
				dataTable = new DataTable(memoryTableName);
				dataTable.Columns.Add(studentNumColName);
				dataView = new DataView(dataTable);
				report.AddResultNotPrimary(dataView, memoryTableName);
			}
			dataTable = dataView.Table;
			List<string> list = new List<string>(dataTable.Rows.Count);
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				list.Add((string)dataRow[studentNumColName]);
			}
			DataView dataView2 = new DataView(currentDataView.Table);
			dataView2.Sort = studentNumColName;
			string strB = studentNumColName.ToLower();
			if (SetupSubProgressBar != null)
			{
				SetupSubProgressBar(0, dataView2.Count);
			}
			int j;
			for (int i = 0; i < dataView2.Count; i = j)
			{
				DataRow row = dataView2[i].Row;
				string text = (string)row[studentNumColName];
				for (j = i + 1; j < dataView2.Count; j++)
				{
					string text2 = (string)dataView2[j].Row[studentNumColName];
					if (text2.CompareTo(text) != 0)
					{
						break;
					}
				}
				foreach (object obj2 in dataView2.Table.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj2;
					string columnName = dataColumn.ColumnName;
					if (columnName.ToLower().CompareTo(strB) != 0)
					{
						if (!dataTable.Columns.Contains(columnName))
						{
							dataTable.Columns.Add(columnName);
						}
					}
				}
				bool flag;
				DataRow dataRow2;
				if (!list.Contains(text))
				{
					list.Add(text);
					flag = true;
					dataRow2 = dataTable.NewRow();
				}
				else
				{
					DataRow[] array = dataTable.Select(string.Concat(new string[]
					{
						"[",
						studentNumColName,
						"]='",
						text,
						"'"
					}));
					dataRow2 = ((array != null && array.Length > 0) ? array[0] : null);
					if (dataRow2 == null)
					{
						flag = true;
						dataRow2 = dataTable.NewRow();
					}
					else
					{
						flag = false;
					}
				}
				for (int k = 0; k < currentDataView.Table.Columns.Count; k++)
				{
					string columnName = currentDataView.Table.Columns[k].ColumnName;
					dataRow2[columnName] = row[columnName];
				}
				if (flag)
				{
					dataTable.Rows.Add(dataRow2);
				}
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0001727C File Offset: 0x0001627C
		public static void NameCurrentTable(ref Report report, string newName)
		{
			ReportFunction.NameCurrentTable(ref report, newName, new List<string>());
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0001728C File Offset: 0x0001628C
		public static void NameCurrentTable(ref Report report, string newName, List<string> codes)
		{
			DataView currentDataView = report.GetCurrentDataView();
			if (codes.Contains("removeallothers"))
			{
				report.RemoveAllBut(currentDataView);
			}
			report.NameCurrentTable(newName);
			if (codes.Contains("copy"))
			{
				DataTable table = currentDataView.Table.Copy();
				DataView dataView = new DataView(table);
				dataView.Sort = currentDataView.Sort;
				report.AddResult(dataView);
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00017308 File Offset: 0x00016308
		public static void ExecuteCommandLine(ref Report report, string fileName, string arguments, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			ProcessStartInfo processStartInfo = (arguments.Length > 0) ? new ProcessStartInfo(fileName, arguments) : new ProcessStartInfo(fileName);
			processStartInfo.CreateNoWindow = true;
			Process process = Process.Start(processStartInfo);
			process.WaitForExit();
			process.Close();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0001734C File Offset: 0x0001634C
		public static void OnlyKeepFirstRows(ref Report report, string uniqueColNames, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			string[] array = uniqueColNames.Split(new char[]
			{
				','
			});
			DataView currentDataView = report.GetCurrentDataView();
			DataView dataView = new DataView(currentDataView.Table);
			dataView.Sort = uniqueColNames;
			ArrayList arrayList = new ArrayList();
			foreach (string value in array)
			{
				arrayList.Add(value);
			}
			DataTable dataTable = dataView.Table.Clone();
			int j = 0;
			while (j < dataView.Count)
			{
				DataRow row = dataView[j].Row;
				dataTable.Rows.Add(row.ItemArray);
				string uniqueRowString = ReportFunction.GetUniqueRowString(row, arrayList);
				while (j < dataView.Count)
				{
					DataRow row2 = dataView[j].Row;
					string uniqueRowString2 = ReportFunction.GetUniqueRowString(row2, arrayList);
					if (uniqueRowString2.CompareTo(uniqueRowString) != 0)
					{
						break;
					}
				}
			}
			report.ReplaceDataView(currentDataView, dataTable.DefaultView);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00017468 File Offset: 0x00016468
		private static bool IsCellTrue(DataRow dr, string colName)
		{
			return dr[colName] != DBNull.Value && Convert.ToBoolean(dr[colName]);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000174A0 File Offset: 0x000164A0
		public static void CrossReferenceWithAccommodations2(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, ref Report report, string cids, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			if (!table.Columns.Contains("UsingTemplate"))
			{
				table.Columns.Add("UsingTemplate", typeof(bool));
			}
			ReportFunction.LookupStudentMethod lookupStudentMethod;
			if (table.Columns.Contains("personid"))
			{
				lookupStudentMethod = ReportFunction.LookupStudentMethod.personid;
			}
			else if (table.Columns.Contains("student_no"))
			{
				table.Columns.Add("personid", typeof(int));
				foreach (object obj in table.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					string plainText = dataRow["student_no"].ToString();
					byte[] parameterValue = tripleDES.Encrypt(plainText);
					da.SelectCommand.CommandText = "SELECT personid FROM people WHERE isactive=1 AND student_no=@sne";
					da.SelectCommand.Parameters.Clear();
					da.SelectCommand.Parameters.Add("@sne", parameterValue);
					DataTable dataTable = new DataTable();
					da.Fill(dataTable);
					if (dataTable.Rows.Count > 0)
					{
						dataRow["personid"] = (int)dataRow["personid"];
					}
					else
					{
						dataRow["personid"] = 0;
					}
				}
				lookupStudentMethod = ReportFunction.LookupStudentMethod.personid;
			}
			else
			{
				lookupStudentMethod = ReportFunction.LookupStudentMethod.Unknown;
			}
			bool flag = table.Columns.Contains("lucourseid");
			if (lookupStudentMethod == ReportFunction.LookupStudentMethod.Unknown)
			{
				throw new Exception("Missing column!  Requires at least 'personid'.");
			}
			string commandText = "SELECT ad.*,p.firstname,p.lastname,p.student_no\r\n  FROM accommodationdataactive ad LEFT JOIN people p ON p.personid=ad.personid\r\n       LEFT JOIN dynamicscreencontrols dsc ON dsc.controlid=ad.controlid AND dsc.screennum=4\r\n  WHERE ad.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,','))\r\n        AND ad.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','))\r\n        AND p.isactive=1\r\n  ORDER BY ad.personid,ad.courseid,dsc.ordernum";
			int num;
			for (int i = 0; i < currentDataView.Count; i = num)
			{
				num = i + 100;
				if (num > currentDataView.Count)
				{
					num = currentDataView.Count;
				}
				StringBuilder stringBuilder = new StringBuilder();
				for (int j = i; j < num; j++)
				{
					DataRow dataRow = currentDataView[j].Row;
					if (j > i)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(dataRow["personid"].ToString());
				}
				da.SelectCommand.CommandText = commandText;
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@pids", stringBuilder.ToString());
				da.SelectCommand.Parameters.Add("@cids", cids);
				DataTable dataTable2 = new DataTable();
				da.Fill(dataTable2);
				dataTable2 = tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable2, new string[]
				{
					"altlongdescription"
				});
				for (int j = i; j < num; j++)
				{
					DataRow dataRow = currentDataView[j].Row;
					int num2 = (dataRow["personid"] == DBNull.Value) ? 0 : ((int)dataRow["personid"]);
					int num3 = flag ? ((dataRow["lucourseid"] == DBNull.Value) ? 0 : ((int)dataRow["lucourseid"])) : 0;
					DataRow[] array = dataTable2.Select("personid=" + num2.ToString() + " AND courseid=" + num3.ToString());
					if (array == null || array.Length < 1)
					{
						if (num3 > 0)
						{
							num3 = 0;
							dataRow["UsingTemplate"] = true;
							array = dataTable2.Select("personid=" + num2.ToString() + " AND courseid=" + num3.ToString());
						}
						else
						{
							dataRow["UsingTemplate"] = false;
						}
					}
					else
					{
						dataRow["UsingTemplate"] = (num3 == 0);
					}
					if (array != null && array.Length > 0)
					{
						foreach (DataRow dataRow2 in array)
						{
							string text = (dataRow2["controlcaption"] == DBNull.Value) ? "Unknown" : ((string)dataRow2["controlcaption"]);
							int num4 = text.IndexOf("~~");
							if (num4 > 0)
							{
								text = text.Substring(0, num4);
							}
							string text2 = ReportFunction.SanitizeColumnName(text);
							bool flag2 = dataRow2["valbytesisencrypted"] != DBNull.Value && Convert.ToBoolean(dataRow2["valbytesisencrypted"]);
							string text3;
							if (flag2)
							{
								if (dataRow2["valbytes"] != DBNull.Value)
								{
									text3 = tripleDES.Decrypt((byte[])dataRow2["valbytes"]);
								}
								else
								{
									text3 = "";
								}
							}
							else
							{
								text3 = dataRow2["valtext"].ToString();
							}
							if (string.IsNullOrEmpty(text3))
							{
								text3 = text;
							}
							if (dataRow2["altlongdescription"] != DBNull.Value)
							{
								string text4 = (string)dataRow2["altlongdescription"];
								if (!string.IsNullOrEmpty(text4))
								{
									if (!string.IsNullOrEmpty(text3))
									{
										text3 = text3 + ": " + text4;
									}
									else
									{
										text3 = text4;
									}
								}
							}
							if (!table.Columns.Contains(text2))
							{
								int num5 = (dataRow2["controlcode"] == DBNull.Value) ? 0 : ((int)dataRow2["controlcode"]);
								if (num5 == 2 || num5 == 700)
								{
									table.Columns.Add(text2, typeof(bool));
								}
								else
								{
									table.Columns.Add(text2);
								}
							}
							DataColumn dataColumn = table.Columns[text2];
							if (dataColumn.DataType == typeof(bool))
							{
								dataRow[text2] = true;
							}
							else
							{
								dataRow[text2] = text3;
							}
						}
					}
				}
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00017B78 File Offset: 0x00016B78
		private static string SanitizeColumnName(string name)
		{
			string arg = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
			string pattern = string.Format("[{0}]", arg);
			return Regex.Replace(name, pattern, "_");
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00017BB4 File Offset: 0x00016BB4
		public static void CrossReferenceWithAccommodations(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, ref Report report, string paramaters, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			string s = paramaters.ToUpper();
			string[] array = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(s, true);
			string text = array[0];
			string text2 = (array.Length > 1) ? array[1] : "";
			bool flag = text.IndexOf('E') >= 0;
			bool flag2 = text.IndexOf('P') >= 0;
			bool flag3 = text.IndexOf('O') >= 0;
			bool flag4 = text.IndexOf('R') >= 0;
			bool flag5 = text.IndexOf('A') >= 0 || text.Trim().Length < 1;
			bool flag6 = text.IndexOf('G') >= 0;
			bool flag7 = text.IndexOf('T') >= 0;
			bool flag8 = text.IndexOf('O') >= 0;
			bool flag9 = text.IndexOf('N') >= 0;
			bool flag10 = text.IndexOf('S') >= 0;
			bool flag11 = text.IndexOf('C') >= 0;
			bool flag12 = text.IndexOf('X') >= 0;
			bool flag13 = text2.IndexOf('E') >= 0;
			bool flag14 = text2.IndexOf('P') >= 0;
			bool flag15 = text2.IndexOf('O') >= 0;
			bool flag16 = text2.IndexOf('R') >= 0;
			bool flag17 = text2.IndexOf('G') >= 0;
			bool flag18 = text2.IndexOf('T') >= 0;
			bool flag19 = text2.IndexOf('O') >= 0;
			bool flag20 = text2.IndexOf('N') >= 0;
			bool flag21 = text2.IndexOf('S') >= 0;
			bool flag22 = text2.IndexOf('C') >= 0;
			bool flag23 = text2.IndexOf('X') >= 0;
			int columnIndex = currentDataView.Table.Columns.IndexOf("personid");
			int num = currentDataView.Table.Columns.IndexOf("lucourseid");
			currentDataView.Table.Columns.Add("Accommodations" + text);
			int columnIndex2 = currentDataView.Table.Columns.Count - 1;
			currentDataView.Table.Columns.Add("AccommodationsShort" + text);
			int columnIndex3 = currentDataView.Table.Columns.Count - 1;
			DataSet dataSet = new DataSet();
			foreach (object obj in currentDataView)
			{
				DataRowView dataRowView = (DataRowView)obj;
				DataRow row = dataRowView.Row;
				int pid = (int)row[columnIndex];
				int num2 = (num >= 0) ? ((int)row[num]) : 0;
				DataTable dataTable = ReportFunction.LoadAccommodations(pid, num2, da, tripleDES);
				if (dataTable.Rows.Count < 1 && num2 > 0)
				{
					dataTable = ReportFunction.LoadAccommodations(pid, 0, da, tripleDES);
				}
				string text3 = "";
				string text4 = "";
				foreach (object obj2 in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj2;
					int num3 = (dataRow["showonletter"] != DBNull.Value) ? ((int)dataRow["showonletter"]) : 0;
					int num4 = (dataRow["showonreport"] != DBNull.Value) ? ((int)dataRow["showonreport"]) : 0;
					bool flag24 = flag5 || (flag && (num3 & 2) == 2) || (flag2 && (num3 & 1) == 1) || (flag3 && (num3 & 4) == 4) || (flag4 && num4 > 0);
					if ((flag6 && ReportFunction.IsCellTrue(dataRow, "isgroup")) || (flag7 && ReportFunction.IsCellTrue(dataRow, "extratime")) || (flag8 && ReportFunction.IsCellTrue(dataRow, "other")) || (flag9 && ReportFunction.IsCellTrue(dataRow, "enlarged")) || (flag10 && ReportFunction.IsCellTrue(dataRow, "needsreaderscribe")) || (flag11 && ReportFunction.IsCellTrue(dataRow, "needscomputer")) || (flag12 && ReportFunction.IsCellTrue(dataRow, "isalone")))
					{
						flag24 = true;
					}
					if ((flag13 && (num3 & 2) == 2) || (flag14 && (num3 & 1) == 1) || (flag15 && (num3 & 4) == 4) || (flag16 && num4 > 0))
					{
						flag24 = false;
					}
					if ((flag17 && ReportFunction.IsCellTrue(dataRow, "isgroup")) || (flag18 && ReportFunction.IsCellTrue(dataRow, "extratime")) || (!flag19 && ReportFunction.IsCellTrue(dataRow, "other")) || (!flag20 && ReportFunction.IsCellTrue(dataRow, "enlarged")) || (!flag21 && ReportFunction.IsCellTrue(dataRow, "needsreaderscribe")) || (!flag22 && ReportFunction.IsCellTrue(dataRow, "needscomputer")) || (flag23 && ReportFunction.IsCellTrue(dataRow, "isalone")))
					{
						flag24 = false;
					}
					if (flag24)
					{
						int num5 = (dataRow["controlcode"] != DBNull.Value) ? ((int)dataRow["controlcode"]) : -1;
						int num6 = (dataRow["setting3"] != DBNull.Value) ? ((int)dataRow["setting3"]) : 0;
						string text5 = dataRow["controlcaption"].ToString();
						string str = dataRow["shortcode"].ToString();
						if (num5 == 1)
						{
							bool decrypt = num6 == 1;
							text5 = text5 + ": " + ClockWorkCore.BytesToString(dataRow, "strval", decrypt, tripleDES);
						}
						else if (num5 == 3)
						{
							if (num6 == 0)
							{
								int lookupGroupID = (dataRow["setting1"] != DBNull.Value) ? ((int)dataRow["setting1"]) : -1;
								DataTable lookupList = DynamicScreen.GetLookupList(lookupGroupID, true, -1, ref dataSet, da, false);
								if (lookupList != null)
								{
									text5 = text5 + ": " + DynamicScreen.GetLookupListValue(lookupList, (dataRow["intval"] != DBNull.Value) ? ((int)dataRow["intval"]) : -1);
								}
							}
							else
							{
								bool decrypt = num6 == -1;
								text5 = text5 + ": " + ClockWorkCore.BytesToString(dataRow, "strval", decrypt, tripleDES);
							}
						}
						if (text3.Length > 0)
						{
							text3 += ", ";
						}
						text3 += text5;
						if (text4.Length > 0)
						{
							text4 += ", ";
						}
						text4 += str;
					}
				}
				row[columnIndex2] = text3;
				row[columnIndex3] = text4;
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00018344 File Offset: 0x00017344
		private static DataTable LoadAccommodations(int pid, int lucid, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			da.SelectCommand.CommandText = "SELECT m.personid,m.controlid,m.controlvalue AS intval,null AS strval,getdate() AS dateval,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.controlcaption,a.shortcode,a.showonletter,a.showonemail,a.extratime,a.showonreport,dc.defaultvalue,a.extratime,a.isalone,a.needscomputer,a.needsreaderscribe,a.isgroup,a.tapedexams,a.other,a.enlarged FROM maininfoaccommodationps m LEFT JOIN dynamiccontrols dc ON dc.controlid=m.controlid LEFT JOIN accommodations a ON a.controlid=m.controlid WHERE m.personid=@pid AND m.courseid=@cid";
			UnivCommand selectCommand = da.SelectCommand;
			selectCommand.CommandText += " UNION SELECT o.personid,o.controlid,0 AS intval,o.controlvalue AS strval,getdate() as dateval,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.controlcaption,a.shortcode,a.showonletter,a.showonemail,a.extratime,a.showonreport,dc.defaultvalue,a.extratime,a.isalone,a.needscomputer,a.needsreaderscribe,a.isgroup,a.tapedexams,a.other,a.enlarged FROM otherinfoaccommodationps o LEFT JOIN dynamiccontrols dc ON dc.controlid=o.controlid LEFT JOIN accommodations a ON a.controlid=o.controlid WHERE o.personid=@pid AND o.courseid=@cid";
			UnivCommand selectCommand2 = da.SelectCommand;
			selectCommand2.CommandText += " UNION SELECT d.personid,d.controlid,0 AS intval,null AS strval,d.controlvalue AS dateval,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.controlcaption,a.shortcode,a.showonletter,a.showonemail,a.extratime,a.showonreport,dc.defaultvalue,a.extratime,a.isalone,a.needscomputer,a.needsreaderscribe,a.isgroup,a.tapedexams,a.other,a.enlarged FROM datetimeinfoaccommodationps d LEFT JOIN dynamiccontrols dc ON dc.controlid=d.controlid LEFT JOIN accommodations a ON a.controlid=d.controlid WHERE d.personid=@pid AND d.courseid=@cid";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@pid", pid);
			da.SelectCommand.Parameters.Add("@cid", lucid);
			DataTable dataTable = new DataTable();
			string text;
			da.Fill(dataTable, out text);
			if (text != null && text.Length > 0)
			{
				ReportFunction.MessageBoxShow(text);
			}
			return dataTable;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00018418 File Offset: 0x00017418
		public static void BreakdownCheckboxCounts(ref Report report, string colNamesTildeUniqueColNames, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			int num = colNamesTildeUniqueColNames.IndexOf('~');
			string text;
			string text2;
			if (num >= 0)
			{
				text = ((num > 0) ? colNamesTildeUniqueColNames.Substring(0, num) : "");
				text2 = ((num + 1 < colNamesTildeUniqueColNames.Length) ? colNamesTildeUniqueColNames.Substring(num + 1) : "");
			}
			else
			{
				text = colNamesTildeUniqueColNames;
				text2 = "student_no";
			}
			string[] array = text2.Split(new char[]
			{
				','
			});
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			if (text.Length < 1)
			{
				foreach (object obj in currentDataView.Table.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					if (dataColumn.DataType == typeof(bool))
					{
						if (text.Length > 0)
						{
							text += ',';
						}
						text += dataColumn.ColumnName;
					}
					else if (dataColumn.DataType == typeof(string) && Array.IndexOf<string>(array, dataColumn.ColumnName) < 0)
					{
						bool flag = true;
						foreach (object obj2 in table.Rows)
						{
							DataRow dataRow = (DataRow)obj2;
							if (dataRow[dataColumn] != DBNull.Value)
							{
								string text3 = dataRow[dataColumn].ToString().ToLower();
								if (text3.Length > 0 && "trueyes".IndexOf(text3) < 0)
								{
									flag = false;
									break;
								}
							}
						}
						if (flag)
						{
							if (text.Length > 0)
							{
								text += ',';
							}
							text += dataColumn.ColumnName;
						}
					}
				}
			}
			string[] array2 = text.Split(new char[]
			{
				','
			});
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Field");
			dataTable.Columns.Add("Count", typeof(int));
			foreach (string text4 in array2)
			{
				ArrayList arrayList = new ArrayList();
				int num2 = 0;
				foreach (object obj3 in currentDataView)
				{
					DataRowView dataRowView = (DataRowView)obj3;
					DataRow dataRow = dataRowView.Row;
					string text5 = "";
					for (int j = 0; j < array.Length; j++)
					{
						text5 = text5 + j.ToString() + ":" + dataRow[array[j]].ToString().Trim().ToLower();
					}
					if (dataRow[text4] != DBNull.Value && ((dataRow[text4] is bool && (bool)dataRow[text4]) || "trueyes".IndexOf(dataRow[text4].ToString().ToLower()) >= 0) && !arrayList.Contains(text5))
					{
						arrayList.Add(text5);
						num2++;
					}
				}
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2[0] = text4;
				dataRow2[1] = num2;
				dataTable.Rows.Add(dataRow2);
			}
			report.AddResult(dataTable.DefaultView);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000188A8 File Offset: 0x000178A8
		public static DataView AddBooleanCountAcrossColumns(DataView dv, string colNames, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataTable table = dv.Table;
			if (colNames.Length < 1)
			{
				foreach (object obj in dv.Table.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					if (dataColumn.DataType == typeof(bool))
					{
						if (colNames.Length > 0)
						{
							colNames += ',';
						}
						colNames += dataColumn.ColumnName;
					}
				}
			}
			string[] array = colNames.Split(new char[]
			{
				','
			});
			ReportFunction.AddColumn(ref table, "MultipleCalculated", typeof(int));
			int columnIndex = table.Columns.Count - 1;
			Type typeFromHandle = typeof(bool);
			foreach (object obj2 in dv)
			{
				DataRowView dataRowView = (DataRowView)obj2;
				DataRow row = dataRowView.Row;
				int num = 0;
				foreach (string text in array)
				{
					if (table.Columns[text].DataType == typeFromHandle)
					{
						if (row[text] != DBNull.Value && (bool)row[text])
						{
							num++;
						}
					}
					else if (row[text] != DBNull.Value && row[text].ToString().Trim().Length > 0)
					{
						num++;
					}
				}
				row[columnIndex] = num;
			}
			return dv;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00018B1C File Offset: 0x00017B1C
		public static void BreakdownMultiple(ref Report report, string colNamesStr, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			string[] array = colNamesStr.Split(new char[]
			{
				','
			});
			ArrayList[] array2 = new ArrayList[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = new ArrayList();
			}
			if (SetupSubProgressBar != null)
			{
				SetupSubProgressBar(0, array2.Length * currentDataView.Count);
			}
			DataView dataView = new DataView(currentDataView.Table);
			dataView.Sort = colNamesStr;
			for (int i = 0; i < array.Length; i++)
			{
				string columnName = array[i];
				ArrayList arrayList = array2[i];
				foreach (object obj in dataView)
				{
					DataRowView dataRowView = (DataRowView)obj;
					DataRow row = dataRowView.Row;
					string text = row[columnName].ToString().Trim();
					if (!arrayList.Contains(text))
					{
						arrayList.Add(text);
					}
				}
			}
			DataTable dataTable = new DataTable();
			for (int i = 0; i < array.Length; i++)
			{
				dataTable.Columns.Add(array[i]);
			}
			dataTable.Columns.Add("Count", typeof(int));
			int[] array3 = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array3[i] = 0;
			}
			bool flag;
			do
			{
				object[] array4 = new object[array.Length + 1];
				for (int i = 0; i < array.Length; i++)
				{
					ArrayList arrayList = array2[i];
					if (array3[i] < arrayList.Count)
					{
						array4[i] = (string)arrayList[array3[i]];
					}
					else
					{
						array4[i] = "";
					}
				}
				array4[array4.Length - 1] = 0;
				dataTable.Rows.Add(array4);
				flag = false;
				for (int i = array.Length - 1; i >= 0; i--)
				{
					ArrayList arrayList = array2[i];
					int num = array3[i] + 1;
					if (num < arrayList.Count)
					{
						array3[i] = num;
						break;
					}
					array3[i] = 0;
					if (i == 0)
					{
						flag = true;
						break;
					}
				}
			}
			while (!flag);
			foreach (object obj2 in dataView)
			{
				DataRowView dataRowView = (DataRowView)obj2;
				DataRow row = dataRowView.Row;
				bool flag2 = false;
				for (int j = 0; j < dataTable.Rows.Count; j++)
				{
					DataRow dataRow = dataTable.Rows[j];
					bool flag3 = true;
					for (int k = 0; k < array.Length; k++)
					{
						string text2 = row[array[k]].ToString().Trim();
						string strB = (string)dataRow[k];
						if (text2.CompareTo(strB) != 0)
						{
							flag3 = false;
							break;
						}
					}
					if (flag3)
					{
						int num2 = (int)dataRow[dataTable.Columns.Count - 1];
						dataRow[dataTable.Columns.Count - 1] = num2 + 1;
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
				}
			}
			report.ReplaceDataView(currentDataView, dataTable.DefaultView);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00018F34 File Offset: 0x00017F34
		private static string[] SplitStrings(string s, string splitChars)
		{
			ArrayList arrayList = new ArrayList();
			int length = splitChars.Length;
			int num = 0;
			int i = s.IndexOf(splitChars);
			if (i < 0)
			{
				arrayList.Add(s);
			}
			while (i >= 0)
			{
				int num2 = i - num;
				if (num2 > 0)
				{
					arrayList.Add(splitChars.Substring(num, num2));
				}
				num = i + length;
				i = s.IndexOf(splitChars, num);
			}
			string[] array = new string[arrayList.Count];
			for (int j = 0; j < arrayList.Count; j++)
			{
				array[j] = (string)arrayList[j];
			}
			return array;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00018FF4 File Offset: 0x00017FF4
		private static string GetUniqueRowString(DataRow dr, ArrayList colNames)
		{
			object[] itemArray = dr.ItemArray;
			string text = "";
			for (int i = 0; i < colNames.Count; i++)
			{
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					i.ToString(),
					".",
					dr[(string)colNames[i]].ToString().ToLower(),
					"."
				});
			}
			return text;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00019084 File Offset: 0x00018084
		public static void RemoveDuplicateItemsFromListInOneCell(ref Report report, string parametersStr, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataView dataView = new DataView(currentDataView.Table.Copy());
			dataView.Sort = currentDataView.Sort;
			string[] array = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(parametersStr, true);
			string[][] array2 = new string[array.Length][];
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				int num = text.IndexOf(',');
				array2[i] = new string[2];
				if (num < 0)
				{
					array2[i][0] = text;
					array2[i][1] = ",";
				}
				else
				{
					array2[i][0] = text.Substring(0, num);
					array2[i][1] = text.Substring(num + 1);
				}
			}
			if (SetupSubProgressBar != null)
			{
				SetupSubProgressBar(0, dataView.Count);
			}
			foreach (object obj in dataView)
			{
				DataRowView dataRowView = (DataRowView)obj;
				if (IncrementSubProgressBar != null)
				{
					IncrementSubProgressBar(1);
				}
				DataRow row = dataRowView.Row;
				for (int i = 0; i < array2.Length; i++)
				{
					ArrayList arrayList = new ArrayList();
					ArrayList arrayList2 = new ArrayList();
					string columnName = array2[i][0];
					string[] array3 = row[columnName].ToString().Trim().Split(array2[i][1].ToCharArray());
					foreach (string text2 in array3)
					{
						string text3 = text2.Trim();
						if (text3.Length > 0)
						{
							string text4 = text3.ToLower();
							if (!arrayList2.Contains(text4))
							{
								arrayList2.Add(text4);
								arrayList.Add(text3);
							}
						}
					}
					if (arrayList.Count > 0)
					{
						row[columnName] = ReportFunction.ArrayListToString(arrayList, true);
					}
					else
					{
						row[columnName] = "";
					}
				}
			}
			report.ReplaceDataView(currentDataView, dataView);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000192F4 File Offset: 0x000182F4
		public static StringDictionary ParseNameEqualsValuePairs_newlinedelimitered(string parameters)
		{
			StringDictionary stringDictionary = new StringDictionary();
			string[] array = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(parameters, true);
			foreach (string text in array)
			{
				int num = text.IndexOf('=');
				if (num > 0)
				{
					string key = text.Substring(0, num);
					num++;
					string value = (num < text.Length) ? text.Substring(num) : "";
					stringDictionary.Add(key, value);
				}
			}
			return stringDictionary;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00019388 File Offset: 0x00018388
		public static int GetArg_Int(StringDictionary args, string name, int defaultVal)
		{
			string text = args[name];
			int result;
			if (text == null)
			{
				result = defaultVal;
			}
			else if (text.Trim().Length < 1)
			{
				result = defaultVal;
			}
			else
			{
				try
				{
					result = int.Parse(text);
				}
				catch
				{
					result = defaultVal;
				}
			}
			return result;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000193EC File Offset: 0x000183EC
		public static string GetArg_String(StringDictionary args, string name)
		{
			return ReportFunction.GetArg_String(args, name, "");
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0001940C File Offset: 0x0001840C
		public static string GetArg_String(StringDictionary args, string name, string defaultValue)
		{
			string text = args[name];
			return (text == null) ? defaultValue : text;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00019430 File Offset: 0x00018430
		public static bool GetArg_Bool(StringDictionary args, string name)
		{
			return ReportFunction.GetArg_Bool(args, name, false);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0001944C File Offset: 0x0001844C
		public static bool GetArg_Bool(StringDictionary args, string name, bool defaultValue)
		{
			string text = args[name];
			bool result;
			if (text == null)
			{
				result = defaultValue;
			}
			else
			{
				text = text.Trim().ToLower();
				result = (text.Length > 0 && (text.CompareTo("1") == 0 || text.CompareTo("y") == 0 || text.CompareTo("yes") == 0 || text.CompareTo("t") == 0 || text.CompareTo("true") == 0));
			}
			return result;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000194D4 File Offset: 0x000184D4
		public static DataTable BatchEmailWithMailMerge3(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, string xml, DataTable t, bool suppressGui)
		{
			BatchEmail batchEmail = new BatchEmail(da, tripleDES, xml);
			if (batchEmail.PromptUser && !suppressGui)
			{
				BatchEmailOptions batchEmailOptions = new BatchEmailOptions();
				DialogResult dialogResult = batchEmailOptions.ShowDialog();
				BatchEmail.BatchEmailSendMode selectedSendMode = batchEmailOptions.SelectedSendMode;
				if (selectedSendMode == BatchEmail.BatchEmailSendMode.DontSendEmails)
				{
					return t;
				}
				batchEmail.SendMode = selectedSendMode;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (object obj in t.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				dictionary.Add(dataColumn.ColumnName, "");
			}
			ReportFunction.AddDataColumn(ref t, "emailSuccessfull", typeof(bool));
			ReportFunction.AddDataColumn(ref t, "emailResult");
			ReportFunction.AddDataColumn(ref t, "emailDetail");
			foreach (object obj2 in t.Rows)
			{
				DataRow dataRow = (DataRow)obj2;
				ReportFunction.ClearDictionary(dictionary);
				for (int i = 0; i < dataRow.Table.Columns.Count - 2; i++)
				{
					dictionary[t.Columns[i].ColumnName] = dataRow[i].ToString();
				}
				EmailResult emailResult = batchEmail.SendEmail(dictionary);
				Exception exception = emailResult.Exception;
				string text = emailResult.Message ?? "";
				dataRow["emailSuccessfull"] = emailResult.Worked.ToString();
				dataRow["emailResult"] = (string.IsNullOrEmpty(text) ? "Success" : text);
				dataRow["emaildetail"] = emailResult.Email.ToString();
				if (batchEmail.DelayBetweenEmails > 0)
				{
					Thread.Sleep(batchEmail.DelayBetweenEmails * 1000);
				}
				if (batchEmail.SendMode == BatchEmail.BatchEmailSendMode.SendFirstEmail)
				{
					batchEmail.SendMode = BatchEmail.BatchEmailSendMode.PreviewEmails;
				}
			}
			if (batchEmail.SendReport)
			{
				DataRow[] array = t.Select("emailSuccessfull=1");
				int num = array.Length;
				int num2 = t.Rows.Count - num;
				string text2 = string.Format("Batch email report for '{0}' ({1}) Fail count={2}\n", batchEmail.Title, batchEmail.EmailHistoryTypeCode, num2.ToString());
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(text2);
				stringBuilder.Append(string.Format("Successfully sent {0}\n", num.ToString()));
				stringBuilder.Append("\n");
				foreach (object obj3 in t.Rows)
				{
					DataRow dataRow = (DataRow)obj3;
					stringBuilder.Append("===================\n");
					stringBuilder.Append(string.Format("Successful: {0}\n", dataRow["emailsuccessfull"].ToString()));
					for (int i = 0; i < t.Columns.Count - 2; i++)
					{
						stringBuilder.Append(dataRow[i].ToString());
						stringBuilder.Append(" . ");
					}
					stringBuilder.Append("\n");
					stringBuilder.Append(dataRow["emailResult"].ToString());
					stringBuilder.Append("\n\n");
				}
				string text3 = batchEmail.AdminEmail;
				if (!ClockWorkCore.IsEmailValid(text3))
				{
					text3 = batchEmail.DefaultAdminEmail;
				}
				IEmailManager emailManager = new EmailManager(new OperationContext
				{
					WhoAmI = 0
				});
				TPMailResult tpmailResult = emailManager.SendEmail(text3, text3, text2, stringBuilder.ToString(), null, null, null, null);
			}
			return t;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00019960 File Offset: 0x00018960
		public static void BatchEmailWithMailMerge3(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, ref Report report, string xml, IncrementProgressBar isp, SetupProgressBar ssp)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable t = (currentDataView == null) ? null : currentDataView.Table;
			DataTable dataTable = ReportFunction.BatchEmailWithMailMerge3(da, tripleDES, xml, t, false);
			if (dataTable != null)
			{
				report.AddResult(dataTable.DefaultView);
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000199A4 File Offset: 0x000189A4
		private static void ClearDictionary(Dictionary<string, string> args)
		{
			List<string> list = new List<string>();
			foreach (string text in args.Keys)
			{
				list.Add(text);
			}
			args.Clear();
			foreach (string text in list)
			{
				args.Add(text, "");
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00019A58 File Offset: 0x00018A58
		private static int TryToFindIntVal(DataRow dr, string colName)
		{
			int result;
			if (dr.Table.Columns.Contains(colName))
			{
				result = ((dr[colName] == DBNull.Value) ? 0 : ((int)dr[colName]));
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00019AA4 File Offset: 0x00018AA4
		private static void ParseEmailTemplate(string templateFilledIn, out string from, out string to, out string cc, out string bcc, out string attachment, out string subject, out string body)
		{
			string text = "";
			body = "";
			StringReader stringReader = new StringReader(templateFilledIn);
			bool flag = false;
			string text2;
			while ((text2 = stringReader.ReadLine()) != null)
			{
				if (flag)
				{
					body = body + text2 + System.Environment.NewLine;
				}
				else if (text2.Trim().Length < 1)
				{
					flag = true;
				}
				else
				{
					text = text + text2 + System.Environment.NewLine;
				}
			}
			stringReader.Close();
			string[] array = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(text, true);
			from = "";
			to = "";
			cc = "";
			bcc = "";
			attachment = "";
			subject = "";
			foreach (string text3 in array)
			{
				int num = text3.IndexOf(':');
				if (num > 0)
				{
					string text4 = text3.Substring(0, num).ToLower().Trim();
					num++;
					string text5 = (num < text3.Length) ? text3.Substring(num).Trim() : "";
					string text6 = text4;
					if (text6 != null)
					{
						if (!(text6 == "from"))
						{
							if (!(text6 == "to"))
							{
								if (!(text6 == "cc"))
								{
									if (!(text6 == "bcc"))
									{
										if (!(text6 == "subject"))
										{
											if (text6 == "attachment")
											{
												attachment = text5;
											}
										}
										else
										{
											subject = text5;
										}
									}
									else
									{
										bcc = text5;
									}
								}
								else
								{
									cc = text5;
								}
							}
							else
							{
								to = text5;
							}
						}
						else
						{
							from = text5;
						}
					}
				}
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00019C74 File Offset: 0x00018C74
		private static string FillInCodes(string templateFilename, DataRow dr, DataTable tWithColumns)
		{
			TextReader textReader = new StreamReader(templateFilename);
			string text = textReader.ReadToEnd();
			textReader.Close();
			for (int i = 0; i < tWithColumns.Columns.Count; i++)
			{
				string pattern = "#<" + tWithColumns.Columns[i].ColumnName.ToLower() + ">#";
				text = ReportFunction.ReplaceFast(text, pattern, dr[i].ToString(), StringComparison.OrdinalIgnoreCase);
			}
			return text;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00019CF8 File Offset: 0x00018CF8
		private static string ReplaceFast(string original, string pattern, string replacement, StringComparison comparisonType)
		{
			string result;
			if (original == null)
			{
				result = null;
			}
			else if (string.IsNullOrEmpty(pattern))
			{
				result = original;
			}
			else
			{
				int length = pattern.Length;
				int num = -1;
				int num2 = 0;
				StringBuilder stringBuilder = new StringBuilder();
				for (;;)
				{
					num = original.IndexOf(pattern, num + 1, comparisonType);
					if (num < 0)
					{
						break;
					}
					stringBuilder.Append(original, num2, num - num2);
					stringBuilder.Append(replacement);
					num2 = num + length;
				}
				stringBuilder.Append(original, num2, original.Length - num2);
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00019D9C File Offset: 0x00018D9C
		public static string GetHashtableValueSafe(Hashtable h, string pName)
		{
			string result;
			if (h.ContainsKey(pName))
			{
				result = h[pName].ToString();
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00019DD0 File Offset: 0x00018DD0
		public static bool GetHashtableValueSafeBool(Hashtable h, string pName)
		{
			string hashtableValueSafe = ReportFunction.GetHashtableValueSafe(h, pName);
			return hashtableValueSafe.Trim().Length > 0 && Convert.ToBoolean(hashtableValueSafe);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00019E0C File Offset: 0x00018E0C
		public static int GetHashtableValueSafeInt(Hashtable h, string pName, int defaultVal)
		{
			string hashtableValueSafe = ReportFunction.GetHashtableValueSafe(h, pName);
			int result;
			if (hashtableValueSafe.Trim().Length < 1)
			{
				result = defaultVal;
			}
			else
			{
				try
				{
					result = int.Parse(hashtableValueSafe);
				}
				catch
				{
					result = defaultVal;
				}
			}
			return result;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00019E60 File Offset: 0x00018E60
		private static string GetUniqueColName(DataTable t, string proposedColName)
		{
			string result;
			if (t.Columns.Contains(proposedColName))
			{
				int i = 1;
				while (i < 1000)
				{
					string text = proposedColName + i++.ToString();
					if (!t.Columns.Contains(text))
					{
						return text;
					}
				}
				result = proposedColName + i.ToString() + "new";
			}
			else
			{
				result = proposedColName;
			}
			return result;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00019EE0 File Offset: 0x00018EE0
		private static string GetUniqueColName2(DataTable t, string proposedColName)
		{
			char[] array = new char[]
			{
				'.',
				' ',
				','
			};
			string text = proposedColName;
			foreach (char oldChar in array)
			{
				text = text.Replace(oldChar, '_');
			}
			return ReportFunction.GetUniqueColName(t, text);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00019F3C File Offset: 0x00018F3C
		private static string ArrayListToString(ArrayList list, bool excludeEmptyOrSpaceOnlyStrings)
		{
			string text = "";
			for (int i = 0; i < list.Count; i++)
			{
				string text2 = ((string)list[i]).Trim();
				if (!excludeEmptyOrSpaceOnlyStrings || text2.Length > 0)
				{
					if (text.Length > 0)
					{
						text += ",";
					}
					text += text2;
				}
			}
			return text;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00019FC0 File Offset: 0x00018FC0
		private static ArrayList ConfirmColNamesExist(DataView dv, string colNames)
		{
			DataTable table = dv.Table;
			string[] array = colNames.Split(new char[]
			{
				','
			});
			ArrayList arrayList = new ArrayList();
			foreach (string text in array)
			{
				if (table.Columns.Contains(text))
				{
					arrayList.Add(text);
				}
			}
			return arrayList;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0001A03C File Offset: 0x0001903C
		public static void FindPersonids(ref Report report, string studentNumColName, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			string columnName = ReportFunction.AddColumn(ref table, "personid", typeof(int));
			string text = (studentNumColName.Trim().Length > 0) ? studentNumColName : "student_no";
			DataView dataView = new DataView(currentDataView.Table);
			dataView.Sort = text;
			string strB = "";
			int num = -1;
			if (SetupSubProgressBar != null)
			{
				SetupSubProgressBar(0, dataView.Count);
			}
			try
			{
				da.Connection.Open();
				foreach (object obj in dataView)
				{
					DataRowView dataRowView = (DataRowView)obj;
					DataRow row = dataRowView.Row;
					if (IncrementSubProgressBar != null)
					{
						IncrementSubProgressBar(1);
					}
					string text2 = dataRowView[text].ToString();
					if (text2.CompareTo(strB) == 0)
					{
						row[columnName] = num;
					}
					else
					{
						strB = text2;
						da.SelectCommand.CommandText = "SELECT personid FROM people WHERE student_no=@snume";
						da.SelectCommand.Parameters.Clear();
						da.SelectCommand.Parameters.Add("@snume", tripleDES.Encrypt(text2));
						object obj2 = da.SelectCommand.ExecuteScalar();
						num = ((obj2 == null) ? -1 : ((int)obj2));
						row[columnName] = num;
					}
				}
			}
			catch (Exception ex)
			{
				report.LogError("FindPersonids", ex);
			}
			finally
			{
				da.Connection.Close();
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0001A260 File Offset: 0x00019260
		public static void SplitStrings(ref Report report, string colName, StringInt[] sections, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			for (int i = 0; i < sections.Length; i++)
			{
				sections[i].Int3 = table.Columns.Count;
				table.Columns.Add(sections[i].S);
			}
			if (SetupSubProgressBar != null)
			{
				SetupSubProgressBar(0, table.Rows.Count);
			}
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (IncrementSubProgressBar != null)
				{
					IncrementSubProgressBar(1);
				}
				string text = dataRow[colName].ToString();
				for (int i = 0; i < sections.Length; i++)
				{
					if (sections[i].Int1 < text.Length)
					{
						if (sections[i].Int1 + sections[i].Int2 <= text.Length)
						{
							dataRow[sections[i].Int3] = text.Substring(sections[i].Int1, sections[i].Int2);
						}
						else
						{
							dataRow[sections[i].Int3] = text.Substring(sections[i].Int1);
						}
					}
				}
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0001A3FC File Offset: 0x000193FC
		public static void BatchEmailMerge(bool testMode, ref Report report, string emailPrimaryColName, string emailSecondaryColName, string subject, string template, string from, string cc, string bcc, string attachments, string dontSendWhenColEmpty_colname, bool useHtml, SmtpSettings smtpSettings, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			ArrayList arrayList = new ArrayList();
			if (SetupSubProgressBar != null)
			{
				SetupSubProgressBar(0, currentDataView.Count);
			}
			object obj = null;
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Sent");
			dataTable.Columns.Add("DateSent", typeof(DateTime));
			dataTable.Columns.Add("To");
			dataTable.Columns.Add("From");
			dataTable.Columns.Add("Cc");
			dataTable.Columns.Add("Bcc");
			dataTable.Columns.Add("Subject");
			dataTable.Columns.Add("Body");
			dataTable.Columns.Add("ErrorMessage");
			dataTable.Columns.Add("RowInfo");
			char delimiter = ',';
			for (int i = 0; i < currentDataView.Count; i++)
			{
				DataRowView dataRowView = currentDataView[i];
				DataRow row = dataRowView.Row;
				if (IncrementSubProgressBar != null)
				{
					IncrementSubProgressBar(1);
				}
				DataRow dataRow = dataTable.NewRow();
				dataRow["RowInfo"] = ReportFunction.GetRowString(row);
				string text = row[emailPrimaryColName].ToString().Trim();
				string text2 = (emailSecondaryColName == null || emailSecondaryColName.Length < 1) ? cc : ReportFunction.MergeEmailAddresses(cc, row[emailSecondaryColName].ToString().Trim(), delimiter);
				string text3;
				text = ReportFunction.ExtractUniqueEmailAddresses(text, delimiter, out text3);
				if (text3.Length > 0)
				{
					dataRow["Sent"] = "ERROR";
					dataRow["ErrorMessage"] = "Invalid email found in primary: " + text3;
				}
				else
				{
					text2 = ReportFunction.ExtractUniqueEmailAddresses(text2, delimiter, out text3);
					if (text3.Length > 0)
					{
						dataRow["Sent"] = "ERROR";
						dataRow["ErrorMessage"] = "Invalid email found in secondary: " + text3;
					}
					else if (text.Length < 1)
					{
						dataRow["Sent"] = "ERROR";
						dataRow["ErrorMessage"] = "No primary email to send to: " + text3;
					}
					else
					{
						string text4 = template;
						string text5 = subject;
						foreach (object obj2 in currentDataView.Table.Columns)
						{
							DataColumn dataColumn = (DataColumn)obj2;
							string text6 = "#<" + dataColumn.ColumnName + ">#";
							if (text4.IndexOf(text6) >= 0)
							{
								text4 = text4.Replace(text6, row[dataColumn.ColumnName].ToString().Trim());
							}
							if (subject.IndexOf(text6) >= 0)
							{
								subject = subject.Replace(text6, row[dataColumn.ColumnName].ToString().Trim());
							}
						}
						try
						{
							dataRow["DateSent"] = DateTime.Now;
							dataRow["To"] = text;
							dataRow["From"] = from;
							dataRow["Cc"] = text2;
							dataRow["Bcc"] = bcc;
							dataRow["Subject"] = text5;
							dataRow["Body"] = text4;
							string text7 = ReportFunction.SendEmail(ref obj, smtpSettings.UseSsl, smtpSettings.Server, smtpSettings.Port, smtpSettings.Username, smtpSettings.Password, useHtml, text4, from, text, text5, text2, bcc, attachments, !testMode);
							if (!testMode)
							{
								Thread.Sleep(1000);
							}
							if (text7 != null && text7.Length > 0)
							{
								dataRow["Sent"] = "ERROR";
								dataRow["ErrorMessage"] = "SendEmail: " + text7;
							}
							else if (!testMode)
							{
								dataRow["Sent"] = "yes";
							}
							else
							{
								dataRow["Sent"] = "no (test mode)";
							}
						}
						catch (Exception ex)
						{
							dataRow["Sent"] = "ERROR";
							dataRow["ErrorMessage"] = ex.ToString();
						}
					}
				}
				dataTable.Rows.Add(dataRow);
			}
			report.ReplaceDataView(currentDataView, dataTable.DefaultView);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0001A930 File Offset: 0x00019930
		private static string SendEmail(ref object mailManObj, bool useSSL, string smtpServer, int smtpPort, string userName, string userPassword, bool bodyHtml, string body, string from, string to, string subject, string cc, string bcc, string attachments, bool actuallySend)
		{
			IEmailManager emailManager = new EmailManager(new OperationContext
			{
				WhoAmI = 0
			});
			TPMailResult tpmailResult = emailManager.SendEmail(to, from, subject, body, bodyHtml ? body : null, cc, bcc, attachments);
			return tpmailResult.ErrorMessage;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0001A97C File Offset: 0x0001997C
		private static string GetRowString(DataRow dr)
		{
			string text = "";
			object[] itemArray = dr.ItemArray;
			foreach (object obj in itemArray)
			{
				text += ((obj == null) ? "" : (obj.ToString().Trim() + ","));
			}
			return text;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0001A9E8 File Offset: 0x000199E8
		private static string MergeEmailAddresses(string currentAddresses, string newAddresses, char delimiter)
		{
			string result;
			if (currentAddresses.Trim().Length > 0)
			{
				result = currentAddresses + ((newAddresses.Trim().Length > 0) ? (delimiter + newAddresses) : "");
			}
			else
			{
				result = newAddresses.Trim();
			}
			return result;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0001AA40 File Offset: 0x00019A40
		private static string ExtractUniqueEmailAddresses(string emails, char delimiter, out string invalidEmails)
		{
			string[] array = emails.Split(new char[]
			{
				delimiter
			});
			string text = "";
			invalidEmails = "";
			ArrayList arrayList = new ArrayList();
			foreach (string text2 in array)
			{
				if (text2.Trim().Length >= 1)
				{
					if (ReportFunction.IsEmailValid(text2))
					{
						string text3 = text2.ToLower();
						if (!arrayList.Contains(text3))
						{
							arrayList.Add(text3);
						}
					}
					else
					{
						invalidEmails = invalidEmails + text2 + ",";
					}
				}
			}
			foreach (object obj in arrayList)
			{
				string str = (string)obj;
				if (text.Length > 0)
				{
					text += delimiter;
				}
				text += str;
			}
			return text;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0001AB84 File Offset: 0x00019B84
		public static bool IsEmailValid(string email)
		{
			Regex regex = new Regex("(?<user>[^@]+)@(?<host>.+)");
			Match match = regex.Match(email);
			return match.Success;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0001ABB0 File Offset: 0x00019BB0
		public static DataView LoadTextFormattedTable(string colInfoStr, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			int num = colInfoStr.IndexOf(System.Environment.NewLine);
			string path = colInfoStr.Substring(0, num);
			string s = colInfoStr.Substring(num + System.Environment.NewLine.Length);
			string[] array = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(s, true);
			DataTable dataTable = new DataTable();
			ArrayList arrayList = new ArrayList();
			foreach (string text in array)
			{
				string[] array3 = text.Split(new char[]
				{
					'.'
				});
				dataTable.Columns.Add(array3[0]);
				int x = int.Parse(array3[1]);
				int y = int.Parse(array3[2]);
				arrayList.Add(new Point(x, y));
			}
			StreamReader streamReader = new StreamReader(path);
			string text2;
			while ((text2 = streamReader.ReadLine()) != null)
			{
				object[] array4 = new object[arrayList.Count];
				for (int j = 0; j < arrayList.Count; j++)
				{
					Point point = (Point)arrayList[j];
					int x2 = point.X;
					array4[j] = text2.Substring(x2, point.Y);
				}
				dataTable.Rows.Add(array4);
			}
			streamReader.Close();
			return new DataView(dataTable);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0001AD20 File Offset: 0x00019D20
		public static string[] SplitStringIntoNEWLINE_delimitered_parts(string s, bool excludeEmptyStrings)
		{
			string[] array = s.Split(System.Environment.NewLine.ToCharArray());
			if (excludeEmptyStrings)
			{
				ArrayList arrayList = new ArrayList();
				foreach (string text in array)
				{
					if (text.Trim().Length > 0)
					{
						arrayList.Add(text.Trim());
					}
				}
				array = new string[arrayList.Count];
				for (int j = 0; j < arrayList.Count; j++)
				{
					array[j] = (string)arrayList[j];
				}
			}
			return array;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0001ADD8 File Offset: 0x00019DD8
		private static void LoadStudentNumbersIntoTempTable(UnivDataAdapter da, string tempTableName, DataTable t)
		{
			string text = (tempTableName.Length > 0 && tempTableName[0] == '#') ? tempTableName : ("#" + tempTableName);
			da.SelectCommand = da.CreateCommand("CREATE TABLE " + text + " (personid int)");
			da.SelectCommand.ExecuteNonQuery2();
			string text2 = "";
			ArrayList arrayList = new ArrayList(t.Rows.Count);
			int num = 0;
			bool flag = t.Columns["personid"].DataType == typeof(int);
			for (int i = 0; i < t.Rows.Count; i++)
			{
				DataRow dataRow = t.Rows[i];
				int num2;
				if (flag)
				{
					num2 = (int)dataRow["personid"];
				}
				else
				{
					try
					{
						num2 = int.Parse(dataRow["personid"].ToString().Trim());
					}
					catch
					{
						num2 = -1;
					}
				}
				if (!arrayList.Contains(num2))
				{
					arrayList.Add(num2);
					num++;
					if (num % 50 == 0)
					{
						da.SelectCommand = da.CreateCommand(string.Concat(new string[]
						{
							"INSERT INTO ",
							text,
							" (personid) SELECT orderid AS personid FROM splitorderids( '",
							text2,
							"',',')"
						}));
						da.SelectCommand.ExecuteNonQuery2();
						text2 = num2.ToString();
					}
					else
					{
						if (text2.Length > 0)
						{
							text2 += ",";
						}
						text2 += num2.ToString();
					}
				}
			}
			if (text2.Length > 0)
			{
				da.SelectCommand = da.CreateCommand(string.Concat(new string[]
				{
					"INSERT INTO ",
					text,
					" (personid) SELECT orderid AS personid FROM splitorderids( '",
					text2,
					"',',')"
				}));
				da.SelectCommand.ExecuteNonQuery2();
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0001B024 File Offset: 0x0001A024
		public static void ExecuteFunctionAgainstMemoryTable(UnivDataAdapter da, ref Report report, int functionCode, string functionName, string parameters, TechnoProReports technoProReports, TripleDESEncryptionClass tripleDES, DataSet comboBoxData, DataTable staffNamesTable, DataTable sessions, DataTable dynamicScreenNonDataControlsTable, DataSet lookupTablesForControls, object[] yearStartEnd, int whoAmIPersonID)
		{
			DataView currentDataView = report.GetCurrentDataView();
			ArrayList customVariables = new ArrayList();
			ArrayList variables = new ArrayList();
			ArrayList arrayList = new ArrayList();
			try
			{
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("searchfunctionid", typeof(int));
				dataTable.Columns.Add("searchinfoid", typeof(int));
				dataTable.Columns.Add("functioncode", typeof(int));
				dataTable.Columns.Add("functionparameters");
				dataTable.Columns.Add("ordernum", typeof(int));
				dataTable.Columns.Add("custom");
				dataTable.Columns.Add("customsqlinjection");
				dataTable.Columns.Add("customsqlinjectionoperator");
				dataTable.Columns.Add("functiondescription");
				DataRow dataRow = dataTable.NewRow();
				dataRow[0] = -1;
				dataRow[1] = -1;
				dataRow[2] = functionCode;
				dataRow[3] = parameters;
				dataRow[4] = 0;
				dataRow[8] = functionName;
				dataTable.Rows.Add(dataRow);
				SetupProgressBar setupSubProgressBar = new SetupProgressBar(ReportFunction.FakeSetupProgressBar);
				IncrementProgressBar incrementSubProgressBar = new IncrementProgressBar(ReportFunction.FakeIncrementProgressBar);
				ReportFunction.RunFunction("", new ReportStep(dataRow), ref report, da, customVariables, tripleDES, incrementSubProgressBar, setupSubProgressBar, ref comboBoxData, ref staffNamesTable, lookupTablesForControls, variables, sessions, yearStartEnd, dynamicScreenNonDataControlsTable, new DataTable(), whoAmIPersonID, technoProReports, -1, ref arrayList, true);
			}
			catch (Exception ex)
			{
				ReportFunction.MessageBoxShow(ex.ToString());
				report.LogError("ExecuteFunctionAgainstMemoryTable", ex);
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0001B224 File Offset: 0x0001A224
		public static DataView ExecuteFunctionAgainstMemoryTable(UnivDataAdapter da, DataTable t, FunctionCode functionCode, string parameters, TripleDESEncryptionClass tripleDES)
		{
			ArrayList customVariables = new ArrayList();
			ArrayList variables = new ArrayList();
			ArrayList arrayList = new ArrayList();
			DataView result;
			try
			{
				DataSet dataSet = new DataSet();
				ReportStep reportStep = new ReportStep(functionCode, parameters);
				Report report = new Report();
				report.AddResult(t.DefaultView);
				DataTable dataTable = new DataTable();
				DataSet lookupTablesForControls = new DataSet();
				DataTable sessions = new DataTable();
				ReportFunction.RunFunction("", reportStep, ref report, da, customVariables, tripleDES, null, null, ref dataSet, ref dataTable, lookupTablesForControls, variables, sessions, null, new DataTable(), new DataTable(), 0, null, -1, ref arrayList, true);
				result = report.GetCurrentDataView();
			}
			catch (Exception ex)
			{
				result = t.DefaultView;
			}
			return result;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0001B2E0 File Offset: 0x0001A2E0
		public static void PullInAppointmentsForAllStudents(UnivDataAdapter da, ref Report report, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			UnivTransaction univTransaction = null;
			DataTable t = new DataTable();
			try
			{
				da.Connection.Open();
				univTransaction = da.Connection.BeginTransaction();
				da.SelectCommand.Transaction = univTransaction;
				ReportFunction.LoadStudentNumbersIntoTempTable(da, "t1", currentDataView.Table);
				da.SelectCommand = da.CreateCommand("SELECT att.personid,app.startdate,app.enddate,atg.title,at.description,att.noshow,app.cancelled FROM attendees att LEFT JOIN appointments app ON app.appointmentid=att.appointmentid LEFT JOIN appointmenttypes at ON at.apptypeid=app.apptypeid LEFT JOIN appointmenttypegroups atg ON atg.appointmenttypegroupid=at.appointmenttypegroupid WHERE att.personid IN (SELECT personid FROM #t1)");
				try
				{
					UnivDataReader reader = da.ExecuteSelectCommandReaderInTransaction(da.Connection.Transaction);
					t = UnivOleDbFactory.ReaderToDataTable(reader);
				}
				catch (Exception ex)
				{
					ReportFunction.MessageBoxShow(ex.ToString());
				}
				univTransaction.Commit();
			}
			catch (Exception ex2)
			{
				if (univTransaction != null)
				{
					univTransaction.Rollback();
				}
				ReportFunction.MessageBoxShow("Rolled back!: " + ex2.ToString());
			}
			finally
			{
				da.Connection.Close();
			}
			ReportFunction.ExtractUniqueRows(ref report, new string[]
			{
				"student_no"
			});
			currentDataView.Sort = "student_no";
			int count = currentDataView.Table.Columns.Count;
			DataTable dataTable = currentDataView.Table.Clone();
			ReportFunction.AddColumn(ref dataTable, "Total appointment count", typeof(int));
			ReportFunction.AddColumn(ref dataTable, "app_startdate", typeof(DateTime));
			ReportFunction.AddColumn(ref dataTable, "app_enddate", typeof(DateTime));
			ReportFunction.AddColumn(ref dataTable, "app_title", typeof(string));
			ReportFunction.AddColumn(ref dataTable, "app_description", typeof(string));
			ReportFunction.AddColumn(ref dataTable, "Cancelled", typeof(bool));
			ReportFunction.AddColumn(ref dataTable, "No-show", typeof(bool));
			bool flag = currentDataView.Table.Columns["personid"].DataType == typeof(int);
			foreach (object obj in currentDataView)
			{
				DataRowView dataRowView = (DataRowView)obj;
				DataRow dataRow = dataRowView.Row;
				int pid;
				if (flag)
				{
					pid = (int)dataRow["personid"];
				}
				else
				{
					try
					{
						pid = int.Parse(dataRow["personid"].ToString().Trim());
					}
					catch
					{
						pid = -1;
					}
				}
				DataTable dataTable2 = ReportFunction.FindAllRowsWithMatchingPid(t, pid, 0);
				if (dataTable2.Rows.Count > 0)
				{
					foreach (object obj2 in dataTable2.Rows)
					{
						DataRow dataRow2 = (DataRow)obj2;
						dataTable.ImportRow(dataRow);
						dataRow = dataTable.Rows[dataTable.Rows.Count - 1];
						dataRow[count] = dataTable2.Rows.Count;
						dataRow[count + 1] = dataRow2[1];
						dataRow[count + 2] = dataRow2[2];
						dataRow[count + 3] = dataRow2[3];
						dataRow[count + 4] = dataRow2[4];
						dataRow[count + 5] = dataRow2[6];
						dataRow[count + 6] = dataRow2[5];
					}
				}
				else
				{
					dataTable.ImportRow(dataRow);
					dataRow = dataTable.Rows[dataTable.Rows.Count - 1];
					dataRow[count] = 0;
				}
			}
			DataView dataView = new DataView(dataTable);
			string sort;
			if (dataTable.Columns.Contains("lastname") && dataTable.Columns.Contains("firstname"))
			{
				sort = "lastname,firstname,app_startdate,app_title,app_description";
			}
			else
			{
				sort = "student_no,app_startdate,app_title,app_description";
			}
			dataView.Sort = sort;
			report.ReplaceDataView(currentDataView, dataView);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0001B7D8 File Offset: 0x0001A7D8
		private static string AddColumn(ref DataTable t, string colName, Type dataType)
		{
			string text;
			if (t.Columns.Contains(colName))
			{
				for (int i = 0; i < 10000; i++)
				{
					text = colName + i.ToString();
					if (!t.Columns.Contains(text))
					{
						break;
					}
				}
				text = colName + colName;
			}
			else
			{
				text = colName;
			}
			t.Columns.Add(text, dataType);
			return text;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0001B854 File Offset: 0x0001A854
		public static void PullInActiveDatesForAllStudents(UnivDataAdapter da, ref Report report, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			UnivTransaction univTransaction = null;
			DataTable dataTable = new DataTable();
			DataTable t = new DataTable();
			try
			{
				da.Connection.Open();
				univTransaction = da.Connection.BeginTransaction();
				da.SelectCommand.Transaction = univTransaction;
				ReportFunction.LoadStudentNumbersIntoTempTable(da, "t1", currentDataView.Table);
				da.SelectCommand = da.CreateCommand("SELECT DISTINCT dateadded FROM (SELECT dateadded FROM people WHERE personid IN (SELECT personid FROM #t1) UNION SELECT dateactive AS dateadded FROM peoplepreviousyears WHERE personid IN (SELECT personid FROM #t1)) a");
				try
				{
					UnivDataReader reader = da.ExecuteSelectCommandReaderInTransaction(da.Connection.Transaction);
					dataTable = UnivOleDbFactory.ReaderToDataTable(reader);
				}
				catch (Exception ex)
				{
					ReportFunction.MessageBoxShow(ex.ToString());
				}
				da.SelectCommand = da.CreateCommand("SELECT DISTINCT personid,dateadded FROM (SELECT t1.personid,p.dateadded FROM #t1 t1 LEFT JOIN people p ON p.personid=t1.personid UNION SELECT t1.personid,ppy.dateactive AS dateadded FROM #t1 t1 LEFT JOIN peoplepreviousyears ppy ON ppy.personid=t1.personid) a ORDER BY personid,dateadded");
				try
				{
					UnivDataReader reader = da.ExecuteSelectCommandReaderInTransaction(da.Connection.Transaction);
					t = UnivOleDbFactory.ReaderToDataTable(reader);
				}
				catch (Exception ex)
				{
					ReportFunction.MessageBoxShow(ex.ToString());
				}
				univTransaction.Commit();
				da.Connection.Close();
			}
			catch (Exception ex2)
			{
				if (univTransaction != null)
				{
					univTransaction.Rollback();
				}
				ReportFunction.MessageBoxShow("Rolled back!: " + ex2.ToString());
			}
			DataTable table = currentDataView.Table;
			ArrayList arrayList = new ArrayList();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (dataRow[0] != DBNull.Value)
				{
					DateTime dateTime = (DateTime)dataRow[0];
					DateTime dateTime2;
					if (dateTime.Month < 5)
					{
						dateTime2 = new DateTime(dateTime.Year - 1, 5, 1);
					}
					else
					{
						dateTime2 = new DateTime(dateTime.Year, 5, 1);
					}
					if (!arrayList.Contains(dateTime2))
					{
						arrayList.Add(dateTime2);
					}
				}
			}
			if (arrayList.Count >= 1)
			{
				arrayList.Sort();
				Type typeFromHandle = typeof(bool);
				int count = table.Columns.Count;
				foreach (object obj2 in arrayList)
				{
					DateTime dateTime2 = (DateTime)obj2;
					DateTime dateTime3 = new DateTime(dateTime2.Year + 1, 4, 30);
					string columnName = dateTime2.ToString("yyyy_MM_dd") + "_to_" + dateTime3.ToString("yyyy_MM_dd");
					table.Columns.Add(columnName, typeFromHandle);
				}
				foreach (object obj3 in table.Rows)
				{
					DataRow dataRow = (DataRow)obj3;
					int pid = (int)dataRow["personid"];
					DataTable dataTable2 = ReportFunction.FindAllRowsWithMatchingPid(t, pid, 0);
					foreach (object obj4 in dataTable2.Rows)
					{
						DataRow dataRow2 = (DataRow)obj4;
						if (dataRow2[1] != DBNull.Value)
						{
							DateTime t2 = (DateTime)dataRow2[1];
							for (int i = 0; i < arrayList.Count; i++)
							{
								DateTime t3 = (DateTime)arrayList[i];
								DateTime t4 = new DateTime(t3.Year + 1, 4, 30, 23, 59, 0);
								if (t2 >= t3 && t2 <= t4)
								{
									dataRow[count + i] = true;
									break;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0001BD50 File Offset: 0x0001AD50
		private static DataTable FindAllRowsWithMatchingPid(DataTable t, int pid, int pidColInd)
		{
			DataTable dataTable = t.Clone();
			foreach (object obj in t.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow[pidColInd];
				if (num == pid)
				{
					dataTable.ImportRow(dataRow);
				}
			}
			return dataTable;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0001BDEC File Offset: 0x0001ADEC
		public static void DecryptAndFixAppointmentMemos(ref Report report, TripleDESEncryptionClass tripleDES, string memoColName, string isEncryptedColName, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			string columnName = ReportFunction.AddStringColumn(ref table, "AppointmentMemo", typeof(string));
			int columnIndex = table.Columns.IndexOf(columnName);
			RichTextBox richTextBox = new RichTextBox();
			if (SetupSubProgressBar != null)
			{
				SetupSubProgressBar(0, currentDataView.Count);
			}
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (IncrementSubProgressBar != null)
				{
					IncrementSubProgressBar(1);
				}
				if (dataRow[memoColName] == DBNull.Value)
				{
					dataRow[columnIndex] = "";
				}
				else
				{
					byte[] bytes = (byte[])dataRow[memoColName];
					bool decrypt = dataRow[isEncryptedColName] != DBNull.Value && (bool)dataRow[isEncryptedColName];
					string rtf = DynamicScreen.BytesToString(bytes, decrypt, tripleDES);
					richTextBox.Rtf = rtf;
					dataRow[columnIndex] = richTextBox.Text;
				}
			}
			richTextBox.Dispose();
			richTextBox = null;
			table.Columns.Remove(memoColName);
			table.Columns.Remove(isEncryptedColName);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0001BF6C File Offset: 0x0001AF6C
		private static void ExtractUniqueStudentsWithRowHavingTheMinimumValueInASpecificColumn(ref Report report, bool returnMinimum, string colToFindUniqueValues, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			int num = table.Columns.IndexOf(colToFindUniqueValues);
			int num2 = table.Columns.IndexOf("student_no");
			if (num2 >= 0 && num >= 0)
			{
				currentDataView.Sort = "student_no";
				if (SetupSubProgressBar != null)
				{
					SetupSubProgressBar(0, currentDataView.Count);
				}
				DataTable dataTable = table.Clone();
				Type dataType = table.Columns[num].DataType;
				bool flag = dataType == typeof(DateTime);
				bool flag2 = dataType == typeof(int);
				bool flag3 = dataType == typeof(double);
				DataRow dataRow = null;
				string text = "";
				foreach (object obj in currentDataView)
				{
					DataRowView dataRowView = (DataRowView)obj;
					if (IncrementSubProgressBar != null)
					{
						IncrementSubProgressBar(1);
					}
					DataRow row = dataRowView.Row;
					string text2 = ((string)row[num2]).Trim().ToLower();
					if (text.Length < 1 || text2.CompareTo(text) != 0)
					{
						if (dataRow != null)
						{
							dataTable.ImportRow(dataRow);
						}
						dataRow = row;
						text = text2;
					}
					else if (row[num] == DBNull.Value)
					{
						if (returnMinimum && dataRow[num] != DBNull.Value)
						{
							dataRow = row;
						}
					}
					else if (dataRow[num] == DBNull.Value)
					{
						if (!returnMinimum)
						{
							dataRow = row;
						}
					}
					else if (flag)
					{
						DateTime t = (DateTime)row[num];
						DateTime t2 = (DateTime)dataRow[num];
						if (returnMinimum && t < t2)
						{
							dataRow = row;
						}
						else if (!returnMinimum && t > t2)
						{
							dataRow = row;
						}
					}
					else if (flag2)
					{
						int num3 = (int)row[num];
						int num4 = (int)dataRow[num];
						if (returnMinimum && num3 < num4)
						{
							dataRow = row;
						}
						else if (!returnMinimum && num3 > num4)
						{
							dataRow = row;
						}
					}
					else if (flag3)
					{
						double num5 = (double)row[num];
						double num6 = (double)row[num];
						if (returnMinimum && num5 < num6)
						{
							dataRow = row;
						}
						else if (!returnMinimum && num5 > num6)
						{
							dataRow = row;
						}
					}
					else
					{
						string text3 = row[num].ToString();
						string strB = dataRow[num].ToString();
						if (returnMinimum && text3.CompareTo(strB) < 0)
						{
							dataRow = row;
						}
						else if (!returnMinimum && text3.CompareTo(strB) > 0)
						{
							dataRow = row;
						}
					}
				}
				if (dataRow != null)
				{
					dataTable.ImportRow(dataRow);
				}
				report.ReplaceDataView(currentDataView, ReportFunction.CloneDataView(currentDataView, dataTable));
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0001C33C File Offset: 0x0001B33C
		private static void MergeRowsByDroppingDuplicateRows(ref Report report, ref ArrayList errors, string parameters, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			string[] array = parameters.Split(new char[]
			{
				','
			});
			ArrayList arrayList = new ArrayList();
			foreach (string text in array)
			{
				string columnName = text.Trim();
				int num = table.Columns.IndexOf(columnName);
				if (num >= 0 && !arrayList.Contains(num))
				{
					arrayList.Add(num);
				}
			}
			if (SetupSubProgressBar != null)
			{
				SetupSubProgressBar(0, table.Rows.Count * 2);
			}
			DataTable dataTable = table.Clone();
			for (int j = 0; j < table.Rows.Count; j++)
			{
				if (IncrementSubProgressBar != null)
				{
					IncrementSubProgressBar(1);
				}
				DataRow dataRow = table.Rows[j];
				bool flag = false;
				for (int k = 0; k < dataTable.Rows.Count; k++)
				{
					DataRow dataRow2 = dataTable.Rows[k];
					bool flag2 = true;
					for (int l = 0; l < arrayList.Count; l++)
					{
						int num2 = ReportFunction.CompareDataRowCells(dataRow[l], dataRow2[l]);
						if (num2 != 0)
						{
							flag2 = false;
							break;
						}
					}
					if (flag2)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					dataTable.LoadDataRow(dataRow.ItemArray, true);
				}
			}
			for (int j = 0; j < dataTable.Rows.Count; j++)
			{
				DataRow dataRow = dataTable.Rows[j];
				for (int k = 0; k < table.Rows.Count; k++)
				{
					if (IncrementSubProgressBar != null)
					{
						IncrementSubProgressBar(1);
					}
					DataRow dataRow2 = table.Rows[k];
					bool flag2 = true;
					for (int l = 0; l < arrayList.Count; l++)
					{
						int num2 = ReportFunction.CompareDataRowCells(dataRow[l], dataRow2[l]);
						if (num2 != 0)
						{
							flag2 = false;
							break;
						}
					}
					if (flag2)
					{
						for (int l = 0; l < table.Columns.Count; l++)
						{
							if (!arrayList.Contains(l))
							{
								int num2 = ReportFunction.CompareDataRowCells(dataRow2[l], dataRow[l]);
								if (num2 > 0)
								{
									dataRow[l] = dataRow2[l];
								}
							}
						}
					}
				}
			}
			table.Rows.Clear();
			report.ReplaceDataView(currentDataView, ReportFunction.CloneDataView(currentDataView, dataTable));
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0001C668 File Offset: 0x0001B668
		private static int CompareDataRowCells(object c1, object c2)
		{
			int result;
			if (c1 == null && c2 == null)
			{
				result = 0;
			}
			else if (c1 == DBNull.Value && c2 == DBNull.Value)
			{
				result = 0;
			}
			else if (c1 == null || c1 == DBNull.Value)
			{
				result = -1;
			}
			else if (c2 == null || c2 == DBNull.Value)
			{
				result = 1;
			}
			else if (c1.GetType() != c2.GetType())
			{
				result = -1;
			}
			else if (c1.GetType() == typeof(int))
			{
				result = ((int)c1).CompareTo((int)c2);
			}
			else if (c1.GetType() == typeof(DateTime))
			{
				result = ((DateTime)c1).CompareTo((DateTime)c2);
			}
			else if (c1.GetType() == typeof(bool))
			{
				result = ((bool)c1).CompareTo((bool)c2);
			}
			else if (c1.GetType() == typeof(double))
			{
				result = ((double)c1).CompareTo((double)c2);
			}
			else
			{
				byte[] array = new byte[1];
				if (c1.GetType() == array.GetType())
				{
					byte[] array2 = (byte[])c1;
					byte[] array3 = (byte[])c2;
					if (array2.Length != array3.Length)
					{
						result = ((array2.Length < 1) ? 1 : -1);
					}
					else if (array2.Length < 1)
					{
						result = 0;
					}
					else
					{
						for (int i = 0; i < array2.Length; i++)
						{
							if (array2[i] != array3[i])
							{
								return -1;
							}
						}
						result = 0;
					}
				}
				else
				{
					string text = c1.ToString().Trim().ToLower();
					string strB = c2.ToString().Trim().ToLower();
					if (text.CompareTo(strB) == 0)
					{
						result = 0;
					}
					else if (text.Length > 0)
					{
						result = 1;
					}
					else
					{
						result = -1;
					}
				}
			}
			return result;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0001C8DC File Offset: 0x0001B8DC
		private static NameValueCollection ParseParameters(string parameters, int startAtInd, string equalsDelimiter, string nameValuePairDelimiter)
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			string[] array = parameters.Split(nameValuePairDelimiter.ToCharArray());
			for (int i = startAtInd; i < array.Length; i++)
			{
				string text = array[i].Trim();
				if (text.Length > 0)
				{
					string[] array2 = text.Split(equalsDelimiter.ToCharArray());
					if (array2.Length == 2)
					{
						nameValueCollection.Add(array2[0].ToLower(), array2[1]);
					}
				}
			}
			return nameValueCollection;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0001C96C File Offset: 0x0001B96C
		private static int GetNumDays(string EveryDaysWeeksMonthsYearsDescription)
		{
			int num = EveryDaysWeeksMonthsYearsDescription.IndexOf(" ");
			int result;
			if (num > 0)
			{
				string s = EveryDaysWeeksMonthsYearsDescription.Substring(0, num);
				string text = EveryDaysWeeksMonthsYearsDescription.Substring(num + 1).ToLower().Trim();
				int num2;
				try
				{
					num2 = int.Parse(s);
				}
				catch
				{
					num2 = 0;
				}
				if (num2 <= 0)
				{
					result = 0;
				}
				else
				{
					DateTime d = DateTime.Now;
					if (text.IndexOf("week") == 0)
					{
						num2 *= 7;
					}
					else if (text.IndexOf("month") == 0)
					{
						d = d.AddMonths(num2);
						num2 = Convert.ToInt32((d - DateTime.Now).TotalDays);
					}
					else if (text.IndexOf("year") == 0)
					{
						d = d.AddYears(num2);
						num2 = Convert.ToInt32((d - DateTime.Now).TotalDays);
					}
					result = num2;
				}
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0001CA98 File Offset: 0x0001BA98
		private static string GetCurrentDatabaseName(UnivDataAdapter da)
		{
			da.SelectCommand.CommandText = "SELECT settingstringvalue FROM settingsgroups WHERE settingcode=312";
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			return (dataTable.Rows.Count < 1) ? "ClockWork" : dataTable.Rows[0][0].ToString().Trim();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0001CAFC File Offset: 0x0001BAFC
		public static void BackupDatabase(ref Report report, ref ArrayList errors, UnivDataAdapter da, string parameters, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			if (SetupSubProgressBar != null)
			{
				SetupSubProgressBar(0, 2);
			}
			NameValueCollection nameValueCollection = ReportFunction.ParseParameters("fn=" + parameters, 0, "=", System.Environment.NewLine);
			string text = nameValueCollection["fn"];
			string text2 = nameValueCollection["delete"];
			string text3 = nameValueCollection["secondary"];
			string text4 = nameValueCollection["zipsecondary"];
			da.SelectCommand.CommandText = "SELECT db_name() AS dbname";
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			string text5 = dataTable.Rows[0][0].ToString().Trim();
			string directoryName = Path.GetDirectoryName(text);
			text = Path.Combine(directoryName, Path.GetFileNameWithoutExtension(text) + "_" + text5 + Path.GetExtension(text));
			if (text3 != null && text3.Length > 0)
			{
				int numDays = ReportFunction.GetNumDays(text3);
				if (numDays > 0 && File.Exists(text))
				{
					DateTime creationTime = File.GetCreationTime(text);
					DateTime lastWriteTime = File.GetLastWriteTime(text);
					if ((DateTime.Now - creationTime).TotalDays >= (double)numDays)
					{
						string text6 = string.Concat(new string[]
						{
							Path.GetFileNameWithoutExtension(text),
							"_",
							creationTime.ToString("yyyy.MM.dd"),
							"_to_",
							lastWriteTime.ToString("yyyy.MM.dd"),
							Path.GetExtension(text)
						});
						text6 = Path.Combine(directoryName, text6);
						if (!File.Exists(text6))
						{
							File.Move(text, text6);
						}
						if (text4 != null)
						{
							text4 = text4.ToLower().Trim();
							if (text4.CompareTo("yes") == 0 || text4.CompareTo("true") == 0 || text4.CompareTo("1") == 0)
							{
								string startDirectory = ReportFunction.GetStartDirectory();
								string fileName = Path.Combine(startDirectory, "7za.exe");
								string text7 = Path.Combine(directoryName, Path.GetFileNameWithoutExtension(text6) + ".7z");
								Process process = Process.Start(new ProcessStartInfo(fileName, string.Concat(new string[]
								{
									"a \"",
									text7,
									"\" \"",
									text6,
									"\" -y"
								}))
								{
									WorkingDirectory = directoryName
								});
								process.WaitForExit();
								File.Delete(text6);
							}
						}
					}
				}
			}
			if (text2 != null && text2.Length > 0)
			{
				int numDays = ReportFunction.GetNumDays(text2);
				if (numDays > 0)
				{
					string[] files = Directory.GetFiles(directoryName, Path.GetFileNameWithoutExtension(text) + "*." + Path.GetExtension(text));
					foreach (string path in files)
					{
						DateTime creationTime = File.GetCreationTime(path);
						if ((DateTime.Now - creationTime).TotalDays >= (double)numDays)
						{
							File.Delete(path);
						}
					}
				}
			}
			da.SelectCommand.CommandText = string.Concat(new string[]
			{
				"BACKUP DATABASE ",
				text5,
				" TO DISK = '",
				text,
				"'"
			});
			if (File.Exists(text))
			{
				UnivCommand selectCommand = da.SelectCommand;
				selectCommand.CommandText += " WITH DIFFERENTIAL;";
			}
			if (IncrementSubProgressBar != null)
			{
				IncrementSubProgressBar(1);
			}
			string text8;
			da.Fill(new DataTable(), out text8);
			if (IncrementSubProgressBar != null)
			{
				IncrementSubProgressBar(1);
			}
			if (text8 != null && text8.Length > 0)
			{
				errors.Add(text8);
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0001CF30 File Offset: 0x0001BF30
		public static void ExportDatabase(ref Report report, ref ArrayList errors, string parameters, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			if (SetupSubProgressBar != null)
			{
				SetupSubProgressBar(0, 2);
			}
			string[] array = parameters.Split(new char[]
			{
				'`'
			});
			string text = array[0];
			string text2 = Path.GetExtension(text).ToLower().Trim();
			string startDirectory = ReportFunction.GetStartDirectory();
			if (IncrementSubProgressBar != null)
			{
				IncrementSubProgressBar(1);
			}
			string text3 = text2;
			if (text3 != null)
			{
				if (!(text3 == ".xls"))
				{
					if (!(text3 == ".mdb"))
					{
						if (!(text3 == ".txt"))
						{
							if (!(text3 == ".csv"))
							{
								if (text3 == ".csv2")
								{
									string tempFilename = ReportFunction.GetTempFilename(".csv");
									TemplatesClass.ExportToDelimeteredText(currentDataView, tempFilename, startDirectory, false);
									string text4 = TemplatesClass.OpenCsvInExcelAndSaveAsCsv(tempFilename, text.Substring(0, text.Length - 1));
									if (text4 != null && text4.Trim().Length > 0)
									{
										errors.Add("Something went wrong trying to save the .csv file as an Excel .csv file. (" + text4 + ")");
									}
									try
									{
										File.Delete(tempFilename);
									}
									catch
									{
									}
								}
							}
							else
							{
								string contents = currentDataView.ConvertDataViewToCsv();
								File.WriteAllText(text, contents);
							}
						}
						else
						{
							bool showColumnNames = array.Length <= 1 || array[1].ToLower().Trim().CompareTo("nocolumns") != 0;
							string contents2 = DataTableUtility.ExportToFormattedText(currentDataView, showColumnNames);
							File.WriteAllText(text, contents2);
						}
					}
				}
				else
				{
					ExcelUtility.ExportDataTableToExcel(text, currentDataView.Table, FileActionAfterExport.None);
				}
			}
			if (IncrementSubProgressBar != null)
			{
				IncrementSubProgressBar(1);
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0001D100 File Offset: 0x0001C100
		public static void InsertRowsIntoADatabaseTable(ref Report report, string parameters, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			string[] array = parameters.Split(new char[]
			{
				'`'
			});
			string text = array[0];
			string connectionString = array[1];
			string str = array[2];
			if (SetupSubProgressBar != null)
			{
				SetupSubProgressBar(0, currentDataView.Table.Rows.Count);
			}
			if (text.CompareTo("sqlserver") == 0)
			{
				SqlConnection sqlConnection = new SqlConnection(connectionString);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("", sqlConnection);
				try
				{
					sqlConnection.Open();
					string text2 = "INSERT INTO " + str + " (";
					string text3 = "";
					for (int i = 0; i < currentDataView.Table.Columns.Count; i++)
					{
						if (i > 0)
						{
							text2 += ",";
							text3 += ",";
						}
						text2 += currentDataView.Table.Columns[i].ColumnName;
						string text4 = "@p" + i.ToString();
						text3 += text4;
					}
					text2 = text2 + ") VALUES (" + text3 + ")";
					foreach (object obj in currentDataView)
					{
						DataRowView dataRowView = (DataRowView)obj;
						if (IncrementSubProgressBar != null)
						{
							IncrementSubProgressBar(1);
						}
						DataRow row = dataRowView.Row;
						sqlDataAdapter.SelectCommand.CommandText = text2;
						sqlDataAdapter.SelectCommand.Parameters.Clear();
						for (int i = 0; i < currentDataView.Table.Columns.Count; i++)
						{
							string text4 = "@p" + i.ToString();
							sqlDataAdapter.SelectCommand.Parameters.AddWithValue(text4, row[i]);
						}
						sqlDataAdapter.SelectCommand.ExecuteNonQuery();
					}
					sqlConnection.Close();
				}
				catch (Exception ex)
				{
					ReportFunction.MessageBoxShow(ex.ToString());
					sqlConnection.Close();
				}
			}
			else if (text.CompareTo("oledb") == 0)
			{
				OleDbConnection oleDbConnection = new OleDbConnection(connectionString);
				OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("", oleDbConnection);
				try
				{
					oleDbConnection.Open();
					string text2 = "INSERT INTO " + str + " (";
					string text3 = "";
					for (int i = 0; i < currentDataView.Table.Columns.Count; i++)
					{
						if (i > 0)
						{
							text2 += ",";
							text3 += ",";
						}
						text2 += currentDataView.Table.Columns[i].ColumnName;
						string text4 = "@p" + i.ToString();
						text3 += text4;
					}
					text2 = text2 + ") VALUES (" + text3 + ")";
					foreach (object obj2 in currentDataView)
					{
						DataRowView dataRowView = (DataRowView)obj2;
						if (IncrementSubProgressBar != null)
						{
							IncrementSubProgressBar(1);
						}
						DataRow row = dataRowView.Row;
						oleDbDataAdapter.SelectCommand.CommandText = text2;
						oleDbDataAdapter.SelectCommand.Parameters.Clear();
						for (int i = 0; i < currentDataView.Table.Columns.Count; i++)
						{
							string text4 = "@p" + i.ToString();
							oleDbDataAdapter.SelectCommand.Parameters.Add(text4, row[i]);
						}
						oleDbDataAdapter.SelectCommand.ExecuteNonQuery();
					}
					oleDbConnection.Close();
				}
				catch (Exception ex)
				{
					ReportFunction.MessageBoxShow(ex.ToString());
					oleDbConnection.Close();
				}
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0001D5EC File Offset: 0x0001C5EC
		public static void Merge2DifferentSetsOfStudentAccommodationsForTheSameStudent(ref Report report, string uniqueCols0, string colsToIgnore0, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			if (SetupSubProgressBar != null)
			{
				SetupSubProgressBar(0, currentDataView.Table.Rows.Count);
			}
			string text = uniqueCols0.ToLower().Trim();
			string text2 = colsToIgnore0.ToLower().Trim();
			DataTable table = currentDataView.Table;
			DataView dataView = new DataView(table);
			dataView.Sort = text;
			string[] array = text.Split(new char[]
			{
				','
			});
			ArrayList arrayList = new ArrayList();
			if (text2.Length > 0)
			{
				string[] array2 = text2.Split(new char[]
				{
					','
				});
				foreach (string value in array2)
				{
					arrayList.Add(value);
				}
			}
			int num = 0;
			for (int j = 1; j < dataView.Count; j++)
			{
				if (IncrementSubProgressBar != null)
				{
					IncrementSubProgressBar(1);
				}
				DataRowView dataRowView = dataView[j - 1];
				DataRowView dataRowView2 = dataView[j];
				DataRow row = dataRowView.Row;
				DataRow row2 = dataRowView2.Row;
				if (num++ > 7)
				{
					num = 0;
				}
				if (num == 0)
				{
					if (IncrementSubProgressBar != null)
					{
						IncrementSubProgressBar(1);
					}
				}
				string text3 = "";
				string text4 = "";
				for (int k = 0; k < array.Length; k++)
				{
					string columnName = array[k];
					text3 = text3 + row[columnName].ToString().Trim() + k.ToString();
					text4 = text4 + row2[columnName].ToString().Trim() + k.ToString();
				}
				if (text3.CompareTo(text4) == 0)
				{
					for (int l = 0; l < table.Columns.Count; l++)
					{
						string columnName2 = table.Columns[l].ColumnName;
						string text5 = columnName2.ToLower();
						if (Array.IndexOf<string>(array, text5) < 0 && !arrayList.Contains(text5))
						{
							if (row[columnName2] != DBNull.Value)
							{
								if (row2[columnName2] == DBNull.Value)
								{
									row2[columnName2] = row[columnName2];
								}
								else if (!(row2[columnName2].ToString() == row[columnName2].ToString()))
								{
									if (row2[columnName2].ToString().Trim().Length >= 1 || row[columnName2].ToString().Trim().Length >= 1)
									{
										Type dataType = table.Columns[l].DataType;
										if (dataType == Type.GetType("System.Int32"))
										{
											int num2 = (int)row[columnName2];
											int num3 = (int)row2[columnName2];
											if (num2 > num3)
											{
												row2[columnName2] = row[columnName2];
											}
										}
										else if (dataType == Type.GetType("System.Double"))
										{
											double num4 = (double)row[columnName2];
											double num5 = (double)row2[columnName2];
											if (num4 > num5)
											{
												row2[columnName2] = row[columnName2];
											}
										}
										else if (dataType == Type.GetType("System.Float"))
										{
											float num6 = (float)row[columnName2];
											float num7 = (float)row2[columnName2];
											if (num6 > num7)
											{
												row2[columnName2] = row[columnName2];
											}
										}
										else if (dataType == Type.GetType("System.Boolean"))
										{
											bool flag = (bool)row[columnName2];
											bool flag2 = (bool)row2[columnName2];
											if (flag && !flag2)
											{
												row2[columnName2] = row[columnName2];
											}
										}
										else if (dataType == Type.GetType("System.DateTime"))
										{
											DateTime t = (DateTime)row[columnName2];
											DateTime t2 = (DateTime)row2[columnName2];
											if (t > t2)
											{
												row2[columnName2] = row[columnName2];
											}
										}
										else
										{
											string text6 = row[columnName2].ToString().Trim();
											string text7 = row2[columnName2].ToString().Trim();
											bool flag3 = false;
											if (text5.IndexOf("time") >= 0)
											{
												string text8 = "";
												bool flag4 = false;
												foreach (char c in text6)
												{
													if (flag4)
													{
														if (!char.IsDigit(c))
														{
															break;
														}
														text8 += c;
													}
													else if (char.IsDigit(c))
													{
														flag4 = true;
														text8 += c;
													}
												}
												string text10 = "";
												flag4 = false;
												foreach (char c in text7)
												{
													if (flag4)
													{
														if (!char.IsDigit(c))
														{
															break;
														}
														text10 += c;
													}
													else if (char.IsDigit(c))
													{
														flag4 = true;
														text10 += c;
													}
												}
												if (text8.Length > 0 && text10.Length > 0)
												{
													try
													{
														int num8 = int.Parse(text8);
														int num9 = int.Parse(text10);
														if (num8 > num9)
														{
															row2[columnName2] = row[columnName2];
														}
														flag3 = true;
													}
													catch
													{
													}
												}
											}
											if (!flag3)
											{
												if (text6.Length > 0)
												{
													if (text7.Length > 0)
													{
														text7 += ", ";
													}
													text7 += text6;
													row2[columnName2] = text7;
												}
											}
										}
									}
								}
							}
						}
					}
					row.Delete();
				}
			}
			table.AcceptChanges();
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0001DD70 File Offset: 0x0001CD70
		private static void SetBlankCellsToNull(ref Report report, string colName, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			int columnIndex = currentDataView.Table.Columns.IndexOf(colName);
			if (SetupSubProgressBar != null)
			{
				SetupSubProgressBar(0, currentDataView.Table.Rows.Count);
			}
			int num = 0;
			foreach (object obj in currentDataView.Table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (num++ > 7)
				{
					num = 0;
				}
				if (num == 0)
				{
					if (IncrementSubProgressBar != null)
					{
						IncrementSubProgressBar(1);
					}
				}
				if (dataRow[columnIndex] != DBNull.Value)
				{
					string text = dataRow[columnIndex].ToString().Trim();
					if (text.Length < 1)
					{
						dataRow[columnIndex] = DBNull.Value;
					}
				}
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0001DEA0 File Offset: 0x0001CEA0
		private static void AddColumnWithCountOfCommaSeparatedItemsInAnotherColumn(ref Report report, string newColName, string existingColName, string delimiter, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			Type type = Type.GetType("System.Int32");
			int columnIndex = currentDataView.Table.Columns.IndexOf(existingColName);
			currentDataView.Table.Columns.Add(newColName, type);
			int columnIndex2 = currentDataView.Table.Columns.Count - 1;
			if (SetupSubProgressBar != null)
			{
				SetupSubProgressBar(0, currentDataView.Table.Rows.Count);
			}
			char[] separator = delimiter.ToCharArray();
			foreach (object obj in currentDataView.Table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (IncrementSubProgressBar != null)
				{
					IncrementSubProgressBar(1);
				}
				if (dataRow[columnIndex] == DBNull.Value)
				{
					dataRow[columnIndex2] = 0;
				}
				else
				{
					string text = dataRow[columnIndex].ToString().Trim();
					if (text.Length > 0)
					{
						string[] array = text.Split(separator);
						int num = 0;
						foreach (string text2 in array)
						{
							text = text2.Trim();
							if (text.Length > 0)
							{
								num++;
							}
						}
						dataRow[columnIndex2] = num;
					}
					else
					{
						dataRow[columnIndex2] = 0;
					}
				}
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0001E07C File Offset: 0x0001D07C
		private static void AddTimeDurationColumn(ref Report report, string startEndDatesColNames, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			string[] array = startEndDatesColNames.Split(new char[]
			{
				','
			});
			string columnName = array[0].Trim();
			string columnName2 = array[1].Trim();
			int columnIndex = currentDataView.Table.Columns.IndexOf(columnName);
			int columnIndex2 = currentDataView.Table.Columns.IndexOf(columnName2);
			currentDataView.Table.Columns.Add("Duration_hours", Type.GetType("System.Double"));
			int columnIndex3 = currentDataView.Table.Columns.Count - 1;
			if (SetupSubProgressBar != null)
			{
				SetupSubProgressBar(0, currentDataView.Table.Rows.Count);
			}
			foreach (object obj in currentDataView.Table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (IncrementSubProgressBar != null)
				{
					IncrementSubProgressBar(1);
				}
				if (dataRow[columnIndex] != DBNull.Value && dataRow[columnIndex2] != DBNull.Value)
				{
					DateTime d = (DateTime)dataRow[columnIndex];
					DateTime d2 = (DateTime)dataRow[columnIndex2];
					d = new DateTime(2000, 1, 1, d.Hour, d.Minute, d.Second);
					d2 = new DateTime(2000, 1, 1, d2.Hour, d2.Minute, d2.Second);
					TimeSpan timeSpan = d2 - d;
					double num = Convert.ToDouble(timeSpan.Minutes) / 60.0;
					dataRow[columnIndex3] = Convert.ToDouble(timeSpan.Hours) + num;
				}
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0001E294 File Offset: 0x0001D294
		public static DataView CloneDataView(DataView oldDv, DataTable newTable)
		{
			return new DataView(newTable, oldDv.RowFilter, oldDv.Sort, oldDv.RowStateFilter);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0001E2C0 File Offset: 0x0001D2C0
		public static DataView CopyDataView(DataView dv)
		{
			DataTable table = dv.Table.Copy();
			return new DataView(table, dv.RowFilter, dv.Sort, dv.RowStateFilter);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0001E2F8 File Offset: 0x0001D2F8
		public static DataView CloneDataView(DataView dv)
		{
			DataTable table = dv.Table.Clone();
			return new DataView(table, dv.RowFilter, dv.Sort, DataViewRowState.Unchanged);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0001E32C File Offset: 0x0001D32C
		private static void MultiplyRows(ref Report report, string colName, string delimiter, SetupProgressBar SetupSubProgressBar, IncrementProgressBar IncrementSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataView dataView = ReportFunction.CloneDataView(currentDataView);
			DataTable table = dataView.Table;
			int num = table.Columns.IndexOf(colName);
			if (num >= 0)
			{
				if (SetupSubProgressBar != null)
				{
					SetupSubProgressBar(0, currentDataView.Table.Rows.Count);
				}
				if (delimiter.CompareTo("<cr>") == 0)
				{
					delimiter = System.Environment.NewLine;
				}
				else if (delimiter.IndexOf("<chr(") == 0)
				{
					string text = delimiter.Substring(5);
					text = text.Substring(0, text.Length - 1);
					int num2 = int.Parse(text);
					text = ((char)num2).ToString();
				}
				for (int i = 0; i < currentDataView.Table.Rows.Count; i++)
				{
					if (IncrementSubProgressBar != null)
					{
						IncrementSubProgressBar(1);
					}
					DataRow dataRow = currentDataView.Table.Rows[i];
					object[] itemArray = dataRow.ItemArray;
					string[] array = dataRow[num].ToString().Split(delimiter.ToCharArray());
					if (array.Length > 1)
					{
						itemArray[num] = array[0];
						table.LoadDataRow(itemArray, true);
						for (int j = 1; j < array.Length; j++)
						{
							itemArray[num] = array[j];
							table.LoadDataRow(itemArray, true);
						}
					}
					else
					{
						table.LoadDataRow(itemArray, true);
					}
				}
			}
			report.ReplaceDataView(currentDataView, dataView);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0001E4DC File Offset: 0x0001D4DC
		private static void CreateNewBooleanColumnsFromUniqueValuesInAColumn(ref Report report, string colName, IncrementProgressBar incrementSubProgressBar, SetupProgressBar setupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataView dataView = ReportFunction.CloneDataView(currentDataView);
			int num = dataView.Table.Columns.IndexOf(colName);
			if (num >= 0)
			{
				if (setupSubProgressBar != null)
				{
					setupSubProgressBar(0, currentDataView.Table.Rows.Count * 2 + 1);
				}
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = new ArrayList();
				foreach (object obj in currentDataView.Table.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					if (incrementSubProgressBar != null)
					{
						incrementSubProgressBar(1);
					}
					string text;
					if (dataRow[num] == DBNull.Value)
					{
						text = "";
					}
					else
					{
						text = dataRow[num].ToString().Trim().ToLower();
					}
					if (text.Length > 0)
					{
						if (!arrayList2.Contains(text))
						{
							arrayList2.Add(text);
							arrayList.Add(dataRow[num].ToString().Trim());
						}
					}
				}
				if (arrayList.Count > 0)
				{
					Type type = Type.GetType("System.Boolean");
					int[] array = new int[arrayList.Count];
					for (int i = 0; i < arrayList.Count; i++)
					{
						string columnName = (string)arrayList[i];
						dataView.Table.Columns.Add(columnName, type);
						array[i] = dataView.Table.Columns.Count - 1;
					}
					foreach (object obj2 in currentDataView.Table.Rows)
					{
						DataRow dataRow = (DataRow)obj2;
						if (incrementSubProgressBar != null)
						{
							incrementSubProgressBar(1);
						}
						string text = dataRow[num].ToString().Trim().ToLower();
						object[] itemArray = dataRow.ItemArray;
						object[] array2 = new object[dataView.Table.Columns.Count];
						for (int j = 0; j < itemArray.Length; j++)
						{
							array2[j] = itemArray[j];
						}
						for (int k = 0; k < array.Length; k++)
						{
							if (((string)arrayList2[k]).CompareTo(text) == 0)
							{
								array2[array[k]] = true;
								break;
							}
						}
						dataView.Table.LoadDataRow(array2, true);
					}
				}
				arrayList2.Clear();
				arrayList2 = null;
				arrayList.Clear();
				arrayList = null;
			}
			report.ReplaceDataView(currentDataView, dataView);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0001E820 File Offset: 0x0001D820
		private static void RunAnotherReportAndConcatenateRowsThatArentAlreadyThere(ref Report report, DataView dv, string matchingColsStr, string colsToImportStr, SetupProgressBar SetupSubProgressBar, IncrementProgressBar IncrementSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			if (currentDataView != null && currentDataView.Table != null && currentDataView.Table.Rows.Count > 0)
			{
				string[] array = matchingColsStr.ToLower().Split(new char[]
				{
					','
				});
				string[] array2 = colsToImportStr.ToLower().Split(new char[]
				{
					','
				});
				int[] array3 = new int[dv.Table.Columns.Count];
				ArrayList arrayList = new ArrayList(array.Length);
				ArrayList arrayList2 = new ArrayList(array2.Length);
				ArrayList arrayList3 = new ArrayList(array.Length);
				ArrayList arrayList4 = new ArrayList(array2.Length);
				for (int i = 0; i < dv.Table.Columns.Count; i++)
				{
					string text = dv.Table.Columns[i].ColumnName.ToLower();
					bool flag = Array.IndexOf<string>(array, text) >= 0;
					bool flag2 = Array.IndexOf<string>(array2, text) >= 0;
					if (flag || flag2)
					{
						if (flag)
						{
							arrayList.Add(i);
							int num = currentDataView.Table.Columns.IndexOf(text);
							arrayList3.Add(num);
						}
						else if (flag2)
						{
							arrayList2.Add(i);
							arrayList4.Add(currentDataView.Table.Columns.IndexOf(text));
						}
						int num2 = currentDataView.Table.Columns.IndexOf(dv.Table.Columns[i].ColumnName);
						if (num2 >= 0)
						{
							array3[i] = num2;
						}
						else
						{
							DataColumn dataColumn = currentDataView.Table.Columns.Add(dv.Table.Columns[i].ColumnName);
							array3[i] = dataColumn.Ordinal;
						}
					}
					else
					{
						array3[i] = -1;
					}
				}
				if (SetupSubProgressBar != null)
				{
					SetupSubProgressBar(0, dv.Count + 1);
				}
				for (int j = 0; j < dv.Count; j++)
				{
					if (IncrementSubProgressBar != null)
					{
						IncrementSubProgressBar(1);
					}
					bool flag3 = false;
					for (int k = 0; k < currentDataView.Table.Rows.Count; k++)
					{
						bool flag4 = true;
						for (int l = 0; l < arrayList.Count; l++)
						{
							int num3 = (int)arrayList[l];
							int num4 = (int)arrayList3[l];
							if (dv[j][num3] != DBNull.Value || currentDataView.Table.Rows[k][num4] != DBNull.Value)
							{
								if (dv[j][num3] == DBNull.Value || currentDataView.Table.Rows[k][num4] == DBNull.Value)
								{
									flag4 = false;
									break;
								}
								if (dv.Table.Columns[num3].DataType == currentDataView.Table.Columns[num4].DataType)
								{
									Type dataType = dv.Table.Columns[num3].DataType;
									if (dataType == typeof(int))
									{
										int num5 = (int)dv[j][num3];
										int num6 = (int)currentDataView.Table.Rows[k][num4];
										if (num5 != num6)
										{
											flag4 = false;
											break;
										}
									}
									else if (dataType == typeof(DateTime))
									{
										DateTime d = (DateTime)dv[j][num3];
										DateTime d2 = (DateTime)currentDataView.Table.Rows[k][num4];
										if (d != d2)
										{
											flag4 = false;
											break;
										}
									}
									else
									{
										string text2 = ((string)dv[j][num3]).Trim().ToLower();
										string strB = ((string)currentDataView.Table.Rows[k][num4]).Trim().ToLower();
										if (text2.CompareTo(strB) != 0)
										{
											flag4 = false;
											break;
										}
									}
								}
								else
								{
									string text2 = dv[j][num3].ToString().ToLower().Trim();
									string strB = currentDataView.Table.Rows[k][num4].ToString().ToLower().Trim();
									if (text2.CompareTo(strB) != 0)
									{
										flag4 = false;
										break;
									}
								}
							}
						}
						if (flag4)
						{
							flag3 = true;
							break;
						}
					}
					if (!flag3)
					{
						object[] array4 = new object[currentDataView.Table.Columns.Count];
						for (int m = 0; m < array3.Length; m++)
						{
							if (array3[m] >= 0)
							{
								array4[array3[m]] = dv[j].Row[m];
							}
						}
						currentDataView.Table.Rows.Add(array4);
					}
				}
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0001EE38 File Offset: 0x0001DE38
		private static void AddNewColumns(ref DataView dv, string newcolinfo)
		{
			string[] array = newcolinfo.Split(new char[]
			{
				'`'
			});
			foreach (string text in array)
			{
				string[] array3 = text.Split(new char[]
				{
					','
				});
				string columnName = array3[0].Trim();
				string text2;
				if (array3.Length > 1)
				{
					text2 = array3[1].Trim().ToLower();
				}
				else
				{
					text2 = "string";
				}
				string text3;
				if (array3.Length > 2)
				{
					text3 = "";
					for (int j = 2; j < array3.Length; j++)
					{
						text3 += array3[j];
					}
				}
				else
				{
					text3 = null;
				}
				int num = dv.Table.Columns.IndexOf(columnName);
				if (num < 0)
				{
					if (text2.CompareTo("bool") == 0)
					{
						DataColumn dataColumn = dv.Table.Columns.Add(columnName, Type.GetType("System.Boolean"));
						if (text3 != null)
						{
							text3 = text3.Trim().ToLower();
							bool flag = text3 == "1" || text3 == "yes" || text3 == "true";
							foreach (object obj in dv.Table.Rows)
							{
								DataRow dataRow = (DataRow)obj;
								dataRow[dataColumn.Ordinal] = flag;
							}
						}
					}
					else if (text2.CompareTo("int") == 0)
					{
						DataColumn dataColumn = dv.Table.Columns.Add(columnName, Type.GetType("System.Int32"));
						if (text3 != null)
						{
							text3 = text3.Trim();
							int num2;
							if (text3.Length > 0)
							{
								try
								{
									num2 = int.Parse(text3);
								}
								catch
								{
									num2 = 0;
								}
							}
							else
							{
								num2 = 0;
							}
							foreach (object obj2 in dv.Table.Rows)
							{
								DataRow dataRow = (DataRow)obj2;
								dataRow[dataColumn.Ordinal] = num2;
							}
						}
					}
					else
					{
						DataColumn dataColumn = dv.Table.Columns.Add(columnName);
						if (text3 != null)
						{
							foreach (object obj3 in dv.Table.Rows)
							{
								DataRow dataRow = (DataRow)obj3;
								dataRow[dataColumn.Ordinal] = text3;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0001F1D4 File Offset: 0x0001E1D4
		private static void StampTable(ref DataView dv, string newColName, string dtype, string newVal, SetupProgressBar SetupSubProgressBar, IncrementProgressBar IncrementSubProgressBar)
		{
			object value;
			if (dtype.CompareTo("bool") == 0)
			{
				dv.Table.Columns.Add(newColName, Type.GetType("System.Boolean"));
				newVal = newVal.Trim().ToLower();
				value = (newVal == "1" || newVal == "true" || newVal == "yes");
			}
			else if (dtype.CompareTo("int") == 0)
			{
				dv.Table.Columns.Add(newColName, Type.GetType("System.Int32"));
				newVal = newVal.Trim();
				try
				{
					value = int.Parse(newVal);
				}
				catch
				{
					value = 0;
				}
			}
			else
			{
				dv.Table.Columns.Add(newColName);
				value = newVal;
			}
			int columnIndex = dv.Table.Columns.Count - 1;
			SetupSubProgressBar(0, dv.Table.Rows.Count + 1);
			foreach (object obj in dv.Table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				IncrementSubProgressBar(1);
				dataRow[columnIndex] = value;
			}
			SetupSubProgressBar(0, 10);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0001F37C File Offset: 0x0001E37C
		private static Report ExecuteAnotherReport(UnivDataAdapter da, TechnoProReports technoProReports, int reportNumToRun, ref ArrayList errors, string dbName, DataSet comboBoxData, DataSet lookupTablesForControls, ArrayList variables, DataTable sessions, object[] yearStartEnd, DataTable dynamicScreenNonDataControlsTable, DataTable searchCustomTable, TripleDESEncryptionClass tripleDES, DataTable staffNamesTable, int whoAmIPersonID, SetupProgressBar SetupSubProgressBar, IncrementProgressBar IncrementSubProgressBar)
		{
			DataTable dataTable = new DataTable();
			if (technoProReports != null)
			{
				dataTable = technoProReports.LoadSearchFromDataSet(reportNumToRun);
			}
			if (dataTable.Rows.Count < 1)
			{
				da.SelectCommand.CommandText = "SELECT si.searchinfoid,si.title,si.description,si.searchgroupid,si.datecreated,si.datelastmodified,si.whocreated,si.wholastmodified,sgi.grouptitle,sgi.groupdescription,sgi.iconindex,si.searchchartinfoid,si.overrideDynamicControlsScreenNum,1 AS dblocationcode FROM searchinfo si LEFT JOIN searchgroupinfo sgi ON sgi.searchgroupinfoid=si.searchgroupid WHERE si.searchinfoid=@searchinfoid";
				if (da.SelectCommand.Parameters.Contains("@searchinfoid"))
				{
					da.SelectCommand.Parameters.SetValue("@searchinfoid", reportNumToRun);
					da.SelectCommand.Parameters.Add("@searchinfoid", reportNumToRun);
				}
				else
				{
					da.SelectCommand.Parameters.Add("@searchinfoid", reportNumToRun);
				}
				string text = null;
				da.Fill(dataTable, out text);
				if (text != null && text.Length > 0)
				{
					errors.Add(text);
				}
			}
			Report result;
			if (dataTable.Rows.Count > 0)
			{
				ArrayList arrayList;
				result = ReportFunction.RunReport(false, dbName, dataTable.Rows[0], da, comboBoxData, lookupTablesForControls, variables, sessions, yearStartEnd, dynamicScreenNonDataControlsTable, searchCustomTable, tripleDES, null, IncrementSubProgressBar, null, SetupSubProgressBar, staffNamesTable, whoAmIPersonID, technoProReports, out arrayList, false, null, null, false, null);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0001F4CC File Offset: 0x0001E4CC
		public static void DecryptDynamicData(ref Report report, TripleDESEncryptionClass tripleDES)
		{
			DataView currentDataView = report.GetCurrentDataView();
			tripleDES.DecryptDataTableBatchDynamicData(currentDataView.Table, "valbytesisencrypted", "valbytes", "valtext");
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0001F4FE File Offset: 0x0001E4FE
		public static void DecryptData(ref Report report, string ColsToDecryptNames, TripleDESEncryptionClass tripleDES, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			ReportFunction.DecryptData(ref report, ColsToDecryptNames, null, "", "", tripleDES, IncrementSubProgressBar, SetupSubProgressBar);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0001F518 File Offset: 0x0001E518
		public static void DecryptData(ref Report report, string ColsToDecryptNames, string encryptionType, string encryptionKey, TripleDESEncryptionClass tripleDES, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			ReportFunction.DecryptData(ref report, ColsToDecryptNames, null, encryptionType, encryptionKey, tripleDES, IncrementSubProgressBar, SetupSubProgressBar);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0001F52C File Offset: 0x0001E52C
		public static void DecryptData(ref Report report, string ColsToDecryptNames, UnivDataAdapter da, string encryptionType, string encryptionKey, TripleDESEncryptionClass tripleDESClockWork, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			TripleDESEncryptionClass tripleDES = ReportFunction.CreateTripleDES(da, encryptionType, encryptionKey, tripleDESClockWork);
			DataTable table = currentDataView.Table;
			string[] array;
			if (ColsToDecryptNames.Trim().Length > 0)
			{
				array = ColsToDecryptNames.ToLower().Split(new char[]
				{
					','
				});
			}
			else
			{
				array = new string[table.Columns.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = table.Columns[i].ColumnName;
				}
			}
			DataTable dataTable = ReportFunction.DecryptData(table, array, tripleDES);
			report.ReplaceDataView(currentDataView, dataTable.DefaultView);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0001F5E7 File Offset: 0x0001E5E7
		public static void EncryptData(ref Report report, string ColsToEncryptNames, TripleDESEncryptionClass tripleDES, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			ReportFunction.EncryptData(ref report, ColsToEncryptNames, "", "", tripleDES, IncrementSubProgressBar, SetupSubProgressBar);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0001F600 File Offset: 0x0001E600
		public static void EncryptData(ref Report report, string ColsToEncryptNames, string encryptionType, string encryptionKey, TripleDESEncryptionClass tripleDES, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			ReportFunction.EncryptData(ref report, ColsToEncryptNames, encryptionType, encryptionKey, tripleDES, IncrementSubProgressBar, SetupSubProgressBar, null);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0001F614 File Offset: 0x0001E614
		public static TripleDESEncryptionClass CreateTripleDES(UnivDataAdapter da, string encryptionType, string encryptionKey, TripleDESEncryptionClass tripleDES)
		{
			if (encryptionKey.IndexOf("#<") == 0 && encryptionKey.IndexOf(">#") > 2)
			{
				string text = encryptionKey.Substring(2, encryptionKey.Length - 4);
				bool flag = text.Length > 0 && text[0] == '.';
				if (flag)
				{
					text = text.Substring(1);
				}
				da.SelectCommand.CommandText = "SELECT settingstringvalue FROM settingsgroups WHERE settingcode=" + text;
				DataTable dataTable = new DataTable();
				da.Fill(dataTable);
				if (dataTable.Rows.Count > 0)
				{
					byte[] inputInBytes = ClockWorkCore.base64Decode(dataTable.Rows[0][0].ToString());
					encryptionKey = tripleDES.Decrypt(inputInBytes);
					if (!flag)
					{
						byte[][] bytes = TripleDESEncryptionClass.GetBytes(true, encryptionKey);
						tripleDES = new TripleDESEncryptionClass(EncryptionType.TripleDES_192bit, bytes[0], bytes[1]);
					}
				}
			}
			if (encryptionType.Length > 0)
			{
				if (encryptionType.ToLower().Trim().CompareTo("tripledes_128bit") == 0)
				{
					byte[][] bytes = TripleDESEncryptionClass.GetBytes(false, encryptionKey);
					tripleDES = new TripleDESEncryptionClass(EncryptionType.TripleDES_128bit, bytes[0], bytes[1]);
				}
				else
				{
					byte[][] bytes = TripleDESEncryptionClass.GetBytes(true, encryptionKey);
					tripleDES = new TripleDESEncryptionClass(EncryptionType.TripleDES_192bit, bytes[0], bytes[1]);
				}
			}
			return tripleDES;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0001F77C File Offset: 0x0001E77C
		public static void EncryptData(ref Report report, string ColsToEncryptNames, string encryptionType, string encryptionKey, TripleDESEncryptionClass tripleDESClockWork, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar, UnivDataAdapter da)
		{
			DataView currentDataView = report.GetCurrentDataView();
			TripleDESEncryptionClass tripleDESEncryptionClass = ReportFunction.CreateTripleDES(da, encryptionType, encryptionKey, tripleDESClockWork);
			DataTable table = currentDataView.Table;
			string[] colNamesToEncryptOrDecryptInLowerCase = ColsToEncryptNames.ToLower().Split(new char[]
			{
				','
			});
			DataTable table2 = tripleDESEncryptionClass.EncryptOrDecryptNameDataTableBatch(true, table, colNamesToEncryptOrDecryptInLowerCase);
			DataView dataView = new DataView(table2);
			dataView.Sort = currentDataView.Sort;
			report.ReplaceDataView(currentDataView, dataView);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0001F7F0 File Offset: 0x0001E7F0
		public static void Sort(ref Report report, string colsToSortBy, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			string[] array = colsToSortBy.Split(new char[]
			{
				','
			});
			string text = "";
			foreach (string text2 in array)
			{
				string text3 = text2.Trim();
				if (currentDataView.Table.Columns.Contains(text3))
				{
					ReportFunction.AddToList(ref text, text3);
				}
			}
			if (text.Trim().Length >= 1)
			{
				try
				{
					currentDataView.Sort = text;
				}
				catch (Exception ex)
				{
					report.LogError("Sort [Can't sort because one of the columns specified doesn't exist!]", ex);
				}
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0001F8C0 File Offset: 0x0001E8C0
		public static void BreakdownData(ref Report report, string ColsToBreakdown, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView dataView = report.GetCurrentDataView();
			DataTable table = dataView.Table;
			string[] array = ColsToBreakdown.Split(new char[]
			{
				','
			});
			int[] array2 = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = table.Columns.IndexOf(array[i]);
			}
			DataTable dataTable = new DataTable("Breakdown " + table.TableName);
			foreach (int num in array2)
			{
				if (num >= 0)
				{
					string text = table.Columns[num].ColumnName;
					string str = text;
					int num2 = 0;
					while (dataTable.Columns.IndexOf(text) >= 0)
					{
						num2++;
						text = str + num2.ToString();
					}
					dataTable.Columns.Add(text, table.Columns[num].DataType);
				}
			}
			Type type = Type.GetType("System.Int32");
			dataTable.Columns.Add("Count", type);
			ReportFunction.CallSetupProgressBar(SetupSubProgressBar, 0, Convert.ToInt32(Convert.ToDouble(table.Rows.Count) / 6.0));
			int num3 = 0;
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (num3++ == 0 && IncrementSubProgressBar != null)
				{
					IncrementSubProgressBar(1);
				}
				if (num3 > 5)
				{
					num3 = 0;
				}
				DataRow dataRow2 = null;
				foreach (object obj2 in dataTable.Rows)
				{
					DataRow dataRow3 = (DataRow)obj2;
					bool flag = true;
					for (int i = 0; i < array2.Length; i++)
					{
						string text2 = dataRow3[i].ToString().Trim().ToLower();
						string strB = dataRow[array2[i]].ToString().Trim().ToLower();
						if (text2.CompareTo(strB) != 0)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						dataRow2 = dataRow3;
						break;
					}
				}
				if (dataRow2 == null)
				{
					object[] array4 = new object[dataTable.Columns.Count];
					for (int i = 0; i < array2.Length; i++)
					{
						int columnIndex = array2[i];
						array4[i] = dataRow[columnIndex];
					}
					array4[array2.Length] = 0;
					dataRow2 = dataTable.Rows.Add(array4);
				}
				int num4 = (int)dataRow2[array2.Length];
				dataRow2[array2.Length] = num4 + 1;
			}
			dataView = new DataView(dataTable);
			if (dataTable != null && dataTable.Columns.Count > 2)
			{
				dataView.Sort = dataTable.Columns[0].ColumnName + "," + dataTable.Columns[1].ColumnName;
			}
			ReportFunction.RemoveItems(ref report, "ItemValue", "", IncrementSubProgressBar, SetupSubProgressBar);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0001FCC4 File Offset: 0x0001ECC4
		public static void RemoveItems(ref Report report, string columnName, string valueToRemove, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			string columnName2;
			bool flag;
			if (columnName[0] == '!')
			{
				columnName2 = columnName.Substring(1);
				flag = true;
			}
			else
			{
				columnName2 = columnName;
				flag = false;
			}
			if (currentDataView != null)
			{
				DataTable table = currentDataView.Table;
				int num = table.Columns.IndexOf(columnName2);
				if (num >= 0)
				{
					DataTable dataTable = table.Clone();
					string strB = valueToRemove.Trim().ToLower();
					foreach (object obj in currentDataView)
					{
						DataRowView dataRowView = (DataRowView)obj;
						DataRow row = dataRowView.Row;
						string text = row[num].ToString().Trim().ToLower();
						bool flag2 = text.CompareTo(strB) == 0;
						if (!flag)
						{
							if (text.CompareTo(strB) != 0)
							{
								dataTable.LoadDataRow(row.ItemArray, true);
							}
						}
						else if (text.CompareTo(strB) == 0)
						{
							dataTable.LoadDataRow(row.ItemArray, true);
						}
					}
					report.ReplaceDataView(currentDataView, dataTable.DefaultView);
				}
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0001FE44 File Offset: 0x0001EE44
		public static void UpdateDataTableBasedOnIntegerPrimaryKey(DataTable tMain, int tMainPidColInd, int tMainStartIndex, int tMainEndIndex, DataTable tResource, int tResourcePidColInd)
		{
			DataView dataView = new DataView(tMain);
			dataView.Sort = tMain.Columns[tMainPidColInd].ColumnName;
			foreach (object obj in new DataView(tResource)
			{
				Sort = tResource.Columns[tResourcePidColInd].ColumnName
			})
			{
				DataRowView dataRowView = (DataRowView)obj;
				DataRow row = dataRowView.Row;
				int num = (int)row[tResourcePidColInd];
				int num4;
				for (int i = 0; i < dataView.Count; i = num4 + 1)
				{
					DataRowView dataRowView2 = dataView[i];
					DataRow row2 = dataRowView2.Row;
					int num2 = (int)row2[tMainPidColInd];
					int num3 = i;
					num4 = i;
					for (int j = num3 + 1; j < dataView.Count; j++)
					{
						DataRowView dataRowView3 = dataView[j];
						DataRow row3 = dataRowView3.Row;
						int num5 = (int)row3[tMainPidColInd];
						if (num5 != num2)
						{
							break;
						}
						num4 = j;
					}
					if (num == num2)
					{
						for (int k = 0; k < tResource.Columns.Count; k++)
						{
							if (k != tResourcePidColInd && tResource.Columns[k].ColumnMapping != MappingType.Hidden)
							{
								int num6 = tMain.Columns.IndexOf(tResource.Columns[k].ColumnName);
								if (num6 >= 0)
								{
									for (int l = num3; l <= num4; l++)
									{
										DataRowView dataRowView4 = dataView[l];
										DataRow row4 = dataRowView4.Row;
										row4[num6] = row[k];
									}
								}
							}
						}
						break;
					}
				}
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00020094 File Offset: 0x0001F094
		private static void AddColumnClearIfExists(DataTable t, string colName, Type dataType)
		{
			int num = t.Columns.IndexOf(colName);
			if (num >= 0)
			{
				foreach (object obj in t.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					dataRow[num] = DBNull.Value;
				}
			}
			else
			{
				t.Columns.Add(colName, dataType);
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0002012C File Offset: 0x0001F12C
		public static void NoShowCancelledReport(ref Report report, UnivDataAdapter da, int pidColInd, DateTime sdate, DateTime edate, string customAppSql, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			if (pidColInd >= 0)
			{
				int num = 50;
				DataView dataView = new DataView(currentDataView.Table);
				dataView.Sort = dataView.Table.Columns[pidColInd].ColumnName;
				ReportFunction.AddColumnClearIfExists(dataView.Table, "NoshowCount_DateRange", typeof(int));
				ReportFunction.AddColumnClearIfExists(dataView.Table, "CancelledCount_DateRange", typeof(int));
				ReportFunction.AddColumnClearIfExists(dataView.Table, "TotalNoshowCount", typeof(int));
				ReportFunction.AddColumnClearIfExists(dataView.Table, "TotalCancelledCount", typeof(int));
				if (SetupSubProgressBar != null)
				{
					SetupSubProgressBar(0, dataView.Count);
				}
				int i = 0;
				int num2 = -1;
				while (i < dataView.Count)
				{
					int num3 = i;
					int num4 = i;
					int num5 = 0;
					int num6 = num3 + 1;
					string text = "";
					while (num5 < num && num6 < dataView.Count)
					{
						DataRowView dataRowView = currentDataView[num6];
						DataRow row = dataRowView.Row;
						int num7 = (int)row[pidColInd];
						if (num7 != num2)
						{
							if (text.Length > 0)
							{
								text += ",";
							}
							text += num7.ToString();
							num2 = num7;
							num5++;
						}
						num4 = num6;
						num6++;
					}
					ClockWorkBaseDataAccess clockWorkBaseDataAccess = new ClockWorkDirectDataAccess(da, null);
					DataTable dataTable = clockWorkBaseDataAccess.LoadNoshowCounts(text, sdate, edate, false, customAppSql);
					dataTable.Columns["noshowcount"].ColumnName = "NoshowCount_DateRange";
					ReportFunction.UpdateDataTableBasedOnIntegerPrimaryKey(currentDataView.Table, pidColInd, num3, num4, dataTable, 0);
					DataTable dataTable2 = clockWorkBaseDataAccess.LoadNoshowCounts(text, sdate, edate, true, customAppSql);
					dataTable2.Columns["noshowcount"].ColumnName = "TotalNoshowCount";
					ReportFunction.UpdateDataTableBasedOnIntegerPrimaryKey(currentDataView.Table, pidColInd, num3, num4, dataTable2, 0);
					DataTable dataTable3 = clockWorkBaseDataAccess.LoadCancelledCounts(text, sdate, edate, false, customAppSql);
					dataTable3.Columns["cancelledcount"].ColumnName = "CancelledCount_DateRange";
					ReportFunction.UpdateDataTableBasedOnIntegerPrimaryKey(currentDataView.Table, pidColInd, num3, num4, dataTable3, 0);
					DataTable dataTable4 = clockWorkBaseDataAccess.LoadCancelledCounts(text, sdate, edate, true, customAppSql);
					dataTable4.Columns["cancelledcount"].ColumnName = "TotalCancelledCount";
					ReportFunction.UpdateDataTableBasedOnIntegerPrimaryKey(currentDataView.Table, pidColInd, num3, num4, dataTable4, 0);
					if (IncrementSubProgressBar != null)
					{
						IncrementSubProgressBar(num4 - num3 + 1);
					}
					i = num4 + 1;
				}
				report.ReplaceDataView(currentDataView, dataView);
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00020404 File Offset: 0x0001F404
		public static void ReorderColumns(ref Report report, string newColNamesOrder, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			if (report != null)
			{
				DataView currentDataView = report.GetCurrentDataView();
				if (currentDataView != null && currentDataView.Table.Columns.Count >= 1)
				{
					DataTable table = currentDataView.Table;
					int[] array = new int[table.Columns.Count];
					string[] array2 = newColNamesOrder.Split(new char[]
					{
						','
					});
					DataTable dataTable = new DataTable();
					bool[] array3 = new bool[table.Columns.Count];
					for (int i = 0; i < array3.Length; i++)
					{
						array3[i] = false;
					}
					for (int i = 0; i < table.Columns.Count; i++)
					{
						int num2;
						if (i < array2.Length)
						{
							string text = array2[i];
							string strB = text.ToLower().Trim();
							int num = -1;
							for (int j = 0; j < table.Columns.Count; j++)
							{
								string text2 = table.Columns[j].ColumnName.ToLower().Trim();
								if (text2.CompareTo(strB) == 0)
								{
									num = j;
									break;
								}
							}
							if (num < 0)
							{
								int k = 0;
								while (array3[k++])
								{
								}
								num2 = k;
								array3[num2] = true;
							}
							else
							{
								num2 = num;
								array3[num] = true;
							}
						}
						else
						{
							int k = -1;
							while (k < array3.Length - 1)
							{
								k++;
								if (!array3[k])
								{
									break;
								}
							}
							num2 = k;
							array3[k] = true;
						}
						array[i] = num2;
						DataColumn dataColumn = table.Columns[num2];
						string text3 = dataColumn.ColumnName;
						if (dataTable.Columns.IndexOf(dataColumn.ColumnName) >= 0)
						{
							text3 += i.ToString();
						}
						dataTable.Columns.Add(new DataColumn(text3, dataColumn.DataType, dataColumn.Expression));
					}
					if (SetupSubProgressBar != null)
					{
						SetupSubProgressBar(0, table.Columns.Count);
					}
					foreach (object obj in currentDataView)
					{
						DataRowView dataRowView = (DataRowView)obj;
						if (IncrementSubProgressBar != null)
						{
							IncrementSubProgressBar(1);
						}
						DataRow row = dataRowView.Row;
						DataRow dataRow = dataTable.NewRow();
						for (int i = 0; i < table.Columns.Count; i++)
						{
							dataRow[i] = row[array[i]];
						}
						dataTable.Rows.Add(dataRow);
					}
					report.ReplaceDataView(currentDataView, dataTable.DefaultView);
				}
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00020740 File Offset: 0x0001F740
		public static string GetEmailsCommaSeparated(DataView dv, string emailColName)
		{
			DataTable table = dv.Table;
			int num = table.Columns.IndexOf(emailColName);
			string result;
			if (num >= 0)
			{
				string text = "";
				foreach (object obj in table.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					string text2 = dataRow[num].ToString().Trim();
					if (text2.Length > 0)
					{
						if (text.Length > 0)
						{
							text += ",";
						}
						text += text2;
					}
				}
				result = text;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00020830 File Offset: 0x0001F830
		public static string GetEmailsCommaSeparatedFromTableWithEmailTag(DataView dv, string emailColName, string EmailTag, string emailValueName)
		{
			DataTable table = dv.Table;
			int num = table.Columns.IndexOf(emailColName);
			int num2 = table.Columns.IndexOf(emailValueName);
			string strB = EmailTag.Trim().ToLower();
			string result;
			if (num >= 0 && num2 >= 0)
			{
				string text = "";
				foreach (object obj in table.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					string text2 = dataRow[num].ToString().Trim().ToLower();
					if (text2.CompareTo(strB) == 0)
					{
						string text3 = dataRow[num2].ToString().Trim();
						if (text3.Length > 0)
						{
							if (text.Length > 0)
							{
								text += ",";
							}
							text += text3;
						}
					}
				}
				result = text;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0002097C File Offset: 0x0001F97C
		public static string GetEmailsCommaSeparatedFromTableWithEmailColumnOrNoEmailColumn(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, DataView dv, string studentnumColName, string emailColName, out int TotalUniqueStudents, out int NumStudentsWithEmails)
		{
			DataTable table = dv.Table;
			int num = table.Columns.IndexOf(studentnumColName);
			string result;
			if (num >= 0)
			{
				int num2;
				if (emailColName != null && emailColName.Length > 0)
				{
					num2 = table.Columns.IndexOf(emailColName);
				}
				else
				{
					num2 = -1;
				}
				string text = "";
				ArrayList arrayList = new ArrayList(table.Rows.Count);
				int num3 = 0;
				foreach (object obj in table.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					if (dataRow.RowState != DataRowState.Deleted)
					{
						string text2 = dataRow[num].ToString().Trim();
						if (!arrayList.Contains(text2))
						{
							arrayList.Add(text2);
							string text3;
							if (num2 < 0)
							{
								byte[] parameterValue = tripleDES.Encrypt(text2);
								da.SelectCommand.CommandText = "SELECT controlvalue FROM otherinfops WHERE personid IN (SELECT personid FROM people WHERE student_no=@student_no) AND controlid=13";
								da.SelectCommand.Parameters.Clear();
								da.SelectCommand.Parameters.Add("@student_no", parameterValue);
								DataTable dataTable = new DataTable();
								da.Fill(dataTable);
								if (dataTable.Rows.Count > 0)
								{
									byte[] inputInBytes = (byte[])dataTable.Rows[0][0];
									text3 = tripleDES.Decrypt(inputInBytes);
								}
								else
								{
									text3 = "";
								}
							}
							else
							{
								text3 = dataRow[num2].ToString().Trim();
							}
							if (text3.Length > 0)
							{
								if (text.Length > 0)
								{
									text += ",";
								}
								text += text3;
								num3++;
							}
						}
					}
				}
				TotalUniqueStudents = arrayList.Count;
				NumStudentsWithEmails = num3;
				result = text;
			}
			else
			{
				TotalUniqueStudents = -1;
				NumStudentsWithEmails = -1;
				result = null;
			}
			return result;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00020BD4 File Offset: 0x0001FBD4
		public static void ExtractUniqueRows(ref Report report, string[] colNames)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			int[] array = new int[colNames.Length];
			for (int i = 0; i < colNames.Length; i++)
			{
				string strB = colNames[i].ToLower().Trim();
				bool flag = false;
				for (int j = 0; j < table.Columns.Count; j++)
				{
					string text = table.Columns[j].ColumnName.ToLower();
					if (text.CompareTo(strB) == 0)
					{
						flag = true;
						array[i] = j;
					}
				}
				if (!flag)
				{
					array = null;
					break;
				}
			}
			if (array != null)
			{
				ArrayList arrayList = new ArrayList(table.Rows.Count);
				DataTable dataTable = table.Clone();
				foreach (object obj in table.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					string text2 = "";
					for (int i = 0; i < array.Length; i++)
					{
						text2 += dataRow[array[i]].ToString().Trim().ToLower();
					}
					if (!arrayList.Contains(text2))
					{
						arrayList.Add(text2);
						ReportFunction.ImportRowCopy(dataTable, dataRow);
					}
				}
				report.ReplaceDataView(currentDataView, dataTable.DefaultView);
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00020D9C File Offset: 0x0001FD9C
		private static DataRow ImportRowCopy(DataTable newTable, DataRow originalDR)
		{
			int count = newTable.Columns.Count;
			DataRow dataRow = newTable.NewRow();
			for (int i = 0; i < count; i++)
			{
				dataRow[i] = originalDR[i];
			}
			newTable.Rows.Add(dataRow);
			dataRow.AcceptChanges();
			return dataRow;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00020DFC File Offset: 0x0001FDFC
		public static DataView ConcatenateDataViews(DataView[] dvs)
		{
			ArrayList arrayList = new ArrayList();
			DataTable dataTable = new DataTable();
			foreach (DataView dataView in dvs)
			{
				DataTable table = dataView.Table;
				for (int j = 0; j < table.Columns.Count; j++)
				{
					string text = table.Columns[j].ColumnName.ToLower().Trim();
					table.Columns[j].ColumnName = text;
					if (!arrayList.Contains(text))
					{
						arrayList.Add(text);
						dataTable.Columns.Add(text, table.Columns[j].DataType);
					}
				}
			}
			foreach (DataView dataView in dvs)
			{
				DataTable table = dataView.Table;
				foreach (object obj in table.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					DataRow dataRow2 = dataTable.NewRow();
					for (int j = 0; j < table.Columns.Count; j++)
					{
						string text = table.Columns[j].ColumnName.ToLower().Trim();
						dataRow2[text] = dataRow[text];
					}
					dataTable.Rows.Add(dataRow2);
				}
			}
			return new DataView(dataTable);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00020FE4 File Offset: 0x0001FFE4
		public static void RenameColumns(ref Report report, string[] colOldNameEqualsNewName, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			ReportFunction.RenameColumns(table, colOldNameEqualsNewName, IncrementSubProgressBar, SetupSubProgressBar);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0002100C File Offset: 0x0002000C
		public static void RenameColumns(DataTable t, string[] colOldNameEqualsNewName, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			if (colOldNameEqualsNewName.Length >= 1)
			{
				ReportFunction.CallSetupProgressBar(SetupSubProgressBar, 0, colOldNameEqualsNewName.Length);
				foreach (string text in colOldNameEqualsNewName)
				{
					ReportFunction.CallIncrementProgressBar(IncrementSubProgressBar);
					string[] array = text.Split(new char[]
					{
						'='
					});
					if (array.Length == 2)
					{
						string name = array[0];
						string columnName = array[1];
						DataColumn dataColumn = t.Columns[name];
						if (dataColumn != null)
						{
							dataColumn.ColumnName = columnName;
						}
					}
				}
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000210BC File Offset: 0x000200BC
		public static void SplitColDataIntoMultipleColumns(ref Report report, string info, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			string[] array = info.Split(System.Environment.NewLine.ToCharArray());
			if (array.Length >= 1)
			{
				ReportFunction.CallSetupProgressBar(SetupSubProgressBar, 0, array.Length);
				foreach (string text in array)
				{
					ReportFunction.CallIncrementProgressBar(IncrementSubProgressBar);
					if (text.Trim().Length > 0)
					{
						string[] array3 = text.Split(new char[]
						{
							'`'
						});
						if (array3.Length >= 3)
						{
							string text2 = array3[0];
							string text3 = array3[1];
							string text4 = array3[2];
							string[] array4 = text4.Split(new char[]
							{
								','
							});
							if (array4.Length > 0)
							{
								text3 = ReportFunction.AddStringColumn(ref table, text3, text2.GetType());
								int num = table.Columns.IndexOf(text3);
								int num2 = table.Columns.IndexOf(text2);
								if (num2 >= 0 && num >= 0)
								{
									foreach (object obj in table.Rows)
									{
										DataRow dataRow = (DataRow)obj;
										string[] array5 = dataRow[num2].ToString().Trim().Split(new char[]
										{
											','
										});
										string value = "";
										if (array5.Length > 0)
										{
											string value2 = dataRow[num].ToString().Trim();
											foreach (string text5 in array5)
											{
												string text6 = text5.Trim().ToLower();
												bool flag = false;
												foreach (string text7 in array4)
												{
													string text8 = text7.Trim().ToLower().Replace("*", "`");
													text8 = text8.Replace("**", "*");
													int num3 = text8.IndexOf('`');
													string text9;
													string text10;
													if (num3 == 0)
													{
														text9 = "";
														text10 = text8.Substring(num3 + 1);
													}
													else if (num3 == text8.Length - 1)
													{
														text9 = text8.Substring(0, num3);
														text10 = text8.Substring(num3 + 1);
													}
													else if (num3 > 0)
													{
														text10 = "";
														text9 = text8.Substring(0, num3);
													}
													else
													{
														text9 = "";
														text10 = "";
														if (text8.CompareTo(text6) == 0)
														{
															flag = true;
															break;
														}
													}
													bool flag2 = text9.Length > 0 && text6.Length >= text9.Length && text6.IndexOf(text9) == 0;
													bool flag3 = text10.Length > 0 && text6.Length >= text10.Length && text6.IndexOf(text10) == text6.Length - text10.Length;
													if (text9.Length > 0)
													{
														if (flag2)
														{
															if (text10.Length <= 0)
															{
																flag = true;
																break;
															}
															if (flag3)
															{
																flag = true;
																break;
															}
														}
													}
													else if (flag3)
													{
														flag = true;
														break;
													}
												}
												if (flag)
												{
													ReportFunction.AddToList(ref value2, text5.Trim());
												}
												else
												{
													ReportFunction.AddToList(ref value, text5);
												}
											}
											dataRow[num2] = value;
											dataRow[num] = value2;
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0002152C File Offset: 0x0002052C
		private static string Left(string s, int chars)
		{
			int length = s.Length;
			string result;
			if (chars <= 0)
			{
				result = "";
			}
			else if (chars >= length)
			{
				result = s;
			}
			else
			{
				result = s.Substring(0, chars);
			}
			return result;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0002156C File Offset: 0x0002056C
		private static string Right(string s, int chars)
		{
			int length = s.Length;
			string result;
			if (chars <= 0)
			{
				result = "";
			}
			else if (chars >= length)
			{
				result = s;
			}
			else
			{
				result = s.Substring(length - chars);
			}
			return result;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000215AC File Offset: 0x000205AC
		public static void AddToList(ref string list, string itemToAdd)
		{
			if (list.Length > 0)
			{
				list += ",";
			}
			list += itemToAdd;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000215E4 File Offset: 0x000205E4
		public static string AddStringColumn(ref DataTable t, string colName, Type dataType)
		{
			int num = 0;
			while (t.Columns.Contains(colName))
			{
				if (num == 0)
				{
					colName += num.ToString();
				}
				else
				{
					colName = colName.Substring(0, colName.Length - 1) + num.ToString();
				}
				num++;
			}
			t.Columns.Add(colName, dataType);
			return colName;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0002165C File Offset: 0x0002065C
		public static void CombineColumns(ref Report report, string[] colNameGroups, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			if (colNameGroups.Length >= 1 && table.Columns.Count >= 1 && table.Rows.Count >= 1)
			{
				int[] array = new int[colNameGroups.Length];
				for (int i = 0; i < colNameGroups.Length; i++)
				{
					string text = colNameGroups[i];
					string[] array2 = text.Split(new char[]
					{
						','
					});
					if (array2.Length >= 2)
					{
						int num = 0;
						for (int j = 0; j < array2.Length; j++)
						{
							string columnName = array2[j].Trim();
							int num2 = table.Columns.IndexOf(columnName);
							if (num2 > -1)
							{
								num++;
							}
						}
						if (num == 0)
						{
							return;
						}
						int[] array3 = new int[num];
						int num3 = 0;
						foreach (string text2 in array2)
						{
							string columnName = text2.Trim();
							int num2 = table.Columns.IndexOf(columnName);
							if (num2 > -1)
							{
								array3[num3++] = num2;
							}
						}
						ReportFunction.CallSetupProgressBar(SetupSubProgressBar, 0, table.Rows.Count);
						int num4 = table.Columns.Count;
						Type type = Type.GetType("System.String");
						foreach (int num5 in array3)
						{
							if (num5 >= 0 && table.Columns[num5].DataType == type)
							{
								num4 = num5;
								break;
							}
						}
						if (num4 >= table.Columns.Count)
						{
							int num6 = -1;
							for (int j = 0; j < array3.Length; j++)
							{
								if (array3[j] >= 0)
								{
									num6 = array3[j];
									break;
								}
							}
							string text3;
							if (num6 >= 0)
							{
								text3 = table.Columns[array3[num6]].ColumnName;
								DataColumn dataColumn = table.Columns[num6];
								dataColumn.ColumnName += "2";
							}
							else
							{
								text3 = "Unknown_";
							}
							int num7 = 2;
							while (table.Columns.Contains(text3))
							{
								text3 = text3.Substring(0, text3.Length - 1) + num7.ToString();
								num7++;
							}
							table.Columns.Add(text3);
						}
						array[i] = num4;
						foreach (object obj in table.Rows)
						{
							DataRow dataRow = (DataRow)obj;
							ReportFunction.CallIncrementProgressBar(IncrementSubProgressBar);
							string text4 = "";
							foreach (int num8 in array3)
							{
								if (num8 >= 0)
								{
									if (dataRow[num8] != DBNull.Value)
									{
										string text5 = dataRow[num8].ToString().Trim();
										if (text5.Length > 0)
										{
											if (text4.Length > 0)
											{
												text4 += ", ";
											}
											text4 += text5;
										}
									}
								}
							}
							dataRow[num4] = text4;
						}
					}
				}
				for (int i = 0; i < colNameGroups.Length; i++)
				{
					string text = colNameGroups[i];
					string[] array2 = text.Split(new char[]
					{
						','
					});
					if (array2.Length >= 2)
					{
						int num4 = array[i];
						int[] array3 = new int[array2.Length];
						for (int j = 0; j < array2.Length; j++)
						{
							string columnName = array2[j].Trim();
							int num2 = table.Columns.IndexOf(columnName);
							array3[j] = num2;
						}
						ArrayList arrayList = new ArrayList();
						foreach (int num2 in array3)
						{
							int num2;
							if (num2 != num4)
							{
								arrayList.Add(table.Columns[num2]);
							}
						}
						foreach (object obj2 in arrayList)
						{
							DataColumn dataColumn2 = (DataColumn)obj2;
							string columnName2 = dataColumn2.ColumnName;
							table.Columns.Remove(dataColumn2);
						}
					}
				}
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00021BA8 File Offset: 0x00020BA8
		public static void RemoveColumns(ref Report report, string[] colsToRemove, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			if (report != null)
			{
				DataView currentDataView = report.GetCurrentDataView();
				if (currentDataView != null)
				{
					DataTable table = currentDataView.Table;
					if (colsToRemove != null && colsToRemove.Length >= 1)
					{
						ReportFunction.CallSetupProgressBar(SetupSubProgressBar, 0, colsToRemove.Length * 2);
						ArrayList arrayList = new ArrayList(colsToRemove.Length);
						foreach (string name in colsToRemove)
						{
							ReportFunction.CallIncrementProgressBar(IncrementSubProgressBar);
							DataColumn dataColumn = table.Columns[name];
							if (dataColumn != null)
							{
								arrayList.Add(dataColumn);
							}
						}
						string[] array = currentDataView.Sort.Split(new char[]
						{
							','
						});
						if (array.Length > 0)
						{
							string text = "";
							foreach (string text2 in array)
							{
								DataColumn dataColumn = table.Columns[text2];
								if (dataColumn != null && !arrayList.Contains(dataColumn))
								{
									if (text.Length > 0)
									{
										text += ",";
									}
									text += text2;
								}
							}
							try
							{
								currentDataView.Sort = text;
							}
							catch
							{
							}
						}
						foreach (object obj in arrayList)
						{
							DataColumn dataColumn = (DataColumn)obj;
							ReportFunction.CallIncrementProgressBar(IncrementSubProgressBar);
							table.Columns.Remove(dataColumn);
						}
					}
				}
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00021DA8 File Offset: 0x00020DA8
		public static DataView KeepOnlyRowsInOtherTable(DataView primaryDv, DataView secondDv)
		{
			DataTable table = primaryDv.Table;
			DataTable table2 = secondDv.Table;
			DataTable dataTable = table2.Clone();
			ArrayList arrayList = new ArrayList(table.Columns.Count);
			for (int i = 0; i < table.Columns.Count; i++)
			{
				string text = table.Columns[i].ColumnName.ToLower().Trim();
				for (int j = 0; j < table2.Columns.Count; j++)
				{
					string strB = table2.Columns[j].ColumnName.ToLower().Trim();
					if (text.CompareTo(strB) == 0)
					{
						arrayList.Add(new Point(i, j));
						break;
					}
				}
			}
			DataView result;
			if (arrayList.Count < 1)
			{
				result = null;
			}
			else
			{
				foreach (object obj in table2.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					foreach (object obj2 in table.Rows)
					{
						DataRow dataRow2 = (DataRow)obj2;
						bool flag = true;
						foreach (object obj3 in arrayList)
						{
							Point point = (Point)obj3;
							string text2 = dataRow[point.Y].ToString().Trim().ToLower();
							string strB2 = dataRow2[point.X].ToString().Trim().ToLower();
							if (text2.CompareTo(strB2) != 0)
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							DataRow dataRow3 = ReportFunction.ImportRowCopy(dataTable, dataRow);
						}
					}
				}
				result = new DataView(dataTable);
			}
			return result;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00022068 File Offset: 0x00021068
		public static void MapColumnNamesToSpecificValues(ref Report report, string nameValuePairs, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			if (currentDataView != null && currentDataView.Table.Rows.Count >= 1)
			{
				SetupSubProgressBar(0, currentDataView.Count);
				string[] array = nameValuePairs.Split(new char[]
				{
					'`'
				});
				if (array.Length >= 1)
				{
					int[] array2 = new int[array.Length];
					string[][] array3 = new string[array.Length][];
					int num = 0;
					for (int i = 0; i < array.Length; i++)
					{
						string[] array4 = array[i].Split(new char[]
						{
							','
						});
						bool flag = false;
						if (array4.Length == 2)
						{
							string columnName = array4[0].Trim();
							string text = array4[1].Trim();
							int num2 = currentDataView.Table.Columns.IndexOf(columnName);
							if (num2 >= 0)
							{
								string[] array5 = text.Split(new char[]
								{
									','
								});
								if (array5.Length > 0)
								{
									array2[i] = num2;
									array3[i] = new string[array5.Length];
									for (int j = 0; j < array5.Length; j++)
									{
										string text2 = array5[j];
										array3[i][j] = text2.Trim().ToLower();
									}
									flag = true;
								}
							}
						}
						if (!flag)
						{
							array2[i] = -1;
							num++;
						}
					}
					if (num > 0)
					{
						int num3 = array2.Length - num;
						if (num3 < 1)
						{
							return;
						}
						int[] array6 = new int[num3];
						string[][] array7 = new string[num3][];
						int j = 0;
						for (int i = 0; i < array2.Length; i++)
						{
							if (array2[i] >= 0)
							{
								array6[j++] = array2[i];
								array7[j] = new string[array3[i].Length];
								Array.Copy(array3[i], array7[j], array7[j].Length);
							}
						}
						array2 = array6;
						array3 = array7;
					}
					foreach (object obj in currentDataView)
					{
						DataRowView dataRowView = (DataRowView)obj;
						IncrementSubProgressBar(1);
						DataRow row = dataRowView.Row;
						for (int i = 0; i < array2.Length; i++)
						{
							string strB = row[array2[i]].ToString().Trim().ToLower();
							for (int j = 0; j < array3[i].Length; j++)
							{
								string text3 = array3[i][j];
								if (text3.CompareTo(strB) == 0)
								{
									row[i] = currentDataView.Table.Columns[array2[i]].ColumnName;
									break;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000223C0 File Offset: 0x000213C0
		public static void WriteData_CUSTOM_DATA(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, ref Report report, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			ReportFunction.WriteData(da, tripleDES, "CUSTOM_DATA", ref report, IncrementSubProgressBar, SetupSubProgressBar);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000223D4 File Offset: 0x000213D4
		public static void WriteData_CUSTOM_COURSES(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, ref Report report, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			ReportFunction.WriteData(da, tripleDES, "CUSTOM_COURSES", ref report, IncrementSubProgressBar, SetupSubProgressBar);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000223E8 File Offset: 0x000213E8
		public static void WriteData2(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, DataTable t, string tableNameToWriteTo)
		{
			try
			{
				da.SelectCommand.CommandText = "TRUNCATE TABLE " + tableNameToWriteTo;
				da.Fill(new DataTable());
			}
			catch
			{
			}
			da.SelectCommand.CommandText = "SELECT * FROM " + tableNameToWriteTo;
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			if (dataTable.Rows.Count > 0)
			{
				da.SelectCommand.CommandText = "DELETE FROM " + tableNameToWriteTo;
				da.Fill(new DataTable());
				da.SelectCommand.CommandText = "SELECT * FROM " + tableNameToWriteTo;
				dataTable = new DataTable();
				da.Fill(dataTable);
				if (dataTable.Rows.Count > 0)
				{
					throw new Exception("Can't clear table " + tableNameToWriteTo);
				}
			}
			if (dataTable.Rows.Count < 1)
			{
				try
				{
					StringBuilder stringBuilder = new StringBuilder();
					StringBuilder stringBuilder2 = new StringBuilder();
					for (int i = 0; i < dataTable.Columns.Count; i++)
					{
						if (i >= t.Columns.Count)
						{
							break;
						}
						if (i > 0)
						{
							stringBuilder.Append(",");
							stringBuilder2.Append(",");
						}
						stringBuilder.Append(dataTable.Columns[i].ColumnName);
						stringBuilder2.AppendFormat("@{0}", dataTable.Columns[i].ColumnName);
					}
					string str = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", tableNameToWriteTo, stringBuilder.ToString(), stringBuilder2.ToString());
					da.Connection.Open();
					foreach (object obj in t.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						da.SelectCommand.CommandText = string.Copy(str);
						da.SelectCommand.Parameters.Clear();
						for (int i = 0; i < dataTable.Columns.Count; i++)
						{
							if (i >= t.Columns.Count)
							{
								break;
							}
							string text = dataRow[i].ToString().Trim();
							string text2 = "@" + dataTable.Columns[i].ColumnName;
							if (dataTable.Columns[i].DataType == typeof(byte[]))
							{
								if (text.Length > 0)
								{
									da.SelectCommand.Parameters.Add(text2, tripleDES.Encrypt(text));
								}
								else
								{
									da.SelectCommand.CommandText = da.SelectCommand.CommandText.Replace(text2, "NULL");
								}
							}
							else
							{
								da.SelectCommand.Parameters.Add(text2, text);
							}
						}
						da.SelectCommand.ExecuteNonQuery();
					}
				}
				catch (Exception ex)
				{
					ReportFunction.Log(ex.ToString());
					throw ex;
				}
				finally
				{
					try
					{
						da.Connection.Close();
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000227EC File Offset: 0x000217EC
		private static void WriteData(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, string tableNameToWriteTo, ref Report report, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			TripleDESEncryptionClass tripleDES2 = ReportFunction.CreateTripleDES(da, "", "#<407>#", tripleDES);
			try
			{
				da.SelectCommand.CommandText = "TRUNCATE TABLE " + tableNameToWriteTo;
				da.Fill(new DataTable());
			}
			catch
			{
			}
			da.SelectCommand.CommandText = "SELECT * FROM " + tableNameToWriteTo;
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			if (dataTable.Rows.Count > 0)
			{
				da.SelectCommand.CommandText = "DELETE FROM " + tableNameToWriteTo;
				da.Fill(new DataTable());
				da.SelectCommand.CommandText = "SELECT * FROM " + tableNameToWriteTo;
				dataTable = new DataTable();
				da.Fill(dataTable);
				if (dataTable.Rows.Count > 0)
				{
					throw new Exception("Can't clear table " + tableNameToWriteTo);
				}
			}
			if (dataTable.Rows.Count < 1)
			{
				DataView currentDataView = report.GetCurrentDataView();
				DataTable table = currentDataView.Table;
				DataTable dataTable2 = ReportFunction.EncryptData(table, null, tripleDES2);
				DataView dvToKeep = new DataView(dataTable2);
				report.ReplaceDataView(currentDataView, dvToKeep);
				string[] array = new string[dataTable.Columns.Count];
				for (int i = 0; i < dataTable.Columns.Count; i++)
				{
					array[i] = dataTable.Columns[i].ColumnName;
				}
				int num = 0;
				try
				{
					da.Connection.Open();
					foreach (object obj in dataTable2.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						int num2 = ReportFunction.ShowIncrementAmount(ref num);
						if (num2 > 0 && IncrementSubProgressBar != null)
						{
							IncrementSubProgressBar(num2);
						}
						da.SelectCommand.Parameters.Clear();
						string text = "";
						string text2 = "";
						for (int i = 0; i < dataTable2.Columns.Count; i++)
						{
							string text3 = "@" + array[i];
							if (i > 0)
							{
								text += ",";
								text2 += ",";
							}
							text += array[i];
							text2 += text3;
							da.SelectCommand.Parameters.Add(text3, (dataRow[i] == DBNull.Value) ? new byte[0] : dataRow[i]);
						}
						string commandText = string.Concat(new string[]
						{
							"INSERT INTO ",
							tableNameToWriteTo,
							" (",
							text,
							") VALUES (",
							text2,
							")"
						});
						da.SelectCommand.CommandText = commandText;
						string text4 = UnivOleDbFactory.ToStringParametersExpanded(da.SelectCommand);
						int num3 = da.SelectCommand.ExecuteNonQuery();
					}
				}
				catch (Exception ex)
				{
					ReportFunction.MessageBoxShow(ex.ToString());
					ReportFunction.Log(ex.ToString());
				}
				finally
				{
					try
					{
						da.Connection.Close();
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00022C04 File Offset: 0x00021C04
		public static DataTable EncryptData(DataTable t, string[] colNamesToEncrypt, TripleDESEncryptionClass tripleDES)
		{
			return tripleDES.EncryptOrDecryptNameDataTableBatch(true, t, colNamesToEncrypt);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00022C20 File Offset: 0x00021C20
		public static DataTable DecryptData(DataTable t, string[] colNamesToDecrypt, TripleDESEncryptionClass tripleDES)
		{
			return tripleDES.EncryptOrDecryptNameDataTableBatch(false, t, colNamesToDecrypt);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00022C3C File Offset: 0x00021C3C
		public static void CreateDefaultDataSyncReports(int whoAmIId, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int reportGroupId)
		{
			ReportStep reportStep = new ReportStep(FunctionCode.Import_CSV_File, "d:\\ClockWorkData\\data\\data.csv");
			ReportStep reportStep2 = new ReportStep(FunctionCode.Write_Data_CUSTOM_DATA, "");
			ReportStep reportStep3 = new ReportStep(FunctionCode.Import_CSV_File, "d:\\ClockWorkData\\data\\courses.csv");
			ReportStep reportStep4 = new ReportStep(FunctionCode.Rename_Columns, "");
			ReportStep reportStep5 = new ReportStep(FunctionCode.Write_Data_CUSTOM_COURSES, "");
			int num = 1;
			int num2 = ReportFunction.CreateCustomReport(whoAmIId, da, tripleDES, reportGroupId, num++, "Move data into ClockWork", "Copies the lookup data contained in the courses.csv and data.csv files into the ClockWork database for future lookup, encrypting along the way.  This step is optional (it is possible to directly query the data source when performing an import or sync), but this method ensures that staff running the client will have access over the network to the lookup data in a secure manner.", new ReportStep[]
			{
				reportStep,
				reportStep2,
				reportStep3,
				reportStep4,
				reportStep5
			});
			reportStep = new ReportStep(FunctionCode.Sql_Query, "SELECT * FROM custom_data WHERE c1=@studentnumberencryptdatasync");
			reportStep2 = new ReportStep(FunctionCode.Decrypt_Data, "`#<407>#`");
			int num3 = ReportFunction.CreateCustomReport(whoAmIId, da, tripleDES, reportGroupId, num++, "Preview student data", "Previews incoming data for a student - no writes are performed to the ClockWork database.", 16, new ReportStep[]
			{
				reportStep,
				reportStep2
			});
			reportStep3 = new ReportStep(FunctionCode.Import_User_Data, "");
			int num4 = ReportFunction.CreateCustomReport(whoAmIId, da, tripleDES, reportGroupId, num++, "Import student data", "Actually imports/updates an existing student's data in ClockWork using the incoming data.", 16, new ReportStep[]
			{
				reportStep,
				reportStep2,
				reportStep3
			});
			reportStep = new ReportStep(FunctionCode.Sql_Query, "SELECT * FROM custom_courses WHERE c1=@studentnumberencryptdatasync ORDER BY student_no,subject,course,section");
			reportStep2 = new ReportStep(FunctionCode.Decrypt_Data, "`#<407>#`");
			reportStep3 = new ReportStep(FunctionCode.Import_Students_Courses, "");
			int num5 = ReportFunction.CreateCustomReport(whoAmIId, da, tripleDES, reportGroupId, num++, "Import student courses", "Actually imports/updates an existing student's courses in ClockWork using the incoming data.", 16, new ReportStep[]
			{
				reportStep,
				reportStep2,
				reportStep3
			});
			reportStep = new ReportStep(FunctionCode.Sql_Query, "\tDECLARE @sdate datetime\r\n\tDECLARE @edate datetime\r\n\tDECLARE @now datetime\r\n\t\r\n\tSET @now = getdate()\r\n\t\r\n\tif month(@now)>=9 \r\n\tbegin\r\n\t   SET @sdate = cast(year(@now) AS char(4)) + '-09-01'\r\n\t   SET @edate = cast((year(@now)+1) AS char(4)) + '-04-30'\r\n\tend\r\n\telse if month(@now)>=5\r\n\tbegin\r\n\t   SET @sdate = cast((year(@now)) AS char(4)) + '-05-01'\r\n\t   SET @edate = cast((year(@now)) AS char(4)) + '-08-30'\r\n\tend\r\n\telse \r\n\tbegin\r\n\t   SET @sdate = cast((year(@now)-1) AS char(4)) + '-09-01'\r\n\t   SET @edate = cast((year(@now)) AS char(4)) + '-04-30'\r\n\tend\r\n\t\r\n\r\nSELECT p.personid,p.firstname,p.middlename,p.lastname,p.student_no \r\nFROM apps LEFT JOIN people p ON p.personid=apps.personid \r\nWHERE apps.startdate>=@sdate AND apps.enddate<=@edate AND apps.personid IN (SELECT personid FROM peoplegroups WHERE groupid=1) AND p.isactive=1");
			reportStep2 = new ReportStep(FunctionCode.Decrypt_Data, "firstname,middlename,lastname,student_no");
			reportStep3 = new ReportStep(FunctionCode.Data_Sync_Update_All_Students, "");
			int num6 = ReportFunction.CreateCustomReport(whoAmIId, da, tripleDES, reportGroupId, num++, "Batch Data Sync", "Updates all existing active students in ClockWork for the current school year (using the incoming data).", new ReportStep[]
			{
				reportStep,
				reportStep2,
				reportStep3
			});
			da.SelectCommand.CommandText = "DELETE FROM settingsgroups WHERE groupid=-1 AND (settingcode=406 OR settingcode=405 OR settingcode=407 OR settingcode=441 OR settingcode=408 OR settingcode=99594 OR settingcode=99595";
			da.SelectCommand.Parameters.Clear();
			da.Fill(new DataTable());
			da.SelectCommand.CommandText = "INSERT INTO settingsgroups (groupid,settingcode,settingvalue,settingstringvalue) VALUES (-1,99595," + num6.ToString() + ",'')";
			da.Fill(new DataTable());
			da.SelectCommand.CommandText = "INSERT INTO settingsgroups (groupid,settingcode,settingvalue,settingstringvalue) VALUES (-1,99594," + num2.ToString() + ",'')";
			da.Fill(new DataTable());
			da.SelectCommand.CommandText = "INSERT INTO settingsgroups (groupid,settingcode,settingvalue,settingstringvalue) VALUES (-1,405," + num4.ToString() + ",'')";
			da.Fill(new DataTable());
			da.SelectCommand.CommandText = "INSERT INTO settingsgroups (groupid,settingcode,settingvalue,settingstringvalue) VALUES (-1,406," + num3.ToString() + ",'')";
			da.Fill(new DataTable());
			da.SelectCommand.CommandText = "INSERT INTO settingsgroups (groupid,settingcode,settingvalue,settingstringvalue) VALUES (-1,441," + num5.ToString() + ",'')";
			da.Fill(new DataTable());
			da.SelectCommand.CommandText = "INSERT INTO settingsgroups (groupid,settingcode,settingvalue,settingstringvalue) VALUES (-1,408,0,'')";
			da.Fill(new DataTable());
			string text = Guid.NewGuid().ToString();
			text = text.Replace("-", "");
			string plainText = text.Substring(0, 12);
			da.SelectCommand.CommandText = "INSERT INTO settingsgroups (groupid,settingcode,settingvalue,settingstringvalue) VALUES (-1,407,@pwd,'')";
			da.SelectCommand.Parameters.Add("@pwd", ClockWorkCore.base64Encode(tripleDES.Encrypt(plainText)));
			da.Fill(new DataTable());
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00022FBC File Offset: 0x00021FBC
		public static int CreateCustomReport(int whoAmIId, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int reportGroupId, int orderNum, string title, string description, params ReportStep[] steps)
		{
			return ReportFunction.CreateCustomReport(whoAmIId, da, tripleDES, reportGroupId, orderNum, title, description, -1, steps);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00022FE0 File Offset: 0x00021FE0
		public static int CreateCustomReport(int whoAmIId, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int reportGroupId, int orderNum, string title, string description, int overrideDynamicControlsScreenNum, params ReportStep[] steps)
		{
			da.SelectCommand.CommandText = "INSERT INTO searchinfo (title,description,searchgroupid,datecreated,datelastmodified,whocreated,wholastmodified,ordernum,searchchartinfoid,overridedynamiccontrolsscreennum) VALUES (@title,@description,@searchgroupid,getdate(),getdate(),@whoami,@whoami,@ordernum,-1,@overrideDynamicControlsScreenNum)";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@title", title);
			da.SelectCommand.Parameters.Add("@description", description);
			da.SelectCommand.Parameters.Add("@searchgroupid", reportGroupId);
			da.SelectCommand.Parameters.Add("@whoami", whoAmIId);
			da.SelectCommand.Parameters.Add("@ordernum", orderNum);
			da.SelectCommand.Parameters.Add("@overrideDynamicControlsScreenNum", overrideDynamicControlsScreenNum);
			DataTable dataTable = new DataTable();
			int num = da.FillReturnIdentity(dataTable, "searchinfoid", "searchinfo");
			if (num > 0)
			{
				int num2 = 1;
				foreach (ReportStep reportStep in steps)
				{
					da.SelectCommand.CommandText = "INSERT INTO searchfunctions (searchinfoid,functioncode,functionparameters,ordernum,custom,customsqlinjection,customsqlinjectionoperator) VALUES (@searchinfoid,@functioncode,@functionparameters,@ordernum,'','','')";
					da.SelectCommand.Parameters.Clear();
					da.SelectCommand.Parameters.Add("@searchinfoid", num);
					da.SelectCommand.Parameters.Add("@functioncode", reportStep.FunctionCode);
					da.SelectCommand.Parameters.Add("@functionparameters", reportStep.Parameters);
					da.SelectCommand.Parameters.Add("@ordernum", num2++);
					da.Fill(new DataTable());
				}
			}
			return num;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000231B8 File Offset: 0x000221B8
		public static void PullInDataPersonIdOnly(UnivDataAdapter da_willBeCopied, TripleDESEncryptionClass tripleDES, ref Report report, string sql, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			if (SetupSubProgressBar != null)
			{
				SetupSubProgressBar(0, currentDataView.Count);
			}
			UnivDataAdapter univDataAdapter = da_willBeCopied.Clone();
			DataView dataView = ReportFunction.CopyDataView(currentDataView);
			DataTable dataTable = currentDataView.Table.Clone();
			int count = dataTable.Columns.Count;
			univDataAdapter.SelectCommand.CommandText = sql;
			univDataAdapter.SelectCommand.Parameters.Clear();
			univDataAdapter.SelectCommand.Parameters.Add("@personid", -1);
			DataTable dataTable2 = new DataTable();
			univDataAdapter.Fill(dataTable2);
			foreach (object obj in dataTable2.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				string columnName = dataColumn.ColumnName;
				if (!dataTable.Columns.Contains(columnName))
				{
					dataTable.Columns.Add(columnName, dataColumn.DataType);
				}
			}
			int i = 0;
			int num = 0;
			SqlConnection sqlConnection = (SqlConnection)univDataAdapter.Connection.GetConnection();
			SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
			sqlCommand.Parameters.AddWithValue("@personid", -1);
			try
			{
				sqlConnection.Open();
				while (i < currentDataView.Count)
				{
					if (IncrementSubProgressBar != null && num % 50 == 0)
					{
						IncrementSubProgressBar(1);
					}
					DataRow row = currentDataView[i].Row;
					int num2 = (int)row["personid"];
					sqlCommand.Parameters.Clear();
					sqlCommand.Parameters.AddWithValue("personid", num2);
					bool flag = false;
					using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
					{
						while (sqlDataReader.Read())
						{
							DataRow dataRow = dataTable.NewRow();
							for (int j = 0; j < count; j++)
							{
								dataRow[j] = row[j];
							}
							for (int j = count; j < dataTable.Columns.Count; j++)
							{
								string columnName = dataTable.Columns[j].ColumnName;
								try
								{
									dataRow[columnName] = sqlDataReader[columnName];
								}
								catch
								{
								}
							}
							dataTable.Rows.Add(dataRow);
							flag = true;
						}
					}
					if (!flag)
					{
						DataRow dataRow = dataTable.NewRow();
						for (int j = 0; j < count; j++)
						{
							dataRow[j] = row[j];
						}
						dataTable.Rows.Add(dataRow);
					}
					i++;
				}
			}
			catch (Exception ex)
			{
				ReportFunction.MessageBoxShow(ex.ToString());
			}
			finally
			{
				sqlConnection.Close();
			}
			report.ReplaceDataView(currentDataView, dataTable.DefaultView);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00023578 File Offset: 0x00022578
		public static void PullInData(UnivDataAdapter da_willBeCopied, TripleDESEncryptionClass tripleDES, ref Report report, string sql, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			if (currentDataView != null && currentDataView.Table.Rows.Count >= 1)
			{
				if (sql.Length >= 1)
				{
					Regex regex = new Regex("@\\b\\w+");
					if (SetupSubProgressBar != null)
					{
						SetupSubProgressBar(0, currentDataView.Count);
					}
					UnivDataAdapter univDataAdapter = da_willBeCopied.Clone();
					DataView dataView = ReportFunction.CopyDataView(currentDataView);
					MatchCollection matchCollection = regex.Matches(sql);
					ColumnIndexCollection columnIndexCollection = new ColumnIndexCollection();
					foreach (object obj in matchCollection)
					{
						Match match = (Match)obj;
						bool encrypted = false;
						string text = match.Value.Trim().ToLower();
						string text2 = text.Substring(1);
						if (!columnIndexCollection.Contains(text2))
						{
							int num = dataView.Table.Columns.IndexOf(text.Substring(1));
							if (num < 0 && text2.CompareTo("personid") == 0)
							{
								num = dataView.Table.Columns.IndexOf("student_no");
								if (num >= 0)
								{
									encrypted = true;
									text2 = "student_no";
								}
							}
							else if (text2.CompareTo("firstname") == 0)
							{
								encrypted = true;
							}
							else if (text2.CompareTo("lastname") == 0)
							{
								encrypted = true;
							}
							else if (text2.CompareTo("student_no") == 0)
							{
								encrypted = true;
							}
							else if (text2.CompareTo("middlename") == 0)
							{
								encrypted = true;
							}
							if (num >= 0)
							{
								if (text2.Length > 1 && text2[0] == '*')
								{
									encrypted = true;
									text2 = text2.Substring(1);
								}
								ColumnIndexClass columnIndexClass = new ColumnIndexClass(num, text2, text, encrypted);
								columnIndexCollection.Add(columnIndexClass);
							}
						}
					}
					byte[] array = new byte[1];
					Type type = array.GetType();
					univDataAdapter.SelectCommand.CommandText = sql;
					try
					{
						int i = 0;
						string text3 = null;
						while (i < dataView.Table.Rows.Count)
						{
							univDataAdapter.SelectCommand.Parameters.Clear();
							foreach (object obj2 in columnIndexCollection)
							{
								ColumnIndexClass columnIndexClass = (ColumnIndexClass)obj2;
								string text = columnIndexClass.ParamName;
								string text2 = columnIndexClass.ColName;
								int index = columnIndexClass.Index;
								if (index >= 0)
								{
									DataRow dataRow = dataView.Table.Rows[i];
									object obj3 = dataRow[index];
									if (columnIndexClass.Encrypted)
									{
										string plainText = obj3.ToString();
										obj3 = tripleDES.Encrypt(plainText);
									}
									if (text.CompareTo("@" + text2) != 0)
									{
										string text4 = "@___x";
										univDataAdapter.SelectCommand.CommandText = "SELECT personid FROM people WHERE " + text2 + "=" + text4;
										if (univDataAdapter.SelectCommand.Parameters.Contains(text4))
										{
											univDataAdapter.SelectCommand.Parameters.SetValue(text4, obj3);
										}
										else
										{
											univDataAdapter.SelectCommand.Parameters.Add(text4, obj3);
										}
										DataTable dataTable = new DataTable();
										univDataAdapter.Fill(dataTable);
										if (dataTable.Rows.Count > 0)
										{
											obj3 = (int)dataTable.Rows[0][0];
										}
										else
										{
											obj3 = -1;
										}
										text = "@personid";
										univDataAdapter.SelectCommand.CommandText = sql;
									}
									if (univDataAdapter.SelectCommand.Parameters.Contains(text))
									{
										univDataAdapter.SelectCommand.Parameters.SetValue(text, obj3);
									}
									else
									{
										univDataAdapter.SelectCommand.Parameters.Add(text, obj3);
									}
								}
							}
							DataTable dataTable2 = new DataTable();
							univDataAdapter.Fill(dataTable2, out text3);
							if (text3 != null && text3.Length > 0)
							{
								IL_691:
								if (text3 != null && text3.Length > 0)
								{
									ReportFunction.MessageBoxShow(text3);
								}
								goto IL_6C9;
							}
							foreach (object obj4 in dataTable2.Rows)
							{
								DataRow dataRow2 = (DataRow)obj4;
								for (int j = 0; j < dataTable2.Columns.Count; j++)
								{
									string columnName = "_" + dataTable2.Columns[j].ColumnName;
									int num2 = dataView.Table.Columns.IndexOf(columnName);
									if (num2 < 0)
									{
										Type type2 = dataTable2.Columns[j].DataType;
										if (type2 == type)
										{
											type2 = Type.GetType("System.String");
										}
										dataView.Table.Columns.Add(columnName, type2);
										num2 = dataView.Table.Columns.IndexOf(columnName);
									}
									if (num2 >= 0)
									{
										object obj3;
										if (dataTable2.Columns[j].DataType == type)
										{
											if (dataRow2[j] == DBNull.Value)
											{
												obj3 = null;
											}
											else
											{
												obj3 = tripleDES.Decrypt((byte[])dataRow2[j]);
											}
										}
										else
										{
											obj3 = dataRow2[j];
										}
										dataView.Table.Rows[i][num2] = obj3;
									}
								}
							}
							if (IncrementSubProgressBar != null)
							{
								IncrementSubProgressBar(1);
							}
							i++;
						}
						goto IL_691;
					}
					catch (Exception ex)
					{
						ReportFunction.MessageBoxShow(ex.ToString());
					}
					IL_6C9:
					report.ReplaceDataView(currentDataView, dataView);
				}
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00023CC0 File Offset: 0x00022CC0
		public static void MoveDataToOtherColumnsForSpecificRows(ref Report report, string nameValuePairs, string colNamesToMoveToOtherColumn, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			if (currentDataView != null && currentDataView.Table.Rows.Count >= 1)
			{
				if (colNamesToMoveToOtherColumn.Length >= 1 && nameValuePairs.Length >= 1)
				{
					SetupSubProgressBar(0, currentDataView.Count);
					DataTable table = currentDataView.Table;
					string[] array = nameValuePairs.Split(new char[]
					{
						','
					});
					if (array.Length >= 1)
					{
						int[] array2 = new int[array.Length];
						string[] array3 = new string[array.Length];
						for (int i = 0; i < array.Length; i++)
						{
							string[] array4 = array[i].Split(new char[]
							{
								'='
							});
							if (array4.Length != 2)
							{
								return;
							}
							array2[i] = table.Columns.IndexOf(array4[0].Trim());
							array3[i] = array4[1].ToLower().Trim();
						}
						string[] array5 = colNamesToMoveToOtherColumn.Split(new char[]
						{
							','
						});
						if (array5.Length >= 1)
						{
							int[] array6 = new int[array5.Length];
							int[] array7 = new int[array5.Length];
							for (int i = 0; i < array5.Length; i++)
							{
								string text = array5[i].Trim();
								array6[i] = table.Columns.IndexOf(text);
								int num = 2;
								string columnName = text + num.ToString();
								while (table.Columns.IndexOf(columnName) >= 0)
								{
									num++;
									columnName = text + num.ToString();
								}
								table.Columns.Add(columnName, table.Columns[text].DataType);
								array7[i] = table.Columns.Count - 1;
							}
							int num2 = 0;
							foreach (object obj in currentDataView)
							{
								DataRowView dataRowView = (DataRowView)obj;
								if (num2++ % 100 == 0)
								{
									IncrementSubProgressBar(100);
								}
								DataRow row = dataRowView.Row;
								for (int i = 0; i < array2.Length; i++)
								{
									string strB = row[array2[i]].ToString().Trim().ToLower();
									string text2 = array3[i];
									if (text2.CompareTo(strB) == 0)
									{
										for (int j = 0; j < array6.Length; j++)
										{
											int num3 = array6[j];
											int num4 = array7[j];
											if (num3 >= 0 && num4 >= 0)
											{
												row[num4] = row[num3];
												row[num3] = DBNull.Value;
											}
										}
										break;
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00024040 File Offset: 0x00023040
		public static void ExtractAndReturnRowsWithTemporaryStudentNumbers(ref Report report, string studentNumColName, int exactNumCharactersInValidStudentNum, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			ReportFunction.IsolateTemporaryStudentNumbers(ref report, studentNumColName, exactNumCharactersInValidStudentNum, true, IncrementSubProgressBar, SetupSubProgressBar);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00024050 File Offset: 0x00023050
		public static void RemoveRowsWithTemporaryStudentNumbers(ref Report report, string studentNumColName, int exactNumCharactersInValidStudentNum, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			ReportFunction.IsolateTemporaryStudentNumbers(ref report, studentNumColName, exactNumCharactersInValidStudentNum, false, IncrementSubProgressBar, SetupSubProgressBar);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00024060 File Offset: 0x00023060
		public static void RemoveRowsWithTemporaryStudentNumbers(ref Report report, string studentNumColName, int minNumCharsInValidStudentNum, int maxNumCharsInValidStudentNum, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			ReportFunction.IsolateTemporaryStudentNumbers(ref report, studentNumColName, minNumCharsInValidStudentNum, maxNumCharsInValidStudentNum, false, IncrementSubProgressBar, SetupSubProgressBar);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00024072 File Offset: 0x00023072
		private static void IsolateTemporaryStudentNumbers(ref Report report, string studentNumColName, int exactNumCharactersInValidStudentNum, bool extractAndReturnRowsWithTemporaryStudentNumbers, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			ReportFunction.IsolateTemporaryStudentNumbers(ref report, studentNumColName, exactNumCharactersInValidStudentNum, exactNumCharactersInValidStudentNum, extractAndReturnRowsWithTemporaryStudentNumbers, IncrementSubProgressBar, SetupSubProgressBar);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00024084 File Offset: 0x00023084
		private static void IsolateTemporaryStudentNumbers(ref Report report, string studentNumColName, int minNumCharsInValidStudentNum, int maxNumCharsInValidStudentNum, bool extractAndReturnRowsWithTemporaryStudentNumbers, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView dataView = report.GetCurrentDataView();
			if (dataView != null && dataView.Table.Rows.Count >= 1)
			{
				if (studentNumColName.Length >= 1)
				{
					if (SetupSubProgressBar != null)
					{
						SetupSubProgressBar(0, dataView.Count);
					}
					int num = dataView.Table.Columns.IndexOf(studentNumColName);
					if (num >= 0)
					{
						DataTable dataTable = dataView.Table.Clone();
						ArrayList arrayList = new ArrayList();
						foreach (object obj in dataView)
						{
							DataRowView dataRowView = (DataRowView)obj;
							if (IncrementSubProgressBar != null)
							{
								IncrementSubProgressBar(1);
							}
							DataRow dataRow = dataRowView.Row;
							string text = dataRow[num].ToString().Trim().ToLower();
							int length = text.Length;
							bool flag = minNumCharsInValidStudentNum < 0 || length >= minNumCharsInValidStudentNum;
							flag = (flag && (maxNumCharsInValidStudentNum < 0 || length <= maxNumCharsInValidStudentNum));
							bool flag2 = flag;
							if (flag2)
							{
								foreach (char c in text)
								{
									if (char.IsLetter(c))
									{
										flag2 = false;
										break;
									}
								}
							}
							if (!flag2)
							{
								if (extractAndReturnRowsWithTemporaryStudentNumbers)
								{
									dataTable.LoadDataRow(dataRow.ItemArray, true);
								}
								else
								{
									arrayList.Add(dataRow);
								}
							}
						}
						DataTable table = dataView.Table;
						if (extractAndReturnRowsWithTemporaryStudentNumbers)
						{
							string sort = dataView.Sort;
							dataView = null;
							dataView = new DataView(dataTable);
							dataView.Sort = sort;
						}
						else
						{
							foreach (object obj2 in arrayList)
							{
								DataRow dataRow = (DataRow)obj2;
								table.Rows.Remove(dataRow);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00024320 File Offset: 0x00023320
		public static bool IsValidStudentNumber(string snum, string acceptableChars, int minStudentNumLen, int maxStudentNumLen)
		{
			string text = snum.Trim();
			foreach (char value in text)
			{
				if (acceptableChars.IndexOf(value) < 0)
				{
					return false;
				}
			}
			return (minStudentNumLen <= 0 || text.Length >= minStudentNumLen) && (maxStudentNumLen <= 0 || text.Length >= maxStudentNumLen);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000243B8 File Offset: 0x000233B8
		public static void ConcatenateColumnCellDataText(ref Report report, string stringConcatenations, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			if (currentDataView != null && currentDataView.Table.Rows.Count >= 1)
			{
				if (SetupSubProgressBar != null)
				{
					SetupSubProgressBar(0, currentDataView.Count);
				}
				DataTable table = currentDataView.Table;
				string[] array = stringConcatenations.Split(new char[]
				{
					'`'
				});
				if (array.Length >= 1)
				{
					string[][] array2 = new string[array.Length][];
					int[] array3 = new int[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						string[] array4 = array[i].Split(new char[]
						{
							'='
						});
						if (array4.Length != 2)
						{
							return;
						}
						string columnName = array4[0].Trim();
						int num = table.Columns.IndexOf(columnName);
						if (num < 0)
						{
							table.Columns.Add(columnName);
							num = table.Columns.IndexOf(columnName);
						}
						array3[i] = num;
						string[] array5 = array4[1].Split(new char[]
						{
							','
						});
						if (array5.Length <= 0)
						{
							return;
						}
						array2[i] = new string[array5.Length];
						Array.Copy(array5, array2[i], array5.Length);
						for (int j = 0; j < array2[i].Length; j++)
						{
							if (array2[i][j].IndexOf("<comma>") >= 0)
							{
								array2[i][j] = array2[i][j].Replace("<comma>", ", ");
							}
							else if (array2[i][j].IndexOf("<newline>") >= 0)
							{
								array2[i][j] = array2[i][j].Replace("<newline>", System.Environment.NewLine);
							}
						}
					}
					int num2 = 0;
					foreach (object obj in currentDataView)
					{
						DataRowView dataRowView = (DataRowView)obj;
						if (IncrementSubProgressBar != null && num2++ % 100 == 0)
						{
							IncrementSubProgressBar(100);
						}
						DataRow row = dataRowView.Row;
						for (int i = 0; i < array3.Length; i++)
						{
							int num = array3[i];
							string[] array6 = array2[i];
							string text = "";
							bool flag = true;
							foreach (string text2 in array6)
							{
								if (text2.Length > 0)
								{
									if (text2[0] == '[' && text2[text2.Length - 1] == ']')
									{
										int num3 = table.Columns.IndexOf(text2.Substring(1, text2.Length - 2));
										string text3;
										if (num3 >= 0)
										{
											text3 = row[num3].ToString().Trim();
										}
										else
										{
											text3 = "";
										}
										if (text3.Length > 0)
										{
											flag = false;
											text += text3;
										}
									}
									else
									{
										text += text2;
									}
								}
							}
							if (flag)
							{
								text = "";
							}
							row[num] = text;
						}
					}
				}
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000247B8 File Offset: 0x000237B8
		public static void SearchAndReplaceCaseSensitive(ref Report report, string colName, string searchString, string replaceString, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			if (currentDataView != null && currentDataView.Table.Rows.Count >= 1)
			{
				SetupSubProgressBar(0, currentDataView.Count);
				DataTable table = currentDataView.Table;
				if (colName.Length >= 1)
				{
					int num = table.Columns.IndexOf(colName);
					if (num >= 0)
					{
						int num2 = 0;
						foreach (object obj in currentDataView)
						{
							DataRowView dataRowView = (DataRowView)obj;
							num2++;
							if (num2 % 20 == 0)
							{
								IncrementSubProgressBar(1);
							}
							DataRow row = dataRowView.Row;
							string text = row[num].ToString();
							if (searchString.Length < 1)
							{
								if (text.Trim().Length < 1)
								{
									row[num] = replaceString;
								}
							}
							else if (text.IndexOf(searchString) >= 0)
							{
								row[num] = text.Replace(searchString, replaceString);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00024934 File Offset: 0x00023934
		public static void GeneralizeDateToMonth(ref Report report, string[] colNames, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			if (currentDataView != null && currentDataView.Table.Rows.Count >= 1)
			{
				SetupSubProgressBar(0, currentDataView.Count);
				DataTable table = currentDataView.Table;
				if (colNames != null && colNames.Length >= 1)
				{
					ArrayList arrayList = new ArrayList();
					foreach (string text in colNames)
					{
						int num = table.Columns.IndexOf(text);
						if (num >= 0)
						{
							string text2 = text + "_month";
							ReportFunction.AddNewColumns(ref currentDataView, text2);
							int num2 = table.Columns.IndexOf(text2);
							if (num2 >= 0)
							{
								arrayList.Add(new Point(num, num2));
							}
						}
					}
					if (arrayList.Count > 0)
					{
						int num3 = 0;
						foreach (object obj in currentDataView)
						{
							DataRowView dataRowView = (DataRowView)obj;
							num3++;
							if (num3 % 20 == 0)
							{
								IncrementSubProgressBar(1);
							}
							DataRow row = dataRowView.Row;
							foreach (object obj2 in arrayList)
							{
								Point point = (Point)obj2;
								int x = point.X;
								int num2 = point.Y;
								DateTime d = DateTime.MinValue;
								if (row[x] != DBNull.Value)
								{
									if (table.Columns[x].DataType == typeof(DateTime))
									{
										d = (DateTime)row[x];
									}
									else if (row[x].ToString().Trim().Length >= 1)
									{
										string s = row[x].ToString();
										try
										{
											d = DateTime.Parse(s);
										}
										catch
										{
											d = DateTime.MinValue;
										}
									}
								}
								if (d != DateTime.MinValue)
								{
									row[num2] = d.ToString("yyyy_MMMM");
								}
								else
								{
									row[num2] = "";
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00024C58 File Offset: 0x00023C58
		public static void StampTableWithDatabaseName(ref Report report, string dbName, string newColName_leaveBlankFor_Department, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			if (currentDataView != null && currentDataView.Table.Rows.Count >= 1)
			{
				if (SetupSubProgressBar != null)
				{
					SetupSubProgressBar(0, currentDataView.Count);
				}
				DataTable table = currentDataView.Table;
				string newColName;
				if (newColName_leaveBlankFor_Department == null || newColName_leaveBlankFor_Department.Length < 1)
				{
					newColName = "Department";
				}
				else
				{
					newColName = newColName_leaveBlankFor_Department;
				}
				ReportFunction.AddDataColumn(ref table, newColName);
				int columnIndex = table.Columns.Count - 1;
				foreach (object obj in currentDataView)
				{
					DataRowView dataRowView = (DataRowView)obj;
					if (IncrementSubProgressBar != null)
					{
						IncrementSubProgressBar(1);
					}
					DataRow row = dataRowView.Row;
					row[columnIndex] = dbName;
				}
				if (SetupSubProgressBar != null)
				{
					SetupSubProgressBar(0, 10);
				}
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00024D84 File Offset: 0x00023D84
		public static void RemoveExtraSpacesFromCommaSeparatedList(ref Report report, string colNames, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			if (currentDataView != null && currentDataView.Table.Rows.Count >= 1)
			{
				SetupSubProgressBar(0, currentDataView.Count);
				DataTable table = currentDataView.Table;
				if (colNames.Length >= 1)
				{
					string[] array = colNames.Split(new char[]
					{
						','
					});
					if (array.Length >= 1)
					{
						int[] array2 = new int[array.Length];
						for (int i = 0; i < array.Length; i++)
						{
							string columnName = array[i].Trim();
							array2[i] = table.Columns.IndexOf(columnName);
							if (array2[i] < 0)
							{
								return;
							}
						}
						foreach (object obj in currentDataView)
						{
							DataRowView dataRowView = (DataRowView)obj;
							IncrementSubProgressBar(1);
							DataRow row = dataRowView.Row;
							for (int j = 0; j < array2.Length; j++)
							{
								string text = row[array2[j]].ToString().Trim();
								string[] array3 = text.Split(new char[]
								{
									','
								});
								string text2 = "";
								for (int k = 0; k < array3.Length; k++)
								{
									string text3 = array3[k].Trim();
									if (text3.Length > 0)
									{
										if (text2.Length > 0)
										{
											text2 += ", ";
										}
										text2 += text3;
									}
								}
								row[array2[j]] = text2;
							}
						}
					}
				}
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00024FB8 File Offset: 0x00023FB8
		public static void MarkRowsAsSpecialThatHaveDiffereningValuesForUniqueRowGroups(ref Report report, string newSpecialColumnName, string uniqueRowColNames, string allShouldBeTheSameColNames, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			if (currentDataView != null && currentDataView.Table.Rows.Count >= 1)
			{
				SetupSubProgressBar(0, currentDataView.Count);
				currentDataView.Sort = uniqueRowColNames;
				DataTable table = currentDataView.Table;
				string[] array = uniqueRowColNames.Split(new char[]
				{
					','
				});
				string[] array2 = allShouldBeTheSameColNames.Split(new char[]
				{
					','
				});
				if (array.Length >= 1 && array2.Length >= 1)
				{
					int[] array3 = new int[array.Length];
					int[] array4 = new int[array2.Length];
					for (int i = 0; i < array.Length; i++)
					{
						int num = table.Columns.IndexOf(array[i].Trim());
						if (num < 0)
						{
							return;
						}
						array3[i] = num;
					}
					for (int i = 0; i < array2.Length; i++)
					{
						int num = table.Columns.IndexOf(array2[i].Trim());
						if (num < 0)
						{
							return;
						}
						array4[i] = num;
					}
					table.Columns.Add(newSpecialColumnName, Type.GetType("System.Boolean"));
					int columnIndex = table.Columns.Count - 1;
					int j = 0;
					while (j < currentDataView.Count)
					{
						int num2;
						ArrayList equivalentRows_ListIsSortedByUniqueColNames = ReportFunction.GetEquivalentRows_ListIsSortedByUniqueColNames(currentDataView, j, array3, out num2);
						equivalentRows_ListIsSortedByUniqueColNames.Add(currentDataView[j].Row);
						bool flag = true;
						for (int k = 1; k < equivalentRows_ListIsSortedByUniqueColNames.Count; k++)
						{
							DataRow dataRow = (DataRow)equivalentRows_ListIsSortedByUniqueColNames[k];
							DataRow dataRow2 = (DataRow)equivalentRows_ListIsSortedByUniqueColNames[k - 1];
							for (int l = 0; l < array4.Length; l++)
							{
								string text = dataRow[array4[l]].ToString().Trim().ToLower();
								string strB = dataRow2[array4[l]].ToString().Trim().ToLower();
								if (text.CompareTo(strB) != 0)
								{
									flag = false;
									break;
								}
							}
							if (!flag)
							{
								break;
							}
						}
						if (!flag)
						{
							foreach (object obj in equivalentRows_ListIsSortedByUniqueColNames)
							{
								DataRow dataRow3 = (DataRow)obj;
								dataRow3[columnIndex] = true;
							}
						}
						int num3 = num2 - j;
						if (num3 > 0)
						{
							if (IncrementSubProgressBar != null)
							{
								IncrementSubProgressBar(num3);
							}
							j = num2;
						}
						else
						{
							j++;
							IncrementSubProgressBar(1);
						}
					}
				}
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000252E4 File Offset: 0x000242E4
		public static void ForceSpecificColumnsAndOrdering(ref Report report, string colNames, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataView dataView = ReportFunction.ForceSpecificColumnsAndOrdering(currentDataView, colNames, IncrementSubProgressBar, SetupSubProgressBar);
			if (dataView != null)
			{
				report.ReplaceDataView(currentDataView, dataView);
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00025318 File Offset: 0x00024318
		public static DataView ForceSpecificColumnsAndOrdering(DataView dv, string colNames, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView result;
			if (dv == null || dv.Table.Rows.Count < 1 || colNames.Trim().Length < 1)
			{
				result = null;
			}
			else
			{
				if (SetupSubProgressBar != null)
				{
					SetupSubProgressBar(0, dv.Count);
				}
				bool flag = false;
				string text = colNames.Replace(System.Environment.NewLine, "");
				if (colNames.IndexOf("ERRORONMISSINGCOLUMNS`ERRORONMISSINGCOLUMNS,") >= 0)
				{
					text = text.Replace("ERRORONMISSINGCOLUMNS`ERRORONMISSINGCOLUMNS,", "");
					flag = true;
				}
				string[] array = text.Split(new char[]
				{
					','
				});
				DataTable dataTable = new DataTable();
				foreach (string text2 in array)
				{
					string[] array3 = text2.Split(new char[]
					{
						'`'
					});
					if (array3.Length > 0)
					{
						string text3 = array3[0];
						string text4;
						if (array3.Length > 1)
						{
							text4 = array3[1].Trim().ToLower();
						}
						else
						{
							text4 = "string";
						}
						int num = dv.Table.Columns.IndexOf(text3);
						if (num >= 0 || !flag)
						{
							if (text4.CompareTo("int32") == 0)
							{
								dataTable.Columns.Add(text3, Type.GetType("System.Int32"));
							}
							else if (text4.CompareTo("bool") == 0 || text4.CompareTo("boolean") == 0)
							{
								dataTable.Columns.Add(text3, Type.GetType("System.Boolean"));
							}
							else if (text4.CompareTo("datetime") == 0)
							{
								dataTable.Columns.Add(text3, Type.GetType("System.DateTime"));
							}
							else
							{
								dataTable.Columns.Add(text3);
							}
						}
						else if (flag)
						{
							Exception ex = new Exception("Mandatory column missing from results set [" + text3 + "]");
							if (SetupSubProgressBar != null)
							{
								SetupSubProgressBar(0, 10);
							}
							throw ex;
						}
					}
				}
				bool flag2 = true;
				for (int j = 0; j < dataTable.Columns.Count; j++)
				{
					if (j >= dv.Table.Columns.Count)
					{
						flag2 = false;
						break;
					}
					if (dataTable.Columns[j].ColumnName.ToLower().Trim().CompareTo(dv.Table.Columns[j].ColumnName.ToLower().Trim()) != 0)
					{
						flag2 = false;
						break;
					}
				}
				if (flag2)
				{
					if (SetupSubProgressBar != null)
					{
						SetupSubProgressBar(0, dv.Count);
					}
					result = null;
				}
				else
				{
					foreach (object obj in dv)
					{
						DataRowView dataRowView = (DataRowView)obj;
						if (IncrementSubProgressBar != null)
						{
							IncrementSubProgressBar(1);
						}
						DataRow row = dataRowView.Row;
						object[] array4 = new object[dataTable.Columns.Count];
						for (int j = 0; j < dv.Table.Columns.Count; j++)
						{
							int num = dataTable.Columns.IndexOf(dv.Table.Columns[j].ColumnName);
							if (num >= 0)
							{
								array4[num] = row[j];
							}
						}
						dataTable.Rows.Add(array4);
					}
					if (SetupSubProgressBar != null)
					{
						SetupSubProgressBar(0, 10);
					}
					result = dataTable.DefaultView;
				}
			}
			return result;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00025758 File Offset: 0x00024758
		public static DataView DecodeDynamicData(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, DataTable t, params string[] uniqueColNames)
		{
			return Reports.DecodeDynamicData(da, tripleDES, (t == null) ? null : t.DefaultView, uniqueColNames);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0002577E File Offset: 0x0002477E
		public static void BreakdownNumbers(ref Report report, string uniqueColNames, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			ReportFunction.BreakdownNumbers(ref report, null, uniqueColNames, "", IncrementSubProgressBar, SetupSubProgressBar);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00025794 File Offset: 0x00024794
		public static void BreakdownNumbers(ref Report report, UnivDataAdapter da, string uniqueColNames, string enforceRows, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			if (currentDataView != null && currentDataView.Table.Rows.Count >= 1 && uniqueColNames.Trim().Length >= 1)
			{
				SetupSubProgressBar(0, Convert.ToInt32(currentDataView.Count / 100));
				ReportFunction.SetNewSortButKeepOldSortValuesAtEndOfNewSort(ref currentDataView, uniqueColNames);
				DataTable table = currentDataView.Table;
				string[] array = uniqueColNames.Split(new char[]
				{
					','
				});
				if (array.Length >= 1)
				{
					int[] array2 = new int[array.Length];
					DataTable dataTable = new DataTable();
					for (int i = 0; i < array.Length; i++)
					{
						int num = table.Columns.IndexOf(array[i].Trim());
						if (num < 0)
						{
							return;
						}
						array2[i] = num;
						dataTable.Columns.Add(table.Columns[num].ColumnName, table.Columns[num].DataType);
					}
					ReportFunction.AddDataColumn(ref dataTable, "NumRows", typeof(int));
					int columnIndex = dataTable.Columns.Count - 1;
					int num2;
					for (int j = 0; j < currentDataView.Count; j = num2)
					{
						DataRowView dataRowView = currentDataView[j];
						DataRow dataRow = dataRowView.Row;
						ArrayList equivalentRows_ListIsSortedByUniqueColNames = ReportFunction.GetEquivalentRows_ListIsSortedByUniqueColNames(currentDataView, j, array2, out num2);
						int num3 = equivalentRows_ListIsSortedByUniqueColNames.Count + 1;
						DataRow dataRow2 = dataTable.NewRow();
						for (int i = 0; i < array2.Length; i++)
						{
							dataRow2[i] = dataRow[array2[i]];
						}
						dataRow2[columnIndex] = num3;
						dataTable.Rows.Add(dataRow2);
						int num4 = num2 - j;
						if (num4 < 1)
						{
							num4 = 1;
							num2 = j + 1;
						}
						for (int i = 0; i < num4; i++)
						{
							if (j % 100 == 0)
							{
								IncrementSubProgressBar(1);
							}
						}
					}
					ArrayList arrayList = new ArrayList();
					string text = enforceRows;
					int num5 = 0;
					do
					{
						int num6 = text.IndexOf("{");
						if (num6 < 0)
						{
							break;
						}
						int num7 = text.IndexOf("}", num6);
						string text2 = text.Substring(num6, num7 - num6 + 1);
						text = text.Remove(num6, num7 - num6 + 1);
						text2 = text2.Replace(',', '~');
						text2 = text2.Replace('{', '[');
						text2 = text2.Replace('}', ']');
						text = text.Insert(num6, text2);
						num5++;
					}
					while (num5 <= 100000);
					string[] array3 = text.Split(new char[]
					{
						','
					});
					foreach (string text3 in array3)
					{
						if (text3.Trim().Length > 0)
						{
							if (text3[0] == '[')
							{
								string text4 = text3.Substring(1, text3.Length - 2);
								text4 = text4.Replace('~', ',');
								da.SelectCommand.CommandText = "SELECT controlid,controlcaption FROM dynamiccontrols WHERE controlid in (SELECT orderid AS controlid FROM splitorderids(@cids,','))";
								da.SelectCommand.Parameters.Clear();
								da.SelectCommand.Parameters.Add("@cids", text4);
								DataTable dataTable2 = new DataTable();
								da.Fill(dataTable2);
								foreach (object obj in dataTable2.Rows)
								{
									DataRow dataRow3 = (DataRow)obj;
									string text5 = dataRow3["controlcaption"].ToString();
									int num6 = text5.IndexOf("~~");
									if (num6 > 0)
									{
										text5 = text5.Substring(0, num6);
									}
									arrayList.Add(text5);
								}
							}
							else
							{
								arrayList.Add(text3);
							}
						}
					}
					foreach (object obj2 in arrayList)
					{
						string text6 = (string)obj2;
						string strB = text6.Trim().ToLower();
						bool flag = false;
						foreach (object obj3 in dataTable.Rows)
						{
							DataRow dataRow = (DataRow)obj3;
							string text7 = dataRow[0].ToString().Trim().ToLower();
							if (text7.CompareTo(strB) == 0)
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							DataRow dataRow4 = dataTable.NewRow();
							dataRow4[0] = text6;
							for (int i = 1; i < dataTable.Columns.Count; i++)
							{
								if (dataTable.Columns[i].DataType == typeof(int))
								{
									dataRow4[i] = 0;
								}
							}
							dataTable.Rows.Add(dataRow4);
						}
					}
					DataView dvToKeep = new DataView(dataTable);
					currentDataView.Sort = uniqueColNames;
					report.ReplaceDataView(currentDataView, dvToKeep);
				}
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00025DF4 File Offset: 0x00024DF4
		private static int LookupInstructorByEmail(UnivDataAdapter da, string instructorName, string instructorEmail, string instructorPhone)
		{
			int result;
			if (!string.IsNullOrEmpty(instructorEmail) && instructorEmail.Trim().Length > 0)
			{
				da.SelectCommand.CommandText = "SELECT lucoursedataid FROM lucoursedata WHERE lookuplisttype=1 AND email=@email";
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@email", instructorEmail);
				DataTable dataTable = new DataTable();
				da.Fill(dataTable);
				if (dataTable.Rows.Count > 0)
				{
					result = (int)dataTable.Rows[0][0];
				}
				else
				{
					da.SelectCommand.CommandText = "INSERT INTO lucoursedata (lookuplisttype,lookupstring,altlookupstring,email,phone) VALUES (1,@instructor,@instructor,@email,@phone)";
					da.SelectCommand.Parameters.Clear();
					da.SelectCommand.Parameters.Add("@instructor", instructorName);
					da.SelectCommand.Parameters.Add("@email", instructorEmail);
					da.SelectCommand.Parameters.Add("@phone", instructorPhone);
					int num = da.FillReturnIdentity(new DataTable(), "lucoursedataid", "lucoursedata");
					result = num;
				}
			}
			else
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00025F28 File Offset: 0x00024F28
		private static int LookupInstructorByUsername(UnivDataAdapter da, string instructorName, string instructorEmail, string instructorPhone, string username)
		{
			int result;
			if (!string.IsNullOrEmpty(username) && username.Trim().Length > 0)
			{
				da.SelectCommand.CommandText = "SELECT lucoursedataid FROM lucoursedata WHERE lookuplisttype=1 AND username=@username";
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@username", username);
				DataTable dataTable = new DataTable();
				da.Fill(dataTable);
				if (dataTable.Rows.Count > 0)
				{
					result = (int)dataTable.Rows[0][0];
				}
				else
				{
					da.SelectCommand.CommandText = "INSERT INTO lucoursedata (lookuplisttype,lookupstring,altlookupstring,email,phone,username) VALUES (1,@instructor,@instructor,@email,@phone,@username)";
					da.SelectCommand.Parameters.Clear();
					da.SelectCommand.Parameters.Add("@instructor", instructorName);
					da.SelectCommand.Parameters.Add("@email", instructorEmail);
					da.SelectCommand.Parameters.Add("@phone", instructorPhone);
					da.SelectCommand.Parameters.Add("@username", username);
					int num = da.FillReturnIdentity(new DataTable(), "lucoursedataid", "lucoursedata");
					result = num;
				}
			}
			else
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00026078 File Offset: 0x00025078
		private static int LookupInstructor(UnivDataAdapter da, string instructorName, string instructorEmail, string instructorPhone, string username)
		{
			int result;
			if ((instructorName == null || instructorName.Trim().Length < 1) && (instructorEmail == null || instructorEmail.Trim().Length < 1))
			{
				result = -1;
			}
			else
			{
				da.SelectCommand.CommandText = "SELECT lucoursedataid AS instructorid FROM lucoursedata WHERE lookuplisttype=1";
				da.SelectCommand.Parameters.Clear();
				if (instructorName != null && instructorName.Length > 0)
				{
					UnivCommand selectCommand = da.SelectCommand;
					selectCommand.CommandText += " AND (lookupstring=@instructor OR altlookupstring=@instructor";
					if (instructorEmail != null && instructorEmail.Length > 0)
					{
						UnivCommand selectCommand2 = da.SelectCommand;
						selectCommand2.CommandText += " OR email=@email";
						da.SelectCommand.Parameters.Add("@email", instructorEmail);
					}
					UnivCommand selectCommand3 = da.SelectCommand;
					selectCommand3.CommandText += ")";
					if (da.SelectCommand.Parameters.Contains("@instructor"))
					{
						da.SelectCommand.Parameters.SetValue("@instructor", instructorName);
					}
					else
					{
						da.SelectCommand.Parameters.Add("@instructor", instructorName);
					}
				}
				else if (instructorEmail != null && instructorEmail.Length > 0)
				{
					UnivCommand selectCommand4 = da.SelectCommand;
					selectCommand4.CommandText += " AND email=@email";
					if (da.SelectCommand.Parameters.Contains("@email"))
					{
						da.SelectCommand.Parameters.SetValue("@email", instructorEmail);
					}
					else
					{
						da.SelectCommand.Parameters.Add("@email", instructorEmail);
					}
				}
				DataTable dataTable = new DataTable();
				da.Fill(dataTable);
				if (dataTable.Rows.Count > 0)
				{
					result = (int)dataTable.Rows[dataTable.Rows.Count - 1][0];
				}
				else
				{
					if (instructorName == null || instructorName.Trim().Length < 0)
					{
						instructorName = ReportFunction.InferInstructorNameFromEmail(instructorEmail);
					}
					if (instructorName.Trim().Length > 0)
					{
						if (username.Length > 0)
						{
							da.SelectCommand.CommandText = "INSERT INTO lucoursedata (lookuplisttype,lookupstring,altlookupstring,email,phone,username) VALUES (1,@instructor,@instructor,@email,@phone,@username)";
							if (da.SelectCommand.Parameters.Contains("@username"))
							{
								da.SelectCommand.Parameters.SetValue("@username", username);
							}
							else
							{
								da.SelectCommand.Parameters.Add("@username", username);
							}
						}
						else
						{
							da.SelectCommand.CommandText = "INSERT INTO lucoursedata (lookuplisttype,lookupstring,altlookupstring,email,phone) VALUES (1,@instructor,@instructor,@email,@phone)";
						}
						if (da.SelectCommand.Parameters.Contains("@instructor"))
						{
							da.SelectCommand.Parameters.SetValue("@instructor", instructorName);
						}
						else
						{
							da.SelectCommand.Parameters.Add("@instructor", instructorName);
						}
						if (da.SelectCommand.Parameters.Contains("@email"))
						{
							da.SelectCommand.Parameters.SetValue("@email", instructorEmail);
						}
						else
						{
							da.SelectCommand.Parameters.Add("@email", (instructorEmail == null) ? "" : instructorEmail);
						}
						if (da.SelectCommand.Parameters.Contains("@phone"))
						{
							da.SelectCommand.Parameters.SetValue("@phone", instructorPhone);
						}
						else
						{
							da.SelectCommand.Parameters.Add("@phone", (instructorPhone == null) ? "" : instructorPhone);
						}
						dataTable = new DataTable();
						result = da.FillReturnIdentity(dataTable, "lucoursedataid", "lucoursedata");
					}
					else
					{
						result = -1;
					}
				}
			}
			return result;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0002647C File Offset: 0x0002547C
		public static int GetClockWorkCourseLuCourseId(UnivDataAdapter da, DateTime sdate, DateTime edate, string term, string duration, string subject, string course, string section, string timeOfDay, string instructorName, string instructorEmail, string instructorPhone, string instructorUsername)
		{
			int result;
			if ((term.Trim().Length < 1 && duration.Trim().Length < 1) || subject.Trim().Length < 1 || course.Trim().Length < 1 || section.Trim().Length < 1)
			{
				result = -1;
			}
			else
			{
				da.SelectCommand.CommandText = "SELECT lucoursedataid AS subjectid FROM lucoursedata WHERE lookuplisttype=0 AND (lookupstring=@subject OR altlookupstring=@subject)";
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@subject", subject);
				DataTable dataTable = new DataTable();
				da.Fill(dataTable);
				int num;
				if (dataTable.Rows.Count > 0)
				{
					num = (int)dataTable.Rows[0][0];
				}
				else
				{
					da.SelectCommand.CommandText = "INSERT INTO lucoursedata (lookuplisttype,lookupstring,altlookupstring,email,phone) VALUES (0,@subject,@subject,'','')";
					da.SelectCommand.Parameters.Clear();
					da.SelectCommand.Parameters.Add("@subject", subject);
					dataTable = new DataTable();
					num = da.FillReturnIdentity(dataTable, "lucoursedataid", "lucoursedata");
				}
				if (num < 0)
				{
					result = -1;
				}
				else
				{
					int num2 = ReportFunction.LookupInstructor(da, instructorName, instructorEmail, instructorPhone, instructorUsername);
					DataRow dataRow = null;
					da.SelectCommand.CommandText = "SELECT startdate,enddate,lucourseid,section,timeofday,instructorid,term,duration,course,subjectid,instructorid FROM lucourses WHERE startdate>=@sdate AND startdate<@edate AND term=@term AND duration=@duration AND subjectid=@subjectid AND course=@course ORDER BY timeofday,section";
					da.SelectCommand.Parameters.Clear();
					da.SelectCommand.Parameters.Add("@sdate", sdate);
					da.SelectCommand.Parameters.Add("@edate", edate.AddDays(1.0).AddMinutes(-1.0));
					da.SelectCommand.Parameters.Add("@term", term);
					da.SelectCommand.Parameters.Add("@duration", duration);
					da.SelectCommand.Parameters.Add("@subjectid", num);
					da.SelectCommand.Parameters.Add("@course", course);
					DataTable dataTable2 = new DataTable();
					da.Fill(dataTable2);
					if (dataTable2.Rows.Count > 0)
					{
						string strB = timeOfDay.Trim().ToLower();
						string strB2 = section.Trim().ToLower();
						foreach (object obj in dataTable2.Rows)
						{
							DataRow dataRow2 = (DataRow)obj;
							string text = dataRow2["timeofday"].ToString().Trim().ToLower();
							string text2 = dataRow2["section"].ToString().Trim().ToLower();
							if (text.CompareTo(strB) == 0 && text2.CompareTo(strB2) == 0)
							{
								dataRow = dataRow2;
							}
						}
						if (dataRow == null)
						{
							DataRow dataRow3 = dataTable2.Rows[0];
							int num3 = ReportFunction.AddClockWorkCourse(da, (DateTime)dataRow3["startdate"], (DateTime)dataRow3["enddate"], dataRow3["term"].ToString(), dataRow3["duration"].ToString(), (int)dataRow3["subjectid"], dataRow3["course"].ToString(), timeOfDay, section, num2);
							if (num3 <= 0)
							{
								return -1;
							}
							dataRow = dataTable2.NewRow();
							dataRow["lucourseid"] = num3;
							dataRow["section"] = section;
							dataRow["timeofday"] = timeOfDay;
							dataRow["instructorid"] = num2;
							dataTable2.Rows.Add(dataRow);
						}
					}
					else
					{
						da.SelectCommand.CommandText = "SELECT DISTINCT startdate,enddate FROM lucourses WHERE startdate>=@sdate AND startdate<@edate AND term=@term AND duration=@duration ORDER BY startdate DESC";
						da.SelectCommand.Parameters.Clear();
						da.SelectCommand.Parameters.Add("@sdate", sdate);
						da.SelectCommand.Parameters.Add("@edate", edate);
						da.SelectCommand.Parameters.Add("@term", term);
						da.SelectCommand.Parameters.Add("@duration", duration);
						DataTable dataTable3 = new DataTable();
						da.Fill(dataTable3);
						if (dataTable3.Rows.Count > 0)
						{
							sdate = (DateTime)dataTable3.Rows[0][0];
							edate = (DateTime)dataTable3.Rows[0][1];
						}
						else
						{
							da.SelectCommand.CommandText = "SELECT DISTINCT startdate,enddate FROM lucourses WHERE term=@term AND duration=@duration ORDER BY startdate DESC";
							da.SelectCommand.Parameters.Clear();
							da.SelectCommand.Parameters.Add("@sdate", sdate);
							da.SelectCommand.Parameters.Add("@edate", edate);
							da.SelectCommand.Parameters.Add("@term", term);
							da.SelectCommand.Parameters.Add("@duration", duration);
							dataTable3 = new DataTable();
							da.Fill(dataTable3);
							if (dataTable3.Rows.Count > 0)
							{
								foreach (object obj2 in dataTable3.Rows)
								{
									DataRow dataRow2 = (DataRow)obj2;
									DateTime dateTime = (DateTime)dataRow2[0];
									DateTime dateTime2 = (DateTime)dataRow2[1];
									TimeSpan value = dateTime2 - dateTime;
									dateTime = new DateTime(sdate.Year, dateTime.Month, dateTime.Day);
									if (dateTime < sdate)
									{
										dateTime.AddYears(1);
									}
									dateTime2 = dateTime.Add(value);
									if (dateTime >= sdate && dateTime < edate && dateTime2 >= sdate && dateTime2 < edate && dateTime2 > dateTime)
									{
										sdate = dateTime;
										edate = dateTime2;
										break;
									}
								}
							}
						}
						int num3 = ReportFunction.AddClockWorkCourse(da, sdate, edate, term, duration, num, course, timeOfDay, section, num2);
						if (num3 <= 0)
						{
							return -1;
						}
						dataRow = dataTable2.NewRow();
						dataRow["lucourseid"] = num3;
						dataRow["section"] = section;
						dataRow["timeofday"] = timeOfDay;
						dataRow["instructorid"] = num2;
						dataTable2.Rows.Add(dataRow);
					}
					if (dataRow == null)
					{
						result = -1;
					}
					else
					{
						int num4 = (int)dataRow["lucourseid"];
						if (num4 < 0)
						{
							result = -1;
						}
						else
						{
							int num5 = (int)dataRow["instructorid"];
							if (num2 > -1 && num5 != num2)
							{
								da.SelectCommand.CommandText = "UPDATE lucourses SET instructorid=@iid WHERE lucourseid=@lucourseid";
								da.SelectCommand.Parameters.Clear();
								da.SelectCommand.Parameters.Add("@iid", num2);
								da.SelectCommand.Parameters.Add("@lucourseid", num4);
								da.Fill(new DataTable());
							}
							result = num4;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00026CE4 File Offset: 0x00025CE4
		private static int AddClockWorkCourse(UnivDataAdapter da, DateTime sdate, DateTime edate, string term, string duration, int subjectId, string course, string timeOfDay, string section, int instructorId)
		{
			string commandText = "INSERT INTO lucourses (startdate,enddate,term,duration,subjectid,course,timeofday,section,instructorid,crosslistcode,equivalentcode,whoadded,dateadded) \r\nSELECT @startdate,@enddate,@term,@duration,@subjectid,@course,@timeofday,@section\r\n    ,@instructorid,@crosslistcode,@equivalentcode,@whoadded,@dateadded\r\nWHERE NOT EXISTS(SELECT lucourseid FROM lucourses \r\n    WHERE   startdate=@startdate AND enddate=@enddate\r\n            AND term=@term AND duration=@duration AND subjectid=@subjectid AND course=@course\r\n            AND timeofday=@timeofday AND section=@section AND instructorid=@instructorid)";
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@startdate", sdate);
			da.SelectCommand.Parameters.Add("@enddate", edate);
			da.SelectCommand.Parameters.Add("@term", term);
			da.SelectCommand.Parameters.Add("@duration", duration);
			da.SelectCommand.Parameters.Add("@subjectid", subjectId);
			da.SelectCommand.Parameters.Add("@course", course.Trim());
			da.SelectCommand.Parameters.Add("@timeofday", timeOfDay.Trim());
			da.SelectCommand.Parameters.Add("@section", section.Trim());
			da.SelectCommand.Parameters.Add("@instructorid", instructorId);
			da.SelectCommand.Parameters.Add("@crosslistcode", -1);
			da.SelectCommand.Parameters.Add("@equivalentcode", -1);
			da.SelectCommand.Parameters.Add("@whoadded", -7);
			da.SelectCommand.Parameters.Add("@dateadded", DateTime.Now);
			DataTable dataTable = new DataTable();
			string text;
			int num = da.FillReturnIdentity(dataTable, "lucourseid", "lucourses", out text);
			if (num < 1)
			{
				da.SelectCommand.CommandText = "SELECT lucourseid FROM lucourses \r\n    WHERE   startdate=@startdate AND enddate=@enddate\r\n            AND term=@term AND duration=@duration AND subjectid=@subjectid AND course=@course\r\n            AND timeofday=@timeofday AND section=@section AND instructorid=@instructorid\r\n            }";
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@startdate", sdate);
				da.SelectCommand.Parameters.Add("@enddate", edate);
				da.SelectCommand.Parameters.Add("@term", term);
				da.SelectCommand.Parameters.Add("@duration", duration);
				da.SelectCommand.Parameters.Add("@subjectid", subjectId);
				da.SelectCommand.Parameters.Add("@course", course.Trim());
				da.SelectCommand.Parameters.Add("@timeofday", timeOfDay.Trim());
				da.SelectCommand.Parameters.Add("@section", section.Trim());
				da.SelectCommand.Parameters.Add("@instructorid", instructorId);
				da.SelectCommand.Parameters.Add("@crosslistcode", -1);
				da.SelectCommand.Parameters.Add("@equivalentcode", -1);
				da.SelectCommand.Parameters.Add("@whoadded", -7);
				da.SelectCommand.Parameters.Add("@dateadded", DateTime.Now);
				DataTable dataTable2 = new DataTable();
				da.Fill(dataTable2);
				if (dataTable2.Rows.Count > 0 && dataTable2.Rows[0][0] != DBNull.Value)
				{
					num = (int)dataTable2.Rows[0][0];
					text = null;
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				ReportFunction.MessageBoxShow(text);
			}
			return num;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000270B8 File Offset: 0x000260B8
		private static string InferInstructorNameFromEmail(string email)
		{
			int num = email.IndexOf("@");
			string result;
			if (num > 0)
			{
				string text = email.Substring(0, num);
				if (text.Length > 3)
				{
					result = text.Substring(1) + ", " + text.Substring(0, 1);
				}
				else
				{
					result = text.Trim();
				}
			}
			else
			{
				result = email.Trim();
			}
			return result;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00027128 File Offset: 0x00026128
		private static DataRow LogToDataTable(ref DataTable t, params object[] args)
		{
			DataRow dataRow = t.NewRow();
			for (int i = 0; i < args.Length; i++)
			{
				dataRow[i] = args[i];
			}
			t.Rows.Add(dataRow);
			return dataRow;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00027170 File Offset: 0x00026170
		private static void EnsureColExists(ref DataTable t, string colName)
		{
			if (!t.Columns.Contains(colName))
			{
				t.Columns.Add(colName);
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000271A0 File Offset: 0x000261A0
		private static DataRow[] FindRows(DataTable t, string operatorStringExAND, DataRow drSource, params string[] drSourceColName)
		{
			int i = 0;
			StringBuilder stringBuilder = new StringBuilder();
			string value = " " + operatorStringExAND + " ";
			while (i < drSourceColName.Length)
			{
				string text = drSourceColName[i];
				string value2 = drSource[text].ToString().Replace("'", "''");
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(value);
				}
				stringBuilder.Append(text);
				stringBuilder.Append("='");
				stringBuilder.Append(value2);
				stringBuilder.Append("'");
				i++;
			}
			return t.Select(stringBuilder.ToString());
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00027254 File Offset: 0x00026254
		private static DataRow[] FindRows(DataTable t, string operatorStringExAND, params string[] nameThenValue)
		{
			int i = 0;
			StringBuilder stringBuilder = new StringBuilder();
			string value = " " + operatorStringExAND + " ";
			while (i < nameThenValue.Length)
			{
				string value2 = nameThenValue[i];
				string value3 = nameThenValue[i + 1].Replace("'", "''");
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(value);
				}
				stringBuilder.Append(value2);
				stringBuilder.Append("='");
				stringBuilder.Append(value3);
				stringBuilder.Append("'");
				i += 2;
			}
			return t.Select(stringBuilder.ToString());
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00027300 File Offset: 0x00026300
		public static DataView ImportStudentCourses(DataView dv, string parameters, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar, TripleDESEncryptionClass tripleDES, UnivDataAdapter da, bool writeChangesToClockWorkDatabase)
		{
			return ReportFunction.ImportStudentCourses(dv, parameters, IncrementSubProgressBar, SetupSubProgressBar, tripleDES, da, writeChangesToClockWorkDatabase, null);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00027324 File Offset: 0x00026324
		public static DataView ImportStudentCourses(DataView dv, string parameters, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar, TripleDESEncryptionClass tripleDES, UnivDataAdapter da, bool writeChangesToClockWorkDatabase, string snum)
		{
			string[] daysOfWeek = new string[]
			{
				"sun",
				"mon",
				"tue",
				"wed",
				"thu",
				"fri",
				"sat"
			};
			bool supportsTimetable = da.DoesTableExist("timetable");
			bool supportsInstructorUsername = da.DoesColumnExist("lucoursedata", "username");
			bool flag = DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(da, DatabaseVersionManager.ClockWorkFeature.Is_DontImportCoursesForStudentsWithCid_TurnedOn_NOTE_notfeaturejustsetting);
			int num = 0;
			if (flag)
			{
				da.SelectCommand.CommandText = "SELECT settingvalue FROM settingsgroups WHERE groupid=-1 AND settingcode=479";
				DataTable dataTable = new DataTable();
				da.Fill(dataTable);
				if (dataTable.Rows.Count > 0)
				{
					DataRow dataRow = dataTable.Rows[0];
					num = (int)dataRow[0];
				}
			}
			DataTable dataTable2 = new DataTable();
			if (num > 0)
			{
				da.SelectCommand.CommandText = "SELECT DISTINCT personid FROM maininfops WHERE controlid=" + num.ToString();
				da.Fill(dataTable2);
			}
			else
			{
				dataTable2.Columns.Add("personid", typeof(int));
			}
			bool supportsCourseRegistrationStatus = DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(da, DatabaseVersionManager.ClockWorkFeature.StudentReferrals);
			DateTime now = DateTime.Now;
			DateTime sdate;
			DateTime edate;
			if (now.Month <= 4)
			{
				sdate = new DateTime(now.Year - 1, 9, 1);
				edate = new DateTime(now.Year, 4, 30);
			}
			else if (now.Month < 9)
			{
				sdate = new DateTime(now.Year, 5, 1);
				edate = new DateTime(now.Year, 8, 30);
			}
			else
			{
				sdate = new DateTime(now.Year, 9, 1);
				edate = new DateTime(now.Year + 1, 4, 30);
			}
			if (dv.Count > 0 && dv.Table.Columns.Contains("yearstartdate"))
			{
				sdate = DateTime.Parse(dv.Table.Rows[0]["yearstartdate"].ToString());
				edate = DateTime.Parse(dv.Table.Rows[0]["yearenddate"].ToString());
			}
			DataTable pidTable = null;
			return ReportFunction.ImportStudentCourses(dv, parameters, IncrementSubProgressBar, SetupSubProgressBar, tripleDES, da, writeChangesToClockWorkDatabase, daysOfWeek, supportsTimetable, supportsInstructorUsername, dataTable2, supportsCourseRegistrationStatus, sdate, edate, pidTable, snum);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000275C0 File Offset: 0x000265C0
		public static DataView ImportStudentCourses(DataView dv, string parameters, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar, TripleDESEncryptionClass tripleDES, UnivDataAdapter da, bool writeChangesToClockWorkDatabase, string[] daysOfWeek, bool supportsTimetable, bool supportsInstructorUsername, DataTable noPids, bool supportsCourseRegistrationStatus, DateTime sdate, DateTime edate, DataTable pidTable)
		{
			return ReportFunction.ImportStudentCourses(dv, parameters, IncrementSubProgressBar, SetupSubProgressBar, tripleDES, da, writeChangesToClockWorkDatabase, daysOfWeek, supportsTimetable, supportsInstructorUsername, noPids, supportsCourseRegistrationStatus, sdate, edate, pidTable, null);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000275F4 File Offset: 0x000265F4
		private static DataTable LoadNotetakerCourses(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int spid, DateTime rangeStart, DateTime rangeEnd, int spType)
		{
			string commandText = "SELECT spa.serviceproviderid,spac.lucourseid\r\n        ,luc.startdate,luc.enddate,luc.term,luc.duration,luc.subjectid,lucd.altlookupstring AS subject\r\n        ,luc.courseid,luc.timeofday,luc.section,luc.instructorid\r\n        ,lucd2.altlookupstring AS instructor,lucd2.email AS instructoremail,lucd2.phone AS instructorphone\r\n        ,lucd2.username AS instructorusername\r\nFROM    serviceproviderapplications spa LEFT JOIN serviceproviderapplicationcourses spac ON spac.serviceproviderid=spa.serviceproviderid AND spac.serviceprovidertype=spa.serviceprovidertype\r\n        LEFT JOIN lucourses luc ON luc.lucourseid=spac.lucourseid\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\nWHERE   spa.serviceproviderid=@spid AND spa.serviceprovidertype=@sptype\r\n        --AND (spac.registrationstatus IS NULL OR NOT spac.registrationstatus=2)\r\n        AND NOT ( ( luc.enddate<@sdate ) OR (luc.startdate > @edate ) )\r\n";
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@sdate", rangeStart);
			da.SelectCommand.Parameters.Add("@edate", rangeEnd);
			da.SelectCommand.Parameters.Add("@spid", spid);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			return dataTable;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000276F8 File Offset: 0x000266F8
		public static void DataSyncNotetakerCourses(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int spid, string snum, int sptype, DataTable t, DataTable notetakersCurrentCourses, int startIndex, int endIndex, DateTime rangeStart, DateTime rangeEnd)
		{
			List<DataRow> list;
			if (notetakersCurrentCourses == null)
			{
				DataTable dataTable = ReportFunction.LoadNotetakerCourses(da, tripleDES, spid, rangeStart, rangeEnd, sptype);
				list = new List<DataRow>(dataTable.Rows.Count);
				foreach (object obj in dataTable.Rows)
				{
					DataRow item = (DataRow)obj;
					list.Add(item);
				}
			}
			else
			{
				list = new List<DataRow>(notetakersCurrentCourses.Select(string.Format("student_no='{0}'", snum)));
			}
			List<Course> list2 = list.ConvertAll<Course>((DataRow dr1) => new Course(dr1));
			if (t.Rows.Count > 0)
			{
				List<Course> handledClockworkCourses = new List<Course>();
				List<Course> list3 = new List<Course>();
				List<Course> list4 = new List<Course>();
				foreach (object obj2 in t.Rows)
				{
					DataRow dr = (DataRow)obj2;
					Course course = new Course(dr);
					Course course3 = list2.Find((Course e) => e.Matches(course));
					if (course3 != null)
					{
						handledClockworkCourses.Add(course3);
						if (course3.RegistrationStatus == 2)
						{
							list4.Add(course3);
						}
					}
					else
					{
						list3.Add(course);
					}
				}
				List<Course> list5 = list2.FindAll((Course f) => !handledClockworkCourses.Contains(f));
				foreach (Course course2 in list5)
				{
					da.SelectCommand.CommandText = "UPDATE serviceproviderapplicationcourses SET registrationstatus=2 WHERE lucourseid=@lucid AND serviceprovidertype=@sptype AND serviceproviderapplicationid IN (SELECT serviceproviderapplicationid FROM serviceproviderapplications WHERE serviceproviderid=@spid)";
					da.SelectCommand.Parameters.Clear();
					da.SelectCommand.Parameters.Add("@lucid", course2.LuCourseId);
					da.SelectCommand.Parameters.Add("@spid", spid);
					da.SelectCommand.Parameters.Add("@sptype", sptype);
					string text;
					da.Fill(new DataTable(), out text);
					if (!string.IsNullOrEmpty(text))
					{
						ReportFunction.MessageBoxShow(text);
					}
				}
				foreach (Course course2 in list3)
				{
					int clockWorkCourseLuCourseId = ReportFunction.GetClockWorkCourseLuCourseId(da, course2.StartDate, course2.EndDate, course2.Term, course2.Duration, course2.Subject, course2.CourseCode, course2.Section, course2.TimeOfDay, course2.InstructorName, course2.InstructorEmail, course2.InstructorPhone, course2.InstructorUsername);
					if (clockWorkCourseLuCourseId > 0)
					{
						string commandText = "DECLARE @spaid int\r\nIF EXISTS(SELECT serviceproviderapplicationid FROM serviceproviderapplications WHERE serviceproviderid=@spid AND serviceprovidertype=@sptype AND dateentered>=@sdate AND dateentered<=@edate)\r\nBEGIN\r\n    SET @spaid = (SELECT TOP 1 serviceproviderapplicationid FROM serviceproviderapplications WHERE serviceproviderid=@spid AND serviceprovidertype=@sptype AND dateentered>=@sdate AND dateentered<=@edate)\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO serviceproviderapplications (serviceproviderid,serviceprovidertype,dateentered,isactive) VALUES (@spid,@sptype,getdate(),1)\r\n    SELECT @spaid = (SELECT CAST(SCOPE_IDENTITY() AS int))\r\nEND\r\nINSERT INTO serviceproviderapplicationcourses (serviceproviderapplicationid,serviceprovidertype,lucourseid,registrationstatus)\r\nVALUES (@spaid,@sptype,@lucid,0)";
						da.SelectCommand.CommandText = commandText;
						da.SelectCommand.Parameters.Clear();
						da.SelectCommand.Parameters.Add("@spid", spid);
						da.SelectCommand.Parameters.Add("@sptype", sptype);
						da.SelectCommand.Parameters.Add("@sdate", course2.StartDate);
						da.SelectCommand.Parameters.Add("@edate", course2.EndDate);
						da.SelectCommand.Parameters.Add("@lucid", clockWorkCourseLuCourseId);
						string text;
						da.Fill(new DataTable(), out text);
						if (!string.IsNullOrEmpty(text))
						{
							ReportFunction.MessageBoxShow(text);
						}
					}
					else
					{
						ReportFunction.MessageBoxShow("Missing lucid");
					}
				}
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00027BD0 File Offset: 0x00026BD0
		public static DataView ImportStudentCourses(DataView dv, string parameters, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar, TripleDESEncryptionClass tripleDES, UnivDataAdapter da, bool writeChangesToClockWorkDatabase, string[] daysOfWeek, bool supportsTimetable, bool supportsInstructorUsername, DataTable noPids, bool supportsCourseRegistrationStatus, DateTime sdate, DateTime edate, DataTable pidTable, string snum)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = new string[]
			{
				"a",
				"n"
			};
			DataView result;
			try
			{
				DataTable emptyGeneralLogTable = ReportFunction.GetEmptyGeneralLogTable(false, false, false, true);
				dv.Sort = "student_no";
				DataTable table = dv.Table;
				ReportFunction.EnsureColExists(ref table, "timeofday");
				ReportFunction.EnsureColExists(ref table, "duration");
				ReportFunction.EnsureColExists(ref table, "instructorname");
				ReportFunction.EnsureColExists(ref table, "instructoremail");
				ReportFunction.EnsureColExists(ref table, "instructorphone");
				bool flag = table.Columns.Contains("studentgrade");
				bool flag2 = dv.Table.Columns.IndexOf("monroom") >= 0;
				int num = 0;
				try
				{
					if (dv.Count < 1)
					{
						if (!string.IsNullOrEmpty(snum))
						{
							num = ClockWorkAPI.Utility.LookupPersonId(da, tripleDES, snum);
							if (num > 0)
							{
								string commandText = "IF EXISTS(SELECT personid FROM datetimeinfops WHERE controlid=8 AND personid=@pid)\r\n                    UPDATE datetimeinfops SET controlvalue=getdate() WHERE controlid=8 AND personid=@pid\r\n                ELSE\r\n                    INSERT INTO datetimeinfops(screennum,personid,controlid,controlvalue) VALUES (1,@pid,8,getdate())";
								da.SelectCommand.CommandText = commandText;
								da.SelectCommand.Parameters.Clear();
								da.SelectCommand.Parameters.Add("@pid", num);
								da.Fill(new DataTable());
							}
						}
					}
					else
					{
						string text = dv[0]["student_no"].ToString().Trim();
						num = ClockWorkAPI.Utility.LookupPersonId(da, tripleDES, snum);
						if (num > 0)
						{
							string commandText = "DELETE FROM datetimeinfops WHERE controlid=8 AND personid=@pid";
							da.SelectCommand.CommandText = commandText;
							da.SelectCommand.Parameters.Clear();
							da.SelectCommand.Parameters.Add("@pid", num);
							da.Fill(new DataTable());
						}
					}
				}
				catch
				{
				}
				CWLogger.Logger.Info("ReportFunction:ImportStudentCourses:Start:pid={0}:dvCount={1}:hasgrade={2}:timetableroomexists={3}", num.ToString(), (dv == null) ? "NULL" : dv.Count.ToString(), flag2.ToString());
				int i = 0;
				while (i < dv.Count)
				{
					bool flag3 = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
					string text2 = dv[i]["student_no"].ToString().Trim();
					if (pidTable == null)
					{
						da.SelectCommand.CommandText = "SELECT  personid,student_no FROM people WHERE student_no=@sne AND isactive=1";
						da.SelectCommand.Parameters.Clear();
						da.SelectCommand.Parameters.Add("@sne", tripleDES.Encrypt(text2));
						pidTable = new DataTable();
						string text3;
						da.Fill(pidTable, out text3);
						pidTable = tripleDES.EncryptOrDecryptNameDataTableBatch(false, pidTable, new string[]
						{
							"student_no"
						});
					}
					DataRow[] array2 = pidTable.Select("student_no='" + text2.Replace("'", "''") + "'");
					int num2;
					if (array2 != null && array2.Length > 0)
					{
						num2 = (int)array2[0][0];
					}
					else
					{
						num2 = -1;
					}
					DataRow[] array3 = noPids.Select("personid=" + num2.ToString());
					if (array3 != null && array3.Length > 0)
					{
						num2 = 0;
					}
					string newLine = System.Environment.NewLine;
					stringBuilder.Append(string.Concat(new string[]
					{
						"Import courses start: snum='",
						text2,
						"'; pid=",
						num2.ToString(),
						newLine
					}));
					dv.Table.Columns.Add("lucid", typeof(int));
					if (num2 > 0)
					{
						int j = i + 1;
						string strB = text2.ToLower().Trim();
						while (j < dv.Count)
						{
							string text4 = dv[j]["student_no"].ToString().Trim().ToLower();
							if (text4.CompareTo(strB) != 0)
							{
								break;
							}
							j++;
						}
						da.SelectCommand.CommandText = "SELECT DISTINCT c.coursesid,c.lucourseid,c.personid,luc.startdate,luc.enddate,luc.term,luc.duration,luc.subjectid,lucd.altlookupstring AS subject,luc.course,luc.timeofday,luc.section,luc.instructorid,lucd2.altlookupstring AS instructorname,lucd2.email AS instructoremail,lucd2.phone AS instructorphone,c.registrationstatus";
						if (supportsTimetable)
						{
							UnivCommand selectCommand = da.SelectCommand;
							selectCommand.CommandText += ",tt.sunstartminutes,tt.sunendminutes,tt.monstartminutes,tt.monendminutes,tt.tuestartminutes,tt.tueendminutes,tt.wedstartminutes,tt.wedendminutes,tt.thustartminutes,tt.thuendminutes,tt.fristartminutes,tt.friendminutes,tt.satstartminutes,tt.satendminutes";
						}
						else
						{
							UnivCommand selectCommand2 = da.SelectCommand;
							selectCommand2.CommandText += ",0 AS sunstartminutes,0 AS sunendminutes,0 AS monstartminutes,0 AS monendminutes,0 AS tuestartminutes,0 AS tueendminutes,0 AS wedstartminutes,0 AS wedendminutes,0 AS thustartminutes,0 AS thuendminutes,0 AS fristartminutes,0 AS friendminutes,0 AS satstartminutes,0 AS satendminutes";
						}
						if (supportsInstructorUsername)
						{
							UnivCommand selectCommand3 = da.SelectCommand;
							selectCommand3.CommandText += ",lucd2.username AS instructorusername";
						}
						else
						{
							UnivCommand selectCommand4 = da.SelectCommand;
							selectCommand4.CommandText += ",'' AS instructorusername";
						}
						UnivCommand selectCommand5 = da.SelectCommand;
						selectCommand5.CommandText += ",tt.timetableid FROM courses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid";
						if (supportsTimetable)
						{
							UnivCommand selectCommand6 = da.SelectCommand;
							selectCommand6.CommandText += " LEFT JOIN timetable tt ON tt.lucourseid=c.lucourseid AND tt.timetabletype='C'";
						}
						UnivCommand selectCommand7 = da.SelectCommand;
						selectCommand7.CommandText += " WHERE c.personid=@pid ";
						UnivCommand selectCommand8 = da.SelectCommand;
						selectCommand8.CommandText += " AND luc.enddate >= getdate() ";
						UnivCommand selectCommand9 = da.SelectCommand;
						selectCommand9.CommandText += " ORDER BY c.coursesid,tt.timetableid DESC";
						da.SelectCommand.Parameters.Clear();
						da.SelectCommand.Parameters.Add("@sne", tripleDES.Encrypt(text2));
						da.SelectCommand.Parameters.Add("@sdate", sdate);
						da.SelectCommand.Parameters.Add("@edate", edate);
						da.SelectCommand.Parameters.Add("@pid", num2);
						DataTable dataTable = new DataTable();
						string text5;
						da.Fill(dataTable, out text5);
						if (text5 != null && text5.Length > 0)
						{
							ReportFunction.MessageBoxShow(text5);
						}
						dataTable.Columns.Add("status", typeof(int));
						for (int k = 0; k < dataTable.Rows.Count; k++)
						{
							dataTable.Rows[k]["status"] = 0;
						}
						stringBuilder.Append(string.Concat(new string[]
						{
							"Students courses:",
							newLine,
							ReportFunction.DataTableToString(dataTable),
							newLine,
							newLine
						}));
						ArrayList arrayList = new ArrayList();
						if (dv.Table.Columns.Contains("coursestatus"))
						{
							for (int k = 0; k < j; k++)
							{
								DataRow row = dv[k].Row;
								string value = row["coursestatus"].ToString().ToLower().Trim();
								if (Array.IndexOf<string>(array, value) < 0)
								{
								}
							}
						}
						for (int k = i; k < j; k++)
						{
							DataRow row = dv[k].Row;
							string text6 = row["term"].ToString().Trim();
							string text7 = dv.Table.Columns.Contains("duration") ? row["duration"].ToString().Trim() : "";
							string text8 = row["subject"].ToString().Trim();
							string text9 = row["course"].ToString().Trim();
							string text10 = row["section"].ToString().Trim();
							string text11 = dv.Table.Columns.Contains("timeofday") ? row["timeofday"].ToString().Trim() : "";
							string text12 = dv.Table.Columns.Contains("instructorname") ? row["instructorname"].ToString().Trim() : "";
							string text13 = dv.Table.Columns.Contains("instructoremail") ? row["instructoremail"].ToString().Trim() : "";
							string instructorPhone = dv.Table.Columns.Contains("instructorphone") ? row["instructorphone"].ToString().Trim() : "";
							string text14 = dv.Table.Columns.Contains("instructorusername") ? row["instructorusername"].ToString().Trim() : "";
							stringBuilder.Append(string.Concat(new string[]
							{
								k.ToString(),
								": ",
								text6,
								" ",
								text7,
								" ",
								text8,
								" ",
								text9,
								" ",
								text10,
								newLine
							}));
							string text15 = text6.ToLower();
							string text16 = text7.ToLower();
							string text17 = text8.ToLower();
							string text18 = text9.ToLower();
							string text19 = text11.ToLower();
							string text20 = text10.ToLower();
							string text21 = text12;
							string instructorEmail = text13.ToLower();
							bool flag4 = false;
							DateTime dateTime;
							DateTime dateTime2;
							if (dv.Table.Columns.Contains("startdate") && dv.Table.Columns.Contains("enddate"))
							{
								string s = row["startdate"].ToString();
								string s2 = row["enddate"].ToString();
								try
								{
									dateTime = DateTime.Parse(s);
									dateTime2 = DateTime.Parse(s2);
								}
								catch (Exception ex)
								{
									dateTime = sdate;
									dateTime2 = edate;
								}
							}
							else
							{
								dateTime = sdate;
								dateTime2 = edate;
							}
							DataRow[] array4 = dataTable.Select(string.Format("duration='{0}' AND term='{1}' AND subject='{2}' AND course='{3}' AND timeofday='{4}' AND section='{5}'", new object[]
							{
								text16.Replace("'", "''"),
								text15.Replace("'", "''"),
								text17.Replace("'", "''"),
								text18.Replace("'", "''"),
								text19.Replace("'", "''"),
								text20.Replace("'", "''")
							}));
							stringBuilder.Append("Found in student courses: " + ((array4 == null) ? "NO" : "Yes") + newLine);
							if (array4 != null && array4.Length > 0)
							{
								DataRow dataRow = array4[0];
								int num3 = (dataRow["lucourseid"] == DBNull.Value) ? 0 : ((int)dataRow["lucourseid"]);
								row["lucid"] = num3;
								stringBuilder.Append("Found lucid: " + num3.ToString() + newLine);
								flag4 = true;
								if (dataRow["status"] == DBNull.Value || (int)dataRow["status"] == 0)
								{
									string text22 = dataRow["timeofday"].ToString().Trim().ToLower();
									string text23 = dataRow["section"].ToString().Trim().ToLower();
									string text24 = dataRow["instructorname"].ToString().Trim().ToLower();
									string text25 = dataRow["instructoremail"].ToString().Trim().ToLower();
									string text26 = dataRow["instructorusername"].ToString().Trim().ToLower();
									int num4 = (dataRow["registrationstatus"] == DBNull.Value) ? 0 : ((int)dataRow["registrationstatus"]);
									stringBuilder.Append(string.Concat(new string[]
									{
										"Instructor change required? (",
										text24,
										" / ",
										text21,
										"): "
									}));
									if (!string.IsNullOrEmpty(text21) && !text24.Equals(text21.ToLower()))
									{
										dataRow["status"] = 1;
										dataRow["instructorname"] = text21;
										int num5 = ReportFunction.LookupInstructor(da, text12, instructorEmail, instructorPhone, text14);
										dataRow["instructorid"] = num5;
										stringBuilder.Append("Yes" + newLine);
									}
									else
									{
										stringBuilder.Append("No" + newLine);
									}
									if (num4 != 1)
									{
										dataRow["status"] = 1;
									}
									else
									{
										dataRow["status"] = 1;
										stringBuilder.Append("No misc change." + newLine);
									}
									if (num3 > 0 && supportsTimetable && row.Table.Columns.Contains("monstartminutes"))
									{
										List<TimeTableItem> timetableItems = TimeTableItem.GetTimetableItems(row);
										List<TimeTableItem> timetableItems2 = TimeTableItem.GetTimetableItems(dataRow);
										bool flag5 = true;
										foreach (TimeTableItem timeTableItem in timetableItems)
										{
											bool flag6 = false;
											foreach (TimeTableItem timeTableItem2 in timetableItems2)
											{
												if (timeTableItem2.DayOfWeek == timeTableItem.DayOfWeek && timeTableItem2.StartMinutes == timeTableItem.StartMinutes && timeTableItem2.EndMinutes == timeTableItem.EndMinutes && timeTableItem2.Location.ToLower().Trim().Equals(timeTableItem.Location.ToLower().Trim()))
												{
													flag6 = true;
													break;
												}
											}
											if (!flag6)
											{
												flag5 = false;
												break;
											}
										}
										if (flag5)
										{
											foreach (TimeTableItem timeTableItem2 in timetableItems2)
											{
												bool flag6 = false;
												foreach (TimeTableItem timeTableItem in timetableItems)
												{
													if (timeTableItem2.DayOfWeek == timeTableItem.DayOfWeek && timeTableItem2.StartMinutes == timeTableItem.StartMinutes && timeTableItem2.EndMinutes == timeTableItem.EndMinutes && timeTableItem2.Location.ToLower().Trim().Equals(timeTableItem.Location.ToLower().Trim()))
													{
														flag6 = true;
														break;
													}
												}
												if (!flag6)
												{
													flag5 = false;
													break;
												}
											}
										}
										if (!flag5)
										{
											da.SelectCommand.CommandText = "DELETE FROM timetable WHERE lucourseid=@lucid;";
											if (flag2)
											{
												UnivCommand selectCommand10 = da.SelectCommand;
												selectCommand10.CommandText += "INSERT INTO timetable (lucourseid,timetabletype,sunstartminutes,sunendminutes,sunroom,monstartminutes,monendminutes,monroom,tuestartminutes,tueendminutes,tueroom,wedstartminutes,wedendminutes,wedroom,thustartminutes,thuendminutes,thuroom,fristartminutes,friendminutes,friroom,satstartminutes,satendminutes,satroom) VALUES (@lucid,'C',@sunstartminutes,@sunendminutes,@sunroom,@monstartminutes,@monendminutes,@monroom,@tuestartminutes,@tueendminutes,@tueroom,@wedstartminutes,@wedendminutes,@wedroom,@thustartminutes,@thuendminutes,@thuroom,@fristartminutes,@friendminutes,@friroom,@satstartminutes,@satendminutes,@satroom)";
											}
											else
											{
												UnivCommand selectCommand11 = da.SelectCommand;
												selectCommand11.CommandText += "INSERT INTO timetable (lucourseid,timetabletype,sunstartminutes,sunendminutes,monstartminutes,monendminutes,tuestartminutes,tueendminutes,wedstartminutes,wedendminutes,thustartminutes,thuendminutes,fristartminutes,friendminutes,satstartminutes,satendminutes) VALUES (@lucid,'C',@sunstartminutes,@sunendminutes,@monstartminutes,@monendminutes,@tuestartminutes,@tueendminutes,@wedstartminutes,@wedendminutes,@thustartminutes,@thuendminutes,@fristartminutes,@friendminutes,@satstartminutes,@satendminutes)";
											}
											da.SelectCommand.Parameters.Clear();
											da.SelectCommand.Parameters.Add("@lucid", num3);
											for (int l = 0; l < 7; l++)
											{
												string text27 = daysOfWeek[l] + "startminutes";
												string text28 = daysOfWeek[l] + "endminutes";
												da.SelectCommand.Parameters.Add("@" + text27, row[text27]);
												da.SelectCommand.Parameters.Add("@" + text28, row[text28]);
												if (flag2)
												{
													string columnName = daysOfWeek[l] + "room";
													string parameterValue = (row.Table.Columns.IndexOf(columnName) >= 0) ? row[daysOfWeek[l] + "room"].ToString() : "";
													da.SelectCommand.Parameters.Add("@" + daysOfWeek[l] + "room", parameterValue);
												}
											}
											da.SelectCommand.ExecuteNonQuery();
										}
									}
									if (dataRow["startdate"] != DBNull.Value && dataRow["enddate"] != DBNull.Value)
									{
										DateTime dateTime3 = (DateTime)dataRow["startdate"];
										DateTime dateTime4 = (DateTime)dataRow["enddate"];
										if ((dateTime != DateTime.MinValue && dateTime2 != DateTime.MinValue && dateTime < dateTime2 && dateTime.Year != dateTime3.Year) || dateTime.Month != dateTime3.Month || dateTime.Day != dateTime3.Day || dateTime2.Year != dateTime4.Year || dateTime2.Month != dateTime4.Month || dateTime2.Day != dateTime4.Day)
										{
											string commandText2 = "UPDATE lucourses SET startdate=@sdate,enddate=@edate WHERE lucourseid=@lucid";
											da.SelectCommand.CommandText = commandText2;
											da.SelectCommand.Parameters.Clear();
											da.SelectCommand.Parameters.Add("@sdate", dateTime);
											da.SelectCommand.Parameters.Add("@edate", dateTime2);
											da.SelectCommand.Parameters.Add("@lucid", num3);
											string text29;
											da.Fill(new DataTable(), out text29);
											if (!string.IsNullOrEmpty(text29))
											{
												ReportFunction.MessageBoxShow(text29);
											}
										}
									}
									stringBuilder.Append("drc['status']=" + dataRow["status"].ToString() + newLine);
								}
							}
							if (!flag4)
							{
								stringBuilder.Append(string.Concat(new string[]
								{
									"Adding new course [",
									text8,
									" ",
									text9,
									"]: ",
									dateTime.ToString("yyyy-MM-dd"),
									" to ",
									dateTime2.ToString("yyyy-MM-dd"),
									newLine
								}));
								int clockWorkCourseLuCourseId = ReportFunction.GetClockWorkCourseLuCourseId(da, dateTime, dateTime2, text6, text7, text8, text9, text10, text11, text12, text13, instructorPhone, text14);
								row["lucid"] = clockWorkCourseLuCourseId;
								stringBuilder.Append("tried to lookup lucid: " + clockWorkCourseLuCourseId.ToString() + newLine);
								if (clockWorkCourseLuCourseId > -1)
								{
									DataRow dataRow2 = dataTable.NewRow();
									dataRow2["coursesid"] = -1;
									dataRow2["lucourseid"] = clockWorkCourseLuCourseId;
									dataRow2["term"] = text6;
									dataRow2["duration"] = text7;
									dataRow2["subject"] = text8;
									dataRow2["course"] = text9;
									dataRow2["status"] = 2;
									if (row.Table.Columns.Contains("monstartminutes"))
									{
										for (int l = 0; l < 7; l++)
										{
											string text27 = daysOfWeek[l] + "startminutes";
											string text28 = daysOfWeek[l] + "endminutes";
											dataRow2[text27] = row[text27];
											dataRow2[text28] = row[text28];
										}
									}
									dataTable.Rows.Add(dataRow2);
								}
							}
						}
						List<int> list = new List<int>();
						foreach (object obj in dataTable.Rows)
						{
							DataRow dataRow = (DataRow)obj;
							int num3 = (int)dataRow["lucourseid"];
							if (!list.Contains(num3))
							{
								list.Add(num3);
								int num6 = (int)dataRow["status"];
								if (num6 == 0)
								{
									DateTime dateTime = (dataRow["startDate"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dataRow["startdate"]);
									if (dateTime >= sdate)
									{
										if (supportsCourseRegistrationStatus && dv.Count > 0)
										{
											if (num6 != 8)
											{
												int num7 = (int)dataRow["coursesid"];
												da.SelectCommand.CommandText = "UPDATE courses SET registrationstatus=2 WHERE coursesid=" + num7.ToString();
												da.SelectCommand.ExecuteNonQuery();
												ReportFunction.LogToDataTable(ref emptyGeneralLogTable, new object[]
												{
													num2,
													-555,
													1,
													1,
													num7,
													"dropped"
												});
											}
										}
									}
								}
								else if (num6 == 1)
								{
									int num7 = (int)dataRow["coursesid"];
									da.SelectCommand.CommandText = "UPDATE courses SET registrationstatus=1 WHERE coursesid=" + num7.ToString();
									da.SelectCommand.ExecuteNonQuery();
									try
									{
										string text22 = dataRow["timeofday"].ToString().Trim().ToLower();
										string text23 = dataRow["section"].ToString().Trim().ToLower();
										int num8 = (int)dataRow["lucourseid"];
										int num9 = (dataRow["instructorid"] == DBNull.Value) ? -1 : ((int)dataRow["instructorid"]);
										string text14 = (dataRow["instructorusername"] == DBNull.Value) ? "" : dataRow["instructorusername"].ToString();
										string text30 = dataRow["instructoremail"].ToString();
										da.SelectCommand.CommandText = string.Concat(new string[]
										{
											"UPDATE lucourses SET section='",
											text23,
											"',timeofday='",
											text22,
											"',instructorid=",
											num9.ToString(),
											" WHERE lucourseid=",
											num8.ToString()
										});
										da.SelectCommand.ExecuteNonQuery();
										bool flag7 = 0 == 0;
									}
									catch
									{
									}
									ReportFunction.LogToDataTable(ref emptyGeneralLogTable, new object[]
									{
										num2,
										-555,
										1,
										1,
										num7,
										"UN-dropped"
									});
								}
								else if (num6 == 2)
								{
									if (num3 > 0)
									{
										da.SelectCommand.CommandText = "INSERT INTO courses (personid,lucourseid,dateadded,whoadded,registrationstatus";
										if (flag)
										{
											UnivCommand selectCommand12 = da.SelectCommand;
											selectCommand12.CommandText += ",grade";
										}
										UnivCommand selectCommand13 = da.SelectCommand;
										selectCommand13.CommandText += ") ";
										UnivCommand selectCommand14 = da.SelectCommand;
										selectCommand14.CommandText += "SELECT @pid AS pid,@lucid AS lucid,getdate() AS dateadded,-555 AS whoadded,1 AS regstatus ";
										if (flag)
										{
											UnivCommand selectCommand15 = da.SelectCommand;
											selectCommand15.CommandText += ",@grade AS grade ";
										}
										UnivCommand selectCommand16 = da.SelectCommand;
										selectCommand16.CommandText += "WHERE NOT EXISTS (SELECT @pid FROM courses WHERE personid=@pid AND lucourseid=@lucid)";
										da.SelectCommand.Parameters.Clear();
										da.SelectCommand.Parameters.Add("@pid", num2);
										da.SelectCommand.Parameters.Add("@lucid", num3);
										if (flag)
										{
											da.SelectCommand.Parameters.Add("@grade", dataRow["studentgrade"].ToString().Trim());
										}
										int num10 = da.SelectCommand.ExecuteNonQuery();
										if (supportsTimetable)
										{
											da.SelectCommand.CommandText = "DELETE FROM timetable WHERE lucourseid=@lucid;";
											if (flag2)
											{
												UnivCommand selectCommand17 = da.SelectCommand;
												selectCommand17.CommandText += "INSERT INTO timetable \r\n        (lucourseid,timetabletype,sunstartminutes,sunendminutes,sunroom,monstartminutes,monendminutes,monroom,tuestartminutes,tueendminutes,tueroom,wedstartminutes,wedendminutes,wedroom,thustartminutes,thuendminutes,thuroom,fristartminutes,friendminutes,friroom,satstartminutes,satendminutes,satroom) \r\nVALUES  (@lucid,'C',@sunstartminutes,@sunendminutes,@sunroom,@monstartminutes,@monendminutes,@monroom,@tuestartminutes,@tueendminutes,@tueroom,@wedstartminutes,@wedendminutes,@wedroom,@thustartminutes,@thuendminutes,@thuroom,@fristartminutes,@friendminutes,@friroom,@satstartminutes,@satendminutes,@satroom)";
											}
											else
											{
												UnivCommand selectCommand18 = da.SelectCommand;
												selectCommand18.CommandText += "INSERT INTO timetable (lucourseid,timetabletype,sunstartminutes,sunendminutes,monstartminutes,monendminutes,tuestartminutes,tueendminutes,wedstartminutes,wedendminutes,thustartminutes,thuendminutes,fristartminutes,friendminutes,satstartminutes,satendminutes) VALUES (@lucid,'C',@sunstartminutes,@sunendminutes,@monstartminutes,@monendminutes,@tuestartminutes,@tueendminutes,@wedstartminutes,@wedendminutes,@thustartminutes,@thuendminutes,@fristartminutes,@friendminutes,@satstartminutes,@satendminutes)";
											}
											da.SelectCommand.Parameters.Clear();
											da.SelectCommand.Parameters.Add("@lucid", num3);
											List<string> list2 = new List<string>();
											for (int l = 0; l < 7; l++)
											{
												string text27 = daysOfWeek[l] + "startminutes";
												string text28 = daysOfWeek[l] + "endminutes";
												da.SelectCommand.Parameters.Add("@" + text27, dataRow[text27]);
												da.SelectCommand.Parameters.Add("@" + text28, dataRow[text28]);
												if (flag2)
												{
													string columnName2 = daysOfWeek[l] + "room";
													string text31 = (dataRow.Table.Columns.IndexOf(columnName2) >= 0) ? dataRow[columnName2].ToString() : "";
													if (!string.IsNullOrEmpty(text31) && text31.Trim().Length > 0)
													{
														if (!list2.Contains(text31))
														{
															list2.Add(text31);
														}
													}
													da.SelectCommand.Parameters.Add("@" + daysOfWeek[l] + "room", text31);
												}
											}
											da.SelectCommand.ExecuteNonQuery();
											da.SelectCommand.CommandText = "UPDATE lucourses SET location=@room WHERE lucourseid=@id";
											da.SelectCommand.Parameters.Clear();
											da.SelectCommand.Parameters.Add("@room", ClockWorkAPI.Utility.ListToString(list2));
											da.SelectCommand.Parameters.Add("@id", num3);
											da.SelectCommand.ExecuteNonQuery();
										}
										ReportFunction.LogToDataTable(ref emptyGeneralLogTable, new object[]
										{
											num2,
											-555,
											1,
											1,
											num3,
											"added: " + num10.ToString()
										});
									}
								}
							}
						}
						i = j;
					}
					else
					{
						i++;
					}
				}
				DataView dv2 = ReportFunction.UpdateInstructorInfo(da, tripleDES, dv);
				DataView dataView = ReportFunction.ImportLuCourseCampusLocationEtc(da, tripleDES, dv2);
				result = dataView;
			}
			catch (Exception ex2)
			{
				ReportFunction.MessageBoxShow(ex2.ToString());
				result = null;
			}
			return result;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00029A38 File Offset: 0x00028A38
		public static DataView UpdateInstructorInfo(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, DataView dv)
		{
			DataView result;
			if (ReportFunction.IgnoreUpdateInstructorInfo)
			{
				result = dv;
			}
			else
			{
				try
				{
					DataTable table = dv.Table;
					bool flag = table.Columns.Contains("instructorname");
					bool flag2 = table.Columns.Contains("instructoremail");
					bool flag3 = table.Columns.Contains("instructorusername");
					StringBuilder stringBuilder = new StringBuilder();
					foreach (object obj in table.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						if (dataRow["lucid"] != DBNull.Value)
						{
							string value = dataRow["lucid"].ToString();
							if (stringBuilder.Length > 0)
							{
								stringBuilder.Append(", ");
							}
							stringBuilder.Append(value);
						}
					}
					string commandText = "SELECT luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,luc.section,luc.timeofday\r\n        ,luc.location,luc.campus,luc.department,luc.other1,luc.other2\r\n        ,luc.instructorid,lucd2.altlookupstring,lucd2.username,lucd2.email\r\nFROM    lucourses luc LEFT JOIN lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\nWHERE   luc.lucourseid IN (SELECT orderid AS lucourseid FROM splitorderids(@lucids,','))";
					da.SelectCommand.CommandText = commandText;
					da.SelectCommand.Parameters.Clear();
					da.SelectCommand.Parameters.Add("@lucids", stringBuilder.ToString());
					DataTable dataTable = new DataTable();
					da.Fill(dataTable);
					foreach (object obj2 in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj2;
						int num = (dataRow["instructorid"] == DBNull.Value) ? 0 : ((int)dataRow["instructorid"]);
						int num2 = (int)dataRow["lucourseid"];
						DataRow[] array = table.Select(string.Format("lucid={0}", num2.ToString()));
						if (array.Length > 0 && num > 0)
						{
							string value2 = dataRow["altlookupstring"].ToString().Trim();
							string value3 = dataRow["email"].ToString().Trim();
							string value4 = dataRow["username"].ToString().Trim();
							string text = flag ? array[0]["instructorname"].ToString().Trim() : null;
							string text2 = flag2 ? array[0]["instructoremail"].ToString().Trim() : null;
							string text3 = flag3 ? array[0]["instructorusername"].ToString().Trim() : null;
							bool flag4 = !string.IsNullOrEmpty(text) && !text.Equals(value2, StringComparison.OrdinalIgnoreCase);
							bool flag5 = !string.IsNullOrEmpty(text2) && !text2.Equals(value3, StringComparison.OrdinalIgnoreCase);
							bool flag6 = !string.IsNullOrEmpty(text3) && !text3.Equals(value4, StringComparison.OrdinalIgnoreCase);
							if (flag3)
							{
								if (flag6)
								{
									int num3 = ReportFunction.LookupInstructorByUsername(da, text, text2, "", text3);
									if (num3 > 0 && num3 != num)
									{
										da.SelectCommand.CommandText = "UPDATE lucourses SET instructorid=@newiid WHERE lucourseid=@lucid";
										da.SelectCommand.Parameters.Clear();
										da.SelectCommand.Parameters.Add("@newiid", num3);
										da.SelectCommand.Parameters.Add("@lucid", num2);
										da.Fill(new DataTable());
									}
								}
								else if (flag4 || flag5)
								{
									da.SelectCommand.Parameters.Clear();
									da.SelectCommand.Parameters.Add("@iid", num);
									if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
									{
										da.SelectCommand.CommandText = "UPDATE lucoursedata SET email=@email,altlookupstring=@name,lookupstring=@name WHERE lucoursedataid=@iid";
										da.SelectCommand.Parameters.Add("@email", text2);
										da.SelectCommand.Parameters.Add("@name", text);
									}
									else if (!string.IsNullOrEmpty(text))
									{
										da.SelectCommand.CommandText = "UPDATE lucoursedata SET altlookupstring=@name,lookupstring=@name WHERE lucoursedataid=@iid";
										da.SelectCommand.Parameters.Add("@name", text);
									}
									else if (!string.IsNullOrEmpty(text2))
									{
										da.SelectCommand.CommandText = "UPDATE lucoursedata SET email=@email WHERE lucoursedataid=@iid";
										da.SelectCommand.Parameters.Add("@email", text2);
									}
									bool flag7 = false;
									if (flag7)
									{
										da.Fill(new DataTable());
									}
								}
							}
							else if (flag5)
							{
								int num3 = ReportFunction.LookupInstructorByEmail(da, text, text2, "");
								if (num3 > 0 && num3 != num)
								{
									da.SelectCommand.CommandText = "UPDATE lucourses SET instructorid=@newiid WHERE lucourseid=@lucid";
									da.SelectCommand.Parameters.Clear();
									da.SelectCommand.Parameters.Add("@newiid", num3);
									da.SelectCommand.Parameters.Add("@lucid", num2);
									da.Fill(new DataTable());
								}
							}
							else if (flag4)
							{
								if (!string.IsNullOrEmpty(text))
								{
									da.SelectCommand.CommandText = "UPDATE lucoursedata SET altlookupstring=@name,lookupstring=@name WHERE lucoursedataid=@iid";
									da.SelectCommand.Parameters.Clear();
									da.SelectCommand.Parameters.Add("@iid", num);
									da.SelectCommand.Parameters.Add("@name", text);
								}
							}
						}
					}
					result = dv;
				}
				catch
				{
					result = dv;
				}
			}
			return result;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0002A0BC File Offset: 0x000290BC
		public static DataView ImportLuCourseCampusLocationEtc(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, DataView dv)
		{
			DataView result;
			if (ReportFunction.ignoreImportLuCourseCampusLocationEtc)
			{
				result = dv;
			}
			else
			{
				DataTable table = dv.Table;
				bool flag = table.Columns.Contains("campus");
				bool flag2 = table.Columns.Contains("location");
				bool flag3 = table.Columns.Contains("department");
				if (!flag && !flag2 && !flag3)
				{
					result = dv;
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (object obj in table.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						if (dataRow["lucid"] != DBNull.Value)
						{
							string value = dataRow["lucid"].ToString();
							if (stringBuilder.Length > 0)
							{
								stringBuilder.Append(", ");
							}
							stringBuilder.Append(value);
						}
					}
					string commandText = "SELECT luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,luc.section,luc.timeofday\r\n        ,luc.location,luc.campus,luc.department,luc.other1,luc.other2\r\nFROM    lucourses luc \r\nWHERE   luc.lucourseid IN (SELECT orderid AS lucourseid FROM splitorderids(@lucids,','))";
					da.SelectCommand.CommandText = commandText;
					da.SelectCommand.Parameters.Clear();
					da.SelectCommand.Parameters.Add("@lucids", stringBuilder.ToString());
					DataTable dataTable = new DataTable();
					da.Fill(dataTable);
					foreach (object obj2 in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj2;
						int num = (int)dataRow["lucourseid"];
						DataRow[] array = table.Select(string.Format("lucid={0}", num.ToString()));
						if (array.Length > 0)
						{
							string text = dataRow["campus"].ToString().Trim();
							string text2 = dataRow["location"].ToString().Trim();
							string value2 = dataRow["department"].ToString().Trim();
							string text3 = null;
							string text4 = null;
							string text5 = null;
							if (flag)
							{
								string text6 = array[0]["campus"].ToString().Trim();
								if (!string.IsNullOrEmpty(text6) && !text6.Equals(text))
								{
									text3 = text6;
								}
							}
							if (flag2)
							{
								string text7 = array[0]["location"].ToString().Trim();
								if (!string.IsNullOrEmpty(text7) && !text7.Equals(text2))
								{
									text4 = text7;
								}
							}
							if (flag3)
							{
								string text8 = array[0]["department"].ToString().Trim();
								if (!string.IsNullOrEmpty(text8) && !text8.Equals(value2))
								{
									text5 = text8;
								}
							}
							if (text4 != null || text3 != null || text5 != null)
							{
								if (string.IsNullOrEmpty(text3))
								{
									text3 = text;
								}
								if (string.IsNullOrEmpty(text4))
								{
									text4 = text2;
								}
								if (string.IsNullOrEmpty(text5))
								{
								}
								commandText = "UPDATE lucourses SET campus=@campus,location=@location,department=@department WHERE lucourseid=@lucid";
								da.SelectCommand.CommandText = commandText;
								da.SelectCommand.Parameters.Clear();
								da.SelectCommand.Parameters.Add("@campus", text3);
								da.SelectCommand.Parameters.Add("@location", text4);
								da.SelectCommand.Parameters.Add("@lucid", num);
								da.Fill(new DataTable());
							}
						}
					}
					result = dv;
				}
			}
			return result;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0002A4F8 File Offset: 0x000294F8
		private static DataTable GetEmptyGeneralLogTable(bool includeSuccess, bool includeid2, bool includeid3, bool includeGeneralLogNote)
		{
			DataTable dataTable = new DataTable("generallog");
			Type typeFromHandle = typeof(int);
			if (includeSuccess)
			{
				dataTable.Columns.Add("success", typeof(bool));
			}
			dataTable.Columns.Add("personid", typeFromHandle);
			dataTable.Columns.Add("whodidit", typeFromHandle);
			dataTable.Columns.Add("logtype", typeFromHandle);
			dataTable.Columns.Add("logsubtype", typeFromHandle);
			dataTable.Columns.Add("id1", typeFromHandle);
			if (includeid2)
			{
				dataTable.Columns.Add("id2", typeFromHandle);
			}
			if (includeid3)
			{
				dataTable.Columns.Add("id3", typeFromHandle);
			}
			if (includeGeneralLogNote)
			{
				dataTable.Columns.Add("generallognote");
			}
			return dataTable;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0002A5EC File Offset: 0x000295EC
		public static DataView ImportStudents(DataView dv, string parameters, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar, TripleDESEncryptionClass tripleDES, UnivDataAdapter Da, bool writeChangesToClockWorkDatabase)
		{
			return ReportFunction.ImportStudents(dv, parameters, IncrementSubProgressBar, SetupSubProgressBar, tripleDES, Da, writeChangesToClockWorkDatabase, false);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0002A610 File Offset: 0x00029610
		public static DataView ImportStudents(DataView dv, string parameters, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar, TripleDESEncryptionClass tripleDES, UnivDataAdapter Da, bool writeChangesToClockWorkDatabase, bool suppressGuiMessages)
		{
			UnivDataAdapter univDataAdapter = Da.Clone();
			ReportFunction.Log("Import Students START " + DateTime.Now.ToString("yyyy-MM-dd hh:mm tt"));
			DataView result;
			try
			{
				string[] array = parameters.Split(System.Environment.NewLine.ToCharArray());
				DataTable table = dv.Table;
				if (SetupSubProgressBar != null)
				{
					SetupSubProgressBar(0, table.Rows.Count);
				}
				byte[] array2 = new byte[]
				{
					2
				};
				DataTable dataTable = new DataTable("rowstoimport");
				dataTable.Columns.Add("dataid", typeof(int));
				dataTable.Columns.Add("screennum", typeof(int));
				dataTable.Columns.Add("personid", typeof(int));
				dataTable.Columns.Add("controlid", typeof(int));
				dataTable.Columns.Add("action");
				dataTable.Columns.Add("controlvalueint", typeof(int));
				dataTable.Columns.Add("controlvaluebytes", array2.GetType());
				dataTable.Columns.Add("controlvaluedatetime", typeof(DateTime));
				dataTable.Columns.Add("note");
				dataTable.Columns.Add("action_taken");
				dataTable.Columns.Add("controlvalueimage", typeof(byte[]));
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < array.Length; i++)
				{
					try
					{
						string text = array[i].Trim();
						if (text.Length > 0)
						{
							DataImportRule value = new DataImportRule(univDataAdapter, tripleDES, table, text);
							arrayList.Add(value);
						}
					}
					catch (Exception ex)
					{
						ReportFunction.Log("Error parsing rule#" + i.ToString() + ": " + ex.ToString());
					}
				}
				ReportFunction.Log("Finished parsing rules: " + arrayList.Count.ToString() + " rule(s) found.");
				if (!table.Columns.Contains("pid"))
				{
					table.Columns.Add("pid", typeof(int));
				}
				ReportFunction.Log(ReportFunction.DataTableToString(table));
				int num = Convert.ToInt32(((table.Rows.Count > 0) ? (table.Rows.Count * 2) : 100) / 100) + 1;
				if (SetupSubProgressBar != null)
				{
					SetupSubProgressBar(0, table.Rows.Count);
				}
				ReportFunction.Log("starting to figure out personids: " + table.Rows.Count.ToString() + " row(s)");
				for (int j = 0; j < table.Rows.Count; j++)
				{
					if (IncrementSubProgressBar != null && j % num == 0)
					{
						IncrementSubProgressBar(num);
					}
					DataRow dataRow = table.Rows[j];
					if (dataRow.RowState != DataRowState.Deleted)
					{
						string text2 = dataRow["student_no"].ToString().Trim();
						ReportFunction.Log("Looking for student-no=" + text2 + " ...");
						byte[] parameterValue = tripleDES.Encrypt(text2);
						univDataAdapter.SelectCommand.CommandText = "SELECT personid,firstname,lastname,middlename FROM people WHERE student_no=@student_no AND isactive=1 AND personid IN (SELECT personid FROM peoplegroups WHERE groupid=1)";
						univDataAdapter.SelectCommand.Parameters.Clear();
						univDataAdapter.SelectCommand.Parameters.Add("@student_no", parameterValue);
						DataTable dataTable2 = new DataTable();
						univDataAdapter.Fill(dataTable2);
						if (dataTable2.Rows.Count < 1)
						{
							univDataAdapter.SelectCommand.CommandText = "SELECT personid,firstname,lastname,middlename FROM people WHERE student_no=@student_no AND isactive=1";
							univDataAdapter.SelectCommand.Parameters.Clear();
							univDataAdapter.SelectCommand.Parameters.Add("@student_no", parameterValue);
							dataTable2 = new DataTable();
							univDataAdapter.Fill(dataTable2);
						}
						if (dataTable2.Rows.Count > 0)
						{
							dataTable2 = tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable2, new string[]
							{
								"firstname",
								"middlename",
								"lastname"
							});
						}
						ReportFunction.Log(ReportFunction.DataTableToString(dataTable2));
						int num2;
						if (dataTable2.Rows.Count > 0)
						{
							DataRow dataRow2 = dataTable2.Rows[0];
							num2 = (int)dataRow2[0];
							string text3 = dataRow2["firstname"].ToString().Trim();
							string text4 = dataRow2["middlename"].ToString().Trim();
							string text5 = dataRow2["lastname"].ToString().Trim();
							string text6 = dataRow["firstname"].ToString();
							string text7 = table.Columns.Contains("middlename") ? dataRow["middlename"].ToString() : "";
							string text8 = dataRow["lastname"].ToString();
							List<string> list = new List<string>();
							univDataAdapter.SelectCommand.CommandText = "";
							univDataAdapter.SelectCommand.Parameters.Clear();
							if (text6.Length > 0 && !text6.ToLower().Equals(text3.ToLower()))
							{
								list.Add("firstname=@fne");
								univDataAdapter.SelectCommand.Parameters.Add("@fne", tripleDES.Encrypt(text6));
							}
							if (text8.Length > 0 && !text8.ToLower().Equals(text5.ToLower()))
							{
								list.Add("lastname=@lne");
								univDataAdapter.SelectCommand.Parameters.Add("@lne", tripleDES.Encrypt(text8));
							}
							if (text7.Length > 0 && !text7.ToLower().Equals(text4.ToLower()))
							{
								list.Add("middlename=@mne");
								univDataAdapter.SelectCommand.Parameters.Add("@mne", tripleDES.Encrypt(text7));
							}
							if (list.Count > 0)
							{
								univDataAdapter.SelectCommand.CommandText = "UPDATE people SET ";
								for (int k = 0; k < list.Count; k++)
								{
									if (k > 0)
									{
										UnivCommand selectCommand = univDataAdapter.SelectCommand;
										selectCommand.CommandText += ",";
									}
									UnivCommand selectCommand2 = univDataAdapter.SelectCommand;
									selectCommand2.CommandText += list[k];
								}
								UnivCommand selectCommand3 = univDataAdapter.SelectCommand;
								selectCommand3.CommandText += " WHERE personid=@pid";
								univDataAdapter.SelectCommand.Parameters.Add("@pid", num2);
								univDataAdapter.Fill(new DataTable());
							}
						}
						else
						{
							num2 = -1;
						}
						dataRow["pid"] = num2;
					}
				}
				ReportFunction.Log("Beginning mapping ...");
				for (int j = 0; j < table.Rows.Count; j++)
				{
					if (IncrementSubProgressBar != null && j % num == 0)
					{
						IncrementSubProgressBar(num);
					}
					DataRow dataRow = table.Rows[j];
					if (dataRow.RowState != DataRowState.Deleted)
					{
						int num2 = (int)dataRow["pid"];
						ReportFunction.Log("Pid=" + num2.ToString());
						if (num2 >= 0)
						{
							foreach (object obj in arrayList)
							{
								DataImportRule dataImportRule = (DataImportRule)obj;
								dataImportRule.Map(dataRow, num2, ref dataTable, table);
							}
						}
					}
				}
				num = Convert.ToInt32(((table.Rows.Count > 0) ? dataTable.Rows.Count : 100) / 100) + 1;
				if (SetupSubProgressBar != null)
				{
					SetupSubProgressBar(0, dataTable.Rows.Count);
				}
				bool flag = IncrementSubProgressBar != null;
				if (writeChangesToClockWorkDatabase)
				{
					try
					{
						ArrayList arrayList2 = new ArrayList();
						bool flag2 = true;
						ReportFunction.Log("Writing changes to database START (rowsToImport.Rows.Count=" + dataTable.Rows.Count.ToString() + ")");
						univDataAdapter.Connection.Open();
						for (int j = 0; j < dataTable.Rows.Count; j++)
						{
							if (flag && j % num == 0)
							{
								IncrementSubProgressBar(num);
							}
							DataRow dataRow = dataTable.Rows[j];
							string text9 = (string)dataRow[4];
							object obj2;
							string text10;
							if (dataRow[5] != DBNull.Value)
							{
								obj2 = dataRow[5];
								text10 = "maininfops";
							}
							else if (dataRow[6] != DBNull.Value)
							{
								obj2 = dataRow[6];
								text10 = "otherinfops";
							}
							else if (dataRow[7] != DBNull.Value)
							{
								obj2 = dataRow[7];
								text10 = "datetimeinfops";
							}
							else if (dataRow[10] != DBNull.Value)
							{
								obj2 = dataRow[10];
								text10 = "imageinfops";
							}
							else
							{
								obj2 = null;
								text10 = "";
							}
							int num2 = (int)dataRow[2];
							int num3 = (int)dataRow[3];
							int num4 = 1;
							if (obj2 != null)
							{
								try
								{
									string text11 = text9;
									if (text11 != null)
									{
										if (!(text11 == "add"))
										{
											if (!(text11 == "delete") && !(text11 == "deletechk"))
											{
												if (text11 == "modify")
												{
													univDataAdapter.SelectCommand.CommandText = string.Concat(new string[]
													{
														"UPDATE ",
														text10,
														" SET controlvalue=@controlvalue WHERE personid=",
														num2.ToString(),
														" AND controlid=",
														num3.ToString()
													});
													univDataAdapter.SelectCommand.Parameters.Clear();
													univDataAdapter.SelectCommand.Parameters.Add("@controlvalue", obj2);
													int num5 = univDataAdapter.SelectCommand.ExecuteNonQuery2();
													ReportFunction.Log(string.Concat(new string[]
													{
														j.ToString(),
														": ",
														UnivOleDbFactory.ToStringParametersExpanded(univDataAdapter.SelectCommand),
														";;;; executenonquery2=",
														num5.ToString()
													}));
													dataRow[9] = "Modified (" + num5.ToString() + ")";
												}
											}
											else
											{
												bool flag3 = text9.CompareTo("deletechk") == 0;
												if (!flag2 || flag3)
												{
													univDataAdapter.SelectCommand.CommandText = string.Concat(new string[]
													{
														"DELETE FROM ",
														text10,
														" WHERE personid=",
														num2.ToString(),
														" AND controlid=",
														num3.ToString()
													});
													int num5 = univDataAdapter.SelectCommand.ExecuteNonQuery2();
													ReportFunction.Log(string.Concat(new string[]
													{
														j.ToString(),
														": ",
														UnivOleDbFactory.ToStringParametersExpanded(univDataAdapter.SelectCommand),
														";;;; executenonquery2=",
														num5.ToString()
													}));
													dataRow[9] = "Deleted (" + num5.ToString() + ")";
												}
												else
												{
													dataRow[9] = "Not deleted (deleting is disabled)";
													arrayList2.Add(dataRow);
												}
											}
										}
										else
										{
											univDataAdapter.SelectCommand.CommandText = string.Concat(new string[]
											{
												"INSERT INTO ",
												text10,
												" (screennum,personid,controlid,controlvalue) SELECT ",
												num4.ToString(),
												" AS screennum,",
												num2.ToString(),
												" AS personid,",
												num3.ToString(),
												" AS controlid,@controlvalue AS controlvalue WHERE NOT EXISTS(SELECT dataid FROM ",
												text10,
												" WHERE screennum=",
												num4.ToString(),
												" AND personid=",
												num2.ToString(),
												" AND controlid=",
												num3.ToString(),
												")"
											});
											univDataAdapter.SelectCommand.Parameters.Clear();
											univDataAdapter.SelectCommand.Parameters.Add("@controlvalue", obj2);
											int num5 = univDataAdapter.SelectCommand.ExecuteNonQuery2();
											ReportFunction.Log(string.Concat(new string[]
											{
												j.ToString(),
												": ",
												UnivOleDbFactory.ToStringParametersExpanded(univDataAdapter.SelectCommand),
												";;;; executenonquery2=",
												num5.ToString()
											}));
											dataRow[9] = "Added (" + num5.ToString() + ")";
										}
									}
								}
								catch (Exception ex2)
								{
									dataRow[9] = "FAILED: " + ex2.ToString();
									ReportFunction.Log(j.ToString() + ": Exception: " + ex2.ToString());
								}
							}
							else
							{
								dataRow[9] = "Nothing done (NULL value)";
								ReportFunction.Log(j.ToString() + ": " + dataRow[9].ToString());
							}
						}
						univDataAdapter.Connection.Close();
						ReportFunction.Log("Writing changes to database END");
						foreach (object obj3 in arrayList2)
						{
							DataRow dataRow = (DataRow)obj3;
							dataTable.Rows.Remove(dataRow);
						}
					}
					catch (Exception ex3)
					{
						try
						{
							univDataAdapter.Connection.Close();
						}
						catch (Exception ex4)
						{
							CWLogger.Logger.Error("ImportStudents:try1:{0}", ex4.ToString());
						}
					}
				}
				else
				{
					for (int j = 0; j < dataTable.Rows.Count; j++)
					{
						DataRow dataRow = dataTable.Rows[j];
						if (flag && j % num == 0)
						{
							IncrementSubProgressBar(num);
						}
						dataRow[9] = "Nothing done (test mode)";
					}
				}
				ReportFunction.Log("Return success (rowsToImport.rows.count=" + dataTable.Rows.Count.ToString() + ")");
				result = new DataView(dataTable);
			}
			catch (Exception ex5)
			{
				if (!suppressGuiMessages)
				{
					ReportFunction.MessageBoxShow(ex5.ToString());
				}
				CWLogger.Logger.Error("ImportStudents:try2:{0}", ex5.ToString());
				result = dv;
			}
			return result;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0002B718 File Offset: 0x0002A718
		public static void Log(string s)
		{
			CWLogger.Logger.Trace(s);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0002B728 File Offset: 0x0002A728
		public static DataView CompareTwoTables(DataView dvOld, DataView dvNew, string uniqueRowColNames, string compareColNames, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataTable dataTable = new DataTable("Comparison_Results");
			DataView dataView = new DataView(dataTable);
			DataView result;
			if (dvOld == null || dvNew == null || dvNew.Count < 1 || dvOld.Count < 1)
			{
				result = dataView;
			}
			else
			{
				SetupSubProgressBar(0, dvOld.Count + dvNew.Count);
				DataTable table = dvOld.Table;
				DataTable table2 = dvNew.Table;
				int[] array;
				int[] array2;
				ReportFunction.FindColumnIndices(dvOld, dvNew, uniqueRowColNames, ref dataTable, out array, out array2);
				if (array == null || array2 == null)
				{
					result = dataView;
				}
				else
				{
					int count = dataTable.Columns.Count;
					ReportFunction.AddDataColumn(ref dataTable, "ComparisonResults");
					int[] array3;
					int[] array4;
					ReportFunction.FindColumnIndices(dvOld, dvNew, compareColNames, ref dataTable, out array3, out array4);
					if (array3 == null || array4 == null)
					{
						result = dataView;
					}
					else
					{
						int[] array5 = new int[array.Length];
						int num = 0;
						for (int i = 0; i < count; i++)
						{
							array5[num++] = i;
						}
						int[] array6 = new int[array3.Length];
						num = 0;
						for (int i = count + 1; i < dataTable.Columns.Count; i++)
						{
							array6[num++] = i;
						}
						foreach (object obj in dvOld)
						{
							DataRowView dataRowView = (DataRowView)obj;
							IncrementSubProgressBar(1);
							DataRow row = dataRowView.Row;
							string uniqueString = ReportFunction.GetUniqueString(array, row);
							DataRow dataRow = ReportFunction.FindFirstMatch(uniqueString, dvNew, array2);
							if (dataRow == null)
							{
								DataRow dataRow2 = dataTable.NewRow();
								dataRow2[count] = "DELETED";
								ReportFunction.CopyColumns(row, ref dataRow2, array, array5);
								ReportFunction.CopyColumns(row, ref dataRow2, array3, array6);
								dataTable.Rows.Add(dataRow2);
							}
							else
							{
								DataRow dataRow2 = dataTable.NewRow();
								dataRow2[count] = "MODIFIED";
								ReportFunction.CopyColumns(row, ref dataRow2, array, array5);
								int num2 = 0;
								for (int j = 0; j < array3.Length; j++)
								{
									string text = row[array3[j]].ToString().Trim().ToLower();
									string strB = dataRow[array4[j]].ToString().Trim().ToLower();
									if (text.CompareTo(strB) != 0)
									{
										dataRow2[array6[j]] = dataRow[array4[j]];
										num2++;
									}
								}
								if (num2 > 0)
								{
									dataTable.Rows.Add(dataRow2);
								}
							}
						}
						foreach (object obj2 in dvNew)
						{
							DataRowView dataRowView = (DataRowView)obj2;
							IncrementSubProgressBar(1);
							DataRow row = dataRowView.Row;
							string uniqueString = ReportFunction.GetUniqueString(array2, row);
							DataRow dataRow3 = ReportFunction.FindFirstMatch(uniqueString, dvOld, array);
							if (dataRow3 == null)
							{
								DataRow dataRow2 = dataTable.NewRow();
								dataRow2[count] = "ADDED";
								ReportFunction.CopyColumns(row, ref dataRow2, array2, array5);
								ReportFunction.CopyColumns(row, ref dataRow2, array4, array6);
								dataTable.Rows.Add(dataRow2);
							}
						}
						string newSortString = dataTable.Columns[count].ColumnName + "," + uniqueRowColNames;
						ReportFunction.SetNewSortButKeepOldSortValuesAtEndOfNewSort(ref dataView, newSortString);
						result = dataView;
					}
				}
			}
			return result;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0002BB44 File Offset: 0x0002AB44
		public static void CopyColumns(DataRow drFrom, ref DataRow drTo, int[] colIndicesFrom, int[] colIndicesTo)
		{
			Type type = Type.GetType("System.String");
			for (int i = 0; i < colIndicesFrom.Length; i++)
			{
				if (drFrom[colIndicesFrom[i]] == DBNull.Value)
				{
					drTo[colIndicesTo[i]] = DBNull.Value;
				}
				else if (drTo.Table.Columns[colIndicesTo[i]].DataType == type)
				{
					drTo[colIndicesTo[i]] = drFrom[colIndicesFrom[i]].ToString();
				}
				else
				{
					drTo[colIndicesTo[i]] = drFrom[colIndicesFrom[i]];
				}
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0002BBF4 File Offset: 0x0002ABF4
		public static void FindColumnIndices(DataView dv1, DataView dv2, string colNames, ref DataTable tableToAddColTo, out int[] indices1, out int[] indices2)
		{
			string[] array = colNames.Split(new char[]
			{
				','
			});
			if (array.Length < 1)
			{
				indices1 = null;
				indices2 = null;
			}
			else
			{
				indices1 = new int[array.Length];
				indices2 = new int[array.Length];
				DataTable table = dv1.Table;
				DataTable table2 = dv2.Table;
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i].Trim();
					int num = table.Columns.IndexOf(text);
					if (num < 0)
					{
						break;
					}
					indices1[i] = num;
					if (tableToAddColTo != null)
					{
						ReportFunction.AddDataColumn(ref tableToAddColTo, text, table.Columns[num].DataType);
					}
					num = table2.Columns.IndexOf(text);
					if (num < 0)
					{
						break;
					}
					indices2[i] = num;
				}
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0002BCF0 File Offset: 0x0002ACF0
		public static void AddDataColumn(ref DataTable t, string newColName)
		{
			ReportFunction.AddDataColumn(ref t, newColName, Type.GetType("System.String"));
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0002BD08 File Offset: 0x0002AD08
		public static void AddDataColumn(ref DataTable t, string newColName, Type newColType)
		{
			string text = newColName;
			int num = 2;
			while (t.Columns.Contains(text))
			{
				text += num.ToString();
				num++;
			}
			t.Columns.Add(text, newColType);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0002BD50 File Offset: 0x0002AD50
		public static void RemoveDuplicateRows(ref Report report, string uniqueRowColNames, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			ReportFunction.RemoveOrKeepDuplicateRows(ref report, uniqueRowColNames, true, IncrementSubProgressBar, SetupSubProgressBar, true);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0002BD5F File Offset: 0x0002AD5F
		public static void RemoveDuplicateRows(ref Report report, string uniqueRowColNames, bool leaveFirstDuplicateRow, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			ReportFunction.RemoveOrKeepDuplicateRows(ref report, uniqueRowColNames, leaveFirstDuplicateRow, IncrementSubProgressBar, SetupSubProgressBar, true);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0002BD6F File Offset: 0x0002AD6F
		public static void KeepOnlyDuplicateRows(ref Report report, string uniqueRowColNames, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			ReportFunction.RemoveOrKeepDuplicateRows(ref report, uniqueRowColNames, false, IncrementSubProgressBar, SetupSubProgressBar, false);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0002BD80 File Offset: 0x0002AD80
		private static void RemoveOrKeepDuplicateRows(ref Report report, string uniqueRowColNames, bool leaveFirstDuplicateRow, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar, bool removeRows)
		{
			DataView currentDataView = report.GetCurrentDataView();
			if (currentDataView != null && currentDataView.Table.Rows.Count >= 1)
			{
				SetupSubProgressBar(0, currentDataView.Count);
				currentDataView.Sort = uniqueRowColNames;
				DataTable table = currentDataView.Table;
				string[] array = uniqueRowColNames.Split(new char[]
				{
					','
				});
				if (array.Length >= 1)
				{
					int[] array2 = new int[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						int num = table.Columns.IndexOf(array[i].Trim());
						if (num < 0)
						{
							return;
						}
						array2[i] = num;
					}
					int j = 0;
					ArrayList arrayList = new ArrayList(currentDataView.Count);
					while (j < currentDataView.Count)
					{
						DataRow row = currentDataView[j].Row;
						int num2;
						ArrayList equivalentRows_ListIsSortedByUniqueColNames = ReportFunction.GetEquivalentRows_ListIsSortedByUniqueColNames(currentDataView, j, array2, out num2);
						if (equivalentRows_ListIsSortedByUniqueColNames.Count > 0 && !leaveFirstDuplicateRow)
						{
							arrayList.Add(row);
						}
						foreach (object obj in equivalentRows_ListIsSortedByUniqueColNames)
						{
							DataRow dataRow = (DataRow)obj;
							arrayList.Add(dataRow);
						}
						int num3 = num2 - j;
						if (num3 > 0)
						{
							for (int k = 0; k < num3; k++)
							{
								IncrementSubProgressBar(1);
							}
							j = num2;
						}
						else
						{
							j++;
							IncrementSubProgressBar(1);
						}
					}
					if (removeRows)
					{
						foreach (object obj2 in arrayList)
						{
							DataRow dataRow = (DataRow)obj2;
							table.Rows.Remove(dataRow);
						}
					}
					else
					{
						DataTable dataTable = table.Clone();
						foreach (object obj3 in arrayList)
						{
							DataRow dataRow = (DataRow)obj3;
							dataTable.ImportRow(dataRow);
						}
						string sort = currentDataView.Sort;
						DataView dataView = new DataView(dataTable);
						dataView.Sort = sort;
						report.ReplaceDataView(currentDataView, dataView);
					}
				}
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0002C06C File Offset: 0x0002B06C
		public static void MapCellsToColumns(ref Report report, string columnNameColName, string columnValueColName, string uniqueColumnNames, DataTable dynamicControlsTable, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			ReportFunction.MapCellsToColumns(null, -1, ref report, columnNameColName, columnValueColName, uniqueColumnNames, dynamicControlsTable, IncrementSubProgressBar, SetupSubProgressBar);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0002C08C File Offset: 0x0002B08C
		public static void MapCellsToColumns(UnivDataAdapter da, int screenNum, ref Report report, string columnNameColName, string columnValueColName, string uniqueColumnNames, DataTable DynamicControlsTable, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			if (currentDataView != null && currentDataView.Table.Rows.Count >= 1)
			{
				DataTable dataTable = DynamicControlsTable;
				if (dataTable == null)
				{
					dataTable = new DataTable();
					if (screenNum >= 0)
					{
						da.SelectCommand.CommandText = "SELECT dsc.controlid,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3 FROM dynamicscreencontrols dsc LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid WHERE dsc.screennum=@screennum AND NOT dc.controlcode IN (SELECT controlcode FROM dynamicscreennondatacontrols)";
						da.SelectCommand.Parameters.Clear();
						da.SelectCommand.Parameters.Add("@screennum", screenNum);
						da.Fill(dataTable);
					}
					else
					{
						da.SelectCommand.CommandText = "SELECT dsc.controlid,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3 FROM dynamiccontrols dc LEFT JOIN  dynamicscreencontrols dsc ON dsc.controlid=dc.controlid WHERE dc.controlcode=@cc AND dc.controlid IN (SELECT controlid FROM dynamicscreencontrols)";
						da.SelectCommand.Parameters.Clear();
						da.SelectCommand.Parameters.Add("@cc", 10);
						da.Fill(dataTable);
					}
				}
				Type type = Type.GetType("System.Boolean");
				Type type2 = Type.GetType("System.DateTime");
				Type type3 = Type.GetType("System.String");
				Type type4 = Type.GetType("System.Int32");
				DataTable table = currentDataView.Table;
				int num = table.Columns.IndexOf(columnNameColName);
				int num2 = table.Columns.IndexOf(columnValueColName);
				if (num >= 0 && num2 >= 0)
				{
					DataTable dataTable2 = table.Clone();
					dataTable2.Columns.Remove(columnNameColName);
					dataTable2.Columns.Remove(columnValueColName);
					int count = dataTable2.Columns.Count;
					int[] array = new int[count];
					int num3 = 0;
					for (int i = 0; i < count + 2; i++)
					{
						if (i != num && i != num2)
						{
							array[num3++] = i;
						}
					}
					int num4 = 0;
					int lookupGroupID = -1;
					bool flag = false;
					for (int j = 0; j < currentDataView.Count; j++)
					{
						DataRowView dataRowView = currentDataView[j];
						DataRow row = dataRowView.Row;
						string text = row[num].ToString().Trim();
						if (text.Length < 1)
						{
							text = "_x";
						}
						int num5 = dataTable2.Columns.IndexOf(text);
						if (num5 >= 0 && num5 < count)
						{
							DataColumn dataColumn = dataTable2.Columns[num5];
							dataColumn.ColumnName += num4.ToString();
							num4++;
							num5 = -1;
						}
						if (num5 < 0)
						{
							flag = false;
							DataRow dataRow = null;
							foreach (object obj in dataTable.Rows)
							{
								DataRow dataRow2 = (DataRow)obj;
								string text2 = dataRow2[2].ToString().Trim();
								if (text2.IndexOf(text) == 0)
								{
									dataRow = dataRow2;
									break;
								}
							}
							if (dataRow != null)
							{
								int num6 = (int)dataRow[1];
								lookupGroupID = (int)dataRow["setting1"];
								int num7 = num6;
								switch (num7)
								{
								case 2:
								case 4:
									dataTable2.Columns.Add(text, type);
									break;
								case 3:
								case 5:
									goto IL_3A9;
								case 6:
									dataTable2.Columns.Add(text, type2);
									break;
								default:
									if (num7 != 10)
									{
										goto IL_3A9;
									}
									dataTable2.Columns.Add(text);
									flag = (da != null);
									break;
								}
								IL_3BA:
								dataTable.Rows.Remove(dataRow);
								goto IL_3DA;
								IL_3A9:
								dataTable2.Columns.Add(text);
								goto IL_3BA;
							}
							dataTable2.Columns.Add(text);
							IL_3DA:
							num5 = dataTable2.Columns.Count - 1;
							if (flag)
							{
								DataSet dataSet = new DataSet();
								DataTable lookupList = DynamicScreen.GetLookupList(lookupGroupID, false, -1, ref dataSet, da, false);
								dataTable2.Columns.Add("date_" + text);
								for (int k = 0; k < lookupList.Rows.Count; k++)
								{
									dataTable2.Columns.Add(lookupList.Rows[k]["lookuptext"].ToString());
								}
							}
						}
						DataRow dataRow3 = dataTable2.NewRow();
						for (int i = 0; i < count; i++)
						{
							dataRow3[i] = row[array[i]];
						}
						Type dataType = dataTable2.Columns[num5].DataType;
						if (row[num2] != DBNull.Value)
						{
							if (flag && dataTable2.Columns.Count > num5 + 1)
							{
								string text3 = row[num2].ToString();
								string text4 = text3;
								string[] array2 = text3.Split(new char[]
								{
									'|'
								});
								string text5 = array2[array2.Length - 1].Trim();
								int num8 = (int)row["personid"];
								int l;
								for (l = j + 1; l < currentDataView.Count; l++)
								{
									DataRowView dataRowView2 = currentDataView[l];
									DataRow row2 = dataRowView2.Row;
									int num9 = (int)row2["personid"];
									if (num9 != num8)
									{
										break;
									}
									text3 = row2[num2].ToString();
									array2 = text3.Split(new char[]
									{
										'|'
									});
									text4 += ((text4.Length > 0) ? ", " : (text3 ?? ""));
									string text6 = array2[array2.Length - 1].Trim();
									if (text6.CompareTo(text5) > 0)
									{
										text5 = text6;
									}
								}
								dataRow3[num5] = text4;
								int num10 = dataTable2.Columns.Count - num5 - 1;
								int k = 0;
								while (k < num10 && k < array2.Length)
								{
									string value = (k == 0) ? text5 : array2[k].Trim().Replace('`', ',').Replace(" ~ ", " | ");
									dataRow3[num5 + 1 + k] = value;
									k++;
								}
								j = l - 1;
							}
							else if (dataType == type)
							{
								string text7 = row[num2].ToString().Trim().ToLower();
								if (text7.Length > 0)
								{
									char c = text7[0];
									dataRow3[num5] = (c == 'y' || c == 't' || c == '0');
								}
							}
							else if (dataType == type2)
							{
								string text8 = row[num2].ToString().Trim();
								if (text8.Length > 0)
								{
									try
									{
										dataRow3[num5] = Convert.ToDateTime(text8);
									}
									catch (Exception ex)
									{
										ReportFunction.MessageBoxShow(ex.ToString() + " (" + text8 + ")");
									}
								}
							}
							else
							{
								dataRow3[num5] = row[num2].ToString();
							}
						}
						dataTable2.Rows.Add(dataRow3);
					}
					if (uniqueColumnNames.Length > 0)
					{
						DataView dvToKeep = new DataView(dataTable2);
						ReportFunction.SetNewSortButKeepOldSortValuesAtEndOfNewSort(ref dvToKeep, uniqueColumnNames);
						report.ReplaceDataView(currentDataView, dvToKeep);
						ReportFunction.MergeRowsAlreadySortedByUniqueColumnNames(ref report, uniqueColumnNames, "", IncrementSubProgressBar, SetupSubProgressBar, false);
					}
					else
					{
						report.ReplaceDataView(currentDataView, dataTable2.DefaultView);
					}
				}
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0002C904 File Offset: 0x0002B904
		public static void SetNewSortButKeepOldSortValuesAtEndOfNewSort(ref DataView dv, string newSortString)
		{
			string sort = dv.Sort;
			string text = newSortString;
			if (sort.Length > 0)
			{
				string[] array = newSortString.Split(new char[]
				{
					','
				});
				string[] array2 = sort.Split(new char[]
				{
					','
				});
				string text2 = "";
				foreach (string text3 in array2)
				{
					if (dv.Table.Columns.Contains(text3))
					{
						string text4 = text3.Trim().ToLower();
						bool flag = false;
						foreach (string text5 in array)
						{
							string strB = text5.Trim().ToLower();
							if (text4.CompareTo(strB) == 0)
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							if (text2.Length > 0)
							{
								text2 += ",";
							}
							text2 += text3.Trim();
						}
					}
				}
				if (text2.Length > 0)
				{
					text = text + "," + text2;
				}
			}
			try
			{
				dv.Sort = text;
			}
			catch (Exception ex)
			{
				ReportFunction.MessageBoxShow(ex.ToString());
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0002CAA4 File Offset: 0x0002BAA4
		public static DataView PullDataIntoOneTableFromAnother(DataView dvKeep0, DataView dvLookup, string matchingColumnNames, string optionalColumnNamesInLookupToPullIn, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView result;
			if (matchingColumnNames.Length < 1 || dvKeep0 == null || dvKeep0.Table.Rows.Count < 1 || dvLookup == null || dvLookup.Table.Rows.Count < 1)
			{
				result = dvKeep0;
			}
			else
			{
				DataTable dataTable = dvKeep0.Table.Copy();
				DataView dataView = new DataView(dataTable);
				dataView.Sort = matchingColumnNames;
				DataTable table = dvLookup.Table;
				string[] array = matchingColumnNames.Split(new char[]
				{
					','
				});
				ArrayList arrayList = new ArrayList(array.Length);
				ArrayList arrayList2 = new ArrayList(array.Length);
				for (int i = 0; i < array.Length; i++)
				{
					int num = dataTable.Columns.IndexOf(array[i].Trim());
					int num2 = table.Columns.IndexOf(array[i].Trim());
					if (num >= 0 && num2 >= 0)
					{
						arrayList.Add(num);
						arrayList2.Add(num2);
					}
				}
				if (arrayList.Count < 1)
				{
					result = dataView;
				}
				else
				{
					int[] array2 = new int[arrayList.Count];
					int[] array3 = new int[arrayList.Count];
					for (int i = 0; i < arrayList.Count; i++)
					{
						array2[i] = (int)arrayList[i];
						array3[i] = (int)arrayList2[i];
					}
					ArrayList arrayList3;
					if (optionalColumnNamesInLookupToPullIn.Length > 0)
					{
						string[] array4 = optionalColumnNamesInLookupToPullIn.Split(new char[]
						{
							','
						});
						arrayList3 = new ArrayList(array4.Length);
						for (int i = 0; i < array4.Length; i++)
						{
							int num3 = table.Columns.IndexOf(array4[i].Trim());
							if (num3 >= 0)
							{
								arrayList3.Add(num3);
							}
						}
					}
					else
					{
						arrayList3 = new ArrayList(table.Columns.Count);
						for (int i = 0; i < table.Columns.Count; i++)
						{
							int num4 = dataTable.Columns.IndexOf(table.Columns[i].ColumnName);
							if (num4 < 0)
							{
								arrayList3.Add(i);
							}
						}
					}
					if (arrayList3.Count < 1)
					{
						result = dataView;
					}
					else
					{
						int[] array5 = new int[arrayList3.Count];
						int[] array6 = new int[arrayList3.Count];
						for (int i = 0; i < arrayList3.Count; i++)
						{
							array5[i] = (int)arrayList3[i];
							int num5 = dataTable.Columns.IndexOf(table.Columns[array5[i]].ColumnName);
							if (num5 < 0)
							{
								dataTable.Columns.Add(table.Columns[array5[i]].ColumnName, table.Columns[array5[i]].DataType);
								array6[i] = dataTable.Columns.Count - 1;
							}
							else
							{
								array6[i] = num5;
							}
						}
						SetupSubProgressBar(0, dataView.Count);
						for (int i = 0; i < dataView.Count; i++)
						{
							IncrementSubProgressBar(1);
							DataRowView dataRowView = dataView[i];
							DataRow row = dataRowView.Row;
							string uniqueString = ReportFunction.GetUniqueString(array2, row);
							DataRow dataRow = ReportFunction.FindFirstMatch(uniqueString, dvLookup, array3);
							if (dataRow != null)
							{
								for (int j = 0; j < array5.Length; j++)
								{
									row[array6[j]] = dataRow[array5[j]];
								}
							}
						}
						result = dataView;
					}
				}
			}
			return result;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0002CECC File Offset: 0x0002BECC
		public static string GetUniqueString(int[] colIndices, DataRow dr)
		{
			string text = "";
			for (int i = 0; i < colIndices.Length; i++)
			{
				text = text + i.ToString() + dr[colIndices[i]].ToString().Trim().ToLower();
			}
			return text;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0002CF20 File Offset: 0x0002BF20
		public static DataRow FindFirstMatch(string uniqueString, DataView dv, int[] uniqueColIndices)
		{
			foreach (object obj in dv)
			{
				DataRowView dataRowView = (DataRowView)obj;
				DataRow row = dataRowView.Row;
				string uniqueString2 = ReportFunction.GetUniqueString(uniqueColIndices, row);
				if (uniqueString2.CompareTo(uniqueString) == 0)
				{
					return row;
				}
			}
			return null;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0002CFB4 File Offset: 0x0002BFB4
		public static void MergeRows(ref Report report, string uniqueColumnNames, string colNameValueAndList, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			if (currentDataView != null && currentDataView.Table != null && currentDataView.Table.Columns.Count >= 1 && currentDataView.Table.Rows.Count >= 1)
			{
				ReportFunction.SetNewSortButKeepOldSortValuesAtEndOfNewSort(ref currentDataView, uniqueColumnNames);
				ReportFunction.MergeRowsAlreadySortedByUniqueColumnNames(ref report, uniqueColumnNames, colNameValueAndList, IncrementSubProgressBar, SetupSubProgressBar, false);
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0002D01C File Offset: 0x0002C01C
		public static void MergeRowsExcludeDuplicatesInCommaSeparatedList(ref Report report, string uniqueColumnNames, string colNameValueAndList, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			ReportFunction.SetNewSortButKeepOldSortValuesAtEndOfNewSort(ref currentDataView, uniqueColumnNames);
			ReportFunction.MergeRowsAlreadySortedByUniqueColumnNames(ref report, uniqueColumnNames, colNameValueAndList, IncrementSubProgressBar, SetupSubProgressBar, true);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0002D048 File Offset: 0x0002C048
		public static DataView MergeTable2IntoTable1(DataView dv1, DataView dv2, string uniqueColsStr, string externalColsStr, string externalTableRenameColsStr, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			string[] array = uniqueColsStr.Split(new char[]
			{
				','
			});
			string[] array2 = externalColsStr.Split(new char[]
			{
				','
			});
			string[] array3 = externalTableRenameColsStr.Split(new char[]
			{
				','
			});
			if (externalTableRenameColsStr.Length > 0)
			{
				foreach (string text in array3)
				{
					string[] array5 = text.Split(new char[]
					{
						'='
					});
					if (array5.Length == 2)
					{
						string name = array5[0];
						string columnName = array5[1];
						if (dv2.Table.Columns.Contains(name))
						{
							dv2.Table.Columns[name].ColumnName = columnName;
						}
					}
				}
			}
			DataView result;
			if (array.Length < 1 || array2.Length < 1 || dv1 == null || dv2 == null || dv2.Table.Rows.Count < 1 || dv1.Table.Rows.Count < 1)
			{
				result = dv1;
			}
			else
			{
				DataTable table = dv1.Table.Clone();
				DataView dataView = new DataView(table);
				dataView.RowFilter = dv1.RowFilter;
				dataView.Sort = dv1.Sort;
				SetupSubProgressBar(0, dataView.Count + array2.Length + 1);
				ArrayList arrayList = new ArrayList(array2.Length);
				ArrayList arrayList2 = new ArrayList(array2.Length);
				for (int j = 0; j < array2.Length; j++)
				{
					IncrementSubProgressBar(1);
					string text2 = array2[j].Trim();
					int num = dv2.Table.Columns.IndexOf(text2);
					if (num >= 0)
					{
						arrayList.Add(num);
						if (dataView.Table.Columns.Contains(text2))
						{
							arrayList2.Add(dataView.Table.Columns.IndexOf(text2));
						}
						else
						{
							arrayList2.Add(dataView.Table.Columns.Count);
							dataView.Table.Columns.Add("ext_" + text2, dv2.Table.Columns[num].DataType);
						}
					}
				}
				if (SetupSubProgressBar != null)
				{
					SetupSubProgressBar(0, dv1.Table.Rows.Count + 1);
				}
				for (int j = 0; j < dv1.Table.Rows.Count; j++)
				{
					IncrementSubProgressBar(1);
					DataRow dataRow = dv1.Table.Rows[j];
					bool flag = false;
					for (int k = 0; k < dv2.Table.Rows.Count; k++)
					{
						DataRow dataRow2 = dv2.Table.Rows[k];
						bool flag2 = true;
						for (int l = 0; l < array.Length; l++)
						{
							if (dataRow[array[l]] == DBNull.Value || dataRow2[array[l]] == DBNull.Value)
							{
								if (dataRow[array[l]] != DBNull.Value || dataRow2[array[l]] != DBNull.Value)
								{
									flag2 = false;
									break;
								}
							}
							else
							{
								string text3 = dataRow[array[l]].ToString().Trim().ToLower();
								string strB = dataRow2[array[l]].ToString().Trim().ToLower();
								if (text3.CompareTo(strB) != 0)
								{
									flag2 = false;
									break;
								}
							}
						}
						if (flag2)
						{
							flag = true;
							DataRow dataRow3 = dataView.Table.NewRow();
							for (int m = 0; m < dv1.Table.Columns.Count; m++)
							{
								dataRow3[m] = dataRow[m];
							}
							for (int m = 0; m < arrayList.Count; m++)
							{
								dataRow3[(int)arrayList2[m]] = dataRow2[(int)arrayList[m]];
							}
							dataView.Table.Rows.Add(dataRow3);
						}
					}
					if (!flag)
					{
						DataRow dataRow3 = dataView.Table.NewRow();
						for (int m = 0; m < dv1.Table.Columns.Count; m++)
						{
							dataRow3[m] = dataRow[m];
						}
						dataView.Table.Rows.Add(dataRow3);
					}
				}
				result = dataView;
			}
			return result;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0002D5A8 File Offset: 0x0002C5A8
		public static void BreakdownCounts(ref Report report, string[] uniqueColNames, bool mergeUniqueColNamesData, string[] foreachColNames, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			ArrayList[] array = new ArrayList[foreachColNames.Length];
			if (mergeUniqueColNamesData)
			{
				ArrayList uniqueValuesInColumns = ReportFunction.GetUniqueValuesInColumns(currentDataView.Table, foreachColNames, false);
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = uniqueValuesInColumns;
				}
			}
			else
			{
				for (int i = 0; i < foreachColNames.Length; i++)
				{
					array[i] = ReportFunction.GetUniqueValuesInColumns(currentDataView.Table, new string[]
					{
						foreachColNames[i]
					}, false);
				}
			}
			ReportFunction.BreakdownCounts(ref report, uniqueColNames, mergeUniqueColNamesData, foreachColNames, array, IncrementSubProgressBar, SetupSubProgressBar);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0002D644 File Offset: 0x0002C644
		public static ArrayList GetUniqueValuesInColumns(DataTable t, string[] colNames, bool includeBlanks)
		{
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			foreach (object obj in t.Rows)
			{
				DataRow dr = (DataRow)obj;
				string text = ReportFunction.MergeColValues(dr, colNames);
				if (text.Length > 0 || includeBlanks)
				{
					string text2 = text.ToLower();
					if (!arrayList.Contains(text2))
					{
						arrayList.Add(text2);
						arrayList2.Add(text);
					}
				}
			}
			arrayList2.Sort();
			return arrayList2;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0002D714 File Offset: 0x0002C714
		public static void BreakdownCounts(ref Report report, string[] uniqueColNames, bool mergeUniqueColNamesData, string[] foreachColNames, ArrayList[] foreachColNamesUniqueValues, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			Type typeFromHandle = typeof(int);
			Type typeFromHandle2 = typeof(double);
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("field");
			dataTable.Columns.Add("fieldvalue");
			for (int i = 0; i < foreachColNames.Length; i++)
			{
				ArrayList arrayList = foreachColNamesUniqueValues[i];
				for (int j = 0; j < arrayList.Count; j++)
				{
					string text = arrayList[j].ToString();
					if (j == 0)
					{
						dataTable.Rows.Add(new object[]
						{
							foreachColNames[i],
							text
						});
					}
					else
					{
						dataTable.Rows.Add(new object[]
						{
							"",
							text
						});
					}
				}
			}
			if (uniqueColNames.Length > 1 && !mergeUniqueColNamesData)
			{
				for (int k = 0; k < uniqueColNames.Length; k++)
				{
					string text2 = uniqueColNames[k];
					Report report2 = new Report(currentDataView);
					ReportFunction.BreakdownCounts(ref report2, new string[]
					{
						text2
					}, true, foreachColNames, new ArrayList[]
					{
						foreachColNamesUniqueValues[k]
					}, IncrementSubProgressBar, SetupSubProgressBar);
					DataView currentDataView2 = report2.GetCurrentDataView();
					if (currentDataView2 != null)
					{
						DataTable table = currentDataView2.Table;
						int count = dataTable.Columns.Count;
						dataTable.Columns.Add(text2 + "_count", typeFromHandle);
						dataTable.Columns.Add(text2 + "_%_of_total", typeFromHandle2);
						dataTable.Columns.Add("allblanks_count", typeFromHandle);
						dataTable.Columns.Add("multiple_count", typeFromHandle);
						dataTable.Columns.Add("total_count", typeFromHandle);
						for (int l = 0; l < foreachColNames.Length; l++)
						{
							ArrayList arrayList2 = foreachColNamesUniqueValues[l];
							for (int m = 0; m < arrayList2.Count; m++)
							{
								for (int n = 2; n < table.Columns.Count; n++)
								{
									dataTable.Rows[l * arrayList2.Count + m][count + n - 2] = table.Rows[l * arrayList2.Count + m][n];
								}
							}
						}
					}
				}
			}
			else
			{
				DataView dataView = new DataView(currentDataView.Table);
				dataView.Sort = ReportFunction.GetArrayCommaSeparated(uniqueColNames);
				int[][] array = new int[foreachColNames.Length][];
				ArrayList[][] array2 = new ArrayList[foreachColNames.Length][];
				for (int k = 0; k < foreachColNames.Length; k++)
				{
					array[k] = new int[foreachColNamesUniqueValues[k].Count];
					for (int i = 0; i < array[k].Length; i++)
					{
						array[k][i] = 0;
					}
					array2[k] = new ArrayList[foreachColNamesUniqueValues[k].Count];
					for (int i = 0; i < array[k].Length; i++)
					{
						array2[k][i] = new ArrayList();
					}
				}
				ArrayList arrayList3 = new ArrayList();
				foreach (object obj in dataView)
				{
					DataRowView dataRowView = (DataRowView)obj;
					DataRow row = dataRowView.Row;
					string text3 = ReportFunction.MergeColValues(row, uniqueColNames);
					string text4 = text3.ToLower();
					if (!arrayList3.Contains(text4))
					{
						arrayList3.Add(text4);
					}
					for (int k = 0; k < foreachColNames.Length; k++)
					{
						string strB = row[foreachColNames[k]].ToString().Trim().ToLower();
						ArrayList arrayList = foreachColNamesUniqueValues[k];
						for (int i = 0; i < arrayList.Count; i++)
						{
							string text5 = arrayList[i].ToString().Trim().ToLower();
							if (text5.CompareTo(strB) == 0)
							{
								if (!array2[k][i].Contains(text4))
								{
									array2[k][i].Add(text4);
									array[k][i]++;
								}
								break;
							}
						}
					}
				}
				string text2 = ReportFunction.GetArrayCommaSeparated(uniqueColNames);
				dataTable.Columns.Add(text2 + "_count", typeFromHandle);
				dataTable.Columns.Add(text2 + "_%_of_total", typeFromHandle2);
				dataTable.Columns.Add("allblanks_count", typeFromHandle);
				dataTable.Columns.Add("multiple_count", typeFromHandle);
				dataTable.Columns.Add("total_count", typeFromHandle);
				int num = 0;
				int count2 = arrayList3.Count;
				for (int k = 0; k < foreachColNames.Length; k++)
				{
					foreach (int num2 in array[k])
					{
						dataTable.Rows[num][2] = num2;
						dataTable.Rows[num][3] = ((count2 > 0) ? Math.Round(Convert.ToDouble(num2) / Convert.ToDouble(count2) * 100.0, 2) : 0.0);
						dataTable.Rows[num][4] = DBNull.Value;
						dataTable.Rows[num][5] = DBNull.Value;
						dataTable.Rows[num][6] = DBNull.Value;
						num++;
					}
				}
				int num3 = 0;
				int num4 = 0;
				for (int k = 0; k < arrayList3.Count; k++)
				{
					string item = arrayList3[k].ToString();
					bool flag = true;
					int num5 = 0;
					for (int i = 0; i < foreachColNamesUniqueValues.Length; i++)
					{
						ArrayList arrayList = foreachColNamesUniqueValues[i];
						for (int l = 0; l < arrayList.Count; l++)
						{
							if (array2[i][l].Contains(item))
							{
								num5++;
								flag = false;
							}
						}
					}
					if (flag)
					{
						num3++;
					}
					else if (num5 > 1)
					{
						num4++;
					}
				}
				dataTable.Rows.Add(new object[]
				{
					"Total",
					"",
					null,
					null,
					num3,
					num4,
					count2
				});
			}
			report.AddResult(dataTable.DefaultView);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0002DEBC File Offset: 0x0002CEBC
		private static string MergeColValues(DataRow dr, string[] colNames)
		{
			string text = "";
			for (int i = 0; i < colNames.Length; i++)
			{
				text += dr[colNames[i]].ToString().Trim();
			}
			return text;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0002DF04 File Offset: 0x0002CF04
		public static void MergeRowsAlreadySortedByUniqueColumnNames(ref Report report, string uniqueColumnNames, string colNameValueAndList, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar, bool dontIncludeDuplicatesInCommaSeparatedList)
		{
			DataView currentDataView = report.GetCurrentDataView();
			if (currentDataView != null && currentDataView.Table.Rows.Count >= 1)
			{
				DataTable table = currentDataView.Table;
				string[] array = uniqueColumnNames.Split(new char[]
				{
					','
				});
				if (array.Length >= 1)
				{
					int[] array2 = new int[array.Length];
					for (int i = 0; i < array2.Length; i++)
					{
						array2[i] = table.Columns.IndexOf(array[i]);
						if (array2[i] < 0)
						{
							return;
						}
					}
					int[] array4;
					string[] array5;
					if (colNameValueAndList.Length > 0)
					{
						string[] array3 = colNameValueAndList.Split(new char[]
						{
							','
						});
						if (array3.Length > 0)
						{
							array4 = new int[array3.Length];
							array5 = new string[array3.Length];
							int num = 0;
							for (int j = 0; j < array3.Length; j++)
							{
								string text = array3[j];
								string[] array6 = text.Split(new char[]
								{
									'='
								});
								bool flag = false;
								if (array6.Length == 2)
								{
									string columnName = array6[0].Trim();
									int num2 = table.Columns.IndexOf(columnName);
									if (num2 >= 0)
									{
										array4[j] = num2;
										array5[j] = array6[1].Trim().ToLower();
										flag = true;
									}
								}
								if (!flag)
								{
									array4[j] = -1;
									array5[j] = "";
									num++;
								}
							}
							if (num > 0)
							{
								int num3 = array4.Length - num;
								if (num3 > 0)
								{
									int[] array7 = new int[num3];
									string[] array8 = new string[num3];
									int k = 0;
									for (int j = 0; j < array4.Length; j++)
									{
										int num2 = array4[j];
										if (num2 >= 0)
										{
											array7[k++] = num2;
											array8[k] = array5[j];
										}
									}
									array4 = null;
									array4 = array7;
									array5 = null;
									array5 = array8;
								}
								else
								{
									array4 = null;
									array5 = null;
								}
							}
						}
						else
						{
							array4 = null;
							array5 = null;
						}
					}
					else
					{
						array4 = null;
						array5 = null;
					}
					Type type = Type.GetType("System.Boolean");
					Type type2 = Type.GetType("System.DateTime");
					DataTable dataTable = new DataTable();
					for (int i = 0; i < table.Columns.Count; i++)
					{
						Type dataType = table.Columns[i].DataType;
						if (dataType == type)
						{
							dataTable.Columns.Add(table.Columns[i].ColumnName, type);
						}
						else if (dataType == type2)
						{
							dataTable.Columns.Add(table.Columns[i].ColumnName, type2);
						}
						else if (dataType == typeof(int))
						{
							bool flag2 = false;
							for (int l = 0; l < array2.Length; l++)
							{
								if (array2[l] == i)
								{
									flag2 = true;
									break;
								}
							}
							if (flag2)
							{
								dataTable.Columns.Add(table.Columns[i].ColumnName, typeof(int));
							}
							else
							{
								dataTable.Columns.Add(table.Columns[i].ColumnName);
							}
						}
						else
						{
							dataTable.Columns.Add(table.Columns[i].ColumnName);
						}
					}
					int m = 0;
					ArrayList arrayList = new ArrayList(currentDataView.Count);
					ArrayList arrayList2 = new ArrayList();
					if (SetupSubProgressBar != null)
					{
						SetupSubProgressBar(0, currentDataView.Count + 1);
					}
					int num4 = 0;
					while (m < currentDataView.Count)
					{
						if (IncrementSubProgressBar != null && num4++ % 100 == 0)
						{
							IncrementSubProgressBar(100);
						}
						DataRowView dataRowView = currentDataView[m];
						DataRow dataRow = dataRowView.Row;
						bool flag3;
						if (array4 != null)
						{
							flag3 = true;
							for (int k = 0; k < array4.Length; k++)
							{
								string text2 = dataRow[array4[k]].ToString().Trim().ToLower();
								if (text2.CompareTo(array5[k]) != 0)
								{
									flag3 = false;
									break;
								}
							}
						}
						else
						{
							flag3 = false;
						}
						if (flag3)
						{
							arrayList2.Add(dataRow);
							m++;
						}
						else
						{
							dataRow = ReportFunction.LoadDataRowMaybeHasDifferentColDataTypes(dataTable, dataRow);
							int num5;
							ArrayList equivalentRows_ListIsSortedByUniqueColNames = ReportFunction.GetEquivalentRows_ListIsSortedByUniqueColNames(currentDataView, m, array2, out num5);
							if (array4 != null)
							{
								ArrayList arrayList3 = new ArrayList();
								foreach (object obj in equivalentRows_ListIsSortedByUniqueColNames)
								{
									DataRow dataRow2 = (DataRow)obj;
									for (int k = 0; k < array4.Length; k++)
									{
										string text2 = dataRow2[array4[k]].ToString().Trim().ToLower();
										if (text2.CompareTo(array5[k]) != 0)
										{
											break;
										}
									}
								}
								foreach (object obj2 in arrayList3)
								{
									DataRow dataRow2 = (DataRow)obj2;
									equivalentRows_ListIsSortedByUniqueColNames.Remove(dataRow2);
									arrayList2.Add(dataRow2);
								}
							}
							m = num5;
							for (int k = 0; k < equivalentRows_ListIsSortedByUniqueColNames.Count; k++)
							{
								DataRow dataRow3 = (DataRow)equivalentRows_ListIsSortedByUniqueColNames[k];
								for (int i = 0; i < dataTable.Columns.Count; i++)
								{
									bool flag2 = false;
									for (int n = 0; n < array2.Length; n++)
									{
										if (array2[n] == i)
										{
											flag2 = true;
											break;
										}
									}
									if (!flag2)
									{
										if (dataRow[i] == DBNull.Value || dataRow3[i] == DBNull.Value)
										{
											if (dataRow3[i] != DBNull.Value)
											{
												dataRow[i] = dataRow3[i];
											}
											string text3 = dataRow3[i].ToString();
											if (text3.CompareTo("True") == 0)
											{
												text3 += ".";
											}
										}
										else
										{
											Type dataType2 = dataTable.Columns[i].DataType;
											bool flag4 = false;
											if (dataType2 == type2)
											{
												DateTime d = (DateTime)dataRow3[i];
												DateTime d2 = (DateTime)dataRow[i];
												if (d == d2)
												{
													flag4 = true;
												}
												else
												{
													dataTable = ReportFunction.ChangeColumnToStringDataType(dataTable, i);
													dataRow = dataTable.Rows[dataTable.Rows.Count - 1];
												}
											}
											else if (dataType2 == type)
											{
												bool flag5 = (bool)dataRow3[i];
												bool flag6 = (bool)dataRow[i];
												if (flag5 == flag6)
												{
													flag4 = true;
												}
												else
												{
													dataTable = ReportFunction.ChangeColumnToStringDataType(dataTable, i);
													dataRow = dataTable.Rows[dataTable.Rows.Count - 1];
												}
											}
											if (!flag4)
											{
												string text4 = dataRow3[i].ToString().Trim();
												string text5 = dataRow[i].ToString().Trim();
												if (text4.Length > 0 && (dontIncludeDuplicatesInCommaSeparatedList || dataTable.Columns[i].ColumnName.ToLower().CompareTo("personid") == 0))
												{
													string[] array9 = text5.Split(new char[]
													{
														','
													});
													if (Array.IndexOf<string>(array9, text4) >= 0)
													{
														text4 = "";
													}
												}
												if (text4.Length > 0)
												{
													if (text5.Length > 0)
													{
														text5 += ", ";
													}
													text5 += text4;
													dataRow[i] = text5;
												}
											}
										}
									}
								}
							}
						}
					}
					foreach (object obj3 in arrayList2)
					{
						DataRow dataRow3 = (DataRow)obj3;
						ReportFunction.LoadDataRowMaybeHasDifferentColDataTypes(dataTable, dataRow3);
					}
					report.ReplaceDataView(currentDataView, dataTable.DefaultView);
				}
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0002E904 File Offset: 0x0002D904
		private static DataRow LoadDataRowMaybeHasDifferentColDataTypes(DataTable t, DataRow dr)
		{
			DataTable table = dr.Table;
			DataRow dataRow = t.NewRow();
			for (int i = 0; i < t.Columns.Count; i++)
			{
				Type dataType = t.Columns[i].DataType;
				Type dataType2 = table.Columns[i].DataType;
				if (dataType != dataType2)
				{
					dataRow[i] = dr[i].ToString();
				}
				else
				{
					dataRow[i] = dr[i];
				}
			}
			t.Rows.Add(dataRow);
			return dataRow;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0002E9AC File Offset: 0x0002D9AC
		private static void ChangeColumnDataTypes(ref DataView dv, string newColDataTypeInfo, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			Type type = Type.GetType("System.Int32");
			Type type2 = Type.GetType("System.String");
			Type type3 = Type.GetType("System.Boolean");
			Type type4 = Type.GetType("System.DateTime");
			string[] array = newColDataTypeInfo.Split(new char[]
			{
				'`'
			});
			if (array.Length >= 1)
			{
				int[] array2 = new int[array.Length];
				int[] array3 = new int[array.Length];
				Type[] array4 = new Type[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					string[] array5 = text.Split(new char[]
					{
						','
					});
					array2[i] = -1;
					array3[i] = -1;
					array4[i] = null;
					if (array5.Length > 0)
					{
						string columnName = array5[0].Trim();
						string text2;
						if (array5.Length > 1)
						{
							text2 = array5[1].Trim().ToLower();
						}
						else
						{
							text2 = "string";
						}
						int num = dv.Table.Columns.IndexOf(columnName);
						if (num >= 0)
						{
							Type type5;
							if (text2.CompareTo("bool") == 0)
							{
								type5 = type3;
							}
							else if (text2.CompareTo("int") == 0)
							{
								type5 = type;
							}
							else if (text2.CompareTo("datetime") == 0)
							{
								type5 = type4;
							}
							else
							{
								type5 = type2;
							}
							Type dataType = dv.Table.Columns[num].DataType;
							if (dataType != type5)
							{
								DataColumn dataColumn = dv.Table.Columns[num];
								dataColumn.ColumnName += "_old";
								DataColumn dataColumn2 = dv.Table.Columns.Add(columnName, type5);
								array3[i] = num;
								array2[i] = dataColumn2.Ordinal;
								array4[i] = type5;
							}
						}
					}
				}
				SetupSubProgressBar(0, dv.Count);
				for (int i = 0; i < dv.Count; i++)
				{
					IncrementSubProgressBar(1);
					DataRow row = dv[i].Row;
					for (int j = 0; j < array3.Length; j++)
					{
						object obj = row[array3[j]];
						if (obj != null && obj != DBNull.Value)
						{
							Type dataType = dv.Table.Columns[array3[j]].DataType;
							Type type5 = array4[j];
							if (type5 == type3)
							{
								string text3 = obj.ToString().Trim().ToLower();
								bool flag = text3 == "1" || text3 == "true" || text3 == "yes";
								row[array2[j]] = flag;
							}
							else if (type5 == type)
							{
								if (dataType == type3)
								{
									if (Convert.ToBoolean(obj))
									{
										row[array2[j]] = "1";
									}
									else
									{
										row[array2[j]] = "0";
									}
								}
								else
								{
									string text3 = obj.ToString().Trim();
									if (text3.Length < 1)
									{
										row[array2[j]] = 0;
									}
									else
									{
										try
										{
											row[array2[j]] = int.Parse(text3);
										}
										catch
										{
											row[array2[j]] = 0;
										}
									}
								}
							}
							else if (type5 == type4)
							{
								string text3 = obj.ToString().Trim();
								try
								{
									row[array2[j]] = DateTime.Parse(text3);
								}
								catch
								{
									row[array2[j]] = DBNull.Value;
								}
							}
							else
							{
								row[array2[j]] = obj.ToString();
							}
						}
					}
				}
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < array3.Length; i++)
				{
					arrayList.Add(dv.Table.Columns[array3[i]].ColumnName);
				}
				foreach (object obj2 in arrayList)
				{
					string name = (string)obj2;
					dv.Table.Columns.Remove(name);
				}
				SetupSubProgressBar(0, 10);
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0002EF04 File Offset: 0x0002DF04
		private static DataTable ChangeColumnToStringDataType(DataTable t0, int colInd)
		{
			DataTable dataTable = new DataTable();
			for (int i = 0; i < t0.Columns.Count; i++)
			{
				if (i == colInd)
				{
					dataTable.Columns.Add(t0.Columns[i].ColumnName);
				}
				else
				{
					dataTable.Columns.Add(t0.Columns[i].ColumnName, t0.Columns[i].DataType);
				}
			}
			foreach (object obj in t0.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				DataRow dataRow2 = dataTable.NewRow();
				for (int i = 0; i < t0.Columns.Count; i++)
				{
					if (i == colInd)
					{
						dataRow2[i] = dataRow[i].ToString();
					}
					else
					{
						dataRow2[i] = dataRow[i];
					}
				}
				dataTable.Rows.Add(dataRow2);
			}
			return dataTable;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0002F058 File Offset: 0x0002E058
		private static ArrayList GetEquivalentRows_ListIsSortedByUniqueColNames(DataView dv, int indexOfDataRowView, int[] uniqueColIndices, out int indexOfFirstNonMatchingRow)
		{
			ArrayList arrayList = new ArrayList(120);
			DataRowView dataRowView = dv[indexOfDataRowView];
			DataRow row = dataRowView.Row;
			int i;
			for (i = indexOfDataRowView + 1; i < dv.Count; i++)
			{
				DataRowView dataRowView2 = dv[i];
				DataRow row2 = dataRowView2.Row;
				bool flag = true;
				for (int j = 0; j < uniqueColIndices.Length; j++)
				{
					string text = row2[uniqueColIndices[j]].ToString().Trim();
					string strB = row[uniqueColIndices[j]].ToString().Trim();
					if (text.CompareTo(strB) != 0)
					{
						flag = false;
						break;
					}
				}
				if (!flag)
				{
					break;
				}
				arrayList.Add(row2);
			}
			indexOfFirstNonMatchingRow = i;
			return arrayList;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0002F134 File Offset: 0x0002E134
		public static SmtpSettings GetSmtpSettings(UnivDataAdapter da)
		{
			return ReportFunction.GetSmtpSettings(da, -1);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0002F150 File Offset: 0x0002E150
		public static SmtpSettings GetSmtpSettings(UnivDataAdapter da, int groupId)
		{
			string commandText = "SELECT settingcode,settingstringvalue,settingvalue FROM settingsgroups WHERE settingcode IN (SELECT orderid AS settingcode FROM splitorderids(@codes,',')) AND groupid=@gid";
			string parameterValue = ClockWorkAPI.Utility.ListToString(new List<int>
			{
				101,
				102,
				103,
				104,
				105
			});
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@gid", groupId);
			da.SelectCommand.Parameters.Add("@codes", parameterValue);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			string text = null;
			string username = null;
			string password = null;
			bool useSsl = false;
			int port = 25;
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow[0];
				string text2 = dataRow[1].ToString();
				int num2 = (dataRow[2] == DBNull.Value) ? 0 : ((int)dataRow[2]);
				switch (num)
				{
				case 101:
					text = text2;
					break;
				case 102:
					if (text2.Length > 0)
					{
						int.TryParse(text2, out port);
					}
					else if (num2 > 0)
					{
						port = num2;
					}
					break;
				case 103:
					if (text2.Length > 0)
					{
						useSsl = ("yes1true".IndexOf(text2.ToLower()) >= 0);
					}
					else if (num2 > 0)
					{
						useSsl = (num2 == 1);
					}
					break;
				case 104:
					username = text2;
					break;
				case 105:
					password = text2;
					break;
				}
			}
			SmtpSettings result;
			if (!string.IsNullOrEmpty(text))
			{
				result = new SmtpSettings(port, text, username, password)
				{
					UseSsl = useSsl
				};
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0002F3AC File Offset: 0x0002E3AC
		private static bool GetStudentNameFromDb(string snum, int colInd, ref DataTable tlookup, TripleDESEncryptionClass tripleDES, UnivDataAdapter da, ref Code code, ref ArrayList codesWithMissingValues)
		{
			da.SelectCommand.CommandText = "SELECT lastname,firstname,student_no FROM people WHERE student_no=@student_no";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@student_no", tripleDES.Encrypt(snum));
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			bool result;
			if (dataTable.Rows.Count > 0)
			{
				object[] array = new object[tlookup.Columns.Count];
				DataRow dataRow = dataTable.Rows[0];
				array[0] = tripleDES.Decrypt((byte[])dataRow[0]);
				array[1] = tripleDES.Decrypt((byte[])dataRow[1]);
				array[2] = tripleDES.Decrypt((byte[])dataRow[2]);
				array[3] = "";
				array[4] = "";
				dataRow = tlookup.Rows.Add(array);
				code.codeValue = (string)dataRow[colInd];
				result = true;
			}
			else
			{
				codesWithMissingValues.Add(code);
				result = false;
			}
			return result;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0002F4CC File Offset: 0x0002E4CC
		private static DataRow GetRowByStudentNumber(string snum, DataTable t, int snumColInd)
		{
			string text = snum.ToLower().Trim();
			foreach (object obj in t.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string strB = dataRow[snumColInd].ToString().Trim().ToLower();
				if (text.CompareTo(strB) == 0)
				{
					return dataRow;
				}
			}
			return null;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0002F578 File Offset: 0x0002E578
		public static void ClearOutDuplicateCellsAfterFirstOne(ref DataView dv, string colName)
		{
			if (dv != null && dv.Table != null && dv.Table.Columns.Contains(colName))
			{
				string text = null;
				foreach (object obj in dv)
				{
					DataRowView dataRowView = (DataRowView)obj;
					DataRow row = dataRowView.Row;
					string text2 = row[colName].ToString().Trim().ToLower();
					if (text != null && text.CompareTo(text2) == 0)
					{
						row[colName] = "";
					}
					text = text2;
				}
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0002F658 File Offset: 0x0002E658
		public static void RemoveBreakdownItemsThatAreTextBased(UnivDataAdapter da, ref DataView dv, string colNameWithControlCaption)
		{
			if (dv != null && dv.Table != null && dv.Table.Columns.Contains(colNameWithControlCaption))
			{
				string text = null;
				ArrayList arrayList = new ArrayList();
				bool flag = false;
				try
				{
					foreach (object obj in dv)
					{
						DataRowView dataRowView = (DataRowView)obj;
						DataRow row = dataRowView.Row;
						string text2 = row[colNameWithControlCaption].ToString().Trim().ToLower();
						if (text != null && text.CompareTo(text2) == 0)
						{
							if (flag)
							{
								row.Delete();
							}
						}
						else
						{
							da.SelectCommand.CommandText = "SELECT controlid,controlcode,setting1,setting2,setting3 FROM dynamiccontrols WHERE controlcaption LIKE @cc";
							da.SelectCommand.Parameters.Clear();
							string str = row[colNameWithControlCaption].ToString().Trim();
							da.SelectCommand.Parameters.Add("@cc", str + "%");
							DataTable dataTable = new DataTable();
							string text3;
							da.Fill(dataTable, out text3);
							if (text3 != null && text3.Length > 0)
							{
								ReportFunction.MessageBoxShow(text3);
							}
							if (dataTable.Rows.Count > 0)
							{
								int num = (int)dataTable.Rows[0][1];
								int num2 = (int)dataTable.Rows[0]["setting3"];
								if (num == 2 || num == 4 || (num == 3 && num2 == 0))
								{
									flag = false;
								}
								else
								{
									flag = true;
									row.Delete();
								}
							}
							else
							{
								flag = false;
							}
						}
						text = text2;
					}
					dv.Table.AcceptChanges();
				}
				catch
				{
				}
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0002F8AC File Offset: 0x0002E8AC
		private static DataTable GetDynamicData(ArrayList studentNumbers, ArrayList codes, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, ref DataSet comboBoxData, DataTable staffNamesTable)
		{
			da.SelectCommand.CommandText = "SELECT personid INTO #tpeople FROM people WHERE ";
			da.SelectCommand.Parameters.Clear();
			for (int i = 0; i < studentNumbers.Count; i++)
			{
				string text = "@snum" + i.ToString();
				string plainText = (string)studentNumbers[i];
				byte[] parameterValue = tripleDES.Encrypt(plainText);
				if (i > 0)
				{
					UnivCommand selectCommand = da.SelectCommand;
					selectCommand.CommandText += " OR ";
				}
				UnivCommand selectCommand2 = da.SelectCommand;
				selectCommand2.CommandText = selectCommand2.CommandText + "student_no=" + text;
				da.SelectCommand.Parameters.Add(text, parameterValue);
			}
			UnivCommand selectCommand3 = da.SelectCommand;
			selectCommand3.CommandText += "; SELECT controlid INTO #tcontrols FROM dynamiccontrols WHERE (";
			for (int i = 0; i < codes.Count; i++)
			{
				string codeText = ((Code)codes[i]).codeText;
				string text = "@ccc" + i.ToString();
				if (i > 0)
				{
					UnivCommand selectCommand4 = da.SelectCommand;
					selectCommand4.CommandText += " OR ";
				}
				UnivCommand selectCommand5 = da.SelectCommand;
				selectCommand5.CommandText = selectCommand5.CommandText + "REPLACE(controlcaption,':','')=" + text;
				da.SelectCommand.Parameters.Add(text, codeText);
			}
			UnivCommand selectCommand6 = da.SelectCommand;
			selectCommand6.CommandText += "); SELECT DISTINCT tp.personid,p.firstname,p.lastname,p.student_no,dsc.screennum,a1.controlid,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,a1.valint,a1.valbytes,a1.valdate FROM ";
			UnivCommand selectCommand7 = da.SelectCommand;
			selectCommand7.CommandText += "#tpeople tp LEFT JOIN people p ON p.personid=tp.personid LEFT JOIN (";
			UnivCommand selectCommand8 = da.SelectCommand;
			selectCommand8.CommandText += "SELECT personid,screennum,controlid,controlvalue AS valint, NULL AS valbytes,NULL AS valdate FROM maininfops WHERE personid IN (SELECT personid FROM #tpeople) AND controlid IN (SELECT controlid FROM #tcontrols) ";
			UnivCommand selectCommand9 = da.SelectCommand;
			selectCommand9.CommandText += "UNION SELECT personid,screennum,controlid,NULL AS valint, controlvalue AS valbytes,NULL AS valdate FROM otherinfops WHERE personid IN (SELECT personid FROM #tpeople) AND controlid IN (SELECT controlid FROM #tcontrols) ";
			UnivCommand selectCommand10 = da.SelectCommand;
			selectCommand10.CommandText += "UNION SELECT personid,screennum,controlid,NULL AS valint, NULL AS valbytes,controlvalue AS valdate FROM datetimeinfops WHERE personid IN (SELECT personid FROM #tpeople) AND controlid IN (SELECT controlid FROM #tcontrols) ";
			UnivCommand selectCommand11 = da.SelectCommand;
			selectCommand11.CommandText += ") a1 ON a1.personid=tp.personid LEFT JOIN dynamiccontrols dc ON dc.controlid=a1.controlid LEFT JOIN dynamicscreencontrols dsc ON dsc.controlid=a1.controlid";
			UnivCommand selectCommand12 = da.SelectCommand;
			selectCommand12.CommandText += " ORDER BY tp.personid,a1.controlid";
			DataTable dataTable = new DataTable();
			string text2;
			da.Fill(dataTable, out text2);
			if (text2 != null && text2.Length > 0)
			{
				ReportFunction.MessageBoxShow("543: " + text2);
			}
			return Reports.FormatStudentData(dataTable, tripleDES, da, ref comboBoxData, staffNamesTable, true);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0002FB50 File Offset: 0x0002EB50
		private static void InsertPersonidsAndLuCourseIdIntoTempTable(UnivDataAdapter da, DataTable dataTable, string tempTableName, TripleDESEncryptionClass tripleDES)
		{
			int columnIndex = dataTable.Columns.IndexOf("personid");
			int columnIndex2 = dataTable.Columns.IndexOf("lucourseid");
			using (UnivCommand univCommand = da.CreateCommand("CREATE TABLE " + tempTableName + " (personid int,lastname varbinary(1000),firstname varbinary(1000),student_no varbinary(1000),lucourseid INT)"))
			{
				univCommand.ExecuteNonQuery2();
			}
			int i = 0;
			ArrayList arrayList = new ArrayList();
			string str = "DECLARE @lucid2 int; IF EXISTS(SELECT dataid FROM maininfoaccommodationps WHERE personid=@pid AND courseid=@lucid) OR EXISTS(SELECT dataid FROM otherinfoaccommodationps WHERE personid=@pid AND courseid=@lucid) OR EXISTS(SELECT dataid FROM datetimeinfoaccommodationps WHERE personid=@pid AND courseid=@lucid) SET @lucid2 = @lucid ELSE SET @lucid2 = 0; ";
			while (i < dataTable.Rows.Count)
			{
				using (UnivCommand univCommand = da.CreateCommand(str + "INSERT INTO " + tempTableName + " (personid,lastname,firstname,student_no,lucourseid) "))
				{
					int num = (int)dataTable.Rows[i][columnIndex];
					int num2 = (int)dataTable.Rows[i][columnIndex2];
					univCommand.Parameters.Clear();
					UnivCommand univCommand2 = univCommand;
					univCommand2.CommandText += " SELECT @pid,p.lastname,p.firstname,p.student_no,@lucid2 FROM people p WHERE p.personid=@pid";
					univCommand.ExecuteNonQuery2();
					i++;
				}
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0002FC90 File Offset: 0x0002EC90
		private static void WriteArrayListToFile(ArrayList list, TextWriter tw)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (i > 0)
				{
					tw.WriteLine("<hr />" + System.Environment.NewLine);
				}
				tw.WriteLine(i.ToString() + ". " + (string)list[i]);
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0002FCFC File Offset: 0x0002ECFC
		private static string ArrayListToString(ArrayList list)
		{
			string text = "";
			for (int i = 0; i < list.Count; i++)
			{
				if (i > 0)
				{
					text = text + "<hr />" + System.Environment.NewLine + System.Environment.NewLine;
				}
				text = text + i.ToString() + ". " + (string)list[i];
			}
			return text;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0002FD6C File Offset: 0x0002ED6C
		private static void AddStringListToAnotherStringList(ArrayList fromList, ref ArrayList toList, string newPrefix)
		{
			foreach (object obj in fromList)
			{
				string str = (string)obj;
				toList.Add(newPrefix + str);
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0002FDD4 File Offset: 0x0002EDD4
		private static DataTable LoadStudentsWithCoursesThatHaventBeenSentAnAccommodationEmailYet(DateTime sdate, DateTime edate, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, DateTime schoolyearsd, DateTime schoolyeared, bool sortByCourse, int emailTemplateId, int emailTemplateId2)
		{
			da.SelectCommand.CommandText = "SELECT DISTINCT c.personid,p.firstname,p.lastname,p.middlename,p.student_no,c.lucourseid,luc.startdate,luc.enddate,luc.term,luc.duration,lucd.altlookupstring AS subject,luc.course,luc.timeofday,luc.[section],lucd2.altlookupstring AS instructor,lucd2.phone AS instructorphone,lucd2.email AS instructoremail,lucd.email AS subjectemail FROM courses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid LEFT JOIN people p ON p.personid=c.personid LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid WHERE (c.registrationstatus IS NULL OR c.registrationstatus=0) AND NOT ( (@enddate<luc.startdate) OR (@startdate > luc.enddate) ) AND NOT EXISTS(SELECT aes.sentid FROM accommodationemailssent aes WHERE (aes.templateused=@templateid OR aes.templateused=@secondtemplateid) AND aes.personid=c.personid AND aes.lucourseid=c.lucourseid) AND p.isactive=1 AND ((p.dateadded >= @schoolyearsd AND p.dateadded < @schoolyeared ) OR p.personid IN (SELECT personid FROM peoplepreviousyears WHERE dateactive>=@schoolyearsd AND dateactive<@schoolyeared)) AND c.personid IN (SELECT DISTINCT personid FROM maininfoaccommodationps UNION SELECT DISTINCT personid FROM otherinfoaccommodationps UNION SELECT DISTINCT personid FROM datetimeinfoaccommodationps) ";
			if (sortByCourse)
			{
				UnivCommand selectCommand = da.SelectCommand;
				selectCommand.CommandText += "ORDER BY luc.term,luc.duration,lucd.altlookupstring,luc.course,luc.timeofday,luc.[section],lucd2.email";
			}
			else
			{
				UnivCommand selectCommand2 = da.SelectCommand;
				selectCommand2.CommandText += "ORDER BY c.personid,luc.term,luc.duration,lucd.altlookupstring,luc.course,luc.timeofday,luc.[section],lucd2.email";
			}
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@startdate", sdate);
			da.SelectCommand.Parameters.Add("@enddate", edate);
			da.SelectCommand.Parameters.Add("@schoolyearsd", schoolyearsd);
			da.SelectCommand.Parameters.Add("@schoolyeared", schoolyeared);
			da.SelectCommand.Parameters.Add("@templateid", emailTemplateId);
			da.SelectCommand.Parameters.Add("@secondtemplateid", emailTemplateId2);
			DataTable dataTable = new DataTable();
			string errmsg;
			da.Fill(dataTable, out errmsg);
			Errors.ShowErrMsg(errmsg);
			return dataTable;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0002FF10 File Offset: 0x0002EF10
		private static void InsertPersonidsIntoTempTable(UnivDataAdapter da, DataTable dataTable, string tempTableName, TripleDESEncryptionClass tripleDES)
		{
			int num = dataTable.Columns.IndexOf("personid");
			int num2 = dataTable.Columns.IndexOf("student_no");
			bool flag = num >= 0 && dataTable.Columns[num].DataType == typeof(int);
			using (UnivCommand univCommand = da.CreateCommand("CREATE TABLE " + tempTableName + " (personid int,lastname varbinary(1000),firstname varbinary(1000),student_no varbinary(1000))"))
			{
				univCommand.ExecuteNonQuery2();
			}
			int i = 0;
			ArrayList arrayList = new ArrayList();
			while (i < dataTable.Rows.Count)
			{
				using (UnivCommand univCommand = da.CreateCommand("INSERT INTO " + tempTableName + " (personid,lastname,firstname,student_no) "))
				{
					if (num >= 0)
					{
						univCommand.Parameters.Clear();
						UnivCommand univCommand2 = univCommand;
						univCommand2.CommandText += "SELECT personid,lastname,firstname,student_no FROM people WHERE (";
						int num3 = 0;
						int num4 = i;
						while (num4 < i + 20 && num4 < dataTable.Rows.Count)
						{
							int num5;
							if (flag)
							{
								num5 = (int)dataTable.Rows[num4][num];
							}
							else
							{
								string text = dataTable.Rows[num4][num].ToString().Trim();
								if (text.Length > 0)
								{
									try
									{
										num5 = int.Parse(text);
									}
									catch
									{
										num5 = -1;
									}
								}
								else
								{
									num5 = -1;
								}
							}
							if (!arrayList.Contains(num5))
							{
								arrayList.Add(num5);
								if (num3++ > 0)
								{
									UnivCommand univCommand3 = univCommand;
									univCommand3.CommandText += " OR ";
								}
								string text2 = "@pid" + num4.ToString();
								UnivCommand univCommand4 = univCommand;
								univCommand4.CommandText = univCommand4.CommandText + "personid=" + text2;
								univCommand.Parameters.Add(text2, num5);
							}
							num4++;
						}
						if (num3 == 0)
						{
							UnivCommand univCommand5 = univCommand;
							univCommand5.CommandText += "1=0";
						}
						UnivCommand univCommand6 = univCommand;
						univCommand6.CommandText = univCommand6.CommandText + ") AND NOT personid IN (SELECT personid FROM " + tempTableName + ")";
						univCommand.ExecuteNonQuery2();
						i += 20;
					}
					else if (num2 >= 0)
					{
						univCommand.Parameters.Clear();
						UnivCommand univCommand7 = univCommand;
						univCommand7.CommandText += "SELECT personid,lastname,firstname,student_no FROM people WHERE (";
						int num3 = 0;
						int num4 = i;
						while (num4 < i + 20 && num4 < dataTable.Rows.Count)
						{
							string text3 = dataTable.Rows[num4][num2].ToString();
							if (!arrayList.Contains(text3))
							{
								arrayList.Add(text3);
								if (num3++ > 0)
								{
									UnivCommand univCommand8 = univCommand;
									univCommand8.CommandText += " OR ";
								}
								byte[] parameterValue = tripleDES.Encrypt(text3);
								string text2 = "@snum" + num4.ToString();
								UnivCommand univCommand9 = univCommand;
								univCommand9.CommandText = univCommand9.CommandText + "student_no=" + text2;
								univCommand.Parameters.Add(text2, parameterValue);
							}
							num4++;
						}
						UnivCommand univCommand10 = univCommand;
						univCommand10.CommandText = univCommand10.CommandText + ") AND NOT personid IN (SELECT personid FROM " + tempTableName + ")";
						univCommand.ExecuteNonQuery2();
						i += 20;
					}
					else
					{
						i += 20;
					}
				}
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00030348 File Offset: 0x0002F348
		private static int AddPersonidToTable(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, DataTable t)
		{
			return ReportFunction.AddPersonidToTable(da, tripleDES, t, "personid");
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00030368 File Offset: 0x0002F368
		private static int AddPersonidToTable(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, DataTable t, string personidColName)
		{
			int num = t.Columns.IndexOf(personidColName);
			int result;
			if (num >= 0)
			{
				result = 0;
			}
			else
			{
				int num2 = t.Columns.IndexOf("student_no");
				if (num2 >= 0)
				{
					if (!t.Columns.Contains("personid"))
					{
						num = t.Columns.Count;
						t.Columns.Add("personid", typeof(int));
						t.Columns[num].ColumnMapping = MappingType.Hidden;
					}
					else
					{
						num = t.Columns.IndexOf("personid");
					}
					int i = 0;
					int num3 = 0;
					da.Connection.Open();
					while (i < t.Rows.Count)
					{
						da.SelectCommand.CommandText = "SELECT personid,student_no FROM people WHERE isactive=1 AND (";
						da.SelectCommand.Parameters.Clear();
						byte[][] array = new byte[25][];
						for (int j = 0; j < 25; j++)
						{
							int num4 = i + j;
							if (num4 < t.Rows.Count)
							{
								DataRow dataRow = t.Rows[num4];
								if (dataRow[num2] != DBNull.Value)
								{
									string plainText = dataRow[num2].ToString();
									byte[] array2 = tripleDES.Encrypt(plainText);
									string text = "@snum" + j.ToString();
									if (j > 0)
									{
										UnivCommand selectCommand = da.SelectCommand;
										selectCommand.CommandText += " OR ";
									}
									UnivCommand selectCommand2 = da.SelectCommand;
									selectCommand2.CommandText = selectCommand2.CommandText + "student_no=" + text;
									da.SelectCommand.Parameters.Add(text, array2);
									array[j] = array2;
								}
								else
								{
									array[j] = null;
								}
							}
							else
							{
								array[j] = null;
							}
						}
						UnivCommand selectCommand3 = da.SelectCommand;
						selectCommand3.CommandText += ")";
						UnivDataReader reader = da.SelectCommand.ExecuteReader2();
						DataTable dataTable = UnivOleDbFactory.ReaderToDataTable(reader);
						for (int j = 0; j < dataTable.Rows.Count; j++)
						{
							byte[] b = (byte[])dataTable.Rows[j][1];
							for (int k = 0; k < array.Length; k++)
							{
								if (array[k] != null)
								{
									byte[] array2 = array[k];
									if (ReportFunction.CompareByteArrays(array2, b))
									{
										t.Rows[k + i][num] = (int)dataTable.Rows[j][0];
										num3++;
										break;
									}
								}
							}
						}
						i += 25;
					}
					da.Connection.Close();
					result = num3;
				}
				else
				{
					result = -1;
				}
			}
			return result;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00030690 File Offset: 0x0002F690
		private static bool CompareByteArrays(byte[] b1, byte[] b2)
		{
			bool result;
			if (b1 == null && b2 == null)
			{
				result = true;
			}
			else if (b1 == null || b2 == null)
			{
				result = false;
			}
			else if (b1.Length != b2.Length)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < b1.Length; i++)
				{
					if (b1[i] != b2[i])
					{
						return false;
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00030700 File Offset: 0x0002F700
		public static void CrossReferenceWithPerStudentData(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, DataSet comboBoxData, DataTable staffNamesTable, string cids, ref Report report, out Exception exception)
		{
			ReportFunction.CrossReferenceWithPerStudentData(da, tripleDES, comboBoxData, staffNamesTable, cids, "", ref report, out exception);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00030718 File Offset: 0x0002F718
		public static DataTable CrossReferenceWithPerStudentData(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, string cids, string personidColName, DataTable currentTable, out Exception exception)
		{
			DataSet comboBoxData = new DataSet();
			DataTable staffNamesTable = new DataTable();
			Report report = new Report();
			report.AddResult(currentTable.DefaultView);
			ReportFunction.CrossReferenceWithPerStudentData(da, tripleDES, comboBoxData, staffNamesTable, cids, personidColName, ref report, out exception);
			DataView currentDataView = report.GetCurrentDataView();
			return (currentDataView == null) ? null : currentDataView.Table;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00030770 File Offset: 0x0002F770
		public static void CrossReferenceWithPerStudentData(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, DataSet comboBoxData, DataTable staffNamesTable, string cids, string personidColName, ref Report report, out Exception exception)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable dataTable = new DataTable();
			da.SelectCommand.CommandText = "SELECT dsc.screennum,dsc.controlid,dsc.ordernum,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue,dc.setting4  FROM\tdynamicscreencontrols dsc LEFT JOIN dynamiccontrols DC ON\tdc.controlid=dsc.controlid WHERE ";
			string[] array = cids.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				if (i > 0)
				{
					UnivCommand selectCommand = da.SelectCommand;
					selectCommand.CommandText += " OR ";
				}
				UnivCommand selectCommand2 = da.SelectCommand;
				selectCommand2.CommandText = selectCommand2.CommandText + "dsc.controlid=" + text.Trim();
			}
			da.SelectCommand.Parameters.Clear();
			string text2;
			da.Fill(dataTable, out text2);
			ReportFunction.CrossReferenceWithPerStudentData(da, tripleDES, comboBoxData, staffNamesTable, dataTable, ref report, out exception, personidColName);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0003084C File Offset: 0x0002F84C
		public static DataTable CrossReferencePerAppointmentData(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, DataTable dataTable, string cidsCommaSeparated, ref DataSet comboBoxData, DataTable staffNamesTable)
		{
			byte[] array = new byte[0];
			DataTable dataTable2 = new DataTable();
			dataTable2.Columns.Add("personid", typeof(int));
			dataTable2.Columns.Add("appointmentid", typeof(int));
			dataTable2.Columns.Add("dataid", typeof(int));
			dataTable2.Columns.Add("screennum", typeof(int));
			dataTable2.Columns.Add("controlid", typeof(int));
			dataTable2.Columns.Add("valint", typeof(int));
			dataTable2.Columns.Add("valbytes", array.GetType());
			dataTable2.Columns.Add("valdate", typeof(DateTime));
			dataTable2.Columns.Add("controlcaption");
			dataTable2.Columns.Add("setting1", typeof(int));
			dataTable2.Columns.Add("setting2", typeof(int));
			dataTable2.Columns.Add("setting3", typeof(int));
			dataTable2.Columns.Add("setting4", typeof(int));
			dataTable2.Columns.Add("setting4string");
			dataTable2.Columns.Add("defaultvalue", typeof(int));
			dataTable2.Columns.Add("controlcode", typeof(int));
			dataTable2.Columns.Add("startdate", typeof(DateTime));
			dataTable2.Columns.Add("lastname", array.GetType());
			dataTable2.Columns.Add("firstname", array.GetType());
			dataTable2.Columns.Add("student_no", array.GetType());
			int[] array2 = new int[dataTable2.Columns.Count];
			for (int i = 0; i < dataTable2.Columns.Count; i++)
			{
				array2[i] = i;
			}
			DataTable dataTable3 = dataTable2.Clone();
			UnivTransaction univTransaction = null;
			try
			{
				da.Connection.Open();
				univTransaction = da.Connection.BeginTransaction();
				da.SelectCommand.Transaction = univTransaction;
				ReportFunction.CreateTempTable(da, dataTable, "#tpidsappids", new string[]
				{
					"personid",
					"appointmentid"
				}, tripleDES);
				string commandText = "SELECT t.personid,t.appointmentid,p.dataid,p.screennum,p.controlid,p.valint,p.valbytes,p.valdate,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.setting4,dc.setting4string,dc.defaultvalue,dc.controlcode,p.startdate,p2.lastname,p2.firstname,p2.student_no FROM (SELECT DISTINCT personid,appointmentid FROM #tpidsappids) t LEFT JOIN perappdata p ON p.personid=t.personid AND p.appointmentid=t.appointmentid LEFT JOIN dynamiccontrols dc ON dc.controlid=p.controlid LEFT JOIN people p2 ON p2.personid=t.personid WHERE NOT p.controlid IS NULL AND p.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','));";
				using (UnivCommand univCommand = da.CreateCommand(""))
				{
					univCommand.CommandText = commandText;
					univCommand.Parameters.Clear();
					univCommand.Parameters.Add("@cids", cidsCommaSeparated);
					UnivDataReader univDataReader = univCommand.ExecuteReader2();
					dataTable2 = UnivOleDbFactory.ToDataTable(univDataReader.ToItemArrays(), dataTable2, array2);
				}
				univTransaction.Commit();
				da.Connection.Close();
			}
			catch (Exception ex)
			{
			}
			finally
			{
				da.Connection.Close();
			}
			DataView defaultView = Reports.FormatPerAppData(dataTable2, da, tripleDES, ref comboBoxData, staffNamesTable).DefaultView;
			foreach (object obj in dataTable2.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				string columnName = dataColumn.ColumnName;
				string text = columnName.ToLower();
				if (text.CompareTo("personid") != 0 && text.CompareTo("appointmentid") != 0)
				{
					if (!dataTable.Columns.Contains(columnName))
					{
						dataTable.Columns.Add(columnName, dataColumn.DataType);
					}
				}
			}
			DataTable table = defaultView.Table;
			foreach (object obj2 in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj2;
				if (dataRow.RowState != DataRowState.Deleted)
				{
					int num = (int)dataRow["personid"];
					int num2 = (int)dataRow["appointmentid"];
					DataRow[] array3 = table.Select("personid=" + num.ToString() + " AND appointmentid=" + num2.ToString());
					if (array3.Length > 0)
					{
						DataRow dataRow2 = array3[0];
						for (int i = 0; i < table.Columns.Count; i++)
						{
							string columnName = table.Columns[i].ColumnName;
							dataRow[columnName] = dataRow2[i];
						}
					}
				}
			}
			foreach (object obj3 in dataTable3.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj3;
				string columnName = dataColumn.ColumnName;
				string text = columnName.ToLower();
				if (text.CompareTo("personid") != 0 && text.CompareTo("appointmentid") != 0)
				{
					dataTable.Columns.Remove(columnName);
				}
			}
			return dataTable;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00030E64 File Offset: 0x0002FE64
		private static void CreateTempTable(UnivDataAdapter da, DataTable dataTable, string temporaryTableName, string[] colNamesToCopyToTempTable, TripleDESEncryptionClass tripleDES)
		{
			byte[] array = new byte[0];
			string str = (temporaryTableName.IndexOf("#") == 0) ? temporaryTableName : ("#" + temporaryTableName);
			string text = "CREATE TABLE " + str + " (";
			for (int i = 0; i < colNamesToCopyToTempTable.Length; i++)
			{
				string text2 = colNamesToCopyToTempTable[i];
				if (i > 0)
				{
					text += ",";
				}
				text = text + text2 + " ";
				Type dataType = dataTable.Columns[text2].DataType;
				if (dataType == typeof(int))
				{
					text += "int";
				}
				else if (dataType == array.GetType())
				{
					text += "varbinary(8000)";
				}
				else if (dataType == typeof(decimal))
				{
					text += "decimal";
				}
				else
				{
					text += "text";
				}
			}
			text += ")";
			using (UnivCommand univCommand = da.CreateCommand(""))
			{
				univCommand.CommandText = text;
				univCommand.ExecuteNonQuery2();
			}
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (dataRow.RowState != DataRowState.Deleted)
				{
					using (UnivCommand univCommand2 = da.CreateCommand(""))
					{
						univCommand2.Parameters.Clear();
						text = "INSERT INTO " + str + " (";
						string text3 = ") VALUES (";
						for (int i = 0; i < colNamesToCopyToTempTable.Length; i++)
						{
							string text2 = colNamesToCopyToTempTable[i];
							if (i > 0)
							{
								text += ",";
								text3 += ",";
							}
							text += text2;
							text3 = text3 + "@" + text2;
							univCommand2.Parameters.Add("@" + text2, dataRow[text2]);
						}
						text3 += ")";
						univCommand2.CommandText = text + text3;
						univCommand2.ExecuteNonQuery2();
					}
				}
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00031168 File Offset: 0x00030168
		public static void CrossReferenceWithPerStudentData(UnivDataAdapter da0, TripleDESEncryptionClass tripleDES, DataSet comboBoxData, DataTable staffNamesTable, DataTable reportViewWithSelectedFieldsToPullIn, ref Report report, out Exception exception)
		{
			ReportFunction.CrossReferenceWithPerStudentData(da0, tripleDES, comboBoxData, staffNamesTable, reportViewWithSelectedFieldsToPullIn, ref report, out exception, "personid");
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000311D4 File Offset: 0x000301D4
		public static void CrossReferenceWithPerStudentData(UnivDataAdapter da0, TripleDESEncryptionClass tripleDES, DataSet comboBoxData, DataTable staffNamesTable, DataTable reportViewWithSelectedFieldsToPullIn, ref Report report, out Exception exception, string personidColName)
		{
			DataView currentDataView = report.GetCurrentDataView();
			if (reportViewWithSelectedFieldsToPullIn != null && reportViewWithSelectedFieldsToPullIn.Rows.Count > 0)
			{
				List<int> list = new List<int>(reportViewWithSelectedFieldsToPullIn.Rows.Count);
				foreach (object obj in reportViewWithSelectedFieldsToPullIn.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					if (!(dataRow["controlid"] is DBNull))
					{
						int item = (int)dataRow["controlid"];
						if (!list.Contains(item))
						{
							list.Add(item);
						}
					}
				}
				List<int> list2 = new List<int>(currentDataView.Table.Rows.Count);
				foreach (object obj2 in currentDataView.Table.Rows)
				{
					DataRow dataRow = (DataRow)obj2;
					if (!(dataRow[personidColName] is DBNull))
					{
						int num = (int)dataRow[personidColName];
						if (num > 0 && !list2.Contains(num))
						{
							list2.Add(num);
						}
					}
				}
				IDynamicFieldClientManager dynamicFieldClientManager = new DynamicFieldClientManager();
				List<DynamicFieldDTO> list3 = dynamicFieldClientManager.LoadFieldsByControlIds(list);
				IDynamicDataClientManager dynamicDataClientManager = new DynamicDataClientManager();
				List<DynamicDataSetDTO> list4 = dynamicDataClientManager.LoadPerStudentDataForMultipleStudents(list2, list);
				list3.Sort((DynamicFieldDTO f1, DynamicFieldDTO f2) => f1.OrderNum.CompareTo(f2.OrderNum));
				foreach (DynamicFieldDTO dynamicFieldDTO in list3)
				{
					string text = ((string.IsNullOrEmpty(dynamicFieldDTO.OriginalCaption) ? dynamicFieldDTO.ControlCaption : dynamicFieldDTO.OriginalCaption) ?? "empty").Replace("~~", "_").Replace(".", "").Replace(",", "");
					if (!currentDataView.Table.Columns.Contains(text))
					{
						eControlCode controlCode = dynamicFieldDTO.ControlCode;
						if (controlCode == eControlCode.CheckBox)
						{
							goto IL_277;
						}
						switch (controlCode)
						{
						case eControlCode.Date:
						case eControlCode.Time:
							currentDataView.Table.Columns.Add(text, typeof(DateTime));
							break;
						default:
							if (controlCode == eControlCode.MyCheckBox)
							{
								goto IL_277;
							}
							currentDataView.Table.Columns.Add(text);
							break;
						}
						continue;
						IL_277:
						currentDataView.Table.Columns.Add(text, typeof(bool));
					}
				}
				foreach (object obj3 in currentDataView.Table.Rows)
				{
					DataRow dataRow = (DataRow)obj3;
					int pid = (dataRow[personidColName] is DBNull) ? 0 : ((int)dataRow[personidColName]);
					if (pid > 0)
					{
						List<DynamicDataSetDTO> list5 = list4.FindAll((DynamicDataSetDTO g) => g.Context.PrimaryId == pid);
						foreach (DynamicDataSetDTO dynamicDataSetDTO in list5)
						{
							foreach (DynamicDataDTO dynamicDataDTO in dynamicDataSetDTO.Data)
							{
								string text = ((string.IsNullOrEmpty(dynamicDataDTO.Field.OriginalCaption) ? dynamicDataDTO.Field.ControlCaption : dynamicDataDTO.Field.OriginalCaption) ?? "empty").Replace("~~", "_").Replace(".", "").Replace(",", "");
								if (currentDataView.Table.Columns.Contains(text))
								{
									DataColumn dataColumn = currentDataView.Table.Columns[text];
									if (dataColumn.DataType == typeof(bool))
									{
										if (dynamicDataDTO.Value != null && dynamicDataDTO.Value is bool)
										{
											dataRow[text] = (bool)dynamicDataDTO.Value;
										}
									}
									else if (dataColumn.DataType == typeof(DateTime))
									{
										if (dynamicDataDTO.Value != null && dynamicDataDTO.Value is DateTime)
										{
											dataRow[text] = (DateTime)dynamicDataDTO.Value;
										}
									}
									else if (dataColumn.DataType == typeof(int))
									{
										if (dynamicDataDTO.Value != null && dynamicDataDTO.Value is int)
										{
											dataRow[text] = (int)dynamicDataDTO.Value;
										}
										else
										{
											dataRow[text] = dynamicDataDTO.ValueId;
										}
									}
									else
									{
										dataRow[text] = dynamicDataDTO.ToDomainObject().GetString();
									}
								}
							}
						}
					}
				}
				exception = null;
				report.AddResult(currentDataView);
			}
			else
			{
				exception = null;
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000318A0 File Offset: 0x000308A0
		public static void LoadAllActiveStudentsWithSpecificData(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, ref Report report, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			da.SelectCommand.CommandText = "SELECT     p.personid,p.firstname,p.middlename,p.lastname,p.student_no FROM people p WHERE      p.isactive=1        AND (   (p.dateadded>=@schoolyearstartdate AND p.dateadded<=@schoolyearenddate)              OR p.personid IN (SELECT personid FROM peoplepreviousyears WHERE dateactive>=@schoolyearstartdate AND dateactive<=@schoolyearenddate) )        AND (@cids='' OR        p.personid IN (SELECT personid FROM maininfops WHERE controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,',')) UNION SELECT personid FROM otherinfops WHERE controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,',')) UNION SELECT personid FROM datetimeinfops WHERE controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,',')) ))";
			VariableCollection variables = report.Variables;
			string parameterValue = (string)variables["cids"].VariableValue;
			DateTime dateTime = (DateTime)variables["schoolyear_startdate"].VariableValue;
			DateTime dateTime2 = (DateTime)variables["schoolyear_enddate"].VariableValue;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@cids", parameterValue);
			da.SelectCommand.Parameters.Add("@schoolyearstartdate", dateTime);
			da.SelectCommand.Parameters.Add("@schoolyearenddate", dateTime2);
			DataTable dataTable = new DataTable();
			string text;
			da.Fill(dataTable, out text);
			if (text != null && text.Length > 0)
			{
				ReportFunction.MessageBoxShow(text);
			}
			DataTable dataTable2 = new DataTable("students");
			dataTable2.Columns.Add("personid", typeof(int));
			dataTable2.Columns.Add("firstname");
			dataTable2.Columns.Add("middlename");
			dataTable2.Columns.Add("lastname");
			dataTable2.Columns.Add("student_no");
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				DataRow dataRow2 = dataTable2.NewRow();
				dataRow2[0] = (int)dataRow[0];
				for (int i = 1; i < 5; i++)
				{
					dataRow2[i] = tripleDES.Decrypt(dataRow[i]);
				}
				dataTable2.Rows.Add(dataRow2);
			}
			DataView defaultView = dataTable2.DefaultView;
			defaultView.Sort = "lastname,firstname";
			report.AddResult(defaultView);
			dataTable.Rows.Clear();
			dataTable.Dispose();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00031B04 File Offset: 0x00030B04
		public static void CrossReferenceWithPerAppointmentData2(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, DataSet comboBoxData, DataTable staffNamesTable, DataTable controlsTable, ref Report report, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar, string apptypeids, DateTime sdate, DateTime edate)
		{
			report.SetVariables(da);
			if (sdate == DateTime.MinValue)
			{
				sdate = (DateTime)report.Variables["startdate"].VariableValue;
			}
			if (edate == DateTime.MinValue)
			{
				edate = (DateTime)report.Variables["enddate"].VariableValue;
			}
			bool flag = (bool)report.Variables["includenoshowappointments"].VariableValue;
			bool flag2 = (bool)report.Variables["includecancelledappointments"].VariableValue;
			bool flag3 = (bool)report.Variables["includetentativeappointments"].VariableValue;
			string sql = string.Concat(new string[]
			{
				"SELECT att.personid,att.appointmentid,app.startdate,app.apptypeid,at.description,app.cancelled,a.controlid,a.appointmentid AS appid,a.valint,a.valbytes,a.valdate FROM attendees att LEFT JOIN appointments app ON app.appointmentid=att.appointmentid LEFT JOIN (SELECT m.personid,m.controlid,m.appointmentid,m.controlvalue AS valint,NULL AS valbytes,NULL AS valdate FROM maininfopa m WHERE m.personid=@personid UNION  SELECT o.personid,o.controlid,o.appointmentid,NULL AS valint,o.controlvalue AS valbytes,NULL AS valdate FROM otherinfopa o WHERE o.personid=@personid UNION  SELECT d.personid,d.controlid,d.appointmentid,NULL AS valint,NULL AS valbytes,d.controlvalue AS valdate FROM datetimeinfopa d WHERE d.personid=@personid) a ON a.personid=att.personid AND a.appointmentid=att.appointmentid LEFT JOIN appointmenttypes at ON at.apptypeid=app.apptypeid WHERE att.personid=@personid AND app.startdate >='",
				sdate.ToString("yyyy-MM-dd"),
				"' AND app.startdate < '",
				edate.ToString("yyyy-MM-dd"),
				"' ",
				(!flag) ? "AND att.noshow=0 " : "",
				"AND app.apptypeid IN (SELECT orderid AS apptypeid FROM splitorderids('",
				apptypeids,
				"',',')) ",
				(!flag2) ? "AND app.cancelled=0 " : "",
				(!flag3) ? "AND NOT app.appcode=-1" : ""
			});
			ReportFunction.PullInDataPersonIdOnly(da, tripleDES, ref report, sql, IncrementSubProgressBar, SetupSubProgressBar);
			DataView dv = Reports.FormatDynamicData(controlsTable, report.GetCurrentDataView().Table, "appointmentid", da, tripleDES);
			report.AddResult(dv);
			sql = "SELECT att.personid AS counsellorpid,p.lastname AS counsellorLastName, p.firstname AS counsellorFirstName FROM attendees att LEFT JOIN people p ON p.personid=att.personid WHERE att.appointmentid=@appointmentid AND att.personid IN (SELECT personid FROM peoplegroups WHERE groupid=2 OR groupid=10)";
			ReportFunction.PullInData(da, tripleDES, ref report, sql, IncrementSubProgressBar, SetupSubProgressBar);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00031D10 File Offset: 0x00030D10
		public static void CrossReferenceWithPerAppointmentData(UnivDataAdapter da0, TripleDESEncryptionClass tripleDES, DataSet comboBoxData, DataTable staffNamesTable, DataTable reportViewWithSelectedFieldsToPullIn, ref Report report, out Exception exception)
		{
			DataView currentDataView = report.GetCurrentDataView();
			if (reportViewWithSelectedFieldsToPullIn != null && reportViewWithSelectedFieldsToPullIn.Rows.Count > 0)
			{
				byte[] array = new byte[5];
				Type type = array.GetType();
				Type typeFromHandle = typeof(int);
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("personid", typeFromHandle);
				dataTable.Columns.Add("firstname", type);
				dataTable.Columns.Add("lastname", type);
				dataTable.Columns.Add("student_no", type);
				dataTable.Columns.Add("screennum", typeFromHandle);
				dataTable.Columns.Add("controlid", typeFromHandle);
				dataTable.Columns.Add("controlcode", typeFromHandle);
				dataTable.Columns.Add("controlcaption");
				dataTable.Columns.Add("setting1", typeFromHandle);
				dataTable.Columns.Add("setting2", typeFromHandle);
				dataTable.Columns.Add("setting3", typeFromHandle);
				dataTable.Columns.Add("valint", typeFromHandle);
				dataTable.Columns.Add("valbytes", type);
				dataTable.Columns.Add("valdate", typeof(DateTime));
				dataTable.Columns.Add("defaultvalue", typeFromHandle);
				dataTable.Columns.Add("miappid", typeFromHandle);
				dataTable.Columns.Add("oiappid", typeFromHandle);
				dataTable.Columns.Add("diappid", typeFromHandle);
				int[] colMapping = new int[]
				{
					0,
					1,
					2,
					3,
					4,
					5,
					6,
					7,
					8,
					9,
					10,
					11,
					12,
					13,
					14,
					15,
					16,
					17
				};
				int num = ReportFunction.AddPersonidToTable(da0, tripleDES, currentDataView.Table);
				UnivTransaction univTransaction = null;
				bool flag = true;
				try
				{
					da0.Connection.Open();
					univTransaction = da0.Connection.BeginTransaction();
					da0.SelectCommand.Transaction = univTransaction;
					ReportFunction.InsertPersonidsIntoTempTable(da0, currentDataView.Table, "#t3", tripleDES);
					string text = "CREATE TABLE #t1 (";
					string text2 = "";
					for (int i = 0; i < reportViewWithSelectedFieldsToPullIn.Columns.Count; i++)
					{
						if (i > 0)
						{
							text += ",";
							text2 += ",";
						}
						if (reportViewWithSelectedFieldsToPullIn.Columns[i].DataType == typeFromHandle)
						{
							text = text + reportViewWithSelectedFieldsToPullIn.Columns[i].ColumnName + " int";
						}
						else
						{
							text = text + reportViewWithSelectedFieldsToPullIn.Columns[i].ColumnName + " varchar(1000)";
						}
						text2 += reportViewWithSelectedFieldsToPullIn.Columns[i].ColumnName;
					}
					text += ")";
					using (UnivCommand univCommand = da0.CreateCommand(text))
					{
						univCommand.ExecuteNonQuery2();
					}
					for (int i = 0; i < reportViewWithSelectedFieldsToPullIn.Rows.Count; i++)
					{
						using (UnivCommand univCommand = da0.CreateCommand(""))
						{
							univCommand.Parameters.Clear();
							text = "INSERT INTO #t1 (" + text2 + ") VALUES (";
							for (int j = 0; j < reportViewWithSelectedFieldsToPullIn.Columns.Count; j++)
							{
								string text3 = "@" + reportViewWithSelectedFieldsToPullIn.Columns[j].ColumnName;
								univCommand.Parameters.Add(text3, reportViewWithSelectedFieldsToPullIn.Rows[i][j]);
								if (j > 0)
								{
									text += ",";
								}
								text += text3;
							}
							text += ")";
							univCommand.CommandText = text;
							univCommand.ExecuteNonQuery2();
						}
					}
					text = "SELECT \taa.personid,aa.firstname,aa.lastname,aa.student_no,aa.screennum,aa.controlid,aa.controlcode,aa.controlcaption,aa.setting1,aa.setting2,aa.setting3,\r\n\t\t\t\t\t\t\t\tmi.controlvalue AS valint,oi.controlvalue AS valbytes,di.controlvalue AS valdate,\r\n\t\t\t\t\t\t\t\taa.defaultvalue,mi.appointmentid AS miappid,oi.appointmentid AS oiappid,di.appointmentid AS diappid\r\n\t\t\t\t\t\t\tFROM\r\n\t\t\t\t\t\t\t\t(SELECT t3.personid,t3.firstname,t3.lastname,t3.student_no,t1.screennum,t1.controlid,t1.ordernum,t1.controlcode,t1.controlcaption,t1.setting1,t1.setting2,t1.setting3,t1.defaultvalue\r\n\t\t\t\t\t\t\t\tFROM\t#t3 t3, #t1 t1 ) aa\r\n\t\t\t\t\t\t\t\tLEFT JOIN maininfopa mi ON mi.personid=aa.personid AND mi.controlid=aa.controlid\r\n\t\t\t\t\t\t\t\tLEFT JOIN otherinfopa oi ON oi.personid=aa.personid AND oi.controlid=aa.controlid\r\n\t\t\t\t\t\t\t\tLEFT JOIN datetimeinfopa di ON di.personid=aa.personid AND di.controlid=aa.controlid\r\n\t\t\t\t\t\t\tORDER BY aa.personid,aa.ordernum";
					using (UnivCommand univCommand = da0.CreateCommand(text))
					{
						UnivDataReader univDataReader = univCommand.ExecuteReader2();
						dataTable = UnivOleDbFactory.ToDataTable(univDataReader.ToItemArrays(), dataTable, colMapping);
					}
					DataTableView.ShowDataTableView(dataTable);
					univTransaction.Commit();
					da0.Connection.Close();
				}
				catch (Exception ex)
				{
					if (univTransaction != null)
					{
						univTransaction.Rollback();
					}
					ReportFunction.MessageBoxShow(ex.ToString());
					exception = ex;
					flag = false;
				}
				finally
				{
					da0.Connection.Close();
					da0.SelectCommand.Transaction = null;
					univTransaction = null;
					exception = null;
					da0.Connection.Open();
					da0.Connection.Close();
				}
				if (flag)
				{
					if (dataTable != null)
					{
						try
						{
							DataView dataView = Reports.FormatDynamicData(reportViewWithSelectedFieldsToPullIn, dataTable, "miappid,oiappid,diappid", da0, tripleDES);
							DataTable table = dataView.Table;
							DataTable table2 = dataView.Table;
							DataView dataView2 = ReportFunction.CopyDataView(currentDataView);
							DataTable table3 = dataView2.Table;
							int count = table3.Columns.Count;
							for (int i = 4; i < table2.Columns.Count; i++)
							{
								table3.Columns.Add(Reports.GetUniqueColName(table3, table2.Columns[i].ColumnName), table2.Columns[i].DataType);
							}
							int num2 = table3.Columns.IndexOf("personid");
							bool flag2 = num2 >= 0 && table3.Columns[num2].DataType == typeof(int);
							for (int i = 0; i < table3.Rows.Count; i++)
							{
								DataRow dataRow = table3.Rows[i];
								if (dataRow[num2] != DBNull.Value)
								{
									int num3;
									if (flag2)
									{
										num3 = (int)dataRow[num2];
									}
									else
									{
										string text4 = dataRow[num2].ToString().Trim();
										if (text4.Length > 0)
										{
											try
											{
												num3 = int.Parse(text4);
											}
											catch
											{
												num3 = -1;
											}
										}
										else
										{
											num3 = -1;
										}
									}
									for (int j = 0; j < table2.Rows.Count; j++)
									{
										if (table2.Rows[j]["personid"] != DBNull.Value)
										{
											int num4;
											if (table2.Columns["personid"].DataType == typeof(int))
											{
												num4 = (int)table2.Rows[j]["personid"];
											}
											else
											{
												string s = table2.Rows[j]["personid"].ToString();
												try
												{
													num4 = int.Parse(s);
												}
												catch
												{
													num4 = -1;
												}
											}
											if (num4 == num3)
											{
												for (int k = count; k < table3.Columns.Count; k++)
												{
													table3.Rows[i][k] = table2.Rows[j][k - count + 4];
												}
											}
										}
									}
								}
							}
							dataView2 = new DataView(table3, dataView2.RowFilter, dataView2.Sort, dataView2.RowStateFilter);
							table3.Columns["personid"].ColumnMapping = MappingType.Hidden;
							exception = null;
							report.AddResult(dataView2);
						}
						catch (Exception ex2)
						{
							exception = ex2;
						}
					}
					else
					{
						exception = null;
					}
				}
			}
			else
			{
				exception = null;
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00032660 File Offset: 0x00031660
		public static void CrossReferenceWithAccommodationData(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, DataSet comboBoxData, DataTable staffNamesTable, int showOnLetter, ref Report report, out Exception exception)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable dataTable = new DataTable();
			da.SelectCommand.CommandText = "SELECT 4 AS screennum,dc.controlid,dsc.ordernum,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue FROM dynamiccontrols dc LEFT JOIN dynamicscreencontrols dsc ON dsc.controlid=dc.controlid WHERE dc.controlid IN (SELECT controlid FROM accommodations WHERE showonletter>0)";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@showonletter", showOnLetter);
			da.Fill(dataTable);
			ReportFunction.CrossReferenceWithPerStudentData(da, tripleDES, comboBoxData, staffNamesTable, dataTable, ref report, out exception);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00032714 File Offset: 0x00031714
		public static void CrossReferenceWithAccommodationData(UnivDataAdapter da0, TripleDESEncryptionClass tripleDES, DataSet comboBoxData, DataTable staffNamesTable, DataTable reportViewWithSelectedFieldsToPullIn, ref Report report, out Exception exception)
		{
			DataView currentDataView = report.GetCurrentDataView();
			if (reportViewWithSelectedFieldsToPullIn != null && reportViewWithSelectedFieldsToPullIn.Rows.Count > 0)
			{
				byte[] array = new byte[5];
				Type type = array.GetType();
				Type typeFromHandle = typeof(int);
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("personid", typeFromHandle);
				dataTable.Columns.Add("firstname", type);
				dataTable.Columns.Add("lastname", type);
				dataTable.Columns.Add("student_no", type);
				dataTable.Columns.Add("screennum", typeFromHandle);
				dataTable.Columns.Add("controlid", typeFromHandle);
				dataTable.Columns.Add("controlcode", typeFromHandle);
				dataTable.Columns.Add("controlcaption");
				dataTable.Columns.Add("setting1", typeFromHandle);
				dataTable.Columns.Add("setting2", typeFromHandle);
				dataTable.Columns.Add("setting3", typeFromHandle);
				dataTable.Columns.Add("valint", typeFromHandle);
				dataTable.Columns.Add("valbytes", type);
				dataTable.Columns.Add("valdate", typeof(DateTime));
				dataTable.Columns.Add("defaultvalue", typeFromHandle);
				int[] colMapping = new int[]
				{
					0,
					1,
					2,
					3,
					4,
					5,
					6,
					7,
					8,
					9,
					10,
					11,
					12,
					13,
					14
				};
				int num = ReportFunction.AddPersonidToTable(da0, tripleDES, currentDataView.Table);
				UnivTransaction univTransaction = null;
				bool flag = true;
				try
				{
					da0.Connection.Open();
					univTransaction = da0.Connection.BeginTransaction();
					da0.SelectCommand.Transaction = univTransaction;
					ReportFunction.InsertPersonidsAndLuCourseIdIntoTempTable(da0, currentDataView.Table, "#t3", tripleDES);
					string text = "CREATE TABLE #t1 (";
					string text2 = "";
					for (int i = 0; i < reportViewWithSelectedFieldsToPullIn.Columns.Count; i++)
					{
						if (i > 0)
						{
							text += ",";
							text2 += ",";
						}
						if (reportViewWithSelectedFieldsToPullIn.Columns[i].DataType == typeFromHandle)
						{
							text = text + reportViewWithSelectedFieldsToPullIn.Columns[i].ColumnName + " int";
						}
						else
						{
							text = text + reportViewWithSelectedFieldsToPullIn.Columns[i].ColumnName + " varchar(1000)";
						}
						text2 += reportViewWithSelectedFieldsToPullIn.Columns[i].ColumnName;
					}
					text += ")";
					using (UnivCommand univCommand = da0.CreateCommand(text))
					{
						univCommand.ExecuteNonQuery2();
					}
					for (int i = 0; i < reportViewWithSelectedFieldsToPullIn.Rows.Count; i++)
					{
						using (UnivCommand univCommand = da0.CreateCommand(""))
						{
							univCommand.Parameters.Clear();
							text = "INSERT INTO #t1 (" + text2 + ") VALUES (";
							for (int j = 0; j < reportViewWithSelectedFieldsToPullIn.Columns.Count; j++)
							{
								string text3 = "@" + reportViewWithSelectedFieldsToPullIn.Columns[j].ColumnName;
								univCommand.Parameters.Add(text3, reportViewWithSelectedFieldsToPullIn.Rows[i][j]);
								if (j > 0)
								{
									text += ",";
								}
								text += text3;
							}
							text += ")";
							univCommand.CommandText = text;
							univCommand.ExecuteNonQuery2();
						}
					}
					text = "SELECT \taa.personid,aa.firstname,aa.lastname,aa.student_no,aa.screennum,aa.controlid,aa.controlcode,aa.controlcaption,aa.setting1,aa.setting2,aa.setting3,\r\n\t\t\t\t\t\t\t\tmi.controlvalue AS valint,oi.controlvalue AS valbytes,di.controlvalue AS valdate,\r\n\t\t\t\t\t\t\t\taa.defaultvalue\r\n\t\t\t\t\t\t\tFROM\r\n\t\t\t\t\t\t\t\t(SELECT \tt3.personid,t3.firstname,t3.lastname,t3.student_no,t1.screennum,t1.controlid,t1.ordernum,t1.controlcode,t1.controlcaption,t1.setting1,t1.setting2,t1.setting3,t1.defaultvalue,t3.lucourseid\r\n\t\t\t\t\t\t\t\tFROM\t#t3 t3, #t1 t1 ) aa\r\n\t\t\t\t\t\t\t\tLEFT JOIN maininfoaccommodationps mi ON mi.personid=aa.personid AND mi.controlid=aa.controlid AND mi.courseid=aa.lucourseid\r\n\t\t\t\t\t\t\t\tLEFT JOIN otherinfoaccommodationps oi ON oi.personid=aa.personid AND oi.controlid=aa.controlid AND oi.courseid=aa.lucourseid\r\n\t\t\t\t\t\t\t\tLEFT JOIN datetimeinfoaccommodationps di ON di.personid=aa.personid AND di.controlid=aa.controlid AND di.courseid=aa.lucourseid\r\n\t\t\t\t\t\t\tORDER BY aa.personid,aa.ordernum";
					text += " SELECT * FROM #t3;";
					using (UnivCommand univCommand = da0.CreateCommand(text))
					{
						UnivDataReader univDataReader = univCommand.ExecuteReader2();
						dataTable = UnivOleDbFactory.ToDataTable(univDataReader.ToItemArrays(), dataTable, colMapping);
					}
					univTransaction.Commit();
					da0.Connection.Close();
					DataTableView.ShowDataTableView(dataTable);
				}
				catch (Exception ex)
				{
					if (univTransaction != null)
					{
						univTransaction.Rollback();
					}
					ReportFunction.MessageBoxShow(ex.ToString());
					exception = ex;
					flag = false;
				}
				finally
				{
					da0.Connection.Close();
					da0.SelectCommand.Transaction = null;
					univTransaction = null;
					exception = null;
					da0.Connection.Open();
					da0.Connection.Close();
				}
				if (flag)
				{
					if (dataTable != null)
					{
						try
						{
							DataView dataView = Reports.FormatAndMapToColumnsStudentDataPerAppointment(new DataView(dataTable), tripleDES, da0.Clone(), ref comboBoxData, staffNamesTable);
							DataTable table = dataView.Table;
							DataTable table2 = dataView.Table;
							DataView dataView2 = ReportFunction.CopyDataView(currentDataView);
							DataTable table3 = dataView2.Table;
							int count = table3.Columns.Count;
							for (int i = 4; i < table2.Columns.Count; i++)
							{
								table3.Columns.Add(Reports.GetUniqueColName(table3, table2.Columns[i].ColumnName), table2.Columns[i].DataType);
							}
							int num2 = table3.Columns.IndexOf("personid");
							bool flag2 = num2 >= 0 && table3.Columns[num2].DataType == typeof(int);
							for (int i = 0; i < table3.Rows.Count; i++)
							{
								DataRow dataRow = table3.Rows[i];
								if (dataRow[num2] != DBNull.Value)
								{
									int num3;
									if (flag2)
									{
										num3 = (int)dataRow[num2];
									}
									else
									{
										string text4 = dataRow[num2].ToString().Trim();
										if (text4.Length > 0)
										{
											try
											{
												num3 = int.Parse(text4);
											}
											catch
											{
												num3 = -1;
											}
										}
										else
										{
											num3 = -1;
										}
									}
									for (int j = 0; j < table2.Rows.Count; j++)
									{
										if (table2.Rows[j]["personid"] != DBNull.Value)
										{
											int num4;
											if (table2.Columns["personid"].DataType == typeof(int))
											{
												num4 = (int)table2.Rows[j]["personid"];
											}
											else
											{
												string s = table2.Rows[j]["personid"].ToString();
												try
												{
													num4 = int.Parse(s);
												}
												catch
												{
													num4 = -1;
												}
											}
											if (num4 == num3)
											{
												for (int k = count; k < table3.Columns.Count; k++)
												{
													table3.Rows[i][k] = table2.Rows[j][k - count + 4];
												}
											}
										}
									}
								}
							}
							dataView2 = new DataView(table3, dataView2.RowFilter, dataView2.Sort, dataView2.RowStateFilter);
							table3.Columns["personid"].ColumnMapping = MappingType.Hidden;
							exception = null;
							report.AddResult(dataView2);
						}
						catch (Exception ex2)
						{
							exception = ex2;
							report.LogError("CrossRefWithAppointment", ex2);
						}
					}
					else
					{
						exception = null;
					}
				}
			}
			else
			{
				exception = null;
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0003304C File Offset: 0x0003204C
		public static int GetLastIndexForGroup(DataTable t, int startIndex, string colName)
		{
			return ReportFunction.GetLastIndexForGroup(t.DefaultView, startIndex, colName);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0003306C File Offset: 0x0003206C
		public static int GetLastIndexForGroup(DataView dv, int startIndex, string colName)
		{
			int i = startIndex;
			string strB = dv[i][colName].ToString().Trim().ToLower();
			while (i < dv.Count)
			{
				string text = dv[i][colName].ToString().Trim().ToLower();
				if (text.CompareTo(strB) != 0)
				{
					break;
				}
				i++;
			}
			return i - 1;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000330E8 File Offset: 0x000320E8
		public static int GetLastIndexForGroup_int(DataTable t, int startIndex, string colName)
		{
			return ReportFunction.GetLastIndexForGroup_int(t.DefaultView, startIndex, colName);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00033108 File Offset: 0x00032108
		public static int GetLastIndexForGroup_int(DataView dv, int startIndex, string colName)
		{
			int i = startIndex;
			int num = (dv[i][colName] == DBNull.Value) ? -1 : ((int)dv[i][colName]);
			while (i < dv.Count)
			{
				int num2 = (dv[i][colName] == DBNull.Value) ? -1 : ((int)dv[i][colName]);
				if (num2 != num)
				{
					break;
				}
				i++;
			}
			return i - 1;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00033198 File Offset: 0x00032198
		public static void SortAttendeesIntoStaffFacilatorAndClientGroupsWithCounts(ref Report report, UnivDataAdapter da, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			currentDataView.Sort = "appointmentid,lastname,firstname,student_no";
			DataTable dataTable = currentDataView.Table.Clone();
			int count = dataTable.Columns.Count;
			dataTable.Columns.Add("StaffFacilitators");
			dataTable.Columns.Add("StaffFacilitators_count", typeof(int));
			dataTable.Columns.Add("Clients");
			dataTable.Columns.Add("Clients_count", typeof(int));
			dataTable.Columns.Add("Clients_count_without_noshow", typeof(int));
			dataTable.Columns.Add("Clients_count_only_noshow", typeof(int));
			bool flag = dataTable.Columns.Contains("misccode");
			if (SetupSubProgressBar != null)
			{
				SetupSubProgressBar(0, currentDataView.Count);
			}
			int num = 100;
			int lastIndexForGroup_int;
			for (int i = 0; i < currentDataView.Count; i = lastIndexForGroup_int + 1)
			{
				if (i % num == 0)
				{
					ReportFunction.CallIncrementProgressBar(IncrementSubProgressBar);
				}
				lastIndexForGroup_int = ReportFunction.GetLastIndexForGroup_int(currentDataView, i, "appointmentid");
				DataRow dataRow = dataTable.NewRow();
				DataRow row = currentDataView[i].Row;
				for (int j = 0; j < count; j++)
				{
					dataRow[j] = row[j];
				}
				int k = i;
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = new ArrayList();
				ArrayList arrayList3 = new ArrayList();
				int num2 = 0;
				int num3 = 0;
				while (k <= lastIndexForGroup_int)
				{
					int lastIndexForGroup = ReportFunction.GetLastIndexForGroup(currentDataView, k, "student_no");
					DataRow row2 = currentDataView[k].Row;
					string text = row2["student_no"].ToString().Trim();
					int num4 = (flag && row2["misccode"] != DBNull.Value) ? ((int)row2["misccode"]) : -1;
					if (!arrayList3.Contains(text))
					{
						arrayList3.Add(text);
						bool flag2 = true;
						for (int l = k; l <= lastIndexForGroup; l++)
						{
							DataRow row3 = currentDataView[l].Row;
							int num5 = (row3["groupid"] == DBNull.Value) ? 0 : ((int)currentDataView[l].Row["groupid"]);
							if (num4 == 1 || num5 == 2)
							{
								flag2 = false;
								break;
							}
						}
						string text2 = row2["firstname"].ToString().Trim();
						string text3 = row2["lastname"].ToString().Trim();
						if (row2["noshow"] != DBNull.Value && Convert.ToBoolean(row2["noshow"]))
						{
							num2++;
						}
						if (flag2)
						{
							arrayList2.Add(string.Concat(new string[]
							{
								text2,
								" ",
								text3,
								" (",
								text,
								")"
							}));
							if (num3 <= 0)
							{
								num3 = (int)row2["personid"];
							}
						}
						else
						{
							arrayList.Add(text2 + " " + text3);
						}
					}
					k = lastIndexForGroup + 1;
				}
				dataRow["StaffFacilitators"] = ReportFunction.GetArrayListCommaSeparated(arrayList);
				dataRow["StaffFacilitators_count"] = arrayList.Count;
				dataRow["Clients"] = ReportFunction.GetArrayListCommaSeparated(arrayList2);
				dataRow["Clients_count"] = arrayList2.Count;
				dataRow["Clients_count_without_noshow"] = arrayList2.Count - num2;
				dataRow["Clients_count_only_noshow"] = num2;
				dataRow["personid"] = num3;
				dataTable.Rows.Add(dataRow);
			}
			dataTable.Columns.Remove("groupid");
			dataTable.Columns.Remove("firstname");
			dataTable.Columns.Remove("lastname");
			dataTable.Columns.Remove("student_no");
			dataTable.Columns.Remove("misccode");
			dataTable.Columns.Remove("noshow");
			report.ReplaceDataView(currentDataView, dataTable.DefaultView);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00033664 File Offset: 0x00032664
		private static string GetArrayListCommaSeparated(ArrayList list)
		{
			return ReportFunction.GetArrayListCommaSeparated(list, ", ");
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00033684 File Offset: 0x00032684
		private static string GetArrayListCommaSeparated(ArrayList list, string delimiter)
		{
			string text = "";
			for (int i = 0; i < list.Count; i++)
			{
				string str = (string)list[i];
				if (i > 0)
				{
					text += delimiter;
				}
				text += str;
			}
			return text;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000336E0 File Offset: 0x000326E0
		private static string GetArrayCommaSeparated(string[] list)
		{
			return ReportFunction.GetArrayCommaSeparated(list, ", ");
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00033700 File Offset: 0x00032700
		private static string GetArrayCommaSeparated(string[] list, string delimiter)
		{
			string text = "";
			for (int i = 0; i < list.Length; i++)
			{
				string str = list[i];
				if (i > 0)
				{
					text += delimiter;
				}
				text += str;
			}
			return text;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00033750 File Offset: 0x00032750
		public static void AddBooleanCountAcrossColumns(ref Report report, string colNames, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			if (colNames.Length < 1)
			{
				foreach (object obj in currentDataView.Table.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					if (dataColumn.DataType == typeof(bool))
					{
						if (colNames.Length > 0)
						{
							colNames += ',';
						}
						colNames += dataColumn.ColumnName;
					}
				}
			}
			string[] array = colNames.Split(new char[]
			{
				','
			});
			ReportFunction.AddColumn(ref table, "MultipleCalculated", typeof(int));
			int columnIndex = table.Columns.Count - 1;
			Type typeFromHandle = typeof(bool);
			foreach (object obj2 in currentDataView)
			{
				DataRowView dataRowView = (DataRowView)obj2;
				DataRow row = dataRowView.Row;
				int num = 0;
				foreach (string text in array)
				{
					if (table.Columns[text].DataType == typeFromHandle)
					{
						if (row[text] != DBNull.Value && (bool)row[text])
						{
							num++;
						}
					}
					else if (row[text] != DBNull.Value && row[text].ToString().Trim().Length > 0)
					{
						num++;
					}
				}
				row[columnIndex] = num;
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000339C8 File Offset: 0x000329C8
		public static void ExplodeListData(ref Report report, UnivDataAdapter da, int listInd, bool returnLatestDateRowOnly, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar SetupSubProgressBar)
		{
			DataView currentDataView = report.GetCurrentDataView();
			DataTable table = currentDataView.Table;
			DataTable dataTable;
			if (listInd >= 0)
			{
				dataTable = table.Clone();
				dataTable.Columns.RemoveAt(listInd);
				da.SelectCommand.CommandText = "SELECT * FROM dynamiccontrols WHERE controlcaption=@cc AND controlcode=10";
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@cc", table.Columns[listInd].ColumnName);
				DataTable dataTable2 = new DataTable();
				da.Fill(dataTable2);
				if (dataTable2.Rows.Count > 0)
				{
					int num = (int)dataTable2.Rows[0]["setting1"];
					da.SelectCommand.CommandText = "SELECT lookuptext FROM lookuplists WHERE visible=1 AND lookupgroupid=15 ORDER BY ordernum,lookuptext";
					dataTable2 = new DataTable();
					da.Fill(dataTable2);
					if (dataTable2.Rows.Count > 0)
					{
						dataTable2.Rows.Add(new object[]
						{
							"Date"
						});
						int count = dataTable.Columns.Count;
						foreach (object obj in dataTable2.Rows)
						{
							DataRow dataRow = (DataRow)obj;
							dataTable.Columns.Add((string)dataRow[0]);
						}
						foreach (object obj2 in table.Rows)
						{
							DataRow dataRow2 = (DataRow)obj2;
							string[] array = dataRow2[listInd].ToString().Trim().Split(new char[]
							{
								','
							});
							if (array == null || array.Length < 1)
							{
								DataRow dataRow3 = dataTable.NewRow();
								dataTable.Rows.Add(dataRow3);
								ReportFunction.CopyDataRowSafe(dataRow2, ref dataRow3);
							}
							else
							{
								DataRow dataRow3 = null;
								string text = "";
								foreach (string text2 in array)
								{
									if (returnLatestDateRowOnly)
									{
										if (dataRow3 == null)
										{
											dataRow3 = dataTable.NewRow();
											dataTable.Rows.Add(dataRow3);
											ReportFunction.CopyDataRowSafe(dataRow2, ref dataRow3);
										}
										string[] array3 = text2.Split(new char[]
										{
											'|'
										});
										if (array3 != null && array3.Length > 0)
										{
											string text3 = array3[array3.Length - 1].Trim();
											if (text.Length < 1 || text3.CompareTo(text) >= 0)
											{
												text = text3;
												int num2 = 0;
												while (count + num2 < dataTable.Columns.Count && num2 < array3.Length)
												{
													dataRow3[count + num2] = array3[num2].Trim();
													num2++;
												}
											}
										}
									}
									else
									{
										dataRow3 = dataTable.NewRow();
										dataTable.Rows.Add(dataRow3);
										ReportFunction.CopyDataRowSafe(dataRow2, ref dataRow3);
										string[] array3 = text2.Split(new char[]
										{
											'|'
										});
										int num2 = 0;
										while (count + num2 < dataTable.Columns.Count && num2 < array3.Length)
										{
											dataRow3[count + num2] = array3[num2].Trim();
											num2++;
										}
									}
								}
							}
						}
					}
					else
					{
						dataTable = table;
					}
				}
				else
				{
					dataTable = table;
				}
			}
			else
			{
				dataTable = table;
			}
			DataView dataView = new DataView(dataTable);
			if (currentDataView.Sort.Length > 0)
			{
				string[] array4 = currentDataView.Sort.Split(new char[]
				{
					','
				});
				string text4 = "";
				for (int j = 0; j < array4.Length; j++)
				{
					string text5 = array4[j];
					if (dataTable.Columns.Contains(text5))
					{
						if (j > 0)
						{
							text4 += ",";
						}
						text4 += text5;
					}
				}
				dataView.Sort = text4;
			}
			report.ReplaceDataView(currentDataView, dataView);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00033ECC File Offset: 0x00032ECC
		private static void CopyDataRowSafe(DataRow dr1_from, ref DataRow dr2_to)
		{
			DataTable table = dr1_from.Table;
			DataTable table2 = dr2_to.Table;
			for (int i = 0; i < table.Columns.Count; i++)
			{
				DataColumn dataColumn = table.Columns[i];
				int num = table2.Columns.IndexOf(dataColumn.ColumnName);
				if (num >= 0)
				{
					dr2_to[num] = dr1_from[i];
				}
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00033F48 File Offset: 0x00032F48
		public static string DataTableToString(DataTable t)
		{
			string text = "";
			foreach (object obj in t.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (text.Length > 0)
				{
					text += ",";
				}
				text += dataColumn.ColumnName;
			}
			text += System.Environment.NewLine;
			text += "============";
			text += System.Environment.NewLine;
			for (int i = 0; i < t.Rows.Count; i++)
			{
				DataRow dataRow = t.Rows[i];
				if (dataRow.RowState != DataRowState.Deleted)
				{
					for (int j = 0; j < t.Columns.Count; j++)
					{
						if (j > 0)
						{
							text += ",";
						}
						text += dataRow[j].ToString().Replace(',', '`').Replace(System.Environment.NewLine, "\\n");
					}
					text += System.Environment.NewLine;
				}
				else
				{
					text = text + "DELETED ROW" + System.Environment.NewLine;
				}
			}
			return text;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000340D8 File Offset: 0x000330D8
		public static DataSet ImportUpdateStudentPreview(UnivDataAdapter da, string snum, TripleDESEncryptionClass tripleDES, out ArrayList errors, out int reportId_importCourses, out string pwd)
		{
			string parameterValue = string.Concat(new string[]
			{
				406.ToString(),
				",",
				405.ToString(),
				",",
				408.ToString(),
				",",
				441.ToString(),
				",",
				407.ToString()
			});
			da.SelectCommand.CommandText = "SELECT settingcode,settingvalue,settingstringvalue FROM settingsgroups WHERE settingcode IN (SELECT orderid AS settingcode FROM splitorderids(@settingcodes,','))";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@settingcodes", parameterValue);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			int reportId_preview = 0;
			int reportId_import = 0;
			int reportId_getgroups = 0;
			reportId_importCourses = 0;
			pwd = "";
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow[0];
				int num2 = num;
				switch (num2)
				{
				case 405:
					reportId_import = (int)dataRow[1];
					break;
				case 406:
					reportId_preview = (int)dataRow[1];
					break;
				case 407:
					pwd = dataRow[2].ToString();
					break;
				case 408:
					reportId_getgroups = (int)dataRow[1];
					break;
				default:
					if (num2 == 441)
					{
						reportId_importCourses = (int)dataRow[1];
					}
					break;
				}
			}
			if (pwd.Length > 0)
			{
				byte[] inputInBytes = ClockWorkCore.base64Decode(pwd);
				pwd = tripleDES.Decrypt(inputInBytes);
			}
			return ReportFunction.ImportUpdateStudentPreview(da, snum, tripleDES, out errors, reportId_preview, reportId_import, reportId_getgroups, reportId_importCourses, pwd);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00034308 File Offset: 0x00033308
		public static DataSet ImportUpdateStudentPreview(UnivDataAdapter da, Settings settings, string snum, TripleDESEncryptionClass tripleDES, out ArrayList errors)
		{
			DataSet result;
			try
			{
				int setting = OldUserSettingClientManager.CurrentInstance.GetSetting(406);
				int setting2 = OldUserSettingClientManager.CurrentInstance.GetSetting(405);
				int setting3 = OldUserSettingClientManager.CurrentInstance.GetSetting(408);
				int setting4 = OldUserSettingClientManager.CurrentInstance.GetSetting(441);
				string text = OldUserSettingClientManager.CurrentInstance.GetSettingString(407, "").Trim();
				if (text.Length > 0)
				{
					byte[] inputInBytes = ClockWorkCore.base64Decode(text);
					text = tripleDES.Decrypt(inputInBytes);
				}
				result = ReportFunction.ImportUpdateStudentPreview(da, snum, tripleDES, out errors, setting, setting2, setting3, setting4, text);
			}
			catch (Exception ex)
			{
				throw new Exception("ImportUpdateStudentPreview0: " + ex.Message, ex.InnerException);
			}
			return result;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000343E4 File Offset: 0x000333E4
		public static DataSet ImportUpdateStudentPreview(UnivDataAdapter da, string snum, TripleDESEncryptionClass tripleDES, out ArrayList errors, int reportId_preview, int reportId_import, int reportId_getgroups, int reportId_importCourses, string pwd)
		{
			int num = 0;
			DataSet result;
			try
			{
				if (reportId_preview > 0 && reportId_import > 0)
				{
					num = 10;
					if (!string.IsNullOrEmpty(snum))
					{
						num = 20;
						ArrayList arrayList = new ArrayList();
						if (!string.IsNullOrEmpty(pwd))
						{
							num = 30;
							TripleDESEncryptionClass tripleDESEncryptionClass = new TripleDESEncryptionClass(EncryptionType.TripleDES_192bit, pwd);
							num = 40;
							byte[] binaryData = tripleDESEncryptionClass.Encrypt(snum);
							num = 50;
							if (ReportFunction.UseUpdatedCreateTripleDES)
							{
								num = 60;
								tripleDESEncryptionClass = ReportFunction.CreateTripleDES(da, "tripledes_192bit", pwd, tripleDES);
								num = 70;
								binaryData = tripleDESEncryptionClass.Encrypt(snum);
							}
							num = 80;
							arrayList.Add(new Variable("sne__base64", ClockWorkCore.base64Encode(binaryData)));
							num = 90;
							arrayList.Add(new Variable("studentnumberencryptdatasync", snum));
							num = 100;
							arrayList.Add(new Variable("studentno", snum));
							num = 110;
						}
						else
						{
							num = 120;
							arrayList.Add(new Variable("studentno", snum));
							num = 130;
							arrayList.Add(new Variable("studentnumberencryptdatasync", snum));
							num = 140;
						}
						Report report = ReportFunction.RunReport(false, da, reportId_preview, tripleDES, arrayList, out errors, false, false);
						num = 150;
						if (errors != null && errors.Count > 0)
						{
							foreach (object obj in errors)
							{
								CWLogger.Logger.Error("Report errors: " + obj.ToString());
							}
						}
						DataView currentDataView = report.GetCurrentDataView();
						num = 160;
						DataSet dataSet = new DataSet();
						currentDataView.Table.TableName = "data";
						num = 170;
						dataSet.Tables.Add(currentDataView.Table);
						num = 180;
						if (reportId_importCourses > 0)
						{
							num = 190;
						}
						num = 200;
						return dataSet;
					}
				}
				errors = null;
				result = null;
			}
			catch (Exception ex)
			{
				throw new Exception("ImportUpdateStudentPreview (x=" + num.ToString() + "): " + ex.Message, ex.InnerException);
			}
			return result;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0003466C File Offset: 0x0003366C
		public static DataSet ImportUpdateStudentPreview(UnivDataAdapter da, string snum, TripleDESEncryptionClass tripleDES, out ArrayList errors, int reportId_preview, int reportId_import, int reportId_getgroups, int reportId_importCourses, string pwd, bool suppressGuiMessages)
		{
			int num = 0;
			DataSet result;
			try
			{
				if (reportId_preview > 0 && reportId_import > 0)
				{
					num = 10;
					if (!string.IsNullOrEmpty(snum))
					{
						num = 20;
						ArrayList arrayList = new ArrayList();
						if (!string.IsNullOrEmpty(pwd))
						{
							num = 30;
							TripleDESEncryptionClass tripleDESEncryptionClass = new TripleDESEncryptionClass(EncryptionType.TripleDES_192bit, pwd);
							num = 40;
							byte[] binaryData = tripleDESEncryptionClass.Encrypt(snum);
							num = 50;
							if (ReportFunction.UseUpdatedCreateTripleDES)
							{
								num = 60;
								tripleDESEncryptionClass = ReportFunction.CreateTripleDES(da, "tripledes_192bit", pwd, tripleDES);
								num = 70;
								binaryData = tripleDESEncryptionClass.Encrypt(snum);
							}
							num = 80;
							arrayList.Add(new Variable("sne__base64", ClockWorkCore.base64Encode(binaryData)));
							num = 90;
							arrayList.Add(new Variable("studentnumberencryptdatasync", snum));
							num = 100;
							arrayList.Add(new Variable("studentno", snum));
							num = 110;
						}
						else
						{
							num = 120;
							arrayList.Add(new Variable("studentno", snum));
							num = 130;
							arrayList.Add(new Variable("studentnumberencryptdatasync", snum));
							num = 140;
						}
						Report report = ReportFunction.RunReport(false, da, reportId_preview, tripleDES, arrayList, out errors, false, suppressGuiMessages);
						num = 150;
						if (errors != null && errors.Count > 0)
						{
							foreach (object obj in errors)
							{
								CWLogger.Logger.Error("Report errors: " + obj.ToString());
							}
						}
						DataView currentDataView = report.GetCurrentDataView();
						num = 160;
						DataSet dataSet = new DataSet();
						currentDataView.Table.TableName = "data";
						num = 170;
						dataSet.Tables.Add(currentDataView.Table);
						num = 180;
						if (reportId_importCourses > 0)
						{
							num = 190;
						}
						num = 200;
						return dataSet;
					}
				}
				errors = null;
				result = null;
			}
			catch (Exception ex)
			{
				throw new Exception("ImportUpdateStudentPreview (x=" + num.ToString() + "): " + ex.Message, ex.InnerException);
			}
			return result;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000348F4 File Offset: 0x000338F4
		public static DataView ImportUpdateStudents(DataView dv, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, IncrementProgressBar ipb, SetupProgressBar spb)
		{
			return ReportFunction.ImportUpdateStudents(dv, da, tripleDES, ipb, spb, false);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00034940 File Offset: 0x00033940
		public static DataView ImportUpdateStudents2(DataView dv, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, bool suppressGuiMessages)
		{
			DataTable table = dv.Table;
			DataView result;
			if (table.Columns.Count < 1)
			{
				result = dv;
			}
			else
			{
				List<PersonBaseDTO> list = new List<PersonBaseDTO>();
				bool flag = table.Columns.Contains("middlename");
				for (int i = 0; i < table.Rows.Count; i++)
				{
					DataRow dataRow = table.Rows[i];
					if (dataRow["personid"] != DBNull.Value)
					{
						PersonBaseDTO student = new PersonBaseDTO
						{
							Student_no = dataRow["student_no"].ToString().ToUpper().Trim(),
							FirstName = dataRow["firstname"].ToString().Trim(),
							LastName = dataRow["lastname"].ToString().Trim(),
							MiddleName = (flag ? dataRow["middlename"].ToString().Trim() : ""),
							PersonId = (int)dataRow["personid"]
						};
						if (list.Find((PersonBaseDTO s) => s.PersonId == student.PersonId) == null)
						{
							list.Add(student);
						}
					}
				}
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = new ArrayList();
				DataTable dataTable = new DataTable();
				da.SelectCommand.CommandText = "SELECT s.settinggroupid,s.groupid,s.settingcode,s.settingvalue,s.settingstringvalue,0 AS ordernum FROM settingsgroups s WHERE s.groupid=-1";
				da.SelectCommand.Parameters.Clear();
				da.Fill(dataTable);
				DataTable dataTable2 = dataTable.Clone();
				int[] array = new int[1];
				Settings settings = new Settings(array, dataTable2, dataTable, -1, da);
				DataTable dataTable3 = new DataTable();
				dataTable3.Columns.Add("student_no");
				dataTable3.Columns.Add("pid", typeof(int));
				dataTable3.Columns.Add("note");
				foreach (PersonBaseDTO personBaseDTO in list)
				{
					string text = "";
					try
					{
						int num;
						ReportFunction.ImportUpdateStudent(settings, -1, personBaseDTO.Student_no, personBaseDTO.FirstName, personBaseDTO.MiddleName, personBaseDTO.LastName, da, tripleDES, out num, out arrayList, out arrayList2, suppressGuiMessages);
						dataTable3.Rows.Add(new object[]
						{
							personBaseDTO.Student_no,
							personBaseDTO.PersonId,
							text
						});
					}
					catch (Exception ex)
					{
						text = ex.ToString();
						CWLogger.Logger.Error("ReportFunctions:ReportFunction:BatchDataSync:Error={0}", text);
						dataTable3.Rows.Add(new object[]
						{
							personBaseDTO.Student_no,
							personBaseDTO.PersonId,
							text
						});
					}
				}
				result = dataTable3.DefaultView;
			}
			return result;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00034C98 File Offset: 0x00033C98
		public static DataView ImportUpdateStudents(DataView dv, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, IncrementProgressBar ipb, SetupProgressBar spb, bool suppressGuiMessages)
		{
			DataTable table = dv.Table;
			DataTable dataTable = new DataTable();
			da.SelectCommand.CommandText = "SELECT s.settinggroupid,s.groupid,s.settingcode,s.settingvalue,s.settingstringvalue,0 AS ordernum FROM settingsgroups s WHERE s.groupid=-1";
			da.SelectCommand.Parameters.Clear();
			da.Fill(dataTable);
			DataTable dataTable2 = dataTable.Clone();
			DataTable dataTable3 = new DataTable();
			dataTable3.Columns.Add("student_no");
			dataTable3.Columns.Add("results");
			int[] array = new int[1];
			Settings settings = new Settings(array, dataTable2, dataTable, -1, da);
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			if (spb != null)
			{
				spb(0, table.Rows.Count);
			}
			int num = 0;
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string text;
				string text2;
				try
				{
					text = dataRow["student_no"].ToString().Trim();
					int num2 = 0;
					try
					{
						int num3 = ReportFunction.ShowIncrementAmount(ref num);
						if (num3 > 0 && ipb != null)
						{
							ipb(num3);
						}
						int num4 = (int)dataRow["personid"];
						string fn = dataRow["firstname"].ToString().Trim();
						string md = table.Columns.Contains("middlename") ? dataRow["middlename"].ToString().Trim() : "";
						string ln = dataRow["lastname"].ToString().Trim();
						num2 = 1;
						using (UnivConnection univConnection = UnivOleDbFactory.CreateConnection(da.Connection.ConnectionString))
						{
							using (UnivDataAdapter univDataAdapter = univConnection.CreateDataAdapter())
							{
								ReportFunction.ImportUpdateStudent(settings, -1, text, fn, md, ln, univDataAdapter, tripleDES, out num4, out arrayList, out arrayList2, suppressGuiMessages);
							}
						}
						text2 = "";
					}
					catch (Exception ex)
					{
						text2 = "Fail: " + num2.ToString() + ": " + ex.ToString();
					}
				}
				catch (Exception ex2)
				{
					text = null;
					text2 = "No snum: " + ex2.ToString();
				}
				dataTable3.Rows.Add(new object[]
				{
					(text == null) ? "NULL" : text,
					text2
				});
				if (!string.IsNullOrEmpty(text2))
				{
					CWLogger.Logger.Error("ImportUpdateStudents:Msg={0}", text2);
				}
			}
			return dataTable3.DefaultView;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00034FEC File Offset: 0x00033FEC
		public static DataSet ImportUpdateStudent(Settings settings, int whoami_pid, string snum, string fn, string md, string ln, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, out int pid, out ArrayList errors)
		{
			ArrayList arrayList;
			return ReportFunction.ImportUpdateStudent(settings, whoami_pid, snum, fn, md, ln, da, tripleDES, out pid, out errors, out arrayList);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00035018 File Offset: 0x00034018
		public static DataSet ImportUpdateStudent(Settings settings, int whoami_pid, string snum, string fn, string md, string ln, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, out int pid, out ArrayList errors, out ArrayList warnings)
		{
			return ReportFunction.ImportUpdateStudent(settings, whoami_pid, snum, fn, md, ln, da, tripleDES, out pid, out errors, out warnings, false);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00035044 File Offset: 0x00034044
		public static DataSet ImportUpdateStudent(Settings settings, int whoami_pid, string snum, string fn, string md, string ln, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, out int pid, out ArrayList errors, out ArrayList warnings, bool suppressGuiMessages)
		{
			int setting = OldUserSettingClientManager.CurrentInstance.GetSetting(406);
			int setting2 = OldUserSettingClientManager.CurrentInstance.GetSetting(405);
			int setting3 = OldUserSettingClientManager.CurrentInstance.GetSetting(408);
			int setting4 = OldUserSettingClientManager.CurrentInstance.GetSetting(441);
			string text = OldUserSettingClientManager.CurrentInstance.GetSettingString(407, "").Trim();
			if (text.Length > 0)
			{
				byte[] inputInBytes = ClockWorkCore.base64Decode(text);
				text = tripleDES.Decrypt(inputInBytes);
			}
			bool flag = OldUserSettingClientManager.CurrentInstance.IntToBool(OldUserSettingClientManager.CurrentInstance.GetSetting(479));
			return ReportFunction.ImportUpdateStudent(setting, setting2, setting3, setting4, text, whoami_pid, snum, fn, md, ln, da, tripleDES, out pid, out errors, out warnings, 0, suppressGuiMessages);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00035118 File Offset: 0x00034118
		public static DataSet ImportUpdateStudent(int reportId_preview, int reportId_import, int reportId_getgroups, int reportId_importCourses, string pwd, int whoami_pid, string snum, string fn, string md, string ln, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, out int pid, out ArrayList errors, out ArrayList warnings)
		{
			return ReportFunction.ImportUpdateStudent(reportId_preview, reportId_import, reportId_getgroups, reportId_importCourses, pwd, whoami_pid, snum, fn, md, ln, da, tripleDES, out pid, out errors, out warnings, 0);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0003514C File Offset: 0x0003414C
		public static DataSet ImportUpdateStudent(int reportId_preview, int reportId_import, int reportId_getgroups, int reportId_importCourses, string pwd, int whoami_pid, string snum, string fn, string md, string ln, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, out int pid, out ArrayList errors, out ArrayList warnings, int dontImportCoursesCid)
		{
			return ReportFunction.ImportUpdateStudent(reportId_preview, reportId_import, reportId_getgroups, reportId_importCourses, pwd, whoami_pid, snum, fn, md, ln, da, tripleDES, out pid, out errors, out warnings, dontImportCoursesCid, false);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00035180 File Offset: 0x00034180
		public static DataSet ImportUpdateStudent(int reportId_preview, int reportId_import, int reportId_getgroups, int reportId_importCourses, string pwd, int whoami_pid, string snum, string fn, string md, string ln, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, out int pid, out ArrayList errors, out ArrayList warnings, int dontImportCoursesCid, bool suppressGuiMessages)
		{
			int num = 0;
			DataSet result;
			try
			{
				warnings = new ArrayList();
				byte[] array = tripleDES.Encrypt(fn.Trim());
				byte[] array2 = tripleDES.Encrypt(ln.Trim());
				byte[] array3 = tripleDES.Encrypt(md.Trim());
				num = 1;
				ArrayList arrayList = new ArrayList();
				if (snum.Trim().Length > 0)
				{
					if (pwd != null && pwd.Length > 0)
					{
						num = 2;
						TripleDESEncryptionClass tripleDESEncryptionClass = new TripleDESEncryptionClass(EncryptionType.TripleDES_192bit, pwd);
						byte[] binaryData = tripleDESEncryptionClass.Encrypt(snum);
						num = 3;
						if (ReportFunction.UseUpdatedCreateTripleDES)
						{
							num = 4;
							tripleDESEncryptionClass = ReportFunction.CreateTripleDES(da, "tripledes_192bit", pwd, tripleDES);
							binaryData = tripleDESEncryptionClass.Encrypt(snum);
							num = 5;
						}
						num = 6;
						arrayList.Add(new Variable("sne__base64", ClockWorkCore.base64Encode(binaryData)));
						num = 7;
						arrayList.Add(new Variable("studentnumberencryptdatasync", snum));
						arrayList.Add(new Variable("studentno", snum));
						num = 8;
					}
					else
					{
						num = 9;
						arrayList.Add(new Variable("studentno", snum));
						arrayList.Add(new Variable("studentnumberencryptdatasync", snum));
						num = 10;
					}
					DataView dataView;
					if (reportId_getgroups > 0)
					{
						num = 12;
						Report report = ReportFunction.RunReport(false, da, reportId_getgroups, tripleDES, arrayList, out errors, false, suppressGuiMessages);
						num = 13;
						dataView = report.GetCurrentDataView();
						num = 14;
					}
					else
					{
						num = 15;
						errors = null;
						dataView = new DataView(new DataTable
						{
							Columns = 
							{
								{
									"gid",
									typeof(int)
								}
							}
						});
						num = 16;
					}
					if (errors == null || errors.Count < 1)
					{
						num = 17;
						ArrayList arrayList2 = new ArrayList();
						arrayList2.Add(1);
						foreach (object obj in dataView.Table.Rows)
						{
							DataRow dataRow = (DataRow)obj;
							int num2 = (int)dataRow[0];
							if (!arrayList2.Contains(num2))
							{
								arrayList2.Add(num2);
							}
						}
						num = 18;
						int[] array4 = new int[arrayList2.Count];
						for (int i = 0; i < arrayList2.Count; i++)
						{
							array4[i] = (int)arrayList2[i];
						}
						num = 19;
						pid = User.CreateClientAccount(da, tripleDES, whoami_pid, snum, fn, md, ln, DateTime.Now, array4);
						num = 20;
						if (pid > 0)
						{
							num = 21;
							Report report2 = ReportFunction.RunReport(false, da, reportId_import, tripleDES, arrayList, out errors, false, suppressGuiMessages);
							num = 22;
							DataView dataView2 = (report2 == null) ? null : report2.GetCurrentDataView();
							if (errors != null && errors.Count > 0)
							{
								num = 23;
								result = null;
							}
							else
							{
								num = 24;
								if (dataView2 != null)
								{
									num = 25;
									DataTable table = dataView2.Table;
									if (table.Rows.Count > 0 && table.Columns.Contains("firstname"))
									{
										num = 26;
										DataRow dataRow2 = table.Rows[0];
										string text = dataRow2["firstname"].ToString();
										string text2 = dataRow2["lastname"].ToString();
										string text3 = table.Columns.Contains("middlename") ? dataRow2["middlename"].ToString() : "";
										num = 27;
										if ((!string.IsNullOrEmpty(text) && !fn.Equals(text, StringComparison.OrdinalIgnoreCase)) || (!string.IsNullOrEmpty(text2) && !ln.Equals(text2, StringComparison.OrdinalIgnoreCase)) || (!string.IsNullOrEmpty(text3) && !md.Equals(text3, StringComparison.OrdinalIgnoreCase)))
										{
											num = 28;
											string text4 = "UPDATE people SET firstname=@fne,lastname=@lne,middlename=@mde WHERE personid=@pid";
											if (string.IsNullOrEmpty(text))
											{
												text4 = text4.Replace("@fne", "firstname");
											}
											if (string.IsNullOrEmpty(text2))
											{
												text4 = text4.Replace("@lne", "lastname");
											}
											if (string.IsNullOrEmpty(text3))
											{
												text4 = text4.Replace("@mde", "middlename");
											}
											da.SelectCommand.CommandText = text4;
											da.SelectCommand.Parameters.Clear();
											if (!string.IsNullOrEmpty(text))
											{
												da.SelectCommand.Parameters.Add("@fne", ClockWorkCore.StringToBytes(text, true, tripleDES));
											}
											if (!string.IsNullOrEmpty(text2))
											{
												da.SelectCommand.Parameters.Add("@lne", ClockWorkCore.StringToBytes(text2, true, tripleDES));
											}
											if (!string.IsNullOrEmpty(text3))
											{
												da.SelectCommand.Parameters.Add("@mde", ClockWorkCore.StringToBytes(text3, true, tripleDES));
											}
											da.SelectCommand.Parameters.Add("@pid", pid);
											da.Fill(new DataTable());
											num = 29;
										}
									}
								}
								num = 30;
								DataSet dataSet = new DataSet();
								dataView2.Table.TableName = "data";
								dataSet.Tables.Add(dataView2.Table);
								num = 31;
								if (reportId_importCourses > 0)
								{
									num = 32;
									if (dontImportCoursesCid > 0)
									{
										num = 33;
										da.SelectCommand.CommandText = "SELECT controlvalue FROM maininfops WHERE controlid=@cid AND personid=@pid";
										da.SelectCommand.Parameters.Clear();
										da.SelectCommand.Parameters.Add("@cid", dontImportCoursesCid);
										da.SelectCommand.Parameters.Add("@pid", pid);
										DataTable dataTable = new DataTable();
										da.Fill(dataTable);
										num = 34;
										if (dataTable.Rows.Count > 0 && (int)dataTable.Rows[0][0] != 0)
										{
											return dataSet;
										}
									}
									num = 35;
									Report report3 = ReportFunction.RunReport(false, da, reportId_importCourses, tripleDES, arrayList, out errors, false, false);
									num = 36;
									DataView currentDataView = report3.GetCurrentDataView();
									num = 37;
									if (currentDataView != null)
									{
										num = 38;
										currentDataView.Table.TableName = "courses";
										dataSet.Tables.Add(currentDataView.Table);
										num = 39;
									}
								}
								num = 40;
								result = dataSet;
							}
						}
						else
						{
							result = null;
						}
					}
					else
					{
						pid = -1;
						result = null;
					}
				}
				else
				{
					num = 11;
					errors = new ArrayList();
					errors.Add("Missing student number");
					pid = -1;
					result = null;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("ImportUpdateStudentPreview: " + num.ToString() + ": " + ex.Message, ex.InnerException);
			}
			return result;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00035928 File Offset: 0x00034928
		public static string GetColumnNamesFromUser(DataTable t0, string autoSelectPossibleMatches, string title, string caption, bool multipleSelect)
		{
			return ReportFunction.GetColumnNamesFromUser(t0, autoSelectPossibleMatches, title, caption, multipleSelect, null);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00035948 File Offset: 0x00034948
		public static string GetColumnNamesFromUser(DataTable t0, string autoSelectPossibleMatches, string title, string caption, bool multipleSelect, IWin32Window owner)
		{
			string result;
			if (t0 == null)
			{
				result = null;
			}
			else
			{
				ArrayList arrayList;
				if (autoSelectPossibleMatches == null || autoSelectPossibleMatches.Length < 1)
				{
					arrayList = null;
				}
				else
				{
					string[] array = autoSelectPossibleMatches.Split(new char[]
					{
						','
					});
					if (array.Length > 0)
					{
						arrayList = new ArrayList(array.Length);
						foreach (string text in array)
						{
							arrayList.Add(text.Trim().ToLower());
						}
					}
					else
					{
						arrayList = null;
					}
				}
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("description");
				ArrayList arrayList2 = new ArrayList();
				for (int j = 0; j < t0.Columns.Count; j++)
				{
					string columnName = t0.Columns[j].ColumnName;
					if (arrayList != null && arrayList.Count > 0)
					{
						string item = columnName.Trim().ToLower();
						if (arrayList.Contains(item))
						{
							arrayList2.Add(j);
						}
					}
					dataTable.Rows.Add(new object[]
					{
						columnName
					});
				}
				if (dataTable.Rows.Count > 0)
				{
					InputList inputList = new InputList(title, caption, dataTable, "description", arrayList2, multipleSelect);
					DialogResult dialogResult = inputList.ShowDialog(owner);
					if (dialogResult == DialogResult.OK && inputList.listBox1.SelectedItems.Count > 0)
					{
						string text = "";
						foreach (object obj in inputList.listBox1.SelectedItems)
						{
							DataRowView dataRowView = (DataRowView)obj;
							if (text.Length > 0)
							{
								text += ",";
							}
							text += (string)dataRowView.Row[0];
						}
						return text;
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00035BC0 File Offset: 0x00034BC0
		public static Report ExtractUnique(bool anything, DataView currDV, IWin32Window owner)
		{
			Report result;
			if (currDV != null)
			{
				string text2;
				if (!currDV.Table.Columns.Contains("student_no") || anything)
				{
					string autoSelectPossibleMatches = "student_no";
					foreach (object obj in currDV.Table.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj;
						string text = dataColumn.ColumnName.ToLower();
						if (text.IndexOf("student_no") >= 0)
						{
							autoSelectPossibleMatches = dataColumn.ColumnName;
							break;
						}
					}
					text2 = ReportFunction.GetColumnNamesFromUser(currDV.Table, autoSelectPossibleMatches, "Extract Unique Students", "Please specify the column that contains the student numbers:", false, owner);
					if (text2 == null)
					{
						return null;
					}
				}
				else
				{
					text2 = "student_no";
				}
				Report report = new Report(currDV);
				ReportFunction.ExtractUniqueRows(ref report, new string[]
				{
					text2
				});
				result = report;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00035CF8 File Offset: 0x00034CF8
		private static bool IntArrayContains(int[] array, int integer)
		{
			bool result;
			if (array == null)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == integer)
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00035D40 File Offset: 0x00034D40
		public static ArrayList GetColumnNamesFromUser(DataTable table, int[] excludeColIndices, int[] autoSelectColIndices, string title, string caption, bool multipleSelect, string checkedColTitle, IWin32Window owner)
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("colind", typeof(int));
			dataTable.Columns["colind"].ColumnMapping = MappingType.Hidden;
			dataTable.Columns.Add("Column_name");
			for (int i = 0; i < table.Columns.Count; i++)
			{
				if (!ReportFunction.IntArrayContains(excludeColIndices, i))
				{
					object[] values = new object[]
					{
						i,
						table.Columns[i].ColumnName
					};
					dataTable.Rows.Add(values);
				}
			}
			InputCheckedList inputCheckedList = new InputCheckedList(dataTable, autoSelectColIndices, title, caption, "Column_name");
			DialogResult dialogResult = inputCheckedList.ShowDialog();
			ArrayList result;
			if (dialogResult == DialogResult.OK && inputCheckedList.ListBox.CheckedItems.Count > 0)
			{
				DataRow[] checkedDataRows = inputCheckedList.GetCheckedDataRows();
				ArrayList arrayList = new ArrayList(checkedDataRows.Length);
				foreach (DataRow dataRow in checkedDataRows)
				{
					arrayList.Add((string)dataRow[1]);
				}
				result = arrayList;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00035E94 File Offset: 0x00034E94
		public static Report RunReport(UnivDataAdapter da, int searchInfoID, TripleDESEncryptionClass tripleDES, ArrayList variables, out ArrayList errors, bool getUserInputForVariableValues)
		{
			return ReportFunction.RunReport(da, searchInfoID, tripleDES, variables, out errors, getUserInputForVariableValues, false);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00035EB4 File Offset: 0x00034EB4
		public static Report RunReport(string dbName, DataRow reportDR, UnivDataAdapter da, DataSet comboBoxData, DataSet lookupTablesForControls, ArrayList variables, DataTable sessions, object[] yearStartEnd, DataTable dynamicScreenNonDataControlsTable, DataTable searchCustomTable, TripleDESEncryptionClass tripleDES, IncrementProgressBar IncrementMainProgressBar, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar2 SetupMainProgressBar, SetupProgressBar SetupSubProgressBar, DataTable staffNamesTable, int whoAmIPersonID, TechnoProReports technoProReports, out ArrayList errors, bool getUserInputForVariableValues, EventHandler reportStartedHandler)
		{
			return ReportFunction.RunReport(dbName, reportDR, da, comboBoxData, lookupTablesForControls, variables, sessions, yearStartEnd, dynamicScreenNonDataControlsTable, searchCustomTable, tripleDES, IncrementMainProgressBar, IncrementSubProgressBar, SetupMainProgressBar, SetupSubProgressBar, staffNamesTable, whoAmIPersonID, technoProReports, out errors, getUserInputForVariableValues, reportStartedHandler, null);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00035EF4 File Offset: 0x00034EF4
		public static Report RunReport(string dbName, DataRow reportDR, UnivDataAdapter da, DataSet comboBoxData, DataSet lookupTablesForControls, ArrayList variables, DataTable sessions, object[] yearStartEnd, DataTable dynamicScreenNonDataControlsTable, DataTable searchCustomTable, TripleDESEncryptionClass tripleDES, IncrementProgressBar IncrementMainProgressBar, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar2 SetupMainProgressBar, SetupProgressBar SetupSubProgressBar, DataTable staffNamesTable, int whoAmIPersonID, TechnoProReports technoProReports, out ArrayList errors, bool getUserInputForVariableValues)
		{
			EventHandler reportStartedHandler = null;
			return ReportFunction.RunReport(dbName, reportDR, da, comboBoxData, lookupTablesForControls, variables, sessions, yearStartEnd, dynamicScreenNonDataControlsTable, searchCustomTable, tripleDES, IncrementMainProgressBar, IncrementSubProgressBar, SetupMainProgressBar, SetupSubProgressBar, staffNamesTable, whoAmIPersonID, technoProReports, out errors, getUserInputForVariableValues, reportStartedHandler, null, false, null);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00035F38 File Offset: 0x00034F38
		public static Report RunReport(string dbName, DataRow reportDR, UnivDataAdapter da, DataSet comboBoxData, DataSet lookupTablesForControls, ArrayList variables, DataTable sessions, object[] yearStartEnd, DataTable dynamicScreenNonDataControlsTable, DataTable searchCustomTable, TripleDESEncryptionClass tripleDES, IncrementProgressBar IncrementMainProgressBar, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar2 SetupMainProgressBar, SetupProgressBar SetupSubProgressBar, DataTable staffNamesTable, int whoAmIPersonID, TechnoProReports technoProReports, out ArrayList errors, bool getUserInputForVariableValues, EventHandler reportStartedHandler, ReportParameterCollection reportParameterCollection, bool suppressGuiMessages, DataTable overrideFunctionsTable)
		{
			return ReportFunction.RunReport(true, dbName, reportDR, da, comboBoxData, lookupTablesForControls, variables, sessions, yearStartEnd, dynamicScreenNonDataControlsTable, searchCustomTable, tripleDES, IncrementMainProgressBar, IncrementSubProgressBar, SetupMainProgressBar, SetupSubProgressBar, staffNamesTable, whoAmIPersonID, technoProReports, out errors, getUserInputForVariableValues, reportStartedHandler, reportParameterCollection, suppressGuiMessages, overrideFunctionsTable);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00035F7C File Offset: 0x00034F7C
		public static Report RunReport(UnivDataAdapter da, int searchInfoID, TripleDESEncryptionClass tripleDES, ArrayList variables, out ArrayList errors)
		{
			return ReportFunction.RunReport(da, searchInfoID, tripleDES, variables, out errors, false, false);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00035F9C File Offset: 0x00034F9C
		public static Report RunReport(UnivDataAdapter da, int searchInfoID, TripleDESEncryptionClass tripleDES, ArrayList variables, out ArrayList errors, bool getUserInputForVariableValues, bool suppressGuiMessages)
		{
			return ReportFunction.RunReport(true, da, searchInfoID, tripleDES, variables, out errors, getUserInputForVariableValues, suppressGuiMessages);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00035FC0 File Offset: 0x00034FC0
		public static Report RunReport(bool clearWindowsRequestedFiles, UnivDataAdapter da, int searchInfoID, TripleDESEncryptionClass tripleDES, ArrayList variables, out ArrayList errors, bool getUserInputForVariableValues, bool suppressGuiMessages)
		{
			IncrementProgressBar incrementMainProgressBar = new IncrementProgressBar(ReportFunction.FakeIncrementProgressBar);
			IncrementProgressBar incrementSubProgressBar = new IncrementProgressBar(ReportFunction.FakeIncrementProgressBar);
			SetupProgressBar2 setupMainProgressBar = new SetupProgressBar2(ReportFunction.FakeSetupProgressBar2);
			SetupProgressBar setupSubProgressBar = new SetupProgressBar(ReportFunction.FakeSetupProgressBar);
			object[] yearStartEnd = ClockWorkCore.GetYearStartEnd(da);
			DataTable t = new DataTable();
			da.SelectCommand.CommandText = "SELECT searchcustomid,searchcustomcode,searchcustomdescription,retrievelistsql,multiselect FROM searchcustom";
			da.Fill(t);
			DB[] dbs = new DB[]
			{
				new DB("main", da, tripleDES)
			};
			NameValueCollection nameValueCollection = new NameValueCollection();
			foreach (object obj in variables)
			{
				Variable variable = (Variable)obj;
				nameValueCollection.Add(variable.VariableName, variable.VariableValue.ToString());
			}
			return ReportFunction.RunReport(clearWindowsRequestedFiles, searchInfoID, nameValueCollection, out errors, incrementMainProgressBar, incrementSubProgressBar, setupMainProgressBar, setupSubProgressBar, dbs, getUserInputForVariableValues, "2.0", suppressGuiMessages, false);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000360E0 File Offset: 0x000350E0
		public static Report RunReport(int reportID, NameValueCollection parameters, out ArrayList errors, IncrementProgressBar IncrementMainProgressBar, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar2 SetupMainProgressBar, SetupProgressBar SetupSubProgressBar, DB[] dbs, bool getUserInputForVariableValues, string reportVersion, bool suppressGuiMessages, bool allowNullLocalAccessConnection)
		{
			return ReportFunction.RunReport(true, reportID, parameters, out errors, IncrementMainProgressBar, IncrementSubProgressBar, SetupMainProgressBar, SetupSubProgressBar, dbs, getUserInputForVariableValues, reportVersion, suppressGuiMessages, allowNullLocalAccessConnection);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0003610C File Offset: 0x0003510C
		public static Report RunReport(bool clearWindowsRequestedFiles, int reportID, NameValueCollection parameters, out ArrayList errors, IncrementProgressBar IncrementMainProgressBar, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar2 SetupMainProgressBar, SetupProgressBar SetupSubProgressBar, DB[] dbs, bool getUserInputForVariableValues, string reportVersion, bool suppressGuiMessages, bool allowNullLocalAccessConnection)
		{
			errors = new ArrayList();
			Report result;
			try
			{
				int num = parameters.Count;
				if (num < 1)
				{
					num = 1;
				}
				ArrayList arrayList = new ArrayList(num);
				for (int i = 0; i < parameters.Count; i++)
				{
					string text = parameters.Keys[i];
					int num2 = text.IndexOf("__base64");
					object varValue;
					if (num2 > 0)
					{
						varValue = ClockWorkCore.base64Decode(parameters[i]);
						text = text.Substring(0, num2);
					}
					else
					{
						varValue = parameters[i];
					}
					arrayList.Add(new Variable(text, varValue));
				}
				object[] yearStartEnd = new object[]
				{
					DateTime.Now,
					DateTime.Now.AddYears(1)
				};
				UnivDataAdapter da = dbs[0].Da;
				da.SelectCommand.CommandText = "SELECT dynamicscreennondatacontrolsid,controlcode FROM dynamicscreennondatacontrols";
				DataTable dataTable = new DataTable();
				da.Fill(dataTable);
				if (reportVersion.Trim().Length < 0)
				{
					reportVersion = "reports.mdb";
				}
				else
				{
					reportVersion = "reports" + reportVersion + ".mdb";
				}
				TechnoProReports technoProReports = ReportFunction.SetupTechnoProReports(reportVersion);
				int whoAmIPersonID = -10;
				Report[] array = new Report[dbs.Length];
				DataTable dataTable2 = new DataTable();
				if (technoProReports != null && technoProReports.ReportDefintiionsDataSet != null)
				{
					DataTable dataTable3 = technoProReports.LoadSearchesFromDataSet(false);
					DataRow[] array2 = dataTable3.Select("searchinfoid=" + reportID.ToString());
					dataTable2 = dataTable3.Clone();
					if (array2.Length > 0)
					{
						dataTable3.ImportRow(array2[0]);
					}
				}
				string text2 = ".";
				if (dataTable2.Rows.Count < 1)
				{
					for (int j = 0; j < dbs.Length; j++)
					{
						UnivDataAdapter da2 = dbs[j].Da;
						da2.SelectCommand.CommandText = "SELECT si.searchinfoid,si.title,si.description,si.searchgroupid,si.datecreated,si.datelastmodified,si.whocreated,si.wholastmodified,sgi.grouptitle,sgi.groupdescription,sgi.iconindex,si.searchchartinfoid,si.overrideDynamicControlsScreenNum, 1 AS dblocation FROM searchinfo si LEFT JOIN searchgroupinfo sgi ON sgi.searchgroupinfoid=si.searchgroupid ";
						UnivCommand selectCommand = da2.SelectCommand;
						selectCommand.CommandText += " WHERE si.searchinfoid=@searchinfoid";
						da2.SelectCommand.Parameters.Clear();
						da2.SelectCommand.Parameters.Add("@searchinfoid", reportID);
						dataTable2 = new DataTable();
						da2.Fill(dataTable2, out text2);
						if (dataTable2.Rows.Count > 0)
						{
							break;
						}
					}
				}
				if (dataTable2.Rows.Count > 0)
				{
					DataRow reportDR = dataTable2.Rows[0];
					Report report = new Report(reportDR);
					report.Start();
					for (int j = 0; j < dbs.Length; j++)
					{
						try
						{
							UnivDataAdapter da2 = dbs[j].Da;
							DataTable searchCustomTable = ReportFunction.LoadCustomTable(da2, technoProReports);
							TripleDESEncryptionClass tripleDES = dbs[j].TripleDES;
							DataTable sessions = new DataTable("sessions");
							DataSet comboBoxData = new DataSet();
							DataSet dataSet = new DataSet();
							DataTable dataTable4 = ReportFunction.LoadStaffNames(da2, tripleDES);
							dataSet.Tables.Add(dataTable4);
							ArrayList arrayList2;
							Report report2 = ReportFunction.RunReport(clearWindowsRequestedFiles, dbs[j].DbDescription, reportDR, da2, comboBoxData, dataSet, arrayList, sessions, yearStartEnd, dataTable, searchCustomTable, tripleDES, IncrementMainProgressBar, IncrementSubProgressBar, SetupMainProgressBar, SetupSubProgressBar, dataTable4, whoAmIPersonID, technoProReports, out arrayList2, getUserInputForVariableValues, null, null, suppressGuiMessages, null);
							if (arrayList2.Count > 0)
							{
								errors = arrayList2;
								return null;
							}
							array[j] = report2;
						}
						catch (Exception ex)
						{
							errors.Add("ERROR WITH '" + dbs[j].DbDescription + ": " + ex.ToString());
							return null;
						}
					}
					report.End();
					if (array.Length > 1)
					{
						Type type = Type.GetType("System.String");
						for (int j = 0; j < array.Length; j++)
						{
							Report report2 = array[j];
							DataView currentDataView = report2.GetCurrentDataView();
							DataTable dataTable3 = currentDataView.Table;
							ReportFunction.AddDataColumn(ref dataTable3, "Department", type);
							int columnIndex = dataTable3.Columns.Count - 1;
							foreach (object obj in dataTable3.Rows)
							{
								DataRow dataRow = (DataRow)obj;
								dataRow[columnIndex] = dbs[j].DbDescription;
							}
						}
						DataView[] array3 = new DataView[array.Length];
						for (int k = 0; k < array.Length; k++)
						{
							array3[k] = array[k].GetCurrentDataView();
						}
						DataView dv = ReportFunction.ConcatenateDataViews(array3);
						report.AddResult(dv);
						result = report;
					}
					else
					{
						result = array[0];
					}
				}
				else if (dataTable2.Columns.Count > 0)
				{
					errors.Add("Can't find report# " + reportID.ToString());
					result = null;
				}
				else
				{
					text2 = "Error trying to load report (" + reportID.ToString() + "): " + text2;
					errors.Add(text2);
					result = null;
				}
			}
			catch (Exception ex2)
			{
				errors.Add(ex2.ToString());
				result = null;
			}
			return result;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000366F8 File Offset: 0x000356F8
		public static Report RunReport(int reportID, NameValueCollection parameters, out ArrayList errors, IncrementProgressBar IncrementMainProgressBar, IncrementProgressBar IncrementSubProgressBar, SetupProgressBar2 SetupMainProgressBar, SetupProgressBar SetupSubProgressBar, DB[] dbs, bool getUserInputForVariableValues, string reportVersion, bool suppressGuiMessages)
		{
			return ReportFunction.RunReport(reportID, parameters, out errors, IncrementMainProgressBar, IncrementSubProgressBar, SetupMainProgressBar, SetupSubProgressBar, dbs, getUserInputForVariableValues, reportVersion, suppressGuiMessages, false);
		}

		// Token: 0x040000DF RID: 223
		private const int interval = 100;

		// Token: 0x040000E0 RID: 224
		public static string[] registryBreakdown = new string[]
		{
			"Software",
			"TechnoPro",
			"ClockWork"
		};

		// Token: 0x040000E1 RID: 225
		public static bool DontSetupLocalAccessConnection = false;

		// Token: 0x040000E2 RID: 226
		private static ExecuteScriptCache ExecuteScript_Cache = null;

		// Token: 0x040000E3 RID: 227
		private static bool IgnoreUpdateInstructorInfo = false;

		// Token: 0x040000E4 RID: 228
		public static bool ignoreImportLuCourseCampusLocationEtc = false;

		// Token: 0x040000E5 RID: 229
		public static bool cantlog = false;

		// Token: 0x040000E6 RID: 230
		private static bool UseUpdatedCreateTripleDES = true;

		// Token: 0x040000E7 RID: 231
		public static List<string> WindowsRequestedFiles = null;

		// Token: 0x02000013 RID: 19
		internal class ListViewControlColumnGroup
		{
			// Token: 0x17000028 RID: 40
			// (get) Token: 0x0600020A RID: 522 RVA: 0x00036780 File Offset: 0x00035780
			// (set) Token: 0x0600020B RID: 523 RVA: 0x00036797 File Offset: 0x00035797
			public string ColName { get; set; }

			// Token: 0x17000029 RID: 41
			// (get) Token: 0x0600020C RID: 524 RVA: 0x000367A0 File Offset: 0x000357A0
			// (set) Token: 0x0600020D RID: 525 RVA: 0x000367B7 File Offset: 0x000357B7
			public List<string> InternalColNames { get; set; }

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x0600020E RID: 526 RVA: 0x000367C0 File Offset: 0x000357C0
			// (set) Token: 0x0600020F RID: 527 RVA: 0x000367D7 File Offset: 0x000357D7
			public bool IsActive { get; set; }

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x06000210 RID: 528 RVA: 0x000367E0 File Offset: 0x000357E0
			// (set) Token: 0x06000211 RID: 529 RVA: 0x000367F7 File Offset: 0x000357F7
			public int StartColIndex { get; set; }
		}

		// Token: 0x02000014 RID: 20
		// (Invoke) Token: 0x06000214 RID: 532
		private delegate void GenericRowLoopAction(DataRow dr, params object[] oo);

		// Token: 0x02000015 RID: 21
		internal class CourseStartEndDateRule
		{
			// Token: 0x1700002C RID: 44
			// (get) Token: 0x06000217 RID: 535 RVA: 0x00036808 File Offset: 0x00035808
			public bool IsDefault
			{
				get
				{
					return this.isDefault;
				}
			}

			// Token: 0x06000218 RID: 536 RVA: 0x00036820 File Offset: 0x00035820
			public CourseStartEndDateRule(string defn)
			{
				string[] array = defn.Split(new char[]
				{
					':'
				});
				string text;
				if (array.Length == 4)
				{
					this.isDefault = false;
					this.colName = array[0];
					this.matchStr = array[1].ToLower();
					this.startDateExistingCol = array[2];
					text = array[3];
				}
				else
				{
					this.isDefault = true;
					this.colName = "";
					this.matchStr = "";
					this.startDateExistingCol = array[1];
					text = array[2];
				}
				int num = text.IndexOf('-');
				if (num > 0)
				{
					this.startMonth = int.Parse(text.Substring(0, 2));
					this.startDay = int.Parse(text.Substring(3, 2));
					this.endMonth = int.Parse(text.Substring(6, 2));
					this.endDay = int.Parse(text.Substring(9, 2));
					this.courseDurationInMonths = 0;
				}
				else
				{
					this.startMonth = 0;
					this.startDay = 0;
					num = text.IndexOf('/');
					if (num > 0)
					{
						this.endMonth = int.Parse(text.Substring(0, 2));
						this.endDay = int.Parse(text.Substring(3, 2));
						this.courseDurationInMonths = 0;
					}
					else
					{
						this.endMonth = 0;
						this.endDay = 0;
						this.courseDurationInMonths = int.Parse(text);
					}
				}
			}

			// Token: 0x06000219 RID: 537 RVA: 0x00036998 File Offset: 0x00035998
			public bool Matches(DataRow dr)
			{
				bool result;
				if (!this.isDefault)
				{
					string text = dr[this.colName].ToString().ToLower().Trim();
					result = (text.CompareTo(this.matchStr) == 0);
				}
				else
				{
					result = false;
				}
				return result;
			}

			// Token: 0x0600021A RID: 538 RVA: 0x000369E4 File Offset: 0x000359E4
			public void CalculateStartEndDates(DataRow dr, out DateTime sdate, out DateTime edate)
			{
				DateTime dateTime = ReportFunction.ParseDateTime(dr[this.startDateExistingCol].ToString());
				if (!(dateTime == DateTime.MinValue))
				{
					if (this.courseDurationInMonths > 0 && this.startMonth == 0)
					{
						sdate = dateTime;
						edate = dateTime.AddMonths(this.courseDurationInMonths);
						return;
					}
					if (this.courseDurationInMonths == 0 && this.startMonth == 0 && this.endMonth > 0)
					{
						sdate = dateTime;
						edate = new DateTime(sdate.Year, this.endMonth, this.endDay);
						if (edate < sdate)
						{
							edate = new DateTime(sdate.Year + 1, this.endMonth, this.endDay);
						}
						return;
					}
				}
				sdate = dateTime;
				edate = dateTime.AddMonths(4);
			}

			// Token: 0x040000EE RID: 238
			private string colName;

			// Token: 0x040000EF RID: 239
			private string matchStr;

			// Token: 0x040000F0 RID: 240
			private int startMonth;

			// Token: 0x040000F1 RID: 241
			private int startDay;

			// Token: 0x040000F2 RID: 242
			private int endMonth;

			// Token: 0x040000F3 RID: 243
			private int endDay;

			// Token: 0x040000F4 RID: 244
			private string startDateExistingCol;

			// Token: 0x040000F5 RID: 245
			private int courseDurationInMonths;

			// Token: 0x040000F6 RID: 246
			private bool isDefault;
		}

		// Token: 0x02000016 RID: 22
		internal class CourseStartEndDateRuleCollection : CollectionBase
		{
			// Token: 0x0600021B RID: 539 RVA: 0x00036AF6 File Offset: 0x00035AF6
			public CourseStartEndDateRuleCollection()
			{
			}

			// Token: 0x0600021C RID: 540 RVA: 0x00036B04 File Offset: 0x00035B04
			public CourseStartEndDateRuleCollection(string defn)
			{
				string[] array = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(defn, true);
				foreach (string defn2 in array)
				{
					ReportFunction.CourseStartEndDateRule value = new ReportFunction.CourseStartEndDateRule(defn2);
					base.List.Add(value);
				}
			}

			// Token: 0x0600021D RID: 541 RVA: 0x00036B58 File Offset: 0x00035B58
			public int Add(ReportFunction.CourseStartEndDateRule rule)
			{
				return base.List.Add(rule);
			}

			// Token: 0x1700002D RID: 45
			public ReportFunction.CourseStartEndDateRule this[int index]
			{
				get
				{
					return (ReportFunction.CourseStartEndDateRule)base.List[index];
				}
				set
				{
					base.List[index] = value;
				}
			}

			// Token: 0x06000220 RID: 544 RVA: 0x00036BAC File Offset: 0x00035BAC
			public void CalculateStartEndDates(DataRow dr, out DateTime sdate, out DateTime edate)
			{
				ReportFunction.CourseStartEndDateRule courseStartEndDateRule = null;
				foreach (object obj in base.List)
				{
					ReportFunction.CourseStartEndDateRule courseStartEndDateRule2 = (ReportFunction.CourseStartEndDateRule)obj;
					if (courseStartEndDateRule2.IsDefault)
					{
						courseStartEndDateRule = courseStartEndDateRule2;
					}
					else if (courseStartEndDateRule2.Matches(dr))
					{
						courseStartEndDateRule2.CalculateStartEndDates(dr, out sdate, out edate);
						return;
					}
				}
				courseStartEndDateRule.CalculateStartEndDates(dr, out sdate, out edate);
			}
		}

		// Token: 0x02000017 RID: 23
		private enum LookupStudentMethod
		{
			// Token: 0x040000F8 RID: 248
			Unknown,
			// Token: 0x040000F9 RID: 249
			personid,
			// Token: 0x040000FA RID: 250
			student_no
		}
	}
}
