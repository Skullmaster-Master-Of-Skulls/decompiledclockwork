using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x02000331 RID: 817
	public sealed class HttpWebRequestElement : ConfigurationElement
	{
		// Token: 0x06001D46 RID: 7494 RVA: 0x0008B7B0 File Offset: 0x000899B0
		public HttpWebRequestElement()
		{
			this.properties.Add(this.maximumResponseHeadersLength);
			this.properties.Add(this.maximumErrorResponseLength);
			this.properties.Add(this.maximumUnauthorizedUploadLength);
			this.properties.Add(this.useUnsafeHeaderParsing);
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x0008B898 File Offset: 0x00089A98
		protected override void PostDeserialize()
		{
			if (base.EvaluationContext.IsMachineLevel)
			{
				return;
			}
			PropertyInformation[] array = new PropertyInformation[]
			{
				base.ElementInformation.Properties["maximumResponseHeadersLength"],
				base.ElementInformation.Properties["maximumErrorResponseLength"]
			};
			foreach (PropertyInformation propertyInformation in array)
			{
				if (propertyInformation.ValueOrigin == PropertyValueOrigin.SetHere)
				{
					try
					{
						ExceptionHelper.WebPermissionUnrestricted.Demand();
					}
					catch (Exception inner)
					{
						throw new ConfigurationErrorsException(SR.GetString("net_config_property_permission", new object[]
						{
							propertyInformation.Name
						}), inner);
					}
				}
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001D48 RID: 7496 RVA: 0x0008B948 File Offset: 0x00089B48
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001D49 RID: 7497 RVA: 0x0008B950 File Offset: 0x00089B50
		// (set) Token: 0x06001D4A RID: 7498 RVA: 0x0008B963 File Offset: 0x00089B63
		[ConfigurationProperty("maximumUnauthorizedUploadLength", DefaultValue = -1)]
		public int MaximumUnauthorizedUploadLength
		{
			get
			{
				return (int)base[this.maximumUnauthorizedUploadLength];
			}
			set
			{
				base[this.maximumUnauthorizedUploadLength] = value;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001D4B RID: 7499 RVA: 0x0008B977 File Offset: 0x00089B77
		// (set) Token: 0x06001D4C RID: 7500 RVA: 0x0008B98A File Offset: 0x00089B8A
		[ConfigurationProperty("maximumErrorResponseLength", DefaultValue = 64)]
		public int MaximumErrorResponseLength
		{
			get
			{
				return (int)base[this.maximumErrorResponseLength];
			}
			set
			{
				base[this.maximumErrorResponseLength] = value;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001D4D RID: 7501 RVA: 0x0008B99E File Offset: 0x00089B9E
		// (set) Token: 0x06001D4E RID: 7502 RVA: 0x0008B9B1 File Offset: 0x00089BB1
		[ConfigurationProperty("maximumResponseHeadersLength", DefaultValue = 64)]
		public int MaximumResponseHeadersLength
		{
			get
			{
				return (int)base[this.maximumResponseHeadersLength];
			}
			set
			{
				base[this.maximumResponseHeadersLength] = value;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001D4F RID: 7503 RVA: 0x0008B9C5 File Offset: 0x00089BC5
		// (set) Token: 0x06001D50 RID: 7504 RVA: 0x0008B9D8 File Offset: 0x00089BD8
		[ConfigurationProperty("useUnsafeHeaderParsing", DefaultValue = false)]
		public bool UseUnsafeHeaderParsing
		{
			get
			{
				return (bool)base[this.useUnsafeHeaderParsing];
			}
			set
			{
				base[this.useUnsafeHeaderParsing] = value;
			}
		}

		// Token: 0x04001C36 RID: 7222
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C37 RID: 7223
		private readonly ConfigurationProperty maximumResponseHeadersLength = new ConfigurationProperty("maximumResponseHeadersLength", typeof(int), 64, ConfigurationPropertyOptions.None);

		// Token: 0x04001C38 RID: 7224
		private readonly ConfigurationProperty maximumErrorResponseLength = new ConfigurationProperty("maximumErrorResponseLength", typeof(int), 64, ConfigurationPropertyOptions.None);

		// Token: 0x04001C39 RID: 7225
		private readonly ConfigurationProperty maximumUnauthorizedUploadLength = new ConfigurationProperty("maximumUnauthorizedUploadLength", typeof(int), -1, ConfigurationPropertyOptions.None);

		// Token: 0x04001C3A RID: 7226
		private readonly ConfigurationProperty useUnsafeHeaderParsing = new ConfigurationProperty("useUnsafeHeaderParsing", typeof(bool), false, ConfigurationPropertyOptions.None);
	}
}
