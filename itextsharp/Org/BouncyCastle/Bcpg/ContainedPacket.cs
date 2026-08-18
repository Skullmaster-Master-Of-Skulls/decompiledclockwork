using System;
using System.IO;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x0200002D RID: 45
	public abstract class ContainedPacket : Packet
	{
		// Token: 0x0600013D RID: 317 RVA: 0x00008970 File Offset: 0x00007970
		public byte[] GetEncoded()
		{
			MemoryStream memoryStream = new MemoryStream();
			BcpgOutputStream bcpgOutputStream = new BcpgOutputStream(memoryStream);
			bcpgOutputStream.WritePacket(this);
			return memoryStream.ToArray();
		}

		// Token: 0x0600013E RID: 318
		public abstract void Encode(BcpgOutputStream bcpgOut);
	}
}
