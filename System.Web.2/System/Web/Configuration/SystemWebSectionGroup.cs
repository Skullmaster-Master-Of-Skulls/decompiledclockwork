using System;
using System.Configuration;
using System.Web.Services.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x0200075B RID: 1883
	public sealed class SystemWebSectionGroup : ConfigurationSectionGroup
	{
		// Token: 0x17001A75 RID: 6773
		// (get) Token: 0x06005AB9 RID: 23225 RVA: 0x0013BBC8 File Offset: 0x00139DC8
		[ConfigurationProperty("anonymousIdentification")]
		public AnonymousIdentificationSection AnonymousIdentification
		{
			get
			{
				return (AnonymousIdentificationSection)base.Sections["anonymousIdentification"];
			}
		}

		// Token: 0x17001A76 RID: 6774
		// (get) Token: 0x06005ABA RID: 23226 RVA: 0x0013BBDF File Offset: 0x00139DDF
		[ConfigurationProperty("authentication")]
		public AuthenticationSection Authentication
		{
			get
			{
				return (AuthenticationSection)base.Sections["authentication"];
			}
		}

		// Token: 0x17001A77 RID: 6775
		// (get) Token: 0x06005ABB RID: 23227 RVA: 0x0013BBF6 File Offset: 0x00139DF6
		[ConfigurationProperty("authorization")]
		public AuthorizationSection Authorization
		{
			get
			{
				return (AuthorizationSection)base.Sections["authorization"];
			}
		}

		// Token: 0x17001A78 RID: 6776
		// (get) Token: 0x06005ABC RID: 23228 RVA: 0x0013BC0D File Offset: 0x00139E0D
		[ConfigurationProperty("browserCaps")]
		public DefaultSection BrowserCaps
		{
			get
			{
				return (DefaultSection)base.Sections["browserCaps"];
			}
		}

		// Token: 0x17001A79 RID: 6777
		// (get) Token: 0x06005ABD RID: 23229 RVA: 0x0013BC24 File Offset: 0x00139E24
		[ConfigurationProperty("clientTarget")]
		public ClientTargetSection ClientTarget
		{
			get
			{
				return (ClientTargetSection)base.Sections["clientTarget"];
			}
		}

		// Token: 0x17001A7A RID: 6778
		// (get) Token: 0x06005ABE RID: 23230 RVA: 0x0013BC3B File Offset: 0x00139E3B
		[ConfigurationProperty("compilation")]
		public CompilationSection Compilation
		{
			get
			{
				return (CompilationSection)base.Sections["compilation"];
			}
		}

		// Token: 0x17001A7B RID: 6779
		// (get) Token: 0x06005ABF RID: 23231 RVA: 0x0013BC52 File Offset: 0x00139E52
		[ConfigurationProperty("customErrors")]
		public CustomErrorsSection CustomErrors
		{
			get
			{
				return (CustomErrorsSection)base.Sections["customErrors"];
			}
		}

		// Token: 0x17001A7C RID: 6780
		// (get) Token: 0x06005AC0 RID: 23232 RVA: 0x0013BC69 File Offset: 0x00139E69
		[ConfigurationProperty("deployment")]
		public DeploymentSection Deployment
		{
			get
			{
				return (DeploymentSection)base.Sections["deployment"];
			}
		}

		// Token: 0x17001A7D RID: 6781
		// (get) Token: 0x06005AC1 RID: 23233 RVA: 0x0013BC80 File Offset: 0x00139E80
		[ConfigurationProperty("deviceFilters")]
		public DefaultSection DeviceFilters
		{
			get
			{
				return (DefaultSection)base.Sections["deviceFilters"];
			}
		}

		// Token: 0x17001A7E RID: 6782
		// (get) Token: 0x06005AC2 RID: 23234 RVA: 0x0013BC97 File Offset: 0x00139E97
		[ConfigurationProperty("fullTrustAssemblies")]
		public FullTrustAssembliesSection FullTrustAssemblies
		{
			get
			{
				return (FullTrustAssembliesSection)base.Sections["fullTrustAssemblies"];
			}
		}

		// Token: 0x17001A7F RID: 6783
		// (get) Token: 0x06005AC3 RID: 23235 RVA: 0x0013BCAE File Offset: 0x00139EAE
		[ConfigurationProperty("globalization")]
		public GlobalizationSection Globalization
		{
			get
			{
				return (GlobalizationSection)base.Sections["globalization"];
			}
		}

		// Token: 0x17001A80 RID: 6784
		// (get) Token: 0x06005AC4 RID: 23236 RVA: 0x0013BCC5 File Offset: 0x00139EC5
		[ConfigurationProperty("healthMonitoring")]
		public HealthMonitoringSection HealthMonitoring
		{
			get
			{
				return (HealthMonitoringSection)base.Sections["healthMonitoring"];
			}
		}

		// Token: 0x17001A81 RID: 6785
		// (get) Token: 0x06005AC5 RID: 23237 RVA: 0x0013BCDC File Offset: 0x00139EDC
		[ConfigurationProperty("hostingEnvironment")]
		public HostingEnvironmentSection HostingEnvironment
		{
			get
			{
				return (HostingEnvironmentSection)base.Sections["hostingEnvironment"];
			}
		}

		// Token: 0x17001A82 RID: 6786
		// (get) Token: 0x06005AC6 RID: 23238 RVA: 0x0013BCF3 File Offset: 0x00139EF3
		[ConfigurationProperty("httpCookies")]
		public HttpCookiesSection HttpCookies
		{
			get
			{
				return (HttpCookiesSection)base.Sections["httpCookies"];
			}
		}

		// Token: 0x17001A83 RID: 6787
		// (get) Token: 0x06005AC7 RID: 23239 RVA: 0x0013BD0A File Offset: 0x00139F0A
		[ConfigurationProperty("httpHandlers")]
		public HttpHandlersSection HttpHandlers
		{
			get
			{
				return (HttpHandlersSection)base.Sections["httpHandlers"];
			}
		}

		// Token: 0x17001A84 RID: 6788
		// (get) Token: 0x06005AC8 RID: 23240 RVA: 0x0013BD21 File Offset: 0x00139F21
		[ConfigurationProperty("httpModules")]
		public HttpModulesSection HttpModules
		{
			get
			{
				return (HttpModulesSection)base.Sections["httpModules"];
			}
		}

		// Token: 0x17001A85 RID: 6789
		// (get) Token: 0x06005AC9 RID: 23241 RVA: 0x0013BD38 File Offset: 0x00139F38
		[ConfigurationProperty("httpRuntime")]
		public HttpRuntimeSection HttpRuntime
		{
			get
			{
				return (HttpRuntimeSection)base.Sections["httpRuntime"];
			}
		}

		// Token: 0x17001A86 RID: 6790
		// (get) Token: 0x06005ACA RID: 23242 RVA: 0x0013BD4F File Offset: 0x00139F4F
		[ConfigurationProperty("identity")]
		public IdentitySection Identity
		{
			get
			{
				return (IdentitySection)base.Sections["identity"];
			}
		}

		// Token: 0x17001A87 RID: 6791
		// (get) Token: 0x06005ACB RID: 23243 RVA: 0x0013BD66 File Offset: 0x00139F66
		[ConfigurationProperty("machineKey")]
		public MachineKeySection MachineKey
		{
			get
			{
				return (MachineKeySection)base.Sections["machineKey"];
			}
		}

		// Token: 0x17001A88 RID: 6792
		// (get) Token: 0x06005ACC RID: 23244 RVA: 0x0013BD7D File Offset: 0x00139F7D
		[ConfigurationProperty("membership")]
		public MembershipSection Membership
		{
			get
			{
				return (MembershipSection)base.Sections["membership"];
			}
		}

		// Token: 0x17001A89 RID: 6793
		// (get) Token: 0x06005ACD RID: 23245 RVA: 0x0013BD94 File Offset: 0x00139F94
		[ConfigurationProperty("mobileControls")]
		[Obsolete("System.Web.Mobile.dll is obsolete.")]
		public ConfigurationSection MobileControls
		{
			get
			{
				return base.Sections["mobileControls"];
			}
		}

		// Token: 0x17001A8A RID: 6794
		// (get) Token: 0x06005ACE RID: 23246 RVA: 0x0013BDA6 File Offset: 0x00139FA6
		[ConfigurationProperty("pages")]
		public PagesSection Pages
		{
			get
			{
				return (PagesSection)base.Sections["pages"];
			}
		}

		// Token: 0x17001A8B RID: 6795
		// (get) Token: 0x06005ACF RID: 23247 RVA: 0x0013BDBD File Offset: 0x00139FBD
		[ConfigurationProperty("partialTrustVisibleAssemblies")]
		public PartialTrustVisibleAssembliesSection PartialTrustVisibleAssemblies
		{
			get
			{
				return (PartialTrustVisibleAssembliesSection)base.Sections["partialTrustVisibleAssemblies"];
			}
		}

		// Token: 0x17001A8C RID: 6796
		// (get) Token: 0x06005AD0 RID: 23248 RVA: 0x0013BDD4 File Offset: 0x00139FD4
		[ConfigurationProperty("processModel")]
		public ProcessModelSection ProcessModel
		{
			get
			{
				return (ProcessModelSection)base.Sections["processModel"];
			}
		}

		// Token: 0x17001A8D RID: 6797
		// (get) Token: 0x06005AD1 RID: 23249 RVA: 0x0013BDEB File Offset: 0x00139FEB
		[ConfigurationProperty("profile")]
		public ProfileSection Profile
		{
			get
			{
				return (ProfileSection)base.Sections["profile"];
			}
		}

		// Token: 0x17001A8E RID: 6798
		// (get) Token: 0x06005AD2 RID: 23250 RVA: 0x0013BE02 File Offset: 0x0013A002
		[ConfigurationProperty("protocols")]
		public DefaultSection Protocols
		{
			get
			{
				return (DefaultSection)base.Sections["protocols"];
			}
		}

		// Token: 0x17001A8F RID: 6799
		// (get) Token: 0x06005AD3 RID: 23251 RVA: 0x0013BE19 File Offset: 0x0013A019
		[ConfigurationProperty("roleManager")]
		public RoleManagerSection RoleManager
		{
			get
			{
				return (RoleManagerSection)base.Sections["roleManager"];
			}
		}

		// Token: 0x17001A90 RID: 6800
		// (get) Token: 0x06005AD4 RID: 23252 RVA: 0x0013BE30 File Offset: 0x0013A030
		[ConfigurationProperty("securityPolicy")]
		public SecurityPolicySection SecurityPolicy
		{
			get
			{
				return (SecurityPolicySection)base.Sections["securityPolicy"];
			}
		}

		// Token: 0x17001A91 RID: 6801
		// (get) Token: 0x06005AD5 RID: 23253 RVA: 0x0013BE47 File Offset: 0x0013A047
		[ConfigurationProperty("sessionState")]
		public SessionStateSection SessionState
		{
			get
			{
				return (SessionStateSection)base.Sections["sessionState"];
			}
		}

		// Token: 0x17001A92 RID: 6802
		// (get) Token: 0x06005AD6 RID: 23254 RVA: 0x0013BE5E File Offset: 0x0013A05E
		[ConfigurationProperty("siteMap")]
		public SiteMapSection SiteMap
		{
			get
			{
				return (SiteMapSection)base.Sections["siteMap"];
			}
		}

		// Token: 0x17001A93 RID: 6803
		// (get) Token: 0x06005AD7 RID: 23255 RVA: 0x0013BE75 File Offset: 0x0013A075
		[ConfigurationProperty("trace")]
		public TraceSection Trace
		{
			get
			{
				return (TraceSection)base.Sections["trace"];
			}
		}

		// Token: 0x17001A94 RID: 6804
		// (get) Token: 0x06005AD8 RID: 23256 RVA: 0x0013BE8C File Offset: 0x0013A08C
		[ConfigurationProperty("trust")]
		public TrustSection Trust
		{
			get
			{
				return (TrustSection)base.Sections["trust"];
			}
		}

		// Token: 0x17001A95 RID: 6805
		// (get) Token: 0x06005AD9 RID: 23257 RVA: 0x0013BEA3 File Offset: 0x0013A0A3
		[ConfigurationProperty("urlMappings")]
		public UrlMappingsSection UrlMappings
		{
			get
			{
				return (UrlMappingsSection)base.Sections["urlMappings"];
			}
		}

		// Token: 0x17001A96 RID: 6806
		// (get) Token: 0x06005ADA RID: 23258 RVA: 0x0013BEBA File Offset: 0x0013A0BA
		[ConfigurationProperty("webControls")]
		public WebControlsSection WebControls
		{
			get
			{
				return (WebControlsSection)base.Sections["webControls"];
			}
		}

		// Token: 0x17001A97 RID: 6807
		// (get) Token: 0x06005ADB RID: 23259 RVA: 0x0013BED1 File Offset: 0x0013A0D1
		[ConfigurationProperty("webParts")]
		public WebPartsSection WebParts
		{
			get
			{
				return (WebPartsSection)base.Sections["WebParts"];
			}
		}

		// Token: 0x17001A98 RID: 6808
		// (get) Token: 0x06005ADC RID: 23260 RVA: 0x0013BEE8 File Offset: 0x0013A0E8
		[ConfigurationProperty("webServices")]
		public WebServicesSection WebServices
		{
			get
			{
				return (WebServicesSection)base.Sections["webServices"];
			}
		}

		// Token: 0x17001A99 RID: 6809
		// (get) Token: 0x06005ADD RID: 23261 RVA: 0x0013BEFF File Offset: 0x0013A0FF
		[ConfigurationProperty("xhtmlConformance")]
		public XhtmlConformanceSection XhtmlConformance
		{
			get
			{
				return (XhtmlConformanceSection)base.Sections["xhtmlConformance"];
			}
		}
	}
}
