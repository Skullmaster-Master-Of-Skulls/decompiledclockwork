using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000403 RID: 1027
	public class AttributeX509 : Asn1Encodable
	{
		// Token: 0x06002312 RID: 8978 RVA: 0x000D81A0 File Offset: 0x000D71A0
		public static AttributeX509 GetInstance(object obj)
		{
			if (obj == null || obj is AttributeX509)
			{
				return (AttributeX509)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new AttributeX509((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x000D81F4 File Offset: 0x000D71F4
		private AttributeX509(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.attrType = DerObjectIdentifier.GetInstance(seq[0]);
			this.attrValues = Asn1Set.GetInstance(seq[1]);
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x000D824F File Offset: 0x000D724F
		public AttributeX509(DerObjectIdentifier attrType, Asn1Set attrValues)
		{
			this.attrType = attrType;
			this.attrValues = attrValues;
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06002315 RID: 8981 RVA: 0x000D8265 File Offset: 0x000D7265
		public DerObjectIdentifier AttrType
		{
			get
			{
				return this.attrType;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06002316 RID: 8982 RVA: 0x000D826D File Offset: 0x000D726D
		public Asn1Set AttrValues
		{
			get
			{
				return this.attrValues;
			}
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x000D8278 File Offset: 0x000D7278
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.attrType,
				this.attrValues
			});
		}

		// Token: 0x040017E2 RID: 6114
		private readonly DerObjectIdentifier attrType;

		// Token: 0x040017E3 RID: 6115
		private readonly Asn1Set attrValues;
	}
}
