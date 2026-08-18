using System;
using System.IO;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x02000138 RID: 312
	public class TrustPacket : ContainedPacket
	{
		// Token: 0x06000B66 RID: 2918 RVA: 0x0004030C File Offset: 0x0003F30C
		public TrustPacket(BcpgInputStream bcpgIn)
		{
			MemoryStream memoryStream = new MemoryStream();
			int num;
			while ((num = bcpgIn.ReadByte()) >= 0)
			{
				memoryStream.WriteByte((byte)num);
			}
			this.levelAndTrustAmount = memoryStream.ToArray();
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00040348 File Offset: 0x0003F348
		public TrustPacket(int trustCode)
		{
			this.levelAndTrustAmount = new byte[]
			{
				(byte)trustCode
			};
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0004036E File Offset: 0x0003F36E
		public byte[] GetLevelAndTrustAmount()
		{
			return (byte[])this.levelAndTrustAmount.Clone();
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00040380 File Offset: 0x0003F380
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WritePacket(PacketTag.Trust, this.levelAndTrustAmount, true);
		}

		// Token: 0x040008FA RID: 2298
		private readonly byte[] levelAndTrustAmount;
	}
}
