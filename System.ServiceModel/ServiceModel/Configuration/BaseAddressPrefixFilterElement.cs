using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005EA RID: 1514
	public sealed class BaseAddressPrefixFilterElement : ConfigurationElement
	{
		// Token: 0x06003A70 RID: 14960 RVA: 0x000E0D58 File Offset: 0x000DEF58
		public BaseAddressPrefixFilterElement()
		{
		}

		// Token: 0x06003A71 RID: 14961 RVA: 0x000E0D60 File Offset: 0x000DEF60
		public BaseAddressPrefixFilterElement(Uri prefix) : this()
		{
			if (prefix == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("prefix");
			}
			this.Prefix = prefix;
		}

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x06003A72 RID: 14962 RVA: 0x000E0D88 File Offset: 0x000DEF88
		// (set) Token: 0x06003A73 RID: 14963 RVA: 0x000E0D9A File Offset: 0x000DEF9A
		[ConfigurationProperty("prefix", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		public Uri Prefix
		{
			get
			{
				return (Uri)base["prefix"];
			}
			set
			{
				base["prefix"] = value;
			}
		}

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x06003A74 RID: 14964 RVA: 0x000E0DA8 File Offset: 0x000DEFA8
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("prefix", typeof(Uri), null, null, null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002A6F RID: 10863
		private ConfigurationPropertyCollection properties;
	}
}
