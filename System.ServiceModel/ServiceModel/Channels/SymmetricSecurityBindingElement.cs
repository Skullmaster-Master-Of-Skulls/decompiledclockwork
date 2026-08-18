using System;
using System.Globalization;
using System.Net.Security;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Text;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000993 RID: 2451
	public sealed class SymmetricSecurityBindingElement : SecurityBindingElement, IPolicyExportExtension
	{
		// Token: 0x06005F7B RID: 24443 RVA: 0x001639A6 File Offset: 0x00161BA6
		private SymmetricSecurityBindingElement(SymmetricSecurityBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
			this.messageProtectionOrder = elementToBeCloned.messageProtectionOrder;
			if (elementToBeCloned.protectionTokenParameters != null)
			{
				this.protectionTokenParameters = elementToBeCloned.protectionTokenParameters.Clone();
			}
			this.requireSignatureConfirmation = elementToBeCloned.requireSignatureConfirmation;
		}

		// Token: 0x06005F7C RID: 24444 RVA: 0x001639E0 File Offset: 0x00161BE0
		public SymmetricSecurityBindingElement() : this(null)
		{
		}

		// Token: 0x06005F7D RID: 24445 RVA: 0x001639E9 File Offset: 0x00161BE9
		public SymmetricSecurityBindingElement(SecurityTokenParameters protectionTokenParameters)
		{
			this.messageProtectionOrder = MessageProtectionOrder.SignBeforeEncryptAndEncryptSignature;
			this.requireSignatureConfirmation = false;
			this.protectionTokenParameters = protectionTokenParameters;
		}

		// Token: 0x170016DF RID: 5855
		// (get) Token: 0x06005F7E RID: 24446 RVA: 0x00163A06 File Offset: 0x00161C06
		// (set) Token: 0x06005F7F RID: 24447 RVA: 0x00163A0E File Offset: 0x00161C0E
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

		// Token: 0x170016E0 RID: 5856
		// (get) Token: 0x06005F80 RID: 24448 RVA: 0x00163A17 File Offset: 0x00161C17
		// (set) Token: 0x06005F81 RID: 24449 RVA: 0x00163A1F File Offset: 0x00161C1F
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

		// Token: 0x170016E1 RID: 5857
		// (get) Token: 0x06005F82 RID: 24450 RVA: 0x00163A45 File Offset: 0x00161C45
		// (set) Token: 0x06005F83 RID: 24451 RVA: 0x00163A4D File Offset: 0x00161C4D
		public SecurityTokenParameters ProtectionTokenParameters
		{
			get
			{
				return this.protectionTokenParameters;
			}
			set
			{
				this.protectionTokenParameters = value;
			}
		}

		// Token: 0x06005F84 RID: 24452 RVA: 0x00163A58 File Offset: 0x00161C58
		internal override ISecurityCapabilities GetIndividualISecurityCapabilities()
		{
			bool supportsServerAuth = false;
			bool flag;
			bool flag2;
			base.GetSupportingTokensCapabilities(out flag, out flag2);
			if (this.ProtectionTokenParameters != null)
			{
				flag = (flag || this.ProtectionTokenParameters.SupportsClientAuthentication);
				flag2 = (flag2 || this.ProtectionTokenParameters.SupportsClientWindowsIdentity);
				if (this.ProtectionTokenParameters.HasAsymmetricKey)
				{
					supportsServerAuth = this.ProtectionTokenParameters.SupportsClientAuthentication;
				}
				else
				{
					supportsServerAuth = this.ProtectionTokenParameters.SupportsServerAuthentication;
				}
			}
			return new SecurityCapabilities(flag, supportsServerAuth, flag2, ProtectionLevel.EncryptAndSign, ProtectionLevel.EncryptAndSign);
		}

		// Token: 0x170016E2 RID: 5858
		// (get) Token: 0x06005F85 RID: 24453 RVA: 0x00163AD0 File Offset: 0x00161CD0
		internal override bool SessionMode
		{
			get
			{
				SecureConversationSecurityTokenParameters secureConversationSecurityTokenParameters = this.ProtectionTokenParameters as SecureConversationSecurityTokenParameters;
				return secureConversationSecurityTokenParameters != null && secureConversationSecurityTokenParameters.RequireCancellation;
			}
		}

		// Token: 0x170016E3 RID: 5859
		// (get) Token: 0x06005F86 RID: 24454 RVA: 0x00163AF4 File Offset: 0x00161CF4
		internal override bool SupportsDuplex
		{
			get
			{
				return this.SessionMode;
			}
		}

		// Token: 0x170016E4 RID: 5860
		// (get) Token: 0x06005F87 RID: 24455 RVA: 0x00163AFC File Offset: 0x00161CFC
		internal override bool SupportsRequestReply
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005F88 RID: 24456 RVA: 0x00163AFF File Offset: 0x00161CFF
		public override void SetKeyDerivation(bool requireDerivedKeys)
		{
			base.SetKeyDerivation(requireDerivedKeys);
			if (this.protectionTokenParameters != null)
			{
				this.protectionTokenParameters.RequireDerivedKeys = requireDerivedKeys;
			}
		}

		// Token: 0x06005F89 RID: 24457 RVA: 0x00163B1C File Offset: 0x00161D1C
		internal override bool IsSetKeyDerivation(bool requireDerivedKeys)
		{
			return base.IsSetKeyDerivation(requireDerivedKeys) && (this.protectionTokenParameters == null || this.protectionTokenParameters.RequireDerivedKeys == requireDerivedKeys);
		}

		// Token: 0x06005F8A RID: 24458 RVA: 0x00163B44 File Offset: 0x00161D44
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
			if (this.ProtectionTokenParameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SymmetricSecurityBindingElementNeedsProtectionTokenParameters", new object[]
				{
					this.ToString()
				})));
			}
			SymmetricSecurityProtocolFactory symmetricSecurityProtocolFactory = new SymmetricSecurityProtocolFactory();
			if (isForService)
			{
				base.ApplyAuditBehaviorSettings(context, symmetricSecurityProtocolFactory);
			}
			symmetricSecurityProtocolFactory.SecurityTokenParameters = this.ProtectionTokenParameters.Clone();
			SecurityBindingElement.SetIssuerBindingContextIfRequired(symmetricSecurityProtocolFactory.SecurityTokenParameters, issuerBindingContext);
			symmetricSecurityProtocolFactory.ApplyConfidentiality = true;
			symmetricSecurityProtocolFactory.RequireConfidentiality = true;
			symmetricSecurityProtocolFactory.ApplyIntegrity = true;
			symmetricSecurityProtocolFactory.RequireIntegrity = true;
			symmetricSecurityProtocolFactory.IdentityVerifier = base.LocalClientSettings.IdentityVerifier;
			symmetricSecurityProtocolFactory.DoRequestSignatureConfirmation = this.RequireSignatureConfirmation;
			symmetricSecurityProtocolFactory.MessageProtectionOrder = this.MessageProtectionOrder;
			symmetricSecurityProtocolFactory.ProtectionRequirements.Add(SecurityBindingElement.ComputeProtectionRequirements(this, context.BindingParameters, context.Binding.Elements, isForService));
			base.ConfigureProtocolFactory(symmetricSecurityProtocolFactory, credentialsManager, isForService, issuerBindingContext, context.Binding);
			return symmetricSecurityProtocolFactory;
		}

		// Token: 0x06005F8B RID: 24459 RVA: 0x00163C51 File Offset: 0x00161E51
		internal override bool RequiresChannelDemuxer()
		{
			return base.RequiresChannelDemuxer() || base.RequiresChannelDemuxer(this.ProtectionTokenParameters);
		}

		// Token: 0x06005F8C RID: 24460 RVA: 0x00163C6C File Offset: 0x00161E6C
		protected override IChannelFactory<TChannel> BuildChannelFactoryCore<TChannel>(BindingContext context)
		{
			ISecurityCapabilities property = this.GetProperty<ISecurityCapabilities>(context);
			SecurityCredentialsManager securityCredentialsManager = context.BindingParameters.Find<SecurityCredentialsManager>();
			if (securityCredentialsManager == null)
			{
				securityCredentialsManager = ClientCredentials.CreateDefaultCredentials();
			}
			bool flag = this.RequiresChannelDemuxer();
			ChannelBuilder channelBuilder = new ChannelBuilder(context, flag);
			if (flag)
			{
				base.ApplyPropertiesOnDemuxer(channelBuilder, context);
			}
			BindingContext bindingContext = context.Clone();
			SecurityChannelFactory<TChannel> result;
			if (this.ProtectionTokenParameters is SecureConversationSecurityTokenParameters)
			{
				SecureConversationSecurityTokenParameters secureConversationSecurityTokenParameters = (SecureConversationSecurityTokenParameters)this.ProtectionTokenParameters;
				if (secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecureConversationSecurityTokenParametersRequireBootstrapBinding")));
				}
				BindingContext bindingContext2 = bindingContext.Clone();
				bindingContext2.BindingParameters.Remove<ChannelProtectionRequirements>();
				bindingContext2.BindingParameters.Add(secureConversationSecurityTokenParameters.BootstrapProtectionRequirements);
				if (secureConversationSecurityTokenParameters.RequireCancellation)
				{
					SessionSymmetricMessageSecurityProtocolFactory sessionSymmetricMessageSecurityProtocolFactory = new SessionSymmetricMessageSecurityProtocolFactory();
					sessionSymmetricMessageSecurityProtocolFactory.SecurityTokenParameters = secureConversationSecurityTokenParameters.Clone();
					((SecureConversationSecurityTokenParameters)sessionSymmetricMessageSecurityProtocolFactory.SecurityTokenParameters).IssuerBindingContext = bindingContext2;
					sessionSymmetricMessageSecurityProtocolFactory.ApplyConfidentiality = true;
					sessionSymmetricMessageSecurityProtocolFactory.RequireConfidentiality = true;
					sessionSymmetricMessageSecurityProtocolFactory.ApplyIntegrity = true;
					sessionSymmetricMessageSecurityProtocolFactory.RequireIntegrity = true;
					sessionSymmetricMessageSecurityProtocolFactory.IdentityVerifier = base.LocalClientSettings.IdentityVerifier;
					sessionSymmetricMessageSecurityProtocolFactory.DoRequestSignatureConfirmation = this.RequireSignatureConfirmation;
					sessionSymmetricMessageSecurityProtocolFactory.MessageProtectionOrder = this.MessageProtectionOrder;
					sessionSymmetricMessageSecurityProtocolFactory.IdentityVerifier = base.LocalClientSettings.IdentityVerifier;
					sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.Add(SecurityBindingElement.ComputeProtectionRequirements(this, context.BindingParameters, context.Binding.Elements, false));
					base.ConfigureProtocolFactory(sessionSymmetricMessageSecurityProtocolFactory, securityCredentialsManager, false, bindingContext, context.Binding);
					SecuritySessionClientSettings<TChannel> securitySessionClientSettings = new SecuritySessionClientSettings<TChannel>();
					securitySessionClientSettings.ChannelBuilder = channelBuilder;
					securitySessionClientSettings.KeyRenewalInterval = base.LocalClientSettings.SessionKeyRenewalInterval;
					securitySessionClientSettings.CanRenewSession = secureConversationSecurityTokenParameters.CanRenewSession;
					securitySessionClientSettings.KeyRolloverInterval = base.LocalClientSettings.SessionKeyRolloverInterval;
					securitySessionClientSettings.TolerateTransportFailures = base.LocalClientSettings.ReconnectTransportOnFailure;
					securitySessionClientSettings.IssuedSecurityTokenParameters = secureConversationSecurityTokenParameters.Clone();
					((SecureConversationSecurityTokenParameters)securitySessionClientSettings.IssuedSecurityTokenParameters).IssuerBindingContext = bindingContext;
					securitySessionClientSettings.SecurityStandardsManager = sessionSymmetricMessageSecurityProtocolFactory.StandardsManager;
					securitySessionClientSettings.SessionProtocolFactory = sessionSymmetricMessageSecurityProtocolFactory;
					result = new SecurityChannelFactory<TChannel>(property, context, securitySessionClientSettings);
				}
				else
				{
					SymmetricSecurityProtocolFactory symmetricSecurityProtocolFactory = new SymmetricSecurityProtocolFactory();
					symmetricSecurityProtocolFactory.SecurityTokenParameters = secureConversationSecurityTokenParameters.Clone();
					((SecureConversationSecurityTokenParameters)symmetricSecurityProtocolFactory.SecurityTokenParameters).IssuerBindingContext = bindingContext2;
					symmetricSecurityProtocolFactory.ApplyConfidentiality = true;
					symmetricSecurityProtocolFactory.RequireConfidentiality = true;
					symmetricSecurityProtocolFactory.ApplyIntegrity = true;
					symmetricSecurityProtocolFactory.RequireIntegrity = true;
					symmetricSecurityProtocolFactory.IdentityVerifier = base.LocalClientSettings.IdentityVerifier;
					symmetricSecurityProtocolFactory.DoRequestSignatureConfirmation = this.RequireSignatureConfirmation;
					symmetricSecurityProtocolFactory.MessageProtectionOrder = this.MessageProtectionOrder;
					symmetricSecurityProtocolFactory.ProtectionRequirements.Add(SecurityBindingElement.ComputeProtectionRequirements(this, context.BindingParameters, context.Binding.Elements, false));
					base.ConfigureProtocolFactory(symmetricSecurityProtocolFactory, securityCredentialsManager, false, bindingContext, context.Binding);
					result = new SecurityChannelFactory<TChannel>(property, context, channelBuilder, symmetricSecurityProtocolFactory);
				}
			}
			else
			{
				SecurityProtocolFactory protocolFactory = this.CreateSecurityProtocolFactory<TChannel>(context, securityCredentialsManager, false, bindingContext);
				result = new SecurityChannelFactory<TChannel>(property, context, channelBuilder, protocolFactory);
			}
			return result;
		}

		// Token: 0x06005F8D RID: 24461 RVA: 0x00163F58 File Offset: 0x00162158
		protected override IChannelListener<TChannel> BuildChannelListenerCore<TChannel>(BindingContext context)
		{
			SecurityChannelListener<TChannel> securityChannelListener = new SecurityChannelListener<TChannel>(this, context);
			SecurityCredentialsManager securityCredentialsManager = context.BindingParameters.Find<SecurityCredentialsManager>();
			if (securityCredentialsManager == null)
			{
				securityCredentialsManager = ServiceCredentials.CreateDefaultCredentials();
			}
			bool flag = this.RequiresChannelDemuxer();
			ChannelBuilder channelBuilder = new ChannelBuilder(context, flag);
			if (flag)
			{
				base.ApplyPropertiesOnDemuxer(channelBuilder, context);
			}
			BindingContext bindingContext = context.Clone();
			if (this.ProtectionTokenParameters is SecureConversationSecurityTokenParameters)
			{
				SecureConversationSecurityTokenParameters secureConversationSecurityTokenParameters = (SecureConversationSecurityTokenParameters)this.ProtectionTokenParameters;
				if (secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecureConversationSecurityTokenParametersRequireBootstrapBinding")));
				}
				BindingContext bindingContext2 = bindingContext.Clone();
				bindingContext2.BindingParameters.Remove<ChannelProtectionRequirements>();
				bindingContext2.BindingParameters.Add(secureConversationSecurityTokenParameters.BootstrapProtectionRequirements);
				IMessageFilterTable<EndpointAddress> endpointFilterTable = context.BindingParameters.Find<IMessageFilterTable<EndpointAddress>>();
				base.AddDemuxerForSecureConversation(channelBuilder, bindingContext2);
				if (secureConversationSecurityTokenParameters.RequireCancellation)
				{
					SessionSymmetricMessageSecurityProtocolFactory sessionSymmetricMessageSecurityProtocolFactory = new SessionSymmetricMessageSecurityProtocolFactory();
					base.ApplyAuditBehaviorSettings(context, sessionSymmetricMessageSecurityProtocolFactory);
					sessionSymmetricMessageSecurityProtocolFactory.SecurityTokenParameters = secureConversationSecurityTokenParameters.Clone();
					((SecureConversationSecurityTokenParameters)sessionSymmetricMessageSecurityProtocolFactory.SecurityTokenParameters).IssuerBindingContext = bindingContext2;
					sessionSymmetricMessageSecurityProtocolFactory.ApplyConfidentiality = true;
					sessionSymmetricMessageSecurityProtocolFactory.RequireConfidentiality = true;
					sessionSymmetricMessageSecurityProtocolFactory.ApplyIntegrity = true;
					sessionSymmetricMessageSecurityProtocolFactory.RequireIntegrity = true;
					sessionSymmetricMessageSecurityProtocolFactory.IdentityVerifier = base.LocalClientSettings.IdentityVerifier;
					sessionSymmetricMessageSecurityProtocolFactory.DoRequestSignatureConfirmation = this.RequireSignatureConfirmation;
					sessionSymmetricMessageSecurityProtocolFactory.MessageProtectionOrder = this.MessageProtectionOrder;
					sessionSymmetricMessageSecurityProtocolFactory.IdentityVerifier = base.LocalClientSettings.IdentityVerifier;
					sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.Add(SecurityBindingElement.ComputeProtectionRequirements(this, context.BindingParameters, context.Binding.Elements, true));
					base.ConfigureProtocolFactory(sessionSymmetricMessageSecurityProtocolFactory, securityCredentialsManager, true, bindingContext, context.Binding);
					securityChannelListener.SessionMode = true;
					securityChannelListener.SessionServerSettings.InactivityTimeout = base.LocalServiceSettings.InactivityTimeout;
					securityChannelListener.SessionServerSettings.KeyRolloverInterval = base.LocalServiceSettings.SessionKeyRolloverInterval;
					securityChannelListener.SessionServerSettings.MaximumPendingSessions = base.LocalServiceSettings.MaxPendingSessions;
					securityChannelListener.SessionServerSettings.MaximumKeyRenewalInterval = base.LocalServiceSettings.SessionKeyRenewalInterval;
					securityChannelListener.SessionServerSettings.TolerateTransportFailures = base.LocalServiceSettings.ReconnectTransportOnFailure;
					securityChannelListener.SessionServerSettings.CanRenewSession = secureConversationSecurityTokenParameters.CanRenewSession;
					securityChannelListener.SessionServerSettings.IssuedSecurityTokenParameters = secureConversationSecurityTokenParameters.Clone();
					((SecureConversationSecurityTokenParameters)securityChannelListener.SessionServerSettings.IssuedSecurityTokenParameters).IssuerBindingContext = bindingContext2;
					securityChannelListener.SessionServerSettings.SecurityStandardsManager = sessionSymmetricMessageSecurityProtocolFactory.StandardsManager;
					securityChannelListener.SessionServerSettings.SessionProtocolFactory = sessionSymmetricMessageSecurityProtocolFactory;
					securityChannelListener.SessionServerSettings.SessionProtocolFactory.EndpointFilterTable = endpointFilterTable;
					if (context.BindingParameters != null && context.BindingParameters.Find<IChannelDemuxFailureHandler>() == null && !base.IsUnderlyingListenerDuplex<TChannel>(context))
					{
						context.BindingParameters.Add(new SecuritySessionServerSettings.SecuritySessionDemuxFailureHandler(sessionSymmetricMessageSecurityProtocolFactory.StandardsManager));
					}
				}
				else
				{
					SymmetricSecurityProtocolFactory symmetricSecurityProtocolFactory = new SymmetricSecurityProtocolFactory();
					base.ApplyAuditBehaviorSettings(context, symmetricSecurityProtocolFactory);
					symmetricSecurityProtocolFactory.SecurityTokenParameters = secureConversationSecurityTokenParameters.Clone();
					((SecureConversationSecurityTokenParameters)symmetricSecurityProtocolFactory.SecurityTokenParameters).IssuerBindingContext = bindingContext2;
					symmetricSecurityProtocolFactory.ApplyConfidentiality = true;
					symmetricSecurityProtocolFactory.RequireConfidentiality = true;
					symmetricSecurityProtocolFactory.ApplyIntegrity = true;
					symmetricSecurityProtocolFactory.RequireIntegrity = true;
					symmetricSecurityProtocolFactory.IdentityVerifier = base.LocalClientSettings.IdentityVerifier;
					symmetricSecurityProtocolFactory.DoRequestSignatureConfirmation = this.RequireSignatureConfirmation;
					symmetricSecurityProtocolFactory.MessageProtectionOrder = this.MessageProtectionOrder;
					symmetricSecurityProtocolFactory.ProtectionRequirements.Add(SecurityBindingElement.ComputeProtectionRequirements(this, context.BindingParameters, context.Binding.Elements, true));
					symmetricSecurityProtocolFactory.EndpointFilterTable = endpointFilterTable;
					base.ConfigureProtocolFactory(symmetricSecurityProtocolFactory, securityCredentialsManager, true, bindingContext, context.Binding);
					securityChannelListener.SecurityProtocolFactory = symmetricSecurityProtocolFactory;
				}
			}
			else
			{
				SecurityProtocolFactory securityProtocolFactory = this.CreateSecurityProtocolFactory<TChannel>(context, securityCredentialsManager, true, bindingContext);
				securityChannelListener.SecurityProtocolFactory = securityProtocolFactory;
			}
			securityChannelListener.InitializeListener(channelBuilder);
			return securityChannelListener;
		}

		// Token: 0x06005F8E RID: 24462 RVA: 0x001642FC File Offset: 0x001624FC
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
				ChannelProtectionRequirements protectionRequirements = base.GetProtectionRequirements(addressing, ProtectionLevel.EncryptAndSign);
				protectionRequirements.Add(context.GetInnerProperty<ChannelProtectionRequirements>() ?? new ChannelProtectionRequirements());
				return (T)((object)protectionRequirements);
			}
			return base.GetProperty<T>(context);
		}

		// Token: 0x06005F8F RID: 24463 RVA: 0x00164390 File Offset: 0x00162590
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
			stringBuilder.Append("ProtectionTokenParameters: ");
			if (this.protectionTokenParameters != null)
			{
				stringBuilder.AppendLine(this.protectionTokenParameters.ToString().Trim().Replace("\n", "\n  "));
			}
			else
			{
				stringBuilder.AppendLine("null");
			}
			return stringBuilder.ToString().Trim();
		}

		// Token: 0x06005F90 RID: 24464 RVA: 0x0016445D File Offset: 0x0016265D
		public override BindingElement Clone()
		{
			return new SymmetricSecurityBindingElement(this);
		}

		// Token: 0x06005F91 RID: 24465 RVA: 0x00164465 File Offset: 0x00162665
		void IPolicyExportExtension.ExportPolicy(MetadataExporter exporter, PolicyConversionContext context)
		{
			SecurityBindingElement.ExportPolicy(exporter, context);
		}

		// Token: 0x0400383C RID: 14396
		private MessageProtectionOrder messageProtectionOrder;

		// Token: 0x0400383D RID: 14397
		private SecurityTokenParameters protectionTokenParameters;

		// Token: 0x0400383E RID: 14398
		private bool requireSignatureConfirmation;
	}
}
