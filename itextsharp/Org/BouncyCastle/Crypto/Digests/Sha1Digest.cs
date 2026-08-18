using System;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000025 RID: 37
	public class Sha1Digest : GeneralDigest
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x000072F4 File Offset: 0x000062F4
		public Sha1Digest()
		{
			this.Reset();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00007310 File Offset: 0x00006310
		public Sha1Digest(Sha1Digest t) : base(t)
		{
			this.H1 = t.H1;
			this.H2 = t.H2;
			this.H3 = t.H3;
			this.H4 = t.H4;
			this.H5 = t.H5;
			Array.Copy(t.X, 0, this.X, 0, t.X.Length);
			this.xOff = t.xOff;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00007394 File Offset: 0x00006394
		public override string AlgorithmName
		{
			get
			{
				return "SHA-1";
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000739B File Offset: 0x0000639B
		public override int GetDigestSize()
		{
			return 20;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000073A0 File Offset: 0x000063A0
		internal override void ProcessWord(byte[] input, int inOff)
		{
			this.X[this.xOff] = Pack.BE_To_UInt32(input, inOff);
			if (++this.xOff == 16)
			{
				this.ProcessBlock();
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000073DC File Offset: 0x000063DC
		internal override void ProcessLength(long bitLength)
		{
			if (this.xOff > 14)
			{
				this.ProcessBlock();
			}
			this.X[14] = (uint)((ulong)bitLength >> 32);
			this.X[15] = (uint)bitLength;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00007408 File Offset: 0x00006408
		public override int DoFinal(byte[] output, int outOff)
		{
			base.Finish();
			Pack.UInt32_To_BE(this.H1, output, outOff);
			Pack.UInt32_To_BE(this.H2, output, outOff + 4);
			Pack.UInt32_To_BE(this.H3, output, outOff + 8);
			Pack.UInt32_To_BE(this.H4, output, outOff + 12);
			Pack.UInt32_To_BE(this.H5, output, outOff + 16);
			this.Reset();
			return 20;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00007470 File Offset: 0x00006470
		public override void Reset()
		{
			base.Reset();
			this.H1 = 1732584193U;
			this.H2 = 4023233417U;
			this.H3 = 2562383102U;
			this.H4 = 271733878U;
			this.H5 = 3285377520U;
			this.xOff = 0;
			Array.Clear(this.X, 0, this.X.Length);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000074D5 File Offset: 0x000064D5
		private static uint F(uint u, uint v, uint w)
		{
			return (u & v) | (~u & w);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000074DF File Offset: 0x000064DF
		private static uint H(uint u, uint v, uint w)
		{
			return u ^ v ^ w;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000074E6 File Offset: 0x000064E6
		private static uint G(uint u, uint v, uint w)
		{
			return (u & v) | (u & w) | (v & w);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000074F4 File Offset: 0x000064F4
		internal override void ProcessBlock()
		{
			for (int i = 16; i < 80; i++)
			{
				uint num = this.X[i - 3] ^ this.X[i - 8] ^ this.X[i - 14] ^ this.X[i - 16];
				this.X[i] = (num << 1 | num >> 31);
			}
			uint num2 = this.H1;
			uint num3 = this.H2;
			uint num4 = this.H3;
			uint num5 = this.H4;
			uint num6 = this.H5;
			int num7 = 0;
			for (int j = 0; j < 4; j++)
			{
				num6 += (num2 << 5 | num2 >> 27) + Sha1Digest.F(num3, num4, num5) + this.X[num7++] + 1518500249U;
				num3 = (num3 << 30 | num3 >> 2);
				num5 += (num6 << 5 | num6 >> 27) + Sha1Digest.F(num2, num3, num4) + this.X[num7++] + 1518500249U;
				num2 = (num2 << 30 | num2 >> 2);
				num4 += (num5 << 5 | num5 >> 27) + Sha1Digest.F(num6, num2, num3) + this.X[num7++] + 1518500249U;
				num6 = (num6 << 30 | num6 >> 2);
				num3 += (num4 << 5 | num4 >> 27) + Sha1Digest.F(num5, num6, num2) + this.X[num7++] + 1518500249U;
				num5 = (num5 << 30 | num5 >> 2);
				num2 += (num3 << 5 | num3 >> 27) + Sha1Digest.F(num4, num5, num6) + this.X[num7++] + 1518500249U;
				num4 = (num4 << 30 | num4 >> 2);
			}
			for (int k = 0; k < 4; k++)
			{
				num6 += (num2 << 5 | num2 >> 27) + Sha1Digest.H(num3, num4, num5) + this.X[num7++] + 1859775393U;
				num3 = (num3 << 30 | num3 >> 2);
				num5 += (num6 << 5 | num6 >> 27) + Sha1Digest.H(num2, num3, num4) + this.X[num7++] + 1859775393U;
				num2 = (num2 << 30 | num2 >> 2);
				num4 += (num5 << 5 | num5 >> 27) + Sha1Digest.H(num6, num2, num3) + this.X[num7++] + 1859775393U;
				num6 = (num6 << 30 | num6 >> 2);
				num3 += (num4 << 5 | num4 >> 27) + Sha1Digest.H(num5, num6, num2) + this.X[num7++] + 1859775393U;
				num5 = (num5 << 30 | num5 >> 2);
				num2 += (num3 << 5 | num3 >> 27) + Sha1Digest.H(num4, num5, num6) + this.X[num7++] + 1859775393U;
				num4 = (num4 << 30 | num4 >> 2);
			}
			for (int l = 0; l < 4; l++)
			{
				num6 += (num2 << 5 | num2 >> 27) + Sha1Digest.G(num3, num4, num5) + this.X[num7++] + 2400959708U;
				num3 = (num3 << 30 | num3 >> 2);
				num5 += (num6 << 5 | num6 >> 27) + Sha1Digest.G(num2, num3, num4) + this.X[num7++] + 2400959708U;
				num2 = (num2 << 30 | num2 >> 2);
				num4 += (num5 << 5 | num5 >> 27) + Sha1Digest.G(num6, num2, num3) + this.X[num7++] + 2400959708U;
				num6 = (num6 << 30 | num6 >> 2);
				num3 += (num4 << 5 | num4 >> 27) + Sha1Digest.G(num5, num6, num2) + this.X[num7++] + 2400959708U;
				num5 = (num5 << 30 | num5 >> 2);
				num2 += (num3 << 5 | num3 >> 27) + Sha1Digest.G(num4, num5, num6) + this.X[num7++] + 2400959708U;
				num4 = (num4 << 30 | num4 >> 2);
			}
			for (int m = 0; m < 4; m++)
			{
				num6 += (num2 << 5 | num2 >> 27) + Sha1Digest.H(num3, num4, num5) + this.X[num7++] + 3395469782U;
				num3 = (num3 << 30 | num3 >> 2);
				num5 += (num6 << 5 | num6 >> 27) + Sha1Digest.H(num2, num3, num4) + this.X[num7++] + 3395469782U;
				num2 = (num2 << 30 | num2 >> 2);
				num4 += (num5 << 5 | num5 >> 27) + Sha1Digest.H(num6, num2, num3) + this.X[num7++] + 3395469782U;
				num6 = (num6 << 30 | num6 >> 2);
				num3 += (num4 << 5 | num4 >> 27) + Sha1Digest.H(num5, num6, num2) + this.X[num7++] + 3395469782U;
				num5 = (num5 << 30 | num5 >> 2);
				num2 += (num3 << 5 | num3 >> 27) + Sha1Digest.H(num4, num5, num6) + this.X[num7++] + 3395469782U;
				num4 = (num4 << 30 | num4 >> 2);
			}
			this.H1 += num2;
			this.H2 += num3;
			this.H3 += num4;
			this.H4 += num5;
			this.H5 += num6;
			this.xOff = 0;
			Array.Clear(this.X, 0, 16);
		}

		// Token: 0x04000075 RID: 117
		private const int DigestLength = 20;

		// Token: 0x04000076 RID: 118
		private const uint Y1 = 1518500249U;

		// Token: 0x04000077 RID: 119
		private const uint Y2 = 1859775393U;

		// Token: 0x04000078 RID: 120
		private const uint Y3 = 2400959708U;

		// Token: 0x04000079 RID: 121
		private const uint Y4 = 3395469782U;

		// Token: 0x0400007A RID: 122
		private uint H1;

		// Token: 0x0400007B RID: 123
		private uint H2;

		// Token: 0x0400007C RID: 124
		private uint H3;

		// Token: 0x0400007D RID: 125
		private uint H4;

		// Token: 0x0400007E RID: 126
		private uint H5;

		// Token: 0x0400007F RID: 127
		private uint[] X = new uint[80];

		// Token: 0x04000080 RID: 128
		private int xOff;
	}
}
