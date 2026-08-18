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

namespace System.Web.Configuration
{
	// Token: 0x02000268 RID: 616
	internal sealed class WebConfigurationHost : DelegatingConfigHost, IInternalConfigWebHost
	{
		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06002050 RID: 8272 RVA: 0x0008CFAF File Offset: 0x0008BFAF
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

		// Token: 0x06002051 RID: 8273 RVA: 0x0008CFCC File Offset: 0x0008BFCC
		internal WebConfigurationHost()
		{
			Type type = Type.GetType("System.Configuration.Internal.InternalConfigHost, System.Configuration, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true);
			base.Host = (IInternalConfigHost)Activator.CreateInstance(type, true);
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x0008D000 File Offset: 0x0008C000
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

		// Token: 0x06002053 RID: 8275 RVA: 0x0008D194 File Offset: 0x0008C194
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

		// Token: 0x06002054 RID: 8276 RVA: 0x0008D1F0 File Offset: 0x0008C1F0
		public override void Init(IInternalConfigRoot configRoot, params object[] hostInitParams)
		{
			bool useConfigMapPath = (bool)hostInitParams[0];
			IConfigMapPath configMapPath = (IConfigMapPath)hostInitParams[1];
			ConfigurationFileMap fileMap = (ConfigurationFileMap)hostInitParams[2];
			string text = (string)hostInitParams[3];
			string appSiteName = (string)hostInitParams[4];
			string appSiteID = (string)hostInitParams[5];
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

		// Token: 0x06002055 RID: 8277 RVA: 0x0008D2A0 File Offset: 0x0008C2A0
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

		// Token: 0x06002056 RID: 8278 RVA: 0x0008D395 File Offset: 0x0008C395
		internal static bool IsMachineConfigPath(string configPath)
		{
			return configPath.Length == "machine".Length;
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x0008D3A9 File Offset: 0x0008C3A9
		internal static bool IsRootWebConfigPath(string configPath)
		{
			return configPath.Length == "machine/webroot".Length;
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x0008D3BD File Offset: 0x0008C3BD
		internal static bool IsVirtualPathConfigPath(string configPath)
		{
			return configPath.Length > "machine/webroot".Length;
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x0008D3D4 File Offset: 0x0008C3D4
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

		// Token: 0x0600205A RID: 8282 RVA: 0x0008D418 File Offset: 0x0008C418
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

		// Token: 0x0600205B RID: 8283 RVA: 0x0008D458 File Offset: 0x0008C458
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
		void IInternalConfigWebHost.GetSiteIDAndVPathFromConfigPath(string configPath, out string siteID, out string vpath)
		{
			VirtualPath virtualPath;
			WebConfigurationHost.GetSiteIDAndVPathFromConfigPath(configPath, out siteID, out virtualPath);
			vpath = VirtualPath.GetVirtualPathString(virtualPath);
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x0008D478 File Offset: 0x0008C478
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

		// Token: 0x0600205D RID: 8285 RVA: 0x0008D4E2 File Offset: 0x0008C4E2
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
		string IInternalConfigWebHost.GetConfigPathFromSiteIDAndVPath(string siteID, string vpath)
		{
			return WebConfigurationHost.GetConfigPathFromSiteIDAndVPath(siteID, VirtualPath.CreateAbsoluteAllowNull(vpath));
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x0008D4F0 File Offset: 0x0008C4F0
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

		// Token: 0x0600205F RID: 8287 RVA: 0x0008D557 File Offset: 0x0008C557
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
			return parentConfigPath + '/' + childConfigPath;
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x0008D57C File Offset: 0x0008C57C
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

		// Token: 0x06002061 RID: 8289 RVA: 0x0008D5D4 File Offset: 0x0008C5D4
		public override string GetStreamName(string configPath)
		{
			if (WebConfigurationHost.IsMachineConfigPath(configPath))
			{
				return this._configMapPath.GetMachineConfigFilename();
			}
			if (WebConfigurationHost.IsRootWebConfigPath(configPath))
			{
				return this._configMapPath.GetRootWebConfigFilename();
			}
			string siteID;
			VirtualPath virtualPath;
			WebConfigurationHost.GetSiteIDAndVPathFromConfigPath(configPath, out siteID, out virtualPath);
			string text;
			string path;
			if (this._configMapPath2 != null)
			{
				this._configMapPath2.GetPathConfigFilename(siteID, virtualPath, out text, out path);
			}
			else
			{
				this._configMapPath.GetPathConfigFilename(siteID, virtualPath.VirtualPathString, out text, out path);
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
				try
				{
					return Path.Combine(text, path);
				}
				catch (ArgumentException)
				{
					return null;
				}
			}
			return null;
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06002062 RID: 8290 RVA: 0x0008D684 File Offset: 0x0008C684
		public override bool SupportsChangeNotifications
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06002063 RID: 8291 RVA: 0x0008D687 File Offset: 0x0008C687
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

		// Token: 0x06002064 RID: 8292 RVA: 0x0008D6A8 File Offset: 0x0008C6A8
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

		// Token: 0x06002065 RID: 8293 RVA: 0x0008D728 File Offset: 0x0008C728
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
					if (object.ReferenceEquals(webConfigurationHostFileChange.Callback, callback))
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

		// Token: 0x06002066 RID: 8294 RVA: 0x0008D7C4 File Offset: 0x0008C7C4
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

		// Token: 0x06002067 RID: 8295 RVA: 0x0008D858 File Offset: 0x0008C858
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

		// Token: 0x06002068 RID: 8296 RVA: 0x0008D8EC File Offset: 0x0008C8EC
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

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06002069 RID: 8297 RVA: 0x0008D95E File Offset: 0x0008C95E
		public override bool SupportsPath
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x0600206A RID: 8298 RVA: 0x0008D961 File Offset: 0x0008C961
		public override bool SupportsLocation
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x0008D964 File Offset: 0x0008C964
		public override bool IsAboveApplication(string configPath)
		{
			return this.GetPathLevel(configPath) == WebApplicationLevel.AboveApplication;
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x0008D974 File Offset: 0x0008C974
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

		// Token: 0x0600206D RID: 8301 RVA: 0x0008D9A0 File Offset: 0x0008C9A0
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

		// Token: 0x0600206E RID: 8302 RVA: 0x0008DA22 File Offset: 0x0008CA22
		public override bool IsLocationApplicable(string configPath)
		{
			return WebConfigurationHost.IsVirtualPathConfigPath(configPath);
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x0008DA2A File Offset: 0x0008CA2A
		internal static void StaticGetRestrictedPermissions(IInternalConfigRecord configRecord, out PermissionSet permissionSet, out bool isHostReady)
		{
			isHostReady = HttpRuntime.IsTrustLevelInitialized;
			permissionSet = null;
			if (isHostReady && WebConfigurationHost.IsVirtualPathConfigPath(configRecord.ConfigPath))
			{
				permissionSet = HttpRuntime.NamedPermissionSet;
			}
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x0008DA4E File Offset: 0x0008CA4E
		public override bool IsTrustedConfigPath(string configPath)
		{
			return !WebConfigurationHost.IsVirtualPathConfigPath(configPath);
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x0008DA59 File Offset: 0x0008CA59
		public override bool IsFullTrustSectionWithoutAptcaAllowed(IInternalConfigRecord configRecord)
		{
			if (HostingEnvironment.IsHosted)
			{
				return HttpRuntime.HasAspNetHostingPermission(AspNetHostingPermissionLevel.Unrestricted);
			}
			return base.Host.IsFullTrustSectionWithoutAptcaAllowed(configRecord);
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x0008DA79 File Offset: 0x0008CA79
		public override void GetRestrictedPermissions(IInternalConfigRecord configRecord, out PermissionSet permissionSet, out bool isHostReady)
		{
			WebConfigurationHost.StaticGetRestrictedPermissions(configRecord, out permissionSet, out isHostReady);
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x0008DA83 File Offset: 0x0008CA83
		public override IDisposable Impersonate()
		{
			return new ApplicationImpersonationContext();
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x0008DA8A File Offset: 0x0008CA8A
		public override bool PrefetchAll(string configPath, string streamName)
		{
			return !WebConfigurationHost.IsMachineConfigPath(configPath);
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x0008DA98 File Offset: 0x0008CA98
		public override bool PrefetchSection(string sectionGroupName, string sectionName)
		{
			return (StringUtil.StringStartsWith(sectionGroupName, "system.web") && (sectionGroupName.Length == "system.web".Length || sectionGroupName["system.web".Length] == '/')) || (string.IsNullOrEmpty(sectionGroupName) && sectionName == "system.codedom");
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x0008DAF2 File Offset: 0x0008CAF2
		public override object CreateDeprecatedConfigContext(string configPath)
		{
			return new HttpConfigurationContext(WebConfigurationHost.VPathFromConfigPath(configPath));
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x0008DB00 File Offset: 0x0008CB00
		public override object CreateConfigurationContext(string configPath, string locationSubPath)
		{
			string path = WebConfigurationHost.VPathFromConfigPath(configPath);
			WebApplicationLevel pathLevel = this.GetPathLevel(configPath);
			return new WebContext(pathLevel, this._appSiteName, VirtualPath.GetVirtualPathString(this._appPath), path, locationSubPath, this._appConfigPath);
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x0008DB3B File Offset: 0x0008CB3B
		public override Type GetConfigType(string typeName, bool throwOnError)
		{
			return BuildManager.GetType(typeName, throwOnError);
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x0008DB44 File Offset: 0x0008CB44
		public override string GetConfigTypeName(Type t)
		{
			return BuildManager.GetNormalizedTypeName(t);
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x0008DB4C File Offset: 0x0008CB4C
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

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x0600207B RID: 8315 RVA: 0x0008DB9C File Offset: 0x0008CB9C
		internal static IInternalConfigConfigurationFactory ConfigurationFactory
		{
			[ReflectionPermission(SecurityAction.Assert, Flags = ReflectionPermissionFlag.MemberAccess)]
			get
			{
				if (WebConfigurationHost.s_configurationFactory == null)
				{
					Type type = Type.GetType("System.Configuration.Internal.InternalConfigConfigurationFactory, System.Configuration, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true);
					WebConfigurationHost.s_configurationFactory = (IInternalConfigConfigurationFactory)Activator.CreateInstance(type, true);
				}
				return WebConfigurationHost.s_configurationFactory;
			}
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x0008DBD4 File Offset: 0x0008CBD4
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

		// Token: 0x04001A91 RID: 6801
		private const string InternalHostTypeName = "System.Configuration.Internal.InternalConfigHost, System.Configuration, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001A92 RID: 6802
		private const string InternalConfigConfigurationFactoryTypeName = "System.Configuration.Internal.InternalConfigConfigurationFactory, System.Configuration, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001A93 RID: 6803
		internal const string MachineConfigName = "machine";

		// Token: 0x04001A94 RID: 6804
		internal const string MachineConfigPath = "machine";

		// Token: 0x04001A95 RID: 6805
		internal const string RootWebConfigName = "webroot";

		// Token: 0x04001A96 RID: 6806
		internal const string RootWebConfigPath = "machine/webroot";

		// Token: 0x04001A97 RID: 6807
		internal const char PathSeparator = '/';

		// Token: 0x04001A98 RID: 6808
		internal const string DefaultSiteID = "1";

		// Token: 0x04001A99 RID: 6809
		private const string SysWebName = "system.web";

		// Token: 0x04001A9A RID: 6810
		private static readonly string RootWebConfigPathAndPathSeparator = "machine/webroot" + '/';

		// Token: 0x04001A9B RID: 6811
		private static readonly string RootWebConfigPathAndDefaultSiteID = WebConfigurationHost.RootWebConfigPathAndPathSeparator + "1";

		// Token: 0x04001A9C RID: 6812
		internal static readonly char[] s_slashSplit = new char[47];

		// Token: 0x04001A9D RID: 6813
		private static IInternalConfigConfigurationFactory s_configurationFactory;

		// Token: 0x04001A9E RID: 6814
		private static string s_defaultSiteName;

		// Token: 0x04001A9F RID: 6815
		private Hashtable _fileChangeCallbacks;

		// Token: 0x04001AA0 RID: 6816
		private IConfigMapPath _configMapPath;

		// Token: 0x04001AA1 RID: 6817
		private IConfigMapPath2 _configMapPath2;

		// Token: 0x04001AA2 RID: 6818
		private VirtualPath _appPath;

		// Token: 0x04001AA3 RID: 6819
		private string _appSiteName;

		// Token: 0x04001AA4 RID: 6820
		private string _appSiteID;

		// Token: 0x04001AA5 RID: 6821
		private string _appConfigPath;
	}
}
