using System;
using System.ComponentModel;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200061F RID: 1567
	public sealed class FederatedMessageSecurityOverHttpElement : ServiceModelConfigurationElement
	{
		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06003C2C RID: 15404 RVA: 0x000E5E05 File Offset: 0x000E4005
		// (set) Token: 0x06003C2D RID: 15405 RVA: 0x000E5E17 File Offset: 0x000E4017
		[ConfigurationProperty("algorithmSuite", DefaultValue = "Default")]
		[TypeConverter(typeof(SecurityAlgorithmSuiteConverter))]
		public SecurityAlgorithmSuite AlgorithmSuite
		{
			get
			{
				return (SecurityAlgorithmSuite)base["algorithmSuite"];
			}
			set
			{
				base["algorithmSuite"] = value;
			}
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06003C2E RID: 15406 RVA: 0x000E5E25 File Offset: 0x000E4025
		[ConfigurationProperty("claimTypeRequirements")]
		public ClaimTypeElementCollection ClaimTypeRequirements
		{
			get
			{
				return (ClaimTypeElementCollection)base["claimTypeRequirements"];
			}
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x06003C2F RID: 15407 RVA: 0x000E5E37 File Offset: 0x000E4037
		// (set) Token: 0x06003C30 RID: 15408 RVA: 0x000E5E49 File Offset: 0x000E4049
		[ConfigurationProperty("establishSecurityContext", DefaultValue = true)]
		public bool EstablishSecurityContext
		{
			get
			{
				return (bool)base["establishSecurityContext"];
			}
			set
			{
				base["establishSecurityContext"] = value;
			}
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06003C31 RID: 15409 RVA: 0x000E5E5C File Offset: 0x000E405C
		// (set) Token: 0x06003C32 RID: 15410 RVA: 0x000E5E6E File Offset: 0x000E406E
		[ConfigurationProperty("issuedKeyType", DefaultValue = SecurityKeyType.SymmetricKey)]
		[ServiceModelEnumValidator(typeof(SecurityKeyTypeHelper))]
		public SecurityKeyType IssuedKeyType
		{
			get
			{
				return (SecurityKeyType)base["issuedKeyType"];
			}
			set
			{
				base["issuedKeyType"] = value;
			}
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06003C33 RID: 15411 RVA: 0x000E5E81 File Offset: 0x000E4081
		// (set) Token: 0x06003C34 RID: 15412 RVA: 0x000E5E93 File Offset: 0x000E4093
		[ConfigurationProperty("issuedTokenType", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string IssuedTokenType
		{
			get
			{
				return (string)base["issuedTokenType"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["issuedTokenType"] = value;
			}
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x06003C35 RID: 15413 RVA: 0x000E5EB0 File Offset: 0x000E40B0
		[ConfigurationProperty("issuer")]
		public IssuedTokenParametersEndpointAddressElement Issuer
		{
			get
			{
				return (IssuedTokenParametersEndpointAddressElement)base["issuer"];
			}
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x06003C36 RID: 15414 RVA: 0x000E5EC2 File Offset: 0x000E40C2
		[ConfigurationProperty("issuerMetadata")]
		public EndpointAddressElementBase IssuerMetadata
		{
			get
			{
				return (EndpointAddressElementBase)base["issuerMetadata"];
			}
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06003C37 RID: 15415 RVA: 0x000E5ED4 File Offset: 0x000E40D4
		// (set) Token: 0x06003C38 RID: 15416 RVA: 0x000E5EE6 File Offset: 0x000E40E6
		[ConfigurationProperty("negotiateServiceCredential", DefaultValue = true)]
		public bool NegotiateServiceCredential
		{
			get
			{
				return (bool)base["negotiateServiceCredential"];
			}
			set
			{
				base["negotiateServiceCredential"] = value;
			}
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06003C39 RID: 15417 RVA: 0x000E5EF9 File Offset: 0x000E40F9
		[ConfigurationProperty("tokenRequestParameters")]
		public XmlElementElementCollection TokenRequestParameters
		{
			get
			{
				return (XmlElementElementCollection)base["tokenRequestParameters"];
			}
		}

		// Token: 0x06003C3A RID: 15418 RVA: 0x000E5F0C File Offset: 0x000E410C
		internal void ApplyConfiguration(FederatedMessageSecurityOverHttp security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			security.NegotiateServiceCredential = this.NegotiateServiceCredential;
			security.AlgorithmSuite = this.AlgorithmSuite;
			security.IssuedKeyType = this.IssuedKeyType;
			security.EstablishSecurityContext = this.EstablishSecurityContext;
			if (!string.IsNullOrEmpty(this.IssuedTokenType))
			{
				security.IssuedTokenType = this.IssuedTokenType;
			}
			if (base.ElementInformation.Properties["issuer"].ValueOrigin != PropertyValueOrigin.Default)
			{
				security.IssuerAddress = ConfigLoader.LoadEndpointAddress(this.Issuer);
				if (!string.IsNullOrEmpty(this.Issuer.Binding))
				{
					security.IssuerBinding = ConfigLoader.LookupBinding(this.Issuer.Binding, this.Issuer.BindingConfiguration, base.EvaluationContext);
				}
			}
			if (base.ElementInformation.Properties["issuerMetadata"].ValueOrigin != PropertyValueOrigin.Default)
			{
				security.IssuerMetadataAddress = ConfigLoader.LoadEndpointAddress(this.IssuerMetadata);
			}
			foreach (object obj in this.TokenRequestParameters)
			{
				XmlElementElement xmlElementElement = (XmlElementElement)obj;
				security.TokenRequestParameters.Add(xmlElementElement.XmlElement);
			}
			foreach (object obj2 in this.ClaimTypeRequirements)
			{
				ClaimTypeElement claimTypeElement = (ClaimTypeElement)obj2;
				security.ClaimTypeRequirements.Add(new ClaimTypeRequirement(claimTypeElement.ClaimType, claimTypeElement.IsOptional));
			}
		}

		// Token: 0x06003C3B RID: 15419 RVA: 0x000E60C0 File Offset: 0x000E42C0
		internal void InitializeFrom(FederatedMessageSecurityOverHttp security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			base.SetPropertyValueIfNotDefaultValue<bool>("negotiateServiceCredential", security.NegotiateServiceCredential);
			base.SetPropertyValueIfNotDefaultValue<SecurityAlgorithmSuite>("algorithmSuite", security.AlgorithmSuite);
			base.SetPropertyValueIfNotDefaultValue<SecurityKeyType>("issuedKeyType", security.IssuedKeyType);
			base.SetPropertyValueIfNotDefaultValue<bool>("establishSecurityContext", security.EstablishSecurityContext);
			if (security.IssuedTokenType != null)
			{
				this.IssuedTokenType = security.IssuedTokenType;
			}
			if (security.IssuerAddress != null)
			{
				this.Issuer.InitializeFrom(security.IssuerAddress);
			}
			if (security.IssuerMetadataAddress != null)
			{
				this.IssuerMetadata.InitializeFrom(security.IssuerMetadataAddress);
			}
			string binding = null;
			if (security.IssuerBinding != null)
			{
				if (null == this.Issuer.Address)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigNullIssuerAddress")));
				}
				this.Issuer.BindingConfiguration = this.Issuer.Address.ToString();
				BindingsSection.TryAdd(this.Issuer.BindingConfiguration, security.IssuerBinding, out binding);
				this.Issuer.Binding = binding;
			}
			foreach (XmlElement element in security.TokenRequestParameters)
			{
				this.TokenRequestParameters.Add(new XmlElementElement(element));
			}
			foreach (ClaimTypeRequirement claimTypeRequirement in security.ClaimTypeRequirements)
			{
				ClaimTypeElement element2 = new ClaimTypeElement(claimTypeRequirement.ClaimType, claimTypeRequirement.IsOptional);
				this.ClaimTypeRequirements.Add(element2);
			}
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06003C3C RID: 15420 RVA: 0x000E6290 File Offset: 0x000E4490
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("algorithmSuite", typeof(SecurityAlgorithmSuite), "Default", new SecurityAlgorithmSuiteConverter(), null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("claimTypeRequirements", typeof(ClaimTypeElementCollection), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("establishSecurityContext", typeof(bool), true, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("issuedKeyType", typeof(SecurityKeyType), SecurityKeyType.SymmetricKey, null, new ServiceModelEnumValidator(typeof(SecurityKeyTypeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("issuedTokenType", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("issuer", typeof(IssuedTokenParametersEndpointAddressElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("issuerMetadata", typeof(EndpointAddressElementBase), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("negotiateServiceCredential", typeof(bool), true, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("tokenRequestParameters", typeof(XmlElementElementCollection), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C7D RID: 11389
		private ConfigurationPropertyCollection properties;
	}
}
