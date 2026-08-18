using System;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000192 RID: 402
	public class X509IssuerSerialKeyIdentifierClause : SecurityKeyIdentifierClause
	{
		// Token: 0x06000D27 RID: 3367 RVA: 0x0003D658 File Offset: 0x0003B858
		public X509IssuerSerialKeyIdentifierClause(string issuerName, string issuerSerialNumber) : base(null)
		{
			if (string.IsNullOrEmpty(issuerName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("issuerName");
			}
			if (string.IsNullOrEmpty(issuerSerialNumber))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("issuerSerialNumber");
			}
			this.issuerName = issuerName;
			this.issuerSerialNumber = issuerSerialNumber;
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0003D6AA File Offset: 0x0003B8AA
		public X509IssuerSerialKeyIdentifierClause(X509Certificate2 certificate) : base(null)
		{
			if (certificate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("certificate");
			}
			this.issuerName = certificate.Issuer;
			this.issuerSerialNumber = Asn1IntegerConverter.Asn1IntegerToDecimalString(certificate.GetSerialNumber());
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x0003D6E3 File Offset: 0x0003B8E3
		public string IssuerName
		{
			get
			{
				return this.issuerName;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x0003D6EB File Offset: 0x0003B8EB
		public string IssuerSerialNumber
		{
			get
			{
				return this.issuerSerialNumber;
			}
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0003D6F4 File Offset: 0x0003B8F4
		public override bool Matches(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			X509IssuerSerialKeyIdentifierClause x509IssuerSerialKeyIdentifierClause = keyIdentifierClause as X509IssuerSerialKeyIdentifierClause;
			return this == x509IssuerSerialKeyIdentifierClause || (x509IssuerSerialKeyIdentifierClause != null && x509IssuerSerialKeyIdentifierClause.Matches(this.issuerName, this.issuerSerialNumber));
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0003D725 File Offset: 0x0003B925
		public bool Matches(X509Certificate2 certificate)
		{
			return certificate != null && this.Matches(certificate.Issuer, Asn1IntegerConverter.Asn1IntegerToDecimalString(certificate.GetSerialNumber()));
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0003D744 File Offset: 0x0003B944
		public bool Matches(string issuerName, string issuerSerialNumber)
		{
			if (issuerName == null)
			{
				return false;
			}
			if (this.issuerSerialNumber != issuerSerialNumber)
			{
				return false;
			}
			if (this.issuerName == issuerName)
			{
				return true;
			}
			bool result = false;
			try
			{
				if (CryptoHelper.IsEqual(new X500DistinguishedName(this.issuerName).RawData, new X500DistinguishedName(issuerName).RawData))
				{
					result = true;
				}
			}
			catch (CryptographicException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Warning);
			}
			return result;
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0003D7BC File Offset: 0x0003B9BC
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "X509IssuerSerialKeyIdentifierClause(Issuer = '{0}', Serial = '{1}')", new object[]
			{
				this.IssuerName,
				this.IssuerSerialNumber
			});
		}

		// Token: 0x04000CAE RID: 3246
		private readonly string issuerName;

		// Token: 0x04000CAF RID: 3247
		private readonly string issuerSerialNumber;
	}
}
