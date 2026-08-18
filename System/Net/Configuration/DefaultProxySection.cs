using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x0200064D RID: 1613
	public sealed class DefaultProxySection : ConfigurationSection
	{
		// Token: 0x060031F1 RID: 12785 RVA: 0x000D510C File Offset: 0x000D410C
		public DefaultProxySection()
		{
			this.properties.Add(this.bypasslist);
			this.properties.Add(this.module);
			this.properties.Add(this.proxy);
			this.properties.Add(this.enabled);
			this.properties.Add(this.useDefaultCredentials);
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x000D5218 File Offset: 0x000D4218
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

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x060031F3 RID: 12787 RVA: 0x000D5274 File Offset: 0x000D4274
		[ConfigurationProperty("bypasslist")]
		public BypassElementCollection BypassList
		{
			get
			{
				return (BypassElementCollection)base[this.bypasslist];
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x060031F4 RID: 12788 RVA: 0x000D5287 File Offset: 0x000D4287
		[ConfigurationProperty("module")]
		public ModuleElement Module
		{
			get
			{
				return (ModuleElement)base[this.module];
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x060031F5 RID: 12789 RVA: 0x000D529A File Offset: 0x000D429A
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x060031F6 RID: 12790 RVA: 0x000D52A2 File Offset: 0x000D42A2
		[ConfigurationProperty("proxy")]
		public ProxyElement Proxy
		{
			get
			{
				return (ProxyElement)base[this.proxy];
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x060031F7 RID: 12791 RVA: 0x000D52B5 File Offset: 0x000D42B5
		// (set) Token: 0x060031F8 RID: 12792 RVA: 0x000D52C8 File Offset: 0x000D42C8
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

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x060031F9 RID: 12793 RVA: 0x000D52DC File Offset: 0x000D42DC
		// (set) Token: 0x060031FA RID: 12794 RVA: 0x000D52EF File Offset: 0x000D42EF
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

		// Token: 0x060031FB RID: 12795 RVA: 0x000D5304 File Offset: 0x000D4304
		protected override void Reset(ConfigurationElement parentElement)
		{
			DefaultProxySection defaultProxySection = new DefaultProxySection();
			defaultProxySection.InitializeDefault();
			base.Reset(defaultProxySection);
		}

		// Token: 0x04002EF8 RID: 12024
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002EF9 RID: 12025
		private readonly ConfigurationProperty bypasslist = new ConfigurationProperty("bypasslist", typeof(BypassElementCollection), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002EFA RID: 12026
		private readonly ConfigurationProperty module = new ConfigurationProperty("module", typeof(ModuleElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002EFB RID: 12027
		private readonly ConfigurationProperty proxy = new ConfigurationProperty("proxy", typeof(ProxyElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002EFC RID: 12028
		private readonly ConfigurationProperty enabled = new ConfigurationProperty("enabled", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002EFD RID: 12029
		private readonly ConfigurationProperty useDefaultCredentials = new ConfigurationProperty("useDefaultCredentials", typeof(bool), false, ConfigurationPropertyOptions.None);
	}
}
