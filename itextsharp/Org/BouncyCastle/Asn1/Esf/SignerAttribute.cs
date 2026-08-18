using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x020005BD RID: 1469
	public class SignerAttribute : Asn1Encodable
	{
		// Token: 0x0600327E RID: 12926 RVA: 0x00139268 File Offset: 0x00138268
		public static SignerAttribute GetInstance(object obj)
		{
			if (obj == null || obj is SignerAttribute)
			{
				return (SignerAttribute)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SignerAttribute(obj);
			}
			throw new ArgumentException("Unknown object in 'SignerAttribute' factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x0600327F RID: 12927 RVA: 0x001392B8 File Offset: 0x001382B8
		private SignerAttribute(object obj)
		{
			Asn1Sequence asn1Sequence = (Asn1Sequence)obj;
			DerTaggedObject derTaggedObject = (DerTaggedObject)asn1Sequence[0];
			if (derTaggedObject.TagNo == 0)
			{
				this.claimedAttributes = Asn1Sequence.GetInstance(derTaggedObject, true);
				return;
			}
			if (derTaggedObject.TagNo == 1)
			{
				this.certifiedAttributes = AttributeCertificate.GetInstance(derTaggedObject);
				return;
			}
			throw new ArgumentException("illegal tag.", "obj");
		}

		// Token: 0x06003280 RID: 12928 RVA: 0x0013931A File Offset: 0x0013831A
		public SignerAttribute(Asn1Sequence claimedAttributes)
		{
			this.claimedAttributes = claimedAttributes;
		}

		// Token: 0x06003281 RID: 12929 RVA: 0x00139329 File Offset: 0x00138329
		public SignerAttribute(AttributeCertificate certifiedAttributes)
		{
			this.certifiedAttributes = certifiedAttributes;
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06003282 RID: 12930 RVA: 0x00139338 File Offset: 0x00138338
		public virtual Asn1Sequence ClaimedAttributes
		{
			get
			{
				return this.claimedAttributes;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06003283 RID: 12931 RVA: 0x00139340 File Offset: 0x00138340
		public virtual AttributeCertificate CertifiedAttributes
		{
			get
			{
				return this.certifiedAttributes;
			}
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x00139348 File Offset: 0x00138348
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this.claimedAttributes != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(0, this.claimedAttributes)
				});
			}
			else
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(1, this.certifiedAttributes)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002289 RID: 8841
		private Asn1Sequence claimedAttributes;

		// Token: 0x0400228A RID: 8842
		private AttributeCertificate certifiedAttributes;
	}
}
