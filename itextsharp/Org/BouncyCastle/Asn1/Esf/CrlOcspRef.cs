using System;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000312 RID: 786
	public class CrlOcspRef : Asn1Encodable
	{
		// Token: 0x06001CAB RID: 7339 RVA: 0x000AB628 File Offset: 0x000AA628
		public static CrlOcspRef GetInstance(object obj)
		{
			if (obj == null || obj is CrlOcspRef)
			{
				return (CrlOcspRef)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CrlOcspRef((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'CrlOcspRef' factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x000AB67C File Offset: 0x000AA67C
		private CrlOcspRef(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			foreach (object obj in seq)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				Asn1Object @object = asn1TaggedObject.GetObject();
				switch (asn1TaggedObject.TagNo)
				{
				case 0:
					this.crlids = CrlListID.GetInstance(@object);
					break;
				case 1:
					this.ocspids = OcspListID.GetInstance(@object);
					break;
				case 2:
					this.otherRev = OtherRevRefs.GetInstance(@object);
					break;
				default:
					throw new ArgumentException("Illegal tag in CrlOcspRef", "seq");
				}
			}
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x000AB73C File Offset: 0x000AA73C
		public CrlOcspRef(CrlListID crlids, OcspListID ocspids, OtherRevRefs otherRev)
		{
			this.crlids = crlids;
			this.ocspids = ocspids;
			this.otherRev = otherRev;
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001CAE RID: 7342 RVA: 0x000AB759 File Offset: 0x000AA759
		public CrlListID CrlIDs
		{
			get
			{
				return this.crlids;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001CAF RID: 7343 RVA: 0x000AB761 File Offset: 0x000AA761
		public OcspListID OcspIDs
		{
			get
			{
				return this.ocspids;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001CB0 RID: 7344 RVA: 0x000AB769 File Offset: 0x000AA769
		public OtherRevRefs OtherRev
		{
			get
			{
				return this.otherRev;
			}
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x000AB774 File Offset: 0x000AA774
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this.crlids != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.crlids.ToAsn1Object())
				});
			}
			if (this.ocspids != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, this.ocspids.ToAsn1Object())
				});
			}
			if (this.otherRev != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 2, this.otherRev.ToAsn1Object())
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040013CA RID: 5066
		private readonly CrlListID crlids;

		// Token: 0x040013CB RID: 5067
		private readonly OcspListID ocspids;

		// Token: 0x040013CC RID: 5068
		private readonly OtherRevRefs otherRev;
	}
}
