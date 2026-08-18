using System;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x02000578 RID: 1400
	public class AttributePkcs : Asn1Encodable
	{
		// Token: 0x06002FC6 RID: 12230 RVA: 0x00127588 File Offset: 0x00126588
		public static AttributePkcs GetInstance(object obj)
		{
			AttributePkcs attributePkcs = obj as AttributePkcs;
			if (obj == null || attributePkcs != null)
			{
				return attributePkcs;
			}
			Asn1Sequence asn1Sequence = obj as Asn1Sequence;
			if (asn1Sequence != null)
			{
				return new AttributePkcs(asn1Sequence);
			}
			throw new ArgumentException("Unknown object in factory: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x001275D4 File Offset: 0x001265D4
		private AttributePkcs(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.attrType = DerObjectIdentifier.GetInstance(seq[0]);
			this.attrValues = Asn1Set.GetInstance(seq[1]);
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x00127624 File Offset: 0x00126624
		public AttributePkcs(DerObjectIdentifier attrType, Asn1Set attrValues)
		{
			this.attrType = attrType;
			this.attrValues = attrValues;
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06002FC9 RID: 12233 RVA: 0x0012763A File Offset: 0x0012663A
		public DerObjectIdentifier AttrType
		{
			get
			{
				return this.attrType;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06002FCA RID: 12234 RVA: 0x00127642 File Offset: 0x00126642
		public Asn1Set AttrValues
		{
			get
			{
				return this.attrValues;
			}
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x0012764C File Offset: 0x0012664C
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.attrType,
				this.attrValues
			});
		}

		// Token: 0x040020D6 RID: 8406
		private readonly DerObjectIdentifier attrType;

		// Token: 0x040020D7 RID: 8407
		private readonly Asn1Set attrValues;
	}
}
