using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Xml;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x02000397 RID: 919
	public class SecurityContextSecurityTokenResolver : SecurityTokenResolver, ISecurityContextSecurityTokenCache
	{
		// Token: 0x060021EF RID: 8687 RVA: 0x0007CA1F File Offset: 0x0007AC1F
		public SecurityContextSecurityTokenResolver(int securityContextCacheCapacity, bool removeOldestTokensOnCacheFull) : this(securityContextCacheCapacity, removeOldestTokensOnCacheFull, SecurityProtocolFactory.defaultMaxClockSkew)
		{
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x0007CA30 File Offset: 0x0007AC30
		public SecurityContextSecurityTokenResolver(int securityContextCacheCapacity, bool removeOldestTokensOnCacheFull, TimeSpan clockSkew)
		{
			if (securityContextCacheCapacity <= 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("securityContextCacheCapacity", SR.GetString("ValueMustBeGreaterThanZero")));
			}
			if (clockSkew < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("clockSkew", SR.GetString("TimeSpanCannotBeLessThanTimeSpanZero")));
			}
			this.capacity = securityContextCacheCapacity;
			this.removeOldestTokensOnCacheFull = removeOldestTokensOnCacheFull;
			this.clockSkew = clockSkew;
			this.tokenCache = new SecurityContextTokenCache(this.capacity, this.removeOldestTokensOnCacheFull, clockSkew);
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x060021F1 RID: 8689 RVA: 0x0007CACA File Offset: 0x0007ACCA
		public int SecurityContextTokenCacheCapacity
		{
			get
			{
				return this.capacity;
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x060021F2 RID: 8690 RVA: 0x0007CAD2 File Offset: 0x0007ACD2
		public TimeSpan ClockSkew
		{
			get
			{
				return this.clockSkew;
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x060021F3 RID: 8691 RVA: 0x0007CADA File Offset: 0x0007ACDA
		public bool RemoveOldestTokensOnCacheFull
		{
			get
			{
				return this.removeOldestTokensOnCacheFull;
			}
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x0007CAE2 File Offset: 0x0007ACE2
		public void AddContext(SecurityContextSecurityToken token)
		{
			this.tokenCache.AddContext(token);
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x0007CAF0 File Offset: 0x0007ACF0
		public bool TryAddContext(SecurityContextSecurityToken token)
		{
			return this.tokenCache.TryAddContext(token);
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x0007CAFE File Offset: 0x0007ACFE
		public void ClearContexts()
		{
			this.tokenCache.ClearContexts();
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x0007CB0B File Offset: 0x0007AD0B
		public void RemoveContext(UniqueId contextId, UniqueId generation)
		{
			this.tokenCache.RemoveContext(contextId, generation, false);
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x0007CB1B File Offset: 0x0007AD1B
		public void RemoveAllContexts(UniqueId contextId)
		{
			this.tokenCache.RemoveAllContexts(contextId);
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x0007CB29 File Offset: 0x0007AD29
		public SecurityContextSecurityToken GetContext(UniqueId contextId, UniqueId generation)
		{
			return this.tokenCache.GetContext(contextId, generation);
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x0007CB38 File Offset: 0x0007AD38
		public Collection<SecurityContextSecurityToken> GetAllContexts(UniqueId contextId)
		{
			return this.tokenCache.GetAllContexts(contextId);
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x0007CB46 File Offset: 0x0007AD46
		public void UpdateContextCachingTime(SecurityContextSecurityToken context, DateTime expirationTime)
		{
			this.tokenCache.UpdateContextCachingTime(context, expirationTime);
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x0007CB58 File Offset: 0x0007AD58
		protected override bool TryResolveTokenCore(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityToken token)
		{
			SecurityContextKeyIdentifierClause securityContextKeyIdentifierClause = keyIdentifierClause as SecurityContextKeyIdentifierClause;
			if (securityContextKeyIdentifierClause != null)
			{
				token = this.tokenCache.GetContext(securityContextKeyIdentifierClause.ContextId, securityContextKeyIdentifierClause.Generation);
			}
			else
			{
				token = null;
			}
			return token != null;
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x0007CB94 File Offset: 0x0007AD94
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

		// Token: 0x060021FE RID: 8702 RVA: 0x0007CBC8 File Offset: 0x0007ADC8
		protected override bool TryResolveTokenCore(SecurityKeyIdentifier keyIdentifier, out SecurityToken token)
		{
			SecurityContextKeyIdentifierClause keyIdentifierClause;
			if (keyIdentifier.TryFind<SecurityContextKeyIdentifierClause>(out keyIdentifierClause))
			{
				return base.TryResolveToken(keyIdentifierClause, out token);
			}
			token = null;
			return false;
		}

		// Token: 0x04001F89 RID: 8073
		private SecurityContextTokenCache tokenCache;

		// Token: 0x04001F8A RID: 8074
		private bool removeOldestTokensOnCacheFull;

		// Token: 0x04001F8B RID: 8075
		private int capacity;

		// Token: 0x04001F8C RID: 8076
		private TimeSpan clockSkew = SecurityProtocolFactory.defaultMaxClockSkew;
	}
}
