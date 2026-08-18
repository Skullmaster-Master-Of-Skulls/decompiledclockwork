using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000413 RID: 1043
	public class Attribute : Asn1Encodable
	{
		// Token: 0x0600237E RID: 9086 RVA: 0x000D9A68 File Offset: 0x000D8A68
		public static Attribute GetInstance(object obj)
		{
			if (obj == null || obj is Attribute)
			{
				return (Attribute)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Attribute((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x000D9ABA File Offset: 0x000D8ABA
		public Attribute(Asn1Sequence seq)
		{
			this.attrType = (DerObjectIdentifier)seq[0];
			this.attrValues = (Asn1Set)seq[1];
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x000D9AE6 File Offset: 0x000D8AE6
		public Attribute(DerObjectIdentifier attrType, Asn1Set attrValues)
		{
			this.attrType = attrType;
			this.attrValues = attrValues;
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06002381 RID: 9089 RVA: 0x000D9AFC File Offset: 0x000D8AFC
		public DerObjectIdentifier AttrType
		{
			get
			{
				return this.attrType;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06002382 RID: 9090 RVA: 0x000D9B04 File Offset: 0x000D8B04
		public Asn1Set AttrValues
		{
			get
			{
				return this.attrValues;
			}
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x000D9B0C File Offset: 0x000D8B0C
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.attrType,
				this.attrValues
			});
		}

		// Token: 0x04001883 RID: 6275
		private DerObjectIdentifier attrType;

		// Token: 0x04001884 RID: 6276
		private Asn1Set attrValues;
	}
}
