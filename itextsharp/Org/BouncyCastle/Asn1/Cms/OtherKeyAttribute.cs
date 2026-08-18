using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000492 RID: 1170
	public class OtherKeyAttribute : Asn1Encodable
	{
		// Token: 0x0600279D RID: 10141 RVA: 0x000EE6E8 File Offset: 0x000ED6E8
		public static OtherKeyAttribute GetInstance(object obj)
		{
			if (obj == null || obj is OtherKeyAttribute)
			{
				return (OtherKeyAttribute)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OtherKeyAttribute((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x000EE73A File Offset: 0x000ED73A
		public OtherKeyAttribute(Asn1Sequence seq)
		{
			this.keyAttrId = (DerObjectIdentifier)seq[0];
			this.keyAttr = seq[1];
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x000EE761 File Offset: 0x000ED761
		public OtherKeyAttribute(DerObjectIdentifier keyAttrId, Asn1Encodable keyAttr)
		{
			this.keyAttrId = keyAttrId;
			this.keyAttr = keyAttr;
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x060027A0 RID: 10144 RVA: 0x000EE777 File Offset: 0x000ED777
		public DerObjectIdentifier KeyAttrId
		{
			get
			{
				return this.keyAttrId;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x060027A1 RID: 10145 RVA: 0x000EE77F File Offset: 0x000ED77F
		public Asn1Encodable KeyAttr
		{
			get
			{
				return this.keyAttr;
			}
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x000EE788 File Offset: 0x000ED788
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.keyAttrId,
				this.keyAttr
			});
		}

		// Token: 0x04001B34 RID: 6964
		private DerObjectIdentifier keyAttrId;

		// Token: 0x04001B35 RID: 6965
		private Asn1Encodable keyAttr;
	}
}
