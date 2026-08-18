using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000329 RID: 809
	internal sealed class AcceleratedTokenAuthenticator : NegotiationTokenAuthenticator<NegotiationTokenAuthenticatorState>
	{
		// Token: 0x06001CB3 RID: 7347 RVA: 0x0006B03C File Offset: 0x0006923C
		public AcceleratedTokenAuthenticator()
		{
			this.keyEntropyMode = SecurityKeyEntropyMode.CombinedEntropy;
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001CB4 RID: 7348 RVA: 0x0006B04B File Offset: 0x0006924B
		// (set) Token: 0x06001CB5 RID: 7349 RVA: 0x0006B053 File Offset: 0x00069253
		public bool PreserveBootstrapTokens
		{
			get
			{
				return this.preserveBootstrapTokens;
			}
			set
			{
				this.preserveBootstrapTokens = value;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x0006B05C File Offset: 0x0006925C
		public override XmlDictionaryString RequestSecurityTokenAction
		{
			get
			{
				return base.StandardsManager.SecureConversationDriver.IssueAction;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001CB7 RID: 7351 RVA: 0x0006B06E File Offset: 0x0006926E
		public override XmlDictionaryString RequestSecurityTokenResponseAction
		{
			get
			{
				return base.StandardsManager.SecureConversationDriver.IssueResponseAction;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x0006B080 File Offset: 0x00069280
		public override XmlDictionaryString RequestSecurityTokenResponseFinalAction
		{
			get
			{
				return base.StandardsManager.SecureConversationDriver.IssueResponseAction;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06001CB9 RID: 7353 RVA: 0x0006B092 File Offset: 0x00069292
		// (set) Token: 0x06001CBA RID: 7354 RVA: 0x0006B09A File Offset: 0x0006929A
		public SecurityBindingElement BootstrapSecurityBindingElement
		{
			get
			{
				return this.bootstrapSecurityBindingElement;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.bootstrapSecurityBindingElement = (SecurityBindingElement)value.Clone();
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001CBB RID: 7355 RVA: 0x0006B0CB File Offset: 0x000692CB
		// (set) Token: 0x06001CBC RID: 7356 RVA: 0x0006B0D3 File Offset: 0x000692D3
		public SecurityKeyEntropyMode KeyEntropyMode
		{
			get
			{
				return this.keyEntropyMode;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				SecurityKeyEntropyModeHelper.Validate(value);
				this.keyEntropyMode = value;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06001CBD RID: 7357 RVA: 0x0006B0ED File Offset: 0x000692ED
		protected override bool IsMultiLegNegotiation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x0006B0F0 File Offset: 0x000692F0
		protected override MessageFilter GetListenerFilter()
		{
			return new AcceleratedTokenAuthenticator.RstDirectFilter(base.StandardsManager, this);
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x0006B100 File Offset: 0x00069300
		protected override Binding GetNegotiationBinding(Binding binding)
		{
			CustomBinding customBinding = new CustomBinding(binding);
			customBinding.Elements.Insert(0, new AcceleratedTokenAuthenticatorBindingElement(this));
			return customBinding;
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x0006B128 File Offset: 0x00069328
		internal IChannelListener<TChannel> BuildNegotiationChannelListener<TChannel>(BindingContext context) where TChannel : class, IChannel
		{
			SecurityCredentialsManager securityCredentialsManager = base.IssuerBindingContext.BindingParameters.Find<SecurityCredentialsManager>();
			if (securityCredentialsManager == null)
			{
				securityCredentialsManager = ServiceCredentials.CreateDefaultCredentials();
			}
			this.bootstrapSecurityBindingElement.ReaderQuotas = context.GetInnerProperty<XmlDictionaryReaderQuotas>();
			if (this.bootstrapSecurityBindingElement.ReaderQuotas == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("EncodingBindingElementDoesNotHandleReaderQuotas")));
			}
			TransportBindingElement transportBindingElement = context.RemainingBindingElements.Find<TransportBindingElement>();
			if (transportBindingElement != null)
			{
				this.bootstrapSecurityBindingElement.MaxReceivedMessageSize = transportBindingElement.MaxReceivedMessageSize;
			}
			SecurityProtocolFactory securityProtocolFactory = this.bootstrapSecurityBindingElement.CreateSecurityProtocolFactory<TChannel>(base.IssuerBindingContext.Clone(), securityCredentialsManager, true, base.IssuerBindingContext.Clone());
			MessageSecurityProtocolFactory messageSecurityProtocolFactory = securityProtocolFactory as MessageSecurityProtocolFactory;
			if (messageSecurityProtocolFactory != null)
			{
				messageSecurityProtocolFactory.ApplyConfidentiality = (messageSecurityProtocolFactory.ApplyIntegrity = (messageSecurityProtocolFactory.RequireConfidentiality = (messageSecurityProtocolFactory.RequireIntegrity = true)));
				MessagePartSpecification parts = new MessagePartSpecification(true);
				messageSecurityProtocolFactory.ProtectionRequirements.OutgoingSignatureParts.AddParts(parts, this.RequestSecurityTokenResponseAction);
				messageSecurityProtocolFactory.ProtectionRequirements.OutgoingEncryptionParts.AddParts(parts, this.RequestSecurityTokenResponseAction);
				messageSecurityProtocolFactory.ProtectionRequirements.IncomingSignatureParts.AddParts(parts, this.RequestSecurityTokenAction);
				messageSecurityProtocolFactory.ProtectionRequirements.IncomingEncryptionParts.AddParts(parts, this.RequestSecurityTokenAction);
			}
			SecurityChannelListener<TChannel> securityChannelListener = new SecurityChannelListener<TChannel>(this.bootstrapSecurityBindingElement, context);
			securityChannelListener.SecurityProtocolFactory = securityProtocolFactory;
			securityChannelListener.SendUnsecuredFaults = !SecurityUtils.IsCompositeDuplexBinding(context);
			ChannelBuilder channelBuilder = new ChannelBuilder(context, true);
			securityChannelListener.InitializeListener(channelBuilder);
			this.shouldMatchRstWithEndpointFilter = SecurityUtils.ShouldMatchRstWithEndpointFilter(this.bootstrapSecurityBindingElement);
			return securityChannelListener;
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x0006B2B4 File Offset: 0x000694B4
		protected override BodyWriter ProcessRequestSecurityToken(Message request, RequestSecurityToken requestSecurityToken, out NegotiationTokenAuthenticatorState negotiationState)
		{
			if (request == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("request");
			}
			if (requestSecurityToken == null)
			{
				throw TraceUtility.ThrowHelperArgumentNull("requestSecurityToken", request);
			}
			BodyWriter result;
			try
			{
				if (requestSecurityToken.RequestType != null && requestSecurityToken.RequestType != base.StandardsManager.TrustDriver.RequestTypeIssue)
				{
					throw TraceUtility.ThrowHelperWarning(new SecurityNegotiationException(SR.GetString("InvalidRstRequestType", new object[]
					{
						requestSecurityToken.RequestType
					})), request);
				}
				if (requestSecurityToken.TokenType != null && requestSecurityToken.TokenType != base.SecurityContextTokenUri)
				{
					throw TraceUtility.ThrowHelperWarning(new SecurityNegotiationException(SR.GetString("CannotIssueRstTokenType", new object[]
					{
						requestSecurityToken.TokenType
					})), request);
				}
				string a;
				string a2;
				requestSecurityToken.GetAppliesToQName(out a, out a2);
				DataContractSerializer serializer;
				EndpointAddress endpointAddress;
				if (a == "EndpointReference" && a2 == request.Version.Addressing.Namespace)
				{
					if (request.Version.Addressing == AddressingVersion.WSAddressing10)
					{
						serializer = DataContractSerializerDefaults.CreateSerializer(typeof(EndpointAddress10), int.MaxValue);
						endpointAddress = requestSecurityToken.GetAppliesTo<EndpointAddress10>(serializer).ToEndpointAddress();
					}
					else
					{
						if (request.Version.Addressing != AddressingVersion.WSAddressingAugust2004)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("AddressingVersionNotSupported", new object[]
							{
								request.Version.Addressing
							})));
						}
						serializer = DataContractSerializerDefaults.CreateSerializer(typeof(EndpointAddressAugust2004), int.MaxValue);
						endpointAddress = requestSecurityToken.GetAppliesTo<EndpointAddressAugust2004>(serializer).ToEndpointAddress();
					}
				}
				else
				{
					endpointAddress = null;
					serializer = null;
				}
				if (this.shouldMatchRstWithEndpointFilter)
				{
					SecurityUtils.MatchRstWithEndpointFilter(request, base.EndpointFilterTable, base.ListenUri);
				}
				int keySize;
				byte[] array;
				byte[] key;
				SecurityToken securityToken;
				WSTrust.Driver.ProcessRstAndIssueKey(requestSecurityToken, null, this.KeyEntropyMode, base.SecurityAlgorithmSuite, out keySize, out array, out key, out securityToken);
				UniqueId contextId = SecurityUtils.GenerateUniqueId();
				string id = SecurityUtils.GenerateId();
				DateTime utcNow = DateTime.UtcNow;
				DateTime dateTime = TimeoutHelper.Add(utcNow, base.ServiceTokenLifetime);
				SecurityMessageProperty security = request.Properties.Security;
				ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies;
				if (security != null)
				{
					authorizationPolicies = SecuritySessionSecurityTokenAuthenticator.CreateSecureConversationPolicies(security, dateTime);
				}
				else
				{
					authorizationPolicies = EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance;
				}
				SecurityContextSecurityToken securityContextSecurityToken = base.IssueSecurityContextToken(contextId, id, key, utcNow, dateTime, authorizationPolicies, base.EncryptStateInServiceToken);
				if (this.preserveBootstrapTokens)
				{
					securityContextSecurityToken.BootstrapMessageProperty = ((security == null) ? null : ((SecurityMessageProperty)security.CreateCopy()));
					SecurityUtils.ErasePasswordInUsernameTokenIfPresent(securityContextSecurityToken.BootstrapMessageProperty);
				}
				RequestSecurityTokenResponse requestSecurityTokenResponse = new RequestSecurityTokenResponse(base.StandardsManager);
				requestSecurityTokenResponse.Context = requestSecurityToken.Context;
				requestSecurityTokenResponse.KeySize = keySize;
				requestSecurityTokenResponse.RequestedUnattachedReference = base.IssuedSecurityTokenParameters.CreateKeyIdentifierClause(securityContextSecurityToken, SecurityTokenReferenceStyle.External);
				requestSecurityTokenResponse.RequestedAttachedReference = base.IssuedSecurityTokenParameters.CreateKeyIdentifierClause(securityContextSecurityToken, SecurityTokenReferenceStyle.Internal);
				requestSecurityTokenResponse.TokenType = base.SecurityContextTokenUri;
				requestSecurityTokenResponse.RequestedSecurityToken = securityContextSecurityToken;
				if (array != null)
				{
					requestSecurityTokenResponse.SetIssuerEntropy(array);
					requestSecurityTokenResponse.ComputeKey = true;
				}
				if (securityToken != null)
				{
					requestSecurityTokenResponse.RequestedProofToken = securityToken;
				}
				if (endpointAddress != null)
				{
					if (request.Version.Addressing == AddressingVersion.WSAddressing10)
					{
						requestSecurityTokenResponse.SetAppliesTo<EndpointAddress10>(EndpointAddress10.FromEndpointAddress(endpointAddress), serializer);
					}
					else
					{
						if (request.Version.Addressing != AddressingVersion.WSAddressingAugust2004)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("AddressingVersionNotSupported", new object[]
							{
								request.Version.Addressing
							})));
						}
						requestSecurityTokenResponse.SetAppliesTo<EndpointAddressAugust2004>(EndpointAddressAugust2004.FromEndpointAddress(endpointAddress), serializer);
					}
				}
				requestSecurityTokenResponse.MakeReadOnly();
				negotiationState = new NegotiationTokenAuthenticatorState();
				negotiationState.SetServiceToken(securityContextSecurityToken);
				if (base.StandardsManager.MessageSecurityVersion.SecureConversationVersion == SecureConversationVersion.WSSecureConversationFeb2005)
				{
					result = requestSecurityTokenResponse;
				}
				else
				{
					if (base.StandardsManager.MessageSecurityVersion.SecureConversationVersion != SecureConversationVersion.WSSecureConversation13)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
					}
					RequestSecurityTokenResponseCollection requestSecurityTokenResponseCollection = new RequestSecurityTokenResponseCollection(new List<RequestSecurityTokenResponse>(1)
					{
						requestSecurityTokenResponse
					}, base.StandardsManager);
					result = requestSecurityTokenResponseCollection;
				}
			}
			finally
			{
				SecuritySessionSecurityTokenAuthenticator.RemoveCachedTokensIfRequired(request.Properties.Security);
			}
			return result;
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x0006B6B8 File Offset: 0x000698B8
		protected override BodyWriter ProcessRequestSecurityTokenResponse(NegotiationTokenAuthenticatorState negotiationState, Message request, RequestSecurityTokenResponse requestSecurityTokenResponse)
		{
			throw TraceUtility.ThrowHelperWarning(new NotSupportedException(SR.GetString("RstDirectDoesNotExpectRstr")), request);
		}

		// Token: 0x04001DDB RID: 7643
		private SecurityBindingElement bootstrapSecurityBindingElement;

		// Token: 0x04001DDC RID: 7644
		private SecurityKeyEntropyMode keyEntropyMode;

		// Token: 0x04001DDD RID: 7645
		private bool shouldMatchRstWithEndpointFilter;

		// Token: 0x04001DDE RID: 7646
		private bool preserveBootstrapTokens;

		// Token: 0x02000B79 RID: 2937
		private class RstDirectFilter : HeaderFilter
		{
			// Token: 0x060072B0 RID: 29360 RVA: 0x001AC284 File Offset: 0x001AA484
			public RstDirectFilter(SecurityStandardsManager standardsManager, AcceleratedTokenAuthenticator authenticator)
			{
				this.standardsManager = standardsManager;
				this.authenticator = authenticator;
			}

			// Token: 0x060072B1 RID: 29361 RVA: 0x001AC29A File Offset: 0x001AA49A
			public override bool Match(Message message)
			{
				return message.Headers.Action == this.authenticator.RequestSecurityTokenAction.Value && this.standardsManager.DoesMessageContainSecurityHeader(message);
			}

			// Token: 0x040040EA RID: 16618
			private SecurityStandardsManager standardsManager;

			// Token: 0x040040EB RID: 16619
			private AcceleratedTokenAuthenticator authenticator;
		}
	}
}
