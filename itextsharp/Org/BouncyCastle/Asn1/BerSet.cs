using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200031C RID: 796
	public class BerSet : DerSet
	{
		// Token: 0x06001CE9 RID: 7401 RVA: 0x000AC1C2 File Offset: 0x000AB1C2
		public new static BerSet FromVector(Asn1EncodableVector v)
		{
			if (v.Count >= 1)
			{
				return new BerSet(v);
			}
			return BerSet.Empty;
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x000AC1D9 File Offset: 0x000AB1D9
		internal new static BerSet FromVector(Asn1EncodableVector v, bool needsSorting)
		{
			if (v.Count >= 1)
			{
				return new BerSet(v, needsSorting);
			}
			return BerSet.Empty;
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x000AC1F1 File Offset: 0x000AB1F1
		public BerSet()
		{
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x000AC1F9 File Offset: 0x000AB1F9
		public BerSet(Asn1Encodable obj) : base(obj)
		{
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x000AC202 File Offset: 0x000AB202
		public BerSet(Asn1EncodableVector v) : base(v, false)
		{
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x000AC20C File Offset: 0x000AB20C
		internal BerSet(Asn1EncodableVector v, bool needsSorting) : base(v, needsSorting)
		{
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x000AC218 File Offset: 0x000AB218
		internal override void Encode(DerOutputStream derOut)
		{
			if (derOut is Asn1OutputStream || derOut is BerOutputStream)
			{
				derOut.WriteByte(49);
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

		// Token: 0x040013F3 RID: 5107
		public new static readonly BerSet Empty = new BerSet();
	}
}
