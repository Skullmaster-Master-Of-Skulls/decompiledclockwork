using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000681 RID: 1665
	public class SecurityElementBase : BindingElementExtensionElement
	{
		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x06003FF9 RID: 16377 RVA: 0x000F250C File Offset: 0x000F070C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("defaultAlgorithmSuite", typeof(SecurityAlgorithmSuite), "Default", new SecurityAlgorithmSuiteConverter(), null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("allowSerializedSigningTokenOnReply", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("enableUnsecuredResponse", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("authenticationMode", typeof(AuthenticationMode), AuthenticationMode.SspiNegotiated, null, new ServiceModelEnumValidator(typeof(AuthenticationModeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("requireDerivedKeys", typeof(bool), true, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("securityHeaderLayout", typeof(SecurityHeaderLayout), SecurityHeaderLayout.Strict, null, new ServiceModelEnumValidator(typeof(SecurityHeaderLayoutHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("includeTimestamp", typeof(bool), true, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("allowInsecureTransport", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("keyEntropyMode", typeof(SecurityKeyEntropyMode), SecurityKeyEntropyMode.CombinedEntropy, null, new ServiceModelEnumValidator(typeof(SecurityKeyEntropyModeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("issuedTokenParameters", typeof(IssuedTokenParametersElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("localClientSettings", typeof(LocalClientSecuritySettingsElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("localServiceSettings", typeof(LocalServiceSecuritySettingsElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("messageProtectionOrder", typeof(MessageProtectionOrder), MessageProtectionOrder.SignBeforeEncryptAndEncryptSignature, null, new ServiceModelEnumValidator(typeof(MessageProtectionOrderHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("protectTokens", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("messageSecurityVersion", typeof(MessageSecurityVersion), "Default", new MessageSecurityVersionConverter(), null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("requireSecurityContextCancellation", typeof(bool), true, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("requireSignatureConfirmation", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("canRenewSecurityContextToken", typeof(bool), true, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x06003FFA RID: 16378 RVA: 0x000F27DD File Offset: 0x000F09DD
		internal SecurityElementBase()
		{
		}

		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x06003FFB RID: 16379 RVA: 0x000F27E5 File Offset: 0x000F09E5
		internal bool HasImportFailed
		{
			get
			{
				return this.failedSecurityBindingElement != null;
			}
		}

		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x06003FFC RID: 16380 RVA: 0x000F27F0 File Offset: 0x000F09F0
		// (set) Token: 0x06003FFD RID: 16381 RVA: 0x000F27F8 File Offset: 0x000F09F8
		internal bool IsSecurityElementBootstrap { get; set; }

		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x06003FFE RID: 16382 RVA: 0x000F2801 File Offset: 0x000F0A01
		// (set) Token: 0x06003FFF RID: 16383 RVA: 0x000F2813 File Offset: 0x000F0A13
		[ConfigurationProperty("defaultAlgorithmSuite", DefaultValue = "Default")]
		[TypeConverter(typeof(SecurityAlgorithmSuiteConverter))]
		public SecurityAlgorithmSuite DefaultAlgorithmSuite
		{
			get
			{
				return (SecurityAlgorithmSuite)base["defaultAlgorithmSuite"];
			}
			set
			{
				base["defaultAlgorithmSuite"] = value;
			}
		}

		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x06004000 RID: 16384 RVA: 0x000F2821 File Offset: 0x000F0A21
		// (set) Token: 0x06004001 RID: 16385 RVA: 0x000F2833 File Offset: 0x000F0A33
		[ConfigurationProperty("allowSerializedSigningTokenOnReply", DefaultValue = false)]
		public bool AllowSerializedSigningTokenOnReply
		{
			get
			{
				return (bool)base["allowSerializedSigningTokenOnReply"];
			}
			set
			{
				base["allowSerializedSigningTokenOnReply"] = value;
			}
		}

		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x06004002 RID: 16386 RVA: 0x000F2846 File Offset: 0x000F0A46
		// (set) Token: 0x06004003 RID: 16387 RVA: 0x000F2858 File Offset: 0x000F0A58
		[ConfigurationProperty("enableUnsecuredResponse", DefaultValue = false)]
		public bool EnableUnsecuredResponse
		{
			get
			{
				return (bool)base["enableUnsecuredResponse"];
			}
			set
			{
				base["enableUnsecuredResponse"] = value;
			}
		}

		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x06004004 RID: 16388 RVA: 0x000F286B File Offset: 0x000F0A6B
		// (set) Token: 0x06004005 RID: 16389 RVA: 0x000F287D File Offset: 0x000F0A7D
		[ConfigurationProperty("authenticationMode", DefaultValue = AuthenticationMode.SspiNegotiated)]
		[ServiceModelEnumValidator(typeof(AuthenticationModeHelper))]
		public AuthenticationMode AuthenticationMode
		{
			get
			{
				return (AuthenticationMode)base["authenticationMode"];
			}
			set
			{
				base["authenticationMode"] = value;
			}
		}

		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x06004006 RID: 16390 RVA: 0x000F2890 File Offset: 0x000F0A90
		public override Type BindingElementType
		{
			get
			{
				return typeof(SecurityBindingElement);
			}
		}

		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x06004007 RID: 16391 RVA: 0x000F289C File Offset: 0x000F0A9C
		// (set) Token: 0x06004008 RID: 16392 RVA: 0x000F28AE File Offset: 0x000F0AAE
		[ConfigurationProperty("requireDerivedKeys", DefaultValue = true)]
		public bool RequireDerivedKeys
		{
			get
			{
				return (bool)base["requireDerivedKeys"];
			}
			set
			{
				base["requireDerivedKeys"] = value;
			}
		}

		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x06004009 RID: 16393 RVA: 0x000F28C1 File Offset: 0x000F0AC1
		// (set) Token: 0x0600400A RID: 16394 RVA: 0x000F28D3 File Offset: 0x000F0AD3
		[ConfigurationProperty("securityHeaderLayout", DefaultValue = SecurityHeaderLayout.Strict)]
		[ServiceModelEnumValidator(typeof(SecurityHeaderLayoutHelper))]
		public SecurityHeaderLayout SecurityHeaderLayout
		{
			get
			{
				return (SecurityHeaderLayout)base["securityHeaderLayout"];
			}
			set
			{
				base["securityHeaderLayout"] = value;
			}
		}

		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x0600400B RID: 16395 RVA: 0x000F28E6 File Offset: 0x000F0AE6
		// (set) Token: 0x0600400C RID: 16396 RVA: 0x000F28F8 File Offset: 0x000F0AF8
		[ConfigurationProperty("includeTimestamp", DefaultValue = true)]
		public bool IncludeTimestamp
		{
			get
			{
				return (bool)base["includeTimestamp"];
			}
			set
			{
				base["includeTimestamp"] = value;
			}
		}

		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x0600400D RID: 16397 RVA: 0x000F290B File Offset: 0x000F0B0B
		// (set) Token: 0x0600400E RID: 16398 RVA: 0x000F291D File Offset: 0x000F0B1D
		[ConfigurationProperty("allowInsecureTransport", DefaultValue = false)]
		public bool AllowInsecureTransport
		{
			get
			{
				return (bool)base["allowInsecureTransport"];
			}
			set
			{
				base["allowInsecureTransport"] = value;
			}
		}

		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x0600400F RID: 16399 RVA: 0x000F2930 File Offset: 0x000F0B30
		// (set) Token: 0x06004010 RID: 16400 RVA: 0x000F2942 File Offset: 0x000F0B42
		[ConfigurationProperty("keyEntropyMode", DefaultValue = SecurityKeyEntropyMode.CombinedEntropy)]
		[ServiceModelEnumValidator(typeof(SecurityKeyEntropyModeHelper))]
		public SecurityKeyEntropyMode KeyEntropyMode
		{
			get
			{
				return (SecurityKeyEntropyMode)base["keyEntropyMode"];
			}
			set
			{
				base["keyEntropyMode"] = value;
			}
		}

		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x06004011 RID: 16401 RVA: 0x000F2955 File Offset: 0x000F0B55
		[ConfigurationProperty("issuedTokenParameters")]
		public IssuedTokenParametersElement IssuedTokenParameters
		{
			get
			{
				return (IssuedTokenParametersElement)base["issuedTokenParameters"];
			}
		}

		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x06004012 RID: 16402 RVA: 0x000F2967 File Offset: 0x000F0B67
		[ConfigurationProperty("localClientSettings")]
		public LocalClientSecuritySettingsElement LocalClientSettings
		{
			get
			{
				return (LocalClientSecuritySettingsElement)base["localClientSettings"];
			}
		}

		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x06004013 RID: 16403 RVA: 0x000F2979 File Offset: 0x000F0B79
		[ConfigurationProperty("localServiceSettings")]
		public LocalServiceSecuritySettingsElement LocalServiceSettings
		{
			get
			{
				return (LocalServiceSecuritySettingsElement)base["localServiceSettings"];
			}
		}

		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x06004014 RID: 16404 RVA: 0x000F298B File Offset: 0x000F0B8B
		// (set) Token: 0x06004015 RID: 16405 RVA: 0x000F299D File Offset: 0x000F0B9D
		[ConfigurationProperty("messageProtectionOrder", DefaultValue = MessageProtectionOrder.SignBeforeEncryptAndEncryptSignature)]
		[ServiceModelEnumValidator(typeof(MessageProtectionOrderHelper))]
		public MessageProtectionOrder MessageProtectionOrder
		{
			get
			{
				return (MessageProtectionOrder)base["messageProtectionOrder"];
			}
			set
			{
				base["messageProtectionOrder"] = value;
			}
		}

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x06004016 RID: 16406 RVA: 0x000F29B0 File Offset: 0x000F0BB0
		// (set) Token: 0x06004017 RID: 16407 RVA: 0x000F29C2 File Offset: 0x000F0BC2
		[ConfigurationProperty("protectTokens", DefaultValue = false)]
		public bool ProtectTokens
		{
			get
			{
				return (bool)base["protectTokens"];
			}
			set
			{
				base["protectTokens"] = value;
			}
		}

		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x06004018 RID: 16408 RVA: 0x000F29D5 File Offset: 0x000F0BD5
		// (set) Token: 0x06004019 RID: 16409 RVA: 0x000F29E7 File Offset: 0x000F0BE7
		[ConfigurationProperty("messageSecurityVersion", DefaultValue = "Default")]
		[TypeConverter(typeof(MessageSecurityVersionConverter))]
		public MessageSecurityVersion MessageSecurityVersion
		{
			get
			{
				return (MessageSecurityVersion)base["messageSecurityVersion"];
			}
			set
			{
				base["messageSecurityVersion"] = value;
			}
		}

		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x0600401A RID: 16410 RVA: 0x000F29F5 File Offset: 0x000F0BF5
		// (set) Token: 0x0600401B RID: 16411 RVA: 0x000F2A07 File Offset: 0x000F0C07
		[ConfigurationProperty("requireSecurityContextCancellation", DefaultValue = true)]
		public bool RequireSecurityContextCancellation
		{
			get
			{
				return (bool)base["requireSecurityContextCancellation"];
			}
			set
			{
				base["requireSecurityContextCancellation"] = value;
			}
		}

		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x0600401C RID: 16412 RVA: 0x000F2A1A File Offset: 0x000F0C1A
		// (set) Token: 0x0600401D RID: 16413 RVA: 0x000F2A2C File Offset: 0x000F0C2C
		[ConfigurationProperty("requireSignatureConfirmation", DefaultValue = false)]
		public bool RequireSignatureConfirmation
		{
			get
			{
				return (bool)base["requireSignatureConfirmation"];
			}
			set
			{
				base["requireSignatureConfirmation"] = value;
			}
		}

		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x0600401E RID: 16414 RVA: 0x000F2A3F File Offset: 0x000F0C3F
		// (set) Token: 0x0600401F RID: 16415 RVA: 0x000F2A51 File Offset: 0x000F0C51
		[ConfigurationProperty("canRenewSecurityContextToken", DefaultValue = true)]
		public bool CanRenewSecurityContextToken
		{
			get
			{
				return (bool)base["canRenewSecurityContextToken"];
			}
			set
			{
				base["canRenewSecurityContextToken"] = value;
			}
		}

		// Token: 0x06004020 RID: 16416 RVA: 0x000F2A64 File Offset: 0x000F0C64
		public override void ApplyConfiguration(BindingElement bindingElement)
		{
			base.ApplyConfiguration(bindingElement);
			SecurityBindingElement securityBindingElement = (SecurityBindingElement)bindingElement;
			if (base.ElementInformation.Properties["defaultAlgorithmSuite"].ValueOrigin != PropertyValueOrigin.Default)
			{
				securityBindingElement.DefaultAlgorithmSuite = this.DefaultAlgorithmSuite;
			}
			if (base.ElementInformation.Properties["includeTimestamp"].ValueOrigin != PropertyValueOrigin.Default)
			{
				securityBindingElement.IncludeTimestamp = this.IncludeTimestamp;
			}
			if (base.ElementInformation.Properties["messageSecurityVersion"].ValueOrigin != PropertyValueOrigin.Default)
			{
				securityBindingElement.MessageSecurityVersion = this.MessageSecurityVersion;
			}
			if (base.ElementInformation.Properties["keyEntropyMode"].ValueOrigin != PropertyValueOrigin.Default)
			{
				securityBindingElement.KeyEntropyMode = this.KeyEntropyMode;
			}
			if (base.ElementInformation.Properties["securityHeaderLayout"].ValueOrigin != PropertyValueOrigin.Default)
			{
				securityBindingElement.SecurityHeaderLayout = this.SecurityHeaderLayout;
			}
			if (base.ElementInformation.Properties["requireDerivedKeys"].ValueOrigin != PropertyValueOrigin.Default)
			{
				securityBindingElement.SetKeyDerivation(this.RequireDerivedKeys);
			}
			if (base.ElementInformation.Properties["allowInsecureTransport"].ValueOrigin != PropertyValueOrigin.Default)
			{
				securityBindingElement.AllowInsecureTransport = this.AllowInsecureTransport;
			}
			if (base.ElementInformation.Properties["enableUnsecuredResponse"].ValueOrigin != PropertyValueOrigin.Default)
			{
				securityBindingElement.EnableUnsecuredResponse = this.EnableUnsecuredResponse;
			}
			if (base.ElementInformation.Properties["protectTokens"].ValueOrigin != PropertyValueOrigin.Default)
			{
				securityBindingElement.ProtectTokens = this.ProtectTokens;
			}
			SymmetricSecurityBindingElement symmetricSecurityBindingElement = securityBindingElement as SymmetricSecurityBindingElement;
			if (symmetricSecurityBindingElement != null)
			{
				if (base.ElementInformation.Properties["messageProtectionOrder"].ValueOrigin != PropertyValueOrigin.Default)
				{
					symmetricSecurityBindingElement.MessageProtectionOrder = this.MessageProtectionOrder;
				}
				if (base.ElementInformation.Properties["requireSignatureConfirmation"].ValueOrigin != PropertyValueOrigin.Default)
				{
					symmetricSecurityBindingElement.RequireSignatureConfirmation = this.RequireSignatureConfirmation;
				}
				SecureConversationSecurityTokenParameters secureConversationSecurityTokenParameters = symmetricSecurityBindingElement.ProtectionTokenParameters as SecureConversationSecurityTokenParameters;
				if (secureConversationSecurityTokenParameters != null)
				{
					secureConversationSecurityTokenParameters.CanRenewSession = this.CanRenewSecurityContextToken;
				}
			}
			AsymmetricSecurityBindingElement asymmetricSecurityBindingElement = securityBindingElement as AsymmetricSecurityBindingElement;
			if (asymmetricSecurityBindingElement != null)
			{
				if (base.ElementInformation.Properties["messageProtectionOrder"].ValueOrigin != PropertyValueOrigin.Default)
				{
					asymmetricSecurityBindingElement.MessageProtectionOrder = this.MessageProtectionOrder;
				}
				if (base.ElementInformation.Properties["requireSignatureConfirmation"].ValueOrigin != PropertyValueOrigin.Default)
				{
					asymmetricSecurityBindingElement.RequireSignatureConfirmation = this.RequireSignatureConfirmation;
				}
				if (base.ElementInformation.Properties["allowSerializedSigningTokenOnReply"].ValueOrigin != PropertyValueOrigin.Default)
				{
					asymmetricSecurityBindingElement.AllowSerializedSigningTokenOnReply = this.AllowSerializedSigningTokenOnReply;
				}
			}
			TransportSecurityBindingElement transportSecurityBindingElement = securityBindingElement as TransportSecurityBindingElement;
			if (transportSecurityBindingElement != null && transportSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing.Count == 1)
			{
				SecureConversationSecurityTokenParameters secureConversationSecurityTokenParameters2 = transportSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing[0] as SecureConversationSecurityTokenParameters;
				if (secureConversationSecurityTokenParameters2 != null)
				{
					secureConversationSecurityTokenParameters2.CanRenewSession = this.CanRenewSecurityContextToken;
				}
			}
			if (base.ElementInformation.Properties["localClientSettings"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.LocalClientSettings.ApplyConfiguration(securityBindingElement.LocalClientSettings);
			}
			if (base.ElementInformation.Properties["localServiceSettings"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.LocalServiceSettings.ApplyConfiguration(securityBindingElement.LocalServiceSettings);
			}
		}

		// Token: 0x06004021 RID: 16417 RVA: 0x000F2D84 File Offset: 0x000F0F84
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			SecurityElementBase securityElementBase = (SecurityElementBase)from;
			if (securityElementBase.ElementInformation.Properties["allowSerializedSigningTokenOnReply"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.AllowSerializedSigningTokenOnReply = securityElementBase.AllowSerializedSigningTokenOnReply;
			}
			if (securityElementBase.ElementInformation.Properties["defaultAlgorithmSuite"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.DefaultAlgorithmSuite = securityElementBase.DefaultAlgorithmSuite;
			}
			if (securityElementBase.ElementInformation.Properties["enableUnsecuredResponse"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.EnableUnsecuredResponse = securityElementBase.EnableUnsecuredResponse;
			}
			if (securityElementBase.ElementInformation.Properties["allowInsecureTransport"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.AllowInsecureTransport = securityElementBase.AllowInsecureTransport;
			}
			if (securityElementBase.ElementInformation.Properties["requireDerivedKeys"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.RequireDerivedKeys = securityElementBase.RequireDerivedKeys;
			}
			if (securityElementBase.ElementInformation.Properties["includeTimestamp"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.IncludeTimestamp = securityElementBase.IncludeTimestamp;
			}
			if (securityElementBase.ElementInformation.Properties["issuedTokenParameters"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.IssuedTokenParameters.Copy(securityElementBase.IssuedTokenParameters);
			}
			if (securityElementBase.ElementInformation.Properties["messageProtectionOrder"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.MessageProtectionOrder = securityElementBase.MessageProtectionOrder;
			}
			if (securityElementBase.ElementInformation.Properties["protectTokens"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.ProtectTokens = securityElementBase.ProtectTokens;
			}
			if (securityElementBase.ElementInformation.Properties["messageSecurityVersion"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.MessageSecurityVersion = securityElementBase.MessageSecurityVersion;
			}
			if (securityElementBase.ElementInformation.Properties["requireSignatureConfirmation"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.RequireSignatureConfirmation = securityElementBase.RequireSignatureConfirmation;
			}
			if (securityElementBase.ElementInformation.Properties["requireSecurityContextCancellation"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.RequireSecurityContextCancellation = securityElementBase.RequireSecurityContextCancellation;
			}
			if (securityElementBase.ElementInformation.Properties["canRenewSecurityContextToken"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.CanRenewSecurityContextToken = securityElementBase.CanRenewSecurityContextToken;
			}
			if (securityElementBase.ElementInformation.Properties["keyEntropyMode"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.KeyEntropyMode = securityElementBase.KeyEntropyMode;
			}
			if (securityElementBase.ElementInformation.Properties["securityHeaderLayout"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.SecurityHeaderLayout = securityElementBase.SecurityHeaderLayout;
			}
			if (securityElementBase.ElementInformation.Properties["localClientSettings"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.LocalClientSettings.CopyFrom(securityElementBase.LocalClientSettings);
			}
			if (securityElementBase.ElementInformation.Properties["localServiceSettings"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.LocalServiceSettings.CopyFrom(securityElementBase.LocalServiceSettings);
			}
			this.failedSecurityBindingElement = securityElementBase.failedSecurityBindingElement;
			this.willX509IssuerReferenceAssertionBeWritten = securityElementBase.willX509IssuerReferenceAssertionBeWritten;
		}

		// Token: 0x06004022 RID: 16418 RVA: 0x000F306E File Offset: 0x000F126E
		protected internal override BindingElement CreateBindingElement()
		{
			return this.CreateBindingElement(false);
		}

		// Token: 0x06004023 RID: 16419 RVA: 0x000F3078 File Offset: 0x000F1278
		protected internal virtual BindingElement CreateBindingElement(bool createTemplateOnly)
		{
			SecurityBindingElement securityBindingElement;
			switch (this.AuthenticationMode)
			{
			case AuthenticationMode.AnonymousForCertificate:
				securityBindingElement = SecurityBindingElement.CreateAnonymousForCertificateBindingElement();
				goto IL_1A2;
			case AuthenticationMode.AnonymousForSslNegotiated:
				securityBindingElement = SecurityBindingElement.CreateSslNegotiationBindingElement(false, this.RequireSecurityContextCancellation);
				goto IL_1A2;
			case AuthenticationMode.CertificateOverTransport:
				securityBindingElement = SecurityBindingElement.CreateCertificateOverTransportBindingElement(this.MessageSecurityVersion);
				goto IL_1A2;
			case AuthenticationMode.IssuedToken:
				securityBindingElement = SecurityBindingElement.CreateIssuedTokenBindingElement(this.IssuedTokenParameters.Create(createTemplateOnly, this.templateKeyType));
				goto IL_1A2;
			case AuthenticationMode.IssuedTokenForCertificate:
				securityBindingElement = SecurityBindingElement.CreateIssuedTokenForCertificateBindingElement(this.IssuedTokenParameters.Create(createTemplateOnly, this.templateKeyType));
				goto IL_1A2;
			case AuthenticationMode.IssuedTokenForSslNegotiated:
				securityBindingElement = SecurityBindingElement.CreateIssuedTokenForSslBindingElement(this.IssuedTokenParameters.Create(createTemplateOnly, this.templateKeyType), this.RequireSecurityContextCancellation);
				goto IL_1A2;
			case AuthenticationMode.IssuedTokenOverTransport:
				securityBindingElement = SecurityBindingElement.CreateIssuedTokenOverTransportBindingElement(this.IssuedTokenParameters.Create(createTemplateOnly, this.templateKeyType));
				goto IL_1A2;
			case AuthenticationMode.Kerberos:
				securityBindingElement = SecurityBindingElement.CreateKerberosBindingElement();
				goto IL_1A2;
			case AuthenticationMode.KerberosOverTransport:
				securityBindingElement = SecurityBindingElement.CreateKerberosOverTransportBindingElement();
				goto IL_1A2;
			case AuthenticationMode.MutualCertificate:
				securityBindingElement = SecurityBindingElement.CreateMutualCertificateBindingElement(this.MessageSecurityVersion);
				goto IL_1A2;
			case AuthenticationMode.MutualCertificateDuplex:
				securityBindingElement = SecurityBindingElement.CreateMutualCertificateDuplexBindingElement(this.MessageSecurityVersion);
				goto IL_1A2;
			case AuthenticationMode.MutualSslNegotiated:
				securityBindingElement = SecurityBindingElement.CreateSslNegotiationBindingElement(true, this.RequireSecurityContextCancellation);
				goto IL_1A2;
			case AuthenticationMode.SspiNegotiated:
				securityBindingElement = SecurityBindingElement.CreateSspiNegotiationBindingElement(this.RequireSecurityContextCancellation);
				goto IL_1A2;
			case AuthenticationMode.UserNameForCertificate:
				securityBindingElement = SecurityBindingElement.CreateUserNameForCertificateBindingElement();
				goto IL_1A2;
			case AuthenticationMode.UserNameForSslNegotiated:
				securityBindingElement = SecurityBindingElement.CreateUserNameForSslBindingElement(this.RequireSecurityContextCancellation);
				goto IL_1A2;
			case AuthenticationMode.UserNameOverTransport:
				securityBindingElement = SecurityBindingElement.CreateUserNameOverTransportBindingElement();
				goto IL_1A2;
			case AuthenticationMode.SspiNegotiatedOverTransport:
				securityBindingElement = SecurityBindingElement.CreateSspiNegotiationOverTransportBindingElement(this.RequireSecurityContextCancellation);
				goto IL_1A2;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("AuthenticationMode", (int)this.AuthenticationMode, typeof(AuthenticationMode)));
			IL_1A2:
			this.ApplyConfiguration(securityBindingElement);
			return securityBindingElement;
		}

		// Token: 0x06004024 RID: 16420 RVA: 0x000F3230 File Offset: 0x000F1430
		protected void AddBindingTemplate(Dictionary<AuthenticationMode, SecurityBindingElement> bindingTemplates, AuthenticationMode mode)
		{
			this.AuthenticationMode = mode;
			try
			{
				bindingTemplates[mode] = (SecurityBindingElement)this.CreateBindingElement(true);
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
			}
		}

		// Token: 0x06004025 RID: 16421 RVA: 0x000F3278 File Offset: 0x000F1478
		private static bool AreTokenParametersMatching(SecurityTokenParameters p1, SecurityTokenParameters p2, bool skipRequireDerivedKeysComparison, bool exactMessageSecurityVersion)
		{
			if (p1 == null || p2 == null)
			{
				return false;
			}
			if (p1.GetType() != p2.GetType())
			{
				return false;
			}
			if (p1.InclusionMode != p2.InclusionMode)
			{
				return false;
			}
			if (!skipRequireDerivedKeysComparison && p1.RequireDerivedKeys != p2.RequireDerivedKeys)
			{
				return false;
			}
			if (p1.ReferenceStyle != p2.ReferenceStyle)
			{
				return false;
			}
			if (p1 is SslSecurityTokenParameters)
			{
				if (((SslSecurityTokenParameters)p1).RequireClientCertificate != ((SslSecurityTokenParameters)p2).RequireClientCertificate)
				{
					return false;
				}
			}
			else if (p1 is SecureConversationSecurityTokenParameters)
			{
				SecureConversationSecurityTokenParameters secureConversationSecurityTokenParameters = (SecureConversationSecurityTokenParameters)p1;
				SecureConversationSecurityTokenParameters secureConversationSecurityTokenParameters2 = (SecureConversationSecurityTokenParameters)p2;
				if (secureConversationSecurityTokenParameters.RequireCancellation != secureConversationSecurityTokenParameters2.RequireCancellation)
				{
					return false;
				}
				if (secureConversationSecurityTokenParameters.CanRenewSession != secureConversationSecurityTokenParameters2.CanRenewSession)
				{
					return false;
				}
				if (!SecurityElementBase.AreBindingsMatching(secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement, secureConversationSecurityTokenParameters2.BootstrapSecurityBindingElement, exactMessageSecurityVersion))
				{
					return false;
				}
			}
			else if (p1 is IssuedSecurityTokenParameters && ((IssuedSecurityTokenParameters)p1).KeyType != ((IssuedSecurityTokenParameters)p2).KeyType)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06004026 RID: 16422 RVA: 0x000F3368 File Offset: 0x000F1568
		private static bool AreTokenParameterCollectionsMatching(Collection<SecurityTokenParameters> c1, Collection<SecurityTokenParameters> c2, bool exactMessageSecurityVersion)
		{
			if (c1.Count != c2.Count)
			{
				return false;
			}
			for (int i = 0; i < c1.Count; i++)
			{
				if (!SecurityElementBase.AreTokenParametersMatching(c1[i], c2[i], true, exactMessageSecurityVersion))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004027 RID: 16423 RVA: 0x000F33B0 File Offset: 0x000F15B0
		internal static bool AreBindingsMatching(SecurityBindingElement b1, SecurityBindingElement b2)
		{
			return SecurityElementBase.AreBindingsMatching(b1, b2, true);
		}

		// Token: 0x06004028 RID: 16424 RVA: 0x000F33BC File Offset: 0x000F15BC
		internal static bool AreBindingsMatching(SecurityBindingElement b1, SecurityBindingElement b2, bool exactMessageSecurityVersion)
		{
			if (b1 == null || b2 == null)
			{
				return b1 == b2;
			}
			if (b1.GetType() != b2.GetType())
			{
				return false;
			}
			if (b1.MessageSecurityVersion != b2.MessageSecurityVersion)
			{
				if (exactMessageSecurityVersion)
				{
					return false;
				}
				if (b1.MessageSecurityVersion.SecurityVersion != b2.MessageSecurityVersion.SecurityVersion || b1.MessageSecurityVersion.TrustVersion != b2.MessageSecurityVersion.TrustVersion || b1.MessageSecurityVersion.SecureConversationVersion != b2.MessageSecurityVersion.SecureConversationVersion || b1.MessageSecurityVersion.SecurityPolicyVersion != b2.MessageSecurityVersion.SecurityPolicyVersion)
				{
					return false;
				}
			}
			if (b1.SecurityHeaderLayout != b2.SecurityHeaderLayout)
			{
				return false;
			}
			if (b1.DefaultAlgorithmSuite != b2.DefaultAlgorithmSuite)
			{
				return false;
			}
			if (b1.IncludeTimestamp != b2.IncludeTimestamp)
			{
				return false;
			}
			if (b1.SecurityHeaderLayout != b2.SecurityHeaderLayout)
			{
				return false;
			}
			if (b1.KeyEntropyMode != b2.KeyEntropyMode)
			{
				return false;
			}
			if (!SecurityElementBase.AreTokenParameterCollectionsMatching(b1.EndpointSupportingTokenParameters.Endorsing, b2.EndpointSupportingTokenParameters.Endorsing, exactMessageSecurityVersion))
			{
				return false;
			}
			if (!SecurityElementBase.AreTokenParameterCollectionsMatching(b1.EndpointSupportingTokenParameters.SignedEncrypted, b2.EndpointSupportingTokenParameters.SignedEncrypted, exactMessageSecurityVersion))
			{
				return false;
			}
			if (!SecurityElementBase.AreTokenParameterCollectionsMatching(b1.EndpointSupportingTokenParameters.Signed, b2.EndpointSupportingTokenParameters.Signed, exactMessageSecurityVersion))
			{
				return false;
			}
			if (!SecurityElementBase.AreTokenParameterCollectionsMatching(b1.EndpointSupportingTokenParameters.SignedEndorsing, b2.EndpointSupportingTokenParameters.SignedEndorsing, exactMessageSecurityVersion))
			{
				return false;
			}
			if (b1.OperationSupportingTokenParameters.Count != b2.OperationSupportingTokenParameters.Count)
			{
				return false;
			}
			foreach (KeyValuePair<string, SupportingTokenParameters> keyValuePair in b1.OperationSupportingTokenParameters)
			{
				if (!b2.OperationSupportingTokenParameters.ContainsKey(keyValuePair.Key))
				{
					return false;
				}
				SupportingTokenParameters supportingTokenParameters = b2.OperationSupportingTokenParameters[keyValuePair.Key];
				if (!SecurityElementBase.AreTokenParameterCollectionsMatching(keyValuePair.Value.Endorsing, supportingTokenParameters.Endorsing, exactMessageSecurityVersion))
				{
					return false;
				}
				if (!SecurityElementBase.AreTokenParameterCollectionsMatching(keyValuePair.Value.SignedEncrypted, supportingTokenParameters.SignedEncrypted, exactMessageSecurityVersion))
				{
					return false;
				}
				if (!SecurityElementBase.AreTokenParameterCollectionsMatching(keyValuePair.Value.Signed, supportingTokenParameters.Signed, exactMessageSecurityVersion))
				{
					return false;
				}
				if (!SecurityElementBase.AreTokenParameterCollectionsMatching(keyValuePair.Value.SignedEndorsing, supportingTokenParameters.SignedEndorsing, exactMessageSecurityVersion))
				{
					return false;
				}
			}
			SymmetricSecurityBindingElement symmetricSecurityBindingElement = b1 as SymmetricSecurityBindingElement;
			if (symmetricSecurityBindingElement != null)
			{
				SymmetricSecurityBindingElement symmetricSecurityBindingElement2 = (SymmetricSecurityBindingElement)b2;
				if (symmetricSecurityBindingElement.MessageProtectionOrder != symmetricSecurityBindingElement2.MessageProtectionOrder)
				{
					return false;
				}
				if (!SecurityElementBase.AreTokenParametersMatching(symmetricSecurityBindingElement.ProtectionTokenParameters, symmetricSecurityBindingElement2.ProtectionTokenParameters, false, exactMessageSecurityVersion))
				{
					return false;
				}
			}
			AsymmetricSecurityBindingElement asymmetricSecurityBindingElement = b1 as AsymmetricSecurityBindingElement;
			if (asymmetricSecurityBindingElement != null)
			{
				AsymmetricSecurityBindingElement asymmetricSecurityBindingElement2 = (AsymmetricSecurityBindingElement)b2;
				if (asymmetricSecurityBindingElement.MessageProtectionOrder != asymmetricSecurityBindingElement2.MessageProtectionOrder)
				{
					return false;
				}
				if (asymmetricSecurityBindingElement.RequireSignatureConfirmation != asymmetricSecurityBindingElement2.RequireSignatureConfirmation)
				{
					return false;
				}
				if (!SecurityElementBase.AreTokenParametersMatching(asymmetricSecurityBindingElement.InitiatorTokenParameters, asymmetricSecurityBindingElement2.InitiatorTokenParameters, true, exactMessageSecurityVersion) || !SecurityElementBase.AreTokenParametersMatching(asymmetricSecurityBindingElement.RecipientTokenParameters, asymmetricSecurityBindingElement2.RecipientTokenParameters, true, exactMessageSecurityVersion))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004029 RID: 16425 RVA: 0x000F36E4 File Offset: 0x000F18E4
		protected virtual void AddBindingTemplates(Dictionary<AuthenticationMode, SecurityBindingElement> bindingTemplates)
		{
			this.AddBindingTemplate(bindingTemplates, AuthenticationMode.AnonymousForCertificate);
			this.AddBindingTemplate(bindingTemplates, AuthenticationMode.AnonymousForSslNegotiated);
			this.AddBindingTemplate(bindingTemplates, AuthenticationMode.CertificateOverTransport);
			if (this.templateKeyType == SecurityKeyType.SymmetricKey)
			{
				this.AddBindingTemplate(bindingTemplates, AuthenticationMode.IssuedToken);
			}
			this.AddBindingTemplate(bindingTemplates, AuthenticationMode.IssuedTokenForCertificate);
			this.AddBindingTemplate(bindingTemplates, AuthenticationMode.IssuedTokenForSslNegotiated);
			this.AddBindingTemplate(bindingTemplates, AuthenticationMode.IssuedTokenOverTransport);
			this.AddBindingTemplate(bindingTemplates, AuthenticationMode.Kerberos);
			this.AddBindingTemplate(bindingTemplates, AuthenticationMode.KerberosOverTransport);
			this.AddBindingTemplate(bindingTemplates, AuthenticationMode.MutualCertificate);
			this.AddBindingTemplate(bindingTemplates, AuthenticationMode.MutualCertificateDuplex);
			this.AddBindingTemplate(bindingTemplates, AuthenticationMode.MutualSslNegotiated);
			this.AddBindingTemplate(bindingTemplates, AuthenticationMode.SspiNegotiated);
			this.AddBindingTemplate(bindingTemplates, AuthenticationMode.UserNameForCertificate);
			this.AddBindingTemplate(bindingTemplates, AuthenticationMode.UserNameForSslNegotiated);
			this.AddBindingTemplate(bindingTemplates, AuthenticationMode.UserNameOverTransport);
			this.AddBindingTemplate(bindingTemplates, AuthenticationMode.SspiNegotiatedOverTransport);
		}

		// Token: 0x0600402A RID: 16426 RVA: 0x000F378C File Offset: 0x000F198C
		private bool TryInitializeAuthenticationMode(SecurityBindingElement sbe)
		{
			bool result;
			if (sbe.OperationSupportingTokenParameters.Count > 0)
			{
				result = false;
			}
			else
			{
				this.SetIssuedTokenKeyType(sbe);
				Dictionary<AuthenticationMode, SecurityBindingElement> dictionary = new Dictionary<AuthenticationMode, SecurityBindingElement>();
				this.AddBindingTemplates(dictionary);
				result = false;
				foreach (AuthenticationMode authenticationMode in dictionary.Keys)
				{
					SecurityBindingElement b = dictionary[authenticationMode];
					if (SecurityElementBase.AreBindingsMatching(sbe, b))
					{
						this.AuthenticationMode = authenticationMode;
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x0600402B RID: 16427 RVA: 0x000F3820 File Offset: 0x000F1A20
		private void SetIssuedTokenKeyType(SecurityBindingElement sbe)
		{
			if (sbe.EndpointSupportingTokenParameters.Endorsing.Count > 0 && sbe.EndpointSupportingTokenParameters.Endorsing[0] is IssuedSecurityTokenParameters)
			{
				this.templateKeyType = ((IssuedSecurityTokenParameters)sbe.EndpointSupportingTokenParameters.Endorsing[0]).KeyType;
				return;
			}
			if (sbe.EndpointSupportingTokenParameters.Signed.Count > 0 && sbe.EndpointSupportingTokenParameters.Signed[0] is IssuedSecurityTokenParameters)
			{
				this.templateKeyType = ((IssuedSecurityTokenParameters)sbe.EndpointSupportingTokenParameters.Signed[0]).KeyType;
				return;
			}
			if (sbe.EndpointSupportingTokenParameters.SignedEncrypted.Count > 0 && sbe.EndpointSupportingTokenParameters.SignedEncrypted[0] is IssuedSecurityTokenParameters)
			{
				this.templateKeyType = ((IssuedSecurityTokenParameters)sbe.EndpointSupportingTokenParameters.SignedEncrypted[0]).KeyType;
				return;
			}
			this.templateKeyType = SecurityKeyType.SymmetricKey;
		}

		// Token: 0x0600402C RID: 16428 RVA: 0x000F391C File Offset: 0x000F1B1C
		protected virtual void InitializeNestedTokenParameterSettings(SecurityTokenParameters sp, bool initializeNestedBindings)
		{
			if (sp is SspiSecurityTokenParameters)
			{
				base.SetPropertyValueIfNotDefaultValue<bool>("requireSecurityContextCancellation", ((SspiSecurityTokenParameters)sp).RequireCancellation);
				return;
			}
			if (sp is SslSecurityTokenParameters)
			{
				base.SetPropertyValueIfNotDefaultValue<bool>("requireSecurityContextCancellation", ((SslSecurityTokenParameters)sp).RequireCancellation);
				return;
			}
			if (sp is IssuedSecurityTokenParameters)
			{
				this.IssuedTokenParameters.InitializeFrom((IssuedSecurityTokenParameters)sp, initializeNestedBindings);
			}
		}

		// Token: 0x0600402D RID: 16429 RVA: 0x000F3984 File Offset: 0x000F1B84
		internal void InitializeFrom(BindingElement bindingElement, bool initializeNestedBindings)
		{
			if (bindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("bindingElement");
			}
			SecurityBindingElement securityBindingElement = (SecurityBindingElement)bindingElement;
			this.DefaultAlgorithmSuite = securityBindingElement.DefaultAlgorithmSuite;
			this.IncludeTimestamp = securityBindingElement.IncludeTimestamp;
			if (securityBindingElement.MessageSecurityVersion != MessageSecurityVersion.Default)
			{
				this.MessageSecurityVersion = securityBindingElement.MessageSecurityVersion;
			}
			base.SetPropertyValueIfNotDefaultValue<SecurityKeyEntropyMode>("keyEntropyMode", securityBindingElement.KeyEntropyMode);
			base.SetPropertyValueIfNotDefaultValue<SecurityHeaderLayout>("securityHeaderLayout", securityBindingElement.SecurityHeaderLayout);
			base.SetPropertyValueIfNotDefaultValue<bool>("protectTokens", securityBindingElement.ProtectTokens);
			base.SetPropertyValueIfNotDefaultValue<bool>("allowInsecureTransport", securityBindingElement.AllowInsecureTransport);
			base.SetPropertyValueIfNotDefaultValue<bool>("enableUnsecuredResponse", securityBindingElement.EnableUnsecuredResponse);
			bool? flag = null;
			if (securityBindingElement.EndpointSupportingTokenParameters.Endorsing.Count == 1)
			{
				this.InitializeNestedTokenParameterSettings(securityBindingElement.EndpointSupportingTokenParameters.Endorsing[0], initializeNestedBindings);
			}
			else if (securityBindingElement.EndpointSupportingTokenParameters.SignedEncrypted.Count == 1)
			{
				this.InitializeNestedTokenParameterSettings(securityBindingElement.EndpointSupportingTokenParameters.SignedEncrypted[0], initializeNestedBindings);
			}
			else if (securityBindingElement.EndpointSupportingTokenParameters.Signed.Count == 1)
			{
				this.InitializeNestedTokenParameterSettings(securityBindingElement.EndpointSupportingTokenParameters.Signed[0], initializeNestedBindings);
			}
			bool flag2 = false;
			foreach (SecurityTokenParameters securityTokenParameters in securityBindingElement.EndpointSupportingTokenParameters.Endorsing)
			{
				if (!securityTokenParameters.HasAsymmetricKey)
				{
					if (flag != null && flag.Value != securityTokenParameters.RequireDerivedKeys)
					{
						flag2 = true;
					}
					else
					{
						flag = new bool?(securityTokenParameters.RequireDerivedKeys);
					}
				}
			}
			SymmetricSecurityBindingElement symmetricSecurityBindingElement = securityBindingElement as SymmetricSecurityBindingElement;
			if (symmetricSecurityBindingElement != null)
			{
				base.SetPropertyValueIfNotDefaultValue<MessageProtectionOrder>("messageProtectionOrder", symmetricSecurityBindingElement.MessageProtectionOrder);
				this.RequireSignatureConfirmation = symmetricSecurityBindingElement.RequireSignatureConfirmation;
				if (symmetricSecurityBindingElement.ProtectionTokenParameters != null)
				{
					this.InitializeNestedTokenParameterSettings(symmetricSecurityBindingElement.ProtectionTokenParameters, initializeNestedBindings);
					if (flag != null && flag.Value != symmetricSecurityBindingElement.ProtectionTokenParameters.RequireDerivedKeys)
					{
						flag2 = true;
					}
					else
					{
						flag = new bool?(symmetricSecurityBindingElement.ProtectionTokenParameters.RequireDerivedKeys);
					}
				}
			}
			else
			{
				AsymmetricSecurityBindingElement asymmetricSecurityBindingElement = securityBindingElement as AsymmetricSecurityBindingElement;
				if (asymmetricSecurityBindingElement != null)
				{
					base.SetPropertyValueIfNotDefaultValue<MessageProtectionOrder>("messageProtectionOrder", asymmetricSecurityBindingElement.MessageProtectionOrder);
					this.RequireSignatureConfirmation = asymmetricSecurityBindingElement.RequireSignatureConfirmation;
					if (asymmetricSecurityBindingElement.InitiatorTokenParameters != null)
					{
						this.InitializeNestedTokenParameterSettings(asymmetricSecurityBindingElement.InitiatorTokenParameters, initializeNestedBindings);
						if (flag != null && flag.Value != asymmetricSecurityBindingElement.InitiatorTokenParameters.RequireDerivedKeys)
						{
							flag2 = true;
						}
						else
						{
							flag = new bool?(asymmetricSecurityBindingElement.InitiatorTokenParameters.RequireDerivedKeys);
						}
					}
				}
			}
			this.willX509IssuerReferenceAssertionBeWritten = this.DoesSecurityBindingElementContainClauseTypeofIssuerSerial(securityBindingElement);
			this.RequireDerivedKeys = flag.GetValueOrDefault(true);
			this.LocalClientSettings.InitializeFrom(securityBindingElement.LocalClientSettings);
			this.LocalServiceSettings.InitializeFrom(securityBindingElement.LocalServiceSettings);
			if (!flag2)
			{
				flag2 = !this.TryInitializeAuthenticationMode(securityBindingElement);
			}
			if (flag2)
			{
				this.failedSecurityBindingElement = securityBindingElement;
			}
		}

		// Token: 0x0600402E RID: 16430 RVA: 0x000F3C7C File Offset: 0x000F1E7C
		protected internal override void InitializeFrom(BindingElement bindingElement)
		{
			this.InitializeFrom(bindingElement, true);
		}

		// Token: 0x0600402F RID: 16431 RVA: 0x000F3C88 File Offset: 0x000F1E88
		private bool DoesSecurityBindingElementContainClauseTypeofIssuerSerial(SecurityBindingElement sbe)
		{
			if (sbe == null)
			{
				return false;
			}
			if (sbe is SymmetricSecurityBindingElement)
			{
				X509SecurityTokenParameters x509SecurityTokenParameters = ((SymmetricSecurityBindingElement)sbe).ProtectionTokenParameters as X509SecurityTokenParameters;
				if (x509SecurityTokenParameters != null && x509SecurityTokenParameters.X509ReferenceStyle == X509KeyIdentifierClauseType.IssuerSerial)
				{
					return true;
				}
			}
			else if (sbe is AsymmetricSecurityBindingElement)
			{
				X509SecurityTokenParameters x509SecurityTokenParameters2 = ((AsymmetricSecurityBindingElement)sbe).InitiatorTokenParameters as X509SecurityTokenParameters;
				if (x509SecurityTokenParameters2 != null && x509SecurityTokenParameters2.X509ReferenceStyle == X509KeyIdentifierClauseType.IssuerSerial)
				{
					return true;
				}
				X509SecurityTokenParameters x509SecurityTokenParameters3 = ((AsymmetricSecurityBindingElement)sbe).RecipientTokenParameters as X509SecurityTokenParameters;
				if (x509SecurityTokenParameters3 != null && x509SecurityTokenParameters3.X509ReferenceStyle == X509KeyIdentifierClauseType.IssuerSerial)
				{
					return true;
				}
			}
			return this.DoesX509TokenParametersContainClauseTypeofIssuerSerial(sbe.EndpointSupportingTokenParameters.Endorsing) || this.DoesX509TokenParametersContainClauseTypeofIssuerSerial(sbe.EndpointSupportingTokenParameters.Signed) || this.DoesX509TokenParametersContainClauseTypeofIssuerSerial(sbe.EndpointSupportingTokenParameters.SignedEncrypted) || this.DoesX509TokenParametersContainClauseTypeofIssuerSerial(sbe.EndpointSupportingTokenParameters.SignedEndorsing) || this.DoesX509TokenParametersContainClauseTypeofIssuerSerial(sbe.OptionalEndpointSupportingTokenParameters.Endorsing) || this.DoesX509TokenParametersContainClauseTypeofIssuerSerial(sbe.OptionalEndpointSupportingTokenParameters.Signed) || this.DoesX509TokenParametersContainClauseTypeofIssuerSerial(sbe.OptionalEndpointSupportingTokenParameters.SignedEncrypted) || this.DoesX509TokenParametersContainClauseTypeofIssuerSerial(sbe.OptionalEndpointSupportingTokenParameters.SignedEndorsing);
		}

		// Token: 0x06004030 RID: 16432 RVA: 0x000F3DB0 File Offset: 0x000F1FB0
		private bool DoesX509TokenParametersContainClauseTypeofIssuerSerial(Collection<SecurityTokenParameters> tokenParameters)
		{
			foreach (SecurityTokenParameters securityTokenParameters in tokenParameters)
			{
				X509SecurityTokenParameters x509SecurityTokenParameters = securityTokenParameters as X509SecurityTokenParameters;
				if (x509SecurityTokenParameters != null && x509SecurityTokenParameters.X509ReferenceStyle == X509KeyIdentifierClauseType.IssuerSerial)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004031 RID: 16433 RVA: 0x000F3E0C File Offset: 0x000F200C
		protected override bool SerializeToXmlElement(XmlWriter writer, string elementName)
		{
			bool result;
			if (this.failedSecurityBindingElement != null && writer != null)
			{
				writer.WriteComment(SR.GetString("ConfigurationSchemaInsuffientForSecurityBindingElementInstance"));
				writer.WriteComment(this.failedSecurityBindingElement.ToString());
				result = true;
			}
			else
			{
				if (writer != null && this.willX509IssuerReferenceAssertionBeWritten)
				{
					writer.WriteComment(SR.GetString("ConfigurationSchemaContainsX509IssuerSerialReference"));
				}
				result = base.SerializeToXmlElement(writer, elementName);
			}
			return result;
		}

		// Token: 0x06004032 RID: 16434 RVA: 0x000F3E70 File Offset: 0x000F2070
		protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			bool result = base.SerializeElement(writer, serializeCollectionKey);
			Func<PropertyInformation, bool> predicate = (PropertyInformation property) => property.ValueOrigin == PropertyValueOrigin.SetHere;
			if (this.IsSecurityElementBootstrap && !base.ElementInformation.Properties.OfType<PropertyInformation>().Any(predicate))
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06004033 RID: 16435 RVA: 0x000F3EC9 File Offset: 0x000F20C9
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			if (sourceElement is SecurityElementBase)
			{
				this.failedSecurityBindingElement = ((SecurityElementBase)sourceElement).failedSecurityBindingElement;
				this.willX509IssuerReferenceAssertionBeWritten = ((SecurityElementBase)sourceElement).willX509IssuerReferenceAssertionBeWritten;
			}
			base.Unmerge(sourceElement, parentElement, saveMode);
		}

		// Token: 0x04002CC5 RID: 11461
		private ConfigurationPropertyCollection properties;

		// Token: 0x04002CC6 RID: 11462
		internal const AuthenticationMode defaultAuthenticationMode = AuthenticationMode.SspiNegotiated;

		// Token: 0x04002CC7 RID: 11463
		private SecurityBindingElement failedSecurityBindingElement;

		// Token: 0x04002CC8 RID: 11464
		private bool willX509IssuerReferenceAssertionBeWritten;

		// Token: 0x04002CC9 RID: 11465
		private SecurityKeyType templateKeyType;
	}
}
