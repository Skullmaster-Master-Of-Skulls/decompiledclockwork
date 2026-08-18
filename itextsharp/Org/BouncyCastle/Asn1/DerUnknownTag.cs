using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x020005BF RID: 1471
	public class DerUnknownTag : Asn1Object
	{
		// Token: 0x0600328B RID: 12939 RVA: 0x0013953D File Offset: 0x0013853D
		public DerUnknownTag(int tag, byte[] data) : this(false, tag, data)
		{
		}

		// Token: 0x0600328C RID: 12940 RVA: 0x00139548 File Offset: 0x00138548
		public DerUnknownTag(bool isConstructed, int tag, byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this.isConstructed = isConstructed;
			this.tag = tag;
			this.data = data;
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x0600328D RID: 12941 RVA: 0x00139573 File Offset: 0x00138573
		public bool IsConstructed
		{
			get
			{
				return this.isConstructed;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x0600328E RID: 12942 RVA: 0x0013957B File Offset: 0x0013857B
		public int Tag
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x0600328F RID: 12943 RVA: 0x00139583 File Offset: 0x00138583
		public byte[] GetData()
		{
			return this.data;
		}

		// Token: 0x06003290 RID: 12944 RVA: 0x0013958B File Offset: 0x0013858B
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(this.isConstructed ? 32 : 0, this.tag, this.data);
		}

		// Token: 0x06003291 RID: 12945 RVA: 0x001395AC File Offset: 0x001385AC
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerUnknownTag derUnknownTag = asn1Object as DerUnknownTag;
			return derUnknownTag != null && (this.isConstructed == derUnknownTag.isConstructed && this.tag == derUnknownTag.tag) && Arrays.AreEqual(this.data, derUnknownTag.data);
		}

		// Token: 0x06003292 RID: 12946 RVA: 0x001395F4 File Offset: 0x001385F4
		protected override int Asn1GetHashCode()
		{
			return this.isConstructed.GetHashCode() ^ this.tag.GetHashCode() ^ Arrays.GetHashCode(this.data);
		}

		// Token: 0x0400228C RID: 8844
		private readonly bool isConstructed;

		// Token: 0x0400228D RID: 8845
		private readonly int tag;

		// Token: 0x0400228E RID: 8846
		private readonly byte[] data;
	}
}
