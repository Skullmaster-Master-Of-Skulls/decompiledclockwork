using System;
using Org.BouncyCastle.Math.EC;

namespace Org.BouncyCastle.Asn1.X9
{
	// Token: 0x020001A5 RID: 421
	public class X9ECPoint : Asn1Encodable
	{
		// Token: 0x0600101A RID: 4122 RVA: 0x0005D4EC File Offset: 0x0005C4EC
		public X9ECPoint(ECPoint p)
		{
			this.p = p;
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0005D4FB File Offset: 0x0005C4FB
		public X9ECPoint(ECCurve c, Asn1OctetString s)
		{
			this.p = c.DecodePoint(s.GetOctets());
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x0005D515 File Offset: 0x0005C515
		public ECPoint Point
		{
			get
			{
				return this.p;
			}
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0005D51D File Offset: 0x0005C51D
		public override Asn1Object ToAsn1Object()
		{
			return new DerOctetString(this.p.GetEncoded());
		}

		// Token: 0x04000BDF RID: 3039
		private readonly ECPoint p;
	}
}
