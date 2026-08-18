using System;
using System.Security.Cryptography.X509Certificates;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000197 RID: 407
	public class X509SigningCredentials : SigningCredentials
	{
		// Token: 0x06000D67 RID: 3431 RVA: 0x0003E907 File Offset: 0x0003CB07
		public X509SigningCredentials(X509Certificate2 certificate) : this(certificate, new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
		{
			new X509SecurityToken(certificate).CreateKeyIdentifierClause<X509RawDataKeyIdentifierClause>()
		}))
		{
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0003E929 File Offset: 0x0003CB29
		public X509SigningCredentials(X509Certificate2 certificate, string signatureAlgorithm, string digestAlgorithm) : this(new X509SecurityToken(certificate), new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
		{
			new X509SecurityToken(certificate).CreateKeyIdentifierClause<X509RawDataKeyIdentifierClause>()
		}), signatureAlgorithm, digestAlgorithm)
		{
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0003E952 File Offset: 0x0003CB52
		public X509SigningCredentials(X509Certificate2 certificate, SecurityKeyIdentifier ski) : this(new X509SecurityToken(certificate), ski, "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256", "http://www.w3.org/2001/04/xmlenc#sha256")
		{
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0003E96B File Offset: 0x0003CB6B
		public X509SigningCredentials(X509Certificate2 certificate, SecurityKeyIdentifier ski, string signatureAlgorithm, string digestAlgorithm) : this(new X509SecurityToken(certificate), ski, signatureAlgorithm, digestAlgorithm)
		{
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x0003E980 File Offset: 0x0003CB80
		internal X509SigningCredentials(X509SecurityToken token, SecurityKeyIdentifier ski, string signatureAlgorithm, string digestAlgorithm) : base(token.SecurityKeys[0], signatureAlgorithm, digestAlgorithm, ski)
		{
			this.certificate = token.Certificate;
			if (!this.certificate.HasPrivateKey)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("token", SR.GetString("ID2057"));
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000D6C RID: 3436 RVA: 0x0003E9D6 File Offset: 0x0003CBD6
		public X509Certificate2 Certificate
		{
			get
			{
				return this.certificate;
			}
		}

		// Token: 0x04000CC5 RID: 3269
		private X509Certificate2 certificate;
	}
}
