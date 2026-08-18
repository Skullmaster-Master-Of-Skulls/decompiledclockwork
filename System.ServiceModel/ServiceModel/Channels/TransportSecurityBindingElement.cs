using System;
using System.Net.Security;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000994 RID: 2452
	[__DynamicallyInvokable]
	public sealed class TransportSecurityBindingElement : SecurityBindingElement, IPolicyExportExtension
	{
		// Token: 0x06005F92 RID: 24466 RVA: 0x00164470 File Offset: 0x00162670
		[__DynamicallyInvokable]
		public TransportSecurityBindingElement()
		{
			base.LocalClientSettings.DetectReplays = (base.LocalServiceSettings.DetectReplays = false);
		}

		// Token: 0x06005F93 RID: 24467 RVA: 0x0016449D File Offset: 0x0016269D
		private TransportSecurityBindingElement(TransportSecurityBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
		}

		// Token: 0x06005F94 RID: 24468 RVA: 0x001644A8 File Offset: 0x001626A8
		internal override ISecurityCapabilities GetIndividualISecurityCapabilities()
		{
			bool supportsClientAuth;
			bool supportsClientWindowsIdentity;
			base.GetSupportingTokensCapabilities(out supportsClientAuth, out supportsClientWindowsIdentity);
			return new SecurityCapabilities(supportsClientAuth, false, supportsClientWindowsIdentity, ProtectionLevel.None, ProtectionLevel.None);
		}

		// Token: 0x170016E5 RID: 5861
		// (get) Token: 0x06005F95 RID: 24469 RVA: 0x001644CC File Offset: 0x001626CC
		internal override bool SessionMode
		{
			get
			{
				SecureConversationSecurityTokenParameters secureConversationSecurityTokenParameters = null;
				if (base.EndpointSupportingTokenParameters.Endorsing.Count > 0)
				{
					secureConversationSecurityTokenParameters = (base.EndpointSupportingTokenParameters.Endorsing[0] as SecureConversationSecurityTokenParameters);
				}
				return secureConversationSecurityTokenParameters != null && secureConversationSecurityTokenParameters.RequireCancellation;
			}
		}

		// Token: 0x170016E6 RID: 5862
		// (get) Token: 0x06005F96 RID: 24470 RVA: 0x00164510 File Offset: 0x00162710
		internal override bool SupportsDuplex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170016E7 RID: 5863
		// (get) Token: 0x06005F97 RID: 24471 RVA: 0x00164513 File Offset: 0x00162713
		internal override bool SupportsRequestReply
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005F98 RID: 24472 RVA: 0x00164518 File Offset: 0x00162718
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
			TransportSecurityProtocolFactory transportSecurityProtocolFactory = new TransportSecurityProtocolFactory();
			if (isForService)
			{
				base.ApplyAuditBehaviorSettings(context, transportSecurityProtocolFactory);
			}
			base.ConfigureProtocolFactory(transportSecurityProtocolFactory, credentialsManager, isForService, issuerBindingContext, context.Binding);
			transportSecurityProtocolFactory.DetectReplays = false;
			return transportSecurityProtocolFactory;
		}

		// Token: 0x06005F99 RID: 24473 RVA: 0x00164578 File Offset: 0x00162778
		protected override IChannelFactory<TChannel> BuildChannelFactoryCore<TChannel>(BindingContext context)
		{
			ISecurityCapabilities property = this.GetProperty<ISecurityCapabilities>(context);
			SecurityCredentialsManager securityCredentialsManager = context.BindingParameters.Find<SecurityCredentialsManager>();
			if (securityCredentialsManager == null)
			{
				securityCredentialsManager = ClientCredentials.CreateDefaultCredentials();
			}
			SecureConversationSecurityTokenParameters secureConversationSecurityTokenParameters = null;
			if (base.EndpointSupportingTokenParameters.Endorsing.Count > 0)
			{
				secureConversationSecurityTokenParameters = (base.EndpointSupportingTokenParameters.Endorsing[0] as SecureConversationSecurityTokenParameters);
			}
			bool flag = this.RequiresChannelDemuxer();
			ChannelBuilder channelBuilder = new ChannelBuilder(context, flag);
			if (flag)
			{
				base.ApplyPropertiesOnDemuxer(channelBuilder, context);
			}
			BindingContext bindingContext = context.Clone();
			SecurityChannelFactory<TChannel> result;
			if (secureConversationSecurityTokenParameters != null)
			{
				if (secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecureConversationSecurityTokenParametersRequireBootstrapBinding")));
				}
				secureConversationSecurityTokenParameters.IssuerBindingContext = bindingContext;
				if (secureConversationSecurityTokenParameters.RequireCancellation)
				{
					SessionSymmetricTransportSecurityProtocolFactory sessionSymmetricTransportSecurityProtocolFactory = new SessionSymmetricTransportSecurityProtocolFactory();
					sessionSymmetricTransportSecurityProtocolFactory.SecurityTokenParameters = secureConversationSecurityTokenParameters.Clone();
					((SecureConversationSecurityTokenParameters)sessionSymmetricTransportSecurityProtocolFactory.SecurityTokenParameters).IssuerBindingContext = bindingContext;
					base.EndpointSupportingTokenParameters.Endorsing.RemoveAt(0);
					try
					{
						base.ConfigureProtocolFactory(sessionSymmetricTransportSecurityProtocolFactory, securityCredentialsManager, false, bindingContext, context.Binding);
					}
					finally
					{
						base.EndpointSupportingTokenParameters.Endorsing.Insert(0, secureConversationSecurityTokenParameters);
					}
					SecuritySessionClientSettings<TChannel> securitySessionClientSettings = new SecuritySessionClientSettings<TChannel>();
					securitySessionClientSettings.ChannelBuilder = channelBuilder;
					securitySessionClientSettings.KeyRenewalInterval = base.LocalClientSettings.SessionKeyRenewalInterval;
					securitySessionClientSettings.KeyRolloverInterval = base.LocalClientSettings.SessionKeyRolloverInterval;
					securitySessionClientSettings.TolerateTransportFailures = base.LocalClientSettings.ReconnectTransportOnFailure;
					securitySessionClientSettings.CanRenewSession = secureConversationSecurityTokenParameters.CanRenewSession;
					securitySessionClientSettings.IssuedSecurityTokenParameters = secureConversationSecurityTokenParameters.Clone();
					((SecureConversationSecurityTokenParameters)securitySessionClientSettings.IssuedSecurityTokenParameters).IssuerBindingContext = bindingContext;
					securitySessionClientSettings.SecurityStandardsManager = sessionSymmetricTransportSecurityProtocolFactory.StandardsManager;
					securitySessionClientSettings.SessionProtocolFactory = sessionSymmetricTransportSecurityProtocolFactory;
					result = new SecurityChannelFactory<TChannel>(property, context, securitySessionClientSettings);
				}
				else
				{
					TransportSecurityProtocolFactory transportSecurityProtocolFactory = new TransportSecurityProtocolFactory();
					base.EndpointSupportingTokenParameters.Endorsing.RemoveAt(0);
					try
					{
						base.ConfigureProtocolFactory(transportSecurityProtocolFactory, securityCredentialsManager, false, bindingContext, context.Binding);
						SecureConversationSecurityTokenParameters secureConversationSecurityTokenParameters2 = (SecureConversationSecurityTokenParameters)secureConversationSecurityTokenParameters.Clone();
						secureConversationSecurityTokenParameters2.IssuerBindingContext = bindingContext;
						transportSecurityProtocolFactory.SecurityBindingElement.EndpointSupportingTokenParameters.Endorsing.Insert(0, secureConversationSecurityTokenParameters2);
					}
					finally
					{
						base.EndpointSupportingTokenParameters.Endorsing.Insert(0, secureConversationSecurityTokenParameters);
					}
					result = new SecurityChannelFactory<TChannel>(property, context, channelBuilder, transportSecurityProtocolFactory);
				}
			}
			else
			{
				SecurityProtocolFactory protocolFactory = this.CreateSecurityProtocolFactory<TChannel>(context, securityCredentialsManager, false, bindingContext);
				result = new SecurityChannelFactory<TChannel>(property, context, channelBuilder, protocolFactory);
			}
			return result;
		}

		// Token: 0x06005F9A RID: 24474 RVA: 0x001647DC File Offset: 0x001629DC
		protected override IChannelListener<TChannel> BuildChannelListenerCore<TChannel>(BindingContext context)
		{
			SecurityChannelListener<TChannel> securityChannelListener = new SecurityChannelListener<TChannel>(this, context);
			SecurityCredentialsManager securityCredentialsManager = context.BindingParameters.Find<SecurityCredentialsManager>();
			if (securityCredentialsManager == null)
			{
				securityCredentialsManager = ServiceCredentials.CreateDefaultCredentials();
			}
			SecureConversationSecurityTokenParameters secureConversationSecurityTokenParameters;
			if (base.EndpointSupportingTokenParameters.Endorsing.Count > 0)
			{
				secureConversationSecurityTokenParameters = (base.EndpointSupportingTokenParameters.Endorsing[0] as SecureConversationSecurityTokenParameters);
			}
			else
			{
				secureConversationSecurityTokenParameters = null;
			}
			bool flag = this.RequiresChannelDemuxer();
			ChannelBuilder channelBuilder = new ChannelBuilder(context, flag);
			if (flag)
			{
				base.ApplyPropertiesOnDemuxer(channelBuilder, context);
			}
			BindingContext bindingContext = context.Clone();
			if (secureConversationSecurityTokenParameters != null)
			{
				if (secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecureConversationSecurityTokenParametersRequireBootstrapBinding")));
				}
				base.AddDemuxerForSecureConversation(channelBuilder, bindingContext);
				if (secureConversationSecurityTokenParameters.RequireCancellation)
				{
					SessionSymmetricTransportSecurityProtocolFactory sessionSymmetricTransportSecurityProtocolFactory = new SessionSymmetricTransportSecurityProtocolFactory();
					base.ApplyAuditBehaviorSettings(context, sessionSymmetricTransportSecurityProtocolFactory);
					sessionSymmetricTransportSecurityProtocolFactory.SecurityTokenParameters = secureConversationSecurityTokenParameters.Clone();
					((SecureConversationSecurityTokenParameters)sessionSymmetricTransportSecurityProtocolFactory.SecurityTokenParameters).IssuerBindingContext = bindingContext;
					base.EndpointSupportingTokenParameters.Endorsing.RemoveAt(0);
					try
					{
						base.ConfigureProtocolFactory(sessionSymmetricTransportSecurityProtocolFactory, securityCredentialsManager, true, bindingContext, context.Binding);
					}
					finally
					{
						base.EndpointSupportingTokenParameters.Endorsing.Insert(0, secureConversationSecurityTokenParameters);
					}
					securityChannelListener.SessionMode = true;
					securityChannelListener.SessionServerSettings.InactivityTimeout = base.LocalServiceSettings.InactivityTimeout;
					securityChannelListener.SessionServerSettings.KeyRolloverInterval = base.LocalServiceSettings.SessionKeyRolloverInterval;
					securityChannelListener.SessionServerSettings.MaximumPendingSessions = base.LocalServiceSettings.MaxPendingSessions;
					securityChannelListener.SessionServerSettings.MaximumKeyRenewalInterval = base.LocalServiceSettings.SessionKeyRenewalInterval;
					securityChannelListener.SessionServerSettings.TolerateTransportFailures = base.LocalServiceSettings.ReconnectTransportOnFailure;
					securityChannelListener.SessionServerSettings.CanRenewSession = secureConversationSecurityTokenParameters.CanRenewSession;
					securityChannelListener.SessionServerSettings.IssuedSecurityTokenParameters = secureConversationSecurityTokenParameters.Clone();
					((SecureConversationSecurityTokenParameters)securityChannelListener.SessionServerSettings.IssuedSecurityTokenParameters).IssuerBindingContext = bindingContext;
					securityChannelListener.SessionServerSettings.SecurityStandardsManager = sessionSymmetricTransportSecurityProtocolFactory.StandardsManager;
					securityChannelListener.SessionServerSettings.SessionProtocolFactory = sessionSymmetricTransportSecurityProtocolFactory;
					if (context.BindingParameters != null && context.BindingParameters.Find<IChannelDemuxFailureHandler>() == null && !base.IsUnderlyingListenerDuplex<TChannel>(context))
					{
						context.BindingParameters.Add(new SecuritySessionServerSettings.SecuritySessionDemuxFailureHandler(sessionSymmetricTransportSecurityProtocolFactory.StandardsManager));
					}
				}
				else
				{
					TransportSecurityProtocolFactory transportSecurityProtocolFactory = new TransportSecurityProtocolFactory();
					base.ApplyAuditBehaviorSettings(context, transportSecurityProtocolFactory);
					base.EndpointSupportingTokenParameters.Endorsing.RemoveAt(0);
					try
					{
						base.ConfigureProtocolFactory(transportSecurityProtocolFactory, securityCredentialsManager, true, bindingContext, context.Binding);
						SecureConversationSecurityTokenParameters secureConversationSecurityTokenParameters2 = (SecureConversationSecurityTokenParameters)secureConversationSecurityTokenParameters.Clone();
						secureConversationSecurityTokenParameters2.IssuerBindingContext = bindingContext;
						transportSecurityProtocolFactory.SecurityBindingElement.EndpointSupportingTokenParameters.Endorsing.Insert(0, secureConversationSecurityTokenParameters2);
					}
					finally
					{
						base.EndpointSupportingTokenParameters.Endorsing.Insert(0, secureConversationSecurityTokenParameters);
					}
					securityChannelListener.SecurityProtocolFactory = transportSecurityProtocolFactory;
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

		// Token: 0x06005F9B RID: 24475 RVA: 0x00164AC8 File Offset: 0x00162CC8
		[__DynamicallyInvokable]
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

		// Token: 0x06005F9C RID: 24476 RVA: 0x00164B5A File Offset: 0x00162D5A
		[__DynamicallyInvokable]
		public override BindingElement Clone()
		{
			return new TransportSecurityBindingElement(this);
		}

		// Token: 0x06005F9D RID: 24477 RVA: 0x00164B64 File Offset: 0x00162D64
		void IPolicyExportExtension.ExportPolicy(MetadataExporter exporter, PolicyConversionContext policyContext)
		{
			if (exporter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("exporter");
			}
			if (policyContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("policyContext");
			}
			if (policyContext.BindingElements.Find<ITransportTokenAssertionProvider>() == null)
			{
				if (!base.AllowInsecureTransport)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ExportOfBindingWithTransportSecurityBindingElementAndNoTransportSecurityNotSupported")));
				}
				SecurityBindingElement.ExportPolicyForTransportTokenAssertionProviders(exporter, policyContext);
			}
		}
	}
}
