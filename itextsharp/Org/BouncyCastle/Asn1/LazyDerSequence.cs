using System;
using System.Collections;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x020000A8 RID: 168
	internal class LazyDerSequence : DerSequence
	{
		// Token: 0x06000547 RID: 1351 RVA: 0x0001BE2C File Offset: 0x0001AE2C
		internal LazyDerSequence(byte[] encoded)
		{
			this.encoded = encoded;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001BE3C File Offset: 0x0001AE3C
		private void Parse()
		{
			Asn1InputStream asn1InputStream = new LazyAsn1InputStream(this.encoded);
			Asn1Object obj;
			while ((obj = asn1InputStream.ReadObject()) != null)
			{
				base.AddObject(obj);
			}
			this.encoded = null;
			this.parsed = true;
		}

		// Token: 0x170000EF RID: 239
		public override Asn1Encodable this[int index]
		{
			get
			{
				if (!this.parsed)
				{
					this.Parse();
				}
				return base[index];
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001BE8D File Offset: 0x0001AE8D
		public override IEnumerator GetEnumerator()
		{
			if (!this.parsed)
			{
				this.Parse();
			}
			return base.GetEnumerator();
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x0001BEA3 File Offset: 0x0001AEA3
		public override int Count
		{
			get
			{
				if (!this.parsed)
				{
					this.Parse();
				}
				return base.Count;
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001BEB9 File Offset: 0x0001AEB9
		internal override void Encode(DerOutputStream derOut)
		{
			if (this.parsed)
			{
				base.Encode(derOut);
				return;
			}
			derOut.WriteEncoded(48, this.encoded);
		}

		// Token: 0x0400029F RID: 671
		private byte[] encoded;

		// Token: 0x040002A0 RID: 672
		private bool parsed;
	}
}
