using System;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x0200047F RID: 1151
	public class MarkerPacket : ContainedPacket
	{
		// Token: 0x0600270D RID: 9997 RVA: 0x000EC883 File Offset: 0x000EB883
		public MarkerPacket(BcpgInputStream bcpgIn)
		{
			bcpgIn.ReadFully(this.marker);
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x000EC8AE File Offset: 0x000EB8AE
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WritePacket(PacketTag.Marker, this.marker, true);
		}

		// Token: 0x04001AD9 RID: 6873
		private byte[] marker = new byte[]
		{
			80,
			71,
			80
		};
	}
}
