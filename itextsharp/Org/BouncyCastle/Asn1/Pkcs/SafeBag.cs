using System;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x0200003A RID: 58
	public class SafeBag : Asn1Encodable
	{
		// Token: 0x0600017C RID: 380 RVA: 0x0000928E File Offset: 0x0000828E
		public SafeBag(DerObjectIdentifier oid, Asn1Object obj)
		{
			this.bagID = oid;
			this.bagValue = obj;
			this.bagAttributes = null;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000092AB File Offset: 0x000082AB
		public SafeBag(DerObjectIdentifier oid, Asn1Object obj, Asn1Set bagAttributes)
		{
			this.bagID = oid;
			this.bagValue = obj;
			this.bagAttributes = bagAttributes;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000092C8 File Offset: 0x000082C8
		public SafeBag(Asn1Sequence seq)
		{
			this.bagID = (DerObjectIdentifier)seq[0];
			this.bagValue = ((DerTaggedObject)seq[1]).GetObject();
			if (seq.Count == 3)
			{
				this.bagAttributes = (Asn1Set)seq[2];
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000931F File Offset: 0x0000831F
		public DerObjectIdentifier BagID
		{
			get
			{
				return this.bagID;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00009327 File Offset: 0x00008327
		public Asn1Object BagValue
		{
			get
			{
				return this.bagValue;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000932F File Offset: 0x0000832F
		public Asn1Set BagAttributes
		{
			get
			{
				return this.bagAttributes;
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00009338 File Offset: 0x00008338
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.bagID,
				new DerTaggedObject(0, this.bagValue)
			});
			if (this.bagAttributes != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.bagAttributes
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040000B4 RID: 180
		private readonly DerObjectIdentifier bagID;

		// Token: 0x040000B5 RID: 181
		private readonly Asn1Object bagValue;

		// Token: 0x040000B6 RID: 182
		private readonly Asn1Set bagAttributes;
	}
}
