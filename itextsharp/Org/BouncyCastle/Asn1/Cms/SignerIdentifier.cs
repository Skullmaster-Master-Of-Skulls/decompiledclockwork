using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x020001BC RID: 444
	public class SignerIdentifier : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x060010B9 RID: 4281 RVA: 0x0005F317 File Offset: 0x0005E317
		public SignerIdentifier(IssuerAndSerialNumber id)
		{
			this.id = id;
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x0005F326 File Offset: 0x0005E326
		public SignerIdentifier(Asn1OctetString id)
		{
			this.id = new DerTaggedObject(false, 0, id);
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0005F33C File Offset: 0x0005E33C
		public SignerIdentifier(Asn1Object id)
		{
			this.id = id;
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0005F34C File Offset: 0x0005E34C
		public static SignerIdentifier GetInstance(object o)
		{
			if (o == null || o is SignerIdentifier)
			{
				return (SignerIdentifier)o;
			}
			if (o is IssuerAndSerialNumber)
			{
				return new SignerIdentifier((IssuerAndSerialNumber)o);
			}
			if (o is Asn1OctetString)
			{
				return new SignerIdentifier((Asn1OctetString)o);
			}
			if (o is Asn1Object)
			{
				return new SignerIdentifier((Asn1Object)o);
			}
			throw new ArgumentException("Illegal object in SignerIdentifier: " + o.GetType().Name);
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x0005F3C1 File Offset: 0x0005E3C1
		public bool IsTagged
		{
			get
			{
				return this.id is Asn1TaggedObject;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x0005F3D1 File Offset: 0x0005E3D1
		public Asn1Encodable ID
		{
			get
			{
				if (this.id is Asn1TaggedObject)
				{
					return Asn1OctetString.GetInstance((Asn1TaggedObject)this.id, false);
				}
				return this.id;
			}
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0005F3F8 File Offset: 0x0005E3F8
		public override Asn1Object ToAsn1Object()
		{
			return this.id.ToAsn1Object();
		}

		// Token: 0x04000C31 RID: 3121
		private Asn1Encodable id;
	}
}
