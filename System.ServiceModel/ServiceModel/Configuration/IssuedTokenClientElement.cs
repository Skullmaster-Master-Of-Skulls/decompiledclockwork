using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.ServiceModel.Description;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000632 RID: 1586
	public sealed class IssuedTokenClientElement : ConfigurationElement
	{
		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x06003CD3 RID: 15571 RVA: 0x000E7E8F File Offset: 0x000E608F
		[ConfigurationProperty("localIssuer")]
		public IssuedTokenParametersEndpointAddressElement LocalIssuer
		{
			get
			{
				return (IssuedTokenParametersEndpointAddressElement)base["localIssuer"];
			}
		}

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x06003CD4 RID: 15572 RVA: 0x000E7EA1 File Offset: 0x000E60A1
		// (set) Token: 0x06003CD5 RID: 15573 RVA: 0x000E7EB3 File Offset: 0x000E60B3
		[ConfigurationProperty("localIssuerChannelBehaviors", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string LocalIssuerChannelBehaviors
		{
			get
			{
				return (string)base["localIssuerChannelBehaviors"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["localIssuerChannelBehaviors"] = value;
			}
		}

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x06003CD6 RID: 15574 RVA: 0x000E7ED0 File Offset: 0x000E60D0
		[ConfigurationProperty("issuerChannelBehaviors")]
		public IssuedTokenClientBehaviorsElementCollection IssuerChannelBehaviors
		{
			get
			{
				return (IssuedTokenClientBehaviorsElementCollection)base["issuerChannelBehaviors"];
			}
		}

		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x06003CD7 RID: 15575 RVA: 0x000E7EE2 File Offset: 0x000E60E2
		// (set) Token: 0x06003CD8 RID: 15576 RVA: 0x000E7EF4 File Offset: 0x000E60F4
		[ConfigurationProperty("cacheIssuedTokens", DefaultValue = true)]
		public bool CacheIssuedTokens
		{
			get
			{
				return (bool)base["cacheIssuedTokens"];
			}
			set
			{
				base["cacheIssuedTokens"] = value;
			}
		}

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x06003CD9 RID: 15577 RVA: 0x000E7F07 File Offset: 0x000E6107
		// (set) Token: 0x06003CDA RID: 15578 RVA: 0x000E7F19 File Offset: 0x000E6119
		[ConfigurationProperty("maxIssuedTokenCachingTime", DefaultValue = "10675199.02:48:05.4775807")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan MaxIssuedTokenCachingTime
		{
			get
			{
				return (TimeSpan)base["maxIssuedTokenCachingTime"];
			}
			set
			{
				base["maxIssuedTokenCachingTime"] = value;
			}
		}

		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x06003CDB RID: 15579 RVA: 0x000E7F2C File Offset: 0x000E612C
		// (set) Token: 0x06003CDC RID: 15580 RVA: 0x000E7F3E File Offset: 0x000E613E
		[ConfigurationProperty("defaultKeyEntropyMode", DefaultValue = SecurityKeyEntropyMode.CombinedEntropy)]
		[ServiceModelEnumValidator(typeof(SecurityKeyEntropyModeHelper))]
		public SecurityKeyEntropyMode DefaultKeyEntropyMode
		{
			get
			{
				return (SecurityKeyEntropyMode)base["defaultKeyEntropyMode"];
			}
			set
			{
				base["defaultKeyEntropyMode"] = value;
			}
		}

		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x06003CDD RID: 15581 RVA: 0x000E7F51 File Offset: 0x000E6151
		// (set) Token: 0x06003CDE RID: 15582 RVA: 0x000E7F63 File Offset: 0x000E6163
		[ConfigurationProperty("issuedTokenRenewalThresholdPercentage", DefaultValue = 60)]
		[IntegerValidator(MinValue = 0, MaxValue = 100)]
		public int IssuedTokenRenewalThresholdPercentage
		{
			get
			{
				return (int)base["issuedTokenRenewalThresholdPercentage"];
			}
			set
			{
				base["issuedTokenRenewalThresholdPercentage"] = value;
			}
		}

		// Token: 0x06003CDF RID: 15583 RVA: 0x000E7F78 File Offset: 0x000E6178
		public void Copy(IssuedTokenClientElement from)
		{
			if (this.IsReadOnly())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigReadOnly")));
			}
			if (from == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("from");
			}
			this.DefaultKeyEntropyMode = from.DefaultKeyEntropyMode;
			this.CacheIssuedTokens = from.CacheIssuedTokens;
			this.MaxIssuedTokenCachingTime = from.MaxIssuedTokenCachingTime;
			this.IssuedTokenRenewalThresholdPercentage = from.IssuedTokenRenewalThresholdPercentage;
			if (from.ElementInformation.Properties["localIssuer"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.LocalIssuer.Copy(from.LocalIssuer);
			}
			if (from.ElementInformation.Properties["localIssuerChannelBehaviors"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.LocalIssuerChannelBehaviors = from.LocalIssuerChannelBehaviors;
			}
			if (from.ElementInformation.Properties["issuerChannelBehaviors"].ValueOrigin != PropertyValueOrigin.Default)
			{
				foreach (object obj in from.IssuerChannelBehaviors)
				{
					IssuedTokenClientBehaviorsElement element = (IssuedTokenClientBehaviorsElement)obj;
					this.IssuerChannelBehaviors.Add(element);
				}
			}
		}

		// Token: 0x06003CE0 RID: 15584 RVA: 0x000E80AC File Offset: 0x000E62AC
		internal void ApplyConfiguration(IssuedTokenClientCredential issuedToken)
		{
			if (issuedToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("issuedToken");
			}
			issuedToken.CacheIssuedTokens = this.CacheIssuedTokens;
			issuedToken.DefaultKeyEntropyMode = this.DefaultKeyEntropyMode;
			issuedToken.MaxIssuedTokenCachingTime = this.MaxIssuedTokenCachingTime;
			issuedToken.IssuedTokenRenewalThresholdPercentage = this.IssuedTokenRenewalThresholdPercentage;
			if (base.ElementInformation.Properties["localIssuer"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.LocalIssuer.Validate();
				issuedToken.LocalIssuerAddress = ConfigLoader.LoadEndpointAddress(this.LocalIssuer);
				if (!string.IsNullOrEmpty(this.LocalIssuer.Binding))
				{
					issuedToken.LocalIssuerBinding = ConfigLoader.LookupBinding(this.LocalIssuer.Binding, this.LocalIssuer.BindingConfiguration, base.EvaluationContext);
				}
			}
			if (!string.IsNullOrEmpty(this.LocalIssuerChannelBehaviors))
			{
				ConfigLoader.LoadChannelBehaviors(this.LocalIssuerChannelBehaviors, base.EvaluationContext, issuedToken.LocalIssuerChannelBehaviors);
			}
			if (base.ElementInformation.Properties["issuerChannelBehaviors"].ValueOrigin != PropertyValueOrigin.Default)
			{
				foreach (object obj in this.IssuerChannelBehaviors)
				{
					IssuedTokenClientBehaviorsElement issuedTokenClientBehaviorsElement = (IssuedTokenClientBehaviorsElement)obj;
					if (!string.IsNullOrEmpty(issuedTokenClientBehaviorsElement.BehaviorConfiguration))
					{
						KeyedByTypeCollection<IEndpointBehavior> keyedByTypeCollection = new KeyedByTypeCollection<IEndpointBehavior>();
						ConfigLoader.LoadChannelBehaviors(issuedTokenClientBehaviorsElement.BehaviorConfiguration, base.EvaluationContext, keyedByTypeCollection);
						issuedToken.IssuerChannelBehaviors.Add(new Uri(issuedTokenClientBehaviorsElement.IssuerAddress), keyedByTypeCollection);
					}
				}
			}
		}

		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x06003CE1 RID: 15585 RVA: 0x000E8230 File Offset: 0x000E6430
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("localIssuer", typeof(IssuedTokenParametersEndpointAddressElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("localIssuerChannelBehaviors", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("issuerChannelBehaviors", typeof(IssuedTokenClientBehaviorsElementCollection), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("cacheIssuedTokens", typeof(bool), true, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxIssuedTokenCachingTime", typeof(TimeSpan), TimeSpan.Parse("10675199.02:48:05.4775807", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("defaultKeyEntropyMode", typeof(SecurityKeyEntropyMode), SecurityKeyEntropyMode.CombinedEntropy, null, new ServiceModelEnumValidator(typeof(SecurityKeyEntropyModeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("issuedTokenRenewalThresholdPercentage", typeof(int), 60, null, new IntegerValidator(0, 100, false), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C87 RID: 11399
		private ConfigurationPropertyCollection properties;
	}
}
