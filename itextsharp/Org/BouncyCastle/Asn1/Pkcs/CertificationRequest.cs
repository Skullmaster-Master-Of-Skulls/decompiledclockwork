using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x0200007A RID: 122
	public class CertificationRequest : Asn1Encodable
	{
		// Token: 0x060003E9 RID: 1001 RVA: 0x000152E4 File Offset: 0x000142E4
		public static CertificationRequest GetInstance(object obj)
		{
			if (obj is CertificationRequest)
			{
				return (CertificationRequest)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertificationRequest((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00015333 File Offset: 0x00014333
		protected CertificationRequest()
		{
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0001533B File Offset: 0x0001433B
		public CertificationRequest(CertificationRequestInfo requestInfo, AlgorithmIdentifier algorithm, DerBitString signature)
		{
			this.reqInfo = requestInfo;
			this.sigAlgId = algorithm;
			this.sigBits = signature;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00015358 File Offset: 0x00014358
		public CertificationRequest(Asn1Sequence seq)
		{
			if (seq.Count != 3)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.reqInfo = CertificationRequestInfo.GetInstance(seq[0]);
			this.sigAlgId = AlgorithmIdentifier.GetInstance(seq[1]);
			this.sigBits = DerBitString.GetInstance(seq[2]);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000153BA File Offset: 0x000143BA
		public CertificationRequestInfo GetCertificationRequestInfo()
		{
			return this.reqInfo;
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x000153C2 File Offset: 0x000143C2
		public AlgorithmIdentifier SignatureAlgorithm
		{
			get
			{
				return this.sigAlgId;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x000153CA File Offset: 0x000143CA
		public DerBitString Signature
		{
			get
			{
				return this.sigBits;
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x000153D4 File Offset: 0x000143D4
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.reqInfo,
				this.sigAlgId,
				this.sigBits
			});
		}

		// Token: 0x0400020B RID: 523
		protected CertificationRequestInfo reqInfo;

		// Token: 0x0400020C RID: 524
		protected AlgorithmIdentifier sigAlgId;

		// Token: 0x0400020D RID: 525
		protected DerBitString sigBits;
	}
}
