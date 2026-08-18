using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000317 RID: 791
	internal sealed class AcceptorSessionSymmetricMessageSecurityProtocol : MessageSecurityProtocol, IAcceptorSecuritySessionProtocol
	{
		// Token: 0x06001B4F RID: 6991 RVA: 0x00066374 File Offset: 0x00064574
		public AcceptorSessionSymmetricMessageSecurityProtocol(SessionSymmetricMessageSecurityProtocolFactory factory, EndpointAddress target) : base(factory, target, null)
		{
			if (factory.ActAsInitiator)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ProtocolMustBeRecipient", new object[]
				{
					base.GetType().ToString()
				})));
			}
			this.requireDerivedKeys = factory.SecurityTokenParameters.RequireDerivedKeys;
			if (this.requireDerivedKeys)
			{
				SecurityTokenSerializer securityTokenSerializer = this.Factory.StandardsManager.SecurityTokenSerializer;
				WSSecureConversation secureConversation = (securityTokenSerializer is WSSecurityTokenSerializer) ? ((WSSecurityTokenSerializer)securityTokenSerializer).SecureConversation : new WSSecurityTokenSerializer(this.Factory.MessageSecurityVersion.SecurityVersion).SecureConversation;
				this.sessionStandardsManager = new SecurityStandardsManager(factory.MessageSecurityVersion, new DerivedKeyCachingSecurityTokenSerializer(2, false, secureConversation, securityTokenSerializer));
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001B50 RID: 6992 RVA: 0x0006643F File Offset: 0x0006463F
		private object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x00066447 File Offset: 0x00064647
		// (set) Token: 0x06001B52 RID: 6994 RVA: 0x0006644F File Offset: 0x0006464F
		public bool ReturnCorrelationState
		{
			get
			{
				return this.returnCorrelationState;
			}
			set
			{
				this.returnCorrelationState = value;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001B53 RID: 6995 RVA: 0x00066458 File Offset: 0x00064658
		protected override bool PerformIncomingAndOutgoingMessageExpectationChecks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001B54 RID: 6996 RVA: 0x0006645B File Offset: 0x0006465B
		private SessionSymmetricMessageSecurityProtocolFactory Factory
		{
			get
			{
				return (SessionSymmetricMessageSecurityProtocolFactory)base.MessageSecurityProtocolFactory;
			}
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x00066468 File Offset: 0x00064668
		public SecurityToken GetOutgoingSessionToken()
		{
			object obj = this.ThisLock;
			SecurityToken result;
			lock (obj)
			{
				result = this.outgoingSessionToken;
			}
			return result;
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x000664AC File Offset: 0x000646AC
		public void SetOutgoingSessionToken(SecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			object obj = this.ThisLock;
			lock (obj)
			{
				this.outgoingSessionToken = token;
				if (this.requireDerivedKeys)
				{
					string keyDerivationAlgorithm = SecurityUtils.GetKeyDerivationAlgorithm(this.sessionStandardsManager.MessageSecurityVersion.SecureConversationVersion);
					this.derivedSignatureToken = new DerivedKeySecurityToken(-1, 0, this.Factory.OutgoingAlgorithmSuite.GetSignatureKeyDerivationLength(token, this.sessionStandardsManager.MessageSecurityVersion.SecureConversationVersion), null, 16, token, this.Factory.SecurityTokenParameters.CreateKeyIdentifierClause(token, SecurityTokenReferenceStyle.External), keyDerivationAlgorithm, SecurityUtils.GenerateId());
					this.derivedEncryptionToken = new DerivedKeySecurityToken(-1, 0, this.Factory.OutgoingAlgorithmSuite.GetEncryptionKeyDerivationLength(token, this.sessionStandardsManager.MessageSecurityVersion.SecureConversationVersion), null, 16, token, this.Factory.SecurityTokenParameters.CreateKeyIdentifierClause(token, SecurityTokenReferenceStyle.External), keyDerivationAlgorithm, SecurityUtils.GenerateId());
				}
			}
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x000665B8 File Offset: 0x000647B8
		public void SetSessionTokenAuthenticator(UniqueId sessionId, SecurityTokenAuthenticator sessionTokenAuthenticator, SecurityTokenResolver sessionTokenResolver)
		{
			base.CommunicationObject.ThrowIfDisposedOrImmutable();
			object obj = this.ThisLock;
			lock (obj)
			{
				this.sessionId = sessionId;
				this.sessionTokenAuthenticator = sessionTokenAuthenticator;
				this.sessionTokenResolver = sessionTokenResolver;
				this.sessionResolverList = new ReadOnlyCollection<SecurityTokenResolver>(new List<SecurityTokenResolver>(1)
				{
					this.sessionTokenResolver
				});
			}
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x00066634 File Offset: 0x00064834
		private void GetTokensForOutgoingMessages(out SecurityToken signingToken, out SecurityToken encryptionToken, out SecurityTokenParameters tokenParameters)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.requireDerivedKeys)
				{
					signingToken = this.derivedSignatureToken;
					encryptionToken = this.derivedEncryptionToken;
				}
				else
				{
					SecurityToken securityToken;
					encryptionToken = (securityToken = this.outgoingSessionToken);
					signingToken = securityToken;
				}
			}
			tokenParameters = this.Factory.GetTokenParameters();
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x000666A4 File Offset: 0x000648A4
		protected override IAsyncResult BeginSecureOutgoingMessageCore(Message message, TimeSpan timeout, SecurityProtocolCorrelationState correlationState, AsyncCallback callback, object state)
		{
			SecurityToken signingToken;
			SecurityToken encryptionToken;
			SecurityTokenParameters tokenParameters;
			this.GetTokensForOutgoingMessages(out signingToken, out encryptionToken, out tokenParameters);
			this.SetUpDelayedSecurityExecution(ref message, signingToken, encryptionToken, tokenParameters, correlationState);
			return new CompletedAsyncResult<Message>(message, callback, state);
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x000666D4 File Offset: 0x000648D4
		protected override SecurityProtocolCorrelationState SecureOutgoingMessageCore(ref Message message, TimeSpan timeout, SecurityProtocolCorrelationState correlationState)
		{
			SecurityToken signingToken;
			SecurityToken encryptionToken;
			SecurityTokenParameters tokenParameters;
			this.GetTokensForOutgoingMessages(out signingToken, out encryptionToken, out tokenParameters);
			this.SetUpDelayedSecurityExecution(ref message, signingToken, encryptionToken, tokenParameters, correlationState);
			return null;
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x000666F9 File Offset: 0x000648F9
		protected override void EndSecureOutgoingMessageCore(IAsyncResult result, out Message message, out SecurityProtocolCorrelationState newCorrelationState)
		{
			message = CompletedAsyncResult<Message>.End(result);
			newCorrelationState = null;
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x00066708 File Offset: 0x00064908
		private void SetUpDelayedSecurityExecution(ref Message message, SecurityToken signingToken, SecurityToken encryptionToken, SecurityTokenParameters tokenParameters, SecurityProtocolCorrelationState correlationState)
		{
			string empty = string.Empty;
			SendSecurityHeader sendSecurityHeader = base.ConfigureSendSecurityHeader(message, empty, null, correlationState);
			if (this.Factory.ApplyIntegrity)
			{
				sendSecurityHeader.SetSigningToken(signingToken, tokenParameters);
			}
			if (this.Factory.ApplyConfidentiality)
			{
				sendSecurityHeader.SetEncryptionToken(encryptionToken, tokenParameters);
			}
			message = sendSecurityHeader.SetupExecution();
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x0006675C File Offset: 0x0006495C
		protected override SecurityProtocolCorrelationState VerifyIncomingMessageCore(ref Message message, string actor, TimeSpan timeout, SecurityProtocolCorrelationState[] correlationStates)
		{
			SessionSymmetricMessageSecurityProtocolFactory factory = this.Factory;
			IList<SupportingTokenAuthenticatorSpecification> list;
			ReceiveSecurityHeader receiveSecurityHeader = base.ConfigureReceiveSecurityHeader(message, string.Empty, correlationStates, this.requireDerivedKeys ? this.sessionStandardsManager : null, out list);
			receiveSecurityHeader.ConfigureSymmetricBindingServerReceiveHeader(this.sessionTokenAuthenticator, this.Factory.SecurityTokenParameters, list);
			receiveSecurityHeader.ConfigureOutOfBandTokenResolver(base.MergeOutOfBandResolvers(list, this.sessionResolverList));
			receiveSecurityHeader.EnforceDerivedKeyRequirement = (message.Headers.Action != factory.StandardsManager.SecureConversationDriver.CloseAction.Value);
			base.ProcessSecurityHeader(receiveSecurityHeader, ref message, null, timeout, correlationStates);
			SecurityToken signatureToken = receiveSecurityHeader.SignatureToken;
			SecurityContextSecurityToken securityContextSecurityToken = signatureToken as SecurityContextSecurityToken;
			if (securityContextSecurityToken == null || securityContextSecurityToken.ContextId != this.sessionId)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("NoSessionTokenPresentInMessage")));
			}
			base.AttachRecipientSecurityProperty(message, signatureToken, false, receiveSecurityHeader.BasicSupportingTokens, receiveSecurityHeader.EndorsingSupportingTokens, receiveSecurityHeader.SignedEndorsingSupportingTokens, receiveSecurityHeader.SignedSupportingTokens, receiveSecurityHeader.SecurityTokenAuthorizationPoliciesMapping);
			return base.GetCorrelationState(null, receiveSecurityHeader);
		}

		// Token: 0x04001D6B RID: 7531
		private SecurityToken outgoingSessionToken;

		// Token: 0x04001D6C RID: 7532
		private SecurityTokenAuthenticator sessionTokenAuthenticator;

		// Token: 0x04001D6D RID: 7533
		private SecurityTokenResolver sessionTokenResolver;

		// Token: 0x04001D6E RID: 7534
		private ReadOnlyCollection<SecurityTokenResolver> sessionResolverList;

		// Token: 0x04001D6F RID: 7535
		private bool returnCorrelationState;

		// Token: 0x04001D70 RID: 7536
		private DerivedKeySecurityToken derivedSignatureToken;

		// Token: 0x04001D71 RID: 7537
		private DerivedKeySecurityToken derivedEncryptionToken;

		// Token: 0x04001D72 RID: 7538
		private UniqueId sessionId;

		// Token: 0x04001D73 RID: 7539
		private SecurityStandardsManager sessionStandardsManager;

		// Token: 0x04001D74 RID: 7540
		private object thisLock = new object();

		// Token: 0x04001D75 RID: 7541
		private bool requireDerivedKeys;
	}
}
