using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000523 RID: 1315
	public class BerNull : DerNull
	{
		// Token: 0x06002CD1 RID: 11473 RVA: 0x001101D8 File Offset: 0x0010F1D8
		[Obsolete("Use static Instance object")]
		public BerNull()
		{
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x001101E0 File Offset: 0x0010F1E0
		private BerNull(int dummy) : base(dummy)
		{
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x001101E9 File Offset: 0x0010F1E9
		internal override void Encode(DerOutputStream derOut)
		{
			if (derOut is Asn1OutputStream || derOut is BerOutputStream)
			{
				derOut.WriteByte(5);
				return;
			}
			base.Encode(derOut);
		}

		// Token: 0x04001ED3 RID: 7891
		public new static readonly BerNull Instance = new BerNull(0);
	}
}
