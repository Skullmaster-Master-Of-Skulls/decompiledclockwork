using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x02000349 RID: 841
	public sealed class WebProxyScriptElement : ConfigurationElement
	{
		// Token: 0x06001E33 RID: 7731 RVA: 0x0008DAFC File Offset: 0x0008BCFC
		public WebProxyScriptElement()
		{
			this.properties.Add(this.autoConfigUrlRetryInterval);
			this.properties.Add(this.downloadTimeout);
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x0008DBAC File Offset: 0x0008BDAC
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

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06001E35 RID: 7733 RVA: 0x0008DC04 File Offset: 0x0008BE04
		// (set) Token: 0x06001E36 RID: 7734 RVA: 0x0008DC17 File Offset: 0x0008BE17
		[ConfigurationProperty("autoConfigUrlRetryInterval", DefaultValue = 600)]
		public int AutoConfigUrlRetryInterval
		{
			get
			{
				return (int)base[this.autoConfigUrlRetryInterval];
			}
			set
			{
				base[this.autoConfigUrlRetryInterval] = value;
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001E37 RID: 7735 RVA: 0x0008DC2B File Offset: 0x0008BE2B
		// (set) Token: 0x06001E38 RID: 7736 RVA: 0x0008DC3E File Offset: 0x0008BE3E
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

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001E39 RID: 7737 RVA: 0x0008DC52 File Offset: 0x0008BE52
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04001CBA RID: 7354
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001CBB RID: 7355
		private readonly ConfigurationProperty autoConfigUrlRetryInterval = new ConfigurationProperty("autoConfigUrlRetryInterval", typeof(int), 600, null, new WebProxyScriptElement.RetryIntervalValidator(), ConfigurationPropertyOptions.None);

		// Token: 0x04001CBC RID: 7356
		private readonly ConfigurationProperty downloadTimeout = new ConfigurationProperty("downloadTimeout", typeof(TimeSpan), TimeSpan.FromMinutes(1.0), null, new TimeSpanValidator(new TimeSpan(0, 0, 0), TimeSpan.MaxValue, false), ConfigurationPropertyOptions.None);

		// Token: 0x020007C7 RID: 1991
		private class RetryIntervalValidator : ConfigurationValidatorBase
		{
			// Token: 0x06004396 RID: 17302 RVA: 0x0011D196 File Offset: 0x0011B396
			public override bool CanValidate(Type type)
			{
				return type == typeof(int);
			}

			// Token: 0x06004397 RID: 17303 RVA: 0x0011D1A8 File Offset: 0x0011B3A8
			public override void Validate(object value)
			{
				int num = (int)value;
				if (num < 0)
				{
					throw new ArgumentOutOfRangeException("value", num, SR.GetString("ArgumentOutOfRange_Bounds_Lower_Upper", new object[]
					{
						0,
						int.MaxValue
					}));
				}
			}
		}
	}
}
