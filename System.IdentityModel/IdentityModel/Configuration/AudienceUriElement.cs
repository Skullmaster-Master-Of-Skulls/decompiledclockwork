using System;
using System.Configuration;

namespace System.IdentityModel.Configuration
{
	// Token: 0x020001BF RID: 447
	public sealed class AudienceUriElement : ConfigurationElement
	{
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x00041C27 File Offset: 0x0003FE27
		// (set) Token: 0x06000E73 RID: 3699 RVA: 0x00041C39 File Offset: 0x0003FE39
		[ConfigurationProperty("value", IsRequired = true, DefaultValue = " ", IsKey = true)]
		[StringValidator(MinLength = 1)]
		public string Value
		{
			get
			{
				return (string)base["value"];
			}
			set
			{
				base["value"] = value;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x00041C47 File Offset: 0x0003FE47
		internal bool IsConfigured
		{
			get
			{
				return base.ElementInformation.Properties["value"].ValueOrigin > PropertyValueOrigin.Default;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x00041C68 File Offset: 0x0003FE68
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("value", typeof(string), " ", null, new StringValidator(1, int.MaxValue, null), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04000D0F RID: 3343
		private const string DefaultValue = " ";

		// Token: 0x04000D10 RID: 3344
		private ConfigurationPropertyCollection properties;
	}
}
