using System;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000554 RID: 1364
	public class RipeMD256Digest : GeneralDigest
	{
		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06002EF5 RID: 12021 RVA: 0x00123673 File Offset: 0x00122673
		public override string AlgorithmName
		{
			get
			{
				return "RIPEMD256";
			}
		}

		// Token: 0x06002EF6 RID: 12022 RVA: 0x0012367A File Offset: 0x0012267A
		public override int GetDigestSize()
		{
			return 32;
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x0012367E File Offset: 0x0012267E
		public RipeMD256Digest()
		{
			this.Reset();
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x0012369C File Offset: 0x0012269C
		public RipeMD256Digest(RipeMD256Digest t) : base(t)
		{
			this.H0 = t.H0;
			this.H1 = t.H1;
			this.H2 = t.H2;
			this.H3 = t.H3;
			this.H4 = t.H4;
			this.H5 = t.H5;
			this.H6 = t.H6;
			this.H7 = t.H7;
			Array.Copy(t.X, 0, this.X, 0, t.X.Length);
			this.xOff = t.xOff;
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x00123744 File Offset: 0x00122744
		internal override void ProcessWord(byte[] input, int inOff)
		{
			this.X[this.xOff++] = ((int)(input[inOff] & byte.MaxValue) | (int)(input[inOff + 1] & byte.MaxValue) << 8 | (int)(input[inOff + 2] & byte.MaxValue) << 16 | (int)(input[inOff + 3] & byte.MaxValue) << 24);
			if (this.xOff == 16)
			{
				this.ProcessBlock();
			}
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x001237AE File Offset: 0x001227AE
		internal override void ProcessLength(long bitLength)
		{
			if (this.xOff > 14)
			{
				this.ProcessBlock();
			}
			this.X[14] = (int)(bitLength & (long)((ulong)-1));
			this.X[15] = (int)((ulong)bitLength >> 32);
		}

		// Token: 0x06002EFB RID: 12027 RVA: 0x001237DC File Offset: 0x001227DC
		private void UnpackWord(int word, byte[] outBytes, int outOff)
		{
			outBytes[outOff] = (byte)word;
			outBytes[outOff + 1] = (byte)((uint)word >> 8);
			outBytes[outOff + 2] = (byte)((uint)word >> 16);
			outBytes[outOff + 3] = (byte)((uint)word >> 24);
		}

		// Token: 0x06002EFC RID: 12028 RVA: 0x00123800 File Offset: 0x00122800
		public override int DoFinal(byte[] output, int outOff)
		{
			base.Finish();
			this.UnpackWord(this.H0, output, outOff);
			this.UnpackWord(this.H1, output, outOff + 4);
			this.UnpackWord(this.H2, output, outOff + 8);
			this.UnpackWord(this.H3, output, outOff + 12);
			this.UnpackWord(this.H4, output, outOff + 16);
			this.UnpackWord(this.H5, output, outOff + 20);
			this.UnpackWord(this.H6, output, outOff + 24);
			this.UnpackWord(this.H7, output, outOff + 28);
			this.Reset();
			return 32;
		}

		// Token: 0x06002EFD RID: 12029 RVA: 0x001238A0 File Offset: 0x001228A0
		public override void Reset()
		{
			base.Reset();
			this.H0 = 1732584193;
			this.H1 = -271733879;
			this.H2 = -1732584194;
			this.H3 = 271733878;
			this.H4 = 1985229328;
			this.H5 = -19088744;
			this.H6 = -1985229329;
			this.H7 = 19088743;
			this.xOff = 0;
			for (int num = 0; num != this.X.Length; num++)
			{
				this.X[num] = 0;
			}
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x0012392E File Offset: 0x0012292E
		private int RL(int x, int n)
		{
			return x << n | (int)((uint)x >> 32 - n);
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x00123940 File Offset: 0x00122940
		private int F1(int x, int y, int z)
		{
			return x ^ y ^ z;
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x00123947 File Offset: 0x00122947
		private int F2(int x, int y, int z)
		{
			return (x & y) | (~x & z);
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x00123951 File Offset: 0x00122951
		private int F3(int x, int y, int z)
		{
			return (x | ~y) ^ z;
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x00123959 File Offset: 0x00122959
		private int F4(int x, int y, int z)
		{
			return (x & z) | (y & ~z);
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x00123963 File Offset: 0x00122963
		private int F1(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F1(b, c, d) + x, s);
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x0012397C File Offset: 0x0012297C
		private int F2(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F2(b, c, d) + x + 1518500249, s);
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x0012399B File Offset: 0x0012299B
		private int F3(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F3(b, c, d) + x + 1859775393, s);
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x001239BA File Offset: 0x001229BA
		private int F4(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F4(b, c, d) + x + -1894007588, s);
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x001239D9 File Offset: 0x001229D9
		private int FF1(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F1(b, c, d) + x, s);
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x001239F2 File Offset: 0x001229F2
		private int FF2(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F2(b, c, d) + x + 1836072691, s);
		}

		// Token: 0x06002F09 RID: 12041 RVA: 0x00123A11 File Offset: 0x00122A11
		private int FF3(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F3(b, c, d) + x + 1548603684, s);
		}

		// Token: 0x06002F0A RID: 12042 RVA: 0x00123A30 File Offset: 0x00122A30
		private int FF4(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F4(b, c, d) + x + 1352829926, s);
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x00123A50 File Offset: 0x00122A50
		internal override void ProcessBlock()
		{
			int num = this.H0;
			int num2 = this.H1;
			int num3 = this.H2;
			int num4 = this.H3;
			int num5 = this.H4;
			int num6 = this.H5;
			int num7 = this.H6;
			int num8 = this.H7;
			num = this.F1(num, num2, num3, num4, this.X[0], 11);
			num4 = this.F1(num4, num, num2, num3, this.X[1], 14);
			num3 = this.F1(num3, num4, num, num2, this.X[2], 15);
			num2 = this.F1(num2, num3, num4, num, this.X[3], 12);
			num = this.F1(num, num2, num3, num4, this.X[4], 5);
			num4 = this.F1(num4, num, num2, num3, this.X[5], 8);
			num3 = this.F1(num3, num4, num, num2, this.X[6], 7);
			num2 = this.F1(num2, num3, num4, num, this.X[7], 9);
			num = this.F1(num, num2, num3, num4, this.X[8], 11);
			num4 = this.F1(num4, num, num2, num3, this.X[9], 13);
			num3 = this.F1(num3, num4, num, num2, this.X[10], 14);
			num2 = this.F1(num2, num3, num4, num, this.X[11], 15);
			num = this.F1(num, num2, num3, num4, this.X[12], 6);
			num4 = this.F1(num4, num, num2, num3, this.X[13], 7);
			num3 = this.F1(num3, num4, num, num2, this.X[14], 9);
			num2 = this.F1(num2, num3, num4, num, this.X[15], 8);
			num5 = this.FF4(num5, num6, num7, num8, this.X[5], 8);
			num8 = this.FF4(num8, num5, num6, num7, this.X[14], 9);
			num7 = this.FF4(num7, num8, num5, num6, this.X[7], 9);
			num6 = this.FF4(num6, num7, num8, num5, this.X[0], 11);
			num5 = this.FF4(num5, num6, num7, num8, this.X[9], 13);
			num8 = this.FF4(num8, num5, num6, num7, this.X[2], 15);
			num7 = this.FF4(num7, num8, num5, num6, this.X[11], 15);
			num6 = this.FF4(num6, num7, num8, num5, this.X[4], 5);
			num5 = this.FF4(num5, num6, num7, num8, this.X[13], 7);
			num8 = this.FF4(num8, num5, num6, num7, this.X[6], 7);
			num7 = this.FF4(num7, num8, num5, num6, this.X[15], 8);
			num6 = this.FF4(num6, num7, num8, num5, this.X[8], 11);
			num5 = this.FF4(num5, num6, num7, num8, this.X[1], 14);
			num8 = this.FF4(num8, num5, num6, num7, this.X[10], 14);
			num7 = this.FF4(num7, num8, num5, num6, this.X[3], 12);
			num6 = this.FF4(num6, num7, num8, num5, this.X[12], 6);
			int num9 = num;
			num = num5;
			num5 = num9;
			num = this.F2(num, num2, num3, num4, this.X[7], 7);
			num4 = this.F2(num4, num, num2, num3, this.X[4], 6);
			num3 = this.F2(num3, num4, num, num2, this.X[13], 8);
			num2 = this.F2(num2, num3, num4, num, this.X[1], 13);
			num = this.F2(num, num2, num3, num4, this.X[10], 11);
			num4 = this.F2(num4, num, num2, num3, this.X[6], 9);
			num3 = this.F2(num3, num4, num, num2, this.X[15], 7);
			num2 = this.F2(num2, num3, num4, num, this.X[3], 15);
			num = this.F2(num, num2, num3, num4, this.X[12], 7);
			num4 = this.F2(num4, num, num2, num3, this.X[0], 12);
			num3 = this.F2(num3, num4, num, num2, this.X[9], 15);
			num2 = this.F2(num2, num3, num4, num, this.X[5], 9);
			num = this.F2(num, num2, num3, num4, this.X[2], 11);
			num4 = this.F2(num4, num, num2, num3, this.X[14], 7);
			num3 = this.F2(num3, num4, num, num2, this.X[11], 13);
			num2 = this.F2(num2, num3, num4, num, this.X[8], 12);
			num5 = this.FF3(num5, num6, num7, num8, this.X[6], 9);
			num8 = this.FF3(num8, num5, num6, num7, this.X[11], 13);
			num7 = this.FF3(num7, num8, num5, num6, this.X[3], 15);
			num6 = this.FF3(num6, num7, num8, num5, this.X[7], 7);
			num5 = this.FF3(num5, num6, num7, num8, this.X[0], 12);
			num8 = this.FF3(num8, num5, num6, num7, this.X[13], 8);
			num7 = this.FF3(num7, num8, num5, num6, this.X[5], 9);
			num6 = this.FF3(num6, num7, num8, num5, this.X[10], 11);
			num5 = this.FF3(num5, num6, num7, num8, this.X[14], 7);
			num8 = this.FF3(num8, num5, num6, num7, this.X[15], 7);
			num7 = this.FF3(num7, num8, num5, num6, this.X[8], 12);
			num6 = this.FF3(num6, num7, num8, num5, this.X[12], 7);
			num5 = this.FF3(num5, num6, num7, num8, this.X[4], 6);
			num8 = this.FF3(num8, num5, num6, num7, this.X[9], 15);
			num7 = this.FF3(num7, num8, num5, num6, this.X[1], 13);
			num6 = this.FF3(num6, num7, num8, num5, this.X[2], 11);
			num9 = num2;
			num2 = num6;
			num6 = num9;
			num = this.F3(num, num2, num3, num4, this.X[3], 11);
			num4 = this.F3(num4, num, num2, num3, this.X[10], 13);
			num3 = this.F3(num3, num4, num, num2, this.X[14], 6);
			num2 = this.F3(num2, num3, num4, num, this.X[4], 7);
			num = this.F3(num, num2, num3, num4, this.X[9], 14);
			num4 = this.F3(num4, num, num2, num3, this.X[15], 9);
			num3 = this.F3(num3, num4, num, num2, this.X[8], 13);
			num2 = this.F3(num2, num3, num4, num, this.X[1], 15);
			num = this.F3(num, num2, num3, num4, this.X[2], 14);
			num4 = this.F3(num4, num, num2, num3, this.X[7], 8);
			num3 = this.F3(num3, num4, num, num2, this.X[0], 13);
			num2 = this.F3(num2, num3, num4, num, this.X[6], 6);
			num = this.F3(num, num2, num3, num4, this.X[13], 5);
			num4 = this.F3(num4, num, num2, num3, this.X[11], 12);
			num3 = this.F3(num3, num4, num, num2, this.X[5], 7);
			num2 = this.F3(num2, num3, num4, num, this.X[12], 5);
			num5 = this.FF2(num5, num6, num7, num8, this.X[15], 9);
			num8 = this.FF2(num8, num5, num6, num7, this.X[5], 7);
			num7 = this.FF2(num7, num8, num5, num6, this.X[1], 15);
			num6 = this.FF2(num6, num7, num8, num5, this.X[3], 11);
			num5 = this.FF2(num5, num6, num7, num8, this.X[7], 8);
			num8 = this.FF2(num8, num5, num6, num7, this.X[14], 6);
			num7 = this.FF2(num7, num8, num5, num6, this.X[6], 6);
			num6 = this.FF2(num6, num7, num8, num5, this.X[9], 14);
			num5 = this.FF2(num5, num6, num7, num8, this.X[11], 12);
			num8 = this.FF2(num8, num5, num6, num7, this.X[8], 13);
			num7 = this.FF2(num7, num8, num5, num6, this.X[12], 5);
			num6 = this.FF2(num6, num7, num8, num5, this.X[2], 14);
			num5 = this.FF2(num5, num6, num7, num8, this.X[10], 13);
			num8 = this.FF2(num8, num5, num6, num7, this.X[0], 13);
			num7 = this.FF2(num7, num8, num5, num6, this.X[4], 7);
			num6 = this.FF2(num6, num7, num8, num5, this.X[13], 5);
			num9 = num3;
			num3 = num7;
			num7 = num9;
			num = this.F4(num, num2, num3, num4, this.X[1], 11);
			num4 = this.F4(num4, num, num2, num3, this.X[9], 12);
			num3 = this.F4(num3, num4, num, num2, this.X[11], 14);
			num2 = this.F4(num2, num3, num4, num, this.X[10], 15);
			num = this.F4(num, num2, num3, num4, this.X[0], 14);
			num4 = this.F4(num4, num, num2, num3, this.X[8], 15);
			num3 = this.F4(num3, num4, num, num2, this.X[12], 9);
			num2 = this.F4(num2, num3, num4, num, this.X[4], 8);
			num = this.F4(num, num2, num3, num4, this.X[13], 9);
			num4 = this.F4(num4, num, num2, num3, this.X[3], 14);
			num3 = this.F4(num3, num4, num, num2, this.X[7], 5);
			num2 = this.F4(num2, num3, num4, num, this.X[15], 6);
			num = this.F4(num, num2, num3, num4, this.X[14], 8);
			num4 = this.F4(num4, num, num2, num3, this.X[5], 6);
			num3 = this.F4(num3, num4, num, num2, this.X[6], 5);
			num2 = this.F4(num2, num3, num4, num, this.X[2], 12);
			num5 = this.FF1(num5, num6, num7, num8, this.X[8], 15);
			num8 = this.FF1(num8, num5, num6, num7, this.X[6], 5);
			num7 = this.FF1(num7, num8, num5, num6, this.X[4], 8);
			num6 = this.FF1(num6, num7, num8, num5, this.X[1], 11);
			num5 = this.FF1(num5, num6, num7, num8, this.X[3], 14);
			num8 = this.FF1(num8, num5, num6, num7, this.X[11], 14);
			num7 = this.FF1(num7, num8, num5, num6, this.X[15], 6);
			num6 = this.FF1(num6, num7, num8, num5, this.X[0], 14);
			num5 = this.FF1(num5, num6, num7, num8, this.X[5], 6);
			num8 = this.FF1(num8, num5, num6, num7, this.X[12], 9);
			num7 = this.FF1(num7, num8, num5, num6, this.X[2], 12);
			num6 = this.FF1(num6, num7, num8, num5, this.X[13], 9);
			num5 = this.FF1(num5, num6, num7, num8, this.X[9], 12);
			num8 = this.FF1(num8, num5, num6, num7, this.X[7], 5);
			num7 = this.FF1(num7, num8, num5, num6, this.X[10], 15);
			num6 = this.FF1(num6, num7, num8, num5, this.X[14], 8);
			num9 = num4;
			num4 = num8;
			num8 = num9;
			this.H0 += num;
			this.H1 += num2;
			this.H2 += num3;
			this.H3 += num4;
			this.H4 += num5;
			this.H5 += num6;
			this.H6 += num7;
			this.H7 += num8;
			this.xOff = 0;
			for (int num10 = 0; num10 != this.X.Length; num10++)
			{
				this.X[num10] = 0;
			}
		}

		// Token: 0x04002051 RID: 8273
		private const int DigestLength = 32;

		// Token: 0x04002052 RID: 8274
		private int H0;

		// Token: 0x04002053 RID: 8275
		private int H1;

		// Token: 0x04002054 RID: 8276
		private int H2;

		// Token: 0x04002055 RID: 8277
		private int H3;

		// Token: 0x04002056 RID: 8278
		private int H4;

		// Token: 0x04002057 RID: 8279
		private int H5;

		// Token: 0x04002058 RID: 8280
		private int H6;

		// Token: 0x04002059 RID: 8281
		private int H7;

		// Token: 0x0400205A RID: 8282
		private int[] X = new int[16];

		// Token: 0x0400205B RID: 8283
		private int xOff;
	}
}
