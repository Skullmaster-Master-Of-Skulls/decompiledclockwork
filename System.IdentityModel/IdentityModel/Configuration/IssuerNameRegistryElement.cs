using System;
using System.Configuration;

namespace System.IdentityModel.Configuration
{
	// Token: 0x020001CA RID: 458
	public sealed class IssuerNameRegistryElement : ConfigurationElementInterceptor
	{
		// Token: 0x06000EF5 RID: 3829 RVA: 0x00041C1F File Offset: 0x0003FE1F
		public IssuerNameRegistryElement()
		{
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x000431FC File Offset: 0x000413FC
		internal IssuerNameRegistryElement(string type)
		{
			this.Type = type;
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x0004320B File Offset: 0x0004140B
		internal bool IsConfigured
		{
			get
			{
				return base.ElementInformation.Properties["type"].ValueOrigin != PropertyValueOrigin.Default || (base.ChildNodes != null && base.ChildNodes.Count > 0);
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x00043243 File Offset: 0x00041443
		// (set) Token: 0x06000EF9 RID: 3833 RVA: 0x00041EC1 File Offset: 0x000400C1
		[ConfigurationProperty("type", IsRequired = false, IsKey = false)]
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

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x00043258 File Offset: 0x00041458
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("type", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04000D7B RID: 3451
		private ConfigurationPropertyCollection properties;
	}
}
