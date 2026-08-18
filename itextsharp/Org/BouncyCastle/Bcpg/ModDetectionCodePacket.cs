using System;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x02000099 RID: 153
	public class ModDetectionCodePacket : ContainedPacket
	{
		// Token: 0x060004E3 RID: 1251 RVA: 0x0001AA2B File Offset: 0x00019A2B
		internal ModDetectionCodePacket(BcpgInputStream bcpgIn)
		{
			if (bcpgIn == null)
			{
				throw new ArgumentNullException("bcpgIn");
			}
			this.digest = new byte[20];
			bcpgIn.ReadFully(this.digest);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0001AA5A File Offset: 0x00019A5A
		public ModDetectionCodePacket(byte[] digest)
		{
			if (digest == null)
			{
				throw new ArgumentNullException("digest");
			}
			this.digest = (byte[])digest.Clone();
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0001AA81 File Offset: 0x00019A81
		public byte[] GetDigest()
		{
			return (byte[])this.digest.Clone();
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0001AA93 File Offset: 0x00019A93
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WritePacket(PacketTag.ModificationDetectionCode, this.digest, false);
		}

		// Token: 0x04000274 RID: 628
		private readonly byte[] digest;
	}
}
