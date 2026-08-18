using System;

namespace System.Configuration
{
	// Token: 0x02000673 RID: 1651
	public sealed class IriParsingElement : ConfigurationElement
	{
		// Token: 0x06003300 RID: 13056 RVA: 0x000D7DB8 File Offset: 0x000D6DB8
		public IriParsingElement()
		{
			this.properties.Add(this.enabled);
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06003301 RID: 13057 RVA: 0x000D7E08 File Offset: 0x000D6E08
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06003302 RID: 13058 RVA: 0x000D7E10 File Offset: 0x000D6E10
		// (set) Token: 0x06003303 RID: 13059 RVA: 0x000D7E23 File Offset: 0x000D6E23
		[ConfigurationProperty("enabled", DefaultValue = false)]
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

		// Token: 0x04002F86 RID: 12166
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002F87 RID: 12167
		private readonly ConfigurationProperty enabled = new ConfigurationProperty("enabled", typeof(bool), false, ConfigurationPropertyOptions.None);
	}
}
