using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x0200033A RID: 826
	public sealed class NetSectionGroup : ConfigurationSectionGroup
	{
		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06001D83 RID: 7555 RVA: 0x0008C072 File Offset: 0x0008A272
		[ConfigurationProperty("authenticationModules")]
		public AuthenticationModulesSection AuthenticationModules
		{
			get
			{
				return (AuthenticationModulesSection)base.Sections["authenticationModules"];
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06001D84 RID: 7556 RVA: 0x0008C089 File Offset: 0x0008A289
		[ConfigurationProperty("connectionManagement")]
		public ConnectionManagementSection ConnectionManagement
		{
			get
			{
				return (ConnectionManagementSection)base.Sections["connectionManagement"];
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06001D85 RID: 7557 RVA: 0x0008C0A0 File Offset: 0x0008A2A0
		[ConfigurationProperty("defaultProxy")]
		public DefaultProxySection DefaultProxy
		{
			get
			{
				return (DefaultProxySection)base.Sections["defaultProxy"];
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06001D86 RID: 7558 RVA: 0x0008C0B7 File Offset: 0x0008A2B7
		public MailSettingsSectionGroup MailSettings
		{
			get
			{
				return (MailSettingsSectionGroup)base.SectionGroups["mailSettings"];
			}
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x0008C0CE File Offset: 0x0008A2CE
		public static NetSectionGroup GetSectionGroup(Configuration config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			return config.GetSectionGroup("system.net") as NetSectionGroup;
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06001D88 RID: 7560 RVA: 0x0008C0EE File Offset: 0x0008A2EE
		[ConfigurationProperty("requestCaching")]
		public RequestCachingSection RequestCaching
		{
			get
			{
				return (RequestCachingSection)base.Sections["requestCaching"];
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001D89 RID: 7561 RVA: 0x0008C105 File Offset: 0x0008A305
		[ConfigurationProperty("settings")]
		public SettingsSection Settings
		{
			get
			{
				return (SettingsSection)base.Sections["settings"];
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001D8A RID: 7562 RVA: 0x0008C11C File Offset: 0x0008A31C
		[ConfigurationProperty("webRequestModules")]
		public WebRequestModulesSection WebRequestModules
		{
			get
			{
				return (WebRequestModulesSection)base.Sections["webRequestModules"];
			}
		}
	}
}
