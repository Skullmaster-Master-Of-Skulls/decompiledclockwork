using System;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x0200047E RID: 1150
	public class SymmetricEncIntegrityPacket : InputStreamPacket
	{
		// Token: 0x0600270C RID: 9996 RVA: 0x000EC865 File Offset: 0x000EB865
		internal SymmetricEncIntegrityPacket(BcpgInputStream bcpgIn) : base(bcpgIn)
		{
			this.version = bcpgIn.ReadByte();
		}

		// Token: 0x04001AD8 RID: 6872
		internal readonly int version;
	}
}
