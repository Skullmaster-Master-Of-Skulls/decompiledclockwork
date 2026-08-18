using System;
using System.Security.Cryptography;

namespace Telerik.Web.Cryptography
{
	// Token: 0x02001848 RID: 6216
	internal sealed class Crc32 : HashAlgorithm
	{
		// Token: 0x0600F15A RID: 61786 RVA: 0x0036DBC8 File Offset: 0x0036BDC8
		public Crc32()
		{
			this.table = Crc32.InitializeTable(3988292384U);
			this.seed = uint.MaxValue;
			this.private_init();
		}

		// Token: 0x0600F15B RID: 61787 RVA: 0x0036DBED File Offset: 0x0036BDED
		public Crc32(uint polynomial, uint seed)
		{
			this.table = Crc32.InitializeTable(polynomial);
			this.seed = seed;
			this.private_init();
		}

		// Token: 0x0600F15C RID: 61788 RVA: 0x0036DC0E File Offset: 0x0036BE0E
		public override void Initialize()
		{
			this.private_init();
		}

		// Token: 0x0600F15D RID: 61789 RVA: 0x0036DC16 File Offset: 0x0036BE16
		private void private_init()
		{
			this.hash = this.seed;
		}

		// Token: 0x0600F15E RID: 61790 RVA: 0x0036DC24 File Offset: 0x0036BE24
		protected override void HashCore(byte[] buffer, int start, int length)
		{
			this.hash = Crc32.CalculateHash(this.table, this.hash, buffer, start, length);
		}

		// Token: 0x0600F15F RID: 61791 RVA: 0x0036DC40 File Offset: 0x0036BE40
		protected override byte[] HashFinal()
		{
			byte[] array = this.UInt32ToBigEndianBytes(~this.hash);
			this.HashValue = array;
			return array;
		}

		// Token: 0x170048E9 RID: 18665
		// (get) Token: 0x0600F160 RID: 61792 RVA: 0x0036DC63 File Offset: 0x0036BE63
		public override int HashSize
		{
			get
			{
				return 32;
			}
		}

		// Token: 0x0600F161 RID: 61793 RVA: 0x0036DC67 File Offset: 0x0036BE67
		public static uint Compute(byte[] buffer)
		{
			return ~Crc32.CalculateHash(Crc32.InitializeTable(3988292384U), uint.MaxValue, buffer, 0, buffer.Length);
		}

		// Token: 0x0600F162 RID: 61794 RVA: 0x0036DC7F File Offset: 0x0036BE7F
		public static uint Compute(uint seed, byte[] buffer)
		{
			return ~Crc32.CalculateHash(Crc32.InitializeTable(3988292384U), seed, buffer, 0, buffer.Length);
		}

		// Token: 0x0600F163 RID: 61795 RVA: 0x0036DC97 File Offset: 0x0036BE97
		public static uint Compute(uint polynomial, uint seed, byte[] buffer)
		{
			return ~Crc32.CalculateHash(Crc32.InitializeTable(polynomial), seed, buffer, 0, buffer.Length);
		}

		// Token: 0x0600F164 RID: 61796 RVA: 0x0036DCAC File Offset: 0x0036BEAC
		private static uint[] InitializeTable(uint polynomial)
		{
			if (polynomial == 3988292384U && Crc32.defaultTable != null)
			{
				return Crc32.defaultTable;
			}
			uint[] array = new uint[256];
			for (int i = 0; i < 256; i++)
			{
				uint num = (uint)i;
				for (int j = 0; j < 8; j++)
				{
					if ((num & 1U) == 1U)
					{
						num = (num >> 1 ^ polynomial);
					}
					else
					{
						num >>= 1;
					}
				}
				array[i] = num;
			}
			if (polynomial == 3988292384U)
			{
				Crc32.defaultTable = array;
			}
			return array;
		}

		// Token: 0x0600F165 RID: 61797 RVA: 0x0036DD1C File Offset: 0x0036BF1C
		private static uint CalculateHash(uint[] table, uint seed, byte[] buffer, int start, int size)
		{
			uint num = seed;
			for (int i = start; i < size; i++)
			{
				num = (num >> 8 ^ table[(int)((UIntPtr)((uint)buffer[i] ^ (num & 255U)))]);
			}
			return num;
		}

		// Token: 0x0600F166 RID: 61798 RVA: 0x0036DD4C File Offset: 0x0036BF4C
		private byte[] UInt32ToBigEndianBytes(uint x)
		{
			return new byte[]
			{
				(byte)(x >> 24 & 255U),
				(byte)(x >> 16 & 255U),
				(byte)(x >> 8 & 255U),
				(byte)(x & 255U)
			};
		}

		// Token: 0x0400456F RID: 17775
		public const uint DefaultPolynomial = 3988292384U;

		// Token: 0x04004570 RID: 17776
		public const uint DefaultSeed = 4294967295U;

		// Token: 0x04004571 RID: 17777
		private uint hash;

		// Token: 0x04004572 RID: 17778
		private uint seed;

		// Token: 0x04004573 RID: 17779
		private uint[] table;

		// Token: 0x04004574 RID: 17780
		private static uint[] defaultTable;
	}
}
