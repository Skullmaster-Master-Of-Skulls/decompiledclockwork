using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000153 RID: 339
	public class DerTaggedObject : Asn1TaggedObject
	{
		// Token: 0x06000C25 RID: 3109 RVA: 0x00042F21 File Offset: 0x00041F21
		public DerTaggedObject(int tagNo, Asn1Encodable obj) : base(tagNo, obj)
		{
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00042F2B File Offset: 0x00041F2B
		public DerTaggedObject(bool explicitly, int tagNo, Asn1Encodable obj) : base(explicitly, tagNo, obj)
		{
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00042F36 File Offset: 0x00041F36
		public DerTaggedObject(int tagNo) : base(false, tagNo, DerSequence.Empty)
		{
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00042F48 File Offset: 0x00041F48
		internal override void Encode(DerOutputStream derOut)
		{
			if (base.IsEmpty())
			{
				derOut.WriteEncoded(160, this.tagNo, new byte[0]);
				return;
			}
			byte[] derEncoded = this.obj.GetDerEncoded();
			if (this.explicitly)
			{
				derOut.WriteEncoded(160, this.tagNo, derEncoded);
				return;
			}
			int flags = (int)((derEncoded[0] & 32) | 128);
			derOut.WriteTag(flags, this.tagNo);
			derOut.Write(derEncoded, 1, derEncoded.Length - 1);
		}
	}
}
