using System;
using System.Collections;

namespace Org.BouncyCastle.Asn1.X9
{
	// Token: 0x02000621 RID: 1569
	public class OtherInfo : Asn1Encodable
	{
		// Token: 0x06003558 RID: 13656 RVA: 0x0014B295 File Offset: 0x0014A295
		public OtherInfo(KeySpecificInfo keyInfo, Asn1OctetString partyAInfo, Asn1OctetString suppPubInfo)
		{
			this.keyInfo = keyInfo;
			this.partyAInfo = partyAInfo;
			this.suppPubInfo = suppPubInfo;
		}

		// Token: 0x06003559 RID: 13657 RVA: 0x0014B2B4 File Offset: 0x0014A2B4
		public OtherInfo(Asn1Sequence seq)
		{
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			this.keyInfo = new KeySpecificInfo((Asn1Sequence)enumerator.Current);
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				DerTaggedObject derTaggedObject = (DerTaggedObject)obj;
				if (derTaggedObject.TagNo == 0)
				{
					this.partyAInfo = (Asn1OctetString)derTaggedObject.GetObject();
				}
				else if (derTaggedObject.TagNo == 2)
				{
					this.suppPubInfo = (Asn1OctetString)derTaggedObject.GetObject();
				}
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x0600355A RID: 13658 RVA: 0x0014B336 File Offset: 0x0014A336
		public KeySpecificInfo KeyInfo
		{
			get
			{
				return this.keyInfo;
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x0600355B RID: 13659 RVA: 0x0014B33E File Offset: 0x0014A33E
		public Asn1OctetString PartyAInfo
		{
			get
			{
				return this.partyAInfo;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x0600355C RID: 13660 RVA: 0x0014B346 File Offset: 0x0014A346
		public Asn1OctetString SuppPubInfo
		{
			get
			{
				return this.suppPubInfo;
			}
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x0014B350 File Offset: 0x0014A350
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.keyInfo
			});
			if (this.partyAInfo != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(0, this.partyAInfo)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				new DerTaggedObject(2, this.suppPubInfo)
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040023A6 RID: 9126
		private KeySpecificInfo keyInfo;

		// Token: 0x040023A7 RID: 9127
		private Asn1OctetString partyAInfo;

		// Token: 0x040023A8 RID: 9128
		private Asn1OctetString suppPubInfo;
	}
}
