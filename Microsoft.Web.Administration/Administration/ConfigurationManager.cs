using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Microsoft.Web.Administration.Interop;
using Microsoft.Web.Management.Utility;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000026 RID: 38
	internal sealed class ConfigurationManager
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x0000614D File Offset: 0x0000514D
		public ConfigurationManager(ServerManager owner, string applicationHostConfigurationPath)
		{
			this._applicationHostConfigurationPath = applicationHostConfigurationPath;
			this._configurations = new SortedList<string, Configuration>(ConfigurationManager.ConfigurationPathComparer.Default);
			this._configurationsCommited = new List<Configuration>();
			this._owner = owner;
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000617E File Offset: 0x0000517E
		internal ServerManager Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00006186 File Offset: 0x00005186
		internal bool RedirectionEnabled
		{
			get
			{
				this.EnsureRedirectionInfoLoaded();
				return this._redirectionEnabled;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00006194 File Offset: 0x00005194
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x0000619C File Offset: 0x0000519C
		internal bool ServiceModel
		{
			get
			{
				return this._serviceModel;
			}
			set
			{
				this._serviceModel = value;
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000061A8 File Offset: 0x000051A8
		internal static string CombineConfigurationPath(string basePath, string locationPath)
		{
			if (string.IsNullOrEmpty(locationPath))
			{
				return basePath;
			}
			basePath = basePath.Trim(new char[]
			{
				'/'
			});
			locationPath = locationPath.Trim(new char[]
			{
				'/'
			});
			return basePath + "/" + locationPath;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000061F8 File Offset: 0x000051F8
		public void CommitChanges()
		{
			try
			{
				foreach (Configuration configuration in this._configurations.Values)
				{
					configuration.CommitChanges();
					this._configurationsCommited.Add(configuration);
				}
				this._configurations.Clear();
			}
			catch
			{
				foreach (Configuration value in this._configurationsCommited)
				{
					int num = this._configurations.IndexOfValue(value);
					if (num != -1)
					{
						this._configurations.RemoveAt(num);
					}
				}
				throw;
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000062CC File Offset: 0x000052CC
		private static bool TryCreateDelegatableAhAdminInstance<TClass, TInterface>(string serverName, out TInterface adminManager) where TClass : class, TInterface where TInterface : class, IAppHostAdminManager
		{
			bool result;
			using (ConfigurationManager.CoTaskMem<ConfigurationManager.COAUTHINFO> coTaskMem = new ConfigurationManager.CoTaskMem<ConfigurationManager.COAUTHINFO>(new ConfigurationManager.COAUTHINFO(ConfigurationManager.RPC_C_AUTHN.GSS_KERBEROS, ConfigurationManager.RPC_C_AUTHZ.DEFAULT, "HOST/" + serverName, ConfigurationManager.RPC_C_AUTHN_LEVEL.PKT_PRIVACY, ConfigurationManager.PRC_C_IMP.DELEGATE, IntPtr.Zero, ConfigurationManager.CoAuthInfoCapabilities.RPC_C_QOS_CAPABILITIES_MUTUAL_AUTH)))
			{
				using (ConfigurationManager.CoTaskMem<Guid> coTaskMem2 = new ConfigurationManager.CoTaskMem<Guid>(typeof(TInterface).GUID))
				{
					ConfigurationManager.MULTI_QI[] array = new ConfigurationManager.MULTI_QI[]
					{
						new ConfigurationManager.MULTI_QI(coTaskMem2.Ptr)
					};
					ConfigurationManager.COSERVERINFO coserverinfo = new ConfigurationManager.COSERVERINFO(serverName, coTaskMem.Ptr);
					if (ConfigurationManager.NativeMethods.CoCreateInstanceEx(typeof(TClass).GUID, null, ConfigurationManager.CLSCTX.CLSCTX_REMOTE_SERVER, ref coserverinfo, array.Length, array) == 0U)
					{
						adminManager = (TInterface)((object)array[0].pItf);
					}
					else
					{
						adminManager = default(TInterface);
					}
					result = (adminManager != null);
				}
			}
			return result;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000063D0 File Offset: 0x000053D0
		private IAppHostAdminManager CreateReadOnlyAdminManager(WebConfigurationMap webConfigMap, string configPathToEdit, bool isAdminConfig)
		{
			return this.CreateAdminManager<AppHostAdminManager, IAppHostAdminManager>(webConfigMap, isAdminConfig);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000063DC File Offset: 0x000053DC
		private TInterface CreateAdminManager<TClass, TInterface>(WebConfigurationMap webConfigMap, bool isAdminConfig) where TClass : class, TInterface, new() where TInterface : class, IAppHostAdminManager
		{
			TInterface tinterface = default(TInterface);
			if (this._owner.ServerName == null)
			{
				tinterface = (TInterface)((object)Activator.CreateInstance<TClass>());
				this.SetAdminManagerProperties(webConfigMap, isAdminConfig, tinterface, false);
			}
			else
			{
				if (!ConfigurationManager.TryCreateDelegatableAhAdminInstance<TClass, TInterface>(this._owner.ServerName, out tinterface))
				{
					Type typeFromCLSID = Type.GetTypeFromCLSID(typeof(TClass).GUID, this._owner.ServerName, true);
					tinterface = (TInterface)((object)Activator.CreateInstance(typeFromCLSID));
				}
				this.SetAdminManagerProperties(webConfigMap, isAdminConfig, tinterface, true);
			}
			return tinterface;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00006470 File Offset: 0x00005470
		private Configuration CreateConfiguration(WebConfigurationMap configMap, string configPathToEdit, bool isAdminConfig)
		{
			if (!this._owner.ReadOnly)
			{
				IAppHostWritableAdminManager adminManager = this.CreateWritableAdminManager(configMap, configPathToEdit, isAdminConfig);
				return new Configuration(this, adminManager, configPathToEdit);
			}
			IAppHostAdminManager appHostAdminManager;
			if (isAdminConfig)
			{
				appHostAdminManager = this.CreateWritableAdminManager(configMap, configPathToEdit, isAdminConfig);
			}
			else
			{
				appHostAdminManager = this.CreateReadOnlyAdminManager(configMap, configPathToEdit, isAdminConfig);
				appHostAdminManager.SetMetadata("expandEnvironmentStrings", false);
			}
			Configuration configuration = new Configuration(this, appHostAdminManager, configPathToEdit);
			configuration.CacheInvalidated += this.OnConfigCacheInvalidated;
			return configuration;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000064E8 File Offset: 0x000054E8
		private IAppHostWritableAdminManager CreateWritableAdminManager(WebConfigurationMap webConfigMap, string configPathToEdit, bool isAdminConfig)
		{
			IAppHostWritableAdminManager appHostWritableAdminManager = this.CreateAdminManager<AppHostWritableAdminManager, IAppHostWritableAdminManager>(webConfigMap, isAdminConfig);
			appHostWritableAdminManager.CommitPath = configPathToEdit;
			return appHostWritableAdminManager;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00006506 File Offset: 0x00005506
		private void EnsureRedirectionInfoLoaded()
		{
			if (this._administrationConfigurationPath == null)
			{
				this.LoadRedirectionInfo();
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00006518 File Offset: 0x00005518
		public Configuration GetAdministrationConfiguration(WebConfigurationMap configMap, string configurationPath)
		{
			string text = "MACHINE/WEBROOT";
			if (!string.IsNullOrEmpty(configurationPath))
			{
				text = ConfigurationManager.CombineConfigurationPath("MACHINE/WEBROOT/APPHOST", configurationPath);
			}
			return this.GetConfiguration(text, "ADMIN|" + text, true);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00006552 File Offset: 0x00005552
		public Configuration GetApplicationHostConfiguration()
		{
			return this.GetConfiguration("MACHINE/WEBROOT/APPHOST", "MACHINE/WEBROOT/APPHOST", false);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00006568 File Offset: 0x00005568
		public Configuration GetConfiguration(string rawConfigurationPath, string cacheKey, bool isAdminConfig)
		{
			Configuration configuration;
			lock (this._configurations)
			{
				if (!this._configurations.TryGetValue(cacheKey, out configuration))
				{
					configuration = this.CreateConfiguration(null, rawConfigurationPath, isAdminConfig);
					this._configurations.Add(cacheKey, configuration);
				}
			}
			return configuration;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000065C4 File Offset: 0x000055C4
		public Configuration GetWebConfiguration(WebConfigurationMap configMap, string configurationPath)
		{
			string text = "MACHINE/WEBROOT";
			if (!string.IsNullOrEmpty(configurationPath))
			{
				text = ConfigurationManager.CombineConfigurationPath("MACHINE/WEBROOT/APPHOST", configurationPath);
			}
			Configuration configuration;
			lock (this._configurations)
			{
				if (!this._configurations.TryGetValue(text, out configuration))
				{
					configuration = this.CreateConfiguration(configMap, text, false);
					this._configurations.Add(text, configuration);
				}
			}
			return configuration;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000663C File Offset: 0x0000563C
		private void LoadRedirectionInfo()
		{
			if (this._administrationConfigurationPath != null)
			{
				return;
			}
			this._redirectionEnabled = false;
			ConfigurationSection configurationSection = null;
			Configuration redirectionConfiguration = this._owner.GetRedirectionConfiguration();
			try
			{
				configurationSection = redirectionConfiguration.GetSection("configurationRedirection");
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode != -2147023728)
				{
					throw;
				}
			}
			if (configurationSection != null)
			{
				using (ImpersonationHelper.ImpersonateProcessIdentity())
				{
					if ((bool)configurationSection.GetAttributeValue("enabled"))
					{
						this._redirectionEnabled = true;
						this._redirectionUserName = (string)configurationSection.GetAttributeValue("userName");
						int num = this._redirectionUserName.IndexOf('\\');
						if (num != -1)
						{
							this._redirectionDomain = this._redirectionUserName.Substring(0, num);
							this._redirectionUserName = this._redirectionUserName.Substring(num + 1);
						}
						this._redirectionPassword = (string)configurationSection.GetAttributeValue("password");
						this._administrationConfigurationPath = (string)configurationSection.GetAttributeValue("path");
					}
					else
					{
						this._administrationConfigurationPath = "%windir%\\system32\\inetsrv\\config";
					}
					return;
				}
			}
			this._administrationConfigurationPath = "%windir%\\system32\\inetsrv\\config";
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00006770 File Offset: 0x00005770
		private void OnConfigCacheInvalidated(object sender, EventArgs e)
		{
			Configuration configuration = (Configuration)sender;
			if (this._configurations != null)
			{
				lock (this._configurations)
				{
					foreach (KeyValuePair<string, Configuration> keyValuePair in this._configurations)
					{
						if (keyValuePair.Value == configuration)
						{
							configuration.ClearCachedObjects();
							this._owner.ConfigurationInvalidated(configuration);
							break;
						}
					}
				}
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00006808 File Offset: 0x00005808
		public void Release()
		{
			this._owner = null;
			if (this._configurations != null)
			{
				lock (this._configurations)
				{
					foreach (Configuration configuration in this._configurations.Values)
					{
						configuration.Release();
					}
					this._configurations = null;
					foreach (Configuration configuration2 in this._configurationsCommited)
					{
						configuration2.Release();
					}
					this._configurationsCommited = null;
				}
			}
			this._redirectionEnabled = false;
			this._redirectionUserName = null;
			this._redirectionPassword = null;
			this._redirectionDomain = null;
			this._administrationConfigurationPath = null;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00006900 File Offset: 0x00005900
		private IntPtr GetRedirectionUserToken()
		{
			return ConfigurationManager.NativeMethods.GetUserToken(this._redirectionUserName, this._redirectionDomain, this._redirectionPassword);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000691C File Offset: 0x0000591C
		private void SetAdminManagerProperties(WebConfigurationMap webConfigMap, bool isAdminConfig, IAppHostAdminManager adminManager, bool isRemote)
		{
			if (adminManager == null)
			{
				throw new ArgumentNullException("adminManager");
			}
			if (isAdminConfig)
			{
				if (webConfigMap == null)
				{
					webConfigMap = this.GetAdministrationConfigMapIfNeeded();
				}
				if (this._serviceModel || webConfigMap != null)
				{
					adminManager.SetMetadata("pathMapper2", null);
					adminManager.SetMetadata("pathMapper2", new ConfigurationManager.AdministrationConfigurationPathMapper(webConfigMap, this));
				}
				else
				{
					adminManager.SetMetadata("pathMapper", "AdministrationConfig");
				}
			}
			else if (this._serviceModel || webConfigMap != null || this._applicationHostConfigurationPath != null)
			{
				adminManager.SetMetadata("pathMapper2", null);
				adminManager.SetMetadata("pathMapper2", new ConfigurationManager.WebConfigurationPathMapper(webConfigMap, this));
			}
			if (this._serviceModel)
			{
				adminManager.SetMetadata("hideExceptionPhysicalPath", true);
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000069CC File Offset: 0x000059CC
		private WebConfigurationMap GetAdministrationConfigMapIfNeeded()
		{
			string path;
			if (this._applicationHostConfigurationPath != null)
			{
				path = Path.GetDirectoryName(this._applicationHostConfigurationPath);
			}
			else if (this._serviceModel)
			{
				this.EnsureRedirectionInfoLoaded();
				path = this._administrationConfigurationPath;
			}
			else
			{
				if (!this.RedirectionEnabled)
				{
					return null;
				}
				if (string.IsNullOrEmpty(this._redirectionUserName))
				{
					return null;
				}
				path = this._administrationConfigurationPath;
			}
			return new WebConfigurationMap(null, Path.Combine(path, "administration.config"));
		}

		// Token: 0x04000068 RID: 104
		internal const string AdministrationConfigPath = "MACHINE/WEBROOT/APPHOST";

		// Token: 0x04000069 RID: 105
		internal const string MachineConfigPath = "MACHINE";

		// Token: 0x0400006A RID: 106
		internal const string RootWebConfigPath = "MACHINE/WEBROOT";

		// Token: 0x0400006B RID: 107
		internal const string ApplicationHostConfigPath = "MACHINE/WEBROOT/APPHOST";

		// Token: 0x0400006C RID: 108
		internal const string RedirectionConfigPath = "MACHINE/REDIRECTION";

		// Token: 0x0400006D RID: 109
		private const int ERROR_NOT_FOUND = -2147023728;

		// Token: 0x0400006E RID: 110
		private string _applicationHostConfigurationPath;

		// Token: 0x0400006F RID: 111
		private ServerManager _owner;

		// Token: 0x04000070 RID: 112
		private SortedList<string, Configuration> _configurations;

		// Token: 0x04000071 RID: 113
		private List<Configuration> _configurationsCommited;

		// Token: 0x04000072 RID: 114
		private string _administrationConfigurationPath;

		// Token: 0x04000073 RID: 115
		private string _redirectionUserName;

		// Token: 0x04000074 RID: 116
		private string _redirectionDomain;

		// Token: 0x04000075 RID: 117
		private string _redirectionPassword;

		// Token: 0x04000076 RID: 118
		private bool _redirectionEnabled;

		// Token: 0x04000077 RID: 119
		private bool _serviceModel;

		// Token: 0x02000027 RID: 39
		private class ConfigurationPathComparer : IComparer<string>
		{
			// Token: 0x060001B9 RID: 441 RVA: 0x00006A3B File Offset: 0x00005A3B
			private ConfigurationPathComparer()
			{
			}

			// Token: 0x060001BA RID: 442 RVA: 0x00006A44 File Offset: 0x00005A44
			public int Compare(string x, string y)
			{
				int num = y.Length - x.Length;
				if (num != 0)
				{
					return num;
				}
				return string.Compare(y, x, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x04000078 RID: 120
			public static readonly ConfigurationManager.ConfigurationPathComparer Default = new ConfigurationManager.ConfigurationPathComparer();
		}

		// Token: 0x02000029 RID: 41
		private class WebConfigurationPathMapper : IAppHostPathMapper2
		{
			// Token: 0x060001BD RID: 445 RVA: 0x00006A78 File Offset: 0x00005A78
			public WebConfigurationPathMapper(WebConfigurationMap configMap, ConfigurationManager configManager)
			{
				this._configMap = configMap;
				this._configManager = configManager;
			}

			// Token: 0x060001BE RID: 446 RVA: 0x00006A90 File Offset: 0x00005A90
			public IntPtr MapPath(string bstrVirtualPath, string bstrMappedPhysicalPath, out string newMappedPhysicalPath)
			{
				IntPtr result = IntPtr.Zero;
				if (bstrVirtualPath.Length == 0)
				{
					newMappedPhysicalPath = string.Empty;
					return result;
				}
				if (bstrVirtualPath.Equals("MACHINE", StringComparison.OrdinalIgnoreCase))
				{
					if (this._configMap == null || this._configMap.MachineConfigurationPath == null)
					{
						newMappedPhysicalPath = bstrMappedPhysicalPath;
						return result;
					}
					newMappedPhysicalPath = this._configMap.MachineConfigurationPath;
					return result;
				}
				else if (bstrVirtualPath.Equals("MACHINE/WEBROOT", StringComparison.OrdinalIgnoreCase))
				{
					if (this._configMap == null || this._configMap.RootWebConfigurationPath == null)
					{
						newMappedPhysicalPath = bstrMappedPhysicalPath;
						return result;
					}
					newMappedPhysicalPath = this._configMap.RootWebConfigurationPath;
					return result;
				}
				else
				{
					if (bstrVirtualPath.Equals("MACHINE/REDIRECTION", StringComparison.OrdinalIgnoreCase))
					{
						newMappedPhysicalPath = bstrMappedPhysicalPath;
						if (this._configManager._serviceModel)
						{
							result = ConfigurationManager.NativeMethods.GetProcessToken();
						}
						return result;
					}
					if (!bstrVirtualPath.Equals("MACHINE/WEBROOT/APPHOST", StringComparison.OrdinalIgnoreCase))
					{
						newMappedPhysicalPath = bstrMappedPhysicalPath;
						return result;
					}
					if (this._configManager._serviceModel && !this._configManager.RedirectionEnabled)
					{
						result = ConfigurationManager.NativeMethods.GetProcessToken();
					}
					if (string.IsNullOrEmpty(this._configManager._applicationHostConfigurationPath))
					{
						newMappedPhysicalPath = bstrMappedPhysicalPath;
						return result;
					}
					newMappedPhysicalPath = this._configManager._applicationHostConfigurationPath;
					return result;
				}
			}

			// Token: 0x04000079 RID: 121
			private WebConfigurationMap _configMap;

			// Token: 0x0400007A RID: 122
			private ConfigurationManager _configManager;
		}

		// Token: 0x0200002A RID: 42
		private class AdministrationConfigurationPathMapper : IAppHostPathMapper2
		{
			// Token: 0x060001BF RID: 447 RVA: 0x00006BA0 File Offset: 0x00005BA0
			public AdministrationConfigurationPathMapper(WebConfigurationMap configMap, ConfigurationManager configManager)
			{
				this._configMap = configMap;
				this._configManager = configManager;
			}

			// Token: 0x060001C0 RID: 448 RVA: 0x00006BB8 File Offset: 0x00005BB8
			public IntPtr MapPath(string bstrVirtualPath, string bstrMappedPhysicalPath, out string bstrNewPhysicalPath)
			{
				IntPtr result = IntPtr.Zero;
				if (bstrVirtualPath.Length == 0)
				{
					bstrNewPhysicalPath = string.Empty;
					return result;
				}
				if (bstrVirtualPath.Equals("MACHINE", StringComparison.OrdinalIgnoreCase))
				{
					if (this._configMap == null || this._configMap.MachineConfigurationPath == null)
					{
						bstrNewPhysicalPath = bstrMappedPhysicalPath;
						return result;
					}
					bstrNewPhysicalPath = this._configMap.MachineConfigurationPath;
					return result;
				}
				else if (bstrVirtualPath.Equals("MACHINE/WEBROOT", StringComparison.OrdinalIgnoreCase))
				{
					if (this._configManager.RedirectionEnabled && !string.IsNullOrEmpty(this._configManager._redirectionUserName))
					{
						result = this._configManager.GetRedirectionUserToken();
					}
					else if (this._configManager._serviceModel)
					{
						result = ConfigurationManager.NativeMethods.GetProcessToken();
					}
					if (this._configMap == null || this._configMap.RootWebConfigurationPath == null)
					{
						bstrNewPhysicalPath = bstrMappedPhysicalPath;
						return result;
					}
					bstrNewPhysicalPath = this._configMap.RootWebConfigurationPath;
					return result;
				}
				else
				{
					if (bstrVirtualPath.Equals("MACHINE/REDIRECTION", StringComparison.OrdinalIgnoreCase))
					{
						if (this._configManager._serviceModel)
						{
							result = ConfigurationManager.NativeMethods.GetProcessToken();
						}
						bstrNewPhysicalPath = bstrMappedPhysicalPath;
						return result;
					}
					if (!bstrVirtualPath.Equals("MACHINE/WEBROOT/APPHOST", StringComparison.OrdinalIgnoreCase))
					{
						bstrNewPhysicalPath = bstrMappedPhysicalPath.Substring(0, bstrMappedPhysicalPath.Length - 10) + "administration.config";
						return result;
					}
					if (this._configManager._serviceModel && !this._configManager.RedirectionEnabled)
					{
						result = ConfigurationManager.NativeMethods.GetProcessToken();
					}
					if (string.IsNullOrEmpty(this._configManager._applicationHostConfigurationPath))
					{
						bstrNewPhysicalPath = bstrMappedPhysicalPath;
						return result;
					}
					bstrNewPhysicalPath = this._configManager._applicationHostConfigurationPath;
					return result;
				}
			}

			// Token: 0x0400007B RID: 123
			private const int webConfigLength = 10;

			// Token: 0x0400007C RID: 124
			private WebConfigurationMap _configMap;

			// Token: 0x0400007D RID: 125
			private ConfigurationManager _configManager;
		}

		// Token: 0x0200002B RID: 43
		private class CoTaskMem<T> : IDisposable where T : struct
		{
			// Token: 0x060001C1 RID: 449 RVA: 0x00006D21 File Offset: 0x00005D21
			public CoTaskMem(T structure)
			{
				this._ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf(structure));
				Marshal.StructureToPtr(structure, this._ptr, false);
			}

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x060001C2 RID: 450 RVA: 0x00006D5C File Offset: 0x00005D5C
			public IntPtr Ptr
			{
				get
				{
					return this._ptr;
				}
			}

			// Token: 0x060001C3 RID: 451 RVA: 0x00006D64 File Offset: 0x00005D64
			public void Dispose()
			{
				if (this._ptr != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(this._ptr);
					this._ptr = IntPtr.Zero;
				}
				GC.SuppressFinalize(this);
			}

			// Token: 0x0400007E RID: 126
			private IntPtr _ptr = IntPtr.Zero;
		}

		// Token: 0x0200002C RID: 44
		[Flags]
		private enum CLSCTX
		{
			// Token: 0x04000080 RID: 128
			CLSCTX_REMOTE_SERVER = 16
		}

		// Token: 0x0200002D RID: 45
		internal enum RPC_C_AUTHN
		{
			// Token: 0x04000082 RID: 130
			GSS_KERBEROS = 16
		}

		// Token: 0x0200002E RID: 46
		internal enum RPC_C_AUTHZ
		{
			// Token: 0x04000084 RID: 132
			DEFAULT = -1
		}

		// Token: 0x0200002F RID: 47
		internal enum RPC_C_AUTHN_LEVEL
		{
			// Token: 0x04000086 RID: 134
			PKT_PRIVACY = 6
		}

		// Token: 0x02000030 RID: 48
		internal enum PRC_C_IMP
		{
			// Token: 0x04000088 RID: 136
			DELEGATE = 4
		}

		// Token: 0x02000031 RID: 49
		[Flags]
		internal enum CoAuthInfoCapabilities
		{
			// Token: 0x0400008A RID: 138
			RPC_C_QOS_CAPABILITIES_MUTUAL_AUTH = 1
		}

		// Token: 0x02000032 RID: 50
		internal struct MULTI_QI
		{
			// Token: 0x060001C4 RID: 452 RVA: 0x00006D94 File Offset: 0x00005D94
			public MULTI_QI(IntPtr pIID)
			{
				this.pIID = pIID;
				this.pItf = null;
				this.hr = 0;
			}

			// Token: 0x0400008B RID: 139
			public IntPtr pIID;

			// Token: 0x0400008C RID: 140
			[MarshalAs(UnmanagedType.Interface)]
			public object pItf;

			// Token: 0x0400008D RID: 141
			public int hr;
		}

		// Token: 0x02000033 RID: 51
		internal struct COSERVERINFO
		{
			// Token: 0x060001C5 RID: 453 RVA: 0x00006DAB File Offset: 0x00005DAB
			public COSERVERINFO(string pwszName, IntPtr pAuthInfo)
			{
				this.dwReserved1 = 0;
				this.dwReserved2 = 0;
				this.pwszName = pwszName;
				this.pAuthInfo = pAuthInfo;
			}

			// Token: 0x0400008E RID: 142
			public int dwReserved1;

			// Token: 0x0400008F RID: 143
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pwszName;

			// Token: 0x04000090 RID: 144
			public IntPtr pAuthInfo;

			// Token: 0x04000091 RID: 145
			public int dwReserved2;
		}

		// Token: 0x02000034 RID: 52
		internal struct COAUTHINFO
		{
			// Token: 0x060001C6 RID: 454 RVA: 0x00006DC9 File Offset: 0x00005DC9
			public COAUTHINFO(ConfigurationManager.RPC_C_AUTHN dwAuthnSvc, ConfigurationManager.RPC_C_AUTHZ dwAuthzSvc, string pwszServerPrincName, ConfigurationManager.RPC_C_AUTHN_LEVEL dwAuthnLevel, ConfigurationManager.PRC_C_IMP dwImpersonationLevel, IntPtr pAuthIdentityData, ConfigurationManager.CoAuthInfoCapabilities dwCapabilities)
			{
				this.dwAuthnSvc = dwAuthnSvc;
				this.dwAuthzSvc = dwAuthzSvc;
				this.pwszServerPrincName = pwszServerPrincName;
				this.dwAuthnLevel = dwAuthnLevel;
				this.dwImpersonationLevel = dwImpersonationLevel;
				this.pAuthIdentityData = pAuthIdentityData;
				this.dwCapabilities = dwCapabilities;
			}

			// Token: 0x04000092 RID: 146
			public ConfigurationManager.RPC_C_AUTHN dwAuthnSvc;

			// Token: 0x04000093 RID: 147
			public ConfigurationManager.RPC_C_AUTHZ dwAuthzSvc;

			// Token: 0x04000094 RID: 148
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pwszServerPrincName;

			// Token: 0x04000095 RID: 149
			public ConfigurationManager.RPC_C_AUTHN_LEVEL dwAuthnLevel;

			// Token: 0x04000096 RID: 150
			public ConfigurationManager.PRC_C_IMP dwImpersonationLevel;

			// Token: 0x04000097 RID: 151
			public IntPtr pAuthIdentityData;

			// Token: 0x04000098 RID: 152
			public ConfigurationManager.CoAuthInfoCapabilities dwCapabilities;
		}

		// Token: 0x02000035 RID: 53
		private static class NativeMethods
		{
			// Token: 0x060001C7 RID: 455
			[DllImport("ole32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			public static extern uint CoCreateInstanceEx([MarshalAs(UnmanagedType.LPStruct)] [In] Guid rclsid, [MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, ConfigurationManager.CLSCTX dwClsCtx, ref ConfigurationManager.COSERVERINFO pServerInfo, int cmq, [In] [Out] ConfigurationManager.MULTI_QI[] pResults);

			// Token: 0x060001C8 RID: 456
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			private static extern bool LogonUser(string userName, string domain, string password, int LogonType, int LogonProvider, ref IntPtr hToken);

			// Token: 0x060001C9 RID: 457
			[DllImport("kernel32.dll")]
			private static extern IntPtr GetCurrentProcess();

			// Token: 0x060001CA RID: 458
			[DllImport("advapi32.dll", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			private static extern bool OpenProcessToken(IntPtr ProcessHandle, TokenAccessLevels DesiredAccess, out IntPtr TokenHandle);

			// Token: 0x060001CB RID: 459 RVA: 0x00006E00 File Offset: 0x00005E00
			internal static IntPtr GetProcessToken()
			{
				IntPtr result;
				try
				{
					using (ImpersonationHelper.ImpersonateProcessIdentity())
					{
						IntPtr currentProcess = ConfigurationManager.NativeMethods.GetCurrentProcess();
						IntPtr intPtr;
						if (!ConfigurationManager.NativeMethods.OpenProcessToken(currentProcess, TokenAccessLevels.MaximumAllowed, out intPtr))
						{
							Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
						}
						result = intPtr;
					}
				}
				catch
				{
					throw;
				}
				return result;
			}

			// Token: 0x060001CC RID: 460 RVA: 0x00006E60 File Offset: 0x00005E60
			internal static IntPtr GetUserToken(string userName, string domain, string password)
			{
				IntPtr zero = IntPtr.Zero;
				if (!ConfigurationManager.NativeMethods.LogonUser(userName, domain, password, 8, 0, ref zero))
				{
					Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
				}
				return zero;
			}

			// Token: 0x04000099 RID: 153
			private const int LOGON32_PROVIDER_DEFAULT = 0;

			// Token: 0x0400009A RID: 154
			private const int LOGON32_LOGON_NETWORK_CLEARTEXT = 8;
		}
	}
}
