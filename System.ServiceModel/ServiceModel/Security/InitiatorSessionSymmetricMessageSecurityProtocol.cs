using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x0200031A RID: 794
	internal sealed class InitiatorSessionSymmetricMessageSecurityProtocol : MessageSecurityProtocol, IInitiatorSecuritySessionProtocol
	{
		// Token: 0x06001B74 RID: 7028 RVA: 0x00066B8C File Offset: 0x00064D8C
		public InitiatorSessionSymmetricMessageSecurityProtocol(SessionSymmetricMessageSecurityProtocolFactory factory, EndpointAddress target, Uri via) : base(factory, target, via)
		{
			if (!factory.ActAsInitiator)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("ProtocolMustBeInitiator", new object[]
				{
					"InitiatorSessionSymmetricMessageSecurityProtocol"
				})));
			}
			this.requireDerivedKeys = factory.SecurityTokenParameters.RequireDerivedKeys;
			if (this.requireDerivedKeys)
			{
				SecurityTokenSerializer securityTokenSerializer = this.Factory.StandardsManager.SecurityTokenSerializer;
				WSSecureConversation secureConversation = (securityTokenSerializer is WSSecurityTokenSerializer) ? ((WSSecurityTokenSerializer)securityTokenSerializer).SecureConversation : new WSSecurityTokenSerializer(this.Factory.MessageSecurityVersion.SecurityVersion).SecureConversation;
				this.sessionStandardsManager = new SecurityStandardsManager(factory.MessageSecurityVersion, new DerivedKeyCachingSecurityTokenSerializer(2, true, secureConversation, securityTokenSerializer));
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001B75 RID: 7029 RVA: 0x00066C51 File Offset: 0x00064E51
		private object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001B76 RID: 7030 RVA: 0x00066C59 File Offset: 0x00064E59
		protected override bool PerformIncomingAndOutgoingMessageExpectationChecks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001B77 RID: 7031 RVA: 0x00066C5C File Offset: 0x00064E5C
		// (set) Token: 0x06001B78 RID: 7032 RVA: 0x00066C64 File Offset: 0x00064E64
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

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001B79 RID: 7033 RVA: 0x00066C6D File Offset: 0x00064E6D
		private SessionSymmetricMessageSecurityProtocolFactory Factory
		{
			get
			{
				return (SessionSymmetricMessageSecurityProtocolFactory)base.MessageSecurityProtocolFactory;
			}
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x00066C7C File Offset: 0x00064E7C
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

		// Token: 0x06001B7B RID: 7035 RVA: 0x00066CC0 File Offset: 0x00064EC0
		public void SetIdentityCheckAuthenticator(SecurityTokenAuthenticator authenticator)
		{
			this.sessionTokenAuthenticator = authenticator;
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x00066CCC File Offset: 0x00064ECC
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
					this.derivedSignatureToken = new DerivedKeySecurityToken(-1, 0, this.Factory.OutgoingAlgorithmSuite.GetSignatureKeyDerivationLength(token, this.sessionStandardsManager.MessageSecurityVersion.SecureConversationVersion), null, 16, token, this.Factory.SecurityTokenParameters.CreateKeyIdentifierClause(token, SecurityTokenReferenceStyle.Internal), keyDerivationAlgorithm, SecurityUtils.GenerateId());
					this.derivedEncryptionToken = new DerivedKeySecurityToken(-1, 0, this.Factory.OutgoingAlgorithmSuite.GetEncryptionKeyDerivationLength(token, this.sessionStandardsManager.MessageSecurityVersion.SecureConversationVersion), null, 16, token, this.Factory.SecurityTokenParameters.CreateKeyIdentifierClause(token, SecurityTokenReferenceStyle.Internal), keyDerivationAlgorithm, SecurityUtils.GenerateId());
				}
			}
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x00066DD8 File Offset: 0x00064FD8
		public List<SecurityToken> GetIncomingSessionTokens()
		{
			object obj = this.ThisLock;
			List<SecurityToken> result;
			lock (obj)
			{
				result = this.incomingSessionTokens;
			}
			return result;
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x00066E1C File Offset: 0x0006501C
		public void SetIncomingSessionTokens(List<SecurityToken> tokens)
		{
			if (tokens == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokens");
			}
			object obj = this.ThisLock;
			lock (obj)
			{
				this.incomingSessionTokens = new List<SecurityToken>(tokens);
			}
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x00066E78 File Offset: 0x00065078
		private void GetTokensForOutgoingMessages(out SecurityToken signingToken, out SecurityToken encryptionToken, out SecurityToken sourceToken, out SecurityTokenParameters tokenParameters)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.requireDerivedKeys)
				{
					signingToken = this.derivedSignatureToken;
					encryptionToken = this.derivedEncryptionToken;
					sourceToken = this.outgoingSessionToken;
				}
				else
				{
					SecurityToken securityToken;
					encryptionToken = (securityToken = this.outgoingSessionToken);
					signingToken = securityToken;
					sourceToken = null;
				}
			}
			if (this.Factory.ApplyConfidentiality)
			{
				base.EnsureOutgoingIdentity(sourceToken ?? encryptionToken, this.sessionTokenAuthenticator);
			}
			tokenParameters = this.Factory.GetTokenParameters();
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x00066F14 File Offset: 0x00065114
		protected override IAsyncResult BeginSecureOutgoingMessageCore(Message message, TimeSpan timeout, SecurityProtocolCorrelationState correlationState, AsyncCallback callback, object state)
		{
			SecurityToken signingToken;
			SecurityToken encryptionToken;
			SecurityToken sourceToken;
			SecurityTokenParameters tokenParameters;
			this.GetTokensForOutgoingMessages(out signingToken, out encryptionToken, out sourceToken, out tokenParameters);
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			IList<SupportingTokenSpecification> supportingTokens;
			if (base.TryGetSupportingTokens(this.Factory, base.Target, base.Via, message, timeoutHelper.RemainingTime(), false, out supportingTokens))
			{
				SecurityProtocolCorrelationState securityProtocolCorrelationState = this.CreateCorrelationStateIfRequired();
				this.SetUpDelayedSecurityExecution(ref message, signingToken, encryptionToken, sourceToken, tokenParameters, supportingTokens, securityProtocolCorrelationState);
				return new CompletedAsyncResult<Message, SecurityProtocolCorrelationState>(message, securityProtocolCorrelationState, callback, state);
			}
			return new InitiatorSessionSymmetricMessageSecurityProtocol.SecureOutgoingMessageAsyncResult(message, this, signingToken, encryptionToken, sourceToken, tokenParameters, timeoutHelper.RemainingTime(), callback, state);
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x00066F97 File Offset: 0x00065197
		internal SecurityProtocolCorrelationState CreateCorrelationStateIfRequired()
		{
			if (this.ReturnCorrelationState)
			{
				return new SecurityProtocolCorrelationState(null);
			}
			return null;
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x00066FAC File Offset: 0x000651AC
		protected override SecurityProtocolCorrelationState SecureOutgoingMessageCore(ref Message message, TimeSpan timeout, SecurityProtocolCorrelationState correlationState)
		{
			SecurityToken signingToken;
			SecurityToken encryptionToken;
			SecurityToken sourceToken;
			SecurityTokenParameters tokenParameters;
			this.GetTokensForOutgoingMessages(out signingToken, out encryptionToken, out sourceToken, out tokenParameters);
			SecurityProtocolCorrelationState securityProtocolCorrelationState = this.CreateCorrelationStateIfRequired();
			IList<SupportingTokenSpecification> supportingTokens;
			base.TryGetSupportingTokens(base.SecurityProtocolFactory, base.Target, base.Via, message, timeout, true, out supportingTokens);
			this.SetUpDelayedSecurityExecution(ref message, signingToken, encryptionToken, sourceToken, tokenParameters, supportingTokens, securityProtocolCorrelationState);
			return securityProtocolCorrelationState;
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x00066FFF File Offset: 0x000651FF
		protected override void EndSecureOutgoingMessageCore(IAsyncResult result, out Message message, out SecurityProtocolCorrelationState newCorrelationState)
		{
			if (result is CompletedAsyncResult<Message, SecurityProtocolCorrelationState>)
			{
				message = CompletedAsyncResult<Message, SecurityProtocolCorrelationState>.End(result, out newCorrelationState);
				return;
			}
			message = InitiatorSessionSymmetricMessageSecurityProtocol.SecureOutgoingMessageAsyncResult.End(result, out newCorrelationState);
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x0006701C File Offset: 0x0006521C
		internal void SetUpDelayedSecurityExecution(ref Message message, SecurityToken signingToken, SecurityToken encryptionToken, SecurityToken sourceToken, SecurityTokenParameters tokenParameters, IList<SupportingTokenSpecification> supportingTokens, SecurityProtocolCorrelationState correlationState)
		{
			SessionSymmetricMessageSecurityProtocolFactory factory = this.Factory;
			string empty = string.Empty;
			SendSecurityHeader sendSecurityHeader = base.ConfigureSendSecurityHeader(message, empty, supportingTokens, correlationState);
			if (sourceToken != null)
			{
				sendSecurityHeader.AddPrerequisiteToken(sourceToken);
			}
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

		// Token: 0x06001B85 RID: 7045 RVA: 0x00067084 File Offset: 0x00065284
		protected override SecurityProtocolCorrelationState VerifyIncomingMessageCore(ref Message message, string actor, TimeSpan timeout, SecurityProtocolCorrelationState[] correlationStates)
		{
			SessionSymmetricMessageSecurityProtocolFactory factory = this.Factory;
			IList<SupportingTokenAuthenticatorSpecification> list;
			ReceiveSecurityHeader receiveSecurityHeader = base.ConfigureReceiveSecurityHeader(message, string.Empty, correlationStates, this.requireDerivedKeys ? this.sessionStandardsManager : null, out list);
			List<SecurityToken> list2 = this.GetIncomingSessionTokens();
			receiveSecurityHeader.ConfigureSymmetricBindingClientReceiveHeader(list2, this.Factory.SecurityTokenParameters);
			receiveSecurityHeader.EnforceDerivedKeyRequirement = (message.Headers.Action != factory.StandardsManager.SecureConversationDriver.CloseResponseAction.Value);
			base.ProcessSecurityHeader(receiveSecurityHeader, ref message, null, timeout, correlationStates);
			SecurityToken signatureToken = receiveSecurityHeader.SignatureToken;
			bool flag = false;
			for (int i = 0; i < list2.Count; i++)
			{
				if (signatureToken == list2[i])
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("NoSessionTokenPresentInMessage")));
			}
			if (factory.RequireIntegrity)
			{
				ReadOnlyCollection<IAuthorizationPolicy> protectionTokenPolicies = this.sessionTokenAuthenticator.ValidateToken(signatureToken);
				base.DoIdentityCheckAndAttachInitiatorSecurityProperty(message, signatureToken, protectionTokenPolicies);
			}
			return null;
		}

		// Token: 0x04001D7D RID: 7549
		private SecurityToken outgoingSessionToken;

		// Token: 0x04001D7E RID: 7550
		private SecurityTokenAuthenticator sessionTokenAuthenticator;

		// Token: 0x04001D7F RID: 7551
		private List<SecurityToken> incomingSessionTokens;

		// Token: 0x04001D80 RID: 7552
		private DerivedKeySecurityToken derivedSignatureToken;

		// Token: 0x04001D81 RID: 7553
		private DerivedKeySecurityToken derivedEncryptionToken;

		// Token: 0x04001D82 RID: 7554
		private SecurityStandardsManager sessionStandardsManager;

		// Token: 0x04001D83 RID: 7555
		private bool requireDerivedKeys;

		// Token: 0x04001D84 RID: 7556
		private object thisLock = new object();

		// Token: 0x04001D85 RID: 7557
		private bool returnCorrelationState;

		// Token: 0x02000B6D RID: 2925
		private sealed class SecureOutgoingMessageAsyncResult : SecurityProtocol.GetSupportingTokensAsyncResult
		{
			// Token: 0x06007265 RID: 29285 RVA: 0x001AB1D8 File Offset: 0x001A93D8
			public SecureOutgoingMessageAsyncResult(Message message, InitiatorSessionSymmetricMessageSecurityProtocol binding, SecurityToken signingToken, SecurityToken encryptionToken, SecurityToken sourceToken, SecurityTokenParameters tokenParameters, TimeSpan timeout, AsyncCallback callback, object state) : base(message, binding, timeout, callback, state)
			{
				this.message = message;
				this.binding = binding;
				this.signingToken = signingToken;
				this.encryptionToken = encryptionToken;
				this.sourceToken = sourceToken;
				this.tokenParameters = tokenParameters;
				base.Start();
			}

			// Token: 0x06007266 RID: 29286 RVA: 0x001AB228 File Offset: 0x001A9428
			protected override bool OnGetSupportingTokensDone(TimeSpan timeout)
			{
				this.newCorrelationState = this.binding.CreateCorrelationStateIfRequired();
				this.binding.SetUpDelayedSecurityExecution(ref this.message, this.signingToken, this.encryptionToken, this.sourceToken, this.tokenParameters, base.SupportingTokens, this.newCorrelationState);
				return true;
			}

			// Token: 0x06007267 RID: 29287 RVA: 0x001AB27C File Offset: 0x001A947C
			internal static Message End(IAsyncResult result, out SecurityProtocolCorrelationState newCorrelationState)
			{
				InitiatorSessionSymmetricMessageSecurityProtocol.SecureOutgoingMessageAsyncResult secureOutgoingMessageAsyncResult = AsyncResult.End<InitiatorSessionSymmetricMessageSecurityProtocol.SecureOutgoingMessageAsyncResult>(result);
				newCorrelationState = secureOutgoingMessageAsyncResult.newCorrelationState;
				return secureOutgoingMessageAsyncResult.message;
			}

			// Token: 0x040040C0 RID: 16576
			private Message message;

			// Token: 0x040040C1 RID: 16577
			private InitiatorSessionSymmetricMessageSecurityProtocol binding;

			// Token: 0x040040C2 RID: 16578
			private SecurityToken signingToken;

			// Token: 0x040040C3 RID: 16579
			private SecurityToken encryptionToken;

			// Token: 0x040040C4 RID: 16580
			private SecurityToken sourceToken;

			// Token: 0x040040C5 RID: 16581
			private SecurityTokenParameters tokenParameters;

			// Token: 0x040040C6 RID: 16582
			private SecurityProtocolCorrelationState newCorrelationState;
		}
	}
}
