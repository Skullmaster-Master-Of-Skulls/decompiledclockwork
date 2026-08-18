using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
using AutoComboBox.MyControls;
using ClockWorkAPI.EntityExtensions;
using EncryptionClassLibrary;
using Microsoft.Win32;
using SettingsLibrary;
using SettingsPermissions;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using UnivOleDb;

namespace ClockWorkAPI
{
	// Token: 0x0200009C RID: 156
	public class ClockWorkCore
	{
		// Token: 0x060007BE RID: 1982 RVA: 0x0002CD54 File Offset: 0x0002BD54
		public static PersonBaseDTO GetPersonFromDataRow(TripleDESEncryptionClass tripleDES, DataRow dr)
		{
			int personId = (dr["personid"] == DBNull.Value) ? 0 : ((int)dr["personid"]);
			string student_no;
			string firstName;
			string lastName;
			string middleName;
			if (dr.Table.Columns["student_no"].DataType == typeof(string))
			{
				student_no = dr["student_no"].ToString();
				firstName = dr["firstname"].ToString();
				lastName = dr["lastname"].ToString();
				middleName = ((!dr.Table.Columns.Contains("middlename")) ? "" : dr["middlename"].ToString());
			}
			else
			{
				student_no = ((dr["student_no"] == DBNull.Value) ? "??" : tripleDES.Decrypt((byte[])dr["student_no"]));
				firstName = ((dr["firstname"] == DBNull.Value) ? "??" : tripleDES.Decrypt((byte[])dr["firstname"]));
				lastName = ((dr["lastname"] == DBNull.Value) ? "??" : tripleDES.Decrypt((byte[])dr["lastname"]));
				middleName = ((!dr.Table.Columns.Contains("middlename") || dr["middlename"] == DBNull.Value) ? "" : tripleDES.Decrypt((byte[])dr["middlename"]));
			}
			return new PersonBaseDTO
			{
				PersonId = personId,
				FirstName = firstName,
				MiddleName = middleName,
				LastName = lastName,
				Student_no = student_no,
				CoreGroup = eCoreGroupDTO.Unknown,
				Tag = new PersonExt()
			};
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0002CF54 File Offset: 0x0002BF54
		public static NameValueCollection ParseParameters(string codes, char delimiter)
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			string[] array = codes.Split(new char[]
			{
				delimiter
			});
			foreach (string text in array)
			{
				int num = text.IndexOf('=');
				if (num >= 0)
				{
					string name = text.Substring(0, num);
					string value = text.Substring(num + 1);
					nameValueCollection.Add(name, value);
				}
			}
			return nameValueCollection;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0002CFE0 File Offset: 0x0002BFE0
		public static void SetBrowserText(WebBrowser browser, string title, string htmlNoHtmlHeadBodyTags)
		{
			browser.Navigate("about:blank");
			if (browser.Document != null)
			{
				browser.Document.Write(string.Empty);
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(ClockWorkCore.html1);
			stringBuilder.Append("<title>");
			stringBuilder.Append(title);
			stringBuilder.Append("</title>");
			stringBuilder.Append(ClockWorkCore.html2);
			stringBuilder.Append(htmlNoHtmlHeadBodyTags);
			stringBuilder.Append(ClockWorkCore.html2);
			browser.DocumentText = stringBuilder.ToString();
			browser.Document.ExecCommand("SelectAll", false, null);
			browser.Document.ExecCommand("FontName", false, "Arial");
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0002D0AC File Offset: 0x0002C0AC
		public static DateTime GetZeroTimeNextDay(DateTime date)
		{
			DateTime dateTime = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
			return dateTime.AddDays(1.0);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0002D0F0 File Offset: 0x0002C0F0
		public static byte[] GetBytes(TripleDESEncryptionClass tripleDES, string txt, bool encrypt)
		{
			byte[] result;
			if (encrypt)
			{
				result = tripleDES.Encrypt(txt);
			}
			else
			{
				UTF8Encoding utf8Encoding = new UTF8Encoding();
				result = utf8Encoding.GetBytes(txt);
			}
			return result;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0002D124 File Offset: 0x0002C124
		public static string FormatAutoIncIdToLookPretty(int id, int numDigits)
		{
			string text = id.ToString();
			int num = numDigits - text.Length;
			if (num > 0)
			{
				text = new string('0', num) + text;
			}
			return text;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0002D164 File Offset: 0x0002C164
		public static string GetConnectionString(out UnivConnection mainConnection, out UnivDataAdapter da, out string errMsg)
		{
			errMsg = null;
			mainConnection = null;
			da = null;
			object registryValue = ClockWorkCore.GetRegistryValue(Registry.LocalMachine, ClockWorkCore.registryBreakdown, "UseLocalMachineSettings", false);
			bool flag;
			if (registryValue != null)
			{
				string text = registryValue.ToString().Trim();
				flag = (text.CompareTo("1") == 0);
			}
			else
			{
				flag = false;
			}
			int num = 0;
			string text2;
			for (;;)
			{
				object obj;
				if (flag)
				{
					obj = null;
				}
				else
				{
					obj = ClockWorkCore.GetRegistryValue(Registry.CurrentUser, ClockWorkCore.registryBreakdown, "cs", false);
				}
				if (obj == null)
				{
					obj = ClockWorkCore.GetRegistryValue(Registry.LocalMachine, ClockWorkCore.registryBreakdown, "cs", false);
					if (obj != null)
					{
						TripleDESEncryptionClass tripleDESEncryptionClass = new TripleDESEncryptionClass();
						text2 = "";
						try
						{
							text2 = tripleDESEncryptionClass.Decrypt((byte[])obj);
						}
						catch (Exception ex)
						{
							errMsg = "thecs=" + text2 + Environment.NewLine + ex.ToString();
							text2 = "";
						}
						if (text2.Length > 0)
						{
							break;
						}
					}
				}
				if (obj == null)
				{
					string[] regKeyBreakdown = new string[]
					{
						"Software",
						"ClockWork"
					};
					RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software");
					try
					{
						registryKey = registryKey.OpenSubKey("ClockWork");
					}
					catch
					{
					}
					if (registryKey != null)
					{
						string[] valueNames = registryKey.GetValueNames();
						foreach (string valueName in valueNames)
						{
							object registryValue2 = ClockWorkCore.GetRegistryValue(Registry.CurrentUser, ClockWorkCore.registryBreakdown, valueName, false);
							if (registryValue2 == null)
							{
								object registryValue3 = ClockWorkCore.GetRegistryValue(Registry.CurrentUser, regKeyBreakdown, valueName, false);
								ClockWorkCore.SetRegistryValue(Registry.CurrentUser, ClockWorkCore.registryBreakdown, valueName, registryValue3, false);
							}
						}
						obj = ClockWorkCore.GetRegistryValue(Registry.CurrentUser, ClockWorkCore.registryBreakdown, "cs", false);
					}
					else
					{
						obj = ClockWorkCore.GetRegistryValue(Registry.LocalMachine, ClockWorkCore.registryBreakdown, "cs", false);
					}
				}
				if (obj == null)
				{
					goto Block_13;
				}
				try
				{
					string text3 = DPAPIencryption.UnProtectData((string)obj, DPAPIencryption.GetEntropy());
					mainConnection = UnivOleDbFactory.CreateConnection(text3);
					da = mainConnection.CreateDataAdapter();
					return text3;
				}
				catch (Exception ex2)
				{
					if (++num > 1)
					{
						return null;
					}
					errMsg = "";
					da = null;
					mainConnection = null;
					return null;
				}
			}
			mainConnection = UnivOleDbFactory.CreateConnection(text2);
			da = mainConnection.CreateDataAdapter();
			errMsg = null;
			return text2;
			Block_13:
			return null;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0002D460 File Offset: 0x0002C460
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

		// Token: 0x060007C6 RID: 1990 RVA: 0x0002D4E4 File Offset: 0x0002C4E4
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

		// Token: 0x060007C7 RID: 1991 RVA: 0x0002D564 File Offset: 0x0002C564
		public static string GetRegistryValueString(RegistryKey StartKey, string[] RegKeyBreakdown, string valueName, bool isEncrypted)
		{
			RegistryKey registryKey = ClockWorkCore.GetRegistryKey(StartKey, RegKeyBreakdown, false, false);
			object registryValue = ClockWorkCore.GetRegistryValue(registryKey, valueName, isEncrypted);
			string result;
			if (registryValue == null)
			{
				result = "";
			}
			else
			{
				result = registryValue.ToString().Trim();
			}
			return result;
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0002D5A8 File Offset: 0x0002C5A8
		public static string GetRegistryValueStringCurrentUser(string valueName, bool isEncrypted)
		{
			RegistryKey currentUser = Registry.CurrentUser;
			string[] regKeyBreakdown = ClockWorkCore.registryBreakdown;
			return ClockWorkCore.GetRegistryValueString(currentUser, regKeyBreakdown, valueName, isEncrypted);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0002D5D0 File Offset: 0x0002C5D0
		public static object GetRegistryValue(RegistryKey StartKey, string[] RegKeyBreakdown, string valueName, bool isEncrypted)
		{
			RegistryKey registryKey = ClockWorkCore.GetRegistryKey(StartKey, RegKeyBreakdown, false, false);
			return ClockWorkCore.GetRegistryValue(registryKey, valueName, isEncrypted);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0002D5F4 File Offset: 0x0002C5F4
		public static object SetRegistryValue(RegistryKey regKey, string valueName, object valueObject, bool isEncrypted)
		{
			if (regKey != null)
			{
				try
				{
					if (isEncrypted)
					{
						string text = (string)valueObject;
						text = DPAPIencryption.ProtectData(text, DPAPIencryption.GetEntropy());
						regKey.SetValue(valueName, text);
					}
					else
					{
						regKey.SetValue(valueName, valueObject);
					}
					return valueObject;
				}
				catch (Exception result)
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0002D660 File Offset: 0x0002C660
		public static object SetRegistryValue(RegistryKey StartKey, string[] RegKeyBreakdown, string valueName, object valueObject, bool isEncrypted)
		{
			RegistryKey registryKey = ClockWorkCore.GetRegistryKey(StartKey, RegKeyBreakdown, true, true);
			return ClockWorkCore.SetRegistryValue(registryKey, valueName, valueObject, isEncrypted);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0002D688 File Offset: 0x0002C688
		public static object SetRegistryValueCurrentUser(string valueName, object valueObject, bool isEncrypted)
		{
			return ClockWorkCore.SetRegistryValue(Registry.CurrentUser, ClockWorkCore.registryBreakdown, valueName, valueObject, isEncrypted);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0002D6AC File Offset: 0x0002C6AC
		public static void DeleteRegistryValue(RegistryKey StartKey, string[] RegKeyBreakdown, string valueName)
		{
			RegistryKey registryKey = ClockWorkCore.GetRegistryKey(StartKey, RegKeyBreakdown, false, true);
			if (registryKey != null)
			{
				registryKey.DeleteValue(valueName, false);
			}
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0002D6D8 File Offset: 0x0002C6D8
		public static string base64Encode(byte[] binaryData)
		{
			string result;
			try
			{
				string text = Convert.ToBase64String(binaryData);
				result = text;
			}
			catch (Exception innerException)
			{
				throw new Exception("Error in base64Encode", innerException);
			}
			return result;
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0002D714 File Offset: 0x0002C714
		public static bool IsEmailValid(string email)
		{
			bool result;
			if (string.IsNullOrEmpty(email))
			{
				result = false;
			}
			else
			{
				Regex regex = new Regex("(?<user>[^@]+)@(?<host>.+)");
				Match match = regex.Match(email);
				result = match.Success;
			}
			return result;
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0002D750 File Offset: 0x0002C750
		public static string GetDataRowCellStringValue(DataRow dr, string colName)
		{
			return (dr[colName] == DBNull.Value) ? "" : ((string)dr[colName]);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0002D784 File Offset: 0x0002C784
		public static byte[] base64Decode(string data)
		{
			byte[] result;
			try
			{
				UTF8Encoding utf8Encoding = new UTF8Encoding();
				Decoder decoder = utf8Encoding.GetDecoder();
				byte[] array = Convert.FromBase64String(data);
				result = array;
			}
			catch (Exception ex)
			{
				throw new Exception("Error in base64Decode" + ex.Message);
			}
			return result;
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x0002D7D8 File Offset: 0x0002C7D8
		public static bool UseAccessibleColoursForToolstrip
		{
			get
			{
				object registryValue = ClockWorkCore.GetRegistryValue(Registry.CurrentUser, ClockWorkCore.registryBreakdown, "AccessibleColoursForToolstrip", false);
				return registryValue != null && "1yestrue".IndexOf(registryValue.ToString().Trim().ToLower()) >= 0;
			}
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0002D826 File Offset: 0x0002C826
		public static void SetUseAccessibleColoursForToolstrip(bool enabled)
		{
			ClockWorkCore.SetRegistryValue(Registry.CurrentUser, ClockWorkCore.registryBreakdown, "AccessibleColoursForToolstrip", enabled ? "1" : "0", false);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0002D850 File Offset: 0x0002C850
		public static int GetSettingsPermissions(UnivDataAdapter da, int personid, int[] groupIDs, out Settings settings, out Permissions permissions)
		{
			da.SelectCommand.CommandText = "SELECT permissionid,personid,permissioncode,permissionvalue FROM permissions WHERE personid=@personid";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@personid", personid);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			da.SelectCommand.CommandText = "SELECT settingid,personid,settingcode,settingvalue,settingstringvalue FROM settings WHERE personid=@personid";
			DataTable dataTable2 = new DataTable();
			da.Fill(dataTable2);
			DataTable dataTable3;
			DataTable dataTable4;
			if (groupIDs != null)
			{
				da.SelectCommand.CommandText = "SELECT permissiongroupid,groupid,permissioncode,permissionvalue FROM permissionsgroups WHERE ";
				da.SelectCommand.Parameters.Clear();
				int num = 0;
				string text = "groupid=-1 OR ";
				foreach (int num2 in groupIDs)
				{
					string text2 = "@g" + num.ToString();
					if (num++ > 0)
					{
						text += " OR ";
					}
					text = text + "groupid=" + text2;
					da.SelectCommand.Parameters.Add(text2, num2);
				}
				UnivCommand selectCommand = da.SelectCommand;
				selectCommand.CommandText += text;
				dataTable3 = new DataTable();
				da.Fill(dataTable3);
				da.SelectCommand.CommandText = "SELECT settinggroupid,groupid,settingcode,settingvalue,settingstringvalue FROM settingsgroups WHERE ";
				UnivCommand selectCommand2 = da.SelectCommand;
				selectCommand2.CommandText += text;
				dataTable4 = new DataTable();
				da.Fill(dataTable4);
			}
			else
			{
				Type type = Type.GetType("System.Int32");
				dataTable3 = new DataTable();
				dataTable3.Columns.Add("permissiongroupid", type);
				dataTable3.Columns.Add("groupid", type);
				dataTable3.Columns.Add("permissioncode", type);
				dataTable3.Columns.Add("permissionvalue", type);
				dataTable4 = new DataTable();
				dataTable4.Columns.Add("settinggroupid", type);
				dataTable4.Columns.Add("groupid", type);
				dataTable4.Columns.Add("settingcode", type);
				dataTable4.Columns.Add("settingvalue", type);
				dataTable4.Columns.Add("settingstringvalue");
			}
			settings = new Settings(groupIDs, dataTable2, dataTable4, personid, da);
			permissions = new Permissions(groupIDs, dataTable, dataTable3);
			return 0;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0002DAD0 File Offset: 0x0002CAD0
		public static PersonBaseDTO GetUserDetails(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, string winLogin, string pass)
		{
			byte[] parameterValue = tripleDES.Encrypt(winLogin.ToUpper());
			byte[] parameterValue2;
			if (pass == null)
			{
				parameterValue2 = tripleDES.Encrypt(".");
			}
			else
			{
				parameterValue2 = tripleDES.Encrypt(pass);
			}
			da.SelectCommand.CommandText = "EXECUTE procPersonSelect_getUserDetails @student_no,@password";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@student_no", parameterValue);
			da.SelectCommand.Parameters.Add("@password", parameterValue2);
			DataTable dataTable = new DataTable();
			string text;
			da.Fill(dataTable, out text);
			PersonBaseDTO result;
			if (dataTable.Rows.Count > 0)
			{
				DataRow dataRow = dataTable.Rows[0];
				int personId = (int)dataRow[0];
				byte[] inputInBytes = (byte[])dataRow[1];
				byte[] inputInBytes2 = (byte[])dataRow[2];
				byte[] inputInBytes3 = (byte[])dataRow[3];
				string firstName = tripleDES.Decrypt(inputInBytes);
				string lastName = tripleDES.Decrypt(inputInBytes2);
				string student_no = tripleDES.Decrypt(inputInBytes3);
				ArrayList arrayList = new ArrayList();
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow2 = (DataRow)obj;
					if (dataRow2[6] != DBNull.Value)
					{
						int num = (int)dataRow2[6];
						arrayList.Add(num);
						if (dataRow2[7] != DBNull.Value)
						{
							bool flag = (bool)dataRow2[7];
							if (flag)
							{
							}
						}
					}
				}
				int[] array;
				if (arrayList.Count > 0)
				{
					array = new int[arrayList.Count];
					for (int i = 0; i < arrayList.Count; i++)
					{
						array[i] = (int)arrayList[i];
					}
					arrayList.Clear();
					arrayList = null;
				}
				else
				{
					array = null;
				}
				PersonBaseDTO personBaseDTO = new PersonBaseDTO
				{
					PersonId = personId,
					FirstName = firstName,
					LastName = lastName,
					Student_no = student_no,
					MiddleName = "",
					CoreGroup = eCoreGroupDTO.Unknown,
					Groups = new List<GroupDTO>(),
					Tag = new PersonExt()
				};
				foreach (int num in array)
				{
					int num;
					GroupDTO group = new GroupDTO
					{
						GroupId = num,
						Description = ""
					};
					eCoreGroupDTO coreGroupFromGroup = group.GetCoreGroupFromGroup();
					personBaseDTO.CoreGroup |= coreGroupFromGroup;
				}
				result = personBaseDTO;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0002DDFC File Offset: 0x0002CDFC
		public static string StringListToCommaSeparatedString(List<string> list)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (list.Count > 0)
			{
				stringBuilder.Append(list[0]);
			}
			for (int i = 1; i < list.Count; i++)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(list[i]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0002DE6C File Offset: 0x0002CE6C
		public static TripleDESEncryptionClass GetEncryptionKey(UnivDataAdapter da, out string errmsg)
		{
			da.SelectCommand.CommandText = "SELECT misccode FROM misc WHERE misccode=1";
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			bool use192bit = dataTable.Rows.Count < 1;
			object registryValue = ClockWorkCore.GetRegistryValue(Registry.LocalMachine, ClockWorkCore.registryBreakdown, "UseLocalMachineSettings", false);
			bool flag;
			if (registryValue != null)
			{
				string text = registryValue.ToString().Trim();
				flag = (text.CompareTo("1") == 0);
			}
			else
			{
				flag = false;
			}
			string[] regKeyBreakdown = ClockWorkCore.registryBreakdown;
			object obj;
			if (flag)
			{
				obj = null;
			}
			else
			{
				obj = ClockWorkCore.GetRegistryValue(Registry.CurrentUser, regKeyBreakdown, "k", true);
			}
			if (obj == null)
			{
				obj = ClockWorkCore.GetRegistryValue(Registry.LocalMachine, ClockWorkCore.registryBreakdown, "k", false);
				if (obj != null)
				{
					TripleDESEncryptionClass tripleDESEncryptionClass = new TripleDESEncryptionClass();
					try
					{
						string text2 = tripleDESEncryptionClass.Decrypt((byte[])obj);
						if (text2.Length > 0)
						{
							byte[][] bytes = TripleDESEncryptionClass.GetBytes(use192bit, text2);
							byte[] key = bytes[0];
							byte[] iv = bytes[1];
							TripleDESEncryptionClass tripleDESEncryptionClass2 = new TripleDESEncryptionClass(key, iv);
							errmsg = null;
							return tripleDESEncryptionClass2;
						}
						errmsg = "Error; Invalid Crypt Keys!";
						return null;
					}
					catch
					{
					}
				}
			}
			string text3 = "";
			if (obj != null)
			{
				text3 = (string)obj;
			}
			TripleDESEncryptionClass result;
			if (text3.Length > 0)
			{
				byte[][] bytes = TripleDESEncryptionClass.GetBytes(use192bit, text3);
				byte[] key = bytes[0];
				byte[] iv = bytes[1];
				TripleDESEncryptionClass tripleDESEncryptionClass2 = new TripleDESEncryptionClass(key, iv);
				errmsg = null;
				result = tripleDESEncryptionClass2;
			}
			else
			{
				errmsg = "Error; Invalid Crypt Keys!";
				result = null;
			}
			return result;
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0002E050 File Offset: 0x0002D050
		public static DataTable GetSessionsTable(UnivDataAdapter da)
		{
			da.SelectCommand.CommandText = "EXECUTE procCourseSelect_getSessions";
			DataTable dataTable = new DataTable("sessions");
			da.Fill(dataTable);
			return dataTable;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0002E088 File Offset: 0x0002D088
		public static ClockWorkCore.ClockWorkLoginTypes GetLoginType(UnivDataAdapter da)
		{
			da.SelectCommand.CommandText = "SELECT misccode,miscstring FROM misc WHERE misccode=101";
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			string text = "windowslogin";
			if (dataTable.Rows.Count > 0)
			{
				text = dataTable.Rows[0][1].ToString().Trim().ToLower();
			}
			ClockWorkCore.ClockWorkLoginTypes result;
			if (text.CompareTo("windowslogin") == 0)
			{
				result = ClockWorkCore.ClockWorkLoginTypes.WindowsLogin;
			}
			else
			{
				result = ClockWorkCore.ClockWorkLoginTypes.ClockWorkLogin;
			}
			return result;
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0002E118 File Offset: 0x0002D118
		public static int GetDatabaseBuild(UnivDataAdapter da)
		{
			int result;
			try
			{
				da.SelectCommand.CommandText = "SELECT misccode,miscstring FROM misc WHERE misccode=1000";
				DataTable dataTable = new DataTable();
				da.Fill(dataTable);
				if (dataTable.Rows.Count > 0)
				{
					int num = 0;
					foreach (object obj in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						string s = dataRow[1].ToString().Trim();
						int num2;
						try
						{
							num2 = int.Parse(s);
						}
						catch
						{
							num2 = 0;
						}
						if (num2 > num)
						{
							num = num2;
						}
					}
					result = num;
				}
				else
				{
					result = 0;
				}
			}
			catch
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0002E224 File Offset: 0x0002D224
		public static DateTime StringToDate(string s)
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

		// Token: 0x060007DC RID: 2012 RVA: 0x0002E278 File Offset: 0x0002D278
		public static string BytesToPlainText(byte[] bytes, TripleDESEncryptionClass tripleDES)
		{
			string text = tripleDES.Decrypt(bytes);
			if (text.StartsWith("{rtf"))
			{
				using (RichTextBox richTextBox = new RichTextBox())
				{
					richTextBox.Rtf = text;
					text = richTextBox.Text;
				}
			}
			return text;
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0002E2E4 File Offset: 0x0002D2E4
		public static byte[] StringToBytes(string txt, bool encrypt, TripleDESEncryptionClass tripleDES)
		{
			byte[] result;
			if (encrypt)
			{
				result = tripleDES.Encrypt(txt);
			}
			else
			{
				UTF8Encoding utf8Encoding = new UTF8Encoding();
				result = utf8Encoding.GetBytes(txt);
			}
			return result;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0002E318 File Offset: 0x0002D318
		public static PersonBaseDTO PersonFromCorePerson(PersonBaseDTO person)
		{
			return new PersonBaseDTO
			{
				PersonId = person.PersonId,
				FirstName = person.FirstName,
				MiddleName = person.MiddleName,
				LastName = person.LastName,
				Student_no = person.Student_no,
				CoreGroup = person.CoreGroup,
				Tag = new PersonExt()
			};
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0002E390 File Offset: 0x0002D390
		public static PersonBaseDTO CorePersonFromPerson(PersonBaseDTO p)
		{
			return new PersonBaseDTO
			{
				PersonId = p.PersonId,
				Student_no = p.Student_no,
				FirstName = p.FirstName,
				LastName = p.LastName,
				MiddleName = p.MiddleName,
				CoreGroup = p.CoreGroup,
				Tag = new PersonExt()
			};
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0002E408 File Offset: 0x0002D408
		public static string BytesToString(byte[] bytes, bool decrypt, TripleDESEncryptionClass tripleDES)
		{
			string result;
			if (decrypt)
			{
				result = tripleDES.Decrypt(bytes);
			}
			else
			{
				UTF8Encoding utf8Encoding = new UTF8Encoding();
				result = utf8Encoding.GetString(bytes);
			}
			return result;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0002E43C File Offset: 0x0002D43C
		public static string BytesToString(DataRow dr, string colName, bool decrypt, TripleDESEncryptionClass tripleDES)
		{
			object obj = dr[colName];
			string result;
			if (obj == DBNull.Value)
			{
				result = "";
			}
			else if (obj is byte[])
			{
				result = ClockWorkCore.BytesToString((byte[])obj, decrypt, tripleDES);
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0002E494 File Offset: 0x0002D494
		public static int[] ExtractIntArrayFromDataTable(DataTable t, string colName)
		{
			int[] array = new int[t.Rows.Count];
			for (int i = 0; i < t.Rows.Count; i++)
			{
				DataRow dataRow = t.Rows[i];
				array[i] = ((dataRow[colName] == DBNull.Value) ? 0 : ((int)dataRow[colName]));
			}
			return array;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0002E504 File Offset: 0x0002D504
		public static string LaunchAppointmentMemoLink(string linkText, Settings settings)
		{
			try
			{
				if (linkText.StartsWith("file:///click_here_to_open_doc#"))
				{
					string s = linkText.Substring(31);
					int num;
					if (int.TryParse(s, out num) && num > 0)
					{
						MyRichText.ShowFile(num);
						return null;
					}
				}
				Process process = new Process();
				string fileName = linkText.StartsWith("file:///") ? linkText.Substring(8) : linkText;
				process.StartInfo.FileName = fileName;
				process.Start();
			}
			catch (Exception ex)
			{
				if (ex.ToString().ToLower().IndexOf("canceled by the user") >= 0)
				{
					return null;
				}
				return ex.ToString();
			}
			return null;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0002E5DC File Offset: 0x0002D5DC
		public static string GetStartDirectory()
		{
			string currentDirectory = Directory.GetCurrentDirectory();
			string path = Path.Combine(currentDirectory, "ClockWork Database Scheduler.exe");
			string result;
			if (File.Exists(path))
			{
				result = currentDirectory;
			}
			else
			{
				object registryValue = ClockWorkCore.GetRegistryValue(Registry.LocalMachine, ClockWorkCore.registryBreakdown, "InstallPath", false);
				string text = (registryValue == null) ? "" : ((string)registryValue);
				if (text.CompareTo(currentDirectory) != 0)
				{
					try
					{
						ClockWorkCore.SetRegistryValue(Registry.LocalMachine, ClockWorkCore.registryBreakdown, "InstallPath", currentDirectory, false);
					}
					catch
					{
					}
				}
				result = "";
			}
			return result;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0002E688 File Offset: 0x0002D688
		public static void GetTermStartEndDates(out DateTime startDate, out DateTime endDate)
		{
			ClockWorkCore.GetTermStartEndDates(DateTime.Now, out startDate, out endDate);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0002E698 File Offset: 0x0002D698
		public static void GetTermStartEndDates(DateTime middleDate, out DateTime startDate, out DateTime endDate)
		{
			int year = middleDate.Year;
			int month = middleDate.Month;
			int day = middleDate.Day;
			if (month <= 3 || (month == 4 && day < 20) || (month == 12 && day > 20))
			{
				startDate = new DateTime(year, 1, 1);
				endDate = new DateTime(year, 4, 30);
			}
			else if (month <= 7 || (month == 8 && day < 20))
			{
				startDate = new DateTime(year, 5, 1);
				endDate = new DateTime(year, 8, 30);
			}
			else
			{
				startDate = new DateTime(year, 9, 1);
				endDate = new DateTime(year, 12, 31);
			}
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0002E760 File Offset: 0x0002D760
		public static object[] GetYearStartEnd(UnivDataAdapter da)
		{
			da.SelectCommand.CommandText = "SELECT startmonth,startday,endmonth,endday,numyearsbetween FROM dateranges WHERE usecode=@schoolyearcode";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@schoolyearcode", 0);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			object[] result;
			if (dataTable.Rows.Count > 0)
			{
				DataRow dataRow = dataTable.Rows[0];
				int month = (int)dataRow[0];
				int day = (int)dataRow[1];
				int num = (int)dataRow[2];
				int day2 = (int)dataRow[3];
				int num2 = (int)dataRow[4];
				DateTime t = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
				DateTime dateTime = new DateTime(DateTime.Now.Year, month, num);
				DateTime dateTime2;
				if (t < dateTime)
				{
					dateTime2 = new DateTime(DateTime.Now.Year - 1, month, day);
				}
				else
				{
					dateTime2 = dateTime;
				}
				DateTime dateTime3 = new DateTime(dateTime2.Year + num2, num, day2);
				result = new object[]
				{
					dateTime2,
					dateTime3
				};
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0002E8E8 File Offset: 0x0002D8E8
		public static int SetSettingsPermissions(UnivDataAdapter da, int personid, int[] groupIDs, out Settings settings, out Permissions permissions)
		{
			string text;
			if (groupIDs == null)
			{
				da.SelectCommand.CommandText = "SELECT pg.groupid FROM peoplegroups pg LEFT JOIN groups g ON g.groupid=pg.groupid WHERE pg.personid=" + personid.ToString() + " ORDER BY g.ordernum,g.groupid";
				DataTable dataTable = new DataTable();
				da.Fill(dataTable, out text);
				if (dataTable.Rows.Count > 0)
				{
					groupIDs = new int[dataTable.Rows.Count];
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
						DataRow dataRow = dataTable.Rows[i];
						groupIDs[i] = ((dataRow[0] != DBNull.Value) ? ((int)dataRow[0]) : -1);
					}
				}
			}
			da.SelectCommand.CommandText = "SELECT permissionid,personid,permissioncode,permissionvalue FROM permissions WHERE personid=@personid";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@personid", personid);
			DataTable dataTable2 = new DataTable();
			da.Fill(dataTable2);
			da.SelectCommand.CommandText = "SELECT settingid,personid,settingcode,settingvalue,settingstringvalue FROM settings WHERE personid=@personid";
			DataTable dataTable3 = new DataTable();
			da.Fill(dataTable3);
			da.SelectCommand.CommandText = "SELECT p.permissiongroupid,p.groupid,p.permissioncode,p.permissionvalue,g.ordernum\r\nFROM peoplegroups pg LEFT JOIN permissionsgroups p ON p.groupid=pg.groupid LEFT JOIN groups g ON g.groupid=p.groupid WHERE NOT p.permissiongroupid IS NULL AND pg.personid=" + personid.ToString();
			UnivCommand selectCommand = da.SelectCommand;
			selectCommand.CommandText = selectCommand.CommandText + " UNION SELECT p.permissiongroupid,p.groupid,p.permissioncode,p.permissionvalue," + int.MaxValue.ToString() + " AS ordernum FROM permissionsgroups p WHERE p.groupid=-1";
			DataTable dataTable4 = new DataTable();
			da.SelectCommand.CommandText = "SELECT q.* FROM (" + da.SelectCommand.CommandText + ") q ORDER BY q.ordernum";
			da.Fill(dataTable4, out text);
			da.SelectCommand.CommandText = "SELECT s.settinggroupid,s.groupid,s.settingcode,s.settingvalue,s.settingstringvalue,g.ordernum\r\nFROM peoplegroups pg LEFT JOIN settingsgroups s ON s.groupid=pg.groupid LEFT JOIN groups g ON g.groupid=pg.groupid \r\nWHERE NOT s.settinggroupid IS NULL AND pg.personid=" + personid.ToString();
			UnivCommand selectCommand2 = da.SelectCommand;
			selectCommand2.CommandText = selectCommand2.CommandText + " UNION SELECT settinggroupid,groupid,settingcode,settingvalue,settingstringvalue," + int.MaxValue.ToString() + " AS ordernum FROM settingsgroups WHERE groupid=-1";
			da.SelectCommand.CommandText = "SELECT q.* FROM (" + da.SelectCommand.CommandText + ") q ORDER BY q.ordernum";
			DataTable dataTable5 = new DataTable();
			da.Fill(dataTable5, out text);
			settings = new Settings(groupIDs, dataTable3, dataTable5, personid, da);
			permissions = new Permissions(groupIDs, dataTable2, dataTable4);
			return -1;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0002EB4C File Offset: 0x0002DB4C
		public static string EncodeUrlVariable(string varValue, bool encrypted, TripleDESEncryptionClass tripleDES)
		{
			string result;
			if (encrypted)
			{
				result = ClockWorkCore.UrlEncodeByteArray(tripleDES.Encrypt(varValue));
			}
			else
			{
				result = varValue;
			}
			return result;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0002EB78 File Offset: 0x0002DB78
		public static string DecodeUrlVariable(string varValue, bool encrypted, TripleDESEncryptionClass tripleDES)
		{
			string result;
			if (encrypted)
			{
				result = tripleDES.Decrypt(ClockWorkCore.UrlDecodeByteArray(varValue));
			}
			else
			{
				result = varValue;
			}
			return result;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0002EBA4 File Offset: 0x0002DBA4
		public static string UrlEncodeByteArray(byte[] bytes)
		{
			string str = ClockWorkCore.ByteArrayToHexString(bytes);
			return HttpUtility.UrlEncode(str);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0002EBC4 File Offset: 0x0002DBC4
		public static byte[] UrlDecodeByteArray(string s)
		{
			string hex = HttpUtility.UrlDecode(s);
			return ClockWorkCore.HexStringToByteArray(hex);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0002EBE8 File Offset: 0x0002DBE8
		public static string ByteArrayToHexString(byte[] Bytes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = "0123456789ABCDEF";
			foreach (byte b in Bytes)
			{
				stringBuilder.Append(text[b >> 4]);
				stringBuilder.Append(text[(int)(b & 15)]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0002ECB4 File Offset: 0x0002DCB4
		public static byte[] HexStringToByteArray(string Hex)
		{
			byte[] array = new byte[Hex.Length / 2];
			int[] array2 = new int[]
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
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				10,
				11,
				12,
				13,
				14,
				15
			};
			int num = 0;
			int i = 0;
			while (i < Hex.Length)
			{
				array[num] = (byte)(array2[(int)(char.ToUpper(Hex[i]) - '0')] << 4 | array2[(int)(char.ToUpper(Hex[i + 1]) - '0')]);
				i += 2;
				num++;
			}
			return array;
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0002ED34 File Offset: 0x0002DD34
		public static bool RunElevated(string fileName, string workingDirectory, string args)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.Verb = "runas";
			processStartInfo.FileName = fileName;
			processStartInfo.Arguments = args;
			processStartInfo.WorkingDirectory = workingDirectory;
			bool result;
			try
			{
				Process.Start(processStartInfo);
				result = true;
			}
			catch (Win32Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0002ED90 File Offset: 0x0002DD90
		public static string GetSettingsValueString(string instanceName, Setting setting, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			if (string.IsNullOrEmpty(instanceName))
			{
				instanceName = "ClockWork";
			}
			da.SelectCommand.CommandText = "SELECT settingstringvalue FROM websettings2 WHERE instancename=@iname AND settingcode=@code";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@iname", instanceName);
			da.SelectCommand.Parameters.Add("@code", setting);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			string result;
			if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
			{
				byte[] inputInBytes = (byte[])dataTable.Rows[0][0];
				result = tripleDES.Decrypt(inputInBytes);
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x040003F1 RID: 1009
		public const int GROUPS_student = 1;

		// Token: 0x040003F2 RID: 1010
		public const int GROUPS_staff = 2;

		// Token: 0x040003F3 RID: 1011
		public const int GROUPS_room = 3;

		// Token: 0x040003F4 RID: 1012
		public const int GROUPS_resource = 4;

		// Token: 0x040003F5 RID: 1013
		public const int GROUPS_roomGroup = 7;

		// Token: 0x040003F6 RID: 1014
		public const int GROUPS_peopleGroup = 8;

		// Token: 0x040003F7 RID: 1015
		public const int GROUPS_resourceGroup = 9;

		// Token: 0x040003F8 RID: 1016
		public const int GROUPS_admin = 10;

		// Token: 0x040003F9 RID: 1017
		public static string[] registryBreakdown = new string[]
		{
			"Software",
			"TechnoPro",
			"ClockWork"
		};

		// Token: 0x040003FA RID: 1018
		private static readonly string html1 = "<html><head>";

		// Token: 0x040003FB RID: 1019
		private static readonly string html2 = "<style TYPE=\"text/css\"> <!-- body { font-family: Arial, Palatino, Zapf Calligraphic, Georgia, Times New Roman, Times, Serif; font-size: .9em;  } h2 { border-bottom-width: 1px; border-bottom-style: solid; border-bottom-color: orange; font-size: 1.1em; margin-bottom: 2px; } --> </style></head><body>";

		// Token: 0x040003FC RID: 1020
		private static readonly string html3 = "</body></html>";

		// Token: 0x0200009D RID: 157
		public enum ClockWorkLoginTypes
		{
			// Token: 0x040003FE RID: 1022
			Unknown,
			// Token: 0x040003FF RID: 1023
			WindowsLogin,
			// Token: 0x04000400 RID: 1024
			ClockWorkLogin
		}
	}
}
