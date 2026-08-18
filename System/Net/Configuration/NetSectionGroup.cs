using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x02000656 RID: 1622
	public sealed class NetSectionGroup : ConfigurationSectionGroup
	{
		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x0600322D RID: 12845 RVA: 0x000D5D5E File Offset: 0x000D4D5E
		[ConfigurationProperty("authenticationModules")]
		public AuthenticationModulesSection AuthenticationModules
		{
			get
			{
				return (AuthenticationModulesSection)base.Sections["authenticationModules"];
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x0600322E RID: 12846 RVA: 0x000D5D75 File Offset: 0x000D4D75
		[ConfigurationProperty("connectionManagement")]
		public ConnectionManagementSection ConnectionManagement
		{
			get
			{
				return (ConnectionManagementSection)base.Sections["connectionManagement"];
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x0600322F RID: 12847 RVA: 0x000D5D8C File Offset: 0x000D4D8C
		[ConfigurationProperty("defaultProxy")]
		public DefaultProxySection DefaultProxy
		{
			get
			{
				return (DefaultProxySection)base.Sections["defaultProxy"];
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06003230 RID: 12848 RVA: 0x000D5DA3 File Offset: 0x000D4DA3
		public MailSettingsSectionGroup MailSettings
		{
			get
			{
				return (MailSettingsSectionGroup)base.SectionGroups["mailSettings"];
			}
		}

		// Token: 0x06003231 RID: 12849 RVA: 0x000D5DBA File Offset: 0x000D4DBA
		public static NetSectionGroup GetSectionGroup(Configuration config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			return config.GetSectionGroup("system.net") as NetSectionGroup;
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06003232 RID: 12850 RVA: 0x000D5DDA File Offset: 0x000D4DDA
		[ConfigurationProperty("requestCaching")]
		public RequestCachingSection RequestCaching
		{
			get
			{
				return (RequestCachingSection)base.Sections["requestCaching"];
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06003233 RID: 12851 RVA: 0x000D5DF1 File Offset: 0x000D4DF1
		[ConfigurationProperty("settings")]
		public SettingsSection Settings
		{
			get
			{
				return (SettingsSection)base.Sections["settings"];
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06003234 RID: 12852 RVA: 0x000D5E08 File Offset: 0x000D4E08
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
