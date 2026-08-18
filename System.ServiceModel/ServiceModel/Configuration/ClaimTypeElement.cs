using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005FF RID: 1535
	public sealed class ClaimTypeElement : ConfigurationElement
	{
		// Token: 0x06003B2E RID: 15150 RVA: 0x000E2D0E File Offset: 0x000E0F0E
		public ClaimTypeElement()
		{
		}

		// Token: 0x06003B2F RID: 15151 RVA: 0x000E2D16 File Offset: 0x000E0F16
		public ClaimTypeElement(string claimType, bool isOptional)
		{
			this.ClaimType = claimType;
			this.IsOptional = isOptional;
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06003B30 RID: 15152 RVA: 0x000E2D2C File Offset: 0x000E0F2C
		// (set) Token: 0x06003B31 RID: 15153 RVA: 0x000E2D3E File Offset: 0x000E0F3E
		[ConfigurationProperty("claimType", DefaultValue = "", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		[StringValidator(MinLength = 0)]
		public string ClaimType
		{
			get
			{
				return (string)base["claimType"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["claimType"] = value;
			}
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06003B32 RID: 15154 RVA: 0x000E2D5B File Offset: 0x000E0F5B
		// (set) Token: 0x06003B33 RID: 15155 RVA: 0x000E2D6D File Offset: 0x000E0F6D
		[ConfigurationProperty("isOptional", DefaultValue = false)]
		public bool IsOptional
		{
			get
			{
				return (bool)base["isOptional"];
			}
			set
			{
				base["isOptional"] = value;
			}
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06003B34 RID: 15156 RVA: 0x000E2D80 File Offset: 0x000E0F80
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("claimType", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey),
						new ConfigurationProperty("isOptional", typeof(bool), false, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002A80 RID: 10880
		private ConfigurationPropertyCollection properties;
	}
}
