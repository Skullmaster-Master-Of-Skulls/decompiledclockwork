using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI.Common
{
	// Token: 0x0200014B RID: 331
	internal class SafeMD5
	{
		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x0002EA24 File Offset: 0x0002CC24
		// (set) Token: 0x06000D20 RID: 3360 RVA: 0x0002EA68 File Offset: 0x0002CC68
		public string Value
		{
			get
			{
				char[] array = new char[this.m_byteInput.Length];
				for (int i = 0; i < this.m_byteInput.Length; i++)
				{
					array[i] = (char)this.m_byteInput[i];
				}
				return new string(array);
			}
			set
			{
				this.m_byteInput = new byte[value.Length];
				for (int i = 0; i < value.Length; i++)
				{
					this.m_byteInput[i] = (byte)value[i];
				}
				this.dgFingerPrint = this.CalculateMD5Value();
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x0002EAB4 File Offset: 0x0002CCB4
		// (set) Token: 0x06000D22 RID: 3362 RVA: 0x0002EAF0 File Offset: 0x0002CCF0
		public byte[] ValueAsByte
		{
			get
			{
				byte[] array = new byte[this.m_byteInput.Length];
				for (int i = 0; i < this.m_byteInput.Length; i++)
				{
					array[i] = this.m_byteInput[i];
				}
				return array;
			}
			set
			{
				this.m_byteInput = new byte[value.Length];
				for (int i = 0; i < value.Length; i++)
				{
					this.m_byteInput[i] = value[i];
				}
				this.dgFingerPrint = this.CalculateMD5Value();
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x0002EB30 File Offset: 0x0002CD30
		public string FingerPrint
		{
			get
			{
				return this.dgFingerPrint.ToString();
			}
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0002EB3D File Offset: 0x0002CD3D
		public SafeMD5()
		{
			this.Value = "";
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0002EB60 File Offset: 0x0002CD60
		protected Digest CalculateMD5Value()
		{
			Digest digest = new Digest();
			byte[] array = this.CreatePaddedBuffer();
			uint num = (uint)(array.Length * 8 / 32);
			for (uint num2 = 0U; num2 < num / 16U; num2 += 1U)
			{
				this.CopyBlock(array, num2);
				this.PerformTransformation(ref digest.A, ref digest.B, ref digest.C, ref digest.D);
			}
			return digest;
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0002EBB9 File Offset: 0x0002CDB9
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		protected void TransF(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
		{
			a = b + SafeMD5Helper.RotateLeft(a + ((b & c) | (~b & d)) + this.X[(int)((UIntPtr)k)] + SafeMD5.T[(int)((UIntPtr)(i - 1U))], s);
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0002EBE9 File Offset: 0x0002CDE9
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		protected void TransG(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
		{
			a = b + SafeMD5Helper.RotateLeft(a + ((b & d) | (c & ~d)) + this.X[(int)((UIntPtr)k)] + SafeMD5.T[(int)((UIntPtr)(i - 1U))], s);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0002EC1A File Offset: 0x0002CE1A
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		protected void TransH(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
		{
			a = b + SafeMD5Helper.RotateLeft(a + (b ^ c ^ d) + this.X[(int)((UIntPtr)k)] + SafeMD5.T[(int)((UIntPtr)(i - 1U))], s);
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0002EC47 File Offset: 0x0002CE47
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		protected void TransI(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
		{
			a = b + SafeMD5Helper.RotateLeft(a + (c ^ (b | ~d)) + this.X[(int)((UIntPtr)k)] + SafeMD5.T[(int)((UIntPtr)(i - 1U))], s);
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x0002EC78 File Offset: 0x0002CE78
		protected void PerformTransformation(ref uint A, ref uint B, ref uint C, ref uint D)
		{
			uint num = A;
			uint num2 = B;
			uint num3 = C;
			uint num4 = D;
			this.TransF(ref A, B, C, D, 0U, 7, 1U);
			this.TransF(ref D, A, B, C, 1U, 12, 2U);
			this.TransF(ref C, D, A, B, 2U, 17, 3U);
			this.TransF(ref B, C, D, A, 3U, 22, 4U);
			this.TransF(ref A, B, C, D, 4U, 7, 5U);
			this.TransF(ref D, A, B, C, 5U, 12, 6U);
			this.TransF(ref C, D, A, B, 6U, 17, 7U);
			this.TransF(ref B, C, D, A, 7U, 22, 8U);
			this.TransF(ref A, B, C, D, 8U, 7, 9U);
			this.TransF(ref D, A, B, C, 9U, 12, 10U);
			this.TransF(ref C, D, A, B, 10U, 17, 11U);
			this.TransF(ref B, C, D, A, 11U, 22, 12U);
			this.TransF(ref A, B, C, D, 12U, 7, 13U);
			this.TransF(ref D, A, B, C, 13U, 12, 14U);
			this.TransF(ref C, D, A, B, 14U, 17, 15U);
			this.TransF(ref B, C, D, A, 15U, 22, 16U);
			this.TransG(ref A, B, C, D, 1U, 5, 17U);
			this.TransG(ref D, A, B, C, 6U, 9, 18U);
			this.TransG(ref C, D, A, B, 11U, 14, 19U);
			this.TransG(ref B, C, D, A, 0U, 20, 20U);
			this.TransG(ref A, B, C, D, 5U, 5, 21U);
			this.TransG(ref D, A, B, C, 10U, 9, 22U);
			this.TransG(ref C, D, A, B, 15U, 14, 23U);
			this.TransG(ref B, C, D, A, 4U, 20, 24U);
			this.TransG(ref A, B, C, D, 9U, 5, 25U);
			this.TransG(ref D, A, B, C, 14U, 9, 26U);
			this.TransG(ref C, D, A, B, 3U, 14, 27U);
			this.TransG(ref B, C, D, A, 8U, 20, 28U);
			this.TransG(ref A, B, C, D, 13U, 5, 29U);
			this.TransG(ref D, A, B, C, 2U, 9, 30U);
			this.TransG(ref C, D, A, B, 7U, 14, 31U);
			this.TransG(ref B, C, D, A, 12U, 20, 32U);
			this.TransH(ref A, B, C, D, 5U, 4, 33U);
			this.TransH(ref D, A, B, C, 8U, 11, 34U);
			this.TransH(ref C, D, A, B, 11U, 16, 35U);
			this.TransH(ref B, C, D, A, 14U, 23, 36U);
			this.TransH(ref A, B, C, D, 1U, 4, 37U);
			this.TransH(ref D, A, B, C, 4U, 11, 38U);
			this.TransH(ref C, D, A, B, 7U, 16, 39U);
			this.TransH(ref B, C, D, A, 10U, 23, 40U);
			this.TransH(ref A, B, C, D, 13U, 4, 41U);
			this.TransH(ref D, A, B, C, 0U, 11, 42U);
			this.TransH(ref C, D, A, B, 3U, 16, 43U);
			this.TransH(ref B, C, D, A, 6U, 23, 44U);
			this.TransH(ref A, B, C, D, 9U, 4, 45U);
			this.TransH(ref D, A, B, C, 12U, 11, 46U);
			this.TransH(ref C, D, A, B, 15U, 16, 47U);
			this.TransH(ref B, C, D, A, 2U, 23, 48U);
			this.TransI(ref A, B, C, D, 0U, 6, 49U);
			this.TransI(ref D, A, B, C, 7U, 10, 50U);
			this.TransI(ref C, D, A, B, 14U, 15, 51U);
			this.TransI(ref B, C, D, A, 5U, 21, 52U);
			this.TransI(ref A, B, C, D, 12U, 6, 53U);
			this.TransI(ref D, A, B, C, 3U, 10, 54U);
			this.TransI(ref C, D, A, B, 10U, 15, 55U);
			this.TransI(ref B, C, D, A, 1U, 21, 56U);
			this.TransI(ref A, B, C, D, 8U, 6, 57U);
			this.TransI(ref D, A, B, C, 15U, 10, 58U);
			this.TransI(ref C, D, A, B, 6U, 15, 59U);
			this.TransI(ref B, C, D, A, 13U, 21, 60U);
			this.TransI(ref A, B, C, D, 4U, 6, 61U);
			this.TransI(ref D, A, B, C, 11U, 10, 62U);
			this.TransI(ref C, D, A, B, 2U, 15, 63U);
			this.TransI(ref B, C, D, A, 9U, 21, 64U);
			A += num;
			B += num2;
			C += num3;
			D += num4;
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0002F170 File Offset: 0x0002D370
		protected byte[] CreatePaddedBuffer()
		{
			int num = 448 - this.m_byteInput.Length * 8 % 512;
			uint num2 = (uint)((num + 512) % 512);
			if (num2 == 0U)
			{
				num2 = 512U;
			}
			uint num3 = (uint)((long)this.m_byteInput.Length + (long)((ulong)(num2 / 8U)) + 8L);
			ulong num4 = (ulong)((long)this.m_byteInput.Length * 8L);
			byte[] array = new byte[num3];
			for (int i = 0; i < this.m_byteInput.Length; i++)
			{
				array[i] = this.m_byteInput[i];
			}
			byte[] array2 = array;
			int num5 = this.m_byteInput.Length;
			array2[num5] |= 128;
			for (int j = 8; j > 0; j--)
			{
				array[(int)(checked((IntPtr)(unchecked((ulong)num3 - (ulong)((long)j)))))] = (byte)(num4 >> (8 - j) * 8 & 255UL);
			}
			return array;
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0002F248 File Offset: 0x0002D448
		protected void CopyBlock(byte[] bMsg, uint block)
		{
			block <<= 6;
			for (uint num = 0U; num < 61U; num += 4U)
			{
				this.X[(int)((UIntPtr)(num >> 2))] = (uint)((int)bMsg[(int)((UIntPtr)(block + (num + 3U)))] << 24 | (int)bMsg[(int)((UIntPtr)(block + (num + 2U)))] << 16 | (int)bMsg[(int)((UIntPtr)(block + (num + 1U)))] << 8 | (int)bMsg[(int)((UIntPtr)(block + num))]);
			}
		}

		// Token: 0x0400033A RID: 826
		protected static readonly uint[] T = new uint[]
		{
			3614090360U,
			3905402710U,
			606105819U,
			3250441966U,
			4118548399U,
			1200080426U,
			2821735955U,
			4249261313U,
			1770035416U,
			2336552879U,
			4294925233U,
			2304563134U,
			1804603682U,
			4254626195U,
			2792965006U,
			1236535329U,
			4129170786U,
			3225465664U,
			643717713U,
			3921069994U,
			3593408605U,
			38016083U,
			3634488961U,
			3889429448U,
			568446438U,
			3275163606U,
			4107603335U,
			1163531501U,
			2850285829U,
			4243563512U,
			1735328473U,
			2368359562U,
			4294588738U,
			2272392833U,
			1839030562U,
			4259657740U,
			2763975236U,
			1272893353U,
			4139469664U,
			3200236656U,
			681279174U,
			3936430074U,
			3572445317U,
			76029189U,
			3654602809U,
			3873151461U,
			530742520U,
			3299628645U,
			4096336452U,
			1126891415U,
			2878612391U,
			4237533241U,
			1700485571U,
			2399980690U,
			4293915773U,
			2240044497U,
			1873313359U,
			4264355552U,
			2734768916U,
			1309151649U,
			4149444226U,
			3174756917U,
			718787259U,
			3951481745U
		};

		// Token: 0x0400033B RID: 827
		protected uint[] X = new uint[16];

		// Token: 0x0400033C RID: 828
		protected Digest dgFingerPrint;

		// Token: 0x0400033D RID: 829
		protected byte[] m_byteInput;
	}
}
