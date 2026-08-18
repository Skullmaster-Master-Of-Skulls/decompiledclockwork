using System;
using System.IO;
using Org.BouncyCastle.Utilities.Date;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x0200013C RID: 316
	public class PublicKeyPacket : ContainedPacket
	{
		// Token: 0x06000B80 RID: 2944 RVA: 0x000407E8 File Offset: 0x0003F7E8
		internal PublicKeyPacket(BcpgInputStream bcpgIn)
		{
			this.version = bcpgIn.ReadByte();
			this.time = (long)((ulong)(bcpgIn.ReadByte() << 24 | bcpgIn.ReadByte() << 16 | bcpgIn.ReadByte() << 8 | bcpgIn.ReadByte()));
			if (this.version <= 3)
			{
				this.validDays = (bcpgIn.ReadByte() << 8 | bcpgIn.ReadByte());
			}
			this.algorithm = (PublicKeyAlgorithmTag)bcpgIn.ReadByte();
			PublicKeyAlgorithmTag publicKeyAlgorithmTag = this.algorithm;
			switch (publicKeyAlgorithmTag)
			{
			case PublicKeyAlgorithmTag.RsaGeneral:
			case PublicKeyAlgorithmTag.RsaEncrypt:
			case PublicKeyAlgorithmTag.RsaSign:
				this.key = new RsaPublicBcpgKey(bcpgIn);
				return;
			default:
				switch (publicKeyAlgorithmTag)
				{
				case PublicKeyAlgorithmTag.ElGamalEncrypt:
				case PublicKeyAlgorithmTag.ElGamalGeneral:
					this.key = new ElGamalPublicBcpgKey(bcpgIn);
					return;
				case PublicKeyAlgorithmTag.Dsa:
					this.key = new DsaPublicBcpgKey(bcpgIn);
					return;
				}
				throw new IOException("unknown PGP public key algorithm encountered");
			}
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x000408C6 File Offset: 0x0003F8C6
		public PublicKeyPacket(PublicKeyAlgorithmTag algorithm, DateTime time, IBcpgKey key)
		{
			this.version = 4;
			this.time = DateTimeUtilities.DateTimeToUnixMs(time) / 1000L;
			this.algorithm = algorithm;
			this.key = key;
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x000408F6 File Offset: 0x0003F8F6
		public int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x000408FE File Offset: 0x0003F8FE
		public PublicKeyAlgorithmTag Algorithm
		{
			get
			{
				return this.algorithm;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x00040906 File Offset: 0x0003F906
		public int ValidDays
		{
			get
			{
				return this.validDays;
			}
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0004090E File Offset: 0x0003F90E
		public DateTime GetTime()
		{
			return DateTimeUtilities.UnixMsToDateTime(this.time * 1000L);
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x00040922 File Offset: 0x0003F922
		public IBcpgKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0004092C File Offset: 0x0003F92C
		public byte[] GetEncodedContents()
		{
			MemoryStream memoryStream = new MemoryStream();
			BcpgOutputStream bcpgOutputStream = new BcpgOutputStream(memoryStream);
			bcpgOutputStream.WriteByte((byte)this.version);
			bcpgOutputStream.WriteInt((int)this.time);
			if (this.version <= 3)
			{
				bcpgOutputStream.WriteShort((short)this.validDays);
			}
			bcpgOutputStream.WriteByte((byte)this.algorithm);
			bcpgOutputStream.WriteObject((BcpgObject)this.key);
			return memoryStream.ToArray();
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0004099A File Offset: 0x0003F99A
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WritePacket(PacketTag.PublicKey, this.GetEncodedContents(), true);
		}

		// Token: 0x04000907 RID: 2311
		private int version;

		// Token: 0x04000908 RID: 2312
		private long time;

		// Token: 0x04000909 RID: 2313
		private int validDays;

		// Token: 0x0400090A RID: 2314
		private PublicKeyAlgorithmTag algorithm;

		// Token: 0x0400090B RID: 2315
		private IBcpgKey key;
	}
}
