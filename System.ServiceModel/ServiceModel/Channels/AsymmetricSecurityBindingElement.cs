using System;
using System.Globalization;
using System.Net.Security;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Text;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200098C RID: 2444
	public sealed class AsymmetricSecurityBindingElement : SecurityBindingElement, IPolicyExportExtension
	{
		// Token: 0x06005EC2 RID: 24258 RVA: 0x0015E704 File Offset: 0x0015C904
		private AsymmetricSecurityBindingElement(AsymmetricSecurityBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
			if (elementToBeCloned.initiatorTokenParameters != null)
			{
				this.initiatorTokenParameters = elementToBeCloned.initiatorTokenParameters.Clone();
			}
			this.messageProtectionOrder = elementToBeCloned.messageProtectionOrder;
			if (elementToBeCloned.recipientTokenParameters != null)
			{
				this.recipientTokenParameters = elementToBeCloned.recipientTokenParameters.Clone();
			}
			this.requireSignatureConfirmation = elementToBeCloned.requireSignatureConfirmation;
			this.allowSerializedSigningTokenOnReply = elementToBeCloned.allowSerializedSigningTokenOnReply;
			this.isCertificateSignatureBinding = elementToBeCloned.isCertificateSignatureBinding;
		}

		// Token: 0x06005EC3 RID: 24259 RVA: 0x0015E77A File Offset: 0x0015C97A
		public AsymmetricSecurityBindingElement() : this(null, null)
		{
		}

		// Token: 0x06005EC4 RID: 24260 RVA: 0x0015E784 File Offset: 0x0015C984
		public AsymmetricSecurityBindingElement(SecurityTokenParameters recipientTokenParameters) : this(recipientTokenParameters, null)
		{
		}

		// Token: 0x06005EC5 RID: 24261 RVA: 0x0015E78E File Offset: 0x0015C98E
		public AsymmetricSecurityBindingElement(SecurityTokenParameters recipientTokenParameters, SecurityTokenParameters initiatorTokenParameters) : this(recipientTokenParameters, initiatorTokenParameters, false)
		{
		}

		// Token: 0x06005EC6 RID: 24262 RVA: 0x0015E799 File Offset: 0x0015C999
		internal AsymmetricSecurityBindingElement(SecurityTokenParameters recipientTokenParameters, SecurityTokenParameters initiatorTokenParameters, bool allowSerializedSigningTokenOnReply)
		{
			this.messageProtectionOrder = MessageProtectionOrder.SignBeforeEncryptAndEncryptSignature;
			this.requireSignatureConfirmation = false;
			this.initiatorTokenParameters = initiatorTokenParameters;
			this.recipientTokenParameters = recipientTokenParameters;
			this.allowSerializedSigningTokenOnReply = allowSerializedSigningTokenOnReply;
			this.isCertificateSignatureBinding = false;
		}

		// Token: 0x170016C1 RID: 5825
		// (get) Token: 0x06005EC7 RID: 24263 RVA: 0x0015E7CB File Offset: 0x0015C9CB
		// (set) Token: 0x06005EC8 RID: 24264 RVA: 0x0015E7D3 File Offset: 0x0015C9D3
		public bool AllowSerializedSigningTokenOnReply
		{
			get
			{
				return this.allowSerializedSigningTokenOnReply;
			}
			set
			{
				this.allowSerializedSigningTokenOnReply = value;
			}
		}

		// Token: 0x170016C2 RID: 5826
		// (get) Token: 0x06005EC9 RID: 24265 RVA: 0x0015E7DC File Offset: 0x0015C9DC
		// (set) Token: 0x06005ECA RID: 24266 RVA: 0x0015E7E4 File Offset: 0x0015C9E4
		public SecurityTokenParameters InitiatorTokenParameters
		{
			get
			{
				return this.initiatorTokenParameters;
			}
			set
			{
				this.initiatorTokenParameters = value;
			}
		}

		// Token: 0x170016C3 RID: 5827
		// (get) Token: 0x06005ECB RID: 24267 RVA: 0x0015E7ED File Offset: 0x0015C9ED
		// (set) Token: 0x06005ECC RID: 24268 RVA: 0x0015E7F5 File Offset: 0x0015C9F5
		public MessageProtectionOrder MessageProtectionOrder
		{
			get
			{
				return this.messageProtectionOrder;
			}
			set
			{
				if (!MessageProtectionOrderHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.messageProtectionOrder = value;
			}
		}

		// Token: 0x170016C4 RID: 5828
		// (get) Token: 0x06005ECD RID: 24269 RVA: 0x0015E81B File Offset: 0x0015CA1B
		// (set) Token: 0x06005ECE RID: 24270 RVA: 0x0015E823 File Offset: 0x0015CA23
		public SecurityTokenParameters RecipientTokenParameters
		{
			get
			{
				return this.recipientTokenParameters;
			}
			set
			{
				this.recipientTokenParameters = value;
			}
		}

		// Token: 0x170016C5 RID: 5829
		// (get) Token: 0x06005ECF RID: 24271 RVA: 0x0015E82C File Offset: 0x0015CA2C
		// (set) Token: 0x06005ED0 RID: 24272 RVA: 0x0015E834 File Offset: 0x0015CA34
		public bool RequireSignatureConfirmation
		{
			get
			{
				return this.requireSignatureConfirmation;
			}
			set
			{
				this.requireSignatureConfirmation = value;
			}
		}

		// Token: 0x06005ED1 RID: 24273 RVA: 0x0015E840 File Offset: 0x0015CA40
		internal override ISecurityCapabilities GetIndividualISecurityCapabilities()
		{
			ProtectionLevel requestProtectionLevel = ProtectionLevel.EncryptAndSign;
			ProtectionLevel responseProtectionLevel = ProtectionLevel.EncryptAndSign;
			bool supportsServerAuth = false;
			if (this.IsCertificateSignatureBinding)
			{
				requestProtectionLevel = ProtectionLevel.Sign;
				responseProtectionLevel = ProtectionLevel.None;
			}
			else if (this.RecipientTokenParameters != null)
			{
				supportsServerAuth = this.RecipientTokenParameters.SupportsServerAuthentication;
			}
			bool flag;
			bool flag2;
			base.GetSupportingTokensCapabilities(out flag, out flag2);
			if (this.InitiatorTokenParameters != null)
			{
				flag = (flag || this.InitiatorTokenParameters.SupportsClientAuthentication);
				flag2 = (flag2 || this.InitiatorTokenParameters.SupportsClientWindowsIdentity);
			}
			return new SecurityCapabilities(flag, supportsServerAuth, flag2, requestProtectionLevel, responseProtectionLevel);
		}

		// Token: 0x170016C6 RID: 5830
		// (get) Token: 0x06005ED2 RID: 24274 RVA: 0x0015E8B8 File Offset: 0x0015CAB8
		internal override bool SupportsDuplex
		{
			get
			{
				return !this.isCertificateSignatureBinding;
			}
		}

		// Token: 0x170016C7 RID: 5831
		// (get) Token: 0x06005ED3 RID: 24275 RVA: 0x0015E8C3 File Offset: 0x0015CAC3
		internal override bool SupportsRequestReply
		{
			get
			{
				return !this.isCertificateSignatureBinding;
			}
		}

		// Token: 0x170016C8 RID: 5832
		// (get) Token: 0x06005ED4 RID: 24276 RVA: 0x0015E8CE File Offset: 0x0015CACE
		// (set) Token: 0x06005ED5 RID: 24277 RVA: 0x0015E8D6 File Offset: 0x0015CAD6
		internal bool IsCertificateSignatureBinding
		{
			get
			{
				return this.isCertificateSignatureBinding;
			}
			set
			{
				this.isCertificateSignatureBinding = value;
			}
		}

		// Token: 0x06005ED6 RID: 24278 RVA: 0x0015E8DF File Offset: 0x0015CADF
		public override void SetKeyDerivation(bool requireDerivedKeys)
		{
			base.SetKeyDerivation(requireDerivedKeys);
			if (this.initiatorTokenParameters != null)
			{
				this.initiatorTokenParameters.RequireDerivedKeys = requireDerivedKeys;
			}
			if (this.recipientTokenParameters != null)
			{
				this.recipientTokenParameters.RequireDerivedKeys = requireDerivedKeys;
			}
		}

		// Token: 0x06005ED7 RID: 24279 RVA: 0x0015E910 File Offset: 0x0015CB10
		internal override bool IsSetKeyDerivation(bool requireDerivedKeys)
		{
			return base.IsSetKeyDerivation(requireDerivedKeys) && (this.initiatorTokenParameters == null || this.initiatorTokenParameters.RequireDerivedKeys == requireDerivedKeys) && (this.recipientTokenParameters == null || this.recipientTokenParameters.RequireDerivedKeys == requireDerivedKeys);
		}

		// Token: 0x06005ED8 RID: 24280 RVA: 0x0015E950 File Offset: 0x0015CB50
		private bool HasProtectionRequirements(ScopedMessagePartSpecification scopedParts)
		{
			foreach (string action in scopedParts.Actions)
			{
				MessagePartSpecification messagePartSpecification;
				if (scopedParts.TryGetParts(action, out messagePartSpecification) && !messagePartSpecification.IsEmpty())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005ED9 RID: 24281 RVA: 0x0015E9B0 File Offset: 0x0015CBB0
		internal override SecurityProtocolFactory CreateSecurityProtocolFactory<TChannel>(BindingContext context, SecurityCredentialsManager credentialsManager, bool isForService, BindingContext issuerBindingContext)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (credentialsManager == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("credentialsManager");
			}
			if (this.InitiatorTokenParameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("AsymmetricSecurityBindingElementNeedsInitiatorTokenParameters", new object[]
				{
					this.ToString()
				})));
			}
			if (this.RecipientTokenParameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("AsymmetricSecurityBindingElementNeedsRecipientTokenParameters", new object[]
				{
					this.ToString()
				})));
			}
			bool flag = !this.isCertificateSignatureBinding && (typeof(IDuplexChannel) == typeof(TChannel) || typeof(IDuplexSessionChannel) == typeof(TChannel));
			AsymmetricSecurityProtocolFactory asymmetricSecurityProtocolFactory = new AsymmetricSecurityProtocolFactory();
			asymmetricSecurityProtocolFactory.ProtectionRequirements.Add(SecurityBindingElement.ComputeProtectionRequirements(this, context.BindingParameters, context.Binding.Elements, isForService));
			asymmetricSecurityProtocolFactory.RequireConfidentiality = this.HasProtectionRequirements(asymmetricSecurityProtocolFactory.ProtectionRequirements.IncomingEncryptionParts);
			asymmetricSecurityProtocolFactory.RequireIntegrity = this.HasProtectionRequirements(asymmetricSecurityProtocolFactory.ProtectionRequirements.IncomingSignatureParts);
			if (this.isCertificateSignatureBinding)
			{
				if (isForService)
				{
					asymmetricSecurityProtocolFactory.ApplyIntegrity = (asymmetricSecurityProtocolFactory.ApplyConfidentiality = false);
				}
				else
				{
					asymmetricSecurityProtocolFactory.ApplyConfidentiality = (asymmetricSecurityProtocolFactory.RequireIntegrity = false);
				}
			}
			else
			{
				asymmetricSecurityProtocolFactory.ApplyIntegrity = this.HasProtectionRequirements(asymmetricSecurityProtocolFactory.ProtectionRequirements.OutgoingSignatureParts);
				asymmetricSecurityProtocolFactory.ApplyConfidentiality = this.HasProtectionRequirements(asymmetricSecurityProtocolFactory.ProtectionRequirements.OutgoingEncryptionParts);
			}
			if (isForService)
			{
				base.ApplyAuditBehaviorSettings(context, asymmetricSecurityProtocolFactory);
				if (asymmetricSecurityProtocolFactory.RequireConfidentiality || (!this.isCertificateSignatureBinding && asymmetricSecurityProtocolFactory.ApplyIntegrity))
				{
					asymmetricSecurityProtocolFactory.AsymmetricTokenParameters = this.RecipientTokenParameters.Clone();
				}
				else
				{
					asymmetricSecurityProtocolFactory.AsymmetricTokenParameters = null;
				}
				asymmetricSecurityProtocolFactory.CryptoTokenParameters = this.InitiatorTokenParameters.Clone();
				SecurityBindingElement.SetIssuerBindingContextIfRequired(asymmetricSecurityProtocolFactory.CryptoTokenParameters, issuerBindingContext);
			}
			else
			{
				if (asymmetricSecurityProtocolFactory.ApplyConfidentiality || (!this.isCertificateSignatureBinding && asymmetricSecurityProtocolFactory.RequireIntegrity))
				{
					asymmetricSecurityProtocolFactory.AsymmetricTokenParameters = this.RecipientTokenParameters.Clone();
				}
				else
				{
					asymmetricSecurityProtocolFactory.AsymmetricTokenParameters = null;
				}
				asymmetricSecurityProtocolFactory.CryptoTokenParameters = this.InitiatorTokenParameters.Clone();
				SecurityBindingElement.SetIssuerBindingContextIfRequired(asymmetricSecurityProtocolFactory.CryptoTokenParameters, issuerBindingContext);
			}
			if (flag)
			{
				if (isForService)
				{
					asymmetricSecurityProtocolFactory.ApplyConfidentiality = (asymmetricSecurityProtocolFactory.ApplyIntegrity = false);
				}
				else
				{
					asymmetricSecurityProtocolFactory.RequireIntegrity = (asymmetricSecurityProtocolFactory.RequireConfidentiality = false);
				}
			}
			else if (!isForService)
			{
				asymmetricSecurityProtocolFactory.AllowSerializedSigningTokenOnReply = this.AllowSerializedSigningTokenOnReply;
			}
			asymmetricSecurityProtocolFactory.IdentityVerifier = base.LocalClientSettings.IdentityVerifier;
			asymmetricSecurityProtocolFactory.DoRequestSignatureConfirmation = this.RequireSignatureConfirmation;
			asymmetricSecurityProtocolFactory.MessageProtectionOrder = this.MessageProtectionOrder;
			base.ConfigureProtocolFactory(asymmetricSecurityProtocolFactory, credentialsManager, isForService, issuerBindingContext, context.Binding);
			if (!asymmetricSecurityProtocolFactory.RequireIntegrity)
			{
				asymmetricSecurityProtocolFactory.DetectReplays = false;
			}
			SecurityProtocolFactory result;
			if (flag)
			{
				AsymmetricSecurityProtocolFactory asymmetricSecurityProtocolFactory2 = new AsymmetricSecurityProtocolFactory();
				if (isForService)
				{
					asymmetricSecurityProtocolFactory2.AsymmetricTokenParameters = this.InitiatorTokenParameters.Clone();
					asymmetricSecurityProtocolFactory2.AsymmetricTokenParameters.ReferenceStyle = SecurityTokenReferenceStyle.External;
					asymmetricSecurityProtocolFactory2.AsymmetricTokenParameters.InclusionMode = SecurityTokenInclusionMode.Never;
					asymmetricSecurityProtocolFactory2.CryptoTokenParameters = this.RecipientTokenParameters.Clone();
					asymmetricSecurityProtocolFactory2.CryptoTokenParameters.ReferenceStyle = SecurityTokenReferenceStyle.Internal;
					asymmetricSecurityProtocolFactory2.CryptoTokenParameters.InclusionMode = SecurityTokenInclusionMode.AlwaysToRecipient;
					asymmetricSecurityProtocolFactory2.IdentityVerifier = null;
				}
				else
				{
					asymmetricSecurityProtocolFactory2.AsymmetricTokenParameters = this.InitiatorTokenParameters.Clone();
					asymmetricSecurityProtocolFactory2.AsymmetricTokenParameters.ReferenceStyle = SecurityTokenReferenceStyle.External;
					asymmetricSecurityProtocolFactory2.AsymmetricTokenParameters.InclusionMode = SecurityTokenInclusionMode.Never;
					asymmetricSecurityProtocolFactory2.CryptoTokenParameters = this.RecipientTokenParameters.Clone();
					asymmetricSecurityProtocolFactory2.CryptoTokenParameters.ReferenceStyle = SecurityTokenReferenceStyle.Internal;
					asymmetricSecurityProtocolFactory2.CryptoTokenParameters.InclusionMode = SecurityTokenInclusionMode.AlwaysToRecipient;
					asymmetricSecurityProtocolFactory2.IdentityVerifier = base.LocalClientSettings.IdentityVerifier;
				}
				asymmetricSecurityProtocolFactory2.DoRequestSignatureConfirmation = this.RequireSignatureConfirmation;
				asymmetricSecurityProtocolFactory2.MessageProtectionOrder = this.MessageProtectionOrder;
				asymmetricSecurityProtocolFactory2.ProtectionRequirements.Add(SecurityBindingElement.ComputeProtectionRequirements(this, context.BindingParameters, context.Binding.Elements, isForService));
				if (isForService)
				{
					asymmetricSecurityProtocolFactory2.ApplyConfidentiality = this.HasProtectionRequirements(asymmetricSecurityProtocolFactory2.ProtectionRequirements.OutgoingEncryptionParts);
					asymmetricSecurityProtocolFactory2.ApplyIntegrity = true;
					asymmetricSecurityProtocolFactory2.RequireIntegrity = (asymmetricSecurityProtocolFactory2.RequireConfidentiality = false);
				}
				else
				{
					asymmetricSecurityProtocolFactory2.RequireConfidentiality = this.HasProtectionRequirements(asymmetricSecurityProtocolFactory2.ProtectionRequirements.IncomingEncryptionParts);
					asymmetricSecurityProtocolFactory2.RequireIntegrity = true;
					asymmetricSecurityProtocolFactory2.ApplyIntegrity = (asymmetricSecurityProtocolFactory2.ApplyConfidentiality = false);
				}
				base.ConfigureProtocolFactory(asymmetricSecurityProtocolFactory2, credentialsManager, !isForService, issuerBindingContext, context.Binding);
				if (!asymmetricSecurityProtocolFactory2.RequireIntegrity)
				{
					asymmetricSecurityProtocolFactory2.DetectReplays = false;
				}
				asymmetricSecurityProtocolFactory2.IsDuplexReply = true;
				result = new DuplexSecurityProtocolFactory
				{
					ForwardProtocolFactory = asymmetricSecurityProtocolFactory,
					ReverseProtocolFactory = asymmetricSecurityProtocolFactory2
				};
			}
			else
			{
				result = asymmetricSecurityProtocolFactory;
			}
			return result;
		}

		// Token: 0x06005EDA RID: 24282 RVA: 0x0015EE3E File Offset: 0x0015D03E
		internal override bool RequiresChannelDemuxer()
		{
			return base.RequiresChannelDemuxer() || base.RequiresChannelDemuxer(this.InitiatorTokenParameters);
		}

		// Token: 0x06005EDB RID: 24283 RVA: 0x0015EE58 File Offset: 0x0015D058
		protected override IChannelFactory<TChannel> BuildChannelFactoryCore<TChannel>(BindingContext context)
		{
			ISecurityCapabilities property = this.GetProperty<ISecurityCapabilities>(context);
			bool flag = this.RequiresChannelDemuxer();
			ChannelBuilder channelBuilder = new ChannelBuilder(context, flag);
			if (flag)
			{
				base.ApplyPropertiesOnDemuxer(channelBuilder, context);
			}
			BindingContext issuanceBindingContext = context.Clone();
			SecurityCredentialsManager securityCredentialsManager = context.BindingParameters.Find<SecurityCredentialsManager>();
			if (securityCredentialsManager == null)
			{
				securityCredentialsManager = ClientCredentials.CreateDefaultCredentials();
			}
			SecurityProtocolFactory protocolFactory = this.CreateSecurityProtocolFactory<TChannel>(context, securityCredentialsManager, false, issuanceBindingContext);
			return new SecurityChannelFactory<TChannel>(property, context, channelBuilder, protocolFactory);
		}

		// Token: 0x06005EDC RID: 24284 RVA: 0x0015EEC0 File Offset: 0x0015D0C0
		protected override IChannelListener<TChannel> BuildChannelListenerCore<TChannel>(BindingContext context)
		{
			bool flag = this.RequiresChannelDemuxer();
			ChannelBuilder channelBuilder = new ChannelBuilder(context, flag);
			if (flag)
			{
				base.ApplyPropertiesOnDemuxer(channelBuilder, context);
			}
			BindingContext issuanceBindingContext = context.Clone();
			SecurityChannelListener<TChannel> securityChannelListener = new SecurityChannelListener<TChannel>(this, context);
			SecurityCredentialsManager securityCredentialsManager = context.BindingParameters.Find<SecurityCredentialsManager>();
			if (securityCredentialsManager == null)
			{
				securityCredentialsManager = ServiceCredentials.CreateDefaultCredentials();
			}
			SecurityProtocolFactory securityProtocolFactory = this.CreateSecurityProtocolFactory<TChannel>(context, securityCredentialsManager, true, issuanceBindingContext);
			securityChannelListener.SecurityProtocolFactory = securityProtocolFactory;
			securityChannelListener.InitializeListener(channelBuilder);
			return securityChannelListener;
		}

		// Token: 0x06005EDD RID: 24285 RVA: 0x0015EF2C File Offset: 0x0015D12C
		public override T GetProperty<T>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (typeof(T) == typeof(ChannelProtectionRequirements))
			{
				AddressingVersion addressing = MessageVersion.Default.Addressing;
				MessageEncodingBindingElement messageEncodingBindingElement = context.Binding.Elements.Find<MessageEncodingBindingElement>();
				if (messageEncodingBindingElement != null)
				{
					addressing = messageEncodingBindingElement.MessageVersion.Addressing;
				}
				ChannelProtectionRequirements protectionRequirements = base.GetProtectionRequirements(addressing, base.GetIndividualProperty<ISecurityCapabilities>().SupportedRequestProtectionLevel);
				protectionRequirements.Add(context.GetInnerProperty<ChannelProtectionRequirements>() ?? new ChannelProtectionRequirements());
				return (T)((object)protectionRequirements);
			}
			return base.GetProperty<T>(context);
		}

		// Token: 0x06005EDE RID: 24286 RVA: 0x0015EFC8 File Offset: 0x0015D1C8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(base.ToString());
			stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "MessageProtectionOrder: {0}", new object[]
			{
				this.messageProtectionOrder.ToString()
			}));
			stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "RequireSignatureConfirmation: {0}", new object[]
			{
				this.requireSignatureConfirmation.ToString()
			}));
			stringBuilder.Append("InitiatorTokenParameters: ");
			if (this.initiatorTokenParameters != null)
			{
				stringBuilder.AppendLine(this.initiatorTokenParameters.ToString().Trim().Replace("\n", "\n  "));
			}
			else
			{
				stringBuilder.AppendLine("null");
			}
			stringBuilder.Append("RecipientTokenParameters: ");
			if (this.recipientTokenParameters != null)
			{
				stringBuilder.AppendLine(this.recipientTokenParameters.ToString().Trim().Replace("\n", "\n  "));
			}
			else
			{
				stringBuilder.AppendLine("null");
			}
			return stringBuilder.ToString().Trim();
		}

		// Token: 0x06005EDF RID: 24287 RVA: 0x0015F0DD File Offset: 0x0015D2DD
		public override BindingElement Clone()
		{
			return new AsymmetricSecurityBindingElement(this);
		}

		// Token: 0x06005EE0 RID: 24288 RVA: 0x0015F0E5 File Offset: 0x0015D2E5
		void IPolicyExportExtension.ExportPolicy(MetadataExporter exporter, PolicyConversionContext context)
		{
			SecurityBindingElement.ExportPolicy(exporter, context);
		}

		// Token: 0x0400380F RID: 14351
		internal const bool defaultAllowSerializedSigningTokenOnReply = false;

		// Token: 0x04003810 RID: 14352
		private bool allowSerializedSigningTokenOnReply;

		// Token: 0x04003811 RID: 14353
		private SecurityTokenParameters initiatorTokenParameters;

		// Token: 0x04003812 RID: 14354
		private MessageProtectionOrder messageProtectionOrder;

		// Token: 0x04003813 RID: 14355
		private SecurityTokenParameters recipientTokenParameters;

		// Token: 0x04003814 RID: 14356
		private bool requireSignatureConfirmation;

		// Token: 0x04003815 RID: 14357
		private bool isCertificateSignatureBinding;
	}
}
