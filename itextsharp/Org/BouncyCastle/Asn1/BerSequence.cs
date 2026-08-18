using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x020002C0 RID: 704
	public class BerSequence : DerSequence
	{
		// Token: 0x06001A76 RID: 6774 RVA: 0x0009C0F7 File Offset: 0x0009B0F7
		public new static BerSequence FromVector(Asn1EncodableVector v)
		{
			if (v.Count >= 1)
			{
				return new BerSequence(v);
			}
			return BerSequence.Empty;
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x0009C10E File Offset: 0x0009B10E
		public BerSequence()
		{
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x0009C116 File Offset: 0x0009B116
		public BerSequence(Asn1Encodable obj) : base(obj)
		{
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x0009C11F File Offset: 0x0009B11F
		public BerSequence(params Asn1Encodable[] v) : base(v)
		{
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x0009C128 File Offset: 0x0009B128
		public BerSequence(Asn1EncodableVector v) : base(v)
		{
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x0009C134 File Offset: 0x0009B134
		internal override void Encode(DerOutputStream derOut)
		{
			if (derOut is Asn1OutputStream || derOut is BerOutputStream)
			{
				derOut.WriteByte(48);
				derOut.WriteByte(128);
				foreach (object obj in this)
				{
					Asn1Encodable obj2 = (Asn1Encodable)obj;
					derOut.WriteObject(obj2);
				}
				derOut.WriteByte(0);
				derOut.WriteByte(0);
				return;
			}
			base.Encode(derOut);
		}

		// Token: 0x040011A8 RID: 4520
		public new static readonly BerSequence Empty = new BerSequence();
	}
}
