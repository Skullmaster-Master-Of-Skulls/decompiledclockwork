using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x02000328 RID: 808
	public sealed class BypassElement : ConfigurationElement
	{
		// Token: 0x06001CFE RID: 7422 RVA: 0x0008AC50 File Offset: 0x00088E50
		public BypassElement()
		{
			this.properties.Add(this.address);
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x0008AC90 File Offset: 0x00088E90
		public BypassElement(string address) : this()
		{
			this.Address = address;
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001D00 RID: 7424 RVA: 0x0008AC9F File Offset: 0x00088E9F
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06001D01 RID: 7425 RVA: 0x0008ACA7 File Offset: 0x00088EA7
		// (set) Token: 0x06001D02 RID: 7426 RVA: 0x0008ACBA File Offset: 0x00088EBA
		[ConfigurationProperty("address", IsRequired = true, IsKey = true)]
		public string Address
		{
			get
			{
				return (string)base[this.address];
			}
			set
			{
				base[this.address] = value;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06001D03 RID: 7427 RVA: 0x0008ACC9 File Offset: 0x00088EC9
		internal string Key
		{
			get
			{
				return this.Address;
			}
		}

		// Token: 0x04001BCC RID: 7116
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001BCD RID: 7117
		private readonly ConfigurationProperty address = new ConfigurationProperty("address", typeof(string), null, ConfigurationPropertyOptions.IsKey);
	}
}
