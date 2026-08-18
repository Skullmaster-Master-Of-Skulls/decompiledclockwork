using System;
using System.Collections;
using System.Configuration;
using System.Configuration.Internal;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Web.Compilation;
using System.Web.Configuration.Internal;
using System.Web.Hosting;
using System.Web.Util;
using Microsoft.Build.Utilities;

namespace System.Web.Configuration
{
	// Token: 0x02000774 RID: 1908
	internal sealed class WebConfigurationHost : DelegatingConfigHost, IInternalConfigWebHost
	{
		// Token: 0x17001AE7 RID: 6887
		// (get) Token: 0x06005BCB RID: 23499 RVA: 0x0013DDB7 File Offset: 0x0013BFB7
		internal static string DefaultSiteName
		{
			get
			{
				if (WebConfigurationHost.s_defaultSiteName == null)
				{
					WebConfigurationHost.s_defaultSiteName = SR.GetString("DefaultSiteName");
				}
				return WebConfigurationHost.s_defaultSiteName;
			}
		}

		// Token: 0x06005BCC RID: 23500 RVA: 0x0013DDD4 File Offset: 0x0013BFD4
		internal WebConfigurationHost()
		{
			Type type = Type.GetType("System.Configuration.Internal.InternalConfigHost, System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true);
			base.Host = (IInternalConfigHost)Activator.CreateInstance(type, true);
		}

		// Token: 0x06005BCD RID: 23501 RVA: 0x0013DE08 File Offset: 0x0013C008
		internal static void GetConfigPaths(IConfigMapPath configMapPath, WebLevel webLevel, VirtualPath virtualPath, string site, string locationSubPath, out VirtualPath appPath, out string appSiteName, out string appSiteID, out string configPath, out string locationConfigPath)
		{
			appPath = null;
			appSiteName = null;
			appSiteID = null;
			if (webLevel == WebLevel.Machine || virtualPath == null)
			{
				if (!string.IsNullOrEmpty(site) && string.IsNullOrEmpty(locationSubPath))
				{
					throw ExceptionUtil.ParameterInvalid("site");
				}
				if (webLevel == WebLevel.Machine)
				{
					configPath = "machine";
				}
				else
				{
					configPath = "machine/webroot";
				}
			}
			else
			{
				if (!string.IsNullOrEmpty(site))
				{
					configMapPath.ResolveSiteArgument(site, out appSiteName, out appSiteID);
					if (string.IsNullOrEmpty(appSiteID))
					{
						throw new InvalidOperationException(SR.GetString("Config_failed_to_resolve_site_id", new object[]
						{
							site
						}));
					}
				}
				else
				{
					if (HostingEnvironment.IsHosted)
					{
						appSiteName = HostingEnvironment.SiteNameNoDemand;
						appSiteID = HostingEnvironment.SiteID;
					}
					if (string.IsNullOrEmpty(appSiteID))
					{
						configMapPath.GetDefaultSiteNameAndID(out appSiteName, out appSiteID);
					}
				}
				configPath = WebConfigurationHost.GetConfigPathFromSiteIDAndVPath(appSiteID, virtualPath);
			}
			locationConfigPath = null;
			string text = null;
			VirtualPath virtualPath2 = null;
			if (locationSubPath != null)
			{
				locationConfigPath = WebConfigurationHost.GetConfigPathFromLocationSubPathBasic(configPath, locationSubPath);
				WebConfigurationHost.GetSiteIDAndVPathFromConfigPath(locationConfigPath, out text, out virtualPath2);
				if (string.IsNullOrEmpty(appSiteID) && !string.IsNullOrEmpty(text))
				{
					configMapPath.ResolveSiteArgument(text, out appSiteName, out appSiteID);
					if (!string.IsNullOrEmpty(appSiteID))
					{
						locationConfigPath = WebConfigurationHost.GetConfigPathFromSiteIDAndVPath(appSiteID, virtualPath2);
					}
					else if (virtualPath2 == null || virtualPath2.VirtualPathString == "/")
					{
						appSiteName = text;
						appSiteID = text;
					}
					else
					{
						appSiteName = null;
						appSiteID = null;
					}
				}
			}
			string text2 = null;
			if (virtualPath2 != null)
			{
				text2 = configMapPath.GetAppPathForPath(appSiteID, virtualPath2.VirtualPathString);
			}
			else if (virtualPath != null)
			{
				text2 = configMapPath.GetAppPathForPath(appSiteID, virtualPath.VirtualPathString);
			}
			if (text2 != null)
			{
				appPath = VirtualPath.Create(text2);
			}
		}

		// Token: 0x06005BCE RID: 23502 RVA: 0x0013DF9C File Offset: 0x0013C19C
		private void ChooseAndInitConfigMapPath(bool useConfigMapPath, IConfigMapPath configMapPath, ConfigurationFileMap fileMap)
		{
			if (useConfigMapPath)
			{
				this._configMapPath = configMapPath;
			}
			else if (fileMap != null)
			{
				this._configMapPath = new UserMapPath(fileMap);
			}
			else if (HostingEnvironment.IsHosted)
			{
				this._configMapPath = HostingPreferredMapPath.GetInstance();
			}
			else
			{
				this._configMapPath = IISMapPath.GetInstance();
			}
			this._configMapPath2 = (this._configMapPath as IConfigMapPath2);
		}

		// Token: 0x06005BCF RID: 23503 RVA: 0x0013DFF8 File Offset: 0x0013C1F8
		public override void Init(IInternalConfigRoot configRoot, params object[] hostInitParams)
		{
			bool useConfigMapPath = (bool)hostInitParams[0];
			IConfigMapPath configMapPath = (IConfigMapPath)hostInitParams[1];
			ConfigurationFileMap fileMap = (ConfigurationFileMap)hostInitParams[2];
			string text = (string)hostInitParams[3];
			string appSiteName = (string)hostInitParams[4];
			string appSiteID = (string)hostInitParams[5];
			if (hostInitParams.Length > 6)
			{
				string moniker = hostInitParams[6] as string;
				this._machineConfigFile = WebConfigurationHost.GetMachineConfigPathFromTargetFrameworkMoniker(moniker);
				if (!string.IsNullOrEmpty(this._machineConfigFile))
				{
					this._rootWebConfigFile = Path.Combine(Path.GetDirectoryName(this._machineConfigFile), "web.config");
				}
			}
			base.Host.Init(configRoot, hostInitParams);
			this.ChooseAndInitConfigMapPath(useConfigMapPath, configMapPath, fileMap);
			text = UrlPath.RemoveSlashFromPathIfNeeded(text);
			this._appPath = VirtualPath.CreateAbsoluteAllowNull(text);
			this._appSiteName = appSiteName;
			this._appSiteID = appSiteID;
			if (!string.IsNullOrEmpty(this._appSiteID) && this._appPath != null)
			{
				this._appConfigPath = WebConfigurationHost.GetConfigPathFromSiteIDAndVPath(this._appSiteID, this._appPath);
			}
		}

		// Token: 0x06005BD0 RID: 23504 RVA: 0x0013E0F0 File Offset: 0x0013C2F0
		public override void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot configRoot, params object[] hostInitConfigurationParams)
		{
			WebLevel webLevel = (WebLevel)hostInitConfigurationParams[0];
			ConfigurationFileMap fileMap = (ConfigurationFileMap)hostInitConfigurationParams[1];
			VirtualPath virtualPath = VirtualPath.CreateAbsoluteAllowNull((string)hostInitConfigurationParams[2]);
			string site = (string)hostInitConfigurationParams[3];
			if (locationSubPath == null)
			{
				locationSubPath = (string)hostInitConfigurationParams[4];
			}
			base.Host.Init(configRoot, hostInitConfigurationParams);
			this.ChooseAndInitConfigMapPath(false, null, fileMap);
			WebConfigurationHost.GetConfigPaths(this._configMapPath, webLevel, virtualPath, site, locationSubPath, out this._appPath, out this._appSiteName, out this._appSiteID, out configPath, out locationConfigPath);
			this._appConfigPath = WebConfigurationHost.GetConfigPathFromSiteIDAndVPath(this._appSiteID, this._appPath);
			if (WebConfigurationHost.IsVirtualPathConfigPath(configPath))
			{
				string siteID;
				VirtualPath virtualPath2;
				WebConfigurationHost.GetSiteIDAndVPathFromConfigPath(configPath, out siteID, out virtualPath2);
				string value;
				if (this._configMapPath2 != null)
				{
					value = this._configMapPath2.MapPath(siteID, virtualPath2);
				}
				else
				{
					value = this._configMapPath.MapPath(siteID, virtualPath2.VirtualPathString);
				}
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentOutOfRangeException("site");
				}
			}
		}

		// Token: 0x06005BD1 RID: 23505 RVA: 0x0013E1E5 File Offset: 0x0013C3E5
		internal static bool IsMachineConfigPath(string configPath)
		{
			return configPath.Length == "machine".Length;
		}

		// Token: 0x06005BD2 RID: 23506 RVA: 0x0013E1F9 File Offset: 0x0013C3F9
		internal static bool IsRootWebConfigPath(string configPath)
		{
			return configPath.Length == "machine/webroot".Length;
		}

		// Token: 0x06005BD3 RID: 23507 RVA: 0x0013E20D File Offset: 0x0013C40D
		internal static bool IsVirtualPathConfigPath(string configPath)
		{
			return configPath.Length > "machine/webroot".Length;
		}

		// Token: 0x06005BD4 RID: 23508 RVA: 0x0013E224 File Offset: 0x0013C424
		internal static bool IsValidSiteArgument(string site)
		{
			if (!string.IsNullOrEmpty(site))
			{
				char c = site[0];
				char c2 = site[site.Length - 1];
				if (c == '/' || c == '\\' || c2 == '/' || c2 == '\\')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005BD5 RID: 23509 RVA: 0x0013E268 File Offset: 0x0013C468
		internal static string VPathFromConfigPath(string configPath)
		{
			if (!WebConfigurationHost.IsVirtualPathConfigPath(configPath))
			{
				return null;
			}
			int startIndex = "machine/webroot".Length + 1;
			int num = configPath.IndexOf('/', startIndex);
			if (num == -1)
			{
				return "/";
			}
			return configPath.Substring(num);
		}

		// Token: 0x06005BD6 RID: 23510 RVA: 0x0013E2A8 File Offset: 0x0013C4A8
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
		void IInternalConfigWebHost.GetSiteIDAndVPathFromConfigPath(string configPath, out string siteID, out string vpath)
		{
			VirtualPath virtualPath;
			WebConfigurationHost.GetSiteIDAndVPathFromConfigPath(configPath, out siteID, out virtualPath);
			vpath = VirtualPath.GetVirtualPathString(virtualPath);
		}

		// Token: 0x06005BD7 RID: 23511 RVA: 0x0013E2C8 File Offset: 0x0013C4C8
		internal static void GetSiteIDAndVPathFromConfigPath(string configPath, out string siteID, out VirtualPath vpath)
		{
			if (!WebConfigurationHost.IsVirtualPathConfigPath(configPath))
			{
				siteID = null;
				vpath = null;
				return;
			}
			int num = "machine/webroot".Length + 1;
			int num2 = configPath.IndexOf('/', num);
			int length;
			if (num2 == -1)
			{
				length = configPath.Length - num;
			}
			else
			{
				length = num2 - num;
			}
			siteID = configPath.Substring(num, length);
			if (num2 == -1)
			{
				vpath = VirtualPath.RootVirtualPath;
				return;
			}
			vpath = VirtualPath.CreateAbsolute(configPath.Substring(num2));
		}

		// Token: 0x06005BD8 RID: 23512 RVA: 0x0013E332 File Offset: 0x0013C532
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
		string IInternalConfigWebHost.GetConfigPathFromSiteIDAndVPath(string siteID, string vpath)
		{
			return WebConfigurationHost.GetConfigPathFromSiteIDAndVPath(siteID, VirtualPath.CreateAbsoluteAllowNull(vpath));
		}

		// Token: 0x06005BD9 RID: 23513 RVA: 0x0013E340 File Offset: 0x0013C540
		internal static string GetConfigPathFromSiteIDAndVPath(string siteID, VirtualPath vpath)
		{
			if (vpath == null || string.IsNullOrEmpty(siteID))
			{
				return "machine/webroot";
			}
			string text = vpath.VirtualPathStringNoTrailingSlash.ToLower(CultureInfo.InvariantCulture);
			string text2 = (siteID == "1") ? WebConfigurationHost.RootWebConfigPathAndDefaultSiteID : (WebConfigurationHost.RootWebConfigPathAndPathSeparator + siteID);
			if (text.Length > 1)
			{
				text2 += text;
			}
			return text2;
		}

		// Token: 0x06005BDA RID: 23514 RVA: 0x0013E3A7 File Offset: 0x0013C5A7
		internal static string CombineConfigPath(string parentConfigPath, string childConfigPath)
		{
			if (string.IsNullOrEmpty(parentConfigPath))
			{
				return childConfigPath;
			}
			if (string.IsNullOrEmpty(childConfigPath))
			{
				return parentConfigPath;
			}
			return parentConfigPath + "/" + childConfigPath;
		}

		// Token: 0x06005BDB RID: 23515 RVA: 0x0013E3CC File Offset: 0x0013C5CC
		public override bool IsConfigRecordRequired(string configPath)
		{
			if (!WebConfigurationHost.IsVirtualPathConfigPath(configPath))
			{
				return true;
			}
			string siteID;
			VirtualPath virtualPath;
			WebConfigurationHost.GetSiteIDAndVPathFromConfigPath(configPath, out siteID, out virtualPath);
			string text;
			if (this._configMapPath2 != null)
			{
				text = this._configMapPath2.MapPath(siteID, virtualPath);
			}
			else
			{
				text = this._configMapPath.MapPath(siteID, virtualPath.VirtualPathString);
			}
			return text == null || FileUtil.DirectoryExists(text, true);
		}

		// Token: 0x06005BDC RID: 23516 RVA: 0x0013E424 File Offset: 0x0013C624
		public override string GetStreamName(string configPath)
		{
			if (WebConfigurationHost.IsMachineConfigPath(configPath))
			{
				if (string.IsNullOrEmpty(this._machineConfigFile))
				{
					return this._configMapPath.GetMachineConfigFilename();
				}
				return this._machineConfigFile;
			}
			else if (WebConfigurationHost.IsRootWebConfigPath(configPath))
			{
				if (string.IsNullOrEmpty(this._rootWebConfigFile))
				{
					return this._configMapPath.GetRootWebConfigFilename();
				}
				return this._rootWebConfigFile;
			}
			else
			{
				string siteID;
				VirtualPath virtualPath;
				WebConfigurationHost.GetSiteIDAndVPathFromConfigPath(configPath, out siteID, out virtualPath);
				string text;
				string baseName;
				if (this._configMapPath2 != null)
				{
					this._configMapPath2.GetPathConfigFilename(siteID, virtualPath, out text, out baseName);
				}
				else
				{
					this._configMapPath.GetPathConfigFilename(siteID, virtualPath.VirtualPathString, out text, out baseName);
				}
				if (text == null)
				{
					return null;
				}
				bool flag;
				bool flag2;
				FileUtil.PhysicalPathStatus(text, true, false, out flag, out flag2);
				if (flag && flag2)
				{
					return this.CombineAndValidatePath(text, baseName);
				}
				return null;
			}
		}

		// Token: 0x06005BDD RID: 23517 RVA: 0x0013E4E0 File Offset: 0x0013C6E0
		[FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.PathDiscovery)]
		private string CombineAndValidatePath(string directory, string baseName)
		{
			string result;
			try
			{
				string text = Path.Combine(directory, baseName);
				Path.GetFullPath(text);
				result = text;
			}
			catch (PathTooLongException)
			{
				result = null;
			}
			catch (NotSupportedException)
			{
				result = null;
			}
			catch (ArgumentException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x17001AE8 RID: 6888
		// (get) Token: 0x06005BDE RID: 23518 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool SupportsChangeNotifications
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001AE9 RID: 6889
		// (get) Token: 0x06005BDF RID: 23519 RVA: 0x0013E538 File Offset: 0x0013C738
		private Hashtable FileChangeCallbacks
		{
			get
			{
				if (this._fileChangeCallbacks == null)
				{
					this._fileChangeCallbacks = new Hashtable(StringComparer.OrdinalIgnoreCase);
				}
				return this._fileChangeCallbacks;
			}
		}

		// Token: 0x06005BE0 RID: 23520 RVA: 0x0013E558 File Offset: 0x0013C758
		public override object StartMonitoringStreamForChanges(string streamName, StreamChangeCallback callback)
		{
			WebConfigurationHostFileChange webConfigurationHostFileChange;
			lock (this)
			{
				webConfigurationHostFileChange = new WebConfigurationHostFileChange(callback);
				ArrayList arrayList = (ArrayList)this.FileChangeCallbacks[streamName];
				if (arrayList == null)
				{
					arrayList = new ArrayList(1);
					this.FileChangeCallbacks.Add(streamName, arrayList);
				}
				arrayList.Add(webConfigurationHostFileChange);
			}
			HttpRuntime.FileChangesMonitor.StartMonitoringFile(streamName, new FileChangeEventHandler(webConfigurationHostFileChange.OnFileChanged));
			return webConfigurationHostFileChange;
		}

		// Token: 0x06005BE1 RID: 23521 RVA: 0x0013E5E0 File Offset: 0x0013C7E0
		public override void StopMonitoringStreamForChanges(string streamName, StreamChangeCallback callback)
		{
			WebConfigurationHostFileChange target = null;
			lock (this)
			{
				ArrayList arrayList = (ArrayList)this.FileChangeCallbacks[streamName];
				int i = 0;
				while (i < arrayList.Count)
				{
					WebConfigurationHostFileChange webConfigurationHostFileChange = (WebConfigurationHostFileChange)arrayList[i];
					if (webConfigurationHostFileChange.Callback == callback)
					{
						target = webConfigurationHostFileChange;
						arrayList.RemoveAt(i);
						if (arrayList.Count == 0)
						{
							this.FileChangeCallbacks.Remove(streamName);
							break;
						}
						break;
					}
					else
					{
						i++;
					}
				}
			}
			HttpRuntime.FileChangesMonitor.StopMonitoringFile(streamName, target);
		}

		// Token: 0x06005BE2 RID: 23522 RVA: 0x0013E684 File Offset: 0x0013C884
		public override bool IsDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition)
		{
			if (allowDefinition <= ConfigurationAllowDefinition.MachineToWebRoot)
			{
				if (allowDefinition == ConfigurationAllowDefinition.MachineOnly)
				{
					return configPath.Length <= "machine".Length;
				}
				if (allowDefinition == ConfigurationAllowDefinition.MachineToWebRoot)
				{
					return configPath.Length <= "machine/webroot".Length;
				}
			}
			else
			{
				if (allowDefinition == ConfigurationAllowDefinition.MachineToApplication)
				{
					return string.IsNullOrEmpty(this._appConfigPath) || configPath.Length <= this._appConfigPath.Length || this.IsApplication(configPath);
				}
				if (allowDefinition == ConfigurationAllowDefinition.Everywhere)
				{
					return true;
				}
			}
			throw ExceptionUtil.UnexpectedError("WebConfigurationHost::IsDefinitionAllowed");
		}

		// Token: 0x06005BE3 RID: 23523 RVA: 0x0013E714 File Offset: 0x0013C914
		public override void VerifyDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition, IConfigErrorInfo errorInfo)
		{
			if (this.IsDefinitionAllowed(configPath, allowDefinition, allowExeDefinition))
			{
				return;
			}
			if (allowDefinition == ConfigurationAllowDefinition.MachineOnly)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_allow_definition_error_machine"), errorInfo.Filename, errorInfo.LineNumber);
			}
			if (allowDefinition == ConfigurationAllowDefinition.MachineToWebRoot)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_allow_definition_error_webroot"), errorInfo.Filename, errorInfo.LineNumber);
			}
			if (allowDefinition != ConfigurationAllowDefinition.MachineToApplication)
			{
				throw ExceptionUtil.UnexpectedError("WebConfigurationHost::VerifyDefinitionAllowed");
			}
			throw new ConfigurationErrorsException(SR.GetString("Config_allow_definition_error_application"), errorInfo.Filename, errorInfo.LineNumber);
		}

		// Token: 0x06005BE4 RID: 23524 RVA: 0x0013E7A4 File Offset: 0x0013C9A4
		private WebApplicationLevel GetPathLevel(string configPath)
		{
			if (!WebConfigurationHost.IsVirtualPathConfigPath(configPath))
			{
				return WebApplicationLevel.AboveApplication;
			}
			if (this._appPath == null)
			{
				return WebApplicationLevel.AboveApplication;
			}
			string s;
			VirtualPath virtualPath;
			WebConfigurationHost.GetSiteIDAndVPathFromConfigPath(configPath, out s, out virtualPath);
			if (!StringUtil.EqualsIgnoreCase(this._appSiteID, s))
			{
				return WebApplicationLevel.AboveApplication;
			}
			if (this._appPath == virtualPath)
			{
				return WebApplicationLevel.AtApplication;
			}
			if (UrlPath.IsEqualOrSubpath(this._appPath.VirtualPathString, virtualPath.VirtualPathString))
			{
				return WebApplicationLevel.BelowApplication;
			}
			return WebApplicationLevel.AboveApplication;
		}

		// Token: 0x17001AEA RID: 6890
		// (get) Token: 0x06005BE5 RID: 23525 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool SupportsPath
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001AEB RID: 6891
		// (get) Token: 0x06005BE6 RID: 23526 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool SupportsLocation
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005BE7 RID: 23527 RVA: 0x0013E816 File Offset: 0x0013CA16
		public override bool IsAboveApplication(string configPath)
		{
			return this.GetPathLevel(configPath) == WebApplicationLevel.AboveApplication;
		}

		// Token: 0x06005BE8 RID: 23528 RVA: 0x0013E824 File Offset: 0x0013CA24
		internal static string GetConfigPathFromLocationSubPathBasic(string configPath, string locationSubPath)
		{
			string result;
			if (WebConfigurationHost.IsVirtualPathConfigPath(configPath))
			{
				result = WebConfigurationHost.CombineConfigPath(configPath, locationSubPath);
			}
			else
			{
				result = WebConfigurationHost.CombineConfigPath("machine/webroot", locationSubPath);
			}
			return result;
		}

		// Token: 0x06005BE9 RID: 23529 RVA: 0x0013E850 File Offset: 0x0013CA50
		public override string GetConfigPathFromLocationSubPath(string configPath, string locationSubPath)
		{
			string result;
			if (WebConfigurationHost.IsVirtualPathConfigPath(configPath))
			{
				result = WebConfigurationHost.CombineConfigPath(configPath, locationSubPath);
			}
			else
			{
				int num = locationSubPath.IndexOf('/');
				string text;
				VirtualPath vpath;
				if (num < 0)
				{
					text = locationSubPath;
					vpath = VirtualPath.RootVirtualPath;
				}
				else
				{
					text = locationSubPath.Substring(0, num);
					vpath = VirtualPath.CreateAbsolute(locationSubPath.Substring(num));
				}
				string siteID;
				if (StringUtil.EqualsIgnoreCase(text, this._appSiteID) || StringUtil.EqualsIgnoreCase(text, this._appSiteName))
				{
					siteID = this._appSiteID;
				}
				else
				{
					siteID = text;
				}
				result = WebConfigurationHost.GetConfigPathFromSiteIDAndVPath(siteID, vpath);
			}
			return result;
		}

		// Token: 0x06005BEA RID: 23530 RVA: 0x0013E8D2 File Offset: 0x0013CAD2
		public override bool IsLocationApplicable(string configPath)
		{
			return WebConfigurationHost.IsVirtualPathConfigPath(configPath);
		}

		// Token: 0x06005BEB RID: 23531 RVA: 0x0013E8DA File Offset: 0x0013CADA
		internal static void StaticGetRestrictedPermissions(IInternalConfigRecord configRecord, out PermissionSet permissionSet, out bool isHostReady)
		{
			isHostReady = HttpRuntime.IsTrustLevelInitialized;
			permissionSet = null;
			if (isHostReady && WebConfigurationHost.IsVirtualPathConfigPath(configRecord.ConfigPath))
			{
				permissionSet = HttpRuntime.NamedPermissionSet;
			}
		}

		// Token: 0x06005BEC RID: 23532 RVA: 0x0013E8FE File Offset: 0x0013CAFE
		public override bool IsTrustedConfigPath(string configPath)
		{
			return !WebConfigurationHost.IsVirtualPathConfigPath(configPath);
		}

		// Token: 0x06005BED RID: 23533 RVA: 0x0013E909 File Offset: 0x0013CB09
		public override bool IsFullTrustSectionWithoutAptcaAllowed(IInternalConfigRecord configRecord)
		{
			if (HostingEnvironment.IsHosted)
			{
				return HttpRuntime.HasAspNetHostingPermission(AspNetHostingPermissionLevel.Unrestricted);
			}
			return base.Host.IsFullTrustSectionWithoutAptcaAllowed(configRecord);
		}

		// Token: 0x06005BEE RID: 23534 RVA: 0x001382E9 File Offset: 0x001364E9
		public override void GetRestrictedPermissions(IInternalConfigRecord configRecord, out PermissionSet permissionSet, out bool isHostReady)
		{
			WebConfigurationHost.StaticGetRestrictedPermissions(configRecord, out permissionSet, out isHostReady);
		}

		// Token: 0x06005BEF RID: 23535 RVA: 0x0013E929 File Offset: 0x0013CB29
		public override IDisposable Impersonate()
		{
			return new ApplicationImpersonationContext();
		}

		// Token: 0x06005BF0 RID: 23536 RVA: 0x0013E930 File Offset: 0x0013CB30
		public override bool PrefetchAll(string configPath, string streamName)
		{
			return !WebConfigurationHost.IsMachineConfigPath(configPath);
		}

		// Token: 0x06005BF1 RID: 23537 RVA: 0x0013E93C File Offset: 0x0013CB3C
		public override bool PrefetchSection(string sectionGroupName, string sectionName)
		{
			return (StringUtil.StringStartsWith(sectionGroupName, "system.web") && (sectionGroupName.Length == "system.web".Length || sectionGroupName["system.web".Length] == '/')) || (string.IsNullOrEmpty(sectionGroupName) && sectionName == "system.codedom");
		}

		// Token: 0x06005BF2 RID: 23538 RVA: 0x0013E996 File Offset: 0x0013CB96
		public override object CreateDeprecatedConfigContext(string configPath)
		{
			return new HttpConfigurationContext(WebConfigurationHost.VPathFromConfigPath(configPath));
		}

		// Token: 0x06005BF3 RID: 23539 RVA: 0x0013E9A4 File Offset: 0x0013CBA4
		public override object CreateConfigurationContext(string configPath, string locationSubPath)
		{
			string path = WebConfigurationHost.VPathFromConfigPath(configPath);
			WebApplicationLevel pathLevel = this.GetPathLevel(configPath);
			return new WebContext(pathLevel, this._appSiteName, VirtualPath.GetVirtualPathString(this._appPath), path, locationSubPath, this._appConfigPath);
		}

		// Token: 0x06005BF4 RID: 23540 RVA: 0x0013E9DF File Offset: 0x0013CBDF
		public override Type GetConfigType(string typeName, bool throwOnError)
		{
			return BuildManager.GetType(typeName, throwOnError);
		}

		// Token: 0x06005BF5 RID: 23541 RVA: 0x0013E9E8 File Offset: 0x0013CBE8
		public override string GetConfigTypeName(Type t)
		{
			return BuildManager.GetNormalizedTypeName(t);
		}

		// Token: 0x06005BF6 RID: 23542 RVA: 0x0013E9F0 File Offset: 0x0013CBF0
		private bool IsApplication(string configPath)
		{
			string siteID;
			VirtualPath virtualPath;
			WebConfigurationHost.GetSiteIDAndVPathFromConfigPath(configPath, out siteID, out virtualPath);
			VirtualPath v;
			if (this._configMapPath2 != null)
			{
				v = this._configMapPath2.GetAppPathForPath(siteID, virtualPath);
			}
			else
			{
				v = VirtualPath.CreateAllowNull(this._configMapPath.GetAppPathForPath(siteID, virtualPath.VirtualPathString));
			}
			return v == virtualPath;
		}

		// Token: 0x17001AEC RID: 6892
		// (get) Token: 0x06005BF7 RID: 23543 RVA: 0x0013EA40 File Offset: 0x0013CC40
		internal static IInternalConfigConfigurationFactory ConfigurationFactory
		{
			[ReflectionPermission(SecurityAction.Assert, Flags = ReflectionPermissionFlag.MemberAccess)]
			get
			{
				if (WebConfigurationHost.s_configurationFactory == null)
				{
					Type type = Type.GetType("System.Configuration.Internal.InternalConfigConfigurationFactory, System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true);
					WebConfigurationHost.s_configurationFactory = (IInternalConfigConfigurationFactory)Activator.CreateInstance(type, true);
				}
				return WebConfigurationHost.s_configurationFactory;
			}
		}

		// Token: 0x06005BF8 RID: 23544 RVA: 0x0013EA78 File Offset: 0x0013CC78
		internal static Configuration OpenConfiguration(WebLevel webLevel, ConfigurationFileMap fileMap, VirtualPath path, string site, string locationSubPath, string server, string userName, string password, IntPtr tokenHandle)
		{
			if (!WebConfigurationHost.IsValidSiteArgument(site))
			{
				throw ExceptionUtil.ParameterInvalid("site");
			}
			locationSubPath = WebConfigurationHost.ConfigurationFactory.NormalizeLocationSubPath(locationSubPath, null);
			bool flag = !string.IsNullOrEmpty(server) && server != "." && !StringUtil.EqualsIgnoreCase(server, "127.0.0.1") && !StringUtil.EqualsIgnoreCase(server, "::1") && !StringUtil.EqualsIgnoreCase(server, "localhost") && !StringUtil.EqualsIgnoreCase(server, Environment.MachineName);
			Configuration result;
			if (flag)
			{
				result = WebConfigurationHost.ConfigurationFactory.Create(typeof(RemoteWebConfigurationHost), new object[]
				{
					webLevel,
					null,
					VirtualPath.GetVirtualPathString(path),
					site,
					locationSubPath,
					server,
					userName,
					password,
					tokenHandle
				});
			}
			else
			{
				if (string.IsNullOrEmpty(server))
				{
					if (!string.IsNullOrEmpty(userName))
					{
						throw ExceptionUtil.ParameterInvalid("userName");
					}
					if (!string.IsNullOrEmpty(password))
					{
						throw ExceptionUtil.ParameterInvalid("password");
					}
					if (tokenHandle != (IntPtr)0)
					{
						throw ExceptionUtil.ParameterInvalid("tokenHandle");
					}
				}
				if (fileMap != null)
				{
					fileMap = (ConfigurationFileMap)fileMap.Clone();
				}
				WebConfigurationFileMap webConfigurationFileMap = fileMap as WebConfigurationFileMap;
				if (webConfigurationFileMap != null && !string.IsNullOrEmpty(site))
				{
					webConfigurationFileMap.Site = site;
				}
				result = WebConfigurationHost.ConfigurationFactory.Create(typeof(WebConfigurationHost), new object[]
				{
					webLevel,
					fileMap,
					VirtualPath.GetVirtualPathString(path),
					site,
					locationSubPath
				});
			}
			return result;
		}

		// Token: 0x06005BF9 RID: 23545 RVA: 0x0013EC04 File Offset: 0x0013CE04
		private static string GetMachineConfigPathFromTargetFrameworkMoniker(string moniker)
		{
			TargetDotNetFrameworkVersion targetFrameworkVersionEnumFromMoniker = WebConfigurationHost.GetTargetFrameworkVersionEnumFromMoniker(moniker);
			if (targetFrameworkVersionEnumFromMoniker == TargetDotNetFrameworkVersion.Version45)
			{
				return null;
			}
			string pathToDotNetFrameworkFile = ToolLocationHelper.GetPathToDotNetFrameworkFile("config\\machine.config", targetFrameworkVersionEnumFromMoniker);
			new FileIOPermission(FileIOPermissionAccess.PathDiscovery, pathToDotNetFrameworkFile).Demand();
			return pathToDotNetFrameworkFile;
		}

		// Token: 0x06005BFA RID: 23546 RVA: 0x0013EC37 File Offset: 0x0013CE37
		private static TargetDotNetFrameworkVersion GetTargetFrameworkVersionEnumFromMoniker(string moniker)
		{
			if (moniker.Contains("3.5") || moniker.Contains("3.0") || moniker.Contains("2.0"))
			{
				return TargetDotNetFrameworkVersion.Version20;
			}
			return TargetDotNetFrameworkVersion.Version45;
		}

		// Token: 0x04003052 RID: 12370
		private const string InternalHostTypeName = "System.Configuration.Internal.InternalConfigHost, System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04003053 RID: 12371
		private const string InternalConfigConfigurationFactoryTypeName = "System.Configuration.Internal.InternalConfigConfigurationFactory, System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04003054 RID: 12372
		internal const string MachineConfigName = "machine";

		// Token: 0x04003055 RID: 12373
		internal const string MachineConfigPath = "machine";

		// Token: 0x04003056 RID: 12374
		internal const string RootWebConfigName = "webroot";

		// Token: 0x04003057 RID: 12375
		internal const string RootWebConfigPath = "machine/webroot";

		// Token: 0x04003058 RID: 12376
		internal const char PathSeparator = '/';

		// Token: 0x04003059 RID: 12377
		internal const string DefaultSiteID = "1";

		// Token: 0x0400305A RID: 12378
		private static readonly string RootWebConfigPathAndPathSeparator = "machine/webroot/";

		// Token: 0x0400305B RID: 12379
		private static readonly string RootWebConfigPathAndDefaultSiteID = WebConfigurationHost.RootWebConfigPathAndPathSeparator + "1";

		// Token: 0x0400305C RID: 12380
		internal static readonly char[] s_slashSplit = new char[47];

		// Token: 0x0400305D RID: 12381
		private static IInternalConfigConfigurationFactory s_configurationFactory;

		// Token: 0x0400305E RID: 12382
		private static string s_defaultSiteName;

		// Token: 0x0400305F RID: 12383
		private Hashtable _fileChangeCallbacks;

		// Token: 0x04003060 RID: 12384
		private IConfigMapPath _configMapPath;

		// Token: 0x04003061 RID: 12385
		private IConfigMapPath2 _configMapPath2;

		// Token: 0x04003062 RID: 12386
		private VirtualPath _appPath;

		// Token: 0x04003063 RID: 12387
		private string _appSiteName;

		// Token: 0x04003064 RID: 12388
		private string _appSiteID;

		// Token: 0x04003065 RID: 12389
		private string _appConfigPath;

		// Token: 0x04003066 RID: 12390
		private string _machineConfigFile;

		// Token: 0x04003067 RID: 12391
		private string _rootWebConfigFile;

		// Token: 0x04003068 RID: 12392
		private const string SysWebName = "system.web";
	}
}
