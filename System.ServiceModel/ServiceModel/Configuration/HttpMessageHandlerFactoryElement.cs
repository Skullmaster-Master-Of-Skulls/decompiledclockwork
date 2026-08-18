using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000624 RID: 1572
	public sealed class HttpMessageHandlerFactoryElement : ConfigurationElement
	{
		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x06003C69 RID: 15465 RVA: 0x000E6C35 File Offset: 0x000E4E35
		// (set) Token: 0x06003C6A RID: 15466 RVA: 0x000E6C47 File Offset: 0x000E4E47
		[ConfigurationProperty("handlers")]
		public DelegatingHandlerElementCollection Handlers
		{
			get
			{
				return (DelegatingHandlerElementCollection)base["handlers"];
			}
			internal set
			{
				base["handlers"] = value;
			}
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06003C6B RID: 15467 RVA: 0x000E6C55 File Offset: 0x000E4E55
		// (set) Token: 0x06003C6C RID: 15468 RVA: 0x000E6C67 File Offset: 0x000E4E67
		[ConfigurationProperty("type")]
		[StringValidator(MinLength = 0)]
		public string Type
		{
			get
			{
				return (string)base["type"];
			}
			set
			{
				base["type"] = value;
			}
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06003C6D RID: 15469 RVA: 0x000E6C78 File Offset: 0x000E4E78
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("handlers", typeof(DelegatingHandlerElementCollection), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("type", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C82 RID: 11394
		private ConfigurationPropertyCollection properties;
	}
}
