using System;
using System.IO;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x0200002F RID: 47
	public class SymmetricKeyEncSessionPacket : ContainedPacket
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00008A67 File Offset: 0x00007A67
		public SymmetricKeyEncSessionPacket(BcpgInputStream bcpgIn)
		{
			this.version = bcpgIn.ReadByte();
			this.encAlgorithm = (SymmetricKeyAlgorithmTag)bcpgIn.ReadByte();
			this.s2k = new S2k(bcpgIn);
			this.secKeyData = bcpgIn.ReadAll();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00008A9F File Offset: 0x00007A9F
		public SymmetricKeyEncSessionPacket(SymmetricKeyAlgorithmTag encAlgorithm, S2k s2k, byte[] secKeyData)
		{
			this.version = 4;
			this.encAlgorithm = encAlgorithm;
			this.s2k = s2k;
			this.secKeyData = secKeyData;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00008AC3 File Offset: 0x00007AC3
		public SymmetricKeyAlgorithmTag EncAlgorithm
		{
			get
			{
				return this.encAlgorithm;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00008ACB File Offset: 0x00007ACB
		public S2k S2k
		{
			get
			{
				return this.s2k;
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00008AD3 File Offset: 0x00007AD3
		public byte[] GetSecKeyData()
		{
			return this.secKeyData;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00008ADB File Offset: 0x00007ADB
		public int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00008AE4 File Offset: 0x00007AE4
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			MemoryStream memoryStream = new MemoryStream();
			BcpgOutputStream bcpgOutputStream = new BcpgOutputStream(memoryStream);
			bcpgOutputStream.Write(new byte[]
			{
				(byte)this.version,
				(byte)this.encAlgorithm
			});
			bcpgOutputStream.WriteObject(this.s2k);
			if (this.secKeyData != null && this.secKeyData.Length > 0)
			{
				bcpgOutputStream.Write(this.secKeyData);
			}
			bcpgOut.WritePacket(PacketTag.SymmetricKeyEncryptedSessionKey, memoryStream.ToArray(), true);
		}

		// Token: 0x040000A0 RID: 160
		private int version;

		// Token: 0x040000A1 RID: 161
		private SymmetricKeyAlgorithmTag encAlgorithm;

		// Token: 0x040000A2 RID: 162
		private S2k s2k;

		// Token: 0x040000A3 RID: 163
		private readonly byte[] secKeyData;
	}
}
