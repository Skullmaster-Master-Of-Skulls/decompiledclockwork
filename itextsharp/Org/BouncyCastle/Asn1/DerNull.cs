using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x020002BD RID: 701
	public class DerNull : Asn1Null
	{
		// Token: 0x06001A66 RID: 6758 RVA: 0x0009BF82 File Offset: 0x0009AF82
		[Obsolete("Use static Instance object")]
		public DerNull()
		{
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x0009BF96 File Offset: 0x0009AF96
		protected internal DerNull(int dummy)
		{
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x0009BFAA File Offset: 0x0009AFAA
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(5, this.zeroBytes);
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x0009BFB9 File Offset: 0x0009AFB9
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			return asn1Object is DerNull;
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x0009BFC4 File Offset: 0x0009AFC4
		protected override int Asn1GetHashCode()
		{
			return -1;
		}

		// Token: 0x040011A2 RID: 4514
		public static readonly DerNull Instance = new DerNull(0);

		// Token: 0x040011A3 RID: 4515
		private byte[] zeroBytes = new byte[0];
	}
}
