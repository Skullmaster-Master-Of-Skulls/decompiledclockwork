using System;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000124 RID: 292
	public class IssuerTokenResolver : SecurityTokenResolver
	{
		// Token: 0x06000805 RID: 2053 RVA: 0x0002171D File Offset: 0x0001F91D
		public IssuerTokenResolver() : this(new X509CertificateStoreTokenResolver(IssuerTokenResolver.DefaultStoreName, IssuerTokenResolver.DefaultStoreLocation))
		{
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00021734 File Offset: 0x0001F934
		public IssuerTokenResolver(SecurityTokenResolver wrappedTokenResolver)
		{
			if (wrappedTokenResolver == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wrappedTokenResolver");
			}
			this._wrappedTokenResolver = wrappedTokenResolver;
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x00021756 File Offset: 0x0001F956
		public SecurityTokenResolver WrappedTokenResolver
		{
			get
			{
				return this._wrappedTokenResolver;
			}
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00021760 File Offset: 0x0001F960
		protected override bool TryResolveSecurityKeyCore(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityKey key)
		{
			if (keyIdentifierClause == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifierClause");
			}
			key = null;
			X509RawDataKeyIdentifierClause x509RawDataKeyIdentifierClause = keyIdentifierClause as X509RawDataKeyIdentifierClause;
			if (x509RawDataKeyIdentifierClause != null)
			{
				key = x509RawDataKeyIdentifierClause.CreateKey();
				return true;
			}
			RsaKeyIdentifierClause rsaKeyIdentifierClause = keyIdentifierClause as RsaKeyIdentifierClause;
			if (rsaKeyIdentifierClause != null)
			{
				key = rsaKeyIdentifierClause.CreateKey();
				return true;
			}
			return this._wrappedTokenResolver.TryResolveSecurityKey(keyIdentifierClause, out key);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x000217C0 File Offset: 0x0001F9C0
		protected override bool TryResolveTokenCore(SecurityKeyIdentifier keyIdentifier, out SecurityToken token)
		{
			if (keyIdentifier == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifier");
			}
			token = null;
			foreach (SecurityKeyIdentifierClause keyIdentifierClause in keyIdentifier)
			{
				if (this.TryResolveTokenCore(keyIdentifierClause, out token))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00021828 File Offset: 0x0001FA28
		protected override bool TryResolveTokenCore(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityToken token)
		{
			if (keyIdentifierClause == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifierClause");
			}
			token = null;
			X509RawDataKeyIdentifierClause x509RawDataKeyIdentifierClause = keyIdentifierClause as X509RawDataKeyIdentifierClause;
			if (x509RawDataKeyIdentifierClause != null)
			{
				token = new X509SecurityToken(new X509Certificate2(x509RawDataKeyIdentifierClause.GetX509RawData()));
				return true;
			}
			RsaKeyIdentifierClause rsaKeyIdentifierClause = keyIdentifierClause as RsaKeyIdentifierClause;
			if (rsaKeyIdentifierClause != null)
			{
				token = new RsaSecurityToken(rsaKeyIdentifierClause.Rsa);
				return true;
			}
			return this._wrappedTokenResolver.TryResolveToken(keyIdentifierClause, out token);
		}

		// Token: 0x04000AEF RID: 2799
		public static readonly StoreName DefaultStoreName = StoreName.TrustedPeople;

		// Token: 0x04000AF0 RID: 2800
		public static readonly StoreLocation DefaultStoreLocation = StoreLocation.LocalMachine;

		// Token: 0x04000AF1 RID: 2801
		private SecurityTokenResolver _wrappedTokenResolver;

		// Token: 0x04000AF2 RID: 2802
		internal static IssuerTokenResolver DefaultInstance = new IssuerTokenResolver();
	}
}
