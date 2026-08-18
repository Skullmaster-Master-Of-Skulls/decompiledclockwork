using System;
using System.IO;

namespace Ionic.Crc
{
	// Token: 0x0200001F RID: 31
	public class CRC32
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000116 RID: 278 RVA: 0x0000AE1C File Offset: 0x0000901C
		public long TotalBytesRead
		{
			get
			{
				return this._TotalBytesRead;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000AE24 File Offset: 0x00009024
		public int Crc32Result
		{
			get
			{
				return (int)(~(int)this._register);
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000AE2D File Offset: 0x0000902D
		public int GetCrc32(Stream input)
		{
			return this.GetCrc32AndCopy(input, null);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000AE38 File Offset: 0x00009038
		public int GetCrc32AndCopy(Stream input, Stream output)
		{
			if (input == null)
			{
				throw new Exception("The input stream must not be null.");
			}
			byte[] array = new byte[8192];
			int num = 8192;
			this._TotalBytesRead = 0L;
			int i = input.Read(array, 0, num);
			if (output != null)
			{
				output.Write(array, 0, i);
			}
			this._TotalBytesRead += (long)i;
			while (i > 0)
			{
				this.SlurpBlock(array, 0, i);
				i = input.Read(array, 0, num);
				if (output != null)
				{
					output.Write(array, 0, i);
				}
				this._TotalBytesRead += (long)i;
			}
			return (int)(~(int)this._register);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000AECC File Offset: 0x000090CC
		public int ComputeCrc32(int W, byte B)
		{
			return this._InternalComputeCrc32((uint)W, B);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000AED6 File Offset: 0x000090D6
		internal int _InternalComputeCrc32(uint W, byte B)
		{
			return (int)(this.crc32Table[(int)((W ^ (uint)B) & 255U)] ^ W >> 8);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000AEEC File Offset: 0x000090EC
		public void SlurpBlock(byte[] block, int offset, int count)
		{
			if (block == null)
			{
				throw new Exception("The data buffer must not be null.");
			}
			for (int i = 0; i < count; i++)
			{
				int num = offset + i;
				byte b = block[num];
				if (this.reverseBits)
				{
					uint num2 = this._register >> 24 ^ (uint)b;
					this._register = (this._register << 8 ^ this.crc32Table[(int)num2]);
				}
				else
				{
					uint num3 = (this._register & 255U) ^ (uint)b;
					this._register = (this._register >> 8 ^ this.crc32Table[(int)num3]);
				}
			}
			this._TotalBytesRead += (long)count;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000AF80 File Offset: 0x00009180
		public void UpdateCRC(byte b)
		{
			if (this.reverseBits)
			{
				uint num = this._register >> 24 ^ (uint)b;
				this._register = (this._register << 8 ^ this.crc32Table[(int)num]);
				return;
			}
			uint num2 = (this._register & 255U) ^ (uint)b;
			this._register = (this._register >> 8 ^ this.crc32Table[(int)num2]);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000AFE0 File Offset: 0x000091E0
		public void UpdateCRC(byte b, int n)
		{
			while (n-- > 0)
			{
				if (this.reverseBits)
				{
					uint num = this._register >> 24 ^ (uint)b;
					this._register = (this._register << 8 ^ this.crc32Table[(int)((num >= 0U) ? num : (num + 256U))]);
				}
				else
				{
					uint num2 = (this._register & 255U) ^ (uint)b;
					this._register = (this._register >> 8 ^ this.crc32Table[(int)((num2 >= 0U) ? num2 : (num2 + 256U))]);
				}
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000B068 File Offset: 0x00009268
		private static uint ReverseBits(uint data)
		{
			uint num = (data & 1431655765U) << 1 | (data >> 1 & 1431655765U);
			num = ((num & 858993459U) << 2 | (num >> 2 & 858993459U));
			num = ((num & 252645135U) << 4 | (num >> 4 & 252645135U));
			return num << 24 | (num & 65280U) << 8 | (num >> 8 & 65280U) | num >> 24;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000B0D4 File Offset: 0x000092D4
		private static byte ReverseBits(byte data)
		{
			int num = (int)data * 131586;
			uint num2 = 17055760U;
			uint num3 = (uint)(num & (int)num2);
			uint num4 = (uint)(num << 2 & (int)((int)num2 << 1));
			return (byte)(16781313U * (num3 + num4) >> 24);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000B108 File Offset: 0x00009308
		private void GenerateLookupTable()
		{
			this.crc32Table = new uint[256];
			byte b = 0;
			do
			{
				uint num = (uint)b;
				for (byte b2 = 8; b2 > 0; b2 -= 1)
				{
					if ((num & 1U) == 1U)
					{
						num = (num >> 1 ^ this.dwPolynomial);
					}
					else
					{
						num >>= 1;
					}
				}
				if (this.reverseBits)
				{
					this.crc32Table[(int)CRC32.ReverseBits(b)] = CRC32.ReverseBits(num);
				}
				else
				{
					this.crc32Table[(int)b] = num;
				}
				b += 1;
			}
			while (b != 0);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000B17C File Offset: 0x0000937C
		private uint gf2_matrix_times(uint[] matrix, uint vec)
		{
			uint num = 0U;
			int num2 = 0;
			while (vec != 0U)
			{
				if ((vec & 1U) == 1U)
				{
					num ^= matrix[num2];
				}
				vec >>= 1;
				num2++;
			}
			return num;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000B1A8 File Offset: 0x000093A8
		private void gf2_matrix_square(uint[] square, uint[] mat)
		{
			for (int i = 0; i < 32; i++)
			{
				square[i] = this.gf2_matrix_times(mat, mat[i]);
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000B1D0 File Offset: 0x000093D0
		public void Combine(int crc, int length)
		{
			uint[] array = new uint[32];
			uint[] array2 = new uint[32];
			if (length == 0)
			{
				return;
			}
			uint num = ~this._register;
			array2[0] = this.dwPolynomial;
			uint num2 = 1U;
			for (int i = 1; i < 32; i++)
			{
				array2[i] = num2;
				num2 <<= 1;
			}
			this.gf2_matrix_square(array, array2);
			this.gf2_matrix_square(array2, array);
			uint num3 = (uint)length;
			do
			{
				this.gf2_matrix_square(array, array2);
				if ((num3 & 1U) == 1U)
				{
					num = this.gf2_matrix_times(array, num);
				}
				num3 >>= 1;
				if (num3 == 0U)
				{
					break;
				}
				this.gf2_matrix_square(array2, array);
				if ((num3 & 1U) == 1U)
				{
					num = this.gf2_matrix_times(array2, num);
				}
				num3 >>= 1;
			}
			while (num3 != 0U);
			num ^= (uint)crc;
			this._register = ~num;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000B287 File Offset: 0x00009487
		public CRC32() : this(false)
		{
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000B290 File Offset: 0x00009490
		public CRC32(bool reverseBits) : this(-306674912, reverseBits)
		{
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000B29E File Offset: 0x0000949E
		public CRC32(int polynomial, bool reverseBits)
		{
			this.reverseBits = reverseBits;
			this.dwPolynomial = (uint)polynomial;
			this.GenerateLookupTable();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000B2C1 File Offset: 0x000094C1
		public void Reset()
		{
			this._register = uint.MaxValue;
		}

		// Token: 0x04000158 RID: 344
		private uint dwPolynomial;

		// Token: 0x04000159 RID: 345
		private long _TotalBytesRead;

		// Token: 0x0400015A RID: 346
		private bool reverseBits;

		// Token: 0x0400015B RID: 347
		private uint[] crc32Table;

		// Token: 0x0400015C RID: 348
		private const int BUFFER_SIZE = 8192;

		// Token: 0x0400015D RID: 349
		private uint _register = uint.MaxValue;
	}
}
