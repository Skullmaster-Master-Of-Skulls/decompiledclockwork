using System;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200010B RID: 267
	public class PgpExperimental : PgpObject
	{
		// Token: 0x06000A5A RID: 2650 RVA: 0x00037021 File Offset: 0x00036021
		public PgpExperimental(BcpgInputStream bcpgIn)
		{
			this.p = (ExperimentalPacket)bcpgIn.ReadPacket();
		}

		// Token: 0x04000856 RID: 2134
		private readonly ExperimentalPacket p;
	}
}
