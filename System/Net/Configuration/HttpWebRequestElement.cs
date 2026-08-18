using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x0200064F RID: 1615
	public sealed class HttpWebRequestElement : ConfigurationElement
	{
		// Token: 0x06003200 RID: 12800 RVA: 0x000D5744 File Offset: 0x000D4744
		public HttpWebRequestElement()
		{
			this.properties.Add(this.maximumResponseHeadersLength);
			this.properties.Add(this.maximumErrorResponseLength);
			this.properties.Add(this.maximumUnauthorizedUploadLength);
			this.properties.Add(this.useUnsafeHeaderParsing);
		}

		// Token: 0x06003201 RID: 12801 RVA: 0x000D582C File Offset: 0x000D482C
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

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06003202 RID: 12802 RVA: 0x000D58E8 File Offset: 0x000D48E8
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06003203 RID: 12803 RVA: 0x000D58F0 File Offset: 0x000D48F0
		// (set) Token: 0x06003204 RID: 12804 RVA: 0x000D5903 File Offset: 0x000D4903
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

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06003205 RID: 12805 RVA: 0x000D5917 File Offset: 0x000D4917
		// (set) Token: 0x06003206 RID: 12806 RVA: 0x000D592A File Offset: 0x000D492A
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

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06003207 RID: 12807 RVA: 0x000D593E File Offset: 0x000D493E
		// (set) Token: 0x06003208 RID: 12808 RVA: 0x000D5951 File Offset: 0x000D4951
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

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06003209 RID: 12809 RVA: 0x000D5965 File Offset: 0x000D4965
		// (set) Token: 0x0600320A RID: 12810 RVA: 0x000D5978 File Offset: 0x000D4978
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

		// Token: 0x04002F00 RID: 12032
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002F01 RID: 12033
		private readonly ConfigurationProperty maximumResponseHeadersLength = new ConfigurationProperty("maximumResponseHeadersLength", typeof(int), 64, ConfigurationPropertyOptions.None);

		// Token: 0x04002F02 RID: 12034
		private readonly ConfigurationProperty maximumErrorResponseLength = new ConfigurationProperty("maximumErrorResponseLength", typeof(int), 64, ConfigurationPropertyOptions.None);

		// Token: 0x04002F03 RID: 12035
		private readonly ConfigurationProperty maximumUnauthorizedUploadLength = new ConfigurationProperty("maximumUnauthorizedUploadLength", typeof(int), -1, ConfigurationPropertyOptions.None);

		// Token: 0x04002F04 RID: 12036
		private readonly ConfigurationProperty useUnsafeHeaderParsing = new ConfigurationProperty("useUnsafeHeaderParsing", typeof(bool), false, ConfigurationPropertyOptions.None);
	}
}
