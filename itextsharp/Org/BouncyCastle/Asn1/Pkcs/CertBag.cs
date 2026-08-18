using System;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x0200030D RID: 781
	public class CertBag : Asn1Encodable
	{
		// Token: 0x06001C92 RID: 7314 RVA: 0x000AB098 File Offset: 0x000AA098
		public CertBag(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.seq = seq;
			this.certID = DerObjectIdentifier.GetInstance(seq[0]);
			this.certValue = Asn1TaggedObject.GetInstance(seq[1]).GetObject();
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x000AB0F4 File Offset: 0x000AA0F4
		public CertBag(DerObjectIdentifier certID, Asn1Object certValue)
		{
			this.certID = certID;
			this.certValue = certValue;
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001C94 RID: 7316 RVA: 0x000AB10A File Offset: 0x000AA10A
		public DerObjectIdentifier CertID
		{
			get
			{
				return this.certID;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001C95 RID: 7317 RVA: 0x000AB112 File Offset: 0x000AA112
		public Asn1Object CertValue
		{
			get
			{
				return this.certValue;
			}
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x000AB11C File Offset: 0x000AA11C
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.certID,
				new DerTaggedObject(0, this.certValue)
			});
		}

		// Token: 0x040013AC RID: 5036
		private readonly Asn1Sequence seq;

		// Token: 0x040013AD RID: 5037
		private readonly DerObjectIdentifier certID;

		// Token: 0x040013AE RID: 5038
		private readonly Asn1Object certValue;
	}
}
