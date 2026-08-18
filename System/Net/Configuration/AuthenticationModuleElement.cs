using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x02000641 RID: 1601
	public sealed class AuthenticationModuleElement : ConfigurationElement
	{
		// Token: 0x06003199 RID: 12697 RVA: 0x000D46D4 File Offset: 0x000D36D4
		public AuthenticationModuleElement()
		{
			this.properties.Add(this.type);
		}

		// Token: 0x0600319A RID: 12698 RVA: 0x000D4714 File Offset: 0x000D3714
		public AuthenticationModuleElement(string typeName) : this()
		{
			if (typeName != (string)this.type.DefaultValue)
			{
				this.Type = typeName;
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x0600319B RID: 12699 RVA: 0x000D473B File Offset: 0x000D373B
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x0600319C RID: 12700 RVA: 0x000D4743 File Offset: 0x000D3743
		// (set) Token: 0x0600319D RID: 12701 RVA: 0x000D4756 File Offset: 0x000D3756
		[ConfigurationProperty("type", IsRequired = true, IsKey = true)]
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

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x0600319E RID: 12702 RVA: 0x000D4765 File Offset: 0x000D3765
		internal string Key
		{
			get
			{
				return this.Type;
			}
		}

		// Token: 0x04002E9F RID: 11935
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002EA0 RID: 11936
		private readonly ConfigurationProperty type = new ConfigurationProperty("type", typeof(string), null, ConfigurationPropertyOptions.IsKey);
	}
}
