using System;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x020005BA RID: 1466
	public class CrlID : Asn1Encodable
	{
		// Token: 0x0600326D RID: 12909 RVA: 0x00138E30 File Offset: 0x00137E30
		public CrlID(Asn1Sequence seq)
		{
			foreach (object obj in seq)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				switch (asn1TaggedObject.TagNo)
				{
				case 0:
					this.crlUrl = DerIA5String.GetInstance(asn1TaggedObject, true);
					break;
				case 1:
					this.crlNum = DerInteger.GetInstance(asn1TaggedObject, true);
					break;
				case 2:
					this.crlTime = DerGeneralizedTime.GetInstance(asn1TaggedObject, true);
					break;
				default:
					throw new ArgumentException("unknown tag number: " + asn1TaggedObject.TagNo);
				}
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x0600326E RID: 12910 RVA: 0x00138EE8 File Offset: 0x00137EE8
		public DerIA5String CrlUrl
		{
			get
			{
				return this.crlUrl;
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x0600326F RID: 12911 RVA: 0x00138EF0 File Offset: 0x00137EF0
		public DerInteger CrlNum
		{
			get
			{
				return this.crlNum;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06003270 RID: 12912 RVA: 0x00138EF8 File Offset: 0x00137EF8
		public DerGeneralizedTime CrlTime
		{
			get
			{
				return this.crlTime;
			}
		}

		// Token: 0x06003271 RID: 12913 RVA: 0x00138F00 File Offset: 0x00137F00
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this.crlUrl != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.crlUrl)
				});
			}
			if (this.crlNum != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, this.crlNum)
				});
			}
			if (this.crlTime != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 2, this.crlTime)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002282 RID: 8834
		private readonly DerIA5String crlUrl;

		// Token: 0x04002283 RID: 8835
		private readonly DerInteger crlNum;

		// Token: 0x04002284 RID: 8836
		private readonly DerGeneralizedTime crlTime;
	}
}
