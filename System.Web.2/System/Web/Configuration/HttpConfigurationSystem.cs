using System;
using System.Configuration.Internal;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Web.Hosting;

namespace System.Web.Configuration
{
	// Token: 0x020006F7 RID: 1783
	internal class HttpConfigurationSystem : IInternalConfigSystem
	{
		// Token: 0x06005606 RID: 22022 RVA: 0x000030B5 File Offset: 0x000012B5
		private HttpConfigurationSystem()
		{
		}

		// Token: 0x06005607 RID: 22023 RVA: 0x0012DC6C File Offset: 0x0012BE6C
		internal static void EnsureInit(IConfigMapPath configMapPath, bool listenToFileChanges, bool initComplete)
		{
			if (!HttpConfigurationSystem.s_inited)
			{
				object obj = HttpConfigurationSystem.s_initLock;
				lock (obj)
				{
					if (!HttpConfigurationSystem.s_inited)
					{
						HttpConfigurationSystem.s_initComplete = initComplete;
						if (configMapPath == null)
						{
							configMapPath = IISMapPath.GetInstance();
						}
						HttpConfigurationSystem.s_configMapPath = configMapPath;
						Type type = Type.GetType("System.Configuration.Internal.ConfigSystem, System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true);
						HttpConfigurationSystem.s_configSystem = (IConfigSystem)Activator.CreateInstance(type, true);
						HttpConfigurationSystem.s_configSystem.Init(typeof(WebConfigurationHost), new object[]
						{
							true,
							HttpConfigurationSystem.s_configMapPath,
							null,
							HostingEnvironment.ApplicationVirtualPath,
							HostingEnvironment.SiteNameNoDemand,
							HostingEnvironment.SiteID
						});
						HttpConfigurationSystem.s_configRoot = HttpConfigurationSystem.s_configSystem.Root;
						HttpConfigurationSystem.s_configHost = (WebConfigurationHost)HttpConfigurationSystem.s_configSystem.Host;
						HttpConfigurationSystem httpConfigurationSystem = new HttpConfigurationSystem();
						if (listenToFileChanges)
						{
							HttpConfigurationSystem.s_configRoot.ConfigChanged += httpConfigurationSystem.OnConfigurationChanged;
						}
						Type type2 = Type.GetType("System.Configuration.Internal.InternalConfigSettingsFactory, System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true);
						HttpConfigurationSystem.s_configSettingsFactory = (IInternalConfigSettingsFactory)Activator.CreateInstance(type2, true);
						HttpConfigurationSystem.s_configSettingsFactory.SetConfigurationSystem(httpConfigurationSystem, initComplete);
						HttpConfigurationSystem.s_httpConfigSystem = httpConfigurationSystem;
						HttpConfigurationSystem.s_inited = true;
					}
				}
			}
		}

		// Token: 0x06005608 RID: 22024 RVA: 0x0012DDBC File Offset: 0x0012BFBC
		internal static void CompleteInit()
		{
			HttpConfigurationSystem.s_configSettingsFactory.CompleteInit();
			HttpConfigurationSystem.s_configSettingsFactory = null;
		}

		// Token: 0x170018D3 RID: 6355
		// (get) Token: 0x06005609 RID: 22025 RVA: 0x0012DDD0 File Offset: 0x0012BFD0
		internal static bool UseHttpConfigurationSystem
		{
			get
			{
				if (!HttpConfigurationSystem.s_inited)
				{
					object obj = HttpConfigurationSystem.s_initLock;
					lock (obj)
					{
						if (!HttpConfigurationSystem.s_inited)
						{
							HttpConfigurationSystem.s_inited = true;
						}
					}
				}
				return HttpConfigurationSystem.s_httpConfigSystem != null;
			}
		}

		// Token: 0x170018D4 RID: 6356
		// (get) Token: 0x0600560A RID: 22026 RVA: 0x0012DE2C File Offset: 0x0012C02C
		internal static bool IsSet
		{
			get
			{
				return HttpConfigurationSystem.s_httpConfigSystem != null;
			}
		}

		// Token: 0x0600560B RID: 22027 RVA: 0x0012DE36 File Offset: 0x0012C036
		object IInternalConfigSystem.GetSection(string configKey)
		{
			return HttpConfigurationSystem.GetSection(configKey);
		}

		// Token: 0x0600560C RID: 22028 RVA: 0x00006164 File Offset: 0x00004364
		void IInternalConfigSystem.RefreshConfig(string sectionName)
		{
		}

		// Token: 0x170018D5 RID: 6357
		// (get) Token: 0x0600560D RID: 22029 RVA: 0x00007722 File Offset: 0x00005922
		bool IInternalConfigSystem.SupportsUserConfig
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600560E RID: 22030 RVA: 0x0012DE40 File Offset: 0x0012C040
		internal static object GetSection(string sectionName)
		{
			HttpContext httpContext = HttpContext.Current;
			if (httpContext != null)
			{
				return httpContext.GetSection(sectionName);
			}
			return HttpConfigurationSystem.GetApplicationSection(sectionName);
		}

		// Token: 0x0600560F RID: 22031 RVA: 0x0012DE64 File Offset: 0x0012C064
		internal static object GetSection(string sectionName, VirtualPath path)
		{
			CachedPathData virtualPathData = CachedPathData.GetVirtualPathData(path, true);
			return virtualPathData.ConfigRecord.GetSection(sectionName);
		}

		// Token: 0x06005610 RID: 22032 RVA: 0x0012DE85 File Offset: 0x0012C085
		internal static object GetSection(string sectionName, string path)
		{
			return HttpConfigurationSystem.GetSection(sectionName, VirtualPath.CreateNonRelativeAllowNull(path));
		}

		// Token: 0x06005611 RID: 22033 RVA: 0x0012DE94 File Offset: 0x0012C094
		internal static object GetApplicationSection(string sectionName)
		{
			CachedPathData applicationPathData = CachedPathData.GetApplicationPathData();
			return applicationPathData.ConfigRecord.GetSection(sectionName);
		}

		// Token: 0x06005612 RID: 22034 RVA: 0x0012DEB4 File Offset: 0x0012C0B4
		internal static IInternalConfigRecord GetUniqueConfigRecord(string configPath)
		{
			if (!HttpConfigurationSystem.UseHttpConfigurationSystem)
			{
				return null;
			}
			return HttpConfigurationSystem.s_configRoot.GetUniqueConfigRecord(configPath);
		}

		// Token: 0x06005613 RID: 22035 RVA: 0x0012DED7 File Offset: 0x0012C0D7
		internal static void AddFileDependency(string file)
		{
			if (string.IsNullOrEmpty(file))
			{
				return;
			}
			if (HttpConfigurationSystem.UseHttpConfigurationSystem)
			{
				if (HttpConfigurationSystem.s_fileChangeEventHandler == null)
				{
					HttpConfigurationSystem.s_fileChangeEventHandler = new FileChangeEventHandler(HttpConfigurationSystem.s_httpConfigSystem.OnConfigFileChanged);
				}
				HttpRuntime.FileChangesMonitor.StartMonitoringFile(file, HttpConfigurationSystem.s_fileChangeEventHandler);
			}
		}

		// Token: 0x06005614 RID: 22036 RVA: 0x0012DF16 File Offset: 0x0012C116
		internal void OnConfigurationChanged(object sender, InternalConfigEventArgs e)
		{
			HttpRuntime.OnConfigChange(null);
		}

		// Token: 0x06005615 RID: 22037 RVA: 0x0012DF20 File Offset: 0x0012C120
		internal void OnConfigFileChanged(object sender, FileChangeEvent e)
		{
			string message = FileChangesMonitor.GenerateErrorMessage(e.Action, e.FileName);
			HttpRuntime.OnConfigChange(message);
		}

		// Token: 0x170018D6 RID: 6358
		// (get) Token: 0x06005616 RID: 22038 RVA: 0x0012DF45 File Offset: 0x0012C145
		internal static string MsCorLibDirectory
		{
			[FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.PathDiscovery)]
			get
			{
				if (HttpConfigurationSystem.s_MsCorLibDirectory == null)
				{
					HttpConfigurationSystem.s_MsCorLibDirectory = RuntimeEnvironment.GetRuntimeDirectory();
				}
				return HttpConfigurationSystem.s_MsCorLibDirectory;
			}
		}

		// Token: 0x170018D7 RID: 6359
		// (get) Token: 0x06005617 RID: 22039 RVA: 0x0012DF5D File Offset: 0x0012C15D
		internal static string MachineConfigurationDirectory
		{
			get
			{
				if (HttpConfigurationSystem.s_MachineConfigurationDirectory == null)
				{
					HttpConfigurationSystem.s_MachineConfigurationDirectory = Path.Combine(HttpConfigurationSystem.MsCorLibDirectory, "Config");
				}
				return HttpConfigurationSystem.s_MachineConfigurationDirectory;
			}
		}

		// Token: 0x170018D8 RID: 6360
		// (get) Token: 0x06005618 RID: 22040 RVA: 0x0012DF7F File Offset: 0x0012C17F
		internal static string MachineConfigurationFilePath
		{
			get
			{
				if (HttpConfigurationSystem.s_MachineConfigurationFilePath == null)
				{
					HttpConfigurationSystem.s_MachineConfigurationFilePath = Path.Combine(HttpConfigurationSystem.MachineConfigurationDirectory, "machine.config");
				}
				return HttpConfigurationSystem.s_MachineConfigurationFilePath;
			}
		}

		// Token: 0x170018D9 RID: 6361
		// (get) Token: 0x06005619 RID: 22041 RVA: 0x0012DFA1 File Offset: 0x0012C1A1
		// (set) Token: 0x0600561A RID: 22042 RVA: 0x0012DFC3 File Offset: 0x0012C1C3
		internal static string RootWebConfigurationFilePath
		{
			get
			{
				if (HttpConfigurationSystem.s_RootWebConfigurationFilePath == null)
				{
					HttpConfigurationSystem.s_RootWebConfigurationFilePath = Path.Combine(HttpConfigurationSystem.MachineConfigurationDirectory, "web.config");
				}
				return HttpConfigurationSystem.s_RootWebConfigurationFilePath;
			}
			set
			{
				HttpConfigurationSystem.s_RootWebConfigurationFilePath = value;
				if (HttpConfigurationSystem.s_RootWebConfigurationFilePath == null)
				{
					HttpConfigurationSystem.s_RootWebConfigurationFilePath = Path.Combine(HttpConfigurationSystem.MachineConfigurationDirectory, "web.config");
				}
			}
		}

		// Token: 0x04002DAE RID: 11694
		private const string InternalConfigSettingsFactoryTypeString = "System.Configuration.Internal.InternalConfigSettingsFactory, System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04002DAF RID: 11695
		internal const string ConfigSystemTypeString = "System.Configuration.Internal.ConfigSystem, System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04002DB0 RID: 11696
		internal const string MachineConfigSubdirectory = "Config";

		// Token: 0x04002DB1 RID: 11697
		internal const string MachineConfigFilename = "machine.config";

		// Token: 0x04002DB2 RID: 11698
		internal const string RootWebConfigFilename = "web.config";

		// Token: 0x04002DB3 RID: 11699
		internal const string WebConfigFileName = "web.config";

		// Token: 0x04002DB4 RID: 11700
		internal const string InetsrvDirectoryName = "inetsrv";

		// Token: 0x04002DB5 RID: 11701
		internal const string ApplicationHostConfigFileName = "applicationHost.config";

		// Token: 0x04002DB6 RID: 11702
		private static object s_initLock = new object();

		// Token: 0x04002DB7 RID: 11703
		private static volatile bool s_inited;

		// Token: 0x04002DB8 RID: 11704
		private static HttpConfigurationSystem s_httpConfigSystem;

		// Token: 0x04002DB9 RID: 11705
		private static IConfigSystem s_configSystem;

		// Token: 0x04002DBA RID: 11706
		private static IConfigMapPath s_configMapPath;

		// Token: 0x04002DBB RID: 11707
		private static WebConfigurationHost s_configHost;

		// Token: 0x04002DBC RID: 11708
		private static FileChangeEventHandler s_fileChangeEventHandler;

		// Token: 0x04002DBD RID: 11709
		private static string s_MsCorLibDirectory;

		// Token: 0x04002DBE RID: 11710
		private static string s_MachineConfigurationDirectory;

		// Token: 0x04002DBF RID: 11711
		private static string s_MachineConfigurationFilePath;

		// Token: 0x04002DC0 RID: 11712
		private static string s_RootWebConfigurationFilePath;

		// Token: 0x04002DC1 RID: 11713
		private static IInternalConfigRoot s_configRoot;

		// Token: 0x04002DC2 RID: 11714
		private static IInternalConfigSettingsFactory s_configSettingsFactory;

		// Token: 0x04002DC3 RID: 11715
		private static bool s_initComplete;
	}
}
