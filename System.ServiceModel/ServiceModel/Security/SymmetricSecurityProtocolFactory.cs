using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x020002D0 RID: 720
	internal class SymmetricSecurityProtocolFactory : MessageSecurityProtocolFactory
	{
		// Token: 0x06001784 RID: 6020 RVA: 0x00059B21 File Offset: 0x00057D21
		public SymmetricSecurityProtocolFactory()
		{
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x00059B29 File Offset: 0x00057D29
		internal SymmetricSecurityProtocolFactory(MessageSecurityProtocolFactory factory) : base(factory)
		{
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001786 RID: 6022 RVA: 0x00059B32 File Offset: 0x00057D32
		// (set) Token: 0x06001787 RID: 6023 RVA: 0x00059B3A File Offset: 0x00057D3A
		public SecurityTokenParameters SecurityTokenParameters
		{
			get
			{
				return this.tokenParameters;
			}
			set
			{
				base.ThrowIfImmutable();
				this.tokenParameters = value;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x00059B49 File Offset: 0x00057D49
		public SecurityTokenProvider RecipientAsymmetricTokenProvider
		{
			get
			{
				return this.recipientAsymmetricTokenProvider;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001789 RID: 6025 RVA: 0x00059B51 File Offset: 0x00057D51
		public SecurityTokenAuthenticator RecipientSymmetricTokenAuthenticator
		{
			get
			{
				return this.recipientSymmetricTokenAuthenticator;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x0600178A RID: 6026 RVA: 0x00059B59 File Offset: 0x00057D59
		public ReadOnlyCollection<SecurityTokenResolver> RecipientOutOfBandTokenResolverList
		{
			get
			{
				return this.recipientOutOfBandTokenResolverList;
			}
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x00059B64 File Offset: 0x00057D64
		public override EndpointIdentity GetIdentityOfSelf()
		{
			EndpointIdentity identityOfSelf;
			if (base.SecurityTokenManager is IEndpointIdentityProvider)
			{
				SecurityTokenRequirement securityTokenRequirement = base.CreateRecipientSecurityTokenRequirement();
				this.SecurityTokenParameters.InitializeSecurityTokenRequirement(securityTokenRequirement);
				identityOfSelf = ((IEndpointIdentityProvider)base.SecurityTokenManager).GetIdentityOfSelf(securityTokenRequirement);
			}
			else
			{
				identityOfSelf = base.GetIdentityOfSelf();
			}
			return identityOfSelf;
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x00059BB0 File Offset: 0x00057DB0
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(Collection<ISecurityContextSecurityTokenCache>))
			{
				Collection<ISecurityContextSecurityTokenCache> property = base.GetProperty<Collection<ISecurityContextSecurityTokenCache>>();
				if (this.recipientSymmetricTokenAuthenticator is ISecurityContextSecurityTokenCacheProvider)
				{
					property.Add(((ISecurityContextSecurityTokenCacheProvider)this.recipientSymmetricTokenAuthenticator).TokenCache);
				}
				return (T)((object)property);
			}
			return base.GetProperty<T>();
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x00059C10 File Offset: 0x00057E10
		public override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (!base.ActAsInitiator)
			{
				if (this.recipientSymmetricTokenAuthenticator != null)
				{
					SecurityUtils.CloseTokenAuthenticatorIfRequired(this.recipientSymmetricTokenAuthenticator, timeoutHelper.RemainingTime());
				}
				if (this.recipientAsymmetricTokenProvider != null)
				{
					SecurityUtils.CloseTokenProviderIfRequired(this.recipientAsymmetricTokenProvider, timeoutHelper.RemainingTime());
				}
			}
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x00059C6E File Offset: 0x00057E6E
		public override void OnAbort()
		{
			if (!base.ActAsInitiator)
			{
				if (this.recipientSymmetricTokenAuthenticator != null)
				{
					SecurityUtils.AbortTokenAuthenticatorIfRequired(this.recipientSymmetricTokenAuthenticator);
				}
				if (this.recipientAsymmetricTokenProvider != null)
				{
					SecurityUtils.AbortTokenProviderIfRequired(this.recipientAsymmetricTokenProvider);
				}
			}
			base.OnAbort();
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x00059CA4 File Offset: 0x00057EA4
		protected override SecurityProtocol OnCreateSecurityProtocol(EndpointAddress target, Uri via, object listenerSecurityState, TimeSpan timeout)
		{
			return new SymmetricSecurityProtocol(this, target, via);
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x00059CB0 File Offset: 0x00057EB0
		private RecipientServiceModelSecurityTokenRequirement CreateRecipientTokenRequirement()
		{
			RecipientServiceModelSecurityTokenRequirement recipientServiceModelSecurityTokenRequirement = base.CreateRecipientSecurityTokenRequirement();
			this.SecurityTokenParameters.InitializeSecurityTokenRequirement(recipientServiceModelSecurityTokenRequirement);
			recipientServiceModelSecurityTokenRequirement.KeyUsage = (this.SecurityTokenParameters.HasAsymmetricKey ? SecurityKeyUsage.Exchange : SecurityKeyUsage.Signature);
			return recipientServiceModelSecurityTokenRequirement;
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x00059CE8 File Offset: 0x00057EE8
		public override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.OnOpen(timeoutHelper.RemainingTime());
			if (this.tokenParameters == null)
			{
				base.OnPropertySettingsError("SecurityTokenParameters", true);
			}
			if (!base.ActAsInitiator)
			{
				SecurityTokenRequirement tokenRequirement = this.CreateRecipientTokenRequirement();
				SecurityTokenResolver securityTokenResolver = null;
				if (this.SecurityTokenParameters.HasAsymmetricKey)
				{
					this.recipientAsymmetricTokenProvider = base.SecurityTokenManager.CreateSecurityTokenProvider(tokenRequirement);
				}
				else
				{
					this.recipientSymmetricTokenAuthenticator = base.SecurityTokenManager.CreateSecurityTokenAuthenticator(tokenRequirement, out securityTokenResolver);
				}
				if (this.RecipientSymmetricTokenAuthenticator != null && this.RecipientAsymmetricTokenProvider != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("OnlyOneOfEncryptedKeyOrSymmetricBindingCanBeSelected")));
				}
				if (securityTokenResolver != null)
				{
					this.recipientOutOfBandTokenResolverList = new ReadOnlyCollection<SecurityTokenResolver>(new Collection<SecurityTokenResolver>
					{
						securityTokenResolver
					});
				}
				else
				{
					this.recipientOutOfBandTokenResolverList = EmptyReadOnlyCollection<SecurityTokenResolver>.Instance;
				}
				if (this.RecipientAsymmetricTokenProvider != null)
				{
					base.Open("RecipientAsymmetricTokenProvider", true, this.RecipientAsymmetricTokenProvider, timeoutHelper.RemainingTime());
				}
				else
				{
					base.Open("RecipientSymmetricTokenAuthenticator", true, this.RecipientSymmetricTokenAuthenticator, timeoutHelper.RemainingTime());
				}
			}
			if (this.tokenParameters.RequireDerivedKeys)
			{
				base.ExpectKeyDerivation = true;
			}
			if (this.tokenParameters.HasAsymmetricKey)
			{
				this.protectionTokenParameters = new WrappedKeySecurityTokenParameters();
				this.protectionTokenParameters.RequireDerivedKeys = this.SecurityTokenParameters.RequireDerivedKeys;
				return;
			}
			this.protectionTokenParameters = this.tokenParameters;
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x00059E45 File Offset: 0x00058045
		internal SecurityTokenParameters GetProtectionTokenParameters()
		{
			return this.protectionTokenParameters;
		}

		// Token: 0x04001C28 RID: 7208
		private SecurityTokenAuthenticator recipientSymmetricTokenAuthenticator;

		// Token: 0x04001C29 RID: 7209
		private SecurityTokenProvider recipientAsymmetricTokenProvider;

		// Token: 0x04001C2A RID: 7210
		private ReadOnlyCollection<SecurityTokenResolver> recipientOutOfBandTokenResolverList;

		// Token: 0x04001C2B RID: 7211
		private SecurityTokenParameters tokenParameters;

		// Token: 0x04001C2C RID: 7212
		private SecurityTokenParameters protectionTokenParameters;
	}
}
