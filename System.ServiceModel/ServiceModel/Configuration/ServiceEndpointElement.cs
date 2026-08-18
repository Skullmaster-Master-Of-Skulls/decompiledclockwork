using System;
using System.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000687 RID: 1671
	public sealed class ServiceEndpointElement : ConfigurationElement, IConfigurationContextProviderInternal
	{
		// Token: 0x1700103E RID: 4158
		// (get) Token: 0x06004076 RID: 16502 RVA: 0x000F4D48 File Offset: 0x000F2F48
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("address", typeof(Uri), "", null, null, ConfigurationPropertyOptions.IsKey),
						new ConfigurationProperty("behaviorConfiguration", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("binding", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.IsKey),
						new ConfigurationProperty("bindingConfiguration", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.IsKey),
						new ConfigurationProperty("name", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("bindingName", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.IsKey),
						new ConfigurationProperty("bindingNamespace", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.IsKey),
						new ConfigurationProperty("contract", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.IsKey),
						new ConfigurationProperty("headers", typeof(AddressHeaderCollectionElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("identity", typeof(IdentityElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("listenUriMode", typeof(ListenUriMode), ListenUriMode.Explicit, null, new ServiceModelEnumValidator(typeof(ListenUriModeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("listenUri", typeof(Uri), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("isSystemEndpoint", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("kind", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.IsKey),
						new ConfigurationProperty("endpointConfiguration", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.IsKey)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x06004077 RID: 16503 RVA: 0x000F4FD8 File Offset: 0x000F31D8
		public ServiceEndpointElement()
		{
		}

		// Token: 0x06004078 RID: 16504 RVA: 0x000F4FE0 File Offset: 0x000F31E0
		public ServiceEndpointElement(Uri address, string contractType) : this()
		{
			this.Address = address;
			this.Contract = contractType;
		}

		// Token: 0x06004079 RID: 16505 RVA: 0x000F4FF8 File Offset: 0x000F31F8
		internal void Copy(ServiceEndpointElement source)
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
			if (propertyInformationCollection["bindingName"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.BindingName = source.BindingName;
			}
			if (propertyInformationCollection["bindingNamespace"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.BindingNamespace = source.BindingNamespace;
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
			if (propertyInformationCollection["listenUriMode"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.ListenUriMode = source.ListenUriMode;
			}
			if (propertyInformationCollection["listenUri"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.ListenUri = source.ListenUri;
			}
			if (propertyInformationCollection["isSystemEndpoint"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.IsSystemEndpoint = source.IsSystemEndpoint;
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

		// Token: 0x1700103F RID: 4159
		// (get) Token: 0x0600407A RID: 16506 RVA: 0x000F5200 File Offset: 0x000F3400
		// (set) Token: 0x0600407B RID: 16507 RVA: 0x000F5212 File Offset: 0x000F3412
		[ConfigurationProperty("address", DefaultValue = "", Options = ConfigurationPropertyOptions.IsKey)]
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

		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x0600407C RID: 16508 RVA: 0x000F5220 File Offset: 0x000F3420
		// (set) Token: 0x0600407D RID: 16509 RVA: 0x000F5232 File Offset: 0x000F3432
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

		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x0600407E RID: 16510 RVA: 0x000F524F File Offset: 0x000F344F
		// (set) Token: 0x0600407F RID: 16511 RVA: 0x000F5261 File Offset: 0x000F3461
		[ConfigurationProperty("binding", Options = ConfigurationPropertyOptions.IsKey)]
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

		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x06004080 RID: 16512 RVA: 0x000F527E File Offset: 0x000F347E
		// (set) Token: 0x06004081 RID: 16513 RVA: 0x000F5290 File Offset: 0x000F3490
		[ConfigurationProperty("bindingConfiguration", DefaultValue = "", Options = ConfigurationPropertyOptions.IsKey)]
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

		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x06004082 RID: 16514 RVA: 0x000F52AD File Offset: 0x000F34AD
		// (set) Token: 0x06004083 RID: 16515 RVA: 0x000F52BF File Offset: 0x000F34BF
		[ConfigurationProperty("name", DefaultValue = "")]
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

		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x06004084 RID: 16516 RVA: 0x000F52DC File Offset: 0x000F34DC
		// (set) Token: 0x06004085 RID: 16517 RVA: 0x000F52EE File Offset: 0x000F34EE
		[ConfigurationProperty("bindingName", DefaultValue = "", Options = ConfigurationPropertyOptions.IsKey)]
		[StringValidator(MinLength = 0)]
		public string BindingName
		{
			get
			{
				return (string)base["bindingName"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["bindingName"] = value;
			}
		}

		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x06004086 RID: 16518 RVA: 0x000F530B File Offset: 0x000F350B
		// (set) Token: 0x06004087 RID: 16519 RVA: 0x000F531D File Offset: 0x000F351D
		[ConfigurationProperty("bindingNamespace", DefaultValue = "", Options = ConfigurationPropertyOptions.IsKey)]
		[StringValidator(MinLength = 0)]
		public string BindingNamespace
		{
			get
			{
				return (string)base["bindingNamespace"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["bindingNamespace"] = value;
			}
		}

		// Token: 0x17001046 RID: 4166
		// (get) Token: 0x06004088 RID: 16520 RVA: 0x000F533A File Offset: 0x000F353A
		// (set) Token: 0x06004089 RID: 16521 RVA: 0x000F534C File Offset: 0x000F354C
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

		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x0600408A RID: 16522 RVA: 0x000F5369 File Offset: 0x000F3569
		[ConfigurationProperty("headers")]
		public AddressHeaderCollectionElement Headers
		{
			get
			{
				return (AddressHeaderCollectionElement)base["headers"];
			}
		}

		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x0600408B RID: 16523 RVA: 0x000F537B File Offset: 0x000F357B
		[ConfigurationProperty("identity")]
		public IdentityElement Identity
		{
			get
			{
				return (IdentityElement)base["identity"];
			}
		}

		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x0600408C RID: 16524 RVA: 0x000F538D File Offset: 0x000F358D
		// (set) Token: 0x0600408D RID: 16525 RVA: 0x000F539F File Offset: 0x000F359F
		[ConfigurationProperty("listenUriMode", DefaultValue = ListenUriMode.Explicit)]
		[ServiceModelEnumValidator(typeof(ListenUriModeHelper))]
		public ListenUriMode ListenUriMode
		{
			get
			{
				return (ListenUriMode)base["listenUriMode"];
			}
			set
			{
				base["listenUriMode"] = value;
			}
		}

		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x0600408E RID: 16526 RVA: 0x000F53B2 File Offset: 0x000F35B2
		// (set) Token: 0x0600408F RID: 16527 RVA: 0x000F53C4 File Offset: 0x000F35C4
		[ConfigurationProperty("listenUri", DefaultValue = null)]
		public Uri ListenUri
		{
			get
			{
				return (Uri)base["listenUri"];
			}
			set
			{
				base["listenUri"] = value;
			}
		}

		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x06004090 RID: 16528 RVA: 0x000F53D2 File Offset: 0x000F35D2
		// (set) Token: 0x06004091 RID: 16529 RVA: 0x000F53E4 File Offset: 0x000F35E4
		[ConfigurationProperty("isSystemEndpoint", DefaultValue = false)]
		public bool IsSystemEndpoint
		{
			get
			{
				return (bool)base["isSystemEndpoint"];
			}
			set
			{
				base["isSystemEndpoint"] = value;
			}
		}

		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x06004092 RID: 16530 RVA: 0x000F53F7 File Offset: 0x000F35F7
		// (set) Token: 0x06004093 RID: 16531 RVA: 0x000F5409 File Offset: 0x000F3609
		[ConfigurationProperty("kind", DefaultValue = "", Options = ConfigurationPropertyOptions.IsKey)]
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

		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x06004094 RID: 16532 RVA: 0x000F5426 File Offset: 0x000F3626
		// (set) Token: 0x06004095 RID: 16533 RVA: 0x000F5438 File Offset: 0x000F3638
		[ConfigurationProperty("endpointConfiguration", DefaultValue = "", Options = ConfigurationPropertyOptions.IsKey)]
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

		// Token: 0x06004096 RID: 16534 RVA: 0x000F5455 File Offset: 0x000F3655
		ContextInformation IConfigurationContextProviderInternal.GetEvaluationContext()
		{
			return base.EvaluationContext;
		}

		// Token: 0x06004097 RID: 16535 RVA: 0x000F545D File Offset: 0x000F365D
		ContextInformation IConfigurationContextProviderInternal.GetOriginalEvaluationContext()
		{
			return null;
		}

		// Token: 0x04002CD3 RID: 11475
		private ConfigurationPropertyCollection properties;
	}
}
