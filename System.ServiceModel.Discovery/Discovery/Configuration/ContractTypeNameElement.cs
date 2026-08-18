using System;
using System.Configuration;

namespace System.ServiceModel.Discovery.Configuration
{
	// Token: 0x020000AE RID: 174
	public sealed class ContractTypeNameElement : ConfigurationElement
	{
		// Token: 0x0600072C RID: 1836 RVA: 0x0001286C File Offset: 0x00010A6C
		public ContractTypeNameElement()
		{
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00012874 File Offset: 0x00010A74
		public ContractTypeNameElement(string name, string ns)
		{
			this.Name = name;
			this.Namespace = ns;
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x0001288A File Offset: 0x00010A8A
		// (set) Token: 0x0600072F RID: 1839 RVA: 0x0001289C File Offset: 0x00010A9C
		[ConfigurationProperty("namespace", DefaultValue = "http://tempuri.org/", Options = ConfigurationPropertyOptions.IsKey)]
		public string Namespace
		{
			get
			{
				return (string)base["namespace"];
			}
			set
			{
				base["namespace"] = value;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x000128AA File Offset: 0x00010AAA
		// (set) Token: 0x06000731 RID: 1841 RVA: 0x000128BC File Offset: 0x00010ABC
		[ConfigurationProperty("name", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		[StringValidator(MinLength = 1)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				base["name"] = value;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x000128CC File Offset: 0x00010ACC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("namespace", typeof(string), "http://tempuri.org/", null, null, ConfigurationPropertyOptions.IsKey),
						new ConfigurationProperty("name", typeof(string), null, null, new StringValidator(1), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x040001C6 RID: 454
		private ConfigurationPropertyCollection properties;
	}
}
