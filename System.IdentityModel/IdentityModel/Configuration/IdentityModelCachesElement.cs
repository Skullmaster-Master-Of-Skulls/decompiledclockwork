using System;
using System.Configuration;

namespace System.IdentityModel.Configuration
{
	// Token: 0x020001C9 RID: 457
	public sealed class IdentityModelCachesElement : ConfigurationElement
	{
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x00043140 File Offset: 0x00041340
		// (set) Token: 0x06000EEF RID: 3823 RVA: 0x00043152 File Offset: 0x00041352
		[ConfigurationProperty("tokenReplayCache", IsRequired = false)]
		public CustomTypeElement TokenReplayCache
		{
			get
			{
				return (CustomTypeElement)base["tokenReplayCache"];
			}
			set
			{
				base["tokenReplayCache"] = value;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x00043160 File Offset: 0x00041360
		// (set) Token: 0x06000EF1 RID: 3825 RVA: 0x00043172 File Offset: 0x00041372
		[ConfigurationProperty("sessionSecurityTokenCache", IsRequired = false)]
		public CustomTypeElement SessionSecurityTokenCache
		{
			get
			{
				return (CustomTypeElement)base["sessionSecurityTokenCache"];
			}
			set
			{
				base["sessionSecurityTokenCache"] = value;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x00043180 File Offset: 0x00041380
		public bool IsConfigured
		{
			get
			{
				return this.TokenReplayCache != null || this.SessionSecurityTokenCache != null;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x00043198 File Offset: 0x00041398
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("tokenReplayCache", typeof(CustomTypeElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("sessionSecurityTokenCache", typeof(CustomTypeElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04000D7A RID: 3450
		private ConfigurationPropertyCollection properties;
	}
}
