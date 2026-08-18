using System;
using System.Collections;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x020001B6 RID: 438
	internal class LazyDerSet : DerSet
	{
		// Token: 0x06001090 RID: 4240 RVA: 0x0005EC46 File Offset: 0x0005DC46
		internal LazyDerSet(byte[] encoded)
		{
			this.encoded = encoded;
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0005EC58 File Offset: 0x0005DC58
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

		// Token: 0x17000323 RID: 803
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

		// Token: 0x06001093 RID: 4243 RVA: 0x0005ECA9 File Offset: 0x0005DCA9
		public override IEnumerator GetEnumerator()
		{
			if (!this.parsed)
			{
				this.Parse();
			}
			return base.GetEnumerator();
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x0005ECBF File Offset: 0x0005DCBF
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

		// Token: 0x06001095 RID: 4245 RVA: 0x0005ECD5 File Offset: 0x0005DCD5
		internal override void Encode(DerOutputStream derOut)
		{
			if (this.parsed)
			{
				base.Encode(derOut);
				return;
			}
			derOut.WriteEncoded(49, this.encoded);
		}

		// Token: 0x04000C29 RID: 3113
		private byte[] encoded;

		// Token: 0x04000C2A RID: 3114
		private bool parsed;
	}
}
