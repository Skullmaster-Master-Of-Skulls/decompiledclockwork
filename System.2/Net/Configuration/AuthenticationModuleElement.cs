using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x02000324 RID: 804
	public sealed class AuthenticationModuleElement : ConfigurationElement
	{
		// Token: 0x06001CE2 RID: 7394 RVA: 0x0008A7F6 File Offset: 0x000889F6
		public AuthenticationModuleElement()
		{
			this.properties.Add(this.type);
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x0008A836 File Offset: 0x00088A36
		public AuthenticationModuleElement(string typeName) : this()
		{
			if (typeName != (string)this.type.DefaultValue)
			{
				this.Type = typeName;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x0008A85D File Offset: 0x00088A5D
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001CE5 RID: 7397 RVA: 0x0008A865 File Offset: 0x00088A65
		// (set) Token: 0x06001CE6 RID: 7398 RVA: 0x0008A878 File Offset: 0x00088A78
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

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001CE7 RID: 7399 RVA: 0x0008A887 File Offset: 0x00088A87
		internal string Key
		{
			get
			{
				return this.Type;
			}
		}

		// Token: 0x04001BC6 RID: 7110
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001BC7 RID: 7111
		private readonly ConfigurationProperty type = new ConfigurationProperty("type", typeof(string), null, ConfigurationPropertyOptions.IsKey);
	}
}
