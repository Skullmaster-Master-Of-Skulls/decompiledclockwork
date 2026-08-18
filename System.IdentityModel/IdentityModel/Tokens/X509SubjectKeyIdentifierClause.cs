using System;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000198 RID: 408
	public class X509SubjectKeyIdentifierClause : BinaryKeyIdentifierClause
	{
		// Token: 0x06000D6D RID: 3437 RVA: 0x0003E9DE File Offset: 0x0003CBDE
		public X509SubjectKeyIdentifierClause(byte[] ski) : this(ski, true)
		{
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0003E9E8 File Offset: 0x0003CBE8
		internal X509SubjectKeyIdentifierClause(byte[] ski, bool cloneBuffer) : base(null, ski, cloneBuffer)
		{
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0003E9F4 File Offset: 0x0003CBF4
		private static byte[] GetSkiRawData(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("certificate");
			}
			X509SubjectKeyIdentifierExtension x509SubjectKeyIdentifierExtension = certificate.Extensions["2.5.29.14"] as X509SubjectKeyIdentifierExtension;
			if (x509SubjectKeyIdentifierExtension != null)
			{
				return x509SubjectKeyIdentifierExtension.RawData;
			}
			return null;
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0000242C File Offset: 0x0000062C
		public byte[] GetX509SubjectKeyIdentifier()
		{
			return base.GetBuffer();
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x0003EA38 File Offset: 0x0003CC38
		public bool Matches(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				return false;
			}
			byte[] skiRawData = X509SubjectKeyIdentifierClause.GetSkiRawData(certificate);
			return skiRawData != null && base.Matches(skiRawData, 2);
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0003EA60 File Offset: 0x0003CC60
		public static bool TryCreateFrom(X509Certificate2 certificate, out X509SubjectKeyIdentifierClause keyIdentifierClause)
		{
			byte[] skiRawData = X509SubjectKeyIdentifierClause.GetSkiRawData(certificate);
			keyIdentifierClause = null;
			if (skiRawData != null)
			{
				byte[] ski = SecurityUtils.CloneBuffer(skiRawData, 2, skiRawData.Length - 2);
				keyIdentifierClause = new X509SubjectKeyIdentifierClause(ski, false);
			}
			return keyIdentifierClause != null;
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0003EA95 File Offset: 0x0003CC95
		public static bool CanCreateFrom(X509Certificate2 certificate)
		{
			return X509SubjectKeyIdentifierClause.GetSkiRawData(certificate) != null;
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0003EAA0 File Offset: 0x0003CCA0
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "X509SubjectKeyIdentifierClause(SKI = 0x{0})", new object[]
			{
				base.ToHexString()
			});
		}

		// Token: 0x04000CC6 RID: 3270
		private const string SubjectKeyIdentifierOid = "2.5.29.14";

		// Token: 0x04000CC7 RID: 3271
		private const int SkiDataOffset = 2;
	}
}
