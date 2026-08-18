using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x0200030F RID: 783
	public class ResponderID : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06001CA0 RID: 7328 RVA: 0x000AB2C0 File Offset: 0x000AA2C0
		public static ResponderID GetInstance(object obj)
		{
			if (obj == null || obj is ResponderID)
			{
				return (ResponderID)obj;
			}
			if (obj is DerOctetString)
			{
				return new ResponderID((DerOctetString)obj);
			}
			if (!(obj is Asn1TaggedObject))
			{
				return new ResponderID(X509Name.GetInstance(obj));
			}
			Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
			if (asn1TaggedObject.TagNo == 1)
			{
				return new ResponderID(X509Name.GetInstance(asn1TaggedObject, true));
			}
			return new ResponderID(Asn1OctetString.GetInstance(asn1TaggedObject, true));
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x000AB330 File Offset: 0x000AA330
		public ResponderID(Asn1OctetString id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			this.id = id;
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x000AB34D File Offset: 0x000AA34D
		public ResponderID(X509Name id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			this.id = id;
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x000AB36A File Offset: 0x000AA36A
		public override Asn1Object ToAsn1Object()
		{
			if (this.id is Asn1OctetString)
			{
				return new DerTaggedObject(true, 2, this.id);
			}
			return new DerTaggedObject(true, 1, this.id);
		}

		// Token: 0x040013B2 RID: 5042
		private readonly Asn1Encodable id;
	}
}
