using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000365 RID: 869
	internal class WrappedTokenCache : SecurityTokenResolver, ISecurityContextSecurityTokenCache
	{
		// Token: 0x06001FE0 RID: 8160 RVA: 0x00077391 File Offset: 0x00075591
		public WrappedTokenCache(SessionSecurityTokenCache tokenCache, SctClaimsHandler sctClaimsHandler)
		{
			if (tokenCache == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenCache");
			}
			if (sctClaimsHandler == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("sctClaimsHandler");
			}
			this._tokenCache = tokenCache;
			this._claimsHandler = sctClaimsHandler;
		}

		// Token: 0x06001FE1 RID: 8161 RVA: 0x000773D0 File Offset: 0x000755D0
		public void AddContext(SecurityContextSecurityToken token)
		{
			this._claimsHandler.SetPrincipalBootstrapTokensAndBindIdfxAuthPolicy(token);
			SessionSecurityTokenCacheKey key = new SessionSecurityTokenCacheKey(this._claimsHandler.EndpointId, token.ContextId, token.KeyGeneration);
			SessionSecurityToken sessionSecurityToken = SecurityContextSecurityTokenHelper.ConvertSctToSessionToken(token, SecureConversationVersion.Default);
			DateTime expiryTime = DateTimeUtil.Add(sessionSecurityToken.ValidTo, this._claimsHandler.SecurityTokenHandlerCollection.Configuration.MaxClockSkew);
			this._tokenCache.AddOrUpdate(key, sessionSecurityToken, expiryTime);
		}

		// Token: 0x06001FE2 RID: 8162 RVA: 0x00077441 File Offset: 0x00075641
		public void ClearContexts()
		{
			this._tokenCache.RemoveAll(this._claimsHandler.EndpointId);
		}

		// Token: 0x06001FE3 RID: 8163 RVA: 0x0007745C File Offset: 0x0007565C
		public Collection<SecurityContextSecurityToken> GetAllContexts(UniqueId contextId)
		{
			Collection<SecurityContextSecurityToken> collection = new Collection<SecurityContextSecurityToken>();
			IEnumerable<SessionSecurityToken> all = this._tokenCache.GetAll(this._claimsHandler.EndpointId, contextId);
			if (all != null)
			{
				foreach (SessionSecurityToken sessionSecurityToken in all)
				{
					if (sessionSecurityToken != null && sessionSecurityToken.IsSecurityContextSecurityTokenWrapper)
					{
						SecurityContextSecurityToken item = SecurityContextSecurityTokenHelper.ConvertSessionTokenToSecurityContextSecurityToken(sessionSecurityToken);
						collection.Add(item);
					}
				}
			}
			return collection;
		}

		// Token: 0x06001FE4 RID: 8164 RVA: 0x000774DC File Offset: 0x000756DC
		public SecurityContextSecurityToken GetContext(UniqueId contextId, UniqueId generation)
		{
			SessionSecurityTokenCacheKey key = new SessionSecurityTokenCacheKey(this._claimsHandler.EndpointId, contextId, generation);
			SessionSecurityToken sessionSecurityToken = this._tokenCache.Get(key);
			SecurityContextSecurityToken result = null;
			if (sessionSecurityToken != null && sessionSecurityToken.IsSecurityContextSecurityTokenWrapper)
			{
				result = SecurityContextSecurityTokenHelper.ConvertSessionTokenToSecurityContextSecurityToken(sessionSecurityToken);
			}
			return result;
		}

		// Token: 0x06001FE5 RID: 8165 RVA: 0x00077520 File Offset: 0x00075720
		public void RemoveAllContexts(UniqueId contextId)
		{
			this._tokenCache.RemoveAll(this._claimsHandler.EndpointId, contextId);
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x0007753C File Offset: 0x0007573C
		public void RemoveContext(UniqueId contextId, UniqueId generation)
		{
			SessionSecurityTokenCacheKey key = new SessionSecurityTokenCacheKey(this._claimsHandler.EndpointId, contextId, generation);
			this._tokenCache.Remove(key);
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x00077568 File Offset: 0x00075768
		public bool TryAddContext(SecurityContextSecurityToken token)
		{
			this._claimsHandler.SetPrincipalBootstrapTokensAndBindIdfxAuthPolicy(token);
			SessionSecurityTokenCacheKey key = new SessionSecurityTokenCacheKey(this._claimsHandler.EndpointId, token.ContextId, token.KeyGeneration);
			SessionSecurityToken value = SecurityContextSecurityTokenHelper.ConvertSctToSessionToken(token, SecureConversationVersion.Default);
			DateTime expiryTime = DateTimeUtil.Add(token.ValidTo, this._claimsHandler.SecurityTokenHandlerCollection.Configuration.MaxClockSkew);
			this._tokenCache.AddOrUpdate(key, value, expiryTime);
			return true;
		}

		// Token: 0x06001FE8 RID: 8168 RVA: 0x000775DC File Offset: 0x000757DC
		public void UpdateContextCachingTime(SecurityContextSecurityToken token, DateTime expirationTime)
		{
			if (token.ValidTo <= expirationTime.ToUniversalTime())
			{
				return;
			}
			SessionSecurityTokenCacheKey key = new SessionSecurityTokenCacheKey(this._claimsHandler.EndpointId, token.ContextId, token.KeyGeneration);
			SessionSecurityToken sessionSecurityToken = SecurityContextSecurityTokenHelper.ConvertSctToSessionToken(token, SecureConversationVersion.Default);
			DateTime expiryTime = DateTimeUtil.Add(sessionSecurityToken.ValidTo, this._claimsHandler.SecurityTokenHandlerCollection.Configuration.MaxClockSkew);
			if (this._tokenCache.Get(key) == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperInvalidOperation(SR.GetString("ID4285", new object[]
				{
					sessionSecurityToken.ContextId.ToString()
				}));
			}
			this._tokenCache.AddOrUpdate(key, sessionSecurityToken, expiryTime);
		}

		// Token: 0x06001FE9 RID: 8169 RVA: 0x00077690 File Offset: 0x00075890
		protected override bool TryResolveSecurityKeyCore(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityKey key)
		{
			SecurityToken securityToken;
			if (this.TryResolveTokenCore(keyIdentifierClause, out securityToken))
			{
				key = ((SecurityContextSecurityToken)securityToken).SecurityKeys[0];
				return true;
			}
			key = null;
			return false;
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x000776C4 File Offset: 0x000758C4
		protected override bool TryResolveTokenCore(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityToken token)
		{
			SecurityContextKeyIdentifierClause securityContextKeyIdentifierClause = keyIdentifierClause as SecurityContextKeyIdentifierClause;
			if (securityContextKeyIdentifierClause != null)
			{
				token = this.GetContext(securityContextKeyIdentifierClause.ContextId, securityContextKeyIdentifierClause.Generation);
			}
			else
			{
				token = null;
			}
			return token != null;
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x000776FC File Offset: 0x000758FC
		protected override bool TryResolveTokenCore(SecurityKeyIdentifier keyIdentifier, out SecurityToken token)
		{
			SecurityContextKeyIdentifierClause keyIdentifierClause;
			if (keyIdentifier.TryFind<SecurityContextKeyIdentifierClause>(out keyIdentifierClause))
			{
				return this.TryResolveTokenCore(keyIdentifierClause, out token);
			}
			token = null;
			return false;
		}

		// Token: 0x04001EFF RID: 7935
		private SessionSecurityTokenCache _tokenCache;

		// Token: 0x04001F00 RID: 7936
		private SctClaimsHandler _claimsHandler;
	}
}
