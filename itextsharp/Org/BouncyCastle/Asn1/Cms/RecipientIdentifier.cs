using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000491 RID: 1169
	public class RecipientIdentifier : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06002796 RID: 10134 RVA: 0x000EE5F4 File Offset: 0x000ED5F4
		public RecipientIdentifier(IssuerAndSerialNumber id)
		{
			this.id = id;
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x000EE603 File Offset: 0x000ED603
		public RecipientIdentifier(Asn1OctetString id)
		{
			this.id = new DerTaggedObject(false, 0, id);
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x000EE619 File Offset: 0x000ED619
		public RecipientIdentifier(Asn1Object id)
		{
			this.id = id;
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x000EE628 File Offset: 0x000ED628
		public static RecipientIdentifier GetInstance(object o)
		{
			if (o == null || o is RecipientIdentifier)
			{
				return (RecipientIdentifier)o;
			}
			if (o is IssuerAndSerialNumber)
			{
				return new RecipientIdentifier((IssuerAndSerialNumber)o);
			}
			if (o is Asn1OctetString)
			{
				return new RecipientIdentifier((Asn1OctetString)o);
			}
			if (o is Asn1Object)
			{
				return new RecipientIdentifier((Asn1Object)o);
			}
			throw new ArgumentException("Illegal object in RecipientIdentifier: " + o.GetType().Name);
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x0600279A RID: 10138 RVA: 0x000EE69D File Offset: 0x000ED69D
		public bool IsTagged
		{
			get
			{
				return this.id is Asn1TaggedObject;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x0600279B RID: 10139 RVA: 0x000EE6AD File Offset: 0x000ED6AD
		public Asn1Encodable ID
		{
			get
			{
				if (this.id is Asn1TaggedObject)
				{
					return Asn1OctetString.GetInstance((Asn1TaggedObject)this.id, false);
				}
				return IssuerAndSerialNumber.GetInstance(this.id);
			}
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x000EE6D9 File Offset: 0x000ED6D9
		public override Asn1Object ToAsn1Object()
		{
			return this.id.ToAsn1Object();
		}

		// Token: 0x04001B33 RID: 6963
		private Asn1Encodable id;
	}
}
