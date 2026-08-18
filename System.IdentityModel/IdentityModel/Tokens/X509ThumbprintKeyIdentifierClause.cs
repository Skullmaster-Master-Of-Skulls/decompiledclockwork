using System;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000199 RID: 409
	public class X509ThumbprintKeyIdentifierClause : BinaryKeyIdentifierClause
	{
		// Token: 0x06000D75 RID: 3445 RVA: 0x0003EAC0 File Offset: 0x0003CCC0
		public X509ThumbprintKeyIdentifierClause(X509Certificate2 certificate) : this(X509ThumbprintKeyIdentifierClause.GetHash(certificate), false)
		{
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0003EACF File Offset: 0x0003CCCF
		public X509ThumbprintKeyIdentifierClause(byte[] thumbprint) : this(thumbprint, true)
		{
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0003E9E8 File Offset: 0x0003CBE8
		internal X509ThumbprintKeyIdentifierClause(byte[] thumbprint, bool cloneBuffer) : base(null, thumbprint, cloneBuffer)
		{
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0003EAD9 File Offset: 0x0003CCD9
		private static byte[] GetHash(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("certificate");
			}
			return certificate.GetCertHash();
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0000242C File Offset: 0x0000062C
		public byte[] GetX509Thumbprint()
		{
			return base.GetBuffer();
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x0003EAF4 File Offset: 0x0003CCF4
		public bool Matches(X509Certificate2 certificate)
		{
			return certificate != null && base.Matches(X509ThumbprintKeyIdentifierClause.GetHash(certificate));
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0003EB07 File Offset: 0x0003CD07
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "X509ThumbprintKeyIdentifierClause(Hash = 0x{0})", new object[]
			{
				base.ToHexString()
			});
		}
	}
}
