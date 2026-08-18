using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x0200066A RID: 1642
	public sealed class WebProxyScriptElement : ConfigurationElement
	{
		// Token: 0x060032CB RID: 13003 RVA: 0x000D75D4 File Offset: 0x000D65D4
		public WebProxyScriptElement()
		{
			this.properties.Add(this.downloadTimeout);
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x000D7648 File Offset: 0x000D6648
		protected override void PostDeserialize()
		{
			if (base.EvaluationContext.IsMachineLevel)
			{
				return;
			}
			try
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
			}
			catch (Exception inner)
			{
				throw new ConfigurationErrorsException(SR.GetString("net_config_element_permission", new object[]
				{
					"webProxyScript"
				}), inner);
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x060032CD RID: 13005 RVA: 0x000D76A4 File Offset: 0x000D66A4
		// (set) Token: 0x060032CE RID: 13006 RVA: 0x000D76B7 File Offset: 0x000D66B7
		[ConfigurationProperty("downloadTimeout", DefaultValue = "00:01:00")]
		public TimeSpan DownloadTimeout
		{
			get
			{
				return (TimeSpan)base[this.downloadTimeout];
			}
			set
			{
				base[this.downloadTimeout] = value;
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x060032CF RID: 13007 RVA: 0x000D76CB File Offset: 0x000D66CB
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04002F75 RID: 12149
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002F76 RID: 12150
		private readonly ConfigurationProperty downloadTimeout = new ConfigurationProperty("downloadTimeout", typeof(TimeSpan), TimeSpan.FromMinutes(1.0), null, new TimeSpanValidator(new TimeSpan(0, 0, 0), TimeSpan.MaxValue, false), ConfigurationPropertyOptions.None);
	}
}
