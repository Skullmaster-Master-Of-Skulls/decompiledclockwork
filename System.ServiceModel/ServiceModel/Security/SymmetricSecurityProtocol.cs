using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x020002CF RID: 719
	internal sealed class SymmetricSecurityProtocol : MessageSecurityProtocol
	{
		// Token: 0x06001770 RID: 6000 RVA: 0x00059351 File Offset: 0x00057551
		public SymmetricSecurityProtocol(SymmetricSecurityProtocolFactory factory, EndpointAddress target, Uri via) : base(factory, target, via)
		{
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001771 RID: 6001 RVA: 0x0005935C File Offset: 0x0005755C
		private SymmetricSecurityProtocolFactory Factory
		{
			get
			{
				return (SymmetricSecurityProtocolFactory)base.MessageSecurityProtocolFactory;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x00059369 File Offset: 0x00057569
		public SecurityTokenProvider InitiatorSymmetricTokenProvider
		{
			get
			{
				base.CommunicationObject.ThrowIfNotOpened();
				return this.initiatorSymmetricTokenProvider;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x0005937C File Offset: 0x0005757C
		public SecurityTokenProvider InitiatorAsymmetricTokenProvider
		{
			get
			{
				base.CommunicationObject.ThrowIfNotOpened();
				return this.initiatorAsymmetricTokenProvider;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001774 RID: 6004 RVA: 0x0005938F File Offset: 0x0005758F
		public SecurityTokenAuthenticator InitiatorTokenAuthenticator
		{
			get
			{
				base.CommunicationObject.ThrowIfNotOpened();
				return this.initiatorTokenAuthenticator;
			}
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x000593A4 File Offset: 0x000575A4
		private InitiatorServiceModelSecurityTokenRequirement CreateInitiatorTokenRequirement()
		{
			InitiatorServiceModelSecurityTokenRequirement initiatorServiceModelSecurityTokenRequirement = base.CreateInitiatorSecurityTokenRequirement();
			this.Factory.SecurityTokenParameters.InitializeSecurityTokenRequirement(initiatorServiceModelSecurityTokenRequirement);
			initiatorServiceModelSecurityTokenRequirement.KeyUsage = (this.Factory.SecurityTokenParameters.HasAsymmetricKey ? SecurityKeyUsage.Exchange : SecurityKeyUsage.Signature);
			initiatorServiceModelSecurityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.MessageDirectionProperty] = MessageDirection.Output;
			if (this.Factory.SecurityTokenParameters.HasAsymmetricKey)
			{
				initiatorServiceModelSecurityTokenRequirement.IsOutOfBandToken = true;
			}
			return initiatorServiceModelSecurityTokenRequirement;
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x00059418 File Offset: 0x00057618
		public override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.OnOpen(timeoutHelper.RemainingTime());
			if (this.Factory.ActAsInitiator)
			{
				InitiatorServiceModelSecurityTokenRequirement tokenRequirement = this.CreateInitiatorTokenRequirement();
				SecurityTokenProvider tokenProvider = this.Factory.SecurityTokenManager.CreateSecurityTokenProvider(tokenRequirement);
				SecurityUtils.OpenTokenProviderIfRequired(tokenProvider, timeoutHelper.RemainingTime());
				if (this.Factory.SecurityTokenParameters.HasAsymmetricKey)
				{
					this.initiatorAsymmetricTokenProvider = tokenProvider;
				}
				else
				{
					this.initiatorSymmetricTokenProvider = tokenProvider;
				}
				InitiatorServiceModelSecurityTokenRequirement tokenRequirement2 = this.CreateInitiatorTokenRequirement();
				SecurityTokenResolver securityTokenResolver;
				this.initiatorTokenAuthenticator = this.Factory.SecurityTokenManager.CreateSecurityTokenAuthenticator(tokenRequirement2, out securityTokenResolver);
				SecurityUtils.OpenTokenAuthenticatorIfRequired(this.initiatorTokenAuthenticator, timeoutHelper.RemainingTime());
			}
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x000594C4 File Offset: 0x000576C4
		public override void OnAbort()
		{
			if (this.Factory.ActAsInitiator)
			{
				SecurityTokenProvider securityTokenProvider = this.initiatorSymmetricTokenProvider ?? this.initiatorAsymmetricTokenProvider;
				if (securityTokenProvider != null)
				{
					SecurityUtils.AbortTokenProviderIfRequired(securityTokenProvider);
				}
				if (this.initiatorTokenAuthenticator != null)
				{
					SecurityUtils.AbortTokenAuthenticatorIfRequired(this.initiatorTokenAuthenticator);
				}
			}
			base.OnAbort();
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x00059514 File Offset: 0x00057714
		public override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (this.Factory.ActAsInitiator)
			{
				SecurityTokenProvider securityTokenProvider = this.initiatorSymmetricTokenProvider ?? this.initiatorAsymmetricTokenProvider;
				if (securityTokenProvider != null)
				{
					SecurityUtils.CloseTokenProviderIfRequired(securityTokenProvider, timeoutHelper.RemainingTime());
				}
				if (this.initiatorTokenAuthenticator != null)
				{
					SecurityUtils.CloseTokenAuthenticatorIfRequired(this.initiatorTokenAuthenticator, timeoutHelper.RemainingTime());
				}
			}
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x0005957E File Offset: 0x0005777E
		private SecurityTokenProvider GetTokenProvider()
		{
			if (this.Factory.ActAsInitiator)
			{
				return this.initiatorSymmetricTokenProvider ?? this.initiatorAsymmetricTokenProvider;
			}
			return this.Factory.RecipientAsymmetricTokenProvider;
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x000595AC File Offset: 0x000577AC
		protected override IAsyncResult BeginSecureOutgoingMessageCore(Message message, TimeSpan timeout, SecurityProtocolCorrelationState correlationState, AsyncCallback callback, object state)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			SecurityToken primaryToken;
			SecurityTokenParameters primaryTokenParameters;
			SecurityToken prerequisiteToken;
			IList<SupportingTokenSpecification> supportingTokens;
			SecurityProtocolCorrelationState securityProtocolCorrelationState;
			if (this.TryGetTokenSynchronouslyForOutgoingSecurity(message, correlationState, false, timeoutHelper.RemainingTime(), out primaryToken, out primaryTokenParameters, out prerequisiteToken, out supportingTokens, out securityProtocolCorrelationState))
			{
				this.SetUpDelayedSecurityExecution(ref message, prerequisiteToken, primaryToken, primaryTokenParameters, supportingTokens, base.GetSignatureConfirmationCorrelationState(correlationState, securityProtocolCorrelationState));
				return new CompletedAsyncResult<Message, SecurityProtocolCorrelationState>(message, securityProtocolCorrelationState, callback, state);
			}
			if (!this.Factory.ActAsInitiator)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ProtocolMustBeInitiator", new object[]
				{
					base.GetType().ToString()
				})));
			}
			SecurityTokenProvider tokenProvider = this.GetTokenProvider();
			return new SymmetricSecurityProtocol.SecureOutgoingMessageAsyncResult(message, this, tokenProvider, this.Factory.ApplyConfidentiality, this.initiatorTokenAuthenticator, correlationState, timeoutHelper.RemainingTime(), callback, state);
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00059668 File Offset: 0x00057868
		protected override SecurityProtocolCorrelationState SecureOutgoingMessageCore(ref Message message, TimeSpan timeout, SecurityProtocolCorrelationState correlationState)
		{
			SecurityToken primaryToken;
			SecurityTokenParameters primaryTokenParameters;
			SecurityToken prerequisiteToken;
			IList<SupportingTokenSpecification> supportingTokens;
			SecurityProtocolCorrelationState securityProtocolCorrelationState;
			this.TryGetTokenSynchronouslyForOutgoingSecurity(message, correlationState, true, timeout, out primaryToken, out primaryTokenParameters, out prerequisiteToken, out supportingTokens, out securityProtocolCorrelationState);
			this.SetUpDelayedSecurityExecution(ref message, prerequisiteToken, primaryToken, primaryTokenParameters, supportingTokens, base.GetSignatureConfirmationCorrelationState(correlationState, securityProtocolCorrelationState));
			return securityProtocolCorrelationState;
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x000596A4 File Offset: 0x000578A4
		private void SetUpDelayedSecurityExecution(ref Message message, SecurityToken prerequisiteToken, SecurityToken primaryToken, SecurityTokenParameters primaryTokenParameters, IList<SupportingTokenSpecification> supportingTokens, SecurityProtocolCorrelationState correlationState)
		{
			string empty = string.Empty;
			SendSecurityHeader sendSecurityHeader = base.ConfigureSendSecurityHeader(message, empty, supportingTokens, correlationState);
			if (prerequisiteToken != null)
			{
				sendSecurityHeader.AddPrerequisiteToken(prerequisiteToken);
			}
			if (this.Factory.ApplyIntegrity || sendSecurityHeader.HasSignedTokens)
			{
				if (!this.Factory.ApplyIntegrity)
				{
					sendSecurityHeader.SignatureParts = MessagePartSpecification.NoParts;
				}
				sendSecurityHeader.SetSigningToken(primaryToken, primaryTokenParameters);
			}
			if (this.Factory.ApplyConfidentiality || sendSecurityHeader.HasEncryptedTokens)
			{
				if (!this.Factory.ApplyConfidentiality)
				{
					sendSecurityHeader.EncryptionParts = MessagePartSpecification.NoParts;
				}
				sendSecurityHeader.SetEncryptionToken(primaryToken, primaryTokenParameters);
			}
			message = sendSecurityHeader.SetupExecution();
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x00059743 File Offset: 0x00057943
		protected override void EndSecureOutgoingMessageCore(IAsyncResult result, out Message message, out SecurityProtocolCorrelationState newCorrelationState)
		{
			if (result is CompletedAsyncResult<Message, SecurityProtocolCorrelationState>)
			{
				message = CompletedAsyncResult<Message, SecurityProtocolCorrelationState>.End(result, out newCorrelationState);
				return;
			}
			message = MessageSecurityProtocol.GetOneTokenAndSetUpSecurityAsyncResult.End(result, out newCorrelationState);
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x00059760 File Offset: 0x00057960
		private WrappedKeySecurityToken CreateWrappedKeyToken(SecurityToken wrappingToken, SecurityTokenParameters wrappingTokenParameters, SecurityTokenReferenceStyle wrappingTokenReferenceStyle)
		{
			int num = Math.Max(128, this.Factory.OutgoingAlgorithmSuite.DefaultSymmetricKeyLength);
			CryptoHelper.ValidateSymmetricKeyLength(num, this.Factory.OutgoingAlgorithmSuite);
			byte[] array = new byte[num / 8];
			CryptoHelper.FillRandomBytes(array);
			string id = SecurityUtils.GenerateId();
			string defaultAsymmetricKeyWrapAlgorithm = this.Factory.OutgoingAlgorithmSuite.DefaultAsymmetricKeyWrapAlgorithm;
			SecurityKeyIdentifierClause clause = wrappingTokenParameters.CreateKeyIdentifierClause(wrappingToken, wrappingTokenReferenceStyle);
			return new WrappedKeySecurityToken(id, array, defaultAsymmetricKeyWrapAlgorithm, wrappingToken, new SecurityKeyIdentifier
			{
				clause
			});
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x000597E4 File Offset: 0x000579E4
		private SecurityToken GetInitiatorToken(SecurityToken providerToken, Message message, TimeSpan timeout, out SecurityTokenParameters tokenParameters, out SecurityToken prerequisiteWrappingToken)
		{
			tokenParameters = null;
			prerequisiteWrappingToken = null;
			SecurityToken result;
			if (this.Factory.SecurityTokenParameters.HasAsymmetricKey)
			{
				bool flag = SendSecurityHeader.ShouldSerializeToken(this.Factory.SecurityTokenParameters, MessageDirection.Input);
				if (flag)
				{
					prerequisiteWrappingToken = providerToken;
				}
				result = this.CreateWrappedKeyToken(providerToken, this.Factory.SecurityTokenParameters, flag ? SecurityTokenReferenceStyle.Internal : SecurityTokenReferenceStyle.External);
			}
			else
			{
				result = providerToken;
			}
			tokenParameters = this.Factory.GetProtectionTokenParameters();
			return result;
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x00059854 File Offset: 0x00057A54
		private bool TryGetTokenSynchronouslyForOutgoingSecurity(Message message, SecurityProtocolCorrelationState correlationState, bool isBlockingCall, TimeSpan timeout, out SecurityToken token, out SecurityTokenParameters tokenParameters, out SecurityToken prerequisiteWrappingToken, out IList<SupportingTokenSpecification> supportingTokens, out SecurityProtocolCorrelationState newCorrelationState)
		{
			SymmetricSecurityProtocolFactory factory = this.Factory;
			supportingTokens = null;
			prerequisiteWrappingToken = null;
			token = null;
			tokenParameters = null;
			newCorrelationState = null;
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (factory.ApplyIntegrity || factory.ApplyConfidentiality)
			{
				if (factory.ActAsInitiator)
				{
					if (!isBlockingCall || !base.TryGetSupportingTokens(factory, base.Target, base.Via, message, timeoutHelper.RemainingTime(), isBlockingCall, out supportingTokens))
					{
						return false;
					}
					SecurityTokenProvider tokenProvider = this.GetTokenProvider();
					SecurityToken tokenAndEnsureOutgoingIdentity = base.GetTokenAndEnsureOutgoingIdentity(tokenProvider, factory.ApplyConfidentiality, timeoutHelper.RemainingTime(), this.initiatorTokenAuthenticator);
					token = this.GetInitiatorToken(tokenAndEnsureOutgoingIdentity, message, timeoutHelper.RemainingTime(), out tokenParameters, out prerequisiteWrappingToken);
					newCorrelationState = base.GetCorrelationState(token);
				}
				else
				{
					token = base.GetCorrelationToken(correlationState);
					tokenParameters = this.Factory.GetProtectionTokenParameters();
				}
			}
			return true;
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x00059924 File Offset: 0x00057B24
		private SecurityToken GetCorrelationToken(SecurityProtocolCorrelationState[] correlationStates, out SecurityTokenParameters correlationTokenParameters)
		{
			SecurityToken correlationToken = base.GetCorrelationToken(correlationStates);
			correlationTokenParameters = this.Factory.GetProtectionTokenParameters();
			return correlationToken;
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x00059947 File Offset: 0x00057B47
		private void EnsureWrappedToken(SecurityToken token, Message message)
		{
			if (!(token is WrappedKeySecurityToken))
			{
				throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("IncomingSigningTokenMustBeAnEncryptedKey")), message);
			}
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x00059968 File Offset: 0x00057B68
		protected override SecurityProtocolCorrelationState VerifyIncomingMessageCore(ref Message message, string actor, TimeSpan timeout, SecurityProtocolCorrelationState[] correlationStates)
		{
			SymmetricSecurityProtocolFactory factory = this.Factory;
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			IList<SupportingTokenAuthenticatorSpecification> list;
			ReceiveSecurityHeader receiveSecurityHeader = base.ConfigureReceiveSecurityHeader(message, string.Empty, correlationStates, out list);
			SecurityToken requiredSigningToken = null;
			if (this.Factory.ActAsInitiator)
			{
				SecurityTokenParameters primaryTokenParameters;
				SecurityToken correlationToken = this.GetCorrelationToken(correlationStates, out primaryTokenParameters);
				receiveSecurityHeader.ConfigureSymmetricBindingClientReceiveHeader(correlationToken, primaryTokenParameters);
				requiredSigningToken = correlationToken;
			}
			else
			{
				if (factory.RecipientSymmetricTokenAuthenticator != null)
				{
					receiveSecurityHeader.ConfigureSymmetricBindingServerReceiveHeader(this.Factory.RecipientSymmetricTokenAuthenticator, this.Factory.SecurityTokenParameters, list);
				}
				else
				{
					receiveSecurityHeader.ConfigureSymmetricBindingServerReceiveHeader(this.Factory.RecipientAsymmetricTokenProvider.GetToken(timeoutHelper.RemainingTime()), this.Factory.SecurityTokenParameters, list);
					receiveSecurityHeader.WrappedKeySecurityTokenAuthenticator = this.Factory.WrappedKeySecurityTokenAuthenticator;
				}
				receiveSecurityHeader.ConfigureOutOfBandTokenResolver(base.MergeOutOfBandResolvers(list, this.Factory.RecipientOutOfBandTokenResolverList));
			}
			base.ProcessSecurityHeader(receiveSecurityHeader, ref message, requiredSigningToken, timeoutHelper.RemainingTime(), correlationStates);
			SecurityToken signatureToken = receiveSecurityHeader.SignatureToken;
			if (factory.RequireIntegrity)
			{
				if (factory.SecurityTokenParameters.HasAsymmetricKey)
				{
					this.EnsureWrappedToken(signatureToken, message);
				}
				else
				{
					MessageSecurityProtocol.EnsureNonWrappedToken(signatureToken, message);
				}
				if (factory.ActAsInitiator)
				{
					if (!factory.SecurityTokenParameters.HasAsymmetricKey)
					{
						ReadOnlyCollection<IAuthorizationPolicy> protectionTokenPolicies = this.initiatorTokenAuthenticator.ValidateToken(signatureToken);
						base.DoIdentityCheckAndAttachInitiatorSecurityProperty(message, signatureToken, protectionTokenPolicies);
					}
					else
					{
						SecurityToken wrappingToken = (signatureToken as WrappedKeySecurityToken).WrappingToken;
						ReadOnlyCollection<IAuthorizationPolicy> protectionTokenPolicies2 = this.initiatorTokenAuthenticator.ValidateToken(wrappingToken);
						base.DoIdentityCheckAndAttachInitiatorSecurityProperty(message, signatureToken, protectionTokenPolicies2);
					}
				}
				else
				{
					base.AttachRecipientSecurityProperty(message, signatureToken, this.Factory.SecurityTokenParameters.HasAsymmetricKey, receiveSecurityHeader.BasicSupportingTokens, receiveSecurityHeader.EndorsingSupportingTokens, receiveSecurityHeader.SignedEndorsingSupportingTokens, receiveSecurityHeader.SignedSupportingTokens, receiveSecurityHeader.SecurityTokenAuthorizationPoliciesMapping);
				}
			}
			return base.GetCorrelationState(signatureToken, receiveSecurityHeader);
		}

		// Token: 0x04001C25 RID: 7205
		private SecurityTokenProvider initiatorSymmetricTokenProvider;

		// Token: 0x04001C26 RID: 7206
		private SecurityTokenProvider initiatorAsymmetricTokenProvider;

		// Token: 0x04001C27 RID: 7207
		private SecurityTokenAuthenticator initiatorTokenAuthenticator;

		// Token: 0x02000B50 RID: 2896
		private sealed class SecureOutgoingMessageAsyncResult : MessageSecurityProtocol.GetOneTokenAndSetUpSecurityAsyncResult
		{
			// Token: 0x06007125 RID: 28965 RVA: 0x001A55B8 File Offset: 0x001A37B8
			public SecureOutgoingMessageAsyncResult(Message m, SymmetricSecurityProtocol binding, SecurityTokenProvider provider, bool doIdentityChecks, SecurityTokenAuthenticator identityCheckAuthenticator, SecurityProtocolCorrelationState correlationState, TimeSpan timeout, AsyncCallback callback, object state) : base(m, binding, provider, doIdentityChecks, identityCheckAuthenticator, correlationState, timeout, callback, state)
			{
				this.symmetricBinding = binding;
				base.Start();
			}

			// Token: 0x06007126 RID: 28966 RVA: 0x001A55E8 File Offset: 0x001A37E8
			protected override void OnGetTokenDone(ref Message message, SecurityToken providerToken, TimeSpan timeout)
			{
				SecurityTokenParameters primaryTokenParameters;
				SecurityToken prerequisiteToken;
				SecurityToken initiatorToken = this.symmetricBinding.GetInitiatorToken(providerToken, message, timeout, out primaryTokenParameters, out prerequisiteToken);
				base.SetCorrelationToken(initiatorToken);
				this.symmetricBinding.SetUpDelayedSecurityExecution(ref message, prerequisiteToken, initiatorToken, primaryTokenParameters, base.SupportingTokens, base.Binding.GetSignatureConfirmationCorrelationState(base.OldCorrelationState, base.NewCorrelationState));
			}

			// Token: 0x0400405B RID: 16475
			private SymmetricSecurityProtocol symmetricBinding;
		}
	}
}
