using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using EncryptionClassLibrary;
using Microsoft.Win32;
using UnivOleDb;

namespace ClockWorkAPI
{
	// Token: 0x0200009E RID: 158
	public class Authorization
	{
		// Token: 0x060007F3 RID: 2035 RVA: 0x0002EED4 File Offset: 0x0002DED4
		public static AuthorizationResults Login(out UnivDataAdapter da, out TripleDESEncryptionClass tripleDES, out int pid)
		{
			da = null;
			tripleDES = null;
			pid = -1;
			string text = "";
			try
			{
				string currentDirectory = Directory.GetCurrentDirectory();
				object registryValue = Authorization.GetRegistryValue(Registry.LocalMachine, Authorization.registryBreakdown, "InstallPath", false);
				string text2 = (registryValue == null) ? "" : ((string)registryValue);
				if (text2.CompareTo(currentDirectory) != 0)
				{
					try
					{
						Authorization.SetRegistryValue(Registry.LocalMachine, Authorization.registryBreakdown, "InstallPath", currentDirectory, false);
					}
					catch
					{
					}
				}
			}
			catch (Exception ex)
			{
				return new AuthorizationResults("error determining start folder");
			}
			bool flag = false;
			string path;
			try
			{
				path = "c:\\ClockWork2.ini";
				if (File.Exists(path))
				{
					flag = true;
				}
				else
				{
					path = "c:\\ClockWork.ini";
					object registryValue2 = Authorization.GetRegistryValue(Registry.LocalMachine, Authorization.registryBreakdown, "ClockWorkIni_Location", false);
					if (registryValue2 != null)
					{
						path = registryValue2.ToString();
					}
				}
			}
			catch
			{
				return new AuthorizationResults("error Getting ClockWork.ini path");
			}
			bool forceAskUser = (Control.ModifierKeys & Keys.Alt) == Keys.Alt;
			bool forceAskUser2 = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
			bool flag2 = (Control.ModifierKeys & Keys.Control) == Keys.Control;
			try
			{
				if (flag)
				{
					text = null;
				}
				else
				{
					UnivConnection univConnection;
					text = Authorization.GetConnectionString(Authorization.registryBreakdown, forceAskUser2, out da, out univConnection);
				}
			}
			catch
			{
				return new AuthorizationResults("error getting connection");
			}
			tripleDES = null;
			try
			{
				if (text == null && File.Exists(path))
				{
					try
					{
						TextReader textReader = new StreamReader(path);
						string inputString = textReader.ReadLine();
						string inputString2 = textReader.ReadLine();
						string text3 = textReader.ReadLine();
						textReader.Close();
						TripleDESEncryptionClass tripleDESEncryptionClass = new TripleDESEncryptionClass();
						string text4 = tripleDESEncryptionClass.Decrypt(inputString2);
						text = tripleDESEncryptionClass.Decrypt(inputString);
						UnivConnection univConnection = UnivOleDbFactory.CreateConnection(text);
						da = univConnection.CreateDataAdapter();
						byte[][] bytes = TripleDESEncryptionClass.GetBytes(true, text4);
						byte[] key = bytes[0];
						byte[] iv = bytes[1];
						tripleDES = new TripleDESEncryptionClass(key, iv);
						if (!flag)
						{
							try
							{
								File.Delete(path);
							}
							catch
							{
							}
							if (da == null)
							{
								text = null;
							}
							else
							{
								Authorization.SetRegistryValue(Registry.CurrentUser, Authorization.registryBreakdown, "cs", text, true);
								Authorization.SetRegistryValue(Registry.CurrentUser, Authorization.registryBreakdown, "k", text4, true);
							}
						}
					}
					catch (Exception ex)
					{
						text = null;
						return new AuthorizationResults("Trying to load from clockwork.ini: " + ex.ToString());
					}
				}
			}
			catch (Exception ex)
			{
				return new AuthorizationResults("Missing connection; trying to check ClockWork.ini");
			}
			AuthorizationResults result;
			if (text == null || da == null)
			{
				result = new AuthorizationResults("unspecified error");
			}
			else
			{
				try
				{
					if (da.SelectCommand == null)
					{
						RegistryKey registryKey = Authorization.GetRegistryKey(Registry.CurrentUser, Authorization.registryBreakdown, true, true);
						object obj;
						if (registryKey != null)
						{
							obj = Authorization.GetRegistryValue(registryKey, "cs", true);
						}
						else
						{
							obj = null;
						}
						if (obj != null && obj is string)
						{
							text = (string)obj;
						}
						else
						{
							text = "";
						}
						if (text.Length > 0)
						{
							UnivConnection univConnection;
							try
							{
								univConnection = UnivOleDbFactory.CreateConnection(text);
							}
							catch
							{
								univConnection = null;
								if (text == null)
								{
									text = "NULL";
								}
								return new AuthorizationResults("Retry setcs: Invalid connectionstring! [" + text + "]");
							}
							if (univConnection != null)
							{
								da = univConnection.CreateDataAdapter();
								if (da == null || da.SelectCommand == null)
								{
									if (text == null)
									{
										text = "NULL";
									}
									if (da != null)
									{
										if (da.SelectCommand == null)
										{
										}
									}
									return new AuthorizationResults("unspecified error2");
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					return new AuthorizationResults("Attempting to set cs again a different way");
				}
				int databaseBuild = ClockWorkCore.GetDatabaseBuild(da);
				ClockWorkCore.ClockWorkLoginTypes clockWorkLoginTypes = ClockWorkCore.ClockWorkLoginTypes.Unknown;
				DataTable dataTable = new DataTable();
				DataTable dataTable2 = new DataTable();
				try
				{
					da.SelectCommand.CommandText = "SELECT misccode FROM misc WHERE misccode=1";
					da.Fill(dataTable);
					da.SelectCommand.CommandText = "SELECT misccode FROM misc WHERE misccode=2";
					da.Fill(dataTable2);
					clockWorkLoginTypes = ClockWorkCore.GetLoginType(da);
				}
				catch (Exception ex)
				{
					return new AuthorizationResults("Loading misc settings");
				}
				bool flag3 = dataTable2 != null && dataTable2.Rows.Count > 0;
				if (tripleDES == null)
				{
					try
					{
						RegistryKey registryKey = Authorization.GetRegistryKey(Registry.CurrentUser, Authorization.registryBreakdown, true, true);
						object registryValue3 = Authorization.GetRegistryValue(registryKey, "k", true);
						string password = "";
						if (registryValue3 != null)
						{
							password = (string)registryValue3;
						}
						byte[][] bytes = TripleDESEncryptionClass.GetBytes(dataTable.Rows.Count < 1, password);
						byte[] key = bytes[0];
						byte[] iv = bytes[1];
						tripleDES = new TripleDESEncryptionClass(key, iv);
						int num;
						if (tripleDES == null)
						{
							num = Authorization.SetEncryptionKey(forceAskUser, dataTable.Rows.Count < 1, out tripleDES);
						}
						else
						{
							num = 1;
						}
						if (num == 0)
						{
							Application.Exit();
						}
					}
					catch
					{
						return new AuthorizationResults("Getting encryption key");
					}
				}
				string text5 = "";
				string text6;
				try
				{
					if (clockWorkLoginTypes == ClockWorkCore.ClockWorkLoginTypes.WindowsLogin)
					{
						text6 = Environment.UserName.ToUpper();
					}
					else
					{
						ClockWorkLoginDialog clockWorkLoginDialog = new ClockWorkLoginDialog();
						object registryValue4 = Authorization.GetRegistryValue(Registry.CurrentUser, Authorization.registryBreakdown, "lu", true);
						if (registryValue4 != null)
						{
							clockWorkLoginDialog.User = registryValue4.ToString();
						}
						object registryValue5 = Authorization.GetRegistryValue(Registry.CurrentUser, Authorization.registryBreakdown, "lp", true);
						if (registryValue5 != null)
						{
							clockWorkLoginDialog.Pass = registryValue5.ToString();
						}
						DialogResult dialogResult = clockWorkLoginDialog.ShowDialog();
						if (dialogResult != DialogResult.OK)
						{
							return new AuthorizationResults("unspecified error4");
						}
						text6 = clockWorkLoginDialog.User.ToUpper();
						text5 = clockWorkLoginDialog.Pass;
					}
				}
				catch (Exception ex)
				{
					string str = clockWorkLoginTypes.ToString();
					return new AuthorizationResults("Getting current user (" + str + ")");
				}
				DataTable dataTable3;
				try
				{
					if (clockWorkLoginTypes == ClockWorkCore.ClockWorkLoginTypes.WindowsLogin)
					{
						dataTable3 = Authorization.LoadWhoAmIDetails(da, tripleDES, text6);
					}
					else
					{
						dataTable3 = new DataTable();
					}
				}
				catch (Exception ex)
				{
					return new AuthorizationResults("loading user details from db");
				}
				try
				{
					if (dataTable3 != null && dataTable3.Rows.Count > 0)
					{
						DataTable dataTable4 = Authorization.LoadUser(da, tripleDES, text6);
						if (flag3 || dataTable4.Rows.Count > 0 || clockWorkLoginTypes == ClockWorkCore.ClockWorkLoginTypes.ClockWorkLogin)
						{
							if (dataTable4.Rows.Count < 1)
							{
								return new AuthorizationResults("Invalid login.");
							}
							string userPassword = Authorization.GetUserPassword();
							byte[] inputInBytes = (byte[])dataTable4.Rows[0][1];
							string strB = tripleDES.Decrypt(inputInBytes);
							if (userPassword == null || userPassword.Trim().Length < 1 || userPassword.CompareTo(strB) != 0)
							{
								return new AuthorizationResults("Invalid login.");
							}
						}
					}
				}
				catch (Exception ex)
				{
					return new AuthorizationResults("User details were found, check if password is required");
				}
				try
				{
					if (dataTable3 == null || dataTable3.Rows.Count < 1)
					{
						DataTable dataTable4 = Authorization.LoadUser(da, tripleDES, text6);
						if (dataTable4 != null && dataTable4.Rows.Count > 0)
						{
							if (text5 == null)
							{
								text5 = Authorization.GetUserPassword();
							}
							if (text5 != null && text5.Trim().Length > 0)
							{
								dataTable4 = Authorization.LoadUser(da, tripleDES, text6);
							}
							else
							{
								dataTable4 = null;
							}
							if (dataTable4 != null && dataTable4.Rows.Count > 0)
							{
								byte[] inputInBytes2 = (byte[])dataTable4.Rows[0][1];
								string text7 = tripleDES.Decrypt(inputInBytes2);
								if (text7.CompareTo(text5) != 0)
								{
									return new AuthorizationResults("Invalid login.");
								}
								int num2 = (int)dataTable4.Rows[0][2];
								da.SelectCommand.CommandText = "SELECT student_no FROM people WHERE personid=@personid";
								da.SelectCommand.Parameters.Clear();
								da.SelectCommand.Parameters.Add("@personid", num2);
								da.SelectCommand.Parameters.Add("@true", true);
								dataTable4 = new DataTable();
								da.Fill(dataTable4);
								if (dataTable4.Rows.Count > 0)
								{
									byte[] inputInBytes3 = (byte[])dataTable4.Rows[0][0];
									string username = tripleDES.Decrypt(inputInBytes3);
									dataTable3 = Authorization.LoadWhoAmIDetails(da, tripleDES, username);
									if (dataTable3 == null)
									{
										return new AuthorizationResults("Trying to load whoami's info!: ");
									}
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					return new AuthorizationResults("User details not found, checking passwords");
				}
				text5 = null;
				if (dataTable3 == null)
				{
					result = new AuthorizationResults("DataBase access failed.");
				}
				else if (dataTable3.Rows.Count < 1)
				{
					string errMsg = "ClockWork Scheduler is not able to start up at this time.  Either you are not entered in the database as a valid user of this software (see your administrator; your current login name is '" + text6 + "') or your stored password for the database is incorrect (see the help for instructions on how to reset the password).";
					result = new AuthorizationResults(errMsg);
				}
				else if (da != null)
				{
					result = new AuthorizationResults(true);
				}
				else
				{
					result = new AuthorizationResults("unspecified error3");
				}
			}
			return result;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0002FAFC File Offset: 0x0002EAFC
		private static string GetConnectionString(string[] registryBreakdown, bool forceAskUser, out UnivDataAdapter da, out UnivConnection mainConnection)
		{
			da = null;
			mainConnection = null;
			bool flag;
			try
			{
				object registryValue = Authorization.GetRegistryValue(Registry.LocalMachine, registryBreakdown, "UseLocalMachineSettings", false);
				if (registryValue != null)
				{
					string text = registryValue.ToString().Trim();
					flag = (text.CompareTo("1") == 0);
				}
				else
				{
					flag = false;
				}
			}
			catch (Exception ex)
			{
				flag = false;
			}
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
					obj = Authorization.GetRegistryValue(Registry.CurrentUser, registryBreakdown, "cs", false);
				}
				if (obj == null)
				{
					try
					{
						obj = Authorization.GetRegistryValue(Registry.LocalMachine, registryBreakdown, "cs", false);
					}
					catch
					{
						obj = null;
					}
					if (obj != null)
					{
						TripleDESEncryptionClass tripleDESEncryptionClass = new TripleDESEncryptionClass();
						text2 = "";
						try
						{
							text2 = tripleDESEncryptionClass.Decrypt((byte[])obj);
						}
						catch (Exception ex2)
						{
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
							object registryValue2 = Authorization.GetRegistryValue(Registry.CurrentUser, registryBreakdown, valueName, false);
							if (registryValue2 == null)
							{
								object registryValue3 = Authorization.GetRegistryValue(Registry.CurrentUser, regKeyBreakdown, valueName, false);
								Authorization.SetRegistryValue(Registry.CurrentUser, registryBreakdown, valueName, registryValue3, false);
							}
						}
						obj = Authorization.GetRegistryValue(Registry.CurrentUser, registryBreakdown, "cs", false);
					}
					else
					{
						obj = Authorization.GetRegistryValue(Registry.LocalMachine, registryBreakdown, "cs", false);
					}
				}
				if (obj == null)
				{
					goto Block_12;
				}
				try
				{
					string text3 = DPAPIencryption.UnProtectData((string)obj, DPAPIencryption.GetEntropy());
					mainConnection = UnivOleDbFactory.CreateConnection(text3);
					da = mainConnection.CreateDataAdapter();
					return text3;
				}
				catch (Exception ex)
				{
					return null;
				}
			}
			mainConnection = UnivOleDbFactory.CreateConnection(text2);
			da = mainConnection.CreateDataAdapter();
			return text2;
			Block_12:
			return null;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0002FDC8 File Offset: 0x0002EDC8
		private static int SetEncryptionKey(bool forceAskUser, bool use192bit, out TripleDESEncryptionClass tripleDES)
		{
			tripleDES = null;
			bool flag;
			try
			{
				object registryValue = Authorization.GetRegistryValue(Registry.LocalMachine, Authorization.registryBreakdown, "UseLocalMachineSettings", false);
				if (registryValue != null)
				{
					string text = registryValue.ToString().Trim();
					flag = (text.CompareTo("1") == 0);
				}
				else
				{
					flag = false;
				}
			}
			catch (Exception ex)
			{
				flag = false;
			}
			string[] regKeyBreakdown = Authorization.registryBreakdown;
			object obj;
			if (flag)
			{
				obj = null;
			}
			else
			{
				obj = Authorization.GetRegistryValue(Registry.CurrentUser, regKeyBreakdown, "k", true);
			}
			if (obj == null)
			{
				obj = Authorization.GetRegistryValue(Registry.LocalMachine, Authorization.registryBreakdown, "k", false);
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
							tripleDES = new TripleDESEncryptionClass(key, iv);
							return -1;
						}
						return 0;
					}
					catch
					{
					}
				}
			}
			string text3 = "";
			if (obj != null && !forceAskUser)
			{
				text3 = (string)obj;
			}
			int result;
			if (text3.Length > 0)
			{
				byte[][] bytes = TripleDESEncryptionClass.GetBytes(use192bit, text3);
				byte[] key = bytes[0];
				byte[] iv = bytes[1];
				tripleDES = new TripleDESEncryptionClass(key, iv);
				result = -1;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0002FF8C File Offset: 0x0002EF8C
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

		// Token: 0x060007F7 RID: 2039 RVA: 0x00030010 File Offset: 0x0002F010
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

		// Token: 0x060007F8 RID: 2040 RVA: 0x00030090 File Offset: 0x0002F090
		public static string GetRegistryValueString(RegistryKey StartKey, string[] RegKeyBreakdown, string valueName, bool isEncrypted)
		{
			RegistryKey registryKey = Authorization.GetRegistryKey(StartKey, RegKeyBreakdown, false, false);
			object registryValue = Authorization.GetRegistryValue(registryKey, valueName, isEncrypted);
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

		// Token: 0x060007F9 RID: 2041 RVA: 0x000300D4 File Offset: 0x0002F0D4
		public static object GetRegistryValue(RegistryKey StartKey, string[] RegKeyBreakdown, string valueName, bool isEncrypted)
		{
			RegistryKey registryKey = Authorization.GetRegistryKey(StartKey, RegKeyBreakdown, false, false);
			return Authorization.GetRegistryValue(registryKey, valueName, isEncrypted);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x000300F8 File Offset: 0x0002F0F8
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

		// Token: 0x060007FB RID: 2043 RVA: 0x00030164 File Offset: 0x0002F164
		public static object SetRegistryValue(RegistryKey StartKey, string[] RegKeyBreakdown, string valueName, object valueObject, bool isEncrypted)
		{
			RegistryKey registryKey = Authorization.GetRegistryKey(StartKey, RegKeyBreakdown, true, true);
			return Authorization.SetRegistryValue(registryKey, valueName, valueObject, isEncrypted);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0003018C File Offset: 0x0002F18C
		private static DataTable LoadWhoAmIDetails(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, string username)
		{
			byte[] parameterValue = tripleDES.Encrypt(username);
			da.SelectCommand.CommandText = "SELECT p.personID,p.firstName,p.lastName,p.student_no,p.isActive,p.dateadded,pg.groupid,pg.isprimarygroup FROM people p LEFT JOIN peoplegroups pg ON pg.personid=p.personid WHERE p.student_no=@student_no AND p.isactive=@true";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@student_no", parameterValue);
			da.SelectCommand.Parameters.Add("@true", true);
			DataTable dataTable = new DataTable();
			try
			{
				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				return null;
			}
			return dataTable;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00030228 File Offset: 0x0002F228
		private static DataTable LoadUser(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, string username)
		{
			byte[] parameterValue = tripleDES.Encrypt(username);
			da.SelectCommand.CommandText = "SELECT username,pass,personid FROM userinfo WHERE username=@username";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@username", parameterValue);
			DataTable dataTable = new DataTable();
			try
			{
				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				return null;
			}
			return dataTable;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x000302A8 File Offset: 0x0002F2A8
		private static string GetUserPassword()
		{
			InputPassword inputPassword = new InputPassword();
			DialogResult dialogResult = inputPassword.ShowDialog();
			string result;
			if (dialogResult == DialogResult.OK)
			{
				result = inputPassword.GetPassword();
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x04000401 RID: 1025
		private const string _licenseParameters = "<LicenseParameters><RSAKeyValue><Modulus>pXmeioxDvTqXkvmTqjfxVliHnoLiAE48CYj0X9Q9qWwH3Wl0S53F8muXcAvkHevOX71x7xJA0Z9opREqXlrlkYCORYPmrgqOuvNCK7vj/QI+cXFym7QFBB8osbWDL54MZ7K/3fGMplTYQJw7kafGVGSJxEQ+PgY9Kst9xJ5v88k=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue><DesignSignature>jxQbr2lE7XKzaLei+tiHe9NaHdqlFwKLHwzzBGjmqr03IHqSmh0efvmKj3tGKVvIBOkKDlAPGoAVegI3yaGug8262KP27dtf1HrIXN5PMnHrnhT9lReAg7dtrkTOd8nLF57vkW7iDquJHZHqZx2JbvD1RINy8zV4LgiI3zFPjtY=</DesignSignature><RuntimeSignature>lmwPpAy1hWhcdFB+RuI0i2lRJaQ3tcGKO/8wEY4iKcXS3TRFyLSKghmL0fi3bfLPQkuir6vGjJnpqRgX7OXVUNSx2IFZ7STWmDjuanAzHoONVJNnTmzQEnsIarg3upr84boPKqYflZfLUmKVjeKjic8xtSFuwgpod9LTufqcPDU=</RuntimeSignature></LicenseParameters>";

		// Token: 0x04000402 RID: 1026
		public static string[] registryBreakdown = new string[]
		{
			"Software",
			"TechnoPro",
			"ClockWork"
		};
	}
}
