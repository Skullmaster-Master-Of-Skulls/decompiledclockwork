using System;

namespace Org.BouncyCastle.Asn1.Ess
{
	// Token: 0x02000408 RID: 1032
	public class ContentIdentifier : Asn1Encodable
	{
		// Token: 0x06002329 RID: 9001 RVA: 0x000D8C24 File Offset: 0x000D7C24
		public static ContentIdentifier GetInstance(object o)
		{
			if (o == null || o is ContentIdentifier)
			{
				return (ContentIdentifier)o;
			}
			if (o is Asn1OctetString)
			{
				return new ContentIdentifier((Asn1OctetString)o);
			}
			throw new ArgumentException("unknown object in 'ContentIdentifier' factory : " + o.GetType().Name + ".");
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x000D8C76 File Offset: 0x000D7C76
		public ContentIdentifier(Asn1OctetString value)
		{
			this.value = value;
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x000D8C85 File Offset: 0x000D7C85
		public ContentIdentifier(byte[] value) : this(new DerOctetString(value))
		{
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x0600232C RID: 9004 RVA: 0x000D8C93 File Offset: 0x000D7C93
		public Asn1OctetString Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x000D8C9B File Offset: 0x000D7C9B
		public override Asn1Object ToAsn1Object()
		{
			return this.value;
		}

		// Token: 0x0400186A RID: 6250
		private Asn1OctetString value;
	}
}
