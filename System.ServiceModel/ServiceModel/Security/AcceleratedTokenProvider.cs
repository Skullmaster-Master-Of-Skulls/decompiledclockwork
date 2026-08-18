using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x0200032B RID: 811
	internal class AcceleratedTokenProvider : NegotiationTokenProvider<AcceleratedTokenProviderState>
	{
		// Token: 0x06001CC7 RID: 7367 RVA: 0x0006B746 File Offset: 0x00069946
		public AcceleratedTokenProvider(SafeFreeCredentials credentialsHandle)
		{
			this.credentialsHandle = credentialsHandle;
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x0006B75C File Offset: 0x0006995C
		// (set) Token: 0x06001CC9 RID: 7369 RVA: 0x0006B764 File Offset: 0x00069964
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

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001CCA RID: 7370 RVA: 0x0006B77E File Offset: 0x0006997E
		// (set) Token: 0x06001CCB RID: 7371 RVA: 0x0006B786 File Offset: 0x00069986
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

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001CCC RID: 7372 RVA: 0x0006B7B7 File Offset: 0x000699B7
		// (set) Token: 0x06001CCD RID: 7373 RVA: 0x0006B7BF File Offset: 0x000699BF
		public Uri PrivacyNoticeUri
		{
			get
			{
				return this.privacyNoticeUri;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.privacyNoticeUri = value;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001CCE RID: 7374 RVA: 0x0006B7D3 File Offset: 0x000699D3
		// (set) Token: 0x06001CCF RID: 7375 RVA: 0x0006B7DB File Offset: 0x000699DB
		public int PrivacyNoticeVersion
		{
			get
			{
				return this.privacyNoticeVersion;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.privacyNoticeVersion = value;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x0006B7EF File Offset: 0x000699EF
		// (set) Token: 0x06001CD1 RID: 7377 RVA: 0x0006B7F7 File Offset: 0x000699F7
		public ChannelParameterCollection ChannelParameters
		{
			get
			{
				return this.channelParameters;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.channelParameters = value;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x0006B80B File Offset: 0x00069A0B
		protected override bool IsMultiLegNegotiation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x0006B80E File Offset: 0x00069A0E
		public override XmlDictionaryString RequestSecurityTokenAction
		{
			get
			{
				return base.StandardsManager.SecureConversationDriver.IssueAction;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001CD4 RID: 7380 RVA: 0x0006B820 File Offset: 0x00069A20
		public override XmlDictionaryString RequestSecurityTokenResponseAction
		{
			get
			{
				return base.StandardsManager.SecureConversationDriver.IssueResponseAction;
			}
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x0006B832 File Offset: 0x00069A32
		public override void OnOpen(TimeSpan timeout)
		{
			if (this.BootstrapSecurityBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("BootstrapSecurityBindingElementNotSet", new object[]
				{
					base.GetType()
				})));
			}
			base.OnOpen(timeout);
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x0006B86C File Offset: 0x00069A6C
		public override void OnOpening()
		{
			base.OnOpening();
			if (this.credentialsHandle == null)
			{
				if (this.BootstrapSecurityBindingElement == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("BootstrapSecurityBindingElementNotSet", new object[]
					{
						base.GetType()
					})));
				}
				this.credentialsHandle = SecurityUtils.GetCredentialsHandle(this.BootstrapSecurityBindingElement, base.IssuerBindingContext);
				this.ownCredentialsHandle = true;
			}
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x0006B8D6 File Offset: 0x00069AD6
		public override void OnClose(TimeSpan timeout)
		{
			base.OnClose(timeout);
			this.FreeCredentialsHandle();
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x0006B8E5 File Offset: 0x00069AE5
		public override void OnAbort()
		{
			base.OnAbort();
			this.FreeCredentialsHandle();
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x0006B8F3 File Offset: 0x00069AF3
		private void FreeCredentialsHandle()
		{
			if (this.credentialsHandle != null)
			{
				if (this.ownCredentialsHandle)
				{
					this.credentialsHandle.Close();
				}
				this.credentialsHandle = null;
			}
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x0006B918 File Offset: 0x00069B18
		protected override IChannelFactory<IRequestChannel> GetNegotiationChannelFactory(IChannelFactory<IRequestChannel> transportChannelFactory, ChannelBuilder channelBuilder)
		{
			ISecurityCapabilities property = this.bootstrapSecurityBindingElement.GetProperty<ISecurityCapabilities>(base.IssuerBindingContext);
			SecurityCredentialsManager securityCredentialsManager = base.IssuerBindingContext.BindingParameters.Find<SecurityCredentialsManager>();
			if (securityCredentialsManager == null)
			{
				securityCredentialsManager = ClientCredentials.CreateDefaultCredentials();
			}
			this.bootstrapSecurityBindingElement.ReaderQuotas = base.IssuerBindingContext.GetInnerProperty<XmlDictionaryReaderQuotas>();
			if (this.bootstrapSecurityBindingElement.ReaderQuotas == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("EncodingBindingElementDoesNotHandleReaderQuotas")));
			}
			TransportBindingElement transportBindingElement = base.IssuerBindingContext.RemainingBindingElements.Find<TransportBindingElement>();
			if (transportBindingElement != null)
			{
				this.bootstrapSecurityBindingElement.MaxReceivedMessageSize = transportBindingElement.MaxReceivedMessageSize;
			}
			SecurityProtocolFactory securityProtocolFactory = this.bootstrapSecurityBindingElement.CreateSecurityProtocolFactory<IRequestChannel>(base.IssuerBindingContext.Clone(), securityCredentialsManager, false, base.IssuerBindingContext.Clone());
			MessageSecurityProtocolFactory messageSecurityProtocolFactory = securityProtocolFactory as MessageSecurityProtocolFactory;
			if (messageSecurityProtocolFactory != null)
			{
				messageSecurityProtocolFactory.ApplyConfidentiality = (messageSecurityProtocolFactory.ApplyIntegrity = (messageSecurityProtocolFactory.RequireConfidentiality = (messageSecurityProtocolFactory.RequireIntegrity = true)));
				MessagePartSpecification parts = new MessagePartSpecification(true);
				messageSecurityProtocolFactory.ProtectionRequirements.IncomingSignatureParts.AddParts(parts, this.RequestSecurityTokenAction);
				messageSecurityProtocolFactory.ProtectionRequirements.IncomingEncryptionParts.AddParts(parts, this.RequestSecurityTokenAction);
				messageSecurityProtocolFactory.ProtectionRequirements.OutgoingSignatureParts.AddParts(parts, this.RequestSecurityTokenResponseAction);
				messageSecurityProtocolFactory.ProtectionRequirements.OutgoingEncryptionParts.AddParts(parts, this.RequestSecurityTokenResponseAction);
			}
			securityProtocolFactory.PrivacyNoticeUri = this.PrivacyNoticeUri;
			securityProtocolFactory.PrivacyNoticeVersion = this.PrivacyNoticeVersion;
			return new SecurityChannelFactory<IRequestChannel>(property, base.IssuerBindingContext, channelBuilder, securityProtocolFactory, transportChannelFactory);
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x0006BAA8 File Offset: 0x00069CA8
		protected override IRequestChannel CreateClientChannel(EndpointAddress target, Uri via)
		{
			IRequestChannel requestChannel = base.CreateClientChannel(target, via);
			if (this.channelParameters != null)
			{
				this.channelParameters.PropagateChannelParameters(requestChannel);
			}
			if (this.ownCredentialsHandle)
			{
				ChannelParameterCollection property = requestChannel.GetProperty<ChannelParameterCollection>();
				if (property != null)
				{
					property.Add(new SspiIssuanceChannelParameter(true, this.credentialsHandle));
				}
			}
			return requestChannel;
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x0006BAF7 File Offset: 0x00069CF7
		protected override bool CreateNegotiationStateCompletesSynchronously(EndpointAddress target, Uri via)
		{
			return true;
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x0006BAFC File Offset: 0x00069CFC
		protected override AcceleratedTokenProviderState CreateNegotiationState(EndpointAddress target, Uri via, TimeSpan timeout)
		{
			byte[] array;
			if (this.keyEntropyMode == SecurityKeyEntropyMode.ClientEntropy || this.keyEntropyMode == SecurityKeyEntropyMode.CombinedEntropy)
			{
				array = new byte[base.SecurityAlgorithmSuite.DefaultSymmetricKeyLength / 8];
				CryptoHelper.FillRandomBytes(array);
			}
			else
			{
				array = null;
			}
			return new AcceleratedTokenProviderState(array);
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x0006BB3D File Offset: 0x00069D3D
		protected override IAsyncResult BeginCreateNegotiationState(EndpointAddress target, Uri via, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult<AcceleratedTokenProviderState>(this.CreateNegotiationState(target, via, timeout), callback, state);
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x0006BB51 File Offset: 0x00069D51
		protected override AcceleratedTokenProviderState EndCreateNegotiationState(IAsyncResult result)
		{
			return CompletedAsyncResult<AcceleratedTokenProviderState>.End(result);
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x0006BB5C File Offset: 0x00069D5C
		protected override BodyWriter GetFirstOutgoingMessageBody(AcceleratedTokenProviderState negotiationState, out MessageProperties messageProperties)
		{
			messageProperties = null;
			RequestSecurityToken requestSecurityToken = new RequestSecurityToken(base.StandardsManager);
			requestSecurityToken.Context = negotiationState.Context;
			requestSecurityToken.KeySize = base.SecurityAlgorithmSuite.DefaultSymmetricKeyLength;
			requestSecurityToken.TokenType = base.SecurityContextTokenUri;
			byte[] requestorEntropy = negotiationState.GetRequestorEntropy();
			if (requestorEntropy != null)
			{
				requestSecurityToken.SetRequestorEntropy(requestorEntropy);
			}
			requestSecurityToken.MakeReadOnly();
			return requestSecurityToken;
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x0006BBBC File Offset: 0x00069DBC
		protected override BodyWriter GetNextOutgoingMessageBody(Message incomingMessage, AcceleratedTokenProviderState negotiationState)
		{
			IssuanceTokenProviderBase<AcceleratedTokenProviderState>.ThrowIfFault(incomingMessage, base.TargetAddress);
			if (incomingMessage.Headers.Action != this.RequestSecurityTokenResponseAction.Value)
			{
				throw TraceUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("InvalidActionForNegotiationMessage", new object[]
				{
					incomingMessage.Headers.Action
				})), incomingMessage);
			}
			SecurityMessageProperty security = incomingMessage.Properties.Security;
			ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies;
			if (security != null && security.ServiceSecurityContext != null)
			{
				authorizationPolicies = security.ServiceSecurityContext.AuthorizationPolicies;
			}
			else
			{
				authorizationPolicies = EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance;
			}
			RequestSecurityTokenResponse requestSecurityTokenResponse = null;
			XmlDictionaryReader readerAtBodyContents = incomingMessage.GetReaderAtBodyContents();
			using (readerAtBodyContents)
			{
				if (base.StandardsManager.MessageSecurityVersion.TrustVersion != TrustVersion.WSTrustFeb2005)
				{
					if (base.StandardsManager.MessageSecurityVersion.TrustVersion == TrustVersion.WSTrust13)
					{
						RequestSecurityTokenResponseCollection requestSecurityTokenResponseCollection = base.StandardsManager.TrustDriver.CreateRequestSecurityTokenResponseCollection(readerAtBodyContents);
						using (IEnumerator<RequestSecurityTokenResponse> enumerator = requestSecurityTokenResponseCollection.RstrCollection.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								RequestSecurityTokenResponse requestSecurityTokenResponse2 = enumerator.Current;
								if (requestSecurityTokenResponse != null)
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("MoreThanOneRSTRInRSTRC")));
								}
								requestSecurityTokenResponse = requestSecurityTokenResponse2;
							}
							goto IL_13D;
						}
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
				}
				requestSecurityTokenResponse = RequestSecurityTokenResponse.CreateFrom(base.StandardsManager, readerAtBodyContents);
				IL_13D:
				incomingMessage.ReadFromBodyContentsToEnd(readerAtBodyContents);
			}
			if (requestSecurityTokenResponse.Context != negotiationState.Context)
			{
				throw TraceUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("BadSecurityNegotiationContext")), incomingMessage);
			}
			byte[] requestorEntropy = negotiationState.GetRequestorEntropy();
			GenericXmlSecurityToken issuedToken = requestSecurityTokenResponse.GetIssuedToken(null, null, this.keyEntropyMode, requestorEntropy, base.SecurityContextTokenUri, authorizationPolicies, base.SecurityAlgorithmSuite.DefaultSymmetricKeyLength, false);
			negotiationState.SetServiceToken(issuedToken);
			return null;
		}

		// Token: 0x04001DE0 RID: 7648
		internal const SecurityKeyEntropyMode defaultKeyEntropyMode = SecurityKeyEntropyMode.CombinedEntropy;

		// Token: 0x04001DE1 RID: 7649
		private SecurityKeyEntropyMode keyEntropyMode = SecurityKeyEntropyMode.CombinedEntropy;

		// Token: 0x04001DE2 RID: 7650
		private SecurityBindingElement bootstrapSecurityBindingElement;

		// Token: 0x04001DE3 RID: 7651
		private Uri privacyNoticeUri;

		// Token: 0x04001DE4 RID: 7652
		private int privacyNoticeVersion;

		// Token: 0x04001DE5 RID: 7653
		private ChannelParameterCollection channelParameters;

		// Token: 0x04001DE6 RID: 7654
		private SafeFreeCredentials credentialsHandle;

		// Token: 0x04001DE7 RID: 7655
		private bool ownCredentialsHandle;
	}
}
