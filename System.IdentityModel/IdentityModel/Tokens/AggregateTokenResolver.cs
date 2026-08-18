using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Selectors;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200010A RID: 266
	public class AggregateTokenResolver : SecurityTokenResolver
	{
		// Token: 0x06000756 RID: 1878 RVA: 0x0001F36A File Offset: 0x0001D56A
		public AggregateTokenResolver(IEnumerable<SecurityTokenResolver> tokenResolvers)
		{
			if (tokenResolvers == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenResolvers");
			}
			this.AddNonEmptyResolvers(tokenResolvers);
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x0001F397 File Offset: 0x0001D597
		public ReadOnlyCollection<SecurityTokenResolver> TokenResolvers
		{
			get
			{
				return this._tokenResolvers.AsReadOnly();
			}
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0001F3A4 File Offset: 0x0001D5A4
		protected override bool TryResolveSecurityKeyCore(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityKey key)
		{
			if (keyIdentifierClause == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifierClause");
			}
			key = null;
			foreach (SecurityTokenResolver securityTokenResolver in this._tokenResolvers)
			{
				if (securityTokenResolver.TryResolveSecurityKey(keyIdentifierClause, out key))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001F418 File Offset: 0x0001D618
		protected override bool TryResolveTokenCore(SecurityKeyIdentifier keyIdentifier, out SecurityToken token)
		{
			if (keyIdentifier == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifer");
			}
			token = null;
			foreach (SecurityTokenResolver securityTokenResolver in this._tokenResolvers)
			{
				if (securityTokenResolver.TryResolveToken(keyIdentifier, out token))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001F48C File Offset: 0x0001D68C
		protected override bool TryResolveTokenCore(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityToken token)
		{
			if (keyIdentifierClause == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifierClause");
			}
			token = null;
			foreach (SecurityTokenResolver securityTokenResolver in this._tokenResolvers)
			{
				if (securityTokenResolver.TryResolveToken(keyIdentifierClause, out token))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001F500 File Offset: 0x0001D700
		private void AddNonEmptyResolvers(IEnumerable<SecurityTokenResolver> resolvers)
		{
			foreach (SecurityTokenResolver securityTokenResolver in resolvers)
			{
				if (securityTokenResolver != null && securityTokenResolver != EmptySecurityTokenResolver.Instance)
				{
					this._tokenResolvers.Add(securityTokenResolver);
				}
			}
		}

		// Token: 0x04000AA8 RID: 2728
		private List<SecurityTokenResolver> _tokenResolvers = new List<SecurityTokenResolver>();
	}
}
