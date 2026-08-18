using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000044 RID: 68
	public class DerSet : Asn1Set
	{
		// Token: 0x060001BD RID: 445 RVA: 0x00009BAF File Offset: 0x00008BAF
		public static DerSet FromVector(Asn1EncodableVector v)
		{
			if (v.Count >= 1)
			{
				return new DerSet(v);
			}
			return DerSet.Empty;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00009BC6 File Offset: 0x00008BC6
		internal static DerSet FromVector(Asn1EncodableVector v, bool needsSorting)
		{
			if (v.Count >= 1)
			{
				return new DerSet(v, needsSorting);
			}
			return DerSet.Empty;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00009BDE File Offset: 0x00008BDE
		public DerSet() : base(0)
		{
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00009BE7 File Offset: 0x00008BE7
		public DerSet(Asn1Encodable obj) : base(1)
		{
			base.AddObject(obj);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00009BF8 File Offset: 0x00008BF8
		public DerSet(params Asn1Encodable[] v) : base(v.Length)
		{
			foreach (Asn1Encodable obj in v)
			{
				base.AddObject(obj);
			}
			base.Sort();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00009C2F File Offset: 0x00008C2F
		public DerSet(Asn1EncodableVector v) : this(v, true)
		{
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00009C3C File Offset: 0x00008C3C
		internal DerSet(Asn1EncodableVector v, bool needsSorting) : base(v.Count)
		{
			foreach (object obj in v)
			{
				Asn1Encodable obj2 = (Asn1Encodable)obj;
				base.AddObject(obj2);
			}
			if (needsSorting)
			{
				base.Sort();
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00009CA8 File Offset: 0x00008CA8
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
			derOut.WriteEncoded(49, bytes);
		}

		// Token: 0x040000D0 RID: 208
		public static readonly DerSet Empty = new DerSet();
	}
}
