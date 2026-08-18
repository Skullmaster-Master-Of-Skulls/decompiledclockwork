using System;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000194 RID: 404
	public class X509RawDataKeyIdentifierClause : BinaryKeyIdentifierClause
	{
		// Token: 0x06000D33 RID: 3379 RVA: 0x0003D8E8 File Offset: 0x0003BAE8
		public X509RawDataKeyIdentifierClause(X509Certificate2 certificate) : base(null, X509RawDataKeyIdentifierClause.GetRawData(certificate), false)
		{
			this.certificate = certificate;
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x0003D8FF File Offset: 0x0003BAFF
		public X509RawDataKeyIdentifierClause(byte[] certificateRawData) : this(certificateRawData, true)
		{
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0003D909 File Offset: 0x0003BB09
		internal X509RawDataKeyIdentifierClause(byte[] certificateRawData, bool cloneBuffer) : base(null, X509Helper.VerifyNotPfx(certificateRawData), cloneBuffer)
		{
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x00002434 File Offset: 0x00000634
		public override bool CanCreateKey
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0003D919 File Offset: 0x0003BB19
		public override SecurityKey CreateKey()
		{
			if (this.key == null)
			{
				if (this.certificate == null)
				{
					this.certificate = new X509Certificate2(base.GetBuffer());
				}
				this.key = new X509AsymmetricSecurityKey(this.certificate);
			}
			return this.key;
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0003D953 File Offset: 0x0003BB53
		private static byte[] GetRawData(X509Certificate certificate)
		{
			if (certificate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("certificate");
			}
			return certificate.GetRawCertData();
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0000242C File Offset: 0x0000062C
		public byte[] GetX509RawData()
		{
			return base.GetBuffer();
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x0003D96E File Offset: 0x0003BB6E
		public bool Matches(X509Certificate2 certificate)
		{
			return certificate != null && base.Matches(X509RawDataKeyIdentifierClause.GetRawData(certificate));
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0003D981 File Offset: 0x0003BB81
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "X509RawDataKeyIdentifierClause(RawData = {0})", new object[]
			{
				base.ToBase64String()
			});
		}

		// Token: 0x04000CB3 RID: 3251
		private X509Certificate2 certificate;

		// Token: 0x04000CB4 RID: 3252
		private X509AsymmetricSecurityKey key;
	}
}
