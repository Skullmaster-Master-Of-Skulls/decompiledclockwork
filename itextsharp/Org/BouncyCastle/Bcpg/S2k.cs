using System;
using System.IO;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x02000034 RID: 52
	public class S2k : BcpgObject
	{
		// Token: 0x0600015C RID: 348 RVA: 0x00008D3C File Offset: 0x00007D3C
		internal S2k(Stream inStr)
		{
			this.type = inStr.ReadByte();
			this.algorithm = (HashAlgorithmTag)inStr.ReadByte();
			if (this.type != 101)
			{
				if (this.type != 0)
				{
					this.iv = new byte[8];
					if (Streams.ReadFully(inStr, this.iv, 0, this.iv.Length) < this.iv.Length)
					{
						throw new EndOfStreamException();
					}
					if (this.type == 3)
					{
						this.itCount = inStr.ReadByte();
						return;
					}
				}
			}
			else
			{
				inStr.ReadByte();
				inStr.ReadByte();
				inStr.ReadByte();
				this.protectionMode = inStr.ReadByte();
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00008DEF File Offset: 0x00007DEF
		public S2k(HashAlgorithmTag algorithm)
		{
			this.type = 0;
			this.algorithm = algorithm;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00008E13 File Offset: 0x00007E13
		public S2k(HashAlgorithmTag algorithm, byte[] iv)
		{
			this.type = 1;
			this.algorithm = algorithm;
			this.iv = iv;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00008E3E File Offset: 0x00007E3E
		public S2k(HashAlgorithmTag algorithm, byte[] iv, int itCount)
		{
			this.type = 3;
			this.algorithm = algorithm;
			this.iv = iv;
			this.itCount = itCount;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00008E70 File Offset: 0x00007E70
		public int Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00008E78 File Offset: 0x00007E78
		public HashAlgorithmTag HashAlgorithm
		{
			get
			{
				return this.algorithm;
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00008E80 File Offset: 0x00007E80
		public byte[] GetIV()
		{
			return Arrays.Clone(this.iv);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00008E8D File Offset: 0x00007E8D
		[Obsolete("Use 'IterationCount' property instead")]
		public long GetIterationCount()
		{
			return this.IterationCount;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00008E95 File Offset: 0x00007E95
		public long IterationCount
		{
			get
			{
				return (long)((long)(16 + (this.itCount & 15)) << (this.itCount >> 4) + 6);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00008EB2 File Offset: 0x00007EB2
		public int ProtectionMode
		{
			get
			{
				return this.protectionMode;
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00008EBC File Offset: 0x00007EBC
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WriteByte((byte)this.type);
			bcpgOut.WriteByte((byte)this.algorithm);
			if (this.type != 101)
			{
				if (this.type != 0)
				{
					bcpgOut.Write(this.iv);
				}
				if (this.type == 3)
				{
					bcpgOut.WriteByte((byte)this.itCount);
					return;
				}
			}
			else
			{
				bcpgOut.WriteByte(71);
				bcpgOut.WriteByte(78);
				bcpgOut.WriteByte(85);
				bcpgOut.WriteByte((byte)this.protectionMode);
			}
		}

		// Token: 0x040000A7 RID: 167
		private const int ExpBias = 6;

		// Token: 0x040000A8 RID: 168
		public const int Simple = 0;

		// Token: 0x040000A9 RID: 169
		public const int Salted = 1;

		// Token: 0x040000AA RID: 170
		public const int SaltedAndIterated = 3;

		// Token: 0x040000AB RID: 171
		public const int GnuDummyS2K = 101;

		// Token: 0x040000AC RID: 172
		internal int type;

		// Token: 0x040000AD RID: 173
		internal HashAlgorithmTag algorithm;

		// Token: 0x040000AE RID: 174
		internal byte[] iv;

		// Token: 0x040000AF RID: 175
		internal int itCount = -1;

		// Token: 0x040000B0 RID: 176
		internal int protectionMode = -1;
	}
}
