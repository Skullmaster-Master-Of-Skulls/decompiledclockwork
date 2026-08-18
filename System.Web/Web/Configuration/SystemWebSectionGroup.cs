using System;
using System.Configuration;
using System.Security.Permissions;
using System.Web.Services.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x02000251 RID: 593
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class SystemWebSectionGroup : ConfigurationSectionGroup
	{
		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x0008AE34 File Offset: 0x00089E34
		[ConfigurationProperty("anonymousIdentification")]
		public AnonymousIdentificationSection AnonymousIdentification
		{
			get
			{
				return (AnonymousIdentificationSection)base.Sections["anonymousIdentification"];
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001F4D RID: 8013 RVA: 0x0008AE4B File Offset: 0x00089E4B
		[ConfigurationProperty("authentication")]
		public AuthenticationSection Authentication
		{
			get
			{
				return (AuthenticationSection)base.Sections["authentication"];
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001F4E RID: 8014 RVA: 0x0008AE62 File Offset: 0x00089E62
		[ConfigurationProperty("authorization")]
		public AuthorizationSection Authorization
		{
			get
			{
				return (AuthorizationSection)base.Sections["authorization"];
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001F4F RID: 8015 RVA: 0x0008AE79 File Offset: 0x00089E79
		[ConfigurationProperty("browserCaps")]
		public DefaultSection BrowserCaps
		{
			get
			{
				return (DefaultSection)base.Sections["browserCaps"];
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001F50 RID: 8016 RVA: 0x0008AE90 File Offset: 0x00089E90
		[ConfigurationProperty("clientTarget")]
		public ClientTargetSection ClientTarget
		{
			get
			{
				return (ClientTargetSection)base.Sections["clientTarget"];
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001F51 RID: 8017 RVA: 0x0008AEA7 File Offset: 0x00089EA7
		[ConfigurationProperty("compilation")]
		public CompilationSection Compilation
		{
			get
			{
				return (CompilationSection)base.Sections["compilation"];
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001F52 RID: 8018 RVA: 0x0008AEBE File Offset: 0x00089EBE
		[ConfigurationProperty("customErrors")]
		public CustomErrorsSection CustomErrors
		{
			get
			{
				return (CustomErrorsSection)base.Sections["customErrors"];
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001F53 RID: 8019 RVA: 0x0008AED5 File Offset: 0x00089ED5
		[ConfigurationProperty("deployment")]
		public DeploymentSection Deployment
		{
			get
			{
				return (DeploymentSection)base.Sections["deployment"];
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001F54 RID: 8020 RVA: 0x0008AEEC File Offset: 0x00089EEC
		[ConfigurationProperty("deviceFilters")]
		public DefaultSection DeviceFilters
		{
			get
			{
				return (DefaultSection)base.Sections["deviceFilters"];
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001F55 RID: 8021 RVA: 0x0008AF03 File Offset: 0x00089F03
		[ConfigurationProperty("globalization")]
		public GlobalizationSection Globalization
		{
			get
			{
				return (GlobalizationSection)base.Sections["globalization"];
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001F56 RID: 8022 RVA: 0x0008AF1A File Offset: 0x00089F1A
		[ConfigurationProperty("healthMonitoring")]
		public HealthMonitoringSection HealthMonitoring
		{
			get
			{
				return (HealthMonitoringSection)base.Sections["healthMonitoring"];
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001F57 RID: 8023 RVA: 0x0008AF31 File Offset: 0x00089F31
		[ConfigurationProperty("hostingEnvironment")]
		public HostingEnvironmentSection HostingEnvironment
		{
			get
			{
				return (HostingEnvironmentSection)base.Sections["hostingEnvironment"];
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001F58 RID: 8024 RVA: 0x0008AF48 File Offset: 0x00089F48
		[ConfigurationProperty("httpCookies")]
		public HttpCookiesSection HttpCookies
		{
			get
			{
				return (HttpCookiesSection)base.Sections["httpCookies"];
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001F59 RID: 8025 RVA: 0x0008AF5F File Offset: 0x00089F5F
		[ConfigurationProperty("httpHandlers")]
		public HttpHandlersSection HttpHandlers
		{
			get
			{
				return (HttpHandlersSection)base.Sections["httpHandlers"];
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001F5A RID: 8026 RVA: 0x0008AF76 File Offset: 0x00089F76
		[ConfigurationProperty("httpModules")]
		public HttpModulesSection HttpModules
		{
			get
			{
				return (HttpModulesSection)base.Sections["httpModules"];
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001F5B RID: 8027 RVA: 0x0008AF8D File Offset: 0x00089F8D
		[ConfigurationProperty("httpRuntime")]
		public HttpRuntimeSection HttpRuntime
		{
			get
			{
				return (HttpRuntimeSection)base.Sections["httpRuntime"];
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001F5C RID: 8028 RVA: 0x0008AFA4 File Offset: 0x00089FA4
		[ConfigurationProperty("identity")]
		public IdentitySection Identity
		{
			get
			{
				return (IdentitySection)base.Sections["identity"];
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001F5D RID: 8029 RVA: 0x0008AFBB File Offset: 0x00089FBB
		[ConfigurationProperty("machineKey")]
		public MachineKeySection MachineKey
		{
			get
			{
				return (MachineKeySection)base.Sections["machineKey"];
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001F5E RID: 8030 RVA: 0x0008AFD2 File Offset: 0x00089FD2
		[ConfigurationProperty("membership")]
		public MembershipSection Membership
		{
			get
			{
				return (MembershipSection)base.Sections["membership"];
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001F5F RID: 8031 RVA: 0x0008AFE9 File Offset: 0x00089FE9
		[ConfigurationProperty("mobileControls")]
		public ConfigurationSection MobileControls
		{
			get
			{
				return base.Sections["mobileControls"];
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001F60 RID: 8032 RVA: 0x0008AFFB File Offset: 0x00089FFB
		[ConfigurationProperty("pages")]
		public PagesSection Pages
		{
			get
			{
				return (PagesSection)base.Sections["pages"];
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001F61 RID: 8033 RVA: 0x0008B012 File Offset: 0x0008A012
		[ConfigurationProperty("processModel")]
		public ProcessModelSection ProcessModel
		{
			get
			{
				return (ProcessModelSection)base.Sections["processModel"];
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001F62 RID: 8034 RVA: 0x0008B029 File Offset: 0x0008A029
		[ConfigurationProperty("profile")]
		public ProfileSection Profile
		{
			get
			{
				return (ProfileSection)base.Sections["profile"];
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001F63 RID: 8035 RVA: 0x0008B040 File Offset: 0x0008A040
		[ConfigurationProperty("protocols")]
		public DefaultSection Protocols
		{
			get
			{
				return (DefaultSection)base.Sections["protocols"];
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001F64 RID: 8036 RVA: 0x0008B057 File Offset: 0x0008A057
		[ConfigurationProperty("roleManager")]
		public RoleManagerSection RoleManager
		{
			get
			{
				return (RoleManagerSection)base.Sections["roleManager"];
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001F65 RID: 8037 RVA: 0x0008B06E File Offset: 0x0008A06E
		[ConfigurationProperty("securityPolicy")]
		public SecurityPolicySection SecurityPolicy
		{
			get
			{
				return (SecurityPolicySection)base.Sections["securityPolicy"];
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001F66 RID: 8038 RVA: 0x0008B085 File Offset: 0x0008A085
		[ConfigurationProperty("sessionState")]
		public SessionStateSection SessionState
		{
			get
			{
				return (SessionStateSection)base.Sections["sessionState"];
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001F67 RID: 8039 RVA: 0x0008B09C File Offset: 0x0008A09C
		[ConfigurationProperty("siteMap")]
		public SiteMapSection SiteMap
		{
			get
			{
				return (SiteMapSection)base.Sections["siteMap"];
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001F68 RID: 8040 RVA: 0x0008B0B3 File Offset: 0x0008A0B3
		[ConfigurationProperty("trace")]
		public TraceSection Trace
		{
			get
			{
				return (TraceSection)base.Sections["trace"];
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001F69 RID: 8041 RVA: 0x0008B0CA File Offset: 0x0008A0CA
		[ConfigurationProperty("trust")]
		public TrustSection Trust
		{
			get
			{
				return (TrustSection)base.Sections["trust"];
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001F6A RID: 8042 RVA: 0x0008B0E1 File Offset: 0x0008A0E1
		[ConfigurationProperty("urlMappings")]
		public UrlMappingsSection UrlMappings
		{
			get
			{
				return (UrlMappingsSection)base.Sections["urlMappings"];
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001F6B RID: 8043 RVA: 0x0008B0F8 File Offset: 0x0008A0F8
		[ConfigurationProperty("webControls")]
		public WebControlsSection WebControls
		{
			get
			{
				return (WebControlsSection)base.Sections["webControls"];
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001F6C RID: 8044 RVA: 0x0008B10F File Offset: 0x0008A10F
		[ConfigurationProperty("webParts")]
		public WebPartsSection WebParts
		{
			get
			{
				return (WebPartsSection)base.Sections["WebParts"];
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001F6D RID: 8045 RVA: 0x0008B126 File Offset: 0x0008A126
		[ConfigurationProperty("webServices")]
		public WebServicesSection WebServices
		{
			get
			{
				return (WebServicesSection)base.Sections["webServices"];
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001F6E RID: 8046 RVA: 0x0008B13D File Offset: 0x0008A13D
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
