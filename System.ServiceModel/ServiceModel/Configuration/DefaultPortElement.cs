using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000613 RID: 1555
	public sealed class DefaultPortElement : ConfigurationElement
	{
		// Token: 0x06003BE1 RID: 15329 RVA: 0x000E51F4 File Offset: 0x000E33F4
		public DefaultPortElement()
		{
		}

		// Token: 0x06003BE2 RID: 15330 RVA: 0x000E51FC File Offset: 0x000E33FC
		public DefaultPortElement(DefaultPortElement other)
		{
			this.Scheme = other.Scheme;
			this.Port = other.Port;
		}

		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06003BE3 RID: 15331 RVA: 0x000E521C File Offset: 0x000E341C
		// (set) Token: 0x06003BE4 RID: 15332 RVA: 0x000E522E File Offset: 0x000E342E
		[ConfigurationProperty("scheme", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		[StringValidator(MinLength = 1)]
		public string Scheme
		{
			get
			{
				return (string)base["scheme"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["scheme"] = value;
			}
		}

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x06003BE5 RID: 15333 RVA: 0x000E524B File Offset: 0x000E344B
		// (set) Token: 0x06003BE6 RID: 15334 RVA: 0x000E525D File Offset: 0x000E345D
		[ConfigurationProperty("port", DefaultValue = 0, Options = ConfigurationPropertyOptions.IsRequired)]
		[IntegerValidator(MinValue = 0, MaxValue = 65535)]
		public int Port
		{
			get
			{
				return (int)base["port"];
			}
			set
			{
				base["port"] = value;
			}
		}

		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x06003BE7 RID: 15335 RVA: 0x000E5270 File Offset: 0x000E3470
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("scheme", typeof(string), null, null, new StringValidator(1, int.MaxValue, null), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey),
						new ConfigurationProperty("port", typeof(int), 0, null, new IntegerValidator(0, 65535, false), ConfigurationPropertyOptions.IsRequired)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C75 RID: 11381
		private ConfigurationPropertyCollection properties;
	}
}
