using System;
using System.Collections;
using System.Configuration;
using System.Configuration.Internal;
using System.Net.Configuration;
using System.Security.Permissions;
using System.Web.Hosting;

namespace System.Web.Configuration
{
	// Token: 0x0200074D RID: 1869
	internal class RuntimeConfig
	{
		// Token: 0x060059F4 RID: 23028 RVA: 0x00139E8C File Offset: 0x0013808C
		internal static RuntimeConfig GetConfig()
		{
			if (!HttpConfigurationSystem.UseHttpConfigurationSystem)
			{
				return RuntimeConfig.GetClientRuntimeConfig();
			}
			HttpContext httpContext = HttpContext.Current;
			if (httpContext != null)
			{
				return RuntimeConfig.GetConfig(httpContext);
			}
			return RuntimeConfig.GetAppConfig();
		}

		// Token: 0x060059F5 RID: 23029 RVA: 0x00139EBB File Offset: 0x001380BB
		internal static RuntimeConfig GetConfig(HttpContext context)
		{
			if (!HttpConfigurationSystem.UseHttpConfigurationSystem)
			{
				return RuntimeConfig.GetClientRuntimeConfig();
			}
			return context.GetRuntimeConfig();
		}

		// Token: 0x060059F6 RID: 23030 RVA: 0x00139ED0 File Offset: 0x001380D0
		internal static RuntimeConfig GetConfig(HttpContext context, VirtualPath path)
		{
			if (!HttpConfigurationSystem.UseHttpConfigurationSystem)
			{
				return RuntimeConfig.GetClientRuntimeConfig();
			}
			return context.GetRuntimeConfig(path);
		}

		// Token: 0x060059F7 RID: 23031 RVA: 0x00139EE6 File Offset: 0x001380E6
		internal static RuntimeConfig GetConfig(string path)
		{
			return RuntimeConfig.GetConfig(VirtualPath.CreateNonRelativeAllowNull(path));
		}

		// Token: 0x060059F8 RID: 23032 RVA: 0x00139EF3 File Offset: 0x001380F3
		internal static RuntimeConfig GetConfig(VirtualPath path)
		{
			if (!HttpConfigurationSystem.UseHttpConfigurationSystem)
			{
				return RuntimeConfig.GetClientRuntimeConfig();
			}
			return CachedPathData.GetVirtualPathData(path, true).RuntimeConfig;
		}

		// Token: 0x060059F9 RID: 23033 RVA: 0x00139F0E File Offset: 0x0013810E
		internal static RuntimeConfig GetAppConfig()
		{
			if (!HttpConfigurationSystem.UseHttpConfigurationSystem)
			{
				return RuntimeConfig.GetClientRuntimeConfig();
			}
			return CachedPathData.GetApplicationPathData().RuntimeConfig;
		}

		// Token: 0x060059FA RID: 23034 RVA: 0x00139F27 File Offset: 0x00138127
		internal static RuntimeConfig GetRootWebConfig()
		{
			if (!HttpConfigurationSystem.UseHttpConfigurationSystem)
			{
				return RuntimeConfig.GetClientRuntimeConfig();
			}
			return CachedPathData.GetRootWebPathData().RuntimeConfig;
		}

		// Token: 0x060059FB RID: 23035 RVA: 0x00139F40 File Offset: 0x00138140
		internal static RuntimeConfig GetMachineConfig()
		{
			if (!HttpConfigurationSystem.UseHttpConfigurationSystem)
			{
				return RuntimeConfig.GetClientRuntimeConfig();
			}
			return CachedPathData.GetMachinePathData().RuntimeConfig;
		}

		// Token: 0x060059FC RID: 23036 RVA: 0x00139F5C File Offset: 0x0013815C
		internal static RuntimeConfig GetLKGConfig(HttpContext context)
		{
			RuntimeConfig runtimeConfig = null;
			bool flag = false;
			try
			{
				runtimeConfig = RuntimeConfig.GetConfig(context);
				flag = true;
			}
			catch
			{
			}
			if (!flag)
			{
				runtimeConfig = RuntimeConfig.GetLKGRuntimeConfig(context.Request.FilePathObject);
			}
			return runtimeConfig.RuntimeConfigLKG;
		}

		// Token: 0x060059FD RID: 23037 RVA: 0x00139FA8 File Offset: 0x001381A8
		internal static RuntimeConfig GetAppLKGConfig()
		{
			RuntimeConfig runtimeConfig = null;
			bool flag = false;
			try
			{
				runtimeConfig = RuntimeConfig.GetAppConfig();
				flag = true;
			}
			catch
			{
			}
			if (!flag)
			{
				runtimeConfig = RuntimeConfig.GetLKGRuntimeConfig(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPathObject);
			}
			return runtimeConfig.RuntimeConfigLKG;
		}

		// Token: 0x17001A0C RID: 6668
		// (get) Token: 0x060059FE RID: 23038 RVA: 0x00139FEC File Offset: 0x001381EC
		internal ConnectionStringsSection ConnectionStrings
		{
			get
			{
				return (ConnectionStringsSection)this.GetSection("connectionStrings", typeof(ConnectionStringsSection), RuntimeConfig.ResultsIndex.ConnectionStrings);
			}
		}

		// Token: 0x17001A0D RID: 6669
		// (get) Token: 0x060059FF RID: 23039 RVA: 0x0013A009 File Offset: 0x00138209
		internal SmtpSection Smtp
		{
			get
			{
				return (SmtpSection)this.GetSection("system.net/mailSettings/smtp", typeof(SmtpSection));
			}
		}

		// Token: 0x17001A0E RID: 6670
		// (get) Token: 0x06005A00 RID: 23040 RVA: 0x0013A025 File Offset: 0x00138225
		internal AnonymousIdentificationSection AnonymousIdentification
		{
			get
			{
				return (AnonymousIdentificationSection)this.GetSection("system.web/anonymousIdentification", typeof(AnonymousIdentificationSection));
			}
		}

		// Token: 0x17001A0F RID: 6671
		// (get) Token: 0x06005A01 RID: 23041 RVA: 0x0013A041 File Offset: 0x00138241
		internal ProtocolsSection Protocols
		{
			get
			{
				return (ProtocolsSection)this.GetSection("system.web/protocols", typeof(ProtocolsSection));
			}
		}

		// Token: 0x17001A10 RID: 6672
		// (get) Token: 0x06005A02 RID: 23042 RVA: 0x0013A05D File Offset: 0x0013825D
		internal AuthenticationSection Authentication
		{
			get
			{
				return (AuthenticationSection)this.GetSection("system.web/authentication", typeof(AuthenticationSection), RuntimeConfig.ResultsIndex.Authentication);
			}
		}

		// Token: 0x17001A11 RID: 6673
		// (get) Token: 0x06005A03 RID: 23043 RVA: 0x0013A07A File Offset: 0x0013827A
		internal AuthorizationSection Authorization
		{
			get
			{
				return (AuthorizationSection)this.GetSection("system.web/authorization", typeof(AuthorizationSection), RuntimeConfig.ResultsIndex.Authorization);
			}
		}

		// Token: 0x17001A12 RID: 6674
		// (get) Token: 0x06005A04 RID: 23044 RVA: 0x0013A097 File Offset: 0x00138297
		internal HttpCapabilitiesDefaultProvider BrowserCaps
		{
			get
			{
				return (HttpCapabilitiesDefaultProvider)this.GetHandlerSection("system.web/browserCaps", typeof(HttpCapabilitiesDefaultProvider), RuntimeConfig.ResultsIndex.BrowserCaps);
			}
		}

		// Token: 0x17001A13 RID: 6675
		// (get) Token: 0x06005A05 RID: 23045 RVA: 0x0013A0B4 File Offset: 0x001382B4
		internal ClientTargetSection ClientTarget
		{
			get
			{
				return (ClientTargetSection)this.GetSection("system.web/clientTarget", typeof(ClientTargetSection), RuntimeConfig.ResultsIndex.ClientTarget);
			}
		}

		// Token: 0x17001A14 RID: 6676
		// (get) Token: 0x06005A06 RID: 23046 RVA: 0x0013A0D1 File Offset: 0x001382D1
		internal CompilationSection Compilation
		{
			get
			{
				return (CompilationSection)this.GetSection("system.web/compilation", typeof(CompilationSection), RuntimeConfig.ResultsIndex.Compilation);
			}
		}

		// Token: 0x17001A15 RID: 6677
		// (get) Token: 0x06005A07 RID: 23047 RVA: 0x0013A0EE File Offset: 0x001382EE
		internal CustomErrorsSection CustomErrors
		{
			get
			{
				return (CustomErrorsSection)this.GetSection("system.web/customErrors", typeof(CustomErrorsSection));
			}
		}

		// Token: 0x17001A16 RID: 6678
		// (get) Token: 0x06005A08 RID: 23048 RVA: 0x0013A10A File Offset: 0x0013830A
		internal GlobalizationSection Globalization
		{
			get
			{
				return (GlobalizationSection)this.GetSection("system.web/globalization", typeof(GlobalizationSection), RuntimeConfig.ResultsIndex.Globalization);
			}
		}

		// Token: 0x17001A17 RID: 6679
		// (get) Token: 0x06005A09 RID: 23049 RVA: 0x0013A127 File Offset: 0x00138327
		internal DeploymentSection Deployment
		{
			get
			{
				return (DeploymentSection)this.GetSection("system.web/deployment", typeof(DeploymentSection));
			}
		}

		// Token: 0x17001A18 RID: 6680
		// (get) Token: 0x06005A0A RID: 23050 RVA: 0x0013A143 File Offset: 0x00138343
		internal FullTrustAssembliesSection FullTrustAssemblies
		{
			get
			{
				return (FullTrustAssembliesSection)this.GetSection("system.web/fullTrustAssemblies", typeof(FullTrustAssembliesSection));
			}
		}

		// Token: 0x17001A19 RID: 6681
		// (get) Token: 0x06005A0B RID: 23051 RVA: 0x0013A15F File Offset: 0x0013835F
		internal HealthMonitoringSection HealthMonitoring
		{
			get
			{
				return (HealthMonitoringSection)this.GetSection("system.web/healthMonitoring", typeof(HealthMonitoringSection));
			}
		}

		// Token: 0x17001A1A RID: 6682
		// (get) Token: 0x06005A0C RID: 23052 RVA: 0x0013A17B File Offset: 0x0013837B
		internal HostingEnvironmentSection HostingEnvironment
		{
			get
			{
				return (HostingEnvironmentSection)this.GetSection("system.web/hostingEnvironment", typeof(HostingEnvironmentSection));
			}
		}

		// Token: 0x17001A1B RID: 6683
		// (get) Token: 0x06005A0D RID: 23053 RVA: 0x0013A197 File Offset: 0x00138397
		internal HttpCookiesSection HttpCookies
		{
			get
			{
				return (HttpCookiesSection)this.GetSection("system.web/httpCookies", typeof(HttpCookiesSection), RuntimeConfig.ResultsIndex.HttpCookies);
			}
		}

		// Token: 0x17001A1C RID: 6684
		// (get) Token: 0x06005A0E RID: 23054 RVA: 0x0013A1B4 File Offset: 0x001383B4
		internal HttpHandlersSection HttpHandlers
		{
			get
			{
				return (HttpHandlersSection)this.GetSection("system.web/httpHandlers", typeof(HttpHandlersSection), RuntimeConfig.ResultsIndex.HttpHandlers);
			}
		}

		// Token: 0x17001A1D RID: 6685
		// (get) Token: 0x06005A0F RID: 23055 RVA: 0x0013A1D2 File Offset: 0x001383D2
		internal HttpModulesSection HttpModules
		{
			get
			{
				return (HttpModulesSection)this.GetSection("system.web/httpModules", typeof(HttpModulesSection), RuntimeConfig.ResultsIndex.HttpModules);
			}
		}

		// Token: 0x17001A1E RID: 6686
		// (get) Token: 0x06005A10 RID: 23056 RVA: 0x0013A1F0 File Offset: 0x001383F0
		internal HttpRuntimeSection HttpRuntime
		{
			get
			{
				return (HttpRuntimeSection)this.GetSection("system.web/httpRuntime", typeof(HttpRuntimeSection), RuntimeConfig.ResultsIndex.HttpRuntime);
			}
		}

		// Token: 0x17001A1F RID: 6687
		// (get) Token: 0x06005A11 RID: 23057 RVA: 0x0013A20E File Offset: 0x0013840E
		internal IdentitySection Identity
		{
			get
			{
				return (IdentitySection)this.GetSection("system.web/identity", typeof(IdentitySection), RuntimeConfig.ResultsIndex.Identity);
			}
		}

		// Token: 0x17001A20 RID: 6688
		// (get) Token: 0x06005A12 RID: 23058 RVA: 0x0013A22C File Offset: 0x0013842C
		internal MachineKeySection MachineKey
		{
			get
			{
				return (MachineKeySection)this.GetSection("system.web/machineKey", typeof(MachineKeySection), RuntimeConfig.ResultsIndex.MachineKey);
			}
		}

		// Token: 0x17001A21 RID: 6689
		// (get) Token: 0x06005A13 RID: 23059 RVA: 0x0013A24A File Offset: 0x0013844A
		internal MembershipSection Membership
		{
			get
			{
				return (MembershipSection)this.GetSection("system.web/membership", typeof(MembershipSection), RuntimeConfig.ResultsIndex.Membership);
			}
		}

		// Token: 0x17001A22 RID: 6690
		// (get) Token: 0x06005A14 RID: 23060 RVA: 0x0013A268 File Offset: 0x00138468
		internal PagesSection Pages
		{
			get
			{
				return (PagesSection)this.GetSection("system.web/pages", typeof(PagesSection), RuntimeConfig.ResultsIndex.Pages);
			}
		}

		// Token: 0x17001A23 RID: 6691
		// (get) Token: 0x06005A15 RID: 23061 RVA: 0x0013A286 File Offset: 0x00138486
		internal PartialTrustVisibleAssembliesSection PartialTrustVisibleAssemblies
		{
			get
			{
				return (PartialTrustVisibleAssembliesSection)this.GetSection("system.web/partialTrustVisibleAssemblies", typeof(PartialTrustVisibleAssembliesSection));
			}
		}

		// Token: 0x17001A24 RID: 6692
		// (get) Token: 0x06005A16 RID: 23062 RVA: 0x0013A2A2 File Offset: 0x001384A2
		internal ProcessModelSection ProcessModel
		{
			get
			{
				return (ProcessModelSection)this.GetSection("system.web/processModel", typeof(ProcessModelSection));
			}
		}

		// Token: 0x17001A25 RID: 6693
		// (get) Token: 0x06005A17 RID: 23063 RVA: 0x0013A2BE File Offset: 0x001384BE
		internal ProfileSection Profile
		{
			get
			{
				return (ProfileSection)this.GetSection("system.web/profile", typeof(ProfileSection), RuntimeConfig.ResultsIndex.Profile);
			}
		}

		// Token: 0x17001A26 RID: 6694
		// (get) Token: 0x06005A18 RID: 23064 RVA: 0x0013A2DC File Offset: 0x001384DC
		internal RoleManagerSection RoleManager
		{
			get
			{
				return (RoleManagerSection)this.GetSection("system.web/roleManager", typeof(RoleManagerSection));
			}
		}

		// Token: 0x17001A27 RID: 6695
		// (get) Token: 0x06005A19 RID: 23065 RVA: 0x0013A2F8 File Offset: 0x001384F8
		internal SecurityPolicySection SecurityPolicy
		{
			get
			{
				return (SecurityPolicySection)this.GetSection("system.web/securityPolicy", typeof(SecurityPolicySection));
			}
		}

		// Token: 0x17001A28 RID: 6696
		// (get) Token: 0x06005A1A RID: 23066 RVA: 0x0013A314 File Offset: 0x00138514
		internal SessionPageStateSection SessionPageState
		{
			get
			{
				return (SessionPageStateSection)this.GetSection("system.web/sessionPageState", typeof(SessionPageStateSection), RuntimeConfig.ResultsIndex.SessionPageState);
			}
		}

		// Token: 0x17001A29 RID: 6697
		// (get) Token: 0x06005A1B RID: 23067 RVA: 0x0013A332 File Offset: 0x00138532
		internal SessionStateSection SessionState
		{
			get
			{
				return (SessionStateSection)this.GetSection("system.web/sessionState", typeof(SessionStateSection));
			}
		}

		// Token: 0x17001A2A RID: 6698
		// (get) Token: 0x06005A1C RID: 23068 RVA: 0x0013A34E File Offset: 0x0013854E
		internal SiteMapSection SiteMap
		{
			get
			{
				return (SiteMapSection)this.GetSection("system.web/siteMap", typeof(SiteMapSection));
			}
		}

		// Token: 0x17001A2B RID: 6699
		// (get) Token: 0x06005A1D RID: 23069 RVA: 0x0013A36A File Offset: 0x0013856A
		internal TraceSection Trace
		{
			get
			{
				return (TraceSection)this.GetSection("system.web/trace", typeof(TraceSection));
			}
		}

		// Token: 0x17001A2C RID: 6700
		// (get) Token: 0x06005A1E RID: 23070 RVA: 0x0013A386 File Offset: 0x00138586
		internal TrustSection Trust
		{
			get
			{
				return (TrustSection)this.GetSection("system.web/trust", typeof(TrustSection));
			}
		}

		// Token: 0x17001A2D RID: 6701
		// (get) Token: 0x06005A1F RID: 23071 RVA: 0x0013A3A2 File Offset: 0x001385A2
		internal UrlMappingsSection UrlMappings
		{
			get
			{
				return (UrlMappingsSection)this.GetSection("system.web/urlMappings", typeof(UrlMappingsSection), RuntimeConfig.ResultsIndex.UrlMappings);
			}
		}

		// Token: 0x17001A2E RID: 6702
		// (get) Token: 0x06005A20 RID: 23072 RVA: 0x0013A3C0 File Offset: 0x001385C0
		internal Hashtable WebControls
		{
			get
			{
				return (Hashtable)this.GetSection("system.web/webControls", typeof(Hashtable), RuntimeConfig.ResultsIndex.WebControls);
			}
		}

		// Token: 0x17001A2F RID: 6703
		// (get) Token: 0x06005A21 RID: 23073 RVA: 0x0013A3DE File Offset: 0x001385DE
		internal WebPartsSection WebParts
		{
			get
			{
				return (WebPartsSection)this.GetSection("system.web/webParts", typeof(WebPartsSection), RuntimeConfig.ResultsIndex.WebParts);
			}
		}

		// Token: 0x17001A30 RID: 6704
		// (get) Token: 0x06005A22 RID: 23074 RVA: 0x0013A3FC File Offset: 0x001385FC
		internal XhtmlConformanceSection XhtmlConformance
		{
			get
			{
				return (XhtmlConformanceSection)this.GetSection("system.web/xhtmlConformance", typeof(XhtmlConformanceSection), RuntimeConfig.ResultsIndex.XhtmlConformance);
			}
		}

		// Token: 0x17001A31 RID: 6705
		// (get) Token: 0x06005A23 RID: 23075 RVA: 0x0013A41A File Offset: 0x0013861A
		internal CacheSection Cache
		{
			get
			{
				return (CacheSection)this.GetSection("system.web/caching/cache", typeof(CacheSection));
			}
		}

		// Token: 0x17001A32 RID: 6706
		// (get) Token: 0x06005A24 RID: 23076 RVA: 0x0013A436 File Offset: 0x00138636
		internal OutputCacheSection OutputCache
		{
			get
			{
				return (OutputCacheSection)this.GetSection("system.web/caching/outputCache", typeof(OutputCacheSection), RuntimeConfig.ResultsIndex.OutputCache);
			}
		}

		// Token: 0x17001A33 RID: 6707
		// (get) Token: 0x06005A25 RID: 23077 RVA: 0x0013A454 File Offset: 0x00138654
		internal OutputCacheSettingsSection OutputCacheSettings
		{
			get
			{
				return (OutputCacheSettingsSection)this.GetSection("system.web/caching/outputCacheSettings", typeof(OutputCacheSettingsSection), RuntimeConfig.ResultsIndex.OutputCacheSettings);
			}
		}

		// Token: 0x17001A34 RID: 6708
		// (get) Token: 0x06005A26 RID: 23078 RVA: 0x0013A472 File Offset: 0x00138672
		internal SqlCacheDependencySection SqlCacheDependency
		{
			get
			{
				return (SqlCacheDependencySection)this.GetSection("system.web/caching/sqlCacheDependency", typeof(SqlCacheDependencySection));
			}
		}

		// Token: 0x17001A35 RID: 6709
		// (get) Token: 0x06005A27 RID: 23079 RVA: 0x0013A48E File Offset: 0x0013868E
		// (set) Token: 0x06005A28 RID: 23080 RVA: 0x0013A496 File Offset: 0x00138696
		internal bool IgnoreConfigErrors { get; set; }

		// Token: 0x06005A29 RID: 23081 RVA: 0x0013A49F File Offset: 0x0013869F
		static RuntimeConfig()
		{
			RuntimeConfig.GetErrorRuntimeConfig();
		}

		// Token: 0x06005A2A RID: 23082 RVA: 0x0013A4B1 File Offset: 0x001386B1
		internal RuntimeConfig(IInternalConfigRecord configRecord) : this(configRecord, false)
		{
		}

		// Token: 0x06005A2B RID: 23083 RVA: 0x0013A4BC File Offset: 0x001386BC
		protected RuntimeConfig(IInternalConfigRecord configRecord, bool permitNull)
		{
			this._configRecord = configRecord;
			this._permitNull = permitNull;
			this._results = new object[24];
			for (int i = 0; i < this._results.Length; i++)
			{
				this._results[i] = RuntimeConfig.s_unevaluatedResult;
			}
		}

		// Token: 0x17001A36 RID: 6710
		// (get) Token: 0x06005A2C RID: 23084 RVA: 0x0013A50C File Offset: 0x0013870C
		private RuntimeConfigLKG RuntimeConfigLKG
		{
			get
			{
				if (this._runtimeConfigLKG == null)
				{
					lock (this)
					{
						if (this._runtimeConfigLKG == null)
						{
							this._runtimeConfigLKG = new RuntimeConfigLKG(this._configRecord);
						}
					}
				}
				return this._runtimeConfigLKG;
			}
		}

		// Token: 0x17001A37 RID: 6711
		// (get) Token: 0x06005A2D RID: 23085 RVA: 0x0013A568 File Offset: 0x00138768
		internal IInternalConfigRecord ConfigRecord
		{
			get
			{
				return this._configRecord;
			}
		}

		// Token: 0x06005A2E RID: 23086 RVA: 0x0013A570 File Offset: 0x00138770
		private static RuntimeConfig GetClientRuntimeConfig()
		{
			if (RuntimeConfig.s_clientRuntimeConfig == null)
			{
				RuntimeConfig.s_clientRuntimeConfig = new ClientRuntimeConfig();
			}
			return RuntimeConfig.s_clientRuntimeConfig;
		}

		// Token: 0x06005A2F RID: 23087 RVA: 0x0013A588 File Offset: 0x00138788
		private static RuntimeConfig GetNullRuntimeConfig()
		{
			if (RuntimeConfig.s_nullRuntimeConfig == null)
			{
				RuntimeConfig.s_nullRuntimeConfig = new NullRuntimeConfig();
			}
			return RuntimeConfig.s_nullRuntimeConfig;
		}

		// Token: 0x06005A30 RID: 23088 RVA: 0x0013A5A0 File Offset: 0x001387A0
		internal static RuntimeConfig GetErrorRuntimeConfig()
		{
			if (RuntimeConfig.s_errorRuntimeConfig == null)
			{
				RuntimeConfig.s_errorRuntimeConfig = new ErrorRuntimeConfig();
			}
			return RuntimeConfig.s_errorRuntimeConfig;
		}

		// Token: 0x06005A31 RID: 23089 RVA: 0x0013A5B8 File Offset: 0x001387B8
		[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
		protected virtual object GetSectionObject(string sectionName)
		{
			return this._configRecord.GetSection(sectionName);
		}

		// Token: 0x06005A32 RID: 23090 RVA: 0x0013A5C8 File Offset: 0x001387C8
		private object GetHandlerSection(string sectionName, Type type, RuntimeConfig.ResultsIndex index)
		{
			object obj = this._results[(int)index];
			if (obj != RuntimeConfig.s_unevaluatedResult)
			{
				return obj;
			}
			obj = this.GetSectionObject(sectionName);
			if (obj != null && obj.GetType() != type)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_unable_to_get_section", new object[]
				{
					sectionName
				}));
			}
			if (index != RuntimeConfig.ResultsIndex.UNUSED)
			{
				this._results[(int)index] = obj;
			}
			return obj;
		}

		// Token: 0x06005A33 RID: 23091 RVA: 0x0013A628 File Offset: 0x00138828
		private object GetSection(string sectionName, Type type)
		{
			return this.GetSection(sectionName, type, RuntimeConfig.ResultsIndex.UNUSED);
		}

		// Token: 0x06005A34 RID: 23092 RVA: 0x0013A634 File Offset: 0x00138834
		private object GetSection(string sectionName, Type type, RuntimeConfig.ResultsIndex index)
		{
			object obj = this._results[(int)index];
			if (obj != RuntimeConfig.s_unevaluatedResult)
			{
				return obj;
			}
			try
			{
				obj = this.GetSectionObject(sectionName);
			}
			catch (ConfigurationErrorsException obj2) when (this.IgnoreConfigErrors)
			{
				return null;
			}
			if (obj == null)
			{
				if (!this._permitNull)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_unable_to_get_section", new object[]
					{
						sectionName
					}));
				}
			}
			else if (obj.GetType() != type)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_unable_to_get_section", new object[]
				{
					sectionName
				}));
			}
			if (index != RuntimeConfig.ResultsIndex.UNUSED)
			{
				this._results[(int)index] = obj;
			}
			return obj;
		}

		// Token: 0x06005A35 RID: 23093 RVA: 0x0013A6E8 File Offset: 0x001388E8
		private static RuntimeConfig GetLKGRuntimeConfig(VirtualPath path)
		{
			try
			{
				path = path.Parent;
				goto IL_29;
			}
			catch
			{
				path = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPathObject;
				goto IL_29;
			}
			IL_14:
			try
			{
				return RuntimeConfig.GetConfig(path);
			}
			catch
			{
				path = path.Parent;
			}
			IL_29:
			if (!(path != null))
			{
				try
				{
					return RuntimeConfig.GetRootWebConfig();
				}
				catch
				{
				}
				try
				{
					return RuntimeConfig.GetMachineConfig();
				}
				catch
				{
				}
				return RuntimeConfig.GetNullRuntimeConfig();
			}
			goto IL_14;
		}

		// Token: 0x04002FBE RID: 12222
		private static RuntimeConfig s_clientRuntimeConfig;

		// Token: 0x04002FBF RID: 12223
		private static RuntimeConfig s_nullRuntimeConfig;

		// Token: 0x04002FC0 RID: 12224
		private static RuntimeConfig s_errorRuntimeConfig;

		// Token: 0x04002FC1 RID: 12225
		private static object s_unevaluatedResult = new object();

		// Token: 0x04002FC2 RID: 12226
		private object[] _results;

		// Token: 0x04002FC3 RID: 12227
		private RuntimeConfigLKG _runtimeConfigLKG;

		// Token: 0x04002FC4 RID: 12228
		protected IInternalConfigRecord _configRecord;

		// Token: 0x04002FC5 RID: 12229
		private bool _permitNull;

		// Token: 0x02000A49 RID: 2633
		internal enum ResultsIndex
		{
			// Token: 0x04003B1A RID: 15130
			UNUSED,
			// Token: 0x04003B1B RID: 15131
			Authentication,
			// Token: 0x04003B1C RID: 15132
			Authorization,
			// Token: 0x04003B1D RID: 15133
			BrowserCaps,
			// Token: 0x04003B1E RID: 15134
			ClientTarget,
			// Token: 0x04003B1F RID: 15135
			Compilation,
			// Token: 0x04003B20 RID: 15136
			ConnectionStrings,
			// Token: 0x04003B21 RID: 15137
			Globalization,
			// Token: 0x04003B22 RID: 15138
			HttpCookies,
			// Token: 0x04003B23 RID: 15139
			HttpHandlers,
			// Token: 0x04003B24 RID: 15140
			HttpModules,
			// Token: 0x04003B25 RID: 15141
			HttpRuntime,
			// Token: 0x04003B26 RID: 15142
			Identity,
			// Token: 0x04003B27 RID: 15143
			MachineKey,
			// Token: 0x04003B28 RID: 15144
			Membership,
			// Token: 0x04003B29 RID: 15145
			OutputCache,
			// Token: 0x04003B2A RID: 15146
			OutputCacheSettings,
			// Token: 0x04003B2B RID: 15147
			Pages,
			// Token: 0x04003B2C RID: 15148
			Profile,
			// Token: 0x04003B2D RID: 15149
			SessionPageState,
			// Token: 0x04003B2E RID: 15150
			WebControls,
			// Token: 0x04003B2F RID: 15151
			WebParts,
			// Token: 0x04003B30 RID: 15152
			UrlMappings,
			// Token: 0x04003B31 RID: 15153
			XhtmlConformance,
			// Token: 0x04003B32 RID: 15154
			SIZE
		}
	}
}
