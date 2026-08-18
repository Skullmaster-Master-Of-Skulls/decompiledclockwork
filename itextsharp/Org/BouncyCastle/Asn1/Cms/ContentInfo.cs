using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000319 RID: 793
	public class ContentInfo : Asn1Encodable
	{
		// Token: 0x06001CD8 RID: 7384 RVA: 0x000ABDF4 File Offset: 0x000AADF4
		public static ContentInfo GetInstance(object obj)
		{
			if (obj == null || obj is ContentInfo)
			{
				return (ContentInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ContentInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name);
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x000ABE44 File Offset: 0x000AAE44
		private ContentInfo(Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.contentType = (DerObjectIdentifier)seq[0];
			if (seq.Count > 1)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)seq[1];
				if (asn1TaggedObject.TagNo != 0)
				{
					throw new ArgumentException("Tag number for 'content' must be 0");
				}
				this.content = asn1TaggedObject.GetObject();
			}
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x000ABED0 File Offset: 0x000AAED0
		public ContentInfo(DerObjectIdentifier contentType, Asn1Encodable content)
		{
			this.contentType = contentType;
			this.content = content;
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001CDB RID: 7387 RVA: 0x000ABEE6 File Offset: 0x000AAEE6
		public DerObjectIdentifier ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001CDC RID: 7388 RVA: 0x000ABEEE File Offset: 0x000AAEEE
		public Asn1Encodable Content
		{
			get
			{
				return this.content;
			}
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x000ABEF8 File Offset: 0x000AAEF8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.contentType
			});
			if (this.content != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new BerTaggedObject(0, this.content)
				});
			}
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x040013DE RID: 5086
		private readonly DerObjectIdentifier contentType;

		// Token: 0x040013DF RID: 5087
		private readonly Asn1Encodable content;
	}
}
