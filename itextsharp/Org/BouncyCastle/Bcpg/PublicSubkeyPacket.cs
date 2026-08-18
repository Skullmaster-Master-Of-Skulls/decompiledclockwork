using System;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x0200013D RID: 317
	public class PublicSubkeyPacket : PublicKeyPacket
	{
		// Token: 0x06000B89 RID: 2953 RVA: 0x000409AA File Offset: 0x0003F9AA
		internal PublicSubkeyPacket(BcpgInputStream bcpgIn) : base(bcpgIn)
		{
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x000409B3 File Offset: 0x0003F9B3
		public PublicSubkeyPacket(PublicKeyAlgorithmTag algorithm, DateTime time, IBcpgKey key) : base(algorithm, time, key)
		{
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x000409BE File Offset: 0x0003F9BE
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WritePacket(PacketTag.PublicSubkey, base.GetEncodedContents(), true);
		}
	}
}
