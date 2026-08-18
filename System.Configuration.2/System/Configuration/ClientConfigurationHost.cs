using System;
using System.Configuration.Internal;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Principal;

namespace System.Configuration
{
	// Token: 0x02000016 RID: 22
	internal sealed class ClientConfigurationHost : DelegatingConfigHost, IInternalConfigClientHost
	{
		// Token: 0x060000CF RID: 207 RVA: 0x0000817C File Offset: 0x0000637C
		internal ClientConfigurationHost()
		{
			base.Host = new InternalConfigHost();
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000818F File Offset: 0x0000638F
		internal ClientConfigPaths ConfigPaths
		{
			get
			{
				if (this._configPaths == null)
				{
					this._configPaths = ClientConfigPaths.GetPaths(this._exePath, this._initComplete);
				}
				return this._configPaths;
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000081B6 File Offset: 0x000063B6
		internal void RefreshConfigPaths()
		{
			if (this._configPaths != null && !this._configPaths.HasEntryAssembly && this._exePath == null)
			{
				ClientConfigPaths.RefreshCurrent();
				this._configPaths = null;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000081E4 File Offset: 0x000063E4
		internal static string MachineConfigFilePath
		{
			[FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.PathDiscovery)]
			get
			{
				if (ClientConfigurationHost.s_machineConfigFilePath == null)
				{
					string runtimeDirectory = RuntimeEnvironment.GetRuntimeDirectory();
					ClientConfigurationHost.s_machineConfigFilePath = Path.Combine(Path.Combine(runtimeDirectory, "Config"), "machine.config");
				}
				return ClientConfigurationHost.s_machineConfigFilePath;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00008223 File Offset: 0x00006423
		internal bool HasRoamingConfig
		{
			get
			{
				if (this._fileMap != null)
				{
					return !string.IsNullOrEmpty(this._fileMap.RoamingUserConfigFilename);
				}
				return this.ConfigPaths.HasRoamingConfig;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x0000824C File Offset: 0x0000644C
		internal bool HasLocalConfig
		{
			get
			{
				if (this._fileMap != null)
				{
					return !string.IsNullOrEmpty(this._fileMap.LocalUserConfigFilename);
				}
				return this.ConfigPaths.HasLocalConfig;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00008275 File Offset: 0x00006475
		internal bool IsAppConfigHttp
		{
			get
			{
				return !this.IsFile(this.GetStreamName("MACHINE/EXE"));
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000828B File Offset: 0x0000648B
		bool IInternalConfigClientHost.IsExeConfig(string configPath)
		{
			return StringUtil.EqualsIgnoreCase(configPath, "MACHINE/EXE");
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00008298 File Offset: 0x00006498
		bool IInternalConfigClientHost.IsRoamingUserConfig(string configPath)
		{
			return StringUtil.EqualsIgnoreCase(configPath, "MACHINE/EXE/ROAMING_USER");
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000082A5 File Offset: 0x000064A5
		bool IInternalConfigClientHost.IsLocalUserConfig(string configPath)
		{
			return StringUtil.EqualsIgnoreCase(configPath, "MACHINE/EXE/ROAMING_USER/LOCAL_USER");
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000082B2 File Offset: 0x000064B2
		private bool IsUserConfig(string configPath)
		{
			return StringUtil.EqualsIgnoreCase(configPath, "MACHINE/EXE/ROAMING_USER") || StringUtil.EqualsIgnoreCase(configPath, "MACHINE/EXE/ROAMING_USER/LOCAL_USER");
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000082CE File Offset: 0x000064CE
		string IInternalConfigClientHost.GetExeConfigPath()
		{
			return "MACHINE/EXE";
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000082D5 File Offset: 0x000064D5
		string IInternalConfigClientHost.GetRoamingUserConfigPath()
		{
			return "MACHINE/EXE/ROAMING_USER";
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000082DC File Offset: 0x000064DC
		string IInternalConfigClientHost.GetLocalUserConfigPath()
		{
			return "MACHINE/EXE/ROAMING_USER/LOCAL_USER";
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000082E4 File Offset: 0x000064E4
		public override void Init(IInternalConfigRoot configRoot, params object[] hostInitParams)
		{
			try
			{
				ConfigurationFileMap configurationFileMap = (ConfigurationFileMap)hostInitParams[0];
				this._exePath = (string)hostInitParams[1];
				base.Host.Init(configRoot, hostInitParams);
				this._initComplete = configRoot.IsDesignTime;
				if (configurationFileMap != null && !string.IsNullOrEmpty(this._exePath))
				{
					throw ExceptionUtil.UnexpectedError("ClientConfigurationHost::Init");
				}
				if (string.IsNullOrEmpty(this._exePath))
				{
					this._exePath = null;
				}
				if (configurationFileMap != null)
				{
					this._fileMap = new ExeConfigurationFileMap();
					if (!string.IsNullOrEmpty(configurationFileMap.MachineConfigFilename))
					{
						this._fileMap.MachineConfigFilename = Path.GetFullPath(configurationFileMap.MachineConfigFilename);
					}
					ExeConfigurationFileMap exeConfigurationFileMap = configurationFileMap as ExeConfigurationFileMap;
					if (exeConfigurationFileMap != null)
					{
						if (!string.IsNullOrEmpty(exeConfigurationFileMap.ExeConfigFilename))
						{
							this._fileMap.ExeConfigFilename = Path.GetFullPath(exeConfigurationFileMap.ExeConfigFilename);
						}
						if (!string.IsNullOrEmpty(exeConfigurationFileMap.RoamingUserConfigFilename))
						{
							this._fileMap.RoamingUserConfigFilename = Path.GetFullPath(exeConfigurationFileMap.RoamingUserConfigFilename);
						}
						if (!string.IsNullOrEmpty(exeConfigurationFileMap.LocalUserConfigFilename))
						{
							this._fileMap.LocalUserConfigFilename = Path.GetFullPath(exeConfigurationFileMap.LocalUserConfigFilename);
						}
					}
				}
			}
			catch (SecurityException)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_client_config_init_security"));
			}
			catch
			{
				throw ExceptionUtil.UnexpectedError("ClientConfigurationHost::Init");
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000844C File Offset: 0x0000664C
		public override void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot configRoot, params object[] hostInitConfigurationParams)
		{
			locationSubPath = null;
			configPath = (string)hostInitConfigurationParams[2];
			locationConfigPath = null;
			this.Init(configRoot, hostInitConfigurationParams);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00008469 File Offset: 0x00006669
		public override bool IsInitDelayed(IInternalConfigRecord configRecord)
		{
			return !this._initComplete && this.IsUserConfig(configRecord.ConfigPath);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00008484 File Offset: 0x00006684
		public override void RequireCompleteInit(IInternalConfigRecord record)
		{
			lock (this)
			{
				if (!this._initComplete)
				{
					this._initComplete = true;
					ClientConfigPaths.RefreshCurrent();
					this._configPaths = null;
					ClientConfigPaths configPaths = this.ConfigPaths;
				}
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000084DC File Offset: 0x000066DC
		public override bool IsConfigRecordRequired(string configPath)
		{
			string name = ConfigPathUtility.GetName(configPath);
			if (name == "MACHINE" || name == "EXE")
			{
				return true;
			}
			if (!(name == "ROAMING_USER"))
			{
				return name == "LOCAL_USER" && this.HasLocalConfig;
			}
			return this.HasRoamingConfig || this.HasLocalConfig;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00008540 File Offset: 0x00006740
		public override string GetStreamName(string configPath)
		{
			string name = ConfigPathUtility.GetName(configPath);
			if (this._fileMap != null)
			{
				if (!(name == "MACHINE"))
				{
					if (name == "EXE")
					{
						return this._fileMap.ExeConfigFilename;
					}
					if (name == "ROAMING_USER")
					{
						return this._fileMap.RoamingUserConfigFilename;
					}
					if (name == "LOCAL_USER")
					{
						return this._fileMap.LocalUserConfigFilename;
					}
				}
				return this._fileMap.MachineConfigFilename;
			}
			if (!(name == "MACHINE"))
			{
				if (name == "EXE")
				{
					return this.ConfigPaths.ApplicationConfigUri;
				}
				if (name == "ROAMING_USER")
				{
					return this.ConfigPaths.RoamingConfigFilename;
				}
				if (name == "LOCAL_USER")
				{
					return this.ConfigPaths.LocalConfigFilename;
				}
			}
			return ClientConfigurationHost.MachineConfigFilePath;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00008620 File Offset: 0x00006820
		public override string GetStreamNameForConfigSource(string streamName, string configSource)
		{
			if (this.IsFile(streamName))
			{
				return base.Host.GetStreamNameForConfigSource(streamName, configSource);
			}
			int num = streamName.LastIndexOf('/');
			if (num < 0)
			{
				return null;
			}
			string str = streamName.Substring(0, num + 1);
			return str + configSource.Replace('\\', '/');
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00008670 File Offset: 0x00006870
		public override object GetStreamVersion(string streamName)
		{
			if (this.IsFile(streamName))
			{
				return base.Host.GetStreamVersion(streamName);
			}
			return ClientConfigurationHost.s_version;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00008690 File Offset: 0x00006890
		public override Stream OpenStreamForRead(string streamName)
		{
			if (this.IsFile(streamName))
			{
				return base.Host.OpenStreamForRead(streamName);
			}
			if (streamName == null)
			{
				return null;
			}
			WebClient webClient = new WebClient();
			try
			{
				webClient.Credentials = CredentialCache.DefaultCredentials;
			}
			catch
			{
			}
			byte[] array = null;
			try
			{
				array = webClient.DownloadData(streamName);
			}
			catch
			{
			}
			if (array == null)
			{
				return null;
			}
			return new MemoryStream(array);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00008708 File Offset: 0x00006908
		public override Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext)
		{
			if (!this.IsFile(streamName))
			{
				throw ExceptionUtil.UnexpectedError("ClientConfigurationHost::OpenStreamForWrite");
			}
			return base.Host.OpenStreamForWrite(streamName, templateStreamName, ref writeContext);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000872C File Offset: 0x0000692C
		public override void DeleteStream(string streamName)
		{
			if (!this.IsFile(streamName))
			{
				throw ExceptionUtil.UnexpectedError("ClientConfigurationHost::Delete");
			}
			base.Host.DeleteStream(streamName);
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x0000874E File Offset: 0x0000694E
		public override bool SupportsRefresh
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00008751 File Offset: 0x00006951
		public override bool SupportsPath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00008751 File Offset: 0x00006951
		public override bool SupportsLocation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00008754 File Offset: 0x00006954
		public override bool IsDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition)
		{
			string text;
			if (allowExeDefinition <= ConfigurationAllowExeDefinition.MachineToApplication)
			{
				if (allowExeDefinition == ConfigurationAllowExeDefinition.MachineOnly)
				{
					text = "MACHINE";
					goto IL_46;
				}
				if (allowExeDefinition == ConfigurationAllowExeDefinition.MachineToApplication)
				{
					text = "MACHINE/EXE";
					goto IL_46;
				}
			}
			else
			{
				if (allowExeDefinition == ConfigurationAllowExeDefinition.MachineToRoamingUser)
				{
					text = "MACHINE/EXE/ROAMING_USER";
					goto IL_46;
				}
				if (allowExeDefinition == ConfigurationAllowExeDefinition.MachineToLocalUser)
				{
					return true;
				}
			}
			throw ExceptionUtil.UnexpectedError("ClientConfigurationHost::IsDefinitionAllowed");
			IL_46:
			return configPath.Length <= text.Length;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000087B8 File Offset: 0x000069B8
		public override void VerifyDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition, IConfigErrorInfo errorInfo)
		{
			if (this.IsDefinitionAllowed(configPath, allowDefinition, allowExeDefinition))
			{
				return;
			}
			if (allowExeDefinition == ConfigurationAllowExeDefinition.MachineOnly)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_allow_exedefinition_error_machine"), errorInfo);
			}
			if (allowExeDefinition == ConfigurationAllowExeDefinition.MachineToApplication)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_allow_exedefinition_error_application"), errorInfo);
			}
			if (allowExeDefinition != ConfigurationAllowExeDefinition.MachineToRoamingUser)
			{
				throw ExceptionUtil.UnexpectedError("ClientConfigurationHost::VerifyDefinitionAllowed");
			}
			throw new ConfigurationErrorsException(SR.GetString("Config_allow_exedefinition_error_roaminguser"), errorInfo);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00008823 File Offset: 0x00006A23
		public override bool PrefetchAll(string configPath, string streamName)
		{
			return !this.IsFile(streamName);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000882F File Offset: 0x00006A2F
		public override bool PrefetchSection(string sectionGroupName, string sectionName)
		{
			return sectionGroupName == "system.net";
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000883C File Offset: 0x00006A3C
		public override bool IsTrustedConfigPath(string configPath)
		{
			return configPath == "MACHINE";
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000884C File Offset: 0x00006A4C
		[SecurityPermission(SecurityAction.Assert, ControlEvidence = true)]
		public override void GetRestrictedPermissions(IInternalConfigRecord configRecord, out PermissionSet permissionSet, out bool isHostReady)
		{
			bool flag = this.IsFile(configRecord.StreamName);
			string text;
			if (flag)
			{
				text = UrlPath.ConvertFileNameToUrl(configRecord.StreamName);
			}
			else
			{
				text = configRecord.StreamName;
			}
			Evidence evidence = new Evidence();
			evidence.AddHostEvidence<Url>(new Url(text));
			evidence.AddHostEvidence<Zone>(Zone.CreateFromUrl(text));
			if (!flag)
			{
				evidence.AddHostEvidence<Site>(Site.CreateFromUrl(text));
			}
			permissionSet = SecurityManager.GetStandardSandbox(evidence);
			isHostReady = true;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000088B6 File Offset: 0x00006AB6
		[SecurityPermission(SecurityAction.Assert, Flags = (SecurityPermissionFlag.UnmanagedCode | SecurityPermissionFlag.ControlPrincipal))]
		public override IDisposable Impersonate()
		{
			return WindowsIdentity.Impersonate(IntPtr.Zero);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000088C2 File Offset: 0x00006AC2
		public override object CreateDeprecatedConfigContext(string configPath)
		{
			return null;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000088C5 File Offset: 0x00006AC5
		public override object CreateConfigurationContext(string configPath, string locationSubPath)
		{
			return new ExeContext(this.GetUserLevel(configPath), this.ConfigPaths.ApplicationUri);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000088E0 File Offset: 0x00006AE0
		private ConfigurationUserLevel GetUserLevel(string configPath)
		{
			string name = ConfigPathUtility.GetName(configPath);
			ConfigurationUserLevel result;
			if (!(name == "MACHINE"))
			{
				if (!(name == "EXE"))
				{
					if (!(name == "LOCAL_USER"))
					{
						if (!(name == "ROAMING_USER"))
						{
							result = ConfigurationUserLevel.None;
						}
						else
						{
							result = ConfigurationUserLevel.PerUserRoaming;
						}
					}
					else
					{
						result = ConfigurationUserLevel.PerUserRoamingAndLocal;
					}
				}
				else
				{
					result = ConfigurationUserLevel.None;
				}
			}
			else
			{
				result = ConfigurationUserLevel.None;
			}
			return result;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00008940 File Offset: 0x00006B40
		internal static Configuration OpenExeConfiguration(ConfigurationFileMap fileMap, bool isMachine, ConfigurationUserLevel userLevel, string exePath)
		{
			if (userLevel != ConfigurationUserLevel.None && userLevel != ConfigurationUserLevel.PerUserRoaming && userLevel != ConfigurationUserLevel.PerUserRoamingAndLocal)
			{
				throw ExceptionUtil.ParameterInvalid("userLevel");
			}
			if (fileMap != null)
			{
				if (string.IsNullOrEmpty(fileMap.MachineConfigFilename))
				{
					throw ExceptionUtil.ParameterNullOrEmpty("fileMap.MachineConfigFilename");
				}
				ExeConfigurationFileMap exeConfigurationFileMap = fileMap as ExeConfigurationFileMap;
				if (exeConfigurationFileMap != null)
				{
					if (userLevel != ConfigurationUserLevel.None)
					{
						if (userLevel != ConfigurationUserLevel.PerUserRoaming)
						{
							if (userLevel != ConfigurationUserLevel.PerUserRoamingAndLocal)
							{
								goto IL_94;
							}
							if (string.IsNullOrEmpty(exeConfigurationFileMap.LocalUserConfigFilename))
							{
								throw ExceptionUtil.ParameterNullOrEmpty("fileMap.LocalUserConfigFilename");
							}
						}
						if (string.IsNullOrEmpty(exeConfigurationFileMap.RoamingUserConfigFilename))
						{
							throw ExceptionUtil.ParameterNullOrEmpty("fileMap.RoamingUserConfigFilename");
						}
					}
					if (string.IsNullOrEmpty(exeConfigurationFileMap.ExeConfigFilename))
					{
						throw ExceptionUtil.ParameterNullOrEmpty("fileMap.ExeConfigFilename");
					}
				}
			}
			IL_94:
			string text = null;
			if (isMachine)
			{
				text = "MACHINE";
			}
			else if (userLevel != ConfigurationUserLevel.None)
			{
				if (userLevel != ConfigurationUserLevel.PerUserRoaming)
				{
					if (userLevel == ConfigurationUserLevel.PerUserRoamingAndLocal)
					{
						text = "MACHINE/EXE/ROAMING_USER/LOCAL_USER";
					}
				}
				else
				{
					text = "MACHINE/EXE/ROAMING_USER";
				}
			}
			else
			{
				text = "MACHINE/EXE";
			}
			return new Configuration(null, typeof(ClientConfigurationHost), new object[]
			{
				fileMap,
				exePath,
				text
			});
		}

		// Token: 0x0400014F RID: 335
		internal const string MachineConfigName = "MACHINE";

		// Token: 0x04000150 RID: 336
		internal const string ExeConfigName = "EXE";

		// Token: 0x04000151 RID: 337
		internal const string RoamingUserConfigName = "ROAMING_USER";

		// Token: 0x04000152 RID: 338
		internal const string LocalUserConfigName = "LOCAL_USER";

		// Token: 0x04000153 RID: 339
		internal const string MachineConfigPath = "MACHINE";

		// Token: 0x04000154 RID: 340
		internal const string ExeConfigPath = "MACHINE/EXE";

		// Token: 0x04000155 RID: 341
		internal const string RoamingUserConfigPath = "MACHINE/EXE/ROAMING_USER";

		// Token: 0x04000156 RID: 342
		internal const string LocalUserConfigPath = "MACHINE/EXE/ROAMING_USER/LOCAL_USER";

		// Token: 0x04000157 RID: 343
		private const string ConfigExtension = ".config";

		// Token: 0x04000158 RID: 344
		private const string MachineConfigFilename = "machine.config";

		// Token: 0x04000159 RID: 345
		private const string MachineConfigSubdirectory = "Config";

		// Token: 0x0400015A RID: 346
		private static object s_init = new object();

		// Token: 0x0400015B RID: 347
		private static object s_version = new object();

		// Token: 0x0400015C RID: 348
		private static volatile string s_machineConfigFilePath;

		// Token: 0x0400015D RID: 349
		private string _exePath;

		// Token: 0x0400015E RID: 350
		private ClientConfigPaths _configPaths;

		// Token: 0x0400015F RID: 351
		private ExeConfigurationFileMap _fileMap;

		// Token: 0x04000160 RID: 352
		private bool _initComplete;
	}
}
