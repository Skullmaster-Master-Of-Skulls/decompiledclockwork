using System;
using System.IO;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x02000441 RID: 1089
	public class PublicKeyEncSessionPacket : ContainedPacket
	{
		// Token: 0x060024EE RID: 9454 RVA: 0x000E05F0 File Offset: 0x000DF5F0
		internal PublicKeyEncSessionPacket(BcpgInputStream bcpgIn)
		{
			this.version = bcpgIn.ReadByte();
			this.keyId |= (long)bcpgIn.ReadByte() << 56;
			this.keyId |= (long)bcpgIn.ReadByte() << 48;
			this.keyId |= (long)bcpgIn.ReadByte() << 40;
			this.keyId |= (long)bcpgIn.ReadByte() << 32;
			this.keyId |= (long)bcpgIn.ReadByte() << 24;
			this.keyId |= (long)bcpgIn.ReadByte() << 16;
			this.keyId |= (long)bcpgIn.ReadByte() << 8;
			this.keyId |= (long)((ulong)bcpgIn.ReadByte());
			this.algorithm = (PublicKeyAlgorithmTag)bcpgIn.ReadByte();
			PublicKeyAlgorithmTag publicKeyAlgorithmTag = this.algorithm;
			switch (publicKeyAlgorithmTag)
			{
			case PublicKeyAlgorithmTag.RsaGeneral:
			case PublicKeyAlgorithmTag.RsaEncrypt:
				this.data = new BigInteger[]
				{
					new MPInteger(bcpgIn).Value
				};
				return;
			default:
				if (publicKeyAlgorithmTag != PublicKeyAlgorithmTag.ElGamalEncrypt && publicKeyAlgorithmTag != PublicKeyAlgorithmTag.ElGamalGeneral)
				{
					throw new IOException("unknown PGP public key algorithm encountered");
				}
				this.data = new BigInteger[]
				{
					new MPInteger(bcpgIn).Value,
					new MPInteger(bcpgIn).Value
				};
				return;
			}
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x000E0744 File Offset: 0x000DF744
		public PublicKeyEncSessionPacket(long keyId, PublicKeyAlgorithmTag algorithm, BigInteger[] data)
		{
			this.version = 3;
			this.keyId = keyId;
			this.algorithm = algorithm;
			this.data = (BigInteger[])data.Clone();
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060024F0 RID: 9456 RVA: 0x000E0772 File Offset: 0x000DF772
		public int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060024F1 RID: 9457 RVA: 0x000E077A File Offset: 0x000DF77A
		public long KeyId
		{
			get
			{
				return this.keyId;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060024F2 RID: 9458 RVA: 0x000E0782 File Offset: 0x000DF782
		public PublicKeyAlgorithmTag Algorithm
		{
			get
			{
				return this.algorithm;
			}
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x000E078A File Offset: 0x000DF78A
		public BigInteger[] GetEncSessionKey()
		{
			return (BigInteger[])this.data.Clone();
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x000E079C File Offset: 0x000DF79C
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			MemoryStream memoryStream = new MemoryStream();
			BcpgOutputStream bcpgOutputStream = new BcpgOutputStream(memoryStream);
			bcpgOutputStream.WriteByte((byte)this.version);
			bcpgOutputStream.WriteLong(this.keyId);
			bcpgOutputStream.WriteByte((byte)this.algorithm);
			for (int num = 0; num != this.data.Length; num++)
			{
				MPInteger.Encode(bcpgOutputStream, this.data[num]);
			}
			bcpgOut.WritePacket(PacketTag.PublicKeyEncryptedSession, memoryStream.ToArray(), true);
		}

		// Token: 0x040019B6 RID: 6582
		private int version;

		// Token: 0x040019B7 RID: 6583
		private long keyId;

		// Token: 0x040019B8 RID: 6584
		private PublicKeyAlgorithmTag algorithm;

		// Token: 0x040019B9 RID: 6585
		private BigInteger[] data;
	}
}
