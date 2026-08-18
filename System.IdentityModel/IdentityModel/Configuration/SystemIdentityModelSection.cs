using System;
using System.Configuration;

namespace System.IdentityModel.Configuration
{
	// Token: 0x020001D3 RID: 467
	public sealed class SystemIdentityModelSection : ConfigurationSection
	{
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x00043F4C File Offset: 0x0004214C
		public static SystemIdentityModelSection Current
		{
			get
			{
				return ConfigurationManager.GetSection("system.identityModel") as SystemIdentityModelSection;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x00043F60 File Offset: 0x00042160
		public static IdentityConfigurationElement DefaultIdentityConfigurationElement
		{
			get
			{
				SystemIdentityModelSection systemIdentityModelSection = SystemIdentityModelSection.Current;
				if (systemIdentityModelSection == null)
				{
					return null;
				}
				return systemIdentityModelSection.IdentityConfigurationElements.GetElement("");
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x00043F88 File Offset: 0x00042188
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public IdentityConfigurationElementCollection IdentityConfigurationElements
		{
			get
			{
				return (IdentityConfigurationElementCollection)base[""];
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x00043F9A File Offset: 0x0004219A
		internal bool IsConfigured
		{
			get
			{
				return this.IdentityConfigurationElements.IsConfigured;
			}
		}

		// Token: 0x04000D93 RID: 3475
		public const string SectionName = "system.identityModel";
	}
}
