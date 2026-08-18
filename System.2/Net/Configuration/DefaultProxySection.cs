using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x0200032F RID: 815
	public sealed class DefaultProxySection : ConfigurationSection
	{
		// Token: 0x06001D37 RID: 7479 RVA: 0x0008B178 File Offset: 0x00089378
		public DefaultProxySection()
		{
			this.properties.Add(this.bypasslist);
			this.properties.Add(this.module);
			this.properties.Add(this.proxy);
			this.properties.Add(this.enabled);
			this.properties.Add(this.useDefaultCredentials);
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x0008B284 File Offset: 0x00089484
		protected override void PostDeserialize()
		{
			if (base.EvaluationContext.IsMachineLevel)
			{
				return;
			}
			try
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
			}
			catch (Exception inner)
			{
				throw new ConfigurationErrorsException(SR.GetString("net_config_section_permission", new object[]
				{
					"defaultProxy"
				}), inner);
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001D39 RID: 7481 RVA: 0x0008B2DC File Offset: 0x000894DC
		[ConfigurationProperty("bypasslist")]
		public BypassElementCollection BypassList
		{
			get
			{
				return (BypassElementCollection)base[this.bypasslist];
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001D3A RID: 7482 RVA: 0x0008B2EF File Offset: 0x000894EF
		[ConfigurationProperty("module")]
		public ModuleElement Module
		{
			get
			{
				return (ModuleElement)base[this.module];
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001D3B RID: 7483 RVA: 0x0008B302 File Offset: 0x00089502
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001D3C RID: 7484 RVA: 0x0008B30A File Offset: 0x0008950A
		[ConfigurationProperty("proxy")]
		public ProxyElement Proxy
		{
			get
			{
				return (ProxyElement)base[this.proxy];
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001D3D RID: 7485 RVA: 0x0008B31D File Offset: 0x0008951D
		// (set) Token: 0x06001D3E RID: 7486 RVA: 0x0008B330 File Offset: 0x00089530
		[ConfigurationProperty("enabled", DefaultValue = true)]
		public bool Enabled
		{
			get
			{
				return (bool)base[this.enabled];
			}
			set
			{
				base[this.enabled] = value;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001D3F RID: 7487 RVA: 0x0008B344 File Offset: 0x00089544
		// (set) Token: 0x06001D40 RID: 7488 RVA: 0x0008B357 File Offset: 0x00089557
		[ConfigurationProperty("useDefaultCredentials", DefaultValue = false)]
		public bool UseDefaultCredentials
		{
			get
			{
				return (bool)base[this.useDefaultCredentials];
			}
			set
			{
				base[this.useDefaultCredentials] = value;
			}
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x0008B36C File Offset: 0x0008956C
		protected override void Reset(ConfigurationElement parentElement)
		{
			DefaultProxySection defaultProxySection = new DefaultProxySection();
			defaultProxySection.InitializeDefault();
			base.Reset(defaultProxySection);
		}

		// Token: 0x04001C2E RID: 7214
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C2F RID: 7215
		private readonly ConfigurationProperty bypasslist = new ConfigurationProperty("bypasslist", typeof(BypassElementCollection), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C30 RID: 7216
		private readonly ConfigurationProperty module = new ConfigurationProperty("module", typeof(ModuleElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C31 RID: 7217
		private readonly ConfigurationProperty proxy = new ConfigurationProperty("proxy", typeof(ProxyElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C32 RID: 7218
		private readonly ConfigurationProperty enabled = new ConfigurationProperty("enabled", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04001C33 RID: 7219
		private readonly ConfigurationProperty useDefaultCredentials = new ConfigurationProperty("useDefaultCredentials", typeof(bool), false, ConfigurationPropertyOptions.None);
	}
}
