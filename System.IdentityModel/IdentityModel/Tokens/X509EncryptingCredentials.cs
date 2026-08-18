using System;
using System.Security.Cryptography.X509Certificates;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000191 RID: 401
	public class X509EncryptingCredentials : EncryptingCredentials
	{
		// Token: 0x06000D1F RID: 3359 RVA: 0x0003D5AA File Offset: 0x0003B7AA
		public X509EncryptingCredentials(X509Certificate2 certificate) : this(new X509SecurityToken(certificate))
		{
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x0003D5B8 File Offset: 0x0003B7B8
		public X509EncryptingCredentials(X509Certificate2 certificate, string keyWrappingAlgorithm) : this(new X509SecurityToken(certificate), keyWrappingAlgorithm)
		{
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x0003D5C7 File Offset: 0x0003B7C7
		public X509EncryptingCredentials(X509Certificate2 certificate, SecurityKeyIdentifier ski) : this(new X509SecurityToken(certificate), ski, "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p")
		{
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0003D5DB File Offset: 0x0003B7DB
		public X509EncryptingCredentials(X509Certificate2 certificate, SecurityKeyIdentifier ski, string keyWrappingAlgorithm) : this(new X509SecurityToken(certificate), ski, keyWrappingAlgorithm)
		{
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0003D5EB File Offset: 0x0003B7EB
		internal X509EncryptingCredentials(X509SecurityToken token) : this(token, new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
		{
			token.CreateKeyIdentifierClause<X509IssuerSerialKeyIdentifierClause>()
		}), "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p")
		{
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0003D60D File Offset: 0x0003B80D
		internal X509EncryptingCredentials(X509SecurityToken token, string keyWrappingAlgorithm) : this(token, new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
		{
			token.CreateKeyIdentifierClause<X509IssuerSerialKeyIdentifierClause>()
		}), keyWrappingAlgorithm)
		{
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0003D62B File Offset: 0x0003B82B
		internal X509EncryptingCredentials(X509SecurityToken token, SecurityKeyIdentifier ski, string keyWrappingAlgorithm) : base(token.SecurityKeys[0], ski, keyWrappingAlgorithm)
		{
			this.certificate = token.Certificate;
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x0003D64D File Offset: 0x0003B84D
		public X509Certificate2 Certificate
		{
			get
			{
				return this.certificate;
			}
		}

		// Token: 0x04000CAD RID: 3245
		private X509Certificate2 certificate;
	}
}
