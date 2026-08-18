using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Net;
using System.Windows.Forms;
using ClockWorkAPI.EntityExtensions;
using EncryptionClassLibrary;
using Microsoft.Win32;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.UI.ClientManager.OldUserSettings;
using UnivOleDb;

namespace ClockWorkAPI
{
	// Token: 0x02000033 RID: 51
	public class ConnectionBaseInfo
	{
		// Token: 0x0600025E RID: 606 RVA: 0x0000DD9E File Offset: 0x0000CD9E
		public void SetClockWorkFavourite(string clockWorkFavourite)
		{
			this.clockWorkFavourite = clockWorkFavourite;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000DDA8 File Offset: 0x0000CDA8
		public string License
		{
			get
			{
				return this.license;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000DDC0 File Offset: 0x0000CDC0
		public string[] RegistryBreakdown
		{
			get
			{
				return this.registryBreakdown;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000DDD8 File Offset: 0x0000CDD8
		public string StartDirectory
		{
			get
			{
				return this.startDirectory;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000DDF0 File Offset: 0x0000CDF0
		public bool ManualOverride
		{
			get
			{
				return this.manualOverride;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000DE08 File Offset: 0x0000CE08
		public string ManualConnectionInfoOverride
		{
			get
			{
				return this.manualConnectionInfoOverride;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000DE20 File Offset: 0x0000CE20
		public LogType LogType
		{
			get
			{
				return this.logType;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000DE38 File Offset: 0x0000CE38
		public string LogConnection
		{
			get
			{
				return this.logConnection;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000DE50 File Offset: 0x0000CE50
		public Log Log
		{
			get
			{
				return this.log;
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000DE68 File Offset: 0x0000CE68
		public string ParseConnectionString(string name)
		{
			NameValueCollection nameValueCollection = ConnectionBaseInfo.ParseString(this.connectionString, ';');
			string text = nameValueCollection[name];
			return (text == null) ? "" : text;
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000DE9C File Offset: 0x0000CE9C
		public string DatabaseServer
		{
			get
			{
				return this.ParseConnectionString("data source");
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000DEBC File Offset: 0x0000CEBC
		public string DatabaseName
		{
			get
			{
				return this.ParseConnectionString("initial catalogue");
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000DEDC File Offset: 0x0000CEDC
		public bool Working
		{
			get
			{
				return this.da != null;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000DEFC File Offset: 0x0000CEFC
		// (set) Token: 0x0600026C RID: 620 RVA: 0x0000DF14 File Offset: 0x0000CF14
		public string ConnectionString
		{
			get
			{
				return this.connectionString;
			}
			set
			{
				this.connectionString = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000DF20 File Offset: 0x0000CF20
		// (set) Token: 0x0600026E RID: 622 RVA: 0x0000DF38 File Offset: 0x0000CF38
		public string DatabasePassword
		{
			get
			{
				return this.databasePassword;
			}
			set
			{
				this.databasePassword = value;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000DF44 File Offset: 0x0000CF44
		public bool SshUse
		{
			get
			{
				return this.sshConnectionString != null && this.sshConnectionString.Trim().Length > 0;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000DF74 File Offset: 0x0000CF74
		// (set) Token: 0x06000271 RID: 625 RVA: 0x0000DF8C File Offset: 0x0000CF8C
		public string SshConnectionString
		{
			get
			{
				return this.sshConnectionString;
			}
			set
			{
				this.sshConnectionString = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000DF98 File Offset: 0x0000CF98
		public UnivDataAdapter Da
		{
			get
			{
				return this.da;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000DFB0 File Offset: 0x0000CFB0
		public TripleDESEncryptionClass TripleDES
		{
			get
			{
				return this.tripleDES;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000DFC8 File Offset: 0x0000CFC8
		public PersonBaseDTO WhoAmI
		{
			get
			{
				return this.whoAmI;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000DFE0 File Offset: 0x0000CFE0
		public bool SslEncrypted
		{
			get
			{
				return this.sslEncrypted;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000DFF8 File Offset: 0x0000CFF8
		public string OverrideClockWorkUsername
		{
			get
			{
				return this.overrideClockWorkUsername;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000E010 File Offset: 0x0000D010
		public string OverrideClockWorkPassword
		{
			get
			{
				return this.overrideClockWorkPassword;
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000E028 File Offset: 0x0000D028
		public void SetOverrideClockWorkUsername(ref string s)
		{
			this.overrideClockWorkUsername = s;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000E033 File Offset: 0x0000D033
		public void SetOverrideClockWorkPassword(ref string s)
		{
			this.overrideClockWorkPassword = s;
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000E040 File Offset: 0x0000D040
		public string Username
		{
			get
			{
				NameValueCollection nameValueCollection = ConnectionBaseInfo.ParseString(this.connectionString, ';');
				string text = nameValueCollection["data source"];
				return (text == null) ? "" : text;
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000E078 File Offset: 0x0000D078
		public static Exception GetConnectionStringAndDatabasePassword(string regName, out string connectionString, out TripleDESEncryptionClass tripleDES)
		{
			Exception result;
			try
			{
				string valueName = regName + "_cs";
				string valueName2 = regName + "_k";
				string[] array = new string[]
				{
					"Software",
					"TechnoPro",
					"ClockWork"
				};
				string[] array2 = new string[array.Length + 1];
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = array[i];
				}
				array2[array.Length] = "mc";
				object registryValue = ClockWorkCore.GetRegistryValue(Registry.LocalMachine, array2, valueName, false);
				object registryValue2 = ClockWorkCore.GetRegistryValue(Registry.LocalMachine, array2, valueName2, false);
				byte[] encData = Convert.FromBase64String((string)registryValue);
				byte[] encData2 = Convert.FromBase64String((string)registryValue2);
				string text = DPAPIEncryptionV2.ByteArrayToString(DPAPIEncryptionV2.UnProtectData(encData, ProtectionScope.LocalMachine));
				string text2 = DPAPIEncryptionV2.ByteArrayToString(DPAPIEncryptionV2.UnProtectData(encData2, ProtectionScope.LocalMachine));
				if (string.IsNullOrEmpty(text))
				{
					tripleDES = null;
					connectionString = "";
					result = new Exception("Missing connection string from local machine registry");
				}
				else if (string.IsNullOrEmpty(text2))
				{
					tripleDES = null;
					connectionString = "";
					result = new Exception("Missing database password from local machine registry");
				}
				else
				{
					connectionString = text;
					string password = text2;
					tripleDES = new TripleDESEncryptionClass(EncryptionType.TripleDES_192bit, password);
					result = null;
				}
			}
			catch (Exception ex)
			{
				tripleDES = null;
				connectionString = "";
				result = ex;
			}
			return result;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000E1F8 File Offset: 0x0000D1F8
		public ConnectionBaseInfo(bool ManualOverride, string ManualConnectionInfoOverride, LogType logType, string logConnection)
		{
			this.registryBreakdown = ClockWorkCore.registryBreakdown;
			this.startDirectory = ClockWorkCore.GetStartDirectory();
			this.logType = logType;
			this.logConnection = logConnection;
			this.log = new Log(logType, logConnection);
			if (logType == LogType.File && File.Exists(logConnection))
			{
				File.Delete(logConnection);
			}
			this.ResetSettings();
			this.manualConnectionInfoOverride = ManualConnectionInfoOverride;
			this.manualOverride = ManualOverride;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000E2A4 File Offset: 0x0000D2A4
		private static void ParseNameEqualsValueString(string s, out string name, out string val)
		{
			int num = s.IndexOf('=');
			if (num > 0)
			{
				name = s.Substring(0, num);
				if (num < s.Length - 1)
				{
					val = s.Substring(num + 1);
				}
				else
				{
					val = "";
				}
			}
			else
			{
				name = "";
				val = "";
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000E30C File Offset: 0x0000D30C
		public static NameValueCollection ParseString(string s, char delimiter)
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			bool flag = false;
			string text = "";
			foreach (char c in s)
			{
				if (c == delimiter && !flag)
				{
					if (text.Length > 0)
					{
						string text2;
						string value;
						ConnectionBaseInfo.ParseNameEqualsValueString(text, out text2, out value);
						if (text2.Length > 0)
						{
							nameValueCollection.Add(text2, value);
						}
					}
					text = "";
				}
				else
				{
					if (c == '"')
					{
						flag = !flag;
					}
					text += c;
				}
			}
			if (text.Length > 0)
			{
				string text2;
				string value;
				ConnectionBaseInfo.ParseNameEqualsValueString(text, out text2, out value);
				if (text2.Length > 0)
				{
					nameValueCollection.Add(text2, value);
				}
			}
			return nameValueCollection;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000E40E File Offset: 0x0000D40E
		private void ResetSettings()
		{
			this.storeType = InfoStoreType.Unknown;
			this.manualConnectionInfoOverride = null;
			this.manualOverride = false;
			this.ResetTempSettings();
			this.license = "";
			this.whoAmI = null;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000E43F File Offset: 0x0000D43F
		private void ResetTempSettings()
		{
			this.connectionString = "";
			this.sshConnectionString = "";
			this.location = "";
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000281 RID: 641 RVA: 0x0000E464 File Offset: 0x0000D464
		// (remove) Token: 0x06000282 RID: 642 RVA: 0x0000E4A0 File Offset: 0x0000D4A0
		public event LoginCompleteHandler OnLoginComplete;

		// Token: 0x06000283 RID: 643 RVA: 0x0000E4DC File Offset: 0x0000D4DC
		private void FireLoginCompleteHandler(bool success, Exception exception)
		{
			if (this.OnLoginComplete != null)
			{
				this.OnLoginComplete(this, success, exception);
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000E508 File Offset: 0x0000D508
		public void LoginASync(LoginCompleteHandler handler, bool reloadSettings, bool loginUser, bool clearTempVariablesWhenConnected)
		{
			this.OnLoginComplete += handler;
			BackgroundWorker backgroundWorker = new BackgroundWorker();
			backgroundWorker.DoWork += this.bw_DoWork;
			backgroundWorker.RunWorkerAsync(new ConnectionBaseInfo.LoginReq
			{
				ReloadSettings = reloadSettings,
				LoginUser = loginUser,
				ClearTempVariablesWhenConnected = clearTempVariablesWhenConnected
			});
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000E560 File Offset: 0x0000D560
		private void bw_DoWork(object sender, DoWorkEventArgs e)
		{
			ConnectionBaseInfo.LoginReq loginReq = (ConnectionBaseInfo.LoginReq)e.Argument;
			Exception ex = this.Login(loginReq.ReloadSettings, loginReq.LoginUser, loginReq.ClearTempVariablesWhenConnected);
			this.FireLoginCompleteHandler(ex == null, ex);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000E5A0 File Offset: 0x0000D5A0
		public Exception Login(bool reloadSettings, bool loginUser, bool clearTempVariablesWhenConnected)
		{
			Exception result;
			if (!reloadSettings)
			{
				Exception ex = this.TryToConnect(loginUser, clearTempVariablesWhenConnected);
				result = ex;
			}
			else
			{
				Exception ex;
				if (this.manualConnectionInfoOverride != null && this.manualConnectionInfoOverride.Length > 0)
				{
					ex = this.LoadSettingsAndTryToConnect(InfoStoreType.ManualConnectionInfoOverride, loginUser, clearTempVariablesWhenConnected);
					if (ex == null)
					{
						return ex;
					}
				}
				this.location = "";
				string path = Path.Combine(Directory.GetCurrentDirectory(), "clockwork2.ini");
				if (File.Exists(path))
				{
					this.location = path;
					ex = this.LoadSettingsAndTryToConnect(InfoStoreType.ClockWork2_ini_local, loginUser, clearTempVariablesWhenConnected);
					if (ex == null)
					{
						return ex;
					}
				}
				path = "c:\\clockwork2.ini";
				if (File.Exists(path))
				{
					this.location = path;
					ex = this.LoadSettingsAndTryToConnect(InfoStoreType.ClockWork2_ini, loginUser, clearTempVariablesWhenConnected);
					if (ex == null)
					{
						return ex;
					}
				}
				path = "c:\\clockwork.ini";
				string registryValueString = ClockWorkCore.GetRegistryValueString(Registry.LocalMachine, this.registryBreakdown, "ClockWorkIni_Location", false);
				if (registryValueString != null && registryValueString.Length > 0)
				{
					path = registryValueString;
				}
				if (File.Exists(path))
				{
					this.location = path;
					ex = this.LoadSettingsAndTryToConnect(InfoStoreType.ClockWork_ini, loginUser, clearTempVariablesWhenConnected);
					if (ex == null)
					{
						return ex;
					}
				}
				object registryValue = ClockWorkCore.GetRegistryValue(Registry.LocalMachine, this.registryBreakdown, "UseLocalMachineSettings", false);
				if (registryValue != null && registryValue.ToString().Trim().CompareTo("1") == 0)
				{
					ex = this.LoadSettingsAndTryToConnect(InfoStoreType.MachineRegistry, loginUser, clearTempVariablesWhenConnected);
					if (ex == null)
					{
						return ex;
					}
				}
				if (this.clockWorkFavourite.Length > 0)
				{
					ex = this.LoadSettingsAndTryToConnect(InfoStoreType.UserRegistryFavourites, loginUser, clearTempVariablesWhenConnected);
				}
				else
				{
					ex = this.LoadSettingsAndTryToConnect(InfoStoreType.UserRegistry, loginUser, clearTempVariablesWhenConnected);
				}
				if (ex == null)
				{
					result = ex;
				}
				else
				{
					result = ex;
				}
			}
			return result;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000E7B0 File Offset: 0x0000D7B0
		private Exception LoadSettingsAndTryToConnect(InfoStoreType infoStoreType, bool loginUser, bool clearTempVariablesWhenConnected)
		{
			Exception ex = this.LoadConnectionInfo(infoStoreType);
			Exception result;
			if (ex != null)
			{
				result = ex;
			}
			else
			{
				ex = this.TryToConnect(loginUser, clearTempVariablesWhenConnected);
				result = ex;
			}
			return result;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000E7E0 File Offset: 0x0000D7E0
		private Exception TryToConnect(bool loginUser, bool clearTempVariablesWhenConnected)
		{
			if (this.sshConnectionString != null && this.sshConnectionString.Trim().Length > 0)
			{
			}
			Exception ex = this.SetUnivDataAdapter();
			Exception result;
			if (ex != null)
			{
				result = ex;
			}
			else
			{
				ex = this.SetTripleDES();
				if (ex != null)
				{
					result = ex;
				}
				else
				{
					if (clearTempVariablesWhenConnected)
					{
						this.ResetTempSettings();
					}
					if (loginUser)
					{
						DataTable dataTable = new DataTable();
						this.da.SelectCommand.CommandText = "SELECT misccode,miscstring FROM misc WHERE misccode=101";
						string text;
						this.da.Fill(dataTable, out text);
						if (text != null && text.Length > 0)
						{
							return new Exception("Database connection faulty! " + text);
						}
						string text2 = (dataTable.Rows.Count > 0) ? dataTable.Rows[0][1].ToString().Trim().ToLower() : "windowslogin";
						string text3 = text2;
						LoginMethod loginMethod;
						if (text3 != null)
						{
							if (text3 == "windowslogin")
							{
								loginMethod = LoginMethod.None;
								goto IL_16C;
							}
							if (text3 == "ldap")
							{
								loginMethod = LoginMethod.LDAP;
								goto IL_16C;
							}
							if (text3 == "domain")
							{
								loginMethod = LoginMethod.ActiveDirectory;
								goto IL_16C;
							}
							if (text3 == "clockworkuser")
							{
								loginMethod = LoginMethod.ClockWorkUser;
								goto IL_16C;
							}
						}
						loginMethod = LoginMethod.ClockWorkUser;
						IL_16C:
						ex = this.LoginToClockWork(loginMethod);
					}
					result = ex;
				}
			}
			return result;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000E96C File Offset: 0x0000D96C
		private Exception LoadConnectionInfo(InfoStoreType infoStoreType)
		{
			this.ResetSettings();
			Exception result;
			switch (infoStoreType)
			{
			case InfoStoreType.ClockWork_ini:
			case InfoStoreType.ClockWork2_ini:
			case InfoStoreType.ClockWork2_ini_local:
			case InfoStoreType.ManualConnectionInfoOverride:
				if (infoStoreType == InfoStoreType.ClockWork2_ini)
				{
					this.location = "c:\\clockwork2.ini";
				}
				else if (infoStoreType == InfoStoreType.ClockWork_ini)
				{
					this.location = "c:\\clockwork.ini";
				}
				else if (infoStoreType == InfoStoreType.ClockWork2_ini_local)
				{
					this.location = Path.Combine(Directory.GetCurrentDirectory(), "clockwork2.ini");
				}
				if (this.location == null || this.location.Length < 1)
				{
					result = new Exception("Missing filename");
				}
				else if (!File.Exists(this.location))
				{
					result = new Exception("File doesn't exist [" + this.location + "]");
				}
				else if (infoStoreType == InfoStoreType.ManualConnectionInfoOverride)
				{
					try
					{
						byte[] array = ClockWorkCore.StringToBytes(this.location, false, null);
						MemoryStream memoryStream = new MemoryStream(array.Length);
						memoryStream.Write(array, 0, array.Length);
						TextReader tr = new StreamReader(memoryStream);
						result = this.LoadConnectionInfo_Stream(tr);
					}
					catch (Exception ex)
					{
						result = ex;
					}
				}
				else
				{
					try
					{
						TextReader tr = new StreamReader(this.location);
						Exception ex2 = this.LoadConnectionInfo_Stream(tr);
						if (infoStoreType == InfoStoreType.ClockWork_ini)
						{
							string text = "c:\\clockwork2.ini";
							if (File.Exists(text))
							{
								string registryValueString = ClockWorkCore.GetRegistryValueString(Registry.LocalMachine, this.registryBreakdown, "ClockWorkIni_Location", false);
								string sourceFileName = (registryValueString != null && registryValueString.Length > 0) ? registryValueString : "c:\\clockwork.ini";
								File.Copy(sourceFileName, text, true);
							}
							else
							{
								ClockWorkCore.SetRegistryValueCurrentUser("cs", this.connectionString, true);
								ClockWorkCore.SetRegistryValueCurrentUser("k", this.databasePassword, true);
							}
							File.Delete(this.location);
							this.location = "";
						}
						result = ex2;
					}
					catch (Exception ex3)
					{
						result = ex3;
					}
				}
				break;
			case InfoStoreType.UserRegistry:
				result = this.LoadConnectionInfo_Registry(Registry.CurrentUser);
				break;
			case InfoStoreType.MachineRegistry:
				result = this.LoadConnectionInfo_Registry(Registry.LocalMachine);
				break;
			case InfoStoreType.ManualOverride:
				result = new Exception("Not yet implemented!");
				break;
			default:
			{
				string str = "Store type not supported: ";
				int num = (int)infoStoreType;
				result = new Exception(str + num.ToString());
				break;
			}
			}
			if (this.connectionString.IndexOf("xenocode") >= 0)
			{
				string text2 = string.Concat(new string[]
				{
					"Provider=SQLOLEDB.1;Data Source=.\\",
					Environment.ExpandEnvironmentVariables("%SQLXENOCODE%"),
					";AttachDBFilename=",
					Environment.ExpandEnvironmentVariables("%AttachDBFilename%"),
					";Integrated Security=True;User Instance=True;Connect Timeout=60"
				});
				this.connectionString = text2;
				this.isXenocode = true;
			}
			return result;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000EC68 File Offset: 0x0000DC68
		private Exception LoadConnectionInfo_RegistryFavourties(RegistryKey startKey, string clockWorkFavourite)
		{
			string[] array = new string[this.registryBreakdown.Length + 1];
			for (int i = 0; i < this.registryBreakdown.Length; i++)
			{
				array[i] = this.registryBreakdown[i];
			}
			array[array.Length - 1] = "mc";
			Exception result;
			try
			{
				this.connectionString = ClockWorkCore.GetRegistryValueString(startKey, array, clockWorkFavourite + "_cs", true);
				this.databasePassword = ClockWorkCore.GetRegistryValueString(startKey, array, clockWorkFavourite + "_k", true);
				this.sshConnectionString = ClockWorkCore.GetRegistryValueString(startKey, array, clockWorkFavourite + "_sh", true);
				result = null;
			}
			catch (Exception ex)
			{
				result = ex;
			}
			return result;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000ED20 File Offset: 0x0000DD20
		private Exception LoadConnectionInfo_WebService(string url, string uname, string pwd)
		{
			string text;
			if (url.IndexOf('?') > 0)
			{
				text = url + "&";
			}
			else
			{
				text = url + "?";
			}
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"uname=",
				uname,
				"&pwd=",
				pwd
			});
			WebClient webClient = new WebClient();
			Stream stream = webClient.OpenRead(text);
			TextReader textReader = new StreamReader(stream);
			Exception result = this.LoadConnectionInfo_Stream(textReader);
			textReader.Close();
			return result;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000EDC0 File Offset: 0x0000DDC0
		protected Exception LoadConnectionInfo_Stream(TextReader tr)
		{
			Exception result;
			try
			{
				string text = tr.ReadLine();
				if (text.Trim().Length < 1)
				{
					string text2 = tr.ReadLine();
					string text3 = tr.ReadLine();
					string text4 = tr.ReadLine();
					TripleDESEncryptionClass tripleDESEncryptionClass = new TripleDESEncryptionClass();
					string url = text2;
					string uname = text3;
					string pwd = text4;
					result = this.LoadConnectionInfo_WebService(url, uname, pwd);
				}
				else
				{
					string inputString = tr.ReadLine();
					this.license = tr.ReadLine();
					tr.Close();
					tr = null;
					TripleDESEncryptionClass tripleDESEncryptionClass = new TripleDESEncryptionClass();
					this.databasePassword = tripleDESEncryptionClass.Decrypt(inputString);
					this.connectionString = tripleDESEncryptionClass.Decrypt(text);
					result = null;
				}
			}
			catch (Exception ex)
			{
				result = ex;
			}
			return result;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000EE88 File Offset: 0x0000DE88
		private Exception LoadConnectionInfo_Registry(RegistryKey startKey)
		{
			Exception result;
			try
			{
				this.connectionString = ClockWorkCore.GetRegistryValueString(startKey, this.RegistryBreakdown, "cs", true);
				this.databasePassword = ClockWorkCore.GetRegistryValueString(startKey, this.RegistryBreakdown, "k", true);
				this.sshConnectionString = ClockWorkCore.GetRegistryValueString(startKey, this.RegistryBreakdown, "sh", true);
				result = null;
			}
			catch (Exception ex)
			{
				result = ex;
			}
			return result;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000EEFC File Offset: 0x0000DEFC
		protected virtual Exception SetUnivDataAdapter()
		{
			if (this.connectionString != null && this.connectionString.Length > 0)
			{
				try
				{
					this.sslEncrypted = (this.connectionString.ToLower().IndexOf("encrypt=true") >= 0);
					UnivConnection univConnection = UnivOleDbFactory.CreateConnection(this.connectionString);
					this.da = univConnection.CreateDataAdapter();
					return null;
				}
				catch
				{
					return null;
				}
			}
			return null;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000EF88 File Offset: 0x0000DF88
		private Exception LoginToClockWork(LoginMethod loginMethod)
		{
			bool flag = (Control.ModifierKeys & Keys.Control) == Keys.Control;
			bool flag2 = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
			Exception result;
			switch (loginMethod)
			{
			case LoginMethod.None:
			{
				string text = Environment.UserName.ToUpper();
				Exception ex = this.LoadWhoAmI(text);
				if (ex != null)
				{
					result = ex;
				}
				else
				{
					byte[] parameterValue = this.tripleDES.Encrypt(text);
					this.da.SelectCommand.CommandText = "SELECT personid FROM userinfo WHERE username=@winlogone AND NOT personid IN (SELECT personid FROM people WHERE isactive=0)";
					this.da.SelectCommand.Parameters.Clear();
					this.da.SelectCommand.Parameters.Add("@winlogone", parameterValue);
					DataTable dataTable = new DataTable();
					string text2;
					this.da.Fill(dataTable, out text2);
					if (!string.IsNullOrEmpty(text2))
					{
						this.Abort();
						result = new Exception(text2);
					}
					else if (dataTable.Rows.Count > 0)
					{
						ex = this.LoginUsingClockWorkLogin(text);
						if (ex == null)
						{
							if ((flag || flag2) && this.whoAmI.CoreGroup == eCoreGroupDTO.Admin)
							{
								this.LetUserLoginAsOtherUser();
							}
							result = null;
						}
						else
						{
							this.Abort();
							result = ex;
						}
					}
					else
					{
						if (flag && this.whoAmI.CoreGroup == eCoreGroupDTO.Admin)
						{
							this.LetUserLoginAsOtherUser();
						}
						result = null;
					}
				}
				break;
			}
			case LoginMethod.ClockWorkUser:
			case LoginMethod.LDAP:
			case LoginMethod.ActiveDirectory:
			{
				Exception ex = this.LoginUsingClockWorkLogin(null, loginMethod);
				if (ex == null)
				{
					result = null;
				}
				else
				{
					this.Abort();
					result = ex;
				}
				break;
			}
			default:
				result = new Exception("Not implemented yet.");
				break;
			}
			return result;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000F160 File Offset: 0x0000E160
		private void Abort()
		{
			this.ResetTempSettings();
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000F16C File Offset: 0x0000E16C
		private Exception LoginUsingClockWorkLogin(string forceUsername)
		{
			return this.LoginUsingClockWorkLogin(forceUsername, LoginMethod.ClockWorkUser);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000F188 File Offset: 0x0000E188
		private Exception LoginUsingClockWorkLogin(string forceUsername, LoginMethod loginMethodToTryFirst)
		{
			bool flag = forceUsername != null && forceUsername.Length > 0;
			string user = flag ? forceUsername : ClockWorkCore.GetRegistryValueString(Registry.CurrentUser, this.RegistryBreakdown, "lu", true);
			string pass = ClockWorkCore.GetRegistryValueString(Registry.CurrentUser, this.RegistryBreakdown, "lp", true);
			DataTable dataTable = new DataTable();
			string text = null;
			this.da.SelectCommand.CommandText = "SELECT settingvalue FROM settingsgroups WHERE groupid=-1 AND settingcode=" + 360.ToString();
			this.da.Fill(dataTable, out text);
			Exception result;
			if (!string.IsNullOrEmpty(text))
			{
				result = new Exception("Faulty connection: " + text);
			}
			else
			{
				bool flag2 = dataTable.Rows.Count <= 0 || (int)dataTable.Rows[0][0] != 0;
				Exception ex = new Exception("User aborted.");
				string text2 = "";
				ClockWorkLoginDialog clockWorkLoginDialog;
				bool flag3;
				ClockWorkLoginDialogPasswordChange clockWorkLoginDialogPasswordChange;
				byte[] parameterValue2;
				bool flag4;
				bool flag5;
				for (;;)
				{
					clockWorkLoginDialog = new ClockWorkLoginDialog();
					clockWorkLoginDialog.User = user;
					clockWorkLoginDialog.Pass = pass;
					if (flag)
					{
						clockWorkLoginDialog.DisableUsernameField();
					}
					if (text2.Length > 0)
					{
						clockWorkLoginDialog.ShowMessage(text2);
						text2 = "";
					}
					DialogResult dialogResult;
					if (this.overrideClockWorkUsername != null)
					{
						clockWorkLoginDialog.User = this.overrideClockWorkUsername;
						clockWorkLoginDialog.Pass = this.overrideClockWorkPassword;
						dialogResult = DialogResult.OK;
					}
					else
					{
						dialogResult = clockWorkLoginDialog.ShowDialog();
					}
					Exception ex2;
					if (dialogResult == DialogResult.OK)
					{
						if (loginMethodToTryFirst == LoginMethod.LDAP)
						{
							this.da.SelectCommand.CommandText = "SELECT misccode,miscstring FROM misc WHERE misccode=" + 1101.ToString();
							dataTable = new DataTable();
							this.da.Fill(dataTable);
							if (dataTable.Rows.Count > 0)
							{
								string settings = dataTable.Rows[0][1].ToString();
								string serverName;
								int port;
								string dc;
								string authTypeStr;
								string lookupAttribute;
								string returnAttributes;
								LDAP.ParseLdapSettings(settings, out serverName, out port, out dc, out authTypeStr, out lookupAttribute, out returnAttributes);
								LDAP.IsAuthenticatedV3(serverName, port, dc, lookupAttribute, returnAttributes, authTypeStr, clockWorkLoginDialog.User, clockWorkLoginDialog.Pass, out ex2);
								if (ex2 != null)
								{
									MessageBox.Show("Ldap failed: " + ex2.ToString() + "; trying clockwork login...");
								}
							}
							else
							{
								ex2 = new Exception("noldapsettings");
							}
						}
						else if (loginMethodToTryFirst == LoginMethod.ActiveDirectory)
						{
							ex2 = new Exception("Active directory not implemented yet.");
							this.da.SelectCommand.CommandText = "SELECT misccode,miscstring FROM misc WHERE misccode=" + 1101.ToString();
							dataTable = new DataTable();
							this.da.Fill(dataTable);
							if (dataTable.Rows.Count > 0)
							{
								string settings = dataTable.Rows[0][1].ToString();
								string serverName;
								int port;
								string dc;
								string authTypeStr;
								string lookupAttribute;
								string returnAttributes;
								LDAP.ParseLdapSettings(settings, out serverName, out port, out dc, out authTypeStr, out lookupAttribute, out returnAttributes);
								LDAP.IsAuthenticatedV3(serverName, port, dc, lookupAttribute, returnAttributes, authTypeStr, clockWorkLoginDialog.User, clockWorkLoginDialog.Pass, out ex2);
								if (ex2 != null)
								{
									MessageBox.Show("Active directory failed: " + ex2.ToString() + "; trying clockwork login...");
								}
							}
							else
							{
								ex2 = new Exception("noldapsettings");
							}
						}
						else
						{
							ex2 = new Exception("noldapcheck");
						}
						if (ex2 != null)
						{
							ex2 = this.CheckClockWorkPassword(-1, clockWorkLoginDialog.User, clockWorkLoginDialog.Pass, out flag3);
						}
						else
						{
							flag3 = false;
						}
					}
					else if (clockWorkLoginDialog.ConnectionFavourite != null)
					{
						this.connectionString = clockWorkLoginDialog.ConnectionFavourite.ConnectionString;
						this.databasePassword = clockWorkLoginDialog.ConnectionFavourite.Password;
						ex2 = this.SetUnivDataAdapter();
						this.SetTripleDES();
						flag3 = false;
					}
					else
					{
						ex2 = null;
						flag3 = false;
					}
					if (dialogResult == DialogResult.Retry || flag3)
					{
						clockWorkLoginDialogPasswordChange = new ClockWorkLoginDialogPasswordChange();
						clockWorkLoginDialogPasswordChange.oldUsername = clockWorkLoginDialog.User;
						clockWorkLoginDialogPasswordChange.oldPassword = clockWorkLoginDialog.Pass;
						DialogResult dialogResult2 = clockWorkLoginDialogPasswordChange.ShowDialog();
						if (dialogResult2 == DialogResult.OK)
						{
							ex2 = this.CheckClockWorkPassword(-1, clockWorkLoginDialogPasswordChange.oldUsername, clockWorkLoginDialogPasswordChange.oldPassword);
							if (ex2 != null)
							{
								this.whoAmI = null;
								user = clockWorkLoginDialogPasswordChange.oldUsername;
								pass = "";
								text2 = "Invalid existing username/password entered; nothing was done.";
							}
							else
							{
								byte[] parameterValue = this.tripleDES.Encrypt(clockWorkLoginDialogPasswordChange.newPassword);
								parameterValue2 = this.tripleDES.Encrypt(clockWorkLoginDialogPasswordChange.oldUsername);
								this.da.SelectCommand.CommandText = "UPDATE userinfo SET pass=@newpass WHERE username=@username";
								this.da.SelectCommand.Parameters.Clear();
								this.da.SelectCommand.Parameters.Add("@username", parameterValue2);
								this.da.SelectCommand.Parameters.Add("@newpass", parameterValue);
								this.da.Fill(new DataTable(), out text);
								if (text == null || text.Length < 1)
								{
									break;
								}
								MessageBox.Show("Something went wrong (" + text + "); your password may not have been changed.  Please try logging in with the new password, or the old password if that doesn't work.");
							}
						}
					}
					else if (dialogResult == DialogResult.OK)
					{
						flag4 = ((Control.ModifierKeys & Keys.Control) == Keys.Control);
						flag5 = ((Control.ModifierKeys & Keys.Shift) == Keys.Shift);
						if (ex2 == null)
						{
							goto IL_6C8;
						}
						this.whoAmI = null;
						user = clockWorkLoginDialog.User;
						pass = "";
						if (this.overrideClockWorkUsername != null)
						{
							goto Block_27;
						}
						text2 = "Invalid username/password entered";
					}
					else if (clockWorkLoginDialog.ConnectionFavourite == null)
					{
						goto Block_31;
					}
				}
				MessageBox.Show("Your password was changed.");
				if (flag2)
				{
					ClockWorkCore.SetRegistryValue(Registry.CurrentUser, this.RegistryBreakdown, "lu", clockWorkLoginDialogPasswordChange.oldUsername, true);
				}
				if (flag3)
				{
					this.da.SelectCommand.CommandText = "UPDATE userinfo SET requirepasswordchange=0 WHERE username=@username";
					this.da.SelectCommand.Parameters.Clear();
					this.da.SelectCommand.Parameters.Add("@username", parameterValue2);
					this.da.Fill(new DataTable());
				}
				this.LoadWhoAmI(clockWorkLoginDialogPasswordChange.oldUsername);
				return null;
				Block_27:
				this.Abort();
				return ex;
				IL_6C8:
				if (flag2)
				{
					ClockWorkCore.SetRegistryValue(Registry.CurrentUser, this.RegistryBreakdown, "lu", clockWorkLoginDialog.UserOriginal, true);
				}
				this.overrideClockWorkUsername = clockWorkLoginDialog.User;
				this.overrideClockWorkPassword = clockWorkLoginDialog.Pass;
				this.LoadWhoAmI(clockWorkLoginDialog.User);
				if (flag4 || flag5)
				{
					this.LetUserLoginAsOtherUser();
				}
				return null;
				Block_31:
				this.Abort();
				result = ex;
			}
			return result;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000F900 File Offset: 0x0000E900
		private void LetUserLoginAsOtherUser()
		{
			if (this.whoAmI != null && this.WhoAmI.CoreGroup == eCoreGroupDTO.Admin)
			{
				InputPassword inputPassword = new InputPassword();
				inputPassword.Text = "Login as any user";
				inputPassword.SetCaption("Enter the username of the person you wish to login as:");
				inputPassword.SetPasswordChar('\0');
				DialogResult dialogResult = inputPassword.ShowDialog();
				if (dialogResult == DialogResult.OK)
				{
					string username = inputPassword.GetPassword().ToUpper();
					this.LoadWhoAmI(username);
				}
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000F980 File Offset: 0x0000E980
		private Exception CheckClockWorkPassword(int pid, string username, string pwd)
		{
			bool flag;
			return this.CheckClockWorkPassword(pid, username, pwd, out flag);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000F9A0 File Offset: 0x0000E9A0
		private Exception CheckClockWorkPassword(int pid, string username, string pwd, out bool forcePasswordChange)
		{
			bool flag = true;
			byte[] parameterValue = this.tripleDES.Encrypt(username);
			this.da.SelectCommand.CommandText = "SELECT pass";
			if (flag)
			{
				UnivCommand selectCommand = this.da.SelectCommand;
				selectCommand.CommandText += ",requirepasswordchange";
			}
			UnivCommand selectCommand2 = this.da.SelectCommand;
			selectCommand2.CommandText += " FROM userinfo WHERE (@pid<0 OR personid=@pid) AND username=@usernamee AND NOT personid IN (SELECT personid FROM people WHERE isactive=0)";
			this.da.SelectCommand.Parameters.Clear();
			this.da.SelectCommand.Parameters.Add("@pid", pid);
			this.da.SelectCommand.Parameters.Add("@usernamee", parameterValue);
			DataTable dataTable = new DataTable();
			string text;
			this.da.Fill(dataTable, out text);
			Exception result;
			if (text != null && text.Length > 0)
			{
				forcePasswordChange = false;
				result = new Exception("Faulty database connection: " + text);
			}
			else
			{
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					byte[] inputInBytes = (byte[])dataRow[0];
					string strB = this.tripleDES.Decrypt(inputInBytes);
					if (pwd.CompareTo(strB) == 0)
					{
						forcePasswordChange = (dataTable.Columns.Contains("requirepasswordchange") && dataRow["requirepasswordchange"] != DBNull.Value && (bool)dataRow["requirepasswordchange"]);
						return null;
					}
				}
				forcePasswordChange = false;
				result = new Exception("Username password not accepted.");
			}
			return result;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000FBB8 File Offset: 0x0000EBB8
		private Exception LoadWhoAmI(string username)
		{
			Exception result;
			try
			{
				this.whoAmI = null;
				byte[] parameterValue = this.tripleDES.Encrypt(username);
				this.da.SelectCommand.CommandText = "SELECT p.personID,p.firstName,p.lastName,p.student_no,p.isActive,p.dateadded,pg.groupid,pg.isprimarygroup FROM people p LEFT JOIN peoplegroups pg ON pg.personid=p.personid WHERE p.student_no=@student_no AND p.isactive=@true";
				this.da.SelectCommand.Parameters.Clear();
				this.da.SelectCommand.Parameters.Add("@student_no", parameterValue);
				this.da.SelectCommand.Parameters.Add("@true", true);
				DataTable dataTable = new DataTable();
				string text;
				this.da.Fill(dataTable, out text);
				if (text != null && text.Length > 0)
				{
					result = new Exception("Faulty connection3: " + text);
				}
				else
				{
					int num;
					if (dataTable.Rows.Count < 1)
					{
						this.da.SelectCommand.CommandText = "SELECT personid FROM userinfo WHERE username=@student_no";
						dataTable = new DataTable();
						this.da.Fill(dataTable, out text);
						if (text != null && text.Length > 0)
						{
							return new Exception("Faulty connection4: " + text);
						}
						if (dataTable.Rows.Count < 1)
						{
							return new Exception("Can't find username");
						}
						num = (int)dataTable.Rows[0][0];
						this.da.SelectCommand.CommandText = "SELECT p.personID,p.firstName,p.lastName,p.student_no,p.isActive,p.dateadded,pg.groupid,pg.isprimarygroup FROM people p LEFT JOIN peoplegroups pg ON pg.personid=p.personid WHERE p.personid=" + num.ToString();
						dataTable = new DataTable();
						this.da.Fill(dataTable, out text);
						if (text != null && text.Length > 0)
						{
							return new Exception("Faulty connection5: " + text);
						}
						if (dataTable.Rows.Count < 1)
						{
							return new Exception("Can't find personid");
						}
					}
					ArrayList arrayList = new ArrayList();
					num = (int)dataTable.Rows[0][0];
					int num2 = -1;
					foreach (object obj in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						int num3 = (int)dataRow[0];
						if (num3 == num)
						{
							if (dataRow["groupid"] != DBNull.Value)
							{
								int num4 = (int)dataRow["groupid"];
								if (Convert.ToBoolean(dataRow["isprimarygroup"]))
								{
									num2 = num4;
								}
								if (!arrayList.Contains(num4))
								{
									arrayList.Add(num4);
								}
							}
						}
					}
					byte[] inputInBytes = (byte[])dataTable.Rows[0][1];
					byte[] inputInBytes2 = (byte[])dataTable.Rows[0][2];
					byte[] inputInBytes3 = (byte[])dataTable.Rows[0][3];
					string firstName = this.tripleDES.Decrypt(inputInBytes);
					string lastName = this.tripleDES.Decrypt(inputInBytes2);
					string student_no = this.tripleDES.Decrypt(inputInBytes3);
					this.whoAmI = new PersonBaseDTO
					{
						PersonId = num,
						FirstName = firstName,
						MiddleName = "",
						LastName = lastName,
						Student_no = student_no,
						Tag = new PersonExt(),
						Groups = new List<GroupDTO>()
					};
					int num5 = num2;
					switch (num5)
					{
					case 1:
						this.whoAmI.CoreGroup = eCoreGroupDTO.Students;
						break;
					case 2:
						this.whoAmI.CoreGroup = eCoreGroupDTO.Staff;
						break;
					default:
						if (num5 == 10)
						{
							this.whoAmI.CoreGroup = eCoreGroupDTO.Admin;
						}
						break;
					}
					foreach (object obj2 in arrayList)
					{
						int num4 = (int)obj2;
						GroupDTO item = new GroupDTO
						{
							GroupId = num4,
							Description = ""
						};
						this.whoAmI.Groups.Add(item);
					}
					result = null;
				}
			}
			catch (Exception ex)
			{
				this.whoAmI = null;
				result = ex;
			}
			return result;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000100D8 File Offset: 0x0000F0D8
		private Exception GetEveryoneSettingInt(int SettingCode, out int SettingValue)
		{
			Exception result;
			if (this.da != null)
			{
				this.da.SelectCommand.CommandText = "SELECT settingvalue FROM settingsgroups WHERE groupid=-1 AND settingcode=" + SettingCode.ToString();
				DataTable dataTable = new DataTable();
				string text;
				this.da.Fill(dataTable, out text);
				if (text != null && text.Length > 0)
				{
					SettingValue = OldUserSettingClientManager.CurrentInstance.GetDefaultValueInt(SettingCode);
					result = new Exception(text);
				}
				else if (dataTable.Rows.Count > 0)
				{
					DataRow dataRow = dataTable.Rows[0];
					SettingValue = (int)dataRow[0];
					result = null;
				}
				else
				{
					SettingValue = OldUserSettingClientManager.CurrentInstance.GetDefaultValueInt(SettingCode);
					result = null;
				}
			}
			else
			{
				SettingValue = OldUserSettingClientManager.CurrentInstance.GetDefaultValueInt(SettingCode);
				result = new Exception("Database connection was unsuccessful.");
			}
			return result;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000101C4 File Offset: 0x0000F1C4
		private bool Use128BitTripleDESEncryption()
		{
			bool result;
			if (this.da != null)
			{
				this.da.SelectCommand.CommandText = "SELECT misccode FROM misc WHERE misccode=1";
				DataTable dataTable = new DataTable();
				string text;
				this.da.Fill(dataTable, out text);
				if (text != null && text.Length > 0)
				{
				}
				result = (dataTable.Rows.Count > 0);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0001023C File Offset: 0x0000F23C
		private Exception SetTripleDES()
		{
			int num;
			Exception everyoneSettingInt = this.GetEveryoneSettingInt(434, out num);
			Exception result;
			if (everyoneSettingInt != null)
			{
				result = everyoneSettingInt;
			}
			else
			{
				EncryptionType encryptionType = (EncryptionType)num;
				if (encryptionType == EncryptionType.TripleDES_192bit)
				{
					if (this.Use128BitTripleDESEncryption())
					{
						encryptionType = EncryptionType.TripleDES_128bit;
					}
				}
				if (this.databasePassword != null && this.databasePassword.Length > 0)
				{
					this.tripleDES = new TripleDESEncryptionClass(encryptionType, this.databasePassword);
					result = null;
				}
				else
				{
					result = new Exception("Missing database password.");
				}
			}
			return result;
		}

		// Token: 0x04000148 RID: 328
		private string[] registryBreakdown;

		// Token: 0x04000149 RID: 329
		private string startDirectory;

		// Token: 0x0400014A RID: 330
		private LogType logType;

		// Token: 0x0400014B RID: 331
		private string logConnection;

		// Token: 0x0400014C RID: 332
		private Log log;

		// Token: 0x0400014D RID: 333
		private UnivDataAdapter da;

		// Token: 0x0400014E RID: 334
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x0400014F RID: 335
		private bool manualOverride;

		// Token: 0x04000150 RID: 336
		private string manualConnectionInfoOverride;

		// Token: 0x04000151 RID: 337
		private string connectionString;

		// Token: 0x04000152 RID: 338
		private string databasePassword;

		// Token: 0x04000153 RID: 339
		private InfoStoreType storeType;

		// Token: 0x04000154 RID: 340
		private string location;

		// Token: 0x04000155 RID: 341
		private string license;

		// Token: 0x04000156 RID: 342
		private PersonBaseDTO whoAmI;

		// Token: 0x04000157 RID: 343
		private bool sslEncrypted = false;

		// Token: 0x04000158 RID: 344
		private string sshConnectionString = "";

		// Token: 0x04000159 RID: 345
		private string clockWorkFavourite = "";

		// Token: 0x0400015A RID: 346
		public bool isXenocode = false;

		// Token: 0x0400015B RID: 347
		private string overrideClockWorkUsername = null;

		// Token: 0x0400015C RID: 348
		private string overrideClockWorkPassword = null;

		// Token: 0x02000034 RID: 52
		internal class LoginReq
		{
			// Token: 0x17000114 RID: 276
			// (get) Token: 0x0600029A RID: 666 RVA: 0x000102D8 File Offset: 0x0000F2D8
			// (set) Token: 0x0600029B RID: 667 RVA: 0x000102EF File Offset: 0x0000F2EF
			public bool ReloadSettings { get; set; }

			// Token: 0x17000115 RID: 277
			// (get) Token: 0x0600029C RID: 668 RVA: 0x000102F8 File Offset: 0x0000F2F8
			// (set) Token: 0x0600029D RID: 669 RVA: 0x0001030F File Offset: 0x0000F30F
			public bool LoginUser { get; set; }

			// Token: 0x17000116 RID: 278
			// (get) Token: 0x0600029E RID: 670 RVA: 0x00010318 File Offset: 0x0000F318
			// (set) Token: 0x0600029F RID: 671 RVA: 0x0001032F File Offset: 0x0000F32F
			public bool ClearTempVariablesWhenConnected { get; set; }
		}
	}
}
