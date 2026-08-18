using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x020002CB RID: 715
	internal sealed class AsymmetricSecurityProtocol : MessageSecurityProtocol
	{
		// Token: 0x0600170D RID: 5901 RVA: 0x00057419 File Offset: 0x00055619
		public AsymmetricSecurityProtocol(AsymmetricSecurityProtocolFactory factory, EndpointAddress target, Uri via) : base(factory, target, via)
		{
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x0600170E RID: 5902 RVA: 0x00057424 File Offset: 0x00055624
		protected override bool DoAutomaticEncryptionMatch
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x00057427 File Offset: 0x00055627
		private AsymmetricSecurityProtocolFactory Factory
		{
			get
			{
				return (AsymmetricSecurityProtocolFactory)base.MessageSecurityProtocolFactory;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x00057434 File Offset: 0x00055634
		public SecurityTokenProvider InitiatorCryptoTokenProvider
		{
			get
			{
				base.CommunicationObject.ThrowIfNotOpened();
				return this.initiatorCryptoTokenProvider;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x00057447 File Offset: 0x00055647
		public SecurityTokenAuthenticator InitiatorAsymmetricTokenAuthenticator
		{
			get
			{
				base.CommunicationObject.ThrowIfNotOpened();
				return this.initiatorAsymmetricTokenAuthenticator;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001712 RID: 5906 RVA: 0x0005745A File Offset: 0x0005565A
		public SecurityTokenProvider InitiatorAsymmetricTokenProvider
		{
			get
			{
				base.CommunicationObject.ThrowIfNotOpened();
				return this.initiatorAsymmetricTokenProvider;
			}
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x00057470 File Offset: 0x00055670
		public override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.OnOpen(timeoutHelper.RemainingTime());
			if (this.Factory.ActAsInitiator)
			{
				if (this.Factory.ApplyIntegrity)
				{
					InitiatorServiceModelSecurityTokenRequirement initiatorServiceModelSecurityTokenRequirement = base.CreateInitiatorSecurityTokenRequirement();
					this.Factory.CryptoTokenParameters.InitializeSecurityTokenRequirement(initiatorServiceModelSecurityTokenRequirement);
					initiatorServiceModelSecurityTokenRequirement.KeyUsage = SecurityKeyUsage.Signature;
					initiatorServiceModelSecurityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.MessageDirectionProperty] = MessageDirection.Output;
					this.initiatorCryptoTokenProvider = this.Factory.SecurityTokenManager.CreateSecurityTokenProvider(initiatorServiceModelSecurityTokenRequirement);
					SecurityUtils.OpenTokenProviderIfRequired(this.initiatorCryptoTokenProvider, timeoutHelper.RemainingTime());
				}
				if (this.Factory.RequireIntegrity || this.Factory.ApplyConfidentiality)
				{
					InitiatorServiceModelSecurityTokenRequirement initiatorServiceModelSecurityTokenRequirement2 = base.CreateInitiatorSecurityTokenRequirement();
					this.Factory.AsymmetricTokenParameters.InitializeSecurityTokenRequirement(initiatorServiceModelSecurityTokenRequirement2);
					initiatorServiceModelSecurityTokenRequirement2.KeyUsage = SecurityKeyUsage.Exchange;
					initiatorServiceModelSecurityTokenRequirement2.Properties[ServiceModelSecurityTokenRequirement.MessageDirectionProperty] = (this.Factory.ApplyConfidentiality ? MessageDirection.Output : MessageDirection.Input);
					this.initiatorAsymmetricTokenProvider = this.Factory.SecurityTokenManager.CreateSecurityTokenProvider(initiatorServiceModelSecurityTokenRequirement2);
					SecurityUtils.OpenTokenProviderIfRequired(this.initiatorAsymmetricTokenProvider, timeoutHelper.RemainingTime());
					InitiatorServiceModelSecurityTokenRequirement initiatorServiceModelSecurityTokenRequirement3 = base.CreateInitiatorSecurityTokenRequirement();
					this.Factory.AsymmetricTokenParameters.InitializeSecurityTokenRequirement(initiatorServiceModelSecurityTokenRequirement3);
					initiatorServiceModelSecurityTokenRequirement3.IsOutOfBandToken = !this.Factory.AllowSerializedSigningTokenOnReply;
					initiatorServiceModelSecurityTokenRequirement3.KeyUsage = SecurityKeyUsage.Exchange;
					initiatorServiceModelSecurityTokenRequirement3.Properties[ServiceModelSecurityTokenRequirement.MessageDirectionProperty] = (this.Factory.ApplyConfidentiality ? MessageDirection.Output : MessageDirection.Input);
					SecurityTokenResolver securityTokenResolver;
					this.initiatorAsymmetricTokenAuthenticator = this.Factory.SecurityTokenManager.CreateSecurityTokenAuthenticator(initiatorServiceModelSecurityTokenRequirement3, out securityTokenResolver);
					SecurityUtils.OpenTokenAuthenticatorIfRequired(this.initiatorAsymmetricTokenAuthenticator, timeoutHelper.RemainingTime());
				}
			}
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x0005761C File Offset: 0x0005581C
		public override void OnAbort()
		{
			if (this.Factory.ActAsInitiator)
			{
				if (this.initiatorCryptoTokenProvider != null)
				{
					SecurityUtils.AbortTokenProviderIfRequired(this.initiatorCryptoTokenProvider);
				}
				if (this.initiatorAsymmetricTokenProvider != null)
				{
					SecurityUtils.AbortTokenProviderIfRequired(this.initiatorAsymmetricTokenProvider);
				}
				if (this.initiatorAsymmetricTokenAuthenticator != null)
				{
					SecurityUtils.AbortTokenAuthenticatorIfRequired(this.initiatorAsymmetricTokenAuthenticator);
				}
			}
			base.OnAbort();
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x00057678 File Offset: 0x00055878
		public override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (this.Factory.ActAsInitiator)
			{
				if (this.initiatorCryptoTokenProvider != null)
				{
					SecurityUtils.CloseTokenProviderIfRequired(this.initiatorCryptoTokenProvider, timeoutHelper.RemainingTime());
				}
				if (this.initiatorAsymmetricTokenProvider != null)
				{
					SecurityUtils.CloseTokenProviderIfRequired(this.initiatorAsymmetricTokenProvider, timeoutHelper.RemainingTime());
				}
				if (this.initiatorAsymmetricTokenAuthenticator != null)
				{
					SecurityUtils.CloseTokenAuthenticatorIfRequired(this.initiatorAsymmetricTokenAuthenticator, timeoutHelper.RemainingTime());
				}
			}
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x000576F8 File Offset: 0x000558F8
		protected override IAsyncResult BeginSecureOutgoingMessageCore(Message message, TimeSpan timeout, SecurityProtocolCorrelationState correlationState, AsyncCallback callback, object state)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			SecurityToken encryptingToken;
			SecurityToken signingToken;
			IList<SupportingTokenSpecification> supportingTokens;
			SecurityProtocolCorrelationState securityProtocolCorrelationState;
			if (this.TryGetTokenSynchronouslyForOutgoingSecurity(message, correlationState, false, timeoutHelper.RemainingTime(), out encryptingToken, out signingToken, out supportingTokens, out securityProtocolCorrelationState))
			{
				this.SetUpDelayedSecurityExecution(ref message, encryptingToken, signingToken, supportingTokens, base.GetSignatureConfirmationCorrelationState(correlationState, securityProtocolCorrelationState));
				return new CompletedAsyncResult<Message, SecurityProtocolCorrelationState>(message, securityProtocolCorrelationState, callback, state);
			}
			if (!this.Factory.ActAsInitiator)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SendingOutgoingmessageOnRecipient")));
			}
			AsymmetricSecurityProtocolFactory factory = this.Factory;
			SecurityTokenProvider primaryProvider = factory.ApplyConfidentiality ? this.initiatorAsymmetricTokenProvider : null;
			SecurityTokenProvider secondaryProvider = factory.ApplyIntegrity ? this.initiatorCryptoTokenProvider : null;
			return new AsymmetricSecurityProtocol.SecureOutgoingMessageAsyncResult(message, this, primaryProvider, secondaryProvider, factory.ApplyConfidentiality, this.initiatorAsymmetricTokenAuthenticator, correlationState, timeoutHelper.RemainingTime(), callback, state);
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x000577C1 File Offset: 0x000559C1
		protected override void EndSecureOutgoingMessageCore(IAsyncResult result, out Message message, out SecurityProtocolCorrelationState newCorrelationState)
		{
			if (result is CompletedAsyncResult<Message, SecurityProtocolCorrelationState>)
			{
				message = CompletedAsyncResult<Message, SecurityProtocolCorrelationState>.End(result, out newCorrelationState);
				return;
			}
			message = MessageSecurityProtocol.GetTwoTokensAndSetUpSecurityAsyncResult.End(result, out newCorrelationState);
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x000577E0 File Offset: 0x000559E0
		protected override SecurityProtocolCorrelationState SecureOutgoingMessageCore(ref Message message, TimeSpan timeout, SecurityProtocolCorrelationState correlationState)
		{
			SecurityToken encryptingToken;
			SecurityToken signingToken;
			IList<SupportingTokenSpecification> supportingTokens;
			SecurityProtocolCorrelationState securityProtocolCorrelationState;
			this.TryGetTokenSynchronouslyForOutgoingSecurity(message, correlationState, true, timeout, out encryptingToken, out signingToken, out supportingTokens, out securityProtocolCorrelationState);
			this.SetUpDelayedSecurityExecution(ref message, encryptingToken, signingToken, supportingTokens, base.GetSignatureConfirmationCorrelationState(correlationState, securityProtocolCorrelationState));
			return securityProtocolCorrelationState;
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x00057814 File Offset: 0x00055A14
		private void SetUpDelayedSecurityExecution(ref Message message, SecurityToken encryptingToken, SecurityToken signingToken, IList<SupportingTokenSpecification> supportingTokens, SecurityProtocolCorrelationState correlationState)
		{
			AsymmetricSecurityProtocolFactory factory = this.Factory;
			string empty = string.Empty;
			SendSecurityHeader sendSecurityHeader = base.ConfigureSendSecurityHeader(message, empty, supportingTokens, correlationState);
			SecurityTokenParameters tokenParameters = this.Factory.ActAsInitiator ? this.Factory.CryptoTokenParameters : this.Factory.AsymmetricTokenParameters;
			SecurityTokenParameters tokenParameters2 = this.Factory.ActAsInitiator ? this.Factory.AsymmetricTokenParameters : this.Factory.CryptoTokenParameters;
			if (this.Factory.ApplyIntegrity || sendSecurityHeader.HasSignedTokens)
			{
				if (!this.Factory.ApplyIntegrity)
				{
					sendSecurityHeader.SignatureParts = MessagePartSpecification.NoParts;
				}
				sendSecurityHeader.SetSigningToken(signingToken, tokenParameters);
			}
			if (this.Factory.ApplyConfidentiality || sendSecurityHeader.HasEncryptedTokens)
			{
				if (!this.Factory.ApplyConfidentiality)
				{
					sendSecurityHeader.EncryptionParts = MessagePartSpecification.NoParts;
				}
				sendSecurityHeader.SetEncryptionToken(encryptingToken, tokenParameters2);
			}
			message = sendSecurityHeader.SetupExecution();
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x000578FC File Offset: 0x00055AFC
		private void AttachRecipientSecurityProperty(Message message, SecurityToken initiatorToken, SecurityToken recipientToken, IList<SecurityToken> basicTokens, IList<SecurityToken> endorsingTokens, IList<SecurityToken> signedEndorsingTokens, IList<SecurityToken> signedTokens, Dictionary<SecurityToken, ReadOnlyCollection<IAuthorizationPolicy>> tokenPoliciesMapping)
		{
			SecurityMessageProperty orCreate = SecurityMessageProperty.GetOrCreate(message);
			orCreate.InitiatorToken = ((initiatorToken != null) ? new SecurityTokenSpecification(initiatorToken, tokenPoliciesMapping[initiatorToken]) : null);
			orCreate.RecipientToken = ((recipientToken != null) ? new SecurityTokenSpecification(recipientToken, EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance) : null);
			base.AddSupportingTokenSpecification(orCreate, basicTokens, endorsingTokens, signedEndorsingTokens, signedTokens, tokenPoliciesMapping);
			orCreate.ServiceSecurityContext = new ServiceSecurityContext(orCreate.GetInitiatorTokenAuthorizationPolicies());
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x00057964 File Offset: 0x00055B64
		private void DoIdentityCheckAndAttachInitiatorSecurityProperty(Message message, SecurityToken initiatorToken, SecurityToken recipientToken, ReadOnlyCollection<IAuthorizationPolicy> recipientTokenPolicies)
		{
			AuthorizationContext authorizationContext = base.EnsureIncomingIdentity(message, recipientToken, recipientTokenPolicies);
			SecurityMessageProperty orCreate = SecurityMessageProperty.GetOrCreate(message);
			orCreate.InitiatorToken = ((initiatorToken != null) ? new SecurityTokenSpecification(initiatorToken, EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance) : null);
			orCreate.RecipientToken = new SecurityTokenSpecification(recipientToken, recipientTokenPolicies);
			orCreate.ServiceSecurityContext = new ServiceSecurityContext(authorizationContext, recipientTokenPolicies ?? EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance);
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x000579C0 File Offset: 0x00055BC0
		protected override SecurityProtocolCorrelationState VerifyIncomingMessageCore(ref Message message, string actor, TimeSpan timeout, SecurityProtocolCorrelationState[] correlationStates)
		{
			AsymmetricSecurityProtocolFactory factory = this.Factory;
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			IList<SupportingTokenAuthenticatorSpecification> list;
			ReceiveSecurityHeader receiveSecurityHeader = base.ConfigureReceiveSecurityHeader(message, string.Empty, correlationStates, out list);
			SecurityToken requiredSigningToken = null;
			if (factory.ActAsInitiator)
			{
				SecurityToken securityToken = null;
				SecurityToken securityToken2 = null;
				if (factory.RequireIntegrity)
				{
					securityToken2 = SecurityProtocol.GetToken(this.initiatorAsymmetricTokenProvider, null, timeoutHelper.RemainingTime());
					requiredSigningToken = securityToken2;
				}
				if (factory.RequireConfidentiality)
				{
					securityToken = base.GetCorrelationToken(correlationStates);
					if (!SecurityUtils.HasSymmetricSecurityKey(securityToken))
					{
						receiveSecurityHeader.WrappedKeySecurityTokenAuthenticator = this.Factory.WrappedKeySecurityTokenAuthenticator;
					}
				}
				SecurityTokenAuthenticator primaryTokenAuthenticator;
				if (factory.AllowSerializedSigningTokenOnReply)
				{
					primaryTokenAuthenticator = this.initiatorAsymmetricTokenAuthenticator;
					requiredSigningToken = null;
				}
				else
				{
					primaryTokenAuthenticator = null;
				}
				receiveSecurityHeader.ConfigureAsymmetricBindingClientReceiveHeader(securityToken2, factory.AsymmetricTokenParameters, securityToken, factory.CryptoTokenParameters, primaryTokenAuthenticator);
			}
			else
			{
				SecurityToken wrappingToken;
				if (this.Factory.RecipientAsymmetricTokenProvider != null && this.Factory.RequireConfidentiality)
				{
					wrappingToken = SecurityProtocol.GetToken(factory.RecipientAsymmetricTokenProvider, null, timeoutHelper.RemainingTime());
				}
				else
				{
					wrappingToken = null;
				}
				receiveSecurityHeader.ConfigureAsymmetricBindingServerReceiveHeader(this.Factory.RecipientCryptoTokenAuthenticator, this.Factory.CryptoTokenParameters, wrappingToken, this.Factory.AsymmetricTokenParameters, list);
				receiveSecurityHeader.WrappedKeySecurityTokenAuthenticator = this.Factory.WrappedKeySecurityTokenAuthenticator;
				receiveSecurityHeader.ConfigureOutOfBandTokenResolver(base.MergeOutOfBandResolvers(list, this.Factory.RecipientOutOfBandTokenResolverList));
			}
			base.ProcessSecurityHeader(receiveSecurityHeader, ref message, requiredSigningToken, timeoutHelper.RemainingTime(), correlationStates);
			SecurityToken signatureToken = receiveSecurityHeader.SignatureToken;
			SecurityToken encryptionToken = receiveSecurityHeader.EncryptionToken;
			if (factory.RequireIntegrity)
			{
				if (factory.ActAsInitiator)
				{
					ReadOnlyCollection<IAuthorizationPolicy> recipientTokenPolicies = this.initiatorAsymmetricTokenAuthenticator.ValidateToken(signatureToken);
					MessageSecurityProtocol.EnsureNonWrappedToken(signatureToken, message);
					this.DoIdentityCheckAndAttachInitiatorSecurityProperty(message, encryptionToken, signatureToken, recipientTokenPolicies);
				}
				else
				{
					MessageSecurityProtocol.EnsureNonWrappedToken(signatureToken, message);
					this.AttachRecipientSecurityProperty(message, signatureToken, encryptionToken, receiveSecurityHeader.BasicSupportingTokens, receiveSecurityHeader.EndorsingSupportingTokens, receiveSecurityHeader.SignedEndorsingSupportingTokens, receiveSecurityHeader.SignedSupportingTokens, receiveSecurityHeader.SecurityTokenAuthorizationPoliciesMapping);
				}
			}
			return base.GetCorrelationState(signatureToken, receiveSecurityHeader);
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x00057BA4 File Offset: 0x00055DA4
		private bool TryGetTokenSynchronouslyForOutgoingSecurity(Message message, SecurityProtocolCorrelationState correlationState, bool isBlockingCall, TimeSpan timeout, out SecurityToken encryptingToken, out SecurityToken signingToken, out IList<SupportingTokenSpecification> supportingTokens, out SecurityProtocolCorrelationState newCorrelationState)
		{
			AsymmetricSecurityProtocolFactory factory = this.Factory;
			encryptingToken = null;
			signingToken = null;
			newCorrelationState = null;
			supportingTokens = null;
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (factory.ActAsInitiator)
			{
				if (!isBlockingCall || !base.TryGetSupportingTokens(this.Factory, base.Target, base.Via, message, timeoutHelper.RemainingTime(), isBlockingCall, out supportingTokens))
				{
					return false;
				}
				if (factory.ApplyConfidentiality)
				{
					encryptingToken = base.GetTokenAndEnsureOutgoingIdentity(this.initiatorAsymmetricTokenProvider, true, timeoutHelper.RemainingTime(), this.initiatorAsymmetricTokenAuthenticator);
				}
				if (factory.ApplyIntegrity)
				{
					signingToken = SecurityProtocol.GetToken(this.initiatorCryptoTokenProvider, base.Target, timeoutHelper.RemainingTime());
					newCorrelationState = base.GetCorrelationState(signingToken);
				}
			}
			else
			{
				if (factory.ApplyConfidentiality)
				{
					encryptingToken = base.GetCorrelationToken(correlationState);
				}
				if (factory.ApplyIntegrity)
				{
					signingToken = SecurityProtocol.GetToken(factory.RecipientAsymmetricTokenProvider, null, timeoutHelper.RemainingTime());
				}
			}
			return true;
		}

		// Token: 0x04001C0F RID: 7183
		private SecurityTokenAuthenticator initiatorAsymmetricTokenAuthenticator;

		// Token: 0x04001C10 RID: 7184
		private SecurityTokenProvider initiatorAsymmetricTokenProvider;

		// Token: 0x04001C11 RID: 7185
		private SecurityTokenProvider initiatorCryptoTokenProvider;

		// Token: 0x02000B4D RID: 2893
		private sealed class SecureOutgoingMessageAsyncResult : MessageSecurityProtocol.GetTwoTokensAndSetUpSecurityAsyncResult
		{
			// Token: 0x06007109 RID: 28937 RVA: 0x001A4FAC File Offset: 0x001A31AC
			public SecureOutgoingMessageAsyncResult(Message m, AsymmetricSecurityProtocol binding, SecurityTokenProvider primaryProvider, SecurityTokenProvider secondaryProvider, bool doIdentityChecks, SecurityTokenAuthenticator identityCheckAuthenticator, SecurityProtocolCorrelationState correlationState, TimeSpan timeout, AsyncCallback callback, object state) : base(m, binding, primaryProvider, secondaryProvider, doIdentityChecks, identityCheckAuthenticator, correlationState, timeout, callback, state)
			{
				base.Start();
			}

			// Token: 0x0600710A RID: 28938 RVA: 0x001A4FD8 File Offset: 0x001A31D8
			protected override void OnBothGetTokenCallsDone(ref Message message, SecurityToken primaryToken, SecurityToken secondaryToken, TimeSpan timeout)
			{
				AsymmetricSecurityProtocol asymmetricSecurityProtocol = (AsymmetricSecurityProtocol)base.Binding;
				if (secondaryToken != null)
				{
					base.SetCorrelationToken(secondaryToken);
				}
				asymmetricSecurityProtocol.SetUpDelayedSecurityExecution(ref message, primaryToken, secondaryToken, base.SupportingTokens, asymmetricSecurityProtocol.GetSignatureConfirmationCorrelationState(base.OldCorrelationState, base.NewCorrelationState));
			}
		}
	}
}
