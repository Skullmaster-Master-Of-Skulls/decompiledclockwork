using System;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x0200013A RID: 314
	public class SecretKeyPacket : ContainedPacket
	{
		// Token: 0x06000B71 RID: 2929 RVA: 0x00040548 File Offset: 0x0003F548
		internal SecretKeyPacket(BcpgInputStream bcpgIn)
		{
			if (this is SecretSubkeyPacket)
			{
				this.pubKeyPacket = new PublicSubkeyPacket(bcpgIn);
			}
			else
			{
				this.pubKeyPacket = new PublicKeyPacket(bcpgIn);
			}
			this.s2kUsage = bcpgIn.ReadByte();
			if (this.s2kUsage == 255 || this.s2kUsage == 254)
			{
				this.encAlgorithm = (SymmetricKeyAlgorithmTag)bcpgIn.ReadByte();
				this.s2k = new S2k(bcpgIn);
			}
			else
			{
				this.encAlgorithm = (SymmetricKeyAlgorithmTag)this.s2kUsage;
			}
			if ((this.s2k == null || this.s2k.Type != 101 || this.s2k.ProtectionMode != 1) && this.s2kUsage != 0)
			{
				if (this.encAlgorithm < SymmetricKeyAlgorithmTag.Aes128)
				{
					this.iv = new byte[8];
				}
				else
				{
					this.iv = new byte[16];
				}
				bcpgIn.ReadFully(this.iv);
			}
			this.secKeyData = bcpgIn.ReadAll();
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00040634 File Offset: 0x0003F634
		public SecretKeyPacket(PublicKeyPacket pubKeyPacket, SymmetricKeyAlgorithmTag encAlgorithm, S2k s2k, byte[] iv, byte[] secKeyData)
		{
			this.pubKeyPacket = pubKeyPacket;
			this.encAlgorithm = encAlgorithm;
			if (encAlgorithm != SymmetricKeyAlgorithmTag.Null)
			{
				this.s2kUsage = 255;
			}
			else
			{
				this.s2kUsage = 0;
			}
			this.s2k = s2k;
			this.iv = Arrays.Clone(iv);
			this.secKeyData = secKeyData;
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00040688 File Offset: 0x0003F688
		public SecretKeyPacket(PublicKeyPacket pubKeyPacket, SymmetricKeyAlgorithmTag encAlgorithm, int s2kUsage, S2k s2k, byte[] iv, byte[] secKeyData)
		{
			this.pubKeyPacket = pubKeyPacket;
			this.encAlgorithm = encAlgorithm;
			this.s2kUsage = s2kUsage;
			this.s2k = s2k;
			this.iv = Arrays.Clone(iv);
			this.secKeyData = secKeyData;
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x000406C2 File Offset: 0x0003F6C2
		public SymmetricKeyAlgorithmTag EncAlgorithm
		{
			get
			{
				return this.encAlgorithm;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x000406CA File Offset: 0x0003F6CA
		public int S2kUsage
		{
			get
			{
				return this.s2kUsage;
			}
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x000406D2 File Offset: 0x0003F6D2
		public byte[] GetIV()
		{
			return Arrays.Clone(this.iv);
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x000406DF File Offset: 0x0003F6DF
		public S2k S2k
		{
			get
			{
				return this.s2k;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x000406E7 File Offset: 0x0003F6E7
		public PublicKeyPacket PublicKeyPacket
		{
			get
			{
				return this.pubKeyPacket;
			}
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x000406EF File Offset: 0x0003F6EF
		public byte[] GetSecretKeyData()
		{
			return this.secKeyData;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x000406F8 File Offset: 0x0003F6F8
		public byte[] GetEncodedContents()
		{
			MemoryStream memoryStream = new MemoryStream();
			BcpgOutputStream bcpgOutputStream = new BcpgOutputStream(memoryStream);
			bcpgOutputStream.Write(this.pubKeyPacket.GetEncodedContents());
			bcpgOutputStream.WriteByte((byte)this.s2kUsage);
			if (this.s2kUsage == 255 || this.s2kUsage == 254)
			{
				bcpgOutputStream.WriteByte((byte)this.encAlgorithm);
				bcpgOutputStream.WriteObject(this.s2k);
			}
			if (this.iv != null)
			{
				bcpgOutputStream.Write(this.iv);
			}
			if (this.secKeyData != null && this.secKeyData.Length > 0)
			{
				bcpgOutputStream.Write(this.secKeyData);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0004079C File Offset: 0x0003F79C
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WritePacket(PacketTag.SecretKey, this.GetEncodedContents(), true);
		}

		// Token: 0x040008FE RID: 2302
		public const int UsageNone = 0;

		// Token: 0x040008FF RID: 2303
		public const int UsageChecksum = 255;

		// Token: 0x04000900 RID: 2304
		public const int UsageSha1 = 254;

		// Token: 0x04000901 RID: 2305
		private PublicKeyPacket pubKeyPacket;

		// Token: 0x04000902 RID: 2306
		private readonly byte[] secKeyData;

		// Token: 0x04000903 RID: 2307
		private int s2kUsage;

		// Token: 0x04000904 RID: 2308
		private SymmetricKeyAlgorithmTag encAlgorithm;

		// Token: 0x04000905 RID: 2309
		private S2k s2k;

		// Token: 0x04000906 RID: 2310
		private byte[] iv;
	}
}
