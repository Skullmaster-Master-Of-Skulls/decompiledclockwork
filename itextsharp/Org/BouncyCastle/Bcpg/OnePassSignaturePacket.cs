using System;
using System.IO;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x0200035D RID: 861
	public class OnePassSignaturePacket : ContainedPacket
	{
		// Token: 0x06001EDA RID: 7898 RVA: 0x000B9F4C File Offset: 0x000B8F4C
		internal OnePassSignaturePacket(BcpgInputStream bcpgIn)
		{
			this.version = bcpgIn.ReadByte();
			this.sigType = bcpgIn.ReadByte();
			this.hashAlgorithm = (HashAlgorithmTag)bcpgIn.ReadByte();
			this.keyAlgorithm = (PublicKeyAlgorithmTag)bcpgIn.ReadByte();
			this.keyId |= (long)bcpgIn.ReadByte() << 56;
			this.keyId |= (long)bcpgIn.ReadByte() << 48;
			this.keyId |= (long)bcpgIn.ReadByte() << 40;
			this.keyId |= (long)bcpgIn.ReadByte() << 32;
			this.keyId |= (long)bcpgIn.ReadByte() << 24;
			this.keyId |= (long)bcpgIn.ReadByte() << 16;
			this.keyId |= (long)bcpgIn.ReadByte() << 8;
			this.keyId |= (long)((ulong)bcpgIn.ReadByte());
			this.nested = bcpgIn.ReadByte();
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x000BA04F File Offset: 0x000B904F
		public OnePassSignaturePacket(int sigType, HashAlgorithmTag hashAlgorithm, PublicKeyAlgorithmTag keyAlgorithm, long keyId, bool isNested)
		{
			this.version = 3;
			this.sigType = sigType;
			this.hashAlgorithm = hashAlgorithm;
			this.keyAlgorithm = keyAlgorithm;
			this.keyId = keyId;
			this.nested = (isNested ? 0 : 1);
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001EDC RID: 7900 RVA: 0x000BA089 File Offset: 0x000B9089
		public int SignatureType
		{
			get
			{
				return this.sigType;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001EDD RID: 7901 RVA: 0x000BA091 File Offset: 0x000B9091
		public PublicKeyAlgorithmTag KeyAlgorithm
		{
			get
			{
				return this.keyAlgorithm;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001EDE RID: 7902 RVA: 0x000BA099 File Offset: 0x000B9099
		public HashAlgorithmTag HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001EDF RID: 7903 RVA: 0x000BA0A1 File Offset: 0x000B90A1
		public long KeyId
		{
			get
			{
				return this.keyId;
			}
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x000BA0AC File Offset: 0x000B90AC
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			MemoryStream memoryStream = new MemoryStream();
			BcpgOutputStream bcpgOutputStream = new BcpgOutputStream(memoryStream);
			bcpgOutputStream.Write(new byte[]
			{
				(byte)this.version,
				(byte)this.sigType,
				(byte)this.hashAlgorithm,
				(byte)this.keyAlgorithm
			});
			bcpgOutputStream.WriteLong(this.keyId);
			bcpgOutputStream.WriteByte((byte)this.nested);
			bcpgOut.WritePacket(PacketTag.OnePassSignature, memoryStream.ToArray(), true);
		}

		// Token: 0x04001558 RID: 5464
		private int version;

		// Token: 0x04001559 RID: 5465
		private int sigType;

		// Token: 0x0400155A RID: 5466
		private HashAlgorithmTag hashAlgorithm;

		// Token: 0x0400155B RID: 5467
		private PublicKeyAlgorithmTag keyAlgorithm;

		// Token: 0x0400155C RID: 5468
		private long keyId;

		// Token: 0x0400155D RID: 5469
		private int nested;
	}
}
