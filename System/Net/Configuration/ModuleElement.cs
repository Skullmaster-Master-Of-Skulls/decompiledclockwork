using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x02000655 RID: 1621
	public sealed class ModuleElement : ConfigurationElement
	{
		// Token: 0x06003228 RID: 12840 RVA: 0x000D5CEC File Offset: 0x000D4CEC
		public ModuleElement()
		{
			this.properties.Add(this.type);
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06003229 RID: 12841 RVA: 0x000D5D2C File Offset: 0x000D4D2C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x0600322A RID: 12842 RVA: 0x000D5D34 File Offset: 0x000D4D34
		// (set) Token: 0x0600322B RID: 12843 RVA: 0x000D5D47 File Offset: 0x000D4D47
		[ConfigurationProperty("type")]
		public string Type
		{
			get
			{
				return (string)base[this.type];
			}
			set
			{
				base[this.type] = value;
			}
		}

		// Token: 0x04002F11 RID: 12049
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002F12 RID: 12050
		private readonly ConfigurationProperty type = new ConfigurationProperty("type", typeof(string), null, ConfigurationPropertyOptions.None);
	}
}
