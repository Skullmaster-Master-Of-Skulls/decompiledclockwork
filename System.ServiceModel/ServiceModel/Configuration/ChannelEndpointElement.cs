using System;
using System.Configuration;
using System.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005FB RID: 1531
	public sealed class ChannelEndpointElement : ConfigurationElement, IConfigurationContextProviderInternal
	{
		// Token: 0x06003B01 RID: 15105 RVA: 0x000E246A File Offset: 0x000E066A
		public ChannelEndpointElement()
		{
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x000E2474 File Offset: 0x000E0674
		public ChannelEndpointElement(EndpointAddress address, string contractType) : this()
		{
			if (address != null)
			{
				this.Address = address.Uri;
				this.Headers.Headers = address.Headers;
				if (address.Identity != null)
				{
					this.Identity.InitializeFrom(address.Identity);
				}
			}
			this.Contract = contractType;
		}

		// Token: 0x06003B03 RID: 15107 RVA: 0x000E24D0 File Offset: 0x000E06D0
		internal void Copy(ChannelEndpointElement source)
		{
			if (source == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("source");
			}
			PropertyInformationCollection propertyInformationCollection = source.ElementInformation.Properties;
			if (propertyInformationCollection["address"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.Address = source.Address;
			}
			if (propertyInformationCollection["behaviorConfiguration"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.BehaviorConfiguration = source.BehaviorConfiguration;
			}
			if (propertyInformationCollection["binding"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.Binding = source.Binding;
			}
			if (propertyInformationCollection["bindingConfiguration"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.BindingConfiguration = source.BindingConfiguration;
			}
			if (propertyInformationCollection["name"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.Name = source.Name;
			}
			if (propertyInformationCollection["contract"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.Contract = source.Contract;
			}
			if (propertyInformationCollection["headers"].ValueOrigin != PropertyValueOrigin.Default && source.Headers != null)
			{
				this.Headers.Copy(source.Headers);
			}
			if (propertyInformationCollection["identity"].ValueOrigin != PropertyValueOrigin.Default && source.Identity != null)
			{
				this.Identity.Copy(source.Identity);
			}
			if (propertyInformationCollection["kind"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.Kind = source.Kind;
			}
			if (propertyInformationCollection["endpointConfiguration"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.EndpointConfiguration = source.EndpointConfiguration;
			}
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06003B04 RID: 15108 RVA: 0x000E2642 File Offset: 0x000E0842
		// (set) Token: 0x06003B05 RID: 15109 RVA: 0x000E2654 File Offset: 0x000E0854
		[ConfigurationProperty("address", Options = ConfigurationPropertyOptions.None)]
		public Uri Address
		{
			get
			{
				return (Uri)base["address"];
			}
			set
			{
				base["address"] = value;
			}
		}

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06003B06 RID: 15110 RVA: 0x000E2662 File Offset: 0x000E0862
		// (set) Token: 0x06003B07 RID: 15111 RVA: 0x000E2674 File Offset: 0x000E0874
		[ConfigurationProperty("behaviorConfiguration", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string BehaviorConfiguration
		{
			get
			{
				return (string)base["behaviorConfiguration"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["behaviorConfiguration"] = value;
			}
		}

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x06003B08 RID: 15112 RVA: 0x000E2691 File Offset: 0x000E0891
		// (set) Token: 0x06003B09 RID: 15113 RVA: 0x000E26A3 File Offset: 0x000E08A3
		[ConfigurationProperty("binding", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string Binding
		{
			get
			{
				return (string)base["binding"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["binding"] = value;
			}
		}

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x06003B0A RID: 15114 RVA: 0x000E26C0 File Offset: 0x000E08C0
		// (set) Token: 0x06003B0B RID: 15115 RVA: 0x000E26D2 File Offset: 0x000E08D2
		[ConfigurationProperty("bindingConfiguration", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string BindingConfiguration
		{
			get
			{
				return (string)base["bindingConfiguration"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["bindingConfiguration"] = value;
			}
		}

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x06003B0C RID: 15116 RVA: 0x000E26EF File Offset: 0x000E08EF
		// (set) Token: 0x06003B0D RID: 15117 RVA: 0x000E2701 File Offset: 0x000E0901
		[ConfigurationProperty("contract", DefaultValue = "", Options = ConfigurationPropertyOptions.IsKey)]
		[StringValidator(MinLength = 0)]
		public string Contract
		{
			get
			{
				return (string)base["contract"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["contract"] = value;
			}
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x06003B0E RID: 15118 RVA: 0x000E271E File Offset: 0x000E091E
		[ConfigurationProperty("headers")]
		public AddressHeaderCollectionElement Headers
		{
			get
			{
				return (AddressHeaderCollectionElement)base["headers"];
			}
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x06003B0F RID: 15119 RVA: 0x000E2730 File Offset: 0x000E0930
		[ConfigurationProperty("identity")]
		public IdentityElement Identity
		{
			get
			{
				return (IdentityElement)base["identity"];
			}
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06003B10 RID: 15120 RVA: 0x000E2742 File Offset: 0x000E0942
		// (set) Token: 0x06003B11 RID: 15121 RVA: 0x000E2754 File Offset: 0x000E0954
		[ConfigurationProperty("name", DefaultValue = "", Options = ConfigurationPropertyOptions.IsKey)]
		[StringValidator(MinLength = 0)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["name"] = value;
			}
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06003B12 RID: 15122 RVA: 0x000E2771 File Offset: 0x000E0971
		// (set) Token: 0x06003B13 RID: 15123 RVA: 0x000E2783 File Offset: 0x000E0983
		[ConfigurationProperty("kind", DefaultValue = "", Options = ConfigurationPropertyOptions.None)]
		[StringValidator(MinLength = 0)]
		public string Kind
		{
			get
			{
				return (string)base["kind"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["kind"] = value;
			}
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06003B14 RID: 15124 RVA: 0x000E27A0 File Offset: 0x000E09A0
		// (set) Token: 0x06003B15 RID: 15125 RVA: 0x000E27B2 File Offset: 0x000E09B2
		[ConfigurationProperty("endpointConfiguration", DefaultValue = "", Options = ConfigurationPropertyOptions.None)]
		[StringValidator(MinLength = 0)]
		public string EndpointConfiguration
		{
			get
			{
				return (string)base["endpointConfiguration"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["endpointConfiguration"] = value;
			}
		}

		// Token: 0x06003B16 RID: 15126 RVA: 0x000E27CF File Offset: 0x000E09CF
		[SecurityCritical]
		protected override void Reset(ConfigurationElement parentElement)
		{
			this.contextHelper.OnReset(parentElement);
			base.Reset(parentElement);
		}

		// Token: 0x06003B17 RID: 15127 RVA: 0x000E27E4 File Offset: 0x000E09E4
		ContextInformation IConfigurationContextProviderInternal.GetEvaluationContext()
		{
			return base.EvaluationContext;
		}

		// Token: 0x06003B18 RID: 15128 RVA: 0x000E27EC File Offset: 0x000E09EC
		[SecurityCritical]
		ContextInformation IConfigurationContextProviderInternal.GetOriginalEvaluationContext()
		{
			return this.contextHelper.GetOriginalContext(this);
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x06003B19 RID: 15129 RVA: 0x000E27FC File Offset: 0x000E09FC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("address", typeof(Uri), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("behaviorConfiguration", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("binding", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("bindingConfiguration", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("contract", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.IsKey),
						new ConfigurationProperty("headers", typeof(AddressHeaderCollectionElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("identity", typeof(IdentityElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("name", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.IsKey),
						new ConfigurationProperty("kind", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("endpointConfiguration", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002A7C RID: 10876
		[SecurityCritical]
		private EvaluationContextHelper contextHelper;

		// Token: 0x04002A7D RID: 10877
		private ConfigurationPropertyCollection properties;
	}
}
