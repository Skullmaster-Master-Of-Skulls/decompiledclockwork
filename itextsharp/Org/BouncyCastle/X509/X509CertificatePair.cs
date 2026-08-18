using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Security.Certificates;

namespace Org.BouncyCastle.X509
{
	// Token: 0x020002E2 RID: 738
	public class X509CertificatePair
	{
		// Token: 0x06001B68 RID: 7016 RVA: 0x000A4FED File Offset: 0x000A3FED
		public X509CertificatePair(X509Certificate forward, X509Certificate reverse)
		{
			this.forward = forward;
			this.reverse = reverse;
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x000A5003 File Offset: 0x000A4003
		public X509CertificatePair(CertificatePair pair)
		{
			if (pair.Forward != null)
			{
				this.forward = new X509Certificate(pair.Forward);
			}
			if (pair.Reverse != null)
			{
				this.reverse = new X509Certificate(pair.Reverse);
			}
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x000A5040 File Offset: 0x000A4040
		public byte[] GetEncoded()
		{
			byte[] derEncoded;
			try
			{
				X509CertificateStructure x509CertificateStructure = null;
				X509CertificateStructure x509CertificateStructure2 = null;
				if (this.forward != null)
				{
					x509CertificateStructure = X509CertificateStructure.GetInstance(Asn1Object.FromByteArray(this.forward.GetEncoded()));
				}
				if (this.reverse != null)
				{
					x509CertificateStructure2 = X509CertificateStructure.GetInstance(Asn1Object.FromByteArray(this.reverse.GetEncoded()));
				}
				derEncoded = new CertificatePair(x509CertificateStructure, x509CertificateStructure2).GetDerEncoded();
			}
			catch (Exception ex)
			{
				throw new CertificateEncodingException(ex.Message, ex);
			}
			return derEncoded;
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x000A50BC File Offset: 0x000A40BC
		public X509Certificate Forward
		{
			get
			{
				return this.forward;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001B6C RID: 7020 RVA: 0x000A50C4 File Offset: 0x000A40C4
		public X509Certificate Reverse
		{
			get
			{
				return this.reverse;
			}
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x000A50CC File Offset: 0x000A40CC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			X509CertificatePair x509CertificatePair = obj as X509CertificatePair;
			return x509CertificatePair != null && object.Equals(this.forward, x509CertificatePair.forward) && object.Equals(this.reverse, x509CertificatePair.reverse);
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x000A5114 File Offset: 0x000A4114
		public override int GetHashCode()
		{
			int num = -1;
			if (this.forward != null)
			{
				num ^= this.forward.GetHashCode();
			}
			if (this.reverse != null)
			{
				num *= 17;
				num ^= this.reverse.GetHashCode();
			}
			return num;
		}

		// Token: 0x040012F5 RID: 4853
		private readonly X509Certificate forward;

		// Token: 0x040012F6 RID: 4854
		private readonly X509Certificate reverse;
	}
}
