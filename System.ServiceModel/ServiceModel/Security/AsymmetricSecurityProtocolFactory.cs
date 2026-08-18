using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.ServiceModel.Description;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x020002CC RID: 716
	internal class AsymmetricSecurityProtocolFactory : MessageSecurityProtocolFactory
	{
		// Token: 0x0600171E RID: 5918 RVA: 0x00057C8D File Offset: 0x00055E8D
		public AsymmetricSecurityProtocolFactory()
		{
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x00057C95 File Offset: 0x00055E95
		internal AsymmetricSecurityProtocolFactory(AsymmetricSecurityProtocolFactory factory) : base(factory)
		{
			this.allowSerializedSigningTokenOnReply = factory.allowSerializedSigningTokenOnReply;
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x00057CAA File Offset: 0x00055EAA
		// (set) Token: 0x06001721 RID: 5921 RVA: 0x00057CB2 File Offset: 0x00055EB2
		public bool AllowSerializedSigningTokenOnReply
		{
			get
			{
				return this.allowSerializedSigningTokenOnReply;
			}
			set
			{
				base.ThrowIfImmutable();
				this.allowSerializedSigningTokenOnReply = value;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001722 RID: 5922 RVA: 0x00057CC1 File Offset: 0x00055EC1
		// (set) Token: 0x06001723 RID: 5923 RVA: 0x00057CC9 File Offset: 0x00055EC9
		public SecurityTokenParameters AsymmetricTokenParameters
		{
			get
			{
				return this.asymmetricTokenParameters;
			}
			set
			{
				base.ThrowIfImmutable();
				this.asymmetricTokenParameters = value;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x00057CD8 File Offset: 0x00055ED8
		public SecurityTokenProvider RecipientAsymmetricTokenProvider
		{
			get
			{
				base.CommunicationObject.ThrowIfNotOpened();
				return this.recipientAsymmetricTokenProvider;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x00057CEB File Offset: 0x00055EEB
		public SecurityTokenAuthenticator RecipientCryptoTokenAuthenticator
		{
			get
			{
				base.CommunicationObject.ThrowIfNotOpened();
				return this.recipientCryptoTokenAuthenticator;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x00057CFE File Offset: 0x00055EFE
		public ReadOnlyCollection<SecurityTokenResolver> RecipientOutOfBandTokenResolverList
		{
			get
			{
				base.CommunicationObject.ThrowIfNotOpened();
				return this.recipientOutOfBandTokenResolverList;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x00057D11 File Offset: 0x00055F11
		// (set) Token: 0x06001728 RID: 5928 RVA: 0x00057D19 File Offset: 0x00055F19
		public SecurityTokenParameters CryptoTokenParameters
		{
			get
			{
				return this.cryptoTokenParameters;
			}
			set
			{
				base.ThrowIfImmutable();
				this.cryptoTokenParameters = value;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x00057D28 File Offset: 0x00055F28
		private bool RequiresAsymmetricTokenProviderForForwardDirection
		{
			get
			{
				return (base.ActAsInitiator && base.ApplyConfidentiality) || (!base.ActAsInitiator && base.RequireConfidentiality);
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x0600172A RID: 5930 RVA: 0x00057D4C File Offset: 0x00055F4C
		private bool RequiresAsymmetricTokenProviderForReturnDirection
		{
			get
			{
				return (base.ActAsInitiator && base.RequireIntegrity) || (!base.ActAsInitiator && base.ApplyIntegrity);
			}
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x00057D70 File Offset: 0x00055F70
		public override EndpointIdentity GetIdentityOfSelf()
		{
			if (base.SecurityTokenManager is IEndpointIdentityProvider && this.AsymmetricTokenParameters != null)
			{
				SecurityTokenRequirement securityTokenRequirement = base.CreateRecipientSecurityTokenRequirement();
				this.AsymmetricTokenParameters.InitializeSecurityTokenRequirement(securityTokenRequirement);
				return ((IEndpointIdentityProvider)base.SecurityTokenManager).GetIdentityOfSelf(securityTokenRequirement);
			}
			return base.GetIdentityOfSelf();
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x00057DC0 File Offset: 0x00055FC0
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(Collection<ISecurityContextSecurityTokenCache>))
			{
				Collection<ISecurityContextSecurityTokenCache> property = base.GetProperty<Collection<ISecurityContextSecurityTokenCache>>();
				if (this.recipientCryptoTokenAuthenticator is ISecurityContextSecurityTokenCacheProvider)
				{
					property.Add(((ISecurityContextSecurityTokenCacheProvider)this.recipientCryptoTokenAuthenticator).TokenCache);
				}
				return (T)((object)property);
			}
			return base.GetProperty<T>();
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x00057E20 File Offset: 0x00056020
		public override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (!base.ActAsInitiator)
			{
				if (this.recipientAsymmetricTokenProvider != null)
				{
					SecurityUtils.CloseTokenProviderIfRequired(this.recipientAsymmetricTokenProvider, timeoutHelper.RemainingTime());
				}
				if (this.recipientCryptoTokenAuthenticator != null)
				{
					SecurityUtils.CloseTokenAuthenticatorIfRequired(this.recipientCryptoTokenAuthenticator, timeoutHelper.RemainingTime());
				}
			}
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x00057E7E File Offset: 0x0005607E
		public override void OnAbort()
		{
			if (!base.ActAsInitiator)
			{
				if (this.recipientAsymmetricTokenProvider != null)
				{
					SecurityUtils.AbortTokenProviderIfRequired(this.recipientAsymmetricTokenProvider);
				}
				if (this.recipientCryptoTokenAuthenticator != null)
				{
					SecurityUtils.AbortTokenAuthenticatorIfRequired(this.recipientCryptoTokenAuthenticator);
				}
			}
			base.OnAbort();
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x00057EB4 File Offset: 0x000560B4
		protected override SecurityProtocol OnCreateSecurityProtocol(EndpointAddress target, Uri via, object listenerSecurityState, TimeSpan timeout)
		{
			return new AsymmetricSecurityProtocol(this, target, via);
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x00057EC0 File Offset: 0x000560C0
		public override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.OnOpen(timeoutHelper.RemainingTime());
			if (base.ActAsInitiator)
			{
				if (base.ApplyIntegrity)
				{
					if (this.CryptoTokenParameters == null)
					{
						base.OnPropertySettingsError("CryptoTokenParameters", true);
					}
					if (this.CryptoTokenParameters.RequireDerivedKeys)
					{
						base.ExpectKeyDerivation = true;
					}
				}
			}
			else
			{
				if (this.CryptoTokenParameters == null)
				{
					base.OnPropertySettingsError("CryptoTokenParameters", true);
				}
				if (this.CryptoTokenParameters.RequireDerivedKeys)
				{
					base.ExpectKeyDerivation = true;
				}
				SecurityTokenResolver securityTokenResolver = null;
				if (base.RequireIntegrity)
				{
					RecipientServiceModelSecurityTokenRequirement recipientServiceModelSecurityTokenRequirement = base.CreateRecipientSecurityTokenRequirement();
					this.CryptoTokenParameters.InitializeSecurityTokenRequirement(recipientServiceModelSecurityTokenRequirement);
					recipientServiceModelSecurityTokenRequirement.KeyUsage = SecurityKeyUsage.Signature;
					recipientServiceModelSecurityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.MessageDirectionProperty] = MessageDirection.Input;
					this.recipientCryptoTokenAuthenticator = base.SecurityTokenManager.CreateSecurityTokenAuthenticator(recipientServiceModelSecurityTokenRequirement, out securityTokenResolver);
					base.Open("RecipientCryptoTokenAuthenticator", true, this.recipientCryptoTokenAuthenticator, timeoutHelper.RemainingTime());
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
			}
			if (this.RequiresAsymmetricTokenProviderForForwardDirection || this.RequiresAsymmetricTokenProviderForReturnDirection)
			{
				if (this.AsymmetricTokenParameters == null)
				{
					base.OnPropertySettingsError("AsymmetricTokenParameters", this.RequiresAsymmetricTokenProviderForForwardDirection);
				}
				else if (this.AsymmetricTokenParameters.RequireDerivedKeys)
				{
					base.ExpectKeyDerivation = true;
				}
				if (!base.ActAsInitiator)
				{
					RecipientServiceModelSecurityTokenRequirement recipientServiceModelSecurityTokenRequirement2 = base.CreateRecipientSecurityTokenRequirement();
					this.AsymmetricTokenParameters.InitializeSecurityTokenRequirement(recipientServiceModelSecurityTokenRequirement2);
					recipientServiceModelSecurityTokenRequirement2.KeyUsage = (this.RequiresAsymmetricTokenProviderForForwardDirection ? SecurityKeyUsage.Exchange : SecurityKeyUsage.Signature);
					recipientServiceModelSecurityTokenRequirement2.Properties[ServiceModelSecurityTokenRequirement.MessageDirectionProperty] = (this.RequiresAsymmetricTokenProviderForForwardDirection ? MessageDirection.Input : MessageDirection.Output);
					this.recipientAsymmetricTokenProvider = base.SecurityTokenManager.CreateSecurityTokenProvider(recipientServiceModelSecurityTokenRequirement2);
					base.Open("RecipientAsymmetricTokenProvider", this.RequiresAsymmetricTokenProviderForForwardDirection, this.recipientAsymmetricTokenProvider, timeoutHelper.RemainingTime());
				}
			}
			if (base.ActAsInitiator && this.AllowSerializedSigningTokenOnReply && base.IdentityVerifier == null)
			{
				base.OnPropertySettingsError("IdentityVerifier", false);
			}
		}

		// Token: 0x04001C12 RID: 7186
		private SecurityTokenParameters cryptoTokenParameters;

		// Token: 0x04001C13 RID: 7187
		private SecurityTokenParameters asymmetricTokenParameters;

		// Token: 0x04001C14 RID: 7188
		private SecurityTokenProvider recipientAsymmetricTokenProvider;

		// Token: 0x04001C15 RID: 7189
		private ReadOnlyCollection<SecurityTokenResolver> recipientOutOfBandTokenResolverList;

		// Token: 0x04001C16 RID: 7190
		private SecurityTokenAuthenticator recipientCryptoTokenAuthenticator;

		// Token: 0x04001C17 RID: 7191
		private bool allowSerializedSigningTokenOnReply;
	}
}
