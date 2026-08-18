using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x0200031B RID: 795
	internal sealed class InitiatorSessionSymmetricTransportSecurityProtocol : TransportSecurityProtocol, IInitiatorSecuritySessionProtocol
	{
		// Token: 0x06001B86 RID: 7046 RVA: 0x00067180 File Offset: 0x00065380
		public InitiatorSessionSymmetricTransportSecurityProtocol(SessionSymmetricTransportSecurityProtocolFactory factory, EndpointAddress target, Uri via) : base(factory, target, via)
		{
			if (!factory.ActAsInitiator)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("ProtocolMustBeInitiator", new object[]
				{
					"InitiatorSessionSymmetricTransportSecurityProtocol"
				})));
			}
			this.requireDerivedKeys = factory.SecurityTokenParameters.RequireDerivedKeys;
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001B87 RID: 7047 RVA: 0x000671E2 File Offset: 0x000653E2
		private SessionSymmetricTransportSecurityProtocolFactory Factory
		{
			get
			{
				return (SessionSymmetricTransportSecurityProtocolFactory)base.SecurityProtocolFactory;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001B88 RID: 7048 RVA: 0x000671EF File Offset: 0x000653EF
		private object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001B89 RID: 7049 RVA: 0x000671F7 File Offset: 0x000653F7
		// (set) Token: 0x06001B8A RID: 7050 RVA: 0x000671FA File Offset: 0x000653FA
		public bool ReturnCorrelationState
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x000671FC File Offset: 0x000653FC
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

		// Token: 0x06001B8C RID: 7052 RVA: 0x00067240 File Offset: 0x00065440
		public void SetIdentityCheckAuthenticator(SecurityTokenAuthenticator authenticator)
		{
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x00067244 File Offset: 0x00065444
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
					string keyDerivationAlgorithm = SecurityUtils.GetKeyDerivationAlgorithm(this.Factory.MessageSecurityVersion.SecureConversationVersion);
					this.derivedSignatureToken = new DerivedKeySecurityToken(-1, 0, this.Factory.OutgoingAlgorithmSuite.GetSignatureKeyDerivationLength(token, this.Factory.MessageSecurityVersion.SecureConversationVersion), null, 16, token, this.Factory.SecurityTokenParameters.CreateKeyIdentifierClause(token, SecurityTokenReferenceStyle.Internal), keyDerivationAlgorithm, SecurityUtils.GenerateId());
				}
			}
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x00067300 File Offset: 0x00065500
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

		// Token: 0x06001B8F RID: 7055 RVA: 0x00067344 File Offset: 0x00065544
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

		// Token: 0x06001B90 RID: 7056 RVA: 0x000673A0 File Offset: 0x000655A0
		private void GetTokensForOutgoingMessages(out SecurityToken signingToken, out SecurityToken sourceToken, out SecurityTokenParameters tokenParameters)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.requireDerivedKeys)
				{
					signingToken = this.derivedSignatureToken;
					sourceToken = this.outgoingSessionToken;
				}
				else
				{
					signingToken = this.outgoingSessionToken;
					sourceToken = null;
				}
			}
			tokenParameters = this.Factory.GetTokenParameters();
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x0006740C File Offset: 0x0006560C
		internal void SetupDelayedSecurityExecution(string actor, ref Message message, SecurityToken signingToken, SecurityToken sourceToken, SecurityTokenParameters tokenParameters, IList<SupportingTokenSpecification> supportingTokens)
		{
			SendSecurityHeader sendSecurityHeader = base.CreateSendSecurityHeaderForTransportProtocol(message, actor, this.Factory);
			sendSecurityHeader.RequireMessageProtection = false;
			if (sourceToken != null)
			{
				sendSecurityHeader.AddPrerequisiteToken(sourceToken);
			}
			base.AddSupportingTokens(sendSecurityHeader, supportingTokens);
			sendSecurityHeader.AddEndorsingSupportingToken(signingToken, tokenParameters);
			message = sendSecurityHeader.SetupExecution();
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x00067458 File Offset: 0x00065658
		protected override void SecureOutgoingMessageAtInitiator(ref Message message, string actor, TimeSpan timeout)
		{
			SecurityToken signingToken;
			SecurityToken sourceToken;
			SecurityTokenParameters tokenParameters;
			this.GetTokensForOutgoingMessages(out signingToken, out sourceToken, out tokenParameters);
			IList<SupportingTokenSpecification> supportingTokens;
			base.TryGetSupportingTokens(base.SecurityProtocolFactory, base.Target, base.Via, message, timeout, true, out supportingTokens);
			this.SetupDelayedSecurityExecution(actor, ref message, signingToken, sourceToken, tokenParameters, supportingTokens);
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x0006749C File Offset: 0x0006569C
		protected override IAsyncResult BeginSecureOutgoingMessageAtInitiatorCore(Message message, string actor, TimeSpan timeout, AsyncCallback callback, object state)
		{
			SecurityToken signingToken;
			SecurityToken sourceToken;
			SecurityTokenParameters tokenParameters;
			this.GetTokensForOutgoingMessages(out signingToken, out sourceToken, out tokenParameters);
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			IList<SupportingTokenSpecification> supportingTokens;
			if (!base.TryGetSupportingTokens(base.SecurityProtocolFactory, base.Target, base.Via, message, timeoutHelper.RemainingTime(), false, out supportingTokens))
			{
				return new InitiatorSessionSymmetricTransportSecurityProtocol.SecureOutgoingMessageAsyncResult(actor, message, this, signingToken, sourceToken, tokenParameters, timeoutHelper.RemainingTime(), callback, state);
			}
			this.SetupDelayedSecurityExecution(actor, ref message, signingToken, sourceToken, tokenParameters, supportingTokens);
			return new CompletedAsyncResult<Message>(message, callback, state);
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x00067510 File Offset: 0x00065710
		protected override Message EndSecureOutgoingMessageAtInitiatorCore(IAsyncResult result)
		{
			if (result is CompletedAsyncResult<Message>)
			{
				return CompletedAsyncResult<Message>.End(result);
			}
			return InitiatorSessionSymmetricTransportSecurityProtocol.SecureOutgoingMessageAsyncResult.End(result);
		}

		// Token: 0x04001D86 RID: 7558
		private SecurityToken outgoingSessionToken;

		// Token: 0x04001D87 RID: 7559
		private List<SecurityToken> incomingSessionTokens;

		// Token: 0x04001D88 RID: 7560
		private object thisLock = new object();

		// Token: 0x04001D89 RID: 7561
		private DerivedKeySecurityToken derivedSignatureToken;

		// Token: 0x04001D8A RID: 7562
		private bool requireDerivedKeys;

		// Token: 0x02000B6E RID: 2926
		private sealed class SecureOutgoingMessageAsyncResult : SecurityProtocol.GetSupportingTokensAsyncResult
		{
			// Token: 0x06007268 RID: 29288 RVA: 0x001AB2A0 File Offset: 0x001A94A0
			public SecureOutgoingMessageAsyncResult(string actor, Message message, InitiatorSessionSymmetricTransportSecurityProtocol binding, SecurityToken signingToken, SecurityToken sourceToken, SecurityTokenParameters tokenParameters, TimeSpan timeout, AsyncCallback callback, object state) : base(message, binding, timeout, callback, state)
			{
				this.actor = actor;
				this.message = message;
				this.binding = binding;
				this.signingToken = signingToken;
				this.sourceToken = sourceToken;
				this.tokenParameters = tokenParameters;
				base.Start();
			}

			// Token: 0x06007269 RID: 29289 RVA: 0x001AB2EE File Offset: 0x001A94EE
			protected override bool OnGetSupportingTokensDone(TimeSpan timeout)
			{
				this.binding.SetupDelayedSecurityExecution(this.actor, ref this.message, this.signingToken, this.sourceToken, this.tokenParameters, base.SupportingTokens);
				return true;
			}

			// Token: 0x0600726A RID: 29290 RVA: 0x001AB320 File Offset: 0x001A9520
			internal static Message End(IAsyncResult result)
			{
				InitiatorSessionSymmetricTransportSecurityProtocol.SecureOutgoingMessageAsyncResult secureOutgoingMessageAsyncResult = AsyncResult.End<InitiatorSessionSymmetricTransportSecurityProtocol.SecureOutgoingMessageAsyncResult>(result);
				return secureOutgoingMessageAsyncResult.message;
			}

			// Token: 0x040040C7 RID: 16583
			private Message message;

			// Token: 0x040040C8 RID: 16584
			private string actor;

			// Token: 0x040040C9 RID: 16585
			private SecurityToken signingToken;

			// Token: 0x040040CA RID: 16586
			private SecurityToken sourceToken;

			// Token: 0x040040CB RID: 16587
			private SecurityTokenParameters tokenParameters;

			// Token: 0x040040CC RID: 16588
			private InitiatorSessionSymmetricTransportSecurityProtocol binding;
		}
	}
}
