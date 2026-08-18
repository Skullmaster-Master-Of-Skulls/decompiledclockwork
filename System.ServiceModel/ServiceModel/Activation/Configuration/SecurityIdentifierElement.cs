using System;
using System.ComponentModel;
using System.Configuration;
using System.Security.Principal;

namespace System.ServiceModel.Activation.Configuration
{
	// Token: 0x020005D3 RID: 1491
	public sealed class SecurityIdentifierElement : ConfigurationElement
	{
		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x060039F4 RID: 14836 RVA: 0x000DFC5C File Offset: 0x000DDE5C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("securityIdentifier", typeof(SecurityIdentifier), null, new SecurityIdentifierConverter(), null, ConfigurationPropertyOptions.IsKey)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x060039F5 RID: 14837 RVA: 0x000DFCA6 File Offset: 0x000DDEA6
		public SecurityIdentifierElement()
		{
		}

		// Token: 0x060039F6 RID: 14838 RVA: 0x000DFCAE File Offset: 0x000DDEAE
		public SecurityIdentifierElement(SecurityIdentifier sid) : this()
		{
			this.SecurityIdentifier = sid;
		}

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x060039F7 RID: 14839 RVA: 0x000DFCBD File Offset: 0x000DDEBD
		// (set) Token: 0x060039F8 RID: 14840 RVA: 0x000DFCCF File Offset: 0x000DDECF
		[ConfigurationProperty("securityIdentifier", DefaultValue = null, Options = ConfigurationPropertyOptions.IsKey)]
		[TypeConverter(typeof(SecurityIdentifierConverter))]
		public SecurityIdentifier SecurityIdentifier
		{
			get
			{
				return (SecurityIdentifier)base["securityIdentifier"];
			}
			set
			{
				base["securityIdentifier"] = value;
			}
		}

		// Token: 0x04002A36 RID: 10806
		private ConfigurationPropertyCollection properties;
	}
}
