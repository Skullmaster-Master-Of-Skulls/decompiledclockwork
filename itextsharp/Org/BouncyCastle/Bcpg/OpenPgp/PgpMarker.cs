using System;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000607 RID: 1543
	public class PgpMarker : PgpObject
	{
		// Token: 0x0600349B RID: 13467 RVA: 0x00147937 File Offset: 0x00146937
		public PgpMarker(BcpgInputStream bcpgIn)
		{
			this.p = (MarkerPacket)bcpgIn.ReadPacket();
		}

		// Token: 0x04002358 RID: 9048
		private readonly MarkerPacket p;
	}
}
