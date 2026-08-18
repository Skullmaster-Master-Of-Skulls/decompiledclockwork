using System;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020000A2 RID: 162
	public class ContentInfo : Asn1Encodable
	{
		// Token: 0x0600051E RID: 1310 RVA: 0x0001B710 File Offset: 0x0001A710
		public static ContentInfo GetInstance(object obj)
		{
			if (obj is ContentInfo)
			{
				return (ContentInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ContentInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0001B75F File Offset: 0x0001A75F
		private ContentInfo(Asn1Sequence seq)
		{
			this.contentType = (DerObjectIdentifier)seq[0];
			if (seq.Count > 1)
			{
				this.content = ((Asn1TaggedObject)seq[1]).GetObject();
			}
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0001B799 File Offset: 0x0001A799
		public ContentInfo(DerObjectIdentifier contentType, Asn1Encodable content)
		{
			this.contentType = contentType;
			this.content = content;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x0001B7AF File Offset: 0x0001A7AF
		public DerObjectIdentifier ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0001B7B7 File Offset: 0x0001A7B7
		public Asn1Encodable Content
		{
			get
			{
				return this.content;
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0001B7C0 File Offset: 0x0001A7C0
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

		// Token: 0x04000292 RID: 658
		private readonly DerObjectIdentifier contentType;

		// Token: 0x04000293 RID: 659
		private readonly Asn1Encodable content;
	}
}
