using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200020E RID: 526
	public class KekIdentifier : Asn1Encodable
	{
		// Token: 0x06001422 RID: 5154 RVA: 0x000733E2 File Offset: 0x000723E2
		public KekIdentifier(byte[] keyIdentifier, DerGeneralizedTime date, OtherKeyAttribute other)
		{
			this.keyIdentifier = new DerOctetString(keyIdentifier);
			this.date = date;
			this.other = other;
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x00073404 File Offset: 0x00072404
		public KekIdentifier(Asn1Sequence seq)
		{
			this.keyIdentifier = (Asn1OctetString)seq[0];
			switch (seq.Count)
			{
			case 1:
				return;
			case 2:
				if (seq[1] is DerGeneralizedTime)
				{
					this.date = (DerGeneralizedTime)seq[1];
					return;
				}
				this.other = OtherKeyAttribute.GetInstance(seq[2]);
				return;
			case 3:
				this.date = (DerGeneralizedTime)seq[1];
				this.other = OtherKeyAttribute.GetInstance(seq[2]);
				return;
			default:
				throw new ArgumentException("Invalid KekIdentifier");
			}
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x000734AA File Offset: 0x000724AA
		public static KekIdentifier GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return KekIdentifier.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x000734B8 File Offset: 0x000724B8
		public static KekIdentifier GetInstance(object obj)
		{
			if (obj == null || obj is KekIdentifier)
			{
				return (KekIdentifier)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new KekIdentifier((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid KekIdentifier: " + obj.GetType().Name);
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x00073505 File Offset: 0x00072505
		public Asn1OctetString KeyIdentifier
		{
			get
			{
				return this.keyIdentifier;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x0007350D File Offset: 0x0007250D
		public DerGeneralizedTime Date
		{
			get
			{
				return this.date;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x00073515 File Offset: 0x00072515
		public OtherKeyAttribute Other
		{
			get
			{
				return this.other;
			}
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x00073520 File Offset: 0x00072520
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.keyIdentifier
			});
			if (this.date != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.date
				});
			}
			if (this.other != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.other
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000DDF RID: 3551
		private Asn1OctetString keyIdentifier;

		// Token: 0x04000DE0 RID: 3552
		private DerGeneralizedTime date;

		// Token: 0x04000DE1 RID: 3553
		private OtherKeyAttribute other;
	}
}
