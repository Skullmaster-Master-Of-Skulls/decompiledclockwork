using System;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x0200013B RID: 315
	public class SecretSubkeyPacket : SecretKeyPacket
	{
		// Token: 0x06000B7C RID: 2940 RVA: 0x000407AC File Offset: 0x0003F7AC
		internal SecretSubkeyPacket(BcpgInputStream bcpgIn) : base(bcpgIn)
		{
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x000407B5 File Offset: 0x0003F7B5
		public SecretSubkeyPacket(PublicKeyPacket pubKeyPacket, SymmetricKeyAlgorithmTag encAlgorithm, S2k s2k, byte[] iv, byte[] secKeyData) : base(pubKeyPacket, encAlgorithm, s2k, iv, secKeyData)
		{
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x000407C4 File Offset: 0x0003F7C4
		public SecretSubkeyPacket(PublicKeyPacket pubKeyPacket, SymmetricKeyAlgorithmTag encAlgorithm, int s2kUsage, S2k s2k, byte[] iv, byte[] secKeyData) : base(pubKeyPacket, encAlgorithm, s2kUsage, s2k, iv, secKeyData)
		{
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x000407D5 File Offset: 0x0003F7D5
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WritePacket(PacketTag.SecretSubkey, base.GetEncodedContents(), true);
		}
	}
}
