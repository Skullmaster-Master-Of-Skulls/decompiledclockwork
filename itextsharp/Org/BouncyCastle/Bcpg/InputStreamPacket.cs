using System;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x0200009B RID: 155
	public class InputStreamPacket : Packet
	{
		// Token: 0x060004E7 RID: 1255 RVA: 0x0001AAA4 File Offset: 0x00019AA4
		public InputStreamPacket(BcpgInputStream bcpgIn)
		{
			this.bcpgIn = bcpgIn;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0001AAB3 File Offset: 0x00019AB3
		public BcpgInputStream GetInputStream()
		{
			return this.bcpgIn;
		}

		// Token: 0x04000281 RID: 641
		private readonly BcpgInputStream bcpgIn;
	}
}
