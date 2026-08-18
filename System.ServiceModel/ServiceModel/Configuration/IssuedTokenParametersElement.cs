using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.IO;
using System.ServiceModel.Description;
using System.ServiceModel.Security.Tokens;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000636 RID: 1590
	public sealed class IssuedTokenParametersElement : ServiceModelConfigurationElement
	{
		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x06003CFF RID: 15615 RVA: 0x000E8BA9 File Offset: 0x000E6DA9
		// (set) Token: 0x06003D00 RID: 15616 RVA: 0x000E8BBB File Offset: 0x000E6DBB
		[ConfigurationProperty("defaultMessageSecurityVersion")]
		[TypeConverter(typeof(MessageSecurityVersionConverter))]
		public MessageSecurityVersion DefaultMessageSecurityVersion
		{
			get
			{
				return (MessageSecurityVersion)base["defaultMessageSecurityVersion"];
			}
			set
			{
				base["defaultMessageSecurityVersion"] = value;
			}
		}

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x06003D01 RID: 15617 RVA: 0x000E8BC9 File Offset: 0x000E6DC9
		[ConfigurationProperty("additionalRequestParameters")]
		public XmlElementElementCollection AdditionalRequestParameters
		{
			get
			{
				return (XmlElementElementCollection)base["additionalRequestParameters"];
			}
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x06003D02 RID: 15618 RVA: 0x000E8BDB File Offset: 0x000E6DDB
		[ConfigurationProperty("claimTypeRequirements")]
		public ClaimTypeElementCollection ClaimTypeRequirements
		{
			get
			{
				return (ClaimTypeElementCollection)base["claimTypeRequirements"];
			}
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06003D03 RID: 15619 RVA: 0x000E8BED File Offset: 0x000E6DED
		[ConfigurationProperty("issuer")]
		public IssuedTokenParametersEndpointAddressElement Issuer
		{
			get
			{
				return (IssuedTokenParametersEndpointAddressElement)base["issuer"];
			}
		}

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x06003D04 RID: 15620 RVA: 0x000E8BFF File Offset: 0x000E6DFF
		[ConfigurationProperty("issuerMetadata")]
		public EndpointAddressElementBase IssuerMetadata
		{
			get
			{
				return (EndpointAddressElementBase)base["issuerMetadata"];
			}
		}

		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x06003D05 RID: 15621 RVA: 0x000E8C11 File Offset: 0x000E6E11
		// (set) Token: 0x06003D06 RID: 15622 RVA: 0x000E8C23 File Offset: 0x000E6E23
		[ConfigurationProperty("keySize", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0)]
		public int KeySize
		{
			get
			{
				return (int)base["keySize"];
			}
			set
			{
				base["keySize"] = value;
			}
		}

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x06003D07 RID: 15623 RVA: 0x000E8C36 File Offset: 0x000E6E36
		// (set) Token: 0x06003D08 RID: 15624 RVA: 0x000E8C48 File Offset: 0x000E6E48
		[ConfigurationProperty("keyType", DefaultValue = SecurityKeyType.SymmetricKey)]
		[ServiceModelEnumValidator(typeof(SecurityKeyTypeHelper))]
		public SecurityKeyType KeyType
		{
			get
			{
				return (SecurityKeyType)base["keyType"];
			}
			set
			{
				base["keyType"] = value;
			}
		}

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x06003D09 RID: 15625 RVA: 0x000E8C5B File Offset: 0x000E6E5B
		internal Collection<IssuedTokenParametersElement> OptionalIssuedTokenParameters
		{
			get
			{
				if (this.IsReadOnly())
				{
					DiagnosticUtility.FailFast("IssuedTokenParametersElement.OptionalIssuedTokenParameters should only be called by Admin APIs");
				}
				if (this.optionalIssuedTokenParameters == null)
				{
					this.optionalIssuedTokenParameters = new Collection<IssuedTokenParametersElement>();
				}
				return this.optionalIssuedTokenParameters;
			}
		}

		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x06003D0A RID: 15626 RVA: 0x000E8C89 File Offset: 0x000E6E89
		// (set) Token: 0x06003D0B RID: 15627 RVA: 0x000E8C9B File Offset: 0x000E6E9B
		[ConfigurationProperty("tokenType", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string TokenType
		{
			get
			{
				return (string)base["tokenType"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["tokenType"] = value;
			}
		}

		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x06003D0C RID: 15628 RVA: 0x000E8CB8 File Offset: 0x000E6EB8
		// (set) Token: 0x06003D0D RID: 15629 RVA: 0x000E8CCA File Offset: 0x000E6ECA
		[ConfigurationProperty("useStrTransform", DefaultValue = false)]
		public bool UseStrTransform
		{
			get
			{
				return (bool)base["useStrTransform"];
			}
			set
			{
				base["useStrTransform"] = value;
			}
		}

		// Token: 0x06003D0E RID: 15630 RVA: 0x000E8CE0 File Offset: 0x000E6EE0
		internal void ApplyConfiguration(IssuedSecurityTokenParameters parameters)
		{
			if (parameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("parameters"));
			}
			if (this.AdditionalRequestParameters != null)
			{
				foreach (object obj in this.AdditionalRequestParameters)
				{
					XmlElementElement xmlElementElement = (XmlElementElement)obj;
					parameters.AdditionalRequestParameters.Add(xmlElementElement.XmlElement);
				}
			}
			if (this.ClaimTypeRequirements != null)
			{
				foreach (object obj2 in this.ClaimTypeRequirements)
				{
					ClaimTypeElement claimTypeElement = (ClaimTypeElement)obj2;
					parameters.ClaimTypeRequirements.Add(new ClaimTypeRequirement(claimTypeElement.ClaimType, claimTypeElement.IsOptional));
				}
			}
			parameters.KeySize = this.KeySize;
			parameters.KeyType = this.KeyType;
			parameters.DefaultMessageSecurityVersion = this.DefaultMessageSecurityVersion;
			parameters.UseStrTransform = this.UseStrTransform;
			if (!string.IsNullOrEmpty(this.TokenType))
			{
				parameters.TokenType = this.TokenType;
			}
			if (base.ElementInformation.Properties["issuer"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.Issuer.Validate();
				parameters.IssuerAddress = ConfigLoader.LoadEndpointAddress(this.Issuer);
				if (!string.IsNullOrEmpty(this.Issuer.Binding))
				{
					parameters.IssuerBinding = ConfigLoader.LookupBinding(this.Issuer.Binding, this.Issuer.BindingConfiguration, base.EvaluationContext);
				}
			}
			if (base.ElementInformation.Properties["issuerMetadata"].ValueOrigin != PropertyValueOrigin.Default)
			{
				parameters.IssuerMetadataAddress = ConfigLoader.LoadEndpointAddress(this.IssuerMetadata);
			}
		}

		// Token: 0x06003D0F RID: 15631 RVA: 0x000E8EB4 File Offset: 0x000E70B4
		internal void Copy(IssuedTokenParametersElement source)
		{
			if (this.IsReadOnly())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigReadOnly")));
			}
			if (source == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("source");
			}
			foreach (object obj in source.AdditionalRequestParameters)
			{
				XmlElementElement source2 = (XmlElementElement)obj;
				XmlElementElement xmlElementElement = new XmlElementElement();
				xmlElementElement.Copy(source2);
				this.AdditionalRequestParameters.Add(xmlElementElement);
			}
			foreach (object obj2 in source.ClaimTypeRequirements)
			{
				ClaimTypeElement claimTypeElement = (ClaimTypeElement)obj2;
				this.ClaimTypeRequirements.Add(new ClaimTypeElement(claimTypeElement.ClaimType, claimTypeElement.IsOptional));
			}
			this.KeySize = source.KeySize;
			this.KeyType = source.KeyType;
			this.TokenType = source.TokenType;
			this.DefaultMessageSecurityVersion = source.DefaultMessageSecurityVersion;
			this.UseStrTransform = source.UseStrTransform;
			if (source.ElementInformation.Properties["issuer"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.Issuer.Copy(source.Issuer);
			}
			if (source.ElementInformation.Properties["issuerMetadata"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.IssuerMetadata.Copy(source.IssuerMetadata);
			}
		}

		// Token: 0x06003D10 RID: 15632 RVA: 0x000E9050 File Offset: 0x000E7250
		internal IssuedSecurityTokenParameters Create(bool createTemplateOnly, SecurityKeyType templateKeyType)
		{
			IssuedSecurityTokenParameters issuedSecurityTokenParameters = new IssuedSecurityTokenParameters();
			if (!createTemplateOnly)
			{
				this.ApplyConfiguration(issuedSecurityTokenParameters);
			}
			else
			{
				issuedSecurityTokenParameters.KeyType = templateKeyType;
			}
			return issuedSecurityTokenParameters;
		}

		// Token: 0x06003D11 RID: 15633 RVA: 0x000E9078 File Offset: 0x000E7278
		internal void InitializeFrom(IssuedSecurityTokenParameters source, bool initializeNestedBindings)
		{
			if (source == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("source");
			}
			base.SetPropertyValueIfNotDefaultValue<SecurityKeyType>("keyType", source.KeyType);
			if (source.KeySize > 0)
			{
				base.SetPropertyValueIfNotDefaultValue<int>("keySize", source.KeySize);
			}
			base.SetPropertyValueIfNotDefaultValue<string>("tokenType", source.TokenType);
			base.SetPropertyValueIfNotDefaultValue<bool>("useStrTransform", source.UseStrTransform);
			if (source.IssuerAddress != null)
			{
				this.Issuer.InitializeFrom(source.IssuerAddress);
			}
			if (source.DefaultMessageSecurityVersion != null)
			{
				base.SetPropertyValueIfNotDefaultValue<MessageSecurityVersion>("defaultMessageSecurityVersion", source.DefaultMessageSecurityVersion);
			}
			if (source.IssuerBinding != null && initializeNestedBindings)
			{
				this.Issuer.BindingConfiguration = this.Issuer.Address.ToString();
				string binding;
				BindingsSection.TryAdd(this.Issuer.BindingConfiguration, source.IssuerBinding, out binding);
				this.Issuer.Binding = binding;
			}
			if (source.IssuerMetadataAddress != null)
			{
				this.IssuerMetadata.InitializeFrom(source.IssuerMetadataAddress);
			}
			foreach (XmlElement element in source.AdditionalRequestParameters)
			{
				this.AdditionalRequestParameters.Add(new XmlElementElement(element));
			}
			foreach (ClaimTypeRequirement claimTypeRequirement in source.ClaimTypeRequirements)
			{
				this.ClaimTypeRequirements.Add(new ClaimTypeElement(claimTypeRequirement.ClaimType, claimTypeRequirement.IsOptional));
			}
			foreach (IssuedSecurityTokenParameters.AlternativeIssuerEndpoint alternativeIssuerEndpoint in source.AlternativeIssuerEndpoints)
			{
				IssuedTokenParametersElement issuedTokenParametersElement = new IssuedTokenParametersElement();
				issuedTokenParametersElement.Issuer.InitializeFrom(alternativeIssuerEndpoint.IssuerAddress);
				if (initializeNestedBindings)
				{
					issuedTokenParametersElement.Issuer.BindingConfiguration = issuedTokenParametersElement.Issuer.Address.ToString();
					string binding2;
					BindingsSection.TryAdd(issuedTokenParametersElement.Issuer.BindingConfiguration, alternativeIssuerEndpoint.IssuerBinding, out binding2);
					issuedTokenParametersElement.Issuer.Binding = binding2;
				}
				this.OptionalIssuedTokenParameters.Add(issuedTokenParametersElement);
			}
		}

		// Token: 0x06003D12 RID: 15634 RVA: 0x000E92D8 File Offset: 0x000E74D8
		protected override bool SerializeToXmlElement(XmlWriter writer, string elementName)
		{
			bool flag = base.SerializeToXmlElement(writer, elementName);
			bool flag2 = this.OptionalIssuedTokenParameters.Count > 0;
			if (flag2 && writer != null)
			{
				MemoryStream memoryStream = new MemoryStream();
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8))
				{
					xmlTextWriter.Formatting = Formatting.Indented;
					xmlTextWriter.WriteStartElement("alternativeIssuedTokenParameters");
					foreach (IssuedTokenParametersElement issuedTokenParametersElement in this.OptionalIssuedTokenParameters)
					{
						issuedTokenParametersElement.SerializeToXmlElement(xmlTextWriter, "issuedTokenParameters");
					}
					xmlTextWriter.WriteEndElement();
					xmlTextWriter.Flush();
					string @string = new UTF8Encoding().GetString(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
					writer.WriteComment(@string.Substring(1, @string.Length - 1));
					xmlTextWriter.Close();
				}
			}
			return flag || flag2;
		}

		// Token: 0x06003D13 RID: 15635 RVA: 0x000E93D8 File Offset: 0x000E75D8
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			if (sourceElement is IssuedTokenParametersElement)
			{
				IssuedTokenParametersElement issuedTokenParametersElement = (IssuedTokenParametersElement)sourceElement;
				this.optionalIssuedTokenParameters = issuedTokenParametersElement.optionalIssuedTokenParameters;
			}
			base.Unmerge(sourceElement, parentElement, saveMode);
		}

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06003D14 RID: 15636 RVA: 0x000E940C File Offset: 0x000E760C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("defaultMessageSecurityVersion", typeof(MessageSecurityVersion), null, new MessageSecurityVersionConverter(), null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("additionalRequestParameters", typeof(XmlElementElementCollection), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("claimTypeRequirements", typeof(ClaimTypeElementCollection), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("issuer", typeof(IssuedTokenParametersEndpointAddressElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("issuerMetadata", typeof(EndpointAddressElementBase), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("keySize", typeof(int), 0, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("keyType", typeof(SecurityKeyType), SecurityKeyType.SymmetricKey, null, new ServiceModelEnumValidator(typeof(SecurityKeyTypeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("tokenType", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("useStrTransform", typeof(bool), false, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C8A RID: 11402
		private Collection<IssuedTokenParametersElement> optionalIssuedTokenParameters;

		// Token: 0x04002C8B RID: 11403
		private ConfigurationPropertyCollection properties;
	}
}
