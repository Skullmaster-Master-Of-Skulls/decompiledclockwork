using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x020000A7 RID: 167
	public class DerSequence : Asn1Sequence
	{
		// Token: 0x06000540 RID: 1344 RVA: 0x0001BCDB File Offset: 0x0001ACDB
		public static DerSequence FromVector(Asn1EncodableVector v)
		{
			if (v.Count >= 1)
			{
				return new DerSequence(v);
			}
			return DerSequence.Empty;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001BCF2 File Offset: 0x0001ACF2
		public DerSequence() : base(0)
		{
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0001BCFB File Offset: 0x0001ACFB
		public DerSequence(Asn1Encodable obj) : base(1)
		{
			base.AddObject(obj);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001BD0C File Offset: 0x0001AD0C
		public DerSequence(params Asn1Encodable[] v) : base(v.Length)
		{
			foreach (Asn1Encodable obj in v)
			{
				base.AddObject(obj);
			}
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001BD40 File Offset: 0x0001AD40
		public DerSequence(Asn1EncodableVector v) : base(v.Count)
		{
			foreach (object obj in v)
			{
				Asn1Encodable obj2 = (Asn1Encodable)obj;
				base.AddObject(obj2);
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0001BDA0 File Offset: 0x0001ADA0
		internal override void Encode(DerOutputStream derOut)
		{
			MemoryStream memoryStream = new MemoryStream();
			DerOutputStream derOutputStream = new DerOutputStream(memoryStream);
			foreach (object obj in this)
			{
				Asn1Encodable obj2 = (Asn1Encodable)obj;
				derOutputStream.WriteObject(obj2);
			}
			derOutputStream.Close();
			byte[] bytes = memoryStream.ToArray();
			derOut.WriteEncoded(48, bytes);
		}

		// Token: 0x0400029E RID: 670
		public static readonly DerSequence Empty = new DerSequence();
	}
}
