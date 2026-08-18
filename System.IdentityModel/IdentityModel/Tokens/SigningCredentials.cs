using System;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000178 RID: 376
	public class SigningCredentials
	{
		// Token: 0x06000BC5 RID: 3013 RVA: 0x00037148 File Offset: 0x00035348
		public SigningCredentials(SecurityKey signingKey, string signatureAlgorithm, string digestAlgorithm) : this(signingKey, signatureAlgorithm, digestAlgorithm, null)
		{
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00037154 File Offset: 0x00035354
		public SigningCredentials(SecurityKey signingKey, string signatureAlgorithm, string digestAlgorithm, SecurityKeyIdentifier signingKeyIdentifier)
		{
			if (signingKey == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("signingKey"));
			}
			if (signatureAlgorithm == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("signatureAlgorithm"));
			}
			if (digestAlgorithm == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("digestAlgorithm"));
			}
			this.signingKey = signingKey;
			this.signatureAlgorithm = signatureAlgorithm;
			this.digestAlgorithm = digestAlgorithm;
			this.signingKeyIdentifier = signingKeyIdentifier;
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x000371CC File Offset: 0x000353CC
		public string DigestAlgorithm
		{
			get
			{
				return this.digestAlgorithm;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x000371D4 File Offset: 0x000353D4
		public string SignatureAlgorithm
		{
			get
			{
				return this.signatureAlgorithm;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x000371DC File Offset: 0x000353DC
		public SecurityKey SigningKey
		{
			get
			{
				return this.signingKey;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x000371E4 File Offset: 0x000353E4
		public SecurityKeyIdentifier SigningKeyIdentifier
		{
			get
			{
				return this.signingKeyIdentifier;
			}
		}

		// Token: 0x04000C48 RID: 3144
		private string digestAlgorithm;

		// Token: 0x04000C49 RID: 3145
		private string signatureAlgorithm;

		// Token: 0x04000C4A RID: 3146
		private SecurityKey signingKey;

		// Token: 0x04000C4B RID: 3147
		private SecurityKeyIdentifier signingKeyIdentifier;
	}
}
