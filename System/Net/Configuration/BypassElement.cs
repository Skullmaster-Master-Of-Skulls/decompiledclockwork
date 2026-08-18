using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x02000645 RID: 1605
	public sealed class BypassElement : ConfigurationElement
	{
		// Token: 0x060031B5 RID: 12725 RVA: 0x000D4B68 File Offset: 0x000D3B68
		public BypassElement()
		{
			this.properties.Add(this.address);
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x000D4BA8 File Offset: 0x000D3BA8
		public BypassElement(string address) : this()
		{
			this.Address = address;
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x060031B7 RID: 12727 RVA: 0x000D4BB7 File Offset: 0x000D3BB7
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x060031B8 RID: 12728 RVA: 0x000D4BBF File Offset: 0x000D3BBF
		// (set) Token: 0x060031B9 RID: 12729 RVA: 0x000D4BD2 File Offset: 0x000D3BD2
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

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x060031BA RID: 12730 RVA: 0x000D4BE1 File Offset: 0x000D3BE1
		internal string Key
		{
			get
			{
				return this.Address;
			}
		}

		// Token: 0x04002EA5 RID: 11941
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002EA6 RID: 11942
		private readonly ConfigurationProperty address = new ConfigurationProperty("address", typeof(string), null, ConfigurationPropertyOptions.IsKey);
	}
}
