using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020005B8 RID: 1464
	public class CertificatePair : Asn1Encodable
	{
		// Token: 0x06003264 RID: 12900 RVA: 0x00138C28 File Offset: 0x00137C28
		public static CertificatePair GetInstance(object obj)
		{
			if (obj == null || obj is CertificatePair)
			{
				return (CertificatePair)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertificatePair((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06003265 RID: 12901 RVA: 0x00138C7C File Offset: 0x00137C7C
		private CertificatePair(Asn1Sequence seq)
		{
			if (seq.Count != 1 && seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			foreach (object obj in seq)
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(obj);
				if (instance.TagNo == 0)
				{
					this.forward = X509CertificateStructure.GetInstance(instance, true);
				}
				else
				{
					if (instance.TagNo != 1)
					{
						throw new ArgumentException("Bad tag number: " + instance.TagNo);
					}
					this.reverse = X509CertificateStructure.GetInstance(instance, true);
				}
			}
		}

		// Token: 0x06003266 RID: 12902 RVA: 0x00138D50 File Offset: 0x00137D50
		public CertificatePair(X509CertificateStructure forward, X509CertificateStructure reverse)
		{
			this.forward = forward;
			this.reverse = reverse;
		}

		// Token: 0x06003267 RID: 12903 RVA: 0x00138D68 File Offset: 0x00137D68
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this.forward != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(0, this.forward)
				});
			}
			if (this.reverse != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(1, this.reverse)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06003268 RID: 12904 RVA: 0x00138DD1 File Offset: 0x00137DD1
		public X509CertificateStructure Forward
		{
			get
			{
				return this.forward;
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06003269 RID: 12905 RVA: 0x00138DD9 File Offset: 0x00137DD9
		public X509CertificateStructure Reverse
		{
			get
			{
				return this.reverse;
			}
		}

		// Token: 0x04002280 RID: 8832
		private X509CertificateStructure forward;

		// Token: 0x04002281 RID: 8833
		private X509CertificateStructure reverse;
	}
}
