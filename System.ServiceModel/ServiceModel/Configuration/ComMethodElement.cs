using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006B1 RID: 1713
	public sealed class ComMethodElement : ConfigurationElement
	{
		// Token: 0x17001113 RID: 4371
		// (get) Token: 0x06004267 RID: 16999 RVA: 0x000FB6FC File Offset: 0x000F98FC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("exposedMethod", typeof(string), null, null, new StringValidator(1, int.MaxValue, null), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x06004268 RID: 17000 RVA: 0x000FB74D File Offset: 0x000F994D
		public ComMethodElement()
		{
		}

		// Token: 0x06004269 RID: 17001 RVA: 0x000FB755 File Offset: 0x000F9955
		public ComMethodElement(string method) : this()
		{
			this.ExposedMethod = method;
		}

		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x0600426A RID: 17002 RVA: 0x000FB764 File Offset: 0x000F9964
		// (set) Token: 0x0600426B RID: 17003 RVA: 0x000FB776 File Offset: 0x000F9976
		[ConfigurationProperty("exposedMethod", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		[StringValidator(MinLength = 1)]
		public string ExposedMethod
		{
			get
			{
				return (string)base["exposedMethod"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["exposedMethod"] = value;
			}
		}

		// Token: 0x04002D00 RID: 11520
		private ConfigurationPropertyCollection properties;
	}
}
