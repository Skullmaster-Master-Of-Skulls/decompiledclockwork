using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000516 RID: 1302
	public class AttributeCertificate : Asn1Encodable
	{
		// Token: 0x06002C85 RID: 11397 RVA: 0x0010EF6C File Offset: 0x0010DF6C
		public static AttributeCertificate GetInstance(object obj)
		{
			if (obj is AttributeCertificate)
			{
				return (AttributeCertificate)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new AttributeCertificate((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x0010EFBB File Offset: 0x0010DFBB
		public AttributeCertificate(AttributeCertificateInfo acinfo, AlgorithmIdentifier signatureAlgorithm, DerBitString signatureValue)
		{
			this.acinfo = acinfo;
			this.signatureAlgorithm = signatureAlgorithm;
			this.signatureValue = signatureValue;
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x0010EFD8 File Offset: 0x0010DFD8
		private AttributeCertificate(Asn1Sequence seq)
		{
			if (seq.Count != 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.acinfo = AttributeCertificateInfo.GetInstance(seq[0]);
			this.signatureAlgorithm = AlgorithmIdentifier.GetInstance(seq[1]);
			this.signatureValue = DerBitString.GetInstance(seq[2]);
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06002C88 RID: 11400 RVA: 0x0010F045 File Offset: 0x0010E045
		public AttributeCertificateInfo ACInfo
		{
			get
			{
				return this.acinfo;
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06002C89 RID: 11401 RVA: 0x0010F04D File Offset: 0x0010E04D
		public AlgorithmIdentifier SignatureAlgorithm
		{
			get
			{
				return this.signatureAlgorithm;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06002C8A RID: 11402 RVA: 0x0010F055 File Offset: 0x0010E055
		public DerBitString SignatureValue
		{
			get
			{
				return this.signatureValue;
			}
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x0010F060 File Offset: 0x0010E060
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.acinfo,
				this.signatureAlgorithm,
				this.signatureValue
			});
		}

		// Token: 0x04001EA4 RID: 7844
		private readonly AttributeCertificateInfo acinfo;

		// Token: 0x04001EA5 RID: 7845
		private readonly AlgorithmIdentifier signatureAlgorithm;

		// Token: 0x04001EA6 RID: 7846
		private readonly DerBitString signatureValue;
	}
}
